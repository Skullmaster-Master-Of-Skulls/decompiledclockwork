using System;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;
using Telerik.Web.UI.PivotGrid.Core.Olap;
using Telerik.Web.UI.PivotGrid.Core.Totals;
using Telerik.Web.UI.PivotGrid.DataProviders.Adomd;
using Telerik.Web.UI.PivotGrid.DataProviders.Queryable;
using Telerik.Web.UI.PivotGrid.Queryable;
using Telerik.Web.UI.PivotGrid.Xmla;

namespace Telerik.Web.UI
{
	// Token: 0x02000D9F RID: 3487
	public class PivotGridAggregateField : PivotGridField
	{
		// Token: 0x06008214 RID: 33300 RVA: 0x001DA93D File Offset: 0x001D8B3D
		public PivotGridAggregateField()
		{
		}

		// Token: 0x06008215 RID: 33301 RVA: 0x001DA945 File Offset: 0x001D8B45
		public PivotGridAggregateField(AggregateDescriptionBase grDescription)
		{
			this.groupDescriptor = grDescription;
		}

		// Token: 0x17002920 RID: 10528
		// (get) Token: 0x06008216 RID: 33302 RVA: 0x001DA954 File Offset: 0x001D8B54
		// (set) Token: 0x06008217 RID: 33303 RVA: 0x001DA95C File Offset: 0x001D8B5C
		[Category("Layout")]
		[TemplateContainer(typeof(PivotGridCell), BindingDirection.OneWay)]
		[DefaultValue(null)]
		[Description("RadPivotGrid Aggregate Cell Template")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public virtual ITemplate CellTemplate
		{
			get
			{
				return this.cellTemplate;
			}
			set
			{
				this.cellTemplate = value;
			}
		}

		// Token: 0x17002921 RID: 10529
		// (get) Token: 0x06008218 RID: 33304 RVA: 0x001DA965 File Offset: 0x001D8B65
		// (set) Token: 0x06008219 RID: 33305 RVA: 0x001DA96D File Offset: 0x001D8B6D
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[Description("RadPivotGrid Header Cell (Aggregate Field) Template")]
		[Category("Layout")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(PivotGridCell), BindingDirection.OneWay)]
		public virtual ITemplate HeaderCellTemplate
		{
			get
			{
				return this.headerCellTemplate;
			}
			set
			{
				this.headerCellTemplate = value;
			}
		}

		// Token: 0x17002922 RID: 10530
		// (get) Token: 0x0600821A RID: 33306 RVA: 0x001DA976 File Offset: 0x001D8B76
		// (set) Token: 0x0600821B RID: 33307 RVA: 0x001DA97E File Offset: 0x001D8B7E
		[Category("Layout")]
		[DefaultValue(null)]
		[Description("RadPivotGrid Total Cell Template (Row Field) Template")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(PivotGridCell), BindingDirection.OneWay)]
		[NotifyParentProperty(true)]
		public virtual ITemplate RowTotalCellTemplate
		{
			get
			{
				return this.rowTotalCellTemplate;
			}
			set
			{
				this.rowTotalCellTemplate = value;
			}
		}

