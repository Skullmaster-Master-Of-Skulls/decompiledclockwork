using System;

namespace System.Xml.Schema
{
	// Token: 0x020001DB RID: 475
	internal class Datatype_negativeInteger : Datatype_nonPositiveInteger
	{
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00063C1E File Offset: 0x00062C1E
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_negativeInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x00063C25 File Offset: 0x00062C25
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NegativeInteger;
			}
		}

		// Token: 0x04000D93 RID: 3475
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(decimal.MinValue, -1m);
	}
}
