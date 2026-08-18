using System;

namespace Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x0200051B RID: 1307
	public class VerisignCzagExtension : DerIA5String
	{
		// Token: 0x06002CA5 RID: 11429 RVA: 0x0010F434 File Offset: 0x0010E434
		public VerisignCzagExtension(DerIA5String str) : base(str.GetString())
		{
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x0010F442 File Offset: 0x0010E442
		public override string ToString()
		{
			return "VerisignCzagExtension: " + this.GetString();
		}
	}
}
