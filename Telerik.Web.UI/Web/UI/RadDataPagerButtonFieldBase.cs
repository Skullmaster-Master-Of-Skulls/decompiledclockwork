using System;
using System.ComponentModel;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001953 RID: 6483
	public abstract class RadDataPagerButtonFieldBase : RadDataPagerField
	{
		// Token: 0x17004BD0 RID: 19408
		// (get) Token: 0x0600FADA RID: 64218 RVA: 0x00387628 File Offset: 0x00385828
		// (set) Token: 0x0600FADB RID: 64219 RVA: 0x00387651 File Offset: 0x00385851
		[TypeConverter(typeof(EnumConverter))]
		[NotifyParentProperty(true)]
		[DefaultValue(PagerFieldButtonType.LinkButton)]
		protected PagerFieldButtonType ButtonType
		{
			get
			{
				object obj = base.ViewState["ButtonType"];
				if (obj != null)
				{
					return (PagerFieldButtonType)obj;
				}
				return PagerFieldButtonType.LinkButton;
			}
			set
			{
				base.ViewState["ButtonType"] = value;
			}
		}

		// Token: 0x0600FADC RID: 64220 RVA: 0x0038766C File Offset: 0x0038586C
		protected WebControl CreateButtonField(PagerFieldButtonType type, string text, string toolTip, string commandName, string commandArgument, string className, string imageUrl, string hiddenSpanText)
		{
			WebControl webControl;
			if (!base.Owner.AllowSEOPaging)
			{
				int num = -1;
				bool flag = int.TryParse(commandArgument, out num);
				webControl = this.CreateButtonFieldForCommand(type, text, toolTip, commandName, commandArgument, hiddenSpanText);
				webControl.CssClass = className;
				if ((base.Owner.ResolvedRenderMode == RenderMode.Lightweight || base.Owner.ResolvedRenderMode == RenderMode.Mobile) && !flag)
				{
					webControl.CssClass = "rdpActionButton " + webControl.CssClass;
				}
			}
			else
			{
				webControl = this.CreateButtonFieldForSEOPaging(text, toolTip, commandArgument, className, imageUrl);
			}
			return this.EnsureEnableState(webControl, commandArgument);
		}

		// Token: 0x0600FADD RID: 64221 RVA: 0x003876FC File Offset: 0x003858FC
		private WebControl CreateButtonFieldForCommand(PagerFieldButtonType type, string text, string toolTip, string commandName, string commandArgument, string hiddenSpanText)
		{
			IButtonControl buttonControl = null;
			int num = -1;
			bool flag = int.TryParse(commandArgument, out num);
			switch (type)
			{
			case PagerFieldButtonType.PushButton:
				buttonControl = new Button();
				if (base.Owner.ResolvedRenderMode == RenderMode.Lightweight || base.Owner.ResolvedRenderMode == RenderMode.Mobile)
				{
					buttonControl = new ElasticButton
					{
						UseSubmitBehavior = false,
						FirstSpanClass = "rdpIcon",
						Text = hiddenSpanText
					};
					if (base.Owner.EnableAriaSupport && !flag)
					{
						((ElasticButton)buttonControl).Attributes.Add("aria-label", string.IsNullOrEmpty(toolTip) ? hiddenSpanText : toolTip);
					}
				}
				((Button)buttonControl).ToolTip = toolTip;
				break;
			case PagerFieldButtonType.LinkButton:
				buttonControl = new LinkButton();
				((LinkButton)buttonControl).ToolTip = toolTip;
				break;
			case PagerFieldButtonType.ImageButton:
				if (base.Owner.ResolvedRenderMode == RenderMode.Lightweight || base.Owner.ResolvedRenderMode == RenderMode.Mobile)
				{
					ElasticButton elasticButton = new ElasticButton
					{
						UseSubmitBehavior = false,
						FirstSpanClass = "rdpIcon"
					};
					buttonControl = elasticButton;
					elasticButton.ToolTip = toolTip;
				}
				else
				{
					ImageButton imageButton = new ImageButton();
					buttonControl = imageButton;
					imageButton.ToolTip = toolTip;
					imageButton.AlternateText = toolTip;
				}
				break;
			}
			buttonControl.Text = (((base.Owner.ResolvedRenderMode == RenderMode.Lightweight || base.Owner.ResolvedRenderMode == RenderMode.Mobile) && flag) ? text : this.PrepareTextFormat(type, text));
			buttonControl.CommandName = commandName;
			buttonControl.CommandArgument = commandArgument;
			buttonControl.CausesValidation = false;
			return buttonControl as WebControl;
		}

		// Token: 0x0600FADE RID: 64222 RVA: 0x0038787C File Offset: 0x00385A7C
		private WebControl CreateButtonFieldForSEOPaging(string text, string toolTip, string commandArgument, string className, string imageUrl)
		{
			HyperLink hyperLink = new HyperLink();
			if (base.Owner.ResolvedRenderMode == RenderMode.Lightweight || base.Owner.ResolvedRenderMode == RenderMode.Mobile)
			{
				this.PrepareLightweightSEOButtonContent(hyperLink, text, commandArgument, className, imageUrl);
			}
			else
			{
				this.PrepareSEOButtonContent(hyperLink, text, commandArgument, className, imageUrl);
			}
			hyperLink.ToolTip = toolTip;
			hyperLink.NavigateUrl = base.SEOPagingLinkBuilder(commandArgument);
			return hyperLink;
		}

		// Token: 0x0600FADF RID: 64223 RVA: 0x00387904 File Offset: 0x00385B04
		private void PrepareLightweightSEOButtonContent(HyperLink button, string text, string commandArgument, string className, string imageUrl)
		{
			button.CssClass = "rdpActionButton ";
			int num = -1;
			if (string.IsNullOrEmpty(imageUrl))
			{
				if (!int.TryParse(commandArgument, out num))
				{
					HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
					htmlGenericControl.Attributes.Add("class", "rdpIcon");
					button.Controls.Add(htmlGenericControl);
					if (commandArgument != null)
					{
						if (!(commandArgument == "First"))
						{
							if (!(commandArgument == "Prev"))
							{
								if (!(commandArgument == "Last"))
								{
									if (commandArgument == "Next")
									{
										button.Attributes.Add("aria-label", string.IsNullOrEmpty(base.Owner.Localization.NextButtonText) ? "Next Page" : base.Owner.Localization.NextButtonText);
									}
								}
								else
								{
									button.Attributes.Add("aria-label", string.IsNullOrEmpty(base.Owner.Localization.LastButtonText) ? "Last Page" : base.Owner.Localization.LastButtonText);
								}
							}
							else
							{
								button.Attributes.Add("aria-label", string.IsNullOrEmpty(base.Owner.Localization.PrevButtonText) ? "Previous Page" : base.Owner.Localization.PrevButtonText);
							}
						}
						else
						{
							button.Attributes.Add("aria-label", string.IsNullOrEmpty(base.Owner.Localization.FirstButtonText) ? "First Page" : base.Owner.Localization.FirstButtonText);
						}
					}
				}
			}
			else
			{
				Image image = new Image();
				image.PreRender += delegate(object s, EventArgs e)
				{
					image.ImageUrl = imageUrl;
				};
				if (commandArgument != null)
				{
					if (!(commandArgument == "First"))
					{
						if (!(commandArgument == "Prev"))
						{
							if (!(commandArgument == "Last"))
							{
								if (commandArgument == "Next")
								{
									image.AlternateText = (string.IsNullOrEmpty(base.Owner.Localization.NextButtonText) ? "Next Page" : base.Owner.Localization.NextButtonText);
								}
							}
							else
							{
								image.AlternateText = (string.IsNullOrEmpty(base.Owner.Localization.LastButtonText) ? "Last Page" : base.Owner.Localization.LastButtonText);
							}
						}
						else
						{
							image.AlternateText = (string.IsNullOrEmpty(base.Owner.Localization.PrevButtonText) ? "Previous Page" : base.Owner.Localization.PrevButtonText);
						}
					}
					else
					{
						image.AlternateText = (string.IsNullOrEmpty(base.Owner.Localization.FirstButtonText) ? "First Page" : base.Owner.Localization.FirstButtonText);
					}
				}
				button.Controls.Add(image);
			}
			if (commandArgument != null)
			{
				if (commandArgument == "First")
				{
					button.CssClass += "rdpPageFirst";
					return;
				}
				if (commandArgument == "Prev")
				{
					button.CssClass += "rdpPagePrev";
					return;
				}
				if (commandArgument == "Last")
				{
					button.CssClass += "rdpPageLast";
					return;
				}
				if (commandArgument == "Next")
				{
					button.CssClass += "rdpPageNext";
					return;
				}
			}
			button.Text = text;
			button.CssClass = className;
		}

		// Token: 0x0600FAE0 RID: 64224 RVA: 0x00387DEC File Offset: 0x00385FEC
		private void PrepareSEOButtonContent(HyperLink button, string text, string commandArgument, string className, string imageUrl)
		{
			Image image = new Image();
			if (commandArgument != null)
			{
				if (commandArgument == "First")
				{
					image.PreRender += delegate(object s, EventArgs e)
					{
						if (string.IsNullOrEmpty(imageUrl))
						{
							image.ImageUrl = this.GetResolvedImageUrl("PagingFirst.gif");
							return;
						}
						image.ImageUrl = imageUrl;
					};
					image.AlternateText = (string.IsNullOrEmpty(base.Owner.Localization.FirstButtonText) ? "First Page" : base.Owner.Localization.FirstButtonText);
					button.Controls.Add(image);
					return;
				}
				if (commandArgument == "Prev")
				{
					image.PreRender += delegate(object s, EventArgs e)
					{
						if (string.IsNullOrEmpty(imageUrl))
						{
							image.ImageUrl = this.GetResolvedImageUrl("PagingPrev.gif");
							return;
						}
						image.ImageUrl = imageUrl;
					};
					image.AlternateText = (string.IsNullOrEmpty(base.Owner.Localization.PrevButtonText) ? "Previous Page" : base.Owner.Localization.PrevButtonText);
					button.Controls.Add(image);
					return;
				}
				if (commandArgument == "Last")
				{
					image.PreRender += delegate(object s, EventArgs e)
					{
						if (string.IsNullOrEmpty(imageUrl))
						{
							image.ImageUrl = this.GetResolvedImageUrl("PagingLast.gif");
							return;
						}
						image.ImageUrl = imageUrl;
					};
					image.AlternateText = (string.IsNullOrEmpty(base.Owner.Localization.LastButtonText) ? "Last Page" : base.Owner.Localization.LastButtonText);
					button.Controls.Add(image);
					return;
				}
				if (commandArgument == "Next")
				{
					image.PreRender += delegate(object s, EventArgs e)
					{
						if (string.IsNullOrEmpty(imageUrl))
						{
							image.ImageUrl = this.GetResolvedImageUrl("PagingNext.gif");
							return;
						}
						image.ImageUrl = imageUrl;
					};
					image.AlternateText = (string.IsNullOrEmpty(base.Owner.Localization.NextButtonText) ? "Next Page" : base.Owner.Localization.NextButtonText);
					button.Controls.Add(image);
					return;
				}
			}
			button.Text = this.PrepareTextFormat(PagerFieldButtonType.LinkButton, text);
			button.CssClass = className;
		}

		// Token: 0x0600FAE1 RID: 64225 RVA: 0x00388038 File Offset: 0x00386238
		protected string GetResolvedImageUrl(string imageName)
		{
			if (base.Owner.Page != null)
			{
				string webResourceUrl = SkinRegistrar.GetWebResourceUrl(base.Owner, string.Format("Telerik.Web.UI.Skins.{0}.Grid.{1}", base.Owner.RuntimeSkin, imageName));
				return webResourceUrl.Replace("&t", "&amp;t");
			}
			return "";
		}

		// Token: 0x0600FAE2 RID: 64226 RVA: 0x0038808C File Offset: 0x0038628C
		protected virtual string PrepareTextFormat(PagerFieldButtonType type, string text)
		{
			string text2 = string.IsNullOrEmpty(text) ? " " : text;
			if (type == PagerFieldButtonType.LinkButton)
			{
				text2 = string.Format("<span>{0}</span>", text2);
			}
			return text2;
		}

		// Token: 0x0600FAE3 RID: 64227 RVA: 0x003880BC File Offset: 0x003862BC
		protected virtual WebControl EnsureEnableState(WebControl button, string commandArgument)
		{
			int currentPageIndex = base.Owner.CurrentPageIndex;
			int num = base.Owner.CalculateStartRowIndex(commandArgument) / base.Owner.PageSize;
			if (currentPageIndex == num)
			{
				HyperLink hyperLink = button as HyperLink;
				if (hyperLink != null && base.Owner.RemoveUrlFromDisabledHyperLinkButtons)
				{
					hyperLink.NavigateUrl = "#";
				}
				else
				{
					button.Attributes.Add("onclick", "return false;");
				}
			}
			return button;
		}

		// Token: 0x0400475B RID: 18267
		protected const string NextButtonClassName = "rdpPageNext";

		// Token: 0x0400475C RID: 18268
		protected const string FirstButtonClassName = "rdpPageFirst";

		// Token: 0x0400475D RID: 18269
		protected const string LastButtonClassName = "rdpPageLast";

		// Token: 0x0400475E RID: 18270
		protected const string PrevButtonClassName = "rdpPagePrev";

		// Token: 0x0400475F RID: 18271
		protected const string CurrentPagerButtonClassName = "rdpCurrentPage";
	}
}
