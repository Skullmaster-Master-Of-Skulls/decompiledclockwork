using System;

namespace Telerik.Web.UI.Scheduler
{
	// Token: 0x020007DD RID: 2013
	internal abstract class ReminderCommand : ICallbackCommand
	{
		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x0600461E RID: 17950 RVA: 0x000DC478 File Offset: 0x000DA678
		public object AppointmentID
		{
			get
			{
				if (!string.IsNullOrEmpty(this.AppointmentInternalID))
				{
					return LosSerializer.Deserialize(this.AppointmentInternalID);
				}
				return null;
			}
		}

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x0600461F RID: 17951 RVA: 0x000DC494 File Offset: 0x000DA694
		// (set) Token: 0x06004620 RID: 17952 RVA: 0x000DC49C File Offset: 0x000DA69C
		public string AppointmentInternalID { get; set; }

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x06004621 RID: 17953 RVA: 0x000DC4A5 File Offset: 0x000DA6A5
		// (set) Token: 0x06004622 RID: 17954 RVA: 0x000DC4AD File Offset: 0x000DA6AD
		public string ReminderID { get; set; }

		// Token: 0x06004623 RID: 17955
		public abstract void Execute(ICallbackCommandContext context);
	}
}
