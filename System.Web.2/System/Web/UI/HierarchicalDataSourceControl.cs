using System;
using System.ComponentModel;

namespace System.Web.UI
{
	// Token: 0x0200028E RID: 654
	[Bindable(false)]
	[ControlBuilder(typeof(DataSourceControlBuilder))]
	[Designer("System.Web.UI.Design.HierarchicalDataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[NonVisualControl]
	public abstract class HierarchicalDataSourceControl : Control, IHierarchicalDataSource
	{
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x000610CF File Offset: 0x0005F2CF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ClientID
		{
			get
			{
				return base.ClientID;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x000610D7 File Offset: 0x0005F2D7
		// (set) Token: 0x06001ECA RID: 7882 RVA: 0x00010D64 File Offset: 0x0000EF64
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ClientIDMode ClientIDMode
		{
			get
			{
				return base.ClientIDMode;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001ECB RID: 7883 RVA: 0x000610DF File Offset: 0x0005F2DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x06001ECD RID: 7885 RVA: 0x000610E7 File Offset: 0x0005F2E7
		[Browsable(false)]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x00028752 File Offset: 0x00026952
		// (set) Token: 0x06001ECF RID: 7887 RVA: 0x000610E7 File Offset: 0x0005F2E7
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

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x06001ED1 RID: 7889 RVA: 0x0006110C File Offset: 0x0005F30C
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

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00061157 File Offset: 0x0005F357
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ApplyStyleSheetSkin(Page page)
		{
			base.ApplyStyleSheetSkin(page);
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00060B2F File Offset: 0x0005ED2F
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x00061160 File Offset: 0x0005F360
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Control FindControl(string id)
		{
			return base.FindControl(id);
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00061169 File Offset: 0x0005F369
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
			{
				base.GetType().Name
			}));
		}

		// Token: 0x06001ED6 RID: 7894
		protected abstract HierarchicalDataSourceView GetHierarchicalView(string viewPath);

		// Token: 0x06001ED7 RID: 7895 RVA: 0x0006118E File Offset: 0x0005F38E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HasControls()
		{
			return base.HasControls();
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x000626D4 File Offset: 0x000608D4
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HierarchicalDataSourceControl.EventDataSourceChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00061206 File Offset: 0x0005F406
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void RenderControl(HtmlTextWriter writer)
		{
			base.RenderControl(writer);
		}

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x06001EDA RID: 7898 RVA: 0x00062702 File Offset: 0x00060902
		// (remove) Token: 0x06001EDB RID: 7899 RVA: 0x00062715 File Offset: 0x00060915
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

		// Token: 0x06001EDC RID: 7900 RVA: 0x00062728 File Offset: 0x00060928
		HierarchicalDataSourceView IHierarchicalDataSource.GetHierarchicalView(string viewPath)
		{
			return this.GetHierarchicalView(viewPath);
		}

		// Token: 0x040019AC RID: 6572
		private static readonly object EventDataSourceChanged = new object();
	}
}
