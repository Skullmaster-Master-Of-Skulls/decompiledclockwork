using System;
using System.IO;

namespace Org.BouncyCastle.Asn1
{
	// Token: 0x020004DA RID: 1242
	public class BerSequenceGenerator : BerGenerator
	{
		// Token: 0x06002A46 RID: 10822 RVA: 0x0010096E File Offset: 0x000FF96E
		public BerSequenceGenerator(Stream outStream) : base(outStream)
		{
			base.WriteBerHeader(48);
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x0010097F File Offset: 0x000FF97F
		public BerSequenceGenerator(Stream outStream, int tagNo, bool isExplicit) : base(outStream, tagNo, isExplicit)
		{
			base.WriteBerHeader(48);
		}
	}
}
