using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Internal;
using Telerik.Web.UI.PivotGrid.Core.Olap;
using Telerik.Web.UI.PivotGrid.Core.ReportFilter;
using Telerik.Web.UI.PivotGrid.DataProviders.Adomd;
using Telerik.Web.UI.PivotGrid.DataProviders.Queryable;
using Telerik.Web.UI.PivotGrid.Xmla;

namespace Telerik.Web.UI
{
	// Token: 0x02000DA1 RID: 3489
	public abstract class PivotGridGroupField : PivotGridField
	{
		// Token: 0x06008241 RID: 33345 RVA: 0x001DB152 File Offset: 0x001D9352
		public PivotGridGroupField()
		{
		}

		// Token: 0x06008242 RID: 33346 RVA: 0x001DB15A File Offset: 0x001D935A
		public PivotGridGroupField(GroupDescription grDescription)
		{
			this.groupDescriptor = grDescription;
		}

		// Token: 0x17002933 RID: 10547
		// (get) Token: 0x06008243 RID: 33347 RVA: 0x001DB16C File Offset: 0x001D936C
		// (set) Token: 0x06008244 RID: 33348 RVA: 0x001DB19A File Offset: 0x001D939A
		[DefaultValue(PivotGridGroupInterval.Default)]
		[NotifyParentProperty(true)]
		public PivotGridGroupInterval GroupInterval
		{
			get
			{
				object obj = base.ViewState["GroupInterval"];
				if (obj == null)
				{
					obj = PivotGridGroupInterval.Default;
				}
				return (PivotGridGroupInterval)obj;
			}
			set
			{
				base.ViewState["GroupInterval"] = value;
			}
		}

		// Token: 0x17002934 RID: 10548
		// (get) Token: 0x06008245 RID: 33349 RVA: 0x001DB1B4 File Offset: 0x001D93B4
		// (set) Token: 0x06008246 RID: 33350 RVA: 0x001DB1EA File Offset: 0x001D93EA
		[DefaultValue(10.0)]
		[NotifyParentProperty(true)]
		public double GroupIntervalNumericRange
		{
			get
			{
				object obj = base.ViewState["GroupIntervalNumericRange"];
				if (obj == null)
				{
					obj = 10.0;
				}
				return (double)obj;
			}
			set
			{
				base.ViewState["GroupIntervalNumericRange"] = value;
			}
		}

		// Token: 0x17002935 RID: 10549
		// (get) Token: 0x06008247 RID: 33351 RVA: 0x001DB204 File Offset: 0x001D9404
		// (set) Token: 0x06008248 RID: 33352 RVA: 0x001DB22D File Offset: 0x001D942D
		[Description("Determines whether the empty groups will be displayed")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool ShowGroupsWhenNoData
		{
			get
			{
				object obj = base.ViewState["ShowGroupsWhenNoData"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["ShowGroupsWhenNoData"] = value;
			}
		}

		// Token: 0x17002936 RID: 10550
		// (get) Token: 0x06008249 RID: 33353 RVA: 0x001DB245 File Offset: 0x001D9445
		[DefaultValue(null)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("RadPivotGridCalculatedItems Collection")]
		[NotifyParentProperty(true)]
		public virtual PivotGridCalculatedItemsCollection CalculatedItems
		{
			get
			{
				if (this.calculatedItems == null)
				{
					this.calculatedItems = new PivotGridCalculatedItemsCollection();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.calculatedItems).TrackViewState();
					}
				}
				return this.calculatedItems;
			}
		}

