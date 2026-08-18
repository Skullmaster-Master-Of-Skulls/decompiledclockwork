using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x02000368 RID: 872
	public class SelectionRangeConverter : TypeConverter
	{
		// Token: 0x06003894 RID: 14484 RVA: 0x000FAC82 File Offset: 0x000F8E82
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(DateTime) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000FACB2 File Offset: 0x000F8EB2
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || destinationType == typeof(DateTime) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000FACE4 File Offset: 0x000F8EE4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string)value).Trim();
				if (text.Length == 0)
				{
					return new SelectionRange(DateTime.Now.Date, DateTime.Now.Date);
				}
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				char c = culture.TextInfo.ListSeparator[0];
				string[] array = text.Split(new char[]
				{
					c
				});
				if (array.Length == 2)
				{
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(DateTime));
					DateTime lower = (DateTime)converter.ConvertFromString(context, culture, array[0]);
					DateTime upper = (DateTime)converter.ConvertFromString(context, culture, array[1]);
					return new SelectionRange(lower, upper);
				}
				throw new ArgumentException(SR.GetString("TextParseFailedFormat", new object[]
				{
					text,
					"Start" + c.ToString() + " End"
				}));
			}
			else
			{
				if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					return new SelectionRange(dateTime, dateTime);
				}
				return base.ConvertFrom(context, culture, value);
			}
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000FADFC File Offset: 0x000F8FFC
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			SelectionRange selectionRange = value as SelectionRange;
			if (selectionRange != null)
			{
				if (destinationType == typeof(string))
				{
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					string separator = culture.TextInfo.ListSeparator + " ";
					PropertyDescriptorCollection properties = base.GetProperties(value);
					string[] array = new string[properties.Count];
					for (int i = 0; i < properties.Count; i++)
					{
						object value2 = properties[i].GetValue(value);
						array[i] = TypeDescriptor.GetConverter(value2).ConvertToString(context, culture, value2);
					}
					return string.Join(separator, array);
				}
				if (destinationType == typeof(DateTime))
				{
					return selectionRange.Start;
				}
				if (destinationType == typeof(InstanceDescriptor))
				{
					ConstructorInfo constructor = typeof(SelectionRange).GetConstructor(new Type[]
					{
						typeof(DateTime),
						typeof(DateTime)
					});
					if (constructor != null)
					{
						return new InstanceDescriptor(constructor, new object[]
						{
							selectionRange.Start,
							selectionRange.End
						});
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000FAF58 File Offset: 0x000F9158
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			object result;
			try
			{
				result = new SelectionRange((DateTime)propertyValues["Start"], (DateTime)propertyValues["End"]);
			}
			catch (InvalidCastException innerException)
			{
				throw new ArgumentException(SR.GetString("PropertyValueInvalidEntry"), innerException);
			}
			catch (NullReferenceException innerException2)
			{
				throw new ArgumentException(SR.GetString("PropertyValueInvalidEntry"), innerException2);
			}
			return result;
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x00013062 File Offset: 0x00011262
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x000FAFD0 File Offset: 0x000F91D0
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(SelectionRange), attributes);
			return properties.Sort(new string[]
			{
				"Start",
				"End"
			});
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x00013062 File Offset: 0x00011262
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
