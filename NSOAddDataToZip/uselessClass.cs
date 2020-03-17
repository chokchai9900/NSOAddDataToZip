using System;
using System.Collections.Generic;
using System.Text;

namespace NSOAddDataToMongo
{
    class uselessClass
    {
        //insert zip list to database
        //const string ZIP_PATH = @"F:\";

        //string connectString = "mongodb://firstclass:Th35F1rstCla55@mongodbnewzfdggw5bmqhbq-vm0.southeastasia.cloudapp.azure.com/water";
        //var mongo = new MongoClient(connectString);
        //var database = mongo.GetDatabase("water");
        //var CollectionZipList = database.GetCollection<ListlZipModel>("Ziplist");

        //var listFile = Directory.GetFiles(ZIP_PATH)
        //    .OrderByDescending(n => Regex.Replace(n, @"\d+", n => n.Value.PadLeft(4, '0')))
        //    .Select(it => new ListlZipModel {
        //        _id = it.Split("\\").LastOrDefault(),
        //        IsRun = false
        //    })
        //    .ToList();
        //Console.WriteLine($"File count : {listFile.Count}");
        //CollectionZipList.InsertMany(listFile);
        //Console.WriteLine("Insert complet !");

        //var countTotalFileMove = 1;

        //const string MAIN_PATH = @"E:\ContranZip\DataBlob";
        //const string DESTINATION_PATH = @"E:\ContranZip\NewDataBlob";
        //string connectString = "mongodb://firstclass:Th35F1rstCla55@mongodbnewzfdggw5bmqhbq-vm0.southeastasia.cloudapp.azure.com/water";
        //var mongo = new MongoClient(connectString);
        //var database = mongo.GetDatabase("water");
        //var blobExistTrueCollection = database.GetCollection<BlobExistModel>("BlobExistTrue");
        //Console.WriteLine($"{stopWatch.Elapsed} : Database Connecting");

        //    var Directory_Tree = Directory.GetDirectories(MAIN_PATH);

        //    foreach (var Directory_List in Directory_Tree)
        //    {
        //        var FileList = Directory.GetFiles(Directory_List)
        //            .Select(it => it.Split("\\").LastOrDefault())
        //            .Where(it => it.StartsWith("bld1v") || it.StartsWith("unt1v") || it.StartsWith("com1v"))
        //            .ToList();

        //var Directory_Name = Directory_List.Split("\\").LastOrDefault();

        //var currentPATH = $@"{DESTINATION_PATH}\\{Directory_Name}";

        //var isBlobExist = blobExistTrueCollection.Find(it => FileList.All(x => it.listBlob.Contains(x))).FirstOrDefault();

        //        if (isBlobExist != null)
        //        {
        //            Directory.Move(Directory_List, currentPATH);
        //            Console.WriteLine($"Round : {countTotalFileMove}");
        //            Console.WriteLine($"{stopWatch.Elapsed} : {Directory_List} is move !");
        //            countTotalFileMove++;
        //        }
        //        else
        //        {
        //            Console.WriteLine($"{Directory_List} is not exist");
        //        }
        //    }

        //copy spacify Directiry in zip 
        //var jsonpath = @"C:\Users\chokc\Desktop\zipdataNew.json";
        //string extractPath = @"E:\testExtractZip";
        //string json = File.ReadAllText(jsonpath);
        //var jsonList = JsonConvert.DeserializeObject<List<ResultModel>>(json);

        //foreach (var List in jsonList)
        //{
        //    Console.WriteLine($"{stopWatch.Elapsed} : Read zip {List.Zippath}.");
        //    using (ZipArchive archive = ZipFile.OpenRead(List.Zippath))
        //    {
        //        foreach (var ContainerName in List.ContainerName)
        //        {
        //            Console.WriteLine($"{stopWatch.Elapsed} : Current Directory : {ContainerName}");
        //            var currentDirectiry = @$"{extractPath}\{ContainerName}";

        //            var result = from currEntry in archive.Entries
        //                         where Path.GetDirectoryName(currEntry.FullName) == ContainerName
        //                         where !String.IsNullOrEmpty(currEntry.Name)
        //                         select currEntry;
        //            foreach (ZipArchiveEntry entry in result)
        //            {
        //                if (!Directory.Exists(currentDirectiry))
        //                {
        //                    Console.WriteLine($"{stopWatch.Elapsed} : Directory Name : {ContainerName}");
        //                    Directory.CreateDirectory(currentDirectiry);
        //                }
        //                entry.ExtractToFile(Path.Combine(currentDirectiry, entry.Name));
        //                Console.WriteLine($"\r{stopWatch.Elapsed} : {entry.Name} Is Copy!");
        //            }
        //        }
        //    }
        //}

        // Group zip 
        //const string path = @"C:\Users\chokc\source\repos\NSOAddDataToZip\NSOAddDataToZip\zipdata.json";
        //string connectString = "mongodb://firstclass:Th35F1rstCla55@mongodbnewzfdggw5bmqhbq-vm0.southeastasia.cloudapp.azure.com/water";

        //var mongo = new MongoClient(connectString);
        //var database = mongo.GetDatabase("water");
        //var collection = database.GetCollection<ResultModel>("ZipList");
        //Console.WriteLine($"{stopWatch.Elapsed} : Database Connecting");

        //var result = new List<ResultModel>();
        //var refomat = new List<ResultModel>();
        //string json = File.ReadAllText(path);
        //Console.WriteLine($"{stopWatch.Elapsed} : Read json file finish !");
        //result = JsonConvert.DeserializeObject<List<ResultModel>>(json);

        //var listGroup = result
        //    .GroupBy(it => it.Zippath)
        //    .Select(it =>
        //    {
        //        return new
        //        {
        //            zipName = it.Key,
        //            isContainer = it.Any(i => true)
        //        };
        //    }
        //    )
        //    .Where(it => it.isContainer == true)
        //    .Select(it => it.zipName)
        //    .ToList();

        //foreach (var item in listGroup)
        //{
        //    var validData = result.Find(it => it.Zippath == item && it.ContainerName != null);

        //    if (validData != null)
        //    {
        //        var ContainerNameData = result.Find(it => it.Zippath == item).ContainerName;
        //        refomat.Add(new ResultModel
        //        {
        //            Zippath = item,
        //            ContainerName = ContainerNameData
        //        });
        //    }
        //}

        //using (StreamWriter file = File.CreateText(@"d:\movie.json"))
        //{
        //    JsonSerializer serializer = new JsonSerializer();
        //    serializer.Serialize(file, refomat);
        //}

        //collection.InsertMany(refomat);

        //Console.WriteLine($"{stopWatch.Elapsed} : Insert data finish !");
    }
}
