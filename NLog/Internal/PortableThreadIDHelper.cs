using System;
using System.Diagnostics;
using System.IO;

namespace NLog.Internal
{
	// Token: 0x020000A8 RID: 168
	internal class PortableThreadIDHelper : ThreadIDHelper
	{
		// Token: 0x06000541 RID: 1345 RVA: 0x0000B8D6 File Offset: 0x00009AD6
		public PortableThreadIDHelper()
		{
			this.currentProcessID = Process.GetCurrentProcess().Id;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0000B8EE File Offset: 0x00009AEE
		public override int CurrentProcessID
		{
			get
			{
				return this.currentProcessID;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000B8F6 File Offset: 0x00009AF6
		public override string CurrentProcessName
		{
			get
			{
				this.GetProcessName();
				return this.currentProcessName;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0000B904 File Offset: 0x00009B04
		public override string CurrentProcessBaseName
		{
			get
			{
				this.GetProcessName();
				return this.currentProcessBaseName;
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000B914 File Offset: 0x00009B14
		private void GetProcessName()
		{
			if (this.currentProcessName == null)
			{
				try
				{
					this.currentProcessName = Process.GetCurrentProcess().MainModule.FileName;
				}
				catch (Exception exception)
				{
					if (exception.MustBeRethrown())
					{
						throw;
					}
					this.currentProcessName = "<unknown>";
				}
				this.currentProcessBaseName = Path.GetFileNameWithoutExtension(this.currentProcessName);
			}
		}

		// Token: 0x04000112 RID: 274
		private const string UnknownProcessName = "<unknown>";

		// Token: 0x04000113 RID: 275
		private readonly int currentProcessID;

		// Token: 0x04000114 RID: 276
		private string currentProcessName;

		// Token: 0x04000115 RID: 277
		private string currentProcessBaseName;
	}
}
