using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Fields;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x020006F4 RID: 1780
	[DataContract]
	public abstract class OlapGroupDescriptionBase : GroupDescription, IInitializeDescription, IValueGroupFilterHost, ILabelGroupFilterHost, IFilteringDescription, IDistinctValuesDescription, IConditionFactory, IFilterOperatorsProvider
	{
		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x06003F4C RID: 16204 RVA: 0x000C9080 File Offset: 0x000C7280
		// (set) Token: 0x06003F4D RID: 16205 RVA: 0x000C9088 File Offset: 0x000C7288
		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Design choice.")]
		[DataMember]
		public string MemberName { get; set; }

		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x06003F4E RID: 16206 RVA: 0x000C9091 File Offset: 0x000C7291
		// (set) Token: 0x06003F4F RID: 16207 RVA: 0x000C9099 File Offset: 0x000C7299
		internal OlapHierarchyFieldInfo FieldInfo { get; set; }

		// Token: 0x06003F50 RID: 16208 RVA: 0x000C90A2 File Offset: 0x000C72A2
		internal override IPivotFieldInfo GetFieldInfo()
		{
			return this.FieldInfo;
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x000C90AC File Offset: 0x000C72AC
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

		// Token: 0x06003F52 RID: 16210 RVA: 0x000C90EC File Offset: 0x000C72EC
		protected internal override IEnumerable<object> GetAllNames(IEnumerable<object> uniqueNames, IEnumerable<object> parentGroupNames)
		{
			return uniqueNames;
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x000C90F0 File Offset: 0x000C72F0
		protected override void CloneCore(Cloneable source)
		{
			OlapGroupDescriptionBase olapGroupDescriptionBase = source as OlapGroupDescriptionBase;
			if (olapGroupDescriptionBase != null)
			{
				this.MemberName = olapGroupDescriptionBase.MemberName;
				this.FieldInfo = olapGroupDescriptionBase.FieldInfo;
			}
			base.CloneCore(source);
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x000C9126 File Offset: 0x000C7326
		public override string GetUniqueName()
		{
			return this.MemberName;
		}

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x06003F55 RID: 16213 RVA: 0x000C912E File Offset: 0x000C732E
		bool IInitializeDescription.Initialized
		{
			get
			{
				return this.FieldInfo != null;
			}
		}

		// Token: 0x06003F56 RID: 16214 RVA: 0x000C913C File Offset: 0x000C733C
		void IInitializeDescription.Initialize(IDataProvider provider)
		{
			if (provider == null)
			{
				return;
			}
			base.Provider = provider;
			OlapHierarchyFieldInfo fieldInfo = provider.FieldInfos.GetFieldDescriptionByMember(this.MemberName) as OlapHierarchyFieldInfo;
			this.InitializeFromFieldInfo(fieldInfo);
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x000C9172 File Offset: 0x000C7372
		internal virtual void InitializeFromFieldInfo(OlapHierarchyFieldInfo fieldInfo)
		{
			if (fieldInfo == null)
			{
				return;
			}
			this.FieldInfo = fieldInfo;
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x000C917F File Offset: 0x000C737F
		internal override bool RequiresRefreshForDistinct()
		{
			return false;
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x000C9182 File Offset: 0x000C7382
		IValueGroupFilter IValueGroupFilterHost.CreateFilter()
		{
			return new OlapValueGroupFilter();
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x000C9189 File Offset: 0x000C7389
		ILabelGroupFilter ILabelGroupFilterHost.CreateFilter()
		{
			return new OlapLabelGroupFilter();
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x000C9190 File Offset: 0x000C7390
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

		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x06003F5C RID: 16220 RVA: 0x000C91E0 File Offset: 0x000C73E0
		Type IFilteringDescription.FilteringType
		{
			get
			{
				return typeof(object);
			}
		}

		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x06003F5D RID: 16221 RVA: 0x000C91EC File Offset: 0x000C73EC
		bool IFilteringDescription.PrefersDistinct
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003F5E RID: 16222 RVA: 0x000C91EF File Offset: 0x000C73EF
		Condition IConditionFactory.CreateCondition(Type conditionType)
		{
			return DescriptionBase.CreateOlapCondition(conditionType);
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x000C91F7 File Offset: 0x000C73F7
		internal virtual DistinctValuesProvider GetDisctinctValuesProvider()
		{
			return null;
		}

		// Token: 0x06003F60 RID: 16224 RVA: 0x000C91FA File Offset: 0x000C73FA
		DistinctValuesProvider IDistinctValuesDescription.GetDisctinctValuesProvider()
		{
			return this.GetDisctinctValuesProvider();
		}
	}
}
