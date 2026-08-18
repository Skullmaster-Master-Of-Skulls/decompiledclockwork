using System;

namespace Org.BouncyCastle.Bcpg.Sig
{
	// Token: 0x020002B1 RID: 689
	public class KeyFlags : SignatureSubpacket
	{
		// Token: 0x06001A16 RID: 6678 RVA: 0x0009AD28 File Offset: 0x00099D28
		private static byte[] IntToByteArray(int v)
		{
			byte[] array = new byte[4];
			int num = 0;
			for (int num2 = 0; num2 != 4; num2++)
			{
				array[num2] = (byte)(v >> num2 * 8);
				if (array[num2] != 0)
				{
					num = num2;
				}
			}
			byte[] array2 = new byte[num + 1];
			Array.Copy(array, 0, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0009AD73 File Offset: 0x00099D73
		public KeyFlags(bool critical, byte[] data) : base(SignatureSubpacketTag.KeyFlags, critical, data)
		{
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0009AD7F File Offset: 0x00099D7F
		public KeyFlags(bool critical, int flags) : base(SignatureSubpacketTag.KeyFlags, critical, KeyFlags.IntToByteArray(flags))
		{
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001A19 RID: 6681 RVA: 0x0009AD90 File Offset: 0x00099D90
		public int Flags
		{
			get
			{
				int num = 0;
				for (int num2 = 0; num2 != this.data.Length; num2++)
				{
					num |= (int)(this.data[num2] & byte.MaxValue) << num2 * 8;
				}
				return num;
			}
		}

		// Token: 0x0400115E RID: 4446
		public const int CertifyOther = 1;

		// Token: 0x0400115F RID: 4447
		public const int SignData = 2;

		// Token: 0x04001160 RID: 4448
		public const int EncryptComms = 4;

		// Token: 0x04001161 RID: 4449
		public const int EncryptStorage = 8;

		// Token: 0x04001162 RID: 4450
		public const int Split = 16;

		// Token: 0x04001163 RID: 4451
		public const int Authentication = 32;

		// Token: 0x04001164 RID: 4452
		public const int Shared = 128;
	}
}
