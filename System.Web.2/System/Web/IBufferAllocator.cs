using System;

namespace System.Web
{
	// Token: 0x0200003B RID: 59
	internal interface IBufferAllocator
	{
		// Token: 0x060004FA RID: 1274
		object GetBuffer();

		// Token: 0x060004FB RID: 1275
		void ReuseBuffer(object buffer);

		// Token: 0x060004FC RID: 1276
		void ReleaseAllBuffers();

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060004FD RID: 1277
		int BufferSize { get; }
	}
}
