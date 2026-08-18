using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Data.Common.CommandTrees.Internal
{
	// Token: 0x02000439 RID: 1081
	internal class XmlExpressionDumper : ExpressionDumper
	{
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06003A19 RID: 14873 RVA: 0x000DDBFE File Offset: 0x000DBDFE
		internal static Encoding DefaultEncoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x000DDC05 File Offset: 0x000DBE05
		internal XmlExpressionDumper(Stream stream) : this(stream, XmlExpressionDumper.DefaultEncoding, true)
		{
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000DDC14 File Offset: 0x000DBE14
		internal XmlExpressionDumper(Stream stream, Encoding encoding, bool indent)
		{
			this._writer = XmlWriter.Create(stream, new XmlWriterSettings
			{
				CheckCharacters = false,
				Indent = true,
				Encoding = encoding
			});
			this._writer.WriteStartDocument(true);
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x000DDC5B File Offset: 0x000DBE5B
		internal void Close()
		{
			this._writer.WriteEndDocument();
			this._writer.Flush();
			this._writer.Close();
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x000DDC80 File Offset: 0x000DBE80
		internal override void Begin(string name, Dictionary<string, object> attrs)
		{
			this._writer.WriteStartElement(name);
			if (attrs != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in attrs)
				{
					this._writer.WriteAttributeString(keyValuePair.Key, (keyValuePair.Value == null) ? "" : keyValuePair.Value.ToString());
				}
			}
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x000DDD04 File Offset: 0x000DBF04
		internal override void End(string name)
		{
			this._writer.WriteEndElement();
		}

		// Token: 0x04001870 RID: 6256
		private XmlWriter _writer;
	}
}
