using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using kaamelott_sampler.Models;

namespace kaamelotSampler
{
    class SampleAdapter : BaseAdapter<Sample>
    {

        Context context;

        List<Sample> items;

        public SampleAdapter(Context context, List<Sample> Items)
        {
            this.context = context;
            items = Items;
        }


        //public override Java.Lang.Object GetItem(int position)
        //{
        //    return position;
        //}

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            SampleAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as SampleAdapterViewHolder;

            if (holder == null)
            {
                holder = new SampleAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.MyRow, parent, false);
                //holder.Title = view.FindViewById<TextView>(Resource.Id.text);
                view.Tag = holder;
            }

            ImageView ImagePerso = view.FindViewById<ImageView>(Resource.Id.ImagePerso);
            TextView Titre = view.FindViewById<TextView>(Resource.Id.lbl_Titre);
            TextView Perso = view.FindViewById<TextView>(Resource.Id.lbl_Perso);

            Titre.Text = items[position].title;
            Perso.Text = items[position].character;
            string personnage = items[position].character.ToLower().Trim().Replace("é", "e").Replace("ê", "e").Replace("è", "e").Replace("î", "i").Replace("’", "'").Replace("'", "").Replace(" ", "").Replace("è", "e").Replace("-", "");
            
            var img = context.Resources.GetIdentifier(personnage, "drawable", context.PackageName);
            if( img != 0)
            {
                ImagePerso.SetImageResource(img);
            }
            else
            {
                ImagePerso.SetImageResource(Resource.Drawable.profile_generic);
            }

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override Sample this[int position] => items[position];
    }

    class SampleAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}