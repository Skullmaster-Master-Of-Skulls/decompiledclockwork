using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000065 RID: 101
	public class SessionServiceManager : ISession, IService
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x00011454 File Offset: 0x0000F654
		public AddSessionResp AddSession(AddSessionReq request)
		{
			ISessionManager sessionManager = new SessionManager(request.GetOperationContext());
			Session session = sessionManager.AddSession(request.Session.ToDomainObject(), request.Count);
			return new AddSessionResp
			{
				Session = session.ToDTO()
			};
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001149C File Offset: 0x0000F69C
		public SubtractSessionResp SubtractSession(SubtractSessionReq request)
		{
			ISessionManager sessionManager = new SessionManager(request.GetOperationContext());
			Session session = sessionManager.SubtractSession(request.Session.ToDomainObject(), request.Count);
			return new SubtractSessionResp
			{
				Session = session.ToDTO()
			};
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000114E4 File Offset: 0x0000F6E4
		public GoToTodaysSessionResp GoToTodaysSession(GoToTodaysSessionReq request)
		{
			ISessionManager sessionManager = new SessionManager(request.GetOperationContext());
			Session session = sessionManager.GoToTodaysSession(request.Session.ToDomainObject());
			return new GoToTodaysSessionResp
			{
				Session = session.ToDTO()
			};
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00011528 File Offset: 0x0000F728
		public GotoSessionResp GotoSession(GotoSessionReq request)
		{
			ISessionManager sessionManager = new SessionManager(request.GetOperationContext());
			Session session = sessionManager.GotoSession(request.Session.ToDomainObject(), request.AcademicTerm.ToDomainObject(), request.Year);
			return new GotoSessionResp
			{
				Session = session.ToDTO()
			};
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001157C File Offset: 0x0000F77C
		public GetCurrentAcademicTermResp GetCurrentAcademicTerm(GetCurrentAcademicTermReq request)
		{
			IAcademicTermManager academicTermManager = new AcademicTermManager(request.GetOperationContext());
			AcademicTerm currentAcademicTerm = academicTermManager.GetCurrentAcademicTerm();
			return new GetCurrentAcademicTermResp
			{
				AcademicTerm = currentAcademicTerm.ToDTO()
			};
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000115B4 File Offset: 0x0000F7B4
		public CopySessionResp CopySession(CopySessionReq request)
		{
			ISessionManager sessionManager = new SessionManager(request.GetOperationContext());
			Session session = sessionManager.CopySession(request.Session.ToDomainObject());
			return new CopySessionResp
			{
				Session = session.ToDTO()
			};
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000115F8 File Offset: 0x0000F7F8
		public LoadAcademicTermsResp LoadAcademicTerms(LoadAcademicTermsReq request)
		{
			IAcademicTermManager academicTermManager = new AcademicTermManager(request.GetOperationContext());
			IList<AcademicTerm> source = academicTermManager.LoadAcademicTerms(request.IgnoreCache);
			LoadAcademicTermsResp loadAcademicTermsResp = new LoadAcademicTermsResp();
			loadAcademicTermsResp.AcademicTerms = source.ToList<AcademicTerm>().ConvertAll<AcademicTermDTO>((AcademicTerm f) => f.ToDTO());
			return loadAcademicTermsResp;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001165C File Offset: 0x0000F85C
		public GetCurrentSessionResp GetCurrentSession(GetCurrentSessionReq request)
		{
			ISessionManager sessionManager = new SessionManager(request.GetOperationContext());
			Session currentSession = sessionManager.GetCurrentSession();
			return new GetCurrentSessionResp
			{
				Session = currentSession.ToDTO()
			};
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00011694 File Offset: 0x0000F894
		public GetSessionByDateResp GetSessionByDate(GetSessionByDateReq request)
		{
			ISessionManager sessionManager = new SessionManager(request.GetOperationContext());
			Session session = sessionManager.GetSession(request.Date);
			return new GetSessionByDateResp
			{
				Session = session.ToDTO()
			};
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000116D4 File Offset: 0x0000F8D4
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000116E8 File Offset: 0x0000F8E8
		public void SetSessionChooserDefaultValue(SetSessionChooserDefaultValueReq Request)
		{
			ISessionManager sessionManager = new SessionManager(Request.GetOperationContext());
			sessionManager.SetSessionChooserDefaultValue(Request.DtpNowAdjusted);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00011710 File Offset: 0x0000F910
		public GetSessionChooserDefaultValueResp GetSessionChooserDefaultValue()
		{
			ISessionManager sessionManager = new SessionManager();
			return new GetSessionChooserDefaultValueResp
			{
				DtpNowAdjusted = sessionManager.GetSessionChooserDefaultValue()
			};
		}
	}
}
