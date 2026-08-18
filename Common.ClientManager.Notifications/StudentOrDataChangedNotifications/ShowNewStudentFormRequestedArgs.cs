using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.Common.ClientManager.Notifications.StudentOrDataChangedNotifications
{
	// Token: 0x0200000B RID: 11
	public class ShowNewStudentFormRequestedArgs : EventArgs
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002F62 File Offset: 0x00001162
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002F6A File Offset: 0x0000116A
		public Func<PersonBaseDTO, bool> StudentAddedResult { get; set; }
	}
}
