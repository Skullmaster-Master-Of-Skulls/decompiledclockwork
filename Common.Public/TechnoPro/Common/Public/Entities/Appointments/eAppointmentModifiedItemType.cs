using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004B5 RID: 1205
	[Flags]
	[Serializable]
	public enum eAppointmentModifiedItemType
	{
		// Token: 0x04001B28 RID: 6952
		None = 0,
		// Token: 0x04001B29 RID: 6953
		DateTime = 1,
		// Token: 0x04001B2A RID: 6954
		AppType = 2,
		// Token: 0x04001B2B RID: 6955
		Cancelled = 4,
		// Token: 0x04001B2C RID: 6956
		Locked = 8,
		// Token: 0x04001B2D RID: 6957
		Private = 16,
		// Token: 0x04001B2E RID: 6958
		SubTitle = 32,
		// Token: 0x04001B2F RID: 6959
		Attendees = 64,
		// Token: 0x04001B30 RID: 6960
		Room = 128,
		// Token: 0x04001B31 RID: 6961
		Location = 256,
		// Token: 0x04001B32 RID: 6962
		ShowTimeAs = 512,
		// Token: 0x04001B33 RID: 6963
		Memo = 1024,
		// Token: 0x04001B34 RID: 6964
		Icons = 2048,
		// Token: 0x04001B35 RID: 6965
		TestInfo = 4096,
		// Token: 0x04001B36 RID: 6966
		WorkshopInfo = 8192,
		// Token: 0x04001B37 RID: 6967
		ExtraAttendeesCount = 16384,
		// Token: 0x04001B38 RID: 6968
		NoShow = 32768,
		// Token: 0x04001B39 RID: 6969
		RecurringInfo = 65536,
		// Token: 0x04001B3A RID: 6970
		MainAppointment = 131072
	}
}
