using System;
using System.IO;
using Org.BouncyCastle.Asn1.Utilities;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200010A RID: 266
	public class WrappedGeneratorStream : FilterStream
	{
		// Token: 0x06000A58 RID: 2648 RVA: 0x00037004 File Offset: 0x00036004
		public WrappedGeneratorStream(IStreamGenerator gen, Stream str) : base(str)
		{
			this.gen = gen;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00037014 File Offset: 0x00036014
		public override void Close()
		{
			this.gen.Close();
		}

		// Token: 0x04000855 RID: 2133
		private readonly IStreamGenerator gen;
	}
}
