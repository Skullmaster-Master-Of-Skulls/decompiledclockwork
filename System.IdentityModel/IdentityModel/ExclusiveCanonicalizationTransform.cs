using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x0200003F RID: 63
	internal class ExclusiveCanonicalizationTransform : Transform
	{
		// Token: 0x06000248 RID: 584 RVA: 0x00009F62 File Offset: 0x00008162
		public ExclusiveCanonicalizationTransform() : this(false)
		{
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00009F6B File Offset: 0x0000816B
		public ExclusiveCanonicalizationTransform(bool isCanonicalizationMethod) : this(isCanonicalizationMethod, false)
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009F78 File Offset: 0x00008178
		public ExclusiveCanonicalizationTransform(bool isCanonicalizationMethod, bool includeComments)
		{
			this.isCanonicalizationMethod = isCanonicalizationMethod;
			this.includeComments = includeComments;
			this.algorithm = (includeComments ? XD.SecurityAlgorithmDictionary.ExclusiveC14nWithComments.Value : XD.SecurityAlgorithmDictionary.ExclusiveC14n.Value);
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00009FD8 File Offset: 0x000081D8
		public override string Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00009FE0 File Offset: 0x000081E0
		public bool IncludeComments
		{
			get
			{
				return this.includeComments;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00009FE8 File Offset: 0x000081E8
		// (set) Token: 0x0600024E RID: 590 RVA: 0x00009FF0 File Offset: 0x000081F0
		public string InclusiveNamespacesPrefixList
		{
			get
			{
				return this.inclusiveNamespacesPrefixList;
			}
			set
			{
				this.inclusiveNamespacesPrefixList = value;
				this.inclusivePrefixes = ExclusiveCanonicalizationTransform.TokenizeInclusivePrefixList(value);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000A005 File Offset: 0x00008205
		public override bool NeedsInclusiveContext
		{
			get
			{
				return this.GetInclusivePrefixes() != null;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000A010 File Offset: 0x00008210
		public string[] GetInclusivePrefixes()
		{
			return this.inclusivePrefixes;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000A018 File Offset: 0x00008218
		private CanonicalizationDriver GetConfiguredDriver(SignatureResourcePool resourcePool)
		{
			CanonicalizationDriver canonicalizationDriver = resourcePool.TakeCanonicalizationDriver();
			canonicalizationDriver.IncludeComments = this.IncludeComments;
			canonicalizationDriver.SetInclusivePrefixes(this.inclusivePrefixes);
			return canonicalizationDriver;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000A048 File Offset: 0x00008248
		public override object Process(object input, SignatureResourcePool resourcePool, DictionaryManager dictionaryManager)
		{
			if (input is XmlReader)
			{
				CanonicalizationDriver configuredDriver = this.GetConfiguredDriver(resourcePool);
				configuredDriver.SetInput(input as XmlReader);
				return configuredDriver.GetMemoryStream();
			}
			if (input is ISecurityElement)
			{
				MemoryStream memoryStream = new MemoryStream();
				XmlDictionaryWriter xmlDictionaryWriter = resourcePool.TakeUtf8Writer();
				xmlDictionaryWriter.StartCanonicalization(memoryStream, false, null);
				(input as ISecurityElement).WriteTo(xmlDictionaryWriter, dictionaryManager);
				xmlDictionaryWriter.EndCanonicalization();
				memoryStream.Seek(0L, SeekOrigin.Begin);
				return memoryStream;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedInputTypeForTransform", new object[]
			{
				input.GetType()
			})));
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000A0E0 File Offset: 0x000082E0
		public override byte[] ProcessAndDigest(object input, SignatureResourcePool resourcePool, string digestAlgorithm, DictionaryManager dictionaryManager)
		{
			HashAlgorithm hashAlgorithm = resourcePool.TakeHashAlgorithm(digestAlgorithm);
			this.ProcessAndDigest(input, resourcePool, hashAlgorithm, dictionaryManager);
			return hashAlgorithm.Hash;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000A108 File Offset: 0x00008308
		public void ProcessAndDigest(object input, SignatureResourcePool resourcePool, HashAlgorithm hash, DictionaryManager dictionaryManger)
		{
			HashStream hashStream = resourcePool.TakeHashStream(hash);
			XmlReader xmlReader = input as XmlReader;
			if (xmlReader != null)
			{
				this.ProcessReaderInput(xmlReader, resourcePool, hashStream);
			}
			else
			{
				if (!(input is ISecurityElement))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedInputTypeForTransform", new object[]
					{
						input.GetType()
					})));
				}
				XmlDictionaryWriter xmlDictionaryWriter = resourcePool.TakeUtf8Writer();
				xmlDictionaryWriter.StartCanonicalization(hashStream, this.IncludeComments, this.GetInclusivePrefixes());
				(input as ISecurityElement).WriteTo(xmlDictionaryWriter, dictionaryManger);
				xmlDictionaryWriter.EndCanonicalization();
			}
			hashStream.FlushHash();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000A19C File Offset: 0x0000839C
		private void ProcessReaderInput(XmlReader reader, SignatureResourcePool resourcePool, HashStream hashStream)
		{
			reader.MoveToContent();
			XmlDictionaryReader xmlDictionaryReader = reader as XmlDictionaryReader;
			if (xmlDictionaryReader != null && xmlDictionaryReader.CanCanonicalize)
			{
				xmlDictionaryReader.StartCanonicalization(hashStream, this.IncludeComments, this.GetInclusivePrefixes());
				xmlDictionaryReader.Skip();
				xmlDictionaryReader.EndCanonicalization();
				return;
			}
			CanonicalizationDriver configuredDriver = this.GetConfiguredDriver(resourcePool);
			configuredDriver.SetInput(reader);
			configuredDriver.WriteTo(hashStream);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000A1F8 File Offset: 0x000083F8
		public override void ReadFrom(XmlDictionaryReader reader, DictionaryManager dictionaryManager, bool preserveComments)
		{
			XmlDictionaryString localName = this.isCanonicalizationMethod ? dictionaryManager.XmlSignatureDictionary.CanonicalizationMethod : dictionaryManager.XmlSignatureDictionary.Transform;
			reader.MoveToStartElement(localName, dictionaryManager.XmlSignatureDictionary.Namespace);
			this.prefix = reader.Prefix;
			bool isEmptyElement = reader.IsEmptyElement;
			this.algorithm = reader.GetAttribute(dictionaryManager.XmlSignatureDictionary.Algorithm, null);
			if (string.IsNullOrEmpty(this.algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("ID0001", new object[]
				{
					dictionaryManager.XmlSignatureDictionary.Algorithm,
					reader.LocalName
				})));
			}
			if (this.algorithm == dictionaryManager.SecurityAlgorithmDictionary.ExclusiveC14nWithComments.Value)
			{
				this.includeComments = preserveComments;
			}
			else
			{
				if (!(this.algorithm == dictionaryManager.SecurityAlgorithmDictionary.ExclusiveC14n.Value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("ID6005", new object[]
					{
						this.algorithm
					})));
				}
				this.includeComments = false;
			}
			reader.Read();
			reader.MoveToContent();
			if (!isEmptyElement)
			{
				if (reader.IsStartElement(dictionaryManager.ExclusiveC14NDictionary.InclusiveNamespaces, dictionaryManager.ExclusiveC14NDictionary.Namespace))
				{
					reader.MoveToStartElement(dictionaryManager.ExclusiveC14NDictionary.InclusiveNamespaces, dictionaryManager.ExclusiveC14NDictionary.Namespace);
					this.inclusiveListElementPrefix = reader.Prefix;
					bool isEmptyElement2 = reader.IsEmptyElement;
					this.InclusiveNamespacesPrefixList = reader.GetAttribute(dictionaryManager.ExclusiveC14NDictionary.PrefixList, null);
					reader.Read();
					if (!isEmptyElement2)
					{
						reader.ReadEndElement();
					}
				}
				reader.MoveToContent();
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000A3B4 File Offset: 0x000085B4
		public override void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			XmlDictionaryString localName = this.isCanonicalizationMethod ? dictionaryManager.XmlSignatureDictionary.CanonicalizationMethod : dictionaryManager.XmlSignatureDictionary.Transform;
			writer.WriteStartElement(this.prefix, localName, dictionaryManager.XmlSignatureDictionary.Namespace);
			writer.WriteAttributeString(dictionaryManager.XmlSignatureDictionary.Algorithm, null, this.algorithm);
			if (this.InclusiveNamespacesPrefixList != null)
			{
				writer.WriteStartElement(this.inclusiveListElementPrefix, dictionaryManager.ExclusiveC14NDictionary.InclusiveNamespaces, dictionaryManager.ExclusiveC14NDictionary.Namespace);
				writer.WriteAttributeString(dictionaryManager.ExclusiveC14NDictionary.PrefixList, null, this.InclusiveNamespacesPrefixList);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A460 File Offset: 0x00008660
		private static string[] TokenizeInclusivePrefixList(string prefixList)
		{
			if (prefixList == null)
			{
				return null;
			}
			string[] array = prefixList.Split(null);
			int num = 0;
			foreach (string text in array)
			{
				if (text == "#default")
				{
					array[num++] = string.Empty;
				}
				else if (text.Length > 0)
				{
					array[num++] = text;
				}
			}
			if (num == 0)
			{
				return null;
			}
			if (num == array.Length)
			{
				return array;
			}
			string[] array2 = new string[num];
			Array.Copy(array, array2, num);
			return array2;
		}

		// Token: 0x04000169 RID: 361
		private bool includeComments;

		// Token: 0x0400016A RID: 362
		private string algorithm;

		// Token: 0x0400016B RID: 363
		private string inclusiveNamespacesPrefixList;

		// Token: 0x0400016C RID: 364
		private string[] inclusivePrefixes;

		// Token: 0x0400016D RID: 365
		private string inclusiveListElementPrefix = "ec";

		// Token: 0x0400016E RID: 366
		private string prefix = "";

		// Token: 0x0400016F RID: 367
		private readonly bool isCanonicalizationMethod;
	}
}
