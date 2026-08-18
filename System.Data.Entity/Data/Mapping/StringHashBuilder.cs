using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace System.Data.Mapping
{
	// Token: 0x02000256 RID: 598
	internal class StringHashBuilder
	{
		// Token: 0x06002574 RID: 9588 RVA: 0x0008BAF6 File Offset: 0x00089CF6
		internal StringHashBuilder(HashAlgorithm hashAlgorithm)
		{
			this._hashAlgorithm = hashAlgorithm;
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x0008BB10 File Offset: 0x00089D10
		internal StringHashBuilder(HashAlgorithm hashAlgorithm, int startingBufferSize) : this(hashAlgorithm)
		{
			this._cachedBuffer = new byte[startingBufferSize];
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x0008BB25 File Offset: 0x00089D25
		internal int CharCount
		{
			get
			{
				return this._totalLength;
			}
		}

		// Token: 0x06002577 RID: 9591 RVA: 0x0008BB2D File Offset: 0x00089D2D
		internal virtual void Append(string s)
		{
			this.InternalAppend(s);
		}

		// Token: 0x06002578 RID: 9592 RVA: 0x0008BB36 File Offset: 0x00089D36
		internal virtual void AppendLine(string s)
		{
			this.InternalAppend(s);
			this.InternalAppend("\n");
		}

		// Token: 0x06002579 RID: 9593 RVA: 0x0008BB4A File Offset: 0x00089D4A
		private void InternalAppend(string s)
		{
			if (s.Length == 0)
			{
				return;
			}
			this._strings.Add(s);
			this._totalLength += s.Length;
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x0008BB74 File Offset: 0x00089D74
		internal string ComputeHash()
		{
			int byteCount = this.GetByteCount();
			if (this._cachedBuffer == null)
			{
				this._cachedBuffer = new byte[byteCount];
			}
			else if (this._cachedBuffer.Length < byteCount)
			{
				int num = Math.Max(this._cachedBuffer.Length + this._cachedBuffer.Length / 2, byteCount);
				this._cachedBuffer = new byte[num];
			}
			int num2 = 0;
			foreach (string text in this._strings)
			{
				num2 += Encoding.Unicode.GetBytes(text, 0, text.Length, this._cachedBuffer, num2);
			}
			byte[] hash = this._hashAlgorithm.ComputeHash(this._cachedBuffer, 0, byteCount);
			return StringHashBuilder.ConvertHashToString(hash);
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x0008BC4C File Offset: 0x00089E4C
		internal void Clear()
		{
			this._strings.Clear();
			this._totalLength = 0;
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x0008BC60 File Offset: 0x00089E60
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			this._strings.ForEach(delegate(string s)
			{
				builder.Append(s);
			});
			return builder.ToString();
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x0008BCA0 File Offset: 0x00089EA0
		private int GetByteCount()
		{
			int num = 0;
			foreach (string s in this._strings)
			{
				num += Encoding.Unicode.GetByteCount(s);
			}
			return num;
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x0008BD00 File Offset: 0x00089F00
		private static string ConvertHashToString(byte[] hash)
		{
			StringBuilder stringBuilder = new StringBuilder(hash.Length * 2);
			for (int i = 0; i < hash.Length; i++)
			{
				stringBuilder.Append(hash[i].ToString("x2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x0008BD4C File Offset: 0x00089F4C
		public static string ComputeHash(HashAlgorithm hashAlgorithm, string source)
		{
			StringHashBuilder stringHashBuilder = new StringHashBuilder(hashAlgorithm);
			stringHashBuilder.Append(source);
			return stringHashBuilder.ComputeHash();
		}

		// Token: 0x04001122 RID: 4386
		private HashAlgorithm _hashAlgorithm;

		// Token: 0x04001123 RID: 4387
		private const string NewLine = "\n";

		// Token: 0x04001124 RID: 4388
		private List<string> _strings = new List<string>();

		// Token: 0x04001125 RID: 4389
		private int _totalLength;

		// Token: 0x04001126 RID: 4390
		private byte[] _cachedBuffer;
	}
}
