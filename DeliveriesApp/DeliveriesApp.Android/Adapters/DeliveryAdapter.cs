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
using DeliveriesApp.Models;

namespace DeliveriesApp.Droid.Adapters
{
    class DeliveryAdapter : BaseAdapter
    {

        private readonly Context _context;
        private List<Delivery> _deliveries;

        public DeliveryAdapter(Context context, List<Delivery> deliveries)
        {
            _context = context;
            _deliveries = deliveries;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            DeliveryAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as DeliveryAdapterViewHolder;

            if (holder == null)
            {
                holder = new DeliveryAdapterViewHolder();
                var inflater = _context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();

                view = inflater.Inflate(Resource.Layout.DeliveryCell, parent, false);
                holder.Name = view.FindViewById<TextView>(Resource.Id.deliveryNameTextView);
                holder.Status = view.FindViewById<TextView>(Resource.Id.deliveryStatusTextView);
                view.Tag = holder;
            }


            var currentDelivery = _deliveries[position];
            holder.Name.Text = currentDelivery.Name;
            holder.Status.Text = currentDelivery.GetStatusForDelivery(currentDelivery.Status);

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count => _deliveries.Count;
    }

    class DeliveryAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public TextView Name { get; set; }
        public TextView Status  { get; set; }
    }
}