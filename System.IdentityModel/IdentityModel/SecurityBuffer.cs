using System;
using System.Runtime.InteropServices;
using System.Security.Authentication.ExtendedProtection;

namespace System.IdentityModel
{
	// Token: 0x02000093 RID: 147
	internal class SecurityBuffer
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x00011F7A File Offset: 0x0001017A
		public SecurityBuffer(byte[] data, int offset, int size, BufferType tokentype)
		{
			this.offset = offset;
			this.size = ((data == null) ? 0 : size);
			this.type = tokentype;
			this.token = data;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00011FA5 File Offset: 0x000101A5
		public SecurityBuffer(byte[] data, BufferType tokentype)
		{
			this.size = ((data == null) ? 0 : data.Length);
			this.type = tokentype;
			this.token = data;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00011FCA File Offset: 0x000101CA
		public SecurityBuffer(int size, BufferType tokentype)
		{
			this.size = size;
			this.type = tokentype;
			this.token = ((size == 0) ? null : DiagnosticUtility.Utility.AllocateByteArray(size));
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00011FF7 File Offset: 0x000101F7
		public SecurityBuffer(ChannelBinding channelBinding)
		{
			this.size = channelBinding.Size;
			this.type = BufferType.ChannelBindings;
			this.unmanagedToken = channelBinding;
		}

		// Token: 0x0400044D RID: 1101
		public int size;

		// Token: 0x0400044E RID: 1102
		public BufferType type;

		// Token: 0x0400044F RID: 1103
		public byte[] token;

		// Token: 0x04000450 RID: 1104
		public int offset;

		// Token: 0x04000451 RID: 1105
		public SafeHandle unmanagedToken;
	}
}
