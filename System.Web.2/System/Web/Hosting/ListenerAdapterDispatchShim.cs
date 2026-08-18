using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x020007E3 RID: 2019
	internal sealed class ListenerAdapterDispatchShim : MarshalByRefObject, IRegisteredObject
	{
		// Token: 0x0600607D RID: 24701 RVA: 0x00047683 File Offset: 0x00045883
		void IRegisteredObject.Stop(bool immediate)
		{
			HostingEnvironment.UnregisterObject(this);
		}

		// Token: 0x0600607E RID: 24702 RVA: 0x0014D9E0 File Offset: 0x0014BBE0
		internal void StartListenerChannel(AppDomainProtocolHandler handler, IListenerChannelCallback listenerCallback)
		{
			IListenerChannelCallback listenerChannelCallback = this.MarshalComProxy(listenerCallback);
			if (listenerChannelCallback != null && handler != null)
			{
				handler.StartListenerChannel(listenerChannelCallback);
			}
		}

		// Token: 0x0600607F RID: 24703 RVA: 0x0014DA04 File Offset: 0x0014BC04
		internal IListenerChannelCallback MarshalComProxy(IListenerChannelCallback defaultDomainCallback)
		{
			IListenerChannelCallback result = null;
			IntPtr iunknownForObject = Marshal.GetIUnknownForObject(defaultDomainCallback);
			if (IntPtr.Zero == iunknownForObject)
			{
				return null;
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				Guid guid = typeof(IListenerChannelCallback).GUID;
				int num = Marshal.QueryInterface(iunknownForObject, ref guid, out zero);
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				result = (IListenerChannelCallback)Marshal.GetObjectForIUnknown(zero);
			}
			finally
			{
				if (IntPtr.Zero != zero)
				{
					Marshal.Release(zero);
				}
				if (IntPtr.Zero != iunknownForObject)
				{
					Marshal.Release(iunknownForObject);
				}
			}
			return result;
		}
	}
}
