using System;

namespace System.Xml.Schema
{
	// Token: 0x0200020B RID: 523
	internal class Datatype_boolean : Datatype_anySimpleType
	{
		// Token: 0x0600217E RID: 8574 RVA: 0x000B68A1 File Offset: 0x000B4AA1
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlBooleanConverter.Create(schemaType);
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x000B68A9 File Offset: 0x000B4AA9
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.miscFacetsChecker;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x000B68B0 File Offset: 0x000B4AB0
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Boolean;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x000B68B4 File Offset: 0x000B4AB4
		public override Type ValueType
		{
			get
			{
				return Datatype_boolean.atomicValueType;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x000B68BB File Offset: 0x000B4ABB
		internal override Type ListValueType
		{
			get
			{
				return Datatype_boolean.listValueType;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002183 RID: 8579 RVA: 0x000B68C2 File Offset: 0x000B4AC2
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x000B68C5 File Offset: 0x000B4AC5
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x000B68CC File Offset: 0x000B4ACC
		internal override int Compare(object value1, object value2)
		{
			return ((bool)value1).CompareTo(value2);
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x000B68E8 File Offset: 0x000B4AE8
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

		// Token: 0x04000E6C RID: 3692
		private static readonly Type atomicValueType = typeof(bool);

		// Token: 0x04000E6D RID: 3693
		private static readonly Type listValueType = typeof(bool[]);
	}
}
