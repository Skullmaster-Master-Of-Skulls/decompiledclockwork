using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x0200003D RID: 61
	internal abstract class LimitedInputStream : BaseInputStream
	{
		// Token: 0x06000197 RID: 407 RVA: 0x00009551 File Offset: 0x00008551
		internal LimitedInputStream(Stream inStream)
		{
			this._in = inStream;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009560 File Offset: 0x00008560
		protected virtual void SetParentEofDetect(bool on)
		{
			if (this._in is IndefiniteLengthInputStream)
			{
				((IndefiniteLengthInputStream)this._in).SetEofOn00(on);
			}
		}

		// Token: 0x040000BA RID: 186
		protected readonly Stream _in;
	}
}
