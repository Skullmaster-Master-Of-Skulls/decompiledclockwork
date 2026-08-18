using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x0200005E RID: 94
	[CLSCompliant(true)]
	public class Asn1Sequence : Asn1Structured
	{
		// Token: 0x06000372 RID: 882 RVA: 0x000113D4 File Offset: 0x000103D4
		public Asn1Sequence() : base(Asn1Sequence.ID, 10)
		{
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000113F0 File Offset: 0x000103F0
		public Asn1Sequence(int size) : base(Asn1Sequence.ID, size)
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001140C File Offset: 0x0001040C
		public Asn1Sequence(Asn1Object[] newContent, int size) : base(Asn1Sequence.ID, newContent, size)
		{
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00011428 File Offset: 0x00010428
		[CLSCompliant(false)]
		public Asn1Sequence(Asn1Decoder dec, Stream in_Renamed, int len) : base(Asn1Sequence.ID)
		{
			base.decodeStructured(dec, in_Renamed, len);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001144C File Offset: 0x0001044C
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SEQUENCE: { ");
		}

		// Token: 0x0400019A RID: 410
		public const int TAG = 16;

		// Token: 0x0400019B RID: 411
		private static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 16);
	}
}
