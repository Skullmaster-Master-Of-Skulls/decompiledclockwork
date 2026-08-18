using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x02000059 RID: 89
	[CLSCompliant(true)]
	public class Asn1Integer : Asn1Numeric
	{
		// Token: 0x0600034F RID: 847 RVA: 0x00010CCC File Offset: 0x0000FCCC
		public Asn1Integer(int content) : base(Asn1Integer.ID, content)
		{
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00010CE8 File Offset: 0x0000FCE8
		public Asn1Integer(long content) : base(Asn1Integer.ID, content)
		{
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00010D04 File Offset: 0x0000FD04
		[CLSCompliant(false)]
		public Asn1Integer(Asn1Decoder dec, Stream in_Renamed, int len) : base(Asn1Integer.ID, (long)dec.decodeNumeric(in_Renamed, len))
		{
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00010D2C File Offset: 0x0000FD2C
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00010D44 File Offset: 0x0000FD44
		public override string ToString()
		{
			return base.ToString() + "INTEGER: " + base.longValue();
		}

		// Token: 0x0400018F RID: 399
		public const int TAG = 2;

		// Token: 0x04000190 RID: 400
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 2);
	}
}
