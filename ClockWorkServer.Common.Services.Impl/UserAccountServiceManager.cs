using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters;
using TechnoPro.Common.Core.Mappers.UserAccount;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.UserAccount;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.UserAccount;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200009D RID: 157
	public class UserAccountServiceManager : IUserAccount, IService
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x0001A978 File Offset: 0x00018B78
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001A98C File Offset: 0x00018B8C
		public void RemovePassword(RemovePasswordReq Request)
		{
			IUserAccountManager userAccountManager = new UserAccountManager(Request.GetOperationContext());
			userAccountManager.RemovePassword(Request.PersonId, Request.UserName);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0001A9BC File Offset: 0x00018BBC
		public CreatePasswordResp CreatePassword(CreatePasswordReq Request)
		{
			IUserAccountManager userAccountManager = new UserAccountManager(Request.GetOperationContext());
			string message;
			bool passwordChangeWasSuccessful = userAccountManager.CreatePassword(Request.PasswordInfo.ToDomainObject(), out message);
			return new CreatePasswordResp
			{
				PasswordChangeWasSuccessful = passwordChangeWasSuccessful,
				Message = message
			};
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0001AA04 File Offset: 0x00018C04
		public void UpdatePasswordRequireChange(UpdatePasswordRequireChangeReq Request)
		{
			IUserAccountManager userAccountManager = new UserAccountManager(Request.GetOperationContext());
			userAccountManager.UpdatePasswordRequireChange(Request.PersonId, Request.UserName, Request.NewDoesRequirePasswordChange);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001AA38 File Offset: 0x00018C38
		public UpdatePasswordResp UpdatePassword(UpdatePasswordReq Request)
		{
			IUserAccountManager userAccountManager = new UserAccountManager(Request.GetOperationContext());
			string message;
			bool passwordChangeWasSuccessful = userAccountManager.UpdatePassword(Request.PersonId, Request.UserName, Request.NewPassword, out message);
			return new UpdatePasswordResp
			{
				PasswordChangeWasSuccessful = passwordChangeWasSuccessful,
				Message = message
			};
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001AA88 File Offset: 0x00018C88
		public void ClearAllPasswords(ClearAllPasswordsReq Request)
		{
			IUserAccountManager userAccountManager = new UserAccountManager(Request.GetOperationContext());
			userAccountManager.ClearAllPasswords(Request.PersonId, Request.ClearPrimaryPassword);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001AAB8 File Offset: 0x00018CB8
		public LoadPrimaryPasswordResp LoadPrimaryPassword(LoadPrimaryPasswordReq Request)
		{
			IUserAccountManager userAccountManager = new UserAccountManager(Request.GetOperationContext());
			UserInfoPassword userInfoPassword = userAccountManager.LoadPrimaryPassword(Request.PersonId);
			return new LoadPrimaryPasswordResp
			{
				PasswordInfo = ((userInfoPassword == null) ? null : userInfoPassword.ToDTO())
			};
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001AAFC File Offset: 0x00018CFC
		public void ClearPrimaryPassword(ClearPrimaryPasswordReq Request)
		{
			IUserAccountManager userAccountManager = new UserAccountManager(Request.GetOperationContext());
			userAccountManager.ClearPrimaryPassword(Request.PersonId);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001AB24 File Offset: 0x00018D24
		public void UpdatePrimaryPasswordRequireChange(UpdatePrimaryPasswordRequireChangeReq Request)
		{
			IUserAccountManager userAccountManager = new UserAccountManager(Request.GetOperationContext());
			userAccountManager.UpdatePrimaryPasswordRequireChange(Request.PersonId, Request.NewDoesRequirePasswordChange);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001AB54 File Offset: 0x00018D54
		public UpdatePrimaryPasswordResp UpdatePrimaryPassword(UpdatePrimaryPasswordReq Request)
		{
			IUserAccountManager userAccountManager = new UserAccountManager(Request.GetOperationContext());
			string message;
			bool passwordChangeWasSuccessful = userAccountManager.UpdatePrimaryPassword(Request.PersonId, Request.NewPassword, out message);
			return new UpdatePrimaryPasswordResp
			{
				PasswordChangeWasSuccessful = passwordChangeWasSuccessful,
				Message = message
			};
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001AB9C File Offset: 0x00018D9C
		public UpdatePrimaryPassword2Resp UpdatePrimaryPassword2(UpdatePrimaryPassword2Req Request)
		{
			IUserAccountManager userAccountManager = new UserAccountManager(Request.GetOperationContext());
			string message;
			bool passwordChangeWasSuccessful = userAccountManager.UpdatePrimaryPassword2(Request.PasswordInfo.ToDomainObject(), out message);
			return new UpdatePrimaryPassword2Resp
			{
				PasswordChangeWasSuccessful = passwordChangeWasSuccessful,
				Message = message
			};
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0001ABE4 File Offset: 0x00018DE4
		public void UpdatePrimaryPasswordExpiry(UpdatePrimaryPasswordExpiryReq Request)
		{
			IUserAccountManager userAccountManager = new UserAccountManager(Request.GetOperationContext());
			userAccountManager.UpdatePrimaryPasswordExpiry(Request.PersonId, Request.NewExpiryDate);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001AC14 File Offset: 0x00018E14
		public ValidatePasswordAgainstPolicyResp ValidatePasswordAgainstPolicy(ValidatePasswordAgainstPolicyReq Request)
		{
			IUserAccountManager userAccountManager = new UserAccountManager(Request.GetOperationContext());
			string message;
			bool passedRequirementsCheck = userAccountManager.ValidatePasswordAgainstPolicy(Request.Password, out message);
			return new ValidatePasswordAgainstPolicyResp
			{
				PassedRequirementsCheck = passedRequirementsCheck,
				Message = message
			};
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0001AC58 File Offset: 0x00018E58
		public LoadPasswordPolicyResp LoadPasswordPolicy(LoadPasswordPolicyReq Request)
		{
			IUserAccountManager userAccountManager = new UserAccountManager(Request.GetOperationContext());
			PasswordPolicy passwordPolicy = userAccountManager.LoadPasswordPolicy();
			return new LoadPasswordPolicyResp
			{
				PasswordPolicy = ((passwordPolicy == null) ? null : passwordPolicy.ToDTO())
			};
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0001AC98 File Offset: 0x00018E98
		public void UpdatePasswordPolicy(UpdatePasswordPolicyReq Request)
		{
			IUserAccountManager userAccountManager = new UserAccountManager(Request.GetOperationContext());
			userAccountManager.UpdatePasswordPolicy(Request.PasswordPolicy.ToDomainObject());
		}
	}
}
