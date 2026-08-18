using System;
using System.ComponentModel;

namespace DynamicScreens.DynamicControlWrappers.TypeConverters
{
	// Token: 0x0200001F RID: 31
	public class RuleConverter : StringConverter
	{
		// Token: 0x06000206 RID: 518 RVA: 0x000193D8 File Offset: 0x000183D8
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000193EC File Offset: 0x000183EC
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00019400 File Offset: 0x00018400
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(HE_GlobalVars._ListOfRules);
		}
	}
}