		// Token: 0x17002937 RID: 10551
		// (get) Token: 0x0600824A RID: 33354 RVA: 0x001DB274 File Offset: 0x001D9474
		// (set) Token: 0x0600824B RID: 33355 RVA: 0x001DB471 File Offset: 0x001D9671
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual GroupDescription GroupDescription
		{
			get
			{
				if (this.groupDescriptor == null && base.Owner != null)
				{
					if (base.Owner.IsBoundToXmla)
					{
						this.groupDescriptor = new XmlaGroupDescription();
						this.groupDescriptor.GroupComparer = new GroupNameComparer();
						PivotGridPrepareDescriptionForFieldEventArgs pivotGridPrepareDescriptionForFieldEventArgs = new PivotGridPrepareDescriptionForFieldEventArgs(this, this.groupDescriptor, DataProviderDescriptionType.Group);
						base.Owner.CallPrepareDescriptionForField(pivotGridPrepareDescriptionForFieldEventArgs);
						this.groupDescriptor = (pivotGridPrepareDescriptionForFieldEventArgs.Description as GroupDescription);
					}
					else if (base.Owner.IsBoundToAdomd)
					{
						this.groupDescriptor = new AdomdGroupDescription();
						this.groupDescriptor.GroupComparer = new GroupNameComparer();
						PivotGridPrepareDescriptionForFieldEventArgs pivotGridPrepareDescriptionForFieldEventArgs2 = new PivotGridPrepareDescriptionForFieldEventArgs(this, this.groupDescriptor, DataProviderDescriptionType.Group);
						base.Owner.CallPrepareDescriptionForField(pivotGridPrepareDescriptionForFieldEventArgs2);
						this.groupDescriptor = (pivotGridPrepareDescriptionForFieldEventArgs2.Description as GroupDescription);
					}
					else if (base.Owner.IsBoundToIQueryableCollection && this.GroupInterval == PivotGridGroupInterval.Default)
					{
						this.groupDescriptor = new QueryablePropertyGroupDescription();
						this.groupDescriptor.GroupComparer = new GroupNameComparer();
					}
					else
					{
						if (this.GroupInterval != PivotGridGroupInterval.Default && this.GroupInterval != PivotGridGroupInterval.Numeric)
						{
							this.groupDescriptor = new DateTimeGroupDescription();
							switch (this.GroupInterval)
							{
							case PivotGridGroupInterval.Year:
								((DateTimeGroupDescription)this.groupDescriptor).Step = DateTimeStep.Year;
								break;
							case PivotGridGroupInterval.Quarter:
								((DateTimeGroupDescription)this.groupDescriptor).Step = DateTimeStep.Quarter;
								break;
							case PivotGridGroupInterval.Month:
								((DateTimeGroupDescription)this.groupDescriptor).Step = DateTimeStep.Month;
								break;
							case PivotGridGroupInterval.Day:
								((DateTimeGroupDescription)this.groupDescriptor).Step = DateTimeStep.Day;
								break;
							}
						}
						else if (this.GroupInterval == PivotGridGroupInterval.Numeric)
						{
							this.groupDescriptor = new DoubleGroupDescription();
							((DoubleGroupDescription)this.groupDescriptor).Step = this.GroupIntervalNumericRange;
						}
						else
						{
							this.groupDescriptor = new PropertyGroupDescription();
						}
						this.groupDescriptor.GroupComparer = new GroupNameComparer();
					}
				}
				if (this.groupDescriptor != null)
				{
					this.groupDescriptor.ShowGroupsWithNoData = this.ShowGroupsWhenNoData;
				}
				return this.groupDescriptor;
			}
			set
			{
				this.groupDescriptor = value;
			}
		}

		// Token: 0x0600824C RID: 33356 RVA: 0x001DB47C File Offset: 0x001D967C
		protected override void OnDescriptionInfoChanged()
		{
			OlapGroupDescription olapGroupDescription = this.GroupDescription as OlapGroupDescription;
			if (olapGroupDescription != null)
			{
				olapGroupDescription.MemberName = base.DataField;
				olapGroupDescription.SortOrder = PivotSerializationHelper.GridSortOrderToCoreSortOrder(base.SortOrder);
				return;
			}
			PropertyGroupDescriptionBase propertyGroupDescriptionBase = this.GroupDescription as PropertyGroupDescriptionBase;
			if (propertyGroupDescriptionBase != null)
			{
				propertyGroupDescriptionBase.PropertyName = base.DataField;
				propertyGroupDescriptionBase.SortOrder = PivotSerializationHelper.GridSortOrderToCoreSortOrder(base.SortOrder);
				return;
			}
			QueryablePropertyGroupDescription queryablePropertyGroupDescription = this.GroupDescription as QueryablePropertyGroupDescription;
			if (queryablePropertyGroupDescription != null)
			{
				queryablePropertyGroupDescription.PropertyName = base.DataField;
				queryablePropertyGroupDescription.SortOrder = PivotSerializationHelper.GridSortOrderToCoreSortOrder(base.SortOrder);
			}
		}

