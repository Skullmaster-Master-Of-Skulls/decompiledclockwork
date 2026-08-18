using System;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;

namespace System.IdentityModel.Claims
{
	// Token: 0x020001E0 RID: 480
	internal class X509Identity : GenericIdentity, IDisposable
	{
		// Token: 0x06000FFA RID: 4090 RVA: 0x00045680 File Offset: 0x00043880
		public X509Identity(X509Certificate2 certificate) : this(certificate, true, true)
		{
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0004568B File Offset: 0x0004388B
		public X509Identity(X500DistinguishedName x500DistinguishedName)
		{
			this.disposable = true;
			base..ctor("X509", "X509");
			this.x500DistinguishedName = x500DistinguishedName;
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x000456AB File Offset: 0x000438AB
		internal X509Identity(X509Certificate2 certificate, bool clone, bool disposable)
		{
			this.disposable = true;
			base..ctor("X509", "X509");
			this.certificate = (clone ? new X509Certificate2(certificate) : certificate);
			this.disposable = (clone || disposable);
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x000456DF File Offset: 0x000438DF
		public override string Name
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.name == null)
				{
					this.name = this.GetName() + "; " + this.certificate.Thumbprint;
				}
				return this.name;
			}
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00045718 File Offset: 0x00043918
		private string GetName()
		{
			if (this.x500DistinguishedName != null)
			{
				return this.x500DistinguishedName.Name;
			}
			string nameInfo = this.certificate.SubjectName.Name;
			if (!string.IsNullOrEmpty(nameInfo))
			{
				return nameInfo;
			}
			nameInfo = this.certificate.GetNameInfo(X509NameType.DnsName, false);
			if (!string.IsNullOrEmpty(nameInfo))
			{
				return nameInfo;
			}
			nameInfo = this.certificate.GetNameInfo(X509NameType.SimpleName, false);
			if (!string.IsNullOrEmpty(nameInfo))
			{
				return nameInfo;
			}
			nameInfo = this.certificate.GetNameInfo(X509NameType.EmailName, false);
			if (!string.IsNullOrEmpty(nameInfo))
			{
				return nameInfo;
			}
			nameInfo = this.certificate.GetNameInfo(X509NameType.UpnName, false);
			if (!string.IsNullOrEmpty(nameInfo))
			{
				return nameInfo;
			}
			return string.Empty;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x000457B9 File Offset: 0x000439B9
		public override ClaimsIdentity Clone()
		{
			if (this.certificate == null)
			{
				return new X509Identity(this.x500DistinguishedName);
			}
			return new X509Identity(this.certificate);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x000457DA File Offset: 0x000439DA
		public void Dispose()
		{
			if (this.disposable && !this.disposed)
			{
				this.disposed = true;
				if (this.certificate != null)
				{
					this.certificate.Reset();
				}
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00045806 File Offset: 0x00043A06
		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().FullName));
			}
		}

		// Token: 0x04000DDA RID: 3546
		private const string X509 = "X509";

		// Token: 0x04000DDB RID: 3547
		private const string Thumbprint = "; ";

		// Token: 0x04000DDC RID: 3548
		private X500DistinguishedName x500DistinguishedName;

		// Token: 0x04000DDD RID: 3549
		private X509Certificate2 certificate;

		// Token: 0x04000DDE RID: 3550
		private string name;

		// Token: 0x04000DDF RID: 3551
		private bool disposed;

		// Token: 0x04000DE0 RID: 3552
		private bool disposable;
	}
}
