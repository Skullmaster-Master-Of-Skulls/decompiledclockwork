using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace System.Xml.Linq
{
	// Token: 0x0200002B RID: 43
	[__DynamicallyInvokable]
	public class XStreamingElement
	{
		// Token: 0x0600020F RID: 527 RVA: 0x00009496 File Offset: 0x00007696
		[__DynamicallyInvokable]
		public XStreamingElement(XName name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000094B9 File Offset: 0x000076B9
		[__DynamicallyInvokable]
		public XStreamingElement(XName name, object content) : this(name)
		{
			object obj;
			if (!(content is List<object>))
			{
				obj = content;
			}
			else
			{
				(obj = new object[1])[0] = content;
			}
			this.content = obj;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000094DD File Offset: 0x000076DD
		[__DynamicallyInvokable]
		public XStreamingElement(XName name, params object[] content) : this(name)
		{
			this.content = content;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000094ED File Offset: 0x000076ED
		// (set) Token: 0x06000213 RID: 531 RVA: 0x000094F5 File Offset: 0x000076F5
		[__DynamicallyInvokable]
		public XName Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.name;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.name = value;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009514 File Offset: 0x00007714
		[__DynamicallyInvokable]
		public void Add(object content)
		{
			if (content != null)
			{
				List<object> list = this.content as List<object>;
				if (list == null)
				{
					list = new List<object>();
					if (this.content != null)
					{
						list.Add(this.content);
					}
					this.content = list;
				}
				list.Add(content);
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000955B File Offset: 0x0000775B
		[__DynamicallyInvokable]
		public void Add(params object[] content)
		{
			this.Add(content);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00009564 File Offset: 0x00007764
		public void Save(string fileName)
		{
			this.Save(fileName, SaveOptions.None);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00009570 File Offset: 0x00007770
		public void Save(string fileName, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using (XmlWriter xmlWriter = XmlWriter.Create(fileName, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000095B0 File Offset: 0x000077B0
		[__DynamicallyInvokable]
		public void Save(Stream stream)
		{
			this.Save(stream, SaveOptions.None);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000095BC File Offset: 0x000077BC
		[__DynamicallyInvokable]
		public void Save(Stream stream, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000095FC File Offset: 0x000077FC
		[__DynamicallyInvokable]
		public void Save(TextWriter textWriter)
		{
			this.Save(textWriter, SaveOptions.None);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00009608 File Offset: 0x00007808
		[__DynamicallyInvokable]
		public void Save(TextWriter textWriter, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00009648 File Offset: 0x00007848
		[__DynamicallyInvokable]
		public void Save(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteStartDocument();
			this.WriteTo(writer);
			writer.WriteEndDocument();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000966B File Offset: 0x0000786B
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.GetXmlString(SaveOptions.None);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00009674 File Offset: 0x00007874
		[__DynamicallyInvokable]
		public string ToString(SaveOptions options)
		{
			return this.GetXmlString(options);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009680 File Offset: 0x00007880
		[__DynamicallyInvokable]
		public void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			new StreamingElementWriter(writer).WriteStreamingElement(this);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000096AC File Offset: 0x000078AC
		private string GetXmlString(SaveOptions o)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				xmlWriterSettings.OmitXmlDeclaration = true;
				if ((o & SaveOptions.DisableFormatting) == SaveOptions.None)
				{
					xmlWriterSettings.Indent = true;
				}
				if ((o & SaveOptions.OmitDuplicateNamespaces) != SaveOptions.None)
				{
					xmlWriterSettings.NamespaceHandling |= NamespaceHandling.OmitDuplicates;
				}
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
				{
					this.WriteTo(xmlWriter);
				}
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x040000B1 RID: 177
		internal XName name;

		// Token: 0x040000B2 RID: 178
		internal object content;
	}
}
