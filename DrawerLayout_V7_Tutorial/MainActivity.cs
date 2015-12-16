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
	[Activity (Label = "DrawerLayout_V7_Tutorial", MainLauncher = true, Icon = "@drawable/icon", Theme="@style/MyTheme")]
	public class MainActivity : ActionBarActivity
	{
		private SupportToolbar mToolbar;
		private MyActionBarDrawerToggle mDrawerToggle;
		private DrawerLayout mDrawerLayout;
		private ListView mLeftDrawer;
		private HomeFragment homeFragment;
		private ProfileFragment profileFragment;
		private SupportFragment mCurrentFragment = new SupportFragment();
		private Stack<SupportFragment> mStackFragments;
		
		private ArrayAdapter mLeftAdapter;
		
		private List<string> mLeftDataSet;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
	
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			mToolbar = FindViewById<SupportToolbar>(Resource.Id.toolbar);
			mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
			mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
			homeFragment = new HomeFragment();

			profileFragment = new ProfileFragment();
			mStackFragments = new Stack<SupportFragment>();
		

			mLeftDrawer.Tag = 0;
			

			SetSupportActionBar(mToolbar);
		
			mLeftDataSet = new List<string>();
			mLeftDataSet.Add ("Left Item 1");
			mLeftDataSet.Add ("Left Item 2");
			mLeftAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mLeftDataSet);
			mLeftDrawer.Adapter = mLeftAdapter;
			mLeftDrawer.ItemClick+= MenuListView_ItemClick;
			

			mDrawerToggle = new MyActionBarDrawerToggle(
				this,							//Host Activity
				mDrawerLayout,					//DrawerLayout
				Resource.String.openDrawer,		//Opened Message
				Resource.String.closeDrawer		//Closed Message
			);

			mDrawerLayout.SetDrawerListener(mDrawerToggle);
			SupportActionBar.SetHomeButtonEnabled(true);
			SupportActionBar.SetDisplayShowTitleEnabled(true);
			mDrawerToggle.SyncState();

			if (bundle != null)
			{
				if (bundle.GetString("DrawerState") == "Opened")
				{
					SupportActionBar.SetTitle(Resource.String.openDrawer);
				}

				else
				{
					SupportActionBar.SetTitle(Resource.String.closeDrawer);
				}
			}

			else
			{
				//This is the first the time the activity is ran
				SupportActionBar.SetTitle(Resource.String.closeDrawer);
			}

			Android.Support.V4.App.FragmentTransaction tx = SupportFragmentManager.BeginTransaction();

			tx.Add(Resource.Id.main, homeFragment);
			tx.Add(Resource.Id.main, profileFragment);
			tx.Hide(profileFragment);

			mCurrentFragment = homeFragment;
			tx.Commit();
		}
		void MenuListView_ItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			Android.Support.V4.App.Fragment fragment = null;

			switch (e.Id)
			{
			case 0:
				ShowFragment(homeFragment);
				break;
			case 1:
				ShowFragment(profileFragment);
				break;
			


			}

			//SupportFragmentManager.BeginTransaction().Replace(Resource.Id.main, fragment).Commit();


			mDrawerLayout.CloseDrawers();
			mDrawerToggle.SyncState();

		}
		private void ShowFragment(SupportFragment fragment)
		{

			if (fragment.IsVisible)
			{
				return;
			}

			var trans = SupportFragmentManager.BeginTransaction();


			fragment.View.BringToFront();
			mCurrentFragment.View.BringToFront();

			trans.Hide(mCurrentFragment);
			trans.Show(fragment);

			trans.AddToBackStack(null);
			mStackFragments.Push(mCurrentFragment);
			trans.Commit();

			mCurrentFragment = fragment;

		}
		public override bool OnOptionsItemSelected (IMenuItem item)
		{		
			switch (item.ItemId)
			{

			case Android.Resource.Id.Home:
				//The hamburger icon was clicked which means the drawer toggle will handle the event
				
				
				mDrawerToggle.OnOptionsItemSelected(item);
				return true;

			case Resource.Id.action_refresh:
				//Refresh
				return true;

			case Resource.Id.action_help:
			

				return true;

			default:
				return base.OnOptionsItemSelected (item);
			}
		}
			
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.action_menu, menu);
			return base.OnCreateOptionsMenu (menu);
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			if (mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
			{
				outState.PutString("DrawerState", "Opened");
			}

			else
			{
				outState.PutString("DrawerState", "Closed");
			}

			base.OnSaveInstanceState (outState);
		}

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			mDrawerToggle.SyncState();
		}

		public override void OnConfigurationChanged (Android.Content.Res.Configuration newConfig)
		{
			base.OnConfigurationChanged (newConfig);
			mDrawerToggle.OnConfigurationChanged(newConfig);
		}
	}
}


