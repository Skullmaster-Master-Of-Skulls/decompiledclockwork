using System;

namespace System.Xml.Schema
{
	// Token: 0x020001B4 RID: 436
	internal class Datatype_string : Datatype_anySimpleType
	{
		// Token: 0x0600164C RID: 5708 RVA: 0x00062EC0 File Offset: 0x00061EC0
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlStringConverter.Create(schemaType);
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x00062EC8 File Offset: 0x00061EC8
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x00062ECB File Offset: 0x00061ECB
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.stringFacetsChecker;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x00062ED2 File Offset: 0x00061ED2
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.String;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00062ED6 File Offset: 0x00061ED6
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.CDATA;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x00062ED9 File Offset: 0x00061ED9
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00062EE0 File Offset: 0x00061EE0
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
