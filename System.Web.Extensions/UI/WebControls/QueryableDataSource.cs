using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Web.Resources;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000C1 RID: 193
	[ParseChildren(true)]
	[PersistChildren(false)]
	public abstract class QueryableDataSource : DataSourceControl, IQueryableDataSource, IDataSource
	{
		// Token: 0x06000964 RID: 2404 RVA: 0x0002419F File Offset: 0x0002239F
		internal QueryableDataSource(IPage page)
		{
			this._page = page;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x000241AE File Offset: 0x000223AE
		internal QueryableDataSource(QueryableDataSourceView view)
		{
			this._view = view;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x000241BD File Offset: 0x000223BD
		protected QueryableDataSource()
		{
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x000241C5 File Offset: 0x000223C5
		private QueryableDataSourceView View
		{
			get
			{
				if (this._view == null)
				{
					this._view = this.CreateQueryableView();
				}
				return this._view;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x000241E4 File Offset: 0x000223E4
		internal IPage IPage
		{
			get
			{
				if (this._page != null)
				{
					return this._page;
				}
				Page page = this.Page;
				if (page == null)
				{
					throw new InvalidOperationException(AtlasWeb.Common_PageCannotBeNull);
				}
				return new PageWrapper(page);
			}
		}

		// Token: 0x06000969 RID: 2409
		protected abstract QueryableDataSourceView CreateQueryableView();

		// Token: 0x0600096A RID: 2410 RVA: 0x0002421B File Offset: 0x0002241B
		protected override ICollection GetViewNames()
		{
			if (this._viewNames == null)
			{
				this._viewNames = new ReadOnlyCollection<string>(new string[]
				{
					"DefaultView"
				});
			}
			return this._viewNames;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00024244 File Offset: 0x00022444
		protected override DataSourceView GetView(string viewName)
		{
			if (viewName == null)
			{
				throw new ArgumentNullException("viewName");
			}
			if (viewName.Length != 0 && !string.Equals(viewName, "DefaultView", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.LinqDataSource_InvalidViewName, new object[]
				{
					this.ID,
					"DefaultView"
				}), "viewName");
			}
			return this.View;
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x000242AC File Offset: 0x000224AC
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.IPage.LoadComplete += this.OnPageLoadComplete;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x000242CC File Offset: 0x000224CC
		internal void SetView(QueryableDataSourceView view)
		{
			this._view = view;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000242D8 File Offset: 0x000224D8
		protected virtual void UpdateParameterVales()
		{
			this.View.WhereParameters.UpdateValues(this.Context, this);
			this.View.OrderGroupsByParameters.UpdateValues(this.Context, this);
			this.View.GroupByParameters.UpdateValues(this.Context, this);
			this.View.OrderByParameters.UpdateValues(this.Context, this);
			this.View.SelectNewParameters.UpdateValues(this.Context, this);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00024358 File Offset: 0x00022558
		private void OnPageLoadComplete(object sender, EventArgs e)
		{
			this.UpdateParameterVales();
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00024360 File Offset: 0x00022560
		protected override object SaveViewState()
		{
			Pair pair = new Pair();
			pair.First = base.SaveViewState();
			if (this._view != null)
			{
				pair.Second = ((IStateManager)this._view).SaveViewState();
			}
			if (pair.First == null && pair.Second == null)
			{
				return null;
			}
			return pair;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x000243AB File Offset: 0x000225AB
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._view != null)
			{
				((IStateManager)this._view).TrackViewState();
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x000243C8 File Offset: 0x000225C8
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			Pair pair = (Pair)savedState;
			base.LoadViewState(pair.First);
			if (pair.Second != null)
			{
				((IStateManager)this.View).LoadViewState(pair.Second);
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0002440C File Offset: 0x0002260C
		public void RaiseViewChanged()
		{
			this.View.RaiseViewChanged();
		}

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06000974 RID: 2420 RVA: 0x00024419 File Offset: 0x00022619
		// (remove) Token: 0x06000975 RID: 2421 RVA: 0x00024427 File Offset: 0x00022627
		public event EventHandler<QueryCreatedEventArgs> QueryCreated
		{
			add
			{
				this.View.QueryCreated += value;
			}
			remove
			{
				this.View.QueryCreated -= value;
			}
		}

		// Token: 0x04000310 RID: 784
		private const string DefaultViewName = "DefaultView";

		// Token: 0x04000311 RID: 785
		private ReadOnlyCollection<string> _viewNames;

		// Token: 0x04000312 RID: 786
		private QueryableDataSourceView _view;

		// Token: 0x04000313 RID: 787
		private new readonly IPage _page;
	}
}
