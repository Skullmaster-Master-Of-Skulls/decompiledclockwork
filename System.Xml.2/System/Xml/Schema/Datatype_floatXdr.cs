using System;

namespace System.Xml.Schema
{
	// Token: 0x0200023D RID: 573
	internal class Datatype_floatXdr : Datatype_float
	{
		// Token: 0x0600228A RID: 8842 RVA: 0x000B7CB4 File Offset: 0x000B5EB4
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
