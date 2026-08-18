using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x02000293 RID: 659
	internal class XmlListConverter : XmlBaseConverter
	{
		// Token: 0x06001F5A RID: 8026 RVA: 0x0008DAE4 File Offset: 0x0008CAE4
		protected XmlListConverter(XmlBaseConverter atomicConverter) : base(atomicConverter)
		{
			this.atomicConverter = atomicConverter;
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x0008DAF4 File Offset: 0x0008CAF4
		protected XmlListConverter(XmlBaseConverter atomicConverter, Type clrTypeDefault) : base(atomicConverter, clrTypeDefault)
		{
			this.atomicConverter = atomicConverter;
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x0008DB05 File Offset: 0x0008CB05
		protected XmlListConverter(XmlSchemaType schemaType) : base(schemaType)
		{
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x0008DB0E File Offset: 0x0008CB0E
		public static XmlValueConverter Create(XmlValueConverter atomicConverter)
		{
			if (atomicConverter == XmlUntypedConverter.Untyped)
			{
				return XmlUntypedConverter.UntypedList;
			}
			if (atomicConverter == XmlAnyConverter.Item)
			{
				return XmlAnyListConverter.ItemList;
			}
			if (atomicConverter == XmlAnyConverter.AnyAtomic)
			{
				return XmlAnyListConverter.AnyAtomicList;
			}
			return new XmlListConverter((XmlBaseConverter)atomicConverter);
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0008DB45 File Offset: 0x0008CB45
		public override object ChangeType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			return this.ChangeListType(value, destinationType, nsResolver);
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0008DB6C File Offset: 0x0008CB6C
		protected override object ChangeListType(object value, Type destinationType, IXmlNamespaceResolver nsResolver)
		{
			Type type = value.GetType();
			if (destinationType == XmlBaseConverter.ObjectType)
			{
				destinationType = base.DefaultClrType;
			}
			if (!(value is IEnumerable) || !this.IsListType(destinationType))
			{
				throw this.CreateInvalidClrMappingException(type, destinationType);
			}
			if (destinationType == XmlBaseConverter.StringType)
			{
				if (type == XmlBaseConverter.StringType)
				{
					return value;
				}
				return this.ListAsString((IEnumerable)value, nsResolver);
			}
			else
			{
				if (type == XmlBaseConverter.StringType)
				{
					value = this.StringAsList((string)value);
				}
				if (destinationType.IsArray)
				{
					Type elementType = destinationType.GetElementType();
					if (elementType == XmlBaseConverter.ObjectType)
					{
						return this.ToArray<object>(value, nsResolver);
					}
					if (type == destinationType)
					{
						return value;
					}
					if (elementType == XmlBaseConverter.BooleanType)
					{
						return this.ToArray<bool>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.ByteType)
					{
						return this.ToArray<byte>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.ByteArrayType)
					{
						return this.ToArray<byte[]>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DateTimeType)
					{
						return this.ToArray<DateTime>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DateTimeOffsetType)
					{
						return this.ToArray<DateTimeOffset>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DecimalType)
					{
						return this.ToArray<decimal>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.DoubleType)
					{
						return this.ToArray<double>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.Int16Type)
					{
						return this.ToArray<short>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.Int32Type)
					{
						return this.ToArray<int>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.Int64Type)
					{
						return this.ToArray<long>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.SByteType)
					{
						return this.ToArray<sbyte>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.SingleType)
					{
						return this.ToArray<float>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.StringType)
					{
						return this.ToArray<string>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.TimeSpanType)
					{
						return this.ToArray<TimeSpan>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UInt16Type)
					{
						return this.ToArray<ushort>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UInt32Type)
					{
						return this.ToArray<uint>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UInt64Type)
					{
						return this.ToArray<ulong>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.UriType)
					{
						return this.ToArray<Uri>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XmlAtomicValueType)
					{
						return this.ToArray<XmlAtomicValue>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XmlQualifiedNameType)
					{
						return this.ToArray<XmlQualifiedName>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XPathItemType)
					{
						return this.ToArray<XPathItem>(value, nsResolver);
					}
					if (elementType == XmlBaseConverter.XPathNavigatorType)
					{
						return this.ToArray<XPathNavigator>(value, nsResolver);
					}
					throw this.CreateInvalidClrMappingException(type, destinationType);
				}
				else
				{
					if (type == base.DefaultClrType && type != XmlBaseConverter.ObjectArrayType)
					{
						return value;
					}
					return this.ToList(value, nsResolver);
				}
			}
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x0008DDA3 File Offset: 0x0008CDA3
		private bool IsListType(Type type)
		{
			return type == XmlBaseConverter.IListType || type == XmlBaseConverter.ICollectionType || type == XmlBaseConverter.IEnumerableType || type == XmlBaseConverter.StringType || type.IsArray;
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x0008DDD0 File Offset: 0x0008CDD0
		private T[] ToArray<T>(object list, IXmlNamespaceResolver nsResolver)
		{
			IList list2 = list as IList;
			if (list2 != null)
			{
				T[] array = new T[list2.Count];
				for (int i = 0; i < list2.Count; i++)
				{
					array[i] = (T)((object)this.atomicConverter.ChangeType(list2[i], typeof(T), nsResolver));
				}
				return array;
			}
			IEnumerable enumerable = list as IEnumerable;
			List<T> list3 = new List<T>();
			foreach (object value in enumerable)
			{
				list3.Add((T)((object)this.atomicConverter.ChangeType(value, typeof(T), nsResolver)));
			}
			return list3.ToArray();
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0008DEAC File Offset: 0x0008CEAC
		private IList ToList(object list, IXmlNamespaceResolver nsResolver)
		{
			IList list2 = list as IList;
			if (list2 != null)
			{
				object[] array = new object[list2.Count];
				for (int i = 0; i < list2.Count; i++)
				{
					array[i] = this.atomicConverter.ChangeType(list2[i], XmlBaseConverter.ObjectType, nsResolver);
				}
				return array;
			}
			IEnumerable enumerable = list as IEnumerable;
			List<object> list3 = new List<object>();
			foreach (object value in enumerable)
			{
				list3.Add(this.atomicConverter.ChangeType(value, XmlBaseConverter.ObjectType, nsResolver));
			}
			return list3;
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x0008DF6C File Offset: 0x0008CF6C
		private List<string> StringAsList(string value)
		{
			return new List<string>(XmlConvert.SplitString(value));
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x0008DF7C File Offset: 0x0008CF7C
		private string ListAsString(IEnumerable list, IXmlNamespaceResolver nsResolver)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object value in list)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(this.atomicConverter.ToString(value, nsResolver));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x0008DFF8 File Offset: 0x0008CFF8
		private new Exception CreateInvalidClrMappingException(Type sourceType, Type destinationType)
		{
			if (sourceType == destinationType)
			{
				return new InvalidCastException(Res.GetString("XmlConvert_TypeListBadMapping", new object[]
				{
					base.XmlTypeName,
					sourceType.Name
				}));
			}
			return new InvalidCastException(Res.GetString("XmlConvert_TypeListBadMapping2", new object[]
			{
				base.XmlTypeName,
				sourceType.Name,
				destinationType.Name
			}));
		}

		// Token: 0x040012A5 RID: 4773
		protected XmlValueConverter atomicConverter;
	}
}
