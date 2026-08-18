using System;
using Telerik.Pdf.Gdi.Font;

namespace Telerik.Pdf.Gdi
{
	// Token: 0x02001630 RID: 5680
	public class GdiKerningPairs
	{
		// Token: 0x0600DCE4 RID: 56548 RVA: 0x00304643 File Offset: 0x00302843
		internal GdiKerningPairs(KerningPairs pairs, PdfUnitConverter converter)
		{
			this.pairs = pairs;
			this.converter = converter;
		}

		// Token: 0x170043A9 RID: 17321
		// (get) Token: 0x0600DCE5 RID: 56549 RVA: 0x00304659 File Offset: 0x00302859
		public int Count
		{
			get
			{
				if (this.pairs != null)
				{
					return this.pairs.Length;
				}
				return 0;
			}
		}

		// Token: 0x0600DCE6 RID: 56550 RVA: 0x00304670 File Offset: 0x00302870
		public bool HasPair(int left, int right)
		{
			return this.pairs != null && this.pairs.HasKerning(left, right);
		}

		// Token: 0x170043AA RID: 17322
		public int this[int left, int right]
		{
			get
			{
				return this.converter.ToPdfUnits(this.pairs[left, right]);
			}
		}

		// Token: 0x04003E63 RID: 15971
		public static readonly GdiKerningPairs Empty = new GdiKerningPairs(null, null);

		// Token: 0x04003E64 RID: 15972
		private KerningPairs pairs;

		// Token: 0x04003E65 RID: 15973
		private PdfUnitConverter converter;
	}
}
