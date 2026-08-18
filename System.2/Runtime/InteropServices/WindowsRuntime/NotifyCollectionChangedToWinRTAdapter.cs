using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F2 RID: 1010
	internal sealed class NotifyCollectionChangedToWinRTAdapter
	{
		// Token: 0x0600263F RID: 9791 RVA: 0x000B0B99 File Offset: 0x000AED99
		private NotifyCollectionChangedToWinRTAdapter()
		{
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000B0BA4 File Offset: 0x000AEDA4
		[SecurityCritical]
		internal EventRegistrationToken add_CollectionChanged(NotifyCollectionChangedEventHandler value)
		{
			INotifyCollectionChanged notifyCollectionChanged = JitHelpers.UnsafeCast<INotifyCollectionChanged>(this);
			EventRegistrationTokenTable<NotifyCollectionChangedEventHandler> orCreateValue = NotifyCollectionChangedToWinRTAdapter.m_weakTable.GetOrCreateValue(notifyCollectionChanged);
			EventRegistrationToken result = orCreateValue.AddEventHandler(value);
			notifyCollectionChanged.CollectionChanged += value;
			return result;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000B0BD4 File Offset: 0x000AEDD4
		[SecurityCritical]
		internal void remove_CollectionChanged(EventRegistrationToken token)
		{
			INotifyCollectionChanged notifyCollectionChanged = JitHelpers.UnsafeCast<INotifyCollectionChanged>(this);
			EventRegistrationTokenTable<NotifyCollectionChangedEventHandler> orCreateValue = NotifyCollectionChangedToWinRTAdapter.m_weakTable.GetOrCreateValue(notifyCollectionChanged);
			NotifyCollectionChangedEventHandler notifyCollectionChangedEventHandler = orCreateValue.ExtractHandler(token);
			if (notifyCollectionChangedEventHandler != null)
			{
				notifyCollectionChanged.CollectionChanged -= notifyCollectionChangedEventHandler;
			}
		}

		// Token: 0x040020AE RID: 8366
		private static ConditionalWeakTable<INotifyCollectionChanged, EventRegistrationTokenTable<NotifyCollectionChangedEventHandler>> m_weakTable = new ConditionalWeakTable<INotifyCollectionChanged, EventRegistrationTokenTable<NotifyCollectionChangedEventHandler>>();
	}
}
