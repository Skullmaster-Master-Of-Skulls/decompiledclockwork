using System;
using System.IO;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000017 RID: 23
	public class RemoteStreamInfo
	{
		// Token: 0x060000AC RID: 172 RVA: 0x000045DC File Offset: 0x000027DC
		public RemoteStreamInfo(Stream remoteStream, string location, string fileName)
		{
			if (remoteStream == null)
			{
				throw Error.ArgumentNull("remoteStream");
			}
			if (location == null)
			{
				throw Error.ArgumentNull("location");
			}
			if (fileName == null)
			{
				throw Error.ArgumentNull("fileName");
			}
			this.FileName = fileName;
			this.RemoteStream = remoteStream;
			this.Location = location;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000462E File Offset: 0x0000282E
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004636 File Offset: 0x00002836
		public string FileName { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000463F File Offset: 0x0000283F
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004647 File Offset: 0x00002847
		public string Location { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004650 File Offset: 0x00002850
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00004658 File Offset: 0x00002858
		public Stream RemoteStream { get; private set; }
	}
}
