using System;

namespace System.Xml.Schema
{
	// Token: 0x02000230 RID: 560
	internal class Datatype_nonPositiveInteger : Datatype_integer
	{
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x000B7548 File Offset: 0x000B5748
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_nonPositiveInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x000B754F File Offset: 0x000B574F
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NonPositiveInteger;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x000B7553 File Offset: 0x000B5753
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000E84 RID: 3716
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(decimal.MinValue, 0m);
	}
}
