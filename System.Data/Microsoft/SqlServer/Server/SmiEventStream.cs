using System;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000039 RID: 57
	internal abstract class SmiEventStream : IDisposable
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000202 RID: 514
		internal abstract bool HasEvents { get; }

		// Token: 0x06000203 RID: 515
		internal abstract void Close(SmiEventSink sink);

		// Token: 0x06000204 RID: 516 RVA: 0x001DD348 File Offset: 0x001DC748
		public virtual void Dispose()
		{
			ADP.InternalError(ADP.InternalErrorCode.UnimplementedSMIMethod);
		}

		// Token: 0x06000205 RID: 517
		internal abstract void ProcessEvent(SmiEventSink sink);
	}
}
