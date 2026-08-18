using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring;
using TechnoPro.Common.Public.Entities.AppointmentsRecurring;

namespace TechnoPro.Common.Core.Mappers.AppointmentsRecurring
{
	// Token: 0x020001FA RID: 506
	public static class RecurringInstanceSetModifyBehaviourMapper
	{
		// Token: 0x0600088F RID: 2191 RVA: 0x000249EA File Offset: 0x00022BEA
		static RecurringInstanceSetModifyBehaviourMapper()
		{
			Mapper.CreateMap<RecurringInstanceSetModifyBehaviourDTO, RecurringInstanceSetModifyBehaviour>();
			Mapper.CreateMap<RecurringInstanceSetModifyBehaviour, RecurringInstanceSetModifyBehaviourDTO>();
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x000249FC File Offset: 0x00022BFC
		public static RecurringInstanceSetModifyBehaviour ToDomainObject(this RecurringInstanceSetModifyBehaviourDTO dto)
		{
			return Mapper.Map<RecurringInstanceSetModifyBehaviourDTO, RecurringInstanceSetModifyBehaviour>(dto);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00024A14 File Offset: 0x00022C14
		public static RecurringInstanceSetModifyBehaviourDTO ToDTO(this RecurringInstanceSetModifyBehaviour item)
		{
			return Mapper.Map<RecurringInstanceSetModifyBehaviour, RecurringInstanceSetModifyBehaviourDTO>(item);
		}
	}
}
