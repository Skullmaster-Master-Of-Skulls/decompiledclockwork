using System;

namespace System.Web.UI
{
	// Token: 0x02000314 RID: 788
	internal abstract class SourceLineInfo
	{
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060024F2 RID: 9458 RVA: 0x0007A4A8 File Offset: 0x000786A8
		// (set) Token: 0x060024F3 RID: 9459 RVA: 0x0007A4B0 File Offset: 0x000786B0
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

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060024F4 RID: 9460 RVA: 0x0007A4B9 File Offset: 0x000786B9
		// (set) Token: 0x060024F5 RID: 9461 RVA: 0x0007A4C1 File Offset: 0x000786C1
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

		// Token: 0x04001D56 RID: 7510
		private string _virtualPath;

		// Token: 0x04001D57 RID: 7511
		private int _line;
	}
}
