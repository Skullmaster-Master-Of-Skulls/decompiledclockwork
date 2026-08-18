using System;
using System.Diagnostics;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CA5 RID: 3237
	public static class TelerikPivotTraceSources
	{
		// Token: 0x17002726 RID: 10022
		// (get) Token: 0x06007975 RID: 31093 RVA: 0x001BE8ED File Offset: 0x001BCAED
		public static TraceSource DataProviderSource
		{
			get
			{
				if (TelerikPivotTraceSources.dataProviderSource == null)
				{
					TelerikPivotTraceSources.dataProviderSource = new TraceSource("Telerik.Web.UI.PivotGrid.DataProviders", SourceLevels.Error);
				}
				return TelerikPivotTraceSources.dataProviderSource;
			}
		}

		// Token: 0x0400212E RID: 8494
		private static TraceSource dataProviderSource;
	}
}
