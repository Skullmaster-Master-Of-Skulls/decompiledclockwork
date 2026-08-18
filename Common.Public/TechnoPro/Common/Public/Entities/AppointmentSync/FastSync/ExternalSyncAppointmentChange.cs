using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync.FastSync
{
	// Token: 0x020004EA RID: 1258
	public class ExternalSyncAppointmentChange : BusinessBase<string>
	{
		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x00028A1C File Offset: 0x00026C1C
		// (set) Token: 0x0600260C RID: 9740 RVA: 0x00028A48 File Offset: 0x00026C48
		public override string Id
		{
			get
			{
				return (this.ExternalAppointmentID != null) ? this.ExternalAppointmentID.UniqueId2 : string.Empty;
			}
			set
			{
				bool flag = this.ExternalAppointmentID != null;
				if (flag)
				{
					this.ExternalAppointmentID.UniqueId2 = value;
				}
			}
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x00028A70 File Offset: 0x00026C70
		// (set) Token: 0x0600260E RID: 9742 RVA: 0x00028A78 File Offset: 0x00026C78
		public ExternalAppointmentId ExternalAppointmentID { get; set; }

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x00028A81 File Offset: 0x00026C81
		// (set) Token: 0x06002610 RID: 9744 RVA: 0x00028A89 File Offset: 0x00026C89
		public eAppointmentSyncChangeType AppointmentSyncChangeType { get; set; }

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x00028A92 File Offset: 0x00026C92
		// (set) Token: 0x06002612 RID: 9746 RVA: 0x00028A9A File Offset: 0x00026C9A
		public DateTime LastModifiedDate { get; set; }

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x00028AA3 File Offset: 0x00026CA3
		// (set) Token: 0x06002614 RID: 9748 RVA: 0x00028AAB File Offset: 0x00026CAB
		public ClockWorkExternalAppMapping Mapping { get; set; }

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x00028AB4 File Offset: 0x00026CB4
		// (set) Token: 0x06002616 RID: 9750 RVA: 0x00028ABC File Offset: 0x00026CBC
		public bool IsPrivate { get; set; }

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x00028AC5 File Offset: 0x00026CC5
		// (set) Token: 0x06002618 RID: 9752 RVA: 0x00028ACD File Offset: 0x00026CCD
		public bool IsAllDayEvent { get; set; }
	}
}
