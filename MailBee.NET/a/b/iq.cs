using System;
using System.IO;
using System.Text;
using System.Xml;
using MailBee;

namespace a.b
{
	// Token: 0x020002B1 RID: 689
	internal class iq
	{
		// Token: 0x0600181D RID: 6173 RVA: 0x0006E100 File Offset: 0x0006D100
		public iq(string A_0)
		{
			XmlAttributeCollection xmlAttributeCollection = null;
			try
			{
				MemoryStream inStream = new MemoryStream(Encoding.Default.GetBytes(A_0));
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(inStream);
				XmlNodeList childNodes = xmlDocument.GetElementsByTagName("keyEncryptor")[0].ChildNodes;
				for (int i = 0; i < childNodes.Count; i++)
				{
					XmlNode xmlNode = childNodes[i];
					if (xmlNode.Name.Equals("p:encryptedKey"))
					{
						xmlAttributeCollection = xmlNode.Attributes;
						break;
					}
				}
				if (xmlAttributeCollection == null)
				{
					throw new EncryptedDocumentException("");
				}
				this.f = int.Parse(xmlAttributeCollection.GetNamedItem("spinCount").Value);
				this.b = Convert.FromBase64String(xmlAttributeCollection.GetNamedItem("encryptedVerifierHashInput").Value);
				this.a = Convert.FromBase64String(xmlAttributeCollection.GetNamedItem("saltValue").Value);
				this.d = Convert.FromBase64String(xmlAttributeCollection.GetNamedItem("encryptedKeyValue").Value);
				if (int.Parse(xmlAttributeCollection.GetNamedItem("saltSize").Value) != this.a.Length)
				{
					throw new EncryptedDocumentException("Invalid salt size");
				}
				this.c = Convert.FromBase64String(xmlAttributeCollection.GetNamedItem("encryptedVerifierHashValue").Value);
				int num = int.Parse(xmlAttributeCollection.GetNamedItem("blockSize").Value);
				string value = xmlAttributeCollection.GetNamedItem("cipherAlgorithm").Value;
				if (!"AES".Equals(value))
				{
					throw new EncryptedDocumentException("Unsupported cipher");
				}
				if (num == 16)
				{
					this.g = 26126;
				}
				else if (num == 24)
				{
					this.g = 26127;
				}
				else
				{
					if (num != 32)
					{
						throw new EncryptedDocumentException("Unsupported block size");
					}
					this.g = 26128;
				}
				string value2 = xmlAttributeCollection.GetNamedItem("cipherChaining").Value;
				if ("ChainingModeCBC".Equals(value2))
				{
					this.h = 2;
				}
				else
				{
					if (!"ChainingModeCFB".Equals(value2))
					{
						throw new EncryptedDocumentException("Unsupported chaining mode");
					}
					this.h = 3;
				}
				this.e = int.Parse(xmlAttributeCollection.GetNamedItem("hashSize").Value);
			}
			catch
			{
				throw new EncryptedDocumentException("Unable to parse keyEncryptor");
			}
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0006E358 File Offset: 0x0006D358
		public iq(az A_0, int A_1)
		{
			if (A_0.a0() != 16)
			{
				throw new Exception("Salt size != 16 !?");
			}
			this.a = new byte[16];
			A_0.ay(this.a);
			this.b = new byte[16];
			A_0.ay(this.b);
			this.e = A_0.a0();
			this.c = new byte[A_1];
			A_0.ay(this.c);
			this.f = 50000;
			this.g = 26126;
			this.h = 1;
			this.d = null;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x0006E3FA File Offset: 0x0006D3FA
		public byte[] c()
		{
			return this.a;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0006E402 File Offset: 0x0006D402
		public byte[] e()
		{
			return this.b;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0006E40A File Offset: 0x0006D40A
		public byte[] g()
		{
			return this.c;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0006E412 File Offset: 0x0006D412
		public int a()
		{
			return this.f;
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0006E41A File Offset: 0x0006D41A
		public int f()
		{
			return this.h;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0006E422 File Offset: 0x0006D422
		public int d()
		{
			return this.g;
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x0006E42A File Offset: 0x0006D42A
		public byte[] b()
		{
			return this.d;
		}

		// Token: 0x04001219 RID: 4633
		private byte[] a;

		// Token: 0x0400121A RID: 4634
		private byte[] b;

		// Token: 0x0400121B RID: 4635
		private byte[] c;

		// Token: 0x0400121C RID: 4636
		private byte[] d;

		// Token: 0x0400121D RID: 4637
		private int e;

		// Token: 0x0400121E RID: 4638
		private int f;

		// Token: 0x0400121F RID: 4639
		private int g;

		// Token: 0x04001220 RID: 4640
		private int h;
	}
}
