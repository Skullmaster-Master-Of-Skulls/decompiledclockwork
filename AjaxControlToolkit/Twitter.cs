using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x020001AA RID: 426
	[ToolboxBitmap(typeof(Accessor), "Twitter.bmp")]
	[ClientCssResource("Twitter")]
	[Designer(typeof(TwitterDesigner))]
	public class Twitter : CompositeControl
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00020CA6 File Offset: 0x0001EEA6
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x00020CAE File Offset: 0x0001EEAE
		public TwitterMode Mode { get; set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x00020CB7 File Offset: 0x0001EEB7
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x00020CBF File Offset: 0x0001EEBF
		[Description("Twitter Screen Name used when Mode=Profile")]
		[Category("Profile")]
		public string ScreenName { get; set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00020CC8 File Offset: 0x0001EEC8
		// (set) Token: 0x06000C70 RID: 3184 RVA: 0x00020CD0 File Offset: 0x0001EED0
		[Description("Twitter Caption")]
		[Category("Search")]
		public string Caption { get; set; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00020CD9 File Offset: 0x0001EED9
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x00020CE1 File Offset: 0x0001EEE1
		[Description("Twitter Title")]
		[Category("Search")]
		public string Title { get; set; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00020CEA File Offset: 0x0001EEEA
		// (set) Token: 0x06000C74 RID: 3188 RVA: 0x00020CF2 File Offset: 0x0001EEF2
		[Description("Twitter Profile Image Url")]
		[Category("Search")]
		public string ProfileImageUrl { get; set; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00020CFB File Offset: 0x0001EEFB
		// (set) Token: 0x06000C76 RID: 3190 RVA: 0x00020D03 File Offset: 0x0001EF03
		public string Search { get; set; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00020D0C File Offset: 0x0001EF0C
		// (set) Token: 0x06000C78 RID: 3192 RVA: 0x00020D14 File Offset: 0x0001EF14
		public bool IncludeRetweets { get; set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00020D1D File Offset: 0x0001EF1D
		// (set) Token: 0x06000C7A RID: 3194 RVA: 0x00020D25 File Offset: 0x0001EF25
		public bool IncludeReplies { get; set; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00020D2E File Offset: 0x0001EF2E
		// (set) Token: 0x06000C7C RID: 3196 RVA: 0x00020D36 File Offset: 0x0001EF36
		public int Count { get; set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x00020D3F File Offset: 0x0001EF3F
		// (set) Token: 0x06000C7E RID: 3198 RVA: 0x00020D47 File Offset: 0x0001EF47
		public int CacheDuration { get; set; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00020D50 File Offset: 0x0001EF50
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x00020D58 File Offset: 0x0001EF58
		[Browsable(true)]
		[Description("Enable get live content from twitter server at design time")]
		public bool IsLiveContentOnDesignMode { get; set; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00020D61 File Offset: 0x0001EF61
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x00020D69 File Offset: 0x0001EF69
		[TemplateContainer(typeof(ListViewItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public ITemplate StatusTemplate { get; set; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00020D72 File Offset: 0x0001EF72
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x00020D7A File Offset: 0x0001EF7A
		[Browsable(false)]
		[TemplateContainer(typeof(ListViewItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate AlternatingStatusTemplate { get; set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00020D83 File Offset: 0x0001EF83
		// (set) Token: 0x06000C86 RID: 3206 RVA: 0x00020D8B File Offset: 0x0001EF8B
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(ListView))]
		public ITemplate EmptyDataTemplate { get; set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00020D94 File Offset: 0x0001EF94
		// (set) Token: 0x06000C88 RID: 3208 RVA: 0x00020D9C File Offset: 0x0001EF9C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[TemplateContainer(typeof(Twitter))]
		public ITemplate LayoutTemplate { get; set; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x00020DA5 File Offset: 0x0001EFA5
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00020DA9 File Offset: 0x0001EFA9
		public Twitter()
		{
			this.Mode = TwitterMode.Profile;
			this.CacheDuration = 300;
			this.Count = 5;
			this.CssClass = "ajax__twitter";
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00020DD5 File Offset: 0x0001EFD5
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ToolkitResourceManager.RegisterCssReferences(this);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00020DE4 File Offset: 0x0001EFE4
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.ControlPropertiesValid();
			IList<TwitterStatus> list = null;
			switch (this.Mode)
			{
			case TwitterMode.Profile:
				list = this.GetProfile();
				if (list != null && list.Count > 0)
				{
					TwitterUser user = list[0].User;
					this.Title = (this.Title ?? user.Name);
					this.Caption = (this.Caption ?? user.ScreenName);
					this.ProfileImageUrl = (this.ProfileImageUrl ?? user.ProfileImageUrl);
				}
				break;
			case TwitterMode.Search:
				list = this.GetSearch();
				break;
			}
			this._listView.DataSource = list;
			this._listView.DataBind();
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00020E9C File Offset: 0x0001F09C
		private void ControlPropertiesValid()
		{
			switch (this.Mode)
			{
			case TwitterMode.Profile:
				if (string.IsNullOrEmpty(this.ScreenName))
				{
					throw new HttpException("ScreenName must have a value");
				}
				break;
			case TwitterMode.Search:
				if (string.IsNullOrEmpty(this.Search))
				{
					throw new HttpException("Search must have a value");
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00020EEF File Offset: 0x0001F0EF
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			this._listView = new ListView();
			this.Controls.Add(this._listView);
			this.PrepareTemplates();
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00020F20 File Offset: 0x0001F120
		private void PrepareTemplates()
		{
			switch (this.Mode)
			{
			case TwitterMode.Profile:
				if (this.LayoutTemplate == null)
				{
					this.LayoutTemplate = new Twitter.DefaultProfileLayoutTemplate(this);
				}
				if (this.StatusTemplate == null)
				{
					this.StatusTemplate = new Twitter.DefaultProfileStatusTemplate(this);
				}
				break;
			case TwitterMode.Search:
				if (this.LayoutTemplate == null)
				{
					this.LayoutTemplate = new Twitter.DefaultSearchLayoutTemplate(this);
				}
				if (this.StatusTemplate == null)
				{
					this.StatusTemplate = new Twitter.DefaultSearchStatusTemplate(this);
				}
				break;
			}
			if (this.EmptyDataTemplate == null)
			{
				this.EmptyDataTemplate = new Twitter.DefaultEmptyDataTemplate();
			}
			this._listView.LayoutTemplate = this.LayoutTemplate;
			this._listView.ItemTemplate = this.StatusTemplate;
			this._listView.AlternatingItemTemplate = this.AlternatingStatusTemplate;
			this._listView.EmptyDataTemplate = this.EmptyDataTemplate;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00020FF0 File Offset: 0x0001F1F0
		private IList<TwitterStatus> GetProfile()
		{
			string key = string.Format("__TwitterProfile_{0}_{1}_{2}_{3}", new object[]
			{
				this.ScreenName,
				this.Count,
				this.IncludeRetweets,
				this.IncludeReplies
			});
			IList<TwitterStatus> list = (IList<TwitterStatus>)this.Context.Cache[key];
			if (list == null)
			{
				TwitterAPI twitterAPI = new TwitterAPI();
				try
				{
					list = twitterAPI.GetProfile(this.ScreenName, this.Count, this.IncludeRetweets, this.IncludeReplies);
				}
				catch
				{
					return null;
				}
				this.Context.Cache.Insert(key, list, null, DateTime.UtcNow.AddSeconds((double)this.CacheDuration), Cache.NoSlidingExpiration);
				return list;
			}
			return list;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x000210D0 File Offset: 0x0001F2D0
		private IList<TwitterStatus> GetSearch()
		{
			string key = string.Format("__TwitterSearch_{0}_{1}", this.Search, this.Count);
			IList<TwitterStatus> list = (IList<TwitterStatus>)this.Context.Cache[key];
			if (list == null)
			{
				TwitterAPI twitterAPI = new TwitterAPI();
				try
				{
					list = twitterAPI.GetSearch(this.Search, this.Count);
				}
				catch
				{
					return null;
				}
				this.Context.Cache.Insert(key, list, null, DateTime.UtcNow.AddSeconds((double)this.CacheDuration), Cache.NoSlidingExpiration);
				return list;
			}
			return list;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00021174 File Offset: 0x0001F374
		public static string Ago(DateTime date)
		{
			TimeSpan timeSpan = DateTime.Now - date;
			if (timeSpan.TotalMinutes < 1.0)
			{
				return "Less than a minute ago";
			}
			if (Math.Round(timeSpan.TotalHours) < 2.0)
			{
				return string.Format("{0} minutes ago", Math.Round(timeSpan.TotalMinutes));
			}
			if (Math.Round(timeSpan.TotalDays) < 2.0)
			{
				return string.Format("{0} hours ago", Math.Round(timeSpan.TotalHours));
			}
			return string.Format("{0} days ago", Math.Round(timeSpan.TotalDays));
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00021228 File Offset: 0x0001F428
		public static string ActivateLinks(string text)
		{
			string pattern = "(((http|https)+\\:\\/\\/)[&#95;.a-z0-9-]+\\.[a-z0-9\\/&#95;:@=.+?,##%&~-]*[^.|\\'|\\# |!|\\(|?|,| |>|<|;|\\)])";
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
			return regex.Replace(text, "<a href=\"$1\">$1</a>");
		}

		// Token: 0x0400048B RID: 1163
		private ListView _listView;

		// Token: 0x020001AB RID: 427
		internal sealed class DefaultProfileStatusTemplate : ITemplate
		{
			// Token: 0x06000C94 RID: 3220 RVA: 0x0002124F File Offset: 0x0001F44F
			internal DefaultProfileStatusTemplate(Twitter twitter)
			{
				this._twitter = twitter;
			}

			// Token: 0x06000C95 RID: 3221 RVA: 0x00021260 File Offset: 0x0001F460
			void ITemplate.InstantiateIn(Control container)
			{
				LiteralControl literalControl = new LiteralControl();
				literalControl.DataBinding += this.ctlStatus_DataBind;
				container.Controls.Add(literalControl);
			}

			// Token: 0x06000C96 RID: 3222 RVA: 0x00021294 File Offset: 0x0001F494
			private void ctlStatus_DataBind(object sender, EventArgs e)
			{
				LiteralControl literalControl = (LiteralControl)sender;
				ListViewDataItem listViewDataItem = (ListViewDataItem)literalControl.NamingContainer;
				TwitterStatus twitterStatus = (TwitterStatus)listViewDataItem.DataItem;
				literalControl.Text = string.Format("<li>{0}<br /><span class=\"ajax__twitter_createat\">{1}</span></li>", Twitter.ActivateLinks(twitterStatus.Text), Twitter.Ago(twitterStatus.CreatedAt));
			}

			// Token: 0x0400049B RID: 1179
			private Twitter _twitter;
		}

		// Token: 0x020001AC RID: 428
		internal sealed class DefaultProfileLayoutTemplate : ITemplate
		{
			// Token: 0x06000C97 RID: 3223 RVA: 0x000212E6 File Offset: 0x0001F4E6
			public DefaultProfileLayoutTemplate(Twitter twitter)
			{
				this._twitter = twitter;
			}

			// Token: 0x06000C98 RID: 3224 RVA: 0x000212F8 File Offset: 0x0001F4F8
			void ITemplate.InstantiateIn(Control container)
			{
				HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
				htmlGenericControl.Attributes.Add("class", "ajax__twitter_header");
				container.Controls.Add(htmlGenericControl);
				System.Web.UI.WebControls.Image child = new System.Web.UI.WebControls.Image
				{
					ImageUrl = this._twitter.ProfileImageUrl
				};
				htmlGenericControl.Controls.Add(child);
				HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("h3");
				htmlGenericControl2.Controls.Add(new LiteralControl(this._twitter.Title));
				htmlGenericControl.Controls.Add(htmlGenericControl2);
				HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("h4");
				htmlGenericControl3.Controls.Add(new LiteralControl(this._twitter.Caption));
				htmlGenericControl.Controls.Add(htmlGenericControl3);
				HtmlGenericControl htmlGenericControl4 = new HtmlGenericControl("ul");
				htmlGenericControl4.Attributes.Add("class", "ajax__twitter_itemlist");
				htmlGenericControl4.Style.Add("margin", "0px");
				container.Controls.Add(htmlGenericControl4);
				PlaceHolder placeHolder = new PlaceHolder();
				placeHolder.ID = "ItemPlaceholder";
				htmlGenericControl4.Controls.Add(placeHolder);
				HtmlGenericControl htmlGenericControl5 = new HtmlGenericControl("div");
				string imageHref = ToolkitResourceManager.GetImageHref("Twitter.24.png", this._twitter, true);
				htmlGenericControl5.Attributes.Add("class", "ajax__twitter_footer");
				htmlGenericControl5.Controls.Add(new System.Web.UI.WebControls.Image
				{
					ImageUrl = imageHref
				});
				container.Controls.Add(htmlGenericControl5);
			}

			// Token: 0x0400049C RID: 1180
			private Twitter _twitter;
		}

		// Token: 0x020001AD RID: 429
		internal sealed class DefaultSearchStatusTemplate : ITemplate
		{
			// Token: 0x06000C99 RID: 3225 RVA: 0x00021483 File Offset: 0x0001F683
			internal DefaultSearchStatusTemplate(Twitter twitter)
			{
				this._twitter = twitter;
			}

			// Token: 0x06000C9A RID: 3226 RVA: 0x00021494 File Offset: 0x0001F694
			void ITemplate.InstantiateIn(Control container)
			{
				LiteralControl literalControl = new LiteralControl();
				literalControl.DataBinding += this.ctlStatus_DataBind;
				container.Controls.Add(literalControl);
			}

			// Token: 0x06000C9B RID: 3227 RVA: 0x000214C8 File Offset: 0x0001F6C8
			private void ctlStatus_DataBind(object sender, EventArgs e)
			{
				LiteralControl literalControl = (LiteralControl)sender;
				ListViewDataItem listViewDataItem = (ListViewDataItem)literalControl.NamingContainer;
				TwitterStatus twitterStatus = (TwitterStatus)listViewDataItem.DataItem;
				literalControl.Text = string.Format("<li><img src=\"{0}\" /><div>{1}<br /><span class=\"ajax__twitter_createat\">{2}</span></div></li>", twitterStatus.User.ProfileImageUrl, twitterStatus.Text, Twitter.Ago(twitterStatus.CreatedAt));
			}

			// Token: 0x0400049D RID: 1181
			private Twitter _twitter;
		}

		// Token: 0x020001AE RID: 430
		internal sealed class DefaultSearchLayoutTemplate : ITemplate
		{
			// Token: 0x06000C9C RID: 3228 RVA: 0x00021520 File Offset: 0x0001F720
			public DefaultSearchLayoutTemplate(Twitter twitter)
			{
				this._twitter = twitter;
			}

			// Token: 0x06000C9D RID: 3229 RVA: 0x00021530 File Offset: 0x0001F730
			void ITemplate.InstantiateIn(Control container)
			{
				HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
				htmlGenericControl.Attributes.Add("class", "ajax__twitter_header");
				container.Controls.Add(htmlGenericControl);
				HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("h3");
				htmlGenericControl2.Controls.Add(new LiteralControl(this._twitter.Title));
				htmlGenericControl.Controls.Add(htmlGenericControl2);
				HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("h4");
				htmlGenericControl3.Controls.Add(new LiteralControl(this._twitter.Caption));
				htmlGenericControl.Controls.Add(htmlGenericControl3);
				HtmlGenericControl htmlGenericControl4 = new HtmlGenericControl("ul");
				htmlGenericControl4.Style.Add("margin", "0px");
				htmlGenericControl4.Attributes.Add("class", "ajax__twitter_itemlist");
				container.Controls.Add(htmlGenericControl4);
				PlaceHolder placeHolder = new PlaceHolder();
				placeHolder.ID = "ItemPlaceholder";
				htmlGenericControl4.Controls.Add(placeHolder);
				HtmlGenericControl htmlGenericControl5 = new HtmlGenericControl("div");
				string imageHref = ToolkitResourceManager.GetImageHref("Twitter.24.png", this._twitter, true);
				htmlGenericControl5.Attributes.Add("class", "ajax__twitter_footer");
				htmlGenericControl5.Controls.Add(new System.Web.UI.WebControls.Image
				{
					ImageUrl = imageHref
				});
				container.Controls.Add(htmlGenericControl5);
			}

			// Token: 0x0400049E RID: 1182
			private Twitter _twitter;
		}

		// Token: 0x020001AF RID: 431
		internal sealed class DefaultEmptyDataTemplate : ITemplate
		{
			// Token: 0x06000C9E RID: 3230 RVA: 0x0002168E File Offset: 0x0001F88E
			void ITemplate.InstantiateIn(Control container)
			{
				container.Controls.Add(new LiteralControl("There are no matching tweets."));
			}
		}
	}
}
