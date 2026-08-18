using System;

namespace System.Xml.Schema
{
	// Token: 0x020001C9 RID: 457
	internal class Datatype_hexBinary : Datatype_anySimpleType
	{
		// Token: 0x060016B0 RID: 5808 RVA: 0x000635EB File Offset: 0x000625EB
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x000635F3 File Offset: 0x000625F3
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.binaryFacetsChecker;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x000635FA File Offset: 0x000625FA
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.HexBinary;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x000635FE File Offset: 0x000625FE
		public override Type ValueType
		{
			get
			{
				return Datatype_hexBinary.atomicValueType;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x00063605 File Offset: 0x00062605
		internal override Type ListValueType
		{
			get
			{
				return Datatype_hexBinary.listValueType;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x0006360C File Offset: 0x0006260C
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x0006360F File Offset: 0x0006260F
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00063613 File Offset: 0x00062613
		internal override int Compare(object value1, object value2)
		{
			return base.Compare((byte[])value1, (byte[])value2);
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00063628 File Offset: 0x00062628
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.binaryFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				byte[] array = null;
				try
				{
					array = XmlConvert.FromBinHexString(s, false);
				}
				catch (ArgumentException result)
				{
					return result;
				}
				catch (XmlException result2)
				{
					return result2;
				}
				ex = DatatypeImplementation.binaryFacetsChecker.CheckValueFacets(array, this);
				if (ex == null)
				{
					typedValue = array;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x04000D88 RID: 3464
		private static readonly Type atomicValueType = typeof(byte[]);

		// Token: 0x04000D89 RID: 3465
		private static readonly Type listValueType = typeof(byte[][]);
	}
}
