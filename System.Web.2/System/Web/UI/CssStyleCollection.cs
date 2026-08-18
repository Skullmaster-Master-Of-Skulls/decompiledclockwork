using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;

namespace System.Web.UI
{
	// Token: 0x0200026B RID: 619
	public sealed class CssStyleCollection
	{
		// Token: 0x06001D54 RID: 7508 RVA: 0x0005F2EF File Offset: 0x0005D4EF
		internal CssStyleCollection() : this(null)
		{
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0005F2F8 File Offset: 0x0005D4F8
		internal CssStyleCollection(StateBag state)
		{
			this._state = state;
		}

		// Token: 0x1700084D RID: 2125
		public string this[string key]
		{
			get
			{
				if (this._table == null)
				{
					this.ParseString();
				}
				string text = (string)this._table[key];
				if (text == null)
				{
					HtmlTextWriterStyle styleKey = CssTextWriter.GetStyleKey(key);
					if (styleKey != (HtmlTextWriterStyle)(-1))
					{
						text = this[styleKey];
					}
				}
				return text;
			}
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x1700084E RID: 2126
		public string this[HtmlTextWriterStyle key]
		{
			get
			{
				if (this._intTable == null)
				{
					return null;
				}
				return (string)this._intTable[(int)key];
			}
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0005F384 File Offset: 0x0005D584
		public ICollection Keys
		{
			get
			{
				if (this._table == null)
				{
					this.ParseString();
				}
				if (this._intTable != null)
				{
					string[] array = new string[this._table.Count + this._intTable.Count];
					int num = 0;
					foreach (object obj in this._table.Keys)
					{
						string text = (string)obj;
						array[num] = text;
						num++;
					}
					foreach (object obj2 in this._intTable.Keys)
					{
						HtmlTextWriterStyle styleKey = (HtmlTextWriterStyle)obj2;
						array[num] = CssTextWriter.GetStyleName(styleKey);
						num++;
					}
					return array;
				}
				return this._table.Keys;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x0005F488 File Offset: 0x0005D688
		public int Count
		{
			get
			{
				if (this._table == null)
				{
					this.ParseString();
				}
				return this._table.Count + ((this._intTable != null) ? this._intTable.Count : 0);
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0005F4BA File Offset: 0x0005D6BA
		// (set) Token: 0x06001D5D RID: 7517 RVA: 0x0005F4F4 File Offset: 0x0005D6F4
		public string Value
		{
			get
			{
				if (this._state == null)
				{
					if (this._style == null)
					{
						this._style = this.BuildString();
					}
					return this._style;
				}
				return (string)this._state["style"];
			}
			set
			{
				if (this._state == null)
				{
					this._style = value;
				}
				else
				{
					this._state["style"] = value;
				}
				this._table = null;
			}
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0005F520 File Offset: 0x0005D720
		public void Add(string key, string value)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentNullException("key");
			}
			if (this._table == null)
			{
				this.ParseString();
			}
			this._table[key] = value;
			if (this._intTable != null)
			{
				HtmlTextWriterStyle styleKey = CssTextWriter.GetStyleKey(key);
				if (styleKey != (HtmlTextWriterStyle)(-1))
				{
					this._intTable.Remove(styleKey);
				}
			}
			if (this._state != null)
			{
				this._state["style"] = this.BuildString();
			}
			this._style = null;
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0005F5A4 File Offset: 0x0005D7A4
		public void Add(HtmlTextWriterStyle key, string value)
		{
			if (this._intTable == null)
			{
				this._intTable = new HybridDictionary();
			}
			this._intTable[(int)key] = value;
			string styleName = CssTextWriter.GetStyleName(key);
			if (styleName.Length != 0)
			{
				if (this._table == null)
				{
					this.ParseString();
				}
				this._table.Remove(styleName);
			}
			if (this._state != null)
			{
				this._state["style"] = this.BuildString();
			}
			this._style = null;
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0005F624 File Offset: 0x0005D824
		public void Remove(string key)
		{
			if (this._table == null)
			{
				this.ParseString();
			}
			if (this._table[key] != null)
			{
				this._table.Remove(key);
				if (this._state != null)
				{
					this._state["style"] = this.BuildString();
				}
				this._style = null;
			}
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0005F680 File Offset: 0x0005D880
		public void Remove(HtmlTextWriterStyle key)
		{
			if (this._intTable == null)
			{
				return;
			}
			this._intTable.Remove((int)key);
			if (this._state != null)
			{
				this._state["style"] = this.BuildString();
			}
			this._style = null;
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0005F6CC File Offset: 0x0005D8CC
		public void Clear()
		{
			this._table = null;
			this._intTable = null;
			if (this._state != null)
			{
				this._state.Remove("style");
			}
			this._style = null;
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0005F6FC File Offset: 0x0005D8FC
		private string BuildString()
		{
			if ((this._table == null || this._table.Count == 0) && (this._intTable == null || this._intTable.Count == 0))
			{
				return null;
			}
			StringWriter stringWriter = new StringWriter();
			CssTextWriter writer = new CssTextWriter(stringWriter);
			this.Render(writer);
			return stringWriter.ToString();
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0005F750 File Offset: 0x0005D950
		private void ParseString()
		{
			this._table = new HybridDictionary(true);
			string text = (this._state == null) ? this._style : ((string)this._state["style"]);
			Match match;
			if (text != null && (match = CssStyleCollection._styleAttribRegex.Match(text, 0)).Success)
			{
				CaptureCollection captures = match.Groups["stylename"].Captures;
				CaptureCollection captures2 = match.Groups["styleval"].Captures;
				for (int i = 0; i < captures.Count; i++)
				{
					string key = captures[i].ToString();
					string value = captures2[i].ToString();
					this._table[key] = value;
				}
			}
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0005F818 File Offset: 0x0005DA18
		internal void Render(CssTextWriter writer)
		{
			if (this._table != null && this._table.Count > 0)
			{
				foreach (object obj in this._table)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					writer.WriteAttribute((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
				}
			}
			if (this._intTable != null && this._intTable.Count > 0)
			{
				foreach (object obj2 in this._intTable)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					writer.WriteAttribute((HtmlTextWriterStyle)dictionaryEntry2.Key, (string)dictionaryEntry2.Value);
				}
			}
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0005F914 File Offset: 0x0005DB14
		internal void Render(HtmlTextWriter writer)
		{
			if (this._table != null && this._table.Count > 0)
			{
				foreach (object obj in this._table)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					writer.AddStyleAttribute((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
				}
			}
			if (this._intTable != null && this._intTable.Count > 0)
			{
				foreach (object obj2 in this._intTable)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					writer.AddStyleAttribute((HtmlTextWriterStyle)dictionaryEntry2.Key, (string)dictionaryEntry2.Value);
				}
			}
		}

		// Token: 0x04001953 RID: 6483
		private static readonly Regex _styleAttribRegex = new Regex("\\G(\\s*(;\\s*)*(?<stylename>[^:]+?)\\s*:\\s*(?<styleval>[^;]*))*\\s*(;\\s*)*$", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Singleline);

		// Token: 0x04001954 RID: 6484
		private StateBag _state;

		// Token: 0x04001955 RID: 6485
		private string _style;

		// Token: 0x04001956 RID: 6486
		private IDictionary _table;

		// Token: 0x04001957 RID: 6487
		private IDictionary _intTable;
	}
}
