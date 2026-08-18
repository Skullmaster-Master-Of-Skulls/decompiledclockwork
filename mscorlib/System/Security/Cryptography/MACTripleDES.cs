using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200088D RID: 2189
	[ComVisible(true)]
	public class MACTripleDES : KeyedHashAlgorithm
	{
		// Token: 0x06004FAC RID: 20396 RVA: 0x001152BC File Offset: 0x001142BC
		public MACTripleDES()
		{
			this.KeyValue = new byte[24];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
			this.des = TripleDES.Create();
			this.HashSizeValue = this.des.BlockSize;
			this.m_bytesPerBlock = this.des.BlockSize / 8;
			this.des.IV = new byte[this.m_bytesPerBlock];
			this.des.Padding = PaddingMode.Zeros;
			this.m_encryptor = null;
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x00115344 File Offset: 0x00114344
		public MACTripleDES(byte[] rgbKey) : this("System.Security.Cryptography.TripleDES", rgbKey)
		{
		}

		// Token: 0x06004FAE RID: 20398 RVA: 0x00115354 File Offset: 0x00114354
		public MACTripleDES(string strTripleDES, byte[] rgbKey)
		{
			if (rgbKey == null)
			{
				throw new ArgumentNullException("rgbKey");
			}
			if (strTripleDES == null)
			{
				this.des = TripleDES.Create();
			}
			else
			{
				this.des = TripleDES.Create(strTripleDES);
			}
			this.HashSizeValue = this.des.BlockSize;
			this.KeyValue = (byte[])rgbKey.Clone();
			this.m_bytesPerBlock = this.des.BlockSize / 8;
			this.des.IV = new byte[this.m_bytesPerBlock];
			this.des.Padding = PaddingMode.Zeros;
			this.m_encryptor = null;
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x001153EF File Offset: 0x001143EF
		public override void Initialize()
		{
			this.m_encryptor = null;
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06004FB0 RID: 20400 RVA: 0x001153F8 File Offset: 0x001143F8
		// (set) Token: 0x06004FB1 RID: 20401 RVA: 0x00115405 File Offset: 0x00114405
		[ComVisible(false)]
		public PaddingMode Padding
		{
			get
			{
				return this.des.Padding;
			}
			set
			{
				if (value < PaddingMode.None || PaddingMode.ISO10126 < value)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidPaddingMode"));
				}
				this.des.Padding = value;
			}
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x0011542C File Offset: 0x0011442C
		protected override void HashCore(byte[] rgbData, int ibStart, int cbSize)
		{
			if (this.m_encryptor == null)
			{
				this.des.Key = this.Key;
				this.m_encryptor = this.des.CreateEncryptor();
				this._ts = new TailStream(this.des.BlockSize / 8);
				this._cs = new CryptoStream(this._ts, this.m_encryptor, CryptoStreamMode.Write);
			}
			this._cs.Write(rgbData, ibStart, cbSize);
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x001154A4 File Offset: 0x001144A4
		protected override byte[] HashFinal()
		{
			if (this.m_encryptor == null)
			{
				this.des.Key = this.Key;
				this.m_encryptor = this.des.CreateEncryptor();
				this._ts = new TailStream(this.des.BlockSize / 8);
				this._cs = new CryptoStream(this._ts, this.m_encryptor, CryptoStreamMode.Write);
			}
			this._cs.FlushFinalBlock();
			return this._ts.Buffer;
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x00115524 File Offset: 0x00114524
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.des != null)
				{
					this.des.Clear();
				}
				if (this.m_encryptor != null)
				{
					this.m_encryptor.Dispose();
				}
				if (this._cs != null)
				{
					this._cs.Clear();
				}
				if (this._ts != null)
				{
					this._ts.Clear();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400290E RID: 10510
		private const int m_bitsPerByte = 8;

		// Token: 0x0400290F RID: 10511
		private ICryptoTransform m_encryptor;

		// Token: 0x04002910 RID: 10512
		private CryptoStream _cs;

		// Token: 0x04002911 RID: 10513
		private TailStream _ts;

		// Token: 0x04002912 RID: 10514
		private int m_bytesPerBlock;

		// Token: 0x04002913 RID: 10515
		private TripleDES des;
	}
}
