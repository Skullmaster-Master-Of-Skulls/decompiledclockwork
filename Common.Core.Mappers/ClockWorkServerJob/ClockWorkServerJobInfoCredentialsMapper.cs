using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;

namespace TechnoPro.Common.Core.Mappers.ClockWorkServerJob
{
	// Token: 0x0200016B RID: 363
	public static class ClockWorkServerJobInfoCredentialsMapper
	{
		// Token: 0x06000641 RID: 1601 RVA: 0x0001CA46 File Offset: 0x0001AC46
		static ClockWorkServerJobInfoCredentialsMapper()
		{
			Mapper.CreateMap<ClockWorkServerJobInfo.Credentials, ClockWorkServerJobInfoDTO.CredentialsDTO>();
			Mapper.CreateMap<ClockWorkServerJobInfoDTO.CredentialsDTO, ClockWorkServerJobInfo.Credentials>();
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001CA58 File Offset: 0x0001AC58
		public static ClockWorkServerJobInfo.Credentials ToDomainObject(this ClockWorkServerJobInfoDTO.CredentialsDTO dto)
		{
			return Mapper.Map<ClockWorkServerJobInfoDTO.CredentialsDTO, ClockWorkServerJobInfo.Credentials>(dto);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001CA70 File Offset: 0x0001AC70
		public static ClockWorkServerJobInfoDTO.CredentialsDTO ToDTO(this ClockWorkServerJobInfo.Credentials bo)
		{
			return Mapper.Map<ClockWorkServerJobInfo.Credentials, ClockWorkServerJobInfoDTO.CredentialsDTO>(bo);
		}
	}
}
