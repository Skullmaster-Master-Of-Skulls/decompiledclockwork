using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Web.DynamicData;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200009C RID: 156
	[DefaultEvent("Selecting")]
	[DefaultProperty("ContextTypeName")]
	[Designer("System.Web.UI.Design.WebControls.LinqDataSourceDesigner, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ResourceDescription("LinqDataSource_Description")]
	[ResourceDisplayName("LinqDataSource_DisplayName")]
	[ToolboxBitmap(typeof(LinqDataSource), "LinqDataSource.bmp")]
	public class LinqDataSource : ContextDataSource, IDynamicDataSource, IDataSource
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x0001C968 File Offset: 0x0001AB68
		public LinqDataSource()
		{
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001C970 File Offset: 0x0001AB70
		internal LinqDataSource(LinqDataSourceView view) : base(view)
		{
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001C979 File Offset: 0x0001AB79
		internal LinqDataSource(IPage page) : base(page)
		{
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001C982 File Offset: 0x0001AB82
		private LinqDataSourceView View
		{
			get
			{
				if (this._view == null)
				{
					this._view = (LinqDataSourceView)this.GetView("DefaultView");
				}
				return this._view;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001C9A8 File Offset: 0x0001ABA8
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0001C9B5 File Offset: 0x0001ABB5
		[DefaultValue(false)]
		[Category("Behavior")]
		[ResourceDescription("LinqDataSource_AutoGenerateOrderByClause")]
		public bool AutoGenerateOrderByClause
		{
			get
			{
				return this.View.AutoGenerateOrderByClause;
			}
			set
			{
				this.View.AutoGenerateOrderByClause = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001C9C3 File Offset: 0x0001ABC3
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0001C9D0 File Offset: 0x0001ABD0
		[DefaultValue(false)]
		[Category("Behavior")]
		[ResourceDescription("LinqDataSource_AutoGenerateWhereClause")]
		public bool AutoGenerateWhereClause
		{
			get
			{
				return this.View.AutoGenerateWhereClause;
			}
			set
			{
				this.View.AutoGenerateWhereClause = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001C9DE File Offset: 0x0001ABDE
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0001C9EB File Offset: 0x0001ABEB
		[DefaultValue(true)]
		[Category("Behavior")]
		[ResourceDescription("LinqDataSource_AutoPage")]
		public bool AutoPage
		{
			get
			{
				return this.View.AutoPage;
			}
			set
			{
				this.View.AutoPage = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001C9F9 File Offset: 0x0001ABF9
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0001CA06 File Offset: 0x0001AC06
		[DefaultValue(true)]
		[Category("Behavior")]
		[ResourceDescription("LinqDataSource_AutoSort")]
		public bool AutoSort
		{
			get
			{
				return this.View.AutoSort;
			}
			set
			{
				this.View.AutoSort = value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001CA14 File Offset: 0x0001AC14
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Data")]
		[ResourceDescription("LinqDataSource_DeleteParameters")]
		[Browsable(false)]
		public ParameterCollection DeleteParameters
		{
			get
			{
				return this.View.DeleteParameters;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001CA21 File Offset: 0x0001AC21
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0001CA2E File Offset: 0x0001AC2E
		[DefaultValue("")]
		[Category("Data")]
		[ResourceDescription("LinqDataSource_ContextTypeName")]
		public override string ContextTypeName
		{
			get
			{
				return this.View.ContextTypeName;
			}
			set
			{
				this.View.ContextTypeName = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001CA3C File Offset: 0x0001AC3C
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x0001CA49 File Offset: 0x0001AC49
		[DefaultValue(false)]
		[Category("Behavior")]
		[ResourceDescription("LinqDataSource_EnableDelete")]
		public bool EnableDelete
		{
			get
			{
				return this.View.EnableDelete;
			}
			set
			{
				this.View.EnableDelete = value;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001CA57 File Offset: 0x0001AC57
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x0001CA64 File Offset: 0x0001AC64
		[DefaultValue(false)]
		[Category("Behavior")]
		[ResourceDescription("LinqDataSource_EnableInsert")]
		public bool EnableInsert
		{
			get
			{
				return this.View.EnableInsert;
			}
			set
			{
				this.View.EnableInsert = value;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001CA72 File Offset: 0x0001AC72
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x0001CA7F File Offset: 0x0001AC7F
		[DefaultValue(true)]
		[Category("Behavior")]
		[ResourceDescription("LinqDataSource_EnableObjectTracking")]
		public bool EnableObjectTracking
		{
			get
			{
				return this.View.EnableObjectTracking;
			}
			set
			{
				this.View.EnableObjectTracking = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001CA8D File Offset: 0x0001AC8D
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x0001CA9A File Offset: 0x0001AC9A
		[DefaultValue(false)]
		[Category("Behavior")]
		[ResourceDescription("LinqDataSource_EnableUpdate")]
		public bool EnableUpdate
		{
			get
			{
				return this.View.EnableUpdate;
			}
			set
			{
				this.View.EnableUpdate = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001CAA8 File Offset: 0x0001ACA8
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x0001CAB5 File Offset: 0x0001ACB5
		[DefaultValue("")]
		[Category("Data")]
		[ResourceDescription("LinqDataSource_GroupBy")]
		public string GroupBy
		{
			get
			{
				return this.View.GroupBy;
			}
			set
			{
				this.View.GroupBy = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001CAC3 File Offset: 0x0001ACC3
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Data")]
		[ResourceDescription("LinqDataSource_GroupByParameters")]
		[Browsable(false)]
		public ParameterCollection GroupByParameters
		{
			get
			{
				return this.View.GroupByParameters;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0001CAD0 File Offset: 0x0001ACD0
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Data")]
		[ResourceDescription("LinqDataSource_InsertParameters")]
		[Browsable(false)]
		public ParameterCollection InsertParameters
		{
			get
			{
				return this.View.InsertParameters;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001CADD File Offset: 0x0001ACDD
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x0001CAEA File Offset: 0x0001ACEA
		[DefaultValue("")]
		[Category("Data")]
		[ResourceDescription("LinqDataSource_OrderBy")]
		public string OrderBy
		{
			get
			{
				return this.View.OrderBy;
			}
			set
			{
				this.View.OrderBy = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Data")]
		[ResourceDescription("LinqDataSource_OrderByParameters")]
		[Browsable(false)]
		public ParameterCollection OrderByParameters
		{
			get
			{
				return this.View.OrderByParameters;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001CB05 File Offset: 0x0001AD05
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x0001CB12 File Offset: 0x0001AD12
		[DefaultValue("")]
		[Category("Data")]
		[ResourceDescription("LinqDataSource_OrderGroupsBy")]
		public string OrderGroupsBy
		{
			get
			{
				return this.View.OrderGroupsBy;
			}
			set
			{
				this.View.OrderGroupsBy = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0001CB20 File Offset: 0x0001AD20
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Data")]
		[ResourceDescription("LinqDataSource_OrderGroupsByParameters")]
		[Browsable(false)]
		public ParameterCollection OrderGroupsByParameters
		{
			get
			{
				return this.View.OrderGroupsByParameters;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001CB2D File Offset: 0x0001AD2D
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x0001CB3A File Offset: 0x0001AD3A
		[DefaultValue("")]
		[Category("Data")]
		[ResourceDescription("LinqDataSource_Select")]
		public string Select
		{
			get
			{
				return this.View.SelectNew;
			}
			set
			{
				this.View.SelectNew = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001CB48 File Offset: 0x0001AD48
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Data")]
		[ResourceDescription("LinqDataSource_SelectParameters")]
		[Browsable(false)]
		public ParameterCollection SelectParameters
		{
			get
			{
				return this.View.SelectNewParameters;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0001CB55 File Offset: 0x0001AD55
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x0001CB62 File Offset: 0x0001AD62
		[DefaultValue(true)]
		[Category("Behavior")]
		[ResourceDescription("LinqDataSource_StoreOriginalValuesInViewState")]
		public bool StoreOriginalValuesInViewState
		{
			get
			{
				return this.View.StoreOriginalValuesInViewState;
			}
			set
			{
				this.View.StoreOriginalValuesInViewState = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001CB70 File Offset: 0x0001AD70
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0001CB7D File Offset: 0x0001AD7D
		[DefaultValue("")]
		[Category("Data")]
		[ResourceDescription("LinqDataSource_TableName")]
		public string TableName
		{
			get
			{
				return this.View.TableName;
			}
			set
			{
				this.View.TableName = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001CB8B File Offset: 0x0001AD8B
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Data")]
		[ResourceDescription("LinqDataSource_UpdateParameters")]
		[Browsable(false)]
		public ParameterCollection UpdateParameters
		{
			get
			{
				return this.View.UpdateParameters;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001CB98 File Offset: 0x0001AD98
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0001CBA5 File Offset: 0x0001ADA5
		[DefaultValue("")]
		[Category("Data")]
		[ResourceDescription("LinqDataSource_Where")]
		public string Where
		{
			get
			{
				return this.View.Where;
			}
			set
			{
				this.View.Where = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001CBB3 File Offset: 0x0001ADB3
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Data")]
		[ResourceDescription("LinqDataSource_WhereParameters")]
		[Browsable(false)]
		public ParameterCollection WhereParameters
		{
			get
			{
				return this.View.WhereParameters;
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060006E7 RID: 1767 RVA: 0x0001CBC0 File Offset: 0x0001ADC0
		// (remove) Token: 0x060006E8 RID: 1768 RVA: 0x0001CBCE File Offset: 0x0001ADCE
		[Category("Data")]
		[ResourceDescription("LinqDataSource_ContextCreated")]
		public event EventHandler<LinqDataSourceStatusEventArgs> ContextCreated
		{
			add
			{
				this.View.ContextCreated += value;
			}
			remove
			{
				this.View.ContextCreated -= value;
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060006E9 RID: 1769 RVA: 0x0001CBDC File Offset: 0x0001ADDC
		// (remove) Token: 0x060006EA RID: 1770 RVA: 0x0001CBEA File Offset: 0x0001ADEA
		[Category("Data")]
		[ResourceDescription("LinqDataSource_ContextCreating")]
		public event EventHandler<LinqDataSourceContextEventArgs> ContextCreating
		{
			add
			{
				this.View.ContextCreating += value;
			}
			remove
			{
				this.View.ContextCreating -= value;
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060006EB RID: 1771 RVA: 0x0001CBF8 File Offset: 0x0001ADF8
		// (remove) Token: 0x060006EC RID: 1772 RVA: 0x0001CC06 File Offset: 0x0001AE06
		[Category("Data")]
		[ResourceDescription("LinqDataSource_ContextDisposing")]
		public event EventHandler<LinqDataSourceDisposeEventArgs> ContextDisposing
		{
			add
			{
				this.View.ContextDisposing += value;
			}
			remove
			{
				this.View.ContextDisposing -= value;
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060006ED RID: 1773 RVA: 0x0001CC14 File Offset: 0x0001AE14
		// (remove) Token: 0x060006EE RID: 1774 RVA: 0x0001CC22 File Offset: 0x0001AE22
		[Category("Data")]
		[ResourceDescription("LinqDataSource_Deleted")]
		public event EventHandler<LinqDataSourceStatusEventArgs> Deleted
		{
			add
			{
				this.View.Deleted += value;
			}
			remove
			{
				this.View.Deleted -= value;
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060006EF RID: 1775 RVA: 0x0001CC30 File Offset: 0x0001AE30
		// (remove) Token: 0x060006F0 RID: 1776 RVA: 0x0001CC3E File Offset: 0x0001AE3E
		[Category("Data")]
		[ResourceDescription("LinqDataSource_Deleting")]
		public event EventHandler<LinqDataSourceDeleteEventArgs> Deleting
		{
			add
			{
				this.View.Deleting += value;
			}
			remove
			{
				this.View.Deleting -= value;
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060006F1 RID: 1777 RVA: 0x0001CC4C File Offset: 0x0001AE4C
		// (remove) Token: 0x060006F2 RID: 1778 RVA: 0x0001CC5A File Offset: 0x0001AE5A
		[Category("Data")]
		[ResourceDescription("LinqDataSource_Inserted")]
		public event EventHandler<LinqDataSourceStatusEventArgs> Inserted
		{
			add
			{
				this.View.Inserted += value;
			}
			remove
			{
				this.View.Inserted -= value;
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060006F3 RID: 1779 RVA: 0x0001CC68 File Offset: 0x0001AE68
		// (remove) Token: 0x060006F4 RID: 1780 RVA: 0x0001CC76 File Offset: 0x0001AE76
		[Category("Data")]
		[ResourceDescription("LinqDataSource_Inserting")]
		public event EventHandler<LinqDataSourceInsertEventArgs> Inserting
		{
			add
			{
				this.View.Inserting += value;
			}
			remove
			{
				this.View.Inserting -= value;
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060006F5 RID: 1781 RVA: 0x0001CC84 File Offset: 0x0001AE84
		// (remove) Token: 0x060006F6 RID: 1782 RVA: 0x0001CC92 File Offset: 0x0001AE92
		[Category("Data")]
		[ResourceDescription("LinqDataSource_Selected")]
		public event EventHandler<LinqDataSourceStatusEventArgs> Selected
		{
			add
			{
				this.View.Selected += value;
			}
			remove
			{
				this.View.Selected -= value;
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060006F7 RID: 1783 RVA: 0x0001CCA0 File Offset: 0x0001AEA0
		// (remove) Token: 0x060006F8 RID: 1784 RVA: 0x0001CCAE File Offset: 0x0001AEAE
		[Category("Data")]
		[ResourceDescription("LinqDataSource_Selecting")]
		public event EventHandler<LinqDataSourceSelectEventArgs> Selecting
		{
			add
			{
				this.View.Selecting += value;
			}
			remove
			{
				this.View.Selecting -= value;
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060006F9 RID: 1785 RVA: 0x0001CCBC File Offset: 0x0001AEBC
		// (remove) Token: 0x060006FA RID: 1786 RVA: 0x0001CCCA File Offset: 0x0001AECA
		[Category("Data")]
		[ResourceDescription("LinqDataSource_Updated")]
		public event EventHandler<LinqDataSourceStatusEventArgs> Updated
		{
			add
			{
				this.View.Updated += value;
			}
			remove
			{
				this.View.Updated -= value;
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060006FB RID: 1787 RVA: 0x0001CCD8 File Offset: 0x0001AED8
		// (remove) Token: 0x060006FC RID: 1788 RVA: 0x0001CCE6 File Offset: 0x0001AEE6
		[Category("Data")]
		[ResourceDescription("LinqDataSource_Updating")]
		public event EventHandler<LinqDataSourceUpdateEventArgs> Updating
		{
			add
			{
				this.View.Updating += value;
			}
			remove
			{
				this.View.Updating -= value;
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001CCF4 File Offset: 0x0001AEF4
		protected virtual LinqDataSourceView CreateView()
		{
			return new LinqDataSourceView(this, "DefaultView", this.Context);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001CD07 File Offset: 0x0001AF07
		protected override QueryableDataSourceView CreateQueryableView()
		{
			return this.CreateView();
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001CD0F File Offset: 0x0001AF0F
		public int Delete(IDictionary keys, IDictionary oldValues)
		{
			return this.View.Delete(keys, oldValues);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001CD1E File Offset: 0x0001AF1E
		public int Insert(IDictionary values)
		{
			return this.View.Insert(values);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001CD2C File Offset: 0x0001AF2C
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.StoreOriginalValuesInViewState && (this.EnableUpdate || this.EnableDelete))
			{
				base.IPage.RegisterRequiresViewStateEncryption();
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001CD58 File Offset: 0x0001AF58
		protected internal override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
			if (this.View != null)
			{
				this.View.ReleaseSelectContexts();
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001CD74 File Offset: 0x0001AF74
		public int Update(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			return this.View.Update(keys, values, oldValues);
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001CD84 File Offset: 0x0001AF84
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0001CDA0 File Offset: 0x0001AFA0
		Type IDynamicDataSource.ContextType
		{
			get
			{
				if (string.IsNullOrEmpty(this.ContextTypeName))
				{
					return null;
				}
				return this.View.ContextType;
			}
			set
			{
				this.View.ContextTypeName = value.AssemblyQualifiedName;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001CDB3 File Offset: 0x0001AFB3
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x0001CDBB File Offset: 0x0001AFBB
		string IDynamicDataSource.EntitySetName
		{
			get
			{
				return this.TableName;
			}
			set
			{
				this.TableName = value;
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000708 RID: 1800 RVA: 0x0001CDC4 File Offset: 0x0001AFC4
		// (remove) Token: 0x06000709 RID: 1801 RVA: 0x0001CDD2 File Offset: 0x0001AFD2
		event EventHandler<DynamicValidatorEventArgs> IDynamicDataSource.Exception
		{
			add
			{
				this.View.Exception += value;
			}
			remove
			{
				this.View.Exception -= value;
			}
		}

		// Token: 0x04000258 RID: 600
		private const string DefaultViewName = "DefaultView";

		// Token: 0x04000259 RID: 601
		private LinqDataSourceView _view;
	}
}
