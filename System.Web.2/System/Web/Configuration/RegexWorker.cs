using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.RegularExpressions;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x02000740 RID: 1856
	public class RegexWorker
	{
		// Token: 0x06005964 RID: 22884 RVA: 0x00137CE0 File Offset: 0x00135EE0
		public RegexWorker(HttpBrowserCapabilities browserCaps)
		{
			this._browserCaps = browserCaps;
		}

		// Token: 0x06005965 RID: 22885 RVA: 0x00137CF0 File Offset: 0x00135EF0
		private string Lookup(string from)
		{
			MatchCollection matchCollection = RegexWorker.RefPat.Matches(from);
			if (matchCollection.Count == 0)
			{
				return from;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				int length = match.Index - num;
				stringBuilder.Append(from.Substring(num, length));
				num = match.Index + match.Length;
				string value = match.Groups["name"].Value;
				string text = null;
				if (this._groups != null)
				{
					text = (string)this._groups[value];
				}
				if (text == null)
				{
					text = this._browserCaps[value];
				}
				stringBuilder.Append(text);
			}
			stringBuilder.Append(from, num, from.Length - num);
			string text2 = stringBuilder.ToString();
			if (text2.Length == 0)
			{
				return null;
			}
			return text2;
		}

		// Token: 0x170019E7 RID: 6631
		public string this[string key]
		{
			get
			{
				return this.Lookup(key);
			}
		}

		// Token: 0x06005967 RID: 22887 RVA: 0x00137E18 File Offset: 0x00136018
		public bool ProcessRegex(string target, string regexExpression)
		{
			if (target == null)
			{
				target = string.Empty;
			}
			Regex regex = RegexUtil.CreateRegex(regexExpression, RegexOptions.ExplicitCapture);
			Match match = regex.Match(target);
			if (!match.Success)
			{
				return false;
			}
			string[] groupNames = regex.GetGroupNames();
			if (groupNames.Length != 0)
			{
				if (this._groups == null)
				{
					this._groups = new Hashtable();
				}
				for (int i = 0; i < groupNames.Length; i++)
				{
					this._groups[groupNames[i]] = match.Groups[i].Value;
				}
			}
			return true;
		}

		// Token: 0x04002F62 RID: 12130
		internal static readonly Regex RefPat = new BrowserCapsRefRegex();

		// Token: 0x04002F63 RID: 12131
		private Hashtable _groups;

		// Token: 0x04002F64 RID: 12132
		private HttpBrowserCapabilities _browserCaps;
	}
}
