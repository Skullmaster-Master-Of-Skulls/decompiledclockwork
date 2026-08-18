using System;
using System.Collections.Generic;

namespace System.Xml
{
	// Token: 0x0200005E RID: 94
	internal class QueryOutputWriter : XmlRawWriter
	{
		// Token: 0x06000351 RID: 849 RVA: 0x00010F0C File Offset: 0x0000FF0C
		public QueryOutputWriter(XmlRawWriter writer, XmlWriterSettings settings)
		{
			this.wrapped = writer;
			this.systemId = settings.DocTypeSystem;
			this.publicId = settings.DocTypePublic;
			if (settings.OutputMethod == XmlOutputMethod.Xml)
			{
				if (this.systemId != null)
				{
					this.outputDocType = true;
					this.checkWellFormedDoc = true;
				}
				if (settings.AutoXmlDeclaration && settings.Standalone == XmlStandalone.Yes)
				{
					this.checkWellFormedDoc = true;
				}
				if (settings.CDataSectionElements.Count > 0)
				{
					this.bitsCData = new BitStack();
					this.lookupCDataElems = new Dictionary<XmlQualifiedName, int>();
					this.qnameCData = new XmlQualifiedName();
					foreach (XmlQualifiedName key in settings.CDataSectionElements)
					{
						this.lookupCDataElems[key] = 0;
					}
					this.bitsCData.PushBit(false);
					return;
				}
			}
			else if (settings.OutputMethod == XmlOutputMethod.Html && (this.systemId != null || this.publicId != null))
			{
				this.outputDocType = true;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00011024 File Offset: 0x00010024
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0001102C File Offset: 0x0001002C
		internal override IXmlNamespaceResolver NamespaceResolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				this.resolver = value;
				this.wrapped.NamespaceResolver = value;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00011041 File Offset: 0x00010041
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.wrapped.WriteXmlDeclaration(standalone);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001104F File Offset: 0x0001004F
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.wrapped.WriteXmlDeclaration(xmldecl);
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00011060 File Offset: 0x00010060
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings settings = this.wrapped.Settings;
				settings.ReadOnly = false;
				settings.DocTypeSystem = this.systemId;
				settings.DocTypePublic = this.publicId;
				settings.ReadOnly = true;
				return settings;
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000110A0 File Offset: 0x000100A0
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.publicId == null && this.systemId == null)
			{
				this.wrapped.WriteDocType(name, pubid, sysid, subset);
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000110C4 File Offset: 0x000100C4
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			if (this.checkWellFormedDoc)
			{
				if (this.depth == 0 && this.hasDocElem)
				{
					throw new XmlException("Xml_NoMultipleRoots", string.Empty);
				}
				this.depth++;
				this.hasDocElem = true;
			}
			if (this.outputDocType)
			{
				this.wrapped.WriteDocType((prefix.Length != 0) ? (prefix + ":" + localName) : localName, this.publicId, this.systemId, null);
				this.outputDocType = false;
			}
			this.wrapped.WriteStartElement(prefix, localName, ns);
			if (this.lookupCDataElems != null)
			{
				this.qnameCData.Init(localName, ns);
				this.bitsCData.PushBit(this.lookupCDataElems.ContainsKey(this.qnameCData));
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00011191 File Offset: 0x00010191
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			this.wrapped.WriteEndElement(prefix, localName, ns);
			if (this.checkWellFormedDoc)
			{
				this.depth--;
			}
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000111D1 File Offset: 0x000101D1
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.EndCDataSection();
			this.wrapped.WriteFullEndElement(prefix, localName, ns);
			if (this.checkWellFormedDoc)
			{
				this.depth--;
			}
			if (this.lookupCDataElems != null)
			{
				this.bitsCData.PopBit();
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00011211 File Offset: 0x00010211
		internal override void StartElementContent()
		{
			this.wrapped.StartElementContent();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001121E File Offset: 0x0001021E
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttr = true;
			this.wrapped.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00011235 File Offset: 0x00010235
		public override void WriteEndAttribute()
		{
			this.inAttr = false;
			this.wrapped.WriteEndAttribute();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00011249 File Offset: 0x00010249
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.wrapped.WriteNamespaceDeclaration(prefix, ns);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00011258 File Offset: 0x00010258
		public override void WriteCData(string text)
		{
			this.wrapped.WriteCData(text);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00011266 File Offset: 0x00010266
		public override void WriteComment(string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteComment(text);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001127A File Offset: 0x0001027A
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.EndCDataSection();
			this.wrapped.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001128F File Offset: 0x0001028F
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(ws);
				return;
			}
			this.wrapped.WriteWhitespace(ws);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000112C2 File Offset: 0x000102C2
		public override void WriteString(string text)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(text);
				return;
			}
			this.wrapped.WriteString(text);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000112F5 File Offset: 0x000102F5
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteChars(buffer, index, count);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00011331 File Offset: 0x00010331
		public override void WriteEntityRef(string name)
		{
			this.EndCDataSection();
			this.wrapped.WriteEntityRef(name);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00011345 File Offset: 0x00010345
		public override void WriteCharEntity(char ch)
		{
			this.EndCDataSection();
			this.wrapped.WriteCharEntity(ch);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00011359 File Offset: 0x00010359
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.EndCDataSection();
			this.wrapped.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001136E File Offset: 0x0001036E
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(new string(buffer, index, count));
				return;
			}
			this.wrapped.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000113AA File Offset: 0x000103AA
		public override void WriteRaw(string data)
		{
			if (!this.inAttr && (this.inCDataSection || this.StartCDataSection()))
			{
				this.wrapped.WriteCData(data);
				return;
			}
			this.wrapped.WriteRaw(data);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000113DD File Offset: 0x000103DD
		public override void Close()
		{
			this.wrapped.Close();
			if (this.checkWellFormedDoc && !this.hasDocElem)
			{
				throw new XmlException("Xml_NoRoot", string.Empty);
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001140A File Offset: 0x0001040A
		public override void Flush()
		{
			this.wrapped.Flush();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00011417 File Offset: 0x00010417
		private bool StartCDataSection()
		{
			if (this.lookupCDataElems != null && this.bitsCData.PeekBit())
			{
				this.inCDataSection = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00011438 File Offset: 0x00010438
		private void EndCDataSection()
		{
			this.inCDataSection = false;
		}

		// Token: 0x0400058D RID: 1421
		private XmlRawWriter wrapped;

		// Token: 0x0400058E RID: 1422
		private bool inCDataSection;

		// Token: 0x0400058F RID: 1423
		private Dictionary<XmlQualifiedName, int> lookupCDataElems;

		// Token: 0x04000590 RID: 1424
		private BitStack bitsCData;

		// Token: 0x04000591 RID: 1425
		private XmlQualifiedName qnameCData;

		// Token: 0x04000592 RID: 1426
		private bool outputDocType;

		// Token: 0x04000593 RID: 1427
		private bool checkWellFormedDoc;

		// Token: 0x04000594 RID: 1428
		private bool hasDocElem;

		// Token: 0x04000595 RID: 1429
		private bool inAttr;

		// Token: 0x04000596 RID: 1430
		private string systemId;

		// Token: 0x04000597 RID: 1431
		private string publicId;

		// Token: 0x04000598 RID: 1432
		private int depth;
	}
}
