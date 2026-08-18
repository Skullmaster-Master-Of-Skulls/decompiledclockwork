using System;

namespace System.Web.Util
{
	// Token: 0x0200022C RID: 556
	internal static class VersionUtil
	{
		// Token: 0x0400182E RID: 6190
		public static readonly Version Framework00 = new Version(0, 0);

		// Token: 0x0400182F RID: 6191
		public static readonly Version Framework20 = new Version(2, 0);

		// Token: 0x04001830 RID: 6192
		public static readonly Version Framework35 = new Version(3, 5);

		// Token: 0x04001831 RID: 6193
		public static readonly Version Framework40 = new Version(4, 0);

		// Token: 0x04001832 RID: 6194
		public static readonly Version Framework45 = new Version(4, 5);

		// Token: 0x04001833 RID: 6195
		public static readonly Version Framework451 = new Version(4, 5, 1);

		// Token: 0x04001834 RID: 6196
		public static readonly Version Framework452 = new Version(4, 5, 2);

		// Token: 0x04001835 RID: 6197
		public static readonly Version Framework46 = new Version(4, 6);

		// Token: 0x04001836 RID: 6198
		public static readonly Version Framework461 = new Version(4, 6, 1);

		// Token: 0x04001837 RID: 6199
		public static readonly Version Framework463 = new Version(4, 6, 3);

		// Token: 0x04001838 RID: 6200
		public static readonly Version Framework472 = new Version(4, 7, 2);

		// Token: 0x04001839 RID: 6201
		public static readonly Version Framework48 = new Version(4, 8);

		// Token: 0x0400183A RID: 6202
		public static readonly Version FrameworkDefault = VersionUtil.Framework40;

		// Token: 0x0400183B RID: 6203
		public const string FrameworkDefaultString = "4.0";
	}
}
