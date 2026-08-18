using System;
using System.Web.Compilation;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x0200028A RID: 650
	[Serializable]
	internal class HostingEnvironmentParameters
	{
		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x0009262A File Offset: 0x0009162A
		// (set) Token: 0x06002155 RID: 8533 RVA: 0x00092632 File Offset: 0x00091632
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

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x0009263B File Offset: 0x0009163B
		// (set) Token: 0x06002157 RID: 8535 RVA: 0x00092643 File Offset: 0x00091643
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

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x00092651 File Offset: 0x00091651
		// (set) Token: 0x06002159 RID: 8537 RVA: 0x00092659 File Offset: 0x00091659
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

		// Token: 0x04001B06 RID: 6918
		private HostingEnvironmentFlags _hostingFlags;

		// Token: 0x04001B07 RID: 6919
		private ClientBuildManagerParameter _clientBuildManagerParameter;

		// Token: 0x04001B08 RID: 6920
		private string _precompTargetPhysicalDir;
	}
}
