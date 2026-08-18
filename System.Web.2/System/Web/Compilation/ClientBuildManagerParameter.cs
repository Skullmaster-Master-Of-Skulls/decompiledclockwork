using System;
using System.Collections.Generic;

namespace System.Web.Compilation
{
	// Token: 0x02000829 RID: 2089
	[Serializable]
	public class ClientBuildManagerParameter
	{
		// Token: 0x17001C30 RID: 7216
		// (get) Token: 0x060063B3 RID: 25523 RVA: 0x0015D7B0 File Offset: 0x0015B9B0
		public List<string> ExcludedVirtualPaths
		{
			get
			{
				if (this._excludedVirtualPaths == null)
				{
					this._excludedVirtualPaths = new List<string>();
				}
				return this._excludedVirtualPaths;
			}
		}

		// Token: 0x17001C31 RID: 7217
		// (get) Token: 0x060063B4 RID: 25524 RVA: 0x0015D7CB File Offset: 0x0015B9CB
		// (set) Token: 0x060063B5 RID: 25525 RVA: 0x0015D7D3 File Offset: 0x0015B9D3
		public PrecompilationFlags PrecompilationFlags
		{
			get
			{
				return this._precompilationFlags;
			}
			set
			{
				this._precompilationFlags = value;
			}
		}

		// Token: 0x17001C32 RID: 7218
		// (get) Token: 0x060063B6 RID: 25526 RVA: 0x0015D7DC File Offset: 0x0015B9DC
		// (set) Token: 0x060063B7 RID: 25527 RVA: 0x0015D7E4 File Offset: 0x0015B9E4
		public string StrongNameKeyFile
		{
			get
			{
				return this._strongNameKeyFile;
			}
			set
			{
				this._strongNameKeyFile = value;
			}
		}

		// Token: 0x17001C33 RID: 7219
		// (get) Token: 0x060063B8 RID: 25528 RVA: 0x0015D7ED File Offset: 0x0015B9ED
		// (set) Token: 0x060063B9 RID: 25529 RVA: 0x0015D7F5 File Offset: 0x0015B9F5
		public string StrongNameKeyContainer
		{
			get
			{
				return this._strongNameKeyContainer;
			}
			set
			{
				this._strongNameKeyContainer = value;
			}
		}

		// Token: 0x040033A4 RID: 13220
		private string _strongNameKeyFile;

		// Token: 0x040033A5 RID: 13221
		private string _strongNameKeyContainer;

		// Token: 0x040033A6 RID: 13222
		private PrecompilationFlags _precompilationFlags;

		// Token: 0x040033A7 RID: 13223
		private List<string> _excludedVirtualPaths;
	}
}
