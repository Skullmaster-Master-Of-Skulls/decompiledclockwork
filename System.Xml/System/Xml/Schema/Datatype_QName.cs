using System;

namespace System.Xml.Schema
{
	// Token: 0x020001CC RID: 460
	internal class Datatype_QName : Datatype_anySimpleType
	{
		// Token: 0x060016D2 RID: 5842 RVA: 0x0006384A File Offset: 0x0006284A
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x00063852 File Offset: 0x00062852
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.qnameFacetsChecker;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x00063859 File Offset: 0x00062859
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.QName;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0006385D File Offset: 0x0006285D
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.QName;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060016D6 RID: 5846 RVA: 0x00063861 File Offset: 0x00062861
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x00063865 File Offset: 0x00062865
		public override Type ValueType
		{
			get
			{
				return Datatype_QName.atomicValueType;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x0006386C File Offset: 0x0006286C
		internal override Type ListValueType
		{
			get
			{
				return Datatype_QName.listValueType;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x00063873 File Offset: 0x00062873
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00063878 File Offset: 0x00062878
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

		// Token: 0x04000D8E RID: 3470
		private static readonly Type atomicValueType = typeof(XmlQualifiedName);

		// Token: 0x04000D8F RID: 3471
		private static readonly Type listValueType = typeof(XmlQualifiedName[]);
	}
}
