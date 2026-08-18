using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020000E7 RID: 231
	public sealed class ScriptingWebServicesSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0002B015 File Offset: 0x00029215
		[ConfigurationProperty("jsonSerialization")]
		public ScriptingJsonSerializationSection JsonSerialization
		{
			get
			{
				return (ScriptingJsonSerializationSection)base.Sections["jsonSerialization"];
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0002B02C File Offset: 0x0002922C
		[ConfigurationProperty("profileService")]
		public ScriptingProfileServiceSection ProfileService
		{
			get
			{
				return (ScriptingProfileServiceSection)base.Sections["profileService"];
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0002B043 File Offset: 0x00029243
		[ConfigurationProperty("authenticationService")]
		public ScriptingAuthenticationServiceSection AuthenticationService
		{
			get
			{
				return (ScriptingAuthenticationServiceSection)base.Sections["authenticationService"];
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0002B05A File Offset: 0x0002925A
		[ConfigurationProperty("roleService")]
		public ScriptingRoleServiceSection RoleService
		{
			get
			{
				return (ScriptingRoleServiceSection)base.Sections["roleService"];
			}
		}
	}
}
