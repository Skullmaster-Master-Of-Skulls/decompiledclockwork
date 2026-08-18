using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001E8 RID: 488
	internal static class ComPlusTLBImportTrace
	{
		// Token: 0x06000FB6 RID: 4022 RVA: 0x00038518 File Offset: 0x00036718
		public static void Trace(TraceEventType type, int traceCode, string description, Guid iid, Guid typeLibraryID)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				ComPlusTLBImportSchema extendedData = new ComPlusTLBImportSchema(iid, typeLibraryID);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00038544 File Offset: 0x00036744
		public static void Trace(TraceEventType type, int traceCode, string description, Guid iid, Guid typeLibraryID, string assembly)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				ComPlusTLBImportFromAssemblySchema extendedData = new ComPlusTLBImportFromAssemblySchema(iid, typeLibraryID, assembly);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00038574 File Offset: 0x00036774
		public static void Trace(TraceEventType type, int traceCode, string description, Guid iid, Guid typeLibraryID, ImporterEventKind eventKind, int eventCode, string eventMsg)
		{
			if (DiagnosticUtility.ShouldTrace(type))
			{
				ComPlusTLBImportConverterEventSchema extendedData = new ComPlusTLBImportConverterEventSchema(iid, typeLibraryID, eventKind, eventCode, eventMsg);
				TraceUtility.TraceEvent(type, traceCode, SR.GetString(description), extendedData);
			}
		}
	}
}
