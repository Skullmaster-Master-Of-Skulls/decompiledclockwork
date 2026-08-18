using System;
using System.Collections;
using System.Globalization;
using System.IO;

namespace System.Web.UI
{
	// Token: 0x02000334 RID: 820
	public class XhtmlTextWriter : HtmlTextWriter
	{
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060025FC RID: 9724 RVA: 0x00007722 File Offset: 0x00005922
		internal override bool RenderDivAroundHiddenInputs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x0007CDC7 File Offset: 0x0007AFC7
		public XhtmlTextWriter(TextWriter writer) : this(writer, "\t")
		{
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x0007CDD8 File Offset: 0x0007AFD8
		public XhtmlTextWriter(TextWriter writer, string tabString) : base(writer, tabString)
		{
			this._commonAttributes.Add("class", true);
			this._commonAttributes.Add("id", true);
			this._commonAttributes.Add("title", true);
			this._commonAttributes.Add("xml:lang", true);
			this.AddRecognizedAttributes("head", new string[]
			{
				"xml:lang"
			});
			this._suppressCommonAttributes["head"] = true;
			this.AddRecognizedAttributes("html", new string[]
			{
				"xml:lang",
				"version",
				"xmlns"
			});
			this._suppressCommonAttributes["html"] = true;
			this.AddRecognizedAttributes("title", new string[]
			{
				"xml:lang"
			});
			this._suppressCommonAttributes["title"] = true;
			this.AddRecognizedAttributes("blockquote", new string[]
			{
				"cite"
			});
			this.AddRecognizedAttributes("br", new string[]
			{
				"class",
				"id",
				"title"
			});
			this._suppressCommonAttributes["br"] = true;
			this.AddRecognizedAttributes("pre", new string[]
			{
				"xml:space"
			});
			this.AddRecognizedAttributes("q", new string[]
			{
				"cite"
			});
			this.AddRecognizedAttributes("a", new string[]
			{
				"accesskey",
				"charset",
				"href",
				"hreflang",
				"rel",
				"rev",
				"tabindex",
				"type",
				"title"
			});
			this.AddRecognizedAttributes("form", new string[]
			{
				"action",
				"method",
				"enctype"
			});
			this.AddRecognizedAttributes("input", new string[]
			{
				"accesskey",
				"checked",
				"maxlength",
				"name",
				"size",
				"src",
				"tabindex",
				"type",
				"value",
				"title",
				"disabled"
			});
			this.AddRecognizedAttributes("label", new string[]
			{
				"accesskey"
			});
			this.AddRecognizedAttributes("label", new string[]
			{
				"for"
			});
			this.AddRecognizedAttributes("select", new string[]
			{
				"multiple",
				"name",
				"size",
				"tabindex",
				"disabled"
			});
			this.AddRecognizedAttributes("option", new string[]
			{
				"selected",
				"value"
			});
			this.AddRecognizedAttributes("textarea", new string[]
			{
				"accesskey",
				"cols",
				"name",
				"rows",
				"tabindex"
			});
			this.AddRecognizedAttributes("table", new string[]
			{
				"summary",
				"width"
			});
			this.AddRecognizedAttributes("td", new string[]
			{
				"abbr",
				"align",
				"axis",
				"colspan",
				"headers",
				"rowspan",
				"scope",
				"valign"
			});
			this.AddRecognizedAttributes("th", new string[]
			{
				"abbr",
				"align",
				"axis",
				"colspan",
				"headers",
				"rowspan",
				"scope",
				"valign"
			});
			this.AddRecognizedAttributes("tr", new string[]
			{
				"align",
				"valign"
			});
			this.AddRecognizedAttributes("img", new string[]
			{
				"alt",
				"height",
				"longdesc",
				"src",
				"width"
			});
			this.AddRecognizedAttributes("object", new string[]
			{
				"archive",
				"classid",
				"codebase",
				"codetype",
				"data",
				"declare",
				"height",
				"name",
				"standby",
				"tabindex",
				"type",
				"width"
			});
			this.AddRecognizedAttributes("param", new string[]
			{
				"id",
				"name",
				"type",
				"value",
				"valuetype"
			});
			this.AddRecognizedAttributes("meta", new string[]
			{
				"xml:lang",
				"content",
				"http-equiv",
				"name",
				"scheme"
			});
			this._suppressCommonAttributes["meta"] = true;
			this.AddRecognizedAttributes("link", new string[]
			{
				"charset",
				"href",
				"hreflang",
				"media",
				"rel",
				"rev",
				"type"
			});
			this.AddRecognizedAttributes("base", new string[]
			{
				"href"
			});
			this._suppressCommonAttributes["base"] = true;
			this.AddRecognizedAttributes("optgroup", new string[]
			{
				"disabled",
				"label"
			});
			this.AddRecognizedAttributes("ol", new string[]
			{
				"start"
			});
			this.AddRecognizedAttributes("li", new string[]
			{
				"value"
			});
			this.AddRecognizedAttributes("style", new string[]
			{
				"xml:lang",
				"media",
				"title",
				"type",
				"xml:space"
			});
			this._suppressCommonAttributes["style"] = true;
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x0007D47F File Offset: 0x0007B67F
		public virtual void AddRecognizedAttribute(string elementName, string attributeName)
		{
			this.AddRecognizedAttributes(elementName, new string[]
			{
				attributeName
			});
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x0007D494 File Offset: 0x0007B694
		private void AddRecognizedAttributes(string elementName, params string[] attributes)
		{
			Hashtable hashtable = (Hashtable)this._elementSpecificAttributes[elementName];
			if (hashtable == null)
			{
				hashtable = new Hashtable(StringComparer.CurrentCultureIgnoreCase);
				this._elementSpecificAttributes[elementName] = hashtable;
			}
			foreach (string key in attributes)
			{
				hashtable.Add(key, true);
			}
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x0007D4F0 File Offset: 0x0007B6F0
		public override bool IsValidFormAttribute(string attributeName)
		{
			Hashtable hashtable = (Hashtable)this._elementSpecificAttributes["form"];
			return hashtable != null && hashtable[attributeName] != null;
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x0007D524 File Offset: 0x0007B724
		protected override bool OnAttributeRender(string name, string value, HtmlTextWriterAttribute key)
		{
			return (this._commonAttributes[name] != null && this._suppressCommonAttributes[base.TagName] == null) || (this._elementSpecificAttributes[base.TagName] != null && ((Hashtable)this._elementSpecificAttributes[base.TagName])[name] != null);
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x0007D588 File Offset: 0x0007B788
		protected override bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
		{
			return this._docType != XhtmlMobileDocType.XhtmlBasic && (!base.TagName.ToLower(CultureInfo.InvariantCulture).Equals("div") || !name.ToLower(CultureInfo.InvariantCulture).Equals("border-collapse"));
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x0007D5D8 File Offset: 0x0007B7D8
		public virtual void RemoveRecognizedAttribute(string elementName, string attributeName)
		{
			Hashtable hashtable = (Hashtable)this._elementSpecificAttributes[elementName];
			if (hashtable == null)
			{
				hashtable = new Hashtable(StringComparer.CurrentCultureIgnoreCase);
				this._elementSpecificAttributes[elementName] = hashtable;
			}
			if (this._commonAttributes[attributeName] == null || this._suppressCommonAttributes[elementName] != null)
			{
				hashtable.Remove(attributeName);
				return;
			}
			this._suppressCommonAttributes[elementName] = true;
			foreach (object obj in this._commonAttributes.Keys)
			{
				string a = (string)obj;
				if (a != attributeName)
				{
					hashtable.Add(attributeName, true);
				}
			}
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x0007D6A8 File Offset: 0x0007B8A8
		public virtual void SetDocType(XhtmlMobileDocType docType)
		{
			this._docType = docType;
			if (docType != XhtmlMobileDocType.XhtmlBasic && this._commonAttributes["style"] == null)
			{
				this._commonAttributes.Add("style", true);
			}
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x0007D6DC File Offset: 0x0007B8DC
		public override void WriteBreak()
		{
			this.WriteFullBeginTag("br/");
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x0007D6E9 File Offset: 0x0007B8E9
		protected Hashtable CommonAttributes
		{
			get
			{
				return this._commonAttributes;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x0007D6F1 File Offset: 0x0007B8F1
		protected Hashtable ElementSpecificAttributes
		{
			get
			{
				return this._elementSpecificAttributes;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x0007D6F9 File Offset: 0x0007B8F9
		protected Hashtable SuppressCommonAttributes
		{
			get
			{
				return this._suppressCommonAttributes;
			}
		}

		// Token: 0x04001DAD RID: 7597
		private Hashtable _commonAttributes = new Hashtable();

		// Token: 0x04001DAE RID: 7598
		private Hashtable _elementSpecificAttributes = new Hashtable(StringComparer.CurrentCultureIgnoreCase);

		// Token: 0x04001DAF RID: 7599
		private Hashtable _suppressCommonAttributes = new Hashtable(StringComparer.CurrentCultureIgnoreCase);

		// Token: 0x04001DB0 RID: 7600
		private XhtmlMobileDocType _docType;
	}
}
