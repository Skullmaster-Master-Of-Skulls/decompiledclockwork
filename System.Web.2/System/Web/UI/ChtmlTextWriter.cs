using System;
using System.Collections;
using System.IO;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000250 RID: 592
	public class ChtmlTextWriter : Html32TextWriter
	{
		// Token: 0x06001B42 RID: 6978 RVA: 0x00055409 File Offset: 0x00053609
		public ChtmlTextWriter(TextWriter writer) : this(writer, "\t")
		{
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x00055418 File Offset: 0x00053618
		public ChtmlTextWriter(TextWriter writer, string tabString) : base(writer, tabString)
		{
			this._globalSuppressedAttributes["onclick"] = true;
			this._globalSuppressedAttributes["ondblclick"] = true;
			this._globalSuppressedAttributes["onmousedown"] = true;
			this._globalSuppressedAttributes["onmouseup"] = true;
			this._globalSuppressedAttributes["onmouseover"] = true;
			this._globalSuppressedAttributes["onmousemove"] = true;
			this._globalSuppressedAttributes["onmouseout"] = true;
			this._globalSuppressedAttributes["onkeypress"] = true;
			this._globalSuppressedAttributes["onkeydown"] = true;
			this._globalSuppressedAttributes["onkeyup"] = true;
			this.RemoveRecognizedAttributeInternal("div", "accesskey");
			this.RemoveRecognizedAttributeInternal("div", "cellspacing");
			this.RemoveRecognizedAttributeInternal("div", "cellpadding");
			this.RemoveRecognizedAttributeInternal("div", "gridlines");
			this.RemoveRecognizedAttributeInternal("div", "rules");
			this.RemoveRecognizedAttributeInternal("span", "cellspacing");
			this.RemoveRecognizedAttributeInternal("span", "cellpadding");
			this.RemoveRecognizedAttributeInternal("span", "gridlines");
			this.RemoveRecognizedAttributeInternal("span", "rules");
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000555CC File Offset: 0x000537CC
		public virtual void AddRecognizedAttribute(string elementName, string attributeName)
		{
			Hashtable hashtable = (Hashtable)this._recognizedAttributes[elementName];
			if (hashtable == null)
			{
				hashtable = new Hashtable(StringComparer.CurrentCultureIgnoreCase);
				this._recognizedAttributes[elementName] = hashtable;
			}
			hashtable.Add(attributeName, true);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x00055614 File Offset: 0x00053814
		protected override bool OnAttributeRender(string name, string value, HtmlTextWriterAttribute key)
		{
			Hashtable hashtable = (Hashtable)this._recognizedAttributes[base.TagName];
			if (hashtable != null && hashtable[name] != null)
			{
				return true;
			}
			if (this._globalSuppressedAttributes[name] != null)
			{
				return false;
			}
			Hashtable hashtable2 = (Hashtable)this._suppressedAttributes[base.TagName];
			return hashtable2 == null || hashtable2[name] == null;
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0005567C File Offset: 0x0005387C
		protected override bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
		{
			return (key != HtmlTextWriterStyle.TextDecoration || !StringUtil.EqualsIgnoreCase("line-through", value)) && base.OnStyleAttributeRender(name, value, key);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0005569B File Offset: 0x0005389B
		protected override bool OnTagRender(string name, HtmlTextWriterTag key)
		{
			return base.OnTagRender(name, key) && key != HtmlTextWriterTag.Span;
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x000556B1 File Offset: 0x000538B1
		public virtual void RemoveRecognizedAttribute(string elementName, string attributeName)
		{
			this.RemoveRecognizedAttributeInternal(elementName, attributeName);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000556BC File Offset: 0x000538BC
		private void RemoveRecognizedAttributeInternal(string elementName, string attributeName)
		{
			Hashtable hashtable = (Hashtable)this._suppressedAttributes[elementName];
			if (hashtable == null)
			{
				hashtable = new Hashtable(StringComparer.CurrentCultureIgnoreCase);
				this._suppressedAttributes[elementName] = hashtable;
			}
			hashtable.Add(attributeName, true);
			hashtable = (Hashtable)this._recognizedAttributes[elementName];
			if (hashtable == null)
			{
				hashtable = new Hashtable(StringComparer.CurrentCultureIgnoreCase);
				this._recognizedAttributes[elementName] = hashtable;
			}
			hashtable.Remove(attributeName);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00055737 File Offset: 0x00053937
		public override void WriteBreak()
		{
			this.Write("<br>");
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x00055744 File Offset: 0x00053944
		public override void WriteEncodedText(string text)
		{
			if (text == null || text.Length == 0)
			{
				return;
			}
			int length = text.Length;
			int num = -1;
			for (int i = 0; i < length; i++)
			{
				int num2 = (int)text[i];
				if (num2 > 160 && num2 < 256)
				{
					if (num != -1)
					{
						base.WriteEncodedText(text.Substring(num, i - num));
						num = -1;
					}
					base.Write(text[i]);
				}
				else if (num == -1)
				{
					num = i;
				}
			}
			if (num != -1)
			{
				if (num == 0)
				{
					base.WriteEncodedText(text);
					return;
				}
				base.WriteEncodedText(text.Substring(num, length - num));
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x000557D4 File Offset: 0x000539D4
		protected Hashtable RecognizedAttributes
		{
			get
			{
				return this._recognizedAttributes;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x000557DC File Offset: 0x000539DC
		protected Hashtable SuppressedAttributes
		{
			get
			{
				return this._suppressedAttributes;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x000557E4 File Offset: 0x000539E4
		protected Hashtable GlobalSuppressedAttributes
		{
			get
			{
				return this._globalSuppressedAttributes;
			}
		}

		// Token: 0x04001893 RID: 6291
		private Hashtable _recognizedAttributes = new Hashtable(StringComparer.CurrentCultureIgnoreCase);

		// Token: 0x04001894 RID: 6292
		private Hashtable _suppressedAttributes = new Hashtable(StringComparer.CurrentCultureIgnoreCase);

		// Token: 0x04001895 RID: 6293
		private Hashtable _globalSuppressedAttributes = new Hashtable(StringComparer.CurrentCultureIgnoreCase);
	}
}
