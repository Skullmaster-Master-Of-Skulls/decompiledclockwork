using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.Common.Public.Entities.OnlineForms;

namespace TechnoPro.Common.Core.Mappers.OnlineForms
{
	// Token: 0x020000B0 RID: 176
	public static class SurveyForDisplayMapper
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000F520 File Offset: 0x0000D720
		static SurveyForDisplayMapper()
		{
			Mapper.CreateMap<OnlineFormForDisplay, OnlineFormForDisplayDTO>();
			Mapper.CreateMap<OnlineFormForDisplayDTO, OnlineFormForDisplay>().ForMember((OnlineFormForDisplay pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<OnlineFormForDisplayDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000F59C File Offset: 0x0000D79C
		public static OnlineFormForDisplay ToDomainObject(this OnlineFormForDisplayDTO onlineFormForDisplayDTO)
		{
			return Mapper.Map<OnlineFormForDisplayDTO, OnlineFormForDisplay>(onlineFormForDisplayDTO);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000F5B4 File Offset: 0x0000D7B4
		public static OnlineFormForDisplayDTO ToDTO(this OnlineFormForDisplay onlineFormForDisplay)
		{
			return Mapper.Map<OnlineFormForDisplay, OnlineFormForDisplayDTO>(onlineFormForDisplay);
		}
	}
}
