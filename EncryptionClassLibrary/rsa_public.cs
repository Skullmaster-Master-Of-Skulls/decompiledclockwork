using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace EncryptionClassLibrary
{
	// Token: 0x0200000E RID: 14
	public class rsa_public
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003B18 File Offset: 0x00001D18
		public static bool VerifyXml(string publicKeyXmlFilename, string xml_filename)
		{
			StreamReader streamReader = new StreamReader(publicKeyXmlFilename);
			string xml = streamReader.ReadToEnd();
			streamReader.Close();
			return rsa_public.VerifyXml2(xml, xml_filename);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003B48 File Offset: 0x00001D48
		public static bool VerifyXml2(string xml, string xml_filename)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			xmlDocument.Load(xml_filename);
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(2048);
			rsacryptoServiceProvider.FromXmlString(xml);
			SignedXml signedXml = new SignedXml(xmlDocument);
			XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("Signature");
			bool flag = elementsByTagName.Count <= 0;
			if (flag)
			{
				throw new CryptographicException("Verification failed: No Signature was found in the document.");
			}
			bool flag2 = elementsByTagName.Count >= 2;
			if (flag2)
			{
				throw new CryptographicException("Verification failed: More that one signature was found for the document.");
			}
			signedXml.LoadXml((XmlElement)elementsByTagName[0]);
			return signedXml.CheckSignature(rsacryptoServiceProvider);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public static void SignXml(string privateKeyXmlFilename, string xml_filename)
		{
			StreamReader streamReader = new StreamReader(privateKeyXmlFilename);
			string xml = streamReader.ReadToEnd();
			streamReader.Close();
			rsa_public.SignXml2(xml, xml_filename);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003C1C File Offset: 0x00001E1C
		public static void SignXml2(string xml, string xml_filename)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			xmlDocument.Load(xml_filename);
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(2048);
			rsacryptoServiceProvider.FromXmlString(xml);
			SignedXml signedXml = new SignedXml(xmlDocument);
			signedXml.SigningKey = rsacryptoServiceProvider;
			Reference reference = new Reference();
			reference.Uri = "";
			XmlDsigEnvelopedSignatureTransform transform = new XmlDsigEnvelopedSignatureTransform();
			reference.AddTransform(transform);
			signedXml.AddReference(reference);
			signedXml.ComputeSignature();
			XmlElement xml2 = signedXml.GetXml();
			xmlDocument.DocumentElement.AppendChild(xmlDocument.ImportNode(xml2, true));
			xmlDocument.Save(xml_filename);
		}
	}
}
