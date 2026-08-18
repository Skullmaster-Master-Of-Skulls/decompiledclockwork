using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000684 RID: 1668
	[DataContract]
	public abstract class LocalAggregateDescription : AggregateDescriptionBase, IStringFormattableAggregate
	{
		// Token: 0x06003CC0 RID: 15552 RVA: 0x000C44F7 File Offset: 0x000C26F7
		internal LocalAggregateDescription()
		{
		}

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x000C44FF File Offset: 0x000C26FF
		// (set) Token: 0x06003CC2 RID: 15554 RVA: 0x000C4507 File Offset: 0x000C2707
		internal PropertyFieldInfo FieldInfo { get; set; }

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x06003CC3 RID: 15555 RVA: 0x000C4510 File Offset: 0x000C2710
		// (set) Token: 0x06003CC4 RID: 15556 RVA: 0x000C4518 File Offset: 0x000C2718
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

		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x06003CC5 RID: 15557 RVA: 0x000C4545 File Offset: 0x000C2745
		// (set) Token: 0x06003CC6 RID: 15558 RVA: 0x000C454D File Offset: 0x000C274D
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

		// Token: 0x06003CC7 RID: 15559 RVA: 0x000C456C File Offset: 0x000C276C
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Design choice.")]
		internal override string GetEffectiveFormat()
		{
			Type dataType = (this.FieldInfo == null) ? null : this.FieldInfo.DataType;
			if (this.StringFormatSelector != null)
			{
				return this.StringFormatSelector.SelectStringFormat();
			}
			string text = this.StringFormat;
			AggregateFunction aggregateFunction = this.GetAggregateFunction();
			if (aggregateFunction != null)
			{
				text = aggregateFunction.GetStringFormat(dataType, text);
			}
			if (base.TotalFormat != null)
			{
				text = base.TotalFormat.GetStringFormat(dataType, text);
			}
			return text;
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x000C45D5 File Offset: 0x000C27D5
		internal override IPivotFieldInfo GetFieldInfo()
		{
			return this.FieldInfo;
		}

		// Token: 0x06003CC9 RID: 15561
		internal abstract AggregateFunction GetAggregateFunction();

		// Token: 0x06003CCA RID: 15562
		protected internal abstract object GetValueForItem(object item);

		// Token: 0x06003CCB RID: 15563 RVA: 0x000C45E0 File Offset: 0x000C27E0
		protected override void CloneCore(Cloneable source)
		{
			base.CloneCore(source);
			LocalAggregateDescription localAggregateDescription = source as LocalAggregateDescription;
			if (localAggregateDescription != null)
			{
				this.StringFormatSelector = Cloneable.CloneOrDefault<StringFormatSelector>(localAggregateDescription.StringFormatSelector);
				this.StringFormat = localAggregateDescription.StringFormat;
				this.FieldInfo = localAggregateDescription.FieldInfo;
			}
		}

		// Token: 0x04001049 RID: 4169
		private string stringFormat;

		// Token: 0x0400104A RID: 4170
		private StringFormatSelector stringFormatSelector;
	}
}
