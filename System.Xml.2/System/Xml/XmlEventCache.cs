using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Xml.Xsl.Runtime;

namespace System.Xml
{
	// Token: 0x020000D0 RID: 208
	internal sealed class XmlEventCache : XmlRawWriter
	{
		// Token: 0x060008CE RID: 2254 RVA: 0x0001F8D7 File Offset: 0x0001DAD7
		public XmlEventCache(string baseUri, bool hasRootNode)
		{
			this.baseUri = baseUri;
			this.hasRootNode = hasRootNode;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001F8ED File Offset: 0x0001DAED
		public void EndEvents()
		{
			if (this.singleText.Count == 0)
			{
				this.AddEvent(XmlEventCache.XmlEventType.Unknown);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0001F903 File Offset: 0x0001DB03
		public string BaseUri
		{
			get
			{
				return this.baseUri;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0001F90B File Offset: 0x0001DB0B
		public bool HasRootNode
		{
			get
			{
				return this.hasRootNode;
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001F914 File Offset: 0x0001DB14
		public void EventsToWriter(XmlWriter writer)
		{
			if (this.singleText.Count != 0)
			{
				writer.WriteString(this.singleText.GetResult());
				return;
			}
			XmlRawWriter xmlRawWriter = writer as XmlRawWriter;
			for (int i = 0; i < this.pages.Count; i++)
			{
				XmlEventCache.XmlEvent[] array = this.pages[i];
				for (int j = 0; j < array.Length; j++)
				{
					switch (array[j].EventType)
					{
					case XmlEventCache.XmlEventType.Unknown:
						return;
					case XmlEventCache.XmlEventType.DocType:
						writer.WriteDocType(array[j].String1, array[j].String2, array[j].String3, (string)array[j].Object);
						break;
					case XmlEventCache.XmlEventType.StartElem:
						writer.WriteStartElement(array[j].String1, array[j].String2, array[j].String3);
						break;
					case XmlEventCache.XmlEventType.StartAttr:
						writer.WriteStartAttribute(array[j].String1, array[j].String2, array[j].String3);
						break;
					case XmlEventCache.XmlEventType.EndAttr:
						writer.WriteEndAttribute();
						break;
					case XmlEventCache.XmlEventType.CData:
						writer.WriteCData(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.Comment:
						writer.WriteComment(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.PI:
						writer.WriteProcessingInstruction(array[j].String1, array[j].String2);
						break;
					case XmlEventCache.XmlEventType.Whitespace:
						writer.WriteWhitespace(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.String:
						writer.WriteString(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.Raw:
						writer.WriteRaw(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.EntRef:
						writer.WriteEntityRef(array[j].String1);
						break;
					case XmlEventCache.XmlEventType.CharEnt:
						writer.WriteCharEntity((char)array[j].Object);
						break;
					case XmlEventCache.XmlEventType.SurrCharEnt:
					{
						char[] array2 = (char[])array[j].Object;
						writer.WriteSurrogateCharEntity(array2[0], array2[1]);
						break;
					}
					case XmlEventCache.XmlEventType.Base64:
					{
						byte[] array3 = (byte[])array[j].Object;
						writer.WriteBase64(array3, 0, array3.Length);
						break;
					}
					case XmlEventCache.XmlEventType.BinHex:
					{
						byte[] array3 = (byte[])array[j].Object;
						writer.WriteBinHex(array3, 0, array3.Length);
						break;
					}
					case XmlEventCache.XmlEventType.XmlDecl1:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteXmlDeclaration((XmlStandalone)array[j].Object);
						}
						break;
					case XmlEventCache.XmlEventType.XmlDecl2:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteXmlDeclaration(array[j].String1);
						}
						break;
					case XmlEventCache.XmlEventType.StartContent:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.StartElementContent();
						}
						break;
					case XmlEventCache.XmlEventType.EndElem:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteEndElement(array[j].String1, array[j].String2, array[j].String3);
						}
						else
						{
							writer.WriteEndElement();
						}
						break;
					case XmlEventCache.XmlEventType.FullEndElem:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteFullEndElement(array[j].String1, array[j].String2, array[j].String3);
						}
						else
						{
							writer.WriteFullEndElement();
						}
						break;
					case XmlEventCache.XmlEventType.Nmsp:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteNamespaceDeclaration(array[j].String1, array[j].String2);
						}
						else
						{
							writer.WriteAttributeString("xmlns", array[j].String1, "http://www.w3.org/2000/xmlns/", array[j].String2);
						}
						break;
					case XmlEventCache.XmlEventType.EndBase64:
						if (xmlRawWriter != null)
						{
							xmlRawWriter.WriteEndBase64();
						}
						break;
					case XmlEventCache.XmlEventType.Close:
						writer.Close();
						break;
					case XmlEventCache.XmlEventType.Flush:
						writer.Flush();
						break;
					case XmlEventCache.XmlEventType.Dispose:
						((IDisposable)writer).Dispose();
						break;
					}
				}
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0001FD24 File Offset: 0x0001DF24
		public string EventsToString()
		{
			if (this.singleText.Count != 0)
			{
				return this.singleText.GetResult();
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			for (int i = 0; i < this.pages.Count; i++)
			{
				XmlEventCache.XmlEvent[] array = this.pages[i];
				for (int j = 0; j < array.Length; j++)
				{
					switch (array[j].EventType)
					{
					case XmlEventCache.XmlEventType.Unknown:
						return stringBuilder.ToString();
					case XmlEventCache.XmlEventType.StartAttr:
						flag = true;
						break;
					case XmlEventCache.XmlEventType.EndAttr:
						flag = false;
						break;
					case XmlEventCache.XmlEventType.CData:
					case XmlEventCache.XmlEventType.Whitespace:
					case XmlEventCache.XmlEventType.String:
					case XmlEventCache.XmlEventType.Raw:
						if (!flag)
						{
							stringBuilder.Append(array[j].String1);
						}
						break;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0001FDFA File Offset: 0x0001DFFA
		public override XmlWriterSettings Settings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001FDFD File Offset: 0x0001DFFD
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.AddEvent(XmlEventCache.XmlEventType.DocType, name, pubid, sysid, subset);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001FE0B File Offset: 0x0001E00B
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.StartElem, prefix, localName, ns);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001FE17 File Offset: 0x0001E017
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.StartAttr, prefix, localName, ns);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001FE23 File Offset: 0x0001E023
		public override void WriteEndAttribute()
		{
			this.AddEvent(XmlEventCache.XmlEventType.EndAttr);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001FE2C File Offset: 0x0001E02C
		public override void WriteCData(string text)
		{
			this.AddEvent(XmlEventCache.XmlEventType.CData, text);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001FE36 File Offset: 0x0001E036
		public override void WriteComment(string text)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Comment, text);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001FE40 File Offset: 0x0001E040
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.AddEvent(XmlEventCache.XmlEventType.PI, name, text);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001FE4B File Offset: 0x0001E04B
		public override void WriteWhitespace(string ws)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Whitespace, ws);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001FE55 File Offset: 0x0001E055
		public override void WriteString(string text)
		{
			if (this.pages == null)
			{
				this.singleText.ConcatNoDelimiter(text);
				return;
			}
			this.AddEvent(XmlEventCache.XmlEventType.String, text);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001FE75 File Offset: 0x0001E075
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001FE85 File Offset: 0x0001E085
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteRaw(new string(buffer, index, count));
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001FE95 File Offset: 0x0001E095
		public override void WriteRaw(string data)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Raw, data);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001FEA0 File Offset: 0x0001E0A0
		public override void WriteEntityRef(string name)
		{
			this.AddEvent(XmlEventCache.XmlEventType.EntRef, name);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001FEAB File Offset: 0x0001E0AB
		public override void WriteCharEntity(char ch)
		{
			this.AddEvent(XmlEventCache.XmlEventType.CharEnt, ch);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001FEBC File Offset: 0x0001E0BC
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			char[] o = new char[]
			{
				lowChar,
				highChar
			};
			this.AddEvent(XmlEventCache.XmlEventType.SurrCharEnt, o);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001FEE1 File Offset: 0x0001E0E1
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Base64, XmlEventCache.ToBytes(buffer, index, count));
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001FEF3 File Offset: 0x0001E0F3
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.AddEvent(XmlEventCache.XmlEventType.BinHex, XmlEventCache.ToBytes(buffer, index, count));
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001FF05 File Offset: 0x0001E105
		public override void Close()
		{
			this.AddEvent(XmlEventCache.XmlEventType.Close);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001FF0F File Offset: 0x0001E10F
		public override void Flush()
		{
			this.AddEvent(XmlEventCache.XmlEventType.Flush);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001FF19 File Offset: 0x0001E119
		public override void WriteValue(object value)
		{
			this.WriteString(XmlUntypedConverter.Untyped.ToString(value, this.resolver));
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001FF32 File Offset: 0x0001E132
		public override void WriteValue(string value)
		{
			this.WriteString(value);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001FF3C File Offset: 0x0001E13C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.AddEvent(XmlEventCache.XmlEventType.Dispose);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001FF70 File Offset: 0x0001E170
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.AddEvent(XmlEventCache.XmlEventType.XmlDecl1, standalone);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001FF80 File Offset: 0x0001E180
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.AddEvent(XmlEventCache.XmlEventType.XmlDecl2, xmldecl);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001FF8B File Offset: 0x0001E18B
		internal override void StartElementContent()
		{
			this.AddEvent(XmlEventCache.XmlEventType.StartContent);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001FF95 File Offset: 0x0001E195
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.EndElem, prefix, localName, ns);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001FFA2 File Offset: 0x0001E1A2
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.FullEndElem, prefix, localName, ns);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001FFAF File Offset: 0x0001E1AF
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.AddEvent(XmlEventCache.XmlEventType.Nmsp, prefix, ns);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001FFBB File Offset: 0x0001E1BB
		internal override void WriteEndBase64()
		{
			this.AddEvent(XmlEventCache.XmlEventType.EndBase64);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001FFC8 File Offset: 0x0001E1C8
		private void AddEvent(XmlEventCache.XmlEventType eventType)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001FFF0 File Offset: 0x0001E1F0
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00020018 File Offset: 0x0001E218
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1, string s2)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1, s2);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00020040 File Offset: 0x0001E240
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1, s2, s3);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0002006C File Offset: 0x0001E26C
		private void AddEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3, object o)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, s1, s2, s3, o);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00020098 File Offset: 0x0001E298
		private void AddEvent(XmlEventCache.XmlEventType eventType, object o)
		{
			int num = this.NewEvent();
			this.pageCurr[num].InitEvent(eventType, o);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000200C0 File Offset: 0x0001E2C0
		private int NewEvent()
		{
			if (this.pages == null)
			{
				this.pages = new List<XmlEventCache.XmlEvent[]>();
				this.pageCurr = new XmlEventCache.XmlEvent[32];
				this.pages.Add(this.pageCurr);
				if (this.singleText.Count != 0)
				{
					this.pageCurr[0].InitEvent(XmlEventCache.XmlEventType.String, this.singleText.GetResult());
					this.pageSize++;
					this.singleText.Clear();
				}
			}
			else if (this.pageSize >= this.pageCurr.Length)
			{
				this.pageCurr = new XmlEventCache.XmlEvent[this.pageSize * 2];
				this.pages.Add(this.pageCurr);
				this.pageSize = 0;
			}
			int num = this.pageSize;
			this.pageSize = num + 1;
			return num;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00020190 File Offset: 0x0001E390
		private static byte[] ToBytes(byte[] buffer, int index, int count)
		{
			if (index != 0 || count != buffer.Length)
			{
				if (buffer.Length - index > count)
				{
					count = buffer.Length - index;
				}
				byte[] array = new byte[count];
				Array.Copy(buffer, index, array, 0, count);
				return array;
			}
			return buffer;
		}

		// Token: 0x04000321 RID: 801
		private List<XmlEventCache.XmlEvent[]> pages;

		// Token: 0x04000322 RID: 802
		private XmlEventCache.XmlEvent[] pageCurr;

		// Token: 0x04000323 RID: 803
		private int pageSize;

		// Token: 0x04000324 RID: 804
		private bool hasRootNode;

		// Token: 0x04000325 RID: 805
		private StringConcat singleText;

		// Token: 0x04000326 RID: 806
		private string baseUri;

		// Token: 0x04000327 RID: 807
		private const int InitialPageSize = 32;

		// Token: 0x0200034C RID: 844
		private enum XmlEventType
		{
			// Token: 0x040015EC RID: 5612
			Unknown,
			// Token: 0x040015ED RID: 5613
			DocType,
			// Token: 0x040015EE RID: 5614
			StartElem,
			// Token: 0x040015EF RID: 5615
			StartAttr,
			// Token: 0x040015F0 RID: 5616
			EndAttr,
			// Token: 0x040015F1 RID: 5617
			CData,
			// Token: 0x040015F2 RID: 5618
			Comment,
			// Token: 0x040015F3 RID: 5619
			PI,
			// Token: 0x040015F4 RID: 5620
			Whitespace,
			// Token: 0x040015F5 RID: 5621
			String,
			// Token: 0x040015F6 RID: 5622
			Raw,
			// Token: 0x040015F7 RID: 5623
			EntRef,
			// Token: 0x040015F8 RID: 5624
			CharEnt,
			// Token: 0x040015F9 RID: 5625
			SurrCharEnt,
			// Token: 0x040015FA RID: 5626
			Base64,
			// Token: 0x040015FB RID: 5627
			BinHex,
			// Token: 0x040015FC RID: 5628
			XmlDecl1,
			// Token: 0x040015FD RID: 5629
			XmlDecl2,
			// Token: 0x040015FE RID: 5630
			StartContent,
			// Token: 0x040015FF RID: 5631
			EndElem,
			// Token: 0x04001600 RID: 5632
			FullEndElem,
			// Token: 0x04001601 RID: 5633
			Nmsp,
			// Token: 0x04001602 RID: 5634
			EndBase64,
			// Token: 0x04001603 RID: 5635
			Close,
			// Token: 0x04001604 RID: 5636
			Flush,
			// Token: 0x04001605 RID: 5637
			Dispose
		}

		// Token: 0x0200034D RID: 845
		private struct XmlEvent
		{
			// Token: 0x06002E2D RID: 11821 RVA: 0x000F4E66 File Offset: 0x000F3066
			public void InitEvent(XmlEventCache.XmlEventType eventType)
			{
				this.eventType = eventType;
			}

			// Token: 0x06002E2E RID: 11822 RVA: 0x000F4E6F File Offset: 0x000F306F
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1)
			{
				this.eventType = eventType;
				this.s1 = s1;
			}

			// Token: 0x06002E2F RID: 11823 RVA: 0x000F4E7F File Offset: 0x000F307F
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1, string s2)
			{
				this.eventType = eventType;
				this.s1 = s1;
				this.s2 = s2;
			}

			// Token: 0x06002E30 RID: 11824 RVA: 0x000F4E96 File Offset: 0x000F3096
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3)
			{
				this.eventType = eventType;
				this.s1 = s1;
				this.s2 = s2;
				this.s3 = s3;
			}

			// Token: 0x06002E31 RID: 11825 RVA: 0x000F4EB5 File Offset: 0x000F30B5
			public void InitEvent(XmlEventCache.XmlEventType eventType, string s1, string s2, string s3, object o)
			{
				this.eventType = eventType;
				this.s1 = s1;
				this.s2 = s2;
				this.s3 = s3;
				this.o = o;
			}

			// Token: 0x06002E32 RID: 11826 RVA: 0x000F4EDC File Offset: 0x000F30DC
			public void InitEvent(XmlEventCache.XmlEventType eventType, object o)
			{
				this.eventType = eventType;
				this.o = o;
			}

			// Token: 0x17000A1E RID: 2590
			// (get) Token: 0x06002E33 RID: 11827 RVA: 0x000F4EEC File Offset: 0x000F30EC
			public XmlEventCache.XmlEventType EventType
			{
				get
				{
					return this.eventType;
				}
			}

			// Token: 0x17000A1F RID: 2591
			// (get) Token: 0x06002E34 RID: 11828 RVA: 0x000F4EF4 File Offset: 0x000F30F4
			public string String1
			{
				get
				{
					return this.s1;
				}
			}

			// Token: 0x17000A20 RID: 2592
			// (get) Token: 0x06002E35 RID: 11829 RVA: 0x000F4EFC File Offset: 0x000F30FC
			public string String2
			{
				get
				{
					return this.s2;
				}
			}

			// Token: 0x17000A21 RID: 2593
			// (get) Token: 0x06002E36 RID: 11830 RVA: 0x000F4F04 File Offset: 0x000F3104
			public string String3
			{
				get
				{
					return this.s3;
				}
			}

			// Token: 0x17000A22 RID: 2594
			// (get) Token: 0x06002E37 RID: 11831 RVA: 0x000F4F0C File Offset: 0x000F310C
			public object Object
			{
				get
				{
					return this.o;
				}
			}

			// Token: 0x04001606 RID: 5638
			private XmlEventCache.XmlEventType eventType;

			// Token: 0x04001607 RID: 5639
			private string s1;

			// Token: 0x04001608 RID: 5640
			private string s2;

			// Token: 0x04001609 RID: 5641
			private string s3;

			// Token: 0x0400160A RID: 5642
			private object o;
		}
	}
}
