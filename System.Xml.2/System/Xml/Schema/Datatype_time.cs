using System;

namespace System.Xml.Schema
{
	// Token: 0x02000218 RID: 536
	internal class Datatype_time : Datatype_dateTimeBase
	{
		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x060021CC RID: 8652 RVA: 0x000B6F01 File Offset: 0x000B5101
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Time;
			}
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x000B6F05 File Offset: 0x000B5105
		internal Datatype_time() : base(XsdDateTimeFlags.Time)
		{
		}
	}
}
