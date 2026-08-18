using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI
{
	// Token: 0x0200027A RID: 634
	[Bindable(false)]
	[ControlBuilder(typeof(DataSourceControlBuilder))]
	[Designer("System.Web.UI.Design.DataSourceDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[NonVisualControl]
	public abstract class DataSourceControl : Control, IDataSource, IListSource
	{
		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x000610CF File Offset: 0x0005F2CF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ClientID
		{
			get
			{
				return base.ClientID;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x000610D7 File Offset: 0x0005F2D7
		// (set) Token: 0x06001E03 RID: 7683 RVA: 0x00010D64 File Offset: 0x0000EF64
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

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x000610DF File Offset: 0x0005F2DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x06001E06 RID: 7686 RVA: 0x000610E7 File Offset: 0x0005F2E7
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

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x00028752 File Offset: 0x00026952
		// (set) Token: 0x06001E08 RID: 7688 RVA: 0x000610E7 File Offset: 0x0005F2E7
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

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x06001E0A RID: 7690 RVA: 0x0006110C File Offset: 0x0005F30C
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

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06001E0B RID: 7691 RVA: 0x00061131 File Offset: 0x0005F331
		// (remove) Token: 0x06001E0C RID: 7692 RVA: 0x00061144 File Offset: 0x0005F344
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

		// Token: 0x06001E0D RID: 7693 RVA: 0x00061157 File Offset: 0x0005F357
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void ApplyStyleSheetSkin(Page page)
		{
			base.ApplyStyleSheetSkin(page);
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x00060B2F File Offset: 0x0005ED2F
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x00061160 File Offset: 0x0005F360
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Control FindControl(string id)
		{
			return base.FindControl(id);
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x00061169 File Offset: 0x0005F369
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
			{
				base.GetType().Name
			}));
		}

		// Token: 0x06001E11 RID: 7697
		protected abstract DataSourceView GetView(string viewName);

		// Token: 0x06001E12 RID: 7698 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual ICollection GetViewNames()
		{
			return null;
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x0006118E File Offset: 0x0005F38E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HasControls()
		{
			return base.HasControls();
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x00061198 File Offset: 0x0005F398
		private void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataSourceControl.EventDataSourceChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x000611C8 File Offset: 0x0005F3C8
		private void OnDataSourceChangedInternal(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataSourceControl.EventDataSourceChangedInternal];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000611F6 File Offset: 0x0005F3F6
		protected virtual void RaiseDataSourceChangedEvent(EventArgs e)
		{
			this.OnDataSourceChangedInternal(e);
			this.OnDataSourceChanged(e);
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x00061206 File Offset: 0x0005F406
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void RenderControl(HtmlTextWriter writer)
		{
			base.RenderControl(writer);
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06001E18 RID: 7704 RVA: 0x0006120F File Offset: 0x0005F40F
		// (remove) Token: 0x06001E19 RID: 7705 RVA: 0x00061222 File Offset: 0x0005F422
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

		// Token: 0x06001E1A RID: 7706 RVA: 0x00061235 File Offset: 0x0005F435
		DataSourceView IDataSource.GetView(string viewName)
		{
			return this.GetView(viewName);
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x0006123E File Offset: 0x0005F43E
		ICollection IDataSource.GetViewNames()
		{
			return this.GetViewNames();
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001E1C RID: 7708 RVA: 0x00061246 File Offset: 0x0005F446
		bool IListSource.ContainsListCollection
		{
			get
			{
				return !base.DesignMode && ListSourceHelper.ContainsListCollection(this);
			}
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00061258 File Offset: 0x0005F458
		IList IListSource.GetList()
		{
			if (base.DesignMode)
			{
				return null;
			}
			return ListSourceHelper.GetList(this);
		}

		// Token: 0x0400197C RID: 6524
		private static readonly object EventDataSourceChanged = new object();

		// Token: 0x0400197D RID: 6525
		private static readonly object EventDataSourceChangedInternal = new object();
	}
}
