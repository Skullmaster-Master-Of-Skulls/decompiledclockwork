using System;
using System.Configuration;
using System.Security.Permissions;

namespace System.Web.Configuration
{
	// Token: 0x020006B8 RID: 1720
	internal class ClientRuntimeConfig : RuntimeConfig
	{
		// Token: 0x0600532F RID: 21295 RVA: 0x00124A03 File Offset: 0x00122C03
		internal ClientRuntimeConfig() : base(null, false)
		{
		}

		// Token: 0x06005330 RID: 21296 RVA: 0x00124A0D File Offset: 0x00122C0D
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		protected override object GetSectionObject(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
