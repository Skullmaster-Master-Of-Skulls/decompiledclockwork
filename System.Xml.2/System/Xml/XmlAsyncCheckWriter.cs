using System;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000C9 RID: 201
	internal class XmlAsyncCheckWriter : XmlWriter
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0001897D File Offset: 0x00016B7D
		internal XmlWriter CoreWriter
		{
			get
			{
				return this.coreWriter;
			}
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00018985 File Offset: 0x00016B85
		public XmlAsyncCheckWriter(XmlWriter writer)
		{
			this.coreWriter = writer;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001899F File Offset: 0x00016B9F
		private void CheckAsync()
		{
			if (!this.lastTask.IsCompleted)
			{
				throw new InvalidOperationException(Res.GetString("Xml_AsyncIsRunningException"));
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x000189C0 File Offset: 0x00016BC0
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings xmlWriterSettings = this.coreWriter.Settings;
				if (xmlWriterSettings != null)
				{
					xmlWriterSettings = xmlWriterSettings.Clone();
				}
				else
				{
					xmlWriterSettings = new XmlWriterSettings();
				}
				xmlWriterSettings.Async = true;
				xmlWriterSettings.ReadOnly = true;
				return xmlWriterSettings;
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000189FA File Offset: 0x00016BFA
		public override void WriteStartDocument()
		{
			this.CheckAsync();
			this.coreWriter.WriteStartDocument();
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00018A0D File Offset: 0x00016C0D
		public override void WriteStartDocument(bool standalone)
		{
			this.CheckAsync();
			this.coreWriter.WriteStartDocument(standalone);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00018A21 File Offset: 0x00016C21
		public override void WriteEndDocument()
		{
			this.CheckAsync();
			this.coreWriter.WriteEndDocument();
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00018A34 File Offset: 0x00016C34
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.CheckAsync();
			this.coreWriter.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00018A4C File Offset: 0x00016C4C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.CheckAsync();
			this.coreWriter.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00018A62 File Offset: 0x00016C62
		public override void WriteEndElement()
		{
			this.CheckAsync();
			this.coreWriter.WriteEndElement();
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00018A75 File Offset: 0x00016C75
		public override void WriteFullEndElement()
		{
			this.CheckAsync();
			this.coreWriter.WriteFullEndElement();
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00018A88 File Offset: 0x00016C88
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.CheckAsync();
			this.coreWriter.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00018A9E File Offset: 0x00016C9E
		public override void WriteEndAttribute()
		{
			this.CheckAsync();
			this.coreWriter.WriteEndAttribute();
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00018AB1 File Offset: 0x00016CB1
		public override void WriteCData(string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteCData(text);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00018AC5 File Offset: 0x00016CC5
		public override void WriteComment(string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteComment(text);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00018AD9 File Offset: 0x00016CD9
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00018AEE File Offset: 0x00016CEE
		public override void WriteEntityRef(string name)
		{
			this.CheckAsync();
			this.coreWriter.WriteEntityRef(name);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00018B02 File Offset: 0x00016D02
		public override void WriteCharEntity(char ch)
		{
			this.CheckAsync();
			this.coreWriter.WriteCharEntity(ch);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00018B16 File Offset: 0x00016D16
		public override void WriteWhitespace(string ws)
		{
			this.CheckAsync();
			this.coreWriter.WriteWhitespace(ws);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00018B2A File Offset: 0x00016D2A
		public override void WriteString(string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteString(text);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00018B3E File Offset: 0x00016D3E
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.CheckAsync();
			this.coreWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00018B53 File Offset: 0x00016D53
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteChars(buffer, index, count);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00018B69 File Offset: 0x00016D69
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteRaw(buffer, index, count);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00018B7F File Offset: 0x00016D7F
		public override void WriteRaw(string data)
		{
			this.CheckAsync();
			this.coreWriter.WriteRaw(data);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00018B93 File Offset: 0x00016D93
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteBase64(buffer, index, count);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00018BA9 File Offset: 0x00016DA9
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteBinHex(buffer, index, count);
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00018BBF File Offset: 0x00016DBF
		public override WriteState WriteState
		{
			get
			{
				this.CheckAsync();
				return this.coreWriter.WriteState;
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00018BD2 File Offset: 0x00016DD2
		public override void Close()
		{
			this.CheckAsync();
			this.coreWriter.Close();
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00018BE5 File Offset: 0x00016DE5
		public override void Flush()
		{
			this.CheckAsync();
			this.coreWriter.Flush();
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00018BF8 File Offset: 0x00016DF8
		public override string LookupPrefix(string ns)
		{
			this.CheckAsync();
			return this.coreWriter.LookupPrefix(ns);
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00018C0C File Offset: 0x00016E0C
		public override XmlSpace XmlSpace
		{
			get
			{
				this.CheckAsync();
				return this.coreWriter.XmlSpace;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00018C1F File Offset: 0x00016E1F
		public override string XmlLang
		{
			get
			{
				this.CheckAsync();
				return this.coreWriter.XmlLang;
			}
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00018C32 File Offset: 0x00016E32
		public override void WriteNmToken(string name)
		{
			this.CheckAsync();
			this.coreWriter.WriteNmToken(name);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00018C46 File Offset: 0x00016E46
		public override void WriteName(string name)
		{
			this.CheckAsync();
			this.coreWriter.WriteName(name);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00018C5A File Offset: 0x00016E5A
		public override void WriteQualifiedName(string localName, string ns)
		{
			this.CheckAsync();
			this.coreWriter.WriteQualifiedName(localName, ns);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00018C6F File Offset: 0x00016E6F
		public override void WriteValue(object value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00018C83 File Offset: 0x00016E83
		public override void WriteValue(string value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00018C97 File Offset: 0x00016E97
		public override void WriteValue(bool value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00018CAB File Offset: 0x00016EAB
		public override void WriteValue(DateTime value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00018CBF File Offset: 0x00016EBF
		public override void WriteValue(DateTimeOffset value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x00018CD3 File Offset: 0x00016ED3
		public override void WriteValue(double value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00018CE7 File Offset: 0x00016EE7
		public override void WriteValue(float value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00018CFB File Offset: 0x00016EFB
		public override void WriteValue(decimal value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00018D0F File Offset: 0x00016F0F
		public override void WriteValue(int value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00018D23 File Offset: 0x00016F23
		public override void WriteValue(long value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00018D37 File Offset: 0x00016F37
		public override void WriteAttributes(XmlReader reader, bool defattr)
		{
			this.CheckAsync();
			this.coreWriter.WriteAttributes(reader, defattr);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00018D4C File Offset: 0x00016F4C
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			this.CheckAsync();
			this.coreWriter.WriteNode(reader, defattr);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00018D61 File Offset: 0x00016F61
		public override void WriteNode(XPathNavigator navigator, bool defattr)
		{
			this.CheckAsync();
			this.coreWriter.WriteNode(navigator, defattr);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00018D76 File Offset: 0x00016F76
		protected override void Dispose(bool disposing)
		{
			this.CheckAsync();
			this.coreWriter.Dispose();
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00018D8C File Offset: 0x00016F8C
		public override Task WriteStartDocumentAsync()
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteStartDocumentAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00018DB4 File Offset: 0x00016FB4
		public override Task WriteStartDocumentAsync(bool standalone)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteStartDocumentAsync(standalone);
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00018DDC File Offset: 0x00016FDC
		public override Task WriteEndDocumentAsync()
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteEndDocumentAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00018E04 File Offset: 0x00017004
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteDocTypeAsync(name, pubid, sysid, subset);
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00018E30 File Offset: 0x00017030
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteStartElementAsync(prefix, localName, ns);
			this.lastTask = result;
			return result;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00018E5C File Offset: 0x0001705C
		public override Task WriteEndElementAsync()
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteEndElementAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00018E84 File Offset: 0x00017084
		public override Task WriteFullEndElementAsync()
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteFullEndElementAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00018EAC File Offset: 0x000170AC
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteStartAttributeAsync(prefix, localName, ns);
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00018ED8 File Offset: 0x000170D8
		protected internal override Task WriteEndAttributeAsync()
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteEndAttributeAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00018F00 File Offset: 0x00017100
		public override Task WriteCDataAsync(string text)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteCDataAsync(text);
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00018F28 File Offset: 0x00017128
		public override Task WriteCommentAsync(string text)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteCommentAsync(text);
			this.lastTask = result;
			return result;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00018F50 File Offset: 0x00017150
		public override Task WriteProcessingInstructionAsync(string name, string text)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteProcessingInstructionAsync(name, text);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00018F7C File Offset: 0x0001717C
		public override Task WriteEntityRefAsync(string name)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteEntityRefAsync(name);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00018FA4 File Offset: 0x000171A4
		public override Task WriteCharEntityAsync(char ch)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteCharEntityAsync(ch);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00018FCC File Offset: 0x000171CC
		public override Task WriteWhitespaceAsync(string ws)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteWhitespaceAsync(ws);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00018FF4 File Offset: 0x000171F4
		public override Task WriteStringAsync(string text)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteStringAsync(text);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001901C File Offset: 0x0001721C
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteSurrogateCharEntityAsync(lowChar, highChar);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00019048 File Offset: 0x00017248
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteCharsAsync(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00019074 File Offset: 0x00017274
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteRawAsync(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x000190A0 File Offset: 0x000172A0
		public override Task WriteRawAsync(string data)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteRawAsync(data);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000190C8 File Offset: 0x000172C8
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteBase64Async(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x000190F4 File Offset: 0x000172F4
		public override Task WriteBinHexAsync(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteBinHexAsync(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00019120 File Offset: 0x00017320
		public override Task FlushAsync()
		{
			this.CheckAsync();
			Task result = this.coreWriter.FlushAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00019148 File Offset: 0x00017348
		public override Task WriteNmTokenAsync(string name)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteNmTokenAsync(name);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00019170 File Offset: 0x00017370
		public override Task WriteNameAsync(string name)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteNameAsync(name);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00019198 File Offset: 0x00017398
		public override Task WriteQualifiedNameAsync(string localName, string ns)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteQualifiedNameAsync(localName, ns);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000191C4 File Offset: 0x000173C4
		public override Task WriteAttributesAsync(XmlReader reader, bool defattr)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteAttributesAsync(reader, defattr);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x000191F0 File Offset: 0x000173F0
		public override Task WriteNodeAsync(XmlReader reader, bool defattr)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteNodeAsync(reader, defattr);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001921C File Offset: 0x0001741C
		public override Task WriteNodeAsync(XPathNavigator navigator, bool defattr)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteNodeAsync(navigator, defattr);
			this.lastTask = result;
			return result;
		}

		// Token: 0x040002E0 RID: 736
		private readonly XmlWriter coreWriter;

		// Token: 0x040002E1 RID: 737
		private Task lastTask = AsyncHelper.DoneTask;
	}
}
