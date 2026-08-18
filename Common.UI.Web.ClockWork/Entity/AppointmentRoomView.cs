using System;

namespace TechnoPro.Common.UI.Web.ClockWork.Entity
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public class AppointmentRoomView
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020C4 File Offset: 0x000002C4
		public int RoomNo
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020DC File Offset: 0x000002DC
		public string RoomName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002050 File Offset: 0x00000250
		public AppointmentRoomView()
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020F4 File Offset: 0x000002F4
		public AppointmentRoomView(int id, string name)
		{
			this.id = id;
			this.name = name;
		}

		// Token: 0x04000003 RID: 3
		private int id;

		// Token: 0x04000004 RID: 4
		private string name;
	}
}
