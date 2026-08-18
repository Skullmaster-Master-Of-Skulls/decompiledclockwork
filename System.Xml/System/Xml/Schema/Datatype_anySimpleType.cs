using System;

namespace System.Xml.Schema
{
	// Token: 0x020001AF RID: 431
	internal class Datatype_anySimpleType : DatatypeImplementation
	{
		// Token: 0x0600161D RID: 5661 RVA: 0x00062657 File Offset: 0x00061657
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlUntypedConverter.Untyped;
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x0006265E File Offset: 0x0006165E
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.miscFacetsChecker;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x00062665 File Offset: 0x00061665
		public override Type ValueType
		{
			get
			{
				return Datatype_anySimpleType.atomicValueType;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x0006266C File Offset: 0x0006166C
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x00062670 File Offset: 0x00061670
		internal override Type ListValueType
		{
			get
			{
				return Datatype_anySimpleType.listValueType;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x00062677 File Offset: 0x00061677
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.None;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x0006267B File Offset: 0x0006167B
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return (RestrictionFlags)0;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x0006267E File Offset: 0x0006167E
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00062681 File Offset: 0x00061681
		internal override int Compare(object value1, object value2)
		{
			return string.Compare(value1.ToString(), value2.ToString(), StringComparison.Ordinal);
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00062695 File Offset: 0x00061695
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = XmlComplianceUtil.NonCDataNormalize(s);
			return null;
		}

		// Token: 0x04000D73 RID: 3443
		private static readonly Type atomicValueType = typeof(string);

		// Token: 0x04000D74 RID: 3444
		private static readonly Type listValueType = typeof(string[]);
	}
}
