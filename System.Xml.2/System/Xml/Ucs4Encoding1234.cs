using System;

namespace System.Xml
{
	// Token: 0x02000085 RID: 133
	internal class Ucs4Encoding1234 : Ucs4Encoding
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x00012883 File Offset: 0x00010A83
		public Ucs4Encoding1234()
		{
			this.ucs4Decoder = new Ucs4Decoder1234();
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00012896 File Offset: 0x00010A96
		public override string EncodingName
		{
			get
			{
				return "ucs-4 (Bigendian)";
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001289D File Offset: 0x00010A9D
		public override byte[] GetPreamble()
		{
			return new byte[]
			{
				0,
				0,
				254,
				byte.MaxValue
			};
		}
	}
}
