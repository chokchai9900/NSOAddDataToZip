using MongoDB.Driver;
using Newtonsoft.Json;
using NSOAddDataToMongo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;

namespace NSOAddDataToMongo

{
    class Program
    {
        static void Main(string[] args)
        {
            var countCopy = 0;
            var countBlobdata = 0;
            const string DESTINATION_PATH = @"E:\ContranZip";
            const string ZIP_PATH = @"F:\";

            string connectString = "mongodb://firstclass:Th35F1rstCla55@mongodbnewzfdggw5bmqhbq-vm0.southeastasia.cloudapp.azure.com/water";
            var mongo = new MongoClient(connectString);
            var database = mongo.GetDatabase("water");
            var CollectionBlobExist = database.GetCollection<BlobExistModel>("BlobExist");
            var CollectionZipList = database.GetCollection<ListlZipModel>("Ziplist");

            Console.WriteLine($"Database Connecting");

            var listBlobExist = CollectionBlobExist.Find(it => it.IsRun == false).ToList();
            var total = listBlobExist.Count;
            Console.WriteLine("Get data done !");

            var ziplistPATH = CollectionZipList.Find(it => it.IsRun == false).ToList().Select(x => $"{ZIP_PATH}{x._id}").ToList();
            Console.WriteLine($"Total zip : {ziplistPATH.Count}");

            foreach (var zipName in ziplistPATH)
            {
                Console.WriteLine($"Now Zip Name : {zipName}");

                using (ZipArchive archive = ZipFile.OpenRead(zipName))
                {
                    var zipEntries = archive.Entries
                        .Where(it => it.FullName.Split("/").LastOrDefault().StartsWith("bld1v") ||
                        it.FullName.Split("/").LastOrDefault().StartsWith("unt1v") ||
                        it.FullName.Split("/").LastOrDefault().StartsWith("com1v"))
                        .ToList();
                    var listDataContainer = zipEntries.Select(it =>
                    {
                        var pathSplit = it.FullName.Split("/").ToList();
                        var dataBlob = new List<string>();
                        if (it.FullName.StartsWith("m"))
                        {
                            dataBlob = pathSplit;
                        }
                        else
                        {
                            dataBlob = pathSplit.Skip(pathSplit.Count - 2).Take(2).ToList();
                        }
                        return new
                        {
                            containerName = dataBlob[0],
                            blobName = dataBlob[1]
                        };
                    })
                    .GroupBy(it => it.containerName)
                    .Select(it => new
                    {
                        containerName = it.Key,
                        listBlob = it.Select(x => x.blobName).ToList()
                    })
                    .OrderByDescending(it => it.containerName)
                    .ToList();

                    foreach (var containerHDD in listDataContainer)
                    {
                        var containerDB = listBlobExist.FirstOrDefault(it => it.listBlob.All(x => containerHDD.listBlob.Contains(x)));
                        
                        if (containerDB != null)
                        {
                            var currentDirectiry = @$"{DESTINATION_PATH}\{containerHDD.containerName}";
                            var x = containerHDD.containerName;
                            var result = from currEntry in archive.Entries
                                         where Path.GetDirectoryName(currEntry.FullName).Contains(containerHDD.containerName)
                                         where !String.IsNullOrEmpty(currEntry.Name)
                                         select currEntry;
                            var countresult = 0;
                             foreach (ZipArchiveEntry entry in result)
                             {
                                if (!Directory.Exists(currentDirectiry))
                                {
                                    Directory.CreateDirectory(currentDirectiry);
                                }
                                entry.ExtractToFile(Path.Combine(currentDirectiry, entry.Name));
                                countresult++;
                                //Console.Write($"\r {countresult}/{result.Count()} : {entry.Name} Copy!");
                            }
                            Console.WriteLine($"container {containerHDD.containerName} Done !!");
                            countBlobdata += containerDB.listBlob.Count;
                            countCopy++;
                            listBlobExist.Remove(containerDB);
                            Console.WriteLine($"{listBlobExist.Count}/{total}");

                            var def = Builders<BlobExistModel>.Update.Set(it => it.IsRun, true);
                            CollectionBlobExist.UpdateOne(it => it._id == containerHDD.containerName, def);
                        }
                    }
                }
                var zipN = zipName.Split("\\").LastOrDefault();
                var defzip = Builders<ListlZipModel>.Update.Set(it => it.IsRun, true);
                CollectionZipList.UpdateOne(it => it._id == zipN, defzip);

                if (!listBlobExist.Any())
                {
                    break;
                }

            }
            Console.WriteLine($"Total copy Container : {countCopy}");
            Console.WriteLine($"Total Blob data copy complet : {countBlobdata}");
        }
    }
}
