using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x02000122 RID: 290
	public static class DynamicListGroupMapper
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x000182D8 File Offset: 0x000164D8
		static DynamicListGroupMapper()
		{
			Mapper.CreateMap<DynamicListGroupDTO, DynamicListGroup>().ForMember((DynamicListGroup pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<DynamicListGroupDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<DynamicListGroup, DynamicListGroupDTO>();
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00018354 File Offset: 0x00016554
		public static DynamicListGroup ToDomainObject(this DynamicListGroupDTO dto)
		{
			return Mapper.Map<DynamicListGroupDTO, DynamicListGroup>(dto);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001836C File Offset: 0x0001656C
		public static DynamicListGroupDTO ToDTO(this DynamicListGroup entity)
		{
			return Mapper.Map<DynamicListGroup, DynamicListGroupDTO>(entity);
		}
	}
}
