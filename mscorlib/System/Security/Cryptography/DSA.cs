using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;
using System.Text;

namespace System.Security.Cryptography
{
	// Token: 0x0200087C RID: 2172
	[ComVisible(true)]
	public abstract class DSA : AsymmetricAlgorithm
	{
		// Token: 0x06004F2B RID: 20267 RVA: 0x0011361D File Offset: 0x0011261D
		public new static DSA Create()
		{
			return DSA.Create("System.Security.Cryptography.DSA");
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x00113629 File Offset: 0x00112629
		public new static DSA Create(string algName)
		{
			return (DSA)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x06004F2D RID: 20269
		public abstract byte[] CreateSignature(byte[] rgbHash);

		// Token: 0x06004F2E RID: 20270
		public abstract bool VerifySignature(byte[] rgbHash, byte[] rgbSignature);

		// Token: 0x06004F2F RID: 20271 RVA: 0x00113638 File Offset: 0x00112638
		public override void FromXmlString(string xmlString)
		{
			if (xmlString == null)
			{
				throw new ArgumentNullException("xmlString");
			}
			DSAParameters parameters = default(DSAParameters);
			Parser parser = new Parser(xmlString);
			SecurityElement topElement = parser.GetTopElement();
			string text = topElement.SearchForTextOfLocalName("P");
			if (text == null)
			{
				throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cryptography_InvalidFromXmlString"), new object[]
				{
					"DSA",
					"P"
				}));
			}
			parameters.P = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text));
			string text2 = topElement.SearchForTextOfLocalName("Q");
			if (text2 == null)
			{
				throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cryptography_InvalidFromXmlString"), new object[]
				{
					"DSA",
					"Q"
				}));
			}
			parameters.Q = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text2));
			string text3 = topElement.SearchForTextOfLocalName("G");
			if (text3 == null)
			{
				throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cryptography_InvalidFromXmlString"), new object[]
				{
					"DSA",
					"G"
				}));
			}
			parameters.G = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text3));
			string text4 = topElement.SearchForTextOfLocalName("Y");
			if (text4 == null)
			{
				throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cryptography_InvalidFromXmlString"), new object[]
				{
					"DSA",
					"Y"
				}));
			}
			parameters.Y = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text4));
			string text5 = topElement.SearchForTextOfLocalName("J");
			if (text5 != null)
			{
				parameters.J = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text5));
			}
			string text6 = topElement.SearchForTextOfLocalName("X");
			if (text6 != null)
			{
				parameters.X = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text6));
			}
			string text7 = topElement.SearchForTextOfLocalName("Seed");
			string text8 = topElement.SearchForTextOfLocalName("PgenCounter");
			if (text7 != null && text8 != null)
			{
				parameters.Seed = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text7));
				parameters.Counter = Utils.ConvertByteArrayToInt(Convert.FromBase64String(Utils.DiscardWhiteSpaces(text8)));
			}
			else if (text7 != null || text8 != null)
			{
				if (text7 == null)
				{
					throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cryptography_InvalidFromXmlString"), new object[]
					{
						"DSA",
						"Seed"
					}));
				}
				throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cryptography_InvalidFromXmlString"), new object[]
				{
					"DSA",
					"PgenCounter"
				}));
			}
			this.ImportParameters(parameters);
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x001138E4 File Offset: 0x001128E4
		public override string ToXmlString(bool includePrivateParameters)
		{
			DSAParameters dsaparameters = this.ExportParameters(includePrivateParameters);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<DSAKeyValue>");
			stringBuilder.Append("<P>" + Convert.ToBase64String(dsaparameters.P) + "</P>");
			stringBuilder.Append("<Q>" + Convert.ToBase64String(dsaparameters.Q) + "</Q>");
			stringBuilder.Append("<G>" + Convert.ToBase64String(dsaparameters.G) + "</G>");
			stringBuilder.Append("<Y>" + Convert.ToBase64String(dsaparameters.Y) + "</Y>");
			if (dsaparameters.J != null)
			{
				stringBuilder.Append("<J>" + Convert.ToBase64String(dsaparameters.J) + "</J>");
			}
			if (dsaparameters.Seed != null)
			{
				stringBuilder.Append("<Seed>" + Convert.ToBase64String(dsaparameters.Seed) + "</Seed>");
				stringBuilder.Append("<PgenCounter>" + Convert.ToBase64String(Utils.ConvertIntToByteArray(dsaparameters.Counter)) + "</PgenCounter>");
			}
			if (includePrivateParameters)
			{
				stringBuilder.Append("<X>" + Convert.ToBase64String(dsaparameters.X) + "</X>");
			}
			stringBuilder.Append("</DSAKeyValue>");
			return stringBuilder.ToString();
		}

		// Token: 0x06004F31 RID: 20273
		public abstract DSAParameters ExportParameters(bool includePrivateParameters);

		// Token: 0x06004F32 RID: 20274
		public abstract void ImportParameters(DSAParameters parameters);
	}
}
