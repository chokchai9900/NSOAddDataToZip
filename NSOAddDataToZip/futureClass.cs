using MongoDB.Driver;
using Newtonsoft.Json;
using NSOAddDataToMongo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace NSOAddDataToMongo
{
    class futureClass
    {
        static void sub()
        {
            var countCopy = 0;

            const string DESTINATION_PATH = @"E:\ContranZip\NewDataBlob";
            const string ZIP_PATH = @"F:\TestZip";

            string connectString = "mongodb://firstclass:Th35F1rstCla55@mongodbnewzfdggw5bmqhbq-vm0.southeastasia.cloudapp.azure.com/water";
            var mongo = new MongoClient(connectString);
            var database = mongo.GetDatabase("water");
            var blobExistTrueCollection = database.GetCollection<BlobExistModel>("BlobExist");
            Console.WriteLine($"Database Connecting");

            var listBlobExist = blobExistTrueCollection.Find(it => true).ToList();
            var total = listBlobExist.Count;
            Console.WriteLine("Get data done !");

            var zipTree = Directory.GetFiles(ZIP_PATH).ToList();
            Console.WriteLine($"Total zip : {zipTree.Count}");

            foreach (var zipName in zipTree)
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

                            var result = from currEntry in archive.Entries
                                         where Path.GetDirectoryName(currEntry.FullName) == containerHDD.containerName
                                         where !String.IsNullOrEmpty(currEntry.Name)
                                         select currEntry;
                            foreach (ZipArchiveEntry entry in result)
                            {
                                if (!Directory.Exists(currentDirectiry))
                                {
                                    Directory.CreateDirectory(currentDirectiry);
                                }
                                entry.ExtractToFile(Path.Combine(currentDirectiry, entry.Name));
                            }
                            Console.WriteLine($"container {containerHDD.containerName} Done !!");
                            Console.WriteLine($"{listBlobExist.Count}/{total}");
                            countCopy++;
                            listBlobExist.Remove(containerDB);
                            /// update
                            //containerHDD.containerName
                        }
                    }
                }
                if (!listBlobExist.Any())
                {
                    break;
                }
            }
            Console.WriteLine($"Total copy Container : {countCopy}");
        }
    }
}
