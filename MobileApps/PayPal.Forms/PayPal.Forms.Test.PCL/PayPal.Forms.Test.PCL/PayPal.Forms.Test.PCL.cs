﻿using System;

using Xamarin.Forms;
using PayPal.Forms.Abstractions.Enum;
using System.Diagnostics;
using PayPal.Forms.Abstractions;

namespace PayPal.Forms.Test.PCL
{
	public class App : Application
	{
		Button BuyOnethingButton;
		Button BuyOnethingCustomAddressButton;
		Button BuyManythingsButton;
		Button BuyManythingsCustomAddressButton;
		Button RequestFuturePaymentsButton;
		Button ProfileSharingButton;

		public App()
		{
			BuyOnethingButton = new Button()
			{
				Text = "Buy One Thing Button"
			};
			BuyOnethingButton.Clicked += BuyOnethingButton_Clicked;

			BuyOnethingCustomAddressButton = new Button()
			{
				Text = "Buy One Thing Button with Custom Address"
			};
			BuyOnethingCustomAddressButton.Clicked += BuyOnethingCustomAddressButton_Clicked;

			BuyManythingsButton = new Button()
			{
				Text = "Buy Many Things Button"
			};
			BuyManythingsButton.Clicked += BuyManythingsButton_Clicked;

			BuyManythingsCustomAddressButton = new Button()
			{
				Text = "Buy Many Things Button with Custom Address"
			};
			BuyManythingsCustomAddressButton.Clicked += BuyManythingsCustomAddressButton_Clicked; ;

			RequestFuturePaymentsButton = new Button
			{
				Text = "Request Future Payments"
			};
			RequestFuturePaymentsButton.Clicked += RequestFuturePaymentsButton_Clicked;

			ProfileSharingButton = new Button
			{
				Text = "Authorize Profile Sharing"
			};
			ProfileSharingButton.Clicked += ProfileSharingButton_Clicked;

			// The root page of your application
			MainPage = new ContentPage
			{
				Content = new StackLayout
				{
					VerticalOptions = LayoutOptions.Center,
					Children = {
						BuyOnethingButton,
						BuyOnethingCustomAddressButton,
						BuyManythingsButton,
						BuyManythingsCustomAddressButton,
						RequestFuturePaymentsButton,
						ProfileSharingButton
					}
				}
			};
		}

		async void BuyManythingsCustomAddressButton_Clicked(object sender, EventArgs e)
		{
			var result = await CrossPayPalManager.Current.Buy(
				new PayPalItem[] {
					new PayPalItem ("sample item #1", 2, new Decimal (87.50), "USD", "sku-12345678"),
					new PayPalItem ("free sample item #2", 1, new Decimal (0.00), "USD", "sku-zero-price"),
					new PayPalItem ("sample item #3 with a longer name", 6, new Decimal (37.99), "USD", "sku-33333")
				},
				new Decimal(20.5),
				new Decimal(13.20),
				new ShippingAddress("My Custom Recipient Name", "Custom Line 1", "", "My City", "My State", "12345", "MX")
			);
			if (result.Status == PayPalStatus.Cancelled)
			{
				Debug.WriteLine("Cancelled");
			}
			else if (result.Status == PayPalStatus.Error)
			{
				Debug.WriteLine(result.ErrorMessage);
			}
			else if (result.Status == PayPalStatus.Successful)
			{
				Debug.WriteLine(result.ServerResponse.Response.Id);
			}
		}

		async void BuyManythingsButton_Clicked(object sender, EventArgs e)
		{
			var result = await CrossPayPalManager.Current.Buy(new PayPalItem[] {
				new PayPalItem ("sample item #1", 2, new Decimal (87.50), "USD",
					"sku-12345678"),
				new PayPalItem ("free sample item #2", 1, new Decimal (0.00),
					"USD", "sku-zero-price"),
				new PayPalItem ("sample item #3 with a longer name", 6, new Decimal (37.99),
					"USD", "sku-33333")
			}, new Decimal(20.5), new Decimal(13.20));
			if (result.Status == PayPalStatus.Cancelled)
			{
				Debug.WriteLine("Cancelled");
			}
			else if (result.Status == PayPalStatus.Error)
			{
				Debug.WriteLine(result.ErrorMessage);
			}
			else if (result.Status == PayPalStatus.Successful)
			{
				Debug.WriteLine(result.ServerResponse.Response.Id);
			}
		}

		async void BuyOnethingCustomAddressButton_Clicked(object sender, EventArgs e)
		{
			var result = await CrossPayPalManager.Current.Buy(
				new PayPalItem("Test Product", new Decimal(12.50), "USD"),
				new Decimal(0),
				new ShippingAddress("My Custom Recipient Name", "Custom Line 1", "", "My City", "My State", "12345", "MX")
			);
			if (result.Status == PayPalStatus.Cancelled)
			{
				Debug.WriteLine("Cancelled");
			}
			else if (result.Status == PayPalStatus.Error)
			{
				Debug.WriteLine(result.ErrorMessage);
			}
			else if (result.Status == PayPalStatus.Successful)
			{
				Debug.WriteLine(result.ServerResponse.Response.Id);
			}
		}

		async void BuyOnethingButton_Clicked(object sender, EventArgs e)
		{
			var result = await CrossPayPalManager.Current.Buy(new PayPalItem("Test Product", new Decimal(12.50), "USD"), new Decimal(0));
			if (result.Status == PayPalStatus.Cancelled)
			{
				Debug.WriteLine("Cancelled");
			}
			else if (result.Status == PayPalStatus.Error)
			{
				Debug.WriteLine(result.ErrorMessage);
			}
			else if (result.Status == PayPalStatus.Successful)
			{
				Debug.WriteLine(result.ServerResponse.Response.Id);
			}
		}

		async void RequestFuturePaymentsButton_Clicked(object sender, EventArgs e)
		{
			var result = await CrossPayPalManager.Current.RequestFuturePayments();
			if (result.Status == PayPalStatus.Cancelled)
			{
				Debug.WriteLine("Cancelled");
			}
			else if (result.Status == PayPalStatus.Error)
			{
				Debug.WriteLine(result.ErrorMessage);
			}
			else if (result.Status == PayPalStatus.Successful)
			{
				//Print Authorization Code
				Debug.WriteLine(result.ServerResponse.Response.Code);
				//Print Client Metadata Id
				Debug.WriteLine(CrossPayPalManager.Current.ClientMetadataId);
			}
		}

		async void ProfileSharingButton_Clicked(object sender, EventArgs e)
		{
			var result = await CrossPayPalManager.Current.AuthorizeProfileSharing();
			if (result.Status == PayPalStatus.Cancelled)
			{
				Debug.WriteLine("Cancelled");
			}
			else if (result.Status == PayPalStatus.Error)
			{
				Debug.WriteLine(result.ErrorMessage);
			}
			else if (result.Status == PayPalStatus.Successful)
			{
				Debug.WriteLine(result.ServerResponse.Response.Code);
			}
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
