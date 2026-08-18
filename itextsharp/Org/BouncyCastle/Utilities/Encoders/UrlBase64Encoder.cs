using System;

namespace Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x02000102 RID: 258
	public class UrlBase64Encoder : Base64Encoder
	{
		// Token: 0x06000A34 RID: 2612 RVA: 0x000346AD File Offset: 0x000336AD
		public UrlBase64Encoder()
		{
			this.encodingTable[this.encodingTable.Length - 2] = 45;
			this.encodingTable[this.encodingTable.Length - 1] = 95;
			this.padding = 46;
			base.InitialiseDecodingTable();
		}
	}
}
