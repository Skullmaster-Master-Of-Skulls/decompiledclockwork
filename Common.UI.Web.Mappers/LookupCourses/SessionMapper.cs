using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;

namespace TechnoPro.Common.UI.Web.Mappers.LookupCourses
{
	// Token: 0x02000003 RID: 3
	public static class SessionMapper
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020CC File Offset: 0x000002CC
		public static SessionView ToView(this SessionDTO dto)
		{
			if (dto == null)
			{
				return null;
			}
			SessionView sessionView = new SessionView();
			sessionView.StartDate = dto.StartDate;
			sessionView.EndDate = dto.EndDate;
			AcademicTermView academicTerm;
			if (dto.AcademicTerm != null)
			{
				AcademicTermView academicTermView = new AcademicTermView();
				academicTermView.TermId = dto.AcademicTerm.TermId;
				academicTermView.Title = dto.AcademicTerm.Title;
				academicTermView.StartMonthDay = dto.AcademicTerm.StartMonthDay;
				academicTerm = academicTermView;
				academicTermView.EndMonthDay = dto.AcademicTerm.EndMonthDay;
			}
			else
			{
				academicTerm = null;
			}
			sessionView.AcademicTerm = academicTerm;
			return sessionView;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002158 File Offset: 0x00000358
		public static SessionDTO ToDTO(this SessionView view)
		{
			if (view == null)
			{
				return null;
			}
			SessionDTO sessionDTO = new SessionDTO();
			sessionDTO.StartDate = view.StartDate;
			sessionDTO.EndDate = view.EndDate;
			AcademicTermDTO academicTerm;
			if (view.AcademicTerm != null)
			{
				AcademicTermDTO academicTermDTO = new AcademicTermDTO();
				academicTermDTO.TermId = view.AcademicTerm.TermId;
				academicTermDTO.Title = view.AcademicTerm.Title;
				academicTermDTO.StartMonthDay = view.AcademicTerm.StartMonthDay;
				academicTerm = academicTermDTO;
				academicTermDTO.EndMonthDay = view.AcademicTerm.EndMonthDay;
			}
			else
			{
				academicTerm = null;
			}
			sessionDTO.AcademicTerm = academicTerm;
			return sessionDTO;
		}
	}
}
