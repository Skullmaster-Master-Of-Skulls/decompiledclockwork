using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x02000725 RID: 1829
	[DataContract]
	public abstract class QueryableAggregateDescriptionBase : AggregateDescriptionBase, IStringFormattableAggregate
	{
		// Token: 0x060040DE RID: 16606 RVA: 0x000CC4D5 File Offset: 0x000CA6D5
		internal QueryableAggregateDescriptionBase()
		{
		}

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x060040DF RID: 16607 RVA: 0x000CC4DD File Offset: 0x000CA6DD
		// (set) Token: 0x060040E0 RID: 16608 RVA: 0x000CC4E5 File Offset: 0x000CA6E5
		[DataMember]
		public string StringFormat
		{
			get
			{
				return this.stringFormat;
			}
			set
			{
				if (this.stringFormat != value)
				{
					this.stringFormat = value;
					base.OnPropertyChanged("StringFormat");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x060040E1 RID: 16609 RVA: 0x000CC512 File Offset: 0x000CA712
		// (set) Token: 0x060040E2 RID: 16610 RVA: 0x000CC51A File Offset: 0x000CA71A
		[DataMember]
		public StringFormatSelector StringFormatSelector
		{
			get
			{
				return this.stringFormatSelector;
			}
			set
			{
				if (this.stringFormatSelector != value)
				{
					this.stringFormatSelector = value;
					base.OnPropertyChanged("StringFormatSelector");
				}
			}
		}

		// Token: 0x060040E3 RID: 16611
		internal abstract AggregateValue CreateAggregate();

		// Token: 0x060040E4 RID: 16612 RVA: 0x000CC538 File Offset: 0x000CA738
		protected override void CloneCore(Cloneable source)
		{
			base.CloneCore(source);
			QueryableAggregateDescriptionBase queryableAggregateDescriptionBase = source as QueryableAggregateDescriptionBase;
			if (queryableAggregateDescriptionBase != null)
			{
				this.StringFormat = queryableAggregateDescriptionBase.StringFormat;
				this.StringFormatSelector = Cloneable.CloneOrDefault<StringFormatSelector>(queryableAggregateDescriptionBase.StringFormatSelector);
			}
		}

		// Token: 0x04001137 RID: 4407
		private string stringFormat;

		// Token: 0x04001138 RID: 4408
		private StringFormatSelector stringFormatSelector;
	}
}
