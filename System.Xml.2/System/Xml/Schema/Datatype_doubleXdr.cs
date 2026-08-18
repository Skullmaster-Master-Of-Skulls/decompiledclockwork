using System;

namespace System.Xml.Schema
{
	// Token: 0x0200023C RID: 572
	internal class Datatype_doubleXdr : Datatype_double
	{
		// Token: 0x06002288 RID: 8840 RVA: 0x000B7C48 File Offset: 0x000B5E48
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			double num;
			try
			{
				num = XmlConvert.ToDouble(s);
			}
			catch (Exception innerException)
			{
				throw new XmlSchemaException(Res.GetString("Sch_InvalidValue", new object[]
				{
					s
				}), innerException);
			}
			if (double.IsInfinity(num) || double.IsNaN(num))
			{
				throw new XmlSchemaException("Sch_InvalidValue", s);
			}
			return num;
		}
	}
}
