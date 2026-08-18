using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000280 RID: 640
	public abstract class XmlSchemaDatatype
	{
		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002647 RID: 9799
		public abstract Type ValueType { get; }

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002648 RID: 9800
		public abstract XmlTokenizedType TokenizedType { get; }

		// Token: 0x06002649 RID: 9801
		public abstract object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr);

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x0600264A RID: 9802 RVA: 0x000CE444 File Offset: 0x000CC644
		public virtual XmlSchemaDatatypeVariety Variety
		{
			get
			{
				return XmlSchemaDatatypeVariety.Atomic;
			}
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000CE447 File Offset: 0x000CC647
		public virtual object ChangeType(object value, Type targetType)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			return this.ValueConverter.ChangeType(value, targetType);
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x000CE478 File Offset: 0x000CC678
		public virtual object ChangeType(object value, Type targetType, IXmlNamespaceResolver namespaceResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (namespaceResolver == null)
			{
				throw new ArgumentNullException("namespaceResolver");
			}
			return this.ValueConverter.ChangeType(value, targetType, namespaceResolver);
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x000CE4B8 File Offset: 0x000CC6B8
		public virtual XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.None;
			}
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000CE4BB File Offset: 0x000CC6BB
		public virtual bool IsDerivedFrom(XmlSchemaDatatype datatype)
		{
			return false;
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x0600264F RID: 9807
		internal abstract bool HasLexicalFacets { get; }

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002650 RID: 9808
		internal abstract bool HasValueFacets { get; }

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002651 RID: 9809
		internal abstract XmlValueConverter ValueConverter { get; }

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002652 RID: 9810
		// (set) Token: 0x06002653 RID: 9811
		internal abstract RestrictionFacets Restriction { get; set; }

		// Token: 0x06002654 RID: 9812
		internal abstract int Compare(object value1, object value2);

		// Token: 0x06002655 RID: 9813
		internal abstract object ParseValue(string s, Type typDest, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr);

		// Token: 0x06002656 RID: 9814
		internal abstract object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, bool createAtomicValue);

		// Token: 0x06002657 RID: 9815
		internal abstract Exception TryParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr, out object typedValue);

		// Token: 0x06002658 RID: 9816
		internal abstract Exception TryParseValue(object value, XmlNameTable nameTable, IXmlNamespaceResolver namespaceResolver, out object typedValue);

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002659 RID: 9817
		internal abstract FacetsChecker FacetsChecker { get; }

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x0600265A RID: 9818
		internal abstract XmlSchemaWhiteSpace BuiltInWhitespaceFacet { get; }

		// Token: 0x0600265B RID: 9819
		internal abstract XmlSchemaDatatype DeriveByRestriction(XmlSchemaObjectCollection facets, XmlNameTable nameTable, XmlSchemaType schemaType);

		// Token: 0x0600265C RID: 9820
		internal abstract XmlSchemaDatatype DeriveByList(XmlSchemaType schemaType);

		// Token: 0x0600265D RID: 9821
		internal abstract void VerifySchemaValid(XmlSchemaObjectTable notations, XmlSchemaObject caller);

		// Token: 0x0600265E RID: 9822
		internal abstract bool IsEqual(object o1, object o2);

		// Token: 0x0600265F RID: 9823
		internal abstract bool IsComparable(XmlSchemaDatatype dtype);

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x000CE4C0 File Offset: 0x000CC6C0
		internal string TypeCodeString
		{
			get
			{
				string result = string.Empty;
				XmlTypeCode typeCode = this.TypeCode;
				switch (this.Variety)
				{
				case XmlSchemaDatatypeVariety.Atomic:
					if (typeCode == XmlTypeCode.AnyAtomicType)
					{
						result = "anySimpleType";
					}
					else
					{
						result = this.TypeCodeToString(typeCode);
					}
					break;
				case XmlSchemaDatatypeVariety.List:
					if (typeCode == XmlTypeCode.AnyAtomicType)
					{
						result = "List of Union";
					}
					else
					{
						result = "List of " + this.TypeCodeToString(typeCode);
					}
					break;
				case XmlSchemaDatatypeVariety.Union:
					result = "Union";
					break;
				}
				return result;
			}
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000CE534 File Offset: 0x000CC734
		internal string TypeCodeToString(XmlTypeCode typeCode)
		{
			switch (typeCode)
			{
			case XmlTypeCode.None:
				return "None";
			case XmlTypeCode.Item:
				return "AnyType";
			case XmlTypeCode.AnyAtomicType:
				return "AnyAtomicType";
			case XmlTypeCode.String:
				return "String";
			case XmlTypeCode.Boolean:
				return "Boolean";
			case XmlTypeCode.Decimal:
				return "Decimal";
			case XmlTypeCode.Float:
				return "Float";
			case XmlTypeCode.Double:
				return "Double";
			case XmlTypeCode.Duration:
				return "Duration";
			case XmlTypeCode.DateTime:
				return "DateTime";
			case XmlTypeCode.Time:
				return "Time";
			case XmlTypeCode.Date:
				return "Date";
			case XmlTypeCode.GYearMonth:
				return "GYearMonth";
			case XmlTypeCode.GYear:
				return "GYear";
			case XmlTypeCode.GMonthDay:
				return "GMonthDay";
			case XmlTypeCode.GDay:
				return "GDay";
			case XmlTypeCode.GMonth:
				return "GMonth";
			case XmlTypeCode.HexBinary:
				return "HexBinary";
			case XmlTypeCode.Base64Binary:
				return "Base64Binary";
			case XmlTypeCode.AnyUri:
				return "AnyUri";
			case XmlTypeCode.QName:
				return "QName";
			case XmlTypeCode.Notation:
				return "Notation";
			case XmlTypeCode.NormalizedString:
				return "NormalizedString";
			case XmlTypeCode.Token:
				return "Token";
			case XmlTypeCode.Language:
				return "Language";
			case XmlTypeCode.NmToken:
				return "NmToken";
			case XmlTypeCode.Name:
				return "Name";
			case XmlTypeCode.NCName:
				return "NCName";
			case XmlTypeCode.Id:
				return "Id";
			case XmlTypeCode.Idref:
				return "Idref";
			case XmlTypeCode.Entity:
				return "Entity";
			case XmlTypeCode.Integer:
				return "Integer";
			case XmlTypeCode.NonPositiveInteger:
				return "NonPositiveInteger";
			case XmlTypeCode.NegativeInteger:
				return "NegativeInteger";
			case XmlTypeCode.Long:
				return "Long";
			case XmlTypeCode.Int:
				return "Int";
			case XmlTypeCode.Short:
				return "Short";
			case XmlTypeCode.Byte:
				return "Byte";
			case XmlTypeCode.NonNegativeInteger:
				return "NonNegativeInteger";
			case XmlTypeCode.UnsignedLong:
				return "UnsignedLong";
			case XmlTypeCode.UnsignedInt:
				return "UnsignedInt";
			case XmlTypeCode.UnsignedShort:
				return "UnsignedShort";
			case XmlTypeCode.UnsignedByte:
				return "UnsignedByte";
			case XmlTypeCode.PositiveInteger:
				return "PositiveInteger";
			}
			return typeCode.ToString();
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000CE738 File Offset: 0x000CC938
		internal static string ConcatenatedToString(object value)
		{
			Type type = value.GetType();
			string result = string.Empty;
			if (type == typeof(IEnumerable) && type != typeof(string))
			{
				StringBuilder stringBuilder = new StringBuilder();
				IEnumerator enumerator = (value as IEnumerable).GetEnumerator();
				if (enumerator.MoveNext())
				{
					stringBuilder.Append("{");
					object obj = enumerator.Current;
					if (obj is IFormattable)
					{
						stringBuilder.Append(((IFormattable)obj).ToString("", CultureInfo.InvariantCulture));
					}
					else
					{
						stringBuilder.Append(obj.ToString());
					}
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(" , ");
						obj = enumerator.Current;
						if (obj is IFormattable)
						{
							stringBuilder.Append(((IFormattable)obj).ToString("", CultureInfo.InvariantCulture));
						}
						else
						{
							stringBuilder.Append(obj.ToString());
						}
					}
					stringBuilder.Append("}");
					result = stringBuilder.ToString();
				}
			}
			else if (value is IFormattable)
			{
				result = ((IFormattable)value).ToString("", CultureInfo.InvariantCulture);
			}
			else
			{
				result = value.ToString();
			}
			return result;
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x000CE874 File Offset: 0x000CCA74
		internal static XmlSchemaDatatype FromXmlTokenizedType(XmlTokenizedType token)
		{
			return DatatypeImplementation.FromXmlTokenizedType(token);
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000CE87C File Offset: 0x000CCA7C
		internal static XmlSchemaDatatype FromXmlTokenizedTypeXsd(XmlTokenizedType token)
		{
			return DatatypeImplementation.FromXmlTokenizedTypeXsd(token);
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x000CE884 File Offset: 0x000CCA84
		internal static XmlSchemaDatatype FromXdrName(string name)
		{
			return DatatypeImplementation.FromXdrName(name);
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x000CE88C File Offset: 0x000CCA8C
		internal static XmlSchemaDatatype DeriveByUnion(XmlSchemaSimpleType[] types, XmlSchemaType schemaType)
		{
			return DatatypeImplementation.DeriveByUnion(types, schemaType);
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000CE898 File Offset: 0x000CCA98
		internal static string XdrCanonizeUri(string uri, XmlNameTable nameTable, SchemaNames schemaNames)
		{
			int num = 5;
			bool flag = false;
			if (uri.Length > 5 && uri.StartsWith("uuid:", StringComparison.Ordinal))
			{
				flag = true;
			}
			else if (uri.Length > 9 && uri.StartsWith("urn:uuid:", StringComparison.Ordinal))
			{
				flag = true;
				num = 9;
			}
			string text;
			if (flag)
			{
				text = nameTable.Add(uri.Substring(0, num) + uri.Substring(num, uri.Length - num).ToUpper(CultureInfo.InvariantCulture));
			}
			else
			{
				text = uri;
			}
			if (Ref.Equal(schemaNames.NsDataTypeAlias, text) || Ref.Equal(schemaNames.NsDataTypeOld, text))
			{
				text = schemaNames.NsDataType;
			}
			else if (Ref.Equal(schemaNames.NsXdrAlias, text))
			{
				text = schemaNames.NsXdr;
			}
			return text;
		}
	}
}
