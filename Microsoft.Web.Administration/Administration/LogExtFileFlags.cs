using System;

namespace Microsoft.Web.Administration
{
	// Token: 0x0200005A RID: 90
	[Flags]
	public enum LogExtFileFlags
	{
		// Token: 0x040000A9 RID: 169
		Date = 1,
		// Token: 0x040000AA RID: 170
		Time = 2,
		// Token: 0x040000AB RID: 171
		ClientIP = 4,
		// Token: 0x040000AC RID: 172
		UserName = 8,
		// Token: 0x040000AD RID: 173
		SiteName = 16,
		// Token: 0x040000AE RID: 174
		ComputerName = 32,
		// Token: 0x040000AF RID: 175
		ServerIP = 64,
		// Token: 0x040000B0 RID: 176
		Method = 128,
		// Token: 0x040000B1 RID: 177
		UriStem = 256,
		// Token: 0x040000B2 RID: 178
		UriQuery = 512,
		// Token: 0x040000B3 RID: 179
		HttpStatus = 1024,
		// Token: 0x040000B4 RID: 180
		Win32Status = 2048,
		// Token: 0x040000B5 RID: 181
		BytesSent = 4096,
		// Token: 0x040000B6 RID: 182
		BytesRecv = 8192,
		// Token: 0x040000B7 RID: 183
		TimeTaken = 16384,
		// Token: 0x040000B8 RID: 184
		ServerPort = 32768,
		// Token: 0x040000B9 RID: 185
		UserAgent = 65536,
		// Token: 0x040000BA RID: 186
		Cookie = 131072,
		// Token: 0x040000BB RID: 187
		Referer = 262144,
		// Token: 0x040000BC RID: 188
		ProtocolVersion = 524288,
		// Token: 0x040000BD RID: 189
		Host = 1048576,
		// Token: 0x040000BE RID: 190
		HttpSubStatus = 2097152
	}
}
