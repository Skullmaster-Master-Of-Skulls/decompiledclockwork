using System;
using System.IO;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000036 RID: 54
	public class DelegatingXmlDictionaryWriter : XmlDictionaryWriter
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x0000832E File Offset: 0x0000652E
		protected DelegatingXmlDictionaryWriter()
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00008336 File Offset: 0x00006536
		protected void InitializeInnerWriter(XmlDictionaryWriter innerWriter)
		{
			if (innerWriter == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("innerWriter");
			}
			this._innerWriter = innerWriter;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008352 File Offset: 0x00006552
		protected void InitializeTracingWriter(XmlWriter tracingWriter)
		{
			this._tracingWriter = tracingWriter;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000835B File Offset: 0x0000655B
		protected XmlDictionaryWriter InnerWriter
		{
			get
			{
				return this._innerWriter;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00008363 File Offset: 0x00006563
		public override void Close()
		{
			this._innerWriter.Close();
			if (this._tracingWriter != null)
			{
				this._tracingWriter.Close();
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00008383 File Offset: 0x00006583
		public override void Flush()
		{
			this._innerWriter.Flush();
			if (this._tracingWriter != null)
			{
				this._tracingWriter.Flush();
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000083A3 File Offset: 0x000065A3
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this._innerWriter.WriteBase64(buffer, index, count);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteBase64(buffer, index, count);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000083C9 File Offset: 0x000065C9
		public override void WriteCData(string text)
		{
			this._innerWriter.WriteCData(text);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteCData(text);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000083EB File Offset: 0x000065EB
		public override void WriteCharEntity(char ch)
		{
			this._innerWriter.WriteCharEntity(ch);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteCharEntity(ch);
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000840D File Offset: 0x0000660D
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this._innerWriter.WriteChars(buffer, index, count);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteChars(buffer, index, count);
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008433 File Offset: 0x00006633
		public override void WriteComment(string text)
		{
			this._innerWriter.WriteComment(text);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteComment(text);
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008455 File Offset: 0x00006655
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this._innerWriter.WriteDocType(name, pubid, sysid, subset);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteDocType(name, pubid, sysid, subset);
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000847F File Offset: 0x0000667F
		public override void WriteEndAttribute()
		{
			this._innerWriter.WriteEndAttribute();
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteEndAttribute();
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000849F File Offset: 0x0000669F
		public override void WriteEndDocument()
		{
			this._innerWriter.WriteEndDocument();
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteEndDocument();
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000084BF File Offset: 0x000066BF
		public override void WriteEndElement()
		{
			this._innerWriter.WriteEndElement();
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteEndElement();
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000084DF File Offset: 0x000066DF
		public override void WriteEntityRef(string name)
		{
			this._innerWriter.WriteEntityRef(name);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteEntityRef(name);
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008501 File Offset: 0x00006701
		public override void WriteFullEndElement()
		{
			this._innerWriter.WriteFullEndElement();
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteFullEndElement();
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008521 File Offset: 0x00006721
		public override void WriteProcessingInstruction(string name, string text)
		{
			this._innerWriter.WriteProcessingInstruction(name, text);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteProcessingInstruction(name, text);
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008545 File Offset: 0x00006745
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this._innerWriter.WriteRaw(buffer, index, count);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000856B File Offset: 0x0000676B
		public override void WriteRaw(string data)
		{
			this._innerWriter.WriteRaw(data);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteRaw(data);
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000858D File Offset: 0x0000678D
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this._innerWriter.WriteStartAttribute(prefix, localName, ns);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteStartAttribute(prefix, localName, ns);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000085B3 File Offset: 0x000067B3
		public override void WriteStartDocument()
		{
			this._innerWriter.WriteStartDocument();
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteStartDocument();
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000085D3 File Offset: 0x000067D3
		public override void WriteStartDocument(bool standalone)
		{
			this._innerWriter.WriteStartDocument(standalone);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteStartDocument(standalone);
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000085F5 File Offset: 0x000067F5
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this._innerWriter.WriteStartElement(prefix, localName, ns);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteStartElement(prefix, localName, ns);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000861B File Offset: 0x0000681B
		public override WriteState WriteState
		{
			get
			{
				return this._innerWriter.WriteState;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008628 File Offset: 0x00006828
		public override void WriteString(string text)
		{
			this._innerWriter.WriteString(text);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteString(text);
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000864A File Offset: 0x0000684A
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this._innerWriter.WriteSurrogateCharEntity(lowChar, highChar);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteSurrogateCharEntity(lowChar, highChar);
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000866E File Offset: 0x0000686E
		public override void WriteWhitespace(string ws)
		{
			this._innerWriter.WriteWhitespace(ws);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteWhitespace(ws);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008690 File Offset: 0x00006890
		public override void WriteXmlAttribute(string localName, string value)
		{
			this._innerWriter.WriteXmlAttribute(localName, value);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteAttributeString(localName, value);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000086B4 File Offset: 0x000068B4
		public override void WriteXmlnsAttribute(string prefix, string namespaceUri)
		{
			this._innerWriter.WriteXmlnsAttribute(prefix, namespaceUri);
			if (this._tracingWriter != null)
			{
				this._tracingWriter.WriteAttributeString(prefix, string.Empty, namespaceUri, string.Empty);
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000086E2 File Offset: 0x000068E2
		public override string LookupPrefix(string ns)
		{
			return this._innerWriter.LookupPrefix(ns);
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000086F0 File Offset: 0x000068F0
		public override bool CanCanonicalize
		{
			get
			{
				return this._innerWriter.CanCanonicalize;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000086FD File Offset: 0x000068FD
		public override void StartCanonicalization(Stream stream, bool includeComments, string[] inclusivePrefixes)
		{
			this._innerWriter.StartCanonicalization(stream, includeComments, inclusivePrefixes);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000870D File Offset: 0x0000690D
		public override void EndCanonicalization()
		{
			this._innerWriter.EndCanonicalization();
		}

		// Token: 0x04000130 RID: 304
		private XmlDictionaryWriter _innerWriter;

		// Token: 0x04000131 RID: 305
		private XmlWriter _tracingWriter;
	}
}
