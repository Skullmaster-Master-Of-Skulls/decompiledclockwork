using System;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.GoogleCalendar.Impl.Adapters
{
	// Token: 0x0200000C RID: 12
	public static class DelegatePermissionAdapter
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00004138 File Offset: 0x00002338
		public static DelegatePermissionLevel ToDelegatePermissionLevel(this string permission)
		{
			DelegatePermissionLevel result;
			if (!(permission == "owner") && !(permission == "writer"))
			{
				if (!(permission == "reader") && !(permission == "freeBusyReader"))
				{
					result = DelegatePermissionLevel.None;
				}
				else
				{
					result = DelegatePermissionLevel.Read;
				}
			}
			else
			{
				result = (DelegatePermissionLevel.Read | DelegatePermissionLevel.Write);
			}
			return result;
		}
	}
}
