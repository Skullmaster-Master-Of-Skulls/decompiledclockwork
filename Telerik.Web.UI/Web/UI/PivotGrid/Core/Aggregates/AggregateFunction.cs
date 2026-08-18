using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C46 RID: 3142
	[DataContract]
	[TypeConverter(typeof(AggregateFunctionConverter))]
	public abstract class AggregateFunction : SettingsNode, INamed
	{
		// Token: 0x170026AA RID: 9898
		// (get) Token: 0x060076DB RID: 30427
		public abstract string DisplayName { get; }

		// Token: 0x060076DC RID: 30428 RVA: 0x001B9BE2 File Offset: 0x001B7DE2
		public virtual string GetStringFormat(Type dataType, string format)
		{
			return null;
		}

		// Token: 0x060076DD RID: 30429 RVA: 0x001B9BE5 File Offset: 0x001B7DE5
		protected internal virtual AggregateValue CreateAggregate(Type dataType)
		{
			return null;
		}

		// Token: 0x060076DE RID: 30430 RVA: 0x001B9BE8 File Offset: 0x001B7DE8
		protected internal virtual AggregateValue CreateAggregate(IAggregateContext context)
		{
			if (context.HasCalculatedGroups)
			{
				return AggregateValue.ErrorAggregateValue;
			}
			return this.CreateAggregate(context.DataType);
		}
	}
}
