using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000E5 RID: 229
	[__DynamicallyInvokable]
	public abstract class XmlWriter : IDisposable
	{
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00040156 File Offset: 0x0003E356
		[__DynamicallyInvokable]
		public virtual XmlWriterSettings Settings
		{
			[__DynamicallyInvokable]
			get
			{
				return null;
			}
		}

		// Token: 0x06000F0A RID: 3850
		[__DynamicallyInvokable]
		public abstract void WriteStartDocument();

		// Token: 0x06000F0B RID: 3851
		[__DynamicallyInvokable]
		public abstract void WriteStartDocument(bool standalone);

		// Token: 0x06000F0C RID: 3852
		[__DynamicallyInvokable]
		public abstract void WriteEndDocument();

		// Token: 0x06000F0D RID: 3853
		[__DynamicallyInvokable]
		public abstract void WriteDocType(string name, string pubid, string sysid, string subset);

		// Token: 0x06000F0E RID: 3854 RVA: 0x00040159 File Offset: 0x0003E359
		[__DynamicallyInvokable]
		public void WriteStartElement(string localName, string ns)
		{
			this.WriteStartElement(null, localName, ns);
		}

		// Token: 0x06000F0F RID: 3855
		[__DynamicallyInvokable]
		public abstract void WriteStartElement(string prefix, string localName, string ns);

		// Token: 0x06000F10 RID: 3856 RVA: 0x00040164 File Offset: 0x0003E364
		[__DynamicallyInvokable]
		public void WriteStartElement(string localName)
		{
			this.WriteStartElement(null, localName, null);
		}

		// Token: 0x06000F11 RID: 3857
		[__DynamicallyInvokable]
		public abstract void WriteEndElement();

		// Token: 0x06000F12 RID: 3858
		[__DynamicallyInvokable]
		public abstract void WriteFullEndElement();

		// Token: 0x06000F13 RID: 3859 RVA: 0x0004016F File Offset: 0x0003E36F
		[__DynamicallyInvokable]
		public void WriteAttributeString(string localName, string ns, string value)
		{
			this.WriteStartAttribute(null, localName, ns);
			this.WriteString(value);
			this.WriteEndAttribute();
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00040187 File Offset: 0x0003E387
		[__DynamicallyInvokable]
		public void WriteAttributeString(string localName, string value)
		{
			this.WriteStartAttribute(null, localName, null);
			this.WriteString(value);
			this.WriteEndAttribute();
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0004019F File Offset: 0x0003E39F
		[__DynamicallyInvokable]
		public void WriteAttributeString(string prefix, string localName, string ns, string value)
		{
			this.WriteStartAttribute(prefix, localName, ns);
			this.WriteString(value);
			this.WriteEndAttribute();
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x000401B8 File Offset: 0x0003E3B8
		[__DynamicallyInvokable]
		public void WriteStartAttribute(string localName, string ns)
		{
			this.WriteStartAttribute(null, localName, ns);
		}

		// Token: 0x06000F17 RID: 3863
		[__DynamicallyInvokable]
		public abstract void WriteStartAttribute(string prefix, string localName, string ns);

		// Token: 0x06000F18 RID: 3864 RVA: 0x000401C3 File Offset: 0x0003E3C3
		[__DynamicallyInvokable]
		public void WriteStartAttribute(string localName)
		{
			this.WriteStartAttribute(null, localName, null);
		}

		// Token: 0x06000F19 RID: 3865
		[__DynamicallyInvokable]
		public abstract void WriteEndAttribute();

		// Token: 0x06000F1A RID: 3866
		[__DynamicallyInvokable]
		public abstract void WriteCData(string text);

		// Token: 0x06000F1B RID: 3867
		[__DynamicallyInvokable]
		public abstract void WriteComment(string text);

		// Token: 0x06000F1C RID: 3868
		[__DynamicallyInvokable]
		public abstract void WriteProcessingInstruction(string name, string text);

		// Token: 0x06000F1D RID: 3869
		[__DynamicallyInvokable]
		public abstract void WriteEntityRef(string name);

		// Token: 0x06000F1E RID: 3870
		[__DynamicallyInvokable]
		public abstract void WriteCharEntity(char ch);

		// Token: 0x06000F1F RID: 3871
		[__DynamicallyInvokable]
		public abstract void WriteWhitespace(string ws);

		// Token: 0x06000F20 RID: 3872
		[__DynamicallyInvokable]
		public abstract void WriteString(string text);

		// Token: 0x06000F21 RID: 3873
		[__DynamicallyInvokable]
		public abstract void WriteSurrogateCharEntity(char lowChar, char highChar);

		// Token: 0x06000F22 RID: 3874
		[__DynamicallyInvokable]
		public abstract void WriteChars(char[] buffer, int index, int count);

		// Token: 0x06000F23 RID: 3875
		[__DynamicallyInvokable]
		public abstract void WriteRaw(char[] buffer, int index, int count);

		// Token: 0x06000F24 RID: 3876
		[__DynamicallyInvokable]
		public abstract void WriteRaw(string data);

		// Token: 0x06000F25 RID: 3877
		[__DynamicallyInvokable]
		public abstract void WriteBase64(byte[] buffer, int index, int count);

		// Token: 0x06000F26 RID: 3878 RVA: 0x000401CE File Offset: 0x0003E3CE
		[__DynamicallyInvokable]
		public virtual void WriteBinHex(byte[] buffer, int index, int count)
		{
			BinHexEncoder.Encode(buffer, index, count, this);
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000F27 RID: 3879
		[__DynamicallyInvokable]
		public abstract WriteState WriteState { [__DynamicallyInvokable] get; }

		// Token: 0x06000F28 RID: 3880 RVA: 0x000401D9 File Offset: 0x0003E3D9
		public virtual void Close()
		{
		}

		// Token: 0x06000F29 RID: 3881
		[__DynamicallyInvokable]
		public abstract void Flush();

		// Token: 0x06000F2A RID: 3882
		[__DynamicallyInvokable]
		public abstract string LookupPrefix(string ns);

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x000401DB File Offset: 0x0003E3DB
		[__DynamicallyInvokable]
		public virtual XmlSpace XmlSpace
		{
			[__DynamicallyInvokable]
			get
			{
				return XmlSpace.Default;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x000401DE File Offset: 0x0003E3DE
		[__DynamicallyInvokable]
		public virtual string XmlLang
		{
			[__DynamicallyInvokable]
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x000401E5 File Offset: 0x0003E3E5
		[__DynamicallyInvokable]
		public virtual void WriteNmToken(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Xml_EmptyName"));
			}
			this.WriteString(XmlConvert.VerifyNMTOKEN(name, ExceptionType.ArgumentException));
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0004020F File Offset: 0x0003E40F
		[__DynamicallyInvokable]
		public virtual void WriteName(string name)
		{
			this.WriteString(XmlConvert.VerifyQName(name, ExceptionType.ArgumentException));
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00040220 File Offset: 0x0003E420
		[__DynamicallyInvokable]
		public virtual void WriteQualifiedName(string localName, string ns)
		{
			if (ns != null && ns.Length > 0)
			{
				string text = this.LookupPrefix(ns);
				if (text == null)
				{
					throw new ArgumentException(Res.GetString("Xml_UndefNamespace", new object[]
					{
						ns
					}));
				}
				if (text.Length > 0)
				{
					this.WriteString(text);
					this.WriteString(":");
				}
			}
			this.WriteString(localName);
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00040280 File Offset: 0x0003E480
		[__DynamicallyInvokable]
		public virtual void WriteValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.WriteString(XmlUntypedConverter.Untyped.ToString(value, null));
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x000402A2 File Offset: 0x0003E4A2
		[__DynamicallyInvokable]
		public virtual void WriteValue(string value)
		{
			if (value == null)
			{
				return;
			}
			this.WriteString(value);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x000402AF File Offset: 0x0003E4AF
		[__DynamicallyInvokable]
		public virtual void WriteValue(bool value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x000402BD File Offset: 0x0003E4BD
		public virtual void WriteValue(DateTime value)
		{
			this.WriteString(XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind));
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x000402CC File Offset: 0x0003E4CC
		[__DynamicallyInvokable]
		public virtual void WriteValue(DateTimeOffset value)
		{
			if (value.Offset != TimeSpan.Zero)
			{
				this.WriteValue(value.LocalDateTime);
				return;
			}
			this.WriteValue(value.UtcDateTime);
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x000402FC File Offset: 0x0003E4FC
		[__DynamicallyInvokable]
		public virtual void WriteValue(double value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0004030A File Offset: 0x0003E50A
		[__DynamicallyInvokable]
		public virtual void WriteValue(float value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00040318 File Offset: 0x0003E518
		[__DynamicallyInvokable]
		public virtual void WriteValue(decimal value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00040326 File Offset: 0x0003E526
		[__DynamicallyInvokable]
		public virtual void WriteValue(int value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00040334 File Offset: 0x0003E534
		[__DynamicallyInvokable]
		public virtual void WriteValue(long value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00040344 File Offset: 0x0003E544
		[__DynamicallyInvokable]
		public virtual void WriteAttributes(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.XmlDeclaration)
			{
				if (reader.MoveToFirstAttribute())
				{
					this.WriteAttributes(reader, defattr);
					reader.MoveToElement();
					return;
				}
			}
			else
			{
				if (reader.NodeType != XmlNodeType.Attribute)
				{
					throw new XmlException("Xml_InvalidPosition", string.Empty);
				}
				do
				{
					if (defattr || !reader.IsDefaultInternal)
					{
						this.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
						while (reader.ReadAttributeValue())
						{
							if (reader.NodeType == XmlNodeType.EntityReference)
							{
								this.WriteEntityRef(reader.Name);
							}
							else
							{
								this.WriteString(reader.Value);
							}
						}
						this.WriteEndAttribute();
					}
				}
				while (reader.MoveToNextAttribute());
			}
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00040404 File Offset: 0x0003E604
		[__DynamicallyInvokable]
		public virtual void WriteNode(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			bool canReadValueChunk = reader.CanReadValueChunk;
			int num = (reader.NodeType == XmlNodeType.None) ? -1 : reader.Depth;
			do
			{
				switch (reader.NodeType)
				{
				case XmlNodeType.Element:
					this.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
					this.WriteAttributes(reader, defattr);
					if (reader.IsEmptyElement)
					{
						this.WriteEndElement();
					}
					break;
				case XmlNodeType.Text:
					if (canReadValueChunk)
					{
						if (this.writeNodeBuffer == null)
						{
							this.writeNodeBuffer = new char[1024];
						}
						int count;
						while ((count = reader.ReadValueChunk(this.writeNodeBuffer, 0, 1024)) > 0)
						{
							this.WriteChars(this.writeNodeBuffer, 0, count);
						}
					}
					else
					{
						this.WriteString(reader.Value);
					}
					break;
				case XmlNodeType.CDATA:
					this.WriteCData(reader.Value);
					break;
				case XmlNodeType.EntityReference:
					this.WriteEntityRef(reader.Name);
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.XmlDeclaration:
					this.WriteProcessingInstruction(reader.Name, reader.Value);
					break;
				case XmlNodeType.Comment:
					this.WriteComment(reader.Value);
					break;
				case XmlNodeType.DocumentType:
					this.WriteDocType(reader.Name, reader.GetAttribute("PUBLIC"), reader.GetAttribute("SYSTEM"), reader.Value);
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					this.WriteWhitespace(reader.Value);
					break;
				case XmlNodeType.EndElement:
					this.WriteFullEndElement();
					break;
				}
			}
			while (reader.Read() && (num < reader.Depth || (num == reader.Depth && reader.NodeType == XmlNodeType.EndElement)));
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x000405C4 File Offset: 0x0003E7C4
		public virtual void WriteNode(XPathNavigator navigator, bool defattr)
		{
			if (navigator == null)
			{
				throw new ArgumentNullException("navigator");
			}
			int num = 0;
			navigator = navigator.Clone();
			for (;;)
			{
				IL_18:
				bool flag = false;
				switch (navigator.NodeType)
				{
				case XPathNodeType.Root:
					flag = true;
					break;
				case XPathNodeType.Element:
					this.WriteStartElement(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
					if (navigator.MoveToFirstAttribute())
					{
						do
						{
							IXmlSchemaInfo schemaInfo = navigator.SchemaInfo;
							if (defattr || schemaInfo == null || !schemaInfo.IsDefault)
							{
								this.WriteStartAttribute(navigator.Prefix, navigator.LocalName, navigator.NamespaceURI);
								this.WriteString(navigator.Value);
								this.WriteEndAttribute();
							}
						}
						while (navigator.MoveToNextAttribute());
						navigator.MoveToParent();
					}
					if (navigator.MoveToFirstNamespace(XPathNamespaceScope.Local))
					{
						this.WriteLocalNamespaces(navigator);
						navigator.MoveToParent();
					}
					flag = true;
					break;
				case XPathNodeType.Text:
					this.WriteString(navigator.Value);
					break;
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
					this.WriteWhitespace(navigator.Value);
					break;
				case XPathNodeType.ProcessingInstruction:
					this.WriteProcessingInstruction(navigator.LocalName, navigator.Value);
					break;
				case XPathNodeType.Comment:
					this.WriteComment(navigator.Value);
					break;
				}
				if (flag)
				{
					if (navigator.MoveToFirstChild())
					{
						num++;
						continue;
					}
					if (navigator.NodeType == XPathNodeType.Element)
					{
						if (navigator.IsEmptyElement)
						{
							this.WriteEndElement();
						}
						else
						{
							this.WriteFullEndElement();
						}
					}
				}
				while (num != 0)
				{
					if (navigator.MoveToNext())
					{
						goto IL_18;
					}
					num--;
					navigator.MoveToParent();
					if (navigator.NodeType == XPathNodeType.Element)
					{
						this.WriteFullEndElement();
					}
				}
				break;
			}
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00040747 File Offset: 0x0003E947
		[__DynamicallyInvokable]
		public void WriteElementString(string localName, string value)
		{
			this.WriteElementString(localName, null, value);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00040752 File Offset: 0x0003E952
		[__DynamicallyInvokable]
		public void WriteElementString(string localName, string ns, string value)
		{
			this.WriteStartElement(localName, ns);
			if (value != null && value.Length != 0)
			{
				this.WriteString(value);
			}
			this.WriteEndElement();
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00040774 File Offset: 0x0003E974
		[__DynamicallyInvokable]
		public void WriteElementString(string prefix, string localName, string ns, string value)
		{
			this.WriteStartElement(prefix, localName, ns);
			if (value != null && value.Length != 0)
			{
				this.WriteString(value);
			}
			this.WriteEndElement();
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0004079A File Offset: 0x0003E99A
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000407A3 File Offset: 0x0003E9A3
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.WriteState != WriteState.Closed)
			{
				this.Close();
			}
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000407B8 File Offset: 0x0003E9B8
		private void WriteLocalNamespaces(XPathNavigator nsNav)
		{
			string localName = nsNav.LocalName;
			string value = nsNav.Value;
			if (nsNav.MoveToNextNamespace(XPathNamespaceScope.Local))
			{
				this.WriteLocalNamespaces(nsNav);
			}
			if (localName.Length == 0)
			{
				this.WriteAttributeString(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/", value);
				return;
			}
			this.WriteAttributeString("xmlns", localName, "http://www.w3.org/2000/xmlns/", value);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00040814 File Offset: 0x0003EA14
		public static XmlWriter Create(string outputFileName)
		{
			return XmlWriter.Create(outputFileName, null);
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0004081D File Offset: 0x0003EA1D
		public static XmlWriter Create(string outputFileName, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			return settings.CreateWriter(outputFileName);
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00040830 File Offset: 0x0003EA30
		[__DynamicallyInvokable]
		public static XmlWriter Create(Stream output)
		{
			return XmlWriter.Create(output, null);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00040839 File Offset: 0x0003EA39
		[__DynamicallyInvokable]
		public static XmlWriter Create(Stream output, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			return settings.CreateWriter(output);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0004084C File Offset: 0x0003EA4C
		[__DynamicallyInvokable]
		public static XmlWriter Create(TextWriter output)
		{
			return XmlWriter.Create(output, null);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00040855 File Offset: 0x0003EA55
		[__DynamicallyInvokable]
		public static XmlWriter Create(TextWriter output, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			return settings.CreateWriter(output);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00040868 File Offset: 0x0003EA68
		[__DynamicallyInvokable]
		public static XmlWriter Create(StringBuilder output)
		{
			return XmlWriter.Create(output, null);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00040871 File Offset: 0x0003EA71
		[__DynamicallyInvokable]
		public static XmlWriter Create(StringBuilder output, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			return settings.CreateWriter(new StringWriter(output, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0004089C File Offset: 0x0003EA9C
		[__DynamicallyInvokable]
		public static XmlWriter Create(XmlWriter output)
		{
			return XmlWriter.Create(output, null);
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x000408A5 File Offset: 0x0003EAA5
		[__DynamicallyInvokable]
		public static XmlWriter Create(XmlWriter output, XmlWriterSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlWriterSettings();
			}
			return settings.CreateWriter(output);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x000408B8 File Offset: 0x0003EAB8
		[__DynamicallyInvokable]
		public virtual Task WriteStartDocumentAsync()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x000408BF File Offset: 0x0003EABF
		[__DynamicallyInvokable]
		public virtual Task WriteStartDocumentAsync(bool standalone)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x000408C6 File Offset: 0x0003EAC6
		[__DynamicallyInvokable]
		public virtual Task WriteEndDocumentAsync()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x000408CD File Offset: 0x0003EACD
		[__DynamicallyInvokable]
		public virtual Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x000408D4 File Offset: 0x0003EAD4
		[__DynamicallyInvokable]
		public virtual Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x000408DB File Offset: 0x0003EADB
		[__DynamicallyInvokable]
		public virtual Task WriteEndElementAsync()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x000408E2 File Offset: 0x0003EAE2
		[__DynamicallyInvokable]
		public virtual Task WriteFullEndElementAsync()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x000408EC File Offset: 0x0003EAEC
		[__DynamicallyInvokable]
		public Task WriteAttributeStringAsync(string prefix, string localName, string ns, string value)
		{
			Task task = this.WriteStartAttributeAsync(prefix, localName, ns);
			if (task.IsSuccess())
			{
				return this.WriteStringAsync(value).CallTaskFuncWhenFinish(new Func<Task>(this.WriteEndAttributeAsync));
			}
			return this.WriteAttributeStringAsyncHelper(task, value);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00040930 File Offset: 0x0003EB30
		private Task WriteAttributeStringAsyncHelper(Task task, string value)
		{
			XmlWriter.<WriteAttributeStringAsyncHelper>d__82 <WriteAttributeStringAsyncHelper>d__;
			<WriteAttributeStringAsyncHelper>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAttributeStringAsyncHelper>d__.<>4__this = this;
			<WriteAttributeStringAsyncHelper>d__.task = task;
			<WriteAttributeStringAsyncHelper>d__.value = value;
			<WriteAttributeStringAsyncHelper>d__.<>1__state = -1;
			<WriteAttributeStringAsyncHelper>d__.<>t__builder.Start<XmlWriter.<WriteAttributeStringAsyncHelper>d__82>(ref <WriteAttributeStringAsyncHelper>d__);
			return <WriteAttributeStringAsyncHelper>d__.<>t__builder.Task;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00040983 File Offset: 0x0003EB83
		[__DynamicallyInvokable]
		protected internal virtual Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0004098A File Offset: 0x0003EB8A
		[__DynamicallyInvokable]
		protected internal virtual Task WriteEndAttributeAsync()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00040991 File Offset: 0x0003EB91
		[__DynamicallyInvokable]
		public virtual Task WriteCDataAsync(string text)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00040998 File Offset: 0x0003EB98
		[__DynamicallyInvokable]
		public virtual Task WriteCommentAsync(string text)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0004099F File Offset: 0x0003EB9F
		[__DynamicallyInvokable]
		public virtual Task WriteProcessingInstructionAsync(string name, string text)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x000409A6 File Offset: 0x0003EBA6
		[__DynamicallyInvokable]
		public virtual Task WriteEntityRefAsync(string name)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x000409AD File Offset: 0x0003EBAD
		[__DynamicallyInvokable]
		public virtual Task WriteCharEntityAsync(char ch)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x000409B4 File Offset: 0x0003EBB4
		[__DynamicallyInvokable]
		public virtual Task WriteWhitespaceAsync(string ws)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x000409BB File Offset: 0x0003EBBB
		[__DynamicallyInvokable]
		public virtual Task WriteStringAsync(string text)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x000409C2 File Offset: 0x0003EBC2
		[__DynamicallyInvokable]
		public virtual Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x000409C9 File Offset: 0x0003EBC9
		[__DynamicallyInvokable]
		public virtual Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x000409D0 File Offset: 0x0003EBD0
		[__DynamicallyInvokable]
		public virtual Task WriteRawAsync(char[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x000409D7 File Offset: 0x0003EBD7
		[__DynamicallyInvokable]
		public virtual Task WriteRawAsync(string data)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x000409DE File Offset: 0x0003EBDE
		[__DynamicallyInvokable]
		public virtual Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x000409E5 File Offset: 0x0003EBE5
		[__DynamicallyInvokable]
		public virtual Task WriteBinHexAsync(byte[] buffer, int index, int count)
		{
			return BinHexEncoder.EncodeAsync(buffer, index, count, this);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x000409F0 File Offset: 0x0003EBF0
		[__DynamicallyInvokable]
		public virtual Task FlushAsync()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x000409F7 File Offset: 0x0003EBF7
		[__DynamicallyInvokable]
		public virtual Task WriteNmTokenAsync(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Xml_EmptyName"));
			}
			return this.WriteStringAsync(XmlConvert.VerifyNMTOKEN(name, ExceptionType.ArgumentException));
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x00040A21 File Offset: 0x0003EC21
		[__DynamicallyInvokable]
		public virtual Task WriteNameAsync(string name)
		{
			return this.WriteStringAsync(XmlConvert.VerifyQName(name, ExceptionType.ArgumentException));
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x00040A30 File Offset: 0x0003EC30
		[__DynamicallyInvokable]
		public virtual Task WriteQualifiedNameAsync(string localName, string ns)
		{
			XmlWriter.<WriteQualifiedNameAsync>d__101 <WriteQualifiedNameAsync>d__;
			<WriteQualifiedNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteQualifiedNameAsync>d__.<>4__this = this;
			<WriteQualifiedNameAsync>d__.localName = localName;
			<WriteQualifiedNameAsync>d__.ns = ns;
			<WriteQualifiedNameAsync>d__.<>1__state = -1;
			<WriteQualifiedNameAsync>d__.<>t__builder.Start<XmlWriter.<WriteQualifiedNameAsync>d__101>(ref <WriteQualifiedNameAsync>d__);
			return <WriteQualifiedNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00040A84 File Offset: 0x0003EC84
		[__DynamicallyInvokable]
		public virtual Task WriteAttributesAsync(XmlReader reader, bool defattr)
		{
			XmlWriter.<WriteAttributesAsync>d__102 <WriteAttributesAsync>d__;
			<WriteAttributesAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAttributesAsync>d__.<>4__this = this;
			<WriteAttributesAsync>d__.reader = reader;
			<WriteAttributesAsync>d__.defattr = defattr;
			<WriteAttributesAsync>d__.<>1__state = -1;
			<WriteAttributesAsync>d__.<>t__builder.Start<XmlWriter.<WriteAttributesAsync>d__102>(ref <WriteAttributesAsync>d__);
			return <WriteAttributesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00040AD7 File Offset: 0x0003ECD7
		[__DynamicallyInvokable]
		public virtual Task WriteNodeAsync(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.Settings != null && reader.Settings.Async)
			{
				return this.WriteNodeAsync_CallAsyncReader(reader, defattr);
			}
			return this.WriteNodeAsync_CallSyncReader(reader, defattr);
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x00040B10 File Offset: 0x0003ED10
		internal Task WriteNodeAsync_CallSyncReader(XmlReader reader, bool defattr)
		{
			XmlWriter.<WriteNodeAsync_CallSyncReader>d__104 <WriteNodeAsync_CallSyncReader>d__;
			<WriteNodeAsync_CallSyncReader>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteNodeAsync_CallSyncReader>d__.<>4__this = this;
			<WriteNodeAsync_CallSyncReader>d__.reader = reader;
			<WriteNodeAsync_CallSyncReader>d__.defattr = defattr;
			<WriteNodeAsync_CallSyncReader>d__.<>1__state = -1;
			<WriteNodeAsync_CallSyncReader>d__.<>t__builder.Start<XmlWriter.<WriteNodeAsync_CallSyncReader>d__104>(ref <WriteNodeAsync_CallSyncReader>d__);
			return <WriteNodeAsync_CallSyncReader>d__.<>t__builder.Task;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x00040B64 File Offset: 0x0003ED64
		internal Task WriteNodeAsync_CallAsyncReader(XmlReader reader, bool defattr)
		{
			XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105 <WriteNodeAsync_CallAsyncReader>d__;
			<WriteNodeAsync_CallAsyncReader>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteNodeAsync_CallAsyncReader>d__.<>4__this = this;
			<WriteNodeAsync_CallAsyncReader>d__.reader = reader;
			<WriteNodeAsync_CallAsyncReader>d__.defattr = defattr;
			<WriteNodeAsync_CallAsyncReader>d__.<>1__state = -1;
			<WriteNodeAsync_CallAsyncReader>d__.<>t__builder.Start<XmlWriter.<WriteNodeAsync_CallAsyncReader>d__105>(ref <WriteNodeAsync_CallAsyncReader>d__);
			return <WriteNodeAsync_CallAsyncReader>d__.<>t__builder.Task;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x00040BB8 File Offset: 0x0003EDB8
		public virtual Task WriteNodeAsync(XPathNavigator navigator, bool defattr)
		{
			XmlWriter.<WriteNodeAsync>d__106 <WriteNodeAsync>d__;
			<WriteNodeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteNodeAsync>d__.<>4__this = this;
			<WriteNodeAsync>d__.navigator = navigator;
			<WriteNodeAsync>d__.defattr = defattr;
			<WriteNodeAsync>d__.<>1__state = -1;
			<WriteNodeAsync>d__.<>t__builder.Start<XmlWriter.<WriteNodeAsync>d__106>(ref <WriteNodeAsync>d__);
			return <WriteNodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00040C0C File Offset: 0x0003EE0C
		[__DynamicallyInvokable]
		public Task WriteElementStringAsync(string prefix, string localName, string ns, string value)
		{
			XmlWriter.<WriteElementStringAsync>d__107 <WriteElementStringAsync>d__;
			<WriteElementStringAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteElementStringAsync>d__.<>4__this = this;
			<WriteElementStringAsync>d__.prefix = prefix;
			<WriteElementStringAsync>d__.localName = localName;
			<WriteElementStringAsync>d__.ns = ns;
			<WriteElementStringAsync>d__.value = value;
			<WriteElementStringAsync>d__.<>1__state = -1;
			<WriteElementStringAsync>d__.<>t__builder.Start<XmlWriter.<WriteElementStringAsync>d__107>(ref <WriteElementStringAsync>d__);
			return <WriteElementStringAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00040C70 File Offset: 0x0003EE70
		private Task WriteLocalNamespacesAsync(XPathNavigator nsNav)
		{
			XmlWriter.<WriteLocalNamespacesAsync>d__108 <WriteLocalNamespacesAsync>d__;
			<WriteLocalNamespacesAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteLocalNamespacesAsync>d__.<>4__this = this;
			<WriteLocalNamespacesAsync>d__.nsNav = nsNav;
			<WriteLocalNamespacesAsync>d__.<>1__state = -1;
			<WriteLocalNamespacesAsync>d__.<>t__builder.Start<XmlWriter.<WriteLocalNamespacesAsync>d__108>(ref <WriteLocalNamespacesAsync>d__);
			return <WriteLocalNamespacesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x00040CBB File Offset: 0x0003EEBB
		[__DynamicallyInvokable]
		protected XmlWriter()
		{
		}

		// Token: 0x04000449 RID: 1097
		private char[] writeNodeBuffer;

		// Token: 0x0400044A RID: 1098
		private const int WriteNodeBufferSize = 1024;
	}
}
