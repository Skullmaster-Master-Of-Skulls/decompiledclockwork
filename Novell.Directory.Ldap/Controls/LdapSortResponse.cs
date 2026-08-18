using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x02000069 RID: 105
	public class LdapSortResponse : LdapControl
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00012490 File Offset: 0x00011490
		public virtual string FailedAttribute
		{
			get
			{
				return this.failedAttribute;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x000124A8 File Offset: 0x000114A8
		public virtual int ResultCode
		{
			get
			{
				return this.resultCode;
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000124C0 File Offset: 0x000114C0
		[CLSCompliant(false)]
		public LdapSortResponse(string oid, bool critical, sbyte[] values) : base(oid, critical, values)
		{
			LBERDecoder lberdecoder = new LBERDecoder();
			if (lberdecoder == null)
			{
				throw new IOException("Decoding error");
			}
			Asn1Object asn1Object = lberdecoder.decode(values);
			if (asn1Object == null || !(asn1Object is Asn1Sequence))
			{
				throw new IOException("Decoding error");
			}
			Asn1Object asn1Object2 = ((Asn1Sequence)asn1Object).get_Renamed(0);
			if (asn1Object2 != null && asn1Object2 is Asn1Enumerated)
			{
				this.resultCode = ((Asn1Enumerated)asn1Object2).intValue();
			}
			if (((Asn1Sequence)asn1Object).size() > 1)
			{
				Asn1Object asn1Object3 = ((Asn1Sequence)asn1Object).get_Renamed(1);
				if (asn1Object3 != null && asn1Object3 is Asn1OctetString)
				{
					this.failedAttribute = ((Asn1OctetString)asn1Object3).stringValue();
				}
			}
		}

		// Token: 0x040001C1 RID: 449
		private string failedAttribute;

		// Token: 0x040001C2 RID: 450
		private int resultCode;
	}
}
