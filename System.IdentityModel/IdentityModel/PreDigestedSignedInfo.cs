using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000065 RID: 101
	internal sealed class PreDigestedSignedInfo : SignedInfo
	{
		// Token: 0x0600031F RID: 799 RVA: 0x0000BEB2 File Offset: 0x0000A0B2
		public PreDigestedSignedInfo(DictionaryManager dictionaryManager) : base(dictionaryManager)
		{
			this.references = new PreDigestedSignedInfo.ReferenceEntry[8];
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000BEC8 File Offset: 0x0000A0C8
		public PreDigestedSignedInfo(DictionaryManager dictionaryManager, string canonicalizationMethod, XmlDictionaryString canonicalizationMethodDictionaryString, string digestMethod, XmlDictionaryString digestMethodDictionaryString, string signatureMethod, XmlDictionaryString signatureMethodDictionaryString) : base(dictionaryManager)
		{
			this.references = new PreDigestedSignedInfo.ReferenceEntry[8];
			base.CanonicalizationMethod = canonicalizationMethod;
			base.CanonicalizationMethodDictionaryString = canonicalizationMethodDictionaryString;
			this.DigestMethod = digestMethod;
			this.digestMethodDictionaryString = digestMethodDictionaryString;
			base.SignatureMethod = signatureMethod;
			base.SignatureMethodDictionaryString = signatureMethodDictionaryString;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000BF16 File Offset: 0x0000A116
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000BF1E File Offset: 0x0000A11E
		public bool AddEnvelopedSignatureTransform
		{
			get
			{
				return this.addEnvelopedSignatureTransform;
			}
			set
			{
				this.addEnvelopedSignatureTransform = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000BF27 File Offset: 0x0000A127
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000BF2F File Offset: 0x0000A12F
		public string DigestMethod
		{
			get
			{
				return this.digestMethod;
			}
			set
			{
				this.digestMethod = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000BF38 File Offset: 0x0000A138
		public override int ReferenceCount
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000BF40 File Offset: 0x0000A140
		public void AddReference(string id, byte[] digest)
		{
			this.AddReference(id, digest, false);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000BF4C File Offset: 0x0000A14C
		public void AddReference(string id, byte[] digest, bool useStrTransform)
		{
			if (!LocalAppContextSwitches.AllowUnlimitedXmlReferences)
			{
				long maxXmlReferencesPerSignedInfo = SecurityUtils.GetMaxXmlReferencesPerSignedInfo();
				if ((long)this.ReferenceCount > maxXmlReferencesPerSignedInfo)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException());
				}
			}
			if (this.count == this.references.Length)
			{
				PreDigestedSignedInfo.ReferenceEntry[] destinationArray = new PreDigestedSignedInfo.ReferenceEntry[this.references.Length * 2];
				Array.Copy(this.references, 0, destinationArray, 0, this.count);
				this.references = destinationArray;
			}
			PreDigestedSignedInfo.ReferenceEntry[] array = this.references;
			int num = this.count;
			this.count = num + 1;
			array[num].Set(id, digest, useStrTransform);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000BFE0 File Offset: 0x0000A1E0
		protected override void ComputeHash(HashStream hashStream)
		{
			if (this.AddEnvelopedSignatureTransform)
			{
				base.ComputeHash(hashStream);
				return;
			}
			PreDigestedSignedInfo.SignedInfoCanonicalFormWriter.Instance.WriteSignedInfoCanonicalForm(hashStream, base.SignatureMethod, this.DigestMethod, this.references, this.count, base.ResourcePool.TakeEncodingBuffer(), base.ResourcePool.TakeBase64Buffer());
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000024C1 File Offset: 0x000006C1
		public override void ComputeReferenceDigests()
		{
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00002D0C File Offset: 0x00000F0C
		public override void ReadFrom(XmlDictionaryReader reader, TransformFactory transformFactory, DictionaryManager dictionaryManager)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00002D0C File Offset: 0x00000F0C
		public override void EnsureAllReferencesVerified()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00002D0C File Offset: 0x00000F0C
		public override bool EnsureDigestValidityIfIdMatches(string id, object resolvedXmlSource)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000C038 File Offset: 0x0000A238
		public override void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			string prefix = "";
			XmlDictionaryString @namespace = dictionaryManager.XmlSignatureDictionary.Namespace;
			writer.WriteStartElement(prefix, dictionaryManager.XmlSignatureDictionary.SignedInfo, @namespace);
			if (base.Id != null)
			{
				writer.WriteAttributeString(dictionaryManager.UtilityDictionary.IdAttribute, null, base.Id);
			}
			base.WriteCanonicalizationMethod(writer, dictionaryManager);
			base.WriteSignatureMethod(writer, dictionaryManager);
			for (int i = 0; i < this.count; i++)
			{
				writer.WriteStartElement(prefix, dictionaryManager.XmlSignatureDictionary.Reference, @namespace);
				writer.WriteStartAttribute(dictionaryManager.XmlSignatureDictionary.URI, null);
				writer.WriteString("#");
				writer.WriteString(this.references[i].id);
				writer.WriteEndAttribute();
				writer.WriteStartElement(prefix, dictionaryManager.XmlSignatureDictionary.Transforms, @namespace);
				if (this.addEnvelopedSignatureTransform)
				{
					writer.WriteStartElement(prefix, dictionaryManager.XmlSignatureDictionary.Transform, @namespace);
					writer.WriteStartAttribute(dictionaryManager.XmlSignatureDictionary.Algorithm, null);
					writer.WriteString(dictionaryManager.XmlSignatureDictionary.EnvelopedSignature);
					writer.WriteEndAttribute();
					writer.WriteEndElement();
				}
				if (this.references[i].useStrTransform)
				{
					writer.WriteStartElement(prefix, dictionaryManager.XmlSignatureDictionary.Transform, @namespace);
					writer.WriteStartAttribute(dictionaryManager.XmlSignatureDictionary.Algorithm, null);
					writer.WriteString("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#STR-Transform");
					writer.WriteEndAttribute();
					writer.WriteStartElement("o", "TransformationParameters", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
					writer.WriteStartElement(prefix, dictionaryManager.XmlSignatureDictionary.CanonicalizationMethod, @namespace);
					writer.WriteStartAttribute(dictionaryManager.XmlSignatureDictionary.Algorithm, null);
					writer.WriteString(dictionaryManager.SecurityAlgorithmDictionary.ExclusiveC14n);
					writer.WriteEndAttribute();
					writer.WriteEndElement();
					writer.WriteEndElement();
					writer.WriteEndElement();
				}
				else
				{
					writer.WriteStartElement(prefix, dictionaryManager.XmlSignatureDictionary.Transform, @namespace);
					writer.WriteStartAttribute(dictionaryManager.XmlSignatureDictionary.Algorithm, null);
					writer.WriteString(dictionaryManager.SecurityAlgorithmDictionary.ExclusiveC14n);
					writer.WriteEndAttribute();
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
				writer.WriteStartElement(prefix, dictionaryManager.XmlSignatureDictionary.DigestMethod, @namespace);
				writer.WriteStartAttribute(dictionaryManager.XmlSignatureDictionary.Algorithm, null);
				if (this.digestMethodDictionaryString != null)
				{
					writer.WriteString(this.digestMethodDictionaryString);
				}
				else
				{
					writer.WriteString(this.digestMethod);
				}
				writer.WriteEndAttribute();
				writer.WriteEndElement();
				byte[] digest = this.references[i].digest;
				writer.WriteStartElement(prefix, dictionaryManager.XmlSignatureDictionary.DigestValue, @namespace);
				writer.WriteBase64(digest, 0, digest.Length);
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000342 RID: 834
		private const int InitialReferenceArraySize = 8;

		// Token: 0x04000343 RID: 835
		private bool addEnvelopedSignatureTransform;

		// Token: 0x04000344 RID: 836
		private int count;

		// Token: 0x04000345 RID: 837
		private string digestMethod;

		// Token: 0x04000346 RID: 838
		private XmlDictionaryString digestMethodDictionaryString;

		// Token: 0x04000347 RID: 839
		private PreDigestedSignedInfo.ReferenceEntry[] references;

		// Token: 0x02000235 RID: 565
		private struct ReferenceEntry
		{
			// Token: 0x060011FD RID: 4605 RVA: 0x0004EA85 File Offset: 0x0004CC85
			public void Set(string id, byte[] digest, bool useStrTransform)
			{
				if (useStrTransform && string.IsNullOrEmpty(id))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException(id));
				}
				this.id = id;
				this.digest = digest;
				this.useStrTransform = useStrTransform;
			}

			// Token: 0x04000F32 RID: 3890
			internal string id;

			// Token: 0x04000F33 RID: 3891
			internal byte[] digest;

			// Token: 0x04000F34 RID: 3892
			internal bool useStrTransform;
		}

		// Token: 0x02000236 RID: 566
		private sealed class SignedInfoCanonicalFormWriter : CanonicalFormWriter
		{
			// Token: 0x060011FE RID: 4606 RVA: 0x0004EAB8 File Offset: 0x0004CCB8
			private SignedInfoCanonicalFormWriter()
			{
				UTF8Encoding utf8WithoutPreamble = CanonicalFormWriter.Utf8WithoutPreamble;
				this.fragment1 = utf8WithoutPreamble.GetBytes("<SignedInfo xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"></CanonicalizationMethod><SignatureMethod Algorithm=\"");
				this.fragment2 = utf8WithoutPreamble.GetBytes("\"></SignatureMethod>");
				this.fragment3 = utf8WithoutPreamble.GetBytes("<Reference URI=\"#");
				this.fragment4 = utf8WithoutPreamble.GetBytes("\"><Transforms><Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"></Transform></Transforms><DigestMethod Algorithm=\"");
				this.fragment4StrTransform = utf8WithoutPreamble.GetBytes("\"><Transforms><Transform Algorithm=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#STR-Transform\"><o:TransformationParameters xmlns:o=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\"><CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"></CanonicalizationMethod></o:TransformationParameters></Transform></Transforms><DigestMethod Algorithm=\"");
				this.fragment5 = utf8WithoutPreamble.GetBytes("\"></DigestMethod><DigestValue>");
				this.fragment6 = utf8WithoutPreamble.GetBytes("</DigestValue></Reference>");
				this.fragment7 = utf8WithoutPreamble.GetBytes("</SignedInfo>");
				this.sha1Digest = utf8WithoutPreamble.GetBytes("http://www.w3.org/2000/09/xmldsig#sha1");
				this.sha256Digest = utf8WithoutPreamble.GetBytes("http://www.w3.org/2001/04/xmlenc#sha256");
				this.hmacSha1Signature = utf8WithoutPreamble.GetBytes("http://www.w3.org/2000/09/xmldsig#hmac-sha1");
				this.rsaSha1Signature = utf8WithoutPreamble.GetBytes("http://www.w3.org/2000/09/xmldsig#rsa-sha1");
			}

			// Token: 0x170004FD RID: 1277
			// (get) Token: 0x060011FF RID: 4607 RVA: 0x0004EB9D File Offset: 0x0004CD9D
			public static PreDigestedSignedInfo.SignedInfoCanonicalFormWriter Instance
			{
				get
				{
					return PreDigestedSignedInfo.SignedInfoCanonicalFormWriter.instance;
				}
			}

			// Token: 0x06001200 RID: 4608 RVA: 0x0004EBA4 File Offset: 0x0004CDA4
			private byte[] EncodeDigestAlgorithm(string algorithm)
			{
				if (algorithm == "http://www.w3.org/2000/09/xmldsig#sha1")
				{
					return this.sha1Digest;
				}
				if (algorithm == "http://www.w3.org/2001/04/xmlenc#sha256")
				{
					return this.sha256Digest;
				}
				return CanonicalFormWriter.Utf8WithoutPreamble.GetBytes(algorithm);
			}

			// Token: 0x06001201 RID: 4609 RVA: 0x0004EBD9 File Offset: 0x0004CDD9
			private byte[] EncodeSignatureAlgorithm(string algorithm)
			{
				if (algorithm == "http://www.w3.org/2000/09/xmldsig#hmac-sha1")
				{
					return this.hmacSha1Signature;
				}
				if (algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1")
				{
					return this.rsaSha1Signature;
				}
				return CanonicalFormWriter.Utf8WithoutPreamble.GetBytes(algorithm);
			}

			// Token: 0x06001202 RID: 4610 RVA: 0x0004EC10 File Offset: 0x0004CE10
			public void WriteSignedInfoCanonicalForm(Stream stream, string signatureMethod, string digestMethod, PreDigestedSignedInfo.ReferenceEntry[] references, int referenceCount, byte[] workBuffer, char[] base64WorkBuffer)
			{
				stream.Write(this.fragment1, 0, this.fragment1.Length);
				byte[] array = this.EncodeSignatureAlgorithm(signatureMethod);
				stream.Write(array, 0, array.Length);
				stream.Write(this.fragment2, 0, this.fragment2.Length);
				byte[] array2 = this.EncodeDigestAlgorithm(digestMethod);
				for (int i = 0; i < referenceCount; i++)
				{
					stream.Write(this.fragment3, 0, this.fragment3.Length);
					CanonicalFormWriter.EncodeAndWrite(stream, workBuffer, references[i].id);
					if (references[i].useStrTransform)
					{
						stream.Write(this.fragment4StrTransform, 0, this.fragment4StrTransform.Length);
					}
					else
					{
						stream.Write(this.fragment4, 0, this.fragment4.Length);
					}
					stream.Write(array2, 0, array2.Length);
					stream.Write(this.fragment5, 0, this.fragment5.Length);
					CanonicalFormWriter.Base64EncodeAndWrite(stream, workBuffer, base64WorkBuffer, references[i].digest);
					stream.Write(this.fragment6, 0, this.fragment6.Length);
				}
				stream.Write(this.fragment7, 0, this.fragment7.Length);
			}

			// Token: 0x04000F35 RID: 3893
			private const string xml1 = "<SignedInfo xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"></CanonicalizationMethod><SignatureMethod Algorithm=\"";

			// Token: 0x04000F36 RID: 3894
			private const string xml2 = "\"></SignatureMethod>";

			// Token: 0x04000F37 RID: 3895
			private const string xml3 = "<Reference URI=\"#";

			// Token: 0x04000F38 RID: 3896
			private const string xml4 = "\"><Transforms><Transform Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"></Transform></Transforms><DigestMethod Algorithm=\"";

			// Token: 0x04000F39 RID: 3897
			private const string xml4WithStrTransform = "\"><Transforms><Transform Algorithm=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#STR-Transform\"><o:TransformationParameters xmlns:o=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\"><CanonicalizationMethod Algorithm=\"http://www.w3.org/2001/10/xml-exc-c14n#\"></CanonicalizationMethod></o:TransformationParameters></Transform></Transforms><DigestMethod Algorithm=\"";

			// Token: 0x04000F3A RID: 3898
			private const string xml5 = "\"></DigestMethod><DigestValue>";

			// Token: 0x04000F3B RID: 3899
			private const string xml6 = "</DigestValue></Reference>";

			// Token: 0x04000F3C RID: 3900
			private const string xml7 = "</SignedInfo>";

			// Token: 0x04000F3D RID: 3901
			private readonly byte[] fragment1;

			// Token: 0x04000F3E RID: 3902
			private readonly byte[] fragment2;

			// Token: 0x04000F3F RID: 3903
			private readonly byte[] fragment3;

			// Token: 0x04000F40 RID: 3904
			private readonly byte[] fragment4;

			// Token: 0x04000F41 RID: 3905
			private readonly byte[] fragment4StrTransform;

			// Token: 0x04000F42 RID: 3906
			private readonly byte[] fragment5;

			// Token: 0x04000F43 RID: 3907
			private readonly byte[] fragment6;

			// Token: 0x04000F44 RID: 3908
			private readonly byte[] fragment7;

			// Token: 0x04000F45 RID: 3909
			private readonly byte[] sha1Digest;

			// Token: 0x04000F46 RID: 3910
			private readonly byte[] sha256Digest;

			// Token: 0x04000F47 RID: 3911
			private readonly byte[] hmacSha1Signature;

			// Token: 0x04000F48 RID: 3912
			private readonly byte[] rsaSha1Signature;

			// Token: 0x04000F49 RID: 3913
			private static readonly PreDigestedSignedInfo.SignedInfoCanonicalFormWriter instance = new PreDigestedSignedInfo.SignedInfoCanonicalFormWriter();
		}
	}
}
