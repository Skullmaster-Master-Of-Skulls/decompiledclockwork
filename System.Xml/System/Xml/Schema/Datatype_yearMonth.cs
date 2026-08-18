using System;

namespace System.Xml.Schema
{
	// Token: 0x020001C4 RID: 452
	internal class Datatype_yearMonth : Datatype_dateTimeBase
	{
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x000635A3 File Offset: 0x000625A3
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GYearMonth;
			}
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x000635A7 File Offset: 0x000625A7
		internal Datatype_yearMonth() : base(XsdDateTimeFlags.GYearMonth)
		{
		}
	}
}
