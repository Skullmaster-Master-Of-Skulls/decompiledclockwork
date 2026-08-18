using System;

namespace System.Xml.Schema
{
	// Token: 0x02000207 RID: 519
	internal class Datatype_anySimpleType : DatatypeImplementation
	{
		// Token: 0x06002162 RID: 8546 RVA: 0x000B67A4 File Offset: 0x000B49A4
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlUntypedConverter.Untyped;
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06002163 RID: 8547 RVA: 0x000B67AB File Offset: 0x000B49AB
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.miscFacetsChecker;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x000B67B2 File Offset: 0x000B49B2
		public override Type ValueType
		{
			get
			{
				return Datatype_anySimpleType.atomicValueType;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06002165 RID: 8549 RVA: 0x000B67B9 File Offset: 0x000B49B9
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x000B67BD File Offset: 0x000B49BD
		internal override Type ListValueType
		{
			get
			{
				return Datatype_anySimpleType.listValueType;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002167 RID: 8551 RVA: 0x000B67C4 File Offset: 0x000B49C4
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.None;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x000B67C8 File Offset: 0x000B49C8
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return (RestrictionFlags)0;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002169 RID: 8553 RVA: 0x000B67CB File Offset: 0x000B49CB
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000B67CE File Offset: 0x000B49CE
		internal override int Compare(object value1, object value2)
		{
			return string.Compare(value1.ToString(), value2.ToString(), StringComparison.Ordinal);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000B67E2 File Offset: 0x000B49E2
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = XmlComplianceUtil.NonCDataNormalize(s);
			return null;
		}

		// Token: 0x04000E6A RID: 3690
		private static readonly Type atomicValueType = typeof(string);

		// Token: 0x04000E6B RID: 3691
		private static readonly Type listValueType = typeof(string[]);
	}
}
