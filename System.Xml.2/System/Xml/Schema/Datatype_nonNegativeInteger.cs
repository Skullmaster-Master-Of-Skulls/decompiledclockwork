using System;

namespace System.Xml.Schema
{
	// Token: 0x02000236 RID: 566
	internal class Datatype_nonNegativeInteger : Datatype_integer
	{
		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x000B78DA File Offset: 0x000B5ADA
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_nonNegativeInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x000B78E1 File Offset: 0x000B5AE1
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NonNegativeInteger;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x000B78E5 File Offset: 0x000B5AE5
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000E92 RID: 3730
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(0m, decimal.MaxValue);
	}
}
