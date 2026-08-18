using System;

namespace TechnoPro.Common.UI.Web.DynamicControls.Controls
{
	// Token: 0x02000007 RID: 7
	public class FileUploadedArgs : EventArgs
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003956 File Offset: 0x00001B56
		// (set) Token: 0x06000064 RID: 100 RVA: 0x0000395E File Offset: 0x00001B5E
		public string clientFileName { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003967 File Offset: 0x00001B67
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000396F File Offset: 0x00001B6F
		public string serverFileName { get; set; }
	}
}
