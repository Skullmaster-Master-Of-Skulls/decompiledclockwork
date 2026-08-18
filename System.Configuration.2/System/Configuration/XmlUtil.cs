using System;
using System.Configuration.Internal;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020000A3 RID: 163
	internal sealed class XmlUtil : IDisposable, IConfigErrorInfo
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x0001DB75 File Offset: 0x0001BD75
		private static int GetPositionOffset(XmlNodeType nodeType)
		{
			return XmlUtil.s_positionOffset[(int)nodeType];
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001DB7E File Offset: 0x0001BD7E
		internal XmlUtil(Stream stream, string name, bool readToFirstElement) : this(stream, name, readToFirstElement, new ConfigurationSchemaErrors())
		{
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001DB90 File Offset: 0x0001BD90
		internal XmlUtil(Stream stream, string name, bool readToFirstElement, ConfigurationSchemaErrors schemaErrors)
		{
			try
			{
				this._streamName = name;
				this._stream = stream;
				this._reader = new XmlTextReader(this._stream);
				this._reader.XmlResolver = null;
				this._schemaErrors = schemaErrors;
				this._lastLineNumber = 1;
				this._lastLinePosition = 1;
				if (readToFirstElement)
				{
					this._reader.WhitespaceHandling = WhitespaceHandling.None;
					bool flag = false;
					while (!flag && this._reader.Read())
					{
						XmlNodeType nodeType = this._reader.NodeType;
						if (nodeType <= XmlNodeType.Comment)
						{
							if (nodeType == XmlNodeType.Element)
							{
								flag = true;
								continue;
							}
							if (nodeType == XmlNodeType.Comment)
							{
								continue;
							}
						}
						else if (nodeType == XmlNodeType.DocumentType || nodeType == XmlNodeType.XmlDeclaration)
						{
							continue;
						}
						throw new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_element"), this);
					}
				}
			}
			catch
			{
				this.ReleaseResources();
				throw;
			}
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001DC60 File Offset: 0x0001BE60
		private void ReleaseResources()
		{
			if (this._reader != null)
			{
				this._reader.Close();
				this._reader = null;
			}
			else if (this._stream != null)
			{
				this._stream.Close();
			}
			this._stream = null;
			if (this._cachedStringWriter != null)
			{
				this._cachedStringWriter.Close();
				this._cachedStringWriter = null;
			}
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001DCBD File Offset: 0x0001BEBD
		public void Dispose()
		{
			this.ReleaseResources();
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0001DCC5 File Offset: 0x0001BEC5
		public string Filename
		{
			get
			{
				return this._streamName;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001DCCD File Offset: 0x0001BECD
		public int LineNumber
		{
			get
			{
				return this.Reader.LineNumber;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0001DCDC File Offset: 0x0001BEDC
		internal int TrueLinePosition
		{
			get
			{
				return this.Reader.LinePosition - XmlUtil.GetPositionOffset(this.Reader.NodeType);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001DD07 File Offset: 0x0001BF07
		internal XmlTextReader Reader
		{
			get
			{
				return this._reader;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0001DD0F File Offset: 0x0001BF0F
		internal ConfigurationSchemaErrors SchemaErrors
		{
			get
			{
				return this._schemaErrors;
			}
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001DD17 File Offset: 0x0001BF17
		internal void ReadToNextElement()
		{
			while (this._reader.Read())
			{
				if (this._reader.MoveToContent() == XmlNodeType.Element)
				{
					return;
				}
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001DD38 File Offset: 0x0001BF38
		internal void SkipToNextElement()
		{
			this._reader.Skip();
			this._reader.MoveToContent();
			while (!this._reader.EOF && this._reader.NodeType != XmlNodeType.Element)
			{
				this._reader.Read();
				this._reader.MoveToContent();
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001DD91 File Offset: 0x0001BF91
		internal void StrictReadToNextElement(ExceptionAction action)
		{
			while (this._reader.Read())
			{
				if (this._reader.NodeType == XmlNodeType.Element)
				{
					return;
				}
				this.VerifyIgnorableNodeType(action);
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		internal void StrictSkipToNextElement(ExceptionAction action)
		{
			this._reader.Skip();
			while (!this._reader.EOF && this._reader.NodeType != XmlNodeType.Element)
			{
				this.VerifyIgnorableNodeType(action);
				this._reader.Read();
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001DDF8 File Offset: 0x0001BFF8
		internal void StrictSkipToOurParentsEndElement(ExceptionAction action)
		{
			int depth = this._reader.Depth;
			while (this._reader.Depth >= depth)
			{
				this._reader.Skip();
			}
			while (!this._reader.EOF && this._reader.NodeType != XmlNodeType.EndElement)
			{
				this.VerifyIgnorableNodeType(action);
				this._reader.Read();
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001DE60 File Offset: 0x0001C060
		internal void VerifyIgnorableNodeType(ExceptionAction action)
		{
			XmlNodeType nodeType = this._reader.NodeType;
			if (nodeType != XmlNodeType.Comment && nodeType != XmlNodeType.EndElement)
			{
				ConfigurationException ce = new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_element"), this);
				this.SchemaErrors.AddError(ce, action);
			}
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001DEA0 File Offset: 0x0001C0A0
		internal void VerifyNoUnrecognizedAttributes(ExceptionAction action)
		{
			if (this._reader.MoveToNextAttribute())
			{
				this.AddErrorUnrecognizedAttribute(action);
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001DEB6 File Offset: 0x0001C0B6
		internal bool VerifyRequiredAttribute(object o, string attrName, ExceptionAction action)
		{
			if (o == null)
			{
				this.AddErrorRequiredAttribute(attrName, action);
				return false;
			}
			return true;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001DEC8 File Offset: 0x0001C0C8
		internal void AddErrorUnrecognizedAttribute(ExceptionAction action)
		{
			ConfigurationErrorsException ce = new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_attribute", new object[]
			{
				this._reader.Name
			}), this);
			this.SchemaErrors.AddError(ce, action);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001DF08 File Offset: 0x0001C108
		internal void AddErrorRequiredAttribute(string attrib, ExceptionAction action)
		{
			ConfigurationErrorsException ce = new ConfigurationErrorsException(SR.GetString("Config_missing_required_attribute", new object[]
			{
				attrib,
				this._reader.Name
			}), this);
			this.SchemaErrors.AddError(ce, action);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001DF4C File Offset: 0x0001C14C
		internal void AddErrorReservedAttribute(ExceptionAction action)
		{
			ConfigurationErrorsException ce = new ConfigurationErrorsException(SR.GetString("Config_reserved_attribute", new object[]
			{
				this._reader.Name
			}), this);
			this.SchemaErrors.AddError(ce, action);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001DF8C File Offset: 0x0001C18C
		internal void AddErrorUnrecognizedElement(ExceptionAction action)
		{
			ConfigurationErrorsException ce = new ConfigurationErrorsException(SR.GetString("Config_base_unrecognized_element"), this);
			this.SchemaErrors.AddError(ce, action);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001DFB8 File Offset: 0x0001C1B8
		internal void VerifyAndGetNonEmptyStringAttribute(ExceptionAction action, out string newValue)
		{
			if (!string.IsNullOrEmpty(this._reader.Value))
			{
				newValue = this._reader.Value;
				return;
			}
			newValue = null;
			ConfigurationException ce = new ConfigurationErrorsException(SR.GetString("Empty_attribute", new object[]
			{
				this._reader.Name
			}), this);
			this.SchemaErrors.AddError(ce, action);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001E01C File Offset: 0x0001C21C
		internal void VerifyAndGetBooleanAttribute(ExceptionAction action, bool defaultValue, out bool newValue)
		{
			if (this._reader.Value == "true")
			{
				newValue = true;
				return;
			}
			if (this._reader.Value == "false")
			{
				newValue = false;
				return;
			}
			newValue = defaultValue;
			ConfigurationErrorsException ce = new ConfigurationErrorsException(SR.GetString("Config_invalid_boolean_attribute", new object[]
			{
				this._reader.Name
			}), this);
			this.SchemaErrors.AddError(ce, action);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001E094 File Offset: 0x0001C294
		internal bool CopyOuterXmlToNextElement(XmlUtilWriter utilWriter, bool limitDepth)
		{
			this.CopyElement(utilWriter);
			return this.CopyReaderToNextElement(utilWriter, limitDepth);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001E0A8 File Offset: 0x0001C2A8
		internal bool SkipChildElementsAndCopyOuterXmlToNextElement(XmlUtilWriter utilWriter)
		{
			bool isEmptyElement = this._reader.IsEmptyElement;
			int lineNumber = this._reader.LineNumber;
			this.CopyXmlNode(utilWriter);
			if (!isEmptyElement)
			{
				while (this._reader.NodeType != XmlNodeType.EndElement)
				{
					if (this._reader.NodeType == XmlNodeType.Element)
					{
						this._reader.Skip();
						if (this._reader.NodeType == XmlNodeType.Whitespace)
						{
							this._reader.Skip();
						}
					}
					else
					{
						this.CopyXmlNode(utilWriter);
					}
				}
				if (this._reader.LineNumber != lineNumber)
				{
					utilWriter.AppendSpacesToLinePosition(this.TrueLinePosition);
				}
				this.CopyXmlNode(utilWriter);
			}
			return this.CopyReaderToNextElement(utilWriter, true);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001E154 File Offset: 0x0001C354
		internal bool CopyReaderToNextElement(XmlUtilWriter utilWriter, bool limitDepth)
		{
			bool flag = true;
			int num;
			if (limitDepth)
			{
				if (this._reader.NodeType == XmlNodeType.EndElement)
				{
					return true;
				}
				num = this._reader.Depth;
			}
			else
			{
				num = 0;
			}
			while (this._reader.NodeType != XmlNodeType.Element && this._reader.Depth >= num)
			{
				flag = this.CopyXmlNode(utilWriter);
				if (!flag)
				{
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001E1B0 File Offset: 0x0001C3B0
		internal bool SkipAndCopyReaderToNextElement(XmlUtilWriter utilWriter, bool limitDepth)
		{
			if (!utilWriter.IsLastLineBlank)
			{
				this._reader.Skip();
				return this.CopyReaderToNextElement(utilWriter, limitDepth);
			}
			int num;
			if (limitDepth)
			{
				num = this._reader.Depth;
			}
			else
			{
				num = 0;
			}
			this._reader.Skip();
			int lineNumber = this._reader.LineNumber;
			while (!this._reader.EOF)
			{
				if (this._reader.NodeType != XmlNodeType.Whitespace)
				{
					if (this._reader.LineNumber > lineNumber)
					{
						utilWriter.SeekToLineStart();
						utilWriter.AppendWhiteSpace(lineNumber + 1, 1, this.LineNumber, this.TrueLinePosition);
					}
					IL_C3:
					while (!this._reader.EOF && this._reader.NodeType != XmlNodeType.Element && this._reader.Depth >= num)
					{
						this.CopyXmlNode(utilWriter);
					}
					return !this._reader.EOF;
				}
				this._reader.Read();
			}
			goto IL_C3;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001E29C File Offset: 0x0001C49C
		private void CopyElement(XmlUtilWriter utilWriter)
		{
			int depth = this._reader.Depth;
			bool isEmptyElement = this._reader.IsEmptyElement;
			this.CopyXmlNode(utilWriter);
			while (this._reader.Depth > depth)
			{
				this.CopyXmlNode(utilWriter);
			}
			if (!isEmptyElement)
			{
				this.CopyXmlNode(utilWriter);
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001E2EC File Offset: 0x0001C4EC
		internal bool CopyXmlNode(XmlUtilWriter utilWriter)
		{
			string text = null;
			int fromLineNumber = -1;
			int fromLinePosition = -1;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			if (utilWriter.TrackPosition)
			{
				num = this._reader.LineNumber;
				num2 = this._reader.LinePosition;
				num3 = utilWriter.LineNumber;
				num4 = utilWriter.LinePosition;
			}
			XmlNodeType nodeType = this._reader.NodeType;
			if (nodeType == XmlNodeType.Whitespace)
			{
				utilWriter.Write(this._reader.Value);
			}
			else if (nodeType == XmlNodeType.Element)
			{
				text = (this._reader.IsEmptyElement ? "/>" : ">");
				fromLineNumber = this._reader.LineNumber;
				fromLinePosition = this._reader.LinePosition + this._reader.Name.Length;
				utilWriter.Write('<');
				utilWriter.Write(this._reader.Name);
				while (this._reader.MoveToNextAttribute())
				{
					int lineNumber = this._reader.LineNumber;
					int linePosition = this._reader.LinePosition;
					utilWriter.AppendRequiredWhiteSpace(fromLineNumber, fromLinePosition, lineNumber, linePosition);
					int num5 = utilWriter.Write(this._reader.Name);
					num5 += utilWriter.Write('=');
					num5 += utilWriter.AppendAttributeValue(this._reader);
					fromLineNumber = lineNumber;
					fromLinePosition = linePosition + num5;
				}
			}
			else if (nodeType == XmlNodeType.EndElement)
			{
				text = ">";
				fromLineNumber = this._reader.LineNumber;
				fromLinePosition = this._reader.LinePosition + this._reader.Name.Length;
				utilWriter.Write("</");
				utilWriter.Write(this._reader.Name);
			}
			else if (nodeType == XmlNodeType.Comment)
			{
				utilWriter.AppendComment(this._reader.Value);
			}
			else if (nodeType == XmlNodeType.Text)
			{
				utilWriter.AppendEscapeTextString(this._reader.Value);
			}
			else if (nodeType == XmlNodeType.XmlDeclaration)
			{
				text = "?>";
				fromLineNumber = this._reader.LineNumber;
				fromLinePosition = this._reader.LinePosition + 3;
				utilWriter.Write("<?xml");
				while (this._reader.MoveToNextAttribute())
				{
					int lineNumber2 = this._reader.LineNumber;
					int linePosition2 = this._reader.LinePosition;
					utilWriter.AppendRequiredWhiteSpace(fromLineNumber, fromLinePosition, lineNumber2, linePosition2);
					int num6 = utilWriter.Write(this._reader.Name);
					num6 += utilWriter.Write('=');
					num6 += utilWriter.AppendAttributeValue(this._reader);
					fromLineNumber = lineNumber2;
					fromLinePosition = linePosition2 + num6;
				}
				this._reader.MoveToElement();
			}
			else if (nodeType == XmlNodeType.SignificantWhitespace)
			{
				utilWriter.Write(this._reader.Value);
			}
			else if (nodeType == XmlNodeType.ProcessingInstruction)
			{
				utilWriter.AppendProcessingInstruction(this._reader.Name, this._reader.Value);
			}
			else if (nodeType == XmlNodeType.EntityReference)
			{
				utilWriter.AppendEntityRef(this._reader.Name);
			}
			else if (nodeType == XmlNodeType.CDATA)
			{
				utilWriter.AppendCData(this._reader.Value);
			}
			else if (nodeType == XmlNodeType.DocumentType)
			{
				int num7 = utilWriter.Write("<!DOCTYPE");
				utilWriter.AppendRequiredWhiteSpace(this._lastLineNumber, this._lastLinePosition + num7, this._reader.LineNumber, this._reader.LinePosition);
				utilWriter.Write(this._reader.Name);
				string text2 = null;
				if (this._reader.HasValue)
				{
					text2 = this._reader.Value;
				}
				fromLineNumber = this._reader.LineNumber;
				fromLinePosition = this._reader.LinePosition + this._reader.Name.Length;
				if (this._reader.MoveToFirstAttribute())
				{
					utilWriter.AppendRequiredWhiteSpace(fromLineNumber, fromLinePosition, this._reader.LineNumber, this._reader.LinePosition);
					string name = this._reader.Name;
					utilWriter.Write(name);
					utilWriter.AppendSpace();
					utilWriter.AppendAttributeValue(this._reader);
					this._reader.MoveToAttribute(0);
					if (name == "PUBLIC")
					{
						this._reader.MoveToAttribute(1);
						utilWriter.AppendSpace();
						utilWriter.AppendAttributeValue(this._reader);
						this._reader.MoveToAttribute(1);
					}
				}
				if (text2 != null && text2.Length > 0)
				{
					utilWriter.Write(" [");
					utilWriter.Write(text2);
					utilWriter.Write(']');
				}
				utilWriter.Write('>');
			}
			bool result = this._reader.Read();
			nodeType = this._reader.NodeType;
			if (text != null)
			{
				int positionOffset = XmlUtil.GetPositionOffset(nodeType);
				int lineNumber3 = this._reader.LineNumber;
				int toLinePosition = this._reader.LinePosition - positionOffset - text.Length;
				utilWriter.AppendWhiteSpace(fromLineNumber, fromLinePosition, lineNumber3, toLinePosition);
				utilWriter.Write(text);
			}
			if (utilWriter.TrackPosition)
			{
				this._lastLineNumber = num - num3 + utilWriter.LineNumber;
				if (num3 == utilWriter.LineNumber)
				{
					this._lastLinePosition = num2 - num4 + utilWriter.LinePosition;
				}
				else
				{
					this._lastLinePosition = utilWriter.LinePosition;
				}
			}
			return result;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001E814 File Offset: 0x0001CA14
		private string RetrieveFullOpenElementTag()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("<");
			stringBuilder.Append(this._reader.Name);
			while (this._reader.MoveToNextAttribute())
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(this._reader.Name);
				stringBuilder.Append("=");
				stringBuilder.Append('"');
				stringBuilder.Append(this._reader.Value);
				stringBuilder.Append('"');
			}
			stringBuilder.Append(">");
			return stringBuilder.ToString();
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001E8B8 File Offset: 0x0001CAB8
		internal string UpdateStartElement(XmlUtilWriter utilWriter, string updatedStartElement, bool needsChildren, int linePosition, int indent)
		{
			string result = null;
			bool flag = false;
			string name = this._reader.Name;
			if (this._reader.IsEmptyElement)
			{
				if (updatedStartElement == null && needsChildren)
				{
					updatedStartElement = this.RetrieveFullOpenElementTag();
				}
				flag = (updatedStartElement != null);
			}
			if (updatedStartElement == null)
			{
				this.CopyXmlNode(utilWriter);
			}
			else
			{
				string str = "</" + name + ">";
				string xmlElement = updatedStartElement + str;
				string text = XmlUtil.FormatXmlElement(xmlElement, linePosition, indent, true);
				int num = text.LastIndexOf('\n') + 1;
				string s;
				if (flag)
				{
					result = text.Substring(num);
					s = text.Substring(0, num);
				}
				else
				{
					s = text.Substring(0, num - 2);
				}
				utilWriter.Write(s);
				this._reader.Read();
			}
			return result;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001E978 File Offset: 0x0001CB78
		private void ResetCachedStringWriter()
		{
			if (this._cachedStringWriter == null)
			{
				this._cachedStringWriter = new StringWriter(new StringBuilder(64), CultureInfo.InvariantCulture);
				return;
			}
			this._cachedStringWriter.GetStringBuilder().Length = 0;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001E9AC File Offset: 0x0001CBAC
		internal string CopySection()
		{
			this.ResetCachedStringWriter();
			WhitespaceHandling whitespaceHandling = this._reader.WhitespaceHandling;
			this._reader.WhitespaceHandling = WhitespaceHandling.All;
			XmlUtilWriter xmlUtilWriter = new XmlUtilWriter(this._cachedStringWriter, false);
			this.CopyElement(xmlUtilWriter);
			this._reader.WhitespaceHandling = whitespaceHandling;
			if (whitespaceHandling == WhitespaceHandling.None && this.Reader.NodeType == XmlNodeType.Whitespace)
			{
				this._reader.Read();
			}
			xmlUtilWriter.Flush();
			return ((StringWriter)xmlUtilWriter.Writer).ToString();
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001EA30 File Offset: 0x0001CC30
		internal static string FormatXmlElement(string xmlElement, int linePosition, int indent, bool skipFirstIndent)
		{
			XmlParserContext context = new XmlParserContext(null, null, null, XmlSpace.Default, Encoding.Unicode);
			XmlTextReader xmlTextReader = new XmlTextReader(xmlElement, XmlNodeType.Element, context);
			StringWriter writer = new StringWriter(new StringBuilder(64), CultureInfo.InvariantCulture);
			XmlUtilWriter xmlUtilWriter = new XmlUtilWriter(writer, false);
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			while (xmlTextReader.Read())
			{
				XmlNodeType nodeType = xmlTextReader.NodeType;
				int num2;
				if (flag2)
				{
					xmlUtilWriter.Flush();
					num2 = num - ((StringWriter)xmlUtilWriter.Writer).GetStringBuilder().Length;
				}
				else
				{
					num2 = 0;
				}
				if (nodeType <= XmlNodeType.CDATA)
				{
					if (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.CDATA)
					{
						goto IL_8D;
					}
				}
				else if (nodeType == XmlNodeType.Comment || nodeType == XmlNodeType.EndElement)
				{
					goto IL_8D;
				}
				IL_C6:
				flag2 = false;
				switch (nodeType)
				{
				case XmlNodeType.Element:
				{
					xmlUtilWriter.Write('<');
					xmlUtilWriter.Write(xmlTextReader.Name);
					num2 += xmlTextReader.Name.Length + 2;
					int attributeCount = xmlTextReader.AttributeCount;
					for (int i = 0; i < attributeCount; i++)
					{
						bool flag3;
						if (num2 > 60)
						{
							xmlUtilWriter.AppendIndent(linePosition, indent, xmlTextReader.Depth - 1, true);
							num2 = indent;
							flag3 = false;
							xmlUtilWriter.Flush();
							num = ((StringWriter)xmlUtilWriter.Writer).GetStringBuilder().Length;
						}
						else
						{
							flag3 = true;
						}
						xmlTextReader.MoveToNextAttribute();
						xmlUtilWriter.Flush();
						int length = ((StringWriter)xmlUtilWriter.Writer).GetStringBuilder().Length;
						if (flag3)
						{
							xmlUtilWriter.AppendSpace();
						}
						xmlUtilWriter.Write(xmlTextReader.Name);
						xmlUtilWriter.Write('=');
						xmlUtilWriter.AppendAttributeValue(xmlTextReader);
						xmlUtilWriter.Flush();
						num2 += ((StringWriter)xmlUtilWriter.Writer).GetStringBuilder().Length - length;
					}
					xmlTextReader.MoveToElement();
					if (xmlTextReader.IsEmptyElement)
					{
						xmlUtilWriter.Write(" />");
					}
					else
					{
						xmlUtilWriter.Write('>');
					}
					break;
				}
				case XmlNodeType.Text:
					xmlUtilWriter.AppendEscapeTextString(xmlTextReader.Value);
					flag2 = true;
					break;
				case XmlNodeType.CDATA:
					xmlUtilWriter.AppendCData(xmlTextReader.Value);
					break;
				case XmlNodeType.EntityReference:
					xmlUtilWriter.AppendEntityRef(xmlTextReader.Name);
					break;
				case XmlNodeType.ProcessingInstruction:
					xmlUtilWriter.AppendProcessingInstruction(xmlTextReader.Name, xmlTextReader.Value);
					break;
				case XmlNodeType.Comment:
					xmlUtilWriter.AppendComment(xmlTextReader.Value);
					break;
				case XmlNodeType.SignificantWhitespace:
					xmlUtilWriter.Write(xmlTextReader.Value);
					break;
				case XmlNodeType.EndElement:
					xmlUtilWriter.Write("</");
					xmlUtilWriter.Write(xmlTextReader.Name);
					xmlUtilWriter.Write('>');
					break;
				}
				flag = true;
				skipFirstIndent = false;
				continue;
				IL_8D:
				if (skipFirstIndent || flag2)
				{
					goto IL_C6;
				}
				xmlUtilWriter.AppendIndent(linePosition, indent, xmlTextReader.Depth, flag);
				if (flag)
				{
					xmlUtilWriter.Flush();
					num = ((StringWriter)xmlUtilWriter.Writer).GetStringBuilder().Length;
					goto IL_C6;
				}
				goto IL_C6;
			}
			xmlUtilWriter.Flush();
			return ((StringWriter)xmlUtilWriter.Writer).ToString();
		}

		// Token: 0x04000369 RID: 873
		private const int MAX_LINE_WIDTH = 60;

		// Token: 0x0400036A RID: 874
		private static readonly int[] s_positionOffset = new int[]
		{
			0,
			1,
			-1,
			0,
			9,
			1,
			-1,
			2,
			4,
			-1,
			10,
			-1,
			-1,
			0,
			0,
			2,
			-1,
			2
		};

		// Token: 0x0400036B RID: 875
		private Stream _stream;

		// Token: 0x0400036C RID: 876
		private string _streamName;

		// Token: 0x0400036D RID: 877
		private XmlTextReader _reader;

		// Token: 0x0400036E RID: 878
		private StringWriter _cachedStringWriter;

		// Token: 0x0400036F RID: 879
		private ConfigurationSchemaErrors _schemaErrors;

		// Token: 0x04000370 RID: 880
		private int _lastLineNumber;

		// Token: 0x04000371 RID: 881
		private int _lastLinePosition;
	}
}
