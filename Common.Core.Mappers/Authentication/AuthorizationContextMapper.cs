using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;

namespace TechnoPro.Common.Core.Mappers.Authentication
{
	// Token: 0x0200018C RID: 396
	public static class AuthorizationContextMapper
	{
		// Token: 0x060006C5 RID: 1733 RVA: 0x0001E790 File Offset: 0x0001C990
		static AuthorizationContextMapper()
		{
			AuthorizationContextItemMapper.CreateMap();
			Mapper.CreateMap<AuthorizationContextDTO, AuthorizationContext>().ForMember((AuthorizationContext pb) => pb.ContextItems, delegate(IMemberConfigurationExpression<AuthorizationContextDTO> m)
			{
				m.MapFrom<List<AuthorizationContextItem>>((AuthorizationContextDTO pbdto) => (pbdto.ContextItems == null) ? null : pbdto.ContextItems.ToList<AuthorizationContextItemDTO>().ConvertAll<AuthorizationContextItem>((AuthorizationContextItemDTO g) => g.ToDomainObject()));
			});
			Mapper.CreateMap<AuthorizationContext, AuthorizationContextDTO>().ForMember((AuthorizationContextDTO pb) => pb.ContextItems, delegate(IMemberConfigurationExpression<AuthorizationContext> m)
			{
				m.MapFrom<List<AuthorizationContextItemDTO>>((AuthorizationContext pbdto) => (pbdto.ContextItems == null) ? null : pbdto.ContextItems.ToList<AuthorizationContextItem>().ConvertAll<AuthorizationContextItemDTO>((AuthorizationContextItem g) => g.ToDTO()));
			});
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001E84C File Offset: 0x0001CA4C
		public static AuthorizationContext ToDomainObject(this AuthorizationContextDTO dto)
		{
			return Mapper.Map<AuthorizationContextDTO, AuthorizationContext>(dto);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001E864 File Offset: 0x0001CA64
		public static AuthorizationContextDTO ToDTO(this AuthorizationContext item)
		{
			return Mapper.Map<AuthorizationContext, AuthorizationContextDTO>(item);
		}
	}
}
