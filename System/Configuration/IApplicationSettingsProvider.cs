using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x020006F9 RID: 1785
	public interface IApplicationSettingsProvider
	{
		// Token: 0x06003724 RID: 14116
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty property);

		// Token: 0x06003725 RID: 14117
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		void Reset(SettingsContext context);

		// Token: 0x06003726 RID: 14118
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		void Upgrade(SettingsContext context, SettingsPropertyCollection properties);
	}
}
