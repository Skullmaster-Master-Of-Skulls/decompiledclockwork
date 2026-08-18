using System;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x020001DC RID: 476
	public class PkixCertPathValidatorException : GeneralSecurityException
	{
		// Token: 0x060012C7 RID: 4807 RVA: 0x0006B39A File Offset: 0x0006A39A
		public PkixCertPathValidatorException()
		{
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0006B3A9 File Offset: 0x0006A3A9
		public PkixCertPathValidatorException(string message) : base(message)
		{
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0006B3B9 File Offset: 0x0006A3B9
		public PkixCertPathValidatorException(string message, Exception cause) : base(message)
		{
			this.cause = cause;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0006B3D0 File Offset: 0x0006A3D0
		public PkixCertPathValidatorException(string message, Exception cause, PkixCertPath certPath, int index) : base(message)
		{
			if (certPath == null && index != -1)
			{
				throw new ArgumentNullException("certPath = null and index != -1");
			}
			if (index < -1 || (certPath != null && index >= certPath.Certificates.Count))
			{
				throw new IndexOutOfRangeException(" index < -1 or out of bound of certPath.getCertificates()");
			}
			this.cause = cause;
			this.certPath = certPath;
			this.index = index;
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x0006B438 File Offset: 0x0006A438
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (message != null)
				{
					return message;
				}
				if (this.cause != null)
				{
					return this.cause.Message;
				}
				return null;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x0006B466 File Offset: 0x0006A466
		public PkixCertPath CertPath
		{
			get
			{
				return this.certPath;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x0006B46E File Offset: 0x0006A46E
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x04000D49 RID: 3401
		private Exception cause;

		// Token: 0x04000D4A RID: 3402
		private PkixCertPath certPath;

		// Token: 0x04000D4B RID: 3403
		private int index = -1;
	}
}
