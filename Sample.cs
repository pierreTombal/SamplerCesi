using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace kaamelott_sampler.Models
{
    public class Sample
    {
        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }

        [JsonProperty(PropertyName = "character")]
        public string character { get; set; }

        [JsonIgnore]
        public string characterfile
        {
            get
            {
                if (!String.IsNullOrEmpty(character))
                {
                    return character.Replace(".mp3", ".png");
                }
                else
                {
                    return "default.png";
                }
            }
        }

        [JsonProperty(PropertyName = "episode")]
        public string episode { get; set; }

        [JsonProperty(PropertyName = "file")]
        public string file { get; set; }

    }

    //public class SampleGroup : ObservableCollection<Sample>
    //{
    //    public String Name { get; private set; }

    //    public SampleGroup(String Name)
    //    {
    //        this.Name = Name;
    //    }

    //    // Whatever other properties

    //    public bool HasItems
    //    {
    //        get
    //        {
    //            return (Count != 0);
    //        }
    //        private set
    //        {
    //        }
    //    }
    //}

    //public class SampleGroupingClass<K, T> : ObservableCollection<T>
    //{
    //    public K Key { get; private set; }

    //    public SampleGroupingClass(K key, IEnumerable<T> items)
    //    {
    //        Key = key;
    //        foreach (var item in items)
    //        {
    //            Items.Add(item);
    //        }
    //    }
    //}

}
