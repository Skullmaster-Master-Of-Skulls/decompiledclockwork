using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.MergeDuplicates.Students;

namespace TechnoPro.Common.Core.Mappers.MergeDuplicatesStudents
{
	// Token: 0x020000BB RID: 187
	public static class DuplicateDynamicDataItemMapper
	{
		// Token: 0x0600031C RID: 796 RVA: 0x0001014C File Offset: 0x0000E34C
		static DuplicateDynamicDataItemMapper()
		{
			DynamicDataMapper.CreateMap();
			Mapper.CreateMap<DuplicateDynamicDataItemDTO, DuplicateDynamicDataItem>().ForMember((DuplicateDynamicDataItem pb) => pb.DataItem1, delegate(IMemberConfigurationExpression<DuplicateDynamicDataItemDTO> m)
			{
				m.MapFrom<DynamicData>((DuplicateDynamicDataItemDTO pbdto) => pbdto.DataItem1.ToDomainObject());
			}).ForMember((DuplicateDynamicDataItem pb) => pb.DataItem2, delegate(IMemberConfigurationExpression<DuplicateDynamicDataItemDTO> m)
			{
				m.MapFrom<DynamicData>((DuplicateDynamicDataItemDTO pbdto) => pbdto.DataItem2.ToDomainObject());
			});
			Mapper.CreateMap<DuplicateDynamicDataItem, DuplicateDynamicDataItemDTO>().ForMember((DuplicateDynamicDataItemDTO pb) => pb.DataItem1, delegate(IMemberConfigurationExpression<DuplicateDynamicDataItem> m)
			{
				m.MapFrom<DynamicDataDTO>((DuplicateDynamicDataItem pbdto) => pbdto.DataItem1.ToDTO());
			}).ForMember((DuplicateDynamicDataItemDTO pb) => pb.DataItem2, delegate(IMemberConfigurationExpression<DuplicateDynamicDataItem> m)
			{
				m.MapFrom<DynamicDataDTO>((DuplicateDynamicDataItem pbdto) => pbdto.DataItem2.ToDTO());
			});
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000102A4 File Offset: 0x0000E4A4
		public static DuplicateDynamicDataItem ToDomainObject(this DuplicateDynamicDataItemDTO dto)
		{
			return Mapper.Map<DuplicateDynamicDataItemDTO, DuplicateDynamicDataItem>(dto);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000102BC File Offset: 0x0000E4BC
		public static DuplicateDynamicDataItemDTO ToDTO(this DuplicateDynamicDataItem item)
		{
			return Mapper.Map<DuplicateDynamicDataItem, DuplicateDynamicDataItemDTO>(item);
		}
	}
}
