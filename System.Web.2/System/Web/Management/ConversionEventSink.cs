using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Web.Management
{
	// Token: 0x02000174 RID: 372
	internal class ConversionEventSink : ITypeLibExporterNotifySink
	{
		// Token: 0x0600149D RID: 5277 RVA: 0x00006164 File Offset: 0x00004364
		public void ReportEvent(ExporterEventKind eventKind, int eventCode, string eventMsg)
		{
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0000298D File Offset: 0x00000B8D
		public object ResolveRef(Assembly assemblyReference)
		{
			return null;
		}
	}
}
