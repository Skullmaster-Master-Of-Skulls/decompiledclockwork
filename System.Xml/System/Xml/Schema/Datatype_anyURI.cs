using System;

namespace System.Xml.Schema
{
	// Token: 0x020001CB RID: 459
	internal class Datatype_anyURI : Datatype_anySimpleType
	{
		// Token: 0x060016C6 RID: 5830 RVA: 0x0006378C File Offset: 0x0006278C
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x00063794 File Offset: 0x00062794
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.stringFacetsChecker;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x0006379B File Offset: 0x0006279B
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyUri;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x0006379F File Offset: 0x0006279F
		public override Type ValueType
		{
			get
			{
				return Datatype_anyURI.atomicValueType;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x000637A6 File Offset: 0x000627A6
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x000637A9 File Offset: 0x000627A9
		internal override Type ListValueType
		{
			get
			{
				return Datatype_anyURI.listValueType;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x000637B0 File Offset: 0x000627B0
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x000637B3 File Offset: 0x000627B3
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x000637B7 File Offset: 0x000627B7
		internal override int Compare(object value1, object value2)
		{
			if (!((Uri)value1).Equals((Uri)value2))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x000637D0 File Offset: 0x000627D0
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

		// Token: 0x04000D8C RID: 3468
		private static readonly Type atomicValueType = typeof(Uri);

		// Token: 0x04000D8D RID: 3469
		private static readonly Type listValueType = typeof(Uri[]);
	}
}
