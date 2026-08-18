using System;
using System.IO;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000F8 RID: 248
	internal sealed class DataTextWriter : XmlWriter
	{
		// Token: 0x06000E45 RID: 3653 RVA: 0x002217D8 File Offset: 0x00220BD8
		internal static XmlWriter CreateWriter(XmlWriter xw)
		{
			return new DataTextWriter(xw);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x002217F8 File Offset: 0x00220BF8
		private DataTextWriter(XmlWriter w)
		{
			this._xmltextWriter = w;
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00221818 File Offset: 0x00220C18
		internal Stream BaseStream
		{
			get
			{
				XmlTextWriter xmlTextWriter = this._xmltextWriter as XmlTextWriter;
				if (xmlTextWriter != null)
				{
					return xmlTextWriter.BaseStream;
				}
				return null;
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00221848 File Offset: 0x00220C48
		public override void WriteStartDocument()
		{
			this._xmltextWriter.WriteStartDocument();
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00221868 File Offset: 0x00220C68
		public override void WriteStartDocument(bool standalone)
		{
			this._xmltextWriter.WriteStartDocument(standalone);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00221888 File Offset: 0x00220C88
		public override void WriteEndDocument()
		{
			this._xmltextWriter.WriteEndDocument();
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x002218A8 File Offset: 0x00220CA8
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this._xmltextWriter.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x002218C8 File Offset: 0x00220CC8
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this._xmltextWriter.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x002218E8 File Offset: 0x00220CE8
		public override void WriteEndElement()
		{
			this._xmltextWriter.WriteEndElement();
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00221908 File Offset: 0x00220D08
		public override void WriteFullEndElement()
		{
			this._xmltextWriter.WriteFullEndElement();
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00221928 File Offset: 0x00220D28
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this._xmltextWriter.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x00221948 File Offset: 0x00220D48
		public override void WriteEndAttribute()
		{
			this._xmltextWriter.WriteEndAttribute();
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00221968 File Offset: 0x00220D68
		public override void WriteCData(string text)
		{
			this._xmltextWriter.WriteCData(text);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x00221988 File Offset: 0x00220D88
		public override void WriteComment(string text)
		{
			this._xmltextWriter.WriteComment(text);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x002219A8 File Offset: 0x00220DA8
		public override void WriteProcessingInstruction(string name, string text)
		{
			this._xmltextWriter.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x002219C8 File Offset: 0x00220DC8
		public override void WriteEntityRef(string name)
		{
			this._xmltextWriter.WriteEntityRef(name);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x002219E8 File Offset: 0x00220DE8
		public override void WriteCharEntity(char ch)
		{
			this._xmltextWriter.WriteCharEntity(ch);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00221A08 File Offset: 0x00220E08
		public override void WriteWhitespace(string ws)
		{
			this._xmltextWriter.WriteWhitespace(ws);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00221A28 File Offset: 0x00220E28
		public override void WriteString(string text)
		{
			this._xmltextWriter.WriteString(text);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00221A48 File Offset: 0x00220E48
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this._xmltextWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00221A68 File Offset: 0x00220E68
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteChars(buffer, index, count);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00221A88 File Offset: 0x00220E88
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00221AA8 File Offset: 0x00220EA8
		public override void WriteRaw(string data)
		{
			this._xmltextWriter.WriteRaw(data);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00221AC8 File Offset: 0x00220EC8
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteBase64(buffer, index, count);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00221AE8 File Offset: 0x00220EE8
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteBinHex(buffer, index, count);
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00221B08 File Offset: 0x00220F08
		public override WriteState WriteState
		{
			get
			{
				return this._xmltextWriter.WriteState;
			}
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00221B28 File Offset: 0x00220F28
		public override void Close()
		{
			this._xmltextWriter.Close();
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00221B48 File Offset: 0x00220F48
		public override void Flush()
		{
			this._xmltextWriter.Flush();
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00221B68 File Offset: 0x00220F68
		public override void WriteName(string name)
		{
			this._xmltextWriter.WriteName(name);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00221B88 File Offset: 0x00220F88
		public override void WriteQualifiedName(string localName, string ns)
		{
			this._xmltextWriter.WriteQualifiedName(localName, ns);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00221BA8 File Offset: 0x00220FA8
		public override string LookupPrefix(string ns)
		{
			return this._xmltextWriter.LookupPrefix(ns);
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00221BC8 File Offset: 0x00220FC8
		public override XmlSpace XmlSpace
		{
			get
			{
				return this._xmltextWriter.XmlSpace;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00221BE8 File Offset: 0x00220FE8
		public override string XmlLang
		{
			get
			{
				return this._xmltextWriter.XmlLang;
			}
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x00221C08 File Offset: 0x00221008
		public override void WriteNmToken(string name)
		{
			this._xmltextWriter.WriteNmToken(name);
		}

		// Token: 0x04000A8F RID: 2703
		private XmlWriter _xmltextWriter;
	}
}
