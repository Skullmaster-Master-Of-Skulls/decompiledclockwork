using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.Design.WebControls;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.Design
{
	// Token: 0x020001B4 RID: 436
	public class TwitterDesigner : CompositeControlDesigner
	{
		// Token: 0x06000CC1 RID: 3265 RVA: 0x00021DC8 File Offset: 0x0001FFC8
		public override void Initialize(IComponent component)
		{
			this._twitter = (component as Twitter);
			if (this._twitter == null)
			{
				throw new ArgumentException("Component must be a Twitter control", "component");
			}
			base.Initialize(component);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00021DF8 File Offset: 0x0001FFF8
		public override string GetDesignTimeHtml()
		{
			string text = base.GetDesignTimeHtml();
			int num = text.IndexOf("<div", 1);
			text = ((num > 0) ? text.Substring(0, text.IndexOf("<div", 1)) : text.Remove(text.Length - 6, 6));
			text = text.Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("\t", string.Empty);
			string text2 = null;
			try
			{
				TwitterMode mode = this._twitter.Mode;
				if (mode == TwitterMode.Profile)
				{
					if (string.IsNullOrEmpty(this._twitter.ScreenName))
					{
						throw new Exception("Please specify a screen name");
					}
				}
				else if (string.IsNullOrEmpty(this._twitter.Search))
				{
					throw new Exception("Please specify a search keyword");
				}
				IList<TwitterStatus> list = this.GenerateData();
				if (list.Count > 0)
				{
					text2 = this.RenderLayout(list);
				}
				else
				{
					text2 = this.RenderEmptyData();
				}
			}
			catch (Exception ex)
			{
				if (text2 == null)
				{
					text2 = "<div>" + ex.Message + "</div>";
				}
			}
			string webResourceUrl = base.ViewControl.Page.ClientScript.GetWebResourceUrl(base.GetType(), "Twitter.css");
			string str = string.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\"/>", webResourceUrl);
			return text + str + text2 + "</div>";
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00021F50 File Offset: 0x00020150
		private string RenderEmptyData()
		{
			return this.PersistTemplate(this._twitter.EmptyDataTemplate);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00021F64 File Offset: 0x00020164
		private IList<TwitterStatus> GenerateData()
		{
			if (this._twitter.IsLiveContentOnDesignMode)
			{
				TwitterAPI twitterAPI = new TwitterAPI();
				TwitterMode mode = this._twitter.Mode;
				IList<TwitterStatus> list;
				if (mode == TwitterMode.Profile)
				{
					list = twitterAPI.GetProfile(this._twitter.ScreenName, this._twitter.Count, this._twitter.IncludeRetweets, this._twitter.IncludeReplies);
					if (list != null && list.Count > 0)
					{
						TwitterUser user = list[0].User;
						this._twitter.Title = (this._twitter.Title ?? user.Name);
						this._twitter.Caption = (this._twitter.Caption ?? user.ScreenName);
						this._twitter.ProfileImageUrl = (this._twitter.ProfileImageUrl ?? user.ProfileImageUrl);
					}
				}
				else
				{
					list = twitterAPI.GetSearch(this._twitter.Search, this._twitter.Count);
				}
				return list;
			}
			return this.GenerateFakeData();
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00022078 File Offset: 0x00020278
		private IList<TwitterStatus> GenerateFakeData()
		{
			List<TwitterStatus> list = new List<TwitterStatus>();
			string webResourceUrl = base.ViewControl.Page.ClientScript.GetWebResourceUrl(base.GetType(), "Twitter.32.png");
			TwitterUser twitterUser = new TwitterUser
			{
				ScreenName = "ajaxcontroltoolkit",
				Description = "Ajax Control Toolkit",
				Id = "ajaxcontroltoolkit",
				Name = "Ajax Control Toolkit",
				Location = "US",
				ProfileImageUrl = webResourceUrl
			};
			string text = string.Empty;
			if (this._twitter.Mode == TwitterMode.Profile)
			{
				list.Add(new TwitterStatus
				{
					CreatedAt = DateTime.Now,
					Text = "Ajax Control Toolkit",
					User = twitterUser
				});
				this._twitter.Title = (this._twitter.Title ?? twitterUser.Name);
				this._twitter.Caption = (this._twitter.Caption ?? twitterUser.ScreenName);
				this._twitter.ProfileImageUrl = (this._twitter.ProfileImageUrl ?? twitterUser.ProfileImageUrl);
			}
			else
			{
				string[] array = this._twitter.Search.Split(new string[]
				{
					" "
				}, StringSplitOptions.RemoveEmptyEntries);
				foreach (string str in array)
				{
					text = text + "<em>" + str + "</em> ";
				}
				text = " " + text;
			}
			string[] array3 = new string[]
			{
				"Lorem <a href='http://www.sample_ipsum_link.com'>ipsum</a> dolor sit amet, " + text + "consectetur adipisicing elit, sed do eiusmod tempor incididunt ut",
				"labore et dolore magna aliqua. Ut enim ad minim veniam, quis " + text + "nostrud exercitation",
				"ullamco laboris " + text + "nisi ut aliquip ex ea <a href='http://comodo_sample_link'>commodo</a> consequat",
				text + "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla",
				"Excepteur sint " + text + "occaecat cupidatat non proident, sunt in culpa qui officia deserunt"
			};
			int num = 0;
			Random random = new Random();
			foreach (string text2 in array3)
			{
				list.Add(new TwitterStatus
				{
					CreatedAt = DateTime.Now.AddMinutes((double)(random.Next(1, 1000) * -1)),
					Text = text2,
					User = twitterUser
				});
				num++;
				if (num > this._twitter.Count)
				{
					break;
				}
			}
			if (this._twitter.Mode == TwitterMode.Profile && list.Count > 1)
			{
				list.RemoveAt(list.Count - 1);
			}
			return list;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00022318 File Offset: 0x00020518
		private string RenderLayout(IList<TwitterStatus> statuses)
		{
			string text = this.RenderEvalScripts(this._twitter.LayoutTemplate, statuses[0]);
			if (string.IsNullOrEmpty(text))
			{
				text = this.PersistTemplate(this._twitter.LayoutTemplate);
			}
			string pattern = "<(asp:)\\b([^>]*?)(ITEMPLACEHOLDER)([^>]*?)(>([^>]*?)</asp:PlaceHolder>|/>)(.*?)";
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
			string text2 = string.Empty;
			foreach (TwitterStatus status in statuses)
			{
				text2 += this.RenderStatus(status);
			}
			return regex.Replace(text, text2);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000223C0 File Offset: 0x000205C0
		private string PersistTemplate(ITemplate template)
		{
			IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			return ControlPersister.PersistTemplate(template, host);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000223EA File Offset: 0x000205EA
		private string FillStatusValue(Match match)
		{
			this._valCounter++;
			return this._values[this._valCounter - 1];
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00022410 File Offset: 0x00020610
		private string RenderEvalScripts(ITemplate template, TwitterStatus status)
		{
			string input = this.PersistTemplate(template);
			Regex regex = new Regex("(<%#) ?.*eval?.*%>", RegexOptions.IgnoreCase);
			MatchCollection matchCollection = regex.Matches(input);
			this._valCounter = 0;
			this._values = new List<string>();
			if (matchCollection.Count > 0)
			{
				Regex regex2 = new Regex("\"(.*?)\"");
				foreach (object obj in matchCollection)
				{
					string text = regex2.Match(obj.ToString()).ToString();
					text = text.Substring(1, text.Length - 2);
					object obj2 = null;
					if (text.Contains("."))
					{
						string[] array = text.Split(new string[]
						{
							"."
						}, StringSplitOptions.RemoveEmptyEntries);
						object value = typeof(TwitterStatus).GetProperty(array[0]).GetValue(status, null);
						if (value != null)
						{
							obj2 = value.GetType().GetProperty(array[1]).GetValue(value, null);
						}
					}
					else
					{
						obj2 = typeof(TwitterStatus).GetProperty(text).GetValue(status, null);
					}
					if (obj2 == null)
					{
						obj2 = "[" + text + "]";
					}
					this._values.Add(obj2.ToString());
				}
				MatchEvaluator evaluator = new MatchEvaluator(this.FillStatusValue);
				return regex.Replace(input, evaluator);
			}
			return null;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00022598 File Offset: 0x00020798
		private string RenderStatus(TwitterStatus status)
		{
			string text = this.RenderEvalScripts(this._twitter.StatusTemplate, status);
			if (string.IsNullOrEmpty(text))
			{
				ListViewDataItem listViewDataItem = new ListViewDataItem(0, 0)
				{
					DataItem = status
				};
				this._twitter.StatusTemplate.InstantiateIn(listViewDataItem);
				listViewDataItem.DataBind();
				text = TwitterDesigner.RenderControl(listViewDataItem);
			}
			return text;
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x000225F0 File Offset: 0x000207F0
		private static string RenderControl(Control control)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				using (HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter))
				{
					control.RenderControl(htmlTextWriter);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x00022654 File Offset: 0x00020854
		public override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00022657 File Offset: 0x00020857
		protected override bool Visible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040004AC RID: 1196
		private Twitter _twitter;

		// Token: 0x040004AD RID: 1197
		private List<string> _values = new List<string>();

		// Token: 0x040004AE RID: 1198
		private int _valCounter;
	}
}
