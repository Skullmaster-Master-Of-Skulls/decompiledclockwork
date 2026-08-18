using System;

namespace TechnoPro.Common.Public.Entities.Membership
{
	// Token: 0x020002AC RID: 684
	public class OnLogonEventArgs : EventArgs
	{
		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0001A1F9 File Offset: 0x000183F9
		// (set) Token: 0x060014AD RID: 5293 RVA: 0x0001A201 File Offset: 0x00018401
		public string Username { get; set; }

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x0001A20A File Offset: 0x0001840A
		// (set) Token: 0x060014AF RID: 5295 RVA: 0x0001A212 File Offset: 0x00018412
		public int PersonId { get; set; }

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x0001A21B File Offset: 0x0001841B
		// (set) Token: 0x060014B1 RID: 5297 RVA: 0x0001A223 File Offset: 0x00018423
		public ClientParameters ClientParameters { get; set; }

		// Token: 0x060014B2 RID: 5298 RVA: 0x0001A22C File Offset: 0x0001842C
		public OnLogonEventArgs()
		{
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0001A236 File Offset: 0x00018436
		public OnLogonEventArgs(string username, int personId, ClientParameters clientParameters)
		{
			this.Username = username;
			this.PersonId = personId;
			this.ClientParameters = clientParameters;
		}
	}
}
