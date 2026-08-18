using System;

namespace System.Xml.Schema
{
	// Token: 0x020001B6 RID: 438
	internal class Datatype_float : Datatype_anySimpleType
	{
		// Token: 0x0600165F RID: 5727 RVA: 0x00062FCA File Offset: 0x00061FCA
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlNumeric2Converter.Create(schemaType);
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001660 RID: 5728 RVA: 0x00062FD2 File Offset: 0x00061FD2
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.numeric2FacetsChecker;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x00062FD9 File Offset: 0x00061FD9
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Float;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001662 RID: 5730 RVA: 0x00062FDD File Offset: 0x00061FDD
		public override Type ValueType
		{
			get
			{
				return Datatype_float.atomicValueType;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x00062FE4 File Offset: 0x00061FE4
		internal override Type ListValueType
		{
			get
			{
				return Datatype_float.listValueType;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x00062FEB File Offset: 0x00061FEB
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x00062FEE File Offset: 0x00061FEE
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace | RestrictionFlags.MaxInclusive | RestrictionFlags.MaxExclusive | RestrictionFlags.MinInclusive | RestrictionFlags.MinExclusive;
			}
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00062FF8 File Offset: 0x00061FF8
		internal override int Compare(object value1, object value2)
		{
			return ((float)value1).CompareTo(value2);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x00063014 File Offset: 0x00062014
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.numeric2FacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				float num;
				ex = XmlConvert.TryToSingle(s, out num);
				if (ex == null)
				{
					ex = DatatypeImplementation.numeric2FacetsChecker.CheckValueFacets(num, this);
					if (ex == null)
					{
						typedValue = num;
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x04000D7C RID: 3452
		private static readonly Type atomicValueType = typeof(float);

		// Token: 0x04000D7D RID: 3453
		private static readonly Type listValueType = typeof(float[]);
	}
}
