using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceModel.Activation;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000822 RID: 2082
	internal class AllowHelper : MarshalByRefObject
	{
		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06004DD8 RID: 19928 RVA: 0x0011C9D4 File Offset: 0x0011ABD4
		private static object ThisLock
		{
			get
			{
				return AllowHelper.thisLock;
			}
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x0011C9DB File Offset: 0x0011ABDB
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x0011C9E0 File Offset: 0x0011ABE0
		private static void EnsureInitialized()
		{
			if (AllowHelper.singleton != null)
			{
				return;
			}
			object obj = AllowHelper.ThisLock;
			lock (obj)
			{
				if (AllowHelper.singleton == null)
				{
					if (AppDomain.CurrentDomain.IsDefaultAppDomain())
					{
						AllowHelper.processWideRefCount = new Dictionary<string, AllowHelper.RegistrationRefCount>();
						AllowHelper.singleton = new AllowHelper();
					}
					else
					{
						Guid clsid = new Guid("CB2F6723-AB3A-11D2-9C40-00C04FA30A3E");
						Guid riid = new Guid("CB2F6722-AB3A-11D2-9C40-00C04FA30A3E");
						ListenerUnsafeNativeMethods.ICorRuntimeHost corRuntimeHost = (ListenerUnsafeNativeMethods.ICorRuntimeHost)RuntimeEnvironment.GetRuntimeInterfaceAsObject(clsid, riid);
						object obj2;
						corRuntimeHost.GetDefaultDomain(out obj2);
						AppDomain appDomain = (AppDomain)obj2;
						if (!appDomain.IsDefaultAppDomain())
						{
							throw Fx.AssertAndThrowFatal("AllowHelper..ctor() GetDefaultDomain did not return the default domain!");
						}
						AllowHelper.singleton = (appDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(AllowHelper).FullName) as AllowHelper);
					}
				}
			}
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x0011CAC8 File Offset: 0x0011ACC8
		public static IDisposable TryAllow(string newSid)
		{
			AllowHelper.EnsureInitialized();
			AllowHelper.singleton.TryAllowCore(newSid);
			return new AllowHelper.RegistrationForAllow(AllowHelper.singleton, newSid);
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x0011CAE8 File Offset: 0x0011ACE8
		private void TryAllowCore(string newSid)
		{
			AllowHelper.EnsureInitialized();
			object obj = AllowHelper.ThisLock;
			lock (obj)
			{
				AllowHelper.RegistrationRefCount registrationRefCount;
				if (!AllowHelper.processWideRefCount.TryGetValue(newSid, out registrationRefCount))
				{
					registrationRefCount = new AllowHelper.RegistrationRefCount(newSid);
				}
				registrationRefCount.AddRef();
			}
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x0011CB44 File Offset: 0x0011AD44
		private void UndoAllow(string grantedSid)
		{
			object obj = AllowHelper.ThisLock;
			lock (obj)
			{
				AllowHelper.RegistrationRefCount registrationRefCount = AllowHelper.processWideRefCount[grantedSid];
				registrationRefCount.RemoveRef();
			}
		}

		// Token: 0x040030B2 RID: 12466
		private static AllowHelper singleton;

		// Token: 0x040030B3 RID: 12467
		private static Dictionary<string, AllowHelper.RegistrationRefCount> processWideRefCount;

		// Token: 0x040030B4 RID: 12468
		private static object thisLock = new object();

		// Token: 0x02000D26 RID: 3366
		private class RegistrationRefCount
		{
			// Token: 0x06007BD6 RID: 31702 RVA: 0x001CEA5C File Offset: 0x001CCC5C
			public RegistrationRefCount(string grantedSid)
			{
				this.grantedSid = grantedSid;
			}

			// Token: 0x06007BD7 RID: 31703 RVA: 0x001CEA6B File Offset: 0x001CCC6B
			public void AddRef()
			{
				if (this.refCount == 0)
				{
					Utility.AddRightGrantedToAccount(new SecurityIdentifier(this.grantedSid), 64);
					AllowHelper.processWideRefCount.Add(this.grantedSid, this);
				}
				this.refCount++;
			}

			// Token: 0x06007BD8 RID: 31704 RVA: 0x001CEAA6 File Offset: 0x001CCCA6
			public void RemoveRef()
			{
				this.refCount--;
				if (this.refCount == 0)
				{
					Utility.RemoveRightGrantedToAccount(new SecurityIdentifier(this.grantedSid), 64);
					AllowHelper.processWideRefCount.Remove(this.grantedSid);
				}
			}

			// Token: 0x04004717 RID: 18199
			private int refCount;

			// Token: 0x04004718 RID: 18200
			private string grantedSid;
		}

		// Token: 0x02000D27 RID: 3367
		private class RegistrationForAllow : IDisposable
		{
			// Token: 0x06007BD9 RID: 31705 RVA: 0x001CEAE1 File Offset: 0x001CCCE1
			public RegistrationForAllow(AllowHelper singleton, string grantedSid)
			{
				this.singleton = singleton;
				this.grantedSid = grantedSid;
			}

			// Token: 0x06007BDA RID: 31706 RVA: 0x001CEAF7 File Offset: 0x001CCCF7
			void IDisposable.Dispose()
			{
				this.singleton.UndoAllow(this.grantedSid);
			}

			// Token: 0x04004719 RID: 18201
			private string grantedSid;

			// Token: 0x0400471A RID: 18202
			private AllowHelper singleton;
		}
	}
}
