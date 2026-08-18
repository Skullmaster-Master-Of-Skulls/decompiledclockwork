using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x02000065 RID: 101
	public class LdapEntryChangeControl : LdapControl
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00011E0C File Offset: 0x00010E0C
		public virtual bool HasChangeNumber
		{
			get
			{
				return this.m_hasChangeNumber;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00011E24 File Offset: 0x00010E24
		public virtual int ChangeNumber
		{
			get
			{
				return this.m_changeNumber;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00011E3C File Offset: 0x00010E3C
		public virtual int ChangeType
		{
			get
			{
				return this.m_changeType;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00011E54 File Offset: 0x00010E54
		public virtual string PreviousDN
		{
			get
			{
				return this.m_previousDN;
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00011E6C File Offset: 0x00010E6C
		[CLSCompliant(false)]
		public LdapEntryChangeControl(string oid, bool critical, sbyte[] value_Renamed) : base(oid, critical, value_Renamed)
		{
			LBERDecoder lberdecoder = new LBERDecoder();
			if (lberdecoder == null)
			{
				throw new IOException("Decoding error.");
			}
			Asn1Object asn1Object = lberdecoder.decode(value_Renamed);
			if (asn1Object == null || !(asn1Object is Asn1Sequence))
			{
				throw new IOException("Decoding error.");
			}
			Asn1Sequence asn1Sequence = (Asn1Sequence)asn1Object;
			Asn1Object asn1Object2 = asn1Sequence.get_Renamed(0);
			if (asn1Object2 == null || !(asn1Object2 is Asn1Enumerated))
			{
				throw new IOException("Decoding error.");
			}
			this.m_changeType = ((Asn1Enumerated)asn1Object2).intValue();
			if (asn1Sequence.size() > 1 && this.m_changeType == 8)
			{
				asn1Object2 = asn1Sequence.get_Renamed(1);
				if (asn1Object2 == null || !(asn1Object2 is Asn1OctetString))
				{
					throw new IOException("Decoding error get previous DN");
				}
				this.m_previousDN = ((Asn1OctetString)asn1Object2).stringValue();
			}
			else
			{
				this.m_previousDN = "";
			}
			if (asn1Sequence.size() == 3)
			{
				asn1Object2 = asn1Sequence.get_Renamed(2);
				if (asn1Object2 == null || !(asn1Object2 is Asn1Integer))
				{
					throw new IOException("Decoding error getting change number");
				}
				this.m_changeNumber = ((Asn1Integer)asn1Object2).intValue();
				this.m_hasChangeNumber = true;
			}
			else
			{
				this.m_hasChangeNumber = false;
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00011F84 File Offset: 0x00010F84
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x040001A6 RID: 422
		private int m_changeType;

		// Token: 0x040001A7 RID: 423
		private string m_previousDN;

		// Token: 0x040001A8 RID: 424
		private bool m_hasChangeNumber;

		// Token: 0x040001A9 RID: 425
		private int m_changeNumber;
	}
}
