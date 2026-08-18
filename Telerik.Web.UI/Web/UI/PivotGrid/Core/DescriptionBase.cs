using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Fields;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap;
using Telerik.Web.UI.PivotGrid.Filtering;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x0200067E RID: 1662
	[DataContract]
	public abstract class DescriptionBase : SettingsNode, IDescriptionBase, INamed
	{
		// Token: 0x06003CA3 RID: 15523 RVA: 0x000C4298 File Offset: 0x000C2498
		internal DescriptionBase()
		{
		}

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x06003CA4 RID: 15524 RVA: 0x000C42A0 File Offset: 0x000C24A0
		public string DisplayName
		{
			get
			{
				return this.GetDisplayName();
			}
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x06003CA5 RID: 15525 RVA: 0x000C42A8 File Offset: 0x000C24A8
		// (set) Token: 0x06003CA6 RID: 15526 RVA: 0x000C42B0 File Offset: 0x000C24B0
		[DataMember]
		public string CustomName
		{
			get
			{
				return this.customName;
			}
			set
			{
				if (this.customName != value)
				{
					this.customName = value;
					base.OnPropertyChanged("CustomName");
					base.OnPropertyChanged("DisplayName");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x000C42E8 File Offset: 0x000C24E8
		protected override void CloneCore(Cloneable source)
		{
			DescriptionBase descriptionBase = source as DescriptionBase;
			if (descriptionBase != null)
			{
				this.CustomName = descriptionBase.CustomName;
			}
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x000C430B File Offset: 0x000C250B
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Design choice.")]
		protected virtual string GetDisplayName()
		{
			if (!string.IsNullOrEmpty(this.CustomName))
			{
				return this.CustomName;
			}
			return null;
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x000C4322 File Offset: 0x000C2522
		IDescriptionBase IDescriptionBase.Clone()
		{
			return base.Clone() as IDescriptionBase;
		}

		// Token: 0x06003CAA RID: 15530
		internal abstract IPivotFieldInfo GetFieldInfo();

		// Token: 0x06003CAB RID: 15531
		public abstract string GetUniqueName();

		// Token: 0x06003CAC RID: 15532 RVA: 0x000C4330 File Offset: 0x000C2530
		internal static Condition CreateLocalCondition(Type conditionType)
		{
			if (conditionType == typeof(ISetCondition))
			{
				return new SetCondition();
			}
			if (conditionType == typeof(IComparisonCondition))
			{
				return new ComparisonCondition();
			}
			if (conditionType == typeof(ITextCondition))
			{
				return new TextCondition();
			}
			if (conditionType == typeof(IIntervalCondition))
			{
				return new IntervalCondition();
			}
			if (conditionType == typeof(IItemsFilterCondition))
			{
				return new ItemsFilterCondition();
			}
			return null;
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x000C43B8 File Offset: 0x000C25B8
		internal static Condition CreateOlapCondition(Type conditionType)
		{
			if (conditionType == typeof(ISetCondition))
			{
				return new OlapSetCondition();
			}
			if (conditionType == typeof(IComparisonCondition))
			{
				return new OlapComparisonCondition();
			}
			if (conditionType == typeof(ITextCondition))
			{
				return new OlapTextCondition();
			}
			if (conditionType == typeof(IIntervalCondition))
			{
				return new OlapIntervalCondition();
			}
			if (conditionType == typeof(IItemsFilterCondition))
			{
				return new OlapItemsFilterCondition();
			}
			return null;
		}

		// Token: 0x04001047 RID: 4167
		private string customName;
	}
}
