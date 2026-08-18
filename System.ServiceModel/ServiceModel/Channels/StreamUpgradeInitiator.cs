using System;
using System.IO;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000835 RID: 2101
	public abstract class StreamUpgradeInitiator
	{
		// Token: 0x06004E7B RID: 20091
		public abstract string GetNextUpgrade();

		// Token: 0x06004E7C RID: 20092
		public abstract Stream InitiateUpgrade(Stream stream);

		// Token: 0x06004E7D RID: 20093
		public abstract IAsyncResult BeginInitiateUpgrade(Stream stream, AsyncCallback callback, object state);

		// Token: 0x06004E7E RID: 20094
		public abstract Stream EndInitiateUpgrade(IAsyncResult result);

		// Token: 0x06004E7F RID: 20095 RVA: 0x0011E497 File Offset: 0x0011C697
		internal virtual IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x0011E4A0 File Offset: 0x0011C6A0
		internal virtual void EndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06004E81 RID: 20097 RVA: 0x0011E4A8 File Offset: 0x0011C6A8
		internal virtual IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x0011E4B1 File Offset: 0x0011C6B1
		internal virtual void EndClose(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x0011E4B9 File Offset: 0x0011C6B9
		internal virtual void Open(TimeSpan timeout)
		{
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x0011E4BB File Offset: 0x0011C6BB
		internal virtual void Close(TimeSpan timeout)
		{
		}
	}
}
