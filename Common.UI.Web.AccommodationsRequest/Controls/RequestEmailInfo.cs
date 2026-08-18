using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.Common.UI.Web.AccommodationsRequest.Controls
{
	// Token: 0x02000006 RID: 6
	public class RequestEmailInfo
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00004F2D File Offset: 0x0000312D
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00004F35 File Offset: 0x00003135
		public string CourseDescription { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00004F3E File Offset: 0x0000313E
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00004F46 File Offset: 0x00003146
		public string Status { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00004F4F File Offset: 0x0000314F
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00004F57 File Offset: 0x00003157
		public int Lucid { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00004F60 File Offset: 0x00003160
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00004F68 File Offset: 0x00003168
		public LookupInstructorDTO Prof { get; set; }
	}
}
