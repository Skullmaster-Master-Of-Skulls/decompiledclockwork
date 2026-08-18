using System;
using System.ComponentModel;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x02000091 RID: 145
	public class TypeConverterMapper : IObjectMapper
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x00011CFB File Offset: 0x0000FEFB
		public object Map(ResolutionContext context)
		{
			if (context.SourceValue == null)
			{
				return context.Engine.CreateObject(context);
			}
			Func<object> converter = TypeConverterMapper.GetConverter(context);
			if (converter == null)
			{
				return null;
			}
			return converter();
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00011D24 File Offset: 0x0000FF24
		private static Func<object> GetConverter(ResolutionContext context)
		{
			TypeConverter typeConverter = TypeConverterMapper.GetTypeConverter(context.SourceType);
			if (typeConverter.CanConvertTo(context.DestinationType))
			{
				return () => typeConverter.ConvertTo(context.SourceValue, context.DestinationType);
			}
			if (context.DestinationType.IsNullableType() && typeConverter.CanConvertTo(Nullable.GetUnderlyingType(context.DestinationType)))
			{
				return () => typeConverter.ConvertTo(context.SourceValue, Nullable.GetUnderlyingType(context.DestinationType));
			}
			typeConverter = TypeConverterMapper.GetTypeConverter(context.DestinationType);
			if (typeConverter.CanConvertFrom(context.SourceType))
			{
				return () => typeConverter.ConvertFrom(context.SourceValue);
			}
			return null;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00011DF4 File Offset: 0x0000FFF4
		public bool IsMatch(TypePair context)
		{
			TypeConverter typeConverter = TypeConverterMapper.GetTypeConverter(context.SourceType);
			TypeConverter typeConverter2 = TypeConverterMapper.GetTypeConverter(context.DestinationType);
			return typeConverter.CanConvertTo(context.DestinationType) || (context.DestinationType.IsNullableType() && typeConverter.CanConvertTo(Nullable.GetUnderlyingType(context.DestinationType))) || typeConverter2.CanConvertFrom(context.SourceType);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00011E57 File Offset: 0x00010057
		private static TypeConverter GetTypeConverter(Type type)
		{
			return TypeDescriptor.GetConverter(type);
		}
	}
}
