using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.Common.ClientManager.ICore.FileStorage
{
	// Token: 0x02000056 RID: 86
	public class FileResultEventArgs : EventArgs
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00002058 File Offset: 0x00000258
		public BinaryFileDTO File { get; set; }
	}
}
