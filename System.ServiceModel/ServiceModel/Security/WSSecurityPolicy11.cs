using System;
using System.Collections.Generic;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000295 RID: 661
	internal class WSSecurityPolicy11 : WSSecurityPolicy
	{
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x0004B264 File Offset: 0x00049464
		public override string WsspNamespaceUri
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/07/securitypolicy";
			}
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0004B26B File Offset: 0x0004946B
		public override bool IsSecurityVersionSupported(MessageSecurityVersion version)
		{
			return version == MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10 || version == MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11 || version == MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0004B287 File Offset: 0x00049487
		public override MessageSecurityVersion GetSupportedMessageSecurityVersion(SecurityVersion version)
		{
			if (version != SecurityVersion.WSSecurity10)
			{
				return MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
			}
			return MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x0004B29C File Offset: 0x0004949C
		public override TrustDriver TrustDriver
		{
			get
			{
				return new WSTrustFeb2005.DriverFeb2005(new SecurityStandardsManager(MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11, WSSecurityTokenSerializer.DefaultInstance));
			}
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0004B2B4 File Offset: 0x000494B4
		public override XmlElement CreateWsspMustNotSendCancelAssertion(bool requireCancel)
		{
			if (!requireCancel)
			{
				return this.CreateMsspAssertion("MustNotSendCancel");
			}
			return null;
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0004B2D3 File Offset: 0x000494D3
		public override bool TryImportWsspMustNotSendCancelAssertion(ICollection<XmlElement> assertions, out bool requireCancellation)
		{
			requireCancellation = !this.TryImportMsspAssertion(assertions, "MustNotSendCancel");
			return true;
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0004B2E8 File Offset: 0x000494E8
		public override XmlElement CreateWsspHttpsTokenAssertion(MetadataExporter exporter, HttpsTransportBindingElement httpsBinding)
		{
			XmlElement xmlElement = this.CreateWsspAssertion("HttpsToken");
			xmlElement.SetAttribute("RequireClientCertificate", httpsBinding.RequireClientCertificate ? "true" : "false");
			return xmlElement;
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0004B324 File Offset: 0x00049524
		public override bool TryImportWsspHttpsTokenAssertion(MetadataImporter importer, ICollection<XmlElement> assertions, HttpsTransportBindingElement httpsBinding)
		{
			if (assertions == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertions");
			}
			XmlElement xmlElement;
			bool result;
			if (this.TryImportWsspAssertion(assertions, "HttpsToken", out xmlElement))
			{
				result = true;
				string attribute = xmlElement.GetAttribute("RequireClientCertificate");
				try
				{
					httpsBinding.RequireClientCertificate = XmlUtil.IsTrue(attribute);
					return result;
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (ex is NullReferenceException)
					{
						throw;
					}
					importer.Errors.Add(new MetadataConversionError(SR.GetString("UnsupportedBooleanAttribute", new object[]
					{
						"RequireClientCertificate",
						ex.Message
					}), false));
					return false;
				}
			}
			result = false;
			return result;
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0004B3D0 File Offset: 0x000495D0
		public override XmlElement CreateWsspTrustAssertion(MetadataExporter exporter, SecurityKeyEntropyMode keyEntropyMode)
		{
			return base.CreateWsspTrustAssertion("Trust10", exporter, keyEntropyMode);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0004B3DF File Offset: 0x000495DF
		public override bool TryImportWsspTrustAssertion(MetadataImporter importer, ICollection<XmlElement> assertions, SecurityBindingElement binding, out XmlElement assertion)
		{
			return base.TryImportWsspTrustAssertion("Trust10", importer, assertions, binding, out assertion);
		}

		// Token: 0x04001A99 RID: 6809
		public const string WsspNamespace = "http://schemas.xmlsoap.org/ws/2005/07/securitypolicy";
	}
}
