using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.ServiceProvidersOriginal.NotesUpload
{
	// Token: 0x02000208 RID: 520
	public class ServiceProviderWithCourseUploadInfoList
	{
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00017070 File Offset: 0x00015270
		// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x00017078 File Offset: 0x00015278
		public ServiceProviderBase ServiceProvider { get; set; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x00017081 File Offset: 0x00015281
		// (set) Token: 0x06000FB9 RID: 4025 RVA: 0x00017089 File Offset: 0x00015289
		public IList<ServiceProviderCourseWithUploadInfo> CoursesWithInfos { get; set; }
	}
}
