using System;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200003F RID: 63
	internal abstract class SmiEventStream : IDisposable
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001FF RID: 511
		internal abstract bool HasEvents { get; }

		// Token: 0x06000200 RID: 512
		internal abstract void Close(SmiEventSink sink);

		// Token: 0x06000201 RID: 513 RVA: 0x0003A480 File Offset: 0x00039880
		public virtual void Dispose()
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000202 RID: 514
		internal abstract void ProcessEvent(SmiEventSink sink);
	}
}
