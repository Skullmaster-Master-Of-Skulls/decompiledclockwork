using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000165 RID: 357
	internal sealed class ComponentManagerBroker : MarshalByRefObject
	{
		// Token: 0x06000EC8 RID: 3784 RVA: 0x0002C61C File Offset: 0x0002A81C
		static ComponentManagerBroker()
		{
			int currentProcessId = SafeNativeMethods.GetCurrentProcessId();
			ComponentManagerBroker._syncObject = new object();
			ComponentManagerBroker._remoteObjectName = string.Format(CultureInfo.CurrentCulture, "ComponentManagerBroker.{0}.{1:X}", new object[]
			{
				Application.WindowsFormsVersion,
				currentProcessId
			});
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0002C664 File Offset: 0x0002A864
		public ComponentManagerBroker()
		{
			if (ComponentManagerBroker._broker == null)
			{
				ComponentManagerBroker._broker = this;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0002C679 File Offset: 0x0002A879
		internal ComponentManagerBroker Singleton
		{
			get
			{
				return ComponentManagerBroker._broker;
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0002C680 File Offset: 0x0002A880
		internal void ClearComponentManager()
		{
			this._proxy = null;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00015ECC File Offset: 0x000140CC
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0002C68C File Offset: 0x0002A88C
		public UnsafeNativeMethods.IMsoComponentManager GetProxy(long pCM)
		{
			if (this._proxy == null)
			{
				UnsafeNativeMethods.IMsoComponentManager original = (UnsafeNativeMethods.IMsoComponentManager)Marshal.GetObjectForIUnknown((IntPtr)pCM);
				this._proxy = new ComponentManagerProxy(this, original);
			}
			return this._proxy;
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0002C6C8 File Offset: 0x0002A8C8
		internal static UnsafeNativeMethods.IMsoComponentManager GetComponentManager(IntPtr pOriginal)
		{
			object syncObject = ComponentManagerBroker._syncObject;
			lock (syncObject)
			{
				if (ComponentManagerBroker._broker == null)
				{
					UnsafeNativeMethods.ICorRuntimeHost corRuntimeHost = (UnsafeNativeMethods.ICorRuntimeHost)RuntimeEnvironment.GetRuntimeInterfaceAsObject(typeof(UnsafeNativeMethods.CorRuntimeHost).GUID, typeof(UnsafeNativeMethods.ICorRuntimeHost).GUID);
					object obj;
					int defaultDomain = corRuntimeHost.GetDefaultDomain(out obj);
					AppDomain appDomain = obj as AppDomain;
					if (appDomain == null)
					{
						appDomain = AppDomain.CurrentDomain;
					}
					if (appDomain == AppDomain.CurrentDomain)
					{
						ComponentManagerBroker._broker = new ComponentManagerBroker();
					}
					else
					{
						ComponentManagerBroker._broker = ComponentManagerBroker.GetRemotedComponentManagerBroker(appDomain);
					}
				}
			}
			return ComponentManagerBroker._broker.GetProxy((long)pOriginal);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0002C780 File Offset: 0x0002A980
		private static ComponentManagerBroker GetRemotedComponentManagerBroker(AppDomain domain)
		{
			Type typeFromHandle = typeof(ComponentManagerBroker);
			ComponentManagerBroker componentManagerBroker = (ComponentManagerBroker)domain.CreateInstanceAndUnwrap(typeFromHandle.Assembly.FullName, typeFromHandle.FullName);
			return componentManagerBroker.Singleton;
		}

		// Token: 0x04000801 RID: 2049
		private static object _syncObject;

		// Token: 0x04000802 RID: 2050
		private static string _remoteObjectName;

		// Token: 0x04000803 RID: 2051
		private static ComponentManagerBroker _broker;

		// Token: 0x04000804 RID: 2052
		[ThreadStatic]
		private ComponentManagerProxy _proxy;
	}
}
