using System;

namespace System.Xml.Schema
{
	// Token: 0x020001E0 RID: 480
	internal class Datatype_nonNegativeInteger : Datatype_integer
	{
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x00063F82 File Offset: 0x00062F82
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_nonNegativeInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x00063F89 File Offset: 0x00062F89
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NonNegativeInteger;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x00063F8D File Offset: 0x00062F8D
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000DA0 RID: 3488
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, decimal.MaxValue);
	}
}
