using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.Public.Entities.ConfidentialityAgreement;

namespace TechnoPro.Common.Core.Mappers.ConfidentialityAgreement
{
	// Token: 0x02000166 RID: 358
	public static class StudentConfidentialityAgreementMapper
	{
		// Token: 0x06000629 RID: 1577 RVA: 0x0001C47C File Offset: 0x0001A67C
		static StudentConfidentialityAgreementMapper()
		{
			Mapper.CreateMap<StudentConfidentialityAgreement, StudentConfidentialityAgreementDTO>();
			Mapper.CreateMap<StudentConfidentialityAgreementDTO, StudentConfidentialityAgreement>().ForMember((StudentConfidentialityAgreement bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<StudentConfidentialityAgreementDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001C4F8 File Offset: 0x0001A6F8
		public static StudentConfidentialityAgreement ToDomainObject(this StudentConfidentialityAgreementDTO dto)
		{
			return Mapper.Map<StudentConfidentialityAgreementDTO, StudentConfidentialityAgreement>(dto);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001C510 File Offset: 0x0001A710
		public static StudentConfidentialityAgreementDTO ToDTO(this StudentConfidentialityAgreement bo)
		{
			return Mapper.Map<StudentConfidentialityAgreement, StudentConfidentialityAgreementDTO>(bo);
		}
	}
}
