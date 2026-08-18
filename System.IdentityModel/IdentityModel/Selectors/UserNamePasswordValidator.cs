using System;
using System.IdentityModel.Tokens;
using System.Web.Security;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001AC RID: 428
	public abstract class UserNamePasswordValidator
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0003FDD3 File Offset: 0x0003DFD3
		public static UserNamePasswordValidator None
		{
			get
			{
				if (UserNamePasswordValidator.none == null)
				{
					UserNamePasswordValidator.none = new UserNamePasswordValidator.NoneUserNamePasswordValidator();
				}
				return UserNamePasswordValidator.none;
			}
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0003FDEB File Offset: 0x0003DFEB
		public static UserNamePasswordValidator CreateMembershipProviderValidator(MembershipProvider provider)
		{
			if (provider == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("provider");
			}
			return new UserNamePasswordValidator.MembershipProviderValidator(provider);
		}

		// Token: 0x06000E09 RID: 3593
		public abstract void Validate(string userName, string password);

		// Token: 0x04000CE8 RID: 3304
		private static UserNamePasswordValidator none;

		// Token: 0x02000297 RID: 663
		private class NoneUserNamePasswordValidator : UserNamePasswordValidator
		{
			// Token: 0x06001377 RID: 4983 RVA: 0x000024C1 File Offset: 0x000006C1
			public override void Validate(string userName, string password)
			{
			}
		}

		// Token: 0x02000298 RID: 664
		private class MembershipProviderValidator : UserNamePasswordValidator
		{
			// Token: 0x06001379 RID: 4985 RVA: 0x00052A96 File Offset: 0x00050C96
			public MembershipProviderValidator(MembershipProvider provider)
			{
				this.provider = provider;
			}

			// Token: 0x0600137A RID: 4986 RVA: 0x00052AA8 File Offset: 0x00050CA8
			public override void Validate(string userName, string password)
			{
				if (!this.provider.ValidateUser(userName, password))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("UserNameAuthenticationFailed", new object[]
					{
						this.provider.GetType().Name
					})));
				}
			}

			// Token: 0x04001135 RID: 4405
			private MembershipProvider provider;
		}
	}
}
