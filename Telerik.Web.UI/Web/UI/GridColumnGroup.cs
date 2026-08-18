using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000B75 RID: 2933
	public class GridColumnGroup : StateManager, IComparable, IDisposable
	{
		// Token: 0x17002446 RID: 9286
		// (get) Token: 0x06006E98 RID: 28312 RVA: 0x0019B124 File Offset: 0x00199324
		// (set) Token: 0x06006E99 RID: 28313 RVA: 0x0019B151 File Offset: 0x00199351
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string HeaderText
		{
			get
			{
				object obj = base.ViewState["ColumnGroupHeaderText"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["ColumnGroupHeaderText"] = value;
			}
		}

		// Token: 0x17002447 RID: 9287
		// (get) Token: 0x06006E9A RID: 28314 RVA: 0x0019B164 File Offset: 0x00199364
		// (set) Token: 0x06006E9B RID: 28315 RVA: 0x0019B191 File Offset: 0x00199391
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string Name
		{
			get
			{
				object obj = base.ViewState["ColumnGroupName"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["ColumnGroupName"] = value;
			}
		}

		// Token: 0x17002448 RID: 9288
		// (get) Token: 0x06006E9C RID: 28316 RVA: 0x0019B1A4 File Offset: 0x001993A4
		// (set) Token: 0x06006E9D RID: 28317 RVA: 0x0019B1D1 File Offset: 0x001993D1
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string ParentGroupName
		{
			get
			{
				object obj = base.ViewState["ColumnParentGroupName"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["ColumnParentGroupName"] = value;
			}
		}

		// Token: 0x17002449 RID: 9289
		// (get) Token: 0x06006E9E RID: 28318 RVA: 0x0019B1E4 File Offset: 0x001993E4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[Category("Style")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual TableItemStyle HeaderStyle
		{
			get
			{
				if (this._headerStyle == null)
				{
					this._headerStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._headerStyle).TrackViewState();
					}
				}
				return this._headerStyle;
			}
		}

		// Token: 0x1700244A RID: 9290
		// (get) Token: 0x06006E9F RID: 28319 RVA: 0x0019B212 File Offset: 0x00199412
		// (set) Token: 0x06006EA0 RID: 28320 RVA: 0x0019B22D File Offset: 0x0019942D
		internal List<GridColumn> Columns
		{
			get
			{
				if (this.gridColumns == null)
				{
					this.gridColumns = new List<GridColumn>();
				}
				return this.gridColumns;
			}
			set
			{
				this.gridColumns = value;
			}
		}

		// Token: 0x1700244B RID: 9291
		// (get) Token: 0x06006EA1 RID: 28321 RVA: 0x0019B236 File Offset: 0x00199436
		// (set) Token: 0x06006EA2 RID: 28322 RVA: 0x0019B23E File Offset: 0x0019943E
		public int ColSpan { get; internal set; }

		// Token: 0x1700244C RID: 9292
		// (get) Token: 0x06006EA3 RID: 28323 RVA: 0x0019B247 File Offset: 0x00199447
		// (set) Token: 0x06006EA4 RID: 28324 RVA: 0x0019B24F File Offset: 0x0019944F
		internal int OrderIndex { get; set; }

		// Token: 0x1700244D RID: 9293
		// (get) Token: 0x06006EA5 RID: 28325 RVA: 0x0019B258 File Offset: 0x00199458
		// (set) Token: 0x06006EA6 RID: 28326 RVA: 0x0019B260 File Offset: 0x00199460
		internal int VisibleColSpan { get; set; }

		// Token: 0x1700244E RID: 9294
		// (get) Token: 0x06006EA7 RID: 28327 RVA: 0x0019B269 File Offset: 0x00199469
		// (set) Token: 0x06006EA8 RID: 28328 RVA: 0x0019B271 File Offset: 0x00199471
		public bool Visible { get; internal set; }

		// Token: 0x1700244F RID: 9295
		// (get) Token: 0x06006EA9 RID: 28329 RVA: 0x0019B27A File Offset: 0x0019947A
		// (set) Token: 0x06006EAA RID: 28330 RVA: 0x0019B282 File Offset: 0x00199482
		internal bool Display { get; set; }

		// Token: 0x17002450 RID: 9296
		// (get) Token: 0x06006EAB RID: 28331 RVA: 0x0019B28B File Offset: 0x0019948B
		// (set) Token: 0x06006EAC RID: 28332 RVA: 0x0019B2A6 File Offset: 0x001994A6
		internal List<GridColumnGroup> ChildGroups
		{
			get
			{
				if (this.childGroups == null)
				{
					this.childGroups = new List<GridColumnGroup>();
				}
				return this.childGroups;
			}
			set
			{
				this.childGroups = value;
			}
		}

		// Token: 0x06006EAD RID: 28333 RVA: 0x0019B2AF File Offset: 0x001994AF
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._headerStyle != null)
			{
				((IStateManager)this._headerStyle).TrackViewState();
			}
		}

		// Token: 0x06006EAE RID: 28334 RVA: 0x0019B2CC File Offset: 0x001994CC
		protected override object SaveViewState()
		{
			object obj = (this._headerStyle != null) ? ((IStateManager)this._headerStyle).SaveViewState() : null;
			return new object[]
			{
				base.SaveViewState(),
				obj
			};
		}

		// Token: 0x06006EAF RID: 28335 RVA: 0x0019B308 File Offset: 0x00199508
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				base.LoadViewState(array[0]);
				((IStateManager)this.HeaderStyle).LoadViewState(array[1]);
				return;
			}
			base.LoadViewState(state);
		}

		// Token: 0x06006EB0 RID: 28336 RVA: 0x0019B340 File Offset: 0x00199540
		public int CompareTo(object obj)
		{
			GridColumnGroup gridColumnGroup = obj as GridColumnGroup;
			if (gridColumnGroup != null)
			{
				return this.OrderIndex.CompareTo(gridColumnGroup.OrderIndex);
			}
			GridColumn gridColumn = obj as GridColumn;
			if (gridColumn != null)
			{
				return this.OrderIndex.CompareTo(gridColumn.OrderIndex);
			}
			return 1;
		}

		// Token: 0x06006EB1 RID: 28337 RVA: 0x0019B38C File Offset: 0x0019958C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06006EB2 RID: 28338 RVA: 0x0019B39B File Offset: 0x0019959B
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this._headerStyle != null)
			{
				this._headerStyle.Dispose();
			}
		}

		// Token: 0x04001DDD RID: 7645
		private TableItemStyle _headerStyle;

		// Token: 0x04001DDE RID: 7646
		private List<GridColumn> gridColumns;

		// Token: 0x04001DDF RID: 7647
		private List<GridColumnGroup> childGroups;
	}
}
