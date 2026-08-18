using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001C2 RID: 450
	public static class ExamFileMapper
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x00021454 File Offset: 0x0001F654
		static ExamFileMapper()
		{
			BinaryFileMapper.CreateMap();
			Mapper.CreateMap<ExamFileDTO, ExamFile>().ForMember((ExamFile pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ExamFileDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ExamFile, ExamFileDTO>();
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x000214D8 File Offset: 0x0001F6D8
		public static ExamFile ToDomainObject(this ExamFileDTO classTestDTO)
		{
			return Mapper.Map<ExamFileDTO, ExamFile>(classTestDTO);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x000214F0 File Offset: 0x0001F6F0
		public static ExamFileDTO ToDTO(this ExamFile classTest)
		{
			return Mapper.Map<ExamFile, ExamFileDTO>(classTest);
		}
	}
}
