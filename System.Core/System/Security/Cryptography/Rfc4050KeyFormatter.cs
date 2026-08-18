using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace System.Security.Cryptography
{
	// Token: 0x0200010E RID: 270
	internal static class Rfc4050KeyFormatter
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x0001DD84 File Offset: 0x0001BF84
		internal static ECParameters FromXml(string xml, out bool isEcdh)
		{
			ECParameters ecparameters = default(ECParameters);
			ECParameters result;
			using (TextReader textReader = new StringReader(xml))
			{
				using (XmlTextReader xmlTextReader = new XmlTextReader(textReader))
				{
					XPathDocument xpathDocument = new XPathDocument(xmlTextReader);
					XPathNavigator xpathNavigator = xpathDocument.CreateNavigator();
					if (!xpathNavigator.MoveToFirstChild())
					{
						throw new ArgumentException(SR.GetString("Cryptography_MissingDomainParameters"));
					}
					ecparameters.Curve = Rfc4050KeyFormatter.ReadCurve(xpathNavigator, out isEcdh);
					if (!xpathNavigator.MoveToNext(XPathNodeType.Element))
					{
						throw new ArgumentException(SR.GetString("Cryptography_MissingPublicKey"));
					}
					Rfc4050KeyFormatter.ReadPublicKey(xpathNavigator, ref ecparameters);
					result = ecparameters;
				}
			}
			return result;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001DE38 File Offset: 0x0001C038
		private static ECCurve ReadCurve(XPathNavigator navigator, out bool isEcdh)
		{
			if (navigator.NamespaceURI != "http://www.w3.org/2001/04/xmldsig-more#")
			{
				throw new ArgumentException(SR.GetString("Cryptography_UnexpectedXmlNamespace", new object[]
				{
					navigator.NamespaceURI,
					"http://www.w3.org/2001/04/xmldsig-more#"
				}));
			}
			bool flag = navigator.Name == "ECDHKeyValue";
			bool flag2 = navigator.Name == "ECDSAKeyValue";
			if (!flag && !flag2)
			{
				throw new ArgumentException(SR.GetString("Cryptography_UnknownEllipticCurveAlgorithm"));
			}
			if (!navigator.MoveToFirstChild() || navigator.Name != "DomainParameters")
			{
				throw new ArgumentException(SR.GetString("Cryptography_MissingDomainParameters"));
			}
			if (!navigator.MoveToFirstChild() || navigator.Name != "NamedCurve")
			{
				throw new ArgumentException(SR.GetString("Cryptography_MissingDomainParameters"));
			}
			if (!navigator.MoveToFirstAttribute() || navigator.Name != "URN" || string.IsNullOrEmpty(navigator.Value))
			{
				throw new ArgumentException(SR.GetString("Cryptography_MissingDomainParameters"));
			}
			string value = navigator.Value;
			if (!value.StartsWith("urn:oid:", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(SR.GetString("Cryptography_UnknownEllipticCurve"));
			}
			navigator.MoveToParent();
			navigator.MoveToParent();
			isEcdh = flag;
			return ECCurve.CreateFromValue(value.Substring("urn:oid:".Length));
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001DF8C File Offset: 0x0001C18C
		private static void ReadPublicKey(XPathNavigator navigator, ref ECParameters parameters)
		{
			if (navigator.NamespaceURI != "http://www.w3.org/2001/04/xmldsig-more#")
			{
				throw new ArgumentException(SR.GetString("Cryptography_UnexpectedXmlNamespace", new object[]
				{
					navigator.NamespaceURI,
					"http://www.w3.org/2001/04/xmldsig-more#"
				}));
			}
			if (navigator.Name != "PublicKey")
			{
				throw new ArgumentException(SR.GetString("Cryptography_MissingPublicKey"));
			}
			if (!navigator.MoveToFirstChild() || navigator.Name != "X")
			{
				throw new ArgumentException(SR.GetString("Cryptography_MissingPublicKey"));
			}
			if (!navigator.MoveToFirstAttribute() || navigator.Name != "Value" || string.IsNullOrEmpty(navigator.Value))
			{
				throw new ArgumentException(SR.GetString("Cryptography_MissingPublicKey"));
			}
			BigInteger bigInteger = BigInteger.Parse(navigator.Value, CultureInfo.InvariantCulture);
			navigator.MoveToParent();
			if (!navigator.MoveToNext(XPathNodeType.Element) || navigator.Name != "Y")
			{
				throw new ArgumentException(SR.GetString("Cryptography_MissingPublicKey"));
			}
			if (!navigator.MoveToFirstAttribute() || navigator.Name != "Value" || string.IsNullOrEmpty(navigator.Value))
			{
				throw new ArgumentException(SR.GetString("Cryptography_MissingPublicKey"));
			}
			BigInteger bigInteger2 = BigInteger.Parse(navigator.Value, CultureInfo.InvariantCulture);
			byte[] array = bigInteger.ToByteArray();
			byte[] array2 = bigInteger2.ToByteArray();
			int num = array.Length;
			int num2 = array2.Length;
			if (num > 0 && array[num - 1] == 0)
			{
				num--;
			}
			if (num2 > 0 && array2[num2 - 1] == 0)
			{
				num2--;
			}
			int num3 = Math.Max(num, num2);
			try
			{
				using (ECDsa ecdsa = ECDsa.Create(parameters.Curve))
				{
					int val = (ecdsa.KeySize + 7) / 8;
					num3 = Math.Max(num3, val);
				}
			}
			catch (ArgumentException)
			{
			}
			catch (CryptographicException)
			{
			}
			catch (NotSupportedException)
			{
			}
			Array.Resize<byte>(ref array, num3);
			Array.Resize<byte>(ref array2, num3);
			Array.Reverse(array);
			Array.Reverse(array2);
			parameters.Q.X = array;
			parameters.Q.Y = array2;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001E1D4 File Offset: 0x0001C3D4
		private static void WriteDomainParameters(XmlWriter writer, ref ECParameters parameters)
		{
			Oid oid = parameters.Curve.Oid;
			if (!parameters.Curve.IsNamed || oid == null)
			{
				throw new ArgumentException(SR.GetString("Cryptography_UnknownEllipticCurve"));
			}
			string text = oid.Value;
			if (string.IsNullOrEmpty(text))
			{
				string friendlyName = oid.FriendlyName;
				if (!(friendlyName == "nistP256"))
				{
					if (!(friendlyName == "nistP384"))
					{
						if (!(friendlyName == "nistP521"))
						{
							text = new Oid
							{
								FriendlyName = oid.FriendlyName
							}.Value;
						}
						else
						{
							text = "1.3.132.0.35";
						}
					}
					else
					{
						text = "1.3.132.0.34";
					}
				}
				else
				{
					text = "1.2.840.10045.3.1.7";
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException(SR.GetString("Cryptography_UnknownEllipticCurve"));
			}
			writer.WriteStartElement("DomainParameters");
			writer.WriteStartElement("NamedCurve");
			writer.WriteAttributeString("URN", "urn:oid:" + text);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001E2D0 File Offset: 0x0001C4D0
		private static void WritePublicKeyValue(XmlWriter writer, ref ECParameters parameters)
		{
			writer.WriteStartElement("PublicKey");
			byte[] x = parameters.Q.X;
			byte[] y = parameters.Q.Y;
			int num = x.Length;
			int num2 = y.Length;
			if ((x[0] & 128) == 128)
			{
				num++;
			}
			if ((y[0] & 128) == 128)
			{
				num2++;
			}
			byte[] array = new byte[num];
			byte[] array2 = new byte[num2];
			Buffer.BlockCopy(x, 0, array, num - x.Length, x.Length);
			Buffer.BlockCopy(y, 0, array2, num2 - y.Length, y.Length);
			Array.Reverse(array);
			Array.Reverse(array2);
			BigInteger bigInteger = new BigInteger(array);
			BigInteger bigInteger2 = new BigInteger(array2);
			writer.WriteStartElement("X");
			writer.WriteAttributeString("Value", bigInteger.ToString("R", CultureInfo.InvariantCulture));
			writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "PrimeFieldElemType");
			writer.WriteEndElement();
			writer.WriteStartElement("Y");
			writer.WriteAttributeString("Value", bigInteger2.ToString("R", CultureInfo.InvariantCulture));
			writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "PrimeFieldElemType");
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001E418 File Offset: 0x0001C618
		internal static string ToXml(ECParameters parameters, bool isEcdh)
		{
			parameters.Validate();
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings
			{
				Indent = true,
				IndentChars = "  ",
				OmitXmlDeclaration = true
			}))
			{
				string localName = isEcdh ? "ECDHKeyValue" : "ECDSAKeyValue";
				xmlWriter.WriteStartElement(localName, "http://www.w3.org/2001/04/xmldsig-more#");
				Rfc4050KeyFormatter.WriteDomainParameters(xmlWriter, ref parameters);
				Rfc4050KeyFormatter.WritePublicKeyValue(xmlWriter, ref parameters);
				xmlWriter.WriteEndElement();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040006A5 RID: 1701
		private const string DomainParametersRoot = "DomainParameters";

		// Token: 0x040006A6 RID: 1702
		private const string ECDHRoot = "ECDHKeyValue";

		// Token: 0x040006A7 RID: 1703
		private const string ECDsaRoot = "ECDSAKeyValue";

		// Token: 0x040006A8 RID: 1704
		private const string NamedCurveElement = "NamedCurve";

		// Token: 0x040006A9 RID: 1705
		private const string Namespace = "http://www.w3.org/2001/04/xmldsig-more#";

		// Token: 0x040006AA RID: 1706
		private const string OidUrnPrefix = "urn:oid:";

		// Token: 0x040006AB RID: 1707
		private const string PublicKeyRoot = "PublicKey";

		// Token: 0x040006AC RID: 1708
		private const string UrnAttribute = "URN";

		// Token: 0x040006AD RID: 1709
		private const string ValueAttribute = "Value";

		// Token: 0x040006AE RID: 1710
		private const string XElement = "X";

		// Token: 0x040006AF RID: 1711
		private const string YElement = "Y";

		// Token: 0x040006B0 RID: 1712
		private const string XsiTypeAttribute = "type";

		// Token: 0x040006B1 RID: 1713
		private const string XsiTypeAttributeValue = "PrimeFieldElemType";

		// Token: 0x040006B2 RID: 1714
		private const string XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x040006B3 RID: 1715
		private const string XsiNamespacePrefix = "xsi";

		// Token: 0x040006B4 RID: 1716
		private const string ECDSA_P256_OID_VALUE = "1.2.840.10045.3.1.7";

		// Token: 0x040006B5 RID: 1717
		private const string ECDSA_P384_OID_VALUE = "1.3.132.0.34";

		// Token: 0x040006B6 RID: 1718
		private const string ECDSA_P521_OID_VALUE = "1.3.132.0.35";
	}
}
