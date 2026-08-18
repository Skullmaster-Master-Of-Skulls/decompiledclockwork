using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x0200005F RID: 95
	[CLSCompliant(true)]
	public class Asn1SequenceOf : Asn1Structured
	{
		// Token: 0x06000378 RID: 888 RVA: 0x00011484 File Offset: 0x00010484
		public Asn1SequenceOf() : base(Asn1SequenceOf.ID)
		{
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000114A0 File Offset: 0x000104A0
		public Asn1SequenceOf(int size) : base(Asn1SequenceOf.ID, size)
		{
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000114BC File Offset: 0x000104BC
		public Asn1SequenceOf(Asn1Sequence sequence) : base(Asn1SequenceOf.ID, sequence.toArray(), sequence.size())
		{
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000114E4 File Offset: 0x000104E4
		[CLSCompliant(false)]
		public Asn1SequenceOf(Asn1Decoder dec, Stream in_Renamed, int len) : base(Asn1SequenceOf.ID)
		{
			base.decodeStructured(dec, in_Renamed, len);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00011508 File Offset: 0x00010508
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SEQUENCE OF: { ");
		}

		// Token: 0x0400019C RID: 412
		public const int TAG = 16;

		// Token: 0x0400019D RID: 413
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 16);
	}
}
