using System;
using System.Net;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.Xml
{
	// Token: 0x0200004B RID: 75
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class XmlSecureResolver : XmlResolver
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00009165 File Offset: 0x00008165
		public XmlSecureResolver(XmlResolver resolver, string securityUrl) : this(resolver, XmlSecureResolver.CreateEvidenceForUrl(securityUrl))
		{
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00009174 File Offset: 0x00008174
		public XmlSecureResolver(XmlResolver resolver, Evidence evidence) : this(resolver, SecurityManager.ResolvePolicy(evidence))
		{
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00009183 File Offset: 0x00008183
		public XmlSecureResolver(XmlResolver resolver, PermissionSet permissionSet)
		{
			this.resolver = resolver;
			this.permissionSet = permissionSet;
		}

		// Token: 0x17000047 RID: 71
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00009199 File Offset: 0x00008199
		public override ICredentials Credentials
		{
			set
			{
				this.resolver.Credentials = value;
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000091A7 File Offset: 0x000081A7
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			this.permissionSet.PermitOnly();
			return this.resolver.GetEntity(absoluteUri, role, ofObjectToReturn);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000091C2 File Offset: 0x000081C2
		public override Uri ResolveUri(Uri baseUri, string relativeUri)
		{
			return this.resolver.ResolveUri(baseUri, relativeUri);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000091D4 File Offset: 0x000081D4
		public static Evidence CreateEvidenceForUrl(string securityUrl)
		{
			Evidence evidence = new Evidence();
			if (securityUrl != null && securityUrl.Length > 0)
			{
				evidence.AddHost(new Url(securityUrl));
				evidence.AddHost(Zone.CreateFromUrl(securityUrl));
				Uri uri = new Uri(securityUrl, UriKind.RelativeOrAbsolute);
				if (uri.IsAbsoluteUri && !uri.IsFile)
				{
					evidence.AddHost(Site.CreateFromUrl(securityUrl));
				}
			}
			return evidence;
		}

		// Token: 0x04000512 RID: 1298
		private XmlResolver resolver;

		// Token: 0x04000513 RID: 1299
		private PermissionSet permissionSet;
	}
}
