using System;

namespace System.Xml.Schema
{
	// Token: 0x0200021D RID: 541
	internal class Datatype_day : Datatype_dateTimeBase
	{
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x000B6F44 File Offset: 0x000B5144
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GDay;
			}
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x000B6F48 File Offset: 0x000B5148
		internal Datatype_day() : base(XsdDateTimeFlags.GDay)
		{
		}
	}
}
