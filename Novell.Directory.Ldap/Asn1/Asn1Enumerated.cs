using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x02000057 RID: 87
	[CLSCompliant(true)]
	public class Asn1Enumerated : Asn1Numeric
	{
		// Token: 0x0600033B RID: 827 RVA: 0x00010984 File Offset: 0x0000F984
		public Asn1Enumerated(int content) : base(Asn1Enumerated.ID, content)
		{
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000109A0 File Offset: 0x0000F9A0
		public Asn1Enumerated(long content) : base(Asn1Enumerated.ID, content)
		{
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000109BC File Offset: 0x0000F9BC
		[CLSCompliant(false)]
		public Asn1Enumerated(Asn1Decoder dec, Stream in_Renamed, int len) : base(Asn1Enumerated.ID, (long)dec.decodeNumeric(in_Renamed, len))
		{
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000109E4 File Offset: 0x0000F9E4
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000109FC File Offset: 0x0000F9FC
		public override string ToString()
		{
			return base.ToString() + "ENUMERATED: " + base.longValue();
		}

		// Token: 0x04000185 RID: 389
		public const int TAG = 10;

		// Token: 0x04000186 RID: 390
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 10);
	}
}
