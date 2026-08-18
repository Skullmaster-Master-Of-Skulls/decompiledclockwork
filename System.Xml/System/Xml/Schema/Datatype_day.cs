using System;

namespace System.Xml.Schema
{
	// Token: 0x020001C7 RID: 455
	internal class Datatype_day : Datatype_dateTimeBase
	{
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x000635CC File Offset: 0x000625CC
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GDay;
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x000635D0 File Offset: 0x000625D0
		internal Datatype_day() : base(XsdDateTimeFlags.GDay)
		{
		}
	}
}
