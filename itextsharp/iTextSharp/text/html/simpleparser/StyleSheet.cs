using System;
using System.Collections.Generic;
using System.Globalization;

namespace iTextSharp.text.html.simpleparser
{
	// Token: 0x02000642 RID: 1602
	public class StyleSheet
	{
		// Token: 0x06003623 RID: 13859 RVA: 0x00151CDC File Offset: 0x00150CDC
		public void ApplyStyle(string tag, Dictionary<string, string> props)
		{
			Dictionary<string, string> dictionary;
			if (this.tagMap.TryGetValue(tag.ToLower(CultureInfo.InvariantCulture), out dictionary))
			{
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>(dictionary);
				foreach (KeyValuePair<string, string> keyValuePair in props)
				{
					dictionary2[keyValuePair.Key] = keyValuePair.Value;
				}
				foreach (KeyValuePair<string, string> keyValuePair2 in dictionary2)
				{
					props[keyValuePair2.Key] = keyValuePair2.Value;
				}
			}
			string text;
			if (!props.TryGetValue("class", out text))
			{
				return;
			}
			if (!this.classMap.TryGetValue(text.ToLower(CultureInfo.InvariantCulture), out dictionary))
			{
				return;
			}
			props.Remove("class");
			foreach (KeyValuePair<string, string> keyValuePair3 in props)
			{
				dictionary[keyValuePair3.Key] = keyValuePair3.Value;
			}
			foreach (KeyValuePair<string, string> keyValuePair4 in dictionary)
			{
				props[keyValuePair4.Key] = keyValuePair4.Value;
			}
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x00151E78 File Offset: 0x00150E78
		public void LoadStyle(string style, Dictionary<string, string> props)
		{
			this.classMap[style.ToLower(CultureInfo.InvariantCulture)] = props;
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x00151E94 File Offset: 0x00150E94
		public void LoadStyle(string style, string key, string value)
		{
			style = style.ToLower(CultureInfo.InvariantCulture);
			Dictionary<string, string> dictionary;
			if (!this.classMap.TryGetValue(style, out dictionary))
			{
				dictionary = new Dictionary<string, string>();
				this.classMap[style] = dictionary;
			}
			dictionary[key] = value;
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x00151ED9 File Offset: 0x00150ED9
		public void LoadTagStyle(string tag, Dictionary<string, string> props)
		{
			this.tagMap[tag.ToLower(CultureInfo.InvariantCulture)] = props;
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x00151EF4 File Offset: 0x00150EF4
		public void LoadTagStyle(string tag, string key, string value)
		{
			tag = tag.ToLower(CultureInfo.InvariantCulture);
			Dictionary<string, string> dictionary;
			if (!this.tagMap.TryGetValue(tag, out dictionary))
			{
				dictionary = new Dictionary<string, string>();
				this.tagMap[tag] = dictionary;
			}
			dictionary[key] = value;
		}

		// Token: 0x0400249B RID: 9371
		public Dictionary<string, Dictionary<string, string>> classMap = new Dictionary<string, Dictionary<string, string>>();

		// Token: 0x0400249C RID: 9372
		public Dictionary<string, Dictionary<string, string>> tagMap = new Dictionary<string, Dictionary<string, string>>();
	}
}
