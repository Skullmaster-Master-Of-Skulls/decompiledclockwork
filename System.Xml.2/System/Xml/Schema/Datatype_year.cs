using System;

namespace System.Xml.Schema
{
	// Token: 0x0200021B RID: 539
	internal class Datatype_year : Datatype_dateTimeBase
	{
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x000B6F28 File Offset: 0x000B5128
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GYear;
			}
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x000B6F2C File Offset: 0x000B512C
		internal Datatype_year() : base(XsdDateTimeFlags.GYear)
		{
		}
	}
}
