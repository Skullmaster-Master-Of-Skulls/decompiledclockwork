using System;

namespace System.Web.UI
{
	// Token: 0x02000388 RID: 904
	internal abstract class SourceLineInfo
	{
		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002C3A RID: 11322 RVA: 0x000C5CD0 File Offset: 0x000C4CD0
		// (set) Token: 0x06002C3B RID: 11323 RVA: 0x000C5CD8 File Offset: 0x000C4CD8
		internal string VirtualPath
		{
			get
			{
				return this._virtualPath;
			}
			set
			{
				this._virtualPath = value;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002C3C RID: 11324 RVA: 0x000C5CE1 File Offset: 0x000C4CE1
		// (set) Token: 0x06002C3D RID: 11325 RVA: 0x000C5CE9 File Offset: 0x000C4CE9
		internal int Line
		{
			get
			{
				return this._line;
			}
			set
			{
				this._line = value;
			}
		}

		// Token: 0x04002083 RID: 8323
		private string _virtualPath;

		// Token: 0x04002084 RID: 8324
		private int _line;
	}
}
