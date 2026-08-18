using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Cases;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.Cases;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.Cases
{
	// Token: 0x02000178 RID: 376
	public static class CaseMapper
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x0001D760 File Offset: 0x0001B960
		static CaseMapper()
		{
			CaseBaseMapper.CreateMap();
			CaseClientMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<CaseDTO, Case>().ForMember((Case pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<CaseDTO> m)
			{
				m.Ignore();
			}).ForMember((Case pb) => pb.Clients, delegate(IMemberConfigurationExpression<CaseDTO> m)
			{
				m.MapFrom<List<CaseClient>>((CaseDTO pbdto) => (pbdto.Clients == null) ? null : (from g in pbdto.Clients
				select g.ToDomainObject()).ToList<CaseClient>());
			}).ForMember((Case pb) => pb.WhoEntered, delegate(IMemberConfigurationExpression<CaseDTO> m)
			{
				m.MapFrom<PersonBase>((CaseDTO pbdto) => (pbdto.WhoEntered == null) ? null : pbdto.WhoEntered.ToDomainObject());
			});
			Mapper.CreateMap<Case, CaseDTO>().ForMember((CaseDTO pb) => pb.Clients, delegate(IMemberConfigurationExpression<Case> m)
			{
				m.MapFrom<List<CaseClientDTO>>((Case pbdto) => (pbdto.Clients == null) ? null : (from g in pbdto.Clients
				select g.ToDTO()).ToList<CaseClientDTO>());
			}).ForMember((CaseDTO pb) => pb.WhoEntered, delegate(IMemberConfigurationExpression<Case> m)
			{
				m.MapFrom<PersonBaseDTO>((Case pbdto) => (pbdto.WhoEntered == null) ? null : pbdto.WhoEntered.ToDTO());
			});
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001D928 File Offset: 0x0001BB28
		public static Case ToDomainObject(this CaseDTO lookupCourseDTO)
		{
			return Mapper.Map<CaseDTO, Case>(lookupCourseDTO);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001D940 File Offset: 0x0001BB40
		public static CaseDTO ToDTO(this Case lookupCourse)
		{
			return Mapper.Map<Case, CaseDTO>(lookupCourse);
		}
	}
}
