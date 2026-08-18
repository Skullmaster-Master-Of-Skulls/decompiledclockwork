using System;

namespace TechnoPro.Common.Public.Entities.Settings
{
	// Token: 0x020001D6 RID: 470
	public class ReferenceArrayFormSettingAttribute : FormSettingAttribute
	{
		// Token: 0x06000DA0 RID: 3488 RVA: 0x00015798 File Offset: 0x00013998
		public ReferenceArrayFormSettingAttribute(string name, string description, Group group, SettingSemantic semanticType, int formSettingCode, FormSettingType type, string tableName, string idColumnName, string valueColumnName) : base(name, description, group, semanticType, formSettingCode, type)
		{
			base.SubGroup = this.subGroup;
			this.tableName = tableName;
			this.idColumnName = idColumnName;
			this.valueColumnName = valueColumnName;
			this.overrideSql = "";
			this.overrideSortByDisplayName = false;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000157F0 File Offset: 0x000139F0
		public ReferenceArrayFormSettingAttribute(string name, Group group, SettingSemantic semanticType, int formSettingCode, FormSettingType type, string tableName, string idColumnName, string valueColumnName) : base(name, group, semanticType, formSettingCode, type)
		{
			base.SubGroup = this.subGroup;
			this.tableName = tableName;
			this.idColumnName = idColumnName;
			this.valueColumnName = valueColumnName;
			this.overrideSql = "";
			this.overrideSortByDisplayName = false;
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x00015844 File Offset: 0x00013A44
		public string TableName
		{
			get
			{
				return this.tableName;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0001585C File Offset: 0x00013A5C
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x00015874 File Offset: 0x00013A74
		public string OverrideSql
		{
			get
			{
				return this.overrideSql;
			}
			set
			{
				this.overrideSql = value;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00015880 File Offset: 0x00013A80
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x00015898 File Offset: 0x00013A98
		public bool OverrideSortByDisplayName
		{
			get
			{
				return this.overrideSortByDisplayName;
			}
			set
			{
				this.overrideSortByDisplayName = value;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x000158A4 File Offset: 0x00013AA4
		public string IdColumnName
		{
			get
			{
				return this.idColumnName;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x000158BC File Offset: 0x00013ABC
		public string ValueColumnName
		{
			get
			{
				return this.valueColumnName;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x000158D4 File Offset: 0x00013AD4
		// (set) Token: 0x06000DAA RID: 3498 RVA: 0x000158EC File Offset: 0x00013AEC
		public bool IsValueEncrypted
		{
			get
			{
				return this.isValueEncrypted;
			}
			set
			{
				this.isValueEncrypted = value;
			}
		}

		// Token: 0x0400094B RID: 2379
		protected string tableName;

		// Token: 0x0400094C RID: 2380
		protected string idColumnName;

		// Token: 0x0400094D RID: 2381
		protected string valueColumnName;

		// Token: 0x0400094E RID: 2382
		protected bool isValueEncrypted;

		// Token: 0x0400094F RID: 2383
		protected string overrideSql;

		// Token: 0x04000950 RID: 2384
		protected bool overrideSortByDisplayName;
	}
}
