using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Xml;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200011D RID: 285
	internal sealed class ManifestSignedXml : SignedXml
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x0001F398 File Offset: 0x0001D598
		public ManifestSignedXml(XmlDocument manifestXml, ManifestKinds manifest) : base(manifestXml)
		{
			this.m_manifest = manifest;
			this.m_manifestXml = manifestXml;
			this.m_namespaceManager = new XmlNamespaceManager(manifestXml.NameTable);
			this.m_namespaceManager.AddNamespace("as", "http://schemas.microsoft.com/windows/pki/2005/Authenticode");
			this.m_namespaceManager.AddNamespace("asm", "urn:schemas-microsoft-com:asm.v1");
			this.m_namespaceManager.AddNamespace("asmv2", "urn:schemas-microsoft-com:asm.v2");
			this.m_namespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			this.m_namespaceManager.AddNamespace("msrel", "http://schemas.microsoft.com/windows/rel/2005/reldata");
			this.m_namespaceManager.AddNamespace("r", "urn:mpeg:mpeg21:2003:01-REL-R-NS");
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001F44C File Offset: 0x0001D64C
		private static byte[] BackwardHexToBytes(string hex)
		{
			if (string.IsNullOrEmpty(hex) || hex.Length % 2 != 0)
			{
				return null;
			}
			byte[] array = new byte[hex.Length / 2];
			int num = hex.Length - 2;
			for (int i = 0; i < array.Length; i++)
			{
				byte? b = ManifestSignedXml.HexToByte(hex[num]);
				byte? b2 = ManifestSignedXml.HexToByte(hex[num + 1]);
				if (b == null || b2 == null)
				{
					return null;
				}
				array[i] = (byte)((int)b.Value << 4 | (int)b2.Value);
				num -= 2;
			}
			return array;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001F4E0 File Offset: 0x0001D6E0
		[SecurityCritical]
		[StorePermission(SecurityAction.Assert, EnumerateCertificates = true, OpenStore = true)]
		private X509Chain BuildSignatureChain(X509Native.AXL_AUTHENTICODE_SIGNER_INFO signer, XmlElement licenseNode, X509RevocationFlag revocationFlag, X509RevocationMode revocationMode)
		{
			X509Chain x509Chain = null;
			if (signer.pChainContext != IntPtr.Zero)
			{
				x509Chain = new X509Chain(signer.pChainContext);
			}
			else if (signer.dwError == -2146762487)
			{
				XmlElement xmlElement = licenseNode.SelectSingleNode("r:issuer/ds:Signature/ds:KeyInfo/ds:X509Data", this.m_namespaceManager) as XmlElement;
				if (xmlElement != null)
				{
					XmlNodeList xmlNodeList = xmlElement.SelectNodes("ds:X509Certificate", this.m_namespaceManager);
					if (xmlNodeList.Count == 1 && xmlNodeList[0] is XmlElement)
					{
						byte[] rawData = Convert.FromBase64String(xmlNodeList[0].InnerText.Trim());
						X509Certificate2 certificate = new X509Certificate2(rawData);
						x509Chain = new X509Chain();
						x509Chain.ChainPolicy.RevocationFlag = revocationFlag;
						x509Chain.ChainPolicy.RevocationMode = revocationMode;
						x509Chain.Build(certificate);
					}
				}
			}
			return x509Chain;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001F5B0 File Offset: 0x0001D7B0
		private byte[] CalculateManifestPublicKeyToken()
		{
			XmlElement xmlElement = this.m_manifestXml.SelectSingleNode("//asm:assembly/asm:assemblyIdentity", this.m_namespaceManager) as XmlElement;
			if (xmlElement == null)
			{
				return null;
			}
			return ManifestSignedXml.HexStringToBytes(xmlElement.GetAttribute("publicKeyToken"));
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001F5F0 File Offset: 0x0001D7F0
		[SecuritySafeCritical]
		private unsafe static byte[] CalculateSignerPublicKeyToken(AsymmetricAlgorithm key)
		{
			ICspAsymmetricAlgorithm cspAsymmetricAlgorithm = key as ICspAsymmetricAlgorithm;
			if (cspAsymmetricAlgorithm == null)
			{
				return null;
			}
			byte[] array = cspAsymmetricAlgorithm.ExportCspBlob(false);
			byte[] array2;
			byte* value;
			if ((array2 = array) == null || array2.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array2[0];
			}
			CapiNative.CRYPTOAPI_BLOB cryptoapi_BLOB = default(CapiNative.CRYPTOAPI_BLOB);
			cryptoapi_BLOB.cbData = array.Length;
			cryptoapi_BLOB.pbData = new IntPtr((void*)value);
			SafeAxlBufferHandle safeAxlBufferHandle;
			int num = CapiNative.UnsafeNativeMethods._AxlPublicKeyBlobToPublicKeyToken(ref cryptoapi_BLOB, out safeAxlBufferHandle);
			if ((num & -2147483648) != 0)
			{
				return null;
			}
			array2 = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			byte[] result;
			try
			{
				safeAxlBufferHandle.DangerousAddRef(ref flag);
				result = ManifestSignedXml.HexStringToBytes(Marshal.PtrToStringUni(safeAxlBufferHandle.DangerousGetHandle()));
			}
			finally
			{
				if (flag)
				{
					safeAxlBufferHandle.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001F6A8 File Offset: 0x0001D8A8
		private static bool CompareBytes(byte[] lhs, byte[] rhs)
		{
			if (lhs == null || rhs == null)
			{
				return false;
			}
			for (int i = 0; i < lhs.Length; i++)
			{
				if (lhs[i] != rhs[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001F6D6 File Offset: 0x0001D8D6
		public override XmlElement GetIdElement(XmlDocument document, string idValue)
		{
			if (base.KeyInfo != null && string.Compare(base.KeyInfo.Id, idValue, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return base.KeyInfo.GetXml();
			}
			return null;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001F704 File Offset: 0x0001D904
		[SecurityCritical]
		private TimestampInformation GetTimestampInformation(X509Native.AXL_AUTHENTICODE_TIMESTAMPER_INFO timestamper, XmlElement licenseNode)
		{
			TimestampInformation result = null;
			if (timestamper.dwError == 0)
			{
				result = new TimestampInformation(timestamper);
			}
			else
			{
				if (timestamper.dwError == -2146762748 || timestamper.dwError == -2146762496)
				{
					XmlElement xmlElement = licenseNode.SelectSingleNode("r:issuer/ds:Signature/ds:Object/as:Timestamp", this.m_namespaceManager) as XmlElement;
					if (xmlElement == null)
					{
						return result;
					}
					byte[] encodedMessage = Convert.FromBase64String(xmlElement.InnerText);
					try
					{
						SignedCms signedCms = new SignedCms();
						signedCms.Decode(encodedMessage);
						signedCms.CheckSignature(true);
						return null;
					}
					catch (CryptographicException e)
					{
						return new TimestampInformation((SignatureVerificationResult)Marshal.GetHRForException(e));
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001F7A0 File Offset: 0x0001D9A0
		private static byte[] HexStringToBytes(string hex)
		{
			if (string.IsNullOrEmpty(hex) || hex.Length % 2 != 0)
			{
				return null;
			}
			byte[] array = new byte[hex.Length / 2];
			for (int i = 0; i < array.Length; i++)
			{
				byte? b = ManifestSignedXml.HexToByte(hex[i]);
				byte? b2 = ManifestSignedXml.HexToByte(hex[i + 1]);
				if (b == null || b2 == null)
				{
					return null;
				}
				array[i] = (byte)((int)b.Value << 4 | (int)b2.Value);
			}
			return array;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0001F824 File Offset: 0x0001DA24
		private static byte? HexToByte(char hex)
		{
			if (hex >= '0' && hex <= '9')
			{
				return new byte?((byte)(hex - '0'));
			}
			if (hex >= 'a' && hex <= 'f')
			{
				return new byte?((byte)(hex - 'a' + '\n'));
			}
			if (hex >= 'A' && hex <= 'F')
			{
				return new byte?((byte)(hex - 'A' + '\n'));
			}
			return null;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0001F880 File Offset: 0x0001DA80
		private static X509Native.AxlVerificationFlags MapRevocationFlags(X509RevocationFlag revocationFlag, X509RevocationMode revocationMode)
		{
			X509Native.AxlVerificationFlags axlVerificationFlags = X509Native.AxlVerificationFlags.None;
			switch (revocationFlag)
			{
			case X509RevocationFlag.EndCertificateOnly:
				axlVerificationFlags |= X509Native.AxlVerificationFlags.RevocationCheckEndCertOnly;
				goto IL_26;
			case X509RevocationFlag.EntireChain:
				axlVerificationFlags |= X509Native.AxlVerificationFlags.RevocationCheckEntireChain;
				goto IL_26;
			}
			axlVerificationFlags |= X509Native.AxlVerificationFlags.None;
			IL_26:
			switch (revocationMode)
			{
			case X509RevocationMode.NoCheck:
				return axlVerificationFlags | X509Native.AxlVerificationFlags.NoRevocationCheck;
			case X509RevocationMode.Offline:
				return axlVerificationFlags | X509Native.AxlVerificationFlags.UrlOnlyCacheRetrieval;
			}
			return axlVerificationFlags | X509Native.AxlVerificationFlags.None;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001F8D8 File Offset: 0x0001DAD8
		private SignatureVerificationResult VerifyAuthenticodeExpectedHash(XmlElement licenseNode)
		{
			XmlElement xmlElement = licenseNode.SelectSingleNode("r:grant/as:ManifestInformation", this.m_namespaceManager) as XmlElement;
			if (xmlElement == null)
			{
				return SignatureVerificationResult.BadSignatureFormat;
			}
			string attribute = xmlElement.GetAttribute("Hash");
			if (string.IsNullOrEmpty(attribute))
			{
				return SignatureVerificationResult.BadSignatureFormat;
			}
			byte[] lhs = ManifestSignedXml.BackwardHexToBytes(attribute);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Parse;
			using (TextReader textReader = new StringReader(this.m_manifestXml.OuterXml))
			{
				using (XmlReader xmlReader = XmlReader.Create(textReader, xmlReaderSettings, this.m_manifestXml.BaseURI))
				{
					xmlDocument.Load(xmlReader);
				}
			}
			XmlElement xmlElement2 = xmlDocument.SelectSingleNode("//asm:assembly/ds:Signature", this.m_namespaceManager) as XmlElement;
			xmlElement2.ParentNode.RemoveChild(xmlElement2);
			XmlDsigExcC14NTransform xmlDsigExcC14NTransform = new XmlDsigExcC14NTransform();
			xmlDsigExcC14NTransform.LoadInput(xmlDocument);
			byte[] rhs = null;
			using (SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider())
			{
				rhs = sha1CryptoServiceProvider.ComputeHash(xmlDsigExcC14NTransform.GetOutput() as MemoryStream);
			}
			if (!ManifestSignedXml.CompareBytes(lhs, rhs))
			{
				return SignatureVerificationResult.BadDigest;
			}
			return SignatureVerificationResult.Valid;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001FA28 File Offset: 0x0001DC28
		[SecuritySafeCritical]
		private SignatureVerificationResult VerifyAuthenticodePublisher(X509Certificate2 publisherCertificate)
		{
			XmlElement xmlElement = this.m_manifestXml.SelectSingleNode("//asm:assembly/asmv2:publisherIdentity", this.m_namespaceManager) as XmlElement;
			if (xmlElement == null)
			{
				return SignatureVerificationResult.BadSignatureFormat;
			}
			string attribute = xmlElement.GetAttribute("name");
			string attribute2 = xmlElement.GetAttribute("issuerKeyHash");
			if (string.IsNullOrEmpty(attribute) || string.IsNullOrEmpty(attribute2))
			{
				return SignatureVerificationResult.BadSignatureFormat;
			}
			SafeAxlBufferHandle safeAxlBufferHandle = null;
			int num = X509Native.UnsafeNativeMethods._AxlGetIssuerPublicKeyHash(publisherCertificate.Handle, out safeAxlBufferHandle);
			if (num != 0)
			{
				return (SignatureVerificationResult)num;
			}
			string strB = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				safeAxlBufferHandle.DangerousAddRef(ref flag);
				strB = Marshal.PtrToStringUni(safeAxlBufferHandle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					safeAxlBufferHandle.DangerousRelease();
				}
			}
			if (string.Compare(attribute, publisherCertificate.SubjectName.Name, StringComparison.Ordinal) != 0 || string.Compare(attribute2, strB, StringComparison.Ordinal) != 0)
			{
				return SignatureVerificationResult.PublisherMismatch;
			}
			return SignatureVerificationResult.Valid;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0001FB00 File Offset: 0x0001DD00
		[SecuritySafeCritical]
		private unsafe AuthenticodeSignatureInformation VerifyAuthenticodeSignature(XmlElement signatureNode, X509RevocationFlag revocationFlag, X509RevocationMode revocationMode)
		{
			XmlElement xmlElement = signatureNode.SelectSingleNode("ds:KeyInfo/msrel:RelData/r:license", this.m_namespaceManager) as XmlElement;
			if (xmlElement == null)
			{
				return null;
			}
			SignatureVerificationResult signatureVerificationResult = this.VerifyAuthenticodeSignatureIdentity(xmlElement);
			if (signatureVerificationResult != SignatureVerificationResult.Valid)
			{
				return new AuthenticodeSignatureInformation(signatureVerificationResult);
			}
			SignatureVerificationResult signatureVerificationResult2 = this.VerifyAuthenticodeExpectedHash(xmlElement);
			if (signatureVerificationResult2 != SignatureVerificationResult.Valid)
			{
				return new AuthenticodeSignatureInformation(signatureVerificationResult2);
			}
			AuthenticodeSignatureInformation authenticodeSignatureInformation = null;
			X509Native.AXL_AUTHENTICODE_SIGNER_INFO signer = default(X509Native.AXL_AUTHENTICODE_SIGNER_INFO);
			signer.cbSize = Marshal.SizeOf(typeof(X509Native.AXL_AUTHENTICODE_SIGNER_INFO));
			X509Native.AXL_AUTHENTICODE_TIMESTAMPER_INFO timestamper = default(X509Native.AXL_AUTHENTICODE_TIMESTAMPER_INFO);
			timestamper.cbsize = Marshal.SizeOf(typeof(X509Native.AXL_AUTHENTICODE_TIMESTAMPER_INFO));
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(xmlElement.OuterXml);
				X509Native.AxlVerificationFlags dwFlags = ManifestSignedXml.MapRevocationFlags(revocationFlag, revocationMode);
				try
				{
					byte[] array;
					byte* value;
					if ((array = bytes) == null || array.Length == 0)
					{
						value = null;
					}
					else
					{
						value = &array[0];
					}
					CapiNative.CRYPTOAPI_BLOB cryptoapi_BLOB = default(CapiNative.CRYPTOAPI_BLOB);
					cryptoapi_BLOB.cbData = bytes.Length;
					cryptoapi_BLOB.pbData = new IntPtr((void*)value);
					int num = X509Native.UnsafeNativeMethods.CertVerifyAuthenticodeLicense(ref cryptoapi_BLOB, dwFlags, ref signer, ref timestamper);
					if (num == -2146762496)
					{
						return new AuthenticodeSignatureInformation(SignatureVerificationResult.MissingSignature);
					}
				}
				finally
				{
					byte[] array = null;
				}
				X509Chain signatureChain = this.BuildSignatureChain(signer, xmlElement, revocationFlag, revocationMode);
				TimestampInformation timestampInformation = this.GetTimestampInformation(timestamper, xmlElement);
				authenticodeSignatureInformation = new AuthenticodeSignatureInformation(signer, signatureChain, timestampInformation);
			}
			finally
			{
				X509Native.UnsafeNativeMethods.CertFreeAuthenticodeSignerInfo(ref signer);
				X509Native.UnsafeNativeMethods.CertFreeAuthenticodeTimestamperInfo(ref timestamper);
			}
			if (authenticodeSignatureInformation.SigningCertificate == null)
			{
				return new AuthenticodeSignatureInformation(authenticodeSignatureInformation.VerificationResult);
			}
			SignatureVerificationResult signatureVerificationResult3 = this.VerifyAuthenticodePublisher(authenticodeSignatureInformation.SigningCertificate);
			if (signatureVerificationResult3 != SignatureVerificationResult.Valid)
			{
				return new AuthenticodeSignatureInformation(signatureVerificationResult3);
			}
			return authenticodeSignatureInformation;
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0001FC9C File Offset: 0x0001DE9C
		private SignatureVerificationResult VerifyAuthenticodeSignatureIdentity(XmlElement licenseNode)
		{
			XmlElement xmlElement = licenseNode.SelectSingleNode("r:grant/as:ManifestInformation/as:assemblyIdentity", this.m_namespaceManager) as XmlElement;
			XmlElement xmlElement2 = this.m_manifestXml.SelectSingleNode("//asm:assembly/asm:assemblyIdentity", this.m_namespaceManager) as XmlElement;
			bool flag = xmlElement2 != null && xmlElement2.HasAttributes;
			bool flag2 = xmlElement != null && xmlElement.HasAttributes;
			if (!flag || !flag2 || xmlElement2.Attributes.Count != xmlElement.Attributes.Count)
			{
				return SignatureVerificationResult.BadSignatureFormat;
			}
			foreach (object obj in xmlElement2.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string attribute = xmlElement.GetAttribute(xmlAttribute.LocalName);
				if (attribute == null || string.Compare(xmlAttribute.Value, attribute, StringComparison.Ordinal) != 0)
				{
					return SignatureVerificationResult.AssemblyIdentityMismatch;
				}
			}
			return SignatureVerificationResult.Valid;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001FD94 File Offset: 0x0001DF94
		private static SignatureVerificationResult VerifyStrongNameSignatureId(XmlElement signatureNode)
		{
			string text = null;
			int num = 0;
			while (num < signatureNode.Attributes.Count && text == null)
			{
				if (string.Compare(signatureNode.Attributes[num].LocalName, "id", StringComparison.OrdinalIgnoreCase) == 0)
				{
					text = signatureNode.Attributes[num].Value;
				}
				num++;
			}
			if (string.IsNullOrEmpty(text))
			{
				return SignatureVerificationResult.BadSignatureFormat;
			}
			if (string.Compare(text, "StrongNameSignature", StringComparison.Ordinal) != 0)
			{
				return SignatureVerificationResult.BadSignatureFormat;
			}
			return SignatureVerificationResult.Valid;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001FE10 File Offset: 0x0001E010
		private static SignatureVerificationResult VerifyStrongNameSignatureTransforms(SignedInfo signedInfo)
		{
			int num = 0;
			foreach (object obj in signedInfo.References)
			{
				Reference reference = (Reference)obj;
				TransformChain transformChain = reference.TransformChain;
				bool flag;
				if (string.IsNullOrEmpty(reference.Uri))
				{
					num++;
					flag = (transformChain != null && transformChain.Count == 2 && string.Compare(transformChain[0].Algorithm, "http://www.w3.org/2000/09/xmldsig#enveloped-signature", StringComparison.Ordinal) == 0 && string.Compare(transformChain[1].Algorithm, "http://www.w3.org/2001/10/xml-exc-c14n#", StringComparison.Ordinal) == 0);
				}
				else if (string.Compare(reference.Uri, "#StrongNameKeyInfo", StringComparison.Ordinal) == 0)
				{
					num++;
					flag = (transformChain != null && transformChain.Count == 1 && string.Compare(transformChain[0].Algorithm, "http://www.w3.org/2001/10/xml-exc-c14n#", StringComparison.Ordinal) == 0);
				}
				else
				{
					flag = true;
				}
				if (!flag)
				{
					return SignatureVerificationResult.BadSignatureFormat;
				}
			}
			if (num == 0)
			{
				return SignatureVerificationResult.BadSignatureFormat;
			}
			return SignatureVerificationResult.Valid;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0001FF30 File Offset: 0x0001E130
		private StrongNameSignatureInformation VerifyStrongNameSignature(XmlElement signatureNode)
		{
			AsymmetricAlgorithm asymmetricAlgorithm;
			if (!base.CheckSignatureReturningKey(out asymmetricAlgorithm))
			{
				return new StrongNameSignatureInformation(SignatureVerificationResult.BadDigest);
			}
			SignatureVerificationResult signatureVerificationResult = ManifestSignedXml.VerifyStrongNameSignatureId(signatureNode);
			if (signatureVerificationResult != SignatureVerificationResult.Valid)
			{
				return new StrongNameSignatureInformation(signatureVerificationResult);
			}
			SignatureVerificationResult signatureVerificationResult2 = ManifestSignedXml.VerifyStrongNameSignatureTransforms(base.Signature.SignedInfo);
			if (signatureVerificationResult2 != SignatureVerificationResult.Valid)
			{
				return new StrongNameSignatureInformation(signatureVerificationResult2);
			}
			if (!ManifestSignedXml.CompareBytes(this.CalculateManifestPublicKeyToken(), ManifestSignedXml.CalculateSignerPublicKeyToken(asymmetricAlgorithm)))
			{
				return new StrongNameSignatureInformation(SignatureVerificationResult.PublicKeyTokenMismatch);
			}
			return new StrongNameSignatureInformation(asymmetricAlgorithm);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0001FFA0 File Offset: 0x0001E1A0
		public ManifestSignatureInformation VerifySignature(X509RevocationFlag revocationFlag, X509RevocationMode revocationMode)
		{
			XmlElement xmlElement = this.m_manifestXml.SelectSingleNode("//ds:Signature", this.m_namespaceManager) as XmlElement;
			if (xmlElement == null)
			{
				return new ManifestSignatureInformation(this.m_manifest, null, null);
			}
			base.LoadXml(xmlElement);
			StrongNameSignatureInformation strongNameSignatureInformation = this.VerifyStrongNameSignature(xmlElement);
			AuthenticodeSignatureInformation authenticodeSignature;
			if (strongNameSignatureInformation.VerificationResult != SignatureVerificationResult.BadDigest)
			{
				authenticodeSignature = this.VerifyAuthenticodeSignature(xmlElement, revocationFlag, revocationMode);
			}
			else
			{
				authenticodeSignature = new AuthenticodeSignatureInformation(SignatureVerificationResult.ContainingSignatureInvalid);
			}
			return new ManifestSignatureInformation(this.m_manifest, strongNameSignatureInformation, authenticodeSignature);
		}

		// Token: 0x040006EC RID: 1772
		private ManifestKinds m_manifest;

		// Token: 0x040006ED RID: 1773
		private XmlDocument m_manifestXml;

		// Token: 0x040006EE RID: 1774
		private XmlNamespaceManager m_namespaceManager;
	}
}
