using System;

namespace System.Xml.Schema
{
	// Token: 0x020001DA RID: 474
	internal class Datatype_nonPositiveInteger : Datatype_integer
	{
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x00063BE8 File Offset: 0x00062BE8
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_nonPositiveInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x00063BEF File Offset: 0x00062BEF
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NonPositiveInteger;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x00063BF3 File Offset: 0x00062BF3
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000D92 RID: 3474
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(decimal.MinValue, 0m);
	}
}
