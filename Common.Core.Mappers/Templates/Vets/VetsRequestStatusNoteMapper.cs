using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.Core.Mappers.Templates.Vets
{
	// Token: 0x02000046 RID: 70
	public static class VetsRequestStatusNoteMapper
	{
		// Token: 0x06000120 RID: 288 RVA: 0x00008B7C File Offset: 0x00006D7C
		static VetsRequestStatusNoteMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<VetsRequestStatusNote, VetsRequestStatusNoteDTO>().ForMember((VetsRequestStatusNoteDTO pb) => pb.WhoEntered, delegate(IMemberConfigurationExpression<VetsRequestStatusNote> m)
			{
				m.MapFrom<PersonBaseDTO>((VetsRequestStatusNote pbdto) => (pbdto.WhoEntered == null) ? null : pbdto.WhoEntered.ToDTO());
			});
			Mapper.CreateMap<VetsRequestStatusNoteDTO, VetsRequestStatusNote>().ForMember((VetsRequestStatusNote pb) => pb.WhoEntered, delegate(IMemberConfigurationExpression<VetsRequestStatusNoteDTO> m)
			{
				m.MapFrom<PersonBase>((VetsRequestStatusNoteDTO pbdto) => (pbdto.WhoEntered == null) ? null : pbdto.WhoEntered.ToDomainObject());
			});
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008C38 File Offset: 0x00006E38
		public static VetsRequestStatusNote ToDomainObject(this VetsRequestStatusNoteDTO surveyDTO)
		{
			return Mapper.Map<VetsRequestStatusNoteDTO, VetsRequestStatusNote>(surveyDTO);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00008C50 File Offset: 0x00006E50
		public static VetsRequestStatusNoteDTO ToDTO(this VetsRequestStatusNote survey)
		{
			return Mapper.Map<VetsRequestStatusNote, VetsRequestStatusNoteDTO>(survey);
		}
	}
}
