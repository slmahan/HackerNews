using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading.Tasks;
using System.Text;

namespace HackerNewsBestStories.Controllers
{
    public class HomeController : Controller
    {
        // GET: HackerNews
        //Hosted web API REST Service base url  
          string storyListUrl = "https://hacker-news.firebaseio.com/v0/beststories.json";
        string storyUrl = "https://hacker-news.firebaseio.com/v0/item/";

        public ActionResult Index()
        {
            List<HackerNewsStoryModel> newsStories = new List<HackerNewsStoryModel>();

            var syncClient = new WebClient();
            var storyList = syncClient.DownloadString(storyListUrl);

            string storyObj = "";

            var pattern = "([" + Regex.Escape("[") + "]|[" + Regex.Escape("]") + "])";

            HackerNewsStoryModel hackerNewsStory;

            storyList = Regex.Replace(storyList, pattern, "");

            string[] storyIDs = storyList.Split(',').Select(sValue => sValue.Trim()).ToArray();

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(HackerNewsStoryModel));

            foreach (string item in storyIDs)
            {
                storyObj = syncClient.DownloadString(new Uri(storyUrl + item + ".json"));
                using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(storyObj)))
                {
                    hackerNewsStory = (HackerNewsStoryModel)serializer.ReadObject(ms);
                    newsStories.Add(hackerNewsStory);
                }
            }
            return View(newsStories);

        }
    }
}