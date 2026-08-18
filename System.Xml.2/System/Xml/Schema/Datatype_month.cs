using System;

namespace System.Xml.Schema
{
	// Token: 0x0200021E RID: 542
	internal class Datatype_month : Datatype_dateTimeBase
	{
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060021D8 RID: 8664 RVA: 0x000B6F52 File Offset: 0x000B5152
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.GMonth;
			}
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000B6F56 File Offset: 0x000B5156
		internal Datatype_month() : base(XsdDateTimeFlags.GMonth)
		{
		}
	}
}
