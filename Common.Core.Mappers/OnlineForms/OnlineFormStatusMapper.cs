using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms;
using TechnoPro.Common.Public.Entities.OnlineForms;

namespace TechnoPro.Common.Core.Mappers.OnlineForms
{
	// Token: 0x020000B4 RID: 180
	public static class OnlineFormStatusMapper
	{
		// Token: 0x06000300 RID: 768 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		static OnlineFormStatusMapper()
		{
			Mapper.CreateMap<OnlineFormStatus, OnlineFormStatusDTO>();
			Mapper.CreateMap<OnlineFormStatusDTO, OnlineFormStatus>().ForMember((OnlineFormStatus pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<OnlineFormStatusDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		public static OnlineFormStatus ToDomainObject(this OnlineFormStatusDTO onlineFormStatusDTO)
		{
			return Mapper.Map<OnlineFormStatusDTO, OnlineFormStatus>(onlineFormStatusDTO);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000FD00 File Offset: 0x0000DF00
		public static OnlineFormStatusDTO ToDTO(this OnlineFormStatus onlineFormStatus)
		{
			return Mapper.Map<OnlineFormStatus, OnlineFormStatusDTO>(onlineFormStatus);
		}
	}
}
