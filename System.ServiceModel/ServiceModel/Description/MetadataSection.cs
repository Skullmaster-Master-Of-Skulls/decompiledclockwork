using System;
using System.Collections.ObjectModel;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Description
{
	// Token: 0x020003F2 RID: 1010
	[XmlRoot(ElementName = "MetadataSection", Namespace = "http://schemas.xmlsoap.org/ws/2004/09/mex")]
	public class MetadataSection
	{
		// Token: 0x06002603 RID: 9731 RVA: 0x000896C1 File Offset: 0x000878C1
		public MetadataSection() : this(null, null, null)
		{
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x000896CC File Offset: 0x000878CC
		public MetadataSection(string dialect, string identifier, object metadata)
		{
			this.dialect = dialect;
			this.identifier = identifier;
			this.metadata = metadata;
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x000896F4 File Offset: 0x000878F4
		public static string ServiceDescriptionDialect
		{
			get
			{
				return "http://schemas.xmlsoap.org/wsdl/";
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x000896FB File Offset: 0x000878FB
		public static string XmlSchemaDialect
		{
			get
			{
				return "http://www.w3.org/2001/XMLSchema";
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x00089702 File Offset: 0x00087902
		public static string PolicyDialect
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2004/09/policy";
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x00089709 File Offset: 0x00087909
		public static string MetadataExchangeDialect
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2004/09/mex";
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x00089710 File Offset: 0x00087910
		[XmlAnyAttribute]
		public Collection<XmlAttribute> Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x00089718 File Offset: 0x00087918
		// (set) Token: 0x0600260B RID: 9739 RVA: 0x00089720 File Offset: 0x00087920
		[XmlAttribute]
		public string Dialect
		{
			get
			{
				return this.dialect;
			}
			set
			{
				this.dialect = value;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x00089729 File Offset: 0x00087929
		// (set) Token: 0x0600260D RID: 9741 RVA: 0x00089731 File Offset: 0x00087931
		[XmlAttribute]
		public string Identifier
		{
			get
			{
				return this.identifier;
			}
			set
			{
				this.identifier = value;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x0008973A File Offset: 0x0008793A
		// (set) Token: 0x0600260F RID: 9743 RVA: 0x00089742 File Offset: 0x00087942
		[XmlAnyElement]
		[XmlElement("schema", typeof(XmlSchema), Namespace = "http://www.w3.org/2001/XMLSchema")]
		[XmlElement("definitions", typeof(ServiceDescription), Namespace = "http://schemas.xmlsoap.org/wsdl/")]
		[XmlElement("MetadataReference", typeof(MetadataReference), Namespace = "http://schemas.xmlsoap.org/ws/2004/09/mex")]
		[XmlElement("Location", typeof(MetadataLocation), Namespace = "http://schemas.xmlsoap.org/ws/2004/09/mex")]
		[XmlElement("Metadata", typeof(MetadataSet), Namespace = "http://schemas.xmlsoap.org/ws/2004/09/mex")]
		public object Metadata
		{
			get
			{
				return this.metadata;
			}
			set
			{
				this.metadata = value;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06002610 RID: 9744 RVA: 0x0008974B File Offset: 0x0008794B
		// (set) Token: 0x06002611 RID: 9745 RVA: 0x00089753 File Offset: 0x00087953
		internal string SourceUrl
		{
			get
			{
				return this.sourceUrl;
			}
			set
			{
				this.sourceUrl = value;
			}
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x0008975C File Offset: 0x0008795C
		public static MetadataSection CreateFromPolicy(XmlElement policy, string identifier)
		{
			if (policy == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("policy");
			}
			if (!MetadataSection.IsPolicyElement(policy))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("policy", SR.GetString("SFxBadMetadataMustBePolicy", new object[]
				{
					"http://schemas.xmlsoap.org/ws/2004/09/policy",
					"Policy",
					policy.NamespaceURI,
					policy.LocalName
				}));
			}
			return new MetadataSection
			{
				Dialect = policy.NamespaceURI,
				Identifier = identifier,
				Metadata = policy
			};
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x000897E8 File Offset: 0x000879E8
		public static MetadataSection CreateFromSchema(XmlSchema schema)
		{
			if (schema == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("schema");
			}
			return new MetadataSection
			{
				Dialect = MetadataSection.XmlSchemaDialect,
				Identifier = schema.TargetNamespace,
				Metadata = schema
			};
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x00089830 File Offset: 0x00087A30
		public static MetadataSection CreateFromServiceDescription(ServiceDescription serviceDescription)
		{
			if (serviceDescription == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("serviceDescription");
			}
			return new MetadataSection
			{
				Dialect = MetadataSection.ServiceDescriptionDialect,
				Identifier = serviceDescription.TargetNamespace,
				Metadata = serviceDescription
			};
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x00089875 File Offset: 0x00087A75
		internal static bool IsPolicyElement(XmlElement policy)
		{
			return (policy.NamespaceURI == "http://schemas.xmlsoap.org/ws/2004/09/policy" || policy.NamespaceURI == "http://www.w3.org/ns/ws-policy") && policy.LocalName == "Policy";
		}

		// Token: 0x04002175 RID: 8565
		private Collection<XmlAttribute> attributes = new Collection<XmlAttribute>();

		// Token: 0x04002176 RID: 8566
		private string dialect;

		// Token: 0x04002177 RID: 8567
		private string identifier;

		// Token: 0x04002178 RID: 8568
		private object metadata;

		// Token: 0x04002179 RID: 8569
		private string sourceUrl;

		// Token: 0x0400217A RID: 8570
		private static XmlDocument xmlDocument = new XmlDocument();
	}
}
