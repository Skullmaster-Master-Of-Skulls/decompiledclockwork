using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000C59 RID: 3161
	[DataContract]
	public abstract class StringFormatSelector : SettingsNode
	{
		// Token: 0x06007763 RID: 30563 RVA: 0x001BAB59 File Offset: 0x001B8D59
		[Obsolete("Obsoleted since 2013 Q2 SP1. Please use SelectStringFormat method.", false)]
		public virtual string SelectStringFormat(Type dataType, PropertyAggregateDescriptionBase aggregateDescription)
		{
			return null;
		}

		// Token: 0x06007764 RID: 30564 RVA: 0x001BAB5C File Offset: 0x001B8D5C
		protected internal virtual string SelectStringFormat()
		{
			return null;
		}
	}
}
