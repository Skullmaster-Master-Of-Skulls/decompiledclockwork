using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000D2 RID: 210
	internal abstract class XmlRawWriter : XmlWriter
	{
		// Token: 0x06000913 RID: 2323 RVA: 0x0002046F File Offset: 0x0001E66F
		public override void WriteStartDocument()
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00020480 File Offset: 0x0001E680
		public override void WriteStartDocument(bool standalone)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00020491 File Offset: 0x0001E691
		public override void WriteEndDocument()
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000204A2 File Offset: 0x0001E6A2
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000204A4 File Offset: 0x0001E6A4
		public override void WriteEndElement()
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000204B5 File Offset: 0x0001E6B5
		public override void WriteFullEndElement()
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000204C6 File Offset: 0x0001E6C6
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			if (this.base64Encoder == null)
			{
				this.base64Encoder = new XmlRawWriterBase64Encoder(this);
			}
			this.base64Encoder.Encode(buffer, index, count);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x000204EA File Offset: 0x0001E6EA
		public override string LookupPrefix(string ns)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x000204FB File Offset: 0x0001E6FB
		public override WriteState WriteState
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0002050C File Offset: 0x0001E70C
		public override XmlSpace XmlSpace
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0002051D File Offset: 0x0001E71D
		public override string XmlLang
		{
			get
			{
				throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0002052E File Offset: 0x0001E72E
		public override void WriteNmToken(string name)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0002053F File Offset: 0x0001E73F
		public override void WriteName(string name)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00020550 File Offset: 0x0001E750
		public override void WriteQualifiedName(string localName, string ns)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00020561 File Offset: 0x0001E761
		public override void WriteCData(string text)
		{
			this.WriteString(text);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0002056A File Offset: 0x0001E76A
		public override void WriteCharEntity(char ch)
		{
			this.WriteString(new string(new char[]
			{
				ch
			}));
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00020581 File Offset: 0x0001E781
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.WriteString(new string(new char[]
			{
				lowChar,
				highChar
			}));
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0002059C File Offset: 0x0001E79C
		public override void WriteWhitespace(string ws)
		{
			this.WriteString(ws);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000205A5 File Offset: 0x0001E7A5
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000205B5 File Offset: 0x0001E7B5
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x000205C5 File Offset: 0x0001E7C5
		public override void WriteRaw(string data)
		{
			this.WriteString(data);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000205CE File Offset: 0x0001E7CE
		public override void WriteValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.WriteString(XmlUntypedConverter.Untyped.ToString(value, this.resolver));
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000205F5 File Offset: 0x0001E7F5
		public override void WriteValue(string value)
		{
			this.WriteString(value);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x000205FE File Offset: 0x0001E7FE
		public override void WriteValue(DateTimeOffset value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0002060C File Offset: 0x0001E80C
		public override void WriteAttributes(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0002061D File Offset: 0x0001E81D
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0002062E File Offset: 0x0001E82E
		public override void WriteNode(XPathNavigator navigator, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0002063F File Offset: 0x0001E83F
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x00020647 File Offset: 0x0001E847
		internal virtual IXmlNamespaceResolver NamespaceResolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				this.resolver = value;
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00020650 File Offset: 0x0001E850
		internal virtual void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00020652 File Offset: 0x0001E852
		internal virtual void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x06000932 RID: 2354
		internal abstract void StartElementContent();

		// Token: 0x06000933 RID: 2355 RVA: 0x00020654 File Offset: 0x0001E854
		internal virtual void OnRootElement(ConformanceLevel conformanceLevel)
		{
		}

		// Token: 0x06000934 RID: 2356
		internal abstract void WriteEndElement(string prefix, string localName, string ns);

		// Token: 0x06000935 RID: 2357 RVA: 0x00020656 File Offset: 0x0001E856
		internal virtual void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.WriteEndElement(prefix, localName, ns);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00020661 File Offset: 0x0001E861
		internal virtual void WriteQualifiedName(string prefix, string localName, string ns)
		{
			if (prefix.Length != 0)
			{
				this.WriteString(prefix);
				this.WriteString(":");
			}
			this.WriteString(localName);
		}

		// Token: 0x06000937 RID: 2359
		internal abstract void WriteNamespaceDeclaration(string prefix, string ns);

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00020684 File Offset: 0x0001E884
		internal virtual bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00020687 File Offset: 0x0001E887
		internal virtual void WriteStartNamespaceDeclaration(string prefix)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0002068E File Offset: 0x0001E88E
		internal virtual void WriteEndNamespaceDeclaration()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00020695 File Offset: 0x0001E895
		internal virtual void WriteEndBase64()
		{
			this.base64Encoder.Flush();
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000206A2 File Offset: 0x0001E8A2
		internal virtual void Close(WriteState currentState)
		{
			this.Close();
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000206AA File Offset: 0x0001E8AA
		public override Task WriteStartDocumentAsync()
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x000206BB File Offset: 0x0001E8BB
		public override Task WriteStartDocumentAsync(bool standalone)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x000206CC File Offset: 0x0001E8CC
		public override Task WriteEndDocumentAsync()
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x000206DD File Offset: 0x0001E8DD
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x000206E4 File Offset: 0x0001E8E4
		public override Task WriteEndElementAsync()
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x000206F5 File Offset: 0x0001E8F5
		public override Task WriteFullEndElementAsync()
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00020706 File Offset: 0x0001E906
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			if (this.base64Encoder == null)
			{
				this.base64Encoder = new XmlRawWriterBase64Encoder(this);
			}
			return this.base64Encoder.EncodeAsync(buffer, index, count);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0002072A File Offset: 0x0001E92A
		public override Task WriteNmTokenAsync(string name)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0002073B File Offset: 0x0001E93B
		public override Task WriteNameAsync(string name)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0002074C File Offset: 0x0001E94C
		public override Task WriteQualifiedNameAsync(string localName, string ns)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0002075D File Offset: 0x0001E95D
		public override Task WriteCDataAsync(string text)
		{
			return this.WriteStringAsync(text);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00020766 File Offset: 0x0001E966
		public override Task WriteCharEntityAsync(char ch)
		{
			return this.WriteStringAsync(new string(new char[]
			{
				ch
			}));
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0002077D File Offset: 0x0001E97D
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			return this.WriteStringAsync(new string(new char[]
			{
				lowChar,
				highChar
			}));
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00020798 File Offset: 0x0001E998
		public override Task WriteWhitespaceAsync(string ws)
		{
			return this.WriteStringAsync(ws);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x000207A1 File Offset: 0x0001E9A1
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			return this.WriteStringAsync(new string(buffer, index, count));
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x000207B1 File Offset: 0x0001E9B1
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			return this.WriteStringAsync(new string(buffer, index, count));
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x000207C1 File Offset: 0x0001E9C1
		public override Task WriteRawAsync(string data)
		{
			return this.WriteStringAsync(data);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x000207CA File Offset: 0x0001E9CA
		public override Task WriteAttributesAsync(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x000207DB File Offset: 0x0001E9DB
		public override Task WriteNodeAsync(XmlReader reader, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000207EC File Offset: 0x0001E9EC
		public override Task WriteNodeAsync(XPathNavigator navigator, bool defattr)
		{
			throw new InvalidOperationException(Res.GetString("Xml_InvalidOperation"));
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x000207FD File Offset: 0x0001E9FD
		internal virtual Task WriteXmlDeclarationAsync(XmlStandalone standalone)
		{
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00020804 File Offset: 0x0001EA04
		internal virtual Task WriteXmlDeclarationAsync(string xmldecl)
		{
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0002080B File Offset: 0x0001EA0B
		internal virtual Task StartElementContentAsync()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00020812 File Offset: 0x0001EA12
		internal virtual Task WriteEndElementAsync(string prefix, string localName, string ns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00020819 File Offset: 0x0001EA19
		internal virtual Task WriteFullEndElementAsync(string prefix, string localName, string ns)
		{
			return this.WriteEndElementAsync(prefix, localName, ns);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00020824 File Offset: 0x0001EA24
		internal virtual Task WriteQualifiedNameAsync(string prefix, string localName, string ns)
		{
			XmlRawWriter.<WriteQualifiedNameAsync>d__74 <WriteQualifiedNameAsync>d__;
			<WriteQualifiedNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteQualifiedNameAsync>d__.<>4__this = this;
			<WriteQualifiedNameAsync>d__.prefix = prefix;
			<WriteQualifiedNameAsync>d__.localName = localName;
			<WriteQualifiedNameAsync>d__.<>1__state = -1;
			<WriteQualifiedNameAsync>d__.<>t__builder.Start<XmlRawWriter.<WriteQualifiedNameAsync>d__74>(ref <WriteQualifiedNameAsync>d__);
			return <WriteQualifiedNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00020877 File Offset: 0x0001EA77
		internal virtual Task WriteNamespaceDeclarationAsync(string prefix, string ns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0002087E File Offset: 0x0001EA7E
		internal virtual Task WriteStartNamespaceDeclarationAsync(string prefix)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00020885 File Offset: 0x0001EA85
		internal virtual Task WriteEndNamespaceDeclarationAsync()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0002088C File Offset: 0x0001EA8C
		internal virtual Task WriteEndBase64Async()
		{
			return this.base64Encoder.FlushAsync();
		}

		// Token: 0x04000332 RID: 818
		protected XmlRawWriterBase64Encoder base64Encoder;

		// Token: 0x04000333 RID: 819
		protected IXmlNamespaceResolver resolver;
	}
}
