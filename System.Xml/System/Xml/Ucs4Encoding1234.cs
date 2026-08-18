using System;

namespace System.Xml
{
	// Token: 0x02000036 RID: 54
	internal class Ucs4Encoding1234 : Ucs4Encoding
	{
		// Token: 0x06000196 RID: 406 RVA: 0x000079C3 File Offset: 0x000069C3
		public Ucs4Encoding1234()
		{
			this.ucs4Decoder = new Ucs4Decoder1234();
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000079D6 File Offset: 0x000069D6
		public override string EncodingName
		{
			get
			{
				return "ucs-4 (Bigendian)";
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000079E0 File Offset: 0x000069E0
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