		// Token: 0x0600824D RID: 33357
		public abstract IEnumerable<object> GetUniqueKeys(int level);

		// Token: 0x0600824E RID: 33358 RVA: 0x001DB510 File Offset: 0x001D9710
		internal override void CopyBaseProperties(PivotGridField field)
		{
			PivotGridGroupField pivotGridGroupField = field as PivotGridGroupField;
			if (pivotGridGroupField != null)
			{
				this.GroupDescription = pivotGridGroupField.GroupDescription;
				if (pivotGridGroupField.GroupInterval != this.GroupInterval)
				{
					this.GroupInterval = pivotGridGroupField.GroupInterval;
				}
				if (pivotGridGroupField.GroupIntervalNumericRange != this.GroupIntervalNumericRange)
				{
					this.GroupIntervalNumericRange = pivotGridGroupField.GroupIntervalNumericRange;
				}
				if (pivotGridGroupField.ShowGroupsWhenNoData != this.ShowGroupsWhenNoData)
				{
					this.ShowGroupsWhenNoData = pivotGridGroupField.ShowGroupsWhenNoData;
				}
			}
			base.CopyBaseProperties(field);
		}

		// Token: 0x0600824F RID: 33359 RVA: 0x001DB588 File Offset: 0x001D9788
		public virtual IEnumerable<object> GetUniqueFilterItems(int index = -1)
		{
			if (base.Owner != null && base.Owner.PivotModel.DataProvider != null)
			{
				DistinctValuesProvider distinctValuesProvider;
				if (this.GroupDescription is IDistinctValuesDescription)
				{
					if (index < 0)
					{
						distinctValuesProvider = ((IDistinctValuesDescription)this.GroupDescription).GetDisctinctValuesProvider();
					}
					else
					{
						XmlaGroupDescription xmlaGroupDescription = ((IDistinctValuesDescription)this.GroupDescription) as XmlaGroupDescription;
						if (xmlaGroupDescription != null)
						{
							distinctValuesProvider = ((IDistinctValuesDescription)xmlaGroupDescription.Levels[index]).GetDisctinctValuesProvider();
						}
						else
						{
							AdomdGroupDescription adomdGroupDescription = ((IDistinctValuesDescription)this.GroupDescription) as AdomdGroupDescription;
							if (adomdGroupDescription != null)
							{
								distinctValuesProvider = ((IDistinctValuesDescription)adomdGroupDescription.Levels[index]).GetDisctinctValuesProvider();
							}
							else
							{
								distinctValuesProvider = null;
							}
						}
					}
				}
				else
				{
					distinctValuesProvider = new LocalDistincsGroupKeysProvider(base.Owner.PivotModel.DataProvider, this.GroupDescription);
				}
				if (distinctValuesProvider != null)
				{
					EventCompletionSource<EventArgs> eventCompletionSource = new EventCompletionSource<EventArgs>(distinctValuesProvider, "Updated");
					distinctValuesProvider.Refresh();
					eventCompletionSource.AwaitEvent();
					eventCompletionSource.Dispose();
					return distinctValuesProvider.DisctinctValues;
				}
			}
			return null;
		}

		// Token: 0x17002938 RID: 10552
		// (get) Token: 0x06008250 RID: 33360 RVA: 0x001DB674 File Offset: 0x001D9874
		internal int ColumnSpan
		{
			get
			{
				OlapGroupDescription olapGroupDescription = this.GroupDescription as OlapGroupDescription;
				if (olapGroupDescription != null && olapGroupDescription.Levels.Count > 1)
				{
					return olapGroupDescription.Levels.Count;
				}
				return 1;
			}
		}

		// Token: 0x040023E2 RID: 9186
		private GroupDescription groupDescriptor;

		// Token: 0x040023E3 RID: 9187
		private PivotGridCalculatedItemsCollection calculatedItems;
	}
}
