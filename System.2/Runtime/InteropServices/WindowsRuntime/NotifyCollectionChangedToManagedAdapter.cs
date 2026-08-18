using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F1 RID: 1009
	internal sealed class NotifyCollectionChangedToManagedAdapter
	{
		// Token: 0x0600263C RID: 9788 RVA: 0x000B0B2D File Offset: 0x000AED2D
		private NotifyCollectionChangedToManagedAdapter()
		{
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x0600263D RID: 9789 RVA: 0x000B0B38 File Offset: 0x000AED38
		// (remove) Token: 0x0600263E RID: 9790 RVA: 0x000B0B70 File Offset: 0x000AED70
		internal event NotifyCollectionChangedEventHandler CollectionChanged
		{
			[SecurityCritical]
			add
			{
				INotifyCollectionChanged_WinRT @object = JitHelpers.UnsafeCast<INotifyCollectionChanged_WinRT>(this);
				Func<NotifyCollectionChangedEventHandler, EventRegistrationToken> addMethod = new Func<NotifyCollectionChangedEventHandler, EventRegistrationToken>(@object.add_CollectionChanged);
				Action<EventRegistrationToken> removeMethod = new Action<EventRegistrationToken>(@object.remove_CollectionChanged);
				WindowsRuntimeMarshal.AddEventHandler<NotifyCollectionChangedEventHandler>(addMethod, removeMethod, value);
			}
			[SecurityCritical]
			remove
			{
				INotifyCollectionChanged_WinRT @object = JitHelpers.UnsafeCast<INotifyCollectionChanged_WinRT>(this);
				Action<EventRegistrationToken> removeMethod = new Action<EventRegistrationToken>(@object.remove_CollectionChanged);
				WindowsRuntimeMarshal.RemoveEventHandler<NotifyCollectionChangedEventHandler>(removeMethod, value);
			}
		}
	}
}
