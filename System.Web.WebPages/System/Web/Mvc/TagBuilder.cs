using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.WebPages.Html;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Mvc
{
	// Token: 0x02000067 RID: 103
	[TypeForwardedFrom("System.Web.Mvc, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class TagBuilder
	{
		// Token: 0x0600027D RID: 637 RVA: 0x00009BEF File Offset: 0x00007DEF
		public TagBuilder(string tagName)
		{
			if (string.IsNullOrEmpty(tagName))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "tagName");
			}
			this.TagName = tagName;
			this.Attributes = new SortedDictionary<string, string>(StringComparer.Ordinal);
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00009C26 File Offset: 0x00007E26
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00009C2E File Offset: 0x00007E2E
		public IDictionary<string, string> Attributes { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00009C37 File Offset: 0x00007E37
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00009C57 File Offset: 0x00007E57
		public string IdAttributeDotReplacement
		{
			get
			{
				if (string.IsNullOrEmpty(this._idAttributeDotReplacement))
				{
					this._idAttributeDotReplacement = HtmlHelper.IdAttributeDotReplacement;
				}
				return this._idAttributeDotReplacement;
			}
			set
			{
				this._idAttributeDotReplacement = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00009C60 File Offset: 0x00007E60
		// (set) Token: 0x06000283 RID: 643 RVA: 0x00009C71 File Offset: 0x00007E71
		public string InnerHtml
		{
			get
			{
				return this._innerHtml ?? string.Empty;
			}
			set
			{
				this._innerHtml = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00009C7A File Offset: 0x00007E7A
		// (set) Token: 0x06000285 RID: 645 RVA: 0x00009C82 File Offset: 0x00007E82
		public string TagName { get; private set; }

		// Token: 0x06000286 RID: 646 RVA: 0x00009C8C File Offset: 0x00007E8C
		public void AddCssClass(string value)
		{
			string str;
			if (this.Attributes.TryGetValue("class", out str))
			{
				this.Attributes["class"] = value + " " + str;
				return;
			}
			this.Attributes["class"] = value;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00009CDB File Offset: 0x00007EDB
		public static string CreateSanitizedId(string originalId)
		{
			return TagBuilder.CreateSanitizedId(originalId, HtmlHelper.IdAttributeDotReplacement);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00009CE8 File Offset: 0x00007EE8
		public static string CreateSanitizedId(string originalId, string invalidCharReplacement)
		{
			if (string.IsNullOrEmpty(originalId))
			{
				return null;
			}
			if (invalidCharReplacement == null)
			{
				throw new ArgumentNullException("invalidCharReplacement");
			}
			char c = originalId[0];
			if (!TagBuilder.Html401IdUtil.IsLetter(c))
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(originalId.Length);
			stringBuilder.Append(c);
			for (int i = 1; i < originalId.Length; i++)
			{
				char c2 = originalId[i];
				if (TagBuilder.Html401IdUtil.IsValidIdCharacter(c2))
				{
					stringBuilder.Append(c2);
				}
				else
				{
					stringBuilder.Append(invalidCharReplacement);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00009D6C File Offset: 0x00007F6C
		public void GenerateId(string name)
		{
			if (!this.Attributes.ContainsKey("id"))
			{
				string value = TagBuilder.CreateSanitizedId(name, this.IdAttributeDotReplacement);
				if (!string.IsNullOrEmpty(value))
				{
					this.Attributes["id"] = value;
				}
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00009DB4 File Offset: 0x00007FB4
		private void AppendAttributes(StringBuilder sb)
		{
			foreach (KeyValuePair<string, string> keyValuePair in this.Attributes)
			{
				string key = keyValuePair.Key;
				if (!string.Equals(key, "id", StringComparison.Ordinal) || !string.IsNullOrEmpty(keyValuePair.Value))
				{
					string value = HttpUtility.HtmlAttributeEncode(keyValuePair.Value);
					sb.Append(' ').Append(key).Append("=\"").Append(value).Append('"');
				}
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009E54 File Offset: 0x00008054
		public void MergeAttribute(string key, string value)
		{
			this.MergeAttribute(key, value, false);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00009E5F File Offset: 0x0000805F
		public void MergeAttribute(string key, string value, bool replaceExisting)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "key");
			}
			if (replaceExisting || !this.Attributes.ContainsKey(key))
			{
				this.Attributes[key] = value;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009E97 File Offset: 0x00008097
		public void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes)
		{
			this.MergeAttributes<TKey, TValue>(attributes, false);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009EA4 File Offset: 0x000080A4
		public void MergeAttributes<TKey, TValue>(IDictionary<TKey, TValue> attributes, bool replaceExisting)
		{
			if (attributes != null)
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair in attributes)
				{
					string key = Convert.ToString(keyValuePair.Key, CultureInfo.InvariantCulture);
					string value = Convert.ToString(keyValuePair.Value, CultureInfo.InvariantCulture);
					this.MergeAttribute(key, value, replaceExisting);
				}
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009F20 File Offset: 0x00008120
		public void SetInnerText(string innerText)
		{
			this.InnerHtml = HttpUtility.HtmlEncode(innerText);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00009F2E File Offset: 0x0000812E
		internal HtmlString ToHtmlString(TagRenderMode renderMode)
		{
			return new HtmlString(this.ToString(renderMode));
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009F3C File Offset: 0x0000813C
		public override string ToString()
		{
			return this.ToString(TagRenderMode.Normal);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009F48 File Offset: 0x00008148
		public string ToString(TagRenderMode renderMode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			switch (renderMode)
			{
			case TagRenderMode.StartTag:
				stringBuilder.Append('<').Append(this.TagName);
				this.AppendAttributes(stringBuilder);
				stringBuilder.Append('>');
				break;
			case TagRenderMode.EndTag:
				stringBuilder.Append("</").Append(this.TagName).Append('>');
				break;
			case TagRenderMode.SelfClosing:
				stringBuilder.Append('<').Append(this.TagName);
				this.AppendAttributes(stringBuilder);
				stringBuilder.Append(" />");
				break;
			default:
				stringBuilder.Append('<').Append(this.TagName);
				this.AppendAttributes(stringBuilder);
				stringBuilder.Append('>').Append(this.InnerHtml).Append("</").Append(this.TagName).Append('>');
				break;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000D0 RID: 208
		private string _idAttributeDotReplacement;

		// Token: 0x040000D1 RID: 209
		private string _innerHtml;

		// Token: 0x02000068 RID: 104
		private static class Html401IdUtil
		{
			// Token: 0x06000293 RID: 659 RVA: 0x0000A038 File Offset: 0x00008238
			private static bool IsAllowableSpecialCharacter(char c)
			{
				return c == '-' || c == ':' || c == '_';
			}

			// Token: 0x06000294 RID: 660 RVA: 0x0000A059 File Offset: 0x00008259
			private static bool IsDigit(char c)
			{
				return '0' <= c && c <= '9';
			}

			// Token: 0x06000295 RID: 661 RVA: 0x0000A06A File Offset: 0x0000826A
			public static bool IsLetter(char c)
			{
				return ('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z');
			}

			// Token: 0x06000296 RID: 662 RVA: 0x0000A087 File Offset: 0x00008287
			public static bool IsValidIdCharacter(char c)
			{
				return TagBuilder.Html401IdUtil.IsLetter(c) || TagBuilder.Html401IdUtil.IsDigit(c) || TagBuilder.Html401IdUtil.IsAllowableSpecialCharacter(c);
			}
		}
	}
}
