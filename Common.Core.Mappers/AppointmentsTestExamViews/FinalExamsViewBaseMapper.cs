using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews;
using TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.FinalExams;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestExamViews
{
	// Token: 0x020001B7 RID: 439
	public static class FinalExamsViewBaseMapper
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x0002093C File Offset: 0x0001EB3C
		static FinalExamsViewBaseMapper()
		{
			Mapper.CreateMap<FinalExamsViewBaseDTO, FinalExamsViewBase>();
			Mapper.CreateMap<FinalExamsViewBase, FinalExamsViewBaseDTO>();
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0002094C File Offset: 0x0001EB4C
		public static FinalExamsViewBase ToDomainObject(this FinalExamsViewBaseDTO appointmentWorkshopInfoDTO)
		{
			return Mapper.Map<FinalExamsViewBaseDTO, FinalExamsViewBase>(appointmentWorkshopInfoDTO);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00020964 File Offset: 0x0001EB64
		public static FinalExamsViewBaseDTO ToDTO(this FinalExamsViewBase appointmentWorkshopInfo)
		{
			return Mapper.Map<FinalExamsViewBase, FinalExamsViewBaseDTO>(appointmentWorkshopInfo);
		}
	}
}
