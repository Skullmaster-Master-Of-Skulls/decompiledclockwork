using System;

namespace System.Xml.Schema
{
	// Token: 0x020001C3 RID: 451
	internal class Datatype_date : Datatype_dateTimeBase
	{
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060016A4 RID: 5796 RVA: 0x00063596 File Offset: 0x00062596
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Date;
			}
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x0006359A File Offset: 0x0006259A
		internal Datatype_date() : base(XsdDateTimeFlags.Date)
		{
		}
	}
}
