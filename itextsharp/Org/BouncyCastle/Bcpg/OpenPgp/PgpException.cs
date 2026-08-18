using System;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000080 RID: 128
	public class PgpException : Exception
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x000160A4 File Offset: 0x000150A4
		public PgpException()
		{
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000160AC File Offset: 0x000150AC
		public PgpException(string message) : base(message)
		{
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000160B5 File Offset: 0x000150B5
		public PgpException(string message, Exception exception) : base(message, exception)
		{
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x000160BF File Offset: 0x000150BF
		[Obsolete("Use InnerException property")]
		public Exception UnderlyingException
		{
			get
			{
				return base.InnerException;
			}
		}
	}
}
