using System;
using System.Collections.Generic;
using Telerik.Web.UI.PivotGrid.Core.DataProviders;
using Telerik.Web.UI.PivotGrid.Core.Engine;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D09 RID: 3337
	public abstract class OlapDataProvider : DataProviderBase
	{
		// Token: 0x06007C58 RID: 31832 RVA: 0x001C963F File Offset: 0x001C783F
		internal OlapDataProvider(IPivotSettings settings) : this(settings, null)
		{
		}

		// Token: 0x06007C59 RID: 31833 RVA: 0x001C9649 File Offset: 0x001C7849
		internal OlapDataProvider(IPivotSettings settings, IFieldDescriptionProvider fieldInfoProvider) : base(settings, fieldInfoProvider)
		{
		}

		// Token: 0x06007C5A RID: 31834 RVA: 0x001C9654 File Offset: 0x001C7854
		internal static List<List<HashSet<object>>> GenerateUniqueKeys()
		{
			return new List<List<HashSet<object>>>();
		}

		// Token: 0x06007C5B RID: 31835 RVA: 0x001C9668 File Offset: 0x001C7868
		internal static OlapPivotResults GetEmptyResults()
		{
			OlapAggregateResultProvider aggregatesProvider = new OlapAggregateResultProvider(default(Coordinate), new Dictionary<Coordinate, AggregateValue[]>());
			return new OlapPivotResults(new PivotResultsProcessingState
			{
				AggregatesProvider = aggregatesProvider
			});
		}
	}
}
