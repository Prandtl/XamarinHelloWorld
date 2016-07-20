using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Uri = Android.Net.Uri;

namespace HelloWorld
{
	[Activity(Label = "Phone Word", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			
			SetContentView(Resource.Layout.Main);

			var phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
			var translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
			var callButton = FindViewById<Button>(Resource.Id.CallButton);

			callButton.Enabled = false;
			var translatedNumber = String.Empty;

			translateButton.Click += (sender, args) =>
			{
				translatedNumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);
				if (string.IsNullOrWhiteSpace(translatedNumber))
				{
					callButton.Text = "Call";
					callButton.Enabled = false;
				}
				else
				{
					callButton.Text = "Call " + translatedNumber;
					callButton.Enabled = true;
				}
			};

			callButton.Click += (sender, args) =>
			{
				var callDialog = new AlertDialog.Builder(this);
				callDialog.SetMessage("Call " + translatedNumber + "?");
				callDialog.SetNeutralButton("Call", delegate
				{
					var callIntent = new Intent(Intent.ActionCall);
					callIntent.SetData(Uri.Parse("tel:" + translatedNumber));
					StartActivity(callIntent);
				});
				callDialog.SetNegativeButton("Cancel", delegate { });
				callDialog.Show();
			};
		}
	}
}

