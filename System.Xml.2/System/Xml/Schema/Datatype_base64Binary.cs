using System;

namespace System.Xml.Schema
{
	// Token: 0x02000220 RID: 544
	internal class Datatype_base64Binary : Datatype_anySimpleType
	{
		// Token: 0x060021E5 RID: 8677 RVA: 0x000B7034 File Offset: 0x000B5234
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x000B703C File Offset: 0x000B523C
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.binaryFacetsChecker;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x000B7043 File Offset: 0x000B5243
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Base64Binary;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x000B7047 File Offset: 0x000B5247
		public override Type ValueType
		{
			get
			{
				return Datatype_base64Binary.atomicValueType;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x000B704E File Offset: 0x000B524E
		internal override Type ListValueType
		{
			get
			{
				return Datatype_base64Binary.listValueType;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x000B7055 File Offset: 0x000B5255
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060021EB RID: 8683 RVA: 0x000B7058 File Offset: 0x000B5258
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x000B705C File Offset: 0x000B525C
		internal override int Compare(object value1, object value2)
		{
			return base.Compare((byte[])value1, (byte[])value2);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000B7070 File Offset: 0x000B5270
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.binaryFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				byte[] array = null;
				try
				{
					array = Convert.FromBase64String(s);
				}
				catch (ArgumentException result)
				{
					return result;
				}
				catch (FormatException result2)
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

		// Token: 0x04000E7C RID: 3708
		private static readonly Type atomicValueType = typeof(byte[]);

		// Token: 0x04000E7D RID: 3709
		private static readonly Type listValueType = typeof(byte[][]);
	}
}
