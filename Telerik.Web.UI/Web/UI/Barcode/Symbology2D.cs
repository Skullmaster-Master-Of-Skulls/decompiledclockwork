using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009F4 RID: 2548
	internal abstract class Symbology2D
	{
		// Token: 0x17001FD5 RID: 8149
		// (get) Token: 0x06006101 RID: 24833 RVA: 0x00146AC6 File Offset: 0x00144CC6
		// (set) Token: 0x06006102 RID: 24834 RVA: 0x00146ACE File Offset: 0x00144CCE
		public List<char> CharSet
		{
			get
			{
				return this.charset;
			}
			set
			{
				this.charset = value;
			}
		}

		// Token: 0x17001FD6 RID: 8150
		// (get) Token: 0x06006103 RID: 24835 RVA: 0x00146AD7 File Offset: 0x00144CD7
		// (set) Token: 0x06006104 RID: 24836 RVA: 0x00146ADF File Offset: 0x00144CDF
		public Dictionary<char, string> Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = value;
			}
		}

		// Token: 0x0400178F RID: 6031
		private List<char> charset;

		// Token: 0x04001790 RID: 6032
		private Dictionary<char, string> encoding;
	}
}
