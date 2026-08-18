using System;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000344 RID: 836
	public class TlsOuputStream : BaseOutputStream
	{
		// Token: 0x06001E3B RID: 7739 RVA: 0x000B56C0 File Offset: 0x000B46C0
		internal TlsOuputStream(TlsProtocolHandler handler)
		{
			this.handler = handler;
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x000B56CF File Offset: 0x000B46CF
		public override void Write(byte[] buf, int offset, int len)
		{
			this.handler.WriteData(buf, offset, len);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x000B56E0 File Offset: 0x000B46E0
		[Obsolete("Use version that takes a 'byte' argument")]
		public void WriteByte(int arg0)
		{
			this.Write(new byte[]
			{
				(byte)arg0
			});
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x000B5700 File Offset: 0x000B4700
		public override void WriteByte(byte b)
		{
			this.Write(new byte[]
			{
				b
			});
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x000B571F File Offset: 0x000B471F
		public override void Close()
		{
			this.handler.Close();
			base.Close();
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x000B5732 File Offset: 0x000B4732
		public override void Flush()
		{
			this.handler.Flush();
		}

		// Token: 0x040014FD RID: 5373
		private readonly TlsProtocolHandler handler;
	}
}
