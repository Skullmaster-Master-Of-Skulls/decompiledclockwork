using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020005C6 RID: 1478
	public class MediaPlayerTitlebar : CompositeControl
	{
		// Token: 0x060034FA RID: 13562 RVA: 0x000AEAC4 File Offset: 0x000ACCC4
		public MediaPlayerTitlebar(RadMediaPlayer ownerMediaPlayer)
		{
			this.owner = ownerMediaPlayer;
			this.ID = "Titlebar";
		}

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x060034FB RID: 13563 RVA: 0x000AEADE File Offset: 0x000ACCDE
		public Literal TitleHolder
		{
			get
			{
				this.EnsureChildControls();
				return this.titleHolder;
			}
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x060034FC RID: 13564 RVA: 0x000AEAEC File Offset: 0x000ACCEC
		public HtmlButton ShareButton
		{
			get
			{
				this.EnsureChildControls();
				return this.btnShare;
			}
		}

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x060034FD RID: 13565 RVA: 0x000AEAFA File Offset: 0x000ACCFA
		public RadSocialShare SocialShare
		{
			get
			{
				this.EnsureChildControls();
				return this.mpSocialShare;
			}
		}

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x060034FE RID: 13566 RVA: 0x000AEB08 File Offset: 0x000ACD08
		public RadMediaPlayer OwnerMediaPlayer
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000AEB28 File Offset: 0x000ACD28
		protected override void CreateChildControls()
		{
			this.titleHolder = new Literal
			{
				ID = "titleHolder"
			};
			this.titleHolder.PreRender += delegate(object sender, EventArgs args)
			{
				((Literal)sender).Text = this.OwnerMediaPlayer.Title;
			};
			this.btnShare = MediaPlayerToolbar.InitializeButtonControl("SocialButton", "Share", this.OwnerMediaPlayer.TitleBarShareToolTip);
			this.mpSocialShare = new RadSocialShare
			{
				ID = "SocialShare",
				Orientation = Orientation.Vertical,
				RenderMode = this.owner.RenderMode
			};
			this.mpSocialShare.PreRender += this.OwnerMediaPlayer.HandleChildControlsPreRender;
			if (this.owner.ResolvedRenderMode != RenderMode.Mobile)
			{
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.ShareOnGooglePlus
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.ShareOnFacebook
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.ShareOnTwitter
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.Digg
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.Tumblr
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.StumbleUpon
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.Reddit
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.LinkedIn
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.Delicious
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.Blogger
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.MySpace
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.GoogleBookmarks
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.MailTo
				});
			}
			else
			{
				this.mpSocialShare.EnableEmbeddedSkins = false;
				this.mpSocialShare.EnableEmbeddedBaseStylesheet = false;
				this.mpSocialShare.Skin = "";
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.ShareOnFacebook,
					LabelText = "Facebook"
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.ShareOnGooglePlus,
					LabelText = "Google+",
					ToolTip = "Google+"
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.ShareOnTwitter,
					LabelText = "Twitter"
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.Digg,
					LabelText = "Digg"
				});
				this.mpSocialShare.MainButtons.Add(new RadSocialButton
				{
					SocialNetType = SocialNetType.LinkedIn,
					LabelText = "LinkedIn"
				});
			}
			this.Controls.Add(this.SocialShare);
			this.Controls.Add(this.btnShare);
			this.Controls.Add(this.TitleHolder);
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000AEEE4 File Offset: 0x000AD0E4
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.owner.ResolvedRenderMode != RenderMode.Mobile)
			{
				this.RenderClassicMode(writer);
				return;
			}
			this.RenderMobileMode(writer);
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000AEF04 File Offset: 0x000AD104
		private void RenderClassicMode(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpTitleBar");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Wrapper");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderBeginTag(HtmlTextWriterTag.H4);
			this.TitleHolder.RenderControl(writer);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpButtSet");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.ShareButton.RenderControl(writer);
			if (this.OwnerMediaPlayer.IsPlayList)
			{
				if (this.OwnerMediaPlayer.PlaylistSettings.Position == MediaPlayerPlaylistPosition.VerticalInside)
				{
					this.RenderButton(writer, "OpenPlaylist", "Open Playlist", false);
					this.RenderButton(writer, "ClosePlaylist", "Close Playlist");
				}
				else
				{
					this.RenderButton(writer, "OpenPlaylist", "Open Playlist");
					this.RenderButton(writer, "ClosePlaylist", "Close Playlist", false);
				}
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpSocialShareBar");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.SocialShare.RenderControl(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000AF024 File Offset: 0x000AD224
		private void RenderMobileMode(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpSocialShare");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpSocialShareBox");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpSocialShareTitle");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("Share on");
			writer.RenderEndTag();
			this.SocialShare.RenderControl(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpTitleBar");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Wrapper");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderBeginTag(HtmlTextWriterTag.H4);
			this.TitleHolder.RenderControl(writer);
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpButtSet");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.ShareButton.RenderControl(writer);
			if (this.OwnerMediaPlayer.IsPlayList)
			{
				if (this.OwnerMediaPlayer.PlaylistSettings.Position == MediaPlayerPlaylistPosition.VerticalInside)
				{
					this.RenderButton(writer, "OpenPlaylist", "Open Playlist", false);
					this.RenderButton(writer, "ClosePlaylist", "Close Playlist");
				}
				else
				{
					this.RenderButton(writer, "OpenPlaylist", "Open Playlist");
					this.RenderButton(writer, "ClosePlaylist", "Close Playlist", false);
				}
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000AF190 File Offset: 0x000AD390
		private void RenderButton(HtmlTextWriter writer, string name, string text, bool visible)
		{
			if (!visible)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			writer.AddAttribute(HtmlTextWriterAttribute.Title, text);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpActionButton rmp" + name + "Button");
			writer.RenderBeginTag(HtmlTextWriterTag.Button);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmpIcon rmp" + name + "Icon");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000AF210 File Offset: 0x000AD410
		private void RenderButton(HtmlTextWriter writer, string name, string text)
		{
			this.RenderButton(writer, name, text, true);
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000AF21C File Offset: 0x000AD41C
		internal static HtmlGenericControl CreateButton(string name, string text)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("button");
			htmlGenericControl.Attributes.Add("title", text);
			htmlGenericControl.Attributes.Add("class", "rigActionButton rig" + name + "Button");
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
			htmlGenericControl2.Attributes.Add("class", "rigIcon rig" + name + "Icon");
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("span");
			htmlGenericControl3.Attributes.Add("class", "rigButtonText");
			htmlGenericControl3.InnerText = text;
			htmlGenericControl.Controls.Add(htmlGenericControl3);
			return htmlGenericControl;
		}

		// Token: 0x04000E53 RID: 3667
		private Literal titleHolder;

		// Token: 0x04000E54 RID: 3668
		private HtmlButton btnShare;

		// Token: 0x04000E55 RID: 3669
		private RadMediaPlayer owner;

		// Token: 0x04000E56 RID: 3670
		private RadSocialShare mpSocialShare;
	}
}
