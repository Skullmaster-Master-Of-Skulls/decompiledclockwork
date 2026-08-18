using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000205 RID: 517
	internal class Datatype_List : Datatype_anySimpleType
	{
		// Token: 0x06002147 RID: 8519 RVA: 0x000B5FE8 File Offset: 0x000B41E8
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			XmlSchemaType xmlSchemaType = null;
			XmlSchemaComplexType xmlSchemaComplexType = schemaType as XmlSchemaComplexType;
			XmlSchemaSimpleType xmlSchemaSimpleType;
			if (xmlSchemaComplexType != null)
			{
				do
				{
					xmlSchemaSimpleType = (xmlSchemaComplexType.BaseXmlSchemaType as XmlSchemaSimpleType);
					if (xmlSchemaSimpleType != null)
					{
						break;
					}
					xmlSchemaComplexType = (xmlSchemaComplexType.BaseXmlSchemaType as XmlSchemaComplexType);
					if (xmlSchemaComplexType == null)
					{
						break;
					}
				}
				while (xmlSchemaComplexType != XmlSchemaComplexType.AnyType);
			}
			else
			{
				xmlSchemaSimpleType = (schemaType as XmlSchemaSimpleType);
			}
			if (xmlSchemaSimpleType != null)
			{
				XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList;
				for (;;)
				{
					xmlSchemaSimpleTypeList = (xmlSchemaSimpleType.Content as XmlSchemaSimpleTypeList);
					if (xmlSchemaSimpleTypeList != null)
					{
						break;
					}
					xmlSchemaSimpleType = (xmlSchemaSimpleType.BaseXmlSchemaType as XmlSchemaSimpleType);
					if (xmlSchemaSimpleType == null || xmlSchemaSimpleType == DatatypeImplementation.AnySimpleType)
					{
						goto IL_6D;
					}
				}
				xmlSchemaType = xmlSchemaSimpleTypeList.BaseItemType;
			}
			IL_6D:
			if (xmlSchemaType == null)
			{
				xmlSchemaType = DatatypeImplementation.GetSimpleTypeFromTypeCode(schemaType.Datatype.TypeCode);
			}
			return XmlListConverter.Create(xmlSchemaType.ValueConverter);
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x000B6081 File Offset: 0x000B4281
		internal Datatype_List(DatatypeImplementation type) : this(type, 0)
		{
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x000B608B File Offset: 0x000B428B
		internal Datatype_List(DatatypeImplementation type, int minListSize)
		{
			this.itemType = type;
			this.minListSize = minListSize;
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x000B60A4 File Offset: 0x000B42A4
		internal override int Compare(object value1, object value2)
		{
			Array array = (Array)value1;
			Array array2 = (Array)value2;
			int length = array.Length;
			if (length != array2.Length)
			{
				return -1;
			}
			XmlAtomicValue[] array3 = array as XmlAtomicValue[];
			if (array3 != null)
			{
				XmlAtomicValue[] array4 = array2 as XmlAtomicValue[];
				for (int i = 0; i < array3.Length; i++)
				{
					XmlSchemaType xmlType = array3[i].XmlType;
					if (xmlType != array4[i].XmlType || !xmlType.Datatype.IsEqual(array3[i].TypedValue, array4[i].TypedValue))
					{
						return -1;
					}
				}
				return 0;
			}
			for (int j = 0; j < array.Length; j++)
			{
				if (this.itemType.Compare(array.GetValue(j), array2.GetValue(j)) != 0)
				{
					return -1;
				}
			}
			return 0;
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x0600214B RID: 8523 RVA: 0x000B6168 File Offset: 0x000B4368
		public override Type ValueType
		{
			get
			{
				return this.ListValueType;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x000B6170 File Offset: 0x000B4370
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return this.itemType.TokenizedType;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x000B617D File Offset: 0x000B437D
		internal override Type ListValueType
		{
			get
			{
				return this.itemType.ListValueType;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x000B618A File Offset: 0x000B438A
		internal override FacetsChecker FacetsChecker
		{
			get
			{
				return DatatypeImplementation.listFacetsChecker;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x000B6191 File Offset: 0x000B4391
		public override XmlTypeCode TypeCode
		{
			get
			{
				return this.itemType.TypeCode;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x000B619E File Offset: 0x000B439E
		internal override RestrictionFlags ValidRestrictionFlags
		{
			get
			{
				return RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength | RestrictionFlags.Pattern | RestrictionFlags.Enumeration | RestrictionFlags.WhiteSpace;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x000B61A2 File Offset: 0x000B43A2
		internal DatatypeImplementation ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000B61AC File Offset: 0x000B43AC
		internal override Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver namespaceResolver, out object typedValue)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string text = value as string;
			typedValue = null;
			if (text != null)
			{
				return this.TryParseValue(text, nameTable, namespaceResolver, out typedValue);
			}
			Exception ex;
			try
			{
				object obj = this.ValueConverter.ChangeType(value, this.ValueType, namespaceResolver);
				Array array = obj as Array;
				bool hasLexicalFacets = this.itemType.HasLexicalFacets;
				bool hasValueFacets = this.itemType.HasValueFacets;
				FacetsChecker facetsChecker = this.itemType.FacetsChecker;
				XmlValueConverter valueConverter = this.itemType.ValueConverter;
				for (int i = 0; i < array.Length; i++)
				{
					object value2 = array.GetValue(i);
					if (hasLexicalFacets)
					{
						string text2 = (string)valueConverter.ChangeType(value2, typeof(string), namespaceResolver);
						ex = facetsChecker.CheckLexicalFacets(ref text2, this.itemType);
						if (ex != null)
						{
							return ex;
						}
					}
					if (hasValueFacets)
					{
						ex = facetsChecker.CheckValueFacets(value2, this.itemType);
						if (ex != null)
						{
							return ex;
						}
					}
				}
				if (this.HasLexicalFacets)
				{
					string text3 = (string)this.ValueConverter.ChangeType(obj, typeof(string), namespaceResolver);
					ex = DatatypeImplementation.listFacetsChecker.CheckLexicalFacets(ref text3, this);
					if (ex != null)
					{
						return ex;
					}
				}
				if (this.HasValueFacets)
				{
					ex = DatatypeImplementation.listFacetsChecker.CheckValueFacets(obj, this);
					if (ex != null)
					{
						return ex;
					}
				}
				typedValue = obj;
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

		// Token: 0x06002153 RID: 8531 RVA: 0x000B6384 File Offset: 0x000B4584
		internal override Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue)
		{
			typedValue = null;
			Exception ex = DatatypeImplementation.listFacetsChecker.CheckLexicalFacets(ref s, this);
			if (ex == null)
			{
				ArrayList arrayList = new ArrayList();
				object obj2;
				if (this.itemType.Variety == XmlSchemaDatatypeVariety.Union)
				{
					string[] array = XmlConvert.SplitString(s);
					for (int i = 0; i < array.Length; i++)
					{
						object obj;
						ex = this.itemType.TryParseValue(array[i], nameTable, nsmgr, out obj);
						if (ex != null)
						{
							return ex;
						}
						XsdSimpleValue xsdSimpleValue = (XsdSimpleValue)obj;
						arrayList.Add(new XmlAtomicValue(xsdSimpleValue.XmlType, xsdSimpleValue.TypedValue, nsmgr));
					}
					obj2 = arrayList.ToArray(typeof(XmlAtomicValue));
				}
				else
				{
					string[] array2 = XmlConvert.SplitString(s);
					for (int j = 0; j < array2.Length; j++)
					{
						ex = this.itemType.TryParseValue(array2[j], nameTable, nsmgr, out typedValue);
						if (ex != null)
						{
							return ex;
						}
						arrayList.Add(typedValue);
					}
					obj2 = arrayList.ToArray(this.itemType.ValueType);
				}
				if (arrayList.Count < this.minListSize)
				{
					return new XmlSchemaException("Sch_EmptyAttributeValue", string.Empty);
				}
				ex = DatatypeImplementation.listFacetsChecker.CheckValueFacets(obj2, this);
				if (ex == null)
				{
					typedValue = obj2;
					return null;
				}
			}
			return ex;
		}

		// Token: 0x04000E65 RID: 3685
		private DatatypeImplementation itemType;

		// Token: 0x04000E66 RID: 3686
		private int minListSize;
	}
}
