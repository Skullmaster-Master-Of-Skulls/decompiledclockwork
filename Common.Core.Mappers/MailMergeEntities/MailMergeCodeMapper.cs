using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Core.Mappers.MailMergeEntities.MailMergeValues;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities
{
	// Token: 0x020000C1 RID: 193
	public static class MailMergeCodeMapper
	{
		// Token: 0x06000336 RID: 822 RVA: 0x00010AEC File Offset: 0x0000ECEC
		static MailMergeCodeMapper()
		{
			MailMergeValueAccommodationDataMapper.CreateMap();
			MailMergeValueBoolMapper.CreateMap();
			MailMergeValueByteArrayMapper.CreateMap();
			MailMergeValueDateTimeMapper.CreateMap();
			MailMergeValueDateTimeNullableMapper.CreateMap();
			MailMergeValueDoubleMapper.CreateMap();
			MailMergeValueDynamicDataMapper.CreateMap();
			MailMergeValueIntMapper.CreateMap();
			MailMergeValueStringMapper.CreateMap();
			MailMergeValueFormatMapper.CreateMap();
			Mapper.CreateMap<MailMergeCodeDTO, MailMergeCode>().ForMember((MailMergeCode pb) => (object)pb.AltPersonIdIndex, delegate(IMemberConfigurationExpression<MailMergeCodeDTO> m)
			{
				m.Ignore();
			}).ForMember((MailMergeCode pb) => pb.MailMergeValueSetterGetter, delegate(IMemberConfigurationExpression<MailMergeCodeDTO> m)
			{
				m.MapFrom<IList<MailMergeValueBase>>((MailMergeCodeDTO pbdto) => (pbdto.MailMergeValues == null || pbdto.MailMergeValues.Count < 1) ? null : pbdto.MailMergeValues.ToDomainObject());
			});
			Mapper.CreateMap<MailMergeCode, MailMergeCodeDTO>().ForMember((MailMergeCodeDTO pb) => pb.MailMergeValues, delegate(IMemberConfigurationExpression<MailMergeCode> m)
			{
				m.MapFrom<IList<MailMergeValueBaseDTO>>((MailMergeCode pbdto) => (pbdto.MailMergeValueSetterGetter == null || pbdto.MailMergeValueSetterGetter.Count < 1) ? null : pbdto.MailMergeValueSetterGetter.ToDTO());
			});
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00010C3C File Offset: 0x0000EE3C
		public static MailMergeCode ToDomainObject(this MailMergeCodeDTO mailMergeCodeDTO)
		{
			return Mapper.Map<MailMergeCodeDTO, MailMergeCode>(mailMergeCodeDTO);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00010C54 File Offset: 0x0000EE54
		public static MailMergeCodeDTO ToDTO(this MailMergeCode mailMergeCode)
		{
			return Mapper.Map<MailMergeCode, MailMergeCodeDTO>(mailMergeCode);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00010C6C File Offset: 0x0000EE6C
		public static IList<MailMergeValueBase> ToDomainObject(this IList<MailMergeValueBaseDTO> dtos)
		{
			bool flag = dtos == null || dtos.Count < 1;
			IList<MailMergeValueBase> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<object> list = new List<object>();
				foreach (MailMergeValueBaseDTO mailMergeValueBaseDTO in dtos)
				{
					string text = mailMergeValueBaseDTO.GetType().Name;
					bool flag2 = text.EndsWith("DTO");
					if (flag2)
					{
						text = text.Substring(0, text.Length - 3);
					}
					string typeName = string.Format("TechnoPro.Common.Core.Mappers.MailMergeEntities.MailMergeValues.{0}Mapper, Common.Core.Mappers", text);
					Type type = Type.GetType(typeName);
					bool flag3 = type != null;
					if (flag3)
					{
						MethodInfo method = type.GetMethod("ToDomainObject", BindingFlags.Static | BindingFlags.Public);
						object item = method.Invoke(null, new object[]
						{
							mailMergeValueBaseDTO
						});
						list.Add(item);
					}
				}
				result = list.Cast<MailMergeValueBase>().ToList<MailMergeValueBase>();
			}
			return result;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00010D70 File Offset: 0x0000EF70
		public static IList<MailMergeValueBaseDTO> ToDTO(this IList<MailMergeValueBase> items)
		{
			bool flag = items == null || items.Count < 1;
			IList<MailMergeValueBaseDTO> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<object> list = new List<object>();
				foreach (MailMergeValueBase mailMergeValueBase in items)
				{
					string name = mailMergeValueBase.GetType().Name;
					string typeName = string.Format("TechnoPro.Common.Core.Mappers.MailMergeEntities.MailMergeValues.{0}Mapper, Common.Core.Mappers", name);
					Type type = Type.GetType(typeName);
					bool flag2 = type != null;
					if (flag2)
					{
						MethodInfo method = type.GetMethod("ToDTO", BindingFlags.Static | BindingFlags.Public);
						object item = method.Invoke(null, new object[]
						{
							mailMergeValueBase
						});
						list.Add(item);
					}
				}
				result = list.Cast<MailMergeValueBaseDTO>().ToList<MailMergeValueBaseDTO>();
			}
			return result;
		}
	}
}
