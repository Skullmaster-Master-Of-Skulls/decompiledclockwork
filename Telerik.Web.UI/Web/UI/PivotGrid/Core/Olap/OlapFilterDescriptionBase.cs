using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Fields;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap.Expressions;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x020006EE RID: 1774
	[DataContract]
	public abstract class OlapFilterDescriptionBase : FilterDescription, IReportFilterDescription, IFilteringDescription, IDistinctValuesDescription, IConditionFactory, IFilterOperatorsProvider
	{
		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x06003F0B RID: 16139 RVA: 0x000C88D5 File Offset: 0x000C6AD5
		// (set) Token: 0x06003F0C RID: 16140 RVA: 0x000C88DD File Offset: 0x000C6ADD
		[DataMember]
		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Design choice.")]
		public string MemberName { get; set; }

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x06003F0D RID: 16141 RVA: 0x000C88E6 File Offset: 0x000C6AE6
		// (set) Token: 0x06003F0E RID: 16142 RVA: 0x000C88EE File Offset: 0x000C6AEE
		internal OlapDataProvider Provider { get; set; }

		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x06003F0F RID: 16143 RVA: 0x000C88F7 File Offset: 0x000C6AF7
		// (set) Token: 0x06003F10 RID: 16144 RVA: 0x000C88FF File Offset: 0x000C6AFF
		internal OlapHierarchyFieldInfo FieldInfo { get; set; }

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x06003F11 RID: 16145 RVA: 0x000C8908 File Offset: 0x000C6B08
		// (set) Token: 0x06003F12 RID: 16146 RVA: 0x000C8910 File Offset: 0x000C6B10
		[DataMember]
		public OlapCondition Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				if (this.condition != value)
				{
					base.ChangeSettingsProperty<OlapCondition>(ref this.condition, value);
					base.OnPropertyChanged("Condition");
					base.NotifySettingsChanged(new SettingsChangedEventArgs());
				}
			}
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x000C893E File Offset: 0x000C6B3E
		public override string GetUniqueName()
		{
			return this.MemberName;
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x000C8946 File Offset: 0x000C6B46
		internal override IPivotFieldInfo GetFieldInfo()
		{
			return this.FieldInfo;
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x000C8950 File Offset: 0x000C6B50
		protected override void CloneCore(Cloneable source)
		{
			OlapFilterDescriptionBase olapFilterDescriptionBase = source as OlapFilterDescriptionBase;
			if (olapFilterDescriptionBase != null)
			{
				this.Condition = Cloneable.CloneOrDefault<OlapCondition>(olapFilterDescriptionBase.Condition);
				this.MemberName = olapFilterDescriptionBase.MemberName;
				this.FieldInfo = olapFilterDescriptionBase.FieldInfo;
			}
			base.CloneCore(source);
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x000C8998 File Offset: 0x000C6B98
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
			return this.MemberName;
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x000C89D8 File Offset: 0x000C6BD8
		internal virtual IEnumerable<OlapExpression> GetExpressions()
		{
			if (this.Condition == null || this.FieldInfo == null)
			{
				return new List<OlapExpression>();
			}
			OlapExpressionOptions options = new OlapExpressionOptions
			{
				HierarchyInfo = this.FieldInfo
			};
			if (this.Condition.IsActive)
			{
				return this.Condition.GetExpressions(options);
			}
			return new List<OlapExpression>();
		}

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x06003F18 RID: 16152 RVA: 0x000C8A31 File Offset: 0x000C6C31
		// (set) Token: 0x06003F19 RID: 16153 RVA: 0x000C8A39 File Offset: 0x000C6C39
		Condition IReportFilterDescription.Condition
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.Condition = (value as OlapCondition);
			}
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x000C8A47 File Offset: 0x000C6C47
		Condition IConditionFactory.CreateCondition(Type conditionType)
		{
			return DescriptionBase.CreateOlapCondition(conditionType);
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x000C8A4F File Offset: 0x000C6C4F
		internal virtual DistinctValuesProvider GetDisctinctValuesProvider()
		{
			return null;
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x000C8A52 File Offset: 0x000C6C52
		internal override bool RequiresRefreshForDistinct()
		{
			return false;
		}

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x06003F1D RID: 16157 RVA: 0x000C8A55 File Offset: 0x000C6C55
		Type IFilteringDescription.FilteringType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x06003F1E RID: 16158 RVA: 0x000C8A61 File Offset: 0x000C6C61
		bool IFilteringDescription.PrefersDistinct
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x000C8A64 File Offset: 0x000C6C64
		DistinctValuesProvider IDistinctValuesDescription.GetDisctinctValuesProvider()
		{
			return this.GetDisctinctValuesProvider();
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x000C8A6C File Offset: 0x000C6C6C
		IEnumerable<object> IFilterOperatorsProvider.GetAvailableConditions()
		{
			return new List<object>
			{
				Comparison.Equals,
				Comparison.DoesNotEqual,
				TextComparison.Contains,
				TextComparison.DoesNotContain,
				IntervalComparison.IsBetween
			};
		}

		// Token: 0x040010BA RID: 4282
		private OlapCondition condition;
	}
}
