using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.DataHolders;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Field;
using TechnoPro.Common.Core.Mappers.CustomForms.Field;
using TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders;
using TechnoPro.Common.Public.Entities.CustomForms.Field;

namespace TechnoPro.Common.Core.Mappers.CustomForms.Data.DataHolders
{
	// Token: 0x02000158 RID: 344
	public static class CustomDataHolderMapper
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x0001B318 File Offset: 0x00019518
		static CustomDataHolderMapper()
		{
			CustomListItemMapper.CreateMap();
			Mapper.CreateMap<CustomDataHolderDTO, CustomDataHolder>().Include<CustomDataBooleanDTO, CustomDataBoolean>().Include<CustomDataDateTimeDTO, CustomDataDateTime>().Include<CustomDataFileDTO, CustomDataFile>().Include<CustomDataIntDTO, CustomDataInt>().Include<CustomDataListItemDTO, CustomDataListItem>().Include<CustomDataStringDTO, CustomDataString>();
			Mapper.CreateMap<CustomDataHolder, CustomDataHolderDTO>().Include<CustomDataBoolean, CustomDataBooleanDTO>().Include<CustomDataDateTime, CustomDataDateTimeDTO>().Include<CustomDataFile, CustomDataFileDTO>().Include<CustomDataInt, CustomDataIntDTO>().Include<CustomDataListItem, CustomDataListItemDTO>().Include<CustomDataString, CustomDataStringDTO>();
			Mapper.CreateMap<CustomDataBooleanDTO, CustomDataBoolean>();
			Mapper.CreateMap<CustomDataBooleanNullableDTO, CustomDataBooleanNullable>();
			Mapper.CreateMap<CustomDataDateTimeDTO, CustomDataDateTime>();
			Mapper.CreateMap<CustomDataFileDTO, CustomDataFile>();
			Mapper.CreateMap<CustomDataIntDTO, CustomDataInt>();
			Mapper.CreateMap<CustomDataListItemDTO, CustomDataListItem>().ForMember((CustomDataListItem pb) => pb.ListItem, delegate(IMemberConfigurationExpression<CustomDataListItemDTO> m)
			{
				m.MapFrom<CustomListItem>((CustomDataListItemDTO pbdto) => (pbdto.ListItem == null) ? null : pbdto.ListItem.ToDomainObject());
			});
			Mapper.CreateMap<CustomDataStringDTO, CustomDataString>();
			Mapper.CreateMap<CustomDataBoolean, CustomDataBooleanDTO>();
			Mapper.CreateMap<CustomDataBooleanNullable, CustomDataBooleanNullableDTO>();
			Mapper.CreateMap<CustomDataDateTime, CustomDataDateTimeDTO>();
			Mapper.CreateMap<CustomDataFile, CustomDataFileDTO>();
			Mapper.CreateMap<CustomDataInt, CustomDataIntDTO>();
			Mapper.CreateMap<CustomDataListItem, CustomDataListItemDTO>().ForMember((CustomDataListItemDTO pb) => pb.ListItem, delegate(IMemberConfigurationExpression<CustomDataListItem> m)
			{
				m.MapFrom<CustomListItemDTO>((CustomDataListItem pbdto) => (pbdto.ListItem == null) ? null : pbdto.ListItem.ToDTO());
			});
			Mapper.CreateMap<CustomDataString, CustomDataStringDTO>();
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001B464 File Offset: 0x00019664
		public static CustomDataHolder ToDomainObject(this CustomDataHolderDTO dto)
		{
			return Mapper.Map<CustomDataHolderDTO, CustomDataHolder>(dto);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001B47C File Offset: 0x0001967C
		public static CustomDataHolderDTO ToDTO(this CustomDataHolder item)
		{
			return Mapper.Map<CustomDataHolder, CustomDataHolderDTO>(item);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001B494 File Offset: 0x00019694
		public static IList<CustomDataHolder> ToDomainObject(this IList<CustomDataHolderDTO> dtos)
		{
			IList<CustomDataHolder> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in dtos
				select g.ToDomainObject()).ToList<CustomDataHolder>();
			}
			return result;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001B4D8 File Offset: 0x000196D8
		public static IList<CustomDataHolderDTO> ToDTO(this IList<CustomDataHolder> items)
		{
			IList<CustomDataHolderDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from g in items
				select g.ToDTO()).ToList<CustomDataHolderDTO>();
			}
			return result;
		}
	}
}
