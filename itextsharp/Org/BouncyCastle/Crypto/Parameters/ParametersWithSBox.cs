using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200042D RID: 1069
	public class ParametersWithSBox : ICipherParameters
	{
		// Token: 0x06002467 RID: 9319 RVA: 0x000DE095 File Offset: 0x000DD095
		public ParametersWithSBox(ICipherParameters parameters, byte[] sBox)
		{
			this.parameters = parameters;
			this.sBox = sBox;
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000DE0AB File Offset: 0x000DD0AB
		public byte[] GetSBox()
		{
			return this.sBox;
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002469 RID: 9321 RVA: 0x000DE0B3 File Offset: 0x000DD0B3
		public ICipherParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04001979 RID: 6521
		private ICipherParameters parameters;

		// Token: 0x0400197A RID: 6522
		private byte[] sBox;
	}
}
