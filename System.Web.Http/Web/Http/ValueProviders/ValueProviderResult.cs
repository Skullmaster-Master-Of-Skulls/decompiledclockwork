using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Web.Http.Properties;

namespace System.Web.Http.ValueProviders
{
	// Token: 0x020001A6 RID: 422
	[Serializable]
	public class ValueProviderResult
	{
		// Token: 0x06000A9D RID: 2717 RVA: 0x000239E2 File Offset: 0x00021BE2
		protected ValueProviderResult()
		{
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x000239EA File Offset: 0x00021BEA
		public ValueProviderResult(object rawValue, string attemptedValue, CultureInfo culture)
		{
			this.RawValue = rawValue;
			this.AttemptedValue = attemptedValue;
			this.Culture = culture;
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00023A07 File Offset: 0x00021C07
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x00023A0F File Offset: 0x00021C0F
		public string AttemptedValue { get; protected set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00023A18 File Offset: 0x00021C18
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x00023A33 File Offset: 0x00021C33
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

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x00023A3C File Offset: 0x00021C3C
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x00023A44 File Offset: 0x00021C44
		public object RawValue { get; protected set; }

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00023A50 File Offset: 0x00021C50
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
			TypeConverter converter = TypeDescriptor.GetConverter(destinationType);
			bool flag = converter.CanConvertFrom(value.GetType());
			if (!flag)
			{
				converter = TypeDescriptor.GetConverter(value.GetType());
			}
			if (flag || converter.CanConvertTo(destinationType))
			{
				object result;
				try
				{
					result = (flag ? converter.ConvertFrom(null, culture, value) : converter.ConvertTo(null, culture, value, destinationType));
				}
				catch (Exception innerException)
				{
					throw Error.InvalidOperation(innerException, SRResources.ValueProviderResult_ConversionThrew, new object[]
					{
						value.GetType(),
						destinationType
					});
				}
				return result;
			}
			if (destinationType.IsEnum && value is int)
			{
				return Enum.ToObject(destinationType, (int)value);
			}
			Type underlyingType = Nullable.GetUnderlyingType(destinationType);
			if (underlyingType != null)
			{
				return ValueProviderResult.ConvertSimpleType(culture, value, underlyingType);
			}
			throw Error.InvalidOperation(SRResources.ValueProviderResult_NoConverterExists, new object[]
			{
				value.GetType(),
				destinationType
			});
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00023B60 File Offset: 0x00021D60
		public object ConvertTo(Type type)
		{
			return this.ConvertTo(type, null);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00023B6C File Offset: 0x00021D6C
		public virtual object ConvertTo(Type type, CultureInfo culture)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			object rawValue = this.RawValue;
			if (rawValue == null)
			{
				if (!type.IsValueType)
				{
					return null;
				}
				return Activator.CreateInstance(type);
			}
			else
			{
				if (type.IsInstanceOfType(rawValue))
				{
					return rawValue;
				}
				CultureInfo culture2 = culture ?? this.Culture;
				return ValueProviderResult.UnwrapPossibleListType(culture2, rawValue, type);
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00023BC8 File Offset: 0x00021DC8
		private static object UnwrapPossibleListType(CultureInfo culture, object value, Type destinationType)
		{
			IList list = value as IList;
			if (destinationType.IsArray)
			{
				Type elementType = destinationType.GetElementType();
				if (list != null)
				{
					IList list2 = Array.CreateInstance(elementType, list.Count);
					for (int i = 0; i < list.Count; i++)
					{
						list2[i] = ValueProviderResult.ConvertSimpleType(culture, list[i], elementType);
					}
					return list2;
				}
				object value2 = ValueProviderResult.ConvertSimpleType(culture, value, elementType);
				IList list3 = Array.CreateInstance(elementType, 1);
				list3[0] = value2;
				return list3;
			}
			else
			{
				if (list == null)
				{
					return ValueProviderResult.ConvertSimpleType(culture, value, destinationType);
				}
				if (list.Count > 0)
				{
					value = list[0];
					return ValueProviderResult.ConvertSimpleType(culture, value, destinationType);
				}
				return null;
			}
		}

		// Token: 0x0400031B RID: 795
		private static readonly CultureInfo _staticCulture = CultureInfo.InvariantCulture;

		// Token: 0x0400031C RID: 796
		private CultureInfo _instanceCulture;
	}
}
