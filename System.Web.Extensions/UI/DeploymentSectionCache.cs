using System;
using System.Configuration;
using System.Security;
using System.Security.Permissions;
using System.Web.Configuration;

namespace System.Web.UI
{
	// Token: 0x0200004D RID: 77
	internal sealed class DeploymentSectionCache : IDeploymentSection
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x00002050 File Offset: 0x00000250
		private DeploymentSectionCache()
		{
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00011DB3 File Offset: 0x0000FFB3
		public static DeploymentSectionCache Instance
		{
			get
			{
				return DeploymentSectionCache._instance;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00011DBA File Offset: 0x0000FFBA
		public bool Retail
		{
			get
			{
				if (this._retail == null)
				{
					this._retail = new bool?(DeploymentSectionCache.GetRetailFromConfig());
				}
				return this._retail.Value;
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00011DE4 File Offset: 0x0000FFE4
		[SecuritySafeCritical]
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool GetRetailFromConfig()
		{
			DeploymentSection deploymentSection = (DeploymentSection)WebConfigurationManager.GetSection("system.web/deployment");
			return deploymentSection.Retail;
		}

		// Token: 0x04000114 RID: 276
		private static readonly DeploymentSectionCache _instance = new DeploymentSectionCache();

		// Token: 0x04000115 RID: 277
		private bool? _retail;
	}
}
