using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000445 RID: 1093
	[DefaultEvent("Click")]
	[DefaultProperty("HotSpots")]
	[ParseChildren(true, "HotSpots")]
	[SupportsEventValidation]
	public class ImageMap : Image, IPostBackEventHandler
	{
		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x060034F8 RID: 13560 RVA: 0x000AB2D1 File Offset: 0x000A94D1
		// (set) Token: 0x060034F9 RID: 13561 RVA: 0x000AB2D9 File Offset: 0x000A94D9
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x060034FA RID: 13562 RVA: 0x000AC187 File Offset: 0x000AA387
		[WebCategory("Behavior")]
		[WebSysDescription("ImageMap_HotSpots")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public HotSpotCollection HotSpots
		{
			get
			{
				if (this._hotSpots == null)
				{
					this._hotSpots = new HotSpotCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._hotSpots).TrackViewState();
					}
				}
				return this._hotSpots;
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x060034FB RID: 13563 RVA: 0x000AC1B8 File Offset: 0x000AA3B8
		// (set) Token: 0x060034FC RID: 13564 RVA: 0x000AC1E1 File Offset: 0x000AA3E1
		[WebCategory("Behavior")]
		[DefaultValue(HotSpotMode.NotSet)]
		[WebSysDescription("HotSpot_HotSpotMode")]
		public virtual HotSpotMode HotSpotMode
		{
			get
			{
				object obj = this.ViewState["HotSpotMode"];
				if (obj != null)
				{
					return (HotSpotMode)obj;
				}
				return HotSpotMode.NotSet;
			}
			set
			{
				if (value < HotSpotMode.NotSet || value > HotSpotMode.Inactive)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["HotSpotMode"] = value;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x060034FD RID: 13565 RVA: 0x000AC20C File Offset: 0x000AA40C
		// (set) Token: 0x060034FE RID: 13566 RVA: 0x000835A9 File Offset: 0x000817A9
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("HotSpot_Target")]
		public virtual string Target
		{
			get
			{
				object obj = this.ViewState["Target"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x140000AB RID: 171
		// (add) Token: 0x060034FF RID: 13567 RVA: 0x000AC239 File Offset: 0x000AA439
		// (remove) Token: 0x06003500 RID: 13568 RVA: 0x000AC24C File Offset: 0x000AA44C
		[Category("Action")]
		[WebSysDescription("ImageMap_Click")]
		public event ImageMapEventHandler Click
		{
			add
			{
				base.Events.AddHandler(ImageMap.EventClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(ImageMap.EventClick, value);
			}
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000AC25F File Offset: 0x000AA45F
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (this._hasHotSpots)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Usemap, "#ImageMap" + this.ClientID, false);
			}
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000AC28C File Offset: 0x000AA48C
		protected override void LoadViewState(object savedState)
		{
			object savedState2 = null;
			object[] array = null;
			if (savedState != null)
			{
				array = (object[])savedState;
				if (array.Length != 2)
				{
					throw new ArgumentException(SR.GetString("ViewState_InvalidViewState"));
				}
				savedState2 = array[0];
			}
			base.LoadViewState(savedState2);
			if (array != null && array[1] != null)
			{
				((IStateManager)this.HotSpots).LoadViewState(array[1]);
			}
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000AC2E0 File Offset: 0x000AA4E0
		protected virtual void OnClick(ImageMapEventArgs e)
		{
			ImageMapEventHandler imageMapEventHandler = (ImageMapEventHandler)base.Events[ImageMap.EventClick];
			if (imageMapEventHandler != null)
			{
				imageMapEventHandler(this, e);
			}
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000AC310 File Offset: 0x000AA510
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Enabled && !base.IsEnabled && this.SupportsDisabledAttribute)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			this._hasHotSpots = (this._hotSpots != null && this._hotSpots.Count > 0);
			base.Render(writer);
			if (this._hasHotSpots)
			{
				string value = "ImageMap" + this.ClientID;
				writer.AddAttribute(HtmlTextWriterAttribute.Name, value);
				writer.AddAttribute(HtmlTextWriterAttribute.Id, value);
				writer.RenderBeginTag(HtmlTextWriterTag.Map);
				HotSpotMode hotSpotMode = this.HotSpotMode;
				if (hotSpotMode == HotSpotMode.NotSet)
				{
					hotSpotMode = HotSpotMode.Navigate;
				}
				int num = 0;
				string target = this.Target;
				foreach (object obj in this._hotSpots)
				{
					HotSpot hotSpot = (HotSpot)obj;
					writer.AddAttribute(HtmlTextWriterAttribute.Shape, hotSpot.MarkupName, false);
					writer.AddAttribute(HtmlTextWriterAttribute.Coords, hotSpot.GetCoordinates());
					HotSpotMode hotSpotMode2 = hotSpot.HotSpotMode;
					if (hotSpotMode2 == HotSpotMode.NotSet)
					{
						hotSpotMode2 = hotSpotMode;
					}
					if (hotSpotMode2 == HotSpotMode.PostBack)
					{
						if (this.Page != null)
						{
							this.Page.VerifyRenderingInServerForm(this);
						}
						if (this.RenderingCompatibility < VersionUtil.Framework40 || base.IsEnabled)
						{
							string argument = num.ToString(CultureInfo.InvariantCulture);
							writer.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackClientHyperlink(this, argument, true));
						}
					}
					else if (hotSpotMode2 == HotSpotMode.Navigate)
					{
						if (this.RenderingCompatibility < VersionUtil.Framework40 || base.IsEnabled)
						{
							string value2 = base.ResolveClientUrl(hotSpot.NavigateUrl);
							writer.AddAttribute(HtmlTextWriterAttribute.Href, value2);
						}
						string text = hotSpot.Target;
						if (text.Length == 0)
						{
							text = target;
						}
						if (text.Length > 0)
						{
							writer.AddAttribute(HtmlTextWriterAttribute.Target, text);
						}
					}
					else if (hotSpotMode2 == HotSpotMode.Inactive)
					{
						writer.AddAttribute("nohref", "true");
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Title, hotSpot.AlternateText);
					writer.AddAttribute(HtmlTextWriterAttribute.Alt, hotSpot.AlternateText);
					string accessKey = hotSpot.AccessKey;
					if (accessKey.Length > 0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, accessKey);
					}
					int tabIndex = (int)hotSpot.TabIndex;
					if (tabIndex != 0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, tabIndex.ToString(NumberFormatInfo.InvariantInfo));
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Area);
					writer.RenderEndTag();
					num++;
				}
				writer.RenderEndTag();
			}
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000AC588 File Offset: 0x000AA788
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = null;
			if (this._hotSpots != null && this._hotSpots.Count > 0)
			{
				obj2 = ((IStateManager)this._hotSpots).SaveViewState();
			}
			if (obj != null || obj2 != null)
			{
				return new object[]
				{
					obj,
					obj2
				};
			}
			return null;
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000AC5D8 File Offset: 0x000AA7D8
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._hotSpots != null)
			{
				((IStateManager)this._hotSpots).TrackViewState();
			}
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000AC5F3 File Offset: 0x000AA7F3
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000AC5FC File Offset: 0x000AA7FC
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			string text = null;
			if (eventArgument != null && this._hotSpots != null)
			{
				int num = int.Parse(eventArgument, CultureInfo.InvariantCulture);
				if (num >= 0 && num < this._hotSpots.Count)
				{
					HotSpot hotSpot = this._hotSpots[num];
					HotSpotMode hotSpotMode = hotSpot.HotSpotMode;
					if (hotSpotMode == HotSpotMode.NotSet)
					{
						hotSpotMode = this.HotSpotMode;
					}
					if (hotSpotMode == HotSpotMode.PostBack)
					{
						text = hotSpot.PostBackValue;
					}
				}
			}
			if (text != null)
			{
				this.OnClick(new ImageMapEventArgs(text));
			}
		}

		// Token: 0x040021B6 RID: 8630
		private static readonly object EventClick = new object();

		// Token: 0x040021B7 RID: 8631
		private bool _hasHotSpots;

		// Token: 0x040021B8 RID: 8632
		private HotSpotCollection _hotSpots;
	}
}
