using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;

namespace TechnoPro.Common.UI.Web.Mappers.LookupCourses
{
	// Token: 0x02000002 RID: 2
	public static class AcademicTermMapper
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002052 File Offset: 0x00000252
		public static AcademicTermView ToView(this AcademicTermDTO dto)
		{
			if (dto == null)
			{
				return null;
			}
			return new AcademicTermView
			{
				TermId = dto.TermId,
				Title = dto.Title,
				StartMonthDay = dto.StartMonthDay,
				EndMonthDay = dto.EndMonthDay
			};
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000208E File Offset: 0x0000028E
		public static AcademicTermDTO ToDTO(this AcademicTermView view)
		{
			if (view == null)
			{
				return null;
			}
			return new AcademicTermDTO
			{
				TermId = view.TermId,
				Title = view.Title,
				StartMonthDay = view.StartMonthDay,
				EndMonthDay = view.EndMonthDay
			};
		}
	}
}
