using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Fields;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.ReportFilter;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000D4F RID: 3407
	[DataContract]
	public abstract class PropertyFilterDescriptionBase : FilterDescription, IInitializeDescription, IReportFilterDescription, IFilteringDescription, IDistinctValuesDescription, IConditionFactory, IFilterOperatorsProvider
	{
		// Token: 0x1700287F RID: 10367
		// (get) Token: 0x06007F02 RID: 32514 RVA: 0x001D0F60 File Offset: 0x001CF160
		// (set) Token: 0x06007F03 RID: 32515 RVA: 0x001D0F68 File Offset: 0x001CF168
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

		// Token: 0x17002880 RID: 10368
		// (get) Token: 0x06007F04 RID: 32516 RVA: 0x001D0FA0 File Offset: 0x001CF1A0
		internal virtual bool TransformsData
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002881 RID: 10369
		// (get) Token: 0x06007F05 RID: 32517 RVA: 0x001D0FA3 File Offset: 0x001CF1A3
		// (set) Token: 0x06007F06 RID: 32518 RVA: 0x001D0FAB File Offset: 0x001CF1AB
		internal PropertyFieldInfo FieldInfo { get; set; }

		// Token: 0x06007F07 RID: 32519 RVA: 0x001D0FB4 File Offset: 0x001CF1B4
		internal override IPivotFieldInfo GetFieldInfo()
		{
			return this.FieldInfo;
		}

		// Token: 0x17002882 RID: 10370
		// (get) Token: 0x06007F08 RID: 32520 RVA: 0x001D0FBC File Offset: 0x001CF1BC
		// (set) Token: 0x06007F09 RID: 32521 RVA: 0x001D0FC4 File Offset: 0x001CF1C4
		[DataMember]
		public LocalCondition Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				if (this.condition != value)
				{
					base.ChangeSettingsProperty<LocalCondition>(ref this.condition, value);
					base.OnPropertyChanged("Condition");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x06007F0A RID: 32522 RVA: 0x001D0FF2 File Offset: 0x001CF1F2
		protected object ExtractValue(object item)
		{
			if (this.FieldInfo == null)
			{
				throw new InvalidOperationException("Member access  has not been initialized. Most probably item does not have property with name: " + this.PropertyName);
			}
			return this.FieldInfo.GetValue(item);
		}

		// Token: 0x06007F0B RID: 32523 RVA: 0x001D101E File Offset: 0x001CF21E
		protected internal virtual object GetFilterItem(object fact)
		{
			if (this.FieldInfo != null)
			{
				return this.FieldInfo.GetValue(fact);
			}
			return null;
		}

		// Token: 0x06007F0C RID: 32524 RVA: 0x001D1036 File Offset: 0x001CF236
		protected internal virtual bool PassesFilter(object value)
		{
			return this.condition == null || !this.condition.IsActive || this.condition.PassesFilter(value);
		}

		// Token: 0x06007F0D RID: 32525 RVA: 0x001D105C File Offset: 0x001CF25C
		protected sealed override void CloneCore(Cloneable source)
		{
			this.CloneOverride(source);
			PropertyFilterDescriptionBase propertyFilterDescriptionBase = source as PropertyFilterDescriptionBase;
			if (propertyFilterDescriptionBase != null)
			{
				this.Condition = Cloneable.CloneOrDefault<LocalCondition>(propertyFilterDescriptionBase.Condition);
				this.PropertyName = propertyFilterDescriptionBase.PropertyName;
				this.FieldInfo = propertyFilterDescriptionBase.FieldInfo;
			}
			base.CloneCore(source);
		}

		// Token: 0x06007F0E RID: 32526
		protected abstract void CloneOverride(Cloneable source);

		// Token: 0x06007F0F RID: 32527 RVA: 0x001D10AC File Offset: 0x001CF2AC
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

		// Token: 0x06007F10 RID: 32528 RVA: 0x001D10EC File Offset: 0x001CF2EC
		public override string GetUniqueName()
		{
			return this.PropertyName;
		}

		// Token: 0x17002883 RID: 10371
		// (get) Token: 0x06007F11 RID: 32529 RVA: 0x001D10F4 File Offset: 0x001CF2F4
		bool IInitializeDescription.Initialized
		{
			get
			{
				return this.FieldInfo != null;
			}
		}

		// Token: 0x06007F12 RID: 32530 RVA: 0x001D1102 File Offset: 0x001CF302
		void IInitializeDescription.Initialize(IDataProvider initializingProvider)
		{
			if (initializingProvider == null)
			{
				return;
			}
			this.provider = initializingProvider;
			this.FieldInfo = (initializingProvider.FieldInfos.GetFieldDescriptionByMember(this.PropertyName) as PropertyFieldInfo);
		}

		// Token: 0x17002884 RID: 10372
		// (get) Token: 0x06007F13 RID: 32531 RVA: 0x001D112B File Offset: 0x001CF32B
		// (set) Token: 0x06007F14 RID: 32532 RVA: 0x001D1133 File Offset: 0x001CF333
		Condition IReportFilterDescription.Condition
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.Condition = (value as LocalCondition);
			}
		}

		// Token: 0x06007F15 RID: 32533 RVA: 0x001D1141 File Offset: 0x001CF341
		Condition IConditionFactory.CreateCondition(Type conditionType)
		{
			return DescriptionBase.CreateLocalCondition(conditionType);
		}

		// Token: 0x06007F16 RID: 32534 RVA: 0x001D114C File Offset: 0x001CF34C
		DistinctValuesProvider IDistinctValuesDescription.GetDisctinctValuesProvider()
		{
			if (this.provider == null)
			{
				return null;
			}
			return new LocalDistinctValuesProvider(this.provider, this);
		}

		// Token: 0x17002885 RID: 10373
		// (get) Token: 0x06007F17 RID: 32535 RVA: 0x001D1171 File Offset: 0x001CF371
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

		// Token: 0x17002886 RID: 10374
		// (get) Token: 0x06007F18 RID: 32536 RVA: 0x001D1191 File Offset: 0x001CF391
		bool IFilteringDescription.PrefersDistinct
		{
			get
			{
				return this.TransformsData;
			}
		}

		// Token: 0x06007F19 RID: 32537 RVA: 0x001D1199 File Offset: 0x001CF399
		IEnumerable<object> IFilterOperatorsProvider.GetAvailableConditions()
		{
			return null;
		}

		// Token: 0x040022FC RID: 8956
		private string propertyName;

		// Token: 0x040022FD RID: 8957
		private LocalCondition condition;

		// Token: 0x040022FE RID: 8958
		private IDataProvider provider;
	}
}
