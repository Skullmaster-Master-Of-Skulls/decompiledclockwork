using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200019A RID: 410
	public class X509WindowsSecurityToken : X509SecurityToken
	{
		// Token: 0x06000D7C RID: 3452 RVA: 0x0003EB27 File Offset: 0x0003CD27
		public X509WindowsSecurityToken(X509Certificate2 certificate, WindowsIdentity windowsIdentity) : this(certificate, windowsIdentity, null, true)
		{
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0003EB33 File Offset: 0x0003CD33
		public X509WindowsSecurityToken(X509Certificate2 certificate, WindowsIdentity windowsIdentity, string id) : this(certificate, windowsIdentity, null, id, true)
		{
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0003EB40 File Offset: 0x0003CD40
		public X509WindowsSecurityToken(X509Certificate2 certificate, WindowsIdentity windowsIdentity, string authenticationType, string id) : this(certificate, windowsIdentity, authenticationType, id, true)
		{
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0003EB4E File Offset: 0x0003CD4E
		internal X509WindowsSecurityToken(X509Certificate2 certificate, WindowsIdentity windowsIdentity, string authenticationType, bool clone) : this(certificate, windowsIdentity, authenticationType, SecurityUniqueId.Create().Value, clone)
		{
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0003EB65 File Offset: 0x0003CD65
		internal X509WindowsSecurityToken(X509Certificate2 certificate, WindowsIdentity windowsIdentity, string authenticationType, string id, bool clone) : base(certificate, id, clone)
		{
			if (windowsIdentity == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("windowsIdentity");
			}
			this.authenticationType = authenticationType;
			this.windowsIdentity = (clone ? SecurityUtils.CloneWindowsIdentityIfNecessary(windowsIdentity, authenticationType) : windowsIdentity);
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x0003EBA0 File Offset: 0x0003CDA0
		public WindowsIdentity WindowsIdentity
		{
			get
			{
				base.ThrowIfDisposed();
				return this.windowsIdentity;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x0003EBAE File Offset: 0x0003CDAE
		public string AuthenticationType
		{
			get
			{
				return this.authenticationType;
			}
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0003EBB8 File Offset: 0x0003CDB8
		public override void Dispose()
		{
			try
			{
				if (!this.disposed)
				{
					this.disposed = true;
					this.windowsIdentity.Dispose();
					this.windowsIdentity = null;
				}
			}
			finally
			{
				base.Dispose();
			}
		}

		// Token: 0x04000CC8 RID: 3272
		private WindowsIdentity windowsIdentity;

		// Token: 0x04000CC9 RID: 3273
		private bool disposed;

		// Token: 0x04000CCA RID: 3274
		private string authenticationType;
	}
}
