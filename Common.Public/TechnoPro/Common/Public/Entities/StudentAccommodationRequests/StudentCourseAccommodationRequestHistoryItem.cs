using System;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests
{
	// Token: 0x020001A4 RID: 420
	public class StudentCourseAccommodationRequestHistoryItem : BusinessBase<int, int>
	{
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00013B48 File Offset: 0x00011D48
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x0000E258 File Offset: 0x0000C458
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

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00013B60 File Offset: 0x00011D60
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int LuCourseId
		{
			get
			{
				return this.SecondId;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00013B78 File Offset: 0x00011D78
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x00013B80 File Offset: 0x00011D80
		public int StudentCourseAccommodationRequestId { get; set; }

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x00013B89 File Offset: 0x00011D89
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x00013B91 File Offset: 0x00011D91
		public eStudentCourseAccommodationRequestHistoryItemHowModified HowModified { get; set; }

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00013B9A File Offset: 0x00011D9A
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x00013BA2 File Offset: 0x00011DA2
		public DateTime DateModified { get; set; }

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00013BAB File Offset: 0x00011DAB
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x00013BB3 File Offset: 0x00011DB3
		public PersonBase WhoModified { get; set; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00013BBC File Offset: 0x00011DBC
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x00013BC4 File Offset: 0x00011DC4
		public LookupCourseBase Course { get; set; }

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00013BCD File Offset: 0x00011DCD
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x00013BD5 File Offset: 0x00011DD5
		public eStudentCourseAccommodationRequestStatus Status { get; set; }

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00013BDE File Offset: 0x00011DDE
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x00013BE6 File Offset: 0x00011DE6
		public bool AccommodationChangesRequested { get; set; }

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00013BEF File Offset: 0x00011DEF
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x00013BF7 File Offset: 0x00011DF7
		public bool AdditionalAccommodationsRequested { get; set; }

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00013C00 File Offset: 0x00011E00
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x00013C08 File Offset: 0x00011E08
		public string Note1 { get; set; }

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00013C11 File Offset: 0x00011E11
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x00013C19 File Offset: 0x00011E19
		public string Note2 { get; set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00013C22 File Offset: 0x00011E22
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x00013C2A File Offset: 0x00011E2A
		public DateTime DateRequested { get; set; }
	}
}
