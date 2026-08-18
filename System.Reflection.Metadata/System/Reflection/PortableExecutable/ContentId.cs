using System;
using System.Collections.Immutable;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000011 RID: 17
	internal struct ContentId
	{
		// Token: 0x0600010A RID: 266 RVA: 0x0000450A File Offset: 0x0000270A
		public ContentId(Guid guid, int stamp)
		{
			this = new ContentId(guid.ToByteArray(), BitConverter.GetBytes(stamp));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000451F File Offset: 0x0000271F
		public ContentId(byte[] guid, byte[] stamp)
		{
			this.Guid = guid;
			this.Stamp = stamp;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000452F File Offset: 0x0000272F
		public bool IsDefault
		{
			get
			{
				return this.Guid == null && this.Stamp == null;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004544 File Offset: 0x00002744
		internal static ContentId FromHash(ImmutableArray<byte> hashCode)
		{
			byte[] array = new byte[16];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = hashCode[i];
			}
			byte b = array[7];
			b = ((b & 15) | 64);
			array[7] = b;
			b = array[8];
			b = ((b & 63) | 128);
			array[8] = b;
			return new ContentId(array, new byte[]
			{
				hashCode[16],
				hashCode[17],
				hashCode[18],
				hashCode[19] | 128
			});
		}

		// Token: 0x0400004F RID: 79
		public const int Size = 20;

		// Token: 0x04000050 RID: 80
		public readonly byte[] Guid;

		// Token: 0x04000051 RID: 81
		public readonly byte[] Stamp;
	}
}
