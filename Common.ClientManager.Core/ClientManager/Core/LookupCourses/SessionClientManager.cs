using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.LookupCourses
{
	// Token: 0x02000044 RID: 68
	public class SessionClientManager : ISessionClientManager, IWebService
	{
		// Token: 0x0600027B RID: 635 RVA: 0x0000B938 File Offset: 0x00009B38
		private SessionDTO GotoSession(SessionDTO session, AcademicTermDTO term, int year)
		{
			session.AcademicTerm = term;
			session.StartDate = new DateTime(year, term.StartMonthDay.Month, term.StartMonthDay.Day);
			session.EndDate = new DateTime(year, term.EndMonthDay.Month, term.EndMonthDay.Day);
			bool flag = term.EndMonthDay < term.StartMonthDay;
			if (flag)
			{
				session.EndDate = session.EndDate.AddYears(1);
			}
			return session;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		public SessionDTO SubtractSession(SessionDTO session, int count)
		{
			IAcademicTermClientManager academicTermClientManager = new AcademicTermClientManager();
			IList<AcademicTermDTO> list = academicTermClientManager.LoadAcademicTerms(false);
			int count2 = list.Count;
			AcademicTermDTO academicTermDTO = list.FirstOrDefault((AcademicTermDTO g) => session.AcademicTerm != null && g.TermId == session.AcademicTerm.TermId);
			int num = (academicTermDTO == null) ? -1 : list.IndexOf(academicTermDTO);
			int num2 = session.StartDate.Year;
			int i = 0;
			while (i < count)
			{
				num--;
				i++;
				bool flag = num < 0;
				if (flag)
				{
					num = count2 - 1;
					num2--;
				}
			}
			this.GotoSession(session, list[num], num2);
			return session;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000BA94 File Offset: 0x00009C94
		public SessionDTO GoToTodaysSession(SessionDTO session)
		{
			IAcademicTermClientManager academicTermClientManager = new AcademicTermClientManager();
			this.GotoSession(session, academicTermClientManager.GetCurrentAcademicTerm(), DateTime.Now.Year);
			return session;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000BAC8 File Offset: 0x00009CC8
		public AcademicTermDTO GetCurrentAcademicTerm()
		{
			IAcademicTermClientManager academicTermClientManager = new AcademicTermClientManager();
			return academicTermClientManager.GetCurrentAcademicTerm();
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000BAE8 File Offset: 0x00009CE8
		public SessionDTO AddSession(SessionDTO session, int count)
		{
			IAcademicTermClientManager academicTermClientManager = new AcademicTermClientManager();
			IList<AcademicTermDTO> list = academicTermClientManager.LoadAcademicTerms(false);
			int count2 = list.Count;
			AcademicTermDTO academicTermDTO = list.FirstOrDefault((AcademicTermDTO g) => g.TermId == session.AcademicTerm.TermId);
			int num = (academicTermDTO == null) ? -1 : list.IndexOf(academicTermDTO);
			int num2 = session.StartDate.Year;
			int i = 0;
			while (i < count)
			{
				num++;
				i++;
				bool flag = num >= count2;
				if (flag)
				{
					num = 0;
					num2++;
				}
			}
			this.GotoSession(session, list[num], num2);
			return session;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		public SessionDTO GetCurrentSession()
		{
			GetCurrentSessionReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCurrentSessionReq>();
			return ClientServiceFactory.GetClientInstance<ISession>().GetCurrentSession(request).Session;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		public SessionDTO GetSessionByDate(DateTime Date)
		{
			GetSessionByDateReq getSessionByDateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetSessionByDateReq>();
			getSessionByDateReq.Date = Date;
			return ClientServiceFactory.GetClientInstance<ISession>().GetSessionByDate(getSessionByDateReq).Session;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000BC18 File Offset: 0x00009E18
		public void SetSessionChooserDefaultValue(DateTime DtpNow)
		{
			SetSessionChooserDefaultValueReq setSessionChooserDefaultValueReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetSessionChooserDefaultValueReq>();
			setSessionChooserDefaultValueReq.DtpNowAdjusted = DtpNow;
			ClientServiceFactory.GetClientInstance<ISession>().SetSessionChooserDefaultValue(setSessionChooserDefaultValueReq);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000BC48 File Offset: 0x00009E48
		public DateTime? GetSessionChooserDefaultValue()
		{
			return ClientServiceFactory.GetClientInstance<ISession>().GetSessionChooserDefaultValue().DtpNowAdjusted;
		}
	}
}
