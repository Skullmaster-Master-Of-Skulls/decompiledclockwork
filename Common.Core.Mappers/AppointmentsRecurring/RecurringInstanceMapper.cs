using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring;
using TechnoPro.Common.Public.Entities.AppointmentsRecurring;

namespace TechnoPro.Common.Core.Mappers.AppointmentsRecurring
{
	// Token: 0x020001F9 RID: 505
	public static class RecurringInstanceMapper
	{
		// Token: 0x06000889 RID: 2185 RVA: 0x00024922 File Offset: 0x00022B22
		static RecurringInstanceMapper()
		{
			Mapper.CreateMap<RecurringInstanceDTO, RecurringInstance>();
			Mapper.CreateMap<RecurringInstance, RecurringInstanceDTO>();
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00024934 File Offset: 0x00022B34
		public static RecurringInstance ToDomainObject(this RecurringInstanceDTO appointmentRecurringInfoDTO)
		{
			return Mapper.Map<RecurringInstanceDTO, RecurringInstance>(appointmentRecurringInfoDTO);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0002494C File Offset: 0x00022B4C
		public static RecurringInstanceDTO ToDTO(this RecurringInstance appointmentRecurringInfo)
		{
			return Mapper.Map<RecurringInstance, RecurringInstanceDTO>(appointmentRecurringInfo);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00024964 File Offset: 0x00022B64
		public static IList<RecurringInstance> ToDomainObject(this IList<RecurringInstanceDTO> list)
		{
			IList<RecurringInstance> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<RecurringInstance>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000249A8 File Offset: 0x00022BA8
		public static IList<RecurringInstanceDTO> ToDTO(this IList<RecurringInstance> list)
		{
			IList<RecurringInstanceDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<RecurringInstanceDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
