using System;

namespace Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x0200031B RID: 795
	public class PkiFailureInfo : DerBitString
	{
		// Token: 0x06001CE6 RID: 7398 RVA: 0x000AC16D File Offset: 0x000AB16D
		public PkiFailureInfo(int info) : base(DerBitString.GetBytes(info), DerBitString.GetPadBits(info))
		{
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x000AC181 File Offset: 0x000AB181
		public PkiFailureInfo(DerBitString info) : base(info.GetBytes(), info.PadBits)
		{
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x000AC198 File Offset: 0x000AB198
		public override string ToString()
		{
			return "PkiFailureInfo: 0x" + base.IntValue.ToString("X");
		}

		// Token: 0x040013E4 RID: 5092
		public const int BadAlg = 128;

		// Token: 0x040013E5 RID: 5093
		public const int BadMessageCheck = 64;

		// Token: 0x040013E6 RID: 5094
		public const int BadRequest = 32;

		// Token: 0x040013E7 RID: 5095
		public const int BadTime = 16;

		// Token: 0x040013E8 RID: 5096
		public const int BadCertId = 8;

		// Token: 0x040013E9 RID: 5097
		public const int BadDataFormat = 4;

		// Token: 0x040013EA RID: 5098
		public const int WrongAuthority = 2;

		// Token: 0x040013EB RID: 5099
		public const int IncorrectData = 1;

		// Token: 0x040013EC RID: 5100
		public const int MissingTimeStamp = 32768;

		// Token: 0x040013ED RID: 5101
		public const int BadPop = 16384;

		// Token: 0x040013EE RID: 5102
		public const int TimeNotAvailable = 512;

		// Token: 0x040013EF RID: 5103
		public const int UnacceptedPolicy = 256;

		// Token: 0x040013F0 RID: 5104
		public const int UnacceptedExtension = 8388608;

		// Token: 0x040013F1 RID: 5105
		public const int AddInfoNotAvailable = 4194304;

		// Token: 0x040013F2 RID: 5106
		public const int SystemFailure = 1073741824;
	}
}
