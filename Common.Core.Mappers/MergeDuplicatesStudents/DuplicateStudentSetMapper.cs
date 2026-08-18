using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates;
using TechnoPro.Common.Public.Entities.MergeDuplicates.Students;

namespace TechnoPro.Common.Core.Mappers.MergeDuplicatesStudents
{
	// Token: 0x020000BD RID: 189
	public static class DuplicateStudentSetMapper
	{
		// Token: 0x06000324 RID: 804 RVA: 0x000105A8 File Offset: 0x0000E7A8
		static DuplicateStudentSetMapper()
		{
			DuplicateStudentMapper.CreateMap();
			DuplicateDynamicDataItemMapper.CreateMap();
			Mapper.CreateMap<DuplicateStudentSetDTO, DuplicateStudentSet>().ForMember((DuplicateStudentSet pb) => pb.DuplicateDataItems, delegate(IMemberConfigurationExpression<DuplicateStudentSetDTO> m)
			{
				m.MapFrom<List<DuplicateDynamicDataItem>>((DuplicateStudentSetDTO pbdto) => (pbdto.DuplicateDataItems == null) ? null : pbdto.DuplicateDataItems.ToList<DuplicateDynamicDataItemDTO>().ConvertAll<DuplicateDynamicDataItem>((DuplicateDynamicDataItemDTO q) => q.ToDomainObject()));
			}).ForMember((DuplicateStudentSet pb) => pb.Student1, delegate(IMemberConfigurationExpression<DuplicateStudentSetDTO> m)
			{
				m.MapFrom<DuplicateStudent>((DuplicateStudentSetDTO pbdto) => (pbdto.Student1 == null) ? null : pbdto.Student1.ToDomainObject());
			}).ForMember((DuplicateStudentSet pb) => pb.Student2, delegate(IMemberConfigurationExpression<DuplicateStudentSetDTO> m)
			{
				m.MapFrom<DuplicateStudent>((DuplicateStudentSetDTO pbdto) => (pbdto.Student2 == null) ? null : pbdto.Student2.ToDomainObject());
			});
			Mapper.CreateMap<DuplicateStudentSet, DuplicateStudentSetDTO>().ForMember((DuplicateStudentSetDTO pb) => pb.DuplicateDataItems, delegate(IMemberConfigurationExpression<DuplicateStudentSet> m)
			{
				m.MapFrom<List<DuplicateDynamicDataItemDTO>>((DuplicateStudentSet pbdto) => (pbdto.DuplicateDataItems == null) ? null : pbdto.DuplicateDataItems.ToList<DuplicateDynamicDataItem>().ConvertAll<DuplicateDynamicDataItemDTO>((DuplicateDynamicDataItem q) => q.ToDTO()));
			}).ForMember((DuplicateStudentSetDTO pb) => pb.Student1, delegate(IMemberConfigurationExpression<DuplicateStudentSet> m)
			{
				m.MapFrom<DuplicateStudentDTO>((DuplicateStudentSet pbdto) => (pbdto.Student1 == null) ? null : pbdto.Student1.ToDTO());
			}).ForMember((DuplicateStudentSetDTO pb) => pb.Student2, delegate(IMemberConfigurationExpression<DuplicateStudentSet> m)
			{
				m.MapFrom<DuplicateStudentDTO>((DuplicateStudentSet pbdto) => (pbdto.Student2 == null) ? null : pbdto.Student2.ToDTO());
			});
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000107A4 File Offset: 0x0000E9A4
		public static DuplicateStudentSet ToDomainObject(this DuplicateStudentSetDTO dto)
		{
			return Mapper.Map<DuplicateStudentSetDTO, DuplicateStudentSet>(dto);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000107BC File Offset: 0x0000E9BC
		public static DuplicateStudentSetDTO ToDTO(this DuplicateStudentSet item)
		{
			return Mapper.Map<DuplicateStudentSet, DuplicateStudentSetDTO>(item);
		}
	}
}
