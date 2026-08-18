using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

namespace AjaxControlToolkit
{
	// Token: 0x020001B0 RID: 432
	public class TwitterAPI
	{
		// Token: 0x06000CA0 RID: 3232 RVA: 0x00021758 File Offset: 0x0001F958
		public List<TwitterStatus> GetSearch(string search, int count)
		{
			string input = this.Query("https://api.twitter.com/1.1/search/tweets.json", new KeyValuePair<string, string>[]
			{
				new KeyValuePair<string, string>("q", search),
				new KeyValuePair<string, string>("count", count.ToString())
			});
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			TwitterAPI.Status status = javaScriptSerializer.Deserialize<TwitterAPI.Status>(input);
			if (status == null || status.Statuses == null)
			{
				return null;
			}
			return (from s in status.Statuses
			select new TwitterStatus
			{
				CreatedAt = this.ParseDateTime(s.created_at),
				Text = s.text,
				User = new TwitterUser
				{
					Id = s.user.id,
					Description = s.user.description,
					Location = s.user.location,
					Name = s.user.name,
					ProfileImageUrl = s.user.profile_image_url,
					ScreenName = s.user.screen_name
				}
			}).ToList<TwitterStatus>();
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002188C File Offset: 0x0001FA8C
		public IList<TwitterStatus> GetProfile(string screenName, int count, bool includeRetweets, bool includeReplies)
		{
			string input = this.Query("https://api.twitter.com/1.1/statuses/user_timeline.json", new KeyValuePair<string, string>[]
			{
				new KeyValuePair<string, string>("screen_name", screenName),
				new KeyValuePair<string, string>("count", count.ToString()),
				new KeyValuePair<string, string>("include_rts", includeRetweets.ToString()),
				new KeyValuePair<string, string>("exclude_replies", (!includeReplies).ToString())
			});
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			List<TwitterAPI.Response> source = javaScriptSerializer.Deserialize<List<TwitterAPI.Response>>(input);
			return (from s in source
			select new TwitterStatus
			{
				CreatedAt = this.ParseDateTime(s.created_at),
				Text = s.text,
				User = new TwitterUser
				{
					Id = s.user.id,
					Description = s.user.description,
					Location = s.user.location,
					Name = s.user.name,
					ProfileImageUrl = s.user.profile_image_url,
					ScreenName = s.user.screen_name
				}
			}).ToList<TwitterStatus>();
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00021990 File Offset: 0x0001FB90
		private string Query(string resourceUrl, IEnumerable<KeyValuePair<string, string>> parameters)
		{
			string text = ConfigurationManager.AppSettings["act:TwitterAccessToken"];
			string stringToEscape = ConfigurationManager.AppSettings["act:TwitterAccessTokenSecret"];
			string text2 = ConfigurationManager.AppSettings["act:TwitterConsumerKey"];
			string stringToEscape2 = ConfigurationManager.AppSettings["act:TwitterConsumerSecret"];
			string text3 = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture)));
			string text4 = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds).ToString(CultureInfo.InvariantCulture);
			List<KeyValuePair<string, string>> first = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("oauth_consumer_key", text2),
				new KeyValuePair<string, string>("oauth_nonce", text3),
				new KeyValuePair<string, string>("oauth_signature_method", "HMAC-SHA1"),
				new KeyValuePair<string, string>("oauth_timestamp", text4),
				new KeyValuePair<string, string>("oauth_token", text),
				new KeyValuePair<string, string>("oauth_version", "1.0")
			};
			IOrderedEnumerable<KeyValuePair<string, string>> source = from tmp in first.Union(parameters.ToArray<KeyValuePair<string, string>>())
			orderby tmp.Key
			select tmp;
			string text5 = string.Join("&", (from p in source
			select string.Format("{0}={1}", p.Key, Uri.EscapeDataString(p.Value))).ToArray<string>());
			text5 = string.Format("{0}&{1}&{2}", "GET", Uri.EscapeDataString(resourceUrl), Uri.EscapeDataString(text5));
			string s = string.Format("{0}&{1}", Uri.EscapeDataString(stringToEscape2), Uri.EscapeDataString(stringToEscape));
			string stringToEscape3;
			using (HMACSHA1 hmacsha = new HMACSHA1(Encoding.ASCII.GetBytes(s)))
			{
				stringToEscape3 = Convert.ToBase64String(hmacsha.ComputeHash(Encoding.ASCII.GetBytes(text5)));
			}
			string value = string.Format("OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", oauth_token=\"{4}\", oauth_signature=\"{5}\", oauth_version=\"{6}\"", new object[]
			{
				Uri.EscapeDataString(text3),
				Uri.EscapeDataString("HMAC-SHA1"),
				Uri.EscapeDataString(text4),
				Uri.EscapeDataString(text2),
				Uri.EscapeDataString(text),
				Uri.EscapeDataString(stringToEscape3),
				Uri.EscapeDataString("1.0")
			});
			ServicePointManager.Expect100Continue = false;
			string str = string.Join("&", (from p in parameters
			select string.Format("{0}={1}", p.Key, Uri.EscapeDataString(p.Value))).ToArray<string>());
			resourceUrl = resourceUrl + "?" + str;
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(resourceUrl);
			httpWebRequest.Headers.Add("Authorization", value);
			httpWebRequest.Method = "GET";
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			string result;
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
				{
					result = streamReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00021CEC File Offset: 0x0001FEEC
		private DateTime ParseDateTime(string date)
		{
			return DateTime.ParseExact(date, "ddd MMM dd HH:mm:ss zzzz yyyy", CultureInfo.InvariantCulture);
		}

		// Token: 0x020001B1 RID: 433
		private class Status
		{
			// Token: 0x170004A8 RID: 1192
			// (get) Token: 0x06000CAA RID: 3242 RVA: 0x00021D06 File Offset: 0x0001FF06
			// (set) Token: 0x06000CAB RID: 3243 RVA: 0x00021D0E File Offset: 0x0001FF0E
			public List<TwitterAPI.Response> Statuses { get; set; }
		}

		// Token: 0x020001B2 RID: 434
		private class Response
		{
			// Token: 0x170004A9 RID: 1193
			// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00021D1F File Offset: 0x0001FF1F
			// (set) Token: 0x06000CAE RID: 3246 RVA: 0x00021D27 File Offset: 0x0001FF27
			public string created_at { get; set; }

			// Token: 0x170004AA RID: 1194
			// (get) Token: 0x06000CAF RID: 3247 RVA: 0x00021D30 File Offset: 0x0001FF30
			// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x00021D38 File Offset: 0x0001FF38
			public string text { get; set; }

			// Token: 0x170004AB RID: 1195
			// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00021D41 File Offset: 0x0001FF41
			// (set) Token: 0x06000CB2 RID: 3250 RVA: 0x00021D49 File Offset: 0x0001FF49
			public TwitterAPI.User user { get; set; }
		}

		// Token: 0x020001B3 RID: 435
		private class User
		{
			// Token: 0x170004AC RID: 1196
			// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00021D5A File Offset: 0x0001FF5A
			// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x00021D62 File Offset: 0x0001FF62
			public string id { get; set; }

			// Token: 0x170004AD RID: 1197
			// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x00021D6B File Offset: 0x0001FF6B
			// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x00021D73 File Offset: 0x0001FF73
			public string screen_name { get; set; }

			// Token: 0x170004AE RID: 1198
			// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00021D7C File Offset: 0x0001FF7C
			// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x00021D84 File Offset: 0x0001FF84
			public string name { get; set; }

			// Token: 0x170004AF RID: 1199
			// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00021D8D File Offset: 0x0001FF8D
			// (set) Token: 0x06000CBB RID: 3259 RVA: 0x00021D95 File Offset: 0x0001FF95
			public string description { get; set; }

			// Token: 0x170004B0 RID: 1200
			// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00021D9E File Offset: 0x0001FF9E
			// (set) Token: 0x06000CBD RID: 3261 RVA: 0x00021DA6 File Offset: 0x0001FFA6
			public string profile_image_url { get; set; }

			// Token: 0x170004B1 RID: 1201
			// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00021DAF File Offset: 0x0001FFAF
			// (set) Token: 0x06000CBF RID: 3263 RVA: 0x00021DB7 File Offset: 0x0001FFB7
			public string location { get; set; }
		}
	}
}
