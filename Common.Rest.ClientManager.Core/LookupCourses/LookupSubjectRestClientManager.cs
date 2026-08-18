using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.LookupCourses
{
	// Token: 0x02000037 RID: 55
	public class LookupSubjectRestClientManager : BearerTokenRestProxy<ILookupSubjectClientManager>, ILookupSubjectClientManager, IWebService
	{
		// Token: 0x06000208 RID: 520 RVA: 0x00006FEC File Offset: 0x000051EC
		public LookupSubjectRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006FF6 File Offset: 0x000051F6
		public LookupSubjectRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007001 File Offset: 0x00005201
		public int SaveSubject(LookupSubjectDTO subject)
		{
			return base.Post<LookupSubjectDTO, int>(subject, "lookupsubject");
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000700F File Offset: 0x0000520F
		public LookupSubjectDTO LoadLookupSubjectBySubjectCode(string SubjectCode)
		{
			return base.Get<LookupSubjectDTO>(string.Format("lookupsubject/subjectcode/{0}", SubjectCode), true);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007023 File Offset: 0x00005223
		public LookupSubjectDTO LoadLookupSubjectBySubjectDescription(string SubjectDescription)
		{
			return base.Get<LookupSubjectDTO>(string.Format("lookupsubject/subjectdescription/{0}", SubjectDescription), true);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007037 File Offset: 0x00005237
		public LookupSubjectDTO LoadLookupSubject(string SubjectCode, string SubjectDescription)
		{
			return base.Get<LookupSubjectDTO>(string.Format("lookupsubject/subjectcode/{0}/subjectdescription/{1}", SubjectCode, SubjectDescription), true);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000704C File Offset: 0x0000524C
		public IList<LookupSubjectDTO> LoadLookupSubjectsBySession(SessionDTO Session)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadLookupSubjectsBySessionReq>().Session = Session;
			return base.Post<SessionDTO, IList<LookupSubjectDTO>>(Session, "lookupsubject/lookupsubjectsbysession");
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000706A File Offset: 0x0000526A
		public IList<LookupSubjectDTO> LoadAllLookupSubjects()
		{
			return base.GetMany<LookupSubjectDTO>("lookupsubject", true);
		}
	}
}
