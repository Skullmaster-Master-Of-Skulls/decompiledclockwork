using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000080 RID: 128
	internal abstract class SignedInfo : ISecurityElement
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x000108FC File Offset: 0x0000EAFC
		protected SignedInfo(DictionaryManager dictionaryManager)
		{
			if (dictionaryManager == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("dictionaryManager");
			}
			this.signatureMethodElement = new ElementWithAlgorithmAttribute(dictionaryManager.XmlSignatureDictionary.SignatureMethod);
			this.dictionaryManager = dictionaryManager;
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00010952 File Offset: 0x0000EB52
		protected DictionaryManager DictionaryManager
		{
			get
			{
				return this.dictionaryManager;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0001095A File Offset: 0x0000EB5A
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x00010962 File Offset: 0x0000EB62
		protected MemoryStream CanonicalStream
		{
			get
			{
				return this.canonicalStream;
			}
			set
			{
				this.canonicalStream = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0001096B File Offset: 0x0000EB6B
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x00010973 File Offset: 0x0000EB73
		protected bool SendSide
		{
			get
			{
				return this.sendSide;
			}
			set
			{
				this.sendSide = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001097C File Offset: 0x0000EB7C
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x00010984 File Offset: 0x0000EB84
		public ISignatureReaderProvider ReaderProvider
		{
			get
			{
				return this.readerProvider;
			}
			set
			{
				this.readerProvider = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0001098D File Offset: 0x0000EB8D
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x00010995 File Offset: 0x0000EB95
		public object SignatureReaderProviderCallbackContext
		{
			get
			{
				return this.signatureReaderProviderCallbackContext;
			}
			set
			{
				this.signatureReaderProviderCallbackContext = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0001099E File Offset: 0x0000EB9E
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x000109AB File Offset: 0x0000EBAB
		public string CanonicalizationMethod
		{
			get
			{
				return this.canonicalizationMethodElement.Algorithm;
			}
			set
			{
				if (value != this.canonicalizationMethodElement.Algorithm)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedTransformAlgorithm")));
				}
			}
		}

		// Token: 0x17000103 RID: 259
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x000109DA File Offset: 0x0000EBDA
		public XmlDictionaryString CanonicalizationMethodDictionaryString
		{
			set
			{
				if (value != null && value.Value != this.canonicalizationMethodElement.Algorithm)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedTransformAlgorithm")));
				}
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00002434 File Offset: 0x00000634
		public bool HasId
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00010A11 File Offset: 0x0000EC11
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x00010A19 File Offset: 0x0000EC19
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

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000476 RID: 1142
		public abstract int ReferenceCount { get; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00010A22 File Offset: 0x0000EC22
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x00010A2F File Offset: 0x0000EC2F
		public string SignatureMethod
		{
			get
			{
				return this.signatureMethodElement.Algorithm;
			}
			set
			{
				this.signatureMethodElement.Algorithm = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00010A3D File Offset: 0x0000EC3D
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x00010A4A File Offset: 0x0000EC4A
		public XmlDictionaryString SignatureMethodDictionaryString
		{
			get
			{
				return this.signatureMethodElement.AlgorithmDictionaryString;
			}
			set
			{
				this.signatureMethodElement.AlgorithmDictionaryString = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00010A58 File Offset: 0x0000EC58
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x00010A73 File Offset: 0x0000EC73
		public SignatureResourcePool ResourcePool
		{
			get
			{
				if (this.resourcePool == null)
				{
					this.resourcePool = new SignatureResourcePool();
				}
				return this.resourcePool;
			}
			set
			{
				this.resourcePool = value;
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00010A7C File Offset: 0x0000EC7C
		public void ComputeHash(HashAlgorithm algorithm)
		{
			if (this.CanonicalizationMethod != "http://www.w3.org/2001/10/xml-exc-c14n#" && this.CanonicalizationMethod != "http://www.w3.org/2001/10/xml-exc-c14n#WithComments")
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnsupportedTransformAlgorithm")));
			}
			HashStream hashStream = this.ResourcePool.TakeHashStream(algorithm);
			this.ComputeHash(hashStream);
			hashStream.FlushHash();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00010AE4 File Offset: 0x0000ECE4
		protected virtual void ComputeHash(HashStream hashStream)
		{
			if (this.sendSide)
			{
				XmlDictionaryWriter xmlDictionaryWriter = this.ResourcePool.TakeUtf8Writer();
				xmlDictionaryWriter.StartCanonicalization(hashStream, false, null);
				this.WriteTo(xmlDictionaryWriter, this.dictionaryManager);
				xmlDictionaryWriter.EndCanonicalization();
				return;
			}
			if (this.canonicalStream != null)
			{
				this.canonicalStream.WriteTo(hashStream);
				return;
			}
			if (this.readerProvider == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("InclusiveNamespacePrefixRequiresSignatureReader")));
			}
			XmlDictionaryReader xmlDictionaryReader = this.readerProvider.GetReader(this.signatureReaderProviderCallbackContext);
			if (!xmlDictionaryReader.CanCanonicalize)
			{
				MemoryStream memoryStream = new MemoryStream();
				XmlDictionaryWriter xmlDictionaryWriter2 = XmlDictionaryWriter.CreateBinaryWriter(memoryStream, this.DictionaryManager.ParentDictionary);
				string[] inclusivePrefixes = this.GetInclusivePrefixes();
				if (inclusivePrefixes != null)
				{
					xmlDictionaryWriter2.WriteStartElement("a");
					for (int i = 0; i < inclusivePrefixes.Length; i++)
					{
						string namespaceForInclusivePrefix = this.GetNamespaceForInclusivePrefix(inclusivePrefixes[i]);
						if (namespaceForInclusivePrefix != null)
						{
							xmlDictionaryWriter2.WriteXmlnsAttribute(inclusivePrefixes[i], namespaceForInclusivePrefix);
						}
					}
				}
				xmlDictionaryReader.MoveToContent();
				xmlDictionaryWriter2.WriteNode(xmlDictionaryReader, false);
				if (inclusivePrefixes != null)
				{
					xmlDictionaryWriter2.WriteEndElement();
				}
				xmlDictionaryWriter2.Flush();
				byte[] buffer = memoryStream.ToArray();
				int count = (int)memoryStream.Length;
				xmlDictionaryWriter2.Close();
				xmlDictionaryReader.Close();
				xmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader(buffer, 0, count, this.DictionaryManager.ParentDictionary, XmlDictionaryReaderQuotas.Max);
				if (inclusivePrefixes != null)
				{
					xmlDictionaryReader.ReadStartElement("a");
				}
			}
			xmlDictionaryReader.ReadStartElement(this.dictionaryManager.XmlSignatureDictionary.Signature, this.dictionaryManager.XmlSignatureDictionary.Namespace);
			xmlDictionaryReader.MoveToStartElement(this.dictionaryManager.XmlSignatureDictionary.SignedInfo, this.dictionaryManager.XmlSignatureDictionary.Namespace);
			xmlDictionaryReader.StartCanonicalization(hashStream, false, this.GetInclusivePrefixes());
			xmlDictionaryReader.Skip();
			xmlDictionaryReader.EndCanonicalization();
			xmlDictionaryReader.Close();
		}

		// Token: 0x0600047F RID: 1151
		public abstract void ComputeReferenceDigests();

		// Token: 0x06000480 RID: 1152 RVA: 0x00010CA8 File Offset: 0x0000EEA8
		protected string[] GetInclusivePrefixes()
		{
			return this.canonicalizationMethodElement.GetInclusivePrefixes();
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00002D0C File Offset: 0x00000F0C
		protected virtual string GetNamespaceForInclusivePrefix(string prefix)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x06000482 RID: 1154
		public abstract void EnsureAllReferencesVerified();

		// Token: 0x06000483 RID: 1155 RVA: 0x00010CB5 File Offset: 0x0000EEB5
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

		// Token: 0x06000484 RID: 1156
		public abstract bool EnsureDigestValidityIfIdMatches(string id, object resolvedXmlSource);

		// Token: 0x06000485 RID: 1157 RVA: 0x00002D0C File Offset: 0x00000F0C
		public virtual bool HasUnverifiedReference(string id)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00010CE5 File Offset: 0x0000EEE5
		protected void ReadCanonicalizationMethod(XmlDictionaryReader reader, DictionaryManager dictionaryManager)
		{
			this.canonicalizationMethodElement.ReadFrom(reader, dictionaryManager, false);
		}

		// Token: 0x06000487 RID: 1159
		public abstract void ReadFrom(XmlDictionaryReader reader, TransformFactory transformFactory, DictionaryManager dictionaryManager);

		// Token: 0x06000488 RID: 1160 RVA: 0x00010CF5 File Offset: 0x0000EEF5
		protected void ReadSignatureMethod(XmlDictionaryReader reader, DictionaryManager dictionaryManager)
		{
			this.signatureMethodElement.ReadFrom(reader, dictionaryManager);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00010D04 File Offset: 0x0000EF04
		protected void WriteCanonicalizationMethod(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			this.canonicalizationMethodElement.WriteTo(writer, dictionaryManager);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00010D13 File Offset: 0x0000EF13
		protected void WriteSignatureMethod(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			this.signatureMethodElement.WriteTo(writer, dictionaryManager);
		}

		// Token: 0x0600048B RID: 1163
		public abstract void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager);

		// Token: 0x040003A8 RID: 936
		private readonly ExclusiveCanonicalizationTransform canonicalizationMethodElement = new ExclusiveCanonicalizationTransform(true);

		// Token: 0x040003A9 RID: 937
		private string id;

		// Token: 0x040003AA RID: 938
		private ElementWithAlgorithmAttribute signatureMethodElement;

		// Token: 0x040003AB RID: 939
		private SignatureResourcePool resourcePool;

		// Token: 0x040003AC RID: 940
		private DictionaryManager dictionaryManager;

		// Token: 0x040003AD RID: 941
		private MemoryStream canonicalStream;

		// Token: 0x040003AE RID: 942
		private ISignatureReaderProvider readerProvider;

		// Token: 0x040003AF RID: 943
		private object signatureReaderProviderCallbackContext;

		// Token: 0x040003B0 RID: 944
		private bool sendSide = true;
	}
}
