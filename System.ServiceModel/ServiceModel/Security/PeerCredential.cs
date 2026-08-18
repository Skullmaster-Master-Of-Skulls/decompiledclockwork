using System;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x02000357 RID: 855
	public class PeerCredential
	{
		// Token: 0x06001F66 RID: 8038 RVA: 0x00074C50 File Offset: 0x00072E50
		internal PeerCredential()
		{
			this.peerAuthentication = new X509PeerCertificateAuthentication();
			this.messageSenderAuthentication = new X509PeerCertificateAuthentication();
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x00074C70 File Offset: 0x00072E70
		internal PeerCredential(PeerCredential other)
		{
			this.certificate = other.certificate;
			this.meshPassword = other.meshPassword;
			this.peerAuthentication = new X509PeerCertificateAuthentication(other.peerAuthentication);
			this.messageSenderAuthentication = new X509PeerCertificateAuthentication(other.messageSenderAuthentication);
			this.isReadOnly = other.isReadOnly;
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x00074CC9 File Offset: 0x00072EC9
		// (set) Token: 0x06001F69 RID: 8041 RVA: 0x00074CD1 File Offset: 0x00072ED1
		public X509Certificate2 Certificate
		{
			get
			{
				return this.certificate;
			}
			set
			{
				this.ThrowIfImmutable();
				this.certificate = value;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x00074CE0 File Offset: 0x00072EE0
		// (set) Token: 0x06001F6B RID: 8043 RVA: 0x00074CE8 File Offset: 0x00072EE8
		public string MeshPassword
		{
			get
			{
				return this.meshPassword;
			}
			set
			{
				this.ThrowIfImmutable();
				this.meshPassword = value;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x00074CF7 File Offset: 0x00072EF7
		// (set) Token: 0x06001F6D RID: 8045 RVA: 0x00074CFF File Offset: 0x00072EFF
		public X509PeerCertificateAuthentication PeerAuthentication
		{
			get
			{
				return this.peerAuthentication;
			}
			set
			{
				this.ThrowIfImmutable();
				this.peerAuthentication = value;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x00074D0E File Offset: 0x00072F0E
		// (set) Token: 0x06001F6F RID: 8047 RVA: 0x00074D16 File Offset: 0x00072F16
		public X509PeerCertificateAuthentication MessageSenderAuthentication
		{
			get
			{
				return this.messageSenderAuthentication;
			}
			set
			{
				this.ThrowIfImmutable();
				this.messageSenderAuthentication = value;
			}
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x00074D25 File Offset: 0x00072F25
		public void SetCertificate(string subjectName, StoreLocation storeLocation, StoreName storeName)
		{
			if (subjectName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("subjectName");
			}
			this.SetCertificate(storeLocation, storeName, X509FindType.FindBySubjectDistinguishedName, subjectName);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x00074D44 File Offset: 0x00072F44
		public void SetCertificate(StoreLocation storeLocation, StoreName storeName, X509FindType findType, object findValue)
		{
			if (findValue == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("findValue");
			}
			this.ThrowIfImmutable();
			this.certificate = SecurityUtils.GetCertificateFromStore(storeName, storeLocation, findType, findValue, null);
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x00074D71 File Offset: 0x00072F71
		internal void MakeReadOnly()
		{
			this.isReadOnly = true;
			this.peerAuthentication.MakeReadOnly();
			this.messageSenderAuthentication.MakeReadOnly();
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x00074D90 File Offset: 0x00072F90
		private void ThrowIfImmutable()
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x00074DB4 File Offset: 0x00072FB4
		private bool SameAuthenticators(X509PeerCertificateAuthentication one, X509PeerCertificateAuthentication two)
		{
			if (one.CertificateValidationMode != two.CertificateValidationMode)
			{
				return false;
			}
			if (one.CertificateValidationMode != X509CertificateValidationMode.Custom)
			{
				return one.GetType().Equals(two.GetType());
			}
			X509CertificateValidator x509CertificateValidator = null;
			X509CertificateValidator x509CertificateValidator2 = null;
			one.TryGetCertificateValidator(out x509CertificateValidator);
			two.TryGetCertificateValidator(out x509CertificateValidator2);
			return x509CertificateValidator != null && x509CertificateValidator2 != null && x509CertificateValidator.Equals(x509CertificateValidator2);
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x00074E14 File Offset: 0x00073014
		internal bool Equals(PeerCredential that, PeerAuthenticationMode mode, bool messageAuthentication)
		{
			if (messageAuthentication)
			{
				if (!this.SameAuthenticators(this.MessageSenderAuthentication, that.messageSenderAuthentication))
				{
					return false;
				}
				if (this.Certificate != null && that.Certificate != null && !this.Certificate.Equals(that.Certificate))
				{
					return false;
				}
			}
			switch (mode)
			{
			case PeerAuthenticationMode.None:
				return true;
			case PeerAuthenticationMode.Password:
				if (!this.MeshPassword.Equals(that.MeshPassword))
				{
					return false;
				}
				if (this.Certificate == null && that.Certificate == null)
				{
					return true;
				}
				if (this.Certificate == null || !this.Certificate.Equals(that.Certificate))
				{
					return false;
				}
				break;
			case PeerAuthenticationMode.MutualCertificate:
				if (!this.Certificate.Equals(that.Certificate))
				{
					return false;
				}
				if (!this.SameAuthenticators(this.PeerAuthentication, that.PeerAuthentication))
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x04001ED9 RID: 7897
		internal const StoreLocation DefaultStoreLocation = StoreLocation.CurrentUser;

		// Token: 0x04001EDA RID: 7898
		internal const StoreName DefaultStoreName = StoreName.My;

		// Token: 0x04001EDB RID: 7899
		internal const X509FindType DefaultFindType = X509FindType.FindBySubjectDistinguishedName;

		// Token: 0x04001EDC RID: 7900
		private X509Certificate2 certificate;

		// Token: 0x04001EDD RID: 7901
		private string meshPassword;

		// Token: 0x04001EDE RID: 7902
		private X509PeerCertificateAuthentication peerAuthentication;

		// Token: 0x04001EDF RID: 7903
		private X509PeerCertificateAuthentication messageSenderAuthentication;

		// Token: 0x04001EE0 RID: 7904
		private bool isReadOnly;
	}
}
