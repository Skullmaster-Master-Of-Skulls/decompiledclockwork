using System;

namespace System.Xml.Schema
{
	// Token: 0x0200020A RID: 522
	internal class Datatype_string : Datatype_anySimpleType
	{
		// Token: 0x06002176 RID: 8566 RVA: 0x000B6842 File Offset: 0x000B4A42
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlStringConverter.Create(schemaType);
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002177 RID: 8567 RVA: 0x000B684A File Offset: 0x000B4A4A
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x000B684D File Offset: 0x000B4A4D
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.stringFacetsChecker;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06002179 RID: 8569 RVA: 0x000B6854 File Offset: 0x000B4A54
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.String;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x000B6858 File Offset: 0x000B4A58
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x000B685B File Offset: 0x000B4A5B
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x000B6860 File Offset: 0x000B4A60
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.stringFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ex = DatatypeImplementation.stringFacetsChecker.CheckValueFacets(s, this);
				if (ex == null)
				{
					typedValue = s;
					return null;
				}
			}
			return ex;
		}
	}
}
