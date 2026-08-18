using System;

namespace NLog.Internal
{
	// Token: 0x02000090 RID: 144
	internal abstract class FileCharacteristicsHelper
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x0000A574 File Offset: 0x00008774
		static FileCharacteristicsHelper()
		{
			if (PlatformDetector.IsDesktopWin32)
			{
				FileCharacteristicsHelper.Helper = new Win32FileCharacteristicsHelper();
				return;
			}
			FileCharacteristicsHelper.Helper = new PortableFileCharacteristicsHelper();
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000A592 File Offset: 0x00008792
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0000A599 File Offset: 0x00008799
		internal static FileCharacteristicsHelper Helper { get; private set; }

		// Token: 0x060004AE RID: 1198
		public abstract FileCharacteristics GetFileCharacteristics(string fileName, IntPtr fileHandle);
	}
}
