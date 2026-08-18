using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserAccount.Parameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.UserAccount;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.UserAccount
{
	// Token: 0x0200000B RID: 11
	public class UserAccountClientManager : IUserAccountClientManager, IWebService
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00003280 File Offset: 0x00001480
		public UserInfoPasswordDTO LoadPrimaryPassword(int PersonId)
		{
			LoadPrimaryPasswordReq loadPrimaryPasswordReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPrimaryPasswordReq>();
			loadPrimaryPasswordReq.PersonId = PersonId;
			return ClientServiceFactory.GetClientInstance<IUserAccount>().LoadPrimaryPassword(loadPrimaryPasswordReq).PasswordInfo;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000032B8 File Offset: 0x000014B8
		public void RemovePassword(int PersonId, string UserName)
		{
			RemovePasswordReq removePasswordReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemovePasswordReq>();
			removePasswordReq.PersonId = PersonId;
			removePasswordReq.UserName = UserName;
			ClientServiceFactory.GetClientInstance<IUserAccount>().RemovePassword(removePasswordReq);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000032F0 File Offset: 0x000014F0
		public CreatePasswordResp CreatePassword(UserInfoPasswordDTO PasswordInfo)
		{
			CreatePasswordReq createPasswordReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreatePasswordReq>();
			createPasswordReq.PasswordInfo = PasswordInfo;
			return ClientServiceFactory.GetClientInstance<IUserAccount>().CreatePassword(createPasswordReq);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003320 File Offset: 0x00001520
		public void UpdatePasswordRequireChange(int PersonId, string UserName, bool NewDoesRequirePasswordChange)
		{
			UpdatePasswordRequireChangeReq updatePasswordRequireChangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdatePasswordRequireChangeReq>();
			updatePasswordRequireChangeReq.PersonId = PersonId;
			updatePasswordRequireChangeReq.UserName = UserName;
			updatePasswordRequireChangeReq.NewDoesRequirePasswordChange = NewDoesRequirePasswordChange;
			ClientServiceFactory.GetClientInstance<IUserAccount>().UpdatePasswordRequireChange(updatePasswordRequireChangeReq);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003360 File Offset: 0x00001560
		public UpdatePasswordResp UpdatePassword(int PersonId, string UserName, string NewPassword)
		{
			UpdatePasswordReq updatePasswordReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdatePasswordReq>();
			updatePasswordReq.PersonId = PersonId;
			updatePasswordReq.UserName = UserName;
			updatePasswordReq.NewPassword = NewPassword;
			return ClientServiceFactory.GetClientInstance<IUserAccount>().UpdatePassword(updatePasswordReq);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000033A0 File Offset: 0x000015A0
		public void ClearAllPasswords(int PersonId, bool ClearPrimaryPassword = true)
		{
			ClearAllPasswordsReq clearAllPasswordsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearAllPasswordsReq>();
			clearAllPasswordsReq.PersonId = PersonId;
			clearAllPasswordsReq.ClearPrimaryPassword = ClearPrimaryPassword;
			ClientServiceFactory.GetClientInstance<IUserAccount>().ClearAllPasswords(clearAllPasswordsReq);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000033D8 File Offset: 0x000015D8
		public void ClearPrimaryPassword(int PersonId)
		{
			ClearPrimaryPasswordReq clearPrimaryPasswordReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearPrimaryPasswordReq>();
			clearPrimaryPasswordReq.PersonId = PersonId;
			ClientServiceFactory.GetClientInstance<IUserAccount>().ClearPrimaryPassword(clearPrimaryPasswordReq);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003408 File Offset: 0x00001608
		public void UpdatePrimaryPasswordRequireChange(int PersonId, bool NewDoesRequirePasswordChange)
		{
			UpdatePrimaryPasswordRequireChangeReq updatePrimaryPasswordRequireChangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdatePrimaryPasswordRequireChangeReq>();
			updatePrimaryPasswordRequireChangeReq.PersonId = PersonId;
			updatePrimaryPasswordRequireChangeReq.NewDoesRequirePasswordChange = NewDoesRequirePasswordChange;
			ClientServiceFactory.GetClientInstance<IUserAccount>().UpdatePrimaryPasswordRequireChange(updatePrimaryPasswordRequireChangeReq);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003440 File Offset: 0x00001640
		public UpdatePrimaryPassword2Resp UpdatePrimaryPassword(UserInfoPasswordDTO PasswordInfo)
		{
			UpdatePrimaryPassword2Req updatePrimaryPassword2Req = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdatePrimaryPassword2Req>();
			updatePrimaryPassword2Req.PasswordInfo = PasswordInfo;
			return ClientServiceFactory.GetClientInstance<IUserAccount>().UpdatePrimaryPassword2(updatePrimaryPassword2Req);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003470 File Offset: 0x00001670
		public void UpdatePrimaryPasswordExpiry(int PersonId, DateTime? NewExpiryDate)
		{
			UpdatePrimaryPasswordExpiryReq updatePrimaryPasswordExpiryReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdatePrimaryPasswordExpiryReq>();
			updatePrimaryPasswordExpiryReq.PersonId = PersonId;
			updatePrimaryPasswordExpiryReq.NewExpiryDate = NewExpiryDate;
			ClientServiceFactory.GetClientInstance<IUserAccount>().UpdatePrimaryPasswordExpiry(updatePrimaryPasswordExpiryReq);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000034A8 File Offset: 0x000016A8
		public ValidatePasswordAgainstPolicyResp ValidatePasswordAgainstPolicy(string Password)
		{
			ValidatePasswordAgainstPolicyReq validatePasswordAgainstPolicyReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ValidatePasswordAgainstPolicyReq>();
			validatePasswordAgainstPolicyReq.Password = Password;
			return ClientServiceFactory.GetClientInstance<IUserAccount>().ValidatePasswordAgainstPolicy(validatePasswordAgainstPolicyReq);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000034D8 File Offset: 0x000016D8
		public PasswordPolicyDTO LoadPasswordPolicy()
		{
			LoadPasswordPolicyReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadPasswordPolicyReq>();
			return ClientServiceFactory.GetClientInstance<IUserAccount>().LoadPasswordPolicy(request).PasswordPolicy;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003508 File Offset: 0x00001708
		public void UpdatePasswordPolicy(PasswordPolicyDTO Policy)
		{
			UpdatePasswordPolicyReq updatePasswordPolicyReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdatePasswordPolicyReq>();
			updatePasswordPolicyReq.PasswordPolicy = Policy;
			ClientServiceFactory.GetClientInstance<IUserAccount>().UpdatePasswordPolicy(updatePasswordPolicyReq);
		}
	}
}
