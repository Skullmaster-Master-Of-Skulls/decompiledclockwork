using System;

namespace System.Xml.Schema
{
	// Token: 0x0200021F RID: 543
	internal class Datatype_hexBinary : Datatype_anySimpleType
	{
		// Token: 0x060021DA RID: 8666 RVA: 0x000B6F63 File Offset: 0x000B5163
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x000B6F6B File Offset: 0x000B516B
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.binaryFacetsChecker;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x000B6F72 File Offset: 0x000B5172
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.HexBinary;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060021DD RID: 8669 RVA: 0x000B6F76 File Offset: 0x000B5176
		public override Type ValueType
		{
			get
			{
				return Datatype_hexBinary.atomicValueType;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060021DE RID: 8670 RVA: 0x000B6F7D File Offset: 0x000B517D
		internal override Type ListValueType
		{
			get
			{
				return Datatype_hexBinary.listValueType;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060021DF RID: 8671 RVA: 0x000B6F84 File Offset: 0x000B5184
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x060021E0 RID: 8672 RVA: 0x000B6F87 File Offset: 0x000B5187
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x000B6F8B File Offset: 0x000B518B
		internal override int Compare(object value1, object value2)
		{
			return base.Compare((byte[])value1, (byte[])value2);
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x000B6FA0 File Offset: 0x000B51A0
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

		// Token: 0x04000E7A RID: 3706
		private static readonly Type atomicValueType = typeof(byte[]);

		// Token: 0x04000E7B RID: 3707
		private static readonly Type listValueType = typeof(byte[][]);
	}
}
