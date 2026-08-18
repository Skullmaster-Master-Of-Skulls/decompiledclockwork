using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020000E6 RID: 230
	public sealed class ScriptingSectionGroup : ConfigurationSectionGroup
	{
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0002AFDF File Offset: 0x000291DF
		[ConfigurationProperty("webServices")]
		public ScriptingWebServicesSectionGroup WebServices
		{
			get
			{
				return (ScriptingWebServicesSectionGroup)base.SectionGroups["webServices"];
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0002AFF6 File Offset: 0x000291F6
		[ConfigurationProperty("scriptResourceHandler")]
		public ScriptingScriptResourceHandlerSection ScriptResourceHandler
		{
			get
			{
				return (ScriptingScriptResourceHandlerSection)base.Sections["scriptResourceHandler"];
			}
		}
	}
}
