using System;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000083 RID: 131
	internal sealed class Reference
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x00011488 File Offset: 0x0000F688
		public Reference(DictionaryManager dictionaryManager) : this(dictionaryManager, null)
		{
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00011492 File Offset: 0x0000F692
		public Reference(DictionaryManager dictionaryManager, string uri) : this(dictionaryManager, uri, null)
		{
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x000114A0 File Offset: 0x0000F6A0
		public Reference(DictionaryManager dictionaryManager, string uri, object resolvedXmlSource)
		{
			if (dictionaryManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionaryManager");
			}
			this.dictionaryManager = dictionaryManager;
			this.digestMethodElement = new ElementWithAlgorithmAttribute(dictionaryManager.XmlSignatureDictionary.DigestMethod);
			this.uri = uri;
			this.resolvedXmlSource = resolvedXmlSource;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00011507 File Offset: 0x0000F707
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x00011514 File Offset: 0x0000F714
		public string DigestMethod
		{
			get
			{
				return this.digestMethodElement.Algorithm;
			}
			set
			{
				this.digestMethodElement.Algorithm = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00011522 File Offset: 0x0000F722
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0001152F File Offset: 0x0000F72F
		public XmlDictionaryString DigestMethodDictionaryString
		{
			get
			{
				return this.digestMethodElement.AlgorithmDictionaryString;
			}
			set
			{
				this.digestMethodElement.AlgorithmDictionaryString = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0001153D File Offset: 0x0000F73D
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x00011545 File Offset: 0x0000F745
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001154E File Offset: 0x0000F74E
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x00011556 File Offset: 0x0000F756
		public SignatureResourcePool ResourcePool
		{
			get
			{
				return this.resourcePool;
			}
			set
			{
				this.resourcePool = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0001155F File Offset: 0x0000F75F
		public TransformChain TransformChain
		{
			get
			{
				return this.transformChain;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00011567 File Offset: 0x0000F767
		public int TransformCount
		{
			get
			{
				return this.transformChain.TransformCount;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00011574 File Offset: 0x0000F774
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0001157C File Offset: 0x0000F77C
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00011585 File Offset: 0x0000F785
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0001158D File Offset: 0x0000F78D
		public string Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00011596 File Offset: 0x0000F796
		public bool Verified
		{
			get
			{
				return this.verified;
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001159E File Offset: 0x0000F79E
		public void AddTransform(Transform transform)
		{
			this.transformChain.Add(transform);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000115AC File Offset: 0x0000F7AC
		public void EnsureDigestValidity(string id, byte[] computedDigest)
		{
			if (!this.EnsureDigestValidityIfIdMatches(id, computedDigest))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("RequiredTargetNotSigned", new object[]
				{
					id
				})));
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000115DC File Offset: 0x0000F7DC
		public void EnsureDigestValidity(string id, object resolvedXmlSource)
		{
			if (!this.EnsureDigestValidityIfIdMatches(id, resolvedXmlSource))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("RequiredTargetNotSigned", new object[]
				{
					id
				})));
			}
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001160C File Offset: 0x0000F80C
		public bool EnsureDigestValidityIfIdMatches(string id, byte[] computedDigest)
		{
			if (this.verified || id != this.ExtractReferredId())
			{
				return false;
			}
			if (!CryptoHelper.FixedTimeEquals(computedDigest, this.GetDigestValue()))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("DigestVerificationFailedForReference", new object[]
				{
					this.uri
				})));
			}
			this.verified = true;
			return true;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00011670 File Offset: 0x0000F870
		public bool EnsureDigestValidityIfIdMatches(string id, object resolvedXmlSource)
		{
			if (this.verified)
			{
				return false;
			}
			if (id != this.ExtractReferredId() && !this.IsStrTranform())
			{
				return false;
			}
			this.resolvedXmlSource = resolvedXmlSource;
			if (!this.CheckDigest())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("DigestVerificationFailedForReference", new object[]
				{
					this.uri
				})));
			}
			this.verified = true;
			return true;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000116DF File Offset: 0x0000F8DF
		public bool IsStrTranform()
		{
			return this.TransformChain.TransformCount == 1 && this.TransformChain[0].Algorithm == "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#STR-Transform";
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001170C File Offset: 0x0000F90C
		public string ExtractReferredId()
		{
			if (this.referredId == null)
			{
				if (StringComparer.OrdinalIgnoreCase.Equals(this.uri, string.Empty))
				{
					return string.Empty;
				}
				if (this.uri == null || this.uri.Length < 2 || this.uri[0] != '#')
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnableToResolveReferenceUriForSignature", new object[]
					{
						this.uri
					})));
				}
				this.referredId = this.uri.Substring(1);
			}
			return this.referredId;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000117A8 File Offset: 0x0000F9A8
		private static bool ShouldPreserveComments(string uri)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(uri))
			{
				string text = uri.Substring(1);
				if (text == "xpointer(/)")
				{
					result = true;
				}
				else if (text.StartsWith("xpointer(id(", StringComparison.Ordinal) && text.IndexOf(")", StringComparison.Ordinal) > 0)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000117F8 File Offset: 0x0000F9F8
		public bool CheckDigest()
		{
			byte[] a = this.ComputeDigest();
			return CryptoHelper.FixedTimeEquals(a, this.GetDigestValue());
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001181A File Offset: 0x0000FA1A
		public void ComputeAndSetDigest()
		{
			this.digestValueElement.Value = this.ComputeDigest();
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00011830 File Offset: 0x0000FA30
		public byte[] ComputeDigest()
		{
			if (this.transformChain.TransformCount == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("EmptyTransformChainNotSupported")));
			}
			if (this.resolvedXmlSource == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnableToResolveReferenceUriForSignature", new object[]
				{
					this.uri
				})));
			}
			return this.transformChain.TransformToDigest(this.resolvedXmlSource, this.ResourcePool, this.DigestMethod, this.dictionaryManager);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000118B8 File Offset: 0x0000FAB8
		public byte[] GetDigestValue()
		{
			return this.digestValueElement.Value;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000118C8 File Offset: 0x0000FAC8
		public void ReadFrom(XmlDictionaryReader reader, TransformFactory transformFactory, DictionaryManager dictionaryManager)
		{
			reader.MoveToStartElement(dictionaryManager.XmlSignatureDictionary.Reference, dictionaryManager.XmlSignatureDictionary.Namespace);
			this.prefix = reader.Prefix;
			this.Id = reader.GetAttribute("Id", null);
			this.Uri = reader.GetAttribute(dictionaryManager.XmlSignatureDictionary.URI, null);
			this.Type = reader.GetAttribute(dictionaryManager.XmlSignatureDictionary.Type, null);
			reader.Read();
			if (reader.IsStartElement(dictionaryManager.XmlSignatureDictionary.Transforms, dictionaryManager.XmlSignatureDictionary.Namespace))
			{
				this.transformChain.ReadFrom(reader, transformFactory, dictionaryManager, Reference.ShouldPreserveComments(this.Uri));
			}
			this.digestMethodElement.ReadFrom(reader, dictionaryManager);
			this.digestValueElement.ReadFrom(reader, dictionaryManager);
			reader.MoveToContent();
			reader.ReadEndElement();
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000119A4 File Offset: 0x0000FBA4
		public void SetResolvedXmlSource(object resolvedXmlSource)
		{
			this.resolvedXmlSource = resolvedXmlSource;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000119B0 File Offset: 0x0000FBB0
		public void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			writer.WriteStartElement(this.prefix, dictionaryManager.XmlSignatureDictionary.Reference, dictionaryManager.XmlSignatureDictionary.Namespace);
			if (this.id != null)
			{
				writer.WriteAttributeString(dictionaryManager.UtilityDictionary.IdAttribute, null, this.id);
			}
			if (this.uri != null)
			{
				writer.WriteAttributeString(dictionaryManager.XmlSignatureDictionary.URI, null, this.uri);
			}
			if (this.type != null)
			{
				writer.WriteAttributeString(dictionaryManager.XmlSignatureDictionary.Type, null, this.type);
			}
			if (this.transformChain.TransformCount > 0)
			{
				this.transformChain.WriteTo(writer, dictionaryManager);
			}
			this.digestMethodElement.WriteTo(writer, dictionaryManager);
			this.digestValueElement.WriteTo(writer, dictionaryManager);
			writer.WriteEndElement();
		}

		// Token: 0x040003B7 RID: 951
		private ElementWithAlgorithmAttribute digestMethodElement;

		// Token: 0x040003B8 RID: 952
		private Reference.DigestValueElement digestValueElement;

		// Token: 0x040003B9 RID: 953
		private string id;

		// Token: 0x040003BA RID: 954
		private string prefix = "";

		// Token: 0x040003BB RID: 955
		private object resolvedXmlSource;

		// Token: 0x040003BC RID: 956
		private readonly TransformChain transformChain = new TransformChain();

		// Token: 0x040003BD RID: 957
		private string type;

		// Token: 0x040003BE RID: 958
		private string uri;

		// Token: 0x040003BF RID: 959
		private SignatureResourcePool resourcePool;

		// Token: 0x040003C0 RID: 960
		private bool verified;

		// Token: 0x040003C1 RID: 961
		private string referredId;

		// Token: 0x040003C2 RID: 962
		private DictionaryManager dictionaryManager;

		// Token: 0x0200023C RID: 572
		private struct DigestValueElement
		{
			// Token: 0x1700050D RID: 1293
			// (get) Token: 0x06001229 RID: 4649 RVA: 0x0004FAF7 File Offset: 0x0004DCF7
			// (set) Token: 0x0600122A RID: 4650 RVA: 0x0004FAFF File Offset: 0x0004DCFF
			internal byte[] Value
			{
				get
				{
					return this.digestValue;
				}
				set
				{
					this.digestValue = value;
					this.digestText = null;
				}
			}

			// Token: 0x0600122B RID: 4651 RVA: 0x0004FB10 File Offset: 0x0004DD10
			public void ReadFrom(XmlDictionaryReader reader, DictionaryManager dictionaryManager)
			{
				reader.MoveToStartElement(dictionaryManager.XmlSignatureDictionary.DigestValue, dictionaryManager.XmlSignatureDictionary.Namespace);
				this.prefix = reader.Prefix;
				reader.Read();
				reader.MoveToContent();
				this.digestText = reader.ReadString();
				this.digestValue = Convert.FromBase64String(this.digestText.Trim());
				reader.MoveToContent();
				reader.ReadEndElement();
			}

			// Token: 0x0600122C RID: 4652 RVA: 0x0004FB84 File Offset: 0x0004DD84
			public void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
			{
				writer.WriteStartElement(this.prefix ?? "", dictionaryManager.XmlSignatureDictionary.DigestValue, dictionaryManager.XmlSignatureDictionary.Namespace);
				if (this.digestText != null)
				{
					writer.WriteString(this.digestText);
				}
				else
				{
					writer.WriteBase64(this.digestValue, 0, this.digestValue.Length);
				}
				writer.WriteEndElement();
			}

			// Token: 0x04000F63 RID: 3939
			private byte[] digestValue;

			// Token: 0x04000F64 RID: 3940
			private string digestText;

			// Token: 0x04000F65 RID: 3941
			private string prefix;
		}
	}
}
