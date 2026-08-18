using System;
using System.Diagnostics;

namespace System.Data
{
	// Token: 0x020000CC RID: 204
	internal sealed class DataSetTraceSource : TraceSource
	{
		// Token: 0x06000C53 RID: 3155 RVA: 0x00068B58 File Offset: 0x00067F58
		private DataSetTraceSource() : base("System.Data.DataSet")
		{
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00068B70 File Offset: 0x00067F70
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

		// Token: 0x040003A3 RID: 931
		private const int DisallowedTypeSeenEventId = 1;

		// Token: 0x040003A4 RID: 932
		private static readonly DataSetTraceSource s_singleton = new DataSetTraceSource();
	}
}
