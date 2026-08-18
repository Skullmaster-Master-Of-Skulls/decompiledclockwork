using System;

namespace System.Xml.Schema
{
	// Token: 0x0200021A RID: 538
	internal class Datatype_yearMonth : Datatype_dateTimeBase
	{
		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x060021D0 RID: 8656 RVA: 0x000B6F1B File Offset: 0x000B511B
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GYearMonth;
			}
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x000B6F1F File Offset: 0x000B511F
		internal Datatype_yearMonth() : base(XsdDateTimeFlags.GYearMonth)
		{
		}
	}
}
