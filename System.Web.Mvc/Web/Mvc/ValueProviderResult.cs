using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001B6 RID: 438
	[Serializable]
	public class ValueProviderResult
	{
		// Token: 0x06000C5D RID: 3165 RVA: 0x00020B3F File Offset: 0x0001ED3F
		protected ValueProviderResult()
		{
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00020B47 File Offset: 0x0001ED47
		public ValueProviderResult(object rawValue, string attemptedValue, CultureInfo culture)
		{
			this.RawValue = rawValue;
			this.AttemptedValue = attemptedValue;
			this.Culture = culture;
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00020B64 File Offset: 0x0001ED64
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x00020B6C File Offset: 0x0001ED6C
		public string AttemptedValue { get; protected set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x00020B75 File Offset: 0x0001ED75
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x00020B90 File Offset: 0x0001ED90
		public CultureInfo Culture
		{
			get
			{
				if (this._instanceCulture == null)
				{
					this._instanceCulture = ValueProviderResult._staticCulture;
				}
				return this._instanceCulture;
			}
			protected set
			{
				this._instanceCulture = value;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x00020B99 File Offset: 0x0001ED99
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x00020BA1 File Offset: 0x0001EDA1
		public object RawValue { get; protected set; }

		// Token: 0x06000C65 RID: 3173 RVA: 0x00020BAC File Offset: 0x0001EDAC
		private static object ConvertSimpleType(CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || destinationType.IsInstanceOfType(value))
			{
				return value;
			}
			string text = value as string;
			if (text != null && string.IsNullOrWhiteSpace(text))
			{
				return null;
			}
			Type underlyingType = Nullable.GetUnderlyingType(destinationType);
			if (underlyingType != null)
			{
				destinationType = underlyingType;
			}
			if (text == null)
			{
				IConvertible convertible = value as IConvertible;
				if (convertible != null)
				{
					try
					{
						return convertible.ToType(destinationType, culture);
					}
					catch
					{
					}
				}
			}
			TypeConverter converter = TypeDescriptor.GetConverter(destinationType);
			bool flag = converter.CanConvertFrom(value.GetType());
			if (!flag)
			{
				converter = TypeDescriptor.GetConverter(value.GetType());
			}
			object result;
			if (!flag && !converter.CanConvertTo(destinationType))
			{
				if (destinationType.IsEnum && value is int)
				{
					return Enum.ToObject(destinationType, (int)value);
				}
				string message = string.Format(CultureInfo.CurrentCulture, MvcResources.ValueProviderResult_NoConverterExists, new object[]
				{
					value.GetType().FullName,
					destinationType.FullName
				});
				throw new InvalidOperationException(message);
			}
			else
			{
				try
				{
					object obj = flag ? converter.ConvertFrom(null, culture, value) : converter.ConvertTo(null, culture, value, destinationType);
					result = obj;
				}
				catch (Exception innerException)
				{
					string message2 = string.Format(CultureInfo.CurrentCulture, MvcResources.ValueProviderResult_ConversionThrew, new object[]
					{
						value.GetType().FullName,
						destinationType.FullName
					});
					throw new InvalidOperationException(message2, innerException);
				}
			}
			return result;
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00020D18 File Offset: 0x0001EF18
		public object ConvertTo(Type type)
		{
			return this.ConvertTo(type, null);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00020D24 File Offset: 0x0001EF24
		public virtual object ConvertTo(Type type, CultureInfo culture)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			CultureInfo culture2 = culture ?? this.Culture;
			return ValueProviderResult.UnwrapPossibleArrayType(culture2, this.RawValue, type);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00020D60 File Offset: 0x0001EF60
		private static object UnwrapPossibleArrayType(CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || destinationType.IsInstanceOfType(value))
			{
				return value;
			}
			Array array = value as Array;
			if (destinationType.IsArray)
			{
				Type elementType = destinationType.GetElementType();
				if (array != null)
				{
					IList list = Array.CreateInstance(elementType, array.Length);
					for (int i = 0; i < array.Length; i++)
					{
						list[i] = ValueProviderResult.ConvertSimpleType(culture, array.GetValue(i), elementType);
					}
					return list;
				}
				object value2 = ValueProviderResult.ConvertSimpleType(culture, value, elementType);
				IList list2 = Array.CreateInstance(elementType, 1);
				list2[0] = value2;
				return list2;
			}
			else
			{
				if (array == null)
				{
					return ValueProviderResult.ConvertSimpleType(culture, value, destinationType);
				}
				if (array.Length > 0)
				{
					value = array.GetValue(0);
					return ValueProviderResult.ConvertSimpleType(culture, value, destinationType);
				}
				return null;
			}
		}

		// Token: 0x04000353 RID: 851
		private static readonly CultureInfo _staticCulture = CultureInfo.InvariantCulture;

		// Token: 0x04000354 RID: 852
		private CultureInfo _instanceCulture;
	}
}
