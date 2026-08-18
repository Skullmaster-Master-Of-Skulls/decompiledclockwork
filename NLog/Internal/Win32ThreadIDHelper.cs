using System;
using System.IO;
using System.Security;
using System.Text;

namespace NLog.Internal
{
	// Token: 0x020000BD RID: 189
	[SecuritySafeCritical]
	internal class Win32ThreadIDHelper : ThreadIDHelper
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x0000C774 File Offset: 0x0000A974
		public Win32ThreadIDHelper()
		{
			this.currentProcessID = NativeMethods.GetCurrentProcessId();
			StringBuilder stringBuilder = new StringBuilder(512);
			if (NativeMethods.GetModuleFileName(IntPtr.Zero, stringBuilder, stringBuilder.Capacity) == 0U)
			{
				throw new InvalidOperationException("Cannot determine program name.");
			}
			this.currentProcessName = stringBuilder.ToString();
			this.currentProcessBaseName = Path.GetFileNameWithoutExtension(this.currentProcessName);
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0000C7D8 File Offset: 0x0000A9D8
		public override int CurrentProcessID
		{
			get
			{
				return this.currentProcessID;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
		public override string CurrentProcessName
		{
			get
			{
				return this.currentProcessName;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
		public override string CurrentProcessBaseName
		{
			get
			{
				return this.currentProcessBaseName;
			}
		}

		// Token: 0x04000142 RID: 322
		private readonly int currentProcessID;

		// Token: 0x04000143 RID: 323
		private readonly string currentProcessName;

		// Token: 0x04000144 RID: 324
		private readonly string currentProcessBaseName;
	}
}
