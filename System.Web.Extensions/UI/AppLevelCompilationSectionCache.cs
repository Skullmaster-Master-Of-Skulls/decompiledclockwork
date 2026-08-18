using System;
using System.Configuration;
using System.Security;
using System.Security.Permissions;
using System.Web.Configuration;

namespace System.Web.UI
{
	// Token: 0x02000040 RID: 64
	internal sealed class AppLevelCompilationSectionCache : ICompilationSection
	{
		// Token: 0x0600029A RID: 666 RVA: 0x00002050 File Offset: 0x00000250
		private AppLevelCompilationSectionCache()
		{
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00010E63 File Offset: 0x0000F063
		public static AppLevelCompilationSectionCache Instance
		{
			get
			{
				return AppLevelCompilationSectionCache._instance;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00010E6A File Offset: 0x0000F06A
		public bool Debug
		{
			get
			{
				if (this._debug == null)
				{
					this._debug = new bool?(AppLevelCompilationSectionCache.GetDebugFromConfig());
				}
				return this._debug.Value;
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00010E94 File Offset: 0x0000F094
		[SecuritySafeCritical]
		[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool GetDebugFromConfig()
		{
			CompilationSection compilationSection = (CompilationSection)WebConfigurationManager.GetWebApplicationSection("system.web/compilation");
			return compilationSection.Debug;
		}

		// Token: 0x040000F8 RID: 248
		private static readonly AppLevelCompilationSectionCache _instance = new AppLevelCompilationSectionCache();

		// Token: 0x040000F9 RID: 249
		private bool? _debug;
	}
}
