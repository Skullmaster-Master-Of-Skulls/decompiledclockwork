using System;

namespace MailBee.Security
{
	// Token: 0x02000100 RID: 256
	public class ClientServerCertificates : IDisposable
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x000283C0 File Offset: 0x000273C0
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x000283C8 File Offset: 0x000273C8
		public CertificateValidationFlags AutoValidation
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x000283D1 File Offset: 0x000273D1
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x000283D9 File Offset: 0x000273D9
		public bool CheckCertificateRevocation
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x000283E2 File Offset: 0x000273E2
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x000283EA File Offset: 0x000273EA
		public Certificate Client
		{
			get
			{
				return this.d;
			}
			set
			{
				this.d = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x000283F4 File Offset: 0x000273F4
		public Certificate Server
		{
			get
			{
				if (this.e == null && this.f != null)
				{
					this.e = new Certificate(this.f, CertFileType.Cer);
					this.e.NameMismatch = this.c;
					this.f = null;
				}
				return this.e;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x00028441 File Offset: 0x00027441
		internal byte[] ServerCertificateBytes
		{
			set
			{
				this.f = value;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x0002844A File Offset: 0x0002744A
		internal bool NameMismatch
		{
			set
			{
				this.c = value;
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00028453 File Offset: 0x00027453
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x040006D1 RID: 1745
		private CertificateValidationFlags a;

		// Token: 0x040006D2 RID: 1746
		private bool b;

		// Token: 0x040006D3 RID: 1747
		private bool c;

		// Token: 0x040006D4 RID: 1748
		private Certificate d;

		// Token: 0x040006D5 RID: 1749
		private Certificate e;

		// Token: 0x040006D6 RID: 1750
		private byte[] f;
	}
}
