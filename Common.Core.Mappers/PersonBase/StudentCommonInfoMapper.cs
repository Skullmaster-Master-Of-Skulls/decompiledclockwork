using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.PersonBase
{
	// Token: 0x020000A8 RID: 168
	public static class StudentCommonInfoMapper
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0000F0D8 File Offset: 0x0000D2D8
		static StudentCommonInfoMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<StudentCommonInfoDTO, StudentCommonInfo>().ForMember((StudentCommonInfo pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<StudentCommonInfoDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<StudentCommonInfo, StudentCommonInfoDTO>();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000F15C File Offset: 0x0000D35C
		public static StudentCommonInfo ToDomainObject(this StudentCommonInfoDTO dto)
		{
			return Mapper.Map<StudentCommonInfoDTO, StudentCommonInfo>(dto);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000F174 File Offset: 0x0000D374
		public static StudentCommonInfoDTO ToDTO(this StudentCommonInfo item)
		{
			return Mapper.Map<StudentCommonInfo, StudentCommonInfoDTO>(item);
		}
	}
}
