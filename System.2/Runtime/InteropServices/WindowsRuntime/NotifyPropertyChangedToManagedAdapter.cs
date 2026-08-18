using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F3 RID: 1011
	internal sealed class NotifyPropertyChangedToManagedAdapter
	{
		// Token: 0x06002643 RID: 9795 RVA: 0x000B0C12 File Offset: 0x000AEE12
		private NotifyPropertyChangedToManagedAdapter()
		{
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06002644 RID: 9796 RVA: 0x000B0C1C File Offset: 0x000AEE1C
		// (remove) Token: 0x06002645 RID: 9797 RVA: 0x000B0C54 File Offset: 0x000AEE54
		internal event PropertyChangedEventHandler PropertyChanged
		{
			[SecurityCritical]
			add
			{
				INotifyPropertyChanged_WinRT @object = JitHelpers.UnsafeCast<INotifyPropertyChanged_WinRT>(this);
				Func<PropertyChangedEventHandler, EventRegistrationToken> addMethod = new Func<PropertyChangedEventHandler, EventRegistrationToken>(@object.add_PropertyChanged);
				Action<EventRegistrationToken> removeMethod = new Action<EventRegistrationToken>(@object.remove_PropertyChanged);
				WindowsRuntimeMarshal.AddEventHandler<PropertyChangedEventHandler>(addMethod, removeMethod, value);
			}
			[SecurityCritical]
			remove
			{
				INotifyPropertyChanged_WinRT @object = JitHelpers.UnsafeCast<INotifyPropertyChanged_WinRT>(this);
				Action<EventRegistrationToken> removeMethod = new Action<EventRegistrationToken>(@object.remove_PropertyChanged);
				WindowsRuntimeMarshal.RemoveEventHandler<PropertyChangedEventHandler>(removeMethod, value);
			}
		}
	}
}
