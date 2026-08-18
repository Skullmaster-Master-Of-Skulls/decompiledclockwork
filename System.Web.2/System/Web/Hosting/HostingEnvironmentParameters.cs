using System;
using System.Collections.Generic;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007AC RID: 1964
	[Serializable]
	internal class HostingEnvironmentParameters
	{
		// Token: 0x17001B24 RID: 6948
		// (get) Token: 0x06005D22 RID: 23842 RVA: 0x0014301F File Offset: 0x0014121F
		// (set) Token: 0x06005D23 RID: 23843 RVA: 0x00143027 File Offset: 0x00141227
		public HostingEnvironmentFlags HostingFlags
		{
			get
			{
				return this._hostingFlags;
			}
			set
			{
				this._hostingFlags = value;
			}
		}

		// Token: 0x17001B25 RID: 6949
		// (get) Token: 0x06005D24 RID: 23844 RVA: 0x00143030 File Offset: 0x00141230
		// (set) Token: 0x06005D25 RID: 23845 RVA: 0x00143038 File Offset: 0x00141238
		public string PrecompilationTargetPhysicalDirectory
		{
			get
			{
				return this._precompTargetPhysicalDir;
			}
			set
			{
				this._precompTargetPhysicalDir = FileUtil.FixUpPhysicalDirectory(value);
			}
		}

		// Token: 0x17001B26 RID: 6950
		// (get) Token: 0x06005D26 RID: 23846 RVA: 0x00143046 File Offset: 0x00141246
		// (set) Token: 0x06005D27 RID: 23847 RVA: 0x0014304E File Offset: 0x0014124E
		public ClientBuildManagerParameter ClientBuildManagerParameter
		{
			get
			{
				return this._clientBuildManagerParameter;
			}
			set
			{
				this._clientBuildManagerParameter = value;
			}
		}

		// Token: 0x17001B27 RID: 6951
		// (get) Token: 0x06005D28 RID: 23848 RVA: 0x00143057 File Offset: 0x00141257
		// (set) Token: 0x06005D29 RID: 23849 RVA: 0x0014305F File Offset: 0x0014125F
		public string IISExpressVersion
		{
			get
			{
				return this._iisExpressVersion;
			}
			set
			{
				this._iisExpressVersion = value;
			}
		}

		// Token: 0x17001B28 RID: 6952
		// (get) Token: 0x06005D2A RID: 23850 RVA: 0x00143068 File Offset: 0x00141268
		// (set) Token: 0x06005D2B RID: 23851 RVA: 0x00143070 File Offset: 0x00141270
		public FcnMode FcnMode { get; set; }

		// Token: 0x17001B29 RID: 6953
		// (get) Token: 0x06005D2C RID: 23852 RVA: 0x00143079 File Offset: 0x00141279
		// (set) Token: 0x06005D2D RID: 23853 RVA: 0x00143081 File Offset: 0x00141281
		public bool FcnSkipReadAndCacheDacls { get; set; }

		// Token: 0x17001B2A RID: 6954
		// (get) Token: 0x06005D2E RID: 23854 RVA: 0x0014308A File Offset: 0x0014128A
		// (set) Token: 0x06005D2F RID: 23855 RVA: 0x00143092 File Offset: 0x00141292
		public KeyValuePair<string, bool>[] ClrQuirksSwitches { get; set; }

		// Token: 0x04003100 RID: 12544
		private HostingEnvironmentFlags _hostingFlags;

		// Token: 0x04003101 RID: 12545
		private ClientBuildManagerParameter _clientBuildManagerParameter;

		// Token: 0x04003102 RID: 12546
		private string _precompTargetPhysicalDir;

		// Token: 0x04003103 RID: 12547
		private string _iisExpressVersion;
	}
}
