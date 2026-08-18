using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020002B9 RID: 697
	public class LazyAsn1InputStream : Asn1InputStream
	{
		// Token: 0x06001A4A RID: 6730 RVA: 0x0009BA0C File Offset: 0x0009AA0C
		public LazyAsn1InputStream(byte[] input) : base(input)
		{
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0009BA15 File Offset: 0x0009AA15
		public LazyAsn1InputStream(Stream inputStream) : base(inputStream)
		{
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0009BA1E File Offset: 0x0009AA1E
		internal override DerSequence CreateDerSequence(DefiniteLengthInputStream dIn)
		{
			return new LazyDerSequence(dIn.ToArray());
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0009BA2B File Offset: 0x0009AA2B
		internal override DerSet CreateDerSet(DefiniteLengthInputStream dIn)
		{
			return new LazyDerSet(dIn.ToArray());
		}
	}
}
