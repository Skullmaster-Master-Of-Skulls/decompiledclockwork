using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020003AF RID: 943
	public class FormApprovalStateAttribute : Attribute
	{
		// Token: 0x06001CB9 RID: 7353 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public FormApprovalStateAttribute()
		{
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x00020D16 File Offset: 0x0001EF16
		public FormApprovalStateAttribute(string displayTitle)
		{
			this.DisplayTitle = displayTitle;
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x00020D28 File Offset: 0x0001EF28
		// (set) Token: 0x06001CBC RID: 7356 RVA: 0x00020D30 File Offset: 0x0001EF30
		public string DisplayTitle { get; set; }
	}
}
