using System;

namespace System.Xml.Schema
{
	// Token: 0x020001E6 RID: 486
	internal class Datatype_doubleXdr : Datatype_double
	{
		// Token: 0x0600175E RID: 5982 RVA: 0x000642F8 File Offset: 0x000632F8
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
