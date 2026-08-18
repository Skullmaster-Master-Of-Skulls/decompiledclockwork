using System;
using System.Diagnostics;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CA1 RID: 3233
	internal class DefaultTraceWriter : IPivotTraceWriter
	{
		// Token: 0x0600796B RID: 31083 RVA: 0x001BE86A File Offset: 0x001BCA6A
		public void WriteLine(string text)
		{
			TelerikPivotTraceSources.DataProviderSource.TraceEvent(TraceEventType.Error, 1, text);
		}
	}
}
