using System;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.LookupCourses.ExtendedDataSyncData
{
	// Token: 0x020002F6 RID: 758
	public class CourseExtendedDataSyncField : BusinessBase<int>
	{
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x0001C1D4 File Offset: 0x0001A3D4
		// (set) Token: 0x060016EE RID: 5870 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ControlId
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

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0001C1EC File Offset: 0x0001A3EC
		// (set) Token: 0x060016F0 RID: 5872 RVA: 0x0001C1F4 File Offset: 0x0001A3F4
		public string ControlCaption { get; set; }

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0001C1FD File Offset: 0x0001A3FD
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x0001C205 File Offset: 0x0001A405
		public eControlCode ControlCode { get; set; }

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x0001C20E File Offset: 0x0001A40E
		// (set) Token: 0x060016F4 RID: 5876 RVA: 0x0001C216 File Offset: 0x0001A416
		public int OrderNum { get; set; }

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x0001C21F File Offset: 0x0001A41F
		// (set) Token: 0x060016F6 RID: 5878 RVA: 0x0001C227 File Offset: 0x0001A427
		public bool IsActive { get; set; }
	}
}
