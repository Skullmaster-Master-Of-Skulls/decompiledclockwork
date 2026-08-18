using System;
using System.Reflection;
using TechnoPro.Common.Public.Entities.SettingsPermissionsGeneral;

namespace TechnoPro.Common.Public.Entities.Settings
{
	// Token: 0x020001D2 RID: 466
	public class SettingDataAttribute : GroupDataAttribute
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06000D7F RID: 3455 RVA: 0x00015438 File Offset: 0x00013638
		// (set) Token: 0x06000D80 RID: 3456 RVA: 0x00015450 File Offset: 0x00013650
		public string SubGroup
		{
			get
			{
				return this.subGroup;
			}
			set
			{
				this.subGroup = value;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x0001545A File Offset: 0x0001365A
		// (set) Token: 0x06000D82 RID: 3458 RVA: 0x00015462 File Offset: 0x00013662
		public eSettingLevel SettingLevel { get; set; }

		// Token: 0x06000D83 RID: 3459 RVA: 0x0001546B File Offset: 0x0001366B
		public SettingDataAttribute(string name, Group group, SettingSemantic semanticType) : base(name)
		{
			this.group = group;
			this.semanticType = semanticType;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00015484 File Offset: 0x00013684
		public SettingDataAttribute(string name, string description, Group group, SettingSemantic semanticType) : base(name)
		{
			this.description = description;
			this.group = group;
			this.semanticType = semanticType;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000154A5 File Offset: 0x000136A5
		public SettingDataAttribute(string name, string subGroup, string description, Group group, SettingSemantic semanticType) : base(name)
		{
			this.subGroup = subGroup;
			this.description = description;
			this.group = group;
			this.semanticType = semanticType;
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x000154D0 File Offset: 0x000136D0
		public Group Group
		{
			get
			{
				return this.group;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x000154E8 File Offset: 0x000136E8
		public Type SystemType
		{
			get
			{
				Type type = this.semanticType.GetType();
				FieldInfo field = type.GetField(this.semanticType.ToString());
				SemanticTypeAttribute[] array = field.GetCustomAttributes(typeof(SemanticTypeAttribute), false) as SemanticTypeAttribute[];
				bool flag = array != null && array.Length != 0;
				Type result;
				if (flag)
				{
					result = array[0].SystemType;
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0001555C File Offset: 0x0001375C
		public SettingSemantic SemanticType
		{
			get
			{
				return this.semanticType;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00015574 File Offset: 0x00013774
		// (set) Token: 0x06000D8A RID: 3466 RVA: 0x0001558C File Offset: 0x0001378C
		public object DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x00015598 File Offset: 0x00013798
		// (set) Token: 0x06000D8C RID: 3468 RVA: 0x000155B0 File Offset: 0x000137B0
		public bool IsHidden
		{
			get
			{
				return this.hidden;
			}
			set
			{
				this.hidden = value;
			}
		}

		// Token: 0x0400093C RID: 2364
		protected Group group;

		// Token: 0x0400093D RID: 2365
		protected SettingSemantic semanticType;

		// Token: 0x0400093E RID: 2366
		protected object defaultValue;

		// Token: 0x0400093F RID: 2367
		protected bool hidden;

		// Token: 0x04000940 RID: 2368
		protected string subGroup;
	}
}
