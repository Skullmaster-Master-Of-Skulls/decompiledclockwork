using System;

namespace System.Xml.Schema
{
	// Token: 0x0200023B RID: 571
	internal class Datatype_positiveInteger : Datatype_nonNegativeInteger
	{
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x000B7C17 File Offset: 0x000B5E17
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return Datatype_positiveInteger.numeric10FacetsChecker;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x000B7C1E File Offset: 0x000B5E1E
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.PositiveInteger;
			}
		}

		// Token: 0x04000E9F RID: 3743
		private static readonly FacetsChecker numeric10FacetsChecker = new Numeric10FacetsChecker(1m, decimal.MaxValue);
	}
}
