using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews;
using TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.FinalExams;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestExamViews
{
	// Token: 0x020001B6 RID: 438
	public static class FinalExamsContextMapper
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x000208FC File Offset: 0x0001EAFC
		static FinalExamsContextMapper()
		{
			Mapper.CreateMap<FinalExamsContextDTO, FinalExamsContext>();
			Mapper.CreateMap<FinalExamsContext, FinalExamsContextDTO>();
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0002090C File Offset: 0x0001EB0C
		public static FinalExamsContext ToDomainObject(this FinalExamsContextDTO appointmentWorkshopInfoDTO)
		{
			return Mapper.Map<FinalExamsContextDTO, FinalExamsContext>(appointmentWorkshopInfoDTO);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00020924 File Offset: 0x0001EB24
		public static FinalExamsContextDTO ToDTO(this FinalExamsContext appointmentWorkshopInfo)
		{
			return Mapper.Map<FinalExamsContext, FinalExamsContextDTO>(appointmentWorkshopInfo);
		}
	}
}
