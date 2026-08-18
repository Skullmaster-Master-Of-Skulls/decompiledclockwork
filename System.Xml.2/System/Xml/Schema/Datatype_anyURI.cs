using System;

namespace System.Xml.Schema
{
	// Token: 0x02000221 RID: 545
	internal class Datatype_anyURI : Datatype_anySimpleType
	{
		// Token: 0x060021F0 RID: 8688 RVA: 0x000B7104 File Offset: 0x000B5304
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x000B710C File Offset: 0x000B530C
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.stringFacetsChecker;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x000B7113 File Offset: 0x000B5313
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyUri;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x000B7117 File Offset: 0x000B5317
		public override Type ValueType
		{
			get
			{
				return Datatype_anyURI.atomicValueType;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x000B711E File Offset: 0x000B531E
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x000B7121 File Offset: 0x000B5321
		internal override Type ListValueType
		{
			get
			{
				return Datatype_anyURI.listValueType;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x000B7128 File Offset: 0x000B5328
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x000B712B File Offset: 0x000B532B
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x000B712F File Offset: 0x000B532F
		internal override int Compare(object value1, object value2)
		{
			if (!((Uri)value1).Equals((Uri)value2))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000B7148 File Offset: 0x000B5348
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.stringFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				Uri uri;
				ex = XmlConvert.TryToUri(s, out uri);
				if (ex == null)
				{
					string originalString = uri.OriginalString;
					ex = ((StringFacetsChecker)DatatypeImplementation.stringFacetsChecker).CheckValueFacets(originalString, this, false);
					if (ex == null)
					{
						typedValue = uri;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000E7E RID: 3710
		private static readonly Type atomicValueType = typeof(Uri);

		// Token: 0x04000E7F RID: 3711
		private static readonly Type listValueType = typeof(Uri[]);
	}
}
