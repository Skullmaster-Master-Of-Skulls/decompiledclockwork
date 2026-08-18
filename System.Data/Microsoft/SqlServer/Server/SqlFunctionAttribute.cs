using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000284 RID: 644
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public class SqlFunctionAttribute : Attribute
	{
		// Token: 0x060021A0 RID: 8608 RVA: 0x00287A38 File Offset: 0x00286E38
		public SqlFunctionAttribute()
		{
			this.m_fDeterministic = false;
			this.m_eDataAccess = DataAccessKind.None;
			this.m_eSystemDataAccess = SystemDataAccessKind.None;
			this.m_fPrecise = false;
			this.m_fName = null;
			this.m_fTableDefinition = null;
			this.m_FillRowMethodName = null;
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060021A1 RID: 8609 RVA: 0x00287A88 File Offset: 0x00286E88
		// (set) Token: 0x060021A2 RID: 8610 RVA: 0x00287AA8 File Offset: 0x00286EA8
		public bool IsDeterministic
		{
			get
			{
				return this.m_fDeterministic;
			}
			set
			{
				this.m_fDeterministic = value;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060021A3 RID: 8611 RVA: 0x00287AC8 File Offset: 0x00286EC8
		// (set) Token: 0x060021A4 RID: 8612 RVA: 0x00287AE8 File Offset: 0x00286EE8
		public DataAccessKind DataAccess
		{
			get
			{
				return this.m_eDataAccess;
			}
			set
			{
				this.m_eDataAccess = value;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060021A5 RID: 8613 RVA: 0x00287B08 File Offset: 0x00286F08
		// (set) Token: 0x060021A6 RID: 8614 RVA: 0x00287B28 File Offset: 0x00286F28
		public SystemDataAccessKind SystemDataAccess
		{
			get
			{
				return this.m_eSystemDataAccess;
			}
			set
			{
				this.m_eSystemDataAccess = value;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060021A7 RID: 8615 RVA: 0x00287B48 File Offset: 0x00286F48
		// (set) Token: 0x060021A8 RID: 8616 RVA: 0x00287B68 File Offset: 0x00286F68
		public bool IsPrecise
		{
			get
			{
				return this.m_fPrecise;
			}
			set
			{
				this.m_fPrecise = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x00287B88 File Offset: 0x00286F88
		// (set) Token: 0x060021AA RID: 8618 RVA: 0x00287BA8 File Offset: 0x00286FA8
		public string Name
		{
			get
			{
				return this.m_fName;
			}
			set
			{
				this.m_fName = value;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x00287BC8 File Offset: 0x00286FC8
		// (set) Token: 0x060021AC RID: 8620 RVA: 0x00287BE8 File Offset: 0x00286FE8
		public string TableDefinition
		{
			get
			{
				return this.m_fTableDefinition;
			}
			set
			{
				this.m_fTableDefinition = value;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x00287C08 File Offset: 0x00287008
		// (set) Token: 0x060021AE RID: 8622 RVA: 0x00287C28 File Offset: 0x00287028
		public string FillRowMethodName
		{
			get
			{
				return this.m_FillRowMethodName;
			}
			set
			{
				this.m_FillRowMethodName = value;
			}
		}

		// Token: 0x04001627 RID: 5671
		private bool m_fDeterministic;

		// Token: 0x04001628 RID: 5672
		private DataAccessKind m_eDataAccess;

		// Token: 0x04001629 RID: 5673
		private SystemDataAccessKind m_eSystemDataAccess;

		// Token: 0x0400162A RID: 5674
		private bool m_fPrecise;

		// Token: 0x0400162B RID: 5675
		private string m_fName;

		// Token: 0x0400162C RID: 5676
		private string m_fTableDefinition;

		// Token: 0x0400162D RID: 5677
		private string m_FillRowMethodName;
	}
}
