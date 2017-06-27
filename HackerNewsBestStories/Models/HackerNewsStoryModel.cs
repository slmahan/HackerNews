using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HackerNewsBestStories
{
    [DataContract]
    public class HackerNewsStoryModel
    {
            [DataMember]
            public string by { get; set; }
            [DataMember]
            public int descendants { get; set; }
            [DataMember]
            public int id { get; set; }
            [DataMember]
            public List<int> kids { get; set; }
            [DataMember]
            public int score { get; set; }
            [DataMember]
            public int time { get; set; }
            [DataMember]
            public string title { get; set; }
            [DataMember]
            public string Type { get; set; }
            [DataMember]
            public string Url { get; set; }
    }
}
