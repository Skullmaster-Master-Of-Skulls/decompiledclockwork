using System;

namespace TechnoPro.Common.UI.Web.Entity.WebLogin
{
	// Token: 0x0200001E RID: 30
	public enum eWebPageTargetAudience
	{
		// Token: 0x0400008A RID: 138
		[WebPageTargetAudience("Unknown", eWebPageTargetAudience.Unknown)]
		Unknown,
		// Token: 0x0400008B RID: 139
		[WebPageTargetAudience("Student", eWebPageTargetAudience.Student)]
		Student,
		// Token: 0x0400008C RID: 140
		[WebPageTargetAudience("Staff / Faculty", eWebPageTargetAudience.Staff)]
		Staff,
		// Token: 0x0400008D RID: 141
		[WebPageTargetAudience("Faculty", eWebPageTargetAudience.Staff)]
		Instructor,
		// Token: 0x0400008E RID: 142
		[WebPageTargetAudience("Notetaker", eWebPageTargetAudience.Student)]
		Notetaker,
		// Token: 0x0400008F RID: 143
		[WebPageTargetAudience("Tutor", eWebPageTargetAudience.Unknown)]
		Tutor
	}
}
