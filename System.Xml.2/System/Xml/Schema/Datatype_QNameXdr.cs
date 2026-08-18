using System;

namespace System.Xml.Schema
{
	// Token: 0x0200023E RID: 574
	internal class Datatype_QNameXdr : Datatype_anySimpleType
	{
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x0600228C RID: 8844 RVA: 0x000B7D20 File Offset: 0x000B5F20
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.QName;
			}
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000B7D24 File Offset: 0x000B5F24
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

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x0600228E RID: 8846 RVA: 0x000B7DA8 File Offset: 0x000B5FA8
		public override Type ValueType
		{
			get
			{
				return Datatype_QNameXdr.atomicValueType;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x0600228F RID: 8847 RVA: 0x000B7DAF File Offset: 0x000B5FAF
		internal override Type ListValueType
		{
			get
			{
				return Datatype_QNameXdr.listValueType;
			}
		}

		// Token: 0x04000EA0 RID: 3744
		private static readonly Type atomicValueType = typeof(XmlQualifiedName);

		// Token: 0x04000EA1 RID: 3745
		private static readonly Type listValueType = typeof(XmlQualifiedName[]);
	}
}
