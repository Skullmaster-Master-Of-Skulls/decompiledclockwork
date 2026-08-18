using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Fields;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.ReportFilter;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CC6 RID: 3270
	[DataContract]
	public abstract class PropertyGroupDescriptionBase : GroupDescription, IInitializeDescription, ILabelGroupFilterHost, IValueGroupFilterHost, IGroupsCountFilterHost, IGroupsPercentFilterHost, IGroupsSumFilterHost, ICanShowEmptyGroups, IFilteringDescription, IDistinctValuesDescription, IConditionFactory, IFilterOperatorsProvider
	{
		// Token: 0x17002747 RID: 10055
		// (get) Token: 0x06007A3E RID: 31294 RVA: 0x001C03EB File Offset: 0x001BE5EB
		// (set) Token: 0x06007A3F RID: 31295 RVA: 0x001C03F3 File Offset: 0x001BE5F3
		[DataMember]
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
			set
			{
				if (this.propertyName != value)
				{
					this.propertyName = value;
					base.OnPropertyChanged("PropertyName");
					base.OnPropertyChanged("DisplayName");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x17002748 RID: 10056
		// (get) Token: 0x06007A40 RID: 31296 RVA: 0x001C042B File Offset: 0x001BE62B
		// (set) Token: 0x06007A41 RID: 31297 RVA: 0x001C0433 File Offset: 0x001BE633
		protected internal CultureInfo Culture
		{
			get
			{
				return this.cultureInfo;
			}
			internal set
			{
				if (this.cultureInfo != value)
				{
					this.cultureInfo = value;
				}
			}
		}

		// Token: 0x17002749 RID: 10057
		// (get) Token: 0x06007A42 RID: 31298 RVA: 0x001C0445 File Offset: 0x001BE645
		[DataMember]
		public Collection<CalculatedItem> CalculatedItems
		{
			get
			{
				if (this.calculatedItems == null)
				{
					this.calculatedItems = new Collection<CalculatedItem>();
				}
				return this.calculatedItems;
			}
		}

		// Token: 0x1700274A RID: 10058
		// (get) Token: 0x06007A43 RID: 31299 RVA: 0x001C0460 File Offset: 0x001BE660
		internal virtual bool TransformsData
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700274B RID: 10059
		// (get) Token: 0x06007A44 RID: 31300 RVA: 0x001C0463 File Offset: 0x001BE663
		// (set) Token: 0x06007A45 RID: 31301 RVA: 0x001C046B File Offset: 0x001BE66B
		internal PropertyFieldInfo FieldInfo { get; set; }

		// Token: 0x06007A46 RID: 31302 RVA: 0x001C0474 File Offset: 0x001BE674
		internal override IPivotFieldInfo GetFieldInfo()
		{
			return this.FieldInfo;
		}

		// Token: 0x06007A47 RID: 31303 RVA: 0x001C047C File Offset: 0x001BE67C
		protected internal virtual object GroupNameFromItem(object item, int level)
		{
			if (this.FieldInfo == null)
			{
				throw new InvalidOperationException("Member access has not been initialized. Most probably item does not have property with name: " + this.PropertyName);
			}
			object value = this.FieldInfo.GetValue(item);
			return PropertyGroupDescriptionBase.ReturnInvalidValuesAsNull(value);
		}

		// Token: 0x06007A48 RID: 31304 RVA: 0x001C04BC File Offset: 0x001BE6BC
		private static object ReturnInvalidValuesAsNull(object value)
		{
			return value;
		}

		// Token: 0x06007A49 RID: 31305 RVA: 0x001C04BF File Offset: 0x001BE6BF
		protected internal override IEnumerable<object> GetAllNames(IEnumerable<object> uniqueNames, IEnumerable<object> parentGroupNames)
		{
			return uniqueNames.Concat(this.CalculatedItems.OfType<object>());
		}

		// Token: 0x06007A4A RID: 31306 RVA: 0x001C04D4 File Offset: 0x001BE6D4
		protected sealed override void CloneCore(Cloneable source)
		{
			base.CloneCore(source);
			PropertyGroupDescriptionBase propertyGroupDescriptionBase = source as PropertyGroupDescriptionBase;
			if (propertyGroupDescriptionBase != null)
			{
				this.PropertyName = propertyGroupDescriptionBase.PropertyName;
				this.FieldInfo = propertyGroupDescriptionBase.FieldInfo;
				this.Culture = propertyGroupDescriptionBase.Culture;
				this.CalculatedItems.Clear();
				foreach (CalculatedItem item in propertyGroupDescriptionBase.CalculatedItems)
				{
					this.calculatedItems.Add(item);
				}
			}
			this.CloneOverride(source);
		}

		// Token: 0x06007A4B RID: 31307
		protected abstract void CloneOverride(Cloneable source);

		// Token: 0x06007A4C RID: 31308 RVA: 0x001C0570 File Offset: 0x001BE770
		protected override string GetDisplayName()
		{
			string displayName = base.GetDisplayName();
			if (displayName != null)
			{
				return displayName;
			}
			if (this.FieldInfo != null && this.FieldInfo.DisplayName != null)
			{
				return this.FieldInfo.DisplayName;
			}
			return this.PropertyName;
		}

		// Token: 0x06007A4D RID: 31309 RVA: 0x001C05B0 File Offset: 0x001BE7B0
		public override string GetUniqueName()
		{
			return this.PropertyName;
		}

		// Token: 0x1700274C RID: 10060
		// (get) Token: 0x06007A4E RID: 31310 RVA: 0x001C05B8 File Offset: 0x001BE7B8
		bool IInitializeDescription.Initialized
		{
			get
			{
				return this.FieldInfo != null;
			}
		}

		// Token: 0x06007A4F RID: 31311 RVA: 0x001C05C8 File Offset: 0x001BE7C8
		void IInitializeDescription.Initialize(IDataProvider provider)
		{
			if (provider == null)
			{
				return;
			}
			base.Provider = provider;
			this.FieldInfo = (provider.FieldInfos.GetFieldDescriptionByMember(this.GetUniqueName()) as PropertyFieldInfo);
			LocalDataSourceProvider localDataSourceProvider = base.Provider as LocalDataSourceProvider;
			if (localDataSourceProvider != null)
			{
				this.Culture = localDataSourceProvider.Culture;
			}
		}

		// Token: 0x06007A50 RID: 31312 RVA: 0x001C0617 File Offset: 0x001BE817
		ILabelGroupFilter ILabelGroupFilterHost.CreateFilter()
		{
			return new LabelGroupFilter();
		}

		// Token: 0x06007A51 RID: 31313 RVA: 0x001C061E File Offset: 0x001BE81E
		IValueGroupFilter IValueGroupFilterHost.CreateFilter()
		{
			return new ValueGroupFilter();
		}

		// Token: 0x06007A52 RID: 31314 RVA: 0x001C0625 File Offset: 0x001BE825
		IGroupsCountFilter IGroupsCountFilterHost.CreateFilter()
		{
			return new GroupsCountFilter();
		}

		// Token: 0x06007A53 RID: 31315 RVA: 0x001C062C File Offset: 0x001BE82C
		IGroupsSumFilter IGroupsSumFilterHost.CreateFilter()
		{
			return new GroupsSumFilter();
		}

		// Token: 0x06007A54 RID: 31316 RVA: 0x001C0633 File Offset: 0x001BE833
		IGroupsPercentFilter IGroupsPercentFilterHost.CreateFilter()
		{
			return new GroupsPercentFilter();
		}

		// Token: 0x1700274D RID: 10061
		// (get) Token: 0x06007A55 RID: 31317 RVA: 0x001C063A File Offset: 0x001BE83A
		Type IFilteringDescription.FilteringType
		{
			get
			{
				if (this.FieldInfo == null)
				{
					return typeof(object);
				}
				return this.FieldInfo.DataType;
			}
		}

		// Token: 0x1700274E RID: 10062
		// (get) Token: 0x06007A56 RID: 31318 RVA: 0x001C065A File Offset: 0x001BE85A
		bool IFilteringDescription.PrefersDistinct
		{
			get
			{
				return this.TransformsData;
			}
		}

		// Token: 0x06007A57 RID: 31319 RVA: 0x001C0664 File Offset: 0x001BE864
		DistinctValuesProvider IDistinctValuesDescription.GetDisctinctValuesProvider()
		{
			if (base.Provider == null)
			{
				return null;
			}
			return new LocalDistincsGroupKeysProvider(base.Provider, this);
		}

		// Token: 0x06007A58 RID: 31320 RVA: 0x001C0689 File Offset: 0x001BE889
		Condition IConditionFactory.CreateCondition(Type conditionType)
		{
			return DescriptionBase.CreateLocalCondition(conditionType);
		}

		// Token: 0x06007A59 RID: 31321 RVA: 0x001C0691 File Offset: 0x001BE891
		IEnumerable<object> IFilterOperatorsProvider.GetAvailableConditions()
		{
			return null;
		}

		// Token: 0x0400217D RID: 8573
		private string propertyName;

		// Token: 0x0400217E RID: 8574
		private Collection<CalculatedItem> calculatedItems = new Collection<CalculatedItem>();

		// Token: 0x0400217F RID: 8575
		private CultureInfo cultureInfo = CultureInfo.InvariantCulture;
	}
}
