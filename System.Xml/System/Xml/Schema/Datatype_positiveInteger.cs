using System;

namespace System.Xml.Schema
{
	// Token: 0x020001E5 RID: 485
	internal class Datatype_positiveInteger : Datatype_nonNegativeInteger
	{
		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x000642C8 File Offset: 0x000632C8
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_positiveInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x0600175B RID: 5979 RVA: 0x000642CF File Offset: 0x000632CF
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.PositiveInteger;
			}
		}

		// Token: 0x04000DAD RID: 3501
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(1m, decimal.MaxValue);
	}
}
