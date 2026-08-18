using System;

namespace System.Xml.Schema
{
	// Token: 0x020001CA RID: 458
	internal class Datatype_base64Binary : Datatype_anySimpleType
	{
		// Token: 0x060016BB RID: 5819 RVA: 0x000636BC File Offset: 0x000626BC
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlMiscConverter.Create(schemaType);
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x000636C4 File Offset: 0x000626C4
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.binaryFacetsChecker;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x000636CB File Offset: 0x000626CB
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Base64Binary;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x000636CF File Offset: 0x000626CF
		public override Type ValueType
		{
			get
			{
				return Datatype_base64Binary.atomicValueType;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x000636D6 File Offset: 0x000626D6
		internal override Type ListValueType
		{
			get
			{
				return Datatype_base64Binary.listValueType;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x000636DD File Offset: 0x000626DD
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x000636E0 File Offset: 0x000626E0
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x000636E4 File Offset: 0x000626E4
		internal override int Compare(object value1, object value2)
		{
			return base.Compare((byte[])value1, (byte[])value2);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x000636F8 File Offset: 0x000626F8
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

		// Token: 0x04000D8A RID: 3466
		private static readonly Type atomicValueType = typeof(byte[]);

		// Token: 0x04000D8B RID: 3467
		private static readonly Type listValueType = typeof(byte[][]);
	}
}
