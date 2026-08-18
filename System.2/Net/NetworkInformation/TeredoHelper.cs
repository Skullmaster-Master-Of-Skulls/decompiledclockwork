using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Security;
using System.Threading;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000304 RID: 772
	[SuppressUnmanagedCodeSecurity]
	internal class TeredoHelper
	{
		// Token: 0x06001B66 RID: 7014 RVA: 0x00082077 File Offset: 0x00080277
		static TeredoHelper()
		{
			AppDomain.CurrentDomain.DomainUnload += TeredoHelper.OnAppDomainUnload;
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00082099 File Offset: 0x00080299
		private TeredoHelper(Action<object> callback, object state)
		{
			this.callback = callback;
			this.state = state;
			this.onStabilizedDelegate = new StableUnicastIpAddressTableDelegate(this.OnStabilized);
			this.runCallbackCalled = false;
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x000820C8 File Offset: 0x000802C8
		public static bool UnsafeNotifyStableUnicastIpAddressTable(Action<object> callback, object state)
		{
			TeredoHelper teredoHelper = new TeredoHelper(callback, state);
			uint num = 0U;
			SafeFreeMibTable safeFreeMibTable = null;
			List<TeredoHelper> obj = TeredoHelper.pendingNotifications;
			lock (obj)
			{
				if (TeredoHelper.impendingAppDomainUnload)
				{
					return false;
				}
				num = UnsafeNetInfoNativeMethods.NotifyStableUnicastIpAddressTable(AddressFamily.Unspecified, out safeFreeMibTable, teredoHelper.onStabilizedDelegate, IntPtr.Zero, out teredoHelper.cancelHandle);
				if (safeFreeMibTable != null)
				{
					safeFreeMibTable.Dispose();
				}
				if (num == 997U)
				{
					TeredoHelper.pendingNotifications.Add(teredoHelper);
					return false;
				}
			}
			if (num != 0U)
			{
				throw new Win32Exception((int)num);
			}
			return true;
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x00082164 File Offset: 0x00080364
		private static void OnAppDomainUnload(object sender, EventArgs args)
		{
			List<TeredoHelper> obj = TeredoHelper.pendingNotifications;
			lock (obj)
			{
				TeredoHelper.impendingAppDomainUnload = true;
				foreach (TeredoHelper teredoHelper in TeredoHelper.pendingNotifications)
				{
					teredoHelper.cancelHandle.Dispose();
				}
			}
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x000821E8 File Offset: 0x000803E8
		private void RunCallback(object o)
		{
			List<TeredoHelper> obj = TeredoHelper.pendingNotifications;
			lock (obj)
			{
				if (TeredoHelper.impendingAppDomainUnload)
				{
					return;
				}
				TeredoHelper.pendingNotifications.Remove(this);
				this.cancelHandle.Dispose();
			}
			this.callback(this.state);
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x00082254 File Offset: 0x00080454
		private void OnStabilized(IntPtr context, IntPtr table)
		{
			UnsafeNetInfoNativeMethods.FreeMibTable(table);
			if (!this.runCallbackCalled)
			{
				lock (this)
				{
					if (!this.runCallbackCalled)
					{
						this.runCallbackCalled = true;
						ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(this.RunCallback), null);
					}
				}
			}
		}

		// Token: 0x04001AEF RID: 6895
		private static List<TeredoHelper> pendingNotifications = new List<TeredoHelper>();

		// Token: 0x04001AF0 RID: 6896
		private static bool impendingAppDomainUnload;

		// Token: 0x04001AF1 RID: 6897
		private readonly Action<object> callback;

		// Token: 0x04001AF2 RID: 6898
		private readonly object state;

		// Token: 0x04001AF3 RID: 6899
		private bool runCallbackCalled;

		// Token: 0x04001AF4 RID: 6900
		private readonly StableUnicastIpAddressTableDelegate onStabilizedDelegate;

		// Token: 0x04001AF5 RID: 6901
		private SafeCancelMibChangeNotify cancelHandle;
	}
}
