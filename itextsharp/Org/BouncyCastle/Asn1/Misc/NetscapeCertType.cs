using System;

namespace Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x0200025E RID: 606
	public class NetscapeCertType : DerBitString
	{
		// Token: 0x060016F8 RID: 5880 RVA: 0x00084D00 File Offset: 0x00083D00
		public NetscapeCertType(int usage) : base(DerBitString.GetBytes(usage), DerBitString.GetPadBits(usage))
		{
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00084D14 File Offset: 0x00083D14
		public NetscapeCertType(DerBitString usage) : base(usage.GetBytes(), usage.PadBits)
		{
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x00084D28 File Offset: 0x00083D28
		public override string ToString()
		{
			byte[] bytes = base.GetBytes();
			return "NetscapeCertType: 0x" + ((int)(bytes[0] & byte.MaxValue)).ToString("X");
		}

		// Token: 0x04000FBC RID: 4028
		public const int SslClient = 128;

		// Token: 0x04000FBD RID: 4029
		public const int SslServer = 64;

		// Token: 0x04000FBE RID: 4030
		public const int Smime = 32;

		// Token: 0x04000FBF RID: 4031
		public const int ObjectSigning = 16;

		// Token: 0x04000FC0 RID: 4032
		public const int Reserved = 8;

		// Token: 0x04000FC1 RID: 4033
		public const int SslCA = 4;

		// Token: 0x04000FC2 RID: 4034
		public const int SmimeCA = 2;

		// Token: 0x04000FC3 RID: 4035
		public const int ObjectSigningCA = 1;
	}
}
