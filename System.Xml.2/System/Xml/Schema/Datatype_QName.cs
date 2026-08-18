using System;

namespace System.Xml.Schema
{
	// Token: 0x02000222 RID: 546
	internal class Datatype_QName : Datatype_anySimpleType
	{
		// Token: 0x060021FC RID: 8700 RVA: 0x000B71C2 File Offset: 0x000B53C2
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x000B71CA File Offset: 0x000B53CA
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.qnameFacetsChecker;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x000B71D1 File Offset: 0x000B53D1
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.QName;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x000B71D5 File Offset: 0x000B53D5
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.QName;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x000B71D9 File Offset: 0x000B53D9
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x000B71DD File Offset: 0x000B53DD
		public override Type ValueType
		{
			get
			{
				return Datatype_QName.atomicValueType;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x000B71E4 File Offset: 0x000B53E4
		internal override Type ListValueType
		{
			get
			{
				return Datatype_QName.listValueType;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x000B71EB File Offset: 0x000B53EB
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000B71F0 File Offset: 0x000B53F0
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			if (s == null || s.Length == 0)
			{
				return new XmlSchemaException("Sch_EmptyAttributeValue", string.Empty);
			}
			Exception ex = DatatypeImplementation.qnameFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				XmlQualifiedName xmlQualifiedName = null;
				try
				{
					string text;
					xmlQualifiedName = XmlQualifiedName.Parse(s, nsmgr, out text);
				}
				catch (ArgumentException result)
				{
					return result;
				}
				catch (XmlException result2)
				{
					return result2;
				}
				ex = DatatypeImplementation.qnameFacetsChecker.CheckValueFacets(xmlQualifiedName, this);
				if (ex == null)
				{
					typedValue = xmlQualifiedName;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x04000E80 RID: 3712
		private static readonly Type atomicValueType = typeof(XmlQualifiedName);

		// Token: 0x04000E81 RID: 3713
		private static readonly Type listValueType = typeof(XmlQualifiedName[]);
	}
}
