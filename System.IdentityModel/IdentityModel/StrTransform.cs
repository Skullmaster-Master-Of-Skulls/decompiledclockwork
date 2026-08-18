using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000AE RID: 174
	internal class StrTransform : Transform
	{
		// Token: 0x06000548 RID: 1352 RVA: 0x000141DD File Offset: 0x000123DD
		public StrTransform()
		{
			this.transformationParameters = new TranformationParameters();
			this.includeComments = false;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00014202 File Offset: 0x00012402
		public override string Algorithm
		{
			get
			{
				return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#STR-Transform";
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00014209 File Offset: 0x00012409
		public bool IncludeComments
		{
			get
			{
				return this.includeComments;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00014211 File Offset: 0x00012411
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x00014219 File Offset: 0x00012419
		public string InclusiveNamespacesPrefixList
		{
			get
			{
				return this.inclusiveNamespacesPrefixList;
			}
			set
			{
				this.inclusiveNamespacesPrefixList = value;
				this.inclusivePrefixes = StrTransform.TokenizeInclusivePrefixList(value);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0001422E File Offset: 0x0001242E
		public override bool NeedsInclusiveContext
		{
			get
			{
				return this.GetInclusivePrefixes() != null;
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00014239 File Offset: 0x00012439
		public string[] GetInclusivePrefixes()
		{
			return this.inclusivePrefixes;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00014244 File Offset: 0x00012444
		private CanonicalizationDriver GetConfiguredDriver(SignatureResourcePool resourcePool)
		{
			CanonicalizationDriver canonicalizationDriver = resourcePool.TakeCanonicalizationDriver();
			canonicalizationDriver.IncludeComments = this.IncludeComments;
			canonicalizationDriver.SetInclusivePrefixes(this.inclusivePrefixes);
			return canonicalizationDriver;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00014274 File Offset: 0x00012474
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

		// Token: 0x06000551 RID: 1361 RVA: 0x0001430C File Offset: 0x0001250C
		public override byte[] ProcessAndDigest(object input, SignatureResourcePool resourcePool, string digestAlgorithm, DictionaryManager dictionaryManager)
		{
			HashAlgorithm hashAlgorithm = resourcePool.TakeHashAlgorithm(digestAlgorithm);
			this.ProcessAndDigest(input, resourcePool, hashAlgorithm, dictionaryManager);
			return hashAlgorithm.Hash;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00014334 File Offset: 0x00012534
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

		// Token: 0x06000553 RID: 1363 RVA: 0x000143C8 File Offset: 0x000125C8
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

		// Token: 0x06000554 RID: 1364 RVA: 0x00014424 File Offset: 0x00012624
		public override void ReadFrom(XmlDictionaryReader reader, DictionaryManager dictionaryManager, bool preserveComments)
		{
			reader.MoveToStartElement(dictionaryManager.XmlSignatureDictionary.Transform, dictionaryManager.XmlSignatureDictionary.Namespace);
			this.prefix = reader.Prefix;
			bool isEmptyElement = reader.IsEmptyElement;
			string attribute = reader.GetAttribute(dictionaryManager.XmlSignatureDictionary.Algorithm, null);
			if (attribute != this.Algorithm)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("AlgorithmMismatchForTransform")));
			}
			reader.MoveToContent();
			reader.Read();
			if (!isEmptyElement)
			{
				if (reader.IsStartElement("TransformationParameters", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"))
				{
					this.transformationParameters.ReadFrom(reader, dictionaryManager);
				}
				reader.MoveToContent();
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000144D8 File Offset: 0x000126D8
		public override void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			writer.WriteStartElement(this.prefix, dictionaryManager.XmlSignatureDictionary.Transform, dictionaryManager.XmlSignatureDictionary.Namespace);
			writer.WriteStartAttribute(dictionaryManager.XmlSignatureDictionary.Algorithm, null);
			writer.WriteString(this.Algorithm);
			writer.WriteEndAttribute();
			this.transformationParameters.WriteTo(writer, dictionaryManager);
			writer.WriteEndElement();
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00014540 File Offset: 0x00012740
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

		// Token: 0x040004C4 RID: 1220
		private readonly bool includeComments;

		// Token: 0x040004C5 RID: 1221
		private string inclusiveNamespacesPrefixList;

		// Token: 0x040004C6 RID: 1222
		private string[] inclusivePrefixes;

		// Token: 0x040004C7 RID: 1223
		private string prefix = "";

		// Token: 0x040004C8 RID: 1224
		private TranformationParameters transformationParameters;
	}
}
