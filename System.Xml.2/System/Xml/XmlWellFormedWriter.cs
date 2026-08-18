using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x020000E1 RID: 225
	internal class XmlWellFormedWriter : XmlWriter
	{
		// Token: 0x06000E26 RID: 3622 RVA: 0x0003BE74 File Offset: 0x0003A074
		internal XmlWellFormedWriter(XmlWriter writer, XmlWriterSettings settings)
		{
			this.writer = writer;
			this.rawWriter = (writer as XmlRawWriter);
			this.predefinedNamespaces = (writer as IXmlNamespaceResolver);
			if (this.rawWriter != null)
			{
				this.rawWriter.NamespaceResolver = new XmlWellFormedWriter.NamespaceResolverProxy(this);
			}
			this.checkCharacters = settings.CheckCharacters;
			this.omitDuplNamespaces = ((settings.NamespaceHandling & NamespaceHandling.OmitDuplicates) > NamespaceHandling.Default);
			this.writeEndDocumentOnClose = settings.WriteEndDocumentOnClose;
			this.conformanceLevel = settings.ConformanceLevel;
			this.stateTable = ((this.conformanceLevel == ConformanceLevel.Document) ? XmlWellFormedWriter.StateTableDocument : XmlWellFormedWriter.StateTableAuto);
			this.currentState = XmlWellFormedWriter.State.Start;
			this.nsStack = new XmlWellFormedWriter.Namespace[8];
			this.nsStack[0].Set("xmlns", "http://www.w3.org/2000/xmlns/", XmlWellFormedWriter.NamespaceKind.Special);
			this.nsStack[1].Set("xml", "http://www.w3.org/XML/1998/namespace", XmlWellFormedWriter.NamespaceKind.Special);
			if (this.predefinedNamespaces == null)
			{
				this.nsStack[2].Set(string.Empty, string.Empty, XmlWellFormedWriter.NamespaceKind.Implied);
			}
			else
			{
				string text = this.predefinedNamespaces.LookupNamespace(string.Empty);
				this.nsStack[2].Set(string.Empty, (text == null) ? string.Empty : text, XmlWellFormedWriter.NamespaceKind.Implied);
			}
			this.nsTop = 2;
			this.elemScopeStack = new XmlWellFormedWriter.ElementScope[8];
			this.elemScopeStack[0].Set(string.Empty, string.Empty, string.Empty, this.nsTop);
			this.elemScopeStack[0].xmlSpace = XmlSpace.None;
			this.elemScopeStack[0].xmlLang = null;
			this.elemTop = 0;
			this.attrStack = new XmlWellFormedWriter.AttrName[8];
			this.hasher = new SecureStringHasher();
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0003C039 File Offset: 0x0003A239
		public override WriteState WriteState
		{
			get
			{
				if (this.currentState <= XmlWellFormedWriter.State.Error)
				{
					return XmlWellFormedWriter.state2WriteState[(int)this.currentState];
				}
				return WriteState.Error;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0003C054 File Offset: 0x0003A254
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings settings = this.writer.Settings;
				settings.ReadOnly = false;
				settings.ConformanceLevel = this.conformanceLevel;
				if (this.omitDuplNamespaces)
				{
					settings.NamespaceHandling |= NamespaceHandling.OmitDuplicates;
				}
				settings.WriteEndDocumentOnClose = this.writeEndDocumentOnClose;
				settings.ReadOnly = true;
				return settings;
			}
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0003C0AA File Offset: 0x0003A2AA
		public override void WriteStartDocument()
		{
			this.WriteStartDocumentImpl(XmlStandalone.Omit);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0003C0B3 File Offset: 0x0003A2B3
		public override void WriteStartDocument(bool standalone)
		{
			this.WriteStartDocumentImpl(standalone ? XmlStandalone.Yes : XmlStandalone.No);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0003C0C4 File Offset: 0x0003A2C4
		public override void WriteEndDocument()
		{
			try
			{
				while (this.elemTop > 0)
				{
					this.WriteEndElement();
				}
				XmlWellFormedWriter.State state = this.currentState;
				this.AdvanceState(XmlWellFormedWriter.Token.EndDocument);
				if (state != XmlWellFormedWriter.State.AfterRootEle)
				{
					throw new ArgumentException(Res.GetString("Xml_NoRoot"));
				}
				if (this.rawWriter == null)
				{
					this.writer.WriteEndDocument();
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0003C134 File Offset: 0x0003A334
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Xml_EmptyName"));
				}
				XmlConvert.VerifyQName(name, ExceptionType.XmlException);
				if (this.conformanceLevel == ConformanceLevel.Fragment)
				{
					throw new InvalidOperationException(Res.GetString("Xml_DtdNotAllowedInFragment"));
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Dtd);
				if (this.dtdWritten)
				{
					this.currentState = XmlWellFormedWriter.State.Error;
					throw new InvalidOperationException(Res.GetString("Xml_DtdAlreadyWritten"));
				}
				if (this.conformanceLevel == ConformanceLevel.Auto)
				{
					this.conformanceLevel = ConformanceLevel.Document;
					this.stateTable = XmlWellFormedWriter.StateTableDocument;
				}
				if (this.checkCharacters)
				{
					int invCharIndex;
					if (pubid != null && (invCharIndex = this.xmlCharType.IsPublicId(pubid)) >= 0)
					{
						string name2 = "Xml_InvalidCharacter";
						object[] args = XmlException.BuildCharExceptionArgs(pubid, invCharIndex);
						throw new ArgumentException(Res.GetString(name2, args), "pubid");
					}
					if (sysid != null && (invCharIndex = this.xmlCharType.IsOnlyCharData(sysid)) >= 0)
					{
						string name3 = "Xml_InvalidCharacter";
						object[] args = XmlException.BuildCharExceptionArgs(sysid, invCharIndex);
						throw new ArgumentException(Res.GetString(name3, args), "sysid");
					}
					if (subset != null && (invCharIndex = this.xmlCharType.IsOnlyCharData(subset)) >= 0)
					{
						string name4 = "Xml_InvalidCharacter";
						object[] args = XmlException.BuildCharExceptionArgs(subset, invCharIndex);
						throw new ArgumentException(Res.GetString(name4, args), "subset");
					}
				}
				this.writer.WriteDocType(name, pubid, sysid, subset);
				this.dtdWritten = true;
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0003C2A4 File Offset: 0x0003A4A4
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			try
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Xml_EmptyLocalName"));
				}
				this.CheckNCName(localName);
				this.AdvanceState(XmlWellFormedWriter.Token.StartElement);
				if (prefix == null)
				{
					if (ns != null)
					{
						prefix = this.LookupPrefix(ns);
					}
					if (prefix == null)
					{
						prefix = string.Empty;
					}
				}
				else if (prefix.Length > 0)
				{
					this.CheckNCName(prefix);
					if (ns == null)
					{
						ns = this.LookupNamespace(prefix);
					}
					if (ns == null || (ns != null && ns.Length == 0))
					{
						throw new ArgumentException(Res.GetString("Xml_PrefixForEmptyNs"));
					}
				}
				if (ns == null)
				{
					ns = this.LookupNamespace(prefix);
					if (ns == null)
					{
						ns = string.Empty;
					}
				}
				if (this.elemTop == 0 && this.rawWriter != null)
				{
					this.rawWriter.OnRootElement(this.conformanceLevel);
				}
				this.writer.WriteStartElement(prefix, localName, ns);
				int num = this.elemTop + 1;
				this.elemTop = num;
				int num2 = num;
				if (num2 == this.elemScopeStack.Length)
				{
					XmlWellFormedWriter.ElementScope[] destinationArray = new XmlWellFormedWriter.ElementScope[num2 * 2];
					Array.Copy(this.elemScopeStack, destinationArray, num2);
					this.elemScopeStack = destinationArray;
				}
				this.elemScopeStack[num2].Set(prefix, localName, ns, this.nsTop);
				this.PushNamespaceImplicit(prefix, ns);
				if (this.attrCount >= 14)
				{
					this.attrHashTable.Clear();
				}
				this.attrCount = 0;
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003C418 File Offset: 0x0003A618
		public override void WriteEndElement()
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.EndElement);
				int num = this.elemTop;
				if (num == 0)
				{
					throw new XmlException("Xml_NoStartTag", string.Empty);
				}
				if (this.rawWriter != null)
				{
					this.elemScopeStack[num].WriteEndElement(this.rawWriter);
				}
				else
				{
					this.writer.WriteEndElement();
				}
				int prevNSTop = this.elemScopeStack[num].prevNSTop;
				if (this.useNsHashtable && prevNSTop < this.nsTop)
				{
					this.PopNamespaces(prevNSTop + 1, this.nsTop);
				}
				this.nsTop = prevNSTop;
				if ((this.elemTop = num - 1) == 0)
				{
					if (this.conformanceLevel == ConformanceLevel.Document)
					{
						this.currentState = XmlWellFormedWriter.State.AfterRootEle;
					}
					else
					{
						this.currentState = XmlWellFormedWriter.State.TopLevel;
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0003C4F0 File Offset: 0x0003A6F0
		public override void WriteFullEndElement()
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.EndElement);
				int num = this.elemTop;
				if (num == 0)
				{
					throw new XmlException("Xml_NoStartTag", string.Empty);
				}
				if (this.rawWriter != null)
				{
					this.elemScopeStack[num].WriteFullEndElement(this.rawWriter);
				}
				else
				{
					this.writer.WriteFullEndElement();
				}
				int prevNSTop = this.elemScopeStack[num].prevNSTop;
				if (this.useNsHashtable && prevNSTop < this.nsTop)
				{
					this.PopNamespaces(prevNSTop + 1, this.nsTop);
				}
				this.nsTop = prevNSTop;
				if ((this.elemTop = num - 1) == 0)
				{
					if (this.conformanceLevel == ConformanceLevel.Document)
					{
						this.currentState = XmlWellFormedWriter.State.AfterRootEle;
					}
					else
					{
						this.currentState = XmlWellFormedWriter.State.TopLevel;
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0003C5C8 File Offset: 0x0003A7C8
		public override void WriteStartAttribute(string prefix, string localName, string namespaceName)
		{
			try
			{
				if (localName == null || localName.Length == 0)
				{
					if (!(prefix == "xmlns"))
					{
						throw new ArgumentException(Res.GetString("Xml_EmptyLocalName"));
					}
					localName = "xmlns";
					prefix = string.Empty;
				}
				this.CheckNCName(localName);
				this.AdvanceState(XmlWellFormedWriter.Token.StartAttribute);
				if (prefix == null)
				{
					if (namespaceName != null && (!(localName == "xmlns") || !(namespaceName == "http://www.w3.org/2000/xmlns/")))
					{
						prefix = this.LookupPrefix(namespaceName);
					}
					if (prefix == null)
					{
						prefix = string.Empty;
					}
				}
				if (namespaceName == null)
				{
					if (prefix != null && prefix.Length > 0)
					{
						namespaceName = this.LookupNamespace(prefix);
					}
					if (namespaceName == null)
					{
						namespaceName = string.Empty;
					}
				}
				if (prefix.Length == 0)
				{
					if (localName[0] == 'x' && localName == "xmlns")
					{
						if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
						{
							throw new ArgumentException(Res.GetString("Xml_XmlnsPrefix"));
						}
						this.curDeclPrefix = string.Empty;
						this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.DefaultXmlns);
						goto IL_224;
					}
					else if (namespaceName.Length > 0)
					{
						prefix = this.LookupPrefix(namespaceName);
						if (prefix == null || prefix.Length == 0)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				else
				{
					if (prefix[0] == 'x')
					{
						if (prefix == "xmlns")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
							{
								throw new ArgumentException(Res.GetString("Xml_XmlnsPrefix"));
							}
							this.curDeclPrefix = localName;
							this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.PrefixedXmlns);
							goto IL_224;
						}
						else if (prefix == "xml")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/XML/1998/namespace")
							{
								throw new ArgumentException(Res.GetString("Xml_XmlPrefix"));
							}
							if (localName == "space")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlSpace);
								goto IL_224;
							}
							if (localName == "lang")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlLang);
								goto IL_224;
							}
						}
					}
					this.CheckNCName(prefix);
					if (namespaceName.Length == 0)
					{
						prefix = string.Empty;
					}
					else
					{
						string text = this.LookupLocalNamespace(prefix);
						if (text != null && text != namespaceName)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				if (prefix.Length != 0)
				{
					this.PushNamespaceImplicit(prefix, namespaceName);
				}
				IL_224:
				this.AddAttribute(prefix, localName, namespaceName);
				if (this.specAttr == XmlWellFormedWriter.SpecialAttribute.No)
				{
					this.writer.WriteStartAttribute(prefix, localName, namespaceName);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0003C844 File Offset: 0x0003AA44
		public override void WriteEndAttribute()
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.EndAttribute);
				if (this.specAttr != XmlWellFormedWriter.SpecialAttribute.No)
				{
					switch (this.specAttr)
					{
					case XmlWellFormedWriter.SpecialAttribute.DefaultXmlns:
					{
						string stringValue = this.attrValueCache.StringValue;
						if (this.PushNamespaceExplicit(string.Empty, stringValue))
						{
							if (this.rawWriter != null)
							{
								if (this.rawWriter.SupportsNamespaceDeclarationInChunks)
								{
									this.rawWriter.WriteStartNamespaceDeclaration(string.Empty);
									this.attrValueCache.Replay(this.rawWriter);
									this.rawWriter.WriteEndNamespaceDeclaration();
								}
								else
								{
									this.rawWriter.WriteNamespaceDeclaration(string.Empty, stringValue);
								}
							}
							else
							{
								this.writer.WriteStartAttribute(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/");
								this.attrValueCache.Replay(this.writer);
								this.writer.WriteEndAttribute();
							}
						}
						this.curDeclPrefix = null;
						break;
					}
					case XmlWellFormedWriter.SpecialAttribute.PrefixedXmlns:
					{
						string stringValue = this.attrValueCache.StringValue;
						if (stringValue.Length == 0)
						{
							throw new ArgumentException(Res.GetString("Xml_PrefixForEmptyNs"));
						}
						if (stringValue == "http://www.w3.org/2000/xmlns/" || (stringValue == "http://www.w3.org/XML/1998/namespace" && this.curDeclPrefix != "xml"))
						{
							throw new ArgumentException(Res.GetString("Xml_CanNotBindToReservedNamespace"));
						}
						if (this.PushNamespaceExplicit(this.curDeclPrefix, stringValue))
						{
							if (this.rawWriter != null)
							{
								if (this.rawWriter.SupportsNamespaceDeclarationInChunks)
								{
									this.rawWriter.WriteStartNamespaceDeclaration(this.curDeclPrefix);
									this.attrValueCache.Replay(this.rawWriter);
									this.rawWriter.WriteEndNamespaceDeclaration();
								}
								else
								{
									this.rawWriter.WriteNamespaceDeclaration(this.curDeclPrefix, stringValue);
								}
							}
							else
							{
								this.writer.WriteStartAttribute("xmlns", this.curDeclPrefix, "http://www.w3.org/2000/xmlns/");
								this.attrValueCache.Replay(this.writer);
								this.writer.WriteEndAttribute();
							}
						}
						this.curDeclPrefix = null;
						break;
					}
					case XmlWellFormedWriter.SpecialAttribute.XmlSpace:
					{
						this.attrValueCache.Trim();
						string stringValue = this.attrValueCache.StringValue;
						if (stringValue == "default")
						{
							this.elemScopeStack[this.elemTop].xmlSpace = XmlSpace.Default;
						}
						else
						{
							if (!(stringValue == "preserve"))
							{
								throw new ArgumentException(Res.GetString("Xml_InvalidXmlSpace", new object[]
								{
									stringValue
								}));
							}
							this.elemScopeStack[this.elemTop].xmlSpace = XmlSpace.Preserve;
						}
						this.writer.WriteStartAttribute("xml", "space", "http://www.w3.org/XML/1998/namespace");
						this.attrValueCache.Replay(this.writer);
						this.writer.WriteEndAttribute();
						break;
					}
					case XmlWellFormedWriter.SpecialAttribute.XmlLang:
					{
						string stringValue = this.attrValueCache.StringValue;
						this.elemScopeStack[this.elemTop].xmlLang = stringValue;
						this.writer.WriteStartAttribute("xml", "lang", "http://www.w3.org/XML/1998/namespace");
						this.attrValueCache.Replay(this.writer);
						this.writer.WriteEndAttribute();
						break;
					}
					}
					this.specAttr = XmlWellFormedWriter.SpecialAttribute.No;
					this.attrValueCache.Clear();
				}
				else
				{
					this.writer.WriteEndAttribute();
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0003CBA0 File Offset: 0x0003ADA0
		public override void WriteCData(string text)
		{
			try
			{
				if (text == null)
				{
					text = string.Empty;
				}
				this.AdvanceState(XmlWellFormedWriter.Token.CData);
				this.writer.WriteCData(text);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003CBE8 File Offset: 0x0003ADE8
		public override void WriteComment(string text)
		{
			try
			{
				if (text == null)
				{
					text = string.Empty;
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Comment);
				this.writer.WriteComment(text);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0003CC30 File Offset: 0x0003AE30
		public override void WriteProcessingInstruction(string name, string text)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Xml_EmptyName"));
				}
				this.CheckNCName(name);
				if (text == null)
				{
					text = string.Empty;
				}
				if (name.Length == 3 && string.Compare(name, "xml", StringComparison.OrdinalIgnoreCase) == 0)
				{
					if (this.currentState != XmlWellFormedWriter.State.Start)
					{
						throw new ArgumentException(Res.GetString((this.conformanceLevel == ConformanceLevel.Document) ? "Xml_DupXmlDecl" : "Xml_CannotWriteXmlDecl"));
					}
					this.xmlDeclFollows = true;
					this.AdvanceState(XmlWellFormedWriter.Token.PI);
					if (this.rawWriter != null)
					{
						this.rawWriter.WriteXmlDeclaration(text);
					}
					else
					{
						this.writer.WriteProcessingInstruction(name, text);
					}
				}
				else
				{
					this.AdvanceState(XmlWellFormedWriter.Token.PI);
					this.writer.WriteProcessingInstruction(name, text);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003CD0C File Offset: 0x0003AF0C
		public override void WriteEntityRef(string name)
		{
			try
			{
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Xml_EmptyName"));
				}
				this.CheckNCName(name);
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteEntityRef(name);
				}
				else
				{
					this.writer.WriteEntityRef(name);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003CD84 File Offset: 0x0003AF84
		public override void WriteCharEntity(char ch)
		{
			try
			{
				if (char.IsSurrogate(ch))
				{
					throw new ArgumentException(Res.GetString("Xml_InvalidSurrogateMissingLowChar"));
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteCharEntity(ch);
				}
				else
				{
					this.writer.WriteCharEntity(ch);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0003CDF0 File Offset: 0x0003AFF0
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			try
			{
				if (!char.IsSurrogatePair(highChar, lowChar))
				{
					throw XmlConvert.CreateInvalidSurrogatePairException(lowChar, highChar);
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteSurrogateCharEntity(lowChar, highChar);
				}
				else
				{
					this.writer.WriteSurrogateCharEntity(lowChar, highChar);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003CE58 File Offset: 0x0003B058
		public override void WriteWhitespace(string ws)
		{
			try
			{
				if (ws == null)
				{
					ws = string.Empty;
				}
				if (!XmlCharType.Instance.IsOnlyWhitespace(ws))
				{
					throw new ArgumentException(Res.GetString("Xml_NonWhitespace"));
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Whitespace);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteWhitespace(ws);
				}
				else
				{
					this.writer.WriteWhitespace(ws);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0003CED8 File Offset: 0x0003B0D8
		public override void WriteString(string text)
		{
			try
			{
				if (text != null)
				{
					this.AdvanceState(XmlWellFormedWriter.Token.Text);
					if (this.SaveAttrValue)
					{
						this.attrValueCache.WriteString(text);
					}
					else
					{
						this.writer.WriteString(text);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0003CF34 File Offset: 0x0003B134
		public override void WriteChars(char[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteChars(buffer, index, count);
				}
				else
				{
					this.writer.WriteChars(buffer, index, count);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0003CFCC File Offset: 0x0003B1CC
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				this.AdvanceState(XmlWellFormedWriter.Token.RawData);
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteRaw(buffer, index, count);
				}
				else
				{
					this.writer.WriteRaw(buffer, index, count);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0003D064 File Offset: 0x0003B264
		public override void WriteRaw(string data)
		{
			try
			{
				if (data != null)
				{
					this.AdvanceState(XmlWellFormedWriter.Token.RawData);
					if (this.SaveAttrValue)
					{
						this.attrValueCache.WriteRaw(data);
					}
					else
					{
						this.writer.WriteRaw(data);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003D0C0 File Offset: 0x0003B2C0
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				this.AdvanceState(XmlWellFormedWriter.Token.Base64);
				this.writer.WriteBase64(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003D140 File Offset: 0x0003B340
		public override void Close()
		{
			if (this.currentState != XmlWellFormedWriter.State.Closed)
			{
				try
				{
					if (this.writeEndDocumentOnClose)
					{
						while (this.currentState != XmlWellFormedWriter.State.Error)
						{
							if (this.elemTop <= 0)
							{
								break;
							}
							this.WriteEndElement();
						}
					}
					else if (this.currentState != XmlWellFormedWriter.State.Error && this.elemTop > 0)
					{
						try
						{
							this.AdvanceState(XmlWellFormedWriter.Token.EndElement);
						}
						catch
						{
							this.currentState = XmlWellFormedWriter.State.Error;
							throw;
						}
					}
					if (this.InBase64 && this.rawWriter != null)
					{
						this.rawWriter.WriteEndBase64();
					}
					this.writer.Flush();
				}
				finally
				{
					try
					{
						if (this.rawWriter != null)
						{
							this.rawWriter.Close(this.WriteState);
						}
						else
						{
							this.writer.Close();
						}
					}
					finally
					{
						this.currentState = XmlWellFormedWriter.State.Closed;
					}
				}
			}
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0003D22C File Offset: 0x0003B42C
		public override void Flush()
		{
			try
			{
				this.writer.Flush();
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0003D264 File Offset: 0x0003B464
		public override string LookupPrefix(string ns)
		{
			string result;
			try
			{
				if (ns == null)
				{
					throw new ArgumentNullException("ns");
				}
				for (int i = this.nsTop; i >= 0; i--)
				{
					if (this.nsStack[i].namespaceUri == ns)
					{
						string prefix = this.nsStack[i].prefix;
						for (i++; i <= this.nsTop; i++)
						{
							if (this.nsStack[i].prefix == prefix)
							{
								return null;
							}
						}
						return prefix;
					}
				}
				result = ((this.predefinedNamespaces != null) ? this.predefinedNamespaces.LookupPrefix(ns) : null);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0003D328 File Offset: 0x0003B528
		public override XmlSpace XmlSpace
		{
			get
			{
				int num = this.elemTop;
				while (num >= 0 && this.elemScopeStack[num].xmlSpace == (XmlSpace)(-1))
				{
					num--;
				}
				return this.elemScopeStack[num].xmlSpace;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0003D36C File Offset: 0x0003B56C
		public override string XmlLang
		{
			get
			{
				int num = this.elemTop;
				while (num > 0 && this.elemScopeStack[num].xmlLang == null)
				{
					num--;
				}
				return this.elemScopeStack[num].xmlLang;
			}
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0003D3B0 File Offset: 0x0003B5B0
		public override void WriteQualifiedName(string localName, string ns)
		{
			try
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Xml_EmptyLocalName"));
				}
				this.CheckNCName(localName);
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				string text = string.Empty;
				if (ns != null && ns.Length != 0)
				{
					text = this.LookupPrefix(ns);
					if (text == null)
					{
						if (this.currentState != XmlWellFormedWriter.State.Attribute)
						{
							throw new ArgumentException(Res.GetString("Xml_UndefNamespace", new object[]
							{
								ns
							}));
						}
						text = this.GeneratePrefix();
						this.PushNamespaceImplicit(text, ns);
					}
				}
				if (this.SaveAttrValue || this.rawWriter == null)
				{
					if (text.Length != 0)
					{
						this.WriteString(text);
						this.WriteString(":");
					}
					this.WriteString(localName);
				}
				else
				{
					this.rawWriter.WriteQualifiedName(text, localName, ns);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0003D494 File Offset: 0x0003B694
		public override void WriteValue(bool value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0003D4D4 File Offset: 0x0003B6D4
		public override void WriteValue(DateTime value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0003D514 File Offset: 0x0003B714
		public override void WriteValue(DateTimeOffset value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0003D554 File Offset: 0x0003B754
		public override void WriteValue(double value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0003D594 File Offset: 0x0003B794
		public override void WriteValue(float value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0003D5D4 File Offset: 0x0003B7D4
		public override void WriteValue(decimal value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0003D614 File Offset: 0x0003B814
		public override void WriteValue(int value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0003D654 File Offset: 0x0003B854
		public override void WriteValue(long value)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
				this.writer.WriteValue(value);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0003D694 File Offset: 0x0003B894
		public override void WriteValue(string value)
		{
			try
			{
				if (value != null)
				{
					if (this.SaveAttrValue)
					{
						this.AdvanceState(XmlWellFormedWriter.Token.Text);
						this.attrValueCache.WriteValue(value);
					}
					else
					{
						this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
						this.writer.WriteValue(value);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0003D6F8 File Offset: 0x0003B8F8
		public override void WriteValue(object value)
		{
			try
			{
				if (this.SaveAttrValue && value is string)
				{
					this.AdvanceState(XmlWellFormedWriter.Token.Text);
					this.attrValueCache.WriteValue((string)value);
				}
				else
				{
					this.AdvanceState(XmlWellFormedWriter.Token.AtomicValue);
					this.writer.WriteValue(value);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0003D764 File Offset: 0x0003B964
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			if (this.IsClosedOrErrorState)
			{
				throw new InvalidOperationException(Res.GetString("Xml_ClosedOrError"));
			}
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.Text);
				base.WriteBinHex(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0003D7B8 File Offset: 0x0003B9B8
		internal XmlWriter InnerWriter
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x0003D7C0 File Offset: 0x0003B9C0
		internal XmlRawWriter RawWriter
		{
			get
			{
				return this.rawWriter;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0003D7C8 File Offset: 0x0003B9C8
		private bool SaveAttrValue
		{
			get
			{
				return this.specAttr > XmlWellFormedWriter.SpecialAttribute.No;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0003D7D3 File Offset: 0x0003B9D3
		private bool InBase64
		{
			get
			{
				return this.currentState == XmlWellFormedWriter.State.B64Content || this.currentState == XmlWellFormedWriter.State.B64Attribute || this.currentState == XmlWellFormedWriter.State.RootLevelB64Attr;
			}
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0003D7F4 File Offset: 0x0003B9F4
		private void SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute special)
		{
			this.specAttr = special;
			if (XmlWellFormedWriter.State.Attribute == this.currentState)
			{
				this.currentState = XmlWellFormedWriter.State.SpecialAttr;
			}
			else if (XmlWellFormedWriter.State.RootLevelAttr == this.currentState)
			{
				this.currentState = XmlWellFormedWriter.State.RootLevelSpecAttr;
			}
			if (this.attrValueCache == null)
			{
				this.attrValueCache = new XmlWellFormedWriter.AttributeValueCache();
			}
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0003D840 File Offset: 0x0003BA40
		private void WriteStartDocumentImpl(XmlStandalone standalone)
		{
			try
			{
				this.AdvanceState(XmlWellFormedWriter.Token.StartDocument);
				if (this.conformanceLevel == ConformanceLevel.Auto)
				{
					this.conformanceLevel = ConformanceLevel.Document;
					this.stateTable = XmlWellFormedWriter.StateTableDocument;
				}
				else if (this.conformanceLevel == ConformanceLevel.Fragment)
				{
					throw new InvalidOperationException(Res.GetString("Xml_CannotStartDocumentOnFragment"));
				}
				if (this.rawWriter != null)
				{
					if (!this.xmlDeclFollows)
					{
						this.rawWriter.WriteXmlDeclaration(standalone);
					}
				}
				else
				{
					this.writer.WriteStartDocument();
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0003D8D0 File Offset: 0x0003BAD0
		private void StartFragment()
		{
			this.conformanceLevel = ConformanceLevel.Fragment;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0003D8DC File Offset: 0x0003BADC
		private void PushNamespaceImplicit(string prefix, string ns)
		{
			int num = this.LookupNamespaceIndex(prefix);
			XmlWellFormedWriter.NamespaceKind kind;
			if (num != -1)
			{
				if (num > this.elemScopeStack[this.elemTop].prevNSTop)
				{
					if (this.nsStack[num].namespaceUri != ns)
					{
						throw new XmlException("Xml_RedefinePrefix", new string[]
						{
							prefix,
							this.nsStack[num].namespaceUri,
							ns
						});
					}
					return;
				}
				else if (this.nsStack[num].kind == XmlWellFormedWriter.NamespaceKind.Special)
				{
					if (!(prefix == "xml"))
					{
						throw new ArgumentException(Res.GetString("Xml_XmlnsPrefix"));
					}
					if (ns != this.nsStack[num].namespaceUri)
					{
						throw new ArgumentException(Res.GetString("Xml_XmlPrefix"));
					}
					kind = XmlWellFormedWriter.NamespaceKind.Implied;
				}
				else
				{
					kind = ((this.nsStack[num].namespaceUri == ns) ? XmlWellFormedWriter.NamespaceKind.Implied : XmlWellFormedWriter.NamespaceKind.NeedToWrite);
				}
			}
			else
			{
				if ((ns == "http://www.w3.org/XML/1998/namespace" && prefix != "xml") || (ns == "http://www.w3.org/2000/xmlns/" && prefix != "xmlns"))
				{
					throw new ArgumentException(Res.GetString("Xml_NamespaceDeclXmlXmlns", new object[]
					{
						prefix
					}));
				}
				if (this.predefinedNamespaces != null)
				{
					string a = this.predefinedNamespaces.LookupNamespace(prefix);
					kind = ((a == ns) ? XmlWellFormedWriter.NamespaceKind.Implied : XmlWellFormedWriter.NamespaceKind.NeedToWrite);
				}
				else
				{
					kind = XmlWellFormedWriter.NamespaceKind.NeedToWrite;
				}
			}
			this.AddNamespace(prefix, ns, kind);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0003DA58 File Offset: 0x0003BC58
		private bool PushNamespaceExplicit(string prefix, string ns)
		{
			bool result = true;
			int num = this.LookupNamespaceIndex(prefix);
			if (num != -1)
			{
				if (num > this.elemScopeStack[this.elemTop].prevNSTop)
				{
					if (this.nsStack[num].namespaceUri != ns)
					{
						throw new XmlException("Xml_RedefinePrefix", new string[]
						{
							prefix,
							this.nsStack[num].namespaceUri,
							ns
						});
					}
					XmlWellFormedWriter.NamespaceKind kind = this.nsStack[num].kind;
					if (kind == XmlWellFormedWriter.NamespaceKind.Written)
					{
						throw XmlWellFormedWriter.DupAttrException((prefix.Length == 0) ? string.Empty : "xmlns", (prefix.Length == 0) ? "xmlns" : prefix);
					}
					if (this.omitDuplNamespaces && kind != XmlWellFormedWriter.NamespaceKind.NeedToWrite)
					{
						result = false;
					}
					this.nsStack[num].kind = XmlWellFormedWriter.NamespaceKind.Written;
					return result;
				}
				else if (this.nsStack[num].namespaceUri == ns && this.omitDuplNamespaces)
				{
					result = false;
				}
			}
			else if (this.predefinedNamespaces != null)
			{
				string a = this.predefinedNamespaces.LookupNamespace(prefix);
				if (a == ns && this.omitDuplNamespaces)
				{
					result = false;
				}
			}
			if ((ns == "http://www.w3.org/XML/1998/namespace" && prefix != "xml") || (ns == "http://www.w3.org/2000/xmlns/" && prefix != "xmlns"))
			{
				throw new ArgumentException(Res.GetString("Xml_NamespaceDeclXmlXmlns", new object[]
				{
					prefix
				}));
			}
			if (prefix.Length > 0 && prefix[0] == 'x')
			{
				if (prefix == "xml")
				{
					if (ns != "http://www.w3.org/XML/1998/namespace")
					{
						throw new ArgumentException(Res.GetString("Xml_XmlPrefix"));
					}
				}
				else if (prefix == "xmlns")
				{
					throw new ArgumentException(Res.GetString("Xml_XmlnsPrefix"));
				}
			}
			this.AddNamespace(prefix, ns, XmlWellFormedWriter.NamespaceKind.Written);
			return result;
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0003DC3C File Offset: 0x0003BE3C
		private void AddNamespace(string prefix, string ns, XmlWellFormedWriter.NamespaceKind kind)
		{
			int num = this.nsTop + 1;
			this.nsTop = num;
			int num2 = num;
			if (num2 == this.nsStack.Length)
			{
				XmlWellFormedWriter.Namespace[] destinationArray = new XmlWellFormedWriter.Namespace[num2 * 2];
				Array.Copy(this.nsStack, destinationArray, num2);
				this.nsStack = destinationArray;
			}
			this.nsStack[num2].Set(prefix, ns, kind);
			if (this.useNsHashtable)
			{
				this.AddToNamespaceHashtable(this.nsTop);
				return;
			}
			if (this.nsTop == 16)
			{
				this.nsHashtable = new Dictionary<string, int>(this.hasher);
				for (int i = 0; i <= this.nsTop; i++)
				{
					this.AddToNamespaceHashtable(i);
				}
				this.useNsHashtable = true;
			}
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x0003DCE8 File Offset: 0x0003BEE8
		private void AddToNamespaceHashtable(int namespaceIndex)
		{
			string prefix = this.nsStack[namespaceIndex].prefix;
			int prevNsIndex;
			if (this.nsHashtable.TryGetValue(prefix, out prevNsIndex))
			{
				this.nsStack[namespaceIndex].prevNsIndex = prevNsIndex;
			}
			this.nsHashtable[prefix] = namespaceIndex;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0003DD38 File Offset: 0x0003BF38
		private int LookupNamespaceIndex(string prefix)
		{
			if (this.useNsHashtable)
			{
				int result;
				if (this.nsHashtable.TryGetValue(prefix, out result))
				{
					return result;
				}
			}
			else
			{
				for (int i = this.nsTop; i >= 0; i--)
				{
					if (this.nsStack[i].prefix == prefix)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0003DD8C File Offset: 0x0003BF8C
		private void PopNamespaces(int indexFrom, int indexTo)
		{
			for (int i = indexTo; i >= indexFrom; i--)
			{
				if (this.nsStack[i].prevNsIndex == -1)
				{
					this.nsHashtable.Remove(this.nsStack[i].prefix);
				}
				else
				{
					this.nsHashtable[this.nsStack[i].prefix] = this.nsStack[i].prevNsIndex;
				}
			}
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0003DE08 File Offset: 0x0003C008
		private static XmlException DupAttrException(string prefix, string localName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (prefix.Length > 0)
			{
				stringBuilder.Append(prefix);
				stringBuilder.Append(':');
			}
			stringBuilder.Append(localName);
			return new XmlException("Xml_DupAttributeName", stringBuilder.ToString());
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0003DE50 File Offset: 0x0003C050
		private void AdvanceState(XmlWellFormedWriter.Token token)
		{
			if (this.currentState < XmlWellFormedWriter.State.Closed)
			{
				XmlWellFormedWriter.State state;
				for (;;)
				{
					state = this.stateTable[(int)(((int)token << 4) + (int)this.currentState)];
					if (state < XmlWellFormedWriter.State.Error)
					{
						break;
					}
					if (state != XmlWellFormedWriter.State.Error)
					{
						switch (state)
						{
						case XmlWellFormedWriter.State.StartContent:
							goto IL_E3;
						case XmlWellFormedWriter.State.StartContentEle:
							goto IL_F0;
						case XmlWellFormedWriter.State.StartContentB64:
							goto IL_FD;
						case XmlWellFormedWriter.State.StartDoc:
							goto IL_10A;
						case XmlWellFormedWriter.State.StartDocEle:
							goto IL_117;
						case XmlWellFormedWriter.State.EndAttrSEle:
							goto IL_124;
						case XmlWellFormedWriter.State.EndAttrEEle:
							goto IL_137;
						case XmlWellFormedWriter.State.EndAttrSCont:
							goto IL_14A;
						case XmlWellFormedWriter.State.EndAttrSAttr:
							goto IL_15D;
						case XmlWellFormedWriter.State.PostB64Cont:
							if (this.rawWriter != null)
							{
								this.rawWriter.WriteEndBase64();
							}
							this.currentState = XmlWellFormedWriter.State.Content;
							continue;
						case XmlWellFormedWriter.State.PostB64Attr:
							if (this.rawWriter != null)
							{
								this.rawWriter.WriteEndBase64();
							}
							this.currentState = XmlWellFormedWriter.State.Attribute;
							continue;
						case XmlWellFormedWriter.State.PostB64RootAttr:
							if (this.rawWriter != null)
							{
								this.rawWriter.WriteEndBase64();
							}
							this.currentState = XmlWellFormedWriter.State.RootLevelAttr;
							continue;
						case XmlWellFormedWriter.State.StartFragEle:
							goto IL_1C8;
						case XmlWellFormedWriter.State.StartFragCont:
							goto IL_1D2;
						case XmlWellFormedWriter.State.StartFragB64:
							goto IL_1DC;
						case XmlWellFormedWriter.State.StartRootLevelAttr:
							goto IL_1E6;
						}
						break;
					}
					goto IL_D1;
				}
				goto IL_1EF;
				IL_D1:
				this.ThrowInvalidStateTransition(token, this.currentState);
				goto IL_1EF;
				IL_E3:
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Content;
				goto IL_1EF;
				IL_F0:
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Element;
				goto IL_1EF;
				IL_FD:
				this.StartElementContent();
				state = XmlWellFormedWriter.State.B64Content;
				goto IL_1EF;
				IL_10A:
				this.WriteStartDocument();
				state = XmlWellFormedWriter.State.Document;
				goto IL_1EF;
				IL_117:
				this.WriteStartDocument();
				state = XmlWellFormedWriter.State.Element;
				goto IL_1EF;
				IL_124:
				this.WriteEndAttribute();
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Element;
				goto IL_1EF;
				IL_137:
				this.WriteEndAttribute();
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Content;
				goto IL_1EF;
				IL_14A:
				this.WriteEndAttribute();
				this.StartElementContent();
				state = XmlWellFormedWriter.State.Content;
				goto IL_1EF;
				IL_15D:
				this.WriteEndAttribute();
				state = XmlWellFormedWriter.State.Attribute;
				goto IL_1EF;
				IL_1C8:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Element;
				goto IL_1EF;
				IL_1D2:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Content;
				goto IL_1EF;
				IL_1DC:
				this.StartFragment();
				state = XmlWellFormedWriter.State.B64Content;
				goto IL_1EF;
				IL_1E6:
				this.WriteEndAttribute();
				state = XmlWellFormedWriter.State.RootLevelAttr;
				IL_1EF:
				this.currentState = state;
				return;
			}
			if (this.currentState == XmlWellFormedWriter.State.Closed || this.currentState == XmlWellFormedWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("Xml_ClosedOrError"));
			}
			throw new InvalidOperationException(Res.GetString("Xml_WrongToken", new object[]
			{
				XmlWellFormedWriter.tokenName[(int)token],
				XmlWellFormedWriter.GetStateName(this.currentState)
			}));
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0003E054 File Offset: 0x0003C254
		private void StartElementContent()
		{
			int prevNSTop = this.elemScopeStack[this.elemTop].prevNSTop;
			for (int i = this.nsTop; i > prevNSTop; i--)
			{
				if (this.nsStack[i].kind == XmlWellFormedWriter.NamespaceKind.NeedToWrite)
				{
					this.nsStack[i].WriteDecl(this.writer, this.rawWriter);
				}
			}
			if (this.rawWriter != null)
			{
				this.rawWriter.StartElementContent();
			}
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0003E0CD File Offset: 0x0003C2CD
		private static string GetStateName(XmlWellFormedWriter.State state)
		{
			if (state >= XmlWellFormedWriter.State.Error)
			{
				return "Error";
			}
			return XmlWellFormedWriter.stateName[(int)state];
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0003E0E4 File Offset: 0x0003C2E4
		internal string LookupNamespace(string prefix)
		{
			for (int i = this.nsTop; i >= 0; i--)
			{
				if (this.nsStack[i].prefix == prefix)
				{
					return this.nsStack[i].namespaceUri;
				}
			}
			if (this.predefinedNamespaces == null)
			{
				return null;
			}
			return this.predefinedNamespaces.LookupNamespace(prefix);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0003E144 File Offset: 0x0003C344
		private string LookupLocalNamespace(string prefix)
		{
			for (int i = this.nsTop; i > this.elemScopeStack[this.elemTop].prevNSTop; i--)
			{
				if (this.nsStack[i].prefix == prefix)
				{
					return this.nsStack[i].namespaceUri;
				}
			}
			return null;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0003E1A4 File Offset: 0x0003C3A4
		private string GeneratePrefix()
		{
			string text = "p" + (this.nsTop - 2).ToString("d", CultureInfo.InvariantCulture);
			if (this.LookupNamespace(text) == null)
			{
				return text;
			}
			int num = 0;
			string text2;
			do
			{
				text2 = text + num.ToString(CultureInfo.InvariantCulture);
				num++;
			}
			while (this.LookupNamespace(text2) != null);
			return text2;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0003E204 File Offset: 0x0003C404
		private unsafe void CheckNCName(string ncname)
		{
			int length = ncname.Length;
			if ((this.xmlCharType.charProperties[ncname[0]] & 4) != 0)
			{
				for (int i = 1; i < length; i++)
				{
					if ((this.xmlCharType.charProperties[ncname[i]] & 8) == 0)
					{
						throw XmlWellFormedWriter.InvalidCharsException(ncname, i);
					}
				}
				return;
			}
			throw XmlWellFormedWriter.InvalidCharsException(ncname, 0);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0003E268 File Offset: 0x0003C468
		private static Exception InvalidCharsException(string name, int badCharIndex)
		{
			string[] array = XmlException.BuildCharExceptionArgs(name, badCharIndex);
			string[] array2 = new string[]
			{
				name,
				array[0],
				array[1]
			};
			string name2 = "Xml_InvalidNameCharsDetail";
			object[] args = array2;
			return new ArgumentException(Res.GetString(name2, args));
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0003E2A8 File Offset: 0x0003C4A8
		private void ThrowInvalidStateTransition(XmlWellFormedWriter.Token token, XmlWellFormedWriter.State currentState)
		{
			string @string = Res.GetString("Xml_WrongToken", new object[]
			{
				XmlWellFormedWriter.tokenName[(int)token],
				XmlWellFormedWriter.GetStateName(currentState)
			});
			if ((currentState == XmlWellFormedWriter.State.Start || currentState == XmlWellFormedWriter.State.AfterRootEle) && this.conformanceLevel == ConformanceLevel.Document)
			{
				throw new InvalidOperationException(@string + " " + Res.GetString("Xml_ConformanceLevelFragment"));
			}
			throw new InvalidOperationException(@string);
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0003E30A File Offset: 0x0003C50A
		private bool IsClosedOrErrorState
		{
			get
			{
				return this.currentState >= XmlWellFormedWriter.State.Closed;
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0003E31C File Offset: 0x0003C51C
		private void AddAttribute(string prefix, string localName, string namespaceName)
		{
			int num = this.attrCount;
			this.attrCount = num + 1;
			int num2 = num;
			if (num2 == this.attrStack.Length)
			{
				XmlWellFormedWriter.AttrName[] destinationArray = new XmlWellFormedWriter.AttrName[num2 * 2];
				Array.Copy(this.attrStack, destinationArray, num2);
				this.attrStack = destinationArray;
			}
			this.attrStack[num2].Set(prefix, localName, namespaceName);
			if (this.attrCount < 14)
			{
				for (int i = 0; i < num2; i++)
				{
					if (this.attrStack[i].IsDuplicate(prefix, localName, namespaceName))
					{
						throw XmlWellFormedWriter.DupAttrException(prefix, localName);
					}
				}
				return;
			}
			if (this.attrCount == 14)
			{
				if (this.attrHashTable == null)
				{
					this.attrHashTable = new Dictionary<string, int>(this.hasher);
				}
				for (int j = 0; j < num2; j++)
				{
					this.AddToAttrHashTable(j);
				}
			}
			this.AddToAttrHashTable(num2);
			for (int k = this.attrStack[num2].prev; k > 0; k = this.attrStack[k].prev)
			{
				k--;
				if (this.attrStack[k].IsDuplicate(prefix, localName, namespaceName))
				{
					throw XmlWellFormedWriter.DupAttrException(prefix, localName);
				}
			}
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0003E444 File Offset: 0x0003C644
		private void AddToAttrHashTable(int attributeIndex)
		{
			string localName = this.attrStack[attributeIndex].localName;
			int count = this.attrHashTable.Count;
			this.attrHashTable[localName] = 0;
			if (count != this.attrHashTable.Count)
			{
				return;
			}
			int num = attributeIndex - 1;
			while (num >= 0 && !(this.attrStack[num].localName == localName))
			{
				num--;
			}
			this.attrStack[attributeIndex].prev = num + 1;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0003E4C6 File Offset: 0x0003C6C6
		public override Task WriteStartDocumentAsync()
		{
			return this.WriteStartDocumentImplAsync(XmlStandalone.Omit);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0003E4CF File Offset: 0x0003C6CF
		public override Task WriteStartDocumentAsync(bool standalone)
		{
			return this.WriteStartDocumentImplAsync(standalone ? XmlStandalone.Yes : XmlStandalone.No);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0003E4E0 File Offset: 0x0003C6E0
		public override Task WriteEndDocumentAsync()
		{
			XmlWellFormedWriter.<WriteEndDocumentAsync>d__115 <WriteEndDocumentAsync>d__;
			<WriteEndDocumentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteEndDocumentAsync>d__.<>4__this = this;
			<WriteEndDocumentAsync>d__.<>1__state = -1;
			<WriteEndDocumentAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteEndDocumentAsync>d__115>(ref <WriteEndDocumentAsync>d__);
			return <WriteEndDocumentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0003E524 File Offset: 0x0003C724
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			XmlWellFormedWriter.<WriteDocTypeAsync>d__116 <WriteDocTypeAsync>d__;
			<WriteDocTypeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteDocTypeAsync>d__.<>4__this = this;
			<WriteDocTypeAsync>d__.name = name;
			<WriteDocTypeAsync>d__.pubid = pubid;
			<WriteDocTypeAsync>d__.sysid = sysid;
			<WriteDocTypeAsync>d__.subset = subset;
			<WriteDocTypeAsync>d__.<>1__state = -1;
			<WriteDocTypeAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteDocTypeAsync>d__116>(ref <WriteDocTypeAsync>d__);
			return <WriteDocTypeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0003E588 File Offset: 0x0003C788
		private Task TryReturnTask(Task task)
		{
			if (task.IsSuccess())
			{
				return AsyncHelper.DoneTask;
			}
			return this._TryReturnTask(task);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0003E5A0 File Offset: 0x0003C7A0
		private Task _TryReturnTask(Task task)
		{
			XmlWellFormedWriter.<_TryReturnTask>d__118 <_TryReturnTask>d__;
			<_TryReturnTask>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_TryReturnTask>d__.<>4__this = this;
			<_TryReturnTask>d__.task = task;
			<_TryReturnTask>d__.<>1__state = -1;
			<_TryReturnTask>d__.<>t__builder.Start<XmlWellFormedWriter.<_TryReturnTask>d__118>(ref <_TryReturnTask>d__);
			return <_TryReturnTask>d__.<>t__builder.Task;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0003E5EB File Offset: 0x0003C7EB
		private Task SequenceRun(Task task, Func<Task> nextTaskFun)
		{
			if (task.IsSuccess())
			{
				return this.TryReturnTask(nextTaskFun());
			}
			return this._SequenceRun(task, nextTaskFun);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0003E60C File Offset: 0x0003C80C
		private Task _SequenceRun(Task task, Func<Task> nextTaskFun)
		{
			XmlWellFormedWriter.<_SequenceRun>d__120 <_SequenceRun>d__;
			<_SequenceRun>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_SequenceRun>d__.<>4__this = this;
			<_SequenceRun>d__.task = task;
			<_SequenceRun>d__.nextTaskFun = nextTaskFun;
			<_SequenceRun>d__.<>1__state = -1;
			<_SequenceRun>d__.<>t__builder.Start<XmlWellFormedWriter.<_SequenceRun>d__120>(ref <_SequenceRun>d__);
			return <_SequenceRun>d__.<>t__builder.Task;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0003E660 File Offset: 0x0003C860
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			Task result;
			try
			{
				if (localName == null || localName.Length == 0)
				{
					throw new ArgumentException(Res.GetString("Xml_EmptyLocalName"));
				}
				this.CheckNCName(localName);
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.StartElement);
				if (task.IsSuccess())
				{
					result = this.WriteStartElementAsync_NoAdvanceState(prefix, localName, ns);
				}
				else
				{
					result = this.WriteStartElementAsync_NoAdvanceState(task, prefix, localName, ns);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0003E6D4 File Offset: 0x0003C8D4
		private Task WriteStartElementAsync_NoAdvanceState(string prefix, string localName, string ns)
		{
			Task result;
			try
			{
				if (prefix == null)
				{
					if (ns != null)
					{
						prefix = this.LookupPrefix(ns);
					}
					if (prefix == null)
					{
						prefix = string.Empty;
					}
				}
				else if (prefix.Length > 0)
				{
					this.CheckNCName(prefix);
					if (ns == null)
					{
						ns = this.LookupNamespace(prefix);
					}
					if (ns == null || (ns != null && ns.Length == 0))
					{
						throw new ArgumentException(Res.GetString("Xml_PrefixForEmptyNs"));
					}
				}
				if (ns == null)
				{
					ns = this.LookupNamespace(prefix);
					if (ns == null)
					{
						ns = string.Empty;
					}
				}
				if (this.elemTop == 0 && this.rawWriter != null)
				{
					this.rawWriter.OnRootElement(this.conformanceLevel);
				}
				Task task = this.writer.WriteStartElementAsync(prefix, localName, ns);
				if (task.IsSuccess())
				{
					this.WriteStartElementAsync_FinishWrite(prefix, localName, ns);
					result = AsyncHelper.DoneTask;
				}
				else
				{
					result = this.WriteStartElementAsync_FinishWrite(task, prefix, localName, ns);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0003E7C0 File Offset: 0x0003C9C0
		private Task WriteStartElementAsync_NoAdvanceState(Task task, string prefix, string localName, string ns)
		{
			XmlWellFormedWriter.<WriteStartElementAsync_NoAdvanceState>d__123 <WriteStartElementAsync_NoAdvanceState>d__;
			<WriteStartElementAsync_NoAdvanceState>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartElementAsync_NoAdvanceState>d__.<>4__this = this;
			<WriteStartElementAsync_NoAdvanceState>d__.task = task;
			<WriteStartElementAsync_NoAdvanceState>d__.prefix = prefix;
			<WriteStartElementAsync_NoAdvanceState>d__.localName = localName;
			<WriteStartElementAsync_NoAdvanceState>d__.ns = ns;
			<WriteStartElementAsync_NoAdvanceState>d__.<>1__state = -1;
			<WriteStartElementAsync_NoAdvanceState>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteStartElementAsync_NoAdvanceState>d__123>(ref <WriteStartElementAsync_NoAdvanceState>d__);
			return <WriteStartElementAsync_NoAdvanceState>d__.<>t__builder.Task;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0003E824 File Offset: 0x0003CA24
		private void WriteStartElementAsync_FinishWrite(string prefix, string localName, string ns)
		{
			try
			{
				int num = this.elemTop + 1;
				this.elemTop = num;
				int num2 = num;
				if (num2 == this.elemScopeStack.Length)
				{
					XmlWellFormedWriter.ElementScope[] destinationArray = new XmlWellFormedWriter.ElementScope[num2 * 2];
					Array.Copy(this.elemScopeStack, destinationArray, num2);
					this.elemScopeStack = destinationArray;
				}
				this.elemScopeStack[num2].Set(prefix, localName, ns, this.nsTop);
				this.PushNamespaceImplicit(prefix, ns);
				if (this.attrCount >= 14)
				{
					this.attrHashTable.Clear();
				}
				this.attrCount = 0;
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0003E8C8 File Offset: 0x0003CAC8
		private Task WriteStartElementAsync_FinishWrite(Task t, string prefix, string localName, string ns)
		{
			XmlWellFormedWriter.<WriteStartElementAsync_FinishWrite>d__125 <WriteStartElementAsync_FinishWrite>d__;
			<WriteStartElementAsync_FinishWrite>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartElementAsync_FinishWrite>d__.<>4__this = this;
			<WriteStartElementAsync_FinishWrite>d__.t = t;
			<WriteStartElementAsync_FinishWrite>d__.prefix = prefix;
			<WriteStartElementAsync_FinishWrite>d__.localName = localName;
			<WriteStartElementAsync_FinishWrite>d__.ns = ns;
			<WriteStartElementAsync_FinishWrite>d__.<>1__state = -1;
			<WriteStartElementAsync_FinishWrite>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteStartElementAsync_FinishWrite>d__125>(ref <WriteStartElementAsync_FinishWrite>d__);
			return <WriteStartElementAsync_FinishWrite>d__.<>t__builder.Task;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003E92C File Offset: 0x0003CB2C
		public override Task WriteEndElementAsync()
		{
			Task result;
			try
			{
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.EndElement);
				result = this.SequenceRun(task, new Func<Task>(this.WriteEndElementAsync_NoAdvanceState));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003E974 File Offset: 0x0003CB74
		private Task WriteEndElementAsync_NoAdvanceState()
		{
			Task result;
			try
			{
				int num = this.elemTop;
				if (num == 0)
				{
					throw new XmlException("Xml_NoStartTag", string.Empty);
				}
				Task task;
				if (this.rawWriter != null)
				{
					task = this.elemScopeStack[num].WriteEndElementAsync(this.rawWriter);
				}
				else
				{
					task = this.writer.WriteEndElementAsync();
				}
				result = this.SequenceRun(task, new Func<Task>(this.WriteEndElementAsync_FinishWrite));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0003E9FC File Offset: 0x0003CBFC
		private Task WriteEndElementAsync_FinishWrite()
		{
			try
			{
				int num = this.elemTop;
				int prevNSTop = this.elemScopeStack[num].prevNSTop;
				if (this.useNsHashtable && prevNSTop < this.nsTop)
				{
					this.PopNamespaces(prevNSTop + 1, this.nsTop);
				}
				this.nsTop = prevNSTop;
				if ((this.elemTop = num - 1) == 0)
				{
					if (this.conformanceLevel == ConformanceLevel.Document)
					{
						this.currentState = XmlWellFormedWriter.State.AfterRootEle;
					}
					else
					{
						this.currentState = XmlWellFormedWriter.State.TopLevel;
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0003EA94 File Offset: 0x0003CC94
		public override Task WriteFullEndElementAsync()
		{
			Task result;
			try
			{
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.EndElement);
				result = this.SequenceRun(task, new Func<Task>(this.WriteFullEndElementAsync_NoAdvanceState));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0003EADC File Offset: 0x0003CCDC
		private Task WriteFullEndElementAsync_NoAdvanceState()
		{
			Task result;
			try
			{
				int num = this.elemTop;
				if (num == 0)
				{
					throw new XmlException("Xml_NoStartTag", string.Empty);
				}
				Task task;
				if (this.rawWriter != null)
				{
					task = this.elemScopeStack[num].WriteFullEndElementAsync(this.rawWriter);
				}
				else
				{
					task = this.writer.WriteFullEndElementAsync();
				}
				result = this.SequenceRun(task, new Func<Task>(this.WriteEndElementAsync_FinishWrite));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0003EB64 File Offset: 0x0003CD64
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string namespaceName)
		{
			Task result;
			try
			{
				if (localName == null || localName.Length == 0)
				{
					if (!(prefix == "xmlns"))
					{
						throw new ArgumentException(Res.GetString("Xml_EmptyLocalName"));
					}
					localName = "xmlns";
					prefix = string.Empty;
				}
				this.CheckNCName(localName);
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.StartAttribute);
				if (task.IsSuccess())
				{
					result = this.WriteStartAttributeAsync_NoAdvanceState(prefix, localName, namespaceName);
				}
				else
				{
					result = this.WriteStartAttributeAsync_NoAdvanceState(task, prefix, localName, namespaceName);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0003EBF8 File Offset: 0x0003CDF8
		private Task WriteStartAttributeAsync_NoAdvanceState(string prefix, string localName, string namespaceName)
		{
			Task result;
			try
			{
				if (prefix == null)
				{
					if (namespaceName != null && (!(localName == "xmlns") || !(namespaceName == "http://www.w3.org/2000/xmlns/")))
					{
						prefix = this.LookupPrefix(namespaceName);
					}
					if (prefix == null)
					{
						prefix = string.Empty;
					}
				}
				if (namespaceName == null)
				{
					if (prefix != null && prefix.Length > 0)
					{
						namespaceName = this.LookupNamespace(prefix);
					}
					if (namespaceName == null)
					{
						namespaceName = string.Empty;
					}
				}
				if (prefix.Length == 0)
				{
					if (localName[0] == 'x' && localName == "xmlns")
					{
						if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
						{
							throw new ArgumentException(Res.GetString("Xml_XmlnsPrefix"));
						}
						this.curDeclPrefix = string.Empty;
						this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.DefaultXmlns);
						goto IL_1DE;
					}
					else if (namespaceName.Length > 0)
					{
						prefix = this.LookupPrefix(namespaceName);
						if (prefix == null || prefix.Length == 0)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				else
				{
					if (prefix[0] == 'x')
					{
						if (prefix == "xmlns")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/2000/xmlns/")
							{
								throw new ArgumentException(Res.GetString("Xml_XmlnsPrefix"));
							}
							this.curDeclPrefix = localName;
							this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.PrefixedXmlns);
							goto IL_1DE;
						}
						else if (prefix == "xml")
						{
							if (namespaceName.Length > 0 && namespaceName != "http://www.w3.org/XML/1998/namespace")
							{
								throw new ArgumentException(Res.GetString("Xml_XmlPrefix"));
							}
							if (localName == "space")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlSpace);
								goto IL_1DE;
							}
							if (localName == "lang")
							{
								this.SetSpecialAttribute(XmlWellFormedWriter.SpecialAttribute.XmlLang);
								goto IL_1DE;
							}
						}
					}
					this.CheckNCName(prefix);
					if (namespaceName.Length == 0)
					{
						prefix = string.Empty;
					}
					else
					{
						string text = this.LookupLocalNamespace(prefix);
						if (text != null && text != namespaceName)
						{
							prefix = this.GeneratePrefix();
						}
					}
				}
				if (prefix.Length != 0)
				{
					this.PushNamespaceImplicit(prefix, namespaceName);
				}
				IL_1DE:
				this.AddAttribute(prefix, localName, namespaceName);
				if (this.specAttr == XmlWellFormedWriter.SpecialAttribute.No)
				{
					result = this.TryReturnTask(this.writer.WriteStartAttributeAsync(prefix, localName, namespaceName));
				}
				else
				{
					result = AsyncHelper.DoneTask;
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0003EE3C File Offset: 0x0003D03C
		private Task WriteStartAttributeAsync_NoAdvanceState(Task task, string prefix, string localName, string namespaceName)
		{
			XmlWellFormedWriter.<WriteStartAttributeAsync_NoAdvanceState>d__133 <WriteStartAttributeAsync_NoAdvanceState>d__;
			<WriteStartAttributeAsync_NoAdvanceState>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartAttributeAsync_NoAdvanceState>d__.<>4__this = this;
			<WriteStartAttributeAsync_NoAdvanceState>d__.task = task;
			<WriteStartAttributeAsync_NoAdvanceState>d__.prefix = prefix;
			<WriteStartAttributeAsync_NoAdvanceState>d__.localName = localName;
			<WriteStartAttributeAsync_NoAdvanceState>d__.namespaceName = namespaceName;
			<WriteStartAttributeAsync_NoAdvanceState>d__.<>1__state = -1;
			<WriteStartAttributeAsync_NoAdvanceState>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteStartAttributeAsync_NoAdvanceState>d__133>(ref <WriteStartAttributeAsync_NoAdvanceState>d__);
			return <WriteStartAttributeAsync_NoAdvanceState>d__.<>t__builder.Task;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0003EEA0 File Offset: 0x0003D0A0
		protected internal override Task WriteEndAttributeAsync()
		{
			Task result;
			try
			{
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.EndAttribute);
				result = this.SequenceRun(task, new Func<Task>(this.WriteEndAttributeAsync_NoAdvance));
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0003EEE8 File Offset: 0x0003D0E8
		private Task WriteEndAttributeAsync_NoAdvance()
		{
			Task result;
			try
			{
				if (this.specAttr != XmlWellFormedWriter.SpecialAttribute.No)
				{
					result = this.WriteEndAttributeAsync_SepcialAtt();
				}
				else
				{
					result = this.TryReturnTask(this.writer.WriteEndAttributeAsync());
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0003EF38 File Offset: 0x0003D138
		private Task WriteEndAttributeAsync_SepcialAtt()
		{
			XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136 <WriteEndAttributeAsync_SepcialAtt>d__;
			<WriteEndAttributeAsync_SepcialAtt>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteEndAttributeAsync_SepcialAtt>d__.<>4__this = this;
			<WriteEndAttributeAsync_SepcialAtt>d__.<>1__state = -1;
			<WriteEndAttributeAsync_SepcialAtt>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteEndAttributeAsync_SepcialAtt>d__136>(ref <WriteEndAttributeAsync_SepcialAtt>d__);
			return <WriteEndAttributeAsync_SepcialAtt>d__.<>t__builder.Task;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0003EF7C File Offset: 0x0003D17C
		public override Task WriteCDataAsync(string text)
		{
			XmlWellFormedWriter.<WriteCDataAsync>d__137 <WriteCDataAsync>d__;
			<WriteCDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCDataAsync>d__.<>4__this = this;
			<WriteCDataAsync>d__.text = text;
			<WriteCDataAsync>d__.<>1__state = -1;
			<WriteCDataAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteCDataAsync>d__137>(ref <WriteCDataAsync>d__);
			return <WriteCDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0003EFC8 File Offset: 0x0003D1C8
		public override Task WriteCommentAsync(string text)
		{
			XmlWellFormedWriter.<WriteCommentAsync>d__138 <WriteCommentAsync>d__;
			<WriteCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCommentAsync>d__.<>4__this = this;
			<WriteCommentAsync>d__.text = text;
			<WriteCommentAsync>d__.<>1__state = -1;
			<WriteCommentAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteCommentAsync>d__138>(ref <WriteCommentAsync>d__);
			return <WriteCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0003F014 File Offset: 0x0003D214
		public override Task WriteProcessingInstructionAsync(string name, string text)
		{
			XmlWellFormedWriter.<WriteProcessingInstructionAsync>d__139 <WriteProcessingInstructionAsync>d__;
			<WriteProcessingInstructionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteProcessingInstructionAsync>d__.<>4__this = this;
			<WriteProcessingInstructionAsync>d__.name = name;
			<WriteProcessingInstructionAsync>d__.text = text;
			<WriteProcessingInstructionAsync>d__.<>1__state = -1;
			<WriteProcessingInstructionAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteProcessingInstructionAsync>d__139>(ref <WriteProcessingInstructionAsync>d__);
			return <WriteProcessingInstructionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0003F068 File Offset: 0x0003D268
		public override Task WriteEntityRefAsync(string name)
		{
			XmlWellFormedWriter.<WriteEntityRefAsync>d__140 <WriteEntityRefAsync>d__;
			<WriteEntityRefAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteEntityRefAsync>d__.<>4__this = this;
			<WriteEntityRefAsync>d__.name = name;
			<WriteEntityRefAsync>d__.<>1__state = -1;
			<WriteEntityRefAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteEntityRefAsync>d__140>(ref <WriteEntityRefAsync>d__);
			return <WriteEntityRefAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0003F0B4 File Offset: 0x0003D2B4
		public override Task WriteCharEntityAsync(char ch)
		{
			XmlWellFormedWriter.<WriteCharEntityAsync>d__141 <WriteCharEntityAsync>d__;
			<WriteCharEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCharEntityAsync>d__.<>4__this = this;
			<WriteCharEntityAsync>d__.ch = ch;
			<WriteCharEntityAsync>d__.<>1__state = -1;
			<WriteCharEntityAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteCharEntityAsync>d__141>(ref <WriteCharEntityAsync>d__);
			return <WriteCharEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0003F100 File Offset: 0x0003D300
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			XmlWellFormedWriter.<WriteSurrogateCharEntityAsync>d__142 <WriteSurrogateCharEntityAsync>d__;
			<WriteSurrogateCharEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteSurrogateCharEntityAsync>d__.<>4__this = this;
			<WriteSurrogateCharEntityAsync>d__.lowChar = lowChar;
			<WriteSurrogateCharEntityAsync>d__.highChar = highChar;
			<WriteSurrogateCharEntityAsync>d__.<>1__state = -1;
			<WriteSurrogateCharEntityAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteSurrogateCharEntityAsync>d__142>(ref <WriteSurrogateCharEntityAsync>d__);
			return <WriteSurrogateCharEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0003F154 File Offset: 0x0003D354
		public override Task WriteWhitespaceAsync(string ws)
		{
			XmlWellFormedWriter.<WriteWhitespaceAsync>d__143 <WriteWhitespaceAsync>d__;
			<WriteWhitespaceAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteWhitespaceAsync>d__.<>4__this = this;
			<WriteWhitespaceAsync>d__.ws = ws;
			<WriteWhitespaceAsync>d__.<>1__state = -1;
			<WriteWhitespaceAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteWhitespaceAsync>d__143>(ref <WriteWhitespaceAsync>d__);
			return <WriteWhitespaceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003F1A0 File Offset: 0x0003D3A0
		public override Task WriteStringAsync(string text)
		{
			Task result;
			try
			{
				if (text == null)
				{
					result = AsyncHelper.DoneTask;
				}
				else
				{
					Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.Text);
					if (task.IsSuccess())
					{
						result = this.WriteStringAsync_NoAdvanceState(text);
					}
					else
					{
						result = this.WriteStringAsync_NoAdvanceState(task, text);
					}
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0003F1FC File Offset: 0x0003D3FC
		private Task WriteStringAsync_NoAdvanceState(string text)
		{
			Task result;
			try
			{
				if (this.SaveAttrValue)
				{
					this.attrValueCache.WriteString(text);
					result = AsyncHelper.DoneTask;
				}
				else
				{
					result = this.TryReturnTask(this.writer.WriteStringAsync(text));
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003F258 File Offset: 0x0003D458
		private Task WriteStringAsync_NoAdvanceState(Task task, string text)
		{
			XmlWellFormedWriter.<WriteStringAsync_NoAdvanceState>d__146 <WriteStringAsync_NoAdvanceState>d__;
			<WriteStringAsync_NoAdvanceState>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStringAsync_NoAdvanceState>d__.<>4__this = this;
			<WriteStringAsync_NoAdvanceState>d__.task = task;
			<WriteStringAsync_NoAdvanceState>d__.text = text;
			<WriteStringAsync_NoAdvanceState>d__.<>1__state = -1;
			<WriteStringAsync_NoAdvanceState>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteStringAsync_NoAdvanceState>d__146>(ref <WriteStringAsync_NoAdvanceState>d__);
			return <WriteStringAsync_NoAdvanceState>d__.<>t__builder.Task;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0003F2AC File Offset: 0x0003D4AC
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			XmlWellFormedWriter.<WriteCharsAsync>d__147 <WriteCharsAsync>d__;
			<WriteCharsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCharsAsync>d__.<>4__this = this;
			<WriteCharsAsync>d__.buffer = buffer;
			<WriteCharsAsync>d__.index = index;
			<WriteCharsAsync>d__.count = count;
			<WriteCharsAsync>d__.<>1__state = -1;
			<WriteCharsAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteCharsAsync>d__147>(ref <WriteCharsAsync>d__);
			return <WriteCharsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0003F308 File Offset: 0x0003D508
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			XmlWellFormedWriter.<WriteRawAsync>d__148 <WriteRawAsync>d__;
			<WriteRawAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawAsync>d__.<>4__this = this;
			<WriteRawAsync>d__.buffer = buffer;
			<WriteRawAsync>d__.index = index;
			<WriteRawAsync>d__.count = count;
			<WriteRawAsync>d__.<>1__state = -1;
			<WriteRawAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteRawAsync>d__148>(ref <WriteRawAsync>d__);
			return <WriteRawAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0003F364 File Offset: 0x0003D564
		public override Task WriteRawAsync(string data)
		{
			XmlWellFormedWriter.<WriteRawAsync>d__149 <WriteRawAsync>d__;
			<WriteRawAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawAsync>d__.<>4__this = this;
			<WriteRawAsync>d__.data = data;
			<WriteRawAsync>d__.<>1__state = -1;
			<WriteRawAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteRawAsync>d__149>(ref <WriteRawAsync>d__);
			return <WriteRawAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0003F3B0 File Offset: 0x0003D5B0
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			Task result;
			try
			{
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (count < 0)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (count > buffer.Length - index)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				Task task = this.AdvanceStateAsync(XmlWellFormedWriter.Token.Base64);
				if (task.IsSuccess())
				{
					result = this.TryReturnTask(this.writer.WriteBase64Async(buffer, index, count));
				}
				else
				{
					result = this.WriteBase64Async_NoAdvanceState(task, buffer, index, count);
				}
			}
			catch
			{
				this.currentState = XmlWellFormedWriter.State.Error;
				throw;
			}
			return result;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003F450 File Offset: 0x0003D650
		private Task WriteBase64Async_NoAdvanceState(Task task, byte[] buffer, int index, int count)
		{
			XmlWellFormedWriter.<WriteBase64Async_NoAdvanceState>d__151 <WriteBase64Async_NoAdvanceState>d__;
			<WriteBase64Async_NoAdvanceState>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteBase64Async_NoAdvanceState>d__.<>4__this = this;
			<WriteBase64Async_NoAdvanceState>d__.task = task;
			<WriteBase64Async_NoAdvanceState>d__.buffer = buffer;
			<WriteBase64Async_NoAdvanceState>d__.index = index;
			<WriteBase64Async_NoAdvanceState>d__.count = count;
			<WriteBase64Async_NoAdvanceState>d__.<>1__state = -1;
			<WriteBase64Async_NoAdvanceState>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteBase64Async_NoAdvanceState>d__151>(ref <WriteBase64Async_NoAdvanceState>d__);
			return <WriteBase64Async_NoAdvanceState>d__.<>t__builder.Task;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0003F4B4 File Offset: 0x0003D6B4
		public override Task FlushAsync()
		{
			XmlWellFormedWriter.<FlushAsync>d__152 <FlushAsync>d__;
			<FlushAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushAsync>d__.<>4__this = this;
			<FlushAsync>d__.<>1__state = -1;
			<FlushAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<FlushAsync>d__152>(ref <FlushAsync>d__);
			return <FlushAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003F4F8 File Offset: 0x0003D6F8
		public override Task WriteQualifiedNameAsync(string localName, string ns)
		{
			XmlWellFormedWriter.<WriteQualifiedNameAsync>d__153 <WriteQualifiedNameAsync>d__;
			<WriteQualifiedNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteQualifiedNameAsync>d__.<>4__this = this;
			<WriteQualifiedNameAsync>d__.localName = localName;
			<WriteQualifiedNameAsync>d__.ns = ns;
			<WriteQualifiedNameAsync>d__.<>1__state = -1;
			<WriteQualifiedNameAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteQualifiedNameAsync>d__153>(ref <WriteQualifiedNameAsync>d__);
			return <WriteQualifiedNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0003F54C File Offset: 0x0003D74C
		public override Task WriteBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlWellFormedWriter.<WriteBinHexAsync>d__154 <WriteBinHexAsync>d__;
			<WriteBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteBinHexAsync>d__.<>4__this = this;
			<WriteBinHexAsync>d__.buffer = buffer;
			<WriteBinHexAsync>d__.index = index;
			<WriteBinHexAsync>d__.count = count;
			<WriteBinHexAsync>d__.<>1__state = -1;
			<WriteBinHexAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteBinHexAsync>d__154>(ref <WriteBinHexAsync>d__);
			return <WriteBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003F5A8 File Offset: 0x0003D7A8
		private Task WriteStartDocumentImplAsync(XmlStandalone standalone)
		{
			XmlWellFormedWriter.<WriteStartDocumentImplAsync>d__155 <WriteStartDocumentImplAsync>d__;
			<WriteStartDocumentImplAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartDocumentImplAsync>d__.<>4__this = this;
			<WriteStartDocumentImplAsync>d__.standalone = standalone;
			<WriteStartDocumentImplAsync>d__.<>1__state = -1;
			<WriteStartDocumentImplAsync>d__.<>t__builder.Start<XmlWellFormedWriter.<WriteStartDocumentImplAsync>d__155>(ref <WriteStartDocumentImplAsync>d__);
			return <WriteStartDocumentImplAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003F5F3 File Offset: 0x0003D7F3
		private Task AdvanceStateAsync_ReturnWhenFinish(Task task, XmlWellFormedWriter.State newState)
		{
			if (task.IsSuccess())
			{
				this.currentState = newState;
				return AsyncHelper.DoneTask;
			}
			return this._AdvanceStateAsync_ReturnWhenFinish(task, newState);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003F614 File Offset: 0x0003D814
		private Task _AdvanceStateAsync_ReturnWhenFinish(Task task, XmlWellFormedWriter.State newState)
		{
			XmlWellFormedWriter.<_AdvanceStateAsync_ReturnWhenFinish>d__157 <_AdvanceStateAsync_ReturnWhenFinish>d__;
			<_AdvanceStateAsync_ReturnWhenFinish>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_AdvanceStateAsync_ReturnWhenFinish>d__.<>4__this = this;
			<_AdvanceStateAsync_ReturnWhenFinish>d__.task = task;
			<_AdvanceStateAsync_ReturnWhenFinish>d__.newState = newState;
			<_AdvanceStateAsync_ReturnWhenFinish>d__.<>1__state = -1;
			<_AdvanceStateAsync_ReturnWhenFinish>d__.<>t__builder.Start<XmlWellFormedWriter.<_AdvanceStateAsync_ReturnWhenFinish>d__157>(ref <_AdvanceStateAsync_ReturnWhenFinish>d__);
			return <_AdvanceStateAsync_ReturnWhenFinish>d__.<>t__builder.Task;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003F667 File Offset: 0x0003D867
		private Task AdvanceStateAsync_ContinueWhenFinish(Task task, XmlWellFormedWriter.State newState, XmlWellFormedWriter.Token token)
		{
			if (task.IsSuccess())
			{
				this.currentState = newState;
				return this.AdvanceStateAsync(token);
			}
			return this._AdvanceStateAsync_ContinueWhenFinish(task, newState, token);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0003F68C File Offset: 0x0003D88C
		private Task _AdvanceStateAsync_ContinueWhenFinish(Task task, XmlWellFormedWriter.State newState, XmlWellFormedWriter.Token token)
		{
			XmlWellFormedWriter.<_AdvanceStateAsync_ContinueWhenFinish>d__159 <_AdvanceStateAsync_ContinueWhenFinish>d__;
			<_AdvanceStateAsync_ContinueWhenFinish>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_AdvanceStateAsync_ContinueWhenFinish>d__.<>4__this = this;
			<_AdvanceStateAsync_ContinueWhenFinish>d__.task = task;
			<_AdvanceStateAsync_ContinueWhenFinish>d__.newState = newState;
			<_AdvanceStateAsync_ContinueWhenFinish>d__.token = token;
			<_AdvanceStateAsync_ContinueWhenFinish>d__.<>1__state = -1;
			<_AdvanceStateAsync_ContinueWhenFinish>d__.<>t__builder.Start<XmlWellFormedWriter.<_AdvanceStateAsync_ContinueWhenFinish>d__159>(ref <_AdvanceStateAsync_ContinueWhenFinish>d__);
			return <_AdvanceStateAsync_ContinueWhenFinish>d__.<>t__builder.Task;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0003F6E8 File Offset: 0x0003D8E8
		private Task AdvanceStateAsync(XmlWellFormedWriter.Token token)
		{
			if (this.currentState < XmlWellFormedWriter.State.Closed)
			{
				XmlWellFormedWriter.State state;
				for (;;)
				{
					state = this.stateTable[(int)(((int)token << 4) + (int)this.currentState)];
					if (state < XmlWellFormedWriter.State.Error)
					{
						break;
					}
					if (state != XmlWellFormedWriter.State.Error)
					{
						switch (state)
						{
						case XmlWellFormedWriter.State.StartContent:
							goto IL_E3;
						case XmlWellFormedWriter.State.StartContentEle:
							goto IL_F1;
						case XmlWellFormedWriter.State.StartContentB64:
							goto IL_FF;
						case XmlWellFormedWriter.State.StartDoc:
							goto IL_10D;
						case XmlWellFormedWriter.State.StartDocEle:
							goto IL_11B;
						case XmlWellFormedWriter.State.EndAttrSEle:
							goto IL_129;
						case XmlWellFormedWriter.State.EndAttrEEle:
							goto IL_14B;
						case XmlWellFormedWriter.State.EndAttrSCont:
							goto IL_16D;
						case XmlWellFormedWriter.State.EndAttrSAttr:
							goto IL_18F;
						case XmlWellFormedWriter.State.PostB64Cont:
							if (this.rawWriter != null)
							{
								goto Block_6;
							}
							this.currentState = XmlWellFormedWriter.State.Content;
							continue;
						case XmlWellFormedWriter.State.PostB64Attr:
							if (this.rawWriter != null)
							{
								goto Block_7;
							}
							this.currentState = XmlWellFormedWriter.State.Attribute;
							continue;
						case XmlWellFormedWriter.State.PostB64RootAttr:
							if (this.rawWriter != null)
							{
								goto Block_8;
							}
							this.currentState = XmlWellFormedWriter.State.RootLevelAttr;
							continue;
						case XmlWellFormedWriter.State.StartFragEle:
							goto IL_217;
						case XmlWellFormedWriter.State.StartFragCont:
							goto IL_221;
						case XmlWellFormedWriter.State.StartFragB64:
							goto IL_22B;
						case XmlWellFormedWriter.State.StartRootLevelAttr:
							goto IL_235;
						}
						break;
					}
					goto IL_D1;
				}
				goto IL_244;
				IL_D1:
				this.ThrowInvalidStateTransition(token, this.currentState);
				goto IL_244;
				IL_E3:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.StartElementContentAsync(), XmlWellFormedWriter.State.Content);
				IL_F1:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.StartElementContentAsync(), XmlWellFormedWriter.State.Element);
				IL_FF:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.StartElementContentAsync(), XmlWellFormedWriter.State.B64Content);
				IL_10D:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteStartDocumentAsync(), XmlWellFormedWriter.State.Document);
				IL_11B:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteStartDocumentAsync(), XmlWellFormedWriter.State.Element);
				IL_129:
				Task task = this.SequenceRun(this.WriteEndAttributeAsync(), new Func<Task>(this.StartElementContentAsync));
				return this.AdvanceStateAsync_ReturnWhenFinish(task, XmlWellFormedWriter.State.Element);
				IL_14B:
				task = this.SequenceRun(this.WriteEndAttributeAsync(), new Func<Task>(this.StartElementContentAsync));
				return this.AdvanceStateAsync_ReturnWhenFinish(task, XmlWellFormedWriter.State.Content);
				IL_16D:
				task = this.SequenceRun(this.WriteEndAttributeAsync(), new Func<Task>(this.StartElementContentAsync));
				return this.AdvanceStateAsync_ReturnWhenFinish(task, XmlWellFormedWriter.State.Content);
				IL_18F:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteEndAttributeAsync(), XmlWellFormedWriter.State.Attribute);
				Block_6:
				return this.AdvanceStateAsync_ContinueWhenFinish(this.rawWriter.WriteEndBase64Async(), XmlWellFormedWriter.State.Content, token);
				Block_7:
				return this.AdvanceStateAsync_ContinueWhenFinish(this.rawWriter.WriteEndBase64Async(), XmlWellFormedWriter.State.Attribute, token);
				Block_8:
				return this.AdvanceStateAsync_ContinueWhenFinish(this.rawWriter.WriteEndBase64Async(), XmlWellFormedWriter.State.RootLevelAttr, token);
				IL_217:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Element;
				goto IL_244;
				IL_221:
				this.StartFragment();
				state = XmlWellFormedWriter.State.Content;
				goto IL_244;
				IL_22B:
				this.StartFragment();
				state = XmlWellFormedWriter.State.B64Content;
				goto IL_244;
				IL_235:
				return this.AdvanceStateAsync_ReturnWhenFinish(this.WriteEndAttributeAsync(), XmlWellFormedWriter.State.RootLevelAttr);
				IL_244:
				this.currentState = state;
				return AsyncHelper.DoneTask;
			}
			if (this.currentState == XmlWellFormedWriter.State.Closed || this.currentState == XmlWellFormedWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("Xml_ClosedOrError"));
			}
			throw new InvalidOperationException(Res.GetString("Xml_WrongToken", new object[]
			{
				XmlWellFormedWriter.tokenName[(int)token],
				XmlWellFormedWriter.GetStateName(this.currentState)
			}));
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0003F948 File Offset: 0x0003DB48
		private Task StartElementContentAsync_WithNS()
		{
			XmlWellFormedWriter.<StartElementContentAsync_WithNS>d__161 <StartElementContentAsync_WithNS>d__;
			<StartElementContentAsync_WithNS>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<StartElementContentAsync_WithNS>d__.<>4__this = this;
			<StartElementContentAsync_WithNS>d__.<>1__state = -1;
			<StartElementContentAsync_WithNS>d__.<>t__builder.Start<XmlWellFormedWriter.<StartElementContentAsync_WithNS>d__161>(ref <StartElementContentAsync_WithNS>d__);
			return <StartElementContentAsync_WithNS>d__.<>t__builder.Task;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0003F98B File Offset: 0x0003DB8B
		private Task StartElementContentAsync()
		{
			if (this.nsTop > this.elemScopeStack[this.elemTop].prevNSTop)
			{
				return this.StartElementContentAsync_WithNS();
			}
			if (this.rawWriter != null)
			{
				this.rawWriter.StartElementContent();
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x0400041B RID: 1051
		private XmlWriter writer;

		// Token: 0x0400041C RID: 1052
		private XmlRawWriter rawWriter;

		// Token: 0x0400041D RID: 1053
		private IXmlNamespaceResolver predefinedNamespaces;

		// Token: 0x0400041E RID: 1054
		private XmlWellFormedWriter.Namespace[] nsStack;

		// Token: 0x0400041F RID: 1055
		private int nsTop;

		// Token: 0x04000420 RID: 1056
		private Dictionary<string, int> nsHashtable;

		// Token: 0x04000421 RID: 1057
		private bool useNsHashtable;

		// Token: 0x04000422 RID: 1058
		private XmlWellFormedWriter.ElementScope[] elemScopeStack;

		// Token: 0x04000423 RID: 1059
		private int elemTop;

		// Token: 0x04000424 RID: 1060
		private XmlWellFormedWriter.AttrName[] attrStack;

		// Token: 0x04000425 RID: 1061
		private int attrCount;

		// Token: 0x04000426 RID: 1062
		private Dictionary<string, int> attrHashTable;

		// Token: 0x04000427 RID: 1063
		private XmlWellFormedWriter.SpecialAttribute specAttr;

		// Token: 0x04000428 RID: 1064
		private XmlWellFormedWriter.AttributeValueCache attrValueCache;

		// Token: 0x04000429 RID: 1065
		private string curDeclPrefix;

		// Token: 0x0400042A RID: 1066
		private XmlWellFormedWriter.State[] stateTable;

		// Token: 0x0400042B RID: 1067
		private XmlWellFormedWriter.State currentState;

		// Token: 0x0400042C RID: 1068
		private bool checkCharacters;

		// Token: 0x0400042D RID: 1069
		private bool omitDuplNamespaces;

		// Token: 0x0400042E RID: 1070
		private bool writeEndDocumentOnClose;

		// Token: 0x0400042F RID: 1071
		private ConformanceLevel conformanceLevel;

		// Token: 0x04000430 RID: 1072
		private bool dtdWritten;

		// Token: 0x04000431 RID: 1073
		private bool xmlDeclFollows;

		// Token: 0x04000432 RID: 1074
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000433 RID: 1075
		private SecureStringHasher hasher;

		// Token: 0x04000434 RID: 1076
		private const int ElementStackInitialSize = 8;

		// Token: 0x04000435 RID: 1077
		private const int NamespaceStackInitialSize = 8;

		// Token: 0x04000436 RID: 1078
		private const int AttributeArrayInitialSize = 8;

		// Token: 0x04000437 RID: 1079
		private const int MaxAttrDuplWalkCount = 14;

		// Token: 0x04000438 RID: 1080
		private const int MaxNamespacesWalkCount = 16;

		// Token: 0x04000439 RID: 1081
		internal static readonly string[] stateName = new string[]
		{
			"Start",
			"TopLevel",
			"Document",
			"Element Start Tag",
			"Element Content",
			"Element Content",
			"Attribute",
			"EndRootElement",
			"Attribute",
			"Special Attribute",
			"End Document",
			"Root Level Attribute Value",
			"Root Level Special Attribute Value",
			"Root Level Base64 Attribute Value",
			"After Root Level Attribute",
			"Closed",
			"Error"
		};

		// Token: 0x0400043A RID: 1082
		internal static readonly string[] tokenName = new string[]
		{
			"StartDocument",
			"EndDocument",
			"PI",
			"Comment",
			"DTD",
			"StartElement",
			"EndElement",
			"StartAttribute",
			"EndAttribute",
			"Text",
			"CDATA",
			"Atomic value",
			"Base64",
			"RawData",
			"Whitespace"
		};

		// Token: 0x0400043B RID: 1083
		private static WriteState[] state2WriteState = new WriteState[]
		{
			WriteState.Start,
			WriteState.Prolog,
			WriteState.Prolog,
			WriteState.Element,
			WriteState.Content,
			WriteState.Content,
			WriteState.Attribute,
			WriteState.Content,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Content,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Attribute,
			WriteState.Closed,
			WriteState.Error
		};

		// Token: 0x0400043C RID: 1084
		private static readonly XmlWellFormedWriter.State[] StateTableDocument = new XmlWellFormedWriter.State[]
		{
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndDocument,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDocEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.StartContentEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContentB64,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error
		};

		// Token: 0x0400043D RID: 1085
		private static readonly XmlWellFormedWriter.State[] StateTableAuto = new XmlWellFormedWriter.State[]
		{
			XmlWellFormedWriter.State.Document,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndDocument,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartDoc,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContentEle,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.EndAttrSEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.EndAttrEEle,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.EndAttrSAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartRootLevelAttr,
			XmlWellFormedWriter.State.StartRootLevelAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Element,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.RootLevelSpecAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.EndAttrSCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragB64,
			XmlWellFormedWriter.State.StartFragB64,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContentB64,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.B64Content,
			XmlWellFormedWriter.State.B64Attribute,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelB64Attr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartFragCont,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.RootLevelSpecAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.TopLevel,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.StartContent,
			XmlWellFormedWriter.State.Content,
			XmlWellFormedWriter.State.PostB64Cont,
			XmlWellFormedWriter.State.PostB64Attr,
			XmlWellFormedWriter.State.AfterRootEle,
			XmlWellFormedWriter.State.Attribute,
			XmlWellFormedWriter.State.SpecialAttr,
			XmlWellFormedWriter.State.Error,
			XmlWellFormedWriter.State.RootLevelAttr,
			XmlWellFormedWriter.State.RootLevelSpecAttr,
			XmlWellFormedWriter.State.PostB64RootAttr,
			XmlWellFormedWriter.State.AfterRootLevelAttr,
			XmlWellFormedWriter.State.Error
		};

		// Token: 0x020003F3 RID: 1011
		private enum State
		{
			// Token: 0x04001A60 RID: 6752
			Start,
			// Token: 0x04001A61 RID: 6753
			TopLevel,
			// Token: 0x04001A62 RID: 6754
			Document,
			// Token: 0x04001A63 RID: 6755
			Element,
			// Token: 0x04001A64 RID: 6756
			Content,
			// Token: 0x04001A65 RID: 6757
			B64Content,
			// Token: 0x04001A66 RID: 6758
			B64Attribute,
			// Token: 0x04001A67 RID: 6759
			AfterRootEle,
			// Token: 0x04001A68 RID: 6760
			Attribute,
			// Token: 0x04001A69 RID: 6761
			SpecialAttr,
			// Token: 0x04001A6A RID: 6762
			EndDocument,
			// Token: 0x04001A6B RID: 6763
			RootLevelAttr,
			// Token: 0x04001A6C RID: 6764
			RootLevelSpecAttr,
			// Token: 0x04001A6D RID: 6765
			RootLevelB64Attr,
			// Token: 0x04001A6E RID: 6766
			AfterRootLevelAttr,
			// Token: 0x04001A6F RID: 6767
			Closed,
			// Token: 0x04001A70 RID: 6768
			Error,
			// Token: 0x04001A71 RID: 6769
			StartContent = 101,
			// Token: 0x04001A72 RID: 6770
			StartContentEle,
			// Token: 0x04001A73 RID: 6771
			StartContentB64,
			// Token: 0x04001A74 RID: 6772
			StartDoc,
			// Token: 0x04001A75 RID: 6773
			StartDocEle = 106,
			// Token: 0x04001A76 RID: 6774
			EndAttrSEle,
			// Token: 0x04001A77 RID: 6775
			EndAttrEEle,
			// Token: 0x04001A78 RID: 6776
			EndAttrSCont,
			// Token: 0x04001A79 RID: 6777
			EndAttrSAttr = 111,
			// Token: 0x04001A7A RID: 6778
			PostB64Cont,
			// Token: 0x04001A7B RID: 6779
			PostB64Attr,
			// Token: 0x04001A7C RID: 6780
			PostB64RootAttr,
			// Token: 0x04001A7D RID: 6781
			StartFragEle,
			// Token: 0x04001A7E RID: 6782
			StartFragCont,
			// Token: 0x04001A7F RID: 6783
			StartFragB64,
			// Token: 0x04001A80 RID: 6784
			StartRootLevelAttr
		}

		// Token: 0x020003F4 RID: 1012
		private enum Token
		{
			// Token: 0x04001A82 RID: 6786
			StartDocument,
			// Token: 0x04001A83 RID: 6787
			EndDocument,
			// Token: 0x04001A84 RID: 6788
			PI,
			// Token: 0x04001A85 RID: 6789
			Comment,
			// Token: 0x04001A86 RID: 6790
			Dtd,
			// Token: 0x04001A87 RID: 6791
			StartElement,
			// Token: 0x04001A88 RID: 6792
			EndElement,
			// Token: 0x04001A89 RID: 6793
			StartAttribute,
			// Token: 0x04001A8A RID: 6794
			EndAttribute,
			// Token: 0x04001A8B RID: 6795
			Text,
			// Token: 0x04001A8C RID: 6796
			CData,
			// Token: 0x04001A8D RID: 6797
			AtomicValue,
			// Token: 0x04001A8E RID: 6798
			Base64,
			// Token: 0x04001A8F RID: 6799
			RawData,
			// Token: 0x04001A90 RID: 6800
			Whitespace
		}

		// Token: 0x020003F5 RID: 1013
		private class NamespaceResolverProxy : IXmlNamespaceResolver
		{
			// Token: 0x06002FB3 RID: 12211 RVA: 0x0010AF2E File Offset: 0x0010912E
			internal NamespaceResolverProxy(XmlWellFormedWriter wfWriter)
			{
				this.wfWriter = wfWriter;
			}

			// Token: 0x06002FB4 RID: 12212 RVA: 0x0010AF3D File Offset: 0x0010913D
			IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06002FB5 RID: 12213 RVA: 0x0010AF44 File Offset: 0x00109144
			string IXmlNamespaceResolver.LookupNamespace(string prefix)
			{
				return this.wfWriter.LookupNamespace(prefix);
			}

			// Token: 0x06002FB6 RID: 12214 RVA: 0x0010AF52 File Offset: 0x00109152
			string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
			{
				return this.wfWriter.LookupPrefix(namespaceName);
			}

			// Token: 0x04001A91 RID: 6801
			private XmlWellFormedWriter wfWriter;
		}

		// Token: 0x020003F6 RID: 1014
		private struct ElementScope
		{
			// Token: 0x06002FB7 RID: 12215 RVA: 0x0010AF60 File Offset: 0x00109160
			internal void Set(string prefix, string localName, string namespaceUri, int prevNSTop)
			{
				this.prevNSTop = prevNSTop;
				this.prefix = prefix;
				this.namespaceUri = namespaceUri;
				this.localName = localName;
				this.xmlSpace = (XmlSpace)(-1);
				this.xmlLang = null;
			}

			// Token: 0x06002FB8 RID: 12216 RVA: 0x0010AF8D File Offset: 0x0010918D
			internal void WriteEndElement(XmlRawWriter rawWriter)
			{
				rawWriter.WriteEndElement(this.prefix, this.localName, this.namespaceUri);
			}

			// Token: 0x06002FB9 RID: 12217 RVA: 0x0010AFA7 File Offset: 0x001091A7
			internal void WriteFullEndElement(XmlRawWriter rawWriter)
			{
				rawWriter.WriteFullEndElement(this.prefix, this.localName, this.namespaceUri);
			}

			// Token: 0x06002FBA RID: 12218 RVA: 0x0010AFC1 File Offset: 0x001091C1
			internal Task WriteEndElementAsync(XmlRawWriter rawWriter)
			{
				return rawWriter.WriteEndElementAsync(this.prefix, this.localName, this.namespaceUri);
			}

			// Token: 0x06002FBB RID: 12219 RVA: 0x0010AFDB File Offset: 0x001091DB
			internal Task WriteFullEndElementAsync(XmlRawWriter rawWriter)
			{
				return rawWriter.WriteFullEndElementAsync(this.prefix, this.localName, this.namespaceUri);
			}

			// Token: 0x04001A92 RID: 6802
			internal int prevNSTop;

			// Token: 0x04001A93 RID: 6803
			internal string prefix;

			// Token: 0x04001A94 RID: 6804
			internal string localName;

			// Token: 0x04001A95 RID: 6805
			internal string namespaceUri;

			// Token: 0x04001A96 RID: 6806
			internal XmlSpace xmlSpace;

			// Token: 0x04001A97 RID: 6807
			internal string xmlLang;
		}

		// Token: 0x020003F7 RID: 1015
		private enum NamespaceKind
		{
			// Token: 0x04001A99 RID: 6809
			Written,
			// Token: 0x04001A9A RID: 6810
			NeedToWrite,
			// Token: 0x04001A9B RID: 6811
			Implied,
			// Token: 0x04001A9C RID: 6812
			Special
		}

		// Token: 0x020003F8 RID: 1016
		private struct Namespace
		{
			// Token: 0x06002FBC RID: 12220 RVA: 0x0010AFF5 File Offset: 0x001091F5
			internal void Set(string prefix, string namespaceUri, XmlWellFormedWriter.NamespaceKind kind)
			{
				this.prefix = prefix;
				this.namespaceUri = namespaceUri;
				this.kind = kind;
				this.prevNsIndex = -1;
			}

			// Token: 0x06002FBD RID: 12221 RVA: 0x0010B014 File Offset: 0x00109214
			internal void WriteDecl(XmlWriter writer, XmlRawWriter rawWriter)
			{
				if (rawWriter != null)
				{
					rawWriter.WriteNamespaceDeclaration(this.prefix, this.namespaceUri);
					return;
				}
				if (this.prefix.Length == 0)
				{
					writer.WriteStartAttribute(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/");
				}
				else
				{
					writer.WriteStartAttribute("xmlns", this.prefix, "http://www.w3.org/2000/xmlns/");
				}
				writer.WriteString(this.namespaceUri);
				writer.WriteEndAttribute();
			}

			// Token: 0x06002FBE RID: 12222 RVA: 0x0010B084 File Offset: 0x00109284
			internal Task WriteDeclAsync(XmlWriter writer, XmlRawWriter rawWriter)
			{
				XmlWellFormedWriter.Namespace.<WriteDeclAsync>d__6 <WriteDeclAsync>d__;
				<WriteDeclAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
				<WriteDeclAsync>d__.<>4__this = this;
				<WriteDeclAsync>d__.writer = writer;
				<WriteDeclAsync>d__.rawWriter = rawWriter;
				<WriteDeclAsync>d__.<>1__state = -1;
				<WriteDeclAsync>d__.<>t__builder.Start<XmlWellFormedWriter.Namespace.<WriteDeclAsync>d__6>(ref <WriteDeclAsync>d__);
				return <WriteDeclAsync>d__.<>t__builder.Task;
			}

			// Token: 0x04001A9D RID: 6813
			internal string prefix;

			// Token: 0x04001A9E RID: 6814
			internal string namespaceUri;

			// Token: 0x04001A9F RID: 6815
			internal XmlWellFormedWriter.NamespaceKind kind;

			// Token: 0x04001AA0 RID: 6816
			internal int prevNsIndex;
		}

		// Token: 0x020003F9 RID: 1017
		private struct AttrName
		{
			// Token: 0x06002FBF RID: 12223 RVA: 0x0010B0DC File Offset: 0x001092DC
			internal void Set(string prefix, string localName, string namespaceUri)
			{
				this.prefix = prefix;
				this.namespaceUri = namespaceUri;
				this.localName = localName;
				this.prev = 0;
			}

			// Token: 0x06002FC0 RID: 12224 RVA: 0x0010B0FA File Offset: 0x001092FA
			internal bool IsDuplicate(string prefix, string localName, string namespaceUri)
			{
				return this.localName == localName && (this.prefix == prefix || this.namespaceUri == namespaceUri);
			}

			// Token: 0x04001AA1 RID: 6817
			internal string prefix;

			// Token: 0x04001AA2 RID: 6818
			internal string namespaceUri;

			// Token: 0x04001AA3 RID: 6819
			internal string localName;

			// Token: 0x04001AA4 RID: 6820
			internal int prev;
		}

		// Token: 0x020003FA RID: 1018
		private enum SpecialAttribute
		{
			// Token: 0x04001AA6 RID: 6822
			No,
			// Token: 0x04001AA7 RID: 6823
			DefaultXmlns,
			// Token: 0x04001AA8 RID: 6824
			PrefixedXmlns,
			// Token: 0x04001AA9 RID: 6825
			XmlSpace,
			// Token: 0x04001AAA RID: 6826
			XmlLang
		}

		// Token: 0x020003FB RID: 1019
		private class AttributeValueCache
		{
			// Token: 0x17000A3F RID: 2623
			// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x0010B128 File Offset: 0x00109328
			internal string StringValue
			{
				get
				{
					if (this.singleStringValue != null)
					{
						return this.singleStringValue;
					}
					return this.stringValue.ToString();
				}
			}

			// Token: 0x06002FC2 RID: 12226 RVA: 0x0010B144 File Offset: 0x00109344
			internal void WriteEntityRef(string name)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				if (!(name == "lt"))
				{
					if (!(name == "gt"))
					{
						if (!(name == "quot"))
						{
							if (!(name == "apos"))
							{
								if (!(name == "amp"))
								{
									this.stringValue.Append('&');
									this.stringValue.Append(name);
									this.stringValue.Append(';');
								}
								else
								{
									this.stringValue.Append('&');
								}
							}
							else
							{
								this.stringValue.Append('\'');
							}
						}
						else
						{
							this.stringValue.Append('"');
						}
					}
					else
					{
						this.stringValue.Append('>');
					}
				}
				else
				{
					this.stringValue.Append('<');
				}
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.EntityRef, name);
			}

			// Token: 0x06002FC3 RID: 12227 RVA: 0x0010B223 File Offset: 0x00109423
			internal void WriteCharEntity(char ch)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(ch);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.CharEntity, ch);
			}

			// Token: 0x06002FC4 RID: 12228 RVA: 0x0010B24D File Offset: 0x0010944D
			internal void WriteSurrogateCharEntity(char lowChar, char highChar)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(highChar);
				this.stringValue.Append(lowChar);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.SurrogateCharEntity, new char[]
				{
					lowChar,
					highChar
				});
			}

			// Token: 0x06002FC5 RID: 12229 RVA: 0x0010B28C File Offset: 0x0010948C
			internal void WriteWhitespace(string ws)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(ws);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace, ws);
			}

			// Token: 0x06002FC6 RID: 12230 RVA: 0x0010B2B1 File Offset: 0x001094B1
			internal void WriteString(string text)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				else if (this.lastItem == -1)
				{
					this.singleStringValue = text;
					return;
				}
				this.stringValue.Append(text);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.String, text);
			}

			// Token: 0x06002FC7 RID: 12231 RVA: 0x0010B2E9 File Offset: 0x001094E9
			internal void WriteChars(char[] buffer, int index, int count)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(buffer, index, count);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars, new XmlWellFormedWriter.AttributeValueCache.BufferChunk(buffer, index, count));
			}

			// Token: 0x06002FC8 RID: 12232 RVA: 0x0010B317 File Offset: 0x00109517
			internal void WriteRaw(char[] buffer, int index, int count)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(buffer, index, count);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars, new XmlWellFormedWriter.AttributeValueCache.BufferChunk(buffer, index, count));
			}

			// Token: 0x06002FC9 RID: 12233 RVA: 0x0010B345 File Offset: 0x00109545
			internal void WriteRaw(string data)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(data);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.Raw, data);
			}

			// Token: 0x06002FCA RID: 12234 RVA: 0x0010B36A File Offset: 0x0010956A
			internal void WriteValue(string value)
			{
				if (this.singleStringValue != null)
				{
					this.StartComplexValue();
				}
				this.stringValue.Append(value);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString, value);
			}

			// Token: 0x06002FCB RID: 12235 RVA: 0x0010B390 File Offset: 0x00109590
			internal void Replay(XmlWriter writer)
			{
				if (this.singleStringValue != null)
				{
					writer.WriteString(this.singleStringValue);
					return;
				}
				for (int i = this.firstItem; i <= this.lastItem; i++)
				{
					XmlWellFormedWriter.AttributeValueCache.Item item = this.items[i];
					switch (item.type)
					{
					case XmlWellFormedWriter.AttributeValueCache.ItemType.EntityRef:
						writer.WriteEntityRef((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.CharEntity:
						writer.WriteCharEntity((char)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.SurrogateCharEntity:
					{
						char[] array = (char[])item.data;
						writer.WriteSurrogateCharEntity(array[0], array[1]);
						break;
					}
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace:
						writer.WriteWhitespace((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.String:
						writer.WriteString((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
						writer.WriteChars(bufferChunk.buffer, bufferChunk.index, bufferChunk.count);
						break;
					}
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Raw:
						writer.WriteRaw((string)item.data);
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
						writer.WriteChars(bufferChunk.buffer, bufferChunk.index, bufferChunk.count);
						break;
					}
					case XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString:
						writer.WriteValue((string)item.data);
						break;
					}
				}
			}

			// Token: 0x06002FCC RID: 12236 RVA: 0x0010B4F4 File Offset: 0x001096F4
			internal void Trim()
			{
				if (this.singleStringValue != null)
				{
					this.singleStringValue = XmlConvert.TrimString(this.singleStringValue);
					return;
				}
				string text = this.stringValue.ToString();
				string text2 = XmlConvert.TrimString(text);
				if (text != text2)
				{
					this.stringValue = new StringBuilder(text2);
				}
				XmlCharType instance = XmlCharType.Instance;
				int num = this.firstItem;
				while (num == this.firstItem && num <= this.lastItem)
				{
					XmlWellFormedWriter.AttributeValueCache.Item item = this.items[num];
					switch (item.type)
					{
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace:
						this.firstItem++;
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.String:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Raw:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString:
						item.data = XmlConvert.TrimStringStart((string)item.data);
						if (((string)item.data).Length == 0)
						{
							this.firstItem++;
						}
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item.data;
						int num2 = bufferChunk.index + bufferChunk.count;
						while (bufferChunk.index < num2 && instance.IsWhiteSpace(bufferChunk.buffer[bufferChunk.index]))
						{
							bufferChunk.index++;
							bufferChunk.count--;
						}
						if (bufferChunk.index == num2)
						{
							this.firstItem++;
						}
						break;
					}
					}
					num++;
				}
				num = this.lastItem;
				while (num == this.lastItem && num >= this.firstItem)
				{
					XmlWellFormedWriter.AttributeValueCache.Item item2 = this.items[num];
					switch (item2.type)
					{
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Whitespace:
						this.lastItem--;
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.String:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.Raw:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.ValueString:
						item2.data = XmlConvert.TrimStringEnd((string)item2.data);
						if (((string)item2.data).Length == 0)
						{
							this.lastItem--;
						}
						break;
					case XmlWellFormedWriter.AttributeValueCache.ItemType.StringChars:
					case XmlWellFormedWriter.AttributeValueCache.ItemType.RawChars:
					{
						XmlWellFormedWriter.AttributeValueCache.BufferChunk bufferChunk2 = (XmlWellFormedWriter.AttributeValueCache.BufferChunk)item2.data;
						while (bufferChunk2.count > 0 && instance.IsWhiteSpace(bufferChunk2.buffer[bufferChunk2.index + bufferChunk2.count - 1]))
						{
							bufferChunk2.count--;
						}
						if (bufferChunk2.count == 0)
						{
							this.lastItem--;
						}
						break;
					}
					}
					num--;
				}
			}

			// Token: 0x06002FCD RID: 12237 RVA: 0x0010B781 File Offset: 0x00109981
			internal void Clear()
			{
				this.singleStringValue = null;
				this.lastItem = -1;
				this.firstItem = 0;
				this.stringValue.Length = 0;
			}

			// Token: 0x06002FCE RID: 12238 RVA: 0x0010B7A4 File Offset: 0x001099A4
			private void StartComplexValue()
			{
				this.stringValue.Append(this.singleStringValue);
				this.AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType.String, this.singleStringValue);
				this.singleStringValue = null;
			}

			// Token: 0x06002FCF RID: 12239 RVA: 0x0010B7CC File Offset: 0x001099CC
			private void AddItem(XmlWellFormedWriter.AttributeValueCache.ItemType type, object data)
			{
				int num = this.lastItem + 1;
				if (this.items == null)
				{
					this.items = new XmlWellFormedWriter.AttributeValueCache.Item[4];
				}
				else if (this.items.Length == num)
				{
					XmlWellFormedWriter.AttributeValueCache.Item[] destinationArray = new XmlWellFormedWriter.AttributeValueCache.Item[num * 2];
					Array.Copy(this.items, destinationArray, num);
					this.items = destinationArray;
				}
				if (this.items[num] == null)
				{
					this.items[num] = new XmlWellFormedWriter.AttributeValueCache.Item();
				}
				this.items[num].Set(type, data);
				this.lastItem = num;
			}

			// Token: 0x06002FD0 RID: 12240 RVA: 0x0010B850 File Offset: 0x00109A50
			internal Task ReplayAsync(XmlWriter writer)
			{
				XmlWellFormedWriter.AttributeValueCache.<ReplayAsync>d__24 <ReplayAsync>d__;
				<ReplayAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
				<ReplayAsync>d__.<>4__this = this;
				<ReplayAsync>d__.writer = writer;
				<ReplayAsync>d__.<>1__state = -1;
				<ReplayAsync>d__.<>t__builder.Start<XmlWellFormedWriter.AttributeValueCache.<ReplayAsync>d__24>(ref <ReplayAsync>d__);
				return <ReplayAsync>d__.<>t__builder.Task;
			}

			// Token: 0x04001AAB RID: 6827
			private StringBuilder stringValue = new StringBuilder();

			// Token: 0x04001AAC RID: 6828
			private string singleStringValue;

			// Token: 0x04001AAD RID: 6829
			private XmlWellFormedWriter.AttributeValueCache.Item[] items;

			// Token: 0x04001AAE RID: 6830
			private int firstItem;

			// Token: 0x04001AAF RID: 6831
			private int lastItem = -1;

			// Token: 0x020004DD RID: 1245
			private enum ItemType
			{
				// Token: 0x04001FAE RID: 8110
				EntityRef,
				// Token: 0x04001FAF RID: 8111
				CharEntity,
				// Token: 0x04001FB0 RID: 8112
				SurrogateCharEntity,
				// Token: 0x04001FB1 RID: 8113
				Whitespace,
				// Token: 0x04001FB2 RID: 8114
				String,
				// Token: 0x04001FB3 RID: 8115
				StringChars,
				// Token: 0x04001FB4 RID: 8116
				Raw,
				// Token: 0x04001FB5 RID: 8117
				RawChars,
				// Token: 0x04001FB6 RID: 8118
				ValueString
			}

			// Token: 0x020004DE RID: 1246
			private class Item
			{
				// Token: 0x060031C7 RID: 12743 RVA: 0x001214F6 File Offset: 0x0011F6F6
				internal Item()
				{
				}

				// Token: 0x060031C8 RID: 12744 RVA: 0x001214FE File Offset: 0x0011F6FE
				internal void Set(XmlWellFormedWriter.AttributeValueCache.ItemType type, object data)
				{
					this.type = type;
					this.data = data;
				}

				// Token: 0x04001FB7 RID: 8119
				internal XmlWellFormedWriter.AttributeValueCache.ItemType type;

				// Token: 0x04001FB8 RID: 8120
				internal object data;
			}

			// Token: 0x020004DF RID: 1247
			private class BufferChunk
			{
				// Token: 0x060031C9 RID: 12745 RVA: 0x0012150E File Offset: 0x0011F70E
				internal BufferChunk(char[] buffer, int index, int count)
				{
					this.buffer = buffer;
					this.index = index;
					this.count = count;
				}

				// Token: 0x04001FB9 RID: 8121
				internal char[] buffer;

				// Token: 0x04001FBA RID: 8122
				internal int index;

				// Token: 0x04001FBB RID: 8123
				internal int count;
			}
		}
	}
}
