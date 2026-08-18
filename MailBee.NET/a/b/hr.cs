using System;
using System.IO;
using System.Text;
using System.Xml;
using MailBee;

namespace a.b
{
	// Token: 0x020002AF RID: 687
	internal class hr
	{
		// Token: 0x0600180A RID: 6154 RVA: 0x0006DCA8 File Offset: 0x0006CCA8
		public hr(az A_0)
		{
			this.k = A_0.a0();
			this.l = A_0.a0();
			this.m = A_0.a0();
			this.n = A_0.a0();
			this.o = A_0.a0();
			this.p = A_0.a0();
			A_0.ax();
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				char c = (char)A_0.az();
				if (c == '\0')
				{
					break;
				}
				stringBuilder.Append(c);
			}
			this.s = stringBuilder.ToString();
			this.q = 1;
			this.r = null;
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0006DD40 File Offset: 0x0006CD40
		public hr(string A_0)
		{
			try
			{
				XmlAttributeCollection attributes;
				try
				{
					MemoryStream inStream = new MemoryStream(Encoding.Default.GetBytes(A_0));
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(inStream);
					attributes = xmlDocument.GetElementsByTagName("keyData")[0].Attributes;
				}
				catch (Exception)
				{
					throw new EncryptedDocumentException("Unable to parse keyData");
				}
				this.o = int.Parse(attributes.GetNamedItem("keyBits").Value);
				this.k = 0;
				this.l = 0;
				this.s = null;
				int num = int.Parse(attributes.GetNamedItem("blockSize").Value);
				string value = attributes.GetNamedItem("cipherAlgorithm").Value;
				if (!"AES".Equals(value))
				{
					throw new EncryptedDocumentException("Unsupported cipher");
				}
				this.p = 24;
				if (num == 16)
				{
					this.m = 26126;
				}
				else if (num == 24)
				{
					this.m = 26127;
				}
				else
				{
					if (num != 32)
					{
						throw new EncryptedDocumentException("Unsupported key length");
					}
					this.m = 26128;
				}
				string value2 = attributes.GetNamedItem("cipherChaining").Value;
				if ("ChainingModeCBC".Equals(value2))
				{
					this.q = 2;
				}
				else
				{
					if (!"ChainingModeCFB".Equals(value2))
					{
						throw new EncryptedDocumentException("Unsupported chaining mode");
					}
					this.q = 3;
				}
				string value3 = attributes.GetNamedItem("hashAlgorithm").Value;
				int num2 = int.Parse(attributes.GetNamedItem("hashSize").Value);
				if (!"SHA1".Equals(value3) || num2 != 20)
				{
					throw new EncryptedDocumentException("Unsupported hash algorithm");
				}
				this.n = 32772;
				string value4 = attributes.GetNamedItem("saltValue").Value;
				int num3 = int.Parse(attributes.GetNamedItem("saltSize").Value);
				this.r = Convert.FromBase64String(value4);
				if (this.r.Length != num3)
				{
					throw new EncryptedDocumentException("Invalid salt length");
				}
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0006DF70 File Offset: 0x0006CF70
		public int i()
		{
			return this.q;
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0006DF78 File Offset: 0x0006CF78
		public int h()
		{
			return this.k;
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x0006DF80 File Offset: 0x0006CF80
		public int b()
		{
			return this.l;
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0006DF88 File Offset: 0x0006CF88
		public int e()
		{
			return this.m;
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0006DF90 File Offset: 0x0006CF90
		public int g()
		{
			return this.n;
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0006DF98 File Offset: 0x0006CF98
		public int f()
		{
			return this.o;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0006DFA0 File Offset: 0x0006CFA0
		public byte[] d()
		{
			return this.r;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0006DFA8 File Offset: 0x0006CFA8
		public int c()
		{
			return this.p;
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0006DFB0 File Offset: 0x0006CFB0
		public string a()
		{
			return this.s;
		}

		// Token: 0x04001201 RID: 4609
		public const int a = 26625;

		// Token: 0x04001202 RID: 4610
		public const int b = 26126;

		// Token: 0x04001203 RID: 4611
		public const int c = 26127;

		// Token: 0x04001204 RID: 4612
		public const int d = 26128;

		// Token: 0x04001205 RID: 4613
		public const int e = 32772;

		// Token: 0x04001206 RID: 4614
		public const int f = 1;

		// Token: 0x04001207 RID: 4615
		public const int g = 24;

		// Token: 0x04001208 RID: 4616
		public const int h = 1;

		// Token: 0x04001209 RID: 4617
		public const int i = 2;

		// Token: 0x0400120A RID: 4618
		public const int j = 3;

		// Token: 0x0400120B RID: 4619
		private int k;

		// Token: 0x0400120C RID: 4620
		private int l;

		// Token: 0x0400120D RID: 4621
		private int m;

		// Token: 0x0400120E RID: 4622
		private int n;

		// Token: 0x0400120F RID: 4623
		private int o;

		// Token: 0x04001210 RID: 4624
		private int p;

		// Token: 0x04001211 RID: 4625
		private int q;

		// Token: 0x04001212 RID: 4626
		private byte[] r;

		// Token: 0x04001213 RID: 4627
		private string s;
	}
}
