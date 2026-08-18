using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x0200006A RID: 106
	public class LdapVirtualListControl : LdapControl
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x0001256C File Offset: 0x0001156C
		public virtual int AfterCount
		{
			get
			{
				return this.m_afterCount;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00012584 File Offset: 0x00011584
		public virtual int BeforeCount
		{
			get
			{
				return this.m_beforeCount;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0001259C File Offset: 0x0001159C
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x000125B4 File Offset: 0x000115B4
		public virtual int ListSize
		{
			get
			{
				return this.m_contentCount;
			}
			set
			{
				this.m_contentCount = value;
				this.BuildIndexedVLVRequest();
				this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x000125E4 File Offset: 0x000115E4
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x000125FC File Offset: 0x000115FC
		public virtual string Context
		{
			get
			{
				return this.m_context;
			}
			set
			{
				int index = 3;
				this.m_context = value;
				if (this.m_vlvRequest.size() == 4)
				{
					this.m_vlvRequest.set_Renamed(index, new Asn1OctetString(this.m_context));
				}
				else if (this.m_vlvRequest.size() == 3)
				{
					this.m_vlvRequest.add(new Asn1OctetString(this.m_context));
				}
				this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00012674 File Offset: 0x00011674
		public LdapVirtualListControl(string jumpTo, int beforeCount, int afterCount) : this(jumpTo, beforeCount, afterCount, null)
		{
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00012690 File Offset: 0x00011690
		public LdapVirtualListControl(string jumpTo, int beforeCount, int afterCount, string context)
		{
			this.m_context = null;
			this.m_startIndex = 0;
			this.m_contentCount = -1;
			base..ctor(LdapVirtualListControl.requestOID, true, null);
			this.m_beforeCount = beforeCount;
			this.m_afterCount = afterCount;
			this.m_jumpTo = jumpTo;
			this.m_context = context;
			this.BuildTypedVLVRequest();
			this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000126FC File Offset: 0x000116FC
		private void BuildTypedVLVRequest()
		{
			this.m_vlvRequest = new Asn1Sequence(4);
			this.m_vlvRequest.add(new Asn1Integer(this.m_beforeCount));
			this.m_vlvRequest.add(new Asn1Integer(this.m_afterCount));
			this.m_vlvRequest.add(new Asn1Tagged(new Asn1Identifier(2, false, LdapVirtualListControl.GREATERTHANOREQUAL), new Asn1OctetString(this.m_jumpTo), false));
			if (this.m_context != null)
			{
				this.m_vlvRequest.add(new Asn1OctetString(this.m_context));
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001278C File Offset: 0x0001178C
		public LdapVirtualListControl(int startIndex, int beforeCount, int afterCount, int contentCount) : this(startIndex, beforeCount, afterCount, contentCount, null)
		{
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000127A8 File Offset: 0x000117A8
		public LdapVirtualListControl(int startIndex, int beforeCount, int afterCount, int contentCount, string context)
		{
			this.m_context = null;
			this.m_startIndex = 0;
			this.m_contentCount = -1;
			base..ctor(LdapVirtualListControl.requestOID, true, null);
			this.m_beforeCount = beforeCount;
			this.m_afterCount = afterCount;
			this.m_startIndex = startIndex;
			this.m_contentCount = contentCount;
			this.m_context = context;
			this.BuildIndexedVLVRequest();
			this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001281C File Offset: 0x0001181C
		private void BuildIndexedVLVRequest()
		{
			this.m_vlvRequest = new Asn1Sequence(4);
			this.m_vlvRequest.add(new Asn1Integer(this.m_beforeCount));
			this.m_vlvRequest.add(new Asn1Integer(this.m_afterCount));
			Asn1Sequence asn1Sequence = new Asn1Sequence(2);
			asn1Sequence.add(new Asn1Integer(this.m_startIndex));
			asn1Sequence.add(new Asn1Integer(this.m_contentCount));
			this.m_vlvRequest.add(new Asn1Tagged(new Asn1Identifier(2, true, LdapVirtualListControl.BYOFFSET), asn1Sequence, false));
			if (this.m_context != null)
			{
				this.m_vlvRequest.add(new Asn1OctetString(this.m_context));
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000128C8 File Offset: 0x000118C8
		public virtual void setRange(int listIndex, int beforeCount, int afterCount)
		{
			this.m_beforeCount = beforeCount;
			this.m_afterCount = afterCount;
			this.m_startIndex = listIndex;
			this.BuildIndexedVLVRequest();
			this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00012908 File Offset: 0x00011908
		public virtual void setRange(string jumpTo, int beforeCount, int afterCount)
		{
			this.m_beforeCount = beforeCount;
			this.m_afterCount = afterCount;
			this.m_jumpTo = jumpTo;
			this.BuildTypedVLVRequest();
			this.setValue(this.m_vlvRequest.getEncoding(new LBEREncoder()));
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00012948 File Offset: 0x00011948
		static LdapVirtualListControl()
		{
			try
			{
				LdapControl.register(LdapVirtualListControl.responseOID, Type.GetType("Novell.Directory.Ldap.Controls.LdapVirtualListResponse"));
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x040001C3 RID: 451
		private static int BYOFFSET = 0;

		// Token: 0x040001C4 RID: 452
		private static int GREATERTHANOREQUAL = 1;

		// Token: 0x040001C5 RID: 453
		private static string requestOID = "2.16.840.1.113730.3.4.9";

		// Token: 0x040001C6 RID: 454
		private static string responseOID = "2.16.840.1.113730.3.4.10";

		// Token: 0x040001C7 RID: 455
		private Asn1Sequence m_vlvRequest;

		// Token: 0x040001C8 RID: 456
		private int m_beforeCount;

		// Token: 0x040001C9 RID: 457
		private int m_afterCount;

		// Token: 0x040001CA RID: 458
		private string m_jumpTo;

		// Token: 0x040001CB RID: 459
		private string m_context;

		// Token: 0x040001CC RID: 460
		private int m_startIndex;

		// Token: 0x040001CD RID: 461
		private int m_contentCount;
	}
}
