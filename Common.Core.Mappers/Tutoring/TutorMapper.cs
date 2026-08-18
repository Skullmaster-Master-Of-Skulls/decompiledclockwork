using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.Common.Core.Mappers.Tutoring
{
	// Token: 0x0200002C RID: 44
	public static class TutorMapper
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00005DF8 File Offset: 0x00003FF8
		static TutorMapper()
		{
			TutorBaseMapper.CreateMap();
			Mapper.CreateMap<TutorDTO, Tutor>().ForMember((Tutor pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<TutorDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<Tutor, TutorDTO>().ForMember((TutorDTO pb) => pb.Tag, delegate(IMemberConfigurationExpression<Tutor> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005EC8 File Offset: 0x000040C8
		public static Tutor ToDomainObject(this TutorDTO dto)
		{
			return Mapper.Map<TutorDTO, Tutor>(dto);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005EE0 File Offset: 0x000040E0
		public static TutorDTO ToDTO(this Tutor item)
		{
			return Mapper.Map<Tutor, TutorDTO>(item);
		}
	}
}
