using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess;
using TechnoPro.Common.Public.Entities.UnivDataAccess;

namespace TechnoPro.Common.Core.Mappers.UnivDataAccess
{
	// Token: 0x02000027 RID: 39
	public static class QueryRequestMapper
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x000057B8 File Offset: 0x000039B8
		static QueryRequestMapper()
		{
			CommonParameterMapper.CreateMap();
			Mapper.CreateMap<QueryRequestDTO, QueryRequest>();
			Mapper.CreateMap<QueryRequest, QueryRequestDTO>();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000057D0 File Offset: 0x000039D0
		public static QueryRequest ToDomainObject(this QueryRequestDTO queryRequestDTO)
		{
			return Mapper.Map<QueryRequestDTO, QueryRequest>(queryRequestDTO);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000057E8 File Offset: 0x000039E8
		public static QueryRequestDTO ToDTO(this QueryRequest queryRequest)
		{
			return Mapper.Map<QueryRequest, QueryRequestDTO>(queryRequest);
		}
	}
}
