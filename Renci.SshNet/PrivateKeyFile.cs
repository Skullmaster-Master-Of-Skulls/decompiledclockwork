using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Renci.SshNet.Abstractions;
using Renci.SshNet.Common;
using Renci.SshNet.Security;
using Renci.SshNet.Security.Cryptography.Ciphers;
using Renci.SshNet.Security.Cryptography.Ciphers.Modes;
using Renci.SshNet.Security.Cryptography.Ciphers.Paddings;

namespace Renci.SshNet
{
	// Token: 0x02000028 RID: 40
	public class PrivateKeyFile : IDisposable
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000793D File Offset: 0x00005B3D
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00007945 File Offset: 0x00005B45
		public HostAlgorithm HostKey { get; private set; }

		// Token: 0x06000212 RID: 530 RVA: 0x0000794E File Offset: 0x00005B4E
		public PrivateKeyFile(Stream privateKey)
		{
			this.Open(privateKey, null);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000795E File Offset: 0x00005B5E
		public PrivateKeyFile(string fileName) : this(fileName, null)
		{
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007968 File Offset: 0x00005B68
		public PrivateKeyFile(string fileName, string passPhrase)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentNullException("fileName");
			}
			using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				this.Open(fileStream, passPhrase);
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000079BC File Offset: 0x00005BBC
		public PrivateKeyFile(Stream privateKey, string passPhrase)
		{
			this.Open(privateKey, passPhrase);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000079CC File Offset: 0x00005BCC
		private void Open(Stream privateKey, string passPhrase)
		{
			if (privateKey == null)
			{
				throw new ArgumentNullException("privateKey");
			}
			Match match;
			using (StreamReader streamReader = new StreamReader(privateKey))
			{
				string input = streamReader.ReadToEnd();
				match = PrivateKeyFile.PrivateKeyRegex.Match(input);
			}
			if (!match.Success)
			{
				throw new SshException("Invalid private key file.");
			}
			string text = match.Result("${keyName}");
			string text2 = match.Result("${cipherName}");
			string text3 = match.Result("${salt}");
			byte[] array = Convert.FromBase64String(match.Result("${data}"));
			byte[] data;
			if (!string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text3))
			{
				if (string.IsNullOrEmpty(passPhrase))
				{
					throw new SshPassPhraseNullOrEmptyException("Private key is encrypted but passphrase is empty.");
				}
				byte[] array2 = new byte[text3.Length / 2];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = Convert.ToByte(text3.Substring(i * 2, 2), 16);
				}
				CipherInfo cipherInfo;
				if (!(text2 == "DES-EDE3-CBC"))
				{
					if (!(text2 == "DES-EDE3-CFB"))
					{
						if (!(text2 == "DES-CBC"))
						{
							if (!(text2 == "AES-128-CBC"))
							{
								if (!(text2 == "AES-192-CBC"))
								{
									if (!(text2 == "AES-256-CBC"))
									{
										throw new SshException(string.Format(CultureInfo.CurrentCulture, "Private key cipher \"{0}\" is not supported.", new object[]
										{
											text2
										}));
									}
									cipherInfo = new CipherInfo(256, (byte[] key, byte[] iv) => new AesCipher(key, new CbcCipherMode(iv), new PKCS7Padding()));
								}
								else
								{
									cipherInfo = new CipherInfo(192, (byte[] key, byte[] iv) => new AesCipher(key, new CbcCipherMode(iv), new PKCS7Padding()));
								}
							}
							else
							{
								cipherInfo = new CipherInfo(128, (byte[] key, byte[] iv) => new AesCipher(key, new CbcCipherMode(iv), new PKCS7Padding()));
							}
						}
						else
						{
							cipherInfo = new CipherInfo(64, (byte[] key, byte[] iv) => new DesCipher(key, new CbcCipherMode(iv), new PKCS7Padding()));
						}
					}
					else
					{
						cipherInfo = new CipherInfo(192, (byte[] key, byte[] iv) => new TripleDesCipher(key, new CfbCipherMode(iv), new PKCS7Padding()));
					}
				}
				else
				{
					cipherInfo = new CipherInfo(192, (byte[] key, byte[] iv) => new TripleDesCipher(key, new CbcCipherMode(iv), new PKCS7Padding()));
				}
				data = PrivateKeyFile.DecryptKey(cipherInfo, array, passPhrase, array2);
			}
			else
			{
				data = array;
			}
			if (text == "RSA")
			{
				this._key = new RsaKey(data);
				this.HostKey = new KeyHostAlgorithm("ssh-rsa", this._key);
				return;
			}
			if (text == "DSA")
			{
				this._key = new DsaKey(data);
				this.HostKey = new KeyHostAlgorithm("ssh-dss", this._key);
				return;
			}
			if (!(text == "SSH2 ENCRYPTED"))
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "Key '{0}' is not supported.", new object[]
				{
					text
				}));
			}
			PrivateKeyFile.SshDataReader sshDataReader = new PrivateKeyFile.SshDataReader(data);
			if (sshDataReader.ReadUInt32() != 1064303083U)
			{
				throw new SshException("Invalid SSH2 private key.");
			}
			sshDataReader.ReadUInt32();
			string text4 = sshDataReader.ReadString(SshData.Ascii);
			string a = sshDataReader.ReadString(SshData.Ascii);
			int num = (int)sshDataReader.ReadUInt32();
			byte[] data2;
			if (a == "none")
			{
				data2 = sshDataReader.ReadBytes(num);
			}
			else
			{
				if (!(a == "3des-cbc"))
				{
					throw new SshException(string.Format("Cipher method '{0}' is not supported.", text2));
				}
				if (string.IsNullOrEmpty(passPhrase))
				{
					throw new SshPassPhraseNullOrEmptyException("Private key is encrypted but passphrase is empty.");
				}
				data2 = new TripleDesCipher(PrivateKeyFile.GetCipherKey(passPhrase, 24), new CbcCipherMode(new byte[8]), new PKCS7Padding()).Decrypt(sshDataReader.ReadBytes(num));
			}
			sshDataReader = new PrivateKeyFile.SshDataReader(data2);
			if ((ulong)sshDataReader.ReadUInt32() > (ulong)((long)(num - 4)))
			{
				throw new SshException("Invalid passphrase.");
			}
			if (text4 == "if-modn{sign{rsa-pkcs1-sha1},encrypt{rsa-pkcs1v2-oaep}}")
			{
				BigInteger exponent = sshDataReader.ReadBigIntWithBits();
				BigInteger d = sshDataReader.ReadBigIntWithBits();
				BigInteger modulus = sshDataReader.ReadBigIntWithBits();
				BigInteger inverseQ = sshDataReader.ReadBigIntWithBits();
				BigInteger q = sshDataReader.ReadBigIntWithBits();
				BigInteger p = sshDataReader.ReadBigIntWithBits();
				this._key = new RsaKey(modulus, exponent, d, p, q, inverseQ);
				this.HostKey = new KeyHostAlgorithm("ssh-rsa", this._key);
				return;
			}
			if (!(text4 == "dl-modp{sign{dsa-nist-sha1},dh{plain}}"))
			{
				throw new NotSupportedException(string.Format("Key type '{0}' is not supported.", text4));
			}
			if (sshDataReader.ReadUInt32() != 0U)
			{
				throw new SshException("Invalid private key");
			}
			BigInteger p2 = sshDataReader.ReadBigIntWithBits();
			BigInteger g = sshDataReader.ReadBigIntWithBits();
			BigInteger q2 = sshDataReader.ReadBigIntWithBits();
			BigInteger y = sshDataReader.ReadBigIntWithBits();
			BigInteger x = sshDataReader.ReadBigIntWithBits();
			this._key = new DsaKey(p2, q2, g, y, x);
			this.HostKey = new KeyHostAlgorithm("ssh-dss", this._key);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007EF4 File Offset: 0x000060F4
		private static byte[] GetCipherKey(string passphrase, int length)
		{
			List<byte> list = new List<byte>();
			using (MD5 md = CryptoAbstraction.CreateMD5())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(passphrase);
				byte[] array = md.ComputeHash(bytes);
				list.AddRange(array);
				while (list.Count < length)
				{
					array = bytes.Concat(array);
					array = md.ComputeHash(array);
					list.AddRange(array);
				}
			}
			return list.ToArray().Take(length);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007F74 File Offset: 0x00006174
		private static byte[] DecryptKey(CipherInfo cipherInfo, byte[] cipherData, string passPhrase, byte[] binarySalt)
		{
			if (cipherInfo == null)
			{
				throw new ArgumentNullException("cipherInfo");
			}
			if (cipherData == null)
			{
				throw new ArgumentNullException("cipherData");
			}
			if (binarySalt == null)
			{
				throw new ArgumentNullException("binarySalt");
			}
			List<byte> list = new List<byte>();
			using (MD5 md = CryptoAbstraction.CreateMD5())
			{
				byte[] array = Encoding.UTF8.GetBytes(passPhrase).Concat(binarySalt.Take(8));
				byte[] array2 = md.ComputeHash(array);
				list.AddRange(array2);
				while (list.Count < cipherInfo.KeySize / 8)
				{
					array2 = array2.Concat(array);
					array2 = md.ComputeHash(array2);
					list.AddRange(array2);
				}
			}
			return cipherInfo.Cipher(list.ToArray(), binarySalt).Decrypt(cipherData);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000803C File Offset: 0x0000623C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000804C File Offset: 0x0000624C
		protected virtual void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				Key key = this._key;
				if (key != null)
				{
					((IDisposable)key).Dispose();
					this._key = null;
				}
				this._isDisposed = true;
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00008088 File Offset: 0x00006288
		~PrivateKeyFile()
		{
			this.Dispose(false);
		}

		// Token: 0x04000091 RID: 145
		private static readonly Regex PrivateKeyRegex = new Regex("^-+ *BEGIN (?<keyName>\\w+( \\w+)*) PRIVATE KEY *-+\\r?\\n((Proc-Type: 4,ENCRYPTED\\r?\\nDEK-Info: (?<cipherName>[A-Z0-9-]+),(?<salt>[A-F0-9]+)\\r?\\n\\r?\\n)|(Comment: \"?[^\\r\\n]*\"?\\r?\\n))?(?<data>([a-zA-Z0-9/+=]{1,80}\\r?\\n)+)-+ *END \\k<keyName> PRIVATE KEY *-+", RegexOptions.Multiline | RegexOptions.Compiled);

		// Token: 0x04000092 RID: 146
		private Key _key;

		// Token: 0x04000094 RID: 148
		private bool _isDisposed;

		// Token: 0x0200012F RID: 303
		private class SshDataReader : SshData
		{
			// Token: 0x06000C71 RID: 3185 RVA: 0x00027E3C File Offset: 0x0002603C
			public SshDataReader(byte[] data)
			{
				base.LoadBytes(data);
			}

			// Token: 0x06000C72 RID: 3186 RVA: 0x00027E4B File Offset: 0x0002604B
			public new uint ReadUInt32()
			{
				return base.ReadUInt32();
			}

			// Token: 0x06000C73 RID: 3187 RVA: 0x00027E53 File Offset: 0x00026053
			public new string ReadString(Encoding encoding)
			{
				return base.ReadString(encoding);
			}

			// Token: 0x06000C74 RID: 3188 RVA: 0x00027E5C File Offset: 0x0002605C
			public new byte[] ReadBytes(int length)
			{
				return base.ReadBytes(length);
			}

			// Token: 0x06000C75 RID: 3189 RVA: 0x00027E68 File Offset: 0x00026068
			public BigInteger ReadBigIntWithBits()
			{
				int num = (int)base.ReadUInt32();
				num = (num + 7) / 8;
				byte[] array = base.ReadBytes(num);
				byte[] array2 = new byte[array.Length + 1];
				Buffer.BlockCopy(array, 0, array2, 1, array.Length);
				return new BigInteger(array2.Reverse<byte>());
			}

			// Token: 0x06000C76 RID: 3190 RVA: 0x0000262A File Offset: 0x0000082A
			protected override void LoadData()
			{
			}

			// Token: 0x06000C77 RID: 3191 RVA: 0x0000262A File Offset: 0x0000082A
			protected override void SaveData()
			{
			}
		}
	}
}
