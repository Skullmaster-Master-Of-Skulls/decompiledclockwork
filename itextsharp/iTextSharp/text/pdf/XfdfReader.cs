using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text.error_messages;
using iTextSharp.text.xml.simpleparser;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200026C RID: 620
	public class XfdfReader : ISimpleXMLDocHandler
	{
		// Token: 0x06001744 RID: 5956 RVA: 0x00085E94 File Offset: 0x00084E94
		public XfdfReader(string filename)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
				SimpleXMLParser.Parse(this, fileStream);
			}
			finally
			{
				try
				{
					if (fileStream != null)
					{
						fileStream.Close();
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00085F00 File Offset: 0x00084F00
		public XfdfReader(byte[] xfdfIn) : this(new MemoryStream(xfdfIn))
		{
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00085F0E File Offset: 0x00084F0E
		public XfdfReader(Stream isp)
		{
			SimpleXMLParser.Parse(this, isp);
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x00085F33 File Offset: 0x00084F33
		public Dictionary<string, string> Fields
		{
			get
			{
				return this.fields;
			}
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00085F3B File Offset: 0x00084F3B
		public string GetField(string name)
		{
			if (this.fields.ContainsKey(name))
			{
				return this.fields[name];
			}
			return null;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00085F59 File Offset: 0x00084F59
		public string GetFieldValue(string name)
		{
			return this.GetField(name);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00085F62 File Offset: 0x00084F62
		public List<string> GetListValues(string name)
		{
			if (this.listFields.ContainsKey(name))
			{
				return this.listFields[name];
			}
			return null;
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00085F80 File Offset: 0x00084F80
		public string FileSpec
		{
			get
			{
				return this.fileSpec;
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00085F88 File Offset: 0x00084F88
		public void StartElement(string tag, Dictionary<string, string> h)
		{
			if (!this.foundRoot)
			{
				if (!tag.Equals("xfdf"))
				{
					throw new Exception(MessageLocalization.GetComposedMessage("root.element.is.not.xfdf.1", tag));
				}
				this.foundRoot = true;
			}
			if (tag.Equals("xfdf"))
			{
				return;
			}
			if (tag.Equals("f"))
			{
				h.TryGetValue("href", out this.fileSpec);
				return;
			}
			if (tag.Equals("fields"))
			{
				this.fields = new Dictionary<string, string>();
				this.listFields = new Dictionary<string, List<string>>();
				return;
			}
			if (tag.Equals("field"))
			{
				string obj;
				h.TryGetValue("name", out obj);
				this.fieldNames.Push(obj);
				return;
			}
			if (tag.Equals("value"))
			{
				this.fieldValues.Push("");
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0008605C File Offset: 0x0008505C
		public void EndElement(string tag)
		{
			if (tag.Equals("value"))
			{
				string text = "";
				for (int i = 0; i < this.fieldNames.Count; i++)
				{
					text = text + "." + this.fieldNames[i];
				}
				if (text.StartsWith("."))
				{
					text = text.Substring(1);
				}
				string text2 = this.fieldValues.Pop();
				string text3;
				this.fields.TryGetValue(text, out text3);
				this.fields[text] = text2;
				if (text3 != null)
				{
					List<string> list;
					this.listFields.TryGetValue(text, out list);
					if (list == null)
					{
						list = new List<string>();
						list.Add(text3);
					}
					list.Add(text2);
					this.listFields[text] = list;
					return;
				}
			}
			else if (tag.Equals("field") && this.fieldNames.Count != 0)
			{
				this.fieldNames.Pop();
			}
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0008614C File Offset: 0x0008514C
		public void StartDocument()
		{
			this.fileSpec = "";
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x00086159 File Offset: 0x00085159
		public void EndDocument()
		{
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0008615C File Offset: 0x0008515C
		public void Text(string str)
		{
			if (this.fieldNames.Count == 0 || this.fieldValues.Count == 0)
			{
				return;
			}
			string text = this.fieldValues.Pop();
			text += str;
			this.fieldValues.Push(text);
		}

		// Token: 0x04000FFA RID: 4090
		private bool foundRoot;

		// Token: 0x04000FFB RID: 4091
		private XfdfReader.Stackr fieldNames = new XfdfReader.Stackr();

		// Token: 0x04000FFC RID: 4092
		private XfdfReader.Stackr fieldValues = new XfdfReader.Stackr();

		// Token: 0x04000FFD RID: 4093
		internal Dictionary<string, string> fields;

		// Token: 0x04000FFE RID: 4094
		protected Dictionary<string, List<string>> listFields;

		// Token: 0x04000FFF RID: 4095
		internal string fileSpec;

		// Token: 0x0200026D RID: 621
		internal class Stackr : List<string>
		{
			// Token: 0x06001751 RID: 5969 RVA: 0x000861A4 File Offset: 0x000851A4
			internal void Push(string obj)
			{
				base.Add(obj);
			}

			// Token: 0x06001752 RID: 5970 RVA: 0x000861B0 File Offset: 0x000851B0
			internal string Pop()
			{
				if (base.Count == 0)
				{
					throw new InvalidOperationException(MessageLocalization.GetComposedMessage("the.stack.is.empty"));
				}
				string result = base[base.Count - 1];
				base.RemoveAt(base.Count - 1);
				return result;
			}
		}
	}
}
