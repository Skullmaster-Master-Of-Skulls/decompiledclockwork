using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x0200006B RID: 107
	public class LdapVirtualListResponse : LdapControl
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x000129AC File Offset: 0x000119AC
		public virtual int ContentCount
		{
			get
			{
				return this.m_ContentCount;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000129C4 File Offset: 0x000119C4
		public virtual int FirstPosition
		{
			get
			{
				return this.m_firstPosition;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x000129DC File Offset: 0x000119DC
		public virtual int ResultCode
		{
			get
			{
				return this.m_resultCode;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x000129F4 File Offset: 0x000119F4
		public virtual string Context
		{
			get
			{
				return this.m_context;
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00012A0C File Offset: 0x00011A0C
		[CLSCompliant(false)]
		public LdapVirtualListResponse(string oid, bool critical, sbyte[] values) : base(oid, critical, values)
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
			if (asn1Object2 == null || !(asn1Object2 is Asn1Integer))
			{
				throw new IOException("Decoding error");
			}
			this.m_firstPosition = ((Asn1Integer)asn1Object2).intValue();
			Asn1Object asn1Object3 = ((Asn1Sequence)asn1Object).get_Renamed(1);
			if (asn1Object3 == null || !(asn1Object3 is Asn1Integer))
			{
				throw new IOException("Decoding error");
			}
			this.m_ContentCount = ((Asn1Integer)asn1Object3).intValue();
			Asn1Object asn1Object4 = ((Asn1Sequence)asn1Object).get_Renamed(2);
			if (asn1Object4 != null && asn1Object4 is Asn1Enumerated)
			{
				this.m_resultCode = ((Asn1Enumerated)asn1Object4).intValue();
				if (((Asn1Sequence)asn1Object).size() > 3)
				{
					Asn1Object asn1Object5 = ((Asn1Sequence)asn1Object).get_Renamed(3);
					if (asn1Object5 != null && asn1Object5 is Asn1OctetString)
					{
						this.m_context = ((Asn1OctetString)asn1Object5).stringValue();
					}
				}
				return;
			}
			throw new IOException("Decoding error");
		}

		// Token: 0x040001CE RID: 462
		private int m_firstPosition;

		// Token: 0x040001CF RID: 463
		private int m_ContentCount;

		// Token: 0x040001D0 RID: 464
		private int m_resultCode;

		// Token: 0x040001D1 RID: 465
		private string m_context = null;
	}
}
