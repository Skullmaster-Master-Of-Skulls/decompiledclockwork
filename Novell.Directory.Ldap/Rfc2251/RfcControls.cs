using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000CD RID: 205
	public class RfcControls : Asn1SequenceOf
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x000181C4 File Offset: 0x000171C4
		public RfcControls() : base(5)
		{
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000181D8 File Offset: 0x000171D8
		[CLSCompliant(false)]
		public RfcControls(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
			for (int i = 0; i < base.size(); i++)
			{
				RfcControl control = new RfcControl((Asn1Sequence)base.get_Renamed(i));
				this.set_Renamed(i, control);
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001821C File Offset: 0x0001721C
		public void add(RfcControl control)
		{
			base.add(control);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00018230 File Offset: 0x00017230
		public void set_Renamed(int index, RfcControl control)
		{
			base.set_Renamed(index, control);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00018248 File Offset: 0x00017248
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(2, true, 0);
		}

		// Token: 0x040003FC RID: 1020
		public const int CONTROLS = 0;
	}
}
