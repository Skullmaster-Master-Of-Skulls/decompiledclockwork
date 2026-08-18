using System;
using System.Diagnostics;

namespace System.Data
{
	// Token: 0x02000098 RID: 152
	internal sealed class DataSetTraceSource : TraceSource
	{
		// Token: 0x06000913 RID: 2323 RVA: 0x001FDDF8 File Offset: 0x001FD1F8
		private DataSetTraceSource() : base("System.Data.DataSet")
		{
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x001FDE18 File Offset: 0x001FD218
		internal static void TraceTypeNotAllowed(Type type)
		{
			TraceEventType eventType = SerializationConfig.IsAuditMode() ? TraceEventType.Warning : TraceEventType.Error;
			DataSetTraceSource dataSetTraceSource = DataSetTraceSource.s_singleton;
			if (dataSetTraceSource.Switch.ShouldTrace(eventType))
			{
				dataSetTraceSource.TraceEvent(eventType, 1, Res.GetString("Data_TypeNotAllowed", new object[]
				{
					type.AssemblyQualifiedName
				}));
			}
		}

		// Token: 0x040007C5 RID: 1989
		private const int DisallowedTypeSeenEventId = 1;

		// Token: 0x040007C6 RID: 1990
		private static readonly DataSetTraceSource s_singleton = new DataSetTraceSource();
	}
}
