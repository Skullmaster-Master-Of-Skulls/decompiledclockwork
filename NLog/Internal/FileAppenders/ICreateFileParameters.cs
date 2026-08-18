using System;
using NLog.Targets;

namespace NLog.Internal.FileAppenders
{
	// Token: 0x02000088 RID: 136
	internal interface ICreateFileParameters
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000482 RID: 1154
		int ConcurrentWriteAttemptDelay { get; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000483 RID: 1155
		int ConcurrentWriteAttempts { get; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000484 RID: 1156
		bool ConcurrentWrites { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000485 RID: 1157
		bool CreateDirs { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000486 RID: 1158
		bool EnableFileDelete { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000487 RID: 1159
		int BufferSize { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000488 RID: 1160
		bool ForceManaged { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000489 RID: 1161
		Win32FileAttributes FileAttributes { get; }
	}
}
