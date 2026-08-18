using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.People
{
	// Token: 0x0200025E RID: 606
	public class MyStudent : BusinessBase<int>
	{
		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x000188E0 File Offset: 0x00016AE0
		// (set) Token: 0x0600123B RID: 4667 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PersonId
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

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x000188F8 File Offset: 0x00016AF8
		// (set) Token: 0x0600123D RID: 4669 RVA: 0x00018910 File Offset: 0x00016B10
		public PersonBase StudentPerson
		{
			get
			{
				return this.studentPerson;
			}
			set
			{
				this.studentPerson = value;
				this.Id = ((this.StudentPerson == null) ? 0 : this.studentPerson.PersonId);
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x00018937 File Offset: 0x00016B37
		// (set) Token: 0x0600123F RID: 4671 RVA: 0x0001893F File Offset: 0x00016B3F
		public PersonBase AssignedAdvisor { get; set; }

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00018948 File Offset: 0x00016B48
		// (set) Token: 0x06001241 RID: 4673 RVA: 0x00018950 File Offset: 0x00016B50
		public IList<MyStudentAppointment> Appointments { get; set; }

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x00018959 File Offset: 0x00016B59
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x00018961 File Offset: 0x00016B61
		public string Email { get; set; }

		// Token: 0x04000FFD RID: 4093
		private PersonBase studentPerson;
	}
}
