using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000567 RID: 1383
	[Bindable(false)]
	[Designer("System.Web.UI.Design.WebControls.WebParts.ProxyWebPartManagerDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[NonVisualControl]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class ProxyWebPartManager : Control
	{
		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x06004633 RID: 17971 RVA: 0x000610CF File Offset: 0x0005F2CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ClientID
		{
			get
			{
				return base.ClientID;
			}
		}

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x06004634 RID: 17972 RVA: 0x000610DF File Offset: 0x0005F2DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x06004635 RID: 17973 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x06004636 RID: 17974 RVA: 0x000610E7 File Offset: 0x0005F2E7
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

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x06004637 RID: 17975 RVA: 0x00028752 File Offset: 0x00026952
		// (set) Token: 0x06004638 RID: 17976 RVA: 0x000610E7 File Offset: 0x0005F2E7
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

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x06004639 RID: 17977 RVA: 0x000E7730 File Offset: 0x000E5930
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Behavior")]
		[WebSysDescription("WebPartManager_StaticConnections")]
		public ProxyWebPartConnectionCollection StaticConnections
		{
			get
			{
				if (this._staticConnections == null)
				{
					this._staticConnections = new ProxyWebPartConnectionCollection();
				}
				return this._staticConnections;
			}
		}

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x0600463A RID: 17978 RVA: 0x00007722 File Offset: 0x00005922
		// (set) Token: 0x0600463B RID: 17979 RVA: 0x0006110C File Offset: 0x0005F30C
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

		// Token: 0x0600463C RID: 17980 RVA: 0x00060B2F File Offset: 0x0005ED2F
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x0600463D RID: 17981 RVA: 0x00061169 File Offset: 0x0005F369
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException(SR.GetString("NoFocusSupport", new object[]
			{
				base.GetType().Name
			}));
		}

		// Token: 0x0600463E RID: 17982 RVA: 0x000E774C File Offset: 0x000E594C
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			Page page = this.Page;
			if (page != null && !base.DesignMode)
			{
				WebPartManager currentWebPartManager = WebPartManager.GetCurrentWebPartManager(page);
				if (currentWebPartManager == null)
				{
					throw new InvalidOperationException(SR.GetString("WebPartManagerRequired"));
				}
				this.StaticConnections.SetWebPartManager(currentWebPartManager);
			}
		}

		// Token: 0x04002695 RID: 9877
		private ProxyWebPartConnectionCollection _staticConnections;
	}
}
