using System;

namespace System.Xml.Schema
{
	// Token: 0x02000219 RID: 537
	internal class Datatype_date : Datatype_dateTimeBase
	{
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x000B6F0E File Offset: 0x000B510E
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Date;
			}
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x000B6F12 File Offset: 0x000B5112
		internal Datatype_date() : base(XsdDateTimeFlags.Date)
		{
		}
	}
}
