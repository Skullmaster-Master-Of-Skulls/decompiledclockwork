using System;

namespace System.Xml.Schema
{
	// Token: 0x02000206 RID: 518
	internal class Datatype_union : Datatype_anySimpleType
	{
		// Token: 0x06002154 RID: 8532 RVA: 0x000B64AD File Offset: 0x000B46AD
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlUnionConverter.Create(schemaType);
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x000B64B5 File Offset: 0x000B46B5
		internal Datatype_union(XmlSchemaSimpleType[] types)
		{
			this.types = types;
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x000B64C4 File Offset: 0x000B46C4
		internal override int Compare(object value1, object value2)
		{
			XsdSimpleValue xsdSimpleValue = value1 as XsdSimpleValue;
			XsdSimpleValue xsdSimpleValue2 = value2 as XsdSimpleValue;
			if (xsdSimpleValue == null || xsdSimpleValue2 == null)
			{
				return -1;
			}
			XmlSchemaType xmlType = xsdSimpleValue.XmlType;
			XmlSchemaType xmlType2 = xsdSimpleValue2.XmlType;
			if (xmlType == xmlType2)
			{
				XmlSchemaDatatype datatype = xmlType.Datatype;
				return datatype.Compare(xsdSimpleValue.TypedValue, xsdSimpleValue2.TypedValue);
			}
			return -1;
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06002157 RID: 8535 RVA: 0x000B6516 File Offset: 0x000B4716
		public override Type ValueType
		{
			get
			{
				return Datatype_union.atomicValueType;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x000B651D File Offset: 0x000B471D
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x000B6521 File Offset: 0x000B4721
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.unionFacetsChecker;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x000B6528 File Offset: 0x000B4728
		internal override Type ListValueType
		{
			get
			{
				return Datatype_union.listValueType;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600215B RID: 8539 RVA: 0x000B652F File Offset: 0x000B472F
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x0600215C RID: 8540 RVA: 0x000B6533 File Offset: 0x000B4733
		internal XmlSchemaSimpleType[] BaseMemberTypes
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x000B653C File Offset: 0x000B473C
		internal bool HasAtomicMembers()
		{
			for (int i = 0; i < this.types.Length; i++)
			{
				if (this.types[i].Datatype.Variety == XmlSchemaDatatypeVariety.List)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x000B6574 File Offset: 0x000B4774
		internal bool IsUnionBaseOf(DatatypeImplementation derivedType)
		{
			for (int i = 0; i < this.types.Length; i++)
			{
				if (derivedType.IsDerivedFrom(this.types[i].Datatype))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000B65AC File Offset: 0x000B47AC
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = null;
			typedValue = null;
			Exception ex = DatatypeImplementation.unionFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				for (int i = 0; i < this.types.Length; i++)
				{
					if (this.types[i].Datatype.TryParseValue(s, nameTable, nsmgr, out typedValue) == null)
					{
						xmlSchemaSimpleType = this.types[i];
						break;
					}
				}
				if (xmlSchemaSimpleType == null)
				{
					ex = new XmlSchemaException("Sch_UnionFailedEx", s);
				}
				else
				{
					typedValue = new XsdSimpleValue(xmlSchemaSimpleType, typedValue);
					ex = DatatypeImplementation.unionFacetsChecker.CheckValueFacets(typedValue, this);
					if (ex == null)
					{
						return null;
					}
				}
			}
			return ex;
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x000B663C File Offset: 0x000B483C
		internal override Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			typedValue = null;
			string text = value as string;
			if (text != null)
			{
				return this.TryParseValue(text, nameTable, nsmgr, out typedValue);
			}
			object obj = null;
			XmlSchemaSimpleType st = null;
			for (int i = 0; i < this.types.Length; i++)
			{
				if (this.types[i].Datatype.TryParseValue(value, nameTable, nsmgr, out obj) == null)
				{
					st = this.types[i];
					break;
				}
			}
			Exception ex;
			if (obj != null)
			{
				try
				{
					if (this.HasLexicalFacets)
					{
						string text2 = (string)this.ValueConverter.ChangeType(obj, typeof(string), nsmgr);
						ex = DatatypeImplementation.unionFacetsChecker.CheckLexicalFacets(ref text2, this);
						if (ex != null)
						{
							return ex;
						}
					}
					typedValue = new XsdSimpleValue(st, obj);
					if (this.HasValueFacets)
					{
						ex = DatatypeImplementation.unionFacetsChecker.CheckValueFacets(typedValue, this);
						if (ex != null)
						{
							return ex;
						}
					}
					return null;
				}
				catch (FormatException ex2)
				{
					ex = ex2;
				}
				catch (InvalidCastException ex3)
				{
					ex = ex3;
				}
				catch (OverflowException ex4)
				{
					ex = ex4;
				}
				catch (ArgumentException ex5)
				{
					ex = ex5;
				}
				return ex;
			}
			ex = new XmlSchemaException("Sch_UnionFailedEx", value.ToString());
			return ex;
		}

		// Token: 0x04000E67 RID: 3687
		private static readonly Type atomicValueType = typeof(object);

		// Token: 0x04000E68 RID: 3688
		private static readonly Type listValueType = typeof(object[]);

		// Token: 0x04000E69 RID: 3689
		private XmlSchemaSimpleType[] types;
	}
}
