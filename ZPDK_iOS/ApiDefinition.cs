﻿using System;

using ObjCRuntime;
using Foundation;
using UIKit;

namespace NativeLibrary
{

	// @protocol ZPConfirmAutoDebitDelegate <NSObject>
	[Protocol, Model(AutoGeneratedName = true)]
	[BaseType(typeof(NSObject))]
	interface ZPConfirmAutoDebitDelegate
	{
		// @required -(void)confirmAutoDebitDidSucceeded:(NSString *)bindingId;
		[Abstract]
		[Export("confirmAutoDebitDidSucceeded:")]
		void ConfirmAutoDebitDidSucceeded(string bindingId);
	}

	// @protocol ZPPaymentDelegate <NSObject>
	[Protocol, Model(AutoGeneratedName = true)]
	[BaseType(typeof(NSObject))]
	interface ZPPaymentDelegate
	{
		// @required -(void)paymentDidSucceeded:(NSString *)transactionId zpTranstoken:(NSString *)zpTranstoken appTransId:(NSString *)appTransId;
		[Abstract]
		[Export("paymentDidSucceeded:zpTranstoken:appTransId:")]
		void PaymentDidSucceeded(string transactionId, string zpTranstoken, string appTransId);

		// @required -(void)paymentDidCanceled:(NSString *)zpTranstoken appTransId:(NSString *)appTransId;
		[Abstract]
		[Export("paymentDidCanceled:appTransId:")]
		void PaymentDidCanceled(string zpTranstoken, string appTransId);

		// @required -(void)paymentDidError:(ZPPaymentErrorCode)errorCode zpTranstoken:(NSString *)zpTranstoken appTransId:(NSString *)appTransId;
		[Abstract]
		[Export("paymentDidError:zpTranstoken:appTransId:")]
		void PaymentDidError(ZPPaymentErrorCode errorCode, string zpTranstoken, string appTransId);
	}

	// @interface ZaloPaySDK : NSObject
	[BaseType(typeof(NSObject))]
	interface ZaloPaySDK
	{
		[Wrap("WeakPaymentDelegate")]
		ZPPaymentDelegate PaymentDelegate { get; set; }

		// @property (nonatomic, weak) id<ZPPaymentDelegate> paymentDelegate;
		[NullAllowed, Export("paymentDelegate", ArgumentSemantic.Weak)]
		NSObject WeakPaymentDelegate { get; set; }

		[Wrap("WeakConfirmAutoDebitDelegate")]
		ZPConfirmAutoDebitDelegate ConfirmAutoDebitDelegate { get; set; }

		// @property (nonatomic, weak) id<ZPConfirmAutoDebitDelegate> confirmAutoDebitDelegate;
		[NullAllowed, Export("confirmAutoDebitDelegate", ArgumentSemantic.Weak)]
		NSObject WeakConfirmAutoDebitDelegate { get; set; }

		// +(instancetype)sharedInstance;
		[Static]
		[Export("sharedInstance")]
		ZaloPaySDK SharedInstance();

		// -(void)initWithAppId:(NSInteger)appId;
		[Export("initWithAppId:")]
		void InitWithAppId(nint appId);

		// -(void)initWithAppId:(NSInteger)appId environment:(ZPZPIEnvironment)environment;
		[Export("initWithAppId:environment:")]
		void InitWithAppId(nint appId, ZPZPIEnvironment environment);

		// -(void)initWithAppId:(NSInteger)appId uriScheme:(NSString *)uriScheme;
		[Export("initWithAppId:uriScheme:")]
		void InitWithAppId(nint appId, string uriScheme);

		// -(void)initWithAppId:(NSInteger)appId uriScheme:(NSString *)uriScheme environment:(ZPZPIEnvironment)environment;
		[Export("initWithAppId:uriScheme:environment:")]
		void InitWithAppId(nint appId, string uriScheme, ZPZPIEnvironment environment);

		// -(BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation;
		[Export("application:openURL:sourceApplication:annotation:")]
		bool Application(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation);

		// -(BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey,id> *)options;
		[Export("application:openURL:options:")]
		bool Application(UIApplication app, NSUrl url, NSDictionary<NSString, NSObject> options);

		// -(void)payOrder:(NSString *)zptranstoken;
		[Export("payOrder:")]
		void PayOrder(string zptranstoken);

		// -(void)confirmAutoDebit:(NSString *)token;
		[Export("confirmAutoDebit:")]
		void ConfirmAutoDebit(string token);

		// -(void)navigateToZaloStore;
		[Export("navigateToZaloStore")]
		void NavigateToZaloStore();
	}

	//[Static]
	//partial interface Constants
	//{
	//	// extern double zpdkVersionNumber;
	//	[Field("zpdkVersionNumber", "__Internal")]
	//	double zpdkVersionNumber { get; }

	//	// extern const unsigned char [] zpdkVersionString;
	//	[Field("zpdkVersionString", "__Internal")]
	//	byte[] zpdkVersionString { get; }
	//}

	//
}
