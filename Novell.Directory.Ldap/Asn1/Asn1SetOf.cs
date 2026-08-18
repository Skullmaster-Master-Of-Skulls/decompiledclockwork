using System;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x02000061 RID: 97
	[CLSCompliant(true)]
	public class Asn1SetOf : Asn1Structured
	{
		// Token: 0x06000383 RID: 899 RVA: 0x000115D4 File Offset: 0x000105D4
		public Asn1SetOf() : base(Asn1SetOf.ID)
		{
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000115F0 File Offset: 0x000105F0
		public Asn1SetOf(int size) : base(Asn1SetOf.ID, size)
		{
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001160C File Offset: 0x0001060C
		public Asn1SetOf(Asn1Set set_Renamed) : base(Asn1SetOf.ID, set_Renamed.toArray(), set_Renamed.size())
		{
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00011634 File Offset: 0x00010634
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SET OF: { ");
		}

		// Token: 0x040001A0 RID: 416
		public const int TAG = 17;

		// Token: 0x040001A1 RID: 417
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 17);
	}
}
