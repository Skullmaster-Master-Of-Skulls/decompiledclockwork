using System;
using System.ComponentModel;
using System.Security;
using System.StubHelpers;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F0 RID: 1008
	internal static class PropertyChangedEventArgsMarshaler
	{
		// Token: 0x0600263A RID: 9786 RVA: 0x000B0AE1 File Offset: 0x000AECE1
		[SecurityCritical]
		internal static IntPtr ConvertToNative(PropertyChangedEventArgs managedArgs)
		{
			if (managedArgs == null)
			{
				return IntPtr.Zero;
			}
			return EventArgsMarshaler.CreateNativePCEventArgsInstance(managedArgs.PropertyName);
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x000B0AF8 File Offset: 0x000AECF8
		[SecurityCritical]
		internal static PropertyChangedEventArgs ConvertToManaged(IntPtr nativeArgsIP)
		{
			if (nativeArgsIP == IntPtr.Zero)
			{
				return null;
			}
			object obj = InterfaceMarshaler.ConvertToManagedWithoutUnboxing(nativeArgsIP);
			IPropertyChangedEventArgs propertyChangedEventArgs = (IPropertyChangedEventArgs)obj;
			return new PropertyChangedEventArgs(propertyChangedEventArgs.PropertyName);
		}
	}
}
