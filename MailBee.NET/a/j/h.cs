using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using MailBee;
using MailBee.Security;

namespace a.j
{
	// Token: 0x020001D4 RID: 468
	internal class h : p
	{
		// Token: 0x06000F25 RID: 3877 RVA: 0x000389D1 File Offset: 0x000379D1
		public h(ClientServerCertificates A_0)
		{
			this.g = A_0;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x000389EC File Offset: 0x000379EC
		public override void d1(IPEndPoint A_0)
		{
			this.e.d1(A_0);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x000389FC File Offset: 0x000379FC
		public bool h(object A_0, X509Certificate A_1, X509Chain A_2, SslPolicyErrors A_3)
		{
			if (A_3 == SslPolicyErrors.None)
			{
				return true;
			}
			CertificateValidationFlags certificateValidationFlags = CertificateValidationFlags.None;
			if ((A_3 & SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.RemoteCertificateNameMismatch)
			{
				certificateValidationFlags |= CertificateValidationFlags.NameMismatch;
			}
			if ((A_3 & SslPolicyErrors.RemoteCertificateChainErrors) == SslPolicyErrors.RemoteCertificateChainErrors)
			{
				foreach (X509ChainStatus x509ChainStatus in A_2.ChainStatus)
				{
					if ((x509ChainStatus.Status & X509ChainStatusFlags.NotTimeValid) == X509ChainStatusFlags.NotTimeValid)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsNotTimeValid;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.NotTimeNested) == X509ChainStatusFlags.NotTimeNested)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsNotTimeNested;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.Revoked) == X509ChainStatusFlags.Revoked)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsRevoked;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.NotSignatureValid) == X509ChainStatusFlags.NotSignatureValid)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsNotSignatureValid;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.NotValidForUsage) == X509ChainStatusFlags.NotValidForUsage)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsNotValidForUsage;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.UntrustedRoot) == X509ChainStatusFlags.UntrustedRoot)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsUntrustedRoot;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.RevocationStatusUnknown) == X509ChainStatusFlags.RevocationStatusUnknown)
					{
						certificateValidationFlags |= CertificateValidationFlags.RevocationStatusUnknown;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.Cyclic) == X509ChainStatusFlags.Cyclic)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsCyclic;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.PartialChain) == X509ChainStatusFlags.PartialChain)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsPartialChain;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.CtlNotTimeValid) == X509ChainStatusFlags.CtlNotTimeValid)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsNotTimeValidCtl;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.CtlNotSignatureValid) == X509ChainStatusFlags.CtlNotSignatureValid)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsNotSignatureValidCtl;
					}
					else if ((x509ChainStatus.Status & X509ChainStatusFlags.CtlNotValidForUsage) == X509ChainStatusFlags.CtlNotValidForUsage)
					{
						certificateValidationFlags |= CertificateValidationFlags.IsNotValidForUsageCtl;
					}
				}
			}
			if (A_0 != null)
			{
				((aq)A_0).a = certificateValidationFlags;
			}
			else if (this.e != null)
			{
				this.e.a = certificateValidationFlags;
			}
			return true;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00038BA3 File Offset: 0x00037BA3
		public bool g(object A_0, X509Certificate A_1, X509Chain A_2, SslPolicyErrors A_3)
		{
			return true;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00038BA6 File Offset: 0x00037BA6
		private SslProtocols g(SecurityProtocol A_0)
		{
			if (A_0 == SecurityProtocol.Auto && ServicePointManager.SecurityProtocol > SecurityProtocolType.SystemDefault)
			{
				A_0 = (SecurityProtocol)ServicePointManager.SecurityProtocol;
			}
			if (A_0 == SecurityProtocol.Auto)
			{
				return SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12;
			}
			return (SslProtocols)A_0;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00038BC4 File Offset: 0x00037BC4
		private void i()
		{
			this.e = new aq(this.e.d0(), true, new RemoteCertificateValidationCallback(this.h));
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00038BEC File Offset: 0x00037BEC
		private void h()
		{
			if (this.g != null)
			{
				this.g.NameMismatch = ((this.e.a & CertificateValidationFlags.NameMismatch) > CertificateValidationFlags.None);
			}
			if (this.g != null && this.g.AutoValidation != CertificateValidationFlags.None)
			{
				this.f.c().a8().b(string.Format(Resources.Instance.Log_WillValidateServerCert, new object[0]), null, LogMessageType.Info, this.f.c());
				CertificateValidationFlags a = this.e.a;
				this.f.c().a8().b(string.Format(Resources.Instance.Log_ServerCertRetrieved, new object[0]), null, LogMessageType.Info, this.f.c());
				if (a != CertificateValidationFlags.None && (a & this.g.AutoValidation) > CertificateValidationFlags.None)
				{
					throw new MailBeeCertificateValidationException(a);
				}
				this.f.c().a8().b(string.Format(Resources.Instance.Log_ServerCertAutoValidationSucceeded, new object[0]), null, LogMessageType.Info, this.f.c());
			}
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00038D08 File Offset: 0x00037D08
		public void g(SecurityProtocol A_0, string A_1)
		{
			this.f.c().a8().b(string.Format(Resources.Instance.Log_WillPerformSslHandshake, new object[0]), null, LogMessageType.Info, this.f.c());
			this.i();
			bool checkCertificateRevocation = this.g != null && this.g.CheckCertificateRevocation;
			try
			{
				if (this.g == null || this.g.Client == null)
				{
					try
					{
						this.e.AuthenticateAsClient(A_1, null, this.g(A_0), checkCertificateRevocation);
						goto IL_DD;
					}
					catch (IOException a_)
					{
						throw new MailBeeSslNegotiationException(141, a_);
					}
					catch (NotSupportedException a_2)
					{
						throw new MailBeeSslNegotiationException(142, a_2);
					}
					catch (ArgumentException a_3)
					{
						throw new MailBeeSslNegotiationException(142, a_3);
					}
				}
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.Add(this.g.Client.AsX509Certificate);
				this.e.AuthenticateAsClient(A_1, x509CertificateCollection, this.g(A_0), checkCertificateRevocation);
				IL_DD:
				this.g.ServerCertificateBytes = this.e.RemoteCertificate.Export(X509ContentType.Cert);
			}
			catch (AuthenticationException a_4)
			{
				throw new MailBeeSslNegotiationException(143, a_4);
			}
			this.f.c().a8().b(string.Format(Resources.Instance.Log_SslHandshakeDone, new object[0]), null, LogMessageType.Info, this.f.c());
			this.h();
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00038E90 File Offset: 0x00037E90
		public override void d2()
		{
			this.e.Close();
			this.e.d2();
			this.f = new byte[0];
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00038EB4 File Offset: 0x00037EB4
		public override Stream d0()
		{
			if (this.e != null)
			{
				return this.e;
			}
			return base.d0();
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00038ECC File Offset: 0x00037ECC
		public override int d3(byte[] A_0, int A_1)
		{
			int result;
			try
			{
				result = this.e.Read(A_0, A_1, A_0.Length - A_1);
			}
			catch (IOException ex)
			{
				if (ex.InnerException is MailBeeException)
				{
					throw ex.InnerException;
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00038F18 File Offset: 0x00037F18
		public override int d4(byte[] A_0, int A_1, int A_2)
		{
			try
			{
				this.e.Write(A_0, A_1, A_2);
			}
			catch (IOException ex)
			{
				if (ex.InnerException is MailBeeException)
				{
					throw ex.InnerException;
				}
				throw;
			}
			return A_2;
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00038F60 File Offset: 0x00037F60
		private Task g()
		{
			h.c c;
			c.c = this;
			c.b = AsyncTaskMethodBuilder.Create();
			c.a = -1;
			AsyncTaskMethodBuilder b = c.b;
			b.Start<h.c>(ref c);
			return c.b.Task;
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x00038FA8 File Offset: 0x00037FA8
		public Task h(SecurityProtocol A_0, string A_1)
		{
			h.d d;
			d.c = this;
			d.e = A_0;
			d.d = A_1;
			d.b = AsyncTaskMethodBuilder.Create();
			d.a = -1;
			AsyncTaskMethodBuilder b = d.b;
			b.Start<h.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00039000 File Offset: 0x00038000
		public override Task<int> d6(byte[] A_0, int A_1)
		{
			h.b b;
			b.c = this;
			b.d = A_0;
			b.e = A_1;
			b.b = AsyncTaskMethodBuilder<int>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<int> b2 = b.b;
			b2.Start<h.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00039058 File Offset: 0x00038058
		public override Task<int> d7(byte[] A_0, int A_1, int A_2)
		{
			h.a a;
			a.c = this;
			a.d = A_0;
			a.e = A_1;
			a.f = A_2;
			a.b = AsyncTaskMethodBuilder<int>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<int> b = a.b;
			b.Start<h.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x04000AD2 RID: 2770
		private new aq e;

		// Token: 0x04000AD3 RID: 2771
		private new byte[] f = new byte[0];

		// Token: 0x04000AD4 RID: 2772
		private new ClientServerCertificates g;
	}
}
