using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x02000060 RID: 96
	[CLSCompliant(true)]
	public class Asn1Set : Asn1Structured
	{
		// Token: 0x0600037E RID: 894 RVA: 0x00011540 File Offset: 0x00010540
		public Asn1Set() : base(Asn1Set.ID)
		{
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001155C File Offset: 0x0001055C
		public Asn1Set(int size) : base(Asn1Set.ID, size)
		{
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00011578 File Offset: 0x00010578
		[CLSCompliant(false)]
		public Asn1Set(Asn1Decoder dec, Stream in_Renamed, int len) : base(Asn1Set.ID)
		{
			base.decodeStructured(dec, in_Renamed, len);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001159C File Offset: 0x0001059C
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SET: { ");
		}

		// Token: 0x0400019E RID: 414
		public const int TAG = 17;

		// Token: 0x0400019F RID: 415
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 17);
	}
}
