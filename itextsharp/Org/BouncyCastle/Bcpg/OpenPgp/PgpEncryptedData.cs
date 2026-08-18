using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x020001E4 RID: 484
	public abstract class PgpEncryptedData
	{
		// Token: 0x06001304 RID: 4868 RVA: 0x0006D0C6 File Offset: 0x0006C0C6
		internal PgpEncryptedData(InputStreamPacket encData)
		{
			this.encData = encData;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0006D0D5 File Offset: 0x0006C0D5
		public virtual Stream GetInputStream()
		{
			return this.encData.GetInputStream();
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0006D0E2 File Offset: 0x0006C0E2
		public bool IsIntegrityProtected()
		{
			return this.encData is SymmetricEncIntegrityPacket;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0006D0F4 File Offset: 0x0006C0F4
		public bool Verify()
		{
			if (!this.IsIntegrityProtected())
			{
				throw new PgpException("data not integrity protected.");
			}
			DigestStream digestStream = (DigestStream)this.encStream;
			while (this.encStream.ReadByte() >= 0)
			{
			}
			byte[] lookAhead = this.truncStream.GetLookAhead();
			IDigest digest = digestStream.ReadDigest();
			digest.BlockUpdate(lookAhead, 0, 2);
			byte[] array = DigestUtilities.DoFinal(digest);
			byte[] array2 = new byte[array.Length];
			Array.Copy(lookAhead, 2, array2, 0, array2.Length);
			return Arrays.ConstantTimeAreEqual(array, array2);
		}

		// Token: 0x04000D55 RID: 3413
		internal InputStreamPacket encData;

		// Token: 0x04000D56 RID: 3414
		internal Stream encStream;

		// Token: 0x04000D57 RID: 3415
		internal PgpEncryptedData.TruncatedStream truncStream;

		// Token: 0x020001E5 RID: 485
		internal class TruncatedStream : BaseInputStream
		{
			// Token: 0x06001308 RID: 4872 RVA: 0x0006D174 File Offset: 0x0006C174
			internal TruncatedStream(Stream inStr)
			{
				int num = Streams.ReadFully(inStr, this.lookAhead, 0, this.lookAhead.Length);
				if (num < 22)
				{
					throw new EndOfStreamException();
				}
				this.inStr = inStr;
				this.bufStart = 0;
				this.bufEnd = num - 22;
			}

			// Token: 0x06001309 RID: 4873 RVA: 0x0006D1D0 File Offset: 0x0006C1D0
			private int FillBuffer()
			{
				if (this.bufEnd < 490)
				{
					return 0;
				}
				Array.Copy(this.lookAhead, 490, this.lookAhead, 0, 22);
				this.bufEnd = Streams.ReadFully(this.inStr, this.lookAhead, 22, 490);
				this.bufStart = 0;
				return this.bufEnd;
			}

			// Token: 0x0600130A RID: 4874 RVA: 0x0006D230 File Offset: 0x0006C230
			public override int ReadByte()
			{
				if (this.bufStart < this.bufEnd)
				{
					return (int)this.lookAhead[this.bufStart++];
				}
				if (this.FillBuffer() < 1)
				{
					return -1;
				}
				return (int)this.lookAhead[this.bufStart++];
			}

			// Token: 0x0600130B RID: 4875 RVA: 0x0006D288 File Offset: 0x0006C288
			public override int Read(byte[] buf, int off, int len)
			{
				int num = this.bufEnd - this.bufStart;
				int num2 = off;
				while (len > num)
				{
					Array.Copy(this.lookAhead, this.bufStart, buf, num2, num);
					this.bufStart += num;
					num2 += num;
					len -= num;
					if ((num = this.FillBuffer()) < 1)
					{
						return num2 - off;
					}
				}
				Array.Copy(this.lookAhead, this.bufStart, buf, num2, len);
				this.bufStart += len;
				return num2 + len - off;
			}

			// Token: 0x0600130C RID: 4876 RVA: 0x0006D30C File Offset: 0x0006C30C
			internal byte[] GetLookAhead()
			{
				byte[] array = new byte[22];
				Array.Copy(this.lookAhead, this.bufStart, array, 0, 22);
				return array;
			}

			// Token: 0x04000D58 RID: 3416
			private const int LookAheadSize = 22;

			// Token: 0x04000D59 RID: 3417
			private const int LookAheadBufSize = 512;

			// Token: 0x04000D5A RID: 3418
			private const int LookAheadBufLimit = 490;

			// Token: 0x04000D5B RID: 3419
			private readonly Stream inStr;

			// Token: 0x04000D5C RID: 3420
			private readonly byte[] lookAhead = new byte[512];

			// Token: 0x04000D5D RID: 3421
			private int bufStart;

			// Token: 0x04000D5E RID: 3422
			private int bufEnd;
		}
	}
}
