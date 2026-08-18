using System;
using System.Collections;

namespace DynamicScreens
{
	// Token: 0x02000044 RID: 68
	public class DynamicControlParameterCollection : CollectionBase
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x000339B8 File Offset: 0x000329B8
		public int Add(DynamicControlParameter parameter)
		{
			return base.List.Add(parameter);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000339D8 File Offset: 0x000329D8
		public int Add(string name, object val, DynamicControlSetting setting, DynamicControlParameterDataType settingDisplayType)
		{
			return base.List.Add(new DynamicControlParameter(name, val, setting, settingDisplayType));
		}

		// Token: 0x1700011A RID: 282
		public DynamicControlParameter this[string name]
		{
			get
			{
				return this.FindParameter(name);
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00033A1C File Offset: 0x00032A1C
		private DynamicControlParameter FindParameter(string name)
		{
			foreach (object obj in base.List)
			{
				DynamicControlParameter dynamicControlParameter = (DynamicControlParameter)obj;
				if (dynamicControlParameter.Equals(name))
				{
					return dynamicControlParameter;
				}
			}
			return null;
		}
	}
}
