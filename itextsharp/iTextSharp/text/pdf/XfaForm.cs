using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace iTextSharp.text.pdf
{
	// Token: 0x020005C6 RID: 1478
	public class XfaForm
	{
		// Token: 0x060032CF RID: 13007 RVA: 0x0013C18E File Offset: 0x0013B18E
		public XfaForm()
		{
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x0013C198 File Offset: 0x0013B198
		public static PdfObject GetXfaObject(PdfReader reader)
		{
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(reader.Catalog.Get(PdfName.ACROFORM));
			if (pdfDictionary == null)
			{
				return null;
			}
			return PdfReader.GetPdfObjectRelease(pdfDictionary.Get(PdfName.XFA));
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x0013C1D8 File Offset: 0x0013B1D8
		public XfaForm(PdfReader reader)
		{
			this.reader = reader;
			PdfObject xfaObject = XfaForm.GetXfaObject(reader);
			if (xfaObject == null)
			{
				this.xfaPresent = false;
				return;
			}
			this.xfaPresent = true;
			MemoryStream memoryStream = new MemoryStream();
			if (xfaObject.IsArray())
			{
				PdfArray pdfArray = (PdfArray)xfaObject;
				for (int i = 1; i < pdfArray.Size; i += 2)
				{
					PdfObject directObject = pdfArray.GetDirectObject(i);
					if (directObject is PRStream)
					{
						byte[] streamBytes = PdfReader.GetStreamBytes((PRStream)directObject);
						memoryStream.Write(streamBytes, 0, streamBytes.Length);
					}
				}
			}
			else if (xfaObject is PRStream)
			{
				byte[] streamBytes2 = PdfReader.GetStreamBytes((PRStream)xfaObject);
				memoryStream.Write(streamBytes2, 0, streamBytes2.Length);
			}
			memoryStream.Seek(0L, SeekOrigin.Begin);
			XmlTextReader xmlTextReader = new XmlTextReader(memoryStream);
			this.domDocument = new XmlDocument();
			this.domDocument.PreserveWhitespace = true;
			this.domDocument.Load(xmlTextReader);
			this.ExtractNodes();
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x0013C2C0 File Offset: 0x0013B2C0
		private void ExtractNodes()
		{
			XmlNode xmlNode = this.domDocument.FirstChild;
			while (xmlNode.NodeType != XmlNodeType.Element || xmlNode.ChildNodes.Count == 0)
			{
				xmlNode = xmlNode.NextSibling;
			}
			for (xmlNode = xmlNode.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					string localName = xmlNode.LocalName;
					if ("template".Equals(localName))
					{
						this.templateNode = xmlNode;
						this.templateSom = new XfaForm.Xml2SomTemplate(xmlNode);
					}
					else if ("datasets".Equals(localName))
					{
						this.datasetsNode = xmlNode;
						this.datasetsSom = new XfaForm.Xml2SomDatasets(xmlNode.FirstChild);
					}
				}
			}
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x0013C364 File Offset: 0x0013B364
		public static void SetXfa(XfaForm form, PdfReader reader, PdfWriter writer)
		{
			PdfDictionary pdfDictionary = (PdfDictionary)PdfReader.GetPdfObjectRelease(reader.Catalog.Get(PdfName.ACROFORM));
			if (pdfDictionary == null)
			{
				return;
			}
			PdfObject xfaObject = XfaForm.GetXfaObject(reader);
			if (xfaObject.IsArray())
			{
				PdfArray pdfArray = (PdfArray)xfaObject;
				int num = -1;
				int num2 = -1;
				for (int i = 0; i < pdfArray.Size; i += 2)
				{
					PdfString asString = pdfArray.GetAsString(i);
					if ("template".Equals(asString.ToString()))
					{
						num = i + 1;
					}
					if ("datasets".Equals(asString.ToString()))
					{
						num2 = i + 1;
					}
				}
				if (num > -1 && num2 > -1)
				{
					reader.KillXref(pdfArray.GetAsIndirectObject(num));
					reader.KillXref(pdfArray.GetAsIndirectObject(num2));
					PdfStream pdfStream = new PdfStream(XfaForm.SerializeDoc(form.templateNode));
					pdfStream.FlateCompress(writer.CompressionLevel);
					pdfArray[num] = writer.AddToBody(pdfStream).IndirectReference;
					PdfStream pdfStream2 = new PdfStream(XfaForm.SerializeDoc(form.datasetsNode));
					pdfStream2.FlateCompress(writer.CompressionLevel);
					pdfArray[num2] = writer.AddToBody(pdfStream2).IndirectReference;
					pdfDictionary.Put(PdfName.XFA, new PdfArray(pdfArray));
					return;
				}
			}
			reader.KillXref(pdfDictionary.Get(PdfName.XFA));
			PdfStream pdfStream3 = new PdfStream(XfaForm.SerializeDoc(form.domDocument));
			pdfStream3.FlateCompress(writer.CompressionLevel);
			PdfIndirectReference indirectReference = writer.AddToBody(pdfStream3).IndirectReference;
			pdfDictionary.Put(PdfName.XFA, indirectReference);
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x0013C4ED File Offset: 0x0013B4ED
		public void SetXfa(PdfWriter writer)
		{
			XfaForm.SetXfa(this, this.reader, writer);
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x0013C4FC File Offset: 0x0013B4FC
		public static byte[] SerializeDoc(XmlNode n)
		{
			MemoryStream memoryStream = new MemoryStream();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, new UTF8Encoding(false));
			xmlTextWriter.WriteNode(new XmlNodeReader(n), true);
			xmlTextWriter.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x060032D6 RID: 13014 RVA: 0x0013C535 File Offset: 0x0013B535
		// (set) Token: 0x060032D7 RID: 13015 RVA: 0x0013C53D File Offset: 0x0013B53D
		public bool XfaPresent
		{
			get
			{
				return this.xfaPresent;
			}
			set
			{
				this.xfaPresent = value;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x060032D8 RID: 13016 RVA: 0x0013C546 File Offset: 0x0013B546
		// (set) Token: 0x060032D9 RID: 13017 RVA: 0x0013C54E File Offset: 0x0013B54E
		public XmlDocument DomDocument
		{
			get
			{
				return this.domDocument;
			}
			set
			{
				this.domDocument = value;
				this.ExtractNodes();
			}
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x0013C560 File Offset: 0x0013B560
		public string FindFieldName(string name, AcroFields af)
		{
			Dictionary<string, AcroFields.Item> fields = af.Fields;
			if (fields.ContainsKey(name))
			{
				return name;
			}
			if (this.acroFieldsSom == null)
			{
				if (fields.Count == 0 && this.xfaPresent)
				{
					this.acroFieldsSom = new XfaForm.AcroFieldsSearch(this.datasetsSom.Name2Node.Keys);
				}
				else
				{
					this.acroFieldsSom = new XfaForm.AcroFieldsSearch(fields.Keys);
				}
			}
			if (this.acroFieldsSom.AcroShort2LongName.ContainsKey(name))
			{
				return this.acroFieldsSom.AcroShort2LongName[name];
			}
			return this.acroFieldsSom.InverseSearchGlobal(XfaForm.Xml2Som.SplitParts(name));
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x0013C5FB File Offset: 0x0013B5FB
		public string FindDatasetsName(string name)
		{
			if (this.datasetsSom.Name2Node.ContainsKey(name))
			{
				return name;
			}
			return this.datasetsSom.InverseSearchGlobal(XfaForm.Xml2Som.SplitParts(name));
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x0013C623 File Offset: 0x0013B623
		public XmlNode FindDatasetsNode(string name)
		{
			if (name == null)
			{
				return null;
			}
			name = this.FindDatasetsName(name);
			if (name == null)
			{
				return null;
			}
			return this.datasetsSom.Name2Node[name];
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x0013C649 File Offset: 0x0013B649
		public static string GetNodeText(XmlNode n)
		{
			if (n == null)
			{
				return "";
			}
			return XfaForm.GetNodeText(n, "");
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x0013C660 File Offset: 0x0013B660
		private static string GetNodeText(XmlNode n, string name)
		{
			for (XmlNode xmlNode = n.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					name = XfaForm.GetNodeText(xmlNode, name);
				}
				else if (xmlNode.NodeType == XmlNodeType.Text)
				{
					name += xmlNode.Value;
				}
			}
			return name;
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x0013C6AC File Offset: 0x0013B6AC
		public void SetNodeText(XmlNode n, string text)
		{
			if (n == null)
			{
				return;
			}
			XmlNode firstChild;
			while ((firstChild = n.FirstChild) != null)
			{
				n.RemoveChild(firstChild);
			}
			n.Attributes.RemoveNamedItem("dataNode", "http://www.xfa.org/schema/xfa-data/1.0/");
			n.AppendChild(this.domDocument.CreateTextNode(text));
			this.changed = true;
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x060032E1 RID: 13025 RVA: 0x0013C70C File Offset: 0x0013B70C
		// (set) Token: 0x060032E0 RID: 13024 RVA: 0x0013C703 File Offset: 0x0013B703
		public PdfReader Reader
		{
			get
			{
				return this.reader;
			}
			set
			{
				this.reader = value;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060032E2 RID: 13026 RVA: 0x0013C714 File Offset: 0x0013B714
		// (set) Token: 0x060032E3 RID: 13027 RVA: 0x0013C71C File Offset: 0x0013B71C
		public bool Changed
		{
			get
			{
				return this.changed;
			}
			set
			{
				this.changed = value;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060032E4 RID: 13028 RVA: 0x0013C725 File Offset: 0x0013B725
		// (set) Token: 0x060032E5 RID: 13029 RVA: 0x0013C72D File Offset: 0x0013B72D
		public XfaForm.Xml2SomTemplate TemplateSom
		{
			get
			{
				return this.templateSom;
			}
			set
			{
				this.templateSom = value;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060032E6 RID: 13030 RVA: 0x0013C736 File Offset: 0x0013B736
		// (set) Token: 0x060032E7 RID: 13031 RVA: 0x0013C73E File Offset: 0x0013B73E
		public XfaForm.Xml2SomDatasets DatasetsSom
		{
			get
			{
				return this.datasetsSom;
			}
			set
			{
				this.datasetsSom = value;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060032E8 RID: 13032 RVA: 0x0013C747 File Offset: 0x0013B747
		// (set) Token: 0x060032E9 RID: 13033 RVA: 0x0013C74F File Offset: 0x0013B74F
		public XfaForm.AcroFieldsSearch AcroFieldsSom
		{
			get
			{
				return this.acroFieldsSom;
			}
			set
			{
				this.acroFieldsSom = value;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x0013C758 File Offset: 0x0013B758
		public XmlNode DatasetsNode
		{
			get
			{
				return this.datasetsNode;
			}
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x0013C760 File Offset: 0x0013B760
		public void FillXfaForm(string file)
		{
			using (FileStream fileStream = new FileStream(file, FileMode.Open))
			{
				this.FillXfaForm(fileStream);
			}
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x0013C798 File Offset: 0x0013B798
		public void FillXfaForm(Stream stream)
		{
			this.FillXfaForm(new XmlTextReader(stream));
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x0013C7A8 File Offset: 0x0013B7A8
		public void FillXfaForm(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			xmlDocument.Load(reader);
			this.FillXfaForm(xmlDocument.DocumentElement);
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x0013C7D8 File Offset: 0x0013B7D8
		public void FillXfaForm(XmlNode node)
		{
			XmlNode firstChild = this.datasetsNode.FirstChild;
			XmlNodeList childNodes = firstChild.ChildNodes;
			if (childNodes.Count == 0)
			{
				firstChild.AppendChild(this.domDocument.ImportNode(node, true));
			}
			else
			{
				firstChild.ReplaceChild(this.domDocument.ImportNode(node, true), firstChild.FirstChild);
			}
			this.ExtractNodes();
			this.Changed = true;
		}

		// Token: 0x0400229A RID: 8858
		public const string XFA_DATA_SCHEMA = "http://www.xfa.org/schema/xfa-data/1.0/";

		// Token: 0x0400229B RID: 8859
		private XfaForm.Xml2SomTemplate templateSom;

		// Token: 0x0400229C RID: 8860
		private XmlNode templateNode;

		// Token: 0x0400229D RID: 8861
		private XfaForm.Xml2SomDatasets datasetsSom;

		// Token: 0x0400229E RID: 8862
		private XfaForm.AcroFieldsSearch acroFieldsSom;

		// Token: 0x0400229F RID: 8863
		private PdfReader reader;

		// Token: 0x040022A0 RID: 8864
		private bool xfaPresent;

		// Token: 0x040022A1 RID: 8865
		private XmlDocument domDocument;

		// Token: 0x040022A2 RID: 8866
		private bool changed;

		// Token: 0x040022A3 RID: 8867
		private XmlNode datasetsNode;

		// Token: 0x020005C7 RID: 1479
		public class InverseStore
		{
			// Token: 0x170008C9 RID: 2249
			// (get) Token: 0x060032EF RID: 13039 RVA: 0x0013C840 File Offset: 0x0013B840
			public string DefaultName
			{
				get
				{
					XfaForm.InverseStore inverseStore = this;
					object obj;
					for (;;)
					{
						obj = inverseStore.follow[0];
						if (obj is string)
						{
							break;
						}
						inverseStore = (XfaForm.InverseStore)obj;
					}
					return (string)obj;
				}
			}

			// Token: 0x060032F0 RID: 13040 RVA: 0x0013C874 File Offset: 0x0013B874
			public bool IsSimilar(string name)
			{
				int num = name.IndexOf('[');
				name = name.Substring(0, num + 1);
				foreach (string text in this.part)
				{
					if (text.StartsWith(name))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x040022A4 RID: 8868
			protected internal List<string> part = new List<string>();

			// Token: 0x040022A5 RID: 8869
			protected internal List<object> follow = new List<object>();
		}

		// Token: 0x020005C8 RID: 1480
		public class Stack2<T> : List<T>
		{
			// Token: 0x060032F2 RID: 13042 RVA: 0x0013C906 File Offset: 0x0013B906
			public T Peek()
			{
				if (base.Count == 0)
				{
					throw new InvalidOperationException();
				}
				return base[base.Count - 1];
			}

			// Token: 0x060032F3 RID: 13043 RVA: 0x0013C924 File Offset: 0x0013B924
			public T Pop()
			{
				if (base.Count == 0)
				{
					throw new InvalidOperationException();
				}
				T result = base[base.Count - 1];
				base.RemoveAt(base.Count - 1);
				return result;
			}

			// Token: 0x060032F4 RID: 13044 RVA: 0x0013C95D File Offset: 0x0013B95D
			public T Push(T item)
			{
				base.Add(item);
				return item;
			}

			// Token: 0x060032F5 RID: 13045 RVA: 0x0013C967 File Offset: 0x0013B967
			public bool Empty()
			{
				return base.Count == 0;
			}
		}

		// Token: 0x020005C9 RID: 1481
		public class Xml2Som
		{
			// Token: 0x060032F7 RID: 13047 RVA: 0x0013C97C File Offset: 0x0013B97C
			public static string EscapeSom(string s)
			{
				if (s == null)
				{
					return "";
				}
				int i = s.IndexOf('.');
				if (i < 0)
				{
					return s;
				}
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				while (i >= 0)
				{
					stringBuilder.Append(s.Substring(num, i - num));
					stringBuilder.Append('\\');
					num = i;
					i = s.IndexOf('.', i + 1);
				}
				stringBuilder.Append(s.Substring(num));
				return stringBuilder.ToString();
			}

			// Token: 0x060032F8 RID: 13048 RVA: 0x0013C9EC File Offset: 0x0013B9EC
			public static string UnescapeSom(string s)
			{
				int i = s.IndexOf('\\');
				if (i < 0)
				{
					return s;
				}
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				while (i >= 0)
				{
					stringBuilder.Append(s.Substring(num, i - num));
					num = i + 1;
					i = s.IndexOf('\\', i + 1);
				}
				stringBuilder.Append(s.Substring(num));
				return stringBuilder.ToString();
			}

			// Token: 0x060032F9 RID: 13049 RVA: 0x0013CA4C File Offset: 0x0013BA4C
			protected string PrintStack()
			{
				if (this.stack.Empty())
				{
					return "";
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in this.stack)
				{
					stringBuilder.Append('.').Append(value);
				}
				return stringBuilder.ToString(1, stringBuilder.Length - 1);
			}

			// Token: 0x060032FA RID: 13050 RVA: 0x0013CAD0 File Offset: 0x0013BAD0
			public static string GetShortName(string s)
			{
				int i = s.IndexOf(".#subform[");
				if (i < 0)
				{
					return s;
				}
				int num = 0;
				StringBuilder stringBuilder = new StringBuilder();
				while (i >= 0)
				{
					stringBuilder.Append(s.Substring(num, i - num));
					i = s.IndexOf("]", i + 10);
					if (i < 0)
					{
						return stringBuilder.ToString();
					}
					num = i + 1;
					i = s.IndexOf(".#subform[", num);
				}
				stringBuilder.Append(s.Substring(num));
				return stringBuilder.ToString();
			}

			// Token: 0x060032FB RID: 13051 RVA: 0x0013CB4E File Offset: 0x0013BB4E
			public void InverseSearchAdd(string unstack)
			{
				XfaForm.Xml2Som.InverseSearchAdd(this.inverseSearch, this.stack, unstack);
			}

			// Token: 0x060032FC RID: 13052 RVA: 0x0013CB64 File Offset: 0x0013BB64
			public static void InverseSearchAdd(Dictionary<string, XfaForm.InverseStore> inverseSearch, XfaForm.Stack2<string> stack, string unstack)
			{
				string text = stack.Peek();
				XfaForm.InverseStore inverseStore;
				inverseSearch.TryGetValue(text, out inverseStore);
				if (inverseStore == null)
				{
					inverseStore = new XfaForm.InverseStore();
					inverseSearch[text] = inverseStore;
				}
				for (int i = stack.Count - 2; i >= 0; i--)
				{
					text = stack[i];
					int num = inverseStore.part.IndexOf(text);
					XfaForm.InverseStore inverseStore2;
					if (num < 0)
					{
						inverseStore.part.Add(text);
						inverseStore2 = new XfaForm.InverseStore();
						inverseStore.follow.Add(inverseStore2);
					}
					else
					{
						inverseStore2 = (XfaForm.InverseStore)inverseStore.follow[num];
					}
					inverseStore = inverseStore2;
				}
				inverseStore.part.Add("");
				inverseStore.follow.Add(unstack);
			}

			// Token: 0x060032FD RID: 13053 RVA: 0x0013CC14 File Offset: 0x0013BC14
			public string InverseSearchGlobal(List<string> parts)
			{
				if (parts.Count == 0)
				{
					return null;
				}
				XfaForm.InverseStore inverseStore;
				this.inverseSearch.TryGetValue(parts[parts.Count - 1], out inverseStore);
				if (inverseStore == null)
				{
					return null;
				}
				int i = parts.Count - 2;
				while (i >= 0)
				{
					string text = parts[i];
					int num = inverseStore.part.IndexOf(text);
					if (num < 0)
					{
						if (inverseStore.IsSimilar(text))
						{
							return null;
						}
						return inverseStore.DefaultName;
					}
					else
					{
						inverseStore = (XfaForm.InverseStore)inverseStore.follow[num];
						i--;
					}
				}
				return inverseStore.DefaultName;
			}

			// Token: 0x060032FE RID: 13054 RVA: 0x0013CCA4 File Offset: 0x0013BCA4
			public static XfaForm.Stack2<string> SplitParts(string name)
			{
				while (name.StartsWith("."))
				{
					name = name.Substring(1);
				}
				XfaForm.Stack2<string> stack = new XfaForm.Stack2<string>();
				int num = 0;
				string text;
				for (;;)
				{
					int num2 = num;
					for (;;)
					{
						num2 = name.IndexOf('.', num2);
						if (num2 < 0 || name[num2 - 1] != '\\')
						{
							break;
						}
						num2++;
					}
					if (num2 < 0)
					{
						break;
					}
					text = name.Substring(num, num2 - num);
					if (!text.EndsWith("]"))
					{
						text += "[0]";
					}
					stack.Add(text);
					num = num2 + 1;
				}
				text = name.Substring(num);
				if (!text.EndsWith("]"))
				{
					text += "[0]";
				}
				stack.Add(text);
				return stack;
			}

			// Token: 0x170008CA RID: 2250
			// (get) Token: 0x060032FF RID: 13055 RVA: 0x0013CD54 File Offset: 0x0013BD54
			// (set) Token: 0x06003300 RID: 13056 RVA: 0x0013CD5C File Offset: 0x0013BD5C
			public List<string> Order
			{
				get
				{
					return this.order;
				}
				set
				{
					this.order = value;
				}
			}

			// Token: 0x170008CB RID: 2251
			// (get) Token: 0x06003301 RID: 13057 RVA: 0x0013CD65 File Offset: 0x0013BD65
			// (set) Token: 0x06003302 RID: 13058 RVA: 0x0013CD6D File Offset: 0x0013BD6D
			public Dictionary<string, XmlNode> Name2Node
			{
				get
				{
					return this.name2Node;
				}
				set
				{
					this.name2Node = value;
				}
			}

			// Token: 0x170008CC RID: 2252
			// (get) Token: 0x06003303 RID: 13059 RVA: 0x0013CD76 File Offset: 0x0013BD76
			// (set) Token: 0x06003304 RID: 13060 RVA: 0x0013CD7E File Offset: 0x0013BD7E
			public Dictionary<string, XfaForm.InverseStore> InverseSearch
			{
				get
				{
					return this.inverseSearch;
				}
				set
				{
					this.inverseSearch = value;
				}
			}

			// Token: 0x040022A6 RID: 8870
			protected List<string> order;

			// Token: 0x040022A7 RID: 8871
			protected Dictionary<string, XmlNode> name2Node;

			// Token: 0x040022A8 RID: 8872
			protected Dictionary<string, XfaForm.InverseStore> inverseSearch;

			// Token: 0x040022A9 RID: 8873
			protected XfaForm.Stack2<string> stack;

			// Token: 0x040022AA RID: 8874
			protected int anform;
		}

		// Token: 0x020005CA RID: 1482
		public class Xml2SomDatasets : XfaForm.Xml2Som
		{
			// Token: 0x06003306 RID: 13062 RVA: 0x0013CD90 File Offset: 0x0013BD90
			public Xml2SomDatasets(XmlNode n)
			{
				this.order = new List<string>();
				this.name2Node = new Dictionary<string, XmlNode>();
				this.stack = new XfaForm.Stack2<string>();
				this.anform = 0;
				this.inverseSearch = new Dictionary<string, XfaForm.InverseStore>();
				this.ProcessDatasetsInternal(n);
			}

			// Token: 0x06003307 RID: 13063 RVA: 0x0013CDE0 File Offset: 0x0013BDE0
			public XmlNode InsertNode(XmlNode n, string shortName)
			{
				XfaForm.Stack2<string> stack = XfaForm.Xml2Som.SplitParts(shortName);
				XmlDocument ownerDocument = n.OwnerDocument;
				XmlNode xmlNode = null;
				n = n.FirstChild;
				while (n.NodeType != XmlNodeType.Element)
				{
					n = n.NextSibling;
				}
				int i = 0;
				IL_106:
				while (i < stack.Count)
				{
					string text = stack[i];
					int num = text.LastIndexOf('[');
					string text2 = text.Substring(0, num);
					num = int.Parse(text.Substring(num + 1, text.Length - num - 2));
					int j = -1;
					for (xmlNode = n.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
					{
						if (xmlNode.NodeType == XmlNodeType.Element)
						{
							string text3 = XfaForm.Xml2Som.EscapeSom(xmlNode.LocalName);
							if (text3.Equals(text2))
							{
								j++;
								if (j == num)
								{
									IL_F9:
									while (j < num)
									{
										xmlNode = ownerDocument.CreateElement(text2);
										xmlNode = n.AppendChild(xmlNode);
										XmlNode xmlNode2 = ownerDocument.CreateNode(XmlNodeType.Attribute, "dataNode", "http://www.xfa.org/schema/xfa-data/1.0/");
										xmlNode2.Value = "dataGroup";
										xmlNode.Attributes.SetNamedItem(xmlNode2);
										j++;
									}
									n = xmlNode;
									i++;
									goto IL_106;
								}
							}
						}
					}
					goto IL_F9;
				}
				XfaForm.Xml2Som.InverseSearchAdd(this.inverseSearch, stack, shortName);
				this.name2Node[shortName] = xmlNode;
				this.order.Add(shortName);
				return xmlNode;
			}

			// Token: 0x06003308 RID: 13064 RVA: 0x0013CF28 File Offset: 0x0013BF28
			private static bool HasChildren(XmlNode n)
			{
				XmlNode namedItem = n.Attributes.GetNamedItem("dataNode", "http://www.xfa.org/schema/xfa-data/1.0/");
				if (namedItem != null)
				{
					string value = namedItem.Value;
					if ("dataGroup".Equals(value))
					{
						return true;
					}
					if ("dataValue".Equals(value))
					{
						return false;
					}
				}
				if (!n.HasChildNodes)
				{
					return false;
				}
				for (XmlNode xmlNode = n.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06003309 RID: 13065 RVA: 0x0013CF9C File Offset: 0x0013BF9C
			private void ProcessDatasetsInternal(XmlNode n)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				for (XmlNode xmlNode = n.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						string text = XfaForm.Xml2Som.EscapeSom(xmlNode.LocalName);
						int value;
						if (!dictionary.ContainsKey(text))
						{
							value = 0;
						}
						else
						{
							value = dictionary[text] + 1;
						}
						dictionary[text] = value;
						if (XfaForm.Xml2SomDatasets.HasChildren(xmlNode))
						{
							this.stack.Push(text + "[" + value.ToString() + "]");
							this.ProcessDatasetsInternal(xmlNode);
							this.stack.Pop();
						}
						else
						{
							this.stack.Push(text + "[" + value.ToString() + "]");
							string text2 = base.PrintStack();
							this.order.Add(text2);
							base.InverseSearchAdd(text2);
							this.name2Node[text2] = xmlNode;
							this.stack.Pop();
						}
					}
				}
			}
		}

		// Token: 0x020005CB RID: 1483
		public class AcroFieldsSearch : XfaForm.Xml2Som
		{
			// Token: 0x0600330A RID: 13066 RVA: 0x0013D09C File Offset: 0x0013C09C
			public AcroFieldsSearch(ICollection<string> items)
			{
				this.inverseSearch = new Dictionary<string, XfaForm.InverseStore>();
				this.acroShort2LongName = new Dictionary<string, string>();
				foreach (string text in items)
				{
					string shortName = XfaForm.Xml2Som.GetShortName(text);
					this.acroShort2LongName[shortName] = text;
					XfaForm.Xml2Som.InverseSearchAdd(this.inverseSearch, XfaForm.Xml2Som.SplitParts(shortName), text);
				}
			}

			// Token: 0x170008CD RID: 2253
			// (get) Token: 0x0600330B RID: 13067 RVA: 0x0013D120 File Offset: 0x0013C120
			// (set) Token: 0x0600330C RID: 13068 RVA: 0x0013D128 File Offset: 0x0013C128
			public Dictionary<string, string> AcroShort2LongName
			{
				get
				{
					return this.acroShort2LongName;
				}
				set
				{
					this.acroShort2LongName = value;
				}
			}

			// Token: 0x040022AB RID: 8875
			private Dictionary<string, string> acroShort2LongName;
		}

		// Token: 0x020005CC RID: 1484
		public class Xml2SomTemplate : XfaForm.Xml2Som
		{
			// Token: 0x0600330D RID: 13069 RVA: 0x0013D134 File Offset: 0x0013C134
			public Xml2SomTemplate(XmlNode n)
			{
				this.order = new List<string>();
				this.name2Node = new Dictionary<string, XmlNode>();
				this.stack = new XfaForm.Stack2<string>();
				this.anform = 0;
				this.templateLevel = 0;
				this.inverseSearch = new Dictionary<string, XfaForm.InverseStore>();
				this.ProcessTemplate(n, null);
			}

			// Token: 0x0600330E RID: 13070 RVA: 0x0013D18C File Offset: 0x0013C18C
			public string GetFieldType(string s)
			{
				XmlNode xmlNode;
				this.name2Node.TryGetValue(s, out xmlNode);
				if (xmlNode == null)
				{
					return null;
				}
				if ("exclGroup".Equals(xmlNode.LocalName))
				{
					return "exclGroup";
				}
				XmlNode xmlNode2 = xmlNode.FirstChild;
				while (xmlNode2 != null && (xmlNode2.NodeType != XmlNodeType.Element || !"ui".Equals(xmlNode2.LocalName)))
				{
					xmlNode2 = xmlNode2.NextSibling;
				}
				if (xmlNode2 == null)
				{
					return null;
				}
				for (XmlNode xmlNode3 = xmlNode2.FirstChild; xmlNode3 != null; xmlNode3 = xmlNode3.NextSibling)
				{
					if (xmlNode3.NodeType == XmlNodeType.Element && (!"extras".Equals(xmlNode3.LocalName) || !"picture".Equals(xmlNode3.LocalName)))
					{
						return xmlNode3.LocalName;
					}
				}
				return null;
			}

			// Token: 0x0600330F RID: 13071 RVA: 0x0013D240 File Offset: 0x0013C240
			private void ProcessTemplate(XmlNode n, Dictionary<string, int> ff)
			{
				if (ff == null)
				{
					ff = new Dictionary<string, int>();
				}
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				for (XmlNode xmlNode = n.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						string localName = xmlNode.LocalName;
						if ("subform".Equals(localName))
						{
							XmlNode namedItem = xmlNode.Attributes.GetNamedItem("name");
							string text = "#subform";
							bool flag = true;
							if (namedItem != null)
							{
								text = XfaForm.Xml2Som.EscapeSom(namedItem.Value);
								flag = false;
							}
							int value;
							if (flag)
							{
								value = this.anform;
								this.anform++;
							}
							else
							{
								if (!dictionary.ContainsKey(text))
								{
									value = 0;
								}
								else
								{
									value = dictionary[text] + 1;
								}
								dictionary[text] = value;
							}
							this.stack.Push(text + "[" + value.ToString() + "]");
							this.templateLevel++;
							if (flag)
							{
								this.ProcessTemplate(xmlNode, ff);
							}
							else
							{
								this.ProcessTemplate(xmlNode, null);
							}
							this.templateLevel--;
							this.stack.Pop();
						}
						else if ("field".Equals(localName) || "exclGroup".Equals(localName))
						{
							XmlNode namedItem2 = xmlNode.Attributes.GetNamedItem("name");
							if (namedItem2 != null)
							{
								string text2 = XfaForm.Xml2Som.EscapeSom(namedItem2.Value);
								int value2;
								if (!ff.ContainsKey(text2))
								{
									value2 = 0;
								}
								else
								{
									value2 = ff[text2] + 1;
								}
								ff[text2] = value2;
								this.stack.Push(text2 + "[" + value2.ToString() + "]");
								string text3 = base.PrintStack();
								this.order.Add(text3);
								base.InverseSearchAdd(text3);
								this.name2Node[text3] = xmlNode;
								this.stack.Pop();
							}
						}
						else if (!this.dynamicForm && this.templateLevel > 0 && "occur".Equals(localName))
						{
							int num = 1;
							int num2 = 1;
							int num3 = 1;
							XmlNode namedItem3 = xmlNode.Attributes.GetNamedItem("initial");
							if (namedItem3 != null)
							{
								try
								{
									num = int.Parse(namedItem3.Value.Trim());
								}
								catch
								{
								}
							}
							namedItem3 = xmlNode.Attributes.GetNamedItem("min");
							if (namedItem3 != null)
							{
								try
								{
									num2 = int.Parse(namedItem3.Value.Trim());
								}
								catch
								{
								}
							}
							namedItem3 = xmlNode.Attributes.GetNamedItem("max");
							if (namedItem3 != null)
							{
								try
								{
									num3 = int.Parse(namedItem3.Value.Trim());
								}
								catch
								{
								}
							}
							if (num != num2 || num2 != num3)
							{
								this.dynamicForm = true;
							}
						}
					}
				}
			}

			// Token: 0x170008CE RID: 2254
			// (get) Token: 0x06003310 RID: 13072 RVA: 0x0013D530 File Offset: 0x0013C530
			// (set) Token: 0x06003311 RID: 13073 RVA: 0x0013D538 File Offset: 0x0013C538
			public bool DynamicForm
			{
				get
				{
					return this.dynamicForm;
				}
				set
				{
					this.dynamicForm = value;
				}
			}

			// Token: 0x040022AC RID: 8876
			private bool dynamicForm;

			// Token: 0x040022AD RID: 8877
			private int templateLevel;
		}
	}
}
