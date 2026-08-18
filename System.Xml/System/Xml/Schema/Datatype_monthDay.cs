using System;

namespace System.Xml.Schema
{
	// Token: 0x020001C6 RID: 454
	internal class Datatype_monthDay : Datatype_dateTimeBase
	{
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x000635BE File Offset: 0x000625BE
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GMonthDay;
			}
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x000635C2 File Offset: 0x000625C2
		internal Datatype_monthDay() : base(XsdDateTimeFlags.GMonthDay)
		{
		}
	}
}
