using System;

namespace System.Configuration.Internal
{
	// Token: 0x020000A9 RID: 169
	internal class ConfigSystem : IConfigSystem
	{
		// Token: 0x060006A8 RID: 1704 RVA: 0x0001F5AC File Offset: 0x0001D7AC
		void IConfigSystem.Init(Type typeConfigHost, params object[] hostInitParams)
		{
			this._configRoot = new InternalConfigRoot();
			this._configHost = (IInternalConfigHost)TypeUtil.CreateInstanceWithReflectionPermission(typeConfigHost);
			this._configRoot.Init(this._configHost, false);
			this._configHost.Init(this._configRoot, hostInitParams);
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001F5F9 File Offset: 0x0001D7F9
		IInternalConfigHost IConfigSystem.Host
		{
			get
			{
				return this._configHost;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0001F601 File Offset: 0x0001D801
		IInternalConfigRoot IConfigSystem.Root
		{
			get
			{
				return this._configRoot;
			}
		}

		// Token: 0x0400044C RID: 1100
		private IInternalConfigRoot _configRoot;

		// Token: 0x0400044D RID: 1101
		private IInternalConfigHost _configHost;
	}
}
