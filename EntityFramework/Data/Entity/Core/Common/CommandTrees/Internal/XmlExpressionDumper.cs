using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x02000135 RID: 309
	internal class XmlExpressionDumper : ExpressionDumper
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00035D32 File Offset: 0x00033F32
		internal static Encoding DefaultEncoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00035D39 File Offset: 0x00033F39
		internal XmlExpressionDumper(Stream stream) : this(stream, XmlExpressionDumper.DefaultEncoding)
		{
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00035D48 File Offset: 0x00033F48
		internal XmlExpressionDumper(Stream stream, Encoding encoding)
		{
			this._writer = XmlWriter.Create(stream, new XmlWriterSettings
			{
				CheckCharacters = false,
				Indent = true,
				Encoding = encoding
			});
			this._writer.WriteStartDocument(true);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00035D8F File Offset: 0x00033F8F
		internal void Close()
		{
			this._writer.WriteEndDocument();
			this._writer.Flush();
			this._writer.Close();
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00035DB4 File Offset: 0x00033FB4
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

		// Token: 0x06000A7D RID: 2685 RVA: 0x00035E38 File Offset: 0x00034038
		internal override void End(string name)
		{
			this._writer.WriteEndElement();
		}

		// Token: 0x040002D1 RID: 721
		private readonly XmlWriter _writer;
	}
}
