using System;
using System.Collections.Generic;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x02000118 RID: 280
	public static class DynamicDataSetWithStudentNameMapper
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x00017330 File Offset: 0x00015530
		static DynamicDataSetWithStudentNameMapper()
		{
			PersonBaseMapper.CreateMap();
			DynamicDataMapper.CreateMap();
			Mapper.CreateMap<DynamicDataSetWithStudentNameDTO, DynamicDataSetWithStudentName>().ForMember((DynamicDataSetWithStudentName pb) => pb.Student, delegate(IMemberConfigurationExpression<DynamicDataSetWithStudentNameDTO> m)
			{
				m.MapFrom<PersonBase>((DynamicDataSetWithStudentNameDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			}).ForMember((DynamicDataSetWithStudentName pb) => pb.Data, delegate(IMemberConfigurationExpression<DynamicDataSetWithStudentNameDTO> m)
			{
				m.MapFrom<List<DynamicData>>((DynamicDataSetWithStudentNameDTO pbdto) => (pbdto.Data == null) ? null : pbdto.Data.ConvertAll<DynamicData>((DynamicDataDTO g) => g.ToDomainObject()));
			});
			Mapper.CreateMap<DynamicDataSetWithStudentName, DynamicDataSetWithStudentNameDTO>().ForMember((DynamicDataSetWithStudentNameDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<DynamicDataSetWithStudentName> m)
			{
				m.MapFrom<PersonBaseDTO>((DynamicDataSetWithStudentName pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			}).ForMember((DynamicDataSetWithStudentNameDTO pb) => pb.Data, delegate(IMemberConfigurationExpression<DynamicDataSetWithStudentName> m)
			{
				m.MapFrom<List<DynamicDataDTO>>((DynamicDataSetWithStudentName pbdto) => (pbdto.Data == null) ? null : pbdto.Data.ConvertAll<DynamicDataDTO>((DynamicData g) => g.ToDTO()));
			});
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00017490 File Offset: 0x00015690
		public static DynamicDataSetWithStudentName ToDomainObject(this DynamicDataSetWithStudentNameDTO dynamicDataDTO)
		{
			return Mapper.Map<DynamicDataSetWithStudentNameDTO, DynamicDataSetWithStudentName>(dynamicDataDTO);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000174A8 File Offset: 0x000156A8
		public static DynamicDataSetWithStudentNameDTO ToDTO(this DynamicDataSetWithStudentName dynamicData)
		{
			return Mapper.Map<DynamicDataSetWithStudentName, DynamicDataSetWithStudentNameDTO>(dynamicData);
		}
	}
}
