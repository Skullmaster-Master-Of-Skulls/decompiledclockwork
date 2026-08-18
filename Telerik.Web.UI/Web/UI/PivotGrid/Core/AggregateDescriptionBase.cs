using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Totals;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000682 RID: 1666
	[DataContract]
	public abstract class AggregateDescriptionBase : DescriptionBase, IAggregateDescription, IDescriptionBase, INamed, ITotalFormatHost, IDescriptionsReferencing
	{
		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x06003CB3 RID: 15539 RVA: 0x000C443E File Offset: 0x000C263E
		// (set) Token: 0x06003CB4 RID: 15540 RVA: 0x000C4446 File Offset: 0x000C2646
		[DataMember]
		public TotalFormat TotalFormat
		{
			get
			{
				return this.totalFormat;
			}
			set
			{
				if (this.totalFormat != value)
				{
					base.ChangeSettingsProperty<TotalFormat>(ref this.totalFormat, value);
					base.OnPropertyChanged("TotalFormat");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06003CB5 RID: 15541 RVA: 0x000C4474 File Offset: 0x000C2674
		public virtual bool DisplayValueAsKpi
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x000C4477 File Offset: 0x000C2677
		bool IDescriptionsReferencing.TrackDescriptions(IDescriptionIndexMap map)
		{
			return this.TrackDescriptions(map);
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x000C4480 File Offset: 0x000C2680
		internal virtual bool TrackDescriptions(IDescriptionIndexMap map)
		{
			IDescriptionsReferencing descriptionsReferencing = this.TotalFormat as IDescriptionsReferencing;
			if (descriptionsReferencing != null && !descriptionsReferencing.TrackDescriptions(map))
			{
				this.TotalFormat = null;
			}
			return true;
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x000C44AF File Offset: 0x000C26AF
		internal virtual string GetEffectiveFormat()
		{
			return null;
		}

		// Token: 0x06003CB9 RID: 15545
		internal abstract RequiredField GetRequiredField();

		// Token: 0x06003CBA RID: 15546 RVA: 0x000C44B4 File Offset: 0x000C26B4
		protected override void CloneCore(Cloneable source)
		{
			base.CloneCore(source);
			AggregateDescriptionBase aggregateDescriptionBase = source as AggregateDescriptionBase;
			if (aggregateDescriptionBase != null)
			{
				this.TotalFormat = Cloneable.CloneOrDefault<TotalFormat>(aggregateDescriptionBase.TotalFormat);
				base.CustomName = aggregateDescriptionBase.CustomName;
			}
		}

		// Token: 0x04001048 RID: 4168
		private TotalFormat totalFormat;
	}
}
