using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000265 RID: 613
	internal class DefiniteLengthInputStream : LimitedInputStream
	{
		// Token: 0x06001725 RID: 5925 RVA: 0x000855D2 File Offset: 0x000845D2
		internal DefiniteLengthInputStream(Stream inStream, int length) : base(inStream)
		{
			if (length < 0)
			{
				throw new ArgumentException("negative lengths not allowed", "length");
			}
			this._originalLength = length;
			this._remaining = length;
			if (length == 0)
			{
				this.SetParentEofDetect(true);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x00085607 File Offset: 0x00084607
		internal int Remaining
		{
			get
			{
				return this._remaining;
			}
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00085610 File Offset: 0x00084610
		public override int ReadByte()
		{
			if (this._remaining == 0)
			{
				return -1;
			}
			int num = this._in.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException(string.Concat(new object[]
				{
					"DEF length ",
					this._originalLength,
					" object truncated by ",
					this._remaining
				}));
			}
			if (--this._remaining == 0)
			{
				this.SetParentEofDetect(true);
			}
			return num;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x00085694 File Offset: 0x00084694
		public override int Read(byte[] buf, int off, int len)
		{
			if (this._remaining == 0)
			{
				return 0;
			}
			int count = Math.Min(len, this._remaining);
			int num = this._in.Read(buf, off, count);
			if (num < 1)
			{
				throw new EndOfStreamException(string.Concat(new object[]
				{
					"DEF length ",
					this._originalLength,
					" object truncated by ",
					this._remaining
				}));
			}
			if ((this._remaining -= num) == 0)
			{
				this.SetParentEofDetect(true);
			}
			return num;
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00085728 File Offset: 0x00084728
		internal byte[] ToArray()
		{
			if (this._remaining == 0)
			{
				return DefiniteLengthInputStream.EmptyBytes;
			}
			byte[] array = new byte[this._remaining];
			if ((this._remaining -= Streams.ReadFully(this._in, array)) != 0)
			{
				throw new EndOfStreamException(string.Concat(new object[]
				{
					"DEF length ",
					this._originalLength,
					" object truncated by ",
					this._remaining
				}));
			}
			this.SetParentEofDetect(true);
			return array;
		}

		// Token: 0x04000FD0 RID: 4048
		private static readonly byte[] EmptyBytes = new byte[0];

		// Token: 0x04000FD1 RID: 4049
		private readonly int _originalLength;

		// Token: 0x04000FD2 RID: 4050
		private int _remaining;
	}
}
