using System;
using System.Net.Http;

namespace System.Web.Http.Tracing
{
	// Token: 0x02000153 RID: 339
	public interface ITraceWriter
	{
		// Token: 0x06000872 RID: 2162
		void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction);
	}
}
