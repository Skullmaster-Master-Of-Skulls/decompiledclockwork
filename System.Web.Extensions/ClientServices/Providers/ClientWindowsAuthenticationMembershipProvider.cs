using System;
using System.Security.Principal;
using System.Threading;
using System.Web.Resources;
using System.Web.Security;

namespace System.Web.ClientServices.Providers
{
	// Token: 0x02000112 RID: 274
	public class ClientWindowsAuthenticationMembershipProvider : MembershipProvider
	{
		// Token: 0x06000E79 RID: 3705 RVA: 0x00034424 File Offset: 0x00032624
		public override bool ValidateUser(string username, string password)
		{
			WindowsIdentity current = WindowsIdentity.GetCurrent();
			if (!string.IsNullOrEmpty(password))
			{
				throw new ArgumentException(AtlasWeb.ArgumentMustBeNull, "password");
			}
			if (!string.IsNullOrEmpty(username) && string.Compare(username, current.Name, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException(AtlasWeb.ArgumentMustBeNull, "username");
			}
			Thread.CurrentPrincipal = new ClientRolePrincipal(current);
			return true;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00034482 File Offset: 0x00032682
		public void Logout()
		{
			Thread.CurrentPrincipal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0001359B File Offset: 0x0001179B
		public override bool EnablePasswordRetrieval
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0001359B File Offset: 0x0001179B
		public override bool EnablePasswordReset
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0001359B File Offset: 0x0001179B
		public override bool RequiresQuestionAndAnswer
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x00032138 File Offset: 0x00030338
		// (set) Token: 0x06000E7F RID: 3711 RVA: 0x000032F4 File Offset: 0x000014F4
		public override string ApplicationName
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0003213F File Offset: 0x0003033F
		public override int MaxInvalidPasswordAttempts
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0003213F File Offset: 0x0003033F
		public override int PasswordAttemptWindow
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x0001359B File Offset: 0x0001179B
		public override bool RequiresUniqueEmail
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0001D1CA File Offset: 0x0001B3CA
		public override MembershipPasswordFormat PasswordFormat
		{
			get
			{
				return MembershipPasswordFormat.Hashed;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x0001D1CA File Offset: 0x0001B3CA
		public override int MinRequiredPasswordLength
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x0001359B File Offset: 0x0001179B
		public override int MinRequiredNonAlphanumericCharacters
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00032146 File Offset: 0x00030346
		public override string PasswordStrengthRegularExpression
		{
			get
			{
				return "*";
			}
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0003214D File Offset: 0x0003034D
		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003214D File Offset: 0x0003034D
		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0003214D File Offset: 0x0003034D
		public override string GetPassword(string username, string answer)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003214D File Offset: 0x0003034D
		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0003214D File Offset: 0x0003034D
		public override string ResetPassword(string username, string answer)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0003214D File Offset: 0x0003034D
		public override void UpdateUser(MembershipUser user)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0003214D File Offset: 0x0003034D
		public override bool UnlockUser(string username)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0003214D File Offset: 0x0003034D
		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003214D File Offset: 0x0003034D
		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0003214D File Offset: 0x0003034D
		public override string GetUserNameByEmail(string email)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003214D File Offset: 0x0003034D
		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0003214D File Offset: 0x0003034D
		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003214D File Offset: 0x0003034D
		public override int GetNumberOfUsersOnline()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003214D File Offset: 0x0003034D
		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003214D File Offset: 0x0003034D
		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotSupportedException();
		}
	}
}
