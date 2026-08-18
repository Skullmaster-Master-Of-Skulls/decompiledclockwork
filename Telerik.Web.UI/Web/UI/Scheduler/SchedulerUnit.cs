using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler
{
	// Token: 0x020007EC RID: 2028
	internal static class SchedulerUnit
	{
		// Token: 0x06004666 RID: 18022 RVA: 0x000DD900 File Offset: 0x000DBB00
		internal static string GetValue(double value, UnitType type)
		{
			string suffix = SchedulerUnit.GetSuffix(type);
			return value.ToString() + suffix;
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x000DD924 File Offset: 0x000DBB24
		private static string GetSuffix(UnitType type)
		{
			switch (type)
			{
			case UnitType.Pixel:
				return "px";
			case UnitType.Point:
				return "pt";
			case UnitType.Pica:
				return "pc";
			case UnitType.Inch:
				return "in";
			case UnitType.Mm:
				return "mm";
			case UnitType.Cm:
				return "cm";
			case UnitType.Percentage:
				return "%";
			case UnitType.Em:
				return "em";
			case UnitType.Ex:
				return "ex";
			default:
				return "px";
			}
		}
	}
}
