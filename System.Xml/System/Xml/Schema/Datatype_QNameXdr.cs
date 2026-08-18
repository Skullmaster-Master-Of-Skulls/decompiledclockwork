using System;

namespace System.Xml.Schema
{
	// Token: 0x020001E8 RID: 488
	internal class Datatype_QNameXdr : Datatype_anySimpleType
	{
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x000643D8 File Offset: 0x000633D8
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.QName;
			}
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x000643DC File Offset: 0x000633DC
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			if (s == null || s.Length == 0)
			{
				throw new XmlSchemaException("Sch_EmptyAttributeValue", string.Empty);
			}
			if (nsmgr == null)
			{
				throw new ArgumentNullException("nsmgr");
			}
			object result;
			try
			{
				string text;
				result = XmlQualifiedName.Parse(s.Trim(), nsmgr, out text);
			}
			catch (XmlSchemaException ex)
			{
				throw ex;
			}
			catch (Exception innerException)
			{
				throw new XmlSchemaException(Res.GetString("Sch_InvalidValue", new object[]
				{
					s
				}), innerException);
			}
			return result;
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001764 RID: 5988 RVA: 0x00064464 File Offset: 0x00063464
		public override Type ValueType
		{
			get
			{
				return Datatype_QNameXdr.atomicValueType;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0006446B File Offset: 0x0006346B
		internal override Type ListValueType
		{
			get
			{
				return Datatype_QNameXdr.listValueType;
			}
		}

		// Token: 0x04000DAE RID: 3502
		private static readonly Type atomicValueType = typeof(XmlQualifiedName);

		// Token: 0x04000DAF RID: 3503
		private static readonly Type listValueType = typeof(XmlQualifiedName[]);
	}
}
