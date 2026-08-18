using System;

namespace System.Xml
{
	// Token: 0x02000037 RID: 55
	internal class Ucs4Encoding4321 : Ucs4Encoding
	{
		// Token: 0x06000199 RID: 409 RVA: 0x00007A05 File Offset: 0x00006A05
		public Ucs4Encoding4321()
		{
			this.ucs4Decoder = new Ucs4Decoder4321();
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00007A18 File Offset: 0x00006A18
		public override string EncodingName
		{
			get
			{
				return "ucs-4";
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007A20 File Offset: 0x00006A20
		public override byte[] GetPreamble()
		{
			byte[] array = new byte[4];
			array[0] = byte.MaxValue;
			array[1] = 254;
			return array;
		}
	}
}
