using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace System.Web.Helpers.AntiXsrf
{
	// Token: 0x02000031 RID: 49
	[DebuggerDisplay("{DebuggerString}")]
	internal sealed class BinaryBlob : IEquatable<BinaryBlob>
	{
		// Token: 0x06000155 RID: 341 RVA: 0x00004F73 File Offset: 0x00003173
		public BinaryBlob(int bitLength) : this(bitLength, BinaryBlob.GenerateNewToken(bitLength))
		{
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00004F82 File Offset: 0x00003182
		public BinaryBlob(int bitLength, byte[] data)
		{
			if (bitLength < 32 || bitLength % 8 != 0)
			{
				throw new ArgumentOutOfRangeException("bitLength");
			}
			if (data == null || data.Length != bitLength / 8)
			{
				throw new ArgumentOutOfRangeException("data");
			}
			this._data = data;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00004FBC File Offset: 0x000031BC
		public int BitLength
		{
			get
			{
				return checked(this._data.Length * 8);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00004FC8 File Offset: 0x000031C8
		private string DebuggerString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder("0x", 2 + this._data.Length * 2);
				for (int i = 0; i < this._data.Length; i++)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0:x2}", new object[]
					{
						this._data[i]
					});
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000502E File Offset: 0x0000322E
		public override bool Equals(object obj)
		{
			return this.Equals(obj as BinaryBlob);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000503C File Offset: 0x0000323C
		public bool Equals(BinaryBlob other)
		{
			return other != null && CryptoUtil.AreByteArraysEqual(this._data, other._data);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005054 File Offset: 0x00003254
		public byte[] GetData()
		{
			return this._data;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000505C File Offset: 0x0000325C
		public override int GetHashCode()
		{
			return BitConverter.ToInt32(this._data, 0);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000506C File Offset: 0x0000326C
		private static byte[] GenerateNewToken(int bitLength)
		{
			byte[] array = new byte[bitLength / 8];
			BinaryBlob._prng.GetBytes(array);
			return array;
		}

		// Token: 0x0400006D RID: 109
		private static readonly RNGCryptoServiceProvider _prng = new RNGCryptoServiceProvider();

		// Token: 0x0400006E RID: 110
		private readonly byte[] _data;
	}
}
