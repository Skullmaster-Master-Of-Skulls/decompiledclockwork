using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.Common.Core.Mappers.Tutoring
{
	// Token: 0x02000029 RID: 41
	public static class MyTutorMapper
	{
		// Token: 0x060000AE RID: 174 RVA: 0x000059B0 File Offset: 0x00003BB0
		static MyTutorMapper()
		{
			TutorMapper.CreateMap();
			Mapper.CreateMap<MyTutorDTO, MyTutor>().ForMember((MyTutor pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<MyTutorDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<MyTutor, MyTutorDTO>();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005A34 File Offset: 0x00003C34
		public static MyTutor ToDomainObject(this MyTutorDTO dto)
		{
			return Mapper.Map<MyTutorDTO, MyTutor>(dto);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005A4C File Offset: 0x00003C4C
		public static MyTutorDTO ToDTO(this MyTutor item)
		{
			return Mapper.Map<MyTutor, MyTutorDTO>(item);
		}
	}
}
