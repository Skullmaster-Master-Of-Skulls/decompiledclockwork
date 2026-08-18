using System;

namespace Telerik.Web.UI.HtmlParsing
{
	// Token: 0x020011DF RID: 4575
	internal class Token
	{
		// Token: 0x17003CF3 RID: 15603
		// (get) Token: 0x0600BCEB RID: 48363 RVA: 0x0029E79D File Offset: 0x0029C99D
		// (set) Token: 0x0600BCEC RID: 48364 RVA: 0x0029E7A5 File Offset: 0x0029C9A5
		internal TokenType Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x17003CF4 RID: 15604
		// (get) Token: 0x0600BCED RID: 48365 RVA: 0x0029E7AE File Offset: 0x0029C9AE
		// (set) Token: 0x0600BCEE RID: 48366 RVA: 0x0029E7B6 File Offset: 0x0029C9B6
		internal string Contents
		{
			get
			{
				return this._contents;
			}
			set
			{
				this._contents = value;
			}
		}

		// Token: 0x17003CF5 RID: 15605
		// (get) Token: 0x0600BCEF RID: 48367 RVA: 0x0029E7BF File Offset: 0x0029C9BF
		// (set) Token: 0x0600BCF0 RID: 48368 RVA: 0x0029E7C7 File Offset: 0x0029C9C7
		internal int Offset
		{
			get
			{
				return this._offset;
			}
			set
			{
				this._offset = value;
			}
		}

		// Token: 0x040031B4 RID: 12724
		private TokenType _type;

		// Token: 0x040031B5 RID: 12725
		private string _contents = string.Empty;

		// Token: 0x040031B6 RID: 12726
		private int _offset;
	}
}
