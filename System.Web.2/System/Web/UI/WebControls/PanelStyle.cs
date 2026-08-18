using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200049B RID: 1179
	public class PanelStyle : Style
	{
		// Token: 0x06003A98 RID: 15000 RVA: 0x000B75F5 File Offset: 0x000B57F5
		public PanelStyle(StateBag bag) : base(bag)
		{
		}

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06003A99 RID: 15001 RVA: 0x000BE1C0 File Offset: 0x000BC3C0
		// (set) Token: 0x06003A9A RID: 15002 RVA: 0x000BE1EA File Offset: 0x000BC3EA
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[UrlProperty]
		[WebSysDescription("Panel_BackImageUrl")]
		public virtual string BackImageUrl
		{
			get
			{
				if (base.IsSet(65536))
				{
					return (string)base.ViewState["BackImageUrl"];
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.ViewState["BackImageUrl"] = value;
				this.SetBit(65536);
			}
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06003A9B RID: 15003 RVA: 0x000BE216 File Offset: 0x000BC416
		// (set) Token: 0x06003A9C RID: 15004 RVA: 0x000BE23C File Offset: 0x000BC43C
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("Panel_Direction")]
		public virtual ContentDirection Direction
		{
			get
			{
				if (base.IsSet(131072))
				{
					return (ContentDirection)base.ViewState["Direction"];
				}
				return ContentDirection.NotSet;
			}
			set
			{
				if (value < ContentDirection.NotSet || value > ContentDirection.RightToLeft)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["Direction"] = value;
				this.SetBit(131072);
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06003A9D RID: 15005 RVA: 0x000BE272 File Offset: 0x000BC472
		// (set) Token: 0x06003A9E RID: 15006 RVA: 0x000BE298 File Offset: 0x000BC498
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("Panel_HorizontalAlign")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (base.IsSet(262144))
				{
					return (HorizontalAlign)base.ViewState["HorizontalAlign"];
				}
				return HorizontalAlign.NotSet;
			}
			set
			{
				if (value < HorizontalAlign.NotSet || value > HorizontalAlign.Justify)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["HorizontalAlign"] = value;
				this.SetBit(262144);
			}
		}

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06003A9F RID: 15007 RVA: 0x000BE2CE File Offset: 0x000BC4CE
		// (set) Token: 0x06003AA0 RID: 15008 RVA: 0x000BE2F4 File Offset: 0x000BC4F4
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("Panel_ScrollBars")]
		public virtual ScrollBars ScrollBars
		{
			get
			{
				if (base.IsSet(524288))
				{
					return (ScrollBars)base.ViewState["ScrollBars"];
				}
				return ScrollBars.None;
			}
			set
			{
				if (value < ScrollBars.None || value > ScrollBars.Auto)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["ScrollBars"] = value;
				this.SetBit(524288);
			}
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x06003AA1 RID: 15009 RVA: 0x000BE32A File Offset: 0x000BC52A
		// (set) Token: 0x06003AA2 RID: 15010 RVA: 0x000BE350 File Offset: 0x000BC550
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("Panel_Wrap")]
		public virtual bool Wrap
		{
			get
			{
				return !base.IsSet(1048576) || (bool)base.ViewState["Wrap"];
			}
			set
			{
				base.ViewState["Wrap"] = value;
				this.SetBit(1048576);
			}
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000BE374 File Offset: 0x000BC574
		public override void CopyFrom(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				base.CopyFrom(s);
				if (s is PanelStyle)
				{
					PanelStyle panelStyle = (PanelStyle)s;
					if (s.RegisteredCssClass.Length != 0)
					{
						if (panelStyle.IsSet(65536))
						{
							base.ViewState.Remove("BackImageUrl");
							base.ClearBit(65536);
						}
						if (panelStyle.IsSet(524288))
						{
							base.ViewState.Remove("ScrollBars");
							base.ClearBit(524288);
						}
						if (panelStyle.IsSet(1048576))
						{
							base.ViewState.Remove("Wrap");
							base.ClearBit(1048576);
						}
					}
					else
					{
						if (panelStyle.IsSet(65536))
						{
							this.BackImageUrl = panelStyle.BackImageUrl;
						}
						if (panelStyle.IsSet(524288))
						{
							this.ScrollBars = panelStyle.ScrollBars;
						}
						if (panelStyle.IsSet(1048576))
						{
							this.Wrap = panelStyle.Wrap;
						}
					}
					if (panelStyle.IsSet(131072))
					{
						this.Direction = panelStyle.Direction;
					}
					if (panelStyle.IsSet(262144))
					{
						this.HorizontalAlign = panelStyle.HorizontalAlign;
					}
				}
			}
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x000BE4B0 File Offset: 0x000BC6B0
		public override void MergeWith(Style s)
		{
			if (s != null && !s.IsEmpty)
			{
				if (this.IsEmpty)
				{
					this.CopyFrom(s);
					return;
				}
				base.MergeWith(s);
				if (s is PanelStyle)
				{
					PanelStyle panelStyle = (PanelStyle)s;
					if (s.RegisteredCssClass.Length == 0)
					{
						if (panelStyle.IsSet(65536) && !base.IsSet(65536))
						{
							this.BackImageUrl = panelStyle.BackImageUrl;
						}
						if (panelStyle.IsSet(524288) && !base.IsSet(524288))
						{
							this.ScrollBars = panelStyle.ScrollBars;
						}
						if (panelStyle.IsSet(1048576) && !base.IsSet(1048576))
						{
							this.Wrap = panelStyle.Wrap;
						}
					}
					if (panelStyle.IsSet(131072) && !base.IsSet(131072))
					{
						this.Direction = panelStyle.Direction;
					}
					if (panelStyle.IsSet(262144) && !base.IsSet(262144))
					{
						this.HorizontalAlign = panelStyle.HorizontalAlign;
					}
				}
			}
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x000BE5C4 File Offset: 0x000BC7C4
		public override void Reset()
		{
			if (base.IsSet(65536))
			{
				base.ViewState.Remove("BackImageUrl");
			}
			if (base.IsSet(131072))
			{
				base.ViewState.Remove("Direction");
			}
			if (base.IsSet(262144))
			{
				base.ViewState.Remove("HorizontalAlign");
			}
			if (base.IsSet(524288))
			{
				base.ViewState.Remove("ScrollBars");
			}
			if (base.IsSet(1048576))
			{
				base.ViewState.Remove("Wrap");
			}
			base.Reset();
		}

		// Token: 0x04002304 RID: 8964
		private const int PROP_BACKIMAGEURL = 65536;

		// Token: 0x04002305 RID: 8965
		private const int PROP_DIRECTION = 131072;

		// Token: 0x04002306 RID: 8966
		private const int PROP_HORIZONTALALIGN = 262144;

		// Token: 0x04002307 RID: 8967
		private const int PROP_SCROLLBARS = 524288;

		// Token: 0x04002308 RID: 8968
		private const int PROP_WRAP = 1048576;

		// Token: 0x04002309 RID: 8969
		private const string STR_BACKIMAGEURL = "BackImageUrl";

		// Token: 0x0400230A RID: 8970
		private const string STR_DIRECTION = "Direction";

		// Token: 0x0400230B RID: 8971
		private const string STR_HORIZONTALALIGN = "HorizontalAlign";

		// Token: 0x0400230C RID: 8972
		private const string STR_SCROLLBARS = "ScrollBars";

		// Token: 0x0400230D RID: 8973
		private const string STR_WRAP = "Wrap";
	}
}
