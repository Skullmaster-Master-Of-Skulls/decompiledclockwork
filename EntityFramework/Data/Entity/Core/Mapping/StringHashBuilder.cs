using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003A8 RID: 936
	internal class StringHashBuilder
	{
		// Token: 0x06002211 RID: 8721 RVA: 0x0009F32D File Offset: 0x0009D52D
		internal StringHashBuilder(HashAlgorithm hashAlgorithm)
		{
			this._hashAlgorithm = hashAlgorithm;
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x0009F347 File Offset: 0x0009D547
		internal StringHashBuilder(HashAlgorithm hashAlgorithm, int startingBufferSize) : this(hashAlgorithm)
		{
			this._cachedBuffer = new byte[startingBufferSize];
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x0009F35C File Offset: 0x0009D55C
		internal int CharCount
		{
			get
			{
				return this._totalLength;
			}
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x0009F364 File Offset: 0x0009D564
		internal virtual void Append(string s)
		{
			this.InternalAppend(s);
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0009F36D File Offset: 0x0009D56D
		internal virtual void AppendLine(string s)
		{
			this.InternalAppend(s);
			this.InternalAppend("\n");
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x0009F381 File Offset: 0x0009D581
		private void InternalAppend(string s)
		{
			if (s.Length == 0)
			{
				return;
			}
			this._strings.Add(s);
			this._totalLength += s.Length;
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x0009F3AC File Offset: 0x0009D5AC
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

		// Token: 0x06002218 RID: 8728 RVA: 0x0009F484 File Offset: 0x0009D684
		internal void Clear()
		{
			this._strings.Clear();
			this._totalLength = 0;
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x0009F4B0 File Offset: 0x0009D6B0
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			this._strings.Each((string s) => builder.Append(s));
			return builder.ToString();
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0009F4F0 File Offset: 0x0009D6F0
		private int GetByteCount()
		{
			int num = 0;
			foreach (string s in this._strings)
			{
				num += Encoding.Unicode.GetByteCount(s);
			}
			return num;
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x0009F550 File Offset: 0x0009D750
		private static string ConvertHashToString(byte[] hash)
		{
			StringBuilder stringBuilder = new StringBuilder(hash.Length * 2);
			for (int i = 0; i < hash.Length; i++)
			{
				stringBuilder.Append(hash[i].ToString("x2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x0009F59C File Offset: 0x0009D79C
		public static string ComputeHash(HashAlgorithm hashAlgorithm, string source)
		{
			StringHashBuilder stringHashBuilder = new StringHashBuilder(hashAlgorithm);
			stringHashBuilder.Append(source);
			return stringHashBuilder.ComputeHash();
		}

		// Token: 0x04000C04 RID: 3076
		private const string NewLine = "\n";

		// Token: 0x04000C05 RID: 3077
		private readonly HashAlgorithm _hashAlgorithm;

		// Token: 0x04000C06 RID: 3078
		private readonly List<string> _strings = new List<string>();

		// Token: 0x04000C07 RID: 3079
		private int _totalLength;

		// Token: 0x04000C08 RID: 3080
		private byte[] _cachedBuffer;
	}
}
