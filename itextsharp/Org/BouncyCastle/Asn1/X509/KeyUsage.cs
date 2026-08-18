using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001AA RID: 426
	public class KeyUsage : DerBitString
	{
		// Token: 0x06001039 RID: 4153 RVA: 0x0005DAC5 File Offset: 0x0005CAC5
		public new static KeyUsage GetInstance(object obj)
		{
			if (obj is KeyUsage)
			{
				return (KeyUsage)obj;
			}
			if (obj is X509Extension)
			{
				return KeyUsage.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			return new KeyUsage(DerBitString.GetInstance(obj));
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0005DAFA File Offset: 0x0005CAFA
		public KeyUsage(int usage) : base(DerBitString.GetBytes(usage), DerBitString.GetPadBits(usage))
		{
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0005DB0E File Offset: 0x0005CB0E
		private KeyUsage(DerBitString usage) : base(usage.GetBytes(), usage.PadBits)
		{
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0005DB24 File Offset: 0x0005CB24
		public override string ToString()
		{
			byte[] bytes = base.GetBytes();
			if (bytes.Length == 1)
			{
				return "KeyUsage: 0x" + ((int)(bytes[0] & byte.MaxValue)).ToString("X");
			}
			return "KeyUsage: 0x" + ((int)(bytes[1] & byte.MaxValue) << 8 | (int)(bytes[0] & byte.MaxValue)).ToString("X");
		}

		// Token: 0x04000BEB RID: 3051
		public const int DigitalSignature = 128;

		// Token: 0x04000BEC RID: 3052
		public const int NonRepudiation = 64;

		// Token: 0x04000BED RID: 3053
		public const int KeyEncipherment = 32;

		// Token: 0x04000BEE RID: 3054
		public const int DataEncipherment = 16;

		// Token: 0x04000BEF RID: 3055
		public const int KeyAgreement = 8;

		// Token: 0x04000BF0 RID: 3056
		public const int KeyCertSign = 4;

		// Token: 0x04000BF1 RID: 3057
		public const int CrlSign = 2;

		// Token: 0x04000BF2 RID: 3058
		public const int EncipherOnly = 1;

		// Token: 0x04000BF3 RID: 3059
		public const int DecipherOnly = 32768;
	}
}
