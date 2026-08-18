using System;

namespace System.Xml.Schema
{
	// Token: 0x020001B5 RID: 437
	internal class Datatype_boolean : Datatype_anySimpleType
	{
		// Token: 0x06001654 RID: 5716 RVA: 0x00062F21 File Offset: 0x00061F21
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlBooleanConverter.Create(schemaType);
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x00062F29 File Offset: 0x00061F29
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.miscFacetsChecker;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x00062F30 File Offset: 0x00061F30
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Boolean;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x00062F34 File Offset: 0x00061F34
		public override Type ValueType
		{
			get
			{
				return Datatype_boolean.atomicValueType;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x00062F3B File Offset: 0x00061F3B
		internal override Type ListValueType
		{
			get
			{
				return Datatype_boolean.listValueType;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x00062F42 File Offset: 0x00061F42
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x00062F45 File Offset: 0x00061F45
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00062F4C File Offset: 0x00061F4C
		internal override int Compare(object value1, object value2)
		{
			return ((bool)value1).CompareTo(value2);
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00062F68 File Offset: 0x00061F68
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.miscFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				bool flag;
				ex = XmlConvert.TryToBoolean(s, out flag);
				if (ex == null)
				{
					typedValue = flag;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x04000D7A RID: 3450
		private static readonly Type atomicValueType = typeof(bool);

		// Token: 0x04000D7B RID: 3451
		private static readonly Type listValueType = typeof(bool[]);
	}
}
