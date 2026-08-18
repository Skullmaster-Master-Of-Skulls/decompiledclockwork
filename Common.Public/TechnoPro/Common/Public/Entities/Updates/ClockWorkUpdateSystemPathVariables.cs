using System;
using System.IO;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x0200014A RID: 330
	public static class ClockWorkUpdateSystemPathVariables
	{
		// Token: 0x0400063D RID: 1597
		public static readonly string UPDATES_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "TechnoPro" + Path.DirectorySeparatorChar.ToString() + "Updates");

		// Token: 0x0400063E RID: 1598
		public static readonly string UPDATES_PUBLIC_PATH = Path.Combine(ClockWorkUpdateSystemPathVariables.UPDATES_PATH, "Public");

		// Token: 0x0400063F RID: 1599
		public static readonly string UPDATES_COMPUTER_PATH = Path.Combine(ClockWorkUpdateSystemPathVariables.UPDATES_PATH, "Computer");

		// Token: 0x04000640 RID: 1600
		public static readonly string UPDATES_RECOVERY_PATH = Path.Combine(ClockWorkUpdateSystemPathVariables.UPDATES_PATH, "Recovery");
	}
}
