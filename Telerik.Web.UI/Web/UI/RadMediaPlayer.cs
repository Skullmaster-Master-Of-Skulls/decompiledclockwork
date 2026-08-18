using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x020005CF RID: 1487
	[EmbeddedSkin("MediaPlayer", "Default")]
	[Description("Telerik RadMediaPlayer")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("MediaPlayer")]
	[ToolboxBitmap(typeof(RadMediaPlayer), "Telerik.Web.UI.MediaPlayer.png")]
	[ToolboxData("<{0}:RadMediaPlayer runat=\"server\"></{0}:RadMediaPlayer>")]
	[TelerikToolboxCategory("Data")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadGrid))]
	[AdaptiveRendering]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadMediaPlayer))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadMediaPlayer))]
	[LightweightRendering]
	[RequiredScript(typeof(MaterialRipple))]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(AnimationFramework), 10)]
	[ClientScriptResource("Telerik.Web.UI.RadMediaPlayer", "Telerik.Web.UI.MediaPlayer.RadMediaPlayerScripts.js")]
	[Designer("Telerik.Web.Design.RadMediaPlayerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadMediaPlayer : RadWebControl, INamingContainer, IPostBackEventHandler, ILocalizableControl
	{
		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x000B0193 File Offset: 0x000AE393
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x06003536 RID: 13622 RVA: 0x000B0197 File Offset: 0x000AE397
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x000B019A File Offset: 0x000AE39A
		internal bool IsPlayList
		{
			get
			{
				return this.Playlist.Count > 0 || !string.IsNullOrEmpty(this.PlaylistSettings.YouTubePlaylist);
			}
		}

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x06003538 RID: 13624 RVA: 0x000B01BF File Offset: 0x000AE3BF
		// (set) Token: 0x06003539 RID: 13625 RVA: 0x000B01C8 File Offset: 0x000AE3C8
		public override string Skin
		{
			get
			{
				return base.Skin;
			}
			set
			{
				string skin = (value == "Sitefinity") ? "Default" : value;
				base.Skin = skin;
			}
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x0600353A RID: 13626 RVA: 0x000B01F2 File Offset: 0x000AE3F2
		// (set) Token: 0x0600353B RID: 13627 RVA: 0x000B0212 File Offset: 0x000AE412
		[Description("The JavaScript function executed when the media has loaded its meta data")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("ready")]
		public string OnClientReady
		{
			get
			{
				return (string)(this.ViewState["OnClientReady"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientReady"] = value;
			}
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x0600353C RID: 13628 RVA: 0x000B0225 File Offset: 0x000AE425
		// (set) Token: 0x0600353D RID: 13629 RVA: 0x000B0245 File Offset: 0x000AE445
		[ClientPropertyName("play")]
		[Category("Client-side events")]
		[Description("The JavaScript function executed when the media has started playing")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientPlay
		{
			get
			{
				return (string)(this.ViewState["OnClientPlay"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientPlay"] = value;
			}
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x0600353E RID: 13630 RVA: 0x000B0258 File Offset: 0x000AE458
		// (set) Token: 0x0600353F RID: 13631 RVA: 0x000B0278 File Offset: 0x000AE478
		[Category("Client-side events")]
		[Description("The JavaScript function executed when the media has reached the end")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("ended")]
		public string OnClientEnded
		{
			get
			{
				return (string)(this.ViewState["OnClientEnded"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientEnded"] = value;
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x06003540 RID: 13632 RVA: 0x000B028B File Offset: 0x000AE48B
		// (set) Token: 0x06003541 RID: 13633 RVA: 0x000B02AB File Offset: 0x000AE4AB
		[DefaultValue("")]
		[Description("The JavaScript function executed when the media has been paused")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("paused")]
		public string OnClientPaused
		{
			get
			{
				return (string)(this.ViewState["OnClientPaused"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientPaused"] = value;
			}
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x06003542 RID: 13634 RVA: 0x000B02BE File Offset: 0x000AE4BE
		// (set) Token: 0x06003543 RID: 13635 RVA: 0x000B02DE File Offset: 0x000AE4DE
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientPropertyName("volumeChanged")]
		[Description("The JavaScript function executed when the volume of the media has been changed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientVolumeChanged
		{
			get
			{
				return (string)(this.ViewState["OnClientVolumeChanged"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientVolumeChanged"] = value;
			}
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06003544 RID: 13636 RVA: 0x000B02F1 File Offset: 0x000AE4F1
		// (set) Token: 0x06003545 RID: 13637 RVA: 0x000B0311 File Offset: 0x000AE511
		[Description("The JavaScript function executed the player is seeking new time position into the media.")]
		[Category("Client-side events")]
		[ClientPropertyName("seekStart")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientSeekStart
		{
			get
			{
				return (string)(this.ViewState["OnClientSeekStart"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientSeekStart"] = value;
			}
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000B0324 File Offset: 0x000AE524
		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			IEnumerable<ScriptReference> scriptReferences = base.GetScriptReferences();
			List<ScriptReference> list = new List<ScriptReference>();
			foreach (ScriptReference item in scriptReferences)
			{
				list.Add(item);
			}
			if (this.ResolvedRenderMode == RenderMode.Mobile)
			{
				list.Add(new ScriptReference("Telerik.Web.UI.Common.TouchScrollExtender.js", Assembly.GetExecutingAssembly().FullName));
			}
			return list;
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000B03A0 File Offset: 0x000AE5A0
		internal void HandleChildControlsPreRender(object sender, EventArgs e)
		{
			ISkinnableControl skinnableControl = sender as ISkinnableControl;
			if (this.ResolvedRenderMode == RenderMode.Mobile && skinnableControl is RadSocialShare)
			{
				return;
			}
			skinnableControl.Skin = base.RuntimeSkin;
			skinnableControl.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000B03E0 File Offset: 0x000AE5E0
		protected override void CreateChildControls()
		{
			this.Controls.Clear();
			this.rlpLoadingIndicator = new RadAjaxLoadingPanel
			{
				ID = "LoadingIndicator",
				BackgroundTransparency = 100
			};
			this.rlpLoadingIndicator.PreRender += this.HandleChildControlsPreRender;
			this.mpToolBar = new MediaPlayerToolbar(this);
			this.mpTitleBar = new MediaPlayerTitlebar(this);
			Panel panel = new Panel
			{
				ID = "Overlay"
			};
			panel.CssClass = "rmpOverlay";
			panel.Style.Add(HtmlTextWriterStyle.Position, "absolute");
			panel.Style.Add(HtmlTextWriterStyle.Height, "100%");
			panel.Style.Add(HtmlTextWriterStyle.Width, "100%");
			Panel panel2 = new Panel
			{
				CssClass = "rmpSubtitles"
			};
			Label label = new Label
			{
				CssClass = "rmpSubtitlesInner"
			};
			label.Style.Add(HtmlTextWriterStyle.Display, "none");
			panel2.Controls.Add(label);
			this.Controls.Add(panel2);
			this.Controls.Add(panel);
			this.Controls.Add(new MediaPlayerBannerStructureControl(this));
			this.Controls.Add(this.ToolBar);
			this.Controls.Add(this.rlpLoadingIndicator);
			if (this.IsPlayList)
			{
				this.CreatePlaylist();
			}
			this.Controls.Add(this.TitleBar);
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000B0554 File Offset: 0x000AE754
		private Dictionary<string, object> GetYoutubePlaylist(string playlistId, string pageToken, string key)
		{
			Dictionary<string, object> result = null;
			if (!string.IsNullOrEmpty(pageToken))
			{
				pageToken = "&pageToken=" + pageToken;
			}
			if (!string.IsNullOrEmpty(key))
			{
				key = "&key=" + key;
			}
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(string.Concat(new string[]
			{
				"https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&maxResults=50",
				key,
				"&playlistId=",
				playlistId,
				pageToken
			}));
			httpWebRequest.Method = "GET";
			try
			{
				using (WebResponse response = httpWebRequest.GetResponse())
				{
					using (Stream responseStream = response.GetResponseStream())
					{
						StreamReader streamReader = new StreamReader(responseStream);
						string input = streamReader.ReadToEnd();
						JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
						result = (Dictionary<string, object>)javaScriptSerializer.DeserializeObject(input);
					}
				}
			}
			catch (Exception)
			{
				return null;
			}
			return result;
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000B0654 File Offset: 0x000AE854
		private List<int> GetYoutubeVideosDuration(List<string> videoIds, string key)
		{
			if (videoIds.Count == 0)
			{
				return new List<int>();
			}
			List<int> list = new List<int>(videoIds.Count);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("https://www.googleapis.com/youtube/v3/videos?part=contentDetails&fields=items/contentDetails/duration&maxResult=50");
			if (!string.IsNullOrEmpty(key))
			{
				stringBuilder.Append("&key=" + key);
			}
			stringBuilder.Append("&id=");
			stringBuilder.Append(videoIds[0]);
			for (int i = 1; i < videoIds.Count; i++)
			{
				stringBuilder.Append("," + videoIds[i]);
			}
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(stringBuilder.ToString());
			httpWebRequest.Method = "GET";
			using (WebResponse response = httpWebRequest.GetResponse())
			{
				using (Stream responseStream = response.GetResponseStream())
				{
					StreamReader streamReader = new StreamReader(responseStream);
					string input = streamReader.ReadToEnd();
					JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
					Dictionary<string, object> dictionary = (Dictionary<string, object>)javaScriptSerializer.DeserializeObject(input);
					object[] array = dictionary["items"] as object[];
					foreach (object obj in array)
					{
						string s = ((obj as Dictionary<string, object>)["contentDetails"] as Dictionary<string, object>)["duration"].ToString();
						list.Add(Convert.ToInt32(XmlConvert.ToTimeSpan(s).TotalSeconds));
					}
				}
			}
			return list;
		}

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x0600354B RID: 13643 RVA: 0x000B07EC File Offset: 0x000AE9EC
		private string YoutubeApiKeyResolved
		{
			get
			{
				string text = this.YoutubeApiKey;
				if (string.IsNullOrEmpty(text))
				{
					text = ConfigurationManager.AppSettings["YoutubeApiKey"];
				}
				return text;
			}
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000B082C File Offset: 0x000AEA2C
		private void CreatePlaylist()
		{
			RadListView radListView = new RadListView
			{
				ID = "Playlist"
			};
			radListView.LayoutTemplate = new MediaPlayerPlaylistLayoutTemplate(this, radListView);
			radListView.ItemTemplate = new MediaPlayerPlaylistItemTemplate(this);
			string youtubeApiKeyResolved = this.YoutubeApiKeyResolved;
			if (!string.IsNullOrEmpty(this.PlaylistSettings.YouTubePlaylist))
			{
				this.Playlist.Clear();
				int num = 0;
				string text = "";
				do
				{
					Dictionary<string, object> youtubePlaylist = this.GetYoutubePlaylist(this.playlistSettings.YouTubePlaylist, text, youtubeApiKeyResolved);
					if (youtubePlaylist == null || youtubePlaylist.Count == 0)
					{
						break;
					}
					object[] array = youtubePlaylist["items"] as object[];
					List<string> list = new List<string>(50);
					foreach (object obj in array)
					{
						Dictionary<string, object> dictionary = (obj as Dictionary<string, object>)["snippet"] as Dictionary<string, object>;
						string poster = "";
						Dictionary<string, object> dictionary2 = dictionary["resourceId"] as Dictionary<string, object>;
						if (dictionary.ContainsKey("thumbnails"))
						{
							Dictionary<string, object> dictionary3 = dictionary["thumbnails"] as Dictionary<string, object>;
							Dictionary<string, object> dictionary4 = dictionary3["default"] as Dictionary<string, object>;
							poster = dictionary4["url"].ToString();
						}
						string text2 = dictionary2["videoId"].ToString();
						MediaPlayerVideoFile item = new MediaPlayerVideoFile
						{
							AutoPlay = this.AutoPlay,
							Path = "https://www.youtube.com/watch?v=" + text2,
							Poster = poster,
							Title = dictionary["title"].ToString()
						};
						this.Playlist.Add(item);
						list.Add(text2);
					}
					List<int> youtubeVideosDuration = this.GetYoutubeVideosDuration(list, youtubeApiKeyResolved);
					foreach (int duration in youtubeVideosDuration)
					{
						this.Playlist[num].Duration = duration;
						num++;
					}
					text = (youtubePlaylist.ContainsKey("nextPageToken") ? youtubePlaylist["nextPageToken"].ToString() : "");
				}
				while (!string.IsNullOrEmpty(text));
			}
			radListView.NeedDataSource += delegate(object sender, RadListViewNeedDataSourceEventArgs args)
			{
				((RadListView)sender).DataSource = this.Playlist;
			};
			this.Controls.Add(radListView);
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x0600354D RID: 13645 RVA: 0x000B0A9C File Offset: 0x000AEC9C
		protected override string CssClassFormatString
		{
			get
			{
				string text = "RadMediaPlayer RadMediaPlayer_{0}";
				if (this.ToolbarDocked)
				{
					text += " rmpToolbarDocked";
				}
				return text;
			}
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x0600354E RID: 13646 RVA: 0x000B0AC4 File Offset: 0x000AECC4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		internal MediaPlayerStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new MediaPlayerStrings(new LocalizationProvider("RadMediaPlayer.Main", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000B0B04 File Offset: 0x000AED04
		internal MediaPlayerFile CreateFileByType(string fileType)
		{
			MediaPlayerFile result;
			if (fileType.IndexOf("MediaPlayerVideoFile", StringComparison.CurrentCulture) > -1)
			{
				result = new MediaPlayerVideoFile();
			}
			else if (fileType.IndexOf("MediaPlayerAudioFile", StringComparison.CurrentCulture) > -1)
			{
				result = new MediaPlayerAudioFile();
			}
			else
			{
				result = new MediaPlayerAudioFile();
			}
			return result;
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000B0B48 File Offset: 0x000AED48
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeClientProperties(descriptor);
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (this.EnableAriaSupport)
			{
				dictionary.Add("_enableAriaSupport", this.EnableAriaSupport);
			}
			dictionary.Add("uniqueID", this.UniqueID);
			dictionary.Add("hdActive", this.HDActive);
			dictionary.Add("fsActive", this.FullScreen);
			dictionary.Add("startVolume", this.StartVolume);
			dictionary.Add("muted", this.Muted);
			dictionary.Add("toolbarDocked", this.ToolbarDocked);
			dictionary.Add("volumeButtonToolTip", this.VolumeButtonToolTip);
			dictionary.Add("fullScreenButtonToolTip", this.FullScreenButtonToolTip);
			dictionary.Add("hdButtonToolTip", this.HDButtonToolTip);
			descriptor.AddProperty("_renderMode", this.ResolvedRenderMode);
			dictionary.Add("_flashModuleUrl", this.Page.ClientScript.GetWebResourceUrl(typeof(RadMediaPlayer), "Telerik.Web.UI.MediaPlayer.FlashMediaPlayer.swf"));
			dictionary.Add("playButtonToolTip", this.PlayButtonToolTip);
			dictionary.Add("pauseButtonToolTip", this.PauseButtonToolTip);
			if (this.IsPlayList)
			{
				if (!string.IsNullOrEmpty(this.PlaylistSettings.YouTubePlaylist))
				{
					dictionary.Add("youTubePlaylist", this.PlaylistSettings.YouTubePlaylist);
				}
				if (this.playlistSettings.SelectedIndex != 0)
				{
					dictionary.Add("selectedIndex", this.PlaylistSettings.SelectedIndex);
				}
				dictionary.Add("playlistPosition", this.PlaylistSettings.Position.ToString());
				dictionary.Add("playlistButtonsTrigger", this.PlaylistSettings.ButtonsTrigger.ToString());
			}
			descriptor.AddProperty("options", dictionary);
			this.DescribeMediaFiles(descriptor);
			this.DescribeBanners(descriptor);
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000B0D4C File Offset: 0x000AEF4C
		private void DescribeBanners(IScriptDescriptor descriptor)
		{
			if (this.Banners.Count > 0)
			{
				List<IDictionary> list = new List<IDictionary>();
				foreach (object obj in this.Banners)
				{
					MediaPlayerBanner mediaPlayerBanner = (MediaPlayerBanner)obj;
					list.Add(mediaPlayerBanner.Describe(this.Page));
				}
				descriptor.AddProperty("_banners", list);
			}
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000B0DD0 File Offset: 0x000AEFD0
		private void DescribeMediaFiles(IScriptDescriptor descriptor)
		{
			if (this.IsPlayList)
			{
				using (IEnumerator<MediaPlayerFile> enumerator = this.Playlist.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MediaPlayerFile mediaFile = enumerator.Current;
						this.mediaFilesData.Add(this.DescribeOptions(mediaFile));
					}
					goto IL_102;
				}
			}
			MediaPlayerFile mediaPlayerFile = new MediaPlayerVideoFile();
			if (this.HDActive)
			{
				mediaPlayerFile.Path = this.HDSource;
			}
			else
			{
				mediaPlayerFile.Path = this.Source;
			}
			mediaPlayerFile.SubtitlesPath = this.Subtitles;
			mediaPlayerFile.AutoPlay = this.AutoPlay;
			mediaPlayerFile.StartTime = this.StartTime;
			mediaPlayerFile.StartVolume = this.StartVolume;
			mediaPlayerFile.Poster = this.Poster;
			mediaPlayerFile.Title = this.Title;
			foreach (MediaPlayerSource item in this.Sources)
			{
				mediaPlayerFile.Sources.Add(item);
			}
			this.mediaFilesData.Add(this.DescribeOptions(mediaPlayerFile));
			IL_102:
			descriptor.AddProperty("_mediaFilesData", this.mediaFilesData);
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000B0F0C File Offset: 0x000AF10C
		internal string LoadTextFileContent(string fileName)
		{
			string result = "";
			try
			{
				result = File.ReadAllText(this.Context.Server.MapPath(fileName));
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000B0F4C File Offset: 0x000AF14C
		internal string ResolveClientUrlIfNeeded(string URL)
		{
			if (!string.IsNullOrEmpty(URL))
			{
				try
				{
					if (VirtualPathUtility.IsAppRelative(URL))
					{
						return this.Page.ResolveClientUrl(URL);
					}
				}
				catch (Exception)
				{
					return URL;
				}
				return URL;
			}
			return "";
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000B0FB4 File Offset: 0x000AF1B4
		private IDictionary DescribeOptions(MediaPlayerFile mediaFile)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("startVolume", mediaFile.StartVolume);
			dictionary.Add("startTime", mediaFile.StartTime);
			dictionary.Add("path", this.ResolveClientUrlIfNeeded(mediaFile.Path));
			dictionary.Add("title", mediaFile.Title);
			if (mediaFile.IsAutoPlaySet)
			{
				dictionary.Add("autoPlay", mediaFile.AutoPlay);
			}
			else
			{
				dictionary.Add("autoPlay", this.AutoPlay);
			}
			if (!this.AutoPlay)
			{
				dictionary.Add("poster", this.ResolveClientUrlIfNeeded(mediaFile.Poster));
			}
			if (mediaFile.Duration != 0)
			{
				dictionary.Add("duration", mediaFile.Duration);
			}
			List<IDictionary> list = new List<IDictionary>();
			string value = this.LoadTextFileContent(mediaFile.SubtitlesPath);
			if (!this.IsPlayList)
			{
				if (!string.IsNullOrEmpty(this.Source))
				{
					Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
					dictionary2.Add("path", this.ResolveClientUrlIfNeeded(this.Source));
					if (!string.IsNullOrEmpty(this.MimeType))
					{
						dictionary2.Add("mimeType", this.MimeType);
					}
					list.Add(dictionary2);
				}
				if (!string.IsNullOrEmpty(this.Subtitles) && string.IsNullOrEmpty(value))
				{
					value = this.LoadTextFileContent(this.Subtitles);
				}
			}
			if (!string.IsNullOrEmpty(value))
			{
				dictionary.Add("subtitlesData", value);
			}
			bool flag = (from source in mediaFile.Sources
			where source.IsHD
			select source).Count<MediaPlayerSource>() > 0;
			MediaPlayerVideoFile mediaPlayerVideoFile = mediaFile as MediaPlayerVideoFile;
			if (mediaPlayerVideoFile != null && !flag)
			{
				flag = (mediaPlayerVideoFile.HDSources.Count > 0 || !string.IsNullOrEmpty(mediaPlayerVideoFile.HDPath));
			}
			if (!this.HDActive || !flag)
			{
				using (IEnumerator<MediaPlayerSource> enumerator = (from source in mediaFile.Sources
				where !source.IsHD
				select source).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MediaPlayerSource mediaPlayerSource = enumerator.Current;
						Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
						dictionary3.Add("path", this.ResolveClientUrlIfNeeded(mediaPlayerSource.Path));
						if (!string.IsNullOrEmpty(mediaPlayerSource.MimeType))
						{
							dictionary3.Add("mimeType", mediaPlayerSource.MimeType);
						}
						list.Add(dictionary3);
					}
					goto IL_38B;
				}
			}
			if (mediaPlayerVideoFile != null)
			{
				List<MediaPlayerSource> list2 = (from source in mediaFile.Sources
				where source.IsHD
				select source).ToList<MediaPlayerSource>();
				if (list2.Count == 0)
				{
					list2 = mediaPlayerVideoFile.HDSources.ToList<MediaPlayerSource>();
					if (list2.Count == 0 && !string.IsNullOrEmpty(mediaPlayerVideoFile.HDPath))
					{
						list2.Add(new MediaPlayerSource
						{
							IsHD = true,
							MimeType = this.MimeType,
							Path = mediaPlayerVideoFile.HDPath
						});
					}
				}
				foreach (MediaPlayerSource mediaPlayerSource2 in list2)
				{
					Dictionary<string, object> dictionary4 = new Dictionary<string, object>();
					dictionary4.Add("path", this.ResolveClientUrlIfNeeded(mediaPlayerSource2.Path));
					if (!string.IsNullOrEmpty(mediaPlayerSource2.MimeType))
					{
						dictionary4.Add("mimeType", mediaPlayerSource2.MimeType);
					}
					list.Add(dictionary4);
				}
			}
			IL_38B:
			dictionary.Add("sources", list);
			return dictionary;
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000B1378 File Offset: 0x000AF578
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "ready", this.OnClientReady);
			RadWebControl.DescribeEvent(descriptor, "play", this.OnClientPlay);
			RadWebControl.DescribeEvent(descriptor, "ended", this.OnClientEnded);
			RadWebControl.DescribeEvent(descriptor, "paused", this.OnClientPaused);
			RadWebControl.DescribeEvent(descriptor, "volumeChanged", this.OnClientVolumeChanged);
			RadWebControl.DescribeEvent(descriptor, "seekStart", this.OnClientSeekStart);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000B13F4 File Offset: 0x000AF5F4
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			if (clientState.ContainsKey("currentTime") && clientState["currentTime"] != null)
			{
				this.StartTime = (double)Convert.ToInt32(clientState["currentTime"]);
			}
			if (clientState.ContainsKey("volume") && clientState["volume"] != null)
			{
				this.StartVolume = Convert.ToInt32(clientState["volume"]);
			}
			if (clientState.ContainsKey("playing") && clientState["playing"] != null)
			{
				this.AutoPlay = Convert.ToBoolean(clientState["playing"]);
			}
			if (clientState.ContainsKey("fsActive") && clientState["fsActive"] != null)
			{
				this.FullScreen = Convert.ToBoolean(clientState["fsActive"]);
			}
			if (clientState.ContainsKey("muted") && clientState["muted"] != null)
			{
				this.Muted = Convert.ToBoolean(clientState["muted"]);
			}
			if (clientState.ContainsKey("selectedIndex") && clientState.ContainsKey("selectedIndex"))
			{
				this.PlaylistSettings.SelectedIndex = (int)clientState["selectedIndex"];
			}
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000B1527 File Offset: 0x000AF727
		protected override void RaisePostDataChangedEvent()
		{
			base.RaisePostDataChangedEvent();
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x000B1530 File Offset: 0x000AF730
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(base.SaveViewState());
			if (this.IsPlayList)
			{
				arrayList.Add(((IStateManager)this.Playlist).SaveViewState());
				arrayList.Add(((IStateManager)this.PlaylistSettings).SaveViewState());
			}
			else
			{
				arrayList.Add(((IStateManager)this.Sources).SaveViewState());
			}
			return arrayList.ToArray();
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000B1598 File Offset: 0x000AF798
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int num = 0;
				base.LoadViewState(array[num++]);
				if (this.IsPlayList)
				{
					((IStateManager)this.Playlist).LoadViewState(array[num++]);
					((IStateManager)this.PlaylistSettings).LoadViewState(array[num++]);
					return;
				}
				((IStateManager)this.Sources).LoadViewState(array[num++]);
			}
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000B1600 File Offset: 0x000AF800
		protected override void TrackViewState()
		{
			if (base.IsTrackingViewState)
			{
				base.TrackViewState();
				return;
			}
			base.TrackViewState();
			if (this.IsPlayList)
			{
				((IStateManager)this.Playlist).TrackViewState();
				((IStateManager)this.PlaylistSettings).TrackViewState();
				return;
			}
			((IStateManager)this.Sources).TrackViewState();
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000B164C File Offset: 0x000AF84C
		protected override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				if (base.DesignMode)
				{
					this.RenderDesignTimeHtml(writer);
					return;
				}
			}
			else
			{
				base.Render(writer);
			}
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000B166D File Offset: 0x000AF86D
		private void RenderDesignTimeHtml(HtmlTextWriter writer)
		{
			writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			writer.Write(string.Format("\r\n\t\t<div class='RadMediaPlayer RadMediaPlayer_{0}' style='width:300px; height:200px'>\r\n\t\t\t<div class='rmpTitleBar'>\r\n\t\t\t\t<h4>{1}</h4>\r\n\t\t\t\t<div class='rmpButtSet'>\r\n\t\t\t\t\t<button type='button' class='rmpActionButton rmpShareButton'>\r\n\t\t\t\t\t\t<span class='rmpIcon rmpShareIcon'></span>\r\n\t\t\t\t\t</button>\r\n\t\t\t\t</div>\r\n\t\t\t</div>\r\n\r\n\t\t\t<div class='rmpToolbarWrapper rmpToolbarDocked'>\r\n\t\t\t\t<div class='rmpToolbar'>\r\n\t\t\t\t\t<div class='rmpLeftControlsSet'>\r\n\t\t\t\t\t\t<button type='button' onclick='playPause(this)' class='rmpActionButton rmpPlayButton'>\r\n\t\t\t\t\t\t\t<span class='rmpIcon rmpPlayIcon'></span>\r\n\t\t\t\t\t\t</button>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t\t<div class='rmpRightControlsSet'>\r\n\t\t\t\t\t\t<span class='rmpProgressText'>\r\n\t\t\t\t\t\t\t<span class='rmpCurrentTime'>0:00</span> / <span class='rmpDurationTime'>0:00</span>\r\n\t\t\t\t\t\t</span>\r\n\t\t\t\t\t\t<button type='button' onclick='hdControl(this)' class='rmpActionButton rmpHDButton'>\r\n\t\t\t\t\t\t\t<span class='rmpIcon rmpHDIcon'></span>\r\n\t\t\t\t\t\t</button>\r\n\r\n\t\t\t\t\t\t<button type='button' onclick='fullScr(this)' class='rmpActionButton rmpFullScrButton'>\r\n\t\t\t\t\t\t\t<span class='rmpIcon rmpFullScrIcon'></span>\r\n\t\t\t\t\t\t</button>\r\n\t\t\t\t\t</div>\r\n\t\t\t\t</div>\r\n\t\t\t</div>\r\n\t\t</div>", this.Skin, this.Title));
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000B1698 File Offset: 0x000AF898
		public void RaisePostBackEvent(string eventArgument)
		{
			bool hdactive;
			if (bool.TryParse(eventArgument, out hdactive))
			{
				this.HDActive = hdactive;
			}
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x0600355F RID: 13663 RVA: 0x000B16B8 File Offset: 0x000AF8B8
		// (set) Token: 0x06003560 RID: 13664 RVA: 0x000B16F4 File Offset: 0x000AF8F4
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Behavior")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		public string Source
		{
			get
			{
				string result = string.Empty;
				if (this.ViewState["Source"] != null)
				{
					result = this.ViewState["Source"].ToString();
				}
				return result;
			}
			set
			{
				this.ViewState["Source"] = value;
			}
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x000B1708 File Offset: 0x000AF908
		// (set) Token: 0x06003562 RID: 13666 RVA: 0x000B1744 File Offset: 0x000AF944
		[NotifyParentProperty(true)]
		[Description("Youtube API key, can be optained from console.developers.google.com")]
		[Category("Behavior")]
		[DefaultValue("")]
		public string YoutubeApiKey
		{
			get
			{
				string result = string.Empty;
				if (this.ViewState["YTAPIKEY"] != null)
				{
					result = this.ViewState["YTAPIKEY"].ToString();
				}
				return result;
			}
			set
			{
				this.ViewState["YTAPIKEY"] = value;
			}
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06003563 RID: 13667 RVA: 0x000B1758 File Offset: 0x000AF958
		// (set) Token: 0x06003564 RID: 13668 RVA: 0x000B1781 File Offset: 0x000AF981
		[Description("Determines if the control will have WAI-ARIA support enabled")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool EnableAriaSupport
		{
			get
			{
				object obj = this.ViewState["EnableAriaSupport"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06003565 RID: 13669 RVA: 0x000B179C File Offset: 0x000AF99C
		// (set) Token: 0x06003566 RID: 13670 RVA: 0x000B17D8 File Offset: 0x000AF9D8
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Behavior")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		public string HDSource
		{
			get
			{
				string result = string.Empty;
				if (this.ViewState["HDPath"] != null)
				{
					result = this.ViewState["HDPath"].ToString();
				}
				return result;
			}
			set
			{
				this.ViewState["HDPath"] = value;
			}
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06003567 RID: 13671 RVA: 0x000B17EC File Offset: 0x000AF9EC
		// (set) Token: 0x06003568 RID: 13672 RVA: 0x000B1828 File Offset: 0x000AFA28
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		public string Subtitles
		{
			get
			{
				string result = string.Empty;
				if (this.ViewState["Subtitles"] != null)
				{
					result = this.ViewState["Subtitles"].ToString();
				}
				return result;
			}
			set
			{
				this.ViewState["Subtitles"] = value;
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06003569 RID: 13673 RVA: 0x000B183C File Offset: 0x000AFA3C
		// (set) Token: 0x0600356A RID: 13674 RVA: 0x000B1878 File Offset: 0x000AFA78
		[DefaultValue("")]
		[Category("Behavior")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		[NotifyParentProperty(true)]
		public string MimeType
		{
			get
			{
				string result = string.Empty;
				if (this.ViewState["MimeType"] != null)
				{
					result = this.ViewState["MimeType"].ToString();
				}
				return result;
			}
			set
			{
				this.ViewState["MimeType"] = value;
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x0600356B RID: 13675 RVA: 0x000B188C File Offset: 0x000AFA8C
		// (set) Token: 0x0600356C RID: 13676 RVA: 0x000B18C8 File Offset: 0x000AFAC8
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public string Poster
		{
			get
			{
				string result = string.Empty;
				if (this.ViewState["Poster"] != null)
				{
					result = this.ViewState["Poster"].ToString();
				}
				return result;
			}
			set
			{
				this.ViewState["Poster"] = value;
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x0600356D RID: 13677 RVA: 0x000B18DC File Offset: 0x000AFADC
		// (set) Token: 0x0600356E RID: 13678 RVA: 0x000B191E File Offset: 0x000AFB1E
		[DefaultValue("")]
		public string Title
		{
			get
			{
				string result = this.Localization.Title;
				if (this.ViewState["Title"] != null)
				{
					result = this.ViewState["Title"].ToString();
				}
				return result;
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x0600356F RID: 13679 RVA: 0x000B1934 File Offset: 0x000AFB34
		// (set) Token: 0x06003570 RID: 13680 RVA: 0x000B196D File Offset: 0x000AFB6D
		[DefaultValue(50)]
		public int StartVolume
		{
			get
			{
				int result = 50;
				if (this.ViewState["StartVolume"] != null)
				{
					result = Convert.ToInt32(this.ViewState["StartVolume"]);
				}
				return result;
			}
			set
			{
				this.ViewState["StartVolume"] = value;
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06003571 RID: 13681 RVA: 0x000B1988 File Offset: 0x000AFB88
		// (set) Token: 0x06003572 RID: 13682 RVA: 0x000B19C8 File Offset: 0x000AFBC8
		[DefaultValue(0.0)]
		public double StartTime
		{
			get
			{
				double result = 0.0;
				if (this.ViewState["StartTime"] != null)
				{
					result = Convert.ToDouble(this.ViewState["StartTime"]);
				}
				return result;
			}
			set
			{
				this.ViewState["StartTime"] = value;
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06003573 RID: 13683 RVA: 0x000B19E0 File Offset: 0x000AFBE0
		// (set) Token: 0x06003574 RID: 13684 RVA: 0x000B1A18 File Offset: 0x000AFC18
		[DefaultValue(false)]
		public bool AutoPlay
		{
			get
			{
				bool result = false;
				if (this.ViewState["AutoPlay"] != null)
				{
					result = Convert.ToBoolean(this.ViewState["AutoPlay"]);
				}
				return result;
			}
			set
			{
				this.ViewState["AutoPlay"] = value;
			}
		}

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06003575 RID: 13685 RVA: 0x000B1A30 File Offset: 0x000AFC30
		// (set) Token: 0x06003576 RID: 13686 RVA: 0x000B1A68 File Offset: 0x000AFC68
		[DefaultValue(false)]
		public bool HDActive
		{
			get
			{
				bool result = false;
				if (this.ViewState["HDActive"] != null)
				{
					result = (bool)this.ViewState["HDActive"];
				}
				return result;
			}
			set
			{
				this.ViewState["HDActive"] = value;
			}
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x000B1A80 File Offset: 0x000AFC80
		// (set) Token: 0x06003578 RID: 13688 RVA: 0x000B1AB8 File Offset: 0x000AFCB8
		[DefaultValue(false)]
		public bool FullScreen
		{
			get
			{
				bool result = false;
				if (this.ViewState["fsActive"] != null)
				{
					result = (bool)this.ViewState["fsActive"];
				}
				return result;
			}
			set
			{
				this.ViewState["fsActive"] = value;
			}
		}

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x06003579 RID: 13689 RVA: 0x000B1AD0 File Offset: 0x000AFCD0
		// (set) Token: 0x0600357A RID: 13690 RVA: 0x000B1B08 File Offset: 0x000AFD08
		[DefaultValue(false)]
		public bool Muted
		{
			get
			{
				bool result = false;
				if (this.ViewState["Muted"] != null)
				{
					result = (bool)this.ViewState["Muted"];
				}
				return result;
			}
			set
			{
				this.ViewState["Muted"] = value;
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x0600357B RID: 13691 RVA: 0x000B1B20 File Offset: 0x000AFD20
		// (set) Token: 0x0600357C RID: 13692 RVA: 0x000B1B67 File Offset: 0x000AFD67
		[DefaultValue(false)]
		public bool ToolbarDocked
		{
			get
			{
				bool result;
				if (this.ViewState["ToolbarDocked"] != null)
				{
					result = Convert.ToBoolean(this.ViewState["ToolbarDocked"]);
				}
				else
				{
					result = this.EnableAriaSupport;
				}
				return result;
			}
			set
			{
				this.ViewState["ToolbarDocked"] = value;
			}
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x0600357D RID: 13693 RVA: 0x000B1B80 File Offset: 0x000AFD80
		// (set) Token: 0x0600357E RID: 13694 RVA: 0x000B1BB3 File Offset: 0x000AFDB3
		[Description("Gets or sets the text of the play button.")]
		[DefaultValue("Play")]
		public virtual string PlayButtonToolTip
		{
			get
			{
				string text = (string)this.ViewState["PlayButtonToolTip"];
				return text ?? this.Localization.PlayButtonToolTip;
			}
			set
			{
				this.ViewState["PlayButtonToolTip"] = value;
			}
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x0600357F RID: 13695 RVA: 0x000B1BC8 File Offset: 0x000AFDC8
		// (set) Token: 0x06003580 RID: 13696 RVA: 0x000B1BFB File Offset: 0x000AFDFB
		[Description("Gets or sets the text of the pause button.")]
		[DefaultValue("Pause")]
		public virtual string PauseButtonToolTip
		{
			get
			{
				string text = (string)this.ViewState["PauseButtonToolTip"];
				return text ?? this.Localization.PauseButtonToolTip;
			}
			set
			{
				this.ViewState["PauseButtonToolTip"] = value;
			}
		}

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06003581 RID: 13697 RVA: 0x000B1C10 File Offset: 0x000AFE10
		// (set) Token: 0x06003582 RID: 13698 RVA: 0x000B1C52 File Offset: 0x000AFE52
		[DefaultValue("Mute")]
		public string VolumeButtonToolTip
		{
			get
			{
				string result = this.Localization.VolumeButtonToolTip;
				if (this.ViewState["VolumeButtonToolTip"] != null)
				{
					result = this.ViewState["VolumeButtonToolTip"].ToString();
				}
				return result;
			}
			set
			{
				this.ViewState["VolumeButtonToolTip"] = value;
			}
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x000B1C68 File Offset: 0x000AFE68
		// (set) Token: 0x06003584 RID: 13700 RVA: 0x000B1CAA File Offset: 0x000AFEAA
		[DefaultValue("HD")]
		public string HDButtonToolTip
		{
			get
			{
				string result = this.Localization.HDButtonToolTip;
				if (this.ViewState["HDButtonToolTip"] != null)
				{
					result = this.ViewState["HDButtonToolTip"].ToString();
				}
				return result;
			}
			set
			{
				this.ViewState["HDButtonToolTip"] = value;
			}
		}

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06003585 RID: 13701 RVA: 0x000B1CC0 File Offset: 0x000AFEC0
		// (set) Token: 0x06003586 RID: 13702 RVA: 0x000B1D02 File Offset: 0x000AFF02
		[DefaultValue("Subtitles")]
		public string SubtitlesButtonToolTip
		{
			get
			{
				string result = this.Localization.SubtitlesButtonToolTip;
				if (this.ViewState["SubtitlesButtonToolTip"] != null)
				{
					result = this.ViewState["SubtitlesButtonToolTip"].ToString();
				}
				return result;
			}
			set
			{
				this.ViewState["SubtitlesButtonToolTip"] = value;
			}
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06003587 RID: 13703 RVA: 0x000B1D18 File Offset: 0x000AFF18
		// (set) Token: 0x06003588 RID: 13704 RVA: 0x000B1D4B File Offset: 0x000AFF4B
		[DefaultValue("Close")]
		[Description("The ToolTip of the image shown as banner")]
		public virtual string BannerCloseButtonToolTip
		{
			get
			{
				string text = (string)this.ViewState["_bcbtt"];
				return text ?? this.Localization.BannerCloseButtonToolTip;
			}
			set
			{
				this.ViewState["_bcbtt"] = value;
			}
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x06003589 RID: 13705 RVA: 0x000B1D60 File Offset: 0x000AFF60
		// (set) Token: 0x0600358A RID: 13706 RVA: 0x000B1DA2 File Offset: 0x000AFFA2
		[DefaultValue("Share")]
		public string TitleBarShareToolTip
		{
			get
			{
				string result = this.Localization.TitleBarShareToolTip;
				if (this.ViewState["TitleBarShareToolTip"] != null)
				{
					result = this.ViewState["TitleBarShareToolTip"].ToString();
				}
				return result;
			}
			set
			{
				this.ViewState["TitleBarShareToolTip"] = value;
			}
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x0600358B RID: 13707 RVA: 0x000B1DB8 File Offset: 0x000AFFB8
		// (set) Token: 0x0600358C RID: 13708 RVA: 0x000B1DFA File Offset: 0x000AFFFA
		[DefaultValue("Full Screen")]
		public string FullScreenButtonToolTip
		{
			get
			{
				string result = this.Localization.FullScreenButtonToolTip;
				if (this.ViewState["FullScreenButtonToolTip"] != null)
				{
					result = this.ViewState["FullScreenButtonToolTip"].ToString();
				}
				return result;
			}
			set
			{
				this.ViewState["FullScreenButtonToolTip"] = value;
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x0600358D RID: 13709 RVA: 0x000B1E0D File Offset: 0x000B000D
		public MediaPlayerToolbar ToolBar
		{
			get
			{
				this.EnsureChildControls();
				return this.mpToolBar;
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x0600358E RID: 13710 RVA: 0x000B1E1B File Offset: 0x000B001B
		public MediaPlayerTitlebar TitleBar
		{
			get
			{
				this.EnsureChildControls();
				return this.mpTitleBar;
			}
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x0600358F RID: 13711 RVA: 0x000B1E29 File Offset: 0x000B0029
		[NotifyParentProperty(true)]
		[MergableProperty(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Default")]
		public MediaPlayerSourcesCollection Sources
		{
			get
			{
				if (this.sources == null)
				{
					this.sources = new MediaPlayerSourcesCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.sources).TrackViewState();
					}
				}
				return this.sources;
			}
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06003590 RID: 13712 RVA: 0x000B1E57 File Offset: 0x000B0057
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Collection of MediaPlayerBanner objects")]
		[Category("Default")]
		public MediaPlayerBannerCollection Banners
		{
			get
			{
				if (this.baners == null)
				{
					this.baners = new MediaPlayerBannerCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.baners).TrackViewState();
					}
				}
				return this.baners;
			}
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x06003591 RID: 13713 RVA: 0x000B1E85 File Offset: 0x000B0085
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Default")]
		[NotifyParentProperty(true)]
		public MediaPlayerFilesCollection Playlist
		{
			get
			{
				if (this.files == null)
				{
					this.files = new MediaPlayerFilesCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.files).TrackViewState();
					}
				}
				return this.files;
			}
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x06003592 RID: 13714 RVA: 0x000B1EB4 File Offset: 0x000B00B4
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		[Description("")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		public MediaPlayerPlaylistSettings PlaylistSettings
		{
			get
			{
				if (this.playlistSettings == null)
				{
					this.playlistSettings = new MediaPlayerPlaylistSettings();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.playlistSettings).TrackViewState();
					}
				}
				return this.playlistSettings;
			}
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x06003593 RID: 13715 RVA: 0x000B1EE2 File Offset: 0x000B00E2
		// (set) Token: 0x06003594 RID: 13716 RVA: 0x000B1F04 File Offset: 0x000B0104
		[Description("Gets or sets a value indicating where RadMediaPlayer will look for its .resx localization files.")]
		[DefaultValue("")]
		[Category("Misc")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x06003595 RID: 13717 RVA: 0x000B1F57 File Offset: 0x000B0157
		// (set) Token: 0x06003596 RID: 13718 RVA: 0x000B1F77 File Offset: 0x000B0177
		[DefaultValue(typeof(CultureInfo), "en-US")]
		[Category("Appearance")]
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				if (value != this.ViewState["Culture"])
				{
					this._localization = null;
				}
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x04000E73 RID: 3699
		internal const string FlashModuleWebResourceName = "Telerik.Web.UI.MediaPlayer.FlashMediaPlayer.swf";

		// Token: 0x04000E74 RID: 3700
		private RadAjaxLoadingPanel rlpLoadingIndicator;

		// Token: 0x04000E75 RID: 3701
		private MediaPlayerStrings _localization;

		// Token: 0x04000E76 RID: 3702
		internal List<IDictionary> mediaFilesData = new List<IDictionary>();

		// Token: 0x04000E77 RID: 3703
		internal List<IDictionary> options = new List<IDictionary>();

		// Token: 0x04000E78 RID: 3704
		private MediaPlayerToolbar mpToolBar;

		// Token: 0x04000E79 RID: 3705
		private MediaPlayerTitlebar mpTitleBar;

		// Token: 0x04000E7A RID: 3706
		private MediaPlayerSourcesCollection sources;

		// Token: 0x04000E7B RID: 3707
		private MediaPlayerBannerCollection baners;

		// Token: 0x04000E7C RID: 3708
		private MediaPlayerFilesCollection files;

		// Token: 0x04000E7D RID: 3709
		private MediaPlayerPlaylistSettings playlistSettings;
	}
}
