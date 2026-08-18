using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x0200007D RID: 125
	public class EnumMapper : IObjectMapper
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x00010C5C File Offset: 0x0000EE5C
		public object Map(ResolutionContext context)
		{
			bool flag = false;
			Type enumerationType = TypeHelper.GetEnumerationType(context.SourceType);
			Type enumerationType2 = TypeHelper.GetEnumerationType(context.DestinationType);
			if (EnumMapper.EnumToStringMapping(context.Types, ref flag))
			{
				if (context.SourceValue == null)
				{
					return context.Engine.CreateObject(context);
				}
				if (!flag)
				{
					return Enum.GetName(enumerationType, context.SourceValue);
				}
				string value = context.SourceValue.ToString();
				if (string.IsNullOrEmpty(value))
				{
					return context.Engine.CreateObject(context);
				}
				return Enum.Parse(enumerationType2, value, true);
			}
			else if (EnumMapper.EnumToEnumMapping(context.Types))
			{
				if (context.SourceValue == null)
				{
					if (context.Engine.ShouldMapSourceValueAsNull(context) && context.DestinationType.IsNullableType())
					{
						return null;
					}
					return context.Engine.CreateObject(context);
				}
				else
				{
					if (!Enum.IsDefined(enumerationType, context.SourceValue))
					{
						return Enum.ToObject(enumerationType2, context.SourceValue);
					}
					if (!Enum.GetNames(enumerationType2).Contains(context.SourceValue.ToString()))
					{
						Type underlyingType = Enum.GetUnderlyingType(enumerationType);
						object value2 = Convert.ChangeType(context.SourceValue, underlyingType);
						return Enum.ToObject(context.DestinationType, value2);
					}
					return Enum.Parse(enumerationType2, Enum.GetName(enumerationType, context.SourceValue), true);
				}
			}
			else
			{
				if (!EnumMapper.EnumToUnderlyingTypeMapping(context.Types, ref flag))
				{
					return null;
				}
				if (flag && context.SourceValue != null)
				{
					return Enum.Parse(enumerationType2, context.SourceValue.ToString(), true);
				}
				if (EnumMapper.EnumToNullableTypeMapping(context.Types))
				{
					return EnumMapper.ConvertEnumToNullableType(context);
				}
				return Convert.ChangeType(context.SourceValue, context.DestinationType, null);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00010DE4 File Offset: 0x0000EFE4
		public bool IsMatch(TypePair context)
		{
			bool flag = false;
			return EnumMapper.EnumToStringMapping(context, ref flag) || EnumMapper.EnumToEnumMapping(context) || EnumMapper.EnumToUnderlyingTypeMapping(context, ref flag);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00010E10 File Offset: 0x0000F010
		private static bool EnumToEnumMapping(TypePair context)
		{
			Type enumerationType = TypeHelper.GetEnumerationType(context.SourceType);
			Type enumerationType2 = TypeHelper.GetEnumerationType(context.DestinationType);
			return enumerationType != null && enumerationType2 != null;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00010E48 File Offset: 0x0000F048
		private static bool EnumToUnderlyingTypeMapping(TypePair context, ref bool toEnum)
		{
			Type enumerationType = TypeHelper.GetEnumerationType(context.SourceType);
			Type enumerationType2 = TypeHelper.GetEnumerationType(context.DestinationType);
			if (enumerationType != null)
			{
				return context.DestinationType.IsAssignableFrom(Enum.GetUnderlyingType(enumerationType));
			}
			if (enumerationType2 != null)
			{
				toEnum = true;
				return context.SourceType.IsAssignableFrom(Enum.GetUnderlyingType(enumerationType2));
			}
			return false;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00010EA8 File Offset: 0x0000F0A8
		private static bool EnumToStringMapping(TypePair context, ref bool toEnum)
		{
			Type enumerationType = TypeHelper.GetEnumerationType(context.SourceType);
			Type enumerationType2 = TypeHelper.GetEnumerationType(context.DestinationType);
			if (enumerationType != null)
			{
				return context.DestinationType.IsAssignableFrom(typeof(string));
			}
			if (enumerationType2 != null)
			{
				toEnum = true;
				return context.SourceType.IsAssignableFrom(typeof(string));
			}
			return false;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00010F0D File Offset: 0x0000F10D
		private static bool EnumToNullableTypeMapping(TypePair context)
		{
			return context.DestinationType.IsGenericType() && context.DestinationType.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00010F38 File Offset: 0x0000F138
		private static object ConvertEnumToNullableType(ResolutionContext context)
		{
			NullableConverter nullableConverter = new NullableConverter(context.DestinationType);
			if (context.IsSourceValueNull)
			{
				return nullableConverter.ConvertFrom(context.SourceValue);
			}
			Type underlyingType = nullableConverter.UnderlyingType;
			return Convert.ChangeType(context.SourceValue, underlyingType, null);
		}
	}
}
