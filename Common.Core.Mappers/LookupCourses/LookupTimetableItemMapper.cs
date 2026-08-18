using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.LookupCourses
{
	// Token: 0x020000E0 RID: 224
	public static class LookupTimetableItemMapper
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x00012078 File Offset: 0x00010278
		static LookupTimetableItemMapper()
		{
			Mapper.CreateMap<LookupTimetableItemDTO, LookupTimetableItem>().ForMember((LookupTimetableItem pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<LookupTimetableItemDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<LookupTimetableItem, LookupTimetableItemDTO>();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000120F4 File Offset: 0x000102F4
		public static LookupTimetableItem ToDomainObject(this LookupTimetableItemDTO lookupTimetableItemDTO)
		{
			return Mapper.Map<LookupTimetableItemDTO, LookupTimetableItem>(lookupTimetableItemDTO);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001210C File Offset: 0x0001030C
		public static LookupTimetableItemDTO ToDTO(this LookupTimetableItem lookupTimetableItem)
		{
			return Mapper.Map<LookupTimetableItem, LookupTimetableItemDTO>(lookupTimetableItem);
		}
	}
}
