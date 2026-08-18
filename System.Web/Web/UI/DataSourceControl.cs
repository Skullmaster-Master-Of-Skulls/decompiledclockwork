using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x020003DD RID: 989
	[Bindable(false)]
	[Designer("System.Web.UI.Design.DataSourceDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[NonVisualControl]
	[ControlBuilder(typeof(DataSourceControlBuilder))]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class DataSourceControl : Control, IDataSource, IListSource
	{
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06003005 RID: 12293 RVA: 0x000D4BEB File Offset: 0x000D3BEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ClientID
		{
			get
			{
				return base.ClientID;
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06003006 RID: 12294 RVA: 0x000D4BF3 File Offset: 0x000D3BF3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06003007 RID: 12295 RVA: 0x000D4BFB File Offset: 0x000D3BFB
		// (set) Token: 0x06003008 RID: 12296 RVA: 0x000D4C00 File Offset: 0x000D3C00
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
		[Browsable(false)]
		public override bool EnableTheming
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("NoThemingSupport", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06003009 RID: 12297 RVA: 0x000D4C32 File Offset: 0x000D3C32
		// (set) Token: 0x0600300A RID: 12298 RVA: 0x000D4C3C File Offset: 0x000D3C3C
		[Browsable(false)]
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string SkinID
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("NoThemingSupport", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x0600300B RID: 12299 RVA: 0x000D4C6E File Offset: 0x000D3C6E
		// (set) Token: 0x0600300C RID: 12300 RVA: 0x000D4C74 File Offset: 0x000D3C74
		[DefaultValue(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Visible
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("ControlNonVisual", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x0600300D RID: 12301 RVA: 0x000D4CA6 File Offset: 0x000D3CA6
		// (remove) Token: 0x0600300E RID: 12302 RVA: 0x000D4CB9 File Offset: 0x000D3CB9
		internal event EventHandler DataSourceChangedInternal
		{
			add
			{
				base.Events.AddHandler(DataSourceControl.EventDataSourceChangedInternal, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataSourceControl.EventDataSourceChangedInternal, value);
			}
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000D4CCC File Offset: 0x000D3CCC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ApplyStyleSheetSkin(Page page)
		{
			base.ApplyStyleSheetSkin(page);
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000D4CD5 File Offset: 0x000D3CD5
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000D4CDD File Offset: 0x000D3CDD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Control FindControl(string id)
		{
			return base.FindControl(id);
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000D4CE8 File Offset: 0x000D3CE8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
			{
				base.GetType().Name
			}));
		}

		// Token: 0x06003013 RID: 12307
		protected abstract DataSourceView GetView(string viewName);

		// Token: 0x06003014 RID: 12308 RVA: 0x000D4D1A File Offset: 0x000D3D1A
		protected virtual ICollection GetViewNames()
		{
			return null;
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000D4D1D File Offset: 0x000D3D1D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HasControls()
		{
			return base.HasControls();
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000D4D28 File Offset: 0x000D3D28
		private void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataSourceControl.EventDataSourceChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000D4D58 File Offset: 0x000D3D58
		private void OnDataSourceChangedInternal(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataSourceControl.EventDataSourceChangedInternal];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000D4D86 File Offset: 0x000D3D86
		protected virtual void RaiseDataSourceChangedEvent(EventArgs e)
		{
			this.OnDataSourceChangedInternal(e);
			this.OnDataSourceChanged(e);
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000D4D96 File Offset: 0x000D3D96
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void RenderControl(HtmlTextWriter writer)
		{
			base.RenderControl(writer);
		}

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x0600301A RID: 12314 RVA: 0x000D4D9F File Offset: 0x000D3D9F
		// (remove) Token: 0x0600301B RID: 12315 RVA: 0x000D4DB2 File Offset: 0x000D3DB2
		event EventHandler IDataSource.DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(DataSourceControl.EventDataSourceChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataSourceControl.EventDataSourceChanged, value);
			}
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000D4DC5 File Offset: 0x000D3DC5
		DataSourceView IDataSource.GetView(string viewName)
		{
			return this.GetView(viewName);
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000D4DCE File Offset: 0x000D3DCE
		ICollection IDataSource.GetViewNames()
		{
			return this.GetViewNames();
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x0600301E RID: 12318 RVA: 0x000D4DD6 File Offset: 0x000D3DD6
		bool IListSource.ContainsListCollection
		{
			get
			{
				return !base.DesignMode && ListSourceHelper.ContainsListCollection(this);
			}
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000D4DE8 File Offset: 0x000D3DE8
		IList IListSource.GetList()
		{
			if (base.DesignMode)
			{
				return null;
			}
			return ListSourceHelper.GetList(this);
		}

		// Token: 0x04002204 RID: 8708
		private static readonly object EventDataSourceChanged = new object();

		// Token: 0x04002205 RID: 8709
		private static readonly object EventDataSourceChangedInternal = new object();
	}
}
