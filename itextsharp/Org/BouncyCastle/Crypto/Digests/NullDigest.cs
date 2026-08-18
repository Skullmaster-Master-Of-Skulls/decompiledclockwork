using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Digests
{
	// Token: 0x020004CB RID: 1227
	public class NullDigest : IDigest
	{
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x000FF9E0 File Offset: 0x000FE9E0
		public string AlgorithmName
		{
			get
			{
				return "NULL";
			}
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000FF9E7 File Offset: 0x000FE9E7
		public int GetByteLength()
		{
			return 0;
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000FF9EA File Offset: 0x000FE9EA
		public int GetDigestSize()
		{
			return (int)this.bOut.Length;
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000FF9F8 File Offset: 0x000FE9F8
		public void Update(byte b)
		{
			this.bOut.WriteByte(b);
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000FFA06 File Offset: 0x000FEA06
		public void BlockUpdate(byte[] inBytes, int inOff, int len)
		{
			this.bOut.Write(inBytes, inOff, len);
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000FFA18 File Offset: 0x000FEA18
		public int DoFinal(byte[] outBytes, int outOff)
		{
			byte[] array = this.bOut.ToArray();
			array.CopyTo(outBytes, outOff);
			this.Reset();
			return array.Length;
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000FFA42 File Offset: 0x000FEA42
		public void Reset()
		{
			this.bOut.SetLength(0L);
		}

		// Token: 0x04001D28 RID: 7464
		private readonly MemoryStream bOut = new MemoryStream();
	}
}
