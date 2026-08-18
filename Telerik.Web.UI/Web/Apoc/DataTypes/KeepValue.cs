using System;

namespace Telerik.Web.Apoc.DataTypes
{
	// Token: 0x02001382 RID: 4994
	internal class KeepValue
	{
		// Token: 0x0600D05B RID: 53339 RVA: 0x002E2EEF File Offset: 0x002E10EF
		public KeepValue(string type, int val)
		{
			this.type = type;
			this.value = val;
		}

		// Token: 0x0600D05C RID: 53340 RVA: 0x002E2F10 File Offset: 0x002E1110
		public int GetValue()
		{
			return this.value;
		}

		// Token: 0x0600D05D RID: 53341 RVA: 0x002E2F18 File Offset: 0x002E1118
		public string GetKeepType()
		{
			return this.type;
		}

		// Token: 0x0600D05E RID: 53342 RVA: 0x002E2F20 File Offset: 0x002E1120
		public override string ToString()
		{
			return this.type;
		}

		// Token: 0x040037DE RID: 14302
		public const string KEEP_WITH_ALWAYS = "KEEP_WITH_ALWAYS";

		// Token: 0x040037DF RID: 14303
		public const string KEEP_WITH_AUTO = "KEEP_WITH_AUTO";

		// Token: 0x040037E0 RID: 14304
		public const string KEEP_WITH_VALUE = "KEEP_WITH_VALUE";

		// Token: 0x040037E1 RID: 14305
		private string type = "KEEP_WITH_AUTO";

		// Token: 0x040037E2 RID: 14306
		private int value;
	}
}
