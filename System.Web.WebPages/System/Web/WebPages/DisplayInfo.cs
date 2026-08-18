using System;

namespace System.Web.WebPages
{
	// Token: 0x0200001A RID: 26
	public class DisplayInfo
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00003C84 File Offset: 0x00001E84
		public DisplayInfo(string filePath, IDisplayMode displayMode)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("filePath");
			}
			if (displayMode == null)
			{
				throw new ArgumentNullException("displayMode");
			}
			this.FilePath = filePath;
			this.DisplayMode = displayMode;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003CB6 File Offset: 0x00001EB6
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00003CBE File Offset: 0x00001EBE
		public IDisplayMode DisplayMode { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003CC7 File Offset: 0x00001EC7
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003CCF File Offset: 0x00001ECF
		public string FilePath { get; private set; }
	}
}
