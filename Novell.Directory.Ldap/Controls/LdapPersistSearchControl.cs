using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x02000066 RID: 102
	public class LdapPersistSearchControl : LdapControl
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00011F9C File Offset: 0x00010F9C
		// (set) Token: 0x060003AC RID: 940 RVA: 0x00011FB4 File Offset: 0x00010FB4
		public virtual int ChangeTypes
		{
			get
			{
				return this.m_changeTypes;
			}
			set
			{
				this.m_changeTypes = value;
				this.m_sequence.set_Renamed(LdapPersistSearchControl.CHANGETYPES_INDEX, new Asn1Integer(this.m_changeTypes));
				this.setValue();
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00011FEC File Offset: 0x00010FEC
		// (set) Token: 0x060003AE RID: 942 RVA: 0x00012004 File Offset: 0x00011004
		public virtual bool ReturnControls
		{
			get
			{
				return this.m_returnControls;
			}
			set
			{
				this.m_returnControls = value;
				this.m_sequence.set_Renamed(LdapPersistSearchControl.RETURNCONTROLS_INDEX, new Asn1Boolean(this.m_returnControls));
				this.setValue();
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0001203C File Offset: 0x0001103C
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x00012054 File Offset: 0x00011054
		public virtual bool ChangesOnly
		{
			get
			{
				return this.m_changesOnly;
			}
			set
			{
				this.m_changesOnly = value;
				this.m_sequence.set_Renamed(LdapPersistSearchControl.CHANGESONLY_INDEX, new Asn1Boolean(this.m_changesOnly));
				this.setValue();
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001208C File Offset: 0x0001108C
		public LdapPersistSearchControl() : this(LdapPersistSearchControl.ANY, true, true, true)
		{
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000120AC File Offset: 0x000110AC
		public LdapPersistSearchControl(int changeTypes, bool changesOnly, bool returnControls, bool isCritical) : base(LdapPersistSearchControl.requestOID, isCritical, null)
		{
			this.m_changeTypes = changeTypes;
			this.m_changesOnly = changesOnly;
			this.m_returnControls = returnControls;
			this.m_sequence = new Asn1Sequence(LdapPersistSearchControl.SEQUENCE_SIZE);
			this.m_sequence.add(new Asn1Integer(this.m_changeTypes));
			this.m_sequence.add(new Asn1Boolean(this.m_changesOnly));
			this.m_sequence.add(new Asn1Boolean(this.m_returnControls));
			this.setValue();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00012138 File Offset: 0x00011138
		public override string ToString()
		{
			sbyte[] encoding = this.m_sequence.getEncoding(LdapPersistSearchControl.s_encoder);
			StringBuilder stringBuilder = new StringBuilder(encoding.Length);
			for (int i = 0; i < encoding.Length; i++)
			{
				stringBuilder.Append(encoding[i].ToString());
				if (i < encoding.Length - 1)
				{
					stringBuilder.Append(",");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000121A0 File Offset: 0x000111A0
		private void setValue()
		{
			base.setValue(this.m_sequence.getEncoding(LdapPersistSearchControl.s_encoder));
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000121C8 File Offset: 0x000111C8
		static LdapPersistSearchControl()
		{
			LdapPersistSearchControl.s_encoder = new LBEREncoder();
			try
			{
				LdapControl.register(LdapPersistSearchControl.responseOID, Type.GetType("Novell.Directory.Ldap.Controls.LdapEntryChangeControl"));
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x040001AA RID: 426
		public const int ADD = 1;

		// Token: 0x040001AB RID: 427
		public const int DELETE = 2;

		// Token: 0x040001AC RID: 428
		public const int MODIFY = 4;

		// Token: 0x040001AD RID: 429
		public const int MODDN = 8;

		// Token: 0x040001AE RID: 430
		private static int SEQUENCE_SIZE = 3;

		// Token: 0x040001AF RID: 431
		private static int CHANGETYPES_INDEX = 0;

		// Token: 0x040001B0 RID: 432
		private static int CHANGESONLY_INDEX = 1;

		// Token: 0x040001B1 RID: 433
		private static int RETURNCONTROLS_INDEX = 2;

		// Token: 0x040001B2 RID: 434
		private static LBEREncoder s_encoder;

		// Token: 0x040001B3 RID: 435
		private int m_changeTypes;

		// Token: 0x040001B4 RID: 436
		private bool m_changesOnly;

		// Token: 0x040001B5 RID: 437
		private bool m_returnControls;

		// Token: 0x040001B6 RID: 438
		private Asn1Sequence m_sequence;

		// Token: 0x040001B7 RID: 439
		private static string requestOID = "2.16.840.1.113730.3.4.3";

		// Token: 0x040001B8 RID: 440
		private static string responseOID = "2.16.840.1.113730.3.4.7";

		// Token: 0x040001B9 RID: 441
		public static readonly int ANY = 15;
	}
}
