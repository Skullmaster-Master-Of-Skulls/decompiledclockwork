using System;

namespace System.Xml
{
	// Token: 0x02000087 RID: 135
	internal class Ucs4Encoding2143 : Ucs4Encoding
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x000128E7 File Offset: 0x00010AE7
		public Ucs4Encoding2143()
		{
			this.ucs4Decoder = new Ucs4Decoder2143();
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x000128FA File Offset: 0x00010AFA
		public override string EncodingName
		{
			get
			{
				return "ucs-4 (order 2143)";
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00012901 File Offset: 0x00010B01
		public override byte[] GetPreamble()
		{
			return new byte[]
			{
				0,
				0,
				byte.MaxValue,
				254
			};
		}
	}
}
