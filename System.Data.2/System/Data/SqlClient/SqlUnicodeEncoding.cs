using System;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x020001F8 RID: 504
	internal sealed class SqlUnicodeEncoding : UnicodeEncoding
	{
		// Token: 0x06001F4F RID: 8015 RVA: 0x000D8C40 File Offset: 0x000D8040
		private SqlUnicodeEncoding() : base(false, false, false)
		{
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x000D8C58 File Offset: 0x000D8058
		public override Decoder GetDecoder()
		{
			return new SqlUnicodeEncoding.SqlUnicodeDecoder();
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x000D8C6C File Offset: 0x000D806C
		public override int GetMaxByteCount(int charCount)
		{
			return charCount * 2;
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x000D8C7C File Offset: 0x000D807C
		public static Encoding SqlUnicodeEncodingInstance
		{
			get
			{
				return SqlUnicodeEncoding._singletonEncoding;
			}
		}

		// Token: 0x040011AF RID: 4527
		private static SqlUnicodeEncoding _singletonEncoding = new SqlUnicodeEncoding();

		// Token: 0x020003D6 RID: 982
		private sealed class SqlUnicodeDecoder : Decoder
		{
			// Token: 0x06003558 RID: 13656 RVA: 0x00144FB4 File Offset: 0x001443B4
			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return count / 2;
			}

			// Token: 0x06003559 RID: 13657 RVA: 0x00144FC4 File Offset: 0x001443C4
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				int num;
				int result;
				bool flag;
				this.Convert(bytes, byteIndex, byteCount, chars, charIndex, chars.Length - charIndex, true, out num, out result, out flag);
				return result;
			}

			// Token: 0x0600355A RID: 13658 RVA: 0x00144FF0 File Offset: 0x001443F0
			public override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
			{
				charsUsed = Math.Min(charCount, byteCount / 2);
				bytesUsed = charsUsed * 2;
				completed = (bytesUsed == byteCount);
				Buffer.BlockCopy(bytes, byteIndex, chars, charIndex * 2, bytesUsed);
			}
		}
	}
}
