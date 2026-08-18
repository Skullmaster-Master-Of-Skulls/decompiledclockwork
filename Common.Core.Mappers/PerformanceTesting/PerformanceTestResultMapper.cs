using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting;
using TechnoPro.Common.Public.Entities.PerformanceTesting;

namespace TechnoPro.Common.Core.Mappers.PerformanceTesting
{
	// Token: 0x020000AD RID: 173
	public static class PerformanceTestResultMapper
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000F44C File Offset: 0x0000D64C
		static PerformanceTestResultMapper()
		{
			PerformanceTestTimeTakenMapper.CreateMap();
			Mapper.CreateMap<PerformanceTestResultDTO, PerformanceTestResult>();
			Mapper.CreateMap<PerformanceTestResult, PerformanceTestResultDTO>();
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000F464 File Offset: 0x0000D664
		public static PerformanceTestResult ToDomainObject(this PerformanceTestResultDTO dto)
		{
			return Mapper.Map<PerformanceTestResultDTO, PerformanceTestResult>(dto);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000F47C File Offset: 0x0000D67C
		public static PerformanceTestResultDTO ToDTO(this PerformanceTestResult item)
		{
			return Mapper.Map<PerformanceTestResult, PerformanceTestResultDTO>(item);
		}
	}
}
