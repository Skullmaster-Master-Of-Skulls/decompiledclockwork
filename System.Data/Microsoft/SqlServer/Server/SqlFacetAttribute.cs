using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000281 RID: 641
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
	public class SqlFacetAttribute : Attribute
	{
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x002878D8 File Offset: 0x00286CD8
		// (set) Token: 0x06002196 RID: 8598 RVA: 0x002878F8 File Offset: 0x00286CF8
		public bool IsFixedLength
		{
			get
			{
				return this.m_IsFixedLength;
			}
			set
			{
				this.m_IsFixedLength = value;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x00287918 File Offset: 0x00286D18
		// (set) Token: 0x06002198 RID: 8600 RVA: 0x00287938 File Offset: 0x00286D38
		public int MaxSize
		{
			get
			{
				return this.m_MaxSize;
			}
			set
			{
				this.m_MaxSize = value;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x00287958 File Offset: 0x00286D58
		// (set) Token: 0x0600219A RID: 8602 RVA: 0x00287978 File Offset: 0x00286D78
		public int Precision
		{
			get
			{
				return this.m_Precision;
			}
			set
			{
				this.m_Precision = value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x00287998 File Offset: 0x00286D98
		// (set) Token: 0x0600219C RID: 8604 RVA: 0x002879B8 File Offset: 0x00286DB8
		public int Scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x002879D8 File Offset: 0x00286DD8
		// (set) Token: 0x0600219E RID: 8606 RVA: 0x002879F8 File Offset: 0x00286DF8
		public bool IsNullable
		{
			get
			{
				return this.m_IsNullable;
			}
			set
			{
				this.m_IsNullable = value;
			}
		}

		// Token: 0x0400161C RID: 5660
		private bool m_IsFixedLength;

		// Token: 0x0400161D RID: 5661
		private int m_MaxSize;

		// Token: 0x0400161E RID: 5662
		private int m_Scale;

		// Token: 0x0400161F RID: 5663
		private int m_Precision;

		// Token: 0x04001620 RID: 5664
		private bool m_IsNullable;
	}
}
