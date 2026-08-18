using System;
using System.Runtime.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A8E RID: 2702
	internal class PerformanceCounterTraceRecord : TraceRecord
	{
		// Token: 0x06006ABA RID: 27322 RVA: 0x0018DFAE File Offset: 0x0018C1AE
		internal PerformanceCounterTraceRecord(string perfCounterName) : this(null, perfCounterName, null)
		{
		}

		// Token: 0x06006ABB RID: 27323 RVA: 0x0018DFB9 File Offset: 0x0018C1B9
		internal PerformanceCounterTraceRecord(string categoryName, string perfCounterName) : this(categoryName, perfCounterName, null)
		{
		}

		// Token: 0x06006ABC RID: 27324 RVA: 0x0018DFC4 File Offset: 0x0018C1C4
		internal PerformanceCounterTraceRecord(string categoryName, string perfCounterName, string instanceName)
		{
			this.categoryName = categoryName;
			this.perfCounterName = perfCounterName;
			this.instanceName = instanceName;
		}

		// Token: 0x17001967 RID: 6503
		// (get) Token: 0x06006ABD RID: 27325 RVA: 0x0018DFE1 File Offset: 0x0018C1E1
		internal override string EventId
		{
			get
			{
				return base.BuildEventId("PerformanceCounter");
			}
		}

		// Token: 0x06006ABE RID: 27326 RVA: 0x0018DFF0 File Offset: 0x0018C1F0
		internal override void WriteTo(XmlWriter writer)
		{
			if (!string.IsNullOrEmpty(this.categoryName))
			{
				writer.WriteElementString("PerformanceCategoryName", this.categoryName);
			}
			writer.WriteElementString("PerformanceCounterName", this.perfCounterName);
			if (!string.IsNullOrEmpty(this.instanceName))
			{
				writer.WriteElementString("InstanceName", this.instanceName);
			}
		}

		// Token: 0x04003CC5 RID: 15557
		private string categoryName;

		// Token: 0x04003CC6 RID: 15558
		private string perfCounterName;

		// Token: 0x04003CC7 RID: 15559
		private string instanceName;
	}
}
