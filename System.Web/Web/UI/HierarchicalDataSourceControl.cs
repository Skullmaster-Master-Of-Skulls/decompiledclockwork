using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x020003FA RID: 1018
	[NonVisualControl]
	[Bindable(false)]
	[ControlBuilder(typeof(DataSourceControlBuilder))]
	[Designer("System.Web.UI.Design.HierarchicalDataSourceDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class HierarchicalDataSourceControl : Control, IHierarchicalDataSource
	{
		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x000DC659 File Offset: 0x000DB659
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ClientID
		{
			get
			{
				return base.ClientID;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x0600323E RID: 12862 RVA: 0x000DC661 File Offset: 0x000DB661
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x000DC669 File Offset: 0x000DB669
		// (set) Token: 0x06003240 RID: 12864 RVA: 0x000DC66C File Offset: 0x000DB66C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
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

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06003241 RID: 12865 RVA: 0x000DC69E File Offset: 0x000DB69E
		// (set) Token: 0x06003242 RID: 12866 RVA: 0x000DC6A8 File Offset: 0x000DB6A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue("")]
		[Browsable(false)]
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

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06003243 RID: 12867 RVA: 0x000DC6DA File Offset: 0x000DB6DA
		// (set) Token: 0x06003244 RID: 12868 RVA: 0x000DC6E0 File Offset: 0x000DB6E0
		[Browsable(false)]
		[DefaultValue(false)]
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

		// Token: 0x06003245 RID: 12869 RVA: 0x000DC712 File Offset: 0x000DB712
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ApplyStyleSheetSkin(Page page)
		{
			base.ApplyStyleSheetSkin(page);
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000DC71B File Offset: 0x000DB71B
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000DC723 File Offset: 0x000DB723
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Control FindControl(string id)
		{
			return base.FindControl(id);
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000DC72C File Offset: 0x000DB72C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
			{
				base.GetType().Name
			}));
		}

		// Token: 0x06003249 RID: 12873
		protected abstract HierarchicalDataSourceView GetHierarchicalView(string viewPath);

		// Token: 0x0600324A RID: 12874 RVA: 0x000DC75E File Offset: 0x000DB75E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HasControls()
		{
			return base.HasControls();
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000DC768 File Offset: 0x000DB768
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HierarchicalDataSourceControl.EventDataSourceChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000DC796 File Offset: 0x000DB796
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void RenderControl(HtmlTextWriter writer)
		{
			base.RenderControl(writer);
		}

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x0600324D RID: 12877 RVA: 0x000DC79F File Offset: 0x000DB79F
		// (remove) Token: 0x0600324E RID: 12878 RVA: 0x000DC7B2 File Offset: 0x000DB7B2
		event EventHandler IHierarchicalDataSource.DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(HierarchicalDataSourceControl.EventDataSourceChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(HierarchicalDataSourceControl.EventDataSourceChanged, value);
			}
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000DC7C5 File Offset: 0x000DB7C5
		HierarchicalDataSourceView IHierarchicalDataSource.GetHierarchicalView(string viewPath)
		{
			return this.GetHierarchicalView(viewPath);
		}

		// Token: 0x040022FC RID: 8956
		private static readonly object EventDataSourceChanged = new object();
	}
}
