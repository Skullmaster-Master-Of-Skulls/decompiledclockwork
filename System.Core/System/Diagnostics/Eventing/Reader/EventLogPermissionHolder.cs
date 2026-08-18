using System;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002BC RID: 700
	internal class EventLogPermissionHolder
	{
		// Token: 0x06001976 RID: 6518 RVA: 0x0005CB08 File Offset: 0x0005AD08
		public static EventLogPermission GetEventLogPermission()
		{
			EventLogPermission eventLogPermission = new EventLogPermission();
			EventLogPermissionEntry value = new EventLogPermissionEntry(EventLogPermissionAccess.Administer, ".");
			eventLogPermission.PermissionEntries.Add(value);
			return eventLogPermission;
		}
	}
}
