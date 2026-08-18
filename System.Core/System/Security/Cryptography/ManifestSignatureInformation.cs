using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;
using System.Xml;

namespace System.Security.Cryptography
{
	// Token: 0x02000103 RID: 259
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManifestSignatureInformation
	{
		// Token: 0x06000881 RID: 2177 RVA: 0x0001CDBC File Offset: 0x0001AFBC
		internal ManifestSignatureInformation(ManifestKinds manifest, StrongNameSignatureInformation strongNameSignature, AuthenticodeSignatureInformation authenticodeSignature)
		{
			this.m_manifest = manifest;
			this.m_strongNameSignature = strongNameSignature;
			this.m_authenticodeSignature = authenticodeSignature;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0001CDD9 File Offset: 0x0001AFD9
		public AuthenticodeSignatureInformation AuthenticodeSignature
		{
			get
			{
				return this.m_authenticodeSignature;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0001CDE1 File Offset: 0x0001AFE1
		public ManifestKinds Manifest
		{
			get
			{
				return this.m_manifest;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0001CDE9 File Offset: 0x0001AFE9
		public StrongNameSignatureInformation StrongNameSignature
		{
			get
			{
				return this.m_strongNameSignature;
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001CDF4 File Offset: 0x0001AFF4
		[SecuritySafeCritical]
		private unsafe static XmlDocument GetManifestXml(ActivationContext application, ManifestKinds manifest)
		{
			IStream stream = null;
			if (manifest == ManifestKinds.Application)
			{
				stream = (InternalActivationContextHelper.GetApplicationComponentManifest(application) as IStream);
			}
			else if (manifest == ManifestKinds.Deployment)
			{
				stream = (InternalActivationContextHelper.GetDeploymentComponentManifest(application) as IStream);
			}
			XmlDocument result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				byte[] array = new byte[4096];
				int num = 0;
				do
				{
					stream.Read(array, array.Length, new IntPtr((void*)(&num)));
					memoryStream.Write(array, 0, num);
				}
				while (num == array.Length);
				memoryStream.Position = 0L;
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				xmlDocument.Load(memoryStream);
				result = xmlDocument;
			}
			return result;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0001CE9C File Offset: 0x0001B09C
		public static ManifestSignatureInformationCollection VerifySignature(ActivationContext application)
		{
			return ManifestSignatureInformation.VerifySignature(application, ManifestKinds.ApplicationAndDeployment);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001CEA5 File Offset: 0x0001B0A5
		public static ManifestSignatureInformationCollection VerifySignature(ActivationContext application, ManifestKinds manifests)
		{
			return ManifestSignatureInformation.VerifySignature(application, manifests, X509RevocationFlag.ExcludeRoot, X509RevocationMode.Online);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001CEB0 File Offset: 0x0001B0B0
		[SecuritySafeCritical]
		public static ManifestSignatureInformationCollection VerifySignature(ActivationContext application, ManifestKinds manifests, X509RevocationFlag revocationFlag, X509RevocationMode revocationMode)
		{
			if (application == null)
			{
				throw new ArgumentNullException("application");
			}
			if (revocationFlag < X509RevocationFlag.EndCertificateOnly || X509RevocationFlag.ExcludeRoot < revocationFlag)
			{
				throw new ArgumentOutOfRangeException("revocationFlag");
			}
			if (revocationMode < X509RevocationMode.NoCheck || X509RevocationMode.Offline < revocationMode)
			{
				throw new ArgumentOutOfRangeException("revocationMode");
			}
			List<ManifestSignatureInformation> list = new List<ManifestSignatureInformation>();
			if ((manifests & ManifestKinds.Deployment) == ManifestKinds.Deployment)
			{
				XmlDocument manifestXml = ManifestSignatureInformation.GetManifestXml(application, ManifestKinds.Deployment);
				ManifestSignedXml manifestSignedXml = new ManifestSignedXml(manifestXml, ManifestKinds.Deployment);
				list.Add(manifestSignedXml.VerifySignature(revocationFlag, revocationMode));
			}
			if ((manifests & ManifestKinds.Application) == ManifestKinds.Application)
			{
				XmlDocument manifestXml2 = ManifestSignatureInformation.GetManifestXml(application, ManifestKinds.Application);
				ManifestSignedXml manifestSignedXml2 = new ManifestSignedXml(manifestXml2, ManifestKinds.Application);
				list.Add(manifestSignedXml2.VerifySignature(revocationFlag, revocationMode));
			}
			return new ManifestSignatureInformationCollection(list);
		}

		// Token: 0x0400067F RID: 1663
		private ManifestKinds m_manifest;

		// Token: 0x04000680 RID: 1664
		private StrongNameSignatureInformation m_strongNameSignature;

		// Token: 0x04000681 RID: 1665
		private AuthenticodeSignatureInformation m_authenticodeSignature;
	}
}
