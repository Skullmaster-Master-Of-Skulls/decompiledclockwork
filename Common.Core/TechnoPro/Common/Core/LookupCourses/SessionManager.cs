using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.LookupCourses
{
	// Token: 0x020000D6 RID: 214
	public class SessionManager : ISessionManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00037D88 File Offset: 0x00035F88
		private SessionDAO sessionDAO
		{
			get
			{
				SessionDAO result;
				if ((result = this.sd) == null)
				{
					result = (this.sd = new SessionDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0000672B File Offset: 0x0000492B
		public SessionManager()
		{
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00037DB3 File Offset: 0x00035FB3
		public SessionManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00037DC5 File Offset: 0x00035FC5
		// (set) Token: 0x06000834 RID: 2100 RVA: 0x00037DCD File Offset: 0x00035FCD
		public OperationContext OpContext { get; set; }

		// Token: 0x06000835 RID: 2101 RVA: 0x00037DD8 File Offset: 0x00035FD8
		public Session GetCurrentSession()
		{
			Session session = new Session();
			this.GoToTodaysSession(session);
			return session;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00037DFC File Offset: 0x00035FFC
		public Session AddSession(Session session, int count)
		{
			IAcademicTermManager academicTermManager = new AcademicTermManager(this.OpContext);
			IList<AcademicTerm> list = academicTermManager.LoadAcademicTerms(false);
			int count2 = list.Count;
			AcademicTerm academicTerm = list.FirstOrDefault((AcademicTerm g) => g.TermId == session.AcademicTerm.TermId);
			int num = (academicTerm == null) ? -1 : list.IndexOf(academicTerm);
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

		// Token: 0x06000837 RID: 2103 RVA: 0x00037EC8 File Offset: 0x000360C8
		public Session GotoSession(Session session, AcademicTerm term, int year)
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

		// Token: 0x06000838 RID: 2104 RVA: 0x00037F64 File Offset: 0x00036164
		public Session SubtractSession(Session session, int count)
		{
			IAcademicTermManager academicTermManager = new AcademicTermManager(this.OpContext);
			IList<AcademicTerm> list = academicTermManager.LoadAcademicTerms(false);
			int count2 = list.Count;
			AcademicTerm academicTerm = list.FirstOrDefault((AcademicTerm g) => g.TermId == session.AcademicTerm.TermId);
			int num = (academicTerm == null) ? -1 : list.IndexOf(academicTerm);
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

		// Token: 0x06000839 RID: 2105 RVA: 0x00038030 File Offset: 0x00036230
		public Session GoToTodaysSession(Session session)
		{
			this.GotoSession(session, this.GetCurrentAcademicTerm(), DateTime.Now.Year);
			return session;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00038060 File Offset: 0x00036260
		public AcademicTerm GetCurrentAcademicTerm()
		{
			return this.GetAcademicTerm(DateTime.Now);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00038080 File Offset: 0x00036280
		public AcademicTerm GetAcademicTerm(DateTime date)
		{
			IAcademicTermManager academicTermManager = new AcademicTermManager(this.OpContext);
			return academicTermManager.GetAcademicTerm(date);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000380A8 File Offset: 0x000362A8
		public IList<AcademicTerm> LoadAcademicTerms()
		{
			IAcademicTermManager academicTermManager = new AcademicTermManager(this.OpContext);
			return academicTermManager.LoadAcademicTerms(false);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000380D0 File Offset: 0x000362D0
		public Session CopySession(Session session)
		{
			return new Session
			{
				AcademicTerm = session.AcademicTerm,
				StartDate = session.StartDate,
				EndDate = session.EndDate
			};
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00038110 File Offset: 0x00036310
		public Session GetSession(DateTime Date)
		{
			AcademicTerm academicTerm = this.GetAcademicTerm(Date);
			Session session = new Session();
			this.GotoSession(session, academicTerm, Date.Year);
			return session;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00038141 File Offset: 0x00036341
		public void SetSessionChooserDefaultValue(DateTime DtpNow)
		{
			this.sessionDAO.SetSessionChooserDefaultValue(DtpNow);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00038154 File Offset: 0x00036354
		public DateTime? GetSessionChooserDefaultValue()
		{
			return this.sessionDAO.GetSessionChooserDefaultValue();
		}

		// Token: 0x0400017E RID: 382
		private SessionDAO sd;
	}
}
