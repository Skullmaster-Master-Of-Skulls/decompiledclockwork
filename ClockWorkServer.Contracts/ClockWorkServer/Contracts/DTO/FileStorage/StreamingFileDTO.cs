using System;
using System.IO;
using System.ServiceModel;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x020005FC RID: 1532
	[MessageContract]
	public class StreamingFileDTO : BasicFileInfoMessageDTO, IDisposable
	{
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x0000E3EB File Offset: 0x0000C5EB
		// (set) Token: 0x06001F57 RID: 8023 RVA: 0x0000E3F3 File Offset: 0x0000C5F3
		[MessageBodyMember(Order = 1)]
		public Stream FileByteStream { get; set; }

		// Token: 0x06001F58 RID: 8024 RVA: 0x0000E3FC File Offset: 0x0000C5FC
		public void Dispose()
		{
			bool flag = this.FileByteStream != null;
			if (flag)
			{
				this.FileByteStream.Close();
				this.FileByteStream = null;
			}
		}
	}
}
