using System;
using ObjCRuntime;

namespace NativeLibrary
{

	[Native]
	public enum ZPZPIEnvironment : long
	{
		Sandbox = 0,
		Production = 1
	}

	[Native]
	public enum ZPPaymentErrorCode : long
	{
		InvalidResponse = -2,
		InvalidOrder = -3,
		Fail = -5
	}
}

