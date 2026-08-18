using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace ClockWorkAPI
{
	// Token: 0x02000058 RID: 88
	public class AppPermissionsManager
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x000178E4 File Offset: 0x000168E4
		public AppPermissionsManager()
		{
			this.Permissions = new Dictionary<int, int>();
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000178FB File Offset: 0x000168FB
		public AppPermissionsManager(bool appointmentPermissionsEnabled)
		{
			this.Permissions = new Dictionary<int, int>();
			this.AppointmentPermissionsEnabled = appointmentPermissionsEnabled;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001791C File Offset: 0x0001691C
		private int GetUserPermissions(int pid, int WhoAmI_id)
		{
			if (!this.Permissions.ContainsKey(pid))
			{
				int personPermissionsForUser = App.GetPersonPermissionsForUser(pid, WhoAmI_id, this.AppointmentPermissionsEnabled);
				this.Permissions.Add(pid, personPermissionsForUser);
			}
			return this.Permissions[pid];
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00017968 File Offset: 0x00016968
		public void SetPermission(int pid, int permissionToViewMyScheduleLevel)
		{
			if (this.Permissions.ContainsKey(pid))
			{
				this.Permissions[pid] = permissionToViewMyScheduleLevel;
			}
			else
			{
				this.Permissions.Add(pid, permissionToViewMyScheduleLevel);
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000179AC File Offset: 0x000169AC
		public int GetPermission(int pid)
		{
			int result;
			if (this.Permissions.ContainsKey(pid))
			{
				result = this.Permissions[pid];
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000179E4 File Offset: 0x000169E4
		public bool IsWhoAmIAllowed_allMatch(int pid, PersonBaseDTO WhoAmI, int appPermissions)
		{
			return this.IsWhoAmIAllowed_allMatch(pid, WhoAmI.PersonId, appPermissions);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00017A04 File Offset: 0x00016A04
		public bool IsWhoAmIAllowed_anyMatch(int pid, PersonBaseDTO WhoAmI, int appPermissions)
		{
			return this.IsWhoAmIAllowed_anyMatch(pid, WhoAmI.PersonId, appPermissions);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00017A24 File Offset: 0x00016A24
		public bool IsWhoAmIAllowed_allMatch(int pid, int WhoAmI_id, int appPermissions)
		{
			bool result;
			if (!this.AppointmentPermissionsEnabled)
			{
				result = true;
			}
			else
			{
				int userPermissions = this.GetUserPermissions(pid, WhoAmI_id);
				bool flag = (userPermissions & appPermissions) == appPermissions;
				result = flag;
			}
			return result;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00017A58 File Offset: 0x00016A58
		public bool IsWhoAmIAllowed_anyMatch(int pid, int WhoAmI_id, int appPermissions)
		{
			bool result;
			if (!this.AppointmentPermissionsEnabled)
			{
				result = true;
			}
			else
			{
				int userPermissions = this.GetUserPermissions(pid, WhoAmI_id);
				bool flag = (userPermissions & appPermissions) > 0;
				result = flag;
			}
			return result;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00017A8C File Offset: 0x00016A8C
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x00017AA3 File Offset: 0x00016AA3
		public Dictionary<int, int> Permissions { get; set; }

		// Token: 0x040001D1 RID: 465
		public bool AppointmentPermissionsEnabled;
	}
}
