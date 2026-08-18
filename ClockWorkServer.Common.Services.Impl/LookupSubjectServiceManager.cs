using System;
using System.Collections.Generic;
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
	// Token: 0x02000063 RID: 99
	public class LookupSubjectServiceManager : ILookupSubject, IService
	{
		// Token: 0x060003AC RID: 940 RVA: 0x00011174 File Offset: 0x0000F374
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00011188 File Offset: 0x0000F388
		public LoadLookupSubjectsBySessionResp LoadLookupSubjectsBySession(LoadLookupSubjectsBySessionReq Request)
		{
			ILookupSubjectManager lookupSubjectManager = new LookupSubjectManager(Request.GetOperationContext());
			List<LookupSubject> list = lookupSubjectManager.LoadLookupSubjectsBySession(Request.Session.ToDomainObject());
			LoadLookupSubjectsBySessionResp loadLookupSubjectsBySessionResp = new LoadLookupSubjectsBySessionResp();
			List<LookupSubjectDTO> subjects;
			if (list != null)
			{
				subjects = list.ConvertAll<LookupSubjectDTO>((LookupSubject f) => f.ToDTO());
			}
			else
			{
				subjects = null;
			}
			loadLookupSubjectsBySessionResp.Subjects = subjects;
			return loadLookupSubjectsBySessionResp;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000111F0 File Offset: 0x0000F3F0
		public LoadLookupSubjectByIdResp LoadLookupSubjectById(LoadLookupSubjectByIdReq Request)
		{
			ILookupSubjectManager lookupSubjectManager = new LookupSubjectManager(Request.GetOperationContext());
			LookupSubject lookupSubject = lookupSubjectManager.LoadLookupSubject(Request.SubjectId);
			return new LoadLookupSubjectByIdResp
			{
				Subject = ((lookupSubject == null) ? null : lookupSubject.ToDTO())
			};
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00011234 File Offset: 0x0000F434
		public SaveSubjectResp SaveSubject(SaveSubjectReq Request)
		{
			ILookupSubjectManager lookupSubjectManager = new LookupSubjectManager(Request.GetOperationContext());
			int subjectId = lookupSubjectManager.SaveSubject(Request.Subject.ToDomainObject());
			return new SaveSubjectResp
			{
				SubjectId = subjectId
			};
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00011274 File Offset: 0x0000F474
		public LoadLookupSubjectBySubjectCodeResp LoadLookupSubjectBySubjectCode(LoadLookupSubjectBySubjectCodeReq Request)
		{
			ILookupSubjectManager lookupSubjectManager = new LookupSubjectManager(Request.GetOperationContext());
			LookupSubject lookupSubject = lookupSubjectManager.LoadLookupSubjectBySubjectCode(Request.SubjectCode);
			return new LoadLookupSubjectBySubjectCodeResp
			{
				Subject = ((lookupSubject == null) ? null : lookupSubject.ToDTO())
			};
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000112B8 File Offset: 0x0000F4B8
		public LoadLookupSubjectBySubjectDescriptionResp LoadLookupSubjectBySubjectDescription(LoadLookupSubjectBySubjectDescriptionReq Request)
		{
			ILookupSubjectManager lookupSubjectManager = new LookupSubjectManager(Request.GetOperationContext());
			LookupSubject lookupSubject = lookupSubjectManager.LoadLookupSubjectBySubjectDescription(Request.SubjectDescription);
			return new LoadLookupSubjectBySubjectDescriptionResp
			{
				Subject = ((lookupSubject == null) ? null : lookupSubject.ToDTO())
			};
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000112FC File Offset: 0x0000F4FC
		public LoadLookupSubjectResp LoadLookupSubject(LoadLookupSubjectReq Request)
		{
			ILookupSubjectManager lookupSubjectManager = new LookupSubjectManager(Request.GetOperationContext());
			LookupSubject lookupSubject = lookupSubjectManager.LoadLookupSubject(Request.SubjectCode, Request.SubjectDescription);
			return new LoadLookupSubjectResp
			{
				Subject = ((lookupSubject == null) ? null : lookupSubject.ToDTO())
			};
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00011348 File Offset: 0x0000F548
		public LoadAllLookupSubjectsResp LoadAllLookupSubjects(LoadAllLookupSubjectsReq Request)
		{
			ILookupSubjectManager lookupSubjectManager = new LookupSubjectManager(Request.GetOperationContext());
			List<LookupSubject> list = lookupSubjectManager.LoadAllLookupSubjects();
			LoadAllLookupSubjectsResp loadAllLookupSubjectsResp = new LoadAllLookupSubjectsResp();
			List<LookupSubjectDTO> subjects;
			if (list != null)
			{
				subjects = list.ConvertAll<LookupSubjectDTO>((LookupSubject f) => f.ToDTO());
			}
			else
			{
				subjects = null;
			}
			loadAllLookupSubjectsResp.Subjects = subjects;
			return loadAllLookupSubjectsResp;
		}
	}
}
