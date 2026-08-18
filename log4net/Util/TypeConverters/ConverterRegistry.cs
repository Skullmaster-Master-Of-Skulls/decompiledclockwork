using System;
using System.Collections;
using System.Net;
using System.Text;
using log4net.Layout;

namespace log4net.Util.TypeConverters
{
	// Token: 0x020000E7 RID: 231
	public sealed class ConverterRegistry
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x00015007 File Offset: 0x00013207
		private ConverterRegistry()
		{
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00015010 File Offset: 0x00013210
		static ConverterRegistry()
		{
			ConverterRegistry.AddConverter(typeof(bool), typeof(BooleanConverter));
			ConverterRegistry.AddConverter(typeof(Encoding), typeof(EncodingConverter));
			ConverterRegistry.AddConverter(typeof(Type), typeof(TypeConverter));
			ConverterRegistry.AddConverter(typeof(PatternLayout), typeof(PatternLayoutConverter));
			ConverterRegistry.AddConverter(typeof(PatternString), typeof(PatternStringConverter));
			ConverterRegistry.AddConverter(typeof(IPAddress), typeof(IPAddressConverter));
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000150CC File Offset: 0x000132CC
		public static void AddConverter(Type destinationType, object converter)
		{
			if (destinationType != null && converter != null)
			{
				lock (ConverterRegistry.s_type2converter)
				{
					ConverterRegistry.s_type2converter[destinationType] = converter;
				}
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00015120 File Offset: 0x00013320
		public static void AddConverter(Type destinationType, Type converterType)
		{
			ConverterRegistry.AddConverter(destinationType, ConverterRegistry.CreateConverterInstance(converterType));
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00015130 File Offset: 0x00013330
		public static IConvertTo GetConvertTo(Type sourceType, Type destinationType)
		{
			IConvertTo result;
			lock (ConverterRegistry.s_type2converter)
			{
				IConvertTo convertTo = ConverterRegistry.s_type2converter[sourceType] as IConvertTo;
				if (convertTo == null)
				{
					convertTo = (ConverterRegistry.GetConverterFromAttribute(sourceType) as IConvertTo);
					if (convertTo != null)
					{
						ConverterRegistry.s_type2converter[sourceType] = convertTo;
					}
				}
				result = convertTo;
			}
			return result;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001519C File Offset: 0x0001339C
		public static IConvertFrom GetConvertFrom(Type destinationType)
		{
			IConvertFrom result;
			lock (ConverterRegistry.s_type2converter)
			{
				IConvertFrom convertFrom = ConverterRegistry.s_type2converter[destinationType] as IConvertFrom;
				if (convertFrom == null)
				{
					convertFrom = (ConverterRegistry.GetConverterFromAttribute(destinationType) as IConvertFrom);
					if (convertFrom != null)
					{
						ConverterRegistry.s_type2converter[destinationType] = convertFrom;
					}
				}
				result = convertFrom;
			}
			return result;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00015208 File Offset: 0x00013408
		private static object GetConverterFromAttribute(Type destinationType)
		{
			object[] customAttributes = destinationType.GetCustomAttributes(typeof(TypeConverterAttribute), true);
			if (customAttributes != null && customAttributes.Length > 0)
			{
				TypeConverterAttribute typeConverterAttribute = customAttributes[0] as TypeConverterAttribute;
				if (typeConverterAttribute != null)
				{
					Type typeFromString = SystemInfo.GetTypeFromString(destinationType, typeConverterAttribute.ConverterTypeName, false, true);
					return ConverterRegistry.CreateConverterInstance(typeFromString);
				}
			}
			return null;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00015254 File Offset: 0x00013454
		private static object CreateConverterInstance(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException("converterType", "CreateConverterInstance cannot create instance, converterType is null");
			}
			if (!typeof(IConvertFrom).IsAssignableFrom(converterType))
			{
				if (!typeof(IConvertTo).IsAssignableFrom(converterType))
				{
					goto IL_69;
				}
			}
			try
			{
				return Activator.CreateInstance(converterType);
			}
			catch (Exception exception)
			{
				LogLog.Error(ConverterRegistry.declaringType, "Cannot CreateConverterInstance of type [" + converterType.FullName + "], Exception in call to Activator.CreateInstance", exception);
				goto IL_88;
			}
			IL_69:
			LogLog.Error(ConverterRegistry.declaringType, "Cannot CreateConverterInstance of type [" + converterType.FullName + "], type does not implement IConvertFrom or IConvertTo");
			IL_88:
			return null;
		}

		// Token: 0x04000296 RID: 662
		private static readonly Type declaringType = typeof(ConverterRegistry);

		// Token: 0x04000297 RID: 663
		private static Hashtable s_type2converter = new Hashtable();
	}
}
