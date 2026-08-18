using System;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x02000310 RID: 784
	internal sealed class SqlStreamingXml
	{
		// Token: 0x060028EF RID: 10479 RVA: 0x002B2B98 File Offset: 0x002B1F98
		public SqlStreamingXml(int i, SqlDataReader reader)
		{
			this._columnOrdinal = i;
			this._reader = reader;
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x002B2BC8 File Offset: 0x002B1FC8
		public void Close()
		{
			((IDisposable)this._xmlWriter).Dispose();
			((IDisposable)this._xmlReader).Dispose();
			this._reader = null;
			this._xmlReader = null;
			this._xmlWriter = null;
			this._strWriter = null;
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x002B2C08 File Offset: 0x002B2008
		public int ColumnOrdinal
		{
			get
			{
				return this._columnOrdinal;
			}
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x002B2C28 File Offset: 0x002B2028
		public long GetChars(long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			if (this._xmlReader == null)
			{
				SqlStream sqlStream = new SqlStream(this._columnOrdinal, this._reader, true, false, false);
				this._xmlReader = sqlStream.ToXmlReader();
				this._strWriter = new StringWriter(null);
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				xmlWriterSettings.CloseOutput = true;
				xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
				this._xmlWriter = XmlWriter.Create(this._strWriter, xmlWriterSettings);
			}
			int num = 0;
			if (dataIndex < this._charsRemoved)
			{
				throw ADP.NonSeqByteAccess(dataIndex, this._charsRemoved, "GetChars");
			}
			if (dataIndex > this._charsRemoved)
			{
				num = (int)(dataIndex - this._charsRemoved);
			}
			if (buffer == null)
			{
				return -1L;
			}
			StringBuilder stringBuilder = this._strWriter.GetStringBuilder();
			int num2;
			while (!this._xmlReader.EOF && stringBuilder.Length < length + num)
			{
				this.WriteXmlElement();
				if (num > 0)
				{
					num2 = ((stringBuilder.Length < num) ? stringBuilder.Length : num);
					stringBuilder.Remove(0, num2);
					num -= num2;
					this._charsRemoved += (long)num2;
				}
			}
			if (num > 0)
			{
				num2 = ((stringBuilder.Length < num) ? stringBuilder.Length : num);
				stringBuilder.Remove(0, num2);
				num -= num2;
				this._charsRemoved += (long)num2;
			}
			if (stringBuilder.Length == 0)
			{
				return 0L;
			}
			num2 = ((stringBuilder.Length < length) ? stringBuilder.Length : length);
			for (int i = 0; i < num2; i++)
			{
				buffer[bufferIndex + i] = stringBuilder[i];
			}
			stringBuilder.Remove(0, num2);
			this._charsRemoved += (long)num2;
			return (long)num2;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x002B2DB8 File Offset: 0x002B21B8
		private void WriteXmlElement()
		{
			if (this._xmlReader.EOF)
			{
				return;
			}
			bool canReadValueChunk = this._xmlReader.CanReadValueChunk;
			char[] array = null;
			this._xmlReader.Read();
			switch (this._xmlReader.NodeType)
			{
			case XmlNodeType.Element:
				this._xmlWriter.WriteStartElement(this._xmlReader.Prefix, this._xmlReader.LocalName, this._xmlReader.NamespaceURI);
				this._xmlWriter.WriteAttributes(this._xmlReader, true);
				if (this._xmlReader.IsEmptyElement)
				{
					this._xmlWriter.WriteEndElement();
				}
				break;
			case XmlNodeType.Text:
				if (canReadValueChunk)
				{
					if (array == null)
					{
						array = new char[1024];
					}
					int count;
					while ((count = this._xmlReader.ReadValueChunk(array, 0, 1024)) > 0)
					{
						this._xmlWriter.WriteChars(array, 0, count);
					}
				}
				else
				{
					this._xmlWriter.WriteString(this._xmlReader.Value);
				}
				break;
			case XmlNodeType.CDATA:
				this._xmlWriter.WriteCData(this._xmlReader.Value);
				break;
			case XmlNodeType.EntityReference:
				this._xmlWriter.WriteEntityRef(this._xmlReader.Name);
				break;
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.XmlDeclaration:
				this._xmlWriter.WriteProcessingInstruction(this._xmlReader.Name, this._xmlReader.Value);
				break;
			case XmlNodeType.Comment:
				this._xmlWriter.WriteComment(this._xmlReader.Value);
				break;
			case XmlNodeType.DocumentType:
				this._xmlWriter.WriteDocType(this._xmlReader.Name, this._xmlReader.GetAttribute("PUBLIC"), this._xmlReader.GetAttribute("SYSTEM"), this._xmlReader.Value);
				break;
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				this._xmlWriter.WriteWhitespace(this._xmlReader.Value);
				break;
			case XmlNodeType.EndElement:
				this._xmlWriter.WriteFullEndElement();
				break;
			}
			this._xmlWriter.Flush();
		}

		// Token: 0x040019A5 RID: 6565
		private int _columnOrdinal;

		// Token: 0x040019A6 RID: 6566
		private SqlDataReader _reader;

		// Token: 0x040019A7 RID: 6567
		private XmlReader _xmlReader;

		// Token: 0x040019A8 RID: 6568
		private XmlWriter _xmlWriter;

		// Token: 0x040019A9 RID: 6569
		private StringWriter _strWriter;

		// Token: 0x040019AA RID: 6570
		private long _charsRemoved;
	}
}
