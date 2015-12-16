using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using SupportFragment = Android.Support.V4.App.Fragment;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using System.Collections.Generic;

namespace DrawerLayout_V7_Tutorial
{
	class HomeFragment : Android.Support.V4.App.Fragment
	{
		public HomeFragment()
		{

		}
		public static Android.Support.V4.App.Fragment newInstance(Context context)
		{
			HomeFragment busrouteFragment = new HomeFragment();
			return busrouteFragment;
		}
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

		}
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			ViewGroup root = (ViewGroup)inflater.Inflate(Resource.Layout.homefragment, null);
			return root;
		}
	}

}


