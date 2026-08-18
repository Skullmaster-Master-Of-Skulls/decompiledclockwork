using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x020000E5 RID: 229
	public class RfcSearchResultEntry : Asn1Sequence
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0001AAE0 File Offset: 0x00019AE0
		public virtual Asn1OctetString ObjectName
		{
			get
			{
				return (Asn1OctetString)base.get_Renamed(0);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001AB00 File Offset: 0x00019B00
		public virtual Asn1Sequence Attributes
		{
			get
			{
				return (Asn1Sequence)base.get_Renamed(1);
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001AB20 File Offset: 0x00019B20
		[CLSCompliant(false)]
		public RfcSearchResultEntry(Asn1Decoder dec, Stream in_Renamed, int len) : base(dec, in_Renamed, len)
		{
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001AB38 File Offset: 0x00019B38
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 4);
		}
	}
}
