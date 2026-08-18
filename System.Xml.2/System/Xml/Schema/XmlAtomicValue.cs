using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x02000269 RID: 617
	public sealed class XmlAtomicValue : XPathItem, ICloneable
	{
		// Token: 0x0600250A RID: 9482 RVA: 0x000CBA11 File Offset: 0x000C9C11
		internal XmlAtomicValue(XmlSchemaType xmlType, bool value)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.clrType = TypeCode.Boolean;
			this.unionVal.boolVal = value;
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x000CBA41 File Offset: 0x000C9C41
		internal XmlAtomicValue(XmlSchemaType xmlType, DateTime value)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.clrType = TypeCode.DateTime;
			this.unionVal.dtVal = value;
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000CBA72 File Offset: 0x000C9C72
		internal XmlAtomicValue(XmlSchemaType xmlType, double value)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.clrType = TypeCode.Double;
			this.unionVal.dblVal = value;
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000CBAA3 File Offset: 0x000C9CA3
		internal XmlAtomicValue(XmlSchemaType xmlType, int value)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.clrType = TypeCode.Int32;
			this.unionVal.i32Val = value;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000CBAD4 File Offset: 0x000C9CD4
		internal XmlAtomicValue(XmlSchemaType xmlType, long value)
		{
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.clrType = TypeCode.Int64;
			this.unionVal.i64Val = value;
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000CBB05 File Offset: 0x000C9D05
		internal XmlAtomicValue(XmlSchemaType xmlType, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.objVal = value;
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000CBB38 File Offset: 0x000C9D38
		internal XmlAtomicValue(XmlSchemaType xmlType, string value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.objVal = value;
			if (nsResolver != null && (this.xmlType.TypeCode == XmlTypeCode.QName || this.xmlType.TypeCode == XmlTypeCode.Notation))
			{
				string prefixFromQName = this.GetPrefixFromQName(value);
				this.nsPrefix = new XmlAtomicValue.NamespacePrefixForQName(prefixFromQName, nsResolver.LookupNamespace(prefixFromQName));
			}
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x000CBBB1 File Offset: 0x000C9DB1
		internal XmlAtomicValue(XmlSchemaType xmlType, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.objVal = value;
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000CBBE4 File Offset: 0x000C9DE4
		internal XmlAtomicValue(XmlSchemaType xmlType, object value, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (xmlType == null)
			{
				throw new ArgumentNullException("xmlType");
			}
			this.xmlType = xmlType;
			this.objVal = value;
			if (nsResolver != null && (this.xmlType.TypeCode == XmlTypeCode.QName || this.xmlType.TypeCode == XmlTypeCode.Notation))
			{
				XmlQualifiedName xmlQualifiedName = this.objVal as XmlQualifiedName;
				string @namespace = xmlQualifiedName.Namespace;
				this.nsPrefix = new XmlAtomicValue.NamespacePrefixForQName(nsResolver.LookupPrefix(@namespace), @namespace);
			}
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000CBC68 File Offset: 0x000C9E68
		public XmlAtomicValue Clone()
		{
			return this;
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x000CBC6B File Offset: 0x000C9E6B
		object ICloneable.Clone()
		{
			return this;
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002515 RID: 9493 RVA: 0x000CBC6E File Offset: 0x000C9E6E
		public override bool IsNode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002516 RID: 9494 RVA: 0x000CBC71 File Offset: 0x000C9E71
		public override XmlSchemaType XmlType
		{
			get
			{
				return this.xmlType;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x000CBC79 File Offset: 0x000C9E79
		public override Type ValueType
		{
			get
			{
				return this.xmlType.Datatype.ValueType;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002518 RID: 9496 RVA: 0x000CBC8C File Offset: 0x000C9E8C
		public override object TypedValue
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ChangeType(this.unionVal.boolVal, this.ValueType);
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ChangeType(this.unionVal.i32Val, this.ValueType);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ChangeType(this.unionVal.i64Val, this.ValueType);
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ChangeType(this.unionVal.dblVal, this.ValueType);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ChangeType(this.unionVal.dtVal, this.ValueType);
						}
					}
				}
				return valueConverter.ChangeType(this.objVal, this.ValueType, this.nsPrefix);
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x000CBD6C File Offset: 0x000C9F6C
		public override bool ValueAsBoolean
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return this.unionVal.boolVal;
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ToBoolean(this.unionVal.i32Val);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ToBoolean(this.unionVal.i64Val);
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ToBoolean(this.unionVal.dblVal);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ToBoolean(this.unionVal.dtVal);
						}
					}
				}
				return valueConverter.ToBoolean(this.objVal);
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x000CBE18 File Offset: 0x000CA018
		public override DateTime ValueAsDateTime
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ToDateTime(this.unionVal.boolVal);
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ToDateTime(this.unionVal.i32Val);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ToDateTime(this.unionVal.i64Val);
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ToDateTime(this.unionVal.dblVal);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return this.unionVal.dtVal;
						}
					}
				}
				return valueConverter.ToDateTime(this.objVal);
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x000CBEC4 File Offset: 0x000CA0C4
		public override double ValueAsDouble
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ToDouble(this.unionVal.boolVal);
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ToDouble(this.unionVal.i32Val);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ToDouble(this.unionVal.i64Val);
						}
						if (typeCode == TypeCode.Double)
						{
							return this.unionVal.dblVal;
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ToDouble(this.unionVal.dtVal);
						}
					}
				}
				return valueConverter.ToDouble(this.objVal);
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x000CBF70 File Offset: 0x000CA170
		public override int ValueAsInt
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ToInt32(this.unionVal.boolVal);
						}
						if (typeCode == TypeCode.Int32)
						{
							return this.unionVal.i32Val;
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ToInt32(this.unionVal.i64Val);
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ToInt32(this.unionVal.dblVal);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ToInt32(this.unionVal.dtVal);
						}
					}
				}
				return valueConverter.ToInt32(this.objVal);
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600251D RID: 9501 RVA: 0x000CC01C File Offset: 0x000CA21C
		public override long ValueAsLong
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ToInt64(this.unionVal.boolVal);
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ToInt64(this.unionVal.i32Val);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return this.unionVal.i64Val;
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ToInt64(this.unionVal.dblVal);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ToInt64(this.unionVal.dtVal);
						}
					}
				}
				return valueConverter.ToInt64(this.objVal);
			}
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x000CC0C8 File Offset: 0x000CA2C8
		public override object ValueAs(Type type, IXmlNamespaceResolver nsResolver)
		{
			XmlValueConverter valueConverter = this.xmlType.ValueConverter;
			if (type == typeof(XPathItem) || type == typeof(XmlAtomicValue))
			{
				return this;
			}
			if (this.objVal == null)
			{
				TypeCode typeCode = this.clrType;
				if (typeCode <= TypeCode.Int32)
				{
					if (typeCode == TypeCode.Boolean)
					{
						return valueConverter.ChangeType(this.unionVal.boolVal, type);
					}
					if (typeCode == TypeCode.Int32)
					{
						return valueConverter.ChangeType(this.unionVal.i32Val, type);
					}
				}
				else
				{
					if (typeCode == TypeCode.Int64)
					{
						return valueConverter.ChangeType(this.unionVal.i64Val, type);
					}
					if (typeCode == TypeCode.Double)
					{
						return valueConverter.ChangeType(this.unionVal.dblVal, type);
					}
					if (typeCode == TypeCode.DateTime)
					{
						return valueConverter.ChangeType(this.unionVal.dtVal, type);
					}
				}
			}
			return valueConverter.ChangeType(this.objVal, type, nsResolver);
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x0600251F RID: 9503 RVA: 0x000CC1A8 File Offset: 0x000CA3A8
		public override string Value
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode <= TypeCode.Int32)
					{
						if (typeCode == TypeCode.Boolean)
						{
							return valueConverter.ToString(this.unionVal.boolVal);
						}
						if (typeCode == TypeCode.Int32)
						{
							return valueConverter.ToString(this.unionVal.i32Val);
						}
					}
					else
					{
						if (typeCode == TypeCode.Int64)
						{
							return valueConverter.ToString(this.unionVal.i64Val);
						}
						if (typeCode == TypeCode.Double)
						{
							return valueConverter.ToString(this.unionVal.dblVal);
						}
						if (typeCode == TypeCode.DateTime)
						{
							return valueConverter.ToString(this.unionVal.dtVal);
						}
					}
				}
				return valueConverter.ToString(this.objVal, this.nsPrefix);
			}
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x000CC260 File Offset: 0x000CA460
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000CC268 File Offset: 0x000CA468
		private string GetPrefixFromQName(string value)
		{
			int num2;
			int num = ValidateNames.ParseQName(value, 0, out num2);
			if (num == 0 || num != value.Length)
			{
				return null;
			}
			if (num2 != 0)
			{
				return value.Substring(0, num2);
			}
			return string.Empty;
		}

		// Token: 0x0400103F RID: 4159
		private XmlSchemaType xmlType;

		// Token: 0x04001040 RID: 4160
		private object objVal;

		// Token: 0x04001041 RID: 4161
		private TypeCode clrType;

		// Token: 0x04001042 RID: 4162
		private XmlAtomicValue.Union unionVal;

		// Token: 0x04001043 RID: 4163
		private XmlAtomicValue.NamespacePrefixForQName nsPrefix;

		// Token: 0x020004A3 RID: 1187
		[StructLayout(LayoutKind.Explicit, Size = 8)]
		private struct Union
		{
			// Token: 0x04001EFD RID: 7933
			[FieldOffset(0)]
			public bool boolVal;

			// Token: 0x04001EFE RID: 7934
			[FieldOffset(0)]
			public double dblVal;

			// Token: 0x04001EFF RID: 7935
			[FieldOffset(0)]
			public long i64Val;

			// Token: 0x04001F00 RID: 7936
			[FieldOffset(0)]
			public int i32Val;

			// Token: 0x04001F01 RID: 7937
			[FieldOffset(0)]
			public DateTime dtVal;
		}

		// Token: 0x020004A4 RID: 1188
		private class NamespacePrefixForQName : IXmlNamespaceResolver
		{
			// Token: 0x06003169 RID: 12649 RVA: 0x0011FE67 File Offset: 0x0011E067
			public NamespacePrefixForQName(string prefix, string ns)
			{
				this.ns = ns;
				this.prefix = prefix;
			}

			// Token: 0x0600316A RID: 12650 RVA: 0x0011FE7D File Offset: 0x0011E07D
			public string LookupNamespace(string prefix)
			{
				if (prefix == this.prefix)
				{
					return this.ns;
				}
				return null;
			}

			// Token: 0x0600316B RID: 12651 RVA: 0x0011FE95 File Offset: 0x0011E095
			public string LookupPrefix(string namespaceName)
			{
				if (this.ns == namespaceName)
				{
					return this.prefix;
				}
				return null;
			}

			// Token: 0x0600316C RID: 12652 RVA: 0x0011FEB0 File Offset: 0x0011E0B0
			public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(1);
				dictionary[this.prefix] = this.ns;
				return dictionary;
			}

			// Token: 0x04001F02 RID: 7938
			public string prefix;

			// Token: 0x04001F03 RID: 7939
			public string ns;
		}
	}
}
