using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Agreement.Srp;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Date;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200042A RID: 1066
	public class TlsProtocolHandler
	{
		// Token: 0x0600243F RID: 9279 RVA: 0x000DCA70 File Offset: 0x000DBA70
		public TlsProtocolHandler(Stream inStr, Stream outStr)
		{
			byte[] inSeed = new ThreadedSeedGenerator().GenerateSeed(20, true);
			this.random = new SecureRandom(inSeed);
			this.rs = new RecordStream(this, inStr, outStr);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000DCAD8 File Offset: 0x000DBAD8
		public TlsProtocolHandler(Stream inStr, Stream outStr, SecureRandom sr)
		{
			this.random = sr;
			this.rs = new RecordStream(this, inStr, outStr);
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x000DCB2C File Offset: 0x000DBB2C
		internal SecureRandom Random
		{
			get
			{
				return this.random;
			}
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000DCB34 File Offset: 0x000DBB34
		internal void ProcessData(short protocol, byte[] buf, int offset, int len)
		{
			switch (protocol)
			{
			case 20:
				this.changeCipherSpecQueue.AddData(buf, offset, len);
				this.processChangeCipherSpec();
				return;
			case 21:
				this.alertQueue.AddData(buf, offset, len);
				this.processAlert();
				return;
			case 22:
				this.handshakeQueue.AddData(buf, offset, len);
				this.processHandshake();
				return;
			case 23:
				if (!this.appDataReady)
				{
					this.FailWithError(2, 10);
				}
				this.applicationDataQueue.AddData(buf, offset, len);
				this.processApplicationData();
				return;
			default:
				return;
			}
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000DCBC8 File Offset: 0x000DBBC8
		private void processHandshake()
		{
			bool flag;
			do
			{
				flag = false;
				if (this.handshakeQueue.Available >= 4)
				{
					byte[] array = new byte[4];
					this.handshakeQueue.Read(array, 0, 4, 0);
					MemoryStream inStr = new MemoryStream(array, false);
					short num = TlsUtilities.ReadUint8(inStr);
					int num2 = TlsUtilities.ReadUint24(inStr);
					if (this.handshakeQueue.Available >= num2 + 4)
					{
						byte[] array2 = new byte[num2];
						this.handshakeQueue.Read(array2, 0, num2, 4);
						this.handshakeQueue.RemoveData(num2 + 4);
						if (num != 20)
						{
							this.rs.UpdateHandshakeData(array, 0, 4);
							this.rs.UpdateHandshakeData(array2, 0, num2);
						}
						MemoryStream memoryStream = new MemoryStream(array2, false);
						short num3 = num;
						switch (num3)
						{
						case 0:
						case 1:
						case 3:
						case 4:
						case 5:
						case 6:
						case 7:
						case 8:
						case 9:
						case 10:
						case 15:
						case 16:
							break;
						case 2:
						{
							short num4 = this.connection_state;
							if (num4 == 1)
							{
								TlsUtilities.CheckVersion(memoryStream, this);
								this.serverRandom = new byte[32];
								TlsUtilities.ReadFully(this.serverRandom, memoryStream);
								TlsUtilities.ReadOpaque8(memoryStream);
								this.chosenCipherSuite = TlsCipherSuiteManager.GetCipherSuite(TlsUtilities.ReadUint16(memoryStream), this);
								short num5 = TlsUtilities.ReadUint8(memoryStream);
								if (num5 != 0)
								{
									this.FailWithError(2, 47);
								}
								if (this.extendedClientHello && memoryStream.Position < memoryStream.Length)
								{
									byte[] buffer = TlsUtilities.ReadOpaque16(memoryStream);
									Hashtable hashtable = new Hashtable();
									MemoryStream memoryStream2 = new MemoryStream(buffer, false);
									while (memoryStream2.Position < memoryStream2.Length)
									{
										int num6 = TlsUtilities.ReadUint16(memoryStream2);
										byte[] value = TlsUtilities.ReadOpaque16(memoryStream2);
										hashtable[num6] = value;
									}
								}
								this.AssertEmpty(memoryStream);
								this.connection_state = 2;
								flag = true;
								goto IL_832;
							}
							this.FailWithError(2, 10);
							goto IL_832;
						}
						case 11:
						{
							short num7 = this.connection_state;
							if (num7 == 2)
							{
								Certificate certificate = Certificate.Parse(memoryStream);
								this.AssertEmpty(memoryStream);
								X509CertificateStructure x509CertificateStructure = certificate.certs[0];
								SubjectPublicKeyInfo subjectPublicKeyInfo = x509CertificateStructure.SubjectPublicKeyInfo;
								try
								{
									this.serverPublicKey = PublicKeyFactory.CreateKey(subjectPublicKeyInfo);
								}
								catch (Exception)
								{
									this.FailWithError(2, 43);
								}
								if (this.serverPublicKey.IsPrivate)
								{
									this.FailWithError(2, 80);
								}
								short keyExchangeAlgorithm = this.chosenCipherSuite.KeyExchangeAlgorithm;
								switch (keyExchangeAlgorithm)
								{
								case 1:
									if (!(this.serverPublicKey is RsaKeyParameters))
									{
										this.FailWithError(2, 46);
									}
									this.validateKeyUsage(x509CertificateStructure, 32);
									break;
								case 2:
								case 4:
									goto IL_209;
								case 3:
									goto IL_1F1;
								case 5:
									goto IL_1CC;
								default:
									switch (keyExchangeAlgorithm)
									{
									case 11:
										goto IL_1CC;
									case 12:
										goto IL_1F1;
									default:
										goto IL_209;
									}
									break;
								}
								IL_212:
								if (!this.verifyer.IsValid(certificate.GetCerts()))
								{
									this.FailWithError(2, 90);
									goto IL_23A;
								}
								goto IL_23A;
								IL_1F1:
								if (!(this.serverPublicKey is DsaPublicKeyParameters))
								{
									this.FailWithError(2, 46);
									goto IL_212;
								}
								goto IL_212;
								IL_1CC:
								if (!(this.serverPublicKey is RsaKeyParameters))
								{
									this.FailWithError(2, 46);
								}
								this.validateKeyUsage(x509CertificateStructure, 128);
								goto IL_212;
								IL_209:
								this.FailWithError(2, 43);
								goto IL_212;
							}
							this.FailWithError(2, 10);
							IL_23A:
							this.connection_state = 3;
							flag = true;
							goto IL_832;
						}
						case 12:
							switch (this.connection_state)
							{
							case 2:
							case 3:
							{
								if (this.connection_state == 2 && this.chosenCipherSuite.KeyExchangeAlgorithm != 10)
								{
									this.FailWithError(2, 10);
								}
								short keyExchangeAlgorithm2 = this.chosenCipherSuite.KeyExchangeAlgorithm;
								switch (keyExchangeAlgorithm2)
								{
								case 3:
									this.processDHEKeyExchange(memoryStream, new TlsDssSigner());
									goto IL_7B5;
								case 4:
									break;
								case 5:
									this.processDHEKeyExchange(memoryStream, new TlsRsaSigner());
									goto IL_7B5;
								default:
									switch (keyExchangeAlgorithm2)
									{
									case 10:
										this.processSRPKeyExchange(memoryStream, null);
										goto IL_7B5;
									case 11:
										this.processSRPKeyExchange(memoryStream, new TlsRsaSigner());
										goto IL_7B5;
									case 12:
										this.processSRPKeyExchange(memoryStream, new TlsDssSigner());
										goto IL_7B5;
									}
									break;
								}
								this.FailWithError(2, 10);
								break;
							}
							default:
								this.FailWithError(2, 10);
								break;
							}
							IL_7B5:
							this.connection_state = 4;
							flag = true;
							goto IL_832;
						case 13:
							switch (this.connection_state)
							{
							case 3:
							case 4:
								if (this.connection_state == 3 && this.chosenCipherSuite.KeyExchangeAlgorithm != 1)
								{
									this.FailWithError(2, 10);
								}
								TlsUtilities.ReadOpaque8(memoryStream);
								TlsUtilities.ReadOpaque16(memoryStream);
								this.AssertEmpty(memoryStream);
								break;
							default:
								this.FailWithError(2, 10);
								break;
							}
							this.connection_state = 5;
							flag = true;
							goto IL_832;
						case 14:
							switch (this.connection_state)
							{
							case 3:
							case 4:
							case 5:
							{
								if (this.connection_state == 3 && this.chosenCipherSuite.KeyExchangeAlgorithm != 1)
								{
									this.FailWithError(2, 10);
								}
								this.AssertEmpty(memoryStream);
								bool flag2 = this.connection_state == 5;
								this.connection_state = 6;
								if (flag2)
								{
									this.sendClientCertificate();
								}
								short keyExchangeAlgorithm3 = this.chosenCipherSuite.KeyExchangeAlgorithm;
								switch (keyExchangeAlgorithm3)
								{
								case 1:
								{
									this.pms = new byte[48];
									this.pms[0] = 3;
									this.pms[1] = 1;
									this.random.NextBytes(this.pms, 2, 46);
									RsaBlindedEngine cipher = new RsaBlindedEngine();
									Pkcs1Encoding pkcs1Encoding = new Pkcs1Encoding(cipher);
									pkcs1Encoding.Init(true, new ParametersWithRandom(this.serverPublicKey, this.random));
									byte[] keData = null;
									try
									{
										keData = pkcs1Encoding.ProcessBlock(this.pms, 0, this.pms.Length);
									}
									catch (InvalidCipherTextException)
									{
										this.FailWithError(2, 80);
									}
									this.sendClientKeyExchange(keData);
									break;
								}
								case 2:
								case 4:
									goto IL_54E;
								case 3:
								case 5:
								{
									byte[] keData2 = BigIntegers.AsUnsignedByteArray(this.Yc);
									this.sendClientKeyExchange(keData2);
									break;
								}
								default:
									switch (keyExchangeAlgorithm3)
									{
									case 10:
									case 11:
									case 12:
									{
										byte[] keData3 = BigIntegers.AsUnsignedByteArray(this.SRP_A);
										this.sendClientKeyExchange(keData3);
										break;
									}
									default:
										goto IL_54E;
									}
									break;
								}
								IL_557:
								this.connection_state = 7;
								if (flag2 && this.clientPrivateKey != null)
								{
									this.sendCertificateVerify();
									this.connection_state = 8;
								}
								byte[] array3 = new byte[]
								{
									1
								};
								this.rs.WriteMessage(20, array3, 0, array3.Length);
								this.connection_state = 9;
								this.ms = new byte[48];
								byte[] array4 = new byte[this.clientRandom.Length + this.serverRandom.Length];
								Array.Copy(this.clientRandom, 0, array4, 0, this.clientRandom.Length);
								Array.Copy(this.serverRandom, 0, array4, this.clientRandom.Length, this.serverRandom.Length);
								TlsUtilities.PRF(this.pms, "master secret", array4, this.ms);
								this.rs.writeSuite = this.chosenCipherSuite;
								this.rs.writeSuite.Init(this, this.ms, this.clientRandom, this.serverRandom);
								byte[] array5 = new byte[12];
								byte[] array6 = new byte[36];
								this.rs.hash1.DoFinal(array6, 0);
								TlsUtilities.PRF(this.ms, "client finished", array6, array5);
								MemoryStream memoryStream3 = new MemoryStream();
								TlsUtilities.WriteUint8(20, memoryStream3);
								TlsUtilities.WriteUint24(12, memoryStream3);
								memoryStream3.Write(array5, 0, array5.Length);
								byte[] array7 = memoryStream3.ToArray();
								this.rs.WriteMessage(22, array7, 0, array7.Length);
								this.connection_state = 10;
								flag = true;
								goto IL_832;
								IL_54E:
								this.FailWithError(2, 10);
								goto IL_557;
							}
							default:
								this.FailWithError(2, 40);
								goto IL_832;
							}
							break;
						default:
							if (num3 == 20)
							{
								short num8 = this.connection_state;
								if (num8 == 11)
								{
									byte[] array8 = new byte[12];
									TlsUtilities.ReadFully(array8, memoryStream);
									this.AssertEmpty(memoryStream);
									byte[] array9 = new byte[12];
									byte[] array10 = new byte[36];
									this.rs.hash2.DoFinal(array10, 0);
									TlsUtilities.PRF(this.ms, "server finished", array10, array9);
									for (int i = 0; i < array8.Length; i++)
									{
										if (array8[i] != array9[i])
										{
											this.FailWithError(2, 40);
										}
									}
									this.connection_state = 12;
									this.appDataReady = true;
									flag = true;
									goto IL_832;
								}
								this.FailWithError(2, 10);
								goto IL_832;
							}
							break;
						}
						this.FailWithError(2, 10);
					}
				}
				IL_832:;
			}
			while (flag);
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000DD42C File Offset: 0x000DC42C
		private void processApplicationData()
		{
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000DD430 File Offset: 0x000DC430
		private void processAlert()
		{
			while (this.alertQueue.Available >= 2)
			{
				byte[] array = new byte[2];
				this.alertQueue.Read(array, 0, 2, 0);
				this.alertQueue.RemoveData(2);
				short num = (short)array[0];
				short num2 = (short)array[1];
				if (num == 2)
				{
					this.failedWithError = true;
					this.closed = true;
					try
					{
						this.rs.Close();
					}
					catch (Exception)
					{
					}
					throw new IOException(TlsProtocolHandler.TLS_ERROR_MESSAGE);
				}
				if (num2 == 0)
				{
					this.FailWithError(1, 0);
				}
			}
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000DD4C0 File Offset: 0x000DC4C0
		private void processChangeCipherSpec()
		{
			while (this.changeCipherSpecQueue.Available > 0)
			{
				byte[] array = new byte[1];
				this.changeCipherSpecQueue.Read(array, 0, 1, 0);
				this.changeCipherSpecQueue.RemoveData(1);
				if (array[0] != 1)
				{
					this.FailWithError(2, 10);
				}
				else if (this.connection_state == 10)
				{
					this.rs.readSuite = this.rs.writeSuite;
					this.connection_state = 11;
				}
				else
				{
					this.FailWithError(2, 40);
				}
			}
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000DD544 File Offset: 0x000DC544
		private void processDHEKeyExchange(MemoryStream inStr, TlsSigner tlsSigner)
		{
			Stream inStr2 = inStr;
			ISigner signer = null;
			if (tlsSigner != null)
			{
				signer = tlsSigner.CreateSigner();
				signer.Init(false, this.serverPublicKey);
				signer.BlockUpdate(this.clientRandom, 0, this.clientRandom.Length);
				signer.BlockUpdate(this.serverRandom, 0, this.serverRandom.Length);
				inStr2 = new SignerStream(inStr, signer, null);
			}
			byte[] bytes = TlsUtilities.ReadOpaque16(inStr2);
			byte[] bytes2 = TlsUtilities.ReadOpaque16(inStr2);
			byte[] bytes3 = TlsUtilities.ReadOpaque16(inStr2);
			if (signer != null)
			{
				byte[] signature = TlsUtilities.ReadOpaque16(inStr);
				if (!signer.VerifySignature(signature))
				{
					this.FailWithError(2, 42);
				}
			}
			this.AssertEmpty(inStr);
			BigInteger bigInteger = new BigInteger(1, bytes);
			BigInteger bigInteger2 = new BigInteger(1, bytes2);
			BigInteger bigInteger3 = new BigInteger(1, bytes3);
			if (!bigInteger.IsProbablePrime(10))
			{
				this.FailWithError(2, 47);
			}
			if (bigInteger2.CompareTo(BigInteger.Two) < 0 || bigInteger2.CompareTo(bigInteger.Subtract(BigInteger.Two)) > 0)
			{
				this.FailWithError(2, 47);
			}
			if (bigInteger3.CompareTo(BigInteger.Two) < 0 || bigInteger3.CompareTo(bigInteger.Subtract(BigInteger.One)) > 0)
			{
				this.FailWithError(2, 47);
			}
			DHParameters parameters = new DHParameters(bigInteger, bigInteger2);
			DHBasicKeyPairGenerator dhbasicKeyPairGenerator = new DHBasicKeyPairGenerator();
			dhbasicKeyPairGenerator.Init(new DHKeyGenerationParameters(this.random, parameters));
			AsymmetricCipherKeyPair asymmetricCipherKeyPair = dhbasicKeyPairGenerator.GenerateKeyPair();
			this.Yc = ((DHPublicKeyParameters)asymmetricCipherKeyPair.Public).Y;
			DHBasicAgreement dhbasicAgreement = new DHBasicAgreement();
			dhbasicAgreement.Init(asymmetricCipherKeyPair.Private);
			BigInteger n = dhbasicAgreement.CalculateAgreement(new DHPublicKeyParameters(bigInteger3, parameters));
			this.pms = BigIntegers.AsUnsignedByteArray(n);
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000DD6E0 File Offset: 0x000DC6E0
		private void processSRPKeyExchange(MemoryStream inStr, TlsSigner tlsSigner)
		{
			Stream inStr2 = inStr;
			ISigner signer = null;
			if (tlsSigner != null)
			{
				signer = tlsSigner.CreateSigner();
				signer.Init(false, this.serverPublicKey);
				signer.BlockUpdate(this.clientRandom, 0, this.clientRandom.Length);
				signer.BlockUpdate(this.serverRandom, 0, this.serverRandom.Length);
				inStr2 = new SignerStream(inStr, signer, null);
			}
			byte[] bytes = TlsUtilities.ReadOpaque16(inStr2);
			byte[] bytes2 = TlsUtilities.ReadOpaque16(inStr2);
			byte[] array = TlsUtilities.ReadOpaque8(inStr2);
			byte[] bytes3 = TlsUtilities.ReadOpaque16(inStr2);
			if (signer != null)
			{
				byte[] signature = TlsUtilities.ReadOpaque16(inStr);
				if (!signer.VerifySignature(signature))
				{
					this.FailWithError(2, 42);
				}
			}
			this.AssertEmpty(inStr);
			BigInteger n = new BigInteger(1, bytes);
			BigInteger g = new BigInteger(1, bytes2);
			byte[] salt = array;
			BigInteger serverB = new BigInteger(1, bytes3);
			Srp6Client srp6Client = new Srp6Client();
			srp6Client.Init(n, g, new Sha1Digest(), this.random);
			this.SRP_A = srp6Client.GenerateClientCredentials(salt, this.SRP_identity, this.SRP_password);
			try
			{
				BigInteger n2 = srp6Client.CalculateSecret(serverB);
				this.pms = BigIntegers.AsUnsignedByteArray(n2);
			}
			catch (CryptoException)
			{
				this.FailWithError(2, 47);
			}
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000DD80C File Offset: 0x000DC80C
		private void validateKeyUsage(X509CertificateStructure c, int keyUsageBits)
		{
			X509Extensions extensions = c.TbsCertificate.Extensions;
			if (extensions != null)
			{
				X509Extension extension = extensions.GetExtension(X509Extensions.KeyUsage);
				if (extension != null)
				{
					DerBitString instance = KeyUsage.GetInstance(extension);
					int num = (int)instance.GetBytes()[0];
					if ((num & keyUsageBits) != keyUsageBits)
					{
						this.FailWithError(2, 46);
					}
				}
			}
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000DD858 File Offset: 0x000DC858
		private void sendClientCertificate()
		{
			MemoryStream memoryStream = new MemoryStream();
			TlsUtilities.WriteUint8(11, memoryStream);
			this.clientCert.Encode(memoryStream);
			byte[] array = memoryStream.ToArray();
			this.rs.WriteMessage(22, array, 0, array.Length);
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000DD898 File Offset: 0x000DC898
		private void sendClientKeyExchange(byte[] keData)
		{
			MemoryStream memoryStream = new MemoryStream();
			TlsUtilities.WriteUint8(16, memoryStream);
			TlsUtilities.WriteUint24(keData.Length + 2, memoryStream);
			TlsUtilities.WriteOpaque16(keData, memoryStream);
			byte[] array = memoryStream.ToArray();
			this.rs.WriteMessage(22, array, 0, array.Length);
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000DD8E0 File Offset: 0x000DC8E0
		private void sendCertificateVerify()
		{
			try
			{
				byte[] array = new byte[36];
				this.rs.hash3.DoFinal(array, 0);
				byte[] array2 = this.clientSigner.CalculateRawSignature(this.clientPrivateKey, array);
				MemoryStream memoryStream = new MemoryStream();
				TlsUtilities.WriteUint8(15, memoryStream);
				TlsUtilities.WriteUint24(array2.Length + 2, memoryStream);
				TlsUtilities.WriteOpaque16(array2, memoryStream);
				byte[] array3 = memoryStream.ToArray();
				this.rs.WriteMessage(22, array3, 0, array3.Length);
			}
			catch (CryptoException)
			{
				this.FailWithError(2, 40);
			}
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x000DD974 File Offset: 0x000DC974
		public virtual void Connect(ICertificateVerifyer verifyer)
		{
			this.Connect(verifyer, null, null);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000DD980 File Offset: 0x000DC980
		internal virtual void Connect(ICertificateVerifyer verifyer, Certificate clientCertificate, AsymmetricKeyParameter clientPrivateKey)
		{
			if (clientCertificate == null)
			{
				clientCertificate = new Certificate(new X509CertificateStructure[0]);
			}
			if (clientPrivateKey == null)
			{
				if (clientCertificate.certs.Length != 0)
				{
					throw new ArgumentException("key not specified for certificate", "clientPrivateKey");
				}
			}
			else
			{
				if (clientCertificate.certs.Length == 0)
				{
					throw new ArgumentException("key specified without certificate", "clientPrivateKey");
				}
				if (!clientPrivateKey.IsPrivate)
				{
					throw new ArgumentException("must be private", "clientPrivateKey");
				}
				if (clientPrivateKey is RsaKeyParameters)
				{
					this.clientSigner = new TlsRsaSigner();
				}
				else
				{
					if (!(clientPrivateKey is DsaPrivateKeyParameters))
					{
						throw new ArgumentException("type not supported", "clientPrivateKey");
					}
					this.clientSigner = new TlsDssSigner();
				}
			}
			this.verifyer = verifyer;
			this.clientCert = clientCertificate;
			this.clientPrivateKey = clientPrivateKey;
			this.clientRandom = new byte[32];
			int num = (int)(DateTimeUtilities.CurrentUnixMs() / 1000L);
			this.clientRandom[0] = (byte)(num >> 24);
			this.clientRandom[1] = (byte)(num >> 16);
			this.clientRandom[2] = (byte)(num >> 8);
			this.clientRandom[3] = (byte)num;
			this.random.NextBytes(this.clientRandom, 4, 28);
			MemoryStream memoryStream = new MemoryStream();
			TlsUtilities.WriteVersion(memoryStream);
			memoryStream.Write(this.clientRandom, 0, this.clientRandom.Length);
			TlsUtilities.WriteUint8(0, memoryStream);
			TlsCipherSuiteManager.WriteCipherSuites(memoryStream);
			byte[] array = new byte[1];
			byte[] buf = array;
			TlsUtilities.WriteOpaque8(buf, memoryStream);
			Hashtable hashtable = new Hashtable();
			this.extendedClientHello = (hashtable.Count > 0);
			if (this.extendedClientHello)
			{
				MemoryStream memoryStream2 = new MemoryStream();
				foreach (object obj in hashtable.Keys)
				{
					int num2 = (int)obj;
					byte[] buf2 = (byte[])hashtable[num2];
					TlsUtilities.WriteUint16(num2, memoryStream2);
					TlsUtilities.WriteOpaque16(buf2, memoryStream2);
				}
				TlsUtilities.WriteOpaque16(memoryStream2.ToArray(), memoryStream);
			}
			MemoryStream memoryStream3 = new MemoryStream();
			TlsUtilities.WriteUint8(1, memoryStream3);
			TlsUtilities.WriteUint24((int)memoryStream.Length, memoryStream3);
			byte[] array2 = memoryStream.ToArray();
			memoryStream3.Write(array2, 0, array2.Length);
			byte[] array3 = memoryStream3.ToArray();
			this.rs.WriteMessage(22, array3, 0, array3.Length);
			this.connection_state = 1;
			while (this.connection_state != 12)
			{
				this.rs.ReadData();
			}
			this.tlsInputStream = new TlsInputStream(this);
			this.tlsOutputStream = new TlsOuputStream(this);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000DDC04 File Offset: 0x000DCC04
		internal int ReadApplicationData(byte[] buf, int offset, int len)
		{
			while (this.applicationDataQueue.Available == 0)
			{
				if (this.closed)
				{
					if (this.failedWithError)
					{
						throw new IOException(TlsProtocolHandler.TLS_ERROR_MESSAGE);
					}
					return 0;
				}
				else
				{
					try
					{
						this.rs.ReadData();
					}
					catch (IOException ex)
					{
						if (!this.closed)
						{
							this.FailWithError(2, 80);
						}
						throw ex;
					}
					catch (Exception ex2)
					{
						if (!this.closed)
						{
							this.FailWithError(2, 80);
						}
						throw ex2;
					}
				}
			}
			len = Math.Min(len, this.applicationDataQueue.Available);
			this.applicationDataQueue.Read(buf, offset, len, 0);
			this.applicationDataQueue.RemoveData(len);
			return len;
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000DDCC0 File Offset: 0x000DCCC0
		internal void WriteData(byte[] buf, int offset, int len)
		{
			if (!this.closed)
			{
				this.rs.WriteMessage(23, TlsProtocolHandler.emptybuf, 0, 0);
				do
				{
					int num = Math.Min(len, 16384);
					try
					{
						this.rs.WriteMessage(23, buf, offset, num);
					}
					catch (IOException ex)
					{
						if (!this.closed)
						{
							this.FailWithError(2, 80);
						}
						throw ex;
					}
					catch (Exception ex2)
					{
						if (!this.closed)
						{
							this.FailWithError(2, 80);
						}
						throw ex2;
					}
					offset += num;
					len -= num;
				}
				while (len > 0);
				return;
			}
			if (this.failedWithError)
			{
				throw new IOException(TlsProtocolHandler.TLS_ERROR_MESSAGE);
			}
			throw new IOException("Sorry, connection has been closed, you cannot write more data");
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06002451 RID: 9297 RVA: 0x000DDD78 File Offset: 0x000DCD78
		[Obsolete("Use 'OutputStream' property instead")]
		public TlsOuputStream TlsOuputStream
		{
			get
			{
				return this.tlsOutputStream;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x000DDD80 File Offset: 0x000DCD80
		public virtual Stream OutputStream
		{
			get
			{
				return this.tlsOutputStream;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06002453 RID: 9299 RVA: 0x000DDD88 File Offset: 0x000DCD88
		[Obsolete("Use 'InputStream' property instead")]
		public TlsInputStream TlsInputStream
		{
			get
			{
				return this.tlsInputStream;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x000DDD90 File Offset: 0x000DCD90
		public virtual Stream InputStream
		{
			get
			{
				return this.tlsInputStream;
			}
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000DDD98 File Offset: 0x000DCD98
		internal void FailWithError(short alertLevel, short alertDescription)
		{
			if (this.closed)
			{
				throw new IOException(TlsProtocolHandler.TLS_ERROR_MESSAGE);
			}
			byte[] message = new byte[]
			{
				(byte)alertLevel,
				(byte)alertDescription
			};
			this.closed = true;
			if (alertLevel == 2)
			{
				this.failedWithError = true;
			}
			this.rs.WriteMessage(21, message, 0, 2);
			this.rs.Close();
			if (alertLevel == 2)
			{
				throw new IOException(TlsProtocolHandler.TLS_ERROR_MESSAGE);
			}
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000DDE05 File Offset: 0x000DCE05
		public virtual void Close()
		{
			if (!this.closed)
			{
				this.FailWithError(1, 0);
			}
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000DDE17 File Offset: 0x000DCE17
		internal void AssertEmpty(MemoryStream inStr)
		{
			if (inStr.Position < inStr.Length)
			{
				this.FailWithError(2, 50);
			}
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000DDE30 File Offset: 0x000DCE30
		internal void Flush()
		{
			this.rs.Flush();
		}

		// Token: 0x04001926 RID: 6438
		private const short RL_CHANGE_CIPHER_SPEC = 20;

		// Token: 0x04001927 RID: 6439
		private const short RL_ALERT = 21;

		// Token: 0x04001928 RID: 6440
		private const short RL_HANDSHAKE = 22;

		// Token: 0x04001929 RID: 6441
		private const short RL_APPLICATION_DATA = 23;

		// Token: 0x0400192A RID: 6442
		private const short HP_HELLO_REQUEST = 0;

		// Token: 0x0400192B RID: 6443
		private const short HP_CLIENT_HELLO = 1;

		// Token: 0x0400192C RID: 6444
		private const short HP_SERVER_HELLO = 2;

		// Token: 0x0400192D RID: 6445
		private const short HP_CERTIFICATE = 11;

		// Token: 0x0400192E RID: 6446
		private const short HP_SERVER_KEY_EXCHANGE = 12;

		// Token: 0x0400192F RID: 6447
		private const short HP_CERTIFICATE_REQUEST = 13;

		// Token: 0x04001930 RID: 6448
		private const short HP_SERVER_HELLO_DONE = 14;

		// Token: 0x04001931 RID: 6449
		private const short HP_CERTIFICATE_VERIFY = 15;

		// Token: 0x04001932 RID: 6450
		private const short HP_CLIENT_KEY_EXCHANGE = 16;

		// Token: 0x04001933 RID: 6451
		private const short HP_FINISHED = 20;

		// Token: 0x04001934 RID: 6452
		private const short CS_CLIENT_HELLO_SEND = 1;

		// Token: 0x04001935 RID: 6453
		private const short CS_SERVER_HELLO_RECEIVED = 2;

		// Token: 0x04001936 RID: 6454
		private const short CS_SERVER_CERTIFICATE_RECEIVED = 3;

		// Token: 0x04001937 RID: 6455
		private const short CS_SERVER_KEY_EXCHANGE_RECEIVED = 4;

		// Token: 0x04001938 RID: 6456
		private const short CS_CERTIFICATE_REQUEST_RECEIVED = 5;

		// Token: 0x04001939 RID: 6457
		private const short CS_SERVER_HELLO_DONE_RECEIVED = 6;

		// Token: 0x0400193A RID: 6458
		private const short CS_CLIENT_KEY_EXCHANGE_SEND = 7;

		// Token: 0x0400193B RID: 6459
		private const short CS_CERTIFICATE_VERIFY_SEND = 8;

		// Token: 0x0400193C RID: 6460
		private const short CS_CLIENT_CHANGE_CIPHER_SPEC_SEND = 9;

		// Token: 0x0400193D RID: 6461
		private const short CS_CLIENT_FINISHED_SEND = 10;

		// Token: 0x0400193E RID: 6462
		private const short CS_SERVER_CHANGE_CIPHER_SPEC_RECEIVED = 11;

		// Token: 0x0400193F RID: 6463
		private const short CS_DONE = 12;

		// Token: 0x04001940 RID: 6464
		internal const short AP_close_notify = 0;

		// Token: 0x04001941 RID: 6465
		internal const short AP_unexpected_message = 10;

		// Token: 0x04001942 RID: 6466
		internal const short AP_bad_record_mac = 20;

		// Token: 0x04001943 RID: 6467
		internal const short AP_decryption_failed = 21;

		// Token: 0x04001944 RID: 6468
		internal const short AP_record_overflow = 22;

		// Token: 0x04001945 RID: 6469
		internal const short AP_decompression_failure = 30;

		// Token: 0x04001946 RID: 6470
		internal const short AP_handshake_failure = 40;

		// Token: 0x04001947 RID: 6471
		internal const short AP_bad_certificate = 42;

		// Token: 0x04001948 RID: 6472
		internal const short AP_unsupported_certificate = 43;

		// Token: 0x04001949 RID: 6473
		internal const short AP_certificate_revoked = 44;

		// Token: 0x0400194A RID: 6474
		internal const short AP_certificate_expired = 45;

		// Token: 0x0400194B RID: 6475
		internal const short AP_certificate_unknown = 46;

		// Token: 0x0400194C RID: 6476
		internal const short AP_illegal_parameter = 47;

		// Token: 0x0400194D RID: 6477
		internal const short AP_unknown_ca = 48;

		// Token: 0x0400194E RID: 6478
		internal const short AP_access_denied = 49;

		// Token: 0x0400194F RID: 6479
		internal const short AP_decode_error = 50;

		// Token: 0x04001950 RID: 6480
		internal const short AP_decrypt_error = 51;

		// Token: 0x04001951 RID: 6481
		internal const short AP_export_restriction = 60;

		// Token: 0x04001952 RID: 6482
		internal const short AP_protocol_version = 70;

		// Token: 0x04001953 RID: 6483
		internal const short AP_insufficient_security = 71;

		// Token: 0x04001954 RID: 6484
		internal const short AP_internal_error = 80;

		// Token: 0x04001955 RID: 6485
		internal const short AP_user_canceled = 90;

		// Token: 0x04001956 RID: 6486
		internal const short AP_no_renegotiation = 100;

		// Token: 0x04001957 RID: 6487
		internal const short AL_warning = 1;

		// Token: 0x04001958 RID: 6488
		internal const short AL_fatal = 2;

		// Token: 0x04001959 RID: 6489
		private static readonly byte[] emptybuf = new byte[0];

		// Token: 0x0400195A RID: 6490
		private static readonly string TLS_ERROR_MESSAGE = "Internal TLS error, this could be an attack";

		// Token: 0x0400195B RID: 6491
		private ByteQueue applicationDataQueue = new ByteQueue();

		// Token: 0x0400195C RID: 6492
		private ByteQueue changeCipherSpecQueue = new ByteQueue();

		// Token: 0x0400195D RID: 6493
		private ByteQueue alertQueue = new ByteQueue();

		// Token: 0x0400195E RID: 6494
		private ByteQueue handshakeQueue = new ByteQueue();

		// Token: 0x0400195F RID: 6495
		private RecordStream rs;

		// Token: 0x04001960 RID: 6496
		private SecureRandom random;

		// Token: 0x04001961 RID: 6497
		private AsymmetricKeyParameter serverPublicKey;

		// Token: 0x04001962 RID: 6498
		private AsymmetricKeyParameter clientPrivateKey;

		// Token: 0x04001963 RID: 6499
		private TlsInputStream tlsInputStream;

		// Token: 0x04001964 RID: 6500
		private TlsOuputStream tlsOutputStream;

		// Token: 0x04001965 RID: 6501
		private bool closed;

		// Token: 0x04001966 RID: 6502
		private bool failedWithError;

		// Token: 0x04001967 RID: 6503
		private bool appDataReady;

		// Token: 0x04001968 RID: 6504
		private bool extendedClientHello;

		// Token: 0x04001969 RID: 6505
		private byte[] clientRandom;

		// Token: 0x0400196A RID: 6506
		private byte[] serverRandom;

		// Token: 0x0400196B RID: 6507
		private byte[] ms;

		// Token: 0x0400196C RID: 6508
		private TlsCipherSuite chosenCipherSuite;

		// Token: 0x0400196D RID: 6509
		private BigInteger SRP_A;

		// Token: 0x0400196E RID: 6510
		private byte[] SRP_identity;

		// Token: 0x0400196F RID: 6511
		private byte[] SRP_password;

		// Token: 0x04001970 RID: 6512
		private BigInteger Yc;

		// Token: 0x04001971 RID: 6513
		private byte[] pms;

		// Token: 0x04001972 RID: 6514
		private ICertificateVerifyer verifyer;

		// Token: 0x04001973 RID: 6515
		private Certificate clientCert;

		// Token: 0x04001974 RID: 6516
		private TlsSigner clientSigner;

		// Token: 0x04001975 RID: 6517
		private short connection_state;
	}
}
