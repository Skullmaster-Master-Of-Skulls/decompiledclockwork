using System;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x02001379 RID: 4985
	internal class AutoLength : Length
	{
		// Token: 0x0600D009 RID: 53257 RVA: 0x002E14DE File Offset: 0x002DF6DE
		public override bool IsAuto()
		{
			return true;
		}

		// Token: 0x0600D00A RID: 53258 RVA: 0x002E14E1 File Offset: 0x002DF6E1
		public override string ToString()
		{
			return "auto";
		}
	}
}
