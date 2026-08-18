using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Data;
using TechnoPro.Common.Public.Entities.Data;

namespace TechnoPro.Common.Core.Mappers.Data
{
	// Token: 0x02000136 RID: 310
	public static class ReassignStaffDropListResultMapper
	{
		// Token: 0x0600054D RID: 1357 RVA: 0x00019860 File Offset: 0x00017A60
		static ReassignStaffDropListResultMapper()
		{
			Mapper.CreateMap<ReassignStaffDropListResult, ReassignStaffDropListResultDTO>();
			Mapper.CreateMap<ReassignStaffDropListResultDTO, ReassignStaffDropListResult>();
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00019870 File Offset: 0x00017A70
		public static ReassignStaffDropListResult ToDomainObject(this ReassignStaffDropListResultDTO groupDTO)
		{
			return Mapper.Map<ReassignStaffDropListResultDTO, ReassignStaffDropListResult>(groupDTO);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00019888 File Offset: 0x00017A88
		public static ReassignStaffDropListResultDTO ToDTO(this ReassignStaffDropListResult group)
		{
			return Mapper.Map<ReassignStaffDropListResult, ReassignStaffDropListResultDTO>(group);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000198A0 File Offset: 0x00017AA0
		public static IList<ReassignStaffDropListResult> ToDomainObject(this IList<ReassignStaffDropListResultDTO> dtos)
		{
			IList<ReassignStaffDropListResult> result;
			if (dtos == null)
			{
				result = null;
			}
			else
			{
				result = (from g in dtos
				select g.ToDomainObject()).ToList<ReassignStaffDropListResult>();
			}
			return result;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000198E4 File Offset: 0x00017AE4
		public static IList<ReassignStaffDropListResultDTO> ToDTO(this IList<ReassignStaffDropListResult> items)
		{
			IList<ReassignStaffDropListResultDTO> result;
			if (items == null)
			{
				result = null;
			}
			else
			{
				result = (from g in items
				select g.ToDTO()).ToList<ReassignStaffDropListResultDTO>();
			}
			return result;
		}
	}
}
