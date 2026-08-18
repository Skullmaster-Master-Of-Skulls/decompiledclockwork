using System;

namespace NLog.Internal
{
	// Token: 0x020000A7 RID: 167
	internal abstract class ThreadIDHelper
	{
		// Token: 0x0600053A RID: 1338 RVA: 0x0000B8A1 File Offset: 0x00009AA1
		static ThreadIDHelper()
		{
			if (PlatformDetector.IsWin32)
			{
				ThreadIDHelper.Instance = new Win32ThreadIDHelper();
				return;
			}
			ThreadIDHelper.Instance = new PortableThreadIDHelper();
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0000B8BF File Offset: 0x00009ABF
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x0000B8C6 File Offset: 0x00009AC6
		public static ThreadIDHelper Instance { get; private set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600053D RID: 1341
		public abstract int CurrentProcessID { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600053E RID: 1342
		public abstract string CurrentProcessName { get; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600053F RID: 1343
		public abstract string CurrentProcessBaseName { get; }
	}
}
