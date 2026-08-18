using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.UI;

namespace System.Web.Helpers
{
	// Token: 0x02000010 RID: 16
	internal class HtmlElement
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00003D44 File Offset: 0x00001F44
		public HtmlElement(string tagName)
		{
			this.TagName = tagName;
			this.Attributes = new Dictionary<string, string>();
			this.Children = new List<HtmlElement>();
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003D69 File Offset: 0x00001F69
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003D71 File Offset: 0x00001F71
		internal string TagName { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003D7A File Offset: 0x00001F7A
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003D82 File Offset: 0x00001F82
		internal string InnerText { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003D8B File Offset: 0x00001F8B
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003D93 File Offset: 0x00001F93
		public IList<HtmlElement> Children { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003D9C File Offset: 0x00001F9C
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003DA4 File Offset: 0x00001FA4
		private IDictionary<string, string> Attributes { get; set; }

		// Token: 0x17000021 RID: 33
		public string this[string name]
		{
			get
			{
				return this.Attributes[name];
			}
			set
			{
				this.MergeAttribute(name, value);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003DC5 File Offset: 0x00001FC5
		public HtmlElement SetInnerText(string innerText)
		{
			this.InnerText = innerText;
			this.Children.Clear();
			return this;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003DDA File Offset: 0x00001FDA
		public HtmlElement AppendChild(HtmlElement e)
		{
			this.Children.Add(e);
			return this;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003DE9 File Offset: 0x00001FE9
		public HtmlElement AppendChild(string innerText)
		{
			this.AppendChild(HtmlElement.CreateSpan(innerText, null));
			return this;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003DFA File Offset: 0x00001FFA
		private void MergeAttribute(string name, string value)
		{
			this.Attributes[name] = value;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003E0C File Offset: 0x0000200C
		public HtmlElement AddCssClass(string className)
		{
			string str;
			if (!this.Attributes.TryGetValue("class", out str))
			{
				this.Attributes["class"] = className;
			}
			else
			{
				this.Attributes["class"] = str + " " + className;
			}
			return this;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003E60 File Offset: 0x00002060
		public IHtmlString ToHtmlString()
		{
			IHtmlString result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				this.WriteTo(stringWriter);
				result = new HtmlString(stringWriter.ToString());
			}
			return result;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003EA8 File Offset: 0x000020A8
		public void WriteTo(TextWriter writer)
		{
			this.WriteToInternal(new HtmlTextWriter(writer));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003EB8 File Offset: 0x000020B8
		private void WriteToInternal(HtmlTextWriter writer)
		{
			foreach (KeyValuePair<string, string> keyValuePair in this.Attributes)
			{
				writer.AddAttribute(keyValuePair.Key, keyValuePair.Value, true);
			}
			writer.RenderBeginTag(this.TagName);
			if (!string.IsNullOrEmpty(this.InnerText))
			{
				writer.WriteEncodedText(this.InnerText);
			}
			else
			{
				foreach (HtmlElement htmlElement in this.Children)
				{
					htmlElement.WriteToInternal(writer);
				}
			}
			writer.RenderEndTag();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003F7C File Offset: 0x0000217C
		public override string ToString()
		{
			return this.ToHtmlString().ToString();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003F8C File Offset: 0x0000218C
		internal static HtmlElement CreateSpan(string innerText, string cssClass = null)
		{
			HtmlElement htmlElement = new HtmlElement("span");
			htmlElement.SetInnerText(innerText);
			if (!string.IsNullOrEmpty(cssClass))
			{
				htmlElement.AddCssClass(cssClass);
			}
			return htmlElement;
		}
	}
}
