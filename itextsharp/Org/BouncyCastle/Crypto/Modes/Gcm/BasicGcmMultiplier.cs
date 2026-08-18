using System;

namespace Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x02000127 RID: 295
	public class BasicGcmMultiplier : IGcmMultiplier
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x000386CC File Offset: 0x000376CC
		public void Init(byte[] H)
		{
			this.H = (byte[])H.Clone();
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000386E0 File Offset: 0x000376E0
		public void MultiplyH(byte[] x)
		{
			byte[] array = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte b = this.H[i];
				for (int j = 7; j >= 0; j--)
				{
					if (((int)b & 1 << j) != 0)
					{
						GcmUtilities.Xor(array, x);
					}
					bool flag = (x[15] & 1) != 0;
					GcmUtilities.ShiftRight(x);
					if (flag)
					{
						int num = 0;
						x[num] ^= 225;
					}
				}
			}
			Array.Copy(array, 0, x, 0, 16);
		}

		// Token: 0x0400088A RID: 2186
		private byte[] H;
	}
}
