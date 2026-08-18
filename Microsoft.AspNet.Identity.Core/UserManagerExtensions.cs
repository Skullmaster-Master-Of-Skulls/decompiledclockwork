using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000030 RID: 48
	public static class UserManagerExtensions
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00004798 File Offset: 0x00002998
		public static ClaimsIdentity CreateIdentity<TUser, TKey>(this UserManager<TUser, TKey> manager, TUser user, string authenticationType) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<ClaimsIdentity>(() => manager.CreateIdentityAsync(user, authenticationType));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004800 File Offset: 0x00002A00
		public static TUser FindById<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<TUser>(() => manager.FindByIdAsync(userId));
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004868 File Offset: 0x00002A68
		public static TUser Find<TUser, TKey>(this UserManager<TUser, TKey> manager, string userName, string password) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<TUser>(() => manager.FindAsync(userName, password));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000048D0 File Offset: 0x00002AD0
		public static TUser FindByName<TUser, TKey>(this UserManager<TUser, TKey> manager, string userName) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<TUser>(() => manager.FindByNameAsync(userName));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004930 File Offset: 0x00002B30
		public static TUser FindByEmail<TUser, TKey>(this UserManager<TUser, TKey> manager, string email) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<TUser>(() => manager.FindByEmailAsync(email));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004990 File Offset: 0x00002B90
		public static IdentityResult Create<TUser, TKey>(this UserManager<TUser, TKey> manager, TUser user) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.CreateAsync(user));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000049F8 File Offset: 0x00002BF8
		public static IdentityResult Create<TUser, TKey>(this UserManager<TUser, TKey> manager, TUser user, string password) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.CreateAsync(user, password));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004A60 File Offset: 0x00002C60
		public static IdentityResult Update<TUser, TKey>(this UserManager<TUser, TKey> manager, TUser user) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.UpdateAsync(user));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004AC0 File Offset: 0x00002CC0
		public static IdentityResult Delete<TUser, TKey>(this UserManager<TUser, TKey> manager, TUser user) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.DeleteAsync(user));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004B20 File Offset: 0x00002D20
		public static bool HasPassword<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<bool>(() => manager.HasPasswordAsync(userId));
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004B88 File Offset: 0x00002D88
		public static IdentityResult AddPassword<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string password) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.AddPasswordAsync(userId, password));
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004BFC File Offset: 0x00002DFC
		public static IdentityResult ChangePassword<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string currentPassword, string newPassword) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.ChangePasswordAsync(userId, currentPassword, newPassword));
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004C78 File Offset: 0x00002E78
		public static IdentityResult ResetPassword<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string token, string newPassword) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.ResetPasswordAsync(userId, token, newPassword));
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004CE8 File Offset: 0x00002EE8
		public static string GeneratePasswordResetToken<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<string>(() => manager.GeneratePasswordResetTokenAsync(userId));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004D48 File Offset: 0x00002F48
		public static string GetSecurityStamp<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<string>(() => manager.GetSecurityStampAsync(userId));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004DA8 File Offset: 0x00002FA8
		public static string GenerateEmailConfirmationToken<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<string>(() => manager.GenerateEmailConfirmationTokenAsync(userId));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004E10 File Offset: 0x00003010
		public static IdentityResult ConfirmEmail<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string token) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.ConfirmEmailAsync(userId, token));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004E78 File Offset: 0x00003078
		public static bool IsEmailConfirmed<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<bool>(() => manager.IsEmailConfirmedAsync(userId));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004ED8 File Offset: 0x000030D8
		public static IdentityResult UpdateSecurityStamp<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.UpdateSecurityStampAsync(userId));
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004F40 File Offset: 0x00003140
		public static bool CheckPassword<TUser, TKey>(this UserManager<TUser, TKey> manager, TUser user, string password) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<bool>(() => manager.CheckPasswordAsync(user, password));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004FA8 File Offset: 0x000031A8
		public static IdentityResult RemovePassword<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.RemovePasswordAsync(userId));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005010 File Offset: 0x00003210
		public static IdentityResult AddLogin<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, UserLoginInfo login) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.AddLoginAsync(userId, login));
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005080 File Offset: 0x00003280
		public static IdentityResult RemoveLogin<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, UserLoginInfo login) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.RemoveLoginAsync(userId, login));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000050E8 File Offset: 0x000032E8
		public static IList<UserLoginInfo> GetLogins<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IList<UserLoginInfo>>(() => manager.GetLoginsAsync(userId));
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005148 File Offset: 0x00003348
		public static TUser Find<TUser, TKey>(this UserManager<TUser, TKey> manager, UserLoginInfo login) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<TUser>(() => manager.FindAsync(login));
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000051B0 File Offset: 0x000033B0
		public static IdentityResult AddClaim<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, Claim claim) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.AddClaimAsync(userId, claim));
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005220 File Offset: 0x00003420
		public static IdentityResult RemoveClaim<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, Claim claim) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.RemoveClaimAsync(userId, claim));
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005288 File Offset: 0x00003488
		public static IList<Claim> GetClaims<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IList<Claim>>(() => manager.GetClaimsAsync(userId));
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000052F0 File Offset: 0x000034F0
		public static IdentityResult AddToRole<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string role) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.AddToRoleAsync(userId, role));
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005360 File Offset: 0x00003560
		public static IdentityResult AddToRoles<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, params string[] roles) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.AddToRolesAsync(userId, roles));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000053D0 File Offset: 0x000035D0
		public static IdentityResult RemoveFromRole<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string role) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.RemoveFromRoleAsync(userId, role));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005440 File Offset: 0x00003640
		public static IdentityResult RemoveFromRoles<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, params string[] roles) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.RemoveFromRolesAsync(userId, roles));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000054A8 File Offset: 0x000036A8
		public static IList<string> GetRoles<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IList<string>>(() => manager.GetRolesAsync(userId));
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005510 File Offset: 0x00003710
		public static bool IsInRole<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string role) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<bool>(() => manager.IsInRoleAsync(userId, role));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005578 File Offset: 0x00003778
		public static string GetEmail<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<string>(() => manager.GetEmailAsync(userId));
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000055E0 File Offset: 0x000037E0
		public static IdentityResult SetEmail<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string email) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.SetEmailAsync(userId, email));
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005648 File Offset: 0x00003848
		public static string GetPhoneNumber<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<string>(() => manager.GetPhoneNumberAsync(userId));
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000056B0 File Offset: 0x000038B0
		public static IdentityResult SetPhoneNumber<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string phoneNumber) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.SetPhoneNumberAsync(userId, phoneNumber));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005724 File Offset: 0x00003924
		public static IdentityResult ChangePhoneNumber<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string phoneNumber, string token) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.ChangePhoneNumberAsync(userId, phoneNumber, token));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005798 File Offset: 0x00003998
		public static string GenerateChangePhoneNumberToken<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string phoneNumber) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<string>(() => manager.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000580C File Offset: 0x00003A0C
		public static bool VerifyChangePhoneNumberToken<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string token, string phoneNumber) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<bool>(() => manager.VerifyChangePhoneNumberTokenAsync(userId, token, phoneNumber));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000587C File Offset: 0x00003A7C
		public static bool IsPhoneNumberConfirmed<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<bool>(() => manager.IsPhoneNumberConfirmedAsync(userId));
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000058E4 File Offset: 0x00003AE4
		public static string GenerateTwoFactorToken<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string providerId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<string>(() => manager.GenerateTwoFactorTokenAsync(userId, providerId));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005958 File Offset: 0x00003B58
		public static bool VerifyTwoFactorToken<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string providerId, string token) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<bool>(() => manager.VerifyTwoFactorTokenAsync(userId, providerId, token));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000059C8 File Offset: 0x00003BC8
		public static IList<string> GetValidTwoFactorProviders<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IList<string>>(() => manager.GetValidTwoFactorProvidersAsync(userId));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005A30 File Offset: 0x00003C30
		public static string GenerateUserToken<TUser, TKey>(this UserManager<TUser, TKey> manager, string purpose, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<string>(() => manager.GenerateUserTokenAsync(purpose, userId));
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005AA4 File Offset: 0x00003CA4
		public static bool VerifyUserToken<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string purpose, string token) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<bool>(() => manager.VerifyUserTokenAsync(userId, purpose, token));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005B20 File Offset: 0x00003D20
		public static IdentityResult NotifyTwoFactorToken<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string twoFactorProvider, string token) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.NotifyTwoFactorTokenAsync(userId, twoFactorProvider, token));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005B90 File Offset: 0x00003D90
		public static bool GetTwoFactorEnabled<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<bool>(() => manager.GetTwoFactorEnabledAsync(userId));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005BF8 File Offset: 0x00003DF8
		public static IdentityResult SetTwoFactorEnabled<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, bool enabled) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.SetTwoFactorEnabledAsync(userId, enabled));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005C6C File Offset: 0x00003E6C
		public static void SendEmail<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string subject, string body) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			AsyncHelper.RunSync(() => manager.SendEmailAsync(userId, subject, body));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public static void SendSms<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, string message) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			AsyncHelper.RunSync(() => manager.SendSmsAsync(userId, message));
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005D48 File Offset: 0x00003F48
		public static bool IsLockedOut<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<bool>(() => manager.IsLockedOutAsync(userId));
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005DB0 File Offset: 0x00003FB0
		public static IdentityResult SetLockoutEnabled<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, bool enabled) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.SetLockoutEnabledAsync(userId, enabled));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005E18 File Offset: 0x00004018
		public static bool GetLockoutEnabled<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<bool>(() => manager.GetLockoutEnabledAsync(userId));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005E78 File Offset: 0x00004078
		public static DateTimeOffset GetLockoutEndDate<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<DateTimeOffset>(() => manager.GetLockoutEndDateAsync(userId));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005EE0 File Offset: 0x000040E0
		public static IdentityResult SetLockoutEndDate<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId, DateTimeOffset lockoutEnd) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.SetLockoutEndDateAsync(userId, lockoutEnd));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005F48 File Offset: 0x00004148
		public static IdentityResult AccessFailed<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.AccessFailedAsync(userId));
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005FA8 File Offset: 0x000041A8
		public static IdentityResult ResetAccessFailedCount<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<IdentityResult>(() => manager.ResetAccessFailedCountAsync(userId));
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006008 File Offset: 0x00004208
		public static int GetAccessFailedCount<TUser, TKey>(this UserManager<TUser, TKey> manager, TKey userId) where TUser : class, IUser<TKey> where TKey : IEquatable<TKey>
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			return AsyncHelper.RunSync<int>(() => manager.GetAccessFailedCountAsync(userId));
		}
	}
}
