using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.PersonBase
{
	// Token: 0x020000AB RID: 171
	public static class UserGroupObjectIdMapper
	{
		// Token: 0x060002DC RID: 732 RVA: 0x0000F290 File Offset: 0x0000D490
		static UserGroupObjectIdMapper()
		{
			Mapper.CreateMap<UserGroupObjectId, UserGroupObjectIdDTO>();
			Mapper.CreateMap<UserGroupObjectIdDTO, UserGroupObjectId>().ForMember((UserGroupObjectId pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<UserGroupObjectIdDTO> m)
			{
				m.Ignore();
			}).ForMember((UserGroupObjectId pb) => (object)pb.SecondId, delegate(IMemberConfigurationExpression<UserGroupObjectIdDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000F370 File Offset: 0x0000D570
		public static UserGroupObjectIdDTO ToDTO(this UserGroupObjectId item)
		{
			return Mapper.Map<UserGroupObjectId, UserGroupObjectIdDTO>(item);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000F388 File Offset: 0x0000D588
		public static UserGroupObjectId ToDomainObject(this UserGroupObjectIdDTO dto)
		{
			return Mapper.Map<UserGroupObjectIdDTO, UserGroupObjectId>(dto);
		}
	}
}
