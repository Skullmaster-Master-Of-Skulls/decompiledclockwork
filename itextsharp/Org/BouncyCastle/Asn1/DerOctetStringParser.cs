using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000315 RID: 789
	public class DerOctetStringParser : Asn1OctetStringParser, IAsn1Convertible
	{
		// Token: 0x06001CBD RID: 7357 RVA: 0x000AB99E File Offset: 0x000AA99E
		internal DerOctetStringParser(DefiniteLengthInputStream stream)
		{
			this.stream = stream;
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x000AB9AD File Offset: 0x000AA9AD
		public Stream GetOctetStream()
		{
			return this.stream;
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x000AB9B8 File Offset: 0x000AA9B8
		public Asn1Object ToAsn1Object()
		{
			Asn1Object result;
			try
			{
				result = new DerOctetString(this.stream.ToArray());
			}
			catch (IOException ex)
			{
				throw new InvalidOperationException("IOException converting stream to byte array: " + ex.Message, ex);
			}
			return result;
		}

		// Token: 0x040013D1 RID: 5073
		private readonly DefiniteLengthInputStream stream;
	}
}
