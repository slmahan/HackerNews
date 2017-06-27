using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{

public static class HackerNewsAPIClient
	{
        public static void GetBestStories()
        {
            var storyListUrl = "https://hacker-news.firebaseio.com/v0/beststories.json";
            var storyUrl = "https://hacker-news.firebaseio.com/v0/item/";

            var syncClient = new WebClient();
            var storyList = syncClient.DownloadString(storyListUrl);

            string storyObj = "";

            var pattern = "([" + Regex.Escape("[") +"]|["+ Regex.Escape("]")+ "])";

            HackerNewsStory hackerNewsStory;

            storyList = Regex.Replace(storyList, pattern, "");
            
            string[] storyIDs = storyList.Split(',').Select(sValue => sValue.Trim()).ToArray();

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(HackerNewsStory));

            foreach (string item in storyIDs) 
            {
              storyObj = syncClient.DownloadString(storyUrl + item + ".json");
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(storyObj)))
                {
                    hackerNewsStory = (HackerNewsStory)serializer.ReadObject(ms);
                    Console.WriteLine("Title: " + hackerNewsStory.title + " - Author: " + hackerNewsStory.by + " - URL: " + hackerNewsStory.Url);
                }
            }

        }
	}
}
