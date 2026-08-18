using System;
using System.Collections;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000042 RID: 66
	public class LdapSearchConstraints : LdapConstraints
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000DBEC File Offset: 0x0000CBEC
		private void InitBlock()
		{
			this.dereference = 0;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000DC00 File Offset: 0x0000CC00
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000DC18 File Offset: 0x0000CC18
		public virtual int BatchSize
		{
			get
			{
				return this.batchSize;
			}
			set
			{
				this.batchSize = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000DC30 File Offset: 0x0000CC30
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000DC48 File Offset: 0x0000CC48
		public virtual int Dereference
		{
			get
			{
				return this.dereference;
			}
			set
			{
				this.dereference = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000DC60 File Offset: 0x0000CC60
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000DC78 File Offset: 0x0000CC78
		public virtual int MaxResults
		{
			get
			{
				return this.maxResults;
			}
			set
			{
				this.maxResults = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000DC90 File Offset: 0x0000CC90
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000DCA8 File Offset: 0x0000CCA8
		public virtual int ServerTimeLimit
		{
			get
			{
				return this.serverTimeLimit;
			}
			set
			{
				this.serverTimeLimit = value;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000DCC0 File Offset: 0x0000CCC0
		public LdapSearchConstraints()
		{
			this.InitBlock();
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000DCF4 File Offset: 0x0000CCF4
		public LdapSearchConstraints(LdapConstraints cons) : base(cons.TimeLimit, cons.ReferralFollowing, cons.getReferralHandler(), cons.HopLimit)
		{
			this.InitBlock();
			LdapControl[] controls = cons.getControls();
			if (controls != null)
			{
				LdapControl[] array = new LdapControl[controls.Length];
				controls.CopyTo(array, 0);
				base.setControls(array);
			}
			Hashtable properties = cons.Properties;
			if (properties != null)
			{
				base.Properties = (Hashtable)properties.Clone();
			}
			if (cons is LdapSearchConstraints)
			{
				LdapSearchConstraints ldapSearchConstraints = (LdapSearchConstraints)cons;
				this.serverTimeLimit = ldapSearchConstraints.ServerTimeLimit;
				this.dereference = ldapSearchConstraints.Dereference;
				this.maxResults = ldapSearchConstraints.MaxResults;
				this.batchSize = ldapSearchConstraints.BatchSize;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000DDBC File Offset: 0x0000CDBC
		public LdapSearchConstraints(int msLimit, int serverTimeLimit, int dereference, int maxResults, bool doReferrals, int batchSize, LdapReferralHandler handler, int hop_limit) : base(msLimit, doReferrals, handler, hop_limit)
		{
			this.InitBlock();
			this.serverTimeLimit = serverTimeLimit;
			this.dereference = dereference;
			this.maxResults = maxResults;
			this.batchSize = batchSize;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000DE18 File Offset: 0x0000CE18
		static LdapSearchConstraints()
		{
			LdapSearchConstraints.nameLock = new object();
		}

		// Token: 0x04000132 RID: 306
		public const int DEREF_NEVER = 0;

		// Token: 0x04000133 RID: 307
		public const int DEREF_SEARCHING = 1;

		// Token: 0x04000134 RID: 308
		public const int DEREF_FINDING = 2;

		// Token: 0x04000135 RID: 309
		public const int DEREF_ALWAYS = 3;

		// Token: 0x04000136 RID: 310
		private int dereference;

		// Token: 0x04000137 RID: 311
		private int serverTimeLimit = 0;

		// Token: 0x04000138 RID: 312
		private int maxResults = 1000;

		// Token: 0x04000139 RID: 313
		private int batchSize = 1;

		// Token: 0x0400013A RID: 314
		private static object nameLock;

		// Token: 0x0400013B RID: 315
		private static int lSConsNum = 0;

		// Token: 0x0400013C RID: 316
		private string name;
	}
}
