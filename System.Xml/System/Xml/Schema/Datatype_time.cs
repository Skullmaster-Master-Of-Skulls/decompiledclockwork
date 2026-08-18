using System;

namespace System.Xml.Schema
{
	// Token: 0x020001C2 RID: 450
	internal class Datatype_time : Datatype_dateTimeBase
	{
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00063589 File Offset: 0x00062589
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Time;
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0006358D File Offset: 0x0006258D
		internal Datatype_time() : base(XsdDateTimeFlags.Time)
		{
		}
	}
}
