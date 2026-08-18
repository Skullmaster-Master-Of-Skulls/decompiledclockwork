using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200060E RID: 1550
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class PanelStyle : Style
	{
		// Token: 0x06004C9A RID: 19610 RVA: 0x00136E88 File Offset: 0x00135E88
		public PanelStyle(StateBag bag) : base(bag)
		{
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x06004C9B RID: 19611 RVA: 0x00136E91 File Offset: 0x00135E91
		// (set) Token: 0x06004C9C RID: 19612 RVA: 0x00136EBB File Offset: 0x00135EBB
		[WebSysDescription("Panel_BackImageUrl")]
		[DefaultValue("")]
		[UrlProperty]
		[WebCategory("Appearance")]
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

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x06004C9D RID: 19613 RVA: 0x00136EE7 File Offset: 0x00135EE7
		// (set) Token: 0x06004C9E RID: 19614 RVA: 0x00136F0D File Offset: 0x00135F0D
		[DefaultValue("")]
		[WebSysDescription("Panel_Direction")]
		[WebCategory("Appearance")]
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

		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x06004C9F RID: 19615 RVA: 0x00136F43 File Offset: 0x00135F43
		// (set) Token: 0x06004CA0 RID: 19616 RVA: 0x00136F69 File Offset: 0x00135F69
		[WebSysDescription("Panel_HorizontalAlign")]
		[WebCategory("Appearance")]
		[DefaultValue("")]
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

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x06004CA1 RID: 19617 RVA: 0x00136F9F File Offset: 0x00135F9F
		// (set) Token: 0x06004CA2 RID: 19618 RVA: 0x00136FC5 File Offset: 0x00135FC5
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

		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x06004CA3 RID: 19619 RVA: 0x00136FFB File Offset: 0x00135FFB
		// (set) Token: 0x06004CA4 RID: 19620 RVA: 0x00137021 File Offset: 0x00136021
		[DefaultValue("")]
		[WebCategory("Appearance")]
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

		// Token: 0x06004CA5 RID: 19621 RVA: 0x00137044 File Offset: 0x00136044
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

		// Token: 0x06004CA6 RID: 19622 RVA: 0x00137180 File Offset: 0x00136180
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

		// Token: 0x06004CA7 RID: 19623 RVA: 0x00137294 File Offset: 0x00136294
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

		// Token: 0x04002C0A RID: 11274
		private const int PROP_BACKIMAGEURL = 65536;

		// Token: 0x04002C0B RID: 11275
		private const int PROP_DIRECTION = 131072;

		// Token: 0x04002C0C RID: 11276
		private const int PROP_HORIZONTALALIGN = 262144;

		// Token: 0x04002C0D RID: 11277
		private const int PROP_SCROLLBARS = 524288;

		// Token: 0x04002C0E RID: 11278
		private const int PROP_WRAP = 1048576;

		// Token: 0x04002C0F RID: 11279
		private const string STR_BACKIMAGEURL = "BackImageUrl";

		// Token: 0x04002C10 RID: 11280
		private const string STR_DIRECTION = "Direction";

		// Token: 0x04002C11 RID: 11281
		private const string STR_HORIZONTALALIGN = "HorizontalAlign";

		// Token: 0x04002C12 RID: 11282
		private const string STR_SCROLLBARS = "ScrollBars";

		// Token: 0x04002C13 RID: 11283
		private const string STR_WRAP = "Wrap";
	}
}
