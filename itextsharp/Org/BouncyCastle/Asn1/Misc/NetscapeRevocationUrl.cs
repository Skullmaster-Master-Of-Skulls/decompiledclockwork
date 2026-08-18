using System;

namespace Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x0200057B RID: 1403
	public class NetscapeRevocationUrl : DerIA5String
	{
		// Token: 0x06002FD6 RID: 12246 RVA: 0x001277FB File Offset: 0x001267FB
		public NetscapeRevocationUrl(DerIA5String str) : base(str.GetString())
		{
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x00127809 File Offset: 0x00126809
		public override string ToString()
		{
			return "NetscapeRevocationUrl: " + this.GetString();
		}
	}
}
