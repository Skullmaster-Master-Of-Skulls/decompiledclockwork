using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200005A RID: 90
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public class SqlFunctionAttribute : Attribute
	{
		// Token: 0x06000479 RID: 1145 RVA: 0x00043A78 File Offset: 0x00042E78
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

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00043ABC File Offset: 0x00042EBC
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x00043AD0 File Offset: 0x00042ED0
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

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00043AE4 File Offset: 0x00042EE4
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00043AF8 File Offset: 0x00042EF8
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00043B0C File Offset: 0x00042F0C
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x00043B20 File Offset: 0x00042F20
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00043B34 File Offset: 0x00042F34
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x00043B48 File Offset: 0x00042F48
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

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00043B5C File Offset: 0x00042F5C
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x00043B70 File Offset: 0x00042F70
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

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00043B84 File Offset: 0x00042F84
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x00043B98 File Offset: 0x00042F98
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

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00043BAC File Offset: 0x00042FAC
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x00043BC0 File Offset: 0x00042FC0
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

		// Token: 0x040001AE RID: 430
		private bool m_fDeterministic;

		// Token: 0x040001AF RID: 431
		private DataAccessKind m_eDataAccess;

		// Token: 0x040001B0 RID: 432
		private SystemDataAccessKind m_eSystemDataAccess;

		// Token: 0x040001B1 RID: 433
		private bool m_fPrecise;

		// Token: 0x040001B2 RID: 434
		private string m_fName;

		// Token: 0x040001B3 RID: 435
		private string m_fTableDefinition;

		// Token: 0x040001B4 RID: 436
		private string m_FillRowMethodName;
	}
}
