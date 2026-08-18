using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.LookupCourses
{
	// Token: 0x020000D5 RID: 213
	public static class AlternateContactMapper
	{
		// Token: 0x06000388 RID: 904 RVA: 0x0001172C File Offset: 0x0000F92C
		static AlternateContactMapper()
		{
			Mapper.CreateMap<AlternateContactDTO, AlternateContact>().ForMember((AlternateContact pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<AlternateContactDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<AlternateContact, AlternateContactDTO>();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000117A8 File Offset: 0x0000F9A8
		public static AlternateContact ToDomainObject(this AlternateContactDTO alternateContactDTO)
		{
			return Mapper.Map<AlternateContactDTO, AlternateContact>(alternateContactDTO);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000117C0 File Offset: 0x0000F9C0
		public static AlternateContactDTO ToDTO(this AlternateContact alternateContact)
		{
			return Mapper.Map<AlternateContact, AlternateContactDTO>(alternateContact);
		}
	}
}
