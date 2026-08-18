using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.PerformanceTesting;

namespace TechnoPro.Common.Core.Mappers.PerformanceTesting
{
	// Token: 0x020000AF RID: 175
	public static class SearchForPersonPerformanceTestResultMapper
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0000F4D4 File Offset: 0x0000D6D4
		static SearchForPersonPerformanceTestResultMapper()
		{
			PerformanceTestResultMapper.CreateMap();
			UserGroupObjectMapper.CreateMap();
			Mapper.CreateMap<SearchForPersonPerformanceTestResultDTO, SearchForPersonPerformanceTestResult>();
			Mapper.CreateMap<SearchForPersonPerformanceTestResult, SearchForPersonPerformanceTestResultDTO>();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		public static SearchForPersonPerformanceTestResult ToDomainObject(this SearchForPersonPerformanceTestResultDTO dto)
		{
			return Mapper.Map<SearchForPersonPerformanceTestResultDTO, SearchForPersonPerformanceTestResult>(dto);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000F508 File Offset: 0x0000D708
		public static SearchForPersonPerformanceTestResultDTO ToDTO(this SearchForPersonPerformanceTestResult item)
		{
			return Mapper.Map<SearchForPersonPerformanceTestResult, SearchForPersonPerformanceTestResultDTO>(item);
		}
	}
}
