using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x0200007A RID: 122
	internal sealed class SignatureResourcePool
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x00010084 File Offset: 0x0000E284
		public char[] TakeBase64Buffer()
		{
			if (this.base64Buffer == null)
			{
				this.base64Buffer = new char[64];
			}
			return this.base64Buffer;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000100A1 File Offset: 0x0000E2A1
		public CanonicalizationDriver TakeCanonicalizationDriver()
		{
			if (this.canonicalizationDriver == null)
			{
				this.canonicalizationDriver = new CanonicalizationDriver();
			}
			else
			{
				this.canonicalizationDriver.Reset();
			}
			return this.canonicalizationDriver;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000100C9 File Offset: 0x0000E2C9
		public byte[] TakeEncodingBuffer()
		{
			if (this.encodingBuffer == null)
			{
				this.encodingBuffer = new byte[64];
			}
			return this.encodingBuffer;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000100E8 File Offset: 0x0000E2E8
		public HashAlgorithm TakeHashAlgorithm(string algorithm)
		{
			if (this.hashAlgorithm == null)
			{
				if (string.IsNullOrEmpty(algorithm))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(algorithm, SR.GetString("EmptyOrNullArgumentString", new object[]
					{
						"algorithm"
					}));
				}
				this.hashAlgorithm = CryptoHelper.CreateHashAlgorithm(algorithm);
			}
			else
			{
				this.hashAlgorithm.Initialize();
			}
			return this.hashAlgorithm;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00010148 File Offset: 0x0000E348
		public HashStream TakeHashStream(HashAlgorithm hash)
		{
			if (this.hashStream == null)
			{
				this.hashStream = new HashStream(hash);
			}
			else
			{
				this.hashStream.Reset(hash);
			}
			return this.hashStream;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00010172 File Offset: 0x0000E372
		public HashStream TakeHashStream(string algorithm)
		{
			return this.TakeHashStream(this.TakeHashAlgorithm(algorithm));
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00010184 File Offset: 0x0000E384
		public XmlDictionaryWriter TakeUtf8Writer()
		{
			if (this.utf8Writer == null)
			{
				this.utf8Writer = XmlDictionaryWriter.CreateTextWriter(Stream.Null, Encoding.UTF8, false);
			}
			else
			{
				((IXmlTextWriterInitializer)this.utf8Writer).SetOutput(Stream.Null, Encoding.UTF8, false);
			}
			return this.utf8Writer;
		}

		// Token: 0x04000396 RID: 918
		private const int BufferSize = 64;

		// Token: 0x04000397 RID: 919
		private CanonicalizationDriver canonicalizationDriver;

		// Token: 0x04000398 RID: 920
		private HashStream hashStream;

		// Token: 0x04000399 RID: 921
		private HashAlgorithm hashAlgorithm;

		// Token: 0x0400039A RID: 922
		private XmlDictionaryWriter utf8Writer;

		// Token: 0x0400039B RID: 923
		private byte[] encodingBuffer;

		// Token: 0x0400039C RID: 924
		private char[] base64Buffer;
	}
}
