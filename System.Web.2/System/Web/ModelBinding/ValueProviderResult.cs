using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.ModelBinding
{
	// Token: 0x02000670 RID: 1648
	[Serializable]
	public class ValueProviderResult
	{
		// Token: 0x06005064 RID: 20580 RVA: 0x000030B5 File Offset: 0x000012B5
		protected ValueProviderResult()
		{
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x0011579F File Offset: 0x0011399F
		public ValueProviderResult(object rawValue, string attemptedValue, CultureInfo culture)
		{
			this.RawValue = rawValue;
			this.AttemptedValue = attemptedValue;
			this.Culture = culture;
		}

		// Token: 0x1700172B RID: 5931
		// (get) Token: 0x06005066 RID: 20582 RVA: 0x001157BC File Offset: 0x001139BC
		// (set) Token: 0x06005067 RID: 20583 RVA: 0x001157C4 File Offset: 0x001139C4
		public string AttemptedValue { get; protected set; }

		// Token: 0x1700172C RID: 5932
		// (get) Token: 0x06005068 RID: 20584 RVA: 0x001157CD File Offset: 0x001139CD
		// (set) Token: 0x06005069 RID: 20585 RVA: 0x001157E8 File Offset: 0x001139E8
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

		// Token: 0x1700172D RID: 5933
		// (get) Token: 0x0600506A RID: 20586 RVA: 0x001157F1 File Offset: 0x001139F1
		// (set) Token: 0x0600506B RID: 20587 RVA: 0x001157F9 File Offset: 0x001139F9
		public object RawValue { get; protected set; }

		// Token: 0x0600506C RID: 20588 RVA: 0x00115804 File Offset: 0x00113A04
		private static object ConvertSimpleType(CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || destinationType.IsInstanceOfType(value))
			{
				return value;
			}
			string text = value as string;
			if (text != null && text.Trim().Length == 0)
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
					object obj = flag ? converter.ConvertFrom(null, culture, value) : converter.ConvertTo(null, culture, value, destinationType);
					result = obj;
				}
				catch (Exception innerException)
				{
					string message = string.Format(CultureInfo.CurrentCulture, SR.GetString("ValueProviderResult_ConversionThrew"), new object[]
					{
						value.GetType().FullName,
						destinationType.FullName
					});
					throw new InvalidOperationException(message, innerException);
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
			string message2 = string.Format(CultureInfo.CurrentCulture, SR.GetString("ValueProviderResult_NoConverterExists"), new object[]
			{
				value.GetType().FullName,
				destinationType.FullName
			});
			throw new InvalidOperationException(message2);
		}

		// Token: 0x0600506D RID: 20589 RVA: 0x0011594C File Offset: 0x00113B4C
		public object ConvertTo(Type type)
		{
			return this.ConvertTo(type, null);
		}

		// Token: 0x0600506E RID: 20590 RVA: 0x00115958 File Offset: 0x00113B58
		public virtual object ConvertTo(Type type, CultureInfo culture)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			CultureInfo culture2 = culture ?? this.Culture;
			return ValueProviderResult.UnwrapPossibleArrayType(culture2, this.RawValue, type);
		}

		// Token: 0x0600506F RID: 20591 RVA: 0x00115994 File Offset: 0x00113B94
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

		// Token: 0x04002AC0 RID: 10944
		private static readonly CultureInfo _staticCulture = CultureInfo.InvariantCulture;

		// Token: 0x04002AC1 RID: 10945
		private CultureInfo _instanceCulture;
	}
}
