using System;

namespace System.Xml.Schema
{
	// Token: 0x020001C5 RID: 453
	internal class Datatype_year : Datatype_dateTimeBase
	{
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060016A8 RID: 5800 RVA: 0x000635B0 File Offset: 0x000625B0
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GYear;
			}
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x000635B4 File Offset: 0x000625B4
		internal Datatype_year() : base(XsdDateTimeFlags.GYear)
		{
		}
	}
}
