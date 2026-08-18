using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel
{
	// Token: 0x0200011F RID: 287
	[__DynamicallyInvokable]
	public class EndpointAddress
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x0001E854 File Offset: 0x0001CA54
		private EndpointAddress(AddressingVersion version, Uri uri, EndpointIdentity identity, AddressHeaderCollection headers, XmlBuffer buffer, int metadataSection, int extensionSection, int pspSection)
		{
			this.Init(version, uri, identity, headers, buffer, metadataSection, extensionSection, pspSection);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001E87C File Offset: 0x0001CA7C
		[__DynamicallyInvokable]
		public EndpointAddress(string uri)
		{
			if (uri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("uri");
			}
			Uri uri2 = new Uri(uri);
			this.Init(uri2, null, null, null, -1, -1, -1);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001E8B6 File Offset: 0x0001CAB6
		[__DynamicallyInvokable]
		public EndpointAddress(Uri uri, params AddressHeader[] addressHeaders) : this(uri, null, addressHeaders)
		{
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001E8C1 File Offset: 0x0001CAC1
		[__DynamicallyInvokable]
		public EndpointAddress(Uri uri, EndpointIdentity identity, params AddressHeader[] addressHeaders)
		{
			if (uri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("uri");
			}
			this.Init(uri, identity, addressHeaders);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001E8EB File Offset: 0x0001CAEB
		public EndpointAddress(Uri uri, EndpointIdentity identity, AddressHeaderCollection headers)
		{
			if (uri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("uri");
			}
			this.Init(uri, identity, headers, null, -1, -1, -1);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001E91C File Offset: 0x0001CB1C
		internal EndpointAddress(Uri newUri, EndpointAddress oldEndpointAddress)
		{
			this.Init(oldEndpointAddress.addressingVersion, newUri, oldEndpointAddress.identity, oldEndpointAddress.headers, oldEndpointAddress.buffer, oldEndpointAddress.metadataSection, oldEndpointAddress.extensionSection, oldEndpointAddress.pspSection);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001E960 File Offset: 0x0001CB60
		internal EndpointAddress(Uri uri, EndpointIdentity identity, AddressHeaderCollection headers, XmlDictionaryReader metadataReader, XmlDictionaryReader extensionReader, XmlDictionaryReader pspReader)
		{
			if (uri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("uri");
			}
			XmlBuffer xmlBuffer = null;
			this.PossiblyPopulateBuffer(metadataReader, ref xmlBuffer, out this.metadataSection);
			EndpointIdentity endpointIdentity;
			int num;
			xmlBuffer = EndpointAddress.ReadExtensions(extensionReader, null, xmlBuffer, out endpointIdentity, out num);
			if (identity != null && endpointIdentity != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MultipleIdentities"), "extensionReader"));
			}
			this.PossiblyPopulateBuffer(pspReader, ref xmlBuffer, out this.pspSection);
			if (xmlBuffer != null)
			{
				xmlBuffer.Close();
			}
			this.Init(uri, identity ?? endpointIdentity, headers, xmlBuffer, this.metadataSection, num, this.pspSection);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001EA06 File Offset: 0x0001CC06
		public EndpointAddress(Uri uri, EndpointIdentity identity, AddressHeaderCollection headers, XmlDictionaryReader metadataReader, XmlDictionaryReader extensionReader) : this(uri, identity, headers, metadataReader, extensionReader, null)
		{
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001EA16 File Offset: 0x0001CC16
		private void Init(Uri uri, EndpointIdentity identity, AddressHeader[] headers)
		{
			if (headers == null || headers.Length == 0)
			{
				this.Init(uri, identity, null, null, -1, -1, -1);
				return;
			}
			this.Init(uri, identity, new AddressHeaderCollection(headers), null, -1, -1, -1);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001EA40 File Offset: 0x0001CC40
		private void Init(Uri uri, EndpointIdentity identity, AddressHeaderCollection headers, XmlBuffer buffer, int metadataSection, int extensionSection, int pspSection)
		{
			this.Init(null, uri, identity, headers, buffer, metadataSection, extensionSection, pspSection);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001EA60 File Offset: 0x0001CC60
		private void Init(AddressingVersion version, Uri uri, EndpointIdentity identity, AddressHeaderCollection headers, XmlBuffer buffer, int metadataSection, int extensionSection, int pspSection)
		{
			if (!uri.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("uri", SR.GetString("UriMustBeAbsolute"));
			}
			this.addressingVersion = version;
			this.uri = uri;
			this.identity = identity;
			this.headers = headers;
			this.buffer = buffer;
			this.metadataSection = metadataSection;
			this.extensionSection = extensionSection;
			this.pspSection = pspSection;
			if (version != null)
			{
				this.isAnonymous = (uri == version.AnonymousUri);
				this.isNone = (uri == version.NoneUri);
			}
			else
			{
				this.isAnonymous = (uri == EndpointAddress.AnonymousUri || uri == EndpointAddress.AnonymousUri);
				this.isNone = (uri == EndpointAddress.NoneUri || uri == EndpointAddress.NoneUri);
			}
			if (this.isAnonymous)
			{
				this.uri = EndpointAddress.AnonymousUri;
			}
			if (this.isNone)
			{
				this.uri = EndpointAddress.NoneUri;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0001EB53 File Offset: 0x0001CD53
		internal static EndpointAddress AnonymousAddress
		{
			get
			{
				if (EndpointAddress.anonymousAddress == null)
				{
					EndpointAddress.anonymousAddress = new EndpointAddress(EndpointAddress.AnonymousUri, new AddressHeader[0]);
				}
				return EndpointAddress.anonymousAddress;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0001EB7C File Offset: 0x0001CD7C
		[__DynamicallyInvokable]
		public static Uri AnonymousUri
		{
			[__DynamicallyInvokable]
			get
			{
				if (EndpointAddress.anonymousUri == null)
				{
					EndpointAddress.anonymousUri = new Uri("http://schemas.microsoft.com/2005/12/ServiceModel/Addressing/Anonymous");
				}
				return EndpointAddress.anonymousUri;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0001EB9F File Offset: 0x0001CD9F
		[__DynamicallyInvokable]
		public static Uri NoneUri
		{
			[__DynamicallyInvokable]
			get
			{
				if (EndpointAddress.noneUri == null)
				{
					EndpointAddress.noneUri = new Uri("http://schemas.microsoft.com/2005/12/ServiceModel/Addressing/None");
				}
				return EndpointAddress.noneUri;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0001EBC2 File Offset: 0x0001CDC2
		internal XmlBuffer Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0001EBCA File Offset: 0x0001CDCA
		[__DynamicallyInvokable]
		public AddressHeaderCollection Headers
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.headers == null)
				{
					this.headers = new AddressHeaderCollection();
				}
				return this.headers;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001EBE5 File Offset: 0x0001CDE5
		[__DynamicallyInvokable]
		public EndpointIdentity Identity
		{
			[__DynamicallyInvokable]
			get
			{
				return this.identity;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0001EBED File Offset: 0x0001CDED
		[__DynamicallyInvokable]
		public bool IsAnonymous
		{
			[__DynamicallyInvokable]
			get
			{
				return this.isAnonymous;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001EBF5 File Offset: 0x0001CDF5
		[__DynamicallyInvokable]
		public bool IsNone
		{
			[__DynamicallyInvokable]
			get
			{
				return this.isNone;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0001EBFD File Offset: 0x0001CDFD
		[TypeConverter(typeof(UriTypeConverter))]
		[__DynamicallyInvokable]
		public Uri Uri
		{
			[__DynamicallyInvokable]
			get
			{
				return this.uri;
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001EC08 File Offset: 0x0001CE08
		[__DynamicallyInvokable]
		public void ApplyTo(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			Uri to = this.Uri;
			if (this.IsAnonymous)
			{
				if (message.Version.Addressing == AddressingVersion.WSAddressing10)
				{
					message.Headers.To = null;
				}
				else
				{
					if (message.Version.Addressing != AddressingVersion.WSAddressingAugust2004)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("AddressingVersionNotSupported", new object[]
						{
							message.Version.Addressing
						})));
					}
					message.Headers.To = message.Version.Addressing.AnonymousUri;
				}
			}
			else if (this.IsNone)
			{
				message.Headers.To = message.Version.Addressing.NoneUri;
			}
			else
			{
				message.Headers.To = to;
			}
			message.Properties.Via = message.Headers.To;
			if (this.headers != null)
			{
				this.headers.AddHeadersTo(message);
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001ED15 File Offset: 0x0001CF15
		internal static bool UriEquals(Uri u1, Uri u2, bool ignoreCase, bool includeHostInComparison)
		{
			return EndpointAddress.UriEquals(u1, u2, ignoreCase, includeHostInComparison, true);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001ED24 File Offset: 0x0001CF24
		internal static bool UriEquals(Uri u1, Uri u2, bool ignoreCase, bool includeHostInComparison, bool includePortInComparison)
		{
			if (u1.Equals(u2))
			{
				return true;
			}
			if (u1.Scheme != u2.Scheme)
			{
				return false;
			}
			if (includePortInComparison && u1.Port != u2.Port)
			{
				return false;
			}
			if (includeHostInComparison && string.Compare(u1.Host, u2.Host, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return false;
			}
			if (string.Compare(u1.AbsolutePath, u2.AbsolutePath, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0)
			{
				return true;
			}
			string components = u1.GetComponents(UriComponents.Path, UriFormat.Unescaped);
			string components2 = u2.GetComponents(UriComponents.Path, UriFormat.Unescaped);
			int num = (components.Length > 0 && components[components.Length - 1] == '/') ? (components.Length - 1) : components.Length;
			int num2 = (components2.Length > 0 && components2[components2.Length - 1] == '/') ? (components2.Length - 1) : components2.Length;
			return num2 == num && string.Compare(components, 0, components2, 0, num, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001EE20 File Offset: 0x0001D020
		internal static int UriGetHashCode(Uri uri, bool includeHostInComparison)
		{
			return EndpointAddress.UriGetHashCode(uri, includeHostInComparison, true);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001EE2C File Offset: 0x0001D02C
		internal static int UriGetHashCode(Uri uri, bool includeHostInComparison, bool includePortInComparison)
		{
			UriComponents uriComponents = UriComponents.Scheme | UriComponents.Path;
			if (includePortInComparison)
			{
				uriComponents |= UriComponents.Port;
			}
			if (includeHostInComparison)
			{
				uriComponents |= UriComponents.Host;
			}
			string text = uri.GetComponents(uriComponents, UriFormat.Unescaped);
			if (text.Length > 0 && text[text.Length - 1] != '/')
			{
				text += "/";
			}
			return StringComparer.OrdinalIgnoreCase.GetHashCode(text);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001EE88 File Offset: 0x0001D088
		internal bool EndpointEquals(EndpointAddress endpointAddress)
		{
			if (endpointAddress == null)
			{
				return false;
			}
			if (this == endpointAddress)
			{
				return true;
			}
			Uri u = this.Uri;
			Uri u2 = endpointAddress.Uri;
			if (!EndpointAddress.UriEquals(u, u2, false, true))
			{
				return false;
			}
			if (this.Identity == null)
			{
				if (endpointAddress.Identity != null)
				{
					return false;
				}
			}
			else if (!this.Identity.Equals(endpointAddress.Identity))
			{
				return false;
			}
			return this.Headers.IsEquivalent(endpointAddress.Headers);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001EF00 File Offset: 0x0001D100
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			EndpointAddress endpointAddress = obj as EndpointAddress;
			return !(endpointAddress == null) && this.EndpointEquals(endpointAddress);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001EF31 File Offset: 0x0001D131
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return EndpointAddress.UriGetHashCode(this.uri, true);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001EF3F File Offset: 0x0001D13F
		internal XmlDictionaryReader GetReaderAtPsp()
		{
			return EndpointAddress.GetReaderAtSection(this.buffer, this.pspSection);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001EF52 File Offset: 0x0001D152
		public XmlDictionaryReader GetReaderAtMetadata()
		{
			return EndpointAddress.GetReaderAtSection(this.buffer, this.metadataSection);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001EF65 File Offset: 0x0001D165
		public XmlDictionaryReader GetReaderAtExtensions()
		{
			return EndpointAddress.GetReaderAtSection(this.buffer, this.extensionSection);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001EF78 File Offset: 0x0001D178
		private static XmlDictionaryReader GetReaderAtSection(XmlBuffer buffer, int section)
		{
			if (buffer == null || section < 0)
			{
				return null;
			}
			XmlDictionaryReader reader = buffer.GetReader(section);
			reader.MoveToContent();
			reader.Read();
			return reader;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001EFA8 File Offset: 0x0001D1A8
		private void PossiblyPopulateBuffer(XmlDictionaryReader reader, ref XmlBuffer buffer, out int section)
		{
			if (reader == null)
			{
				section = -1;
				return;
			}
			if (buffer == null)
			{
				buffer = new XmlBuffer(32767);
			}
			section = buffer.SectionCount;
			XmlDictionaryWriter xmlDictionaryWriter = buffer.OpenSection(reader.Quotas);
			xmlDictionaryWriter.WriteStartElement("Dummy", "http://Dummy");
			EndpointAddress.Copy(xmlDictionaryWriter, reader);
			buffer.CloseSection();
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001F004 File Offset: 0x0001D204
		public static EndpointAddress ReadFrom(XmlDictionaryReader reader)
		{
			AddressingVersion addressingVersion;
			return EndpointAddress.ReadFrom(reader, out addressingVersion);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001F01C File Offset: 0x0001D21C
		internal static EndpointAddress ReadFrom(XmlDictionaryReader reader, out AddressingVersion version)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			reader.ReadFullStartElement();
			reader.MoveToContent();
			if (reader.IsNamespaceUri(AddressingVersion.WSAddressing10.DictionaryNamespace))
			{
				version = AddressingVersion.WSAddressing10;
			}
			else if (reader.IsNamespaceUri(AddressingVersion.WSAddressingAugust2004.DictionaryNamespace))
			{
				version = AddressingVersion.WSAddressingAugust2004;
			}
			else
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("reader", SR.GetString("CannotDetectAddressingVersion"));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("reader", SR.GetString("AddressingVersionNotSupported", new object[]
				{
					reader.NamespaceURI
				}));
			}
			EndpointAddress result = EndpointAddress.ReadFromDriver(version, reader);
			reader.ReadEndElement();
			return result;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001F0DC File Offset: 0x0001D2DC
		public static EndpointAddress ReadFrom(XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString ns)
		{
			AddressingVersion addressingVersion;
			return EndpointAddress.ReadFrom(reader, localName, ns, out addressingVersion);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001F0F4 File Offset: 0x0001D2F4
		internal static EndpointAddress ReadFrom(XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString ns, out AddressingVersion version)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			reader.ReadFullStartElement(localName, ns);
			reader.MoveToContent();
			if (reader.IsNamespaceUri(AddressingVersion.WSAddressing10.DictionaryNamespace))
			{
				version = AddressingVersion.WSAddressing10;
			}
			else if (reader.IsNamespaceUri(AddressingVersion.WSAddressingAugust2004.DictionaryNamespace))
			{
				version = AddressingVersion.WSAddressingAugust2004;
			}
			else
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("reader", SR.GetString("CannotDetectAddressingVersion"));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("reader", SR.GetString("AddressingVersionNotSupported", new object[]
				{
					reader.NamespaceURI
				}));
			}
			EndpointAddress result = EndpointAddress.ReadFromDriver(version, reader);
			reader.ReadEndElement();
			return result;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001F1B5 File Offset: 0x0001D3B5
		public static EndpointAddress ReadFrom(AddressingVersion addressingVersion, XmlReader reader)
		{
			return EndpointAddress.ReadFrom(addressingVersion, XmlDictionaryReader.CreateDictionaryReader(reader));
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001F1C4 File Offset: 0x0001D3C4
		public static EndpointAddress ReadFrom(AddressingVersion addressingVersion, XmlReader reader, string localName, string ns)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("addressingVersion");
			}
			XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateDictionaryReader(reader);
			xmlDictionaryReader.ReadFullStartElement(localName, ns);
			EndpointAddress result = EndpointAddress.ReadFromDriver(addressingVersion, xmlDictionaryReader);
			reader.ReadEndElement();
			return result;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001F218 File Offset: 0x0001D418
		[__DynamicallyInvokable]
		public static EndpointAddress ReadFrom(AddressingVersion addressingVersion, XmlDictionaryReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("addressingVersion");
			}
			reader.ReadFullStartElement();
			EndpointAddress result = EndpointAddress.ReadFromDriver(addressingVersion, reader);
			reader.ReadEndElement();
			return result;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001F260 File Offset: 0x0001D460
		public static EndpointAddress ReadFrom(AddressingVersion addressingVersion, XmlDictionaryReader reader, XmlDictionaryString localName, XmlDictionaryString ns)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("addressingVersion");
			}
			reader.ReadFullStartElement(localName, ns);
			EndpointAddress result = EndpointAddress.ReadFromDriver(addressingVersion, reader);
			reader.ReadEndElement();
			return result;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001F2AC File Offset: 0x0001D4AC
		private static EndpointAddress ReadFromDriver(AddressingVersion addressingVersion, XmlDictionaryReader reader)
		{
			int num = -1;
			Uri uri;
			AddressHeaderCollection addressHeaderCollection;
			EndpointIdentity endpointIdentity;
			XmlBuffer xmlBuffer;
			int num2;
			int num3;
			bool flag;
			if (addressingVersion == AddressingVersion.WSAddressing10)
			{
				flag = EndpointAddress.ReadContentsFrom10(reader, out uri, out addressHeaderCollection, out endpointIdentity, out xmlBuffer, out num2, out num3);
			}
			else
			{
				if (addressingVersion != AddressingVersion.WSAddressingAugust2004)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("addressingVersion", SR.GetString("AddressingVersionNotSupported", new object[]
					{
						addressingVersion
					}));
				}
				flag = EndpointAddress.ReadContentsFrom200408(reader, out uri, out addressHeaderCollection, out endpointIdentity, out xmlBuffer, out num2, out num3, out num);
			}
			if (flag && addressHeaderCollection == null && endpointIdentity == null && xmlBuffer == null)
			{
				return EndpointAddress.AnonymousAddress;
			}
			return new EndpointAddress(addressingVersion, uri, endpointIdentity, addressHeaderCollection, xmlBuffer, num2, num3, num);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001F344 File Offset: 0x0001D544
		internal static XmlBuffer ReadExtensions(XmlDictionaryReader reader, AddressingVersion version, XmlBuffer buffer, out EndpointIdentity identity, out int section)
		{
			if (reader == null)
			{
				identity = null;
				section = -1;
				return buffer;
			}
			identity = null;
			XmlDictionaryWriter xmlDictionaryWriter = null;
			reader.MoveToContent();
			while (reader.IsStartElement())
			{
				if (reader.IsStartElement(XD.AddressingDictionary.Identity, XD.AddressingDictionary.IdentityExtensionNamespace))
				{
					if (identity != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(EndpointAddress.CreateXmlException(reader, SR.GetString("UnexpectedDuplicateElement", new object[]
						{
							XD.AddressingDictionary.Identity.Value,
							XD.AddressingDictionary.IdentityExtensionNamespace.Value
						})));
					}
					identity = EndpointIdentity.ReadIdentity(reader);
				}
				else
				{
					if (version != null && reader.NamespaceURI == version.Namespace)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(EndpointAddress.CreateXmlException(reader, SR.GetString("AddressingExtensionInBadNS", new object[]
						{
							reader.LocalName,
							reader.NamespaceURI
						})));
					}
					if (xmlDictionaryWriter == null)
					{
						if (buffer == null)
						{
							buffer = new XmlBuffer(32767);
						}
						xmlDictionaryWriter = buffer.OpenSection(reader.Quotas);
						xmlDictionaryWriter.WriteStartElement("Dummy", "http://Dummy");
					}
					xmlDictionaryWriter.WriteNode(reader, true);
				}
				reader.MoveToContent();
			}
			if (xmlDictionaryWriter != null)
			{
				xmlDictionaryWriter.WriteEndElement();
				buffer.CloseSection();
				section = buffer.SectionCount - 1;
			}
			else
			{
				section = -1;
			}
			return buffer;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001F494 File Offset: 0x0001D694
		private static bool ReadContentsFrom200408(XmlDictionaryReader reader, out Uri uri, out AddressHeaderCollection headers, out EndpointIdentity identity, out XmlBuffer buffer, out int metadataSection, out int extensionSection, out int pspSection)
		{
			buffer = null;
			headers = null;
			extensionSection = -1;
			metadataSection = -1;
			pspSection = -1;
			reader.MoveToContent();
			if (!reader.IsStartElement(XD.AddressingDictionary.Address, AddressingVersion.WSAddressingAugust2004.DictionaryNamespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(EndpointAddress.CreateXmlException(reader, SR.GetString("UnexpectedElementExpectingElement", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI,
					XD.AddressingDictionary.Address.Value,
					XD.Addressing200408Dictionary.Namespace.Value
				})));
			}
			string text = reader.ReadElementContentAsString();
			reader.MoveToContent();
			if (reader.IsStartElement(XD.AddressingDictionary.ReferenceProperties, AddressingVersion.WSAddressingAugust2004.DictionaryNamespace))
			{
				headers = AddressHeaderCollection.ReadServiceParameters(reader, true);
			}
			reader.MoveToContent();
			if (reader.IsStartElement(XD.AddressingDictionary.ReferenceParameters, AddressingVersion.WSAddressingAugust2004.DictionaryNamespace))
			{
				if (headers != null)
				{
					List<AddressHeader> list = new List<AddressHeader>();
					foreach (AddressHeader item in headers)
					{
						list.Add(item);
					}
					AddressHeaderCollection addressHeaderCollection = AddressHeaderCollection.ReadServiceParameters(reader);
					foreach (AddressHeader item2 in addressHeaderCollection)
					{
						list.Add(item2);
					}
					headers = new AddressHeaderCollection(list);
				}
				else
				{
					headers = AddressHeaderCollection.ReadServiceParameters(reader);
				}
			}
			XmlDictionaryWriter xmlDictionaryWriter = null;
			reader.MoveToContent();
			if (reader.IsStartElement(XD.AddressingDictionary.PortType, AddressingVersion.WSAddressingAugust2004.DictionaryNamespace))
			{
				if (xmlDictionaryWriter == null)
				{
					if (buffer == null)
					{
						buffer = new XmlBuffer(32767);
					}
					xmlDictionaryWriter = buffer.OpenSection(reader.Quotas);
					xmlDictionaryWriter.WriteStartElement("Dummy", "http://Dummy");
				}
				xmlDictionaryWriter.WriteNode(reader, true);
			}
			reader.MoveToContent();
			if (reader.IsStartElement(XD.AddressingDictionary.ServiceName, AddressingVersion.WSAddressingAugust2004.DictionaryNamespace))
			{
				if (xmlDictionaryWriter == null)
				{
					if (buffer == null)
					{
						buffer = new XmlBuffer(32767);
					}
					xmlDictionaryWriter = buffer.OpenSection(reader.Quotas);
					xmlDictionaryWriter.WriteStartElement("Dummy", "http://Dummy");
				}
				xmlDictionaryWriter.WriteNode(reader, true);
			}
			reader.MoveToContent();
			while (reader.IsNamespaceUri(XD.PolicyDictionary.Namespace))
			{
				if (xmlDictionaryWriter == null)
				{
					if (buffer == null)
					{
						buffer = new XmlBuffer(32767);
					}
					xmlDictionaryWriter = buffer.OpenSection(reader.Quotas);
					xmlDictionaryWriter.WriteStartElement("Dummy", "http://Dummy");
				}
				xmlDictionaryWriter.WriteNode(reader, true);
				reader.MoveToContent();
			}
			if (xmlDictionaryWriter != null)
			{
				xmlDictionaryWriter.WriteEndElement();
				buffer.CloseSection();
				pspSection = buffer.SectionCount - 1;
				xmlDictionaryWriter = null;
			}
			else
			{
				pspSection = -1;
			}
			if (reader.IsStartElement("Metadata", "http://schemas.xmlsoap.org/ws/2004/09/mex"))
			{
				if (xmlDictionaryWriter == null)
				{
					if (buffer == null)
					{
						buffer = new XmlBuffer(32767);
					}
					xmlDictionaryWriter = buffer.OpenSection(reader.Quotas);
					xmlDictionaryWriter.WriteStartElement("Dummy", "http://Dummy");
				}
				xmlDictionaryWriter.WriteNode(reader, true);
			}
			if (xmlDictionaryWriter != null)
			{
				xmlDictionaryWriter.WriteEndElement();
				buffer.CloseSection();
				metadataSection = buffer.SectionCount - 1;
			}
			else
			{
				metadataSection = -1;
			}
			reader.MoveToContent();
			buffer = EndpointAddress.ReadExtensions(reader, AddressingVersion.WSAddressingAugust2004, buffer, out identity, out extensionSection);
			if (buffer != null)
			{
				buffer.Close();
			}
			if (text == "http://schemas.xmlsoap.org/ws/2004/08/addressing/role/anonymous")
			{
				uri = AddressingVersion.WSAddressingAugust2004.AnonymousUri;
				if (headers == null && identity == null)
				{
					return true;
				}
			}
			else if (!Uri.TryCreate(text, UriKind.Absolute, out uri))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("InvalidUriValue", new object[]
				{
					text,
					XD.AddressingDictionary.Address.Value,
					AddressingVersion.WSAddressingAugust2004.Namespace
				})));
			}
			return false;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001F87C File Offset: 0x0001DA7C
		private static bool ReadContentsFrom10(XmlDictionaryReader reader, out Uri uri, out AddressHeaderCollection headers, out EndpointIdentity identity, out XmlBuffer buffer, out int metadataSection, out int extensionSection)
		{
			buffer = null;
			extensionSection = -1;
			metadataSection = -1;
			if (!reader.IsStartElement(XD.AddressingDictionary.Address, XD.Addressing10Dictionary.Namespace))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(EndpointAddress.CreateXmlException(reader, SR.GetString("UnexpectedElementExpectingElement", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI,
					XD.AddressingDictionary.Address.Value,
					XD.Addressing10Dictionary.Namespace.Value
				})));
			}
			string text = reader.ReadElementContentAsString();
			if (reader.IsStartElement(XD.AddressingDictionary.ReferenceParameters, XD.Addressing10Dictionary.Namespace))
			{
				headers = AddressHeaderCollection.ReadServiceParameters(reader);
			}
			else
			{
				headers = null;
			}
			if (reader.IsStartElement(XD.Addressing10Dictionary.Metadata, XD.Addressing10Dictionary.Namespace))
			{
				reader.ReadFullStartElement();
				buffer = new XmlBuffer(32767);
				metadataSection = 0;
				XmlDictionaryWriter xmlDictionaryWriter = buffer.OpenSection(reader.Quotas);
				xmlDictionaryWriter.WriteStartElement("Dummy", "http://Dummy");
				while (reader.NodeType != XmlNodeType.EndElement && !reader.EOF)
				{
					xmlDictionaryWriter.WriteNode(reader, true);
				}
				xmlDictionaryWriter.Flush();
				buffer.CloseSection();
				reader.ReadEndElement();
			}
			buffer = EndpointAddress.ReadExtensions(reader, AddressingVersion.WSAddressing10, buffer, out identity, out extensionSection);
			if (buffer != null)
			{
				buffer.Close();
			}
			if (text == "http://www.w3.org/2005/08/addressing/anonymous")
			{
				uri = AddressingVersion.WSAddressing10.AnonymousUri;
				if (headers == null && identity == null)
				{
					return true;
				}
			}
			else
			{
				if (text == "http://www.w3.org/2005/08/addressing/none")
				{
					uri = AddressingVersion.WSAddressing10.NoneUri;
					return false;
				}
				if (!Uri.TryCreate(text, UriKind.Absolute, out uri))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("InvalidUriValue", new object[]
					{
						text,
						XD.AddressingDictionary.Address.Value,
						XD.Addressing10Dictionary.Namespace.Value
					})));
				}
			}
			return false;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001FA6C File Offset: 0x0001DC6C
		private static XmlException CreateXmlException(XmlDictionaryReader reader, string message)
		{
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			if (xmlLineInfo != null)
			{
				return new XmlException(message, null, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
			}
			return new XmlException(message);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001FA9D File Offset: 0x0001DC9D
		private static bool Done(XmlDictionaryReader reader)
		{
			reader.MoveToContent();
			return reader.NodeType == XmlNodeType.EndElement || reader.EOF;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001FAB8 File Offset: 0x0001DCB8
		internal static void Copy(XmlDictionaryWriter writer, XmlDictionaryReader reader)
		{
			while (!EndpointAddress.Done(reader))
			{
				writer.WriteNode(reader, true);
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001FACC File Offset: 0x0001DCCC
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.uri.ToString();
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001FADC File Offset: 0x0001DCDC
		[__DynamicallyInvokable]
		public void WriteContentsTo(AddressingVersion addressingVersion, XmlDictionaryWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("addressingVersion");
			}
			if (addressingVersion == AddressingVersion.WSAddressing10)
			{
				this.WriteContentsTo10(writer);
				return;
			}
			if (addressingVersion == AddressingVersion.WSAddressingAugust2004)
			{
				this.WriteContentsTo200408(writer);
				return;
			}
			if (addressingVersion == AddressingVersion.None)
			{
				this.WriteContentsToNone(writer);
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("addressingVersion", SR.GetString("AddressingVersionNotSupported", new object[]
			{
				addressingVersion
			}));
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001FB62 File Offset: 0x0001DD62
		private void WriteContentsToNone(XmlDictionaryWriter writer)
		{
			writer.WriteString(this.Uri.AbsoluteUri);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001FB78 File Offset: 0x0001DD78
		private void WriteContentsTo200408(XmlDictionaryWriter writer)
		{
			writer.WriteStartElement(XD.AddressingDictionary.Address, XD.Addressing200408Dictionary.Namespace);
			if (this.isAnonymous)
			{
				writer.WriteString(XD.Addressing200408Dictionary.Anonymous);
			}
			else
			{
				if (this.isNone)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("addressingVersion", SR.GetString("SFxNone2004"));
				}
				writer.WriteString(this.Uri.AbsoluteUri);
			}
			writer.WriteEndElement();
			if (this.headers != null && this.headers.HasReferenceProperties)
			{
				writer.WriteStartElement(XD.AddressingDictionary.ReferenceProperties, XD.Addressing200408Dictionary.Namespace);
				this.headers.WriteReferencePropertyContentsTo(writer);
				writer.WriteEndElement();
			}
			if (this.headers != null && this.headers.HasNonReferenceProperties)
			{
				writer.WriteStartElement(XD.AddressingDictionary.ReferenceParameters, XD.Addressing200408Dictionary.Namespace);
				this.headers.WriteNonReferencePropertyContentsTo(writer);
				writer.WriteEndElement();
			}
			if (this.pspSection >= 0)
			{
				XmlDictionaryReader readerAtSection = EndpointAddress.GetReaderAtSection(this.buffer, this.pspSection);
				EndpointAddress.Copy(writer, readerAtSection);
			}
			if (this.metadataSection >= 0)
			{
				XmlDictionaryReader readerAtSection = EndpointAddress.GetReaderAtSection(this.buffer, this.metadataSection);
				EndpointAddress.Copy(writer, readerAtSection);
			}
			if (this.Identity != null)
			{
				this.Identity.WriteTo(writer);
			}
			if (this.extensionSection >= 0)
			{
				XmlDictionaryReader readerAtSection = EndpointAddress.GetReaderAtSection(this.buffer, this.extensionSection);
				while (readerAtSection.IsStartElement())
				{
					if (readerAtSection.NamespaceURI == AddressingVersion.WSAddressingAugust2004.Namespace)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(EndpointAddress.CreateXmlException(readerAtSection, SR.GetString("AddressingExtensionInBadNS", new object[]
						{
							readerAtSection.LocalName,
							readerAtSection.NamespaceURI
						})));
					}
					writer.WriteNode(readerAtSection, true);
				}
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001FD48 File Offset: 0x0001DF48
		private void WriteContentsTo10(XmlDictionaryWriter writer)
		{
			writer.WriteStartElement(XD.AddressingDictionary.Address, XD.Addressing10Dictionary.Namespace);
			if (this.isAnonymous)
			{
				writer.WriteString(XD.Addressing10Dictionary.Anonymous);
			}
			else if (this.isNone)
			{
				writer.WriteString(XD.Addressing10Dictionary.NoneAddress);
			}
			else
			{
				writer.WriteString(this.Uri.AbsoluteUri);
			}
			writer.WriteEndElement();
			if (this.headers != null && this.headers.Count > 0)
			{
				writer.WriteStartElement(XD.AddressingDictionary.ReferenceParameters, XD.Addressing10Dictionary.Namespace);
				this.headers.WriteContentsTo(writer);
				writer.WriteEndElement();
			}
			if (this.metadataSection >= 0)
			{
				XmlDictionaryReader readerAtSection = EndpointAddress.GetReaderAtSection(this.buffer, this.metadataSection);
				writer.WriteStartElement(XD.Addressing10Dictionary.Metadata, XD.Addressing10Dictionary.Namespace);
				EndpointAddress.Copy(writer, readerAtSection);
				writer.WriteEndElement();
			}
			if (this.Identity != null)
			{
				this.Identity.WriteTo(writer);
			}
			if (this.extensionSection >= 0)
			{
				XmlDictionaryReader readerAtSection2 = EndpointAddress.GetReaderAtSection(this.buffer, this.extensionSection);
				while (readerAtSection2.IsStartElement())
				{
					if (readerAtSection2.NamespaceURI == AddressingVersion.WSAddressing10.Namespace)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(EndpointAddress.CreateXmlException(readerAtSection2, SR.GetString("AddressingExtensionInBadNS", new object[]
						{
							readerAtSection2.LocalName,
							readerAtSection2.NamespaceURI
						})));
					}
					writer.WriteNode(readerAtSection2, true);
				}
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001FECC File Offset: 0x0001E0CC
		public void WriteContentsTo(AddressingVersion addressingVersion, XmlWriter writer)
		{
			XmlDictionaryWriter writer2 = XmlDictionaryWriter.CreateDictionaryWriter(writer);
			this.WriteContentsTo(addressingVersion, writer2);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001FEE8 File Offset: 0x0001E0E8
		public void WriteTo(AddressingVersion addressingVersion, XmlDictionaryWriter writer)
		{
			this.WriteTo(addressingVersion, writer, XD.AddressingDictionary.EndpointReference, addressingVersion.DictionaryNamespace);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001FF04 File Offset: 0x0001E104
		public void WriteTo(AddressingVersion addressingVersion, XmlDictionaryWriter writer, XmlDictionaryString localName, XmlDictionaryString ns)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("addressingVersion");
			}
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ns");
			}
			writer.WriteStartElement(localName, ns);
			this.WriteContentsTo(addressingVersion, writer);
			writer.WriteEndElement();
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001FF78 File Offset: 0x0001E178
		public void WriteTo(AddressingVersion addressingVersion, XmlWriter writer)
		{
			XmlDictionaryString xmlDictionaryString = addressingVersion.DictionaryNamespace;
			if (xmlDictionaryString == null)
			{
				xmlDictionaryString = XD.AddressingDictionary.Empty;
			}
			this.WriteTo(addressingVersion, XmlDictionaryWriter.CreateDictionaryWriter(writer), XD.AddressingDictionary.EndpointReference, xmlDictionaryString);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001FFB4 File Offset: 0x0001E1B4
		public void WriteTo(AddressingVersion addressingVersion, XmlWriter writer, string localName, string ns)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (addressingVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("addressingVersion");
			}
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ns");
			}
			writer.WriteStartElement(localName, ns);
			this.WriteContentsTo(addressingVersion, writer);
			writer.WriteEndElement();
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00020025 File Offset: 0x0001E225
		[__DynamicallyInvokable]
		public static bool operator ==(EndpointAddress address1, EndpointAddress address2)
		{
			if (address2 == null)
			{
				return address1 == null;
			}
			return address2.Equals(address1);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00020036 File Offset: 0x0001E236
		[__DynamicallyInvokable]
		public static bool operator !=(EndpointAddress address1, EndpointAddress address2)
		{
			if (address2 == null)
			{
				return address1 != null;
			}
			return !address2.Equals(address1);
		}

		// Token: 0x04000AC5 RID: 2757
		private static Uri anonymousUri;

		// Token: 0x04000AC6 RID: 2758
		private static Uri noneUri;

		// Token: 0x04000AC7 RID: 2759
		private static EndpointAddress anonymousAddress;

		// Token: 0x04000AC8 RID: 2760
		private AddressingVersion addressingVersion;

		// Token: 0x04000AC9 RID: 2761
		private AddressHeaderCollection headers;

		// Token: 0x04000ACA RID: 2762
		private EndpointIdentity identity;

		// Token: 0x04000ACB RID: 2763
		private Uri uri;

		// Token: 0x04000ACC RID: 2764
		private XmlBuffer buffer;

		// Token: 0x04000ACD RID: 2765
		private int extensionSection;

		// Token: 0x04000ACE RID: 2766
		private int metadataSection;

		// Token: 0x04000ACF RID: 2767
		private int pspSection;

		// Token: 0x04000AD0 RID: 2768
		private bool isAnonymous;

		// Token: 0x04000AD1 RID: 2769
		private bool isNone;

		// Token: 0x04000AD2 RID: 2770
		internal const string DummyName = "Dummy";

		// Token: 0x04000AD3 RID: 2771
		internal const string DummyNamespace = "http://Dummy";
	}
}
