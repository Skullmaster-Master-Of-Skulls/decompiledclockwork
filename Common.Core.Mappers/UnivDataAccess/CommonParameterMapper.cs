using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess;
using TechnoPro.Common.Public.Entities.UnivDataAccess;

namespace TechnoPro.Common.Core.Mappers.UnivDataAccess
{
	// Token: 0x02000026 RID: 38
	public static class CommonParameterMapper
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00005776 File Offset: 0x00003976
		static CommonParameterMapper()
		{
			Mapper.CreateMap<CommonParameter, CommonParameterDTO>();
			Mapper.CreateMap<CommonParameterDTO, CommonParameter>();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005788 File Offset: 0x00003988
		public static CommonParameter ToDomainObject(this CommonParameterDTO dto)
		{
			return Mapper.Map<CommonParameterDTO, CommonParameter>(dto);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000057A0 File Offset: 0x000039A0
		public static CommonParameterDTO ToDTO(this CommonParameter item)
		{
			return Mapper.Map<CommonParameter, CommonParameterDTO>(item);
		}
	}
}
