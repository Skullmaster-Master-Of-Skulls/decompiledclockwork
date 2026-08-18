using System;

namespace System.Xml.Schema
{
	// Token: 0x0200021C RID: 540
	internal class Datatype_monthDay : Datatype_dateTimeBase
	{
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x000B6F36 File Offset: 0x000B5136
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GMonthDay;
			}
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x000B6F3A File Offset: 0x000B513A
		internal Datatype_monthDay() : base(XsdDateTimeFlags.GMonthDay)
		{
		}
	}
}
