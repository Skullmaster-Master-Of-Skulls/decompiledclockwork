using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.PersonBase
{
	// Token: 0x020000AA RID: 170
	public static class StudentWithCommonInfoMapper
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x0000F244 File Offset: 0x0000D444
		static StudentWithCommonInfoMapper()
		{
			PersonBaseMapper.CreateMap();
			StudentCommonInfoMapper.CreateMap();
			Mapper.CreateMap<StudentWithCommonInfoDTO, StudentWithCommonInfo>();
			Mapper.CreateMap<StudentWithCommonInfo, StudentWithCommonInfoDTO>();
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000F260 File Offset: 0x0000D460
		public static StudentWithCommonInfo ToDomainObject(this StudentWithCommonInfoDTO dto)
		{
			return Mapper.Map<StudentWithCommonInfoDTO, StudentWithCommonInfo>(dto);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000F278 File Offset: 0x0000D478
		public static StudentWithCommonInfoDTO ToDTO(this StudentWithCommonInfo item)
		{
			return Mapper.Map<StudentWithCommonInfo, StudentWithCommonInfoDTO>(item);
		}
	}
}
