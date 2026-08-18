using System;
using System.Security.Principal;

namespace System.Web.Security
{
	// Token: 0x020005CD RID: 1485
	[Serializable]
	public class ActiveDirectoryMembershipUser : MembershipUser
	{
		// Token: 0x1700162B RID: 5675
		// (get) Token: 0x06004B42 RID: 19266 RVA: 0x000FF1BD File Offset: 0x000FD3BD
		// (set) Token: 0x06004B43 RID: 19267 RVA: 0x000FF1BD File Offset: 0x000FD3BD
		public override DateTime LastLoginDate
		{
			get
			{
				throw new NotSupportedException(SR.GetString("ADMembership_UserProperty_not_supported", new object[]
				{
					"LastLoginDate"
				}));
			}
			set
			{
				throw new NotSupportedException(SR.GetString("ADMembership_UserProperty_not_supported", new object[]
				{
					"LastLoginDate"
				}));
			}
		}

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x06004B44 RID: 19268 RVA: 0x000FF1DC File Offset: 0x000FD3DC
		// (set) Token: 0x06004B45 RID: 19269 RVA: 0x000FF1DC File Offset: 0x000FD3DC
		public override DateTime LastActivityDate
		{
			get
			{
				throw new NotSupportedException(SR.GetString("ADMembership_UserProperty_not_supported", new object[]
				{
					"LastActivityDate"
				}));
			}
			set
			{
				throw new NotSupportedException(SR.GetString("ADMembership_UserProperty_not_supported", new object[]
				{
					"LastActivityDate"
				}));
			}
		}

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x06004B46 RID: 19270 RVA: 0x000FF1FB File Offset: 0x000FD3FB
		// (set) Token: 0x06004B47 RID: 19271 RVA: 0x000FF203 File Offset: 0x000FD403
		public override string Email
		{
			get
			{
				return base.Email;
			}
			set
			{
				base.Email = value;
				this.emailModified = true;
			}
		}

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x06004B48 RID: 19272 RVA: 0x000FF213 File Offset: 0x000FD413
		// (set) Token: 0x06004B49 RID: 19273 RVA: 0x000FF21B File Offset: 0x000FD41B
		public override string Comment
		{
			get
			{
				return base.Comment;
			}
			set
			{
				base.Comment = value;
				this.commentModified = true;
			}
		}

		// Token: 0x1700162F RID: 5679
		// (get) Token: 0x06004B4A RID: 19274 RVA: 0x000FF22B File Offset: 0x000FD42B
		// (set) Token: 0x06004B4B RID: 19275 RVA: 0x000FF233 File Offset: 0x000FD433
		public override bool IsApproved
		{
			get
			{
				return base.IsApproved;
			}
			set
			{
				base.IsApproved = value;
				this.isApprovedModified = true;
			}
		}

		// Token: 0x17001630 RID: 5680
		// (get) Token: 0x06004B4C RID: 19276 RVA: 0x000FF243 File Offset: 0x000FD443
		public override object ProviderUserKey
		{
			get
			{
				if (this.sid == null && this.sidBinaryForm != null)
				{
					this.sid = new SecurityIdentifier(this.sidBinaryForm, 0);
				}
				return this.sid;
			}
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x000FF274 File Offset: 0x000FD474
		public ActiveDirectoryMembershipUser(string providerName, string name, object providerUserKey, string email, string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate, DateTime lastPasswordChangedDate, DateTime lastLockoutDate) : base(providerName, name, null, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate)
		{
			if (providerUserKey != null && !(providerUserKey is SecurityIdentifier))
			{
				throw new ArgumentException(SR.GetString("ADMembership_InvalidProviderUserKey"), "providerUserKey");
			}
			this.sid = (SecurityIdentifier)providerUserKey;
			if (this.sid != null)
			{
				this.sidBinaryForm = new byte[this.sid.BinaryLength];
				this.sid.GetBinaryForm(this.sidBinaryForm, 0);
			}
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x000FF318 File Offset: 0x000FD518
		internal ActiveDirectoryMembershipUser(string providerName, string name, byte[] sidBinaryForm, object providerUserKey, string email, string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate, DateTime lastPasswordChangedDate, DateTime lastLockoutDate, bool valuesAreUpdated) : base(providerName, name, null, email, passwordQuestion, comment, isApproved, isLockedOut, creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockoutDate)
		{
			if (valuesAreUpdated)
			{
				this.emailModified = false;
				this.commentModified = false;
				this.isApprovedModified = false;
			}
			this.sidBinaryForm = sidBinaryForm;
			this.sid = (SecurityIdentifier)providerUserKey;
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x000FF384 File Offset: 0x000FD584
		protected ActiveDirectoryMembershipUser()
		{
		}

		// Token: 0x04002890 RID: 10384
		internal bool emailModified = true;

		// Token: 0x04002891 RID: 10385
		internal bool commentModified = true;

		// Token: 0x04002892 RID: 10386
		internal bool isApprovedModified = true;

		// Token: 0x04002893 RID: 10387
		private byte[] sidBinaryForm;

		// Token: 0x04002894 RID: 10388
		[NonSerialized]
		private SecurityIdentifier sid;
	}
}
