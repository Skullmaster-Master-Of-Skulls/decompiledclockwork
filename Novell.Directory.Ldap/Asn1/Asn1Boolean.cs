using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x02000052 RID: 82
	[CLSCompliant(true)]
	public class Asn1Boolean : Asn1Object
	{
		// Token: 0x0600031B RID: 795 RVA: 0x00010768 File Offset: 0x0000F768
		public Asn1Boolean(bool content) : base(Asn1Boolean.ID)
		{
			this.content = content;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0001078C File Offset: 0x0000F78C
		[CLSCompliant(false)]
		public Asn1Boolean(Asn1Decoder dec, Stream in_Renamed, int len) : base(Asn1Boolean.ID)
		{
			this.content = (bool)dec.decodeBoolean(in_Renamed, len);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000107BC File Offset: 0x0000F7BC
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000107D4 File Offset: 0x0000F7D4
		public bool booleanValue()
		{
			return this.content;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000107EC File Offset: 0x0000F7EC
		public override string ToString()
		{
			return base.ToString() + "BOOLEAN: " + this.content;
		}

		// Token: 0x04000180 RID: 384
		public const int TAG = 1;

		// Token: 0x04000181 RID: 385
		private bool content;

		// Token: 0x04000182 RID: 386
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 1);
	}
}
