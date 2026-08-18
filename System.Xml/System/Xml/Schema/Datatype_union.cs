using System;

namespace System.Xml.Schema
{
	// Token: 0x020001B1 RID: 433
	internal class Datatype_union : Datatype_anySimpleType
	{
		// Token: 0x06001636 RID: 5686 RVA: 0x00062B9D File Offset: 0x00061B9D
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlUnionConverter.Create(schemaType);
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x00062BA5 File Offset: 0x00061BA5
		internal Datatype_union(XmlSchemaSimpleType[] types)
		{
			this.types = types;
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00062BB4 File Offset: 0x00061BB4
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

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00062C06 File Offset: 0x00061C06
		public override Type ValueType
		{
			get
			{
				return Datatype_union.atomicValueType;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x00062C0D File Offset: 0x00061C0D
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x00062C11 File Offset: 0x00061C11
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.unionFacetsChecker;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x00062C18 File Offset: 0x00061C18
		internal override Type ListValueType
		{
			get
			{
				return Datatype_union.listValueType;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x00062C1F File Offset: 0x00061C1F
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Pattern | RestrictionFlags.Enumeration;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x00062C23 File Offset: 0x00061C23
		internal XmlSchemaSimpleType[] BaseMemberTypes
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00062C2C File Offset: 0x00061C2C
		internal bool HasAtomicMembers()
		{
			foreach (XmlSchemaSimpleType xmlSchemaSimpleType in this.types)
			{
				if (xmlSchemaSimpleType.Datatype.Variety == XmlSchemaDatatypeVariety.List)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00062C68 File Offset: 0x00061C68
		internal bool IsUnionBaseOf(DatatypeImplementation derivedType)
		{
			foreach (XmlSchemaSimpleType xmlSchemaSimpleType in this.types)
			{
				if (derivedType.IsDerivedFrom(xmlSchemaSimpleType.Datatype))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00062CA4 File Offset: 0x00061CA4
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = null;
			typedValue = null;
			Exception ex = DatatypeImplementation.unionFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				foreach (XmlSchemaSimpleType xmlSchemaSimpleType2 in this.types)
				{
					if (xmlSchemaSimpleType2.Datatype.TryParseValue(s, nameTable, nsmgr, out typedValue) == null)
					{
						xmlSchemaSimpleType = xmlSchemaSimpleType2;
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

		// Token: 0x06001642 RID: 5698 RVA: 0x00062D30 File Offset: 0x00061D30
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
			foreach (XmlSchemaSimpleType xmlSchemaSimpleType in this.types)
			{
				if (xmlSchemaSimpleType.Datatype.TryParseValue(value, nameTable, nsmgr, out obj) == null)
				{
					st = xmlSchemaSimpleType;
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

		// Token: 0x04000D77 RID: 3447
		private static readonly Type atomicValueType = typeof(object);

		// Token: 0x04000D78 RID: 3448
		private static readonly Type listValueType = typeof(object[]);

		// Token: 0x04000D79 RID: 3449
		private XmlSchemaSimpleType[] types;
	}
}
