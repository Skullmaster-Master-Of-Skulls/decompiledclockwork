using System;

namespace System.Xml.Schema
{
	// Token: 0x020001E7 RID: 487
	internal class Datatype_floatXdr : Datatype_float
	{
		// Token: 0x06001760 RID: 5984 RVA: 0x00064368 File Offset: 0x00063368
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			float num;
			try
			{
				num = XmlConvert.ToSingle(s);
			}
			catch (Exception innerException)
			{
				throw new XmlSchemaException(Res.GetString("Sch_InvalidValue", new object[]
				{
					s
				}), innerException);
			}
			if (float.IsInfinity(num) || float.IsNaN(num))
			{
				throw new XmlSchemaException("Sch_InvalidValue", s);
			}
			return num;
		}
	}
}
