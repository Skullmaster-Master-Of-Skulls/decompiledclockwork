using System;

namespace System.Xml
{
	// Token: 0x02000088 RID: 136
	internal class Ucs4Encoding3412 : Ucs4Encoding
	{
		// Token: 0x060004F0 RID: 1264 RVA: 0x00012919 File Offset: 0x00010B19
		public Ucs4Encoding3412()
		{
			this.ucs4Decoder = new Ucs4Decoder3412();
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001292C File Offset: 0x00010B2C
		public override string EncodingName
		{
			get
			{
				return "ucs-4 (order 3412)";
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00012933 File Offset: 0x00010B33
		public override byte[] GetPreamble()
		{
			byte[] array = new byte[4];
			array[0] = 254;
			array[1] = byte.MaxValue;
			return array;
		}
	}
}
