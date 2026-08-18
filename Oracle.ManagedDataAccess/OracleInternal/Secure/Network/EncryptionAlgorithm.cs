using System;

namespace OracleInternal.Secure.Network
{
	// Token: 0x02000345 RID: 837
	public abstract class EncryptionAlgorithm
	{
		// Token: 0x06001D67 RID: 7527
		public abstract void init(byte[] key, byte[] iv);

		// Token: 0x06001D68 RID: 7528
		public abstract byte[] decrypt(byte[] ebuf);

		// Token: 0x06001D69 RID: 7529
		public abstract byte[] decrypt(byte[] ebuf, int length);

		// Token: 0x06001D6A RID: 7530
		public abstract byte[] encrypt(byte[] buffer);

		// Token: 0x06001D6B RID: 7531
		public abstract byte[] encrypt(byte[] buffer, int length);

		// Token: 0x06001D6C RID: 7532
		public abstract int maxDelta();

		// Token: 0x06001D6D RID: 7533
		public abstract void setSessionKey(byte[] key, byte[] iv);

		// Token: 0x04001FB0 RID: 8112
		internal const int NAE_40_KEY_SIZE_BITS = 40;

		// Token: 0x04001FB1 RID: 8113
		internal const int NAE_56_KEY_SIZE_BITS = 56;

		// Token: 0x04001FB2 RID: 8114
		internal const int NAE_128_KEY_SIZE_BITS = 128;

		// Token: 0x04001FB3 RID: 8115
		internal const int NAE_256_KEY_SIZE_BITS = 256;

		// Token: 0x04001FB4 RID: 8116
		internal const int NAE_CBC_0 = 1;

		// Token: 0x04001FB5 RID: 8117
		internal const int NAE_CBC_8 = 2;

		// Token: 0x04001FB6 RID: 8118
		internal const int NAE_RAW_0 = 3;

		// Token: 0x04001FB7 RID: 8119
		internal const int NAE_RAW_8 = 4;

		// Token: 0x04001FB8 RID: 8120
		internal const int \u0001 = 0;

		// Token: 0x04001FB9 RID: 8121
		internal const int RC4_40 = 140;

		// Token: 0x04001FBA RID: 8122
		internal const int RC4_56 = 141;

		// Token: 0x04001FBB RID: 8123
		internal const int RC4_128 = 142;

		// Token: 0x04001FBC RID: 8124
		internal const int RC4_256 = 143;

		// Token: 0x04001FBD RID: 8125
		internal const int DES_40_RAW_0 = 210;

		// Token: 0x04001FBE RID: 8126
		internal const int DES_56_RAW_0 = 211;

		// Token: 0x04001FBF RID: 8127
		internal const int DES_40_RAW_8 = 212;

		// Token: 0x04001FC0 RID: 8128
		internal const int DES_56_RAW_8 = 213;

		// Token: 0x04001FC1 RID: 8129
		internal const int DES_40_CBC_0 = 220;

		// Token: 0x04001FC2 RID: 8130
		internal const int DES_56_CBC_0 = 221;

		// Token: 0x04001FC3 RID: 8131
		internal const int DES_40_CBC_8 = 222;

		// Token: 0x04001FC4 RID: 8132
		internal const int DES_56_CBC_8 = 223;
	}
}