		// Token: 0x17002923 RID: 10531
		// (get) Token: 0x0600821C RID: 33308 RVA: 0x001DA987 File Offset: 0x001D8B87
		// (set) Token: 0x0600821D RID: 33309 RVA: 0x001DA98F File Offset: 0x001D8B8F
		[DefaultValue(null)]
		[Description("RadPivotGrid Total Cell Template (Column Field) Template")]
		[Category("Layout")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(PivotGridCell), BindingDirection.OneWay)]
		[NotifyParentProperty(true)]
		public virtual ITemplate ColumnTotalCellTemplate
		{
			get
			{
				return this.columnTotalCellTemplate;
			}
			set
			{
				this.columnTotalCellTemplate = value;
			}
		}

		// Token: 0x17002924 RID: 10532
		// (get) Token: 0x0600821E RID: 33310 RVA: 0x001DA998 File Offset: 0x001D8B98
		// (set) Token: 0x0600821F RID: 33311 RVA: 0x001DA9A0 File Offset: 0x001D8BA0
		[Category("Layout")]
		[DefaultValue(null)]
		[Description("RadPivotGrid Total Cell Template (Row And Column) Template")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(PivotGridCell), BindingDirection.OneWay)]
		[NotifyParentProperty(true)]
		public virtual ITemplate RowAndColumnTotalCellTemplate
		{
			get
			{
				return this.rowAndColumnTotalCellTemplate;
			}
			set
			{
				this.rowAndColumnTotalCellTemplate = value;
			}
		}

		// Token: 0x17002925 RID: 10533
		// (get) Token: 0x06008220 RID: 33312 RVA: 0x001DA9A9 File Offset: 0x001D8BA9
		// (set) Token: 0x06008221 RID: 33313 RVA: 0x001DA9B1 File Offset: 0x001D8BB1
		[Category("Layout")]
		[DefaultValue(null)]
		[Description("RadPivotGrid Grand Total Cell Template (Row Field) Template")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(PivotGridCell), BindingDirection.OneWay)]
		public virtual ITemplate RowGrandTotalCellTemplate
		{
			get
			{
				return this.rowGrandTotalCellTemplate;
			}
			set
			{
				this.rowGrandTotalCellTemplate = value;
			}
		}

		// Token: 0x17002926 RID: 10534
		// (get) Token: 0x06008222 RID: 33314 RVA: 0x001DA9BA File Offset: 0x001D8BBA
		// (set) Token: 0x06008223 RID: 33315 RVA: 0x001DA9C2 File Offset: 0x001D8BC2
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[Description("RadPivotGrid Grand Total Cell Template (Column Field) Template")]
		[Category("Layout")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(PivotGridCell), BindingDirection.OneWay)]
		public virtual ITemplate ColumnGrandTotalCellTemplate
		{
			get
			{
				return this.columnGrandTotalCellTemplate;
			}
			set
			{
				this.columnGrandTotalCellTemplate = value;
			}
		}

		// Token: 0x17002927 RID: 10535
		// (get) Token: 0x06008224 RID: 33316 RVA: 0x001DA9CB File Offset: 0x001D8BCB
		// (set) Token: 0x06008225 RID: 33317 RVA: 0x001DA9D3 File Offset: 0x001D8BD3
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[Description("RadPivotGrid Grand Total Cell Template (Row And Column) Template")]
		[Category("Layout")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(PivotGridCell), BindingDirection.OneWay)]
		public virtual ITemplate RowAndColumnGrandTotalCellTemplate
		{
			get
			{
				return this.rowAndColumnGrandTotalCellTemplate;
			}
			set
			{
				this.rowAndColumnGrandTotalCellTemplate = value;
			}
		}

		// Token: 0x17002928 RID: 10536
		// (get) Token: 0x06008226 RID: 33318 RVA: 0x001DA9DC File Offset: 0x001D8BDC
		// (set) Token: 0x06008227 RID: 33319 RVA: 0x001DA9E4 File Offset: 0x001D8BE4
		[TemplateContainer(typeof(PivotGridCell), BindingDirection.OneWay)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[Description("RadPivotGrid Grand Total Header Cell Template (Row Field) Template")]
		[Category("Layout")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual ITemplate RowGrandTotalHeaderCellTemplate
		{
			get
			{
				return this.rowGrandTotalHeaderCellTemplate;
			}
			set
			{
				this.rowGrandTotalHeaderCellTemplate = value;
			}
		}

		// Token: 0x17002929 RID: 10537
		// (get) Token: 0x06008228 RID: 33320 RVA: 0x001DA9ED File Offset: 0x001D8BED
		// (set) Token: 0x06008229 RID: 33321 RVA: 0x001DA9F5 File Offset: 0x001D8BF5
		[Category("Layout")]
		[TemplateContainer(typeof(PivotGridCell), BindingDirection.OneWay)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("RadPivotGrid Grand Total Header Cell Template (Column Field) Template")]
		[DefaultValue(null)]
		public virtual ITemplate ColumnGrandTotalHeaderCellTemplate
		{
			get
			{
				return this.columnGrandTotalHeaderCellTemplate;
			}
			set
			{
				this.columnGrandTotalHeaderCellTemplate = value;
			}
		}

		// Token: 0x1700292A RID: 10538
		// (get) Token: 0x0600822A RID: 33322 RVA: 0x001DAA00 File Offset: 0x001D8C00
		// (set) Token: 0x0600822B RID: 33323 RVA: 0x001DAA52 File Offset: 0x001D8C52
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public PivotGridTotalFormat TotalFormat
		{
			get
			{
				if (base.ViewState["TotalFormat"] == null)
				{
					base.ViewState["TotalFormat"] = new PivotGridTotalFormat
					{
						TotalFunction = PivotGridTotalFunction.NoCalculation
					};
				}
				return base.ViewState["TotalFormat"] as PivotGridTotalFormat;
			}
			set
			{
				base.ViewState["TotalFormat"] = value;
			}
		}

		// Token: 0x1700292B RID: 10539
		// (get) Token: 0x0600822C RID: 33324 RVA: 0x001DAA65 File Offset: 0x001D8C65
		public int AggregateIndex
		{
			get
			{
				return this.GetAggregateIndex();
			}
		}

		// Token: 0x1700292C RID: 10540
		// (get) Token: 0x0600822D RID: 33325 RVA: 0x001DAA70 File Offset: 0x001D8C70
		// (set) Token: 0x0600822E RID: 33326 RVA: 0x001DAA9E File Offset: 0x001D8C9E
		[NotifyParentProperty(true)]
		[DefaultValue(PivotGridAggregate.Sum)]
		public PivotGridAggregate Aggregate
		{
			get
			{
				object obj = base.ViewState["Aggregate"];
				if (obj == null)
				{
					obj = PivotGridAggregate.Sum;
				}
				return (PivotGridAggregate)obj;
			}
			set
			{
				base.ViewState["Aggregate"] = value;
				this.OnDescriptionInfoChanged();
			}
		}

		// Token: 0x1700292D RID: 10541
		// (get) Token: 0x0600822F RID: 33327 RVA: 0x001DAABC File Offset: 0x001D8CBC
		// (set) Token: 0x06008230 RID: 33328 RVA: 0x001DAAE9 File Offset: 0x001D8CE9
		[NotifyParentProperty(true)]
		public virtual string GrandTotalAggregateFormatString
		{
			get
			{
				object obj = base.ViewState["GrandTotalAggregateFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["GrandTotalAggregateFormatString"] = value;
			}
		}

		// Token: 0x1700292E RID: 10542
		// (get) Token: 0x06008231 RID: 33329 RVA: 0x001DAAFC File Offset: 0x001D8CFC
		// (set) Token: 0x06008232 RID: 33330 RVA: 0x001DAB2A File Offset: 0x001D8D2A
		[DefaultValue(null)]
		[TypeConverter(typeof(GridStringArrayConverter))]
		[NotifyParentProperty(true)]
		public virtual string[] CalculationDataFields
		{
			get
			{
				object obj = base.ViewState["CalculationDataFields"];
				if (obj == null)
				{
					obj = new string[0];
				}
				return (string[])obj;
			}
			set
			{
				base.ViewState["CalculationDataFields"] = value;
				this.OnDescriptionInfoChanged();
			}
		}

		// Token: 0x1700292F RID: 10543
		// (get) Token: 0x06008233 RID: 33331 RVA: 0x001DAB44 File Offset: 0x001D8D44
		// (set) Token: 0x06008234 RID: 33332 RVA: 0x001DAB72 File Offset: 0x001D8D72
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual AggregateFunction[] CalculationAggregates
		{
			get
			{
				object obj = base.ViewState["CalculationAggregates"];
				if (obj == null)
				{
					obj = new AggregateFunction[0];
				}
				return (AggregateFunction[])obj;
			}
			set
			{
				base.ViewState["CalculationAggregates"] = value;
				this.OnDescriptionInfoChanged();
			}
		}

		// Token: 0x17002930 RID: 10544
		// (get) Token: 0x06008235 RID: 33333 RVA: 0x001DAB8C File Offset: 0x001D8D8C
		// (set) Token: 0x06008236 RID: 33334 RVA: 0x001DABB9 File Offset: 0x001D8DB9
		public virtual string CalculationExpression
		{
			get
			{
				object obj = base.ViewState["CalculationExpression"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["CalculationExpression"] = value;
			}
		}

		// Token: 0x17002931 RID: 10545
		// (get) Token: 0x06008237 RID: 33335 RVA: 0x001DABCC File Offset: 0x001D8DCC
		// (set) Token: 0x06008238 RID: 33336 RVA: 0x001DABF5 File Offset: 0x001D8DF5
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public virtual bool IgnoreNullValues
		{
			get
			{
				object obj = base.ViewState["IgnoreNullValues"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["IgnoreNullValues"] = value;
			}
		}

		// Token: 0x06008239 RID: 33337 RVA: 0x001DAC2C File Offset: 0x001D8E2C
		private int GetAggregateIndex()
		{
			int result = this.ZoneIndex;
			if (base.Owner != null)
			{
				PivotGridField[] array = (from f in base.Owner.Fields
				where f is PivotGridAggregateField && !f.IsHidden
				orderby f.ZoneIndex
				select f).ToArray<PivotGridField>();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == this)
					{
						result = i;
					}
				}
			}
			return result;
		}

		// Token: 0x17002932 RID: 10546
		// (get) Token: 0x0600823A RID: 33338 RVA: 0x001DACB4 File Offset: 0x001D8EB4
		// (set) Token: 0x0600823B RID: 33339 RVA: 0x001DADA1 File Offset: 0x001D8FA1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public AggregateDescriptionBase GroupDescription
		{
			get
			{
				if (this.groupDescriptor == null && base.Owner != null)
				{
					if (base.Owner.IsBoundToXmla)
					{
						this.groupDescriptor = new XmlaAggregateDescription();
					}
					else if (base.Owner.IsBoundToAdomd)
					{
						this.groupDescriptor = new AdomdAggregateDescription();
					}
					else if (base.Owner.IsBoundToIQueryableCollection)
					{
						this.groupDescriptor = new QueryablePropertyAggregateDescription();
						((QueryablePropertyAggregateDescription)this.groupDescriptor).IgnoreNullValues = this.IgnoreNullValues;
					}
					else if (this.CalculationDataFields.Length > 0)
					{
						this.groupDescriptor = new CalculatedAggregateDescription();
					}
					else
					{
						this.groupDescriptor = new PropertyAggregateDescription();
						((PropertyAggregateDescription)this.groupDescriptor).IgnoreNullValues = this.IgnoreNullValues;
					}
					if (this.TotalFormat.TotalFunction != PivotGridTotalFunction.NoCalculation)
					{
						this.groupDescriptor.TotalFormat = this.MapToCoreTotalFormat(this.TotalFormat);
					}
				}
				return this.groupDescriptor;
			}
			set
			{
				this.groupDescriptor = value;
			}
		}

		// Token: 0x0600823C RID: 33340 RVA: 0x001DADAC File Offset: 0x001D8FAC
		public TotalFormat MapToCoreTotalFormat(PivotGridTotalFormat totalFormat)
		{
			TotalFormat totalFormat2 = null;
			switch (totalFormat.TotalFunction)
			{
			case PivotGridTotalFunction.DifferenceFrom:
				totalFormat2 = new DifferenceFrom
				{
					GroupName = totalFormat.GroupName
				};
				break;
			case PivotGridTotalFunction.DifferenceFromPrevious:
				totalFormat2 = new DifferenceFromPrevious();
				break;
			case PivotGridTotalFunction.DifferenceFromNext:
				totalFormat2 = new DifferenceFromNext();
				break;
			case PivotGridTotalFunction.PercentDifferenceFrom:
				totalFormat2 = new PercentDifferenceFrom
				{
					GroupName = totalFormat.GroupName
				};
				break;
			case PivotGridTotalFunction.PercentDifferenceFromPrevious:
				totalFormat2 = new PercentDifferenceFromPrevious();
				break;
			case PivotGridTotalFunction.PercentDifferenceFromNext:
				totalFormat2 = new PercentDifferenceFromNext();
				break;
			case PivotGridTotalFunction.Index:
				totalFormat2 = new Index();
				break;
			case PivotGridTotalFunction.PercentOf:
				totalFormat2 = new PercentOf
				{
					GroupName = totalFormat.GroupName
				};
				break;
			case PivotGridTotalFunction.PercentOfPrevious:
				totalFormat2 = new PercentOfPrevious();
				break;
			case PivotGridTotalFunction.PercentOfNext:
				totalFormat2 = new PercentOfNext();
				break;
			case PivotGridTotalFunction.PercentOfGrandTotal:
				totalFormat2 = new PercentOfGrandTotal();
				break;
			case PivotGridTotalFunction.PercentOfColumnTotal:
				totalFormat2 = new PercentOfColumnTotal
				{
					Level = totalFormat.Level
				};
				break;
			case PivotGridTotalFunction.PercentOfRowTotal:
				totalFormat2 = new PercentOfRowTotal
				{
					Level = totalFormat.Level
				};
				break;
			case PivotGridTotalFunction.PercentRunningTotalsIn:
				totalFormat2 = new PercentRunningTotalsIn();
				break;
			case PivotGridTotalFunction.RunningTotalsIn:
				totalFormat2 = new RunningTotalsIn();
				break;
			case PivotGridTotalFunction.RankTotals:
				totalFormat2 = new RankTotals
				{
					SortOrder = PivotSerializationHelper.GridSortOrderToCoreSortOrder(totalFormat.SortOrder)
				};
				break;
			}
			SiblingTotalsFormat siblingTotalsFormat = totalFormat2 as SiblingTotalsFormat;
			if (siblingTotalsFormat != null)
			{
				siblingTotalsFormat.Axis = ((totalFormat.Axis == PivotGridAxis.Columns) ? PivotAxis.Columns : PivotAxis.Rows);
				siblingTotalsFormat.Level = totalFormat.Level;
			}
			return totalFormat2;
		}

		// Token: 0x0600823D RID: 33341 RVA: 0x001DAF34 File Offset: 0x001D9134
		protected override void OnDescriptionInfoChanged()
		{
			if (this.GroupDescription != null && this.TotalFormat.TotalFunction != PivotGridTotalFunction.NoCalculation)
			{
				this.GroupDescription.TotalFormat = this.MapToCoreTotalFormat(this.TotalFormat);
			}
			if (this.GroupDescription is OlapAggregateDescription)
			{
				((OlapAggregateDescription)this.GroupDescription).MemberName = base.DataField;
				return;
			}
			if (this.GroupDescription is PropertyAggregateDescription)
			{
				((PropertyAggregateDescription)this.GroupDescription).PropertyName = base.DataField;
				((PropertyAggregateDescription)this.GroupDescription).AggregateFunction = (AggregateFunction)new AggregateFunctionConverter().ConvertFrom(this.Aggregate.ToString());
				return;
			}
			if (this.GroupDescription is CalculatedAggregateDescription)
			{
				((CalculatedAggregateDescription)this.GroupDescription).CalculatedFieldName = base.DataField;
				return;
			}
			if (!(this.GroupDescription is QueryablePropertyAggregateDescription))
			{
				return;
			}
			((QueryablePropertyAggregateDescription)this.GroupDescription).PropertyName = base.DataField;
			AggregateFunction aggregateFunction = (AggregateFunction)new AggregateFunctionConverter().ConvertFrom(this.Aggregate.ToString());
			if (aggregateFunction.Equals(QueryableAggregateFunction.Sum.ToString()) || aggregateFunction.Equals(QueryableAggregateFunction.Min.ToString()) || aggregateFunction.Equals(QueryableAggregateFunction.Max.ToString()) || aggregateFunction.Equals(QueryableAggregateFunction.Count.ToString()) || aggregateFunction.Equals(QueryableAggregateFunction.Average.ToString()))
			{
				((QueryablePropertyAggregateDescription)this.GroupDescription).AggregateFunction = (QueryableAggregateFunction)Enum.Parse(typeof(QueryableAggregateFunction), this.Aggregate.ToString());
				return;
			}
			throw new ArgumentException(string.Format("The {0} aggregate is not supported when QueryableProvider is used", aggregateFunction.ToString()));
		}

		// Token: 0x0600823E RID: 33342 RVA: 0x001DB0F8 File Offset: 0x001D92F8
		internal override void CopyBaseProperties(PivotGridField field)
		{
			base.CopyBaseProperties(field);
			PivotGridAggregateField pivotGridAggregateField = field as PivotGridAggregateField;
			if (pivotGridAggregateField != null)
			{
				this.Aggregate = pivotGridAggregateField.Aggregate;
				this.GrandTotalAggregateFormatString = pivotGridAggregateField.GrandTotalAggregateFormatString;
				this.GroupDescription = pivotGridAggregateField.GroupDescription;
				this.CalculationDataFields = pivotGridAggregateField.CalculationDataFields;
				this.CalculationExpression = pivotGridAggregateField.CalculationExpression;
			}
		}

		// Token: 0x040023D2 RID: 9170
		private AggregateDescriptionBase groupDescriptor;

		// Token: 0x040023D3 RID: 9171
		private ITemplate cellTemplate;

		// Token: 0x040023D4 RID: 9172
		private ITemplate headerCellTemplate;

		// Token: 0x040023D5 RID: 9173
		private ITemplate columnTotalCellTemplate;

		// Token: 0x040023D6 RID: 9174
		private ITemplate rowTotalCellTemplate;

		// Token: 0x040023D7 RID: 9175
		private ITemplate rowAndColumnTotalCellTemplate;

		// Token: 0x040023D8 RID: 9176
		private ITemplate rowGrandTotalCellTemplate;

		// Token: 0x040023D9 RID: 9177
		private ITemplate columnGrandTotalCellTemplate;

		// Token: 0x040023DA RID: 9178
		private ITemplate rowAndColumnGrandTotalCellTemplate;

		// Token: 0x040023DB RID: 9179
		private ITemplate rowGrandTotalHeaderCellTemplate;

		// Token: 0x040023DC RID: 9180
		private ITemplate columnGrandTotalHeaderCellTemplate;
	}
}
