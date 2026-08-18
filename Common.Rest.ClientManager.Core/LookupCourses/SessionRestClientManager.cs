using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.LookupCourses
{
	// Token: 0x02000039 RID: 57
	public class SessionRestClientManager : BearerTokenRestProxy<ISessionClientManager>, ISessionClientManager, IWebService
	{
		// Token: 0x06000214 RID: 532 RVA: 0x000070DA File Offset: 0x000052DA
		public SessionRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000070E4 File Offset: 0x000052E4
		public SessionRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000070EF File Offset: 0x000052EF
		public AcademicTermDTO GetCurrentAcademicTerm()
		{
			return base.Get<AcademicTermDTO>("session/currentacademicterm", true);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000070FD File Offset: 0x000052FD
		public SessionDTO GetCurrentSession()
		{
			return base.Get<SessionDTO>("session/currentsession", true);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000710C File Offset: 0x0000530C
		public SessionDTO AddSession(SessionDTO session, int count)
		{
			AddSessionReq addSessionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AddSessionReq>();
			addSessionReq.Session = session;
			addSessionReq.Count = count;
			return base.Post<AddSessionReq, SessionDTO>(addSessionReq, "session/add");
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007140 File Offset: 0x00005340
		public SessionDTO SubtractSession(SessionDTO session, int count)
		{
			SubtractSessionReq subtractSessionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SubtractSessionReq>();
			subtractSessionReq.Session = session;
			subtractSessionReq.Count = count;
			return base.Post<SubtractSessionReq, SessionDTO>(subtractSessionReq, "session/subtract");
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007172 File Offset: 0x00005372
		public SessionDTO GetSessionByDate(DateTime Date)
		{
			return base.Get<SessionDTO>(string.Format("session/date/{0}", Date), true);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000718B File Offset: 0x0000538B
		public SessionDTO GoToTodaysSession(SessionDTO session)
		{
			return base.Get<SessionDTO>("session/gototodayssession", true);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000719C File Offset: 0x0000539C
		public void SetSessionChooserDefaultValue(DateTime DtpNow)
		{
			SetSessionChooserDefaultValueReq setSessionChooserDefaultValueReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetSessionChooserDefaultValueReq>();
			setSessionChooserDefaultValueReq.DtpNowAdjusted = DtpNow;
			base.Post<SetSessionChooserDefaultValueReq>(setSessionChooserDefaultValueReq, "session/setsessionchooserdefaultvalue");
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000071C7 File Offset: 0x000053C7
		public DateTime? GetSessionChooserDefaultValue()
		{
			return base.Get<DateTime?>("session/sessionchooserdefaultvalue", true);
		}
	}
}
