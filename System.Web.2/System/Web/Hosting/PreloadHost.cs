using System;
using System.Configuration;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007D6 RID: 2006
	internal sealed class PreloadHost : MarshalByRefObject, IRegisteredObject
	{
		// Token: 0x0600602D RID: 24621 RVA: 0x000474BC File Offset: 0x000456BC
		public PreloadHost()
		{
			HostingEnvironment.RegisterObject(this);
		}

		// Token: 0x0600602E RID: 24622 RVA: 0x0014C69C File Offset: 0x0014A89C
		public void CreateIProcessHostPreloadClientInstanceAndCallPreload(string preloadObjTypeName, string[] paramsForStartupObj)
		{
			using (new ApplicationImpersonationContext())
			{
				Type type = null;
				try
				{
					type = Type.GetType(preloadObjTypeName, true);
				}
				catch (Exception e)
				{
					throw new InvalidOperationException(Misc.FormatExceptionMessage(e, new string[]
					{
						SR.GetString("Failure_Create_Application_Preload_Provider_Type", new object[]
						{
							preloadObjTypeName
						})
					}));
				}
				if (!typeof(IProcessHostPreloadClient).IsAssignableFrom(type))
				{
					throw new ConfigurationErrorsException(SR.GetString("Invalid_Application_Preload_Provider_Type", new object[]
					{
						preloadObjTypeName
					}));
				}
				IProcessHostPreloadClient processHostPreloadClient = (IProcessHostPreloadClient)Activator.CreateInstance(type);
				processHostPreloadClient.Preload(paramsForStartupObj);
			}
		}

		// Token: 0x17001B83 RID: 7043
		// (get) Token: 0x0600602F RID: 24623 RVA: 0x001436D4 File Offset: 0x001418D4
		internal Exception InitializationException
		{
			get
			{
				return HttpRuntime.InitializationException;
			}
		}

		// Token: 0x06006030 RID: 24624 RVA: 0x00047683 File Offset: 0x00045883
		void IRegisteredObject.Stop(bool immediate)
		{
			HostingEnvironment.UnregisterObject(this);
		}

		// Token: 0x06006031 RID: 24625 RVA: 0x0000298D File Offset: 0x00000B8D
		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
