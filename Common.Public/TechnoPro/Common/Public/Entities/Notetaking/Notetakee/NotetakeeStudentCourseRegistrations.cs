using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Notetaking.Notetakee.Status;

namespace TechnoPro.Common.Public.Entities.Notetaking.Notetakee
{
	// Token: 0x02000285 RID: 645
	public class NotetakeeStudentCourseRegistrations : BusinessBase<int>
	{
		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x00019830 File Offset: 0x00017A30
		// (set) Token: 0x06001394 RID: 5012 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int StudentPersonId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x00019848 File Offset: 0x00017A48
		// (set) Token: 0x06001396 RID: 5014 RVA: 0x00019850 File Offset: 0x00017A50
		public IList<NotetakeeCourseRegistration> CourseRegistrations { get; set; }

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x00019859 File Offset: 0x00017A59
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x00019861 File Offset: 0x00017A61
		public NotetakeeCourseRegistrationStudentStatus StudentStatus { get; set; }
	}
}
