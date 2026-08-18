using System;

namespace System.Web.Configuration
{
	// Token: 0x020006BC RID: 1724
	internal enum ClsCtx
	{
		// Token: 0x04002BAE RID: 11182
		Inproc = 3,
		// Token: 0x04002BAF RID: 11183
		Server = 21,
		// Token: 0x04002BB0 RID: 11184
		All = 23,
		// Token: 0x04002BB1 RID: 11185
		InprocServer = 1,
		// Token: 0x04002BB2 RID: 11186
		InprocHandler,
		// Token: 0x04002BB3 RID: 11187
		LocalServer = 4,
		// Token: 0x04002BB4 RID: 11188
		InprocServer16 = 8,
		// Token: 0x04002BB5 RID: 11189
		RemoteServer = 16,
		// Token: 0x04002BB6 RID: 11190
		InprocHandler16 = 32,
		// Token: 0x04002BB7 RID: 11191
		InprocServerX86 = 64,
		// Token: 0x04002BB8 RID: 11192
		InprocHandlerX86 = 128,
		// Token: 0x04002BB9 RID: 11193
		EServerHandler = 256,
		// Token: 0x04002BBA RID: 11194
		Reserved = 512,
		// Token: 0x04002BBB RID: 11195
		NoCodeDownload = 1024,
		// Token: 0x04002BBC RID: 11196
		NoWX86Translation = 2048,
		// Token: 0x04002BBD RID: 11197
		NoCustomMarshal = 4096,
		// Token: 0x04002BBE RID: 11198
		EnableCodeDownload = 8192,
		// Token: 0x04002BBF RID: 11199
		NoFailureLog = 16384,
		// Token: 0x04002BC0 RID: 11200
		DisableAAA = 32768,
		// Token: 0x04002BC1 RID: 11201
		EnableAAA = 65536,
		// Token: 0x04002BC2 RID: 11202
		FromDefaultContext = 131072
	}
}
