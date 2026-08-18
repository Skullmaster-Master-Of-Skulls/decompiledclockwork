using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020000E8 RID: 232
	public sealed class SystemWebExtensionsSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0002B071 File Offset: 0x00029271
		[ConfigurationProperty("scripting")]
		public ScriptingSectionGroup Scripting
		{
			get
			{
				return (ScriptingSectionGroup)base.SectionGroups["scripting"];
			}
		}
	}
}
