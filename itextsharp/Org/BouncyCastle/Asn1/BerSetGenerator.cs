using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020000B3 RID: 179
	public class BerSetGenerator : BerGenerator
	{
		// Token: 0x0600059B RID: 1435 RVA: 0x0001CF61 File Offset: 0x0001BF61
		public BerSetGenerator(Stream outStream) : base(outStream)
		{
			base.WriteBerHeader(49);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001CF72 File Offset: 0x0001BF72
		public BerSetGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream, tagNo, isExplicit)
		{
			base.WriteBerHeader(49);
		}
	}
}
