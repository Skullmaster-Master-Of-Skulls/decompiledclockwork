using System;
using System.Drawing;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;

namespace TechnoPro.Common.Public.Entities.ClockWorkServer
{
	// Token: 0x0200044B RID: 1099
	public class ClockWorkServerInfo : BusinessBase<string>
	{
		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x0002556A File Offset: 0x0002376A
		// (set) Token: 0x06002149 RID: 8521 RVA: 0x00025572 File Offset: 0x00023772
		public string DepartmentTitle { get; set; }

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x0600214A RID: 8522 RVA: 0x0002557B File Offset: 0x0002377B
		// (set) Token: 0x0600214B RID: 8523 RVA: 0x00025583 File Offset: 0x00023783
		public string DepartmentDescription { get; set; }

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x0600214C RID: 8524 RVA: 0x0002558C File Offset: 0x0002378C
		// (set) Token: 0x0600214D RID: 8525 RVA: 0x00025594 File Offset: 0x00023794
		public string ServerVersion { get; set; }

		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x0002559D File Offset: 0x0002379D
		// (set) Token: 0x0600214F RID: 8527 RVA: 0x000255A5 File Offset: 0x000237A5
		public eBindingType PreferredBindingType { get; set; }

		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x000255AE File Offset: 0x000237AE
		// (set) Token: 0x06002151 RID: 8529 RVA: 0x000255B6 File Offset: 0x000237B6
		public Image DepartmentLogoImage { get; set; }
	}
}
