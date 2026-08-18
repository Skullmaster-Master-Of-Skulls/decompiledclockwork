using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Discovery.Version11;
using System.ServiceModel.Discovery.VersionApril2005;
using System.ServiceModel.Discovery.VersionCD1;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200002A RID: 42
	public sealed class DiscoveryVersion
	{
		// Token: 0x06000242 RID: 578 RVA: 0x00007110 File Offset: 0x00005310
		private DiscoveryVersion(string name, string discoveryNamespace, IDiscoveryVersionImplementation discoveryVersionImplementation)
		{
			this.name = name;
			this.discoveryNamespace = discoveryNamespace;
			this.discoveryVersionImplementation = discoveryVersionImplementation;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00007130 File Offset: 0x00005330
		public static DiscoveryVersion WSDiscoveryApril2005
		{
			get
			{
				if (DiscoveryVersion.wsDiscoveryApril2005 == null)
				{
					object obj = DiscoveryVersion.staticLock;
					lock (obj)
					{
						if (DiscoveryVersion.wsDiscoveryApril2005 == null)
						{
							DiscoveryVersion.wsDiscoveryApril2005 = new DiscoveryVersion("WSDiscoveryApril2005", "http://schemas.xmlsoap.org/ws/2005/04/discovery", new DiscoveryVersionApril2005Implementation());
						}
					}
				}
				return DiscoveryVersion.wsDiscoveryApril2005;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00007198 File Offset: 0x00005398
		public static DiscoveryVersion WSDiscoveryCD1
		{
			get
			{
				if (DiscoveryVersion.wsDiscoveryCD1 == null)
				{
					object obj = DiscoveryVersion.staticLock;
					lock (obj)
					{
						if (DiscoveryVersion.wsDiscoveryCD1 == null)
						{
							DiscoveryVersion.wsDiscoveryCD1 = new DiscoveryVersion("WSDiscoveryCD1", "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09", new DiscoveryVersionCD1Implementation());
						}
					}
				}
				return DiscoveryVersion.wsDiscoveryCD1;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00007200 File Offset: 0x00005400
		public static DiscoveryVersion WSDiscovery11
		{
			get
			{
				if (DiscoveryVersion.wsDiscovery11 == null)
				{
					object obj = DiscoveryVersion.staticLock;
					lock (obj)
					{
						if (DiscoveryVersion.wsDiscovery11 == null)
						{
							DiscoveryVersion.wsDiscovery11 = new DiscoveryVersion("WSDiscovery11", "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01", new DiscoveryVersion11Implementation());
						}
					}
				}
				return DiscoveryVersion.wsDiscovery11;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00007268 File Offset: 0x00005468
		public string Namespace
		{
			get
			{
				return this.discoveryNamespace;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00007270 File Offset: 0x00005470
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00007278 File Offset: 0x00005478
		public MessageVersion MessageVersion
		{
			get
			{
				return this.discoveryVersionImplementation.MessageVersion;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00007285 File Offset: 0x00005485
		public Uri AdhocAddress
		{
			get
			{
				return this.discoveryVersionImplementation.DiscoveryAddress;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00007292 File Offset: 0x00005492
		internal static DiscoveryVersion DefaultDiscoveryVersion
		{
			get
			{
				return DiscoveryVersion.FromName("WSDiscovery11");
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000729E File Offset: 0x0000549E
		internal IDiscoveryVersionImplementation Implementation
		{
			get
			{
				return this.discoveryVersionImplementation;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000072A8 File Offset: 0x000054A8
		public static DiscoveryVersion FromName(string name)
		{
			if (name == null)
			{
				throw FxTrace.Exception.ArgumentNull("name");
			}
			if (DiscoveryVersion.WSDiscovery11.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
			{
				return DiscoveryVersion.WSDiscovery11;
			}
			if (DiscoveryVersion.WSDiscoveryCD1.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
			{
				return DiscoveryVersion.WSDiscoveryCD1;
			}
			if (DiscoveryVersion.WSDiscoveryApril2005.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
			{
				return DiscoveryVersion.WSDiscoveryApril2005;
			}
			throw FxTrace.Exception.AsError(new ArgumentOutOfRangeException(SR.DiscoveryIncorrectVersion(name, DiscoveryVersion.WSDiscovery11.Name, DiscoveryVersion.WSDiscoveryCD1.Name, DiscoveryVersion.WSDiscoveryApril2005.Name)));
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00007346 File Offset: 0x00005546
		public override string ToString()
		{
			return SR.DiscoveryVersionToString(this.Name, this.Namespace);
		}

		// Token: 0x0400007D RID: 125
		private static DiscoveryVersion wsDiscoveryApril2005;

		// Token: 0x0400007E RID: 126
		private static DiscoveryVersion wsDiscoveryCD1;

		// Token: 0x0400007F RID: 127
		private static DiscoveryVersion wsDiscovery11;

		// Token: 0x04000080 RID: 128
		private static object staticLock = new object();

		// Token: 0x04000081 RID: 129
		private string name;

		// Token: 0x04000082 RID: 130
		private string discoveryNamespace;

		// Token: 0x04000083 RID: 131
		private IDiscoveryVersionImplementation discoveryVersionImplementation;

		// Token: 0x020000CF RID: 207
		internal class SchemaQualifiedNames
		{
			// Token: 0x060007EB RID: 2027 RVA: 0x00014C9C File Offset: 0x00012E9C
			internal SchemaQualifiedNames(string versionNameSpace, string wsaNameSpace)
			{
				this.AppSequenceType = new XmlQualifiedName("AppSequenceType", versionNameSpace);
				this.AnyType = new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");
				this.AnyUriType = new XmlQualifiedName("anyURI", "http://www.w3.org/2001/XMLSchema");
				this.EprElement = new XmlQualifiedName("EndpointReference", wsaNameSpace);
				this.MetadataVersionElement = new XmlQualifiedName("MetadataVersion", versionNameSpace);
				this.ProbeMatchType = new XmlQualifiedName("ProbeMatchType", versionNameSpace);
				this.ProbeType = new XmlQualifiedName("ProbeType", versionNameSpace);
				this.QNameListType = new XmlQualifiedName("QNameListType", versionNameSpace);
				this.QNameType = new XmlQualifiedName("QName", "http://www.w3.org/2001/XMLSchema");
				this.ResolveType = new XmlQualifiedName("ResolveType", versionNameSpace);
				this.ScopesElement = new XmlQualifiedName("Scopes", versionNameSpace);
				this.ScopesType = new XmlQualifiedName("ScopesType", versionNameSpace);
				this.TypesElement = new XmlQualifiedName("Types", versionNameSpace);
				this.UnsignedIntType = new XmlQualifiedName("unsignedInt", "http://www.w3.org/2001/XMLSchema");
				this.UriListType = new XmlQualifiedName("UriListType", versionNameSpace);
				this.XAddrsElement = new XmlQualifiedName("XAddrs", versionNameSpace);
			}

			// Token: 0x040001FD RID: 509
			public readonly XmlQualifiedName AppSequenceType;

			// Token: 0x040001FE RID: 510
			public readonly XmlQualifiedName AnyType;

			// Token: 0x040001FF RID: 511
			public readonly XmlQualifiedName AnyUriType;

			// Token: 0x04000200 RID: 512
			public readonly XmlQualifiedName EprElement;

			// Token: 0x04000201 RID: 513
			public readonly XmlQualifiedName MetadataVersionElement;

			// Token: 0x04000202 RID: 514
			public readonly XmlQualifiedName ProbeMatchType;

			// Token: 0x04000203 RID: 515
			public readonly XmlQualifiedName ProbeType;

			// Token: 0x04000204 RID: 516
			public readonly XmlQualifiedName QNameListType;

			// Token: 0x04000205 RID: 517
			public readonly XmlQualifiedName QNameType;

			// Token: 0x04000206 RID: 518
			public readonly XmlQualifiedName ResolveType;

			// Token: 0x04000207 RID: 519
			public readonly XmlQualifiedName ScopesElement;

			// Token: 0x04000208 RID: 520
			public readonly XmlQualifiedName ScopesType;

			// Token: 0x04000209 RID: 521
			public readonly XmlQualifiedName TypesElement;

			// Token: 0x0400020A RID: 522
			public readonly XmlQualifiedName UnsignedIntType;

			// Token: 0x0400020B RID: 523
			public readonly XmlQualifiedName UriListType;

			// Token: 0x0400020C RID: 524
			public readonly XmlQualifiedName XAddrsElement;
		}
	}
}
