using System;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x02000394 RID: 916
	public class BerApplicationSpecificParser : IAsn1ApplicationSpecificParser, IAsn1Convertible
	{
		// Token: 0x06001FD0 RID: 8144 RVA: 0x000BD1F4 File Offset: 0x000BC1F4
		internal BerApplicationSpecificParser(int tag, Asn1StreamParser parser)
		{
			this.tag = tag;
			this.parser = parser;
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x000BD20A File Offset: 0x000BC20A
		public IAsn1Convertible ReadObject()
		{
			return this.parser.ReadObject();
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x000BD217 File Offset: 0x000BC217
		public Asn1Object ToAsn1Object()
		{
			return new BerApplicationSpecific(this.tag, this.parser.ReadVector());
		}

		// Token: 0x040015F1 RID: 5617
		private readonly int tag;

		// Token: 0x040015F2 RID: 5618
		private readonly Asn1StreamParser parser;
	}
}
