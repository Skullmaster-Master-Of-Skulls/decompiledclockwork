using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.Common.ClientManager.Notifications.StudentOrDataChangedNotifications
{
	// Token: 0x0200000A RID: 10
	public class ShowStudentInfoRequestedArgs : EventArgs
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002F51 File Offset: 0x00001151
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002F59 File Offset: 0x00001159
		public PersonBaseDTO Student { get; set; }
	}
}
