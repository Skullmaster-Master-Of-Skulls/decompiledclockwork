using System;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Public.Entities.AppointmentsWorkshops
{
	// Token: 0x020004A8 RID: 1192
	public class WorkshopDefinitionOrAppType
	{
		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x0002738E File Offset: 0x0002558E
		// (set) Token: 0x060023E6 RID: 9190 RVA: 0x00027396 File Offset: 0x00025596
		public AppType AppType { get; set; }

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x0002739F File Offset: 0x0002559F
		// (set) Token: 0x060023E8 RID: 9192 RVA: 0x000273A7 File Offset: 0x000255A7
		public WorkshopDefinition WorkshopDefinition { get; set; }
	}
}
