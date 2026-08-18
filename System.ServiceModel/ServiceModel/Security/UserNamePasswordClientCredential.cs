using System;

namespace System.ServiceModel.Security
{
	// Token: 0x0200033C RID: 828
	[__DynamicallyInvokable]
	public sealed class UserNamePasswordClientCredential
	{
		// Token: 0x06001E0F RID: 7695 RVA: 0x0006FD40 File Offset: 0x0006DF40
		internal UserNamePasswordClientCredential()
		{
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x0006FD48 File Offset: 0x0006DF48
		internal UserNamePasswordClientCredential(UserNamePasswordClientCredential other)
		{
			this.userName = other.userName;
			this.password = other.password;
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x0006FD74 File Offset: 0x0006DF74
		// (set) Token: 0x06001E12 RID: 7698 RVA: 0x0006FD7C File Offset: 0x0006DF7C
		[__DynamicallyInvokable]
		public string UserName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.userName;
			}
			[__DynamicallyInvokable]
			set
			{
				this.ThrowIfImmutable();
				this.userName = value;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x0006FD8B File Offset: 0x0006DF8B
		// (set) Token: 0x06001E14 RID: 7700 RVA: 0x0006FD93 File Offset: 0x0006DF93
		[__DynamicallyInvokable]
		public string Password
		{
			[__DynamicallyInvokable]
			get
			{
				return this.password;
			}
			[__DynamicallyInvokable]
			set
			{
				this.ThrowIfImmutable();
				this.password = value;
			}
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x0006FDA2 File Offset: 0x0006DFA2
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x0006FDAB File Offset: 0x0006DFAB
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x04001E59 RID: 7769
		private string userName;

		// Token: 0x04001E5A RID: 7770
		private string password;

		// Token: 0x04001E5B RID: 7771
		private bool isReadOnly;
	}
}
