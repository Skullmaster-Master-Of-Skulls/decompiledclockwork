using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ClockWorkLogger;
using TechnoPro.Common.ICore.Authentication;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Authentication.ADFS;
using TechnoPro.Common.Security.Saml;

namespace TechnoPro.Common.Core.AuthenticationADFS
{
	// Token: 0x02000002 RID: 2
	public class ADFS2AuthManager : IADFSAuthManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public AdfsParameters Parameters { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public OperationContext OpContext { get; set; }

		// Token: 0x06000005 RID: 5 RVA: 0x00002074 File Offset: 0x00000274
		public bool ValidateToken(string token, out ClaimsPrincipal claimsPrincipal)
		{
			bool flag = string.IsNullOrEmpty(token);
			bool result;
			if (flag)
			{
				CWLogger.Logger.Warn("ADFS2AuthManager:ValdiateToken:token is null or empty - skipping...");
				claimsPrincipal = null;
				result = false;
			}
			else
			{
				CWLogger.Logger.Debug("ADFSAuthManager:ValidateToken:token={0}", token);
				string text = "";
				try
				{
					text = ADFS2AuthManager.Base64Decode(token);
				}
				catch (Exception ex)
				{
					CWLogger.Logger.Warn("ADFS2AuthManager:ValidateToken:Error (ignored) base 64 decoding:token={0}:err={1}", token ?? "NULL", ex.ToString());
					text = token;
				}
				CWLogger.Logger.Debug("ADFSAuthManager:ValidateToken:samlResponseXml={0}", text ?? "NULL");
				SecurityTokenElement tokenIssuer = new SecurityTokenElement
				{
					Name = this.Parameters.IssuerName,
					UriToken = new Uri(this.Parameters.UriToken),
					StoreLocation = this.Parameters.StoreLocation,
					StoreName = this.Parameters.StoreName,
					FindType = X509FindType.FindByThumbprint,
					FindValue = this.Parameters.CertificateThumbprint
				};
				Saml2Response saml2Response = new Saml2Response();
				saml2Response.ReadXml(text, tokenIssuer);
				SamlResponseStatusCode? statusCode = saml2Response.StatusCode;
				bool flag2;
				if (statusCode != null)
				{
					statusCode = saml2Response.StatusCode;
					flag2 = (statusCode.Value > SamlResponseStatusCode.Success);
				}
				else
				{
					flag2 = true;
				}
				bool flag3 = flag2;
				if (flag3)
				{
					claimsPrincipal = null;
					CWLogger logger = CWLogger.Logger;
					string str = "ADFS2AuthManager::ValidateToken: ";
					statusCode = saml2Response.StatusCode;
					logger.Error(str + (((statusCode != null) ? statusCode.GetValueOrDefault().ToString() : null) ?? "NULL"));
					result = false;
				}
				else
				{
					IDictionary<string, string> claims = saml2Response.GetClaims();
					claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(from c in claims
					select new Claim(c.Key, c.Value)));
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002258 File Offset: 0x00000458
		public string GetSamlResponseFromSamlArtifact(string samlArt, string relyingPartyId, CertificateLocation privateSigningCertLocation, string artifactResolutionServiceUri)
		{
			bool flag = string.IsNullOrEmpty(samlArt);
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string id = "_" + Guid.NewGuid().ToString();
				string artifact = Uri.UnescapeDataString(samlArt);
				string artifactResolveSoap = string.Concat(new string[]
				{
					"<samlp:ArtifactResolve xmlns:samlp=\"urn:oasis:names:tc:SAML:2.0:protocol\"\r\nxmlns:saml=\"urn:oasis:names:tc:SAML:2.0:assertion\"\r\nID=\"",
					id,
					"\"\r\nVersion=\"2.0\"\r\nIssueInstant=\"",
					DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
					"\">\r\n<saml:Issuer>",
					relyingPartyId,
					"</saml:Issuer>\r\n\r\n<samlp:Artifact>",
					artifact,
					"</samlp:Artifact>\r\n</samlp:ArtifactResolve>"
				});
				result = Task.Run<string>(() => ADFS2AuthManager.ConsumeArtifactRequest(artifact, id, artifactResolveSoap, privateSigningCertLocation, artifactResolutionServiceUri)).Result;
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000233C File Offset: 0x0000053C
		private static string Base64Decode(string base64EncodedData)
		{
			byte[] bytes = Convert.FromBase64String(base64EncodedData);
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002360 File Offset: 0x00000560
		private static X509Certificate2 LoadCert(StoreLocation storeLocation, StoreName storeName, X509FindType findType, string findValue)
		{
			string findValue2 = (findValue ?? "").Trim().ToUpper();
			X509Store x509Store = new X509Store(storeName, storeLocation);
			x509Store.Open(OpenFlags.OpenExistingOnly);
			X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(findType, findValue2, false);
			return (x509Certificate2Collection.Count > 0) ? x509Certificate2Collection[0] : null;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023BC File Offset: 0x000005BC
		private static string SignArtifactResolve(string artifactResolveBody, X509Certificate2 certificate, string referenceUri)
		{
			bool flag = artifactResolveBody == null || certificate == null;
			if (flag)
			{
				throw new ArgumentNullException();
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(artifactResolveBody);
			SignedXml signedXml = new SignedXml(xmlDocument);
			signedXml.SigningKey = certificate.PrivateKey;
			signedXml.SignedInfo.CanonicalizationMethod = "http://www.w3.org/2001/10/xml-exc-c14n#";
			Reference reference = new Reference();
			reference.Uri = referenceUri;
			XmlDsigEnvelopedSignatureTransform transform = new XmlDsigEnvelopedSignatureTransform(true);
			reference.AddTransform(transform);
			reference.AddTransform(new XmlDsigExcC14NTransform
			{
				Algorithm = "http://www.w3.org/2001/10/xml-exc-c14n#"
			});
			signedXml.AddReference(reference);
			signedXml.ComputeSignature();
			XmlElement xml = signedXml.GetXml();
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");
			XmlNode xmlNode = null;
			try
			{
				xmlNode = xmlDocument.DocumentElement.SelectSingleNode("//samlp:Artifact", xmlNamespaceManager);
			}
			catch
			{
			}
			bool flag2 = xmlNode == null;
			if (flag2)
			{
				XmlNode refChild = xmlDocument.DocumentElement.ChildNodes[xmlDocument.DocumentElement.ChildNodes.Count - 1];
				xmlDocument.DocumentElement.InsertBefore(xmlDocument.ImportNode(xml, true), refChild);
			}
			else
			{
				xmlDocument.DocumentElement.InsertBefore(xmlDocument.ImportNode(xml, true), xmlNode);
			}
			return ADFS2AuthManager.CreateSoapEnvelope(xmlDocument.OuterXml);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002528 File Offset: 0x00000728
		private static string CreateSoapEnvelope(string soapBodyContent)
		{
			bool flag = string.IsNullOrEmpty(soapBodyContent);
			if (flag)
			{
				throw new ArgumentNullException();
			}
			return string.Format("<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\"><s:Body>{0}</s:Body></s:Envelope>", soapBodyContent);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002558 File Offset: 0x00000758
		[DebuggerStepThrough]
		private static Task<string> ConsumeArtifactRequest(string artifact, string id, string artifactResolveSoap, CertificateLocation privateSigningCertLocation, string artifactResolutionServiceUri)
		{
			ADFS2AuthManager.<ConsumeArtifactRequest>d__14 <ConsumeArtifactRequest>d__ = new ADFS2AuthManager.<ConsumeArtifactRequest>d__14();
			<ConsumeArtifactRequest>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ConsumeArtifactRequest>d__.artifact = artifact;
			<ConsumeArtifactRequest>d__.id = id;
			<ConsumeArtifactRequest>d__.artifactResolveSoap = artifactResolveSoap;
			<ConsumeArtifactRequest>d__.privateSigningCertLocation = privateSigningCertLocation;
			<ConsumeArtifactRequest>d__.artifactResolutionServiceUri = artifactResolutionServiceUri;
			<ConsumeArtifactRequest>d__.<>1__state = -1;
			<ConsumeArtifactRequest>d__.<>t__builder.Start<ADFS2AuthManager.<ConsumeArtifactRequest>d__14>(ref <ConsumeArtifactRequest>d__);
			return <ConsumeArtifactRequest>d__.<>t__builder.Task;
		}
	}
}
