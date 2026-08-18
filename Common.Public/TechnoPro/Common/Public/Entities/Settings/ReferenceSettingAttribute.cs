using System;

namespace TechnoPro.Common.Public.Entities.Settings
{
	// Token: 0x020001D3 RID: 467
	public class ReferenceSettingAttribute : SettingDataAttribute
	{
		// Token: 0x06000D8D RID: 3469 RVA: 0x000155BC File Offset: 0x000137BC
		public ReferenceSettingAttribute(string name, string subGroup, string description, Group group, SettingSemantic semanticType, string tableName, string idColumnName, string valueColumnName) : base(name, subGroup, description, group, semanticType)
		{
			base.SubGroup = subGroup;
			this.tableName = tableName;
			this.idColumnName = idColumnName;
			this.valueColumnName = valueColumnName;
			this.overrideSql = "";
			this.allowMultipleSelections = true;
			this.overrideSortByDisplayName = false;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00015614 File Offset: 0x00013814
		public ReferenceSettingAttribute(string name, string subGroup, Group group, SettingSemantic semanticType, string tableName, string idColumnName, string valueColumnName) : base(name, subGroup, group, semanticType)
		{
			base.SubGroup = subGroup;
			this.tableName = tableName;
			this.idColumnName = idColumnName;
			this.valueColumnName = valueColumnName;
			this.overrideSql = "";
			this.allowMultipleSelections = true;
			this.overrideSortByDisplayName = false;
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06000D8F RID: 3471 RVA: 0x00015667 File Offset: 0x00013867
		public string TableName
		{
			get
			{
				return this.tableName;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00015670 File Offset: 0x00013870
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x00015688 File Offset: 0x00013888
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

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00015694 File Offset: 0x00013894
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x000156AC File Offset: 0x000138AC
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

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x000156B6 File Offset: 0x000138B6
		public string IdColumnName
		{
			get
			{
				return this.idColumnName;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x000156BE File Offset: 0x000138BE
		public string ValueColumnName
		{
			get
			{
				return this.valueColumnName;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x000156C8 File Offset: 0x000138C8
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x000156E0 File Offset: 0x000138E0
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

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x000156EC File Offset: 0x000138EC
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x00015704 File Offset: 0x00013904
		public bool AllowMultipleSelections
		{
			get
			{
				return this.allowMultipleSelections;
			}
			set
			{
				this.allowMultipleSelections = value;
			}
		}

		// Token: 0x04000942 RID: 2370
		protected string tableName;

		// Token: 0x04000943 RID: 2371
		protected string idColumnName;

		// Token: 0x04000944 RID: 2372
		protected string valueColumnName;

		// Token: 0x04000945 RID: 2373
		protected bool isValueEncrypted;

		// Token: 0x04000946 RID: 2374
		protected string overrideSql;

		// Token: 0x04000947 RID: 2375
		protected bool allowMultipleSelections;

		// Token: 0x04000948 RID: 2376
		protected bool overrideSortByDisplayName;
	}
}
