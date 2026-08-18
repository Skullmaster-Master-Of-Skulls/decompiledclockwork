using System;
using System.Security.Principal;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200038E RID: 910
	internal class WindowsSidIdentity : IIdentity
	{
		// Token: 0x060021C6 RID: 8646 RVA: 0x0007BFB1 File Offset: 0x0007A1B1
		public WindowsSidIdentity(SecurityIdentifier sid)
		{
			if (sid == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sid");
			}
			this.sid = sid;
			this.authenticationType = string.Empty;
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0007BFE4 File Offset: 0x0007A1E4
		public WindowsSidIdentity(SecurityIdentifier sid, string name, string authenticationType)
		{
			if (sid == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("sid");
			}
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (authenticationType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("authenticationType");
			}
			this.sid = sid;
			this.name = name;
			this.authenticationType = authenticationType;
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x060021C8 RID: 8648 RVA: 0x0007C04B File Offset: 0x0007A24B
		public SecurityIdentifier SecurityIdentifier
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x060021C9 RID: 8649 RVA: 0x0007C053 File Offset: 0x0007A253
		public string AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x060021CA RID: 8650 RVA: 0x0007C05B File Offset: 0x0007A25B
		public bool IsAuthenticated
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x0007C05E File Offset: 0x0007A25E
		public string Name
		{
			get
			{
				if (this.name == null)
				{
					this.name = ((NTAccount)this.sid.Translate(typeof(NTAccount))).Value;
				}
				return this.name;
			}
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x0007C094 File Offset: 0x0007A294
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			WindowsSidIdentity windowsSidIdentity = obj as WindowsSidIdentity;
			return windowsSidIdentity != null && this.sid == windowsSidIdentity.SecurityIdentifier;
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x0007C0C4 File Offset: 0x0007A2C4
		public override int GetHashCode()
		{
			return this.sid.GetHashCode();
		}

		// Token: 0x04001F83 RID: 8067
		private SecurityIdentifier sid;

		// Token: 0x04001F84 RID: 8068
		private string name;

		// Token: 0x04001F85 RID: 8069
		private string authenticationType;
	}
}
