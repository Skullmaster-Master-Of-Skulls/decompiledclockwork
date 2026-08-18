using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers.TypeConverters
{
	// Token: 0x0200002B RID: 43
	public class ClockWorkUserGroupIdConverter : StringConverter
	{
		// Token: 0x060002CF RID: 719 RVA: 0x0001E7E4 File Offset: 0x0001D7E4
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0001E7F8 File Offset: 0x0001D7F8
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0001E80C File Offset: 0x0001D80C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(HE_GlobalVars_ClockWorkGroupList._ListOfGroups);
		}
	}
}
