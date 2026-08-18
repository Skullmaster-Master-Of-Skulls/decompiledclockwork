using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.LookupCourses
{
	// Token: 0x020000D4 RID: 212
	public class LookupSubjectManager : ILookupSubjectManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00037B73 File Offset: 0x00035D73
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x00037B7B File Offset: 0x00035D7B
		public ILookupSubjectDAO dao { get; set; }

		// Token: 0x0600081D RID: 2077 RVA: 0x00037B84 File Offset: 0x00035D84
		public LookupSubjectManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new LookupSubjectDAO(opContext);
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00037BA3 File Offset: 0x00035DA3
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x00037BAB File Offset: 0x00035DAB
		public OperationContext OpContext { get; set; }

		// Token: 0x06000820 RID: 2080 RVA: 0x00037BB4 File Offset: 0x00035DB4
		public List<LookupSubject> LoadAllLookupSubjects()
		{
			return this.dao.LoadAllLookupSubjects();
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00037BD4 File Offset: 0x00035DD4
		public List<LookupSubject> LoadLookupSubjectsBySession(Session Session)
		{
			return this.dao.LoadLookupSubjectsBySession(Session);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00037BF4 File Offset: 0x00035DF4
		public LookupSubject LoadLookupSubject(int SubjectId)
		{
			return this.dao.LoadLookupSubject(SubjectId);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00037C14 File Offset: 0x00035E14
		public int SaveSubject(LookupSubject subject)
		{
			this.dao.SaveSubject(subject);
			return subject.SubjectId;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00037C3C File Offset: 0x00035E3C
		public LookupSubject LoadLookupSubjectBySubjectCode(string SubjectCode)
		{
			return this.dao.LoadLookupSubjectBySubjectCode(SubjectCode);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00037C5C File Offset: 0x00035E5C
		public LookupSubject LoadLookupSubjectBySubjectDescription(string SubjectDescription)
		{
			return this.dao.LoadLookupSubjectBySubjectDescription(SubjectDescription);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00037C7C File Offset: 0x00035E7C
		public LookupSubject LoadLookupSubject(string SubjectCode, string SubjectDescription)
		{
			return this.dao.LoadLookupSubject(SubjectCode, SubjectDescription);
		}
	}
}
