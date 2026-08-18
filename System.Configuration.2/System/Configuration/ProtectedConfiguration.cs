using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x0200007B RID: 123
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public static class ProtectedConfiguration
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0001949C File Offset: 0x0001769C
		public static ProtectedConfigurationProviderCollection Providers
		{
			get
			{
				ProtectedConfigurationSection protectedConfigurationSection = PrivilegedConfigurationManager.GetSection("configProtectedData") as ProtectedConfigurationSection;
				if (protectedConfigurationSection == null)
				{
					return new ProtectedConfigurationProviderCollection();
				}
				return protectedConfigurationSection.GetAllProviders();
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000194C8 File Offset: 0x000176C8
		public static string DefaultProvider
		{
			get
			{
				ProtectedConfigurationSection protectedConfigurationSection = PrivilegedConfigurationManager.GetSection("configProtectedData") as ProtectedConfigurationSection;
				if (protectedConfigurationSection != null)
				{
					return protectedConfigurationSection.DefaultProvider;
				}
				return "";
			}
		}

		// Token: 0x040002CE RID: 718
		public const string RsaProviderName = "RsaProtectedConfigurationProvider";

		// Token: 0x040002CF RID: 719
		public const string DataProtectionProviderName = "DataProtectionConfigurationProvider";

		// Token: 0x040002D0 RID: 720
		public const string ProtectedDataSectionName = "configProtectedData";
	}
}
