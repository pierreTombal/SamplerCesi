using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading.Tasks;
using kaamelott_sampler.Models;
using Newtonsoft.Json;
using System.Linq;
using Android.Media;

namespace kaamelotSampler
{
    [Activity(Label = "kaamelot Sampler", Icon ="@drawable/icon", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private TextView lbl_titre;
        private ListView listsample;
        List<Sample> mylist;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            lbl_titre = FindViewById<TextView>(Resource.Id.lbl_Titre);
            listsample = FindViewById<ListView>(Resource.Id.listView1);
            listsample.ItemClick += Listsample_ItemClick;


            await LoadSamples();
        }

        public async void Listsample_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var sample = mylist[e.Position];
            await PlayAudioFileAsync(sample.file);
        }

        public async Task LoadSamples()
        {
            
            string jsonbrut = await GetJson_fromAssetFileAsync("sounds.json");
            mylist = JsonConvert.DeserializeObject<List<Sample>>(jsonbrut);
            mylist.OrderBy(x => x.title);

            ListView lv1 = FindViewById<ListView>(Resource.Id.listView1);

            SampleAdapter adapter= new SampleAdapter(this, mylist);

            lv1.Adapter = adapter;
        }

        public async Task<string> GetJson_fromAssetFileAsync(string fileName)
        {
            StreamReader strm = new StreamReader(this.Assets.Open(fileName));
            string response = await strm.ReadToEndAsync();

            strm.Close();
            return response;
        }

        public async Task PlayAudioFileAsync(string filename)
        {
            await Task.Factory.StartNew(() =>
            {
                var player = new MediaPlayer();
                var fd = global::Android.App.Application.Context.Assets.OpenFd(filename);
                player.Prepared += (s, e) =>
                {
                    player.Start();
                };
                player.Completion += (s, e) =>
                {
                    player.Release();
                };
                player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
                player.Prepare();
            });
        }
    }
}

