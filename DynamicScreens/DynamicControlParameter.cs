using System;

namespace DynamicScreens
{
	// Token: 0x02000043 RID: 67
	public class DynamicControlParameter
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x0003387C File Offset: 0x0003287C
		public DynamicControlParameter(string name, object val, DynamicControlSetting setting, DynamicControlParameterDataType settingDisplayType)
		{
			this.name = name;
			this.val = val;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00033898 File Offset: 0x00032898
		// (set) Token: 0x060003CA RID: 970 RVA: 0x000338B0 File Offset: 0x000328B0
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003CB RID: 971 RVA: 0x000338BC File Offset: 0x000328BC
		// (set) Token: 0x060003CC RID: 972 RVA: 0x000338D4 File Offset: 0x000328D4
		public object Val
		{
			get
			{
				return this.val;
			}
			set
			{
				this.val = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003CD RID: 973 RVA: 0x000338E0 File Offset: 0x000328E0
		// (set) Token: 0x060003CE RID: 974 RVA: 0x000338F8 File Offset: 0x000328F8
		public DynamicControlSetting Setting
		{
			get
			{
				return this.setting;
			}
			set
			{
				this.setting = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00033904 File Offset: 0x00032904
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0003391C File Offset: 0x0003291C
		public DynamicControlParameterDataType SettingDisplayType
		{
			get
			{
				return this.settingDisplayType;
			}
			set
			{
				this.settingDisplayType = value;
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00033928 File Offset: 0x00032928
		public override bool Equals(object obj)
		{
			bool result;
			if (obj == null)
			{
				result = false;
			}
			else if (obj is string)
			{
				string text = (string)obj;
				result = (this.name.ToLower().CompareTo(text.ToLower()) == 0);
			}
			else if (obj is DynamicControlParameter)
			{
				DynamicControlParameter dynamicControlParameter = (DynamicControlParameter)obj;
				result = (dynamicControlParameter.Name.ToLower().CompareTo(this.name.ToLower()) == 0);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x040002BB RID: 699
		private string name;

		// Token: 0x040002BC RID: 700
		private object val;

		// Token: 0x040002BD RID: 701
		private DynamicControlSetting setting;

		// Token: 0x040002BE RID: 702
		private DynamicControlParameterDataType settingDisplayType;
	}
}
