using System;
using System.Collections.Generic;

namespace ClockWorkAPI.StudentSummary
{
	// Token: 0x02000096 RID: 150
	public class StudentSummarySection
	{
		// Token: 0x060007A5 RID: 1957 RVA: 0x0002C969 File Offset: 0x0002B969
		public StudentSummarySection(StudentSummarySectionType type, List<int> relevantIds, string title)
		{
			this.title = title;
			this.ids = relevantIds;
			this.sectionType = type;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0002C990 File Offset: 0x0002B990
		public StudentSummarySection(StudentSummarySectionType type, List<int> relevantIds)
		{
			this.ids = relevantIds;
			this.sectionType = type;
			if (type <= StudentSummarySectionType.Accommodations)
			{
				switch (type)
				{
				case StudentSummarySectionType.PerStudentForm:
					this.title = "Data";
					break;
				case StudentSummarySectionType.PerAppointmentForm:
					this.title = "Data";
					break;
				case (StudentSummarySectionType)3:
					break;
				case StudentSummarySectionType.PerDateForm:
					this.title = "Data";
					break;
				default:
					if (type == StudentSummarySectionType.Accommodations)
					{
						this.title = "Accommodations";
					}
					break;
				}
			}
			else if (type != StudentSummarySectionType.Courses)
			{
				if (type != StudentSummarySectionType.Appointments)
				{
					if (type == StudentSummarySectionType.Groups)
					{
						this.title = "Departments";
					}
				}
				else
				{
					this.title = "Appointments";
				}
			}
			else
			{
				this.title = "Courses";
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0002CA4C File Offset: 0x0002BA4C
		public string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0002CA64 File Offset: 0x0002BA64
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0002CA7C File Offset: 0x0002BA7C
		public List<int> Ids
		{
			get
			{
				return this.ids;
			}
			set
			{
				this.ids = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0002CA88 File Offset: 0x0002BA88
		public StudentSummarySectionType SectionType
		{
			get
			{
				return this.sectionType;
			}
		}

		// Token: 0x040003E3 RID: 995
		private StudentSummarySectionType sectionType = StudentSummarySectionType.none;

		// Token: 0x040003E4 RID: 996
		private List<int> ids;

		// Token: 0x040003E5 RID: 997
		private string title;
	}
}
