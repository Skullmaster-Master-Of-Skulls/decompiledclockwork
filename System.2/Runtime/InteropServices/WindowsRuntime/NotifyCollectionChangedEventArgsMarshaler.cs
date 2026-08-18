using System;
using System.Collections.Specialized;
using System.Security;
using System.StubHelpers;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003EF RID: 1007
	internal static class NotifyCollectionChangedEventArgsMarshaler
	{
		// Token: 0x06002638 RID: 9784 RVA: 0x000B0A65 File Offset: 0x000AEC65
		[SecurityCritical]
		internal static IntPtr ConvertToNative(NotifyCollectionChangedEventArgs managedArgs)
		{
			if (managedArgs == null)
			{
				return IntPtr.Zero;
			}
			return EventArgsMarshaler.CreateNativeNCCEventArgsInstance((int)managedArgs.Action, managedArgs.NewItems, managedArgs.OldItems, managedArgs.NewStartingIndex, managedArgs.OldStartingIndex);
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x000B0A94 File Offset: 0x000AEC94
		[SecurityCritical]
		internal static NotifyCollectionChangedEventArgs ConvertToManaged(IntPtr nativeArgsIP)
		{
			if (nativeArgsIP == IntPtr.Zero)
			{
				return null;
			}
			object obj = InterfaceMarshaler.ConvertToManagedWithoutUnboxing(nativeArgsIP);
			INotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs = (INotifyCollectionChangedEventArgs)obj;
			return new NotifyCollectionChangedEventArgs(notifyCollectionChangedEventArgs.Action, notifyCollectionChangedEventArgs.NewItems, notifyCollectionChangedEventArgs.OldItems, notifyCollectionChangedEventArgs.NewStartingIndex, notifyCollectionChangedEventArgs.OldStartingIndex);
		}
	}
}
