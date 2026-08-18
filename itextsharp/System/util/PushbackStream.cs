using System;
using System.IO;

namespace System.util
{
	// Token: 0x020002E0 RID: 736
	public class PushbackStream : FilterStream
	{
		// Token: 0x06001B51 RID: 6993 RVA: 0x000A4C7A File Offset: 0x000A3C7A
		public PushbackStream(Stream s) : base(s)
		{
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x000A4C8C File Offset: 0x000A3C8C
		public override int ReadByte()
		{
			if (this.buf != -1)
			{
				int result = this.buf;
				this.buf = -1;
				return result;
			}
			return base.ReadByte();
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x000A4CB8 File Offset: 0x000A3CB8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.buf != -1 && count > 0)
			{
				buffer[offset] = (byte)this.buf;
				this.buf = -1;
				return 1;
			}
			return base.Read(buffer, offset, count);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x000A4CE3 File Offset: 0x000A3CE3
		public virtual void Unread(int b)
		{
			if (this.buf != -1)
			{
				throw new InvalidOperationException("Can only push back one byte");
			}
			this.buf = (b & 255);
		}

		// Token: 0x040012EF RID: 4847
		private int buf = -1;
	}
}
