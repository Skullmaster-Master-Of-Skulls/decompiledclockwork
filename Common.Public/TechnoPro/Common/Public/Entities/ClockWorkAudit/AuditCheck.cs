using System;

namespace TechnoPro.Common.Public.Entities.ClockWorkAudit
{
	// Token: 0x02000460 RID: 1120
	public class AuditCheck
	{
		// Token: 0x06002226 RID: 8742 RVA: 0x0000D55A File Offset: 0x0000B75A
		public AuditCheck()
		{
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x00026228 File Offset: 0x00024428
		public AuditCheck(string title, eAuditStatus status, params string[] note)
		{
			this.Status = status;
			this.Title = title;
			try
			{
				bool flag = note == null || note.Length < 1;
				if (flag)
				{
					this.Note = "";
				}
				else
				{
					bool flag2 = note.Length == 1;
					if (flag2)
					{
						this.Note = note[0];
					}
					else
					{
						string[] array = new string[note.Length - 1];
						for (int i = 1; i < note.Length; i++)
						{
							array[i - 1] = note[i];
						}
						string format = note[0];
						object[] args = array;
						this.Note = string.Format(format, args);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x000262D8 File Offset: 0x000244D8
		// (set) Token: 0x06002229 RID: 8745 RVA: 0x000262E0 File Offset: 0x000244E0
		public string Title { get; set; }

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x000262E9 File Offset: 0x000244E9
		// (set) Token: 0x0600222B RID: 8747 RVA: 0x000262F1 File Offset: 0x000244F1
		public string Description { get; set; }

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x000262FA File Offset: 0x000244FA
		// (set) Token: 0x0600222D RID: 8749 RVA: 0x00026302 File Offset: 0x00024502
		public string Note { get; set; }

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x0002630B File Offset: 0x0002450B
		// (set) Token: 0x0600222F RID: 8751 RVA: 0x00026313 File Offset: 0x00024513
		public eAuditStatus Status { get; set; }
	}
}
