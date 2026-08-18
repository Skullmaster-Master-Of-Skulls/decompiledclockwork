using System;
using System.IO;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000393 RID: 915
	public class BerOctetStringParser : Asn1OctetStringParser, IAsn1Convertible
	{
		// Token: 0x06001FCD RID: 8141 RVA: 0x000BD18A File Offset: 0x000BC18A
		internal BerOctetStringParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x000BD199 File Offset: 0x000BC199
		public Stream GetOctetStream()
		{
			return new ConstructedOctetStream(this._parser);
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x000BD1A8 File Offset: 0x000BC1A8
		public Asn1Object ToAsn1Object()
		{
			Asn1Object result;
			try
			{
				result = new BerOctetString(Streams.ReadAll(this.GetOctetStream()));
			}
			catch (IOException ex)
			{
				throw new InvalidOperationException("IOException converting stream to byte array: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x040015F0 RID: 5616
		private readonly Asn1StreamParser _parser;
	}
}
