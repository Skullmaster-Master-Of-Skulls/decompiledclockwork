using System;
using System.Collections;

namespace System.Web.Configuration
{
	// Token: 0x0200074A RID: 1866
	internal class RuleInfoComparer : IComparer
	{
		// Token: 0x060059CD RID: 22989 RVA: 0x00139A4C File Offset: 0x00137C4C
		public int Compare(object x, object y)
		{
			Type realType = ((HealthMonitoringSectionHelper.RuleInfo)x)._eventMappingSettings.RealType;
			Type realType2 = ((HealthMonitoringSectionHelper.RuleInfo)y)._eventMappingSettings.RealType;
			int result;
			if (realType.Equals(realType2))
			{
				result = 0;
			}
			else if (realType.IsSubclassOf(realType2))
			{
				result = 1;
			}
			else
			{
				if (!realType2.IsSubclassOf(realType))
				{
					return string.Compare(realType.ToString(), realType2.ToString(), StringComparison.Ordinal);
				}
				result = -1;
			}
			return result;
		}
	}
}
