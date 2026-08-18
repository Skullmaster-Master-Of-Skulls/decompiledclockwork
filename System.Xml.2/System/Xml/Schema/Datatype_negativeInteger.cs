using System;

namespace System.Xml.Schema
{
	// Token: 0x02000231 RID: 561
	internal class Datatype_negativeInteger : Datatype_nonPositiveInteger
	{
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x000B7579 File Offset: 0x000B5779
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_negativeInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x000B7580 File Offset: 0x000B5780
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NegativeInteger;
			}
		}

		// Token: 0x04000E85 RID: 3717
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(decimal.MinValue, -1m);
	}
}
