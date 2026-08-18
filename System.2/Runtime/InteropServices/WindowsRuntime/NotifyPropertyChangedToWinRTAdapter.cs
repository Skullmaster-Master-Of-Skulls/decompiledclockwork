using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F4 RID: 1012
	internal sealed class NotifyPropertyChangedToWinRTAdapter
	{
		// Token: 0x06002646 RID: 9798 RVA: 0x000B0C7D File Offset: 0x000AEE7D
		private NotifyPropertyChangedToWinRTAdapter()
		{
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x000B0C88 File Offset: 0x000AEE88
		[SecurityCritical]
		internal EventRegistrationToken add_PropertyChanged(PropertyChangedEventHandler value)
		{
			INotifyPropertyChanged notifyPropertyChanged = JitHelpers.UnsafeCast<INotifyPropertyChanged>(this);
			EventRegistrationTokenTable<PropertyChangedEventHandler> orCreateValue = NotifyPropertyChangedToWinRTAdapter.m_weakTable.GetOrCreateValue(notifyPropertyChanged);
			EventRegistrationToken result = orCreateValue.AddEventHandler(value);
			notifyPropertyChanged.PropertyChanged += value;
			return result;
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000B0CB8 File Offset: 0x000AEEB8
		[SecurityCritical]
		internal void remove_PropertyChanged(EventRegistrationToken token)
		{
			INotifyPropertyChanged notifyPropertyChanged = JitHelpers.UnsafeCast<INotifyPropertyChanged>(this);
			EventRegistrationTokenTable<PropertyChangedEventHandler> orCreateValue = NotifyPropertyChangedToWinRTAdapter.m_weakTable.GetOrCreateValue(notifyPropertyChanged);
			PropertyChangedEventHandler propertyChangedEventHandler = orCreateValue.ExtractHandler(token);
			if (propertyChangedEventHandler != null)
			{
				notifyPropertyChanged.PropertyChanged -= propertyChangedEventHandler;
			}
		}

		// Token: 0x040020AF RID: 8367
		private static ConditionalWeakTable<INotifyPropertyChanged, EventRegistrationTokenTable<PropertyChangedEventHandler>> m_weakTable = new ConditionalWeakTable<INotifyPropertyChanged, EventRegistrationTokenTable<PropertyChangedEventHandler>>();
	}
}
