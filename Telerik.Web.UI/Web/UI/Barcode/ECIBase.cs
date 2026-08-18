using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Barcode
{
	// Token: 0x020009E0 RID: 2528
	public abstract class ECIBase
	{
		// Token: 0x17001FD3 RID: 8147
		// (get) Token: 0x060060C3 RID: 24771 RVA: 0x0012C8A1 File Offset: 0x0012AAA1
		// (set) Token: 0x060060C4 RID: 24772 RVA: 0x0012C8A9 File Offset: 0x0012AAA9
		public List<string> UnicodeValues
		{
			get
			{
				return this.unicodeValues;
			}
			set
			{
				this.unicodeValues = value;
			}
		}

		// Token: 0x17001FD4 RID: 8148
		// (get) Token: 0x060060C5 RID: 24773 RVA: 0x0012C8B2 File Offset: 0x0012AAB2
		// (set) Token: 0x060060C6 RID: 24774 RVA: 0x0012C8BA File Offset: 0x0012AABA
		public List<string> EncodedValues
		{
			get
			{
				return this.encodedValues;
			}
			set
			{
				this.encodedValues = value;
			}
		}

		// Token: 0x0400178D RID: 6029
		private List<string> unicodeValues;

		// Token: 0x0400178E RID: 6030
		private List<string> encodedValues;
	}
}
