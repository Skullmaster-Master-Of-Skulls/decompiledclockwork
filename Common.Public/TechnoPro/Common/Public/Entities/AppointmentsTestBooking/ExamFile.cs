using System;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x02000505 RID: 1285
	public class ExamFile : BusinessBase<int>
	{
		// Token: 0x0600270A RID: 9994 RVA: 0x000294B4 File Offset: 0x000276B4
		public ExamFile()
		{
			this.IsVisible = true;
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x000294C8 File Offset: 0x000276C8
		// (set) Token: 0x0600270C RID: 9996 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ExamFileId
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

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x000294E0 File Offset: 0x000276E0
		// (set) Token: 0x0600270E RID: 9998 RVA: 0x000294E8 File Offset: 0x000276E8
		public int ExamId { get; set; }

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x0600270F RID: 9999 RVA: 0x000294F1 File Offset: 0x000276F1
		// (set) Token: 0x06002710 RID: 10000 RVA: 0x000294F9 File Offset: 0x000276F9
		public BinaryFile File { get; set; }

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x00029502 File Offset: 0x00027702
		// (set) Token: 0x06002712 RID: 10002 RVA: 0x0002950A File Offset: 0x0002770A
		public DateTime DateEntered { get; set; }

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x06002713 RID: 10003 RVA: 0x00029513 File Offset: 0x00027713
		// (set) Token: 0x06002714 RID: 10004 RVA: 0x0002951B File Offset: 0x0002771B
		public int WhoEntered { get; set; }

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x00029524 File Offset: 0x00027724
		// (set) Token: 0x06002716 RID: 10006 RVA: 0x0002952C File Offset: 0x0002772C
		public string Description { get; set; }

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x00029535 File Offset: 0x00027735
		// (set) Token: 0x06002718 RID: 10008 RVA: 0x0002953D File Offset: 0x0002773D
		public bool IsVisible { get; set; }
	}
}
