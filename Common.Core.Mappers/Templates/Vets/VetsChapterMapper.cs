using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Vets;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.Common.Core.Mappers.Templates.Vets
{
	// Token: 0x02000044 RID: 68
	public static class VetsChapterMapper
	{
		// Token: 0x06000118 RID: 280 RVA: 0x000089E4 File Offset: 0x00006BE4
		static VetsChapterMapper()
		{
			Mapper.CreateMap<VetsChapter, VetsChapterDTO>();
			Mapper.CreateMap<VetsChapterDTO, VetsChapter>().ForMember((VetsChapter pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<VetsChapterDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00008A60 File Offset: 0x00006C60
		public static VetsChapter ToDomainObject(this VetsChapterDTO surveyDTO)
		{
			return Mapper.Map<VetsChapterDTO, VetsChapter>(surveyDTO);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00008A78 File Offset: 0x00006C78
		public static VetsChapterDTO ToDTO(this VetsChapter survey)
		{
			return Mapper.Map<VetsChapter, VetsChapterDTO>(survey);
		}
	}
}
