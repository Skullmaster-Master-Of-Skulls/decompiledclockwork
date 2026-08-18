using System;
using System.Runtime.Serialization;

namespace MailBee.Security
{
	// Token: 0x02000116 RID: 278
	[Serializable]
	public class MailBeeCertificateValidationException : MailBeeCertificateException
	{
		// Token: 0x0600091B RID: 2331 RVA: 0x0002A0FC File Offset: 0x000290FC
		internal MailBeeCertificateValidationException(CertificateValidationFlags A_0) : base(1110)
		{
			this._status = A_0;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0002A110 File Offset: 0x00029110
		protected MailBeeCertificateValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0002A11A File Offset: 0x0002911A
		public CertificateValidationFlags Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x0400070D RID: 1805
		private CertificateValidationFlags _status;
	}
}
