using System;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000473 RID: 1139
	public class TlsInputStream : BaseInputStream
	{
		// Token: 0x060026D0 RID: 9936 RVA: 0x000EACCC File Offset: 0x000E9CCC
		internal TlsInputStream(TlsProtocolHandler handler)
		{
			this.handler = handler;
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x000EACDB File Offset: 0x000E9CDB
		public override int Read(byte[] buf, int offset, int len)
		{
			return this.handler.ReadApplicationData(buf, offset, len);
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x000EACEC File Offset: 0x000E9CEC
		public override int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) <= 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x000EAD11 File Offset: 0x000E9D11
		public override void Close()
		{
			this.handler.Close();
			base.Close();
		}

		// Token: 0x04001AB9 RID: 6841
		private readonly TlsProtocolHandler handler;
	}
}
