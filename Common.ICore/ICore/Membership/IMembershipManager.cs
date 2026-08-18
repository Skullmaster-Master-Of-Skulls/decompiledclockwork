using System;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Public.Entities.Membership;

namespace TechnoPro.Common.ICore.Membership
{
	// Token: 0x02000061 RID: 97
	public interface IMembershipManager
	{
		// Token: 0x0600029D RID: 669
		bool ValidateClockWorkHashingAuthentication(ClockWorkHashAuthentication hashAuth);

		// Token: 0x0600029E RID: 670
		bool TryLogon(string username, string password, ClientParameters clientParameters, out AuthenticationSession ticket);

		// Token: 0x0600029F RID: 671
		bool TryLogon(ClockWorkHashAuthentication hashAuth, ClientParameters clientParameters, out AuthenticationSession ticket);

		// Token: 0x060002A0 RID: 672
		bool TryLogon(string username, string password, string logonAsUsername, ClientParameters clientParameters, out AuthenticationSession ticket);

		// Token: 0x060002A1 RID: 673
		AuthTicket Authenticate(Guid ticket);

		// Token: 0x060002A2 RID: 674
		void Logout(Guid ticket, ClientParameters clientParameters);

		// Token: 0x060002A3 RID: 675
		void RemoveExpiredSessions();

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002A4 RID: 676
		// (remove) Token: 0x060002A5 RID: 677
		event EventHandler<OnLogonEventArgs> OnUserLogon;

		// Token: 0x060002A6 RID: 678
		bool ChangeUserPassword(string UserName, string CurrentPassword, string NewPassword, out string msg);

		// Token: 0x060002A7 RID: 679
		bool UserMustChangePassword(string UserName);

		// Token: 0x060002A8 RID: 680
		bool ChangeUserPasswordByAdmin(Guid ticket, string UserName, string NewPassword, out string msg);

		// Token: 0x060002A9 RID: 681
		void LoadAuthenticationSessions();
	}
}
