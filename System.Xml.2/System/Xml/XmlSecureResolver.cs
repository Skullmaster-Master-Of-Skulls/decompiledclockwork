using System;
using System.IO;
using System.Net;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000098 RID: 152
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class XmlSecureResolver : XmlResolver
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x00014038 File Offset: 0x00012238
		public XmlSecureResolver(XmlResolver resolver, string securityUrl) : this(resolver, XmlSecureResolver.CreateEvidenceForUrl(securityUrl))
		{
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00014047 File Offset: 0x00012247
		public XmlSecureResolver(XmlResolver resolver, Evidence evidence) : this(resolver, SecurityManager.GetStandardSandbox(evidence))
		{
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00014056 File Offset: 0x00012256
		public XmlSecureResolver(XmlResolver resolver, PermissionSet permissionSet)
		{
			this.resolver = resolver;
			this.permissionSet = permissionSet;
		}

		// Token: 0x17000109 RID: 265
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0001406C File Offset: 0x0001226C
		public override ICredentials Credentials
		{
			set
			{
				this.resolver.Credentials = value;
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001407A File Offset: 0x0001227A
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			this.permissionSet.PermitOnly();
			return this.resolver.GetEntity(absoluteUri, role, ofObjectToReturn);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00014095 File Offset: 0x00012295
		public override Uri ResolveUri(Uri baseUri, string relativeUri)
		{
			return this.resolver.ResolveUri(baseUri, relativeUri);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000140A4 File Offset: 0x000122A4
		public static Evidence CreateEvidenceForUrl(string securityUrl)
		{
			Evidence evidence = new Evidence();
			if (securityUrl != null && securityUrl.Length > 0)
			{
				evidence.AddHostEvidence<Url>(new Url(securityUrl));
				evidence.AddHostEvidence<Zone>(Zone.CreateFromUrl(securityUrl));
				Uri uri = new Uri(securityUrl, UriKind.RelativeOrAbsolute);
				if (uri.IsAbsoluteUri && !uri.IsFile)
				{
					evidence.AddHostEvidence<Site>(Site.CreateFromUrl(securityUrl));
				}
				if (uri.IsAbsoluteUri && uri.IsUnc)
				{
					string directoryName = Path.GetDirectoryName(uri.LocalPath);
					if (directoryName != null && directoryName.Length != 0)
					{
						evidence.AddHostEvidence<XmlSecureResolver.UncDirectory>(new XmlSecureResolver.UncDirectory(directoryName));
					}
				}
			}
			return evidence;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00014133 File Offset: 0x00012333
		public override Task<object> GetEntityAsync(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			this.permissionSet.PermitOnly();
			return this.resolver.GetEntityAsync(absoluteUri, role, ofObjectToReturn);
		}

		// Token: 0x04000245 RID: 581
		private XmlResolver resolver;

		// Token: 0x04000246 RID: 582
		private PermissionSet permissionSet;

		// Token: 0x02000316 RID: 790
		[Serializable]
		private class UncDirectory : EvidenceBase, IIdentityPermissionFactory
		{
			// Token: 0x06002DC0 RID: 11712 RVA: 0x000EDD89 File Offset: 0x000EBF89
			public UncDirectory(string uncDirectory)
			{
				this.uncDir = uncDirectory;
			}

			// Token: 0x06002DC1 RID: 11713 RVA: 0x000EDD98 File Offset: 0x000EBF98
			public IPermission CreateIdentityPermission(Evidence evidence)
			{
				return new FileIOPermission(FileIOPermissionAccess.Read, this.uncDir);
			}

			// Token: 0x06002DC2 RID: 11714 RVA: 0x000EDDA6 File Offset: 0x000EBFA6
			public override EvidenceBase Clone()
			{
				return new XmlSecureResolver.UncDirectory(this.uncDir);
			}

			// Token: 0x06002DC3 RID: 11715 RVA: 0x000EDDB4 File Offset: 0x000EBFB4
			private SecurityElement ToXml()
			{
				SecurityElement securityElement = new SecurityElement("System.Xml.XmlSecureResolver");
				securityElement.AddAttribute("version", "1");
				securityElement.AddChild(new SecurityElement("UncDirectory", this.uncDir));
				return securityElement;
			}

			// Token: 0x06002DC4 RID: 11716 RVA: 0x000EDDF3 File Offset: 0x000EBFF3
			public override string ToString()
			{
				return this.ToXml().ToString();
			}

			// Token: 0x0400148E RID: 5262
			private string uncDir;
		}
	}
}
