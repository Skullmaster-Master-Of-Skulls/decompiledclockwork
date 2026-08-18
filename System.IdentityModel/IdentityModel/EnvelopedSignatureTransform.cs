using System;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x0200003D RID: 61
	internal sealed class EnvelopedSignatureTransform : Transform
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000098F0 File Offset: 0x00007AF0
		public override string Algorithm
		{
			get
			{
				return XD.XmlSignatureDictionary.EnvelopedSignature.Value;
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009904 File Offset: 0x00007B04
		public override object Process(object input, SignatureResourcePool resourcePool, DictionaryManager dictionaryManager)
		{
			XmlTokenStream xmlTokenStream = input as XmlTokenStream;
			if (xmlTokenStream != null)
			{
				xmlTokenStream.SetElementExclusion("Signature", "http://www.w3.org/2000/09/xmldsig#");
				return xmlTokenStream;
			}
			WrappedReader wrappedReader = input as WrappedReader;
			if (wrappedReader != null)
			{
				wrappedReader.XmlTokens.SetElementExclusion("Signature", "http://www.w3.org/2000/09/xmldsig#", new int?(1));
				return wrappedReader;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedInputTypeForTransform", new object[]
			{
				input.GetType()
			})));
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000997C File Offset: 0x00007B7C
		public override byte[] ProcessAndDigest(object input, SignatureResourcePool resourcePool, string digestAlgorithm, DictionaryManager dictionaryManager)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedLastTransform")));
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00009998 File Offset: 0x00007B98
		public override void ReadFrom(XmlDictionaryReader reader, DictionaryManager dictionaryManager, bool preserveComments)
		{
			reader.MoveToContent();
			string a = XmlHelper.ReadEmptyElementAndRequiredAttribute(reader, dictionaryManager.XmlSignatureDictionary.Transform, dictionaryManager.XmlSignatureDictionary.Namespace, dictionaryManager.XmlSignatureDictionary.Algorithm, out this.prefix);
			if (a != this.Algorithm)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("AlgorithmMismatchForTransform")));
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009A04 File Offset: 0x00007C04
		public override void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			writer.WriteStartElement(this.prefix, dictionaryManager.XmlSignatureDictionary.Transform, dictionaryManager.XmlSignatureDictionary.Namespace);
			writer.WriteAttributeString(dictionaryManager.XmlSignatureDictionary.Algorithm, null, this.Algorithm);
			writer.WriteEndElement();
		}

		// Token: 0x0400015A RID: 346
		private string prefix = "";
	}
}
