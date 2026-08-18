using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x02000648 RID: 1608
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class PublisherIdentityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060039FB RID: 14843 RVA: 0x000C2AD4 File Offset: 0x000C1AD4
		public PublisherIdentityPermissionAttribute(SecurityAction action) : base(action)
		{
			this.m_x509cert = null;
			this.m_certFile = null;
			this.m_signedFile = null;
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060039FC RID: 14844 RVA: 0x000C2AF2 File Offset: 0x000C1AF2
		// (set) Token: 0x060039FD RID: 14845 RVA: 0x000C2AFA File Offset: 0x000C1AFA
		public string X509Certificate
		{
			get
			{
				return this.m_x509cert;
			}
			set
			{
				this.m_x509cert = value;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x000C2B03 File Offset: 0x000C1B03
		// (set) Token: 0x060039FF RID: 14847 RVA: 0x000C2B0B File Offset: 0x000C1B0B
		public string CertFile
		{
			get
			{
				return this.m_certFile;
			}
			set
			{
				this.m_certFile = value;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06003A00 RID: 14848 RVA: 0x000C2B14 File Offset: 0x000C1B14
		// (set) Token: 0x06003A01 RID: 14849 RVA: 0x000C2B1C File Offset: 0x000C1B1C
		public string SignedFile
		{
			get
			{
				return this.m_signedFile;
			}
			set
			{
				this.m_signedFile = value;
			}
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x000C2B28 File Offset: 0x000C1B28
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new PublisherIdentityPermission(PermissionState.Unrestricted);
			}
			if (this.m_x509cert != null)
			{
				return new PublisherIdentityPermission(new X509Certificate(Hex.DecodeHexString(this.m_x509cert)));
			}
			if (this.m_certFile != null)
			{
				return new PublisherIdentityPermission(System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromCertFile(this.m_certFile));
			}
			if (this.m_signedFile != null)
			{
				return new PublisherIdentityPermission(System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromSignedFile(this.m_signedFile));
			}
			return new PublisherIdentityPermission(PermissionState.None);
		}

		// Token: 0x04001E1C RID: 7708
		private string m_x509cert;

		// Token: 0x04001E1D RID: 7709
		private string m_certFile;

		// Token: 0x04001E1E RID: 7710
		private string m_signedFile;
	}
}
