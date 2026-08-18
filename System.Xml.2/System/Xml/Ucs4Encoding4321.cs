using System;

namespace System.Xml
{
	// Token: 0x02000086 RID: 134
	internal class Ucs4Encoding4321 : Ucs4Encoding
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x000128B5 File Offset: 0x00010AB5
		public Ucs4Encoding4321()
		{
			this.ucs4Decoder = new Ucs4Decoder4321();
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x000128C8 File Offset: 0x00010AC8
		public override string EncodingName
		{
			get
			{
				return "ucs-4";
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000128CF File Offset: 0x00010ACF
		public override byte[] GetPreamble()
		{
			byte[] array = new byte[4];
			array[0] = byte.MaxValue;
			array[1] = 254;
			return array;
		}
	}
}
