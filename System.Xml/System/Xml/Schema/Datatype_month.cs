using System;

namespace System.Xml.Schema
{
	// Token: 0x020001C8 RID: 456
	internal class Datatype_month : Datatype_dateTimeBase
	{
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x000635DA File Offset: 0x000625DA
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GMonth;
			}
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x000635DE File Offset: 0x000625DE
		internal Datatype_month() : base(XsdDateTimeFlags.GMonth)
		{
		}
	}
}
