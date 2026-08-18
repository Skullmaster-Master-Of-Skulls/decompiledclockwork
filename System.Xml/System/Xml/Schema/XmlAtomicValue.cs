using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x0200022A RID: 554
	public sealed class XmlAtomicValue : XPathItem, ICloneable
	{
		// Token: 0x06001A52 RID: 6738 RVA: 0x0007F445 File Offset: 0x0007E445
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

		// Token: 0x06001A53 RID: 6739 RVA: 0x0007F475 File Offset: 0x0007E475
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

		// Token: 0x06001A54 RID: 6740 RVA: 0x0007F4A6 File Offset: 0x0007E4A6
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

		// Token: 0x06001A55 RID: 6741 RVA: 0x0007F4D7 File Offset: 0x0007E4D7
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

		// Token: 0x06001A56 RID: 6742 RVA: 0x0007F508 File Offset: 0x0007E508
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

		// Token: 0x06001A57 RID: 6743 RVA: 0x0007F539 File Offset: 0x0007E539
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

		// Token: 0x06001A58 RID: 6744 RVA: 0x0007F56C File Offset: 0x0007E56C
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

		// Token: 0x06001A59 RID: 6745 RVA: 0x0007F5E5 File Offset: 0x0007E5E5
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

		// Token: 0x06001A5A RID: 6746 RVA: 0x0007F618 File Offset: 0x0007E618
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

		// Token: 0x06001A5B RID: 6747 RVA: 0x0007F69C File Offset: 0x0007E69C
		public XmlAtomicValue Clone()
		{
			return this;
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x0007F69F File Offset: 0x0007E69F
		object ICloneable.Clone()
		{
			return this;
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x0007F6A2 File Offset: 0x0007E6A2
		public override bool IsNode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x0007F6A5 File Offset: 0x0007E6A5
		public override XmlSchemaType XmlType
		{
			get
			{
				return this.xmlType;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001A5F RID: 6751 RVA: 0x0007F6AD File Offset: 0x0007E6AD
		public override Type ValueType
		{
			get
			{
				return this.xmlType.Datatype.ValueType;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x0007F6C0 File Offset: 0x0007E6C0
		public override object TypedValue
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode == TypeCode.Boolean)
					{
						return valueConverter.ChangeType(this.unionVal.boolVal, this.ValueType);
					}
					switch (typeCode)
					{
					case TypeCode.Int32:
						return valueConverter.ChangeType(this.unionVal.i32Val, this.ValueType);
					case TypeCode.UInt32:
						break;
					case TypeCode.Int64:
						return valueConverter.ChangeType(this.unionVal.i64Val, this.ValueType);
					default:
						switch (typeCode)
						{
						case TypeCode.Double:
							return valueConverter.ChangeType(this.unionVal.dblVal, this.ValueType);
						case TypeCode.DateTime:
							return valueConverter.ChangeType(this.unionVal.dtVal, this.ValueType);
						}
						break;
					}
				}
				return valueConverter.ChangeType(this.objVal, this.ValueType, this.nsPrefix);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x0007F7AC File Offset: 0x0007E7AC
		public override bool ValueAsBoolean
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode == TypeCode.Boolean)
					{
						return this.unionVal.boolVal;
					}
					switch (typeCode)
					{
					case TypeCode.Int32:
						return valueConverter.ToBoolean(this.unionVal.i32Val);
					case TypeCode.UInt32:
						break;
					case TypeCode.Int64:
						return valueConverter.ToBoolean(this.unionVal.i64Val);
					default:
						switch (typeCode)
						{
						case TypeCode.Double:
							return valueConverter.ToBoolean(this.unionVal.dblVal);
						case TypeCode.DateTime:
							return valueConverter.ToBoolean(this.unionVal.dtVal);
						}
						break;
					}
				}
				return valueConverter.ToBoolean(this.objVal);
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x0007F868 File Offset: 0x0007E868
		public override DateTime ValueAsDateTime
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode == TypeCode.Boolean)
					{
						return valueConverter.ToDateTime(this.unionVal.boolVal);
					}
					switch (typeCode)
					{
					case TypeCode.Int32:
						return valueConverter.ToDateTime(this.unionVal.i32Val);
					case TypeCode.UInt32:
						break;
					case TypeCode.Int64:
						return valueConverter.ToDateTime(this.unionVal.i64Val);
					default:
						switch (typeCode)
						{
						case TypeCode.Double:
							return valueConverter.ToDateTime(this.unionVal.dblVal);
						case TypeCode.DateTime:
							return this.unionVal.dtVal;
						}
						break;
					}
				}
				return valueConverter.ToDateTime(this.objVal);
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x0007F924 File Offset: 0x0007E924
		public override double ValueAsDouble
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode == TypeCode.Boolean)
					{
						return valueConverter.ToDouble(this.unionVal.boolVal);
					}
					switch (typeCode)
					{
					case TypeCode.Int32:
						return valueConverter.ToDouble(this.unionVal.i32Val);
					case TypeCode.UInt32:
						break;
					case TypeCode.Int64:
						return valueConverter.ToDouble(this.unionVal.i64Val);
					default:
						switch (typeCode)
						{
						case TypeCode.Double:
							return this.unionVal.dblVal;
						case TypeCode.DateTime:
							return valueConverter.ToDouble(this.unionVal.dtVal);
						}
						break;
					}
				}
				return valueConverter.ToDouble(this.objVal);
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x0007F9E0 File Offset: 0x0007E9E0
		public override int ValueAsInt
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode == TypeCode.Boolean)
					{
						return valueConverter.ToInt32(this.unionVal.boolVal);
					}
					switch (typeCode)
					{
					case TypeCode.Int32:
						return this.unionVal.i32Val;
					case TypeCode.UInt32:
						break;
					case TypeCode.Int64:
						return valueConverter.ToInt32(this.unionVal.i64Val);
					default:
						switch (typeCode)
						{
						case TypeCode.Double:
							return valueConverter.ToInt32(this.unionVal.dblVal);
						case TypeCode.DateTime:
							return valueConverter.ToInt32(this.unionVal.dtVal);
						}
						break;
					}
				}
				return valueConverter.ToInt32(this.objVal);
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x0007FA9C File Offset: 0x0007EA9C
		public override long ValueAsLong
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode == TypeCode.Boolean)
					{
						return valueConverter.ToInt64(this.unionVal.boolVal);
					}
					switch (typeCode)
					{
					case TypeCode.Int32:
						return valueConverter.ToInt64(this.unionVal.i32Val);
					case TypeCode.UInt32:
						break;
					case TypeCode.Int64:
						return this.unionVal.i64Val;
					default:
						switch (typeCode)
						{
						case TypeCode.Double:
							return valueConverter.ToInt64(this.unionVal.dblVal);
						case TypeCode.DateTime:
							return valueConverter.ToInt64(this.unionVal.dtVal);
						}
						break;
					}
				}
				return valueConverter.ToInt64(this.objVal);
			}
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x0007FB58 File Offset: 0x0007EB58
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
				if (typeCode == TypeCode.Boolean)
				{
					return valueConverter.ChangeType(this.unionVal.boolVal, type);
				}
				switch (typeCode)
				{
				case TypeCode.Int32:
					return valueConverter.ChangeType(this.unionVal.i32Val, type);
				case TypeCode.UInt32:
					break;
				case TypeCode.Int64:
					return valueConverter.ChangeType(this.unionVal.i64Val, type);
				default:
					switch (typeCode)
					{
					case TypeCode.Double:
						return valueConverter.ChangeType(this.unionVal.dblVal, type);
					case TypeCode.DateTime:
						return valueConverter.ChangeType(this.unionVal.dtVal, type);
					}
					break;
				}
			}
			return valueConverter.ChangeType(this.objVal, type, nsResolver);
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x0007FC3C File Offset: 0x0007EC3C
		public override string Value
		{
			get
			{
				XmlValueConverter valueConverter = this.xmlType.ValueConverter;
				if (this.objVal == null)
				{
					TypeCode typeCode = this.clrType;
					if (typeCode == TypeCode.Boolean)
					{
						return valueConverter.ToString(this.unionVal.boolVal);
					}
					switch (typeCode)
					{
					case TypeCode.Int32:
						return valueConverter.ToString(this.unionVal.i32Val);
					case TypeCode.UInt32:
						break;
					case TypeCode.Int64:
						return valueConverter.ToString(this.unionVal.i64Val);
					default:
						switch (typeCode)
						{
						case TypeCode.Double:
							return valueConverter.ToString(this.unionVal.dblVal);
						case TypeCode.DateTime:
							return valueConverter.ToString(this.unionVal.dtVal);
						}
						break;
					}
				}
				return valueConverter.ToString(this.objVal, this.nsPrefix);
			}
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0007FD03 File Offset: 0x0007ED03
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0007FD0C File Offset: 0x0007ED0C
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

		// Token: 0x040010A2 RID: 4258
		private XmlSchemaType xmlType;

		// Token: 0x040010A3 RID: 4259
		private object objVal;

		// Token: 0x040010A4 RID: 4260
		private TypeCode clrType;

		// Token: 0x040010A5 RID: 4261
		private XmlAtomicValue.Union unionVal;

		// Token: 0x040010A6 RID: 4262
		private XmlAtomicValue.NamespacePrefixForQName nsPrefix;

		// Token: 0x0200022B RID: 555
		[StructLayout(LayoutKind.Explicit, Size = 8)]
		private struct Union
		{
			// Token: 0x040010A7 RID: 4263
			[FieldOffset(0)]
			public bool boolVal;

			// Token: 0x040010A8 RID: 4264
			[FieldOffset(0)]
			public double dblVal;

			// Token: 0x040010A9 RID: 4265
			[FieldOffset(0)]
			public long i64Val;

			// Token: 0x040010AA RID: 4266
			[FieldOffset(0)]
			public int i32Val;

			// Token: 0x040010AB RID: 4267
			[FieldOffset(0)]
			public DateTime dtVal;
		}

		// Token: 0x0200022C RID: 556
		private class NamespacePrefixForQName : IXmlNamespaceResolver
		{
			// Token: 0x06001A6A RID: 6762 RVA: 0x0007FD42 File Offset: 0x0007ED42
			public NamespacePrefixForQName(string prefix, string ns)
			{
				this.ns = ns;
				this.prefix = prefix;
			}

			// Token: 0x06001A6B RID: 6763 RVA: 0x0007FD58 File Offset: 0x0007ED58
			public string LookupNamespace(string prefix)
			{
				if (prefix == this.prefix)
				{
					return this.ns;
				}
				return null;
			}

			// Token: 0x06001A6C RID: 6764 RVA: 0x0007FD70 File Offset: 0x0007ED70
			public string LookupPrefix(string namespaceName)
			{
				if (this.ns == namespaceName)
				{
					return this.prefix;
				}
				return null;
			}

			// Token: 0x06001A6D RID: 6765 RVA: 0x0007FD88 File Offset: 0x0007ED88
			public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(1);
				dictionary[this.prefix] = this.ns;
				return dictionary;
			}

			// Token: 0x040010AC RID: 4268
			public string prefix;

			// Token: 0x040010AD RID: 4269
			public string ns;
		}
	}
}
