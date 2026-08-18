using System;

namespace TechnoPro.Common.UI.Web.Appointments.Entity
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	public class AppointmentRoomView
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public int RoomNo
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public string RoomName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public AppointmentRoomView()
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
		public AppointmentRoomView(int id, string name)
		{
			this.id = id;
			this.name = name;
		}

		// Token: 0x04000001 RID: 1
		private int id;

		// Token: 0x04000002 RID: 2
		private string name;
	}
}
