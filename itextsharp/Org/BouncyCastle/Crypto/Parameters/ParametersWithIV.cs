using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020002A1 RID: 673
	public class ParametersWithIV : ICipherParameters
	{
		// Token: 0x06001964 RID: 6500 RVA: 0x0009419F File Offset: 0x0009319F
		public ParametersWithIV(ICipherParameters parameters, byte[] iv) : this(parameters, iv, 0, iv.Length)
		{
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x000941B0 File Offset: 0x000931B0
		public ParametersWithIV(ICipherParameters parameters, byte[] iv, int ivOff, int ivLen)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			if (iv == null)
			{
				throw new ArgumentNullException("iv");
			}
			this.parameters = parameters;
			this.iv = new byte[ivLen];
			Array.Copy(iv, ivOff, this.iv, 0, ivLen);
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00094203 File Offset: 0x00093203
		public byte[] GetIV()
		{
			return (byte[])this.iv.Clone();
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x00094215 File Offset: 0x00093215
		public ICipherParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x040010FF RID: 4351
		private readonly ICipherParameters parameters;

		// Token: 0x04001100 RID: 4352
		private readonly byte[] iv;
	}
}
