using System;
using System.Drawing;

namespace ClockWorkAPI.EntityExtensions
{
	// Token: 0x0200005F RID: 95
	public class AttendeeExt
	{
		// Token: 0x06000538 RID: 1336 RVA: 0x00019F44 File Offset: 0x00018F44
		public AttendeeExt()
		{
			this.MyAppRectangle = Rectangle.Empty;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00019F5C File Offset: 0x00018F5C
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x00019F73 File Offset: 0x00018F73
		public Rectangle MyAppRectangle { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00019F7C File Offset: 0x00018F7C
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x00019F93 File Offset: 0x00018F93
		public int Column { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00019F9C File Offset: 0x00018F9C
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x00019FB3 File Offset: 0x00018FB3
		public int StartX1 { get; set; }
	}
}
