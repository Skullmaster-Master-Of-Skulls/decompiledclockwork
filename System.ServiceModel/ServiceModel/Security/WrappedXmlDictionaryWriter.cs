using System;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000291 RID: 657
	internal class WrappedXmlDictionaryWriter : XmlDictionaryWriter
	{
		// Token: 0x06001343 RID: 4931 RVA: 0x00046965 File Offset: 0x00044B65
		public WrappedXmlDictionaryWriter(XmlDictionaryWriter writer, string id)
		{
			this.innerWriter = writer;
			this.index = 0;
			this.insertId = false;
			this.isStrReferenceElement = false;
			this.id = id;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00046990 File Offset: 0x00044B90
		public override void WriteStartAttribute(string prefix, string localName, string namespaceUri)
		{
			if (this.isStrReferenceElement && this.insertId && localName == XD.UtilityDictionary.IdAttribute.Value)
			{
				this.insertId = false;
			}
			this.innerWriter.WriteStartAttribute(prefix, localName, namespaceUri);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000469D0 File Offset: 0x00044BD0
		public override void WriteStartElement(string prefix, string localName, string namespaceUri)
		{
			if (this.isStrReferenceElement && this.insertId)
			{
				if (this.id != null)
				{
					this.innerWriter.WriteAttributeString(XD.UtilityDictionary.Prefix.Value, XD.UtilityDictionary.IdAttribute, XD.UtilityDictionary.Namespace, this.id);
				}
				this.isStrReferenceElement = false;
				this.insertId = false;
			}
			this.index++;
			if (this.index == 1 && localName == XD.SecurityJan2004Dictionary.SecurityTokenReference.Value)
			{
				this.insertId = true;
				this.isStrReferenceElement = true;
			}
			this.innerWriter.WriteStartElement(prefix, localName, namespaceUri);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00046A81 File Offset: 0x00044C81
		public override void Close()
		{
			this.innerWriter.Close();
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00046A8E File Offset: 0x00044C8E
		public override void Flush()
		{
			this.innerWriter.Flush();
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00046A9B File Offset: 0x00044C9B
		public override string LookupPrefix(string ns)
		{
			return this.innerWriter.LookupPrefix(ns);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00046AA9 File Offset: 0x00044CA9
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.innerWriter.WriteBase64(buffer, index, count);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00046AB9 File Offset: 0x00044CB9
		public override void WriteCData(string text)
		{
			this.innerWriter.WriteCData(text);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00046AC7 File Offset: 0x00044CC7
		public override void WriteCharEntity(char ch)
		{
			this.innerWriter.WriteCharEntity(ch);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00046AD5 File Offset: 0x00044CD5
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.innerWriter.WriteChars(buffer, index, count);
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00046AE5 File Offset: 0x00044CE5
		public override void WriteComment(string text)
		{
			this.innerWriter.WriteComment(text);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x00046AF3 File Offset: 0x00044CF3
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.innerWriter.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00046B05 File Offset: 0x00044D05
		public override void WriteEndAttribute()
		{
			this.innerWriter.WriteEndAttribute();
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00046B12 File Offset: 0x00044D12
		public override void WriteEndDocument()
		{
			this.innerWriter.WriteEndDocument();
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00046B1F File Offset: 0x00044D1F
		public override void WriteEndElement()
		{
			this.innerWriter.WriteEndElement();
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00046B2C File Offset: 0x00044D2C
		public override void WriteEntityRef(string name)
		{
			this.innerWriter.WriteEntityRef(name);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00046B3A File Offset: 0x00044D3A
		public override void WriteFullEndElement()
		{
			this.innerWriter.WriteFullEndElement();
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00046B47 File Offset: 0x00044D47
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.innerWriter.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00046B56 File Offset: 0x00044D56
		public override void WriteRaw(string data)
		{
			this.innerWriter.WriteRaw(data);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00046B64 File Offset: 0x00044D64
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.innerWriter.WriteRaw(buffer, index, count);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00046B74 File Offset: 0x00044D74
		public override void WriteStartDocument(bool standalone)
		{
			this.innerWriter.WriteStartDocument(standalone);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00046B82 File Offset: 0x00044D82
		public override void WriteStartDocument()
		{
			this.innerWriter.WriteStartDocument();
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x00046B8F File Offset: 0x00044D8F
		public override WriteState WriteState
		{
			get
			{
				return this.innerWriter.WriteState;
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00046B9C File Offset: 0x00044D9C
		public override void WriteString(string text)
		{
			this.innerWriter.WriteString(text);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00046BAA File Offset: 0x00044DAA
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.innerWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00046BB9 File Offset: 0x00044DB9
		public override void WriteWhitespace(string ws)
		{
			this.innerWriter.WriteWhitespace(ws);
		}

		// Token: 0x04001A20 RID: 6688
		private XmlDictionaryWriter innerWriter;

		// Token: 0x04001A21 RID: 6689
		private int index;

		// Token: 0x04001A22 RID: 6690
		private bool insertId;

		// Token: 0x04001A23 RID: 6691
		private bool isStrReferenceElement;

		// Token: 0x04001A24 RID: 6692
		private string id;
	}
}
