using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.LookupCourses
{
	// Token: 0x02000042 RID: 66
	public class LookupSubjectClientManager : ILookupSubjectClientManager, IWebService
	{
		// Token: 0x06000271 RID: 625 RVA: 0x0000B778 File Offset: 0x00009978
		public int SaveSubject(LookupSubjectDTO subject)
		{
			SaveSubjectReq saveSubjectReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveSubjectReq>();
			saveSubjectReq.Subject = subject;
			return ClientServiceFactory.GetClientInstance<ILookupSubject>().SaveSubject(saveSubjectReq).SubjectId;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000B7B0 File Offset: 0x000099B0
		public LookupSubjectDTO LoadLookupSubjectBySubjectCode(string SubjectCode)
		{
			LoadLookupSubjectBySubjectCodeReq loadLookupSubjectBySubjectCodeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadLookupSubjectBySubjectCodeReq>();
			loadLookupSubjectBySubjectCodeReq.SubjectCode = SubjectCode;
			return ClientServiceFactory.GetClientInstance<ILookupSubject>().LoadLookupSubjectBySubjectCode(loadLookupSubjectBySubjectCodeReq).Subject;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000B7E8 File Offset: 0x000099E8
		public LookupSubjectDTO LoadLookupSubjectBySubjectDescription(string SubjectDescription)
		{
			LoadLookupSubjectBySubjectDescriptionReq loadLookupSubjectBySubjectDescriptionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadLookupSubjectBySubjectDescriptionReq>();
			loadLookupSubjectBySubjectDescriptionReq.SubjectDescription = SubjectDescription;
			return ClientServiceFactory.GetClientInstance<ILookupSubject>().LoadLookupSubjectBySubjectDescription(loadLookupSubjectBySubjectDescriptionReq).Subject;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000B820 File Offset: 0x00009A20
		public LookupSubjectDTO LoadLookupSubject(string SubjectCode, string SubjectDescription)
		{
			LoadLookupSubjectReq loadLookupSubjectReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadLookupSubjectReq>();
			loadLookupSubjectReq.SubjectCode = SubjectCode;
			loadLookupSubjectReq.SubjectDescription = SubjectDescription;
			return ClientServiceFactory.GetClientInstance<ILookupSubject>().LoadLookupSubject(loadLookupSubjectReq).Subject;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000B860 File Offset: 0x00009A60
		public IList<LookupSubjectDTO> LoadLookupSubjectsBySession(SessionDTO Session)
		{
			LoadLookupSubjectsBySessionReq loadLookupSubjectsBySessionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadLookupSubjectsBySessionReq>();
			loadLookupSubjectsBySessionReq.Session = Session;
			return ClientServiceFactory.GetClientInstance<ILookupSubject>().LoadLookupSubjectsBySession(loadLookupSubjectsBySessionReq).Subjects;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000B898 File Offset: 0x00009A98
		public IList<LookupSubjectDTO> LoadAllLookupSubjects()
		{
			LoadAllLookupSubjectsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllLookupSubjectsReq>();
			return ClientServiceFactory.GetClientInstance<ILookupSubject>().LoadAllLookupSubjects(request).Subjects;
		}
	}
}
