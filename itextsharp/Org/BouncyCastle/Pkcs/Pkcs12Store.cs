using System;
using System.Collections;
using System.Globalization;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Utilities;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x02000601 RID: 1537
	public class Pkcs12Store
	{
		// Token: 0x06003462 RID: 13410 RVA: 0x001454E2 File Offset: 0x001444E2
		private static SubjectKeyIdentifier CreateSubjectKeyID(AsymmetricKeyParameter pubKey)
		{
			return new SubjectKeyIdentifier(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pubKey));
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x001454F0 File Offset: 0x001444F0
		internal Pkcs12Store(DerObjectIdentifier keyAlgorithm, DerObjectIdentifier certAlgorithm, bool useDerEncoding)
		{
			this.keyAlgorithm = keyAlgorithm;
			this.certAlgorithm = certAlgorithm;
			this.useDerEncoding = useDerEncoding;
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x0014554F File Offset: 0x0014454F
		public Pkcs12Store() : this(PkcsObjectIdentifiers.PbeWithShaAnd3KeyTripleDesCbc, PkcsObjectIdentifiers.PbewithShaAnd40BitRC2Cbc, false)
		{
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x00145562 File Offset: 0x00144562
		public Pkcs12Store(Stream input, char[] password) : this()
		{
			this.Load(input, password);
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x00145574 File Offset: 0x00144574
		public void Load(Stream input, char[] password)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			Asn1Sequence seq = (Asn1Sequence)Asn1Object.FromStream(input);
			Pfx pfx = new Pfx(seq);
			ContentInfo authSafe = pfx.AuthSafe;
			bool flag = false;
			bool wrongPkcs12Zero = false;
			if (pfx.MacData != null)
			{
				MacData macData = pfx.MacData;
				DigestInfo mac = macData.Mac;
				AlgorithmIdentifier algorithmID = mac.AlgorithmID;
				byte[] salt = macData.GetSalt();
				int intValue = macData.IterationCount.IntValue;
				byte[] octets = ((Asn1OctetString)authSafe.Content).GetOctets();
				byte[] a = Pkcs12Store.CalculatePbeMac(algorithmID.ObjectID, salt, intValue, password, false, octets);
				byte[] digest = mac.GetDigest();
				if (!Arrays.ConstantTimeAreEqual(a, digest))
				{
					if (password.Length > 0)
					{
						throw new IOException("PKCS12 key store MAC invalid - wrong password or corrupted file.");
					}
					a = Pkcs12Store.CalculatePbeMac(algorithmID.ObjectID, salt, intValue, password, true, octets);
					if (!Arrays.ConstantTimeAreEqual(a, digest))
					{
						throw new IOException("PKCS12 key store MAC invalid - wrong password or corrupted file.");
					}
					wrongPkcs12Zero = true;
				}
			}
			this.keys.Clear();
			this.localIds.Clear();
			ArrayList arrayList = new ArrayList();
			if (authSafe.ContentType.Equals(PkcsObjectIdentifiers.Data))
			{
				byte[] octets2 = ((Asn1OctetString)authSafe.Content).GetOctets();
				AuthenticatedSafe authenticatedSafe = new AuthenticatedSafe((Asn1Sequence)Asn1Object.FromByteArray(octets2));
				ContentInfo[] contentInfo = authenticatedSafe.GetContentInfo();
				ContentInfo[] array = contentInfo;
				int i = 0;
				while (i < array.Length)
				{
					ContentInfo contentInfo2 = array[i];
					DerObjectIdentifier contentType = contentInfo2.ContentType;
					if (contentType.Equals(PkcsObjectIdentifiers.Data))
					{
						byte[] octets3 = ((Asn1OctetString)contentInfo2.Content).GetOctets();
						Asn1Sequence asn1Sequence = (Asn1Sequence)Asn1Object.FromByteArray(octets3);
						using (IEnumerator enumerator = asn1Sequence.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								Asn1Sequence seq2 = (Asn1Sequence)obj;
								SafeBag safeBag = new SafeBag(seq2);
								if (safeBag.BagID.Equals(PkcsObjectIdentifiers.Pkcs8ShroudedKeyBag))
								{
									EncryptedPrivateKeyInfo instance = EncryptedPrivateKeyInfo.GetInstance(safeBag.BagValue);
									PrivateKeyInfo keyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(password, wrongPkcs12Zero, instance);
									AsymmetricKeyParameter key = PrivateKeyFactory.CreateKey(keyInfo);
									Hashtable hashtable = new Hashtable();
									AsymmetricKeyEntry value = new AsymmetricKeyEntry(key, hashtable);
									string text = null;
									Asn1OctetString asn1OctetString = null;
									if (safeBag.BagAttributes != null)
									{
										foreach (object obj2 in safeBag.BagAttributes)
										{
											Asn1Sequence asn1Sequence2 = (Asn1Sequence)obj2;
											DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)asn1Sequence2[0];
											Asn1Set asn1Set = (Asn1Set)asn1Sequence2[1];
											if (asn1Set.Count > 0)
											{
												Asn1Encodable asn1Encodable = asn1Set[0];
												if (hashtable.ContainsKey(derObjectIdentifier.Id))
												{
													if (!hashtable[derObjectIdentifier.Id].Equals(asn1Encodable))
													{
														throw new IOException("attempt to add existing attribute with different value");
													}
												}
												else
												{
													hashtable.Add(derObjectIdentifier.Id, asn1Encodable);
												}
												if (derObjectIdentifier.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName))
												{
													text = ((DerBmpString)asn1Encodable).GetString();
													this.keys[text] = value;
												}
												else if (derObjectIdentifier.Equals(PkcsObjectIdentifiers.Pkcs9AtLocalKeyID))
												{
													asn1OctetString = (Asn1OctetString)asn1Encodable;
												}
											}
										}
									}
									if (asn1OctetString != null)
									{
										string text2 = Hex.ToHexString(asn1OctetString.GetOctets());
										if (text == null)
										{
											this.keys[text2] = value;
										}
										else
										{
											this.localIds[text] = text2;
										}
									}
									else
									{
										flag = true;
										this.keys["unmarked"] = value;
									}
								}
								else if (safeBag.BagID.Equals(PkcsObjectIdentifiers.CertBag))
								{
									arrayList.Add(safeBag);
								}
								else
								{
									Console.WriteLine("extra " + safeBag.BagID);
									Console.WriteLine("extra " + Asn1Dump.DumpAsString(safeBag));
								}
							}
							goto IL_7F9;
						}
						goto IL_3E4;
					}
					goto IL_3E4;
					IL_7F9:
					i++;
					continue;
					IL_3E4:
					if (contentType.Equals(PkcsObjectIdentifiers.EncryptedData))
					{
						EncryptedData instance2 = EncryptedData.GetInstance(contentInfo2.Content);
						byte[] data = Pkcs12Store.CryptPbeData(false, instance2.EncryptionAlgorithm, password, wrongPkcs12Zero, instance2.Content.GetOctets());
						Asn1Sequence asn1Sequence3 = (Asn1Sequence)Asn1Object.FromByteArray(data);
						using (IEnumerator enumerator = asn1Sequence3.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj3 = enumerator.Current;
								Asn1Sequence seq3 = (Asn1Sequence)obj3;
								SafeBag safeBag2 = new SafeBag(seq3);
								if (safeBag2.BagID.Equals(PkcsObjectIdentifiers.CertBag))
								{
									arrayList.Add(safeBag2);
								}
								else if (safeBag2.BagID.Equals(PkcsObjectIdentifiers.Pkcs8ShroudedKeyBag))
								{
									EncryptedPrivateKeyInfo instance3 = EncryptedPrivateKeyInfo.GetInstance(safeBag2.BagValue);
									PrivateKeyInfo keyInfo2 = PrivateKeyInfoFactory.CreatePrivateKeyInfo(password, wrongPkcs12Zero, instance3);
									AsymmetricKeyParameter key2 = PrivateKeyFactory.CreateKey(keyInfo2);
									Hashtable hashtable2 = new Hashtable();
									AsymmetricKeyEntry value2 = new AsymmetricKeyEntry(key2, hashtable2);
									string text3 = null;
									Asn1OctetString asn1OctetString2 = null;
									foreach (object obj4 in safeBag2.BagAttributes)
									{
										Asn1Sequence asn1Sequence4 = (Asn1Sequence)obj4;
										DerObjectIdentifier derObjectIdentifier2 = (DerObjectIdentifier)asn1Sequence4[0];
										Asn1Set asn1Set2 = (Asn1Set)asn1Sequence4[1];
										if (asn1Set2.Count > 0)
										{
											Asn1Encodable asn1Encodable2 = asn1Set2[0];
											if (hashtable2.ContainsKey(derObjectIdentifier2.Id))
											{
												if (!hashtable2[derObjectIdentifier2.Id].Equals(asn1Encodable2))
												{
													throw new IOException("attempt to add existing attribute with different value");
												}
											}
											else
											{
												hashtable2.Add(derObjectIdentifier2.Id, asn1Encodable2);
											}
											if (derObjectIdentifier2.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName))
											{
												text3 = ((DerBmpString)asn1Encodable2).GetString();
												this.keys[text3] = value2;
											}
											else if (derObjectIdentifier2.Equals(PkcsObjectIdentifiers.Pkcs9AtLocalKeyID))
											{
												asn1OctetString2 = (Asn1OctetString)asn1Encodable2;
											}
										}
									}
									string text4 = Hex.ToHexString(asn1OctetString2.GetOctets());
									if (text3 == null)
									{
										this.keys[text4] = value2;
									}
									else
									{
										this.localIds[text3] = text4;
									}
								}
								else if (safeBag2.BagID.Equals(PkcsObjectIdentifiers.KeyBag))
								{
									PrivateKeyInfo instance4 = PrivateKeyInfo.GetInstance(safeBag2.BagValue);
									AsymmetricKeyParameter key3 = PrivateKeyFactory.CreateKey(instance4);
									string text5 = null;
									Asn1OctetString asn1OctetString3 = null;
									Hashtable hashtable3 = new Hashtable();
									AsymmetricKeyEntry value3 = new AsymmetricKeyEntry(key3, hashtable3);
									foreach (object obj5 in safeBag2.BagAttributes)
									{
										Asn1Sequence asn1Sequence5 = (Asn1Sequence)obj5;
										DerObjectIdentifier derObjectIdentifier3 = (DerObjectIdentifier)asn1Sequence5[0];
										Asn1Set asn1Set3 = (Asn1Set)asn1Sequence5[1];
										if (asn1Set3.Count > 0)
										{
											Asn1Encodable asn1Encodable3 = asn1Set3[0];
											if (hashtable3.ContainsKey(derObjectIdentifier3.Id))
											{
												if (!hashtable3[derObjectIdentifier3.Id].Equals(asn1Encodable3))
												{
													throw new IOException("attempt to add existing attribute with different value");
												}
											}
											else
											{
												hashtable3.Add(derObjectIdentifier3.Id, asn1Encodable3);
											}
											if (derObjectIdentifier3.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName))
											{
												text5 = ((DerBmpString)asn1Encodable3).GetString();
												this.keys[text5] = value3;
											}
											else if (derObjectIdentifier3.Equals(PkcsObjectIdentifiers.Pkcs9AtLocalKeyID))
											{
												asn1OctetString3 = (Asn1OctetString)asn1Encodable3;
											}
										}
									}
									string text6 = Hex.ToHexString(asn1OctetString3.GetOctets());
									if (text5 == null)
									{
										this.keys[text6] = value3;
									}
									else
									{
										this.localIds[text5] = text6;
									}
								}
								else
								{
									Console.WriteLine("extra " + safeBag2.BagID);
									Console.WriteLine("extra " + Asn1Dump.DumpAsString(safeBag2));
								}
							}
							goto IL_7F9;
						}
					}
					Console.WriteLine("extra " + contentType);
					Console.WriteLine("extra " + Asn1Dump.DumpAsString(contentInfo2.Content));
					goto IL_7F9;
				}
			}
			this.certs.Clear();
			this.chainCerts.Clear();
			this.keyCerts.Clear();
			foreach (object obj6 in arrayList)
			{
				SafeBag safeBag3 = (SafeBag)obj6;
				CertBag certBag = new CertBag((Asn1Sequence)safeBag3.BagValue);
				byte[] octets4 = ((Asn1OctetString)certBag.CertValue).GetOctets();
				X509Certificate x509Certificate = new X509CertificateParser().ReadCertificate(octets4);
				Hashtable hashtable4 = new Hashtable();
				Asn1OctetString asn1OctetString4 = null;
				string text7 = null;
				if (safeBag3.BagAttributes != null)
				{
					foreach (object obj7 in safeBag3.BagAttributes)
					{
						Asn1Sequence asn1Sequence6 = (Asn1Sequence)obj7;
						DerObjectIdentifier derObjectIdentifier4 = (DerObjectIdentifier)asn1Sequence6[0];
						Asn1Set asn1Set4 = (Asn1Set)asn1Sequence6[1];
						if (asn1Set4.Count > 0)
						{
							Asn1Encodable asn1Encodable4 = asn1Set4[0];
							if (hashtable4.ContainsKey(derObjectIdentifier4.Id))
							{
								if (!hashtable4[derObjectIdentifier4.Id].Equals(asn1Encodable4))
								{
									throw new IOException("attempt to add existing attribute with different value");
								}
							}
							else
							{
								hashtable4.Add(derObjectIdentifier4.Id, asn1Encodable4);
							}
							if (derObjectIdentifier4.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName))
							{
								text7 = ((DerBmpString)asn1Encodable4).GetString();
							}
							else if (derObjectIdentifier4.Equals(PkcsObjectIdentifiers.Pkcs9AtLocalKeyID))
							{
								asn1OctetString4 = (Asn1OctetString)asn1Encodable4;
							}
						}
					}
				}
				Pkcs12Store.CertId certId = new Pkcs12Store.CertId(x509Certificate.GetPublicKey());
				X509CertificateEntry value4 = new X509CertificateEntry(x509Certificate, hashtable4);
				this.chainCerts[certId] = value4;
				if (flag)
				{
					if (this.keyCerts.Count == 0)
					{
						string text8 = Hex.ToHexString(certId.Id);
						this.keyCerts[text8] = value4;
						object value5 = this.keys["unmarked"];
						this.keys.Remove("unmarked");
						this.keys[text8] = value5;
					}
				}
				else
				{
					if (asn1OctetString4 != null)
					{
						string key4 = Hex.ToHexString(asn1OctetString4.GetOctets());
						this.keyCerts[key4] = value4;
					}
					if (text7 != null)
					{
						this.certs[text7] = value4;
					}
				}
			}
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x00146094 File Offset: 0x00145094
		public AsymmetricKeyEntry GetKey(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			return (AsymmetricKeyEntry)this.keys[alias];
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x001460B5 File Offset: 0x001450B5
		public bool IsCertificateEntry(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			return this.certs[alias] != null && this.keys[alias] == null;
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x001460E4 File Offset: 0x001450E4
		public bool IsKeyEntry(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			return this.keys[alias] != null;
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x00146108 File Offset: 0x00145108
		private Hashtable GetAliasesTable()
		{
			Hashtable hashtable = new Hashtable();
			foreach (object obj in this.certs.Keys)
			{
				string key = (string)obj;
				hashtable[key] = "cert";
			}
			foreach (object obj2 in this.keys.Keys)
			{
				string key2 = (string)obj2;
				if (hashtable[key2] == null)
				{
					hashtable[key2] = "key";
				}
			}
			return hashtable;
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x001461D8 File Offset: 0x001451D8
		public IEnumerable Aliases
		{
			get
			{
				return new EnumerableProxy(this.GetAliasesTable().Keys);
			}
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x001461EA File Offset: 0x001451EA
		public bool ContainsAlias(string alias)
		{
			return this.certs[alias] != null || this.keys[alias] != null;
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x00146210 File Offset: 0x00145210
		public X509CertificateEntry GetCertificate(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			X509CertificateEntry x509CertificateEntry = (X509CertificateEntry)this.certs[alias];
			if (x509CertificateEntry == null)
			{
				string text = (string)this.localIds[alias];
				if (text != null)
				{
					x509CertificateEntry = (X509CertificateEntry)this.keyCerts[text];
				}
				else
				{
					x509CertificateEntry = (X509CertificateEntry)this.keyCerts[alias];
				}
			}
			return x509CertificateEntry;
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x0014627C File Offset: 0x0014527C
		public string GetCertificateAlias(X509Certificate cert)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			foreach (object obj in this.certs)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				X509CertificateEntry x509CertificateEntry = (X509CertificateEntry)dictionaryEntry.Value;
				if (x509CertificateEntry.Certificate.Equals(cert))
				{
					return (string)dictionaryEntry.Key;
				}
			}
			foreach (object obj2 in this.keyCerts)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
				X509CertificateEntry x509CertificateEntry2 = (X509CertificateEntry)dictionaryEntry2.Value;
				if (x509CertificateEntry2.Certificate.Equals(cert))
				{
					return (string)dictionaryEntry2.Key;
				}
			}
			return null;
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x00146388 File Offset: 0x00145388
		public X509CertificateEntry[] GetCertificateChain(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			if (!this.IsKeyEntry(alias))
			{
				return null;
			}
			X509CertificateEntry x509CertificateEntry = this.GetCertificate(alias);
			if (x509CertificateEntry != null)
			{
				ArrayList arrayList = new ArrayList();
				while (x509CertificateEntry != null)
				{
					X509Certificate certificate = x509CertificateEntry.Certificate;
					X509CertificateEntry x509CertificateEntry2 = null;
					Asn1OctetString extensionValue = certificate.GetExtensionValue(X509Extensions.AuthorityKeyIdentifier);
					if (extensionValue != null)
					{
						AuthorityKeyIdentifier instance = AuthorityKeyIdentifier.GetInstance(Asn1Object.FromByteArray(extensionValue.GetOctets()));
						if (instance.GetKeyIdentifier() != null)
						{
							x509CertificateEntry2 = (X509CertificateEntry)this.chainCerts[new Pkcs12Store.CertId(instance.GetKeyIdentifier())];
						}
					}
					if (x509CertificateEntry2 == null)
					{
						X509Name issuerDN = certificate.IssuerDN;
						X509Name subjectDN = certificate.SubjectDN;
						if (!issuerDN.Equivalent(subjectDN))
						{
							foreach (object obj in this.chainCerts.Keys)
							{
								Pkcs12Store.CertId key = (Pkcs12Store.CertId)obj;
								X509CertificateEntry x509CertificateEntry3 = (X509CertificateEntry)this.chainCerts[key];
								X509Certificate certificate2 = x509CertificateEntry3.Certificate;
								X509Name subjectDN2 = certificate2.SubjectDN;
								if (subjectDN2.Equivalent(issuerDN))
								{
									try
									{
										certificate.Verify(certificate2.GetPublicKey());
										x509CertificateEntry2 = x509CertificateEntry3;
										break;
									}
									catch (InvalidKeyException)
									{
									}
								}
							}
						}
					}
					arrayList.Add(x509CertificateEntry);
					if (x509CertificateEntry2 != x509CertificateEntry)
					{
						x509CertificateEntry = x509CertificateEntry2;
					}
					else
					{
						x509CertificateEntry = null;
					}
				}
				return (X509CertificateEntry[])arrayList.ToArray(typeof(X509CertificateEntry));
			}
			return null;
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x00146514 File Offset: 0x00145514
		public void SetCertificateEntry(string alias, X509CertificateEntry certEntry)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			if (certEntry == null)
			{
				throw new ArgumentNullException("certEntry");
			}
			if (this.keys[alias] != null)
			{
				throw new ArgumentException("There is a key entry with the name " + alias + ".");
			}
			this.certs[alias] = certEntry;
			this.chainCerts[new Pkcs12Store.CertId(certEntry.Certificate.GetPublicKey())] = certEntry;
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x0014658C File Offset: 0x0014558C
		public void SetKeyEntry(string alias, AsymmetricKeyEntry keyEntry, X509CertificateEntry[] chain)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			if (keyEntry == null)
			{
				throw new ArgumentNullException("keyEntry");
			}
			if (keyEntry.Key.IsPrivate && chain == null)
			{
				throw new ArgumentException("No certificate chain for private key");
			}
			if (this.keys[alias] != null)
			{
				this.DeleteEntry(alias);
			}
			this.keys[alias] = keyEntry;
			this.certs[alias] = chain[0];
			for (int num = 0; num != chain.Length; num++)
			{
				this.chainCerts[new Pkcs12Store.CertId(chain[num].Certificate.GetPublicKey())] = chain[num];
			}
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x00146630 File Offset: 0x00145630
		public void DeleteEntry(string alias)
		{
			if (alias == null)
			{
				throw new ArgumentNullException("alias");
			}
			AsymmetricKeyEntry asymmetricKeyEntry = (AsymmetricKeyEntry)this.keys[alias];
			if (asymmetricKeyEntry != null)
			{
				this.keys.Remove(alias);
			}
			X509CertificateEntry x509CertificateEntry = (X509CertificateEntry)this.certs[alias];
			if (x509CertificateEntry != null)
			{
				this.certs.Remove(alias);
				this.chainCerts.Remove(new Pkcs12Store.CertId(x509CertificateEntry.Certificate.GetPublicKey()));
			}
			if (asymmetricKeyEntry != null)
			{
				string text = (string)this.localIds[alias];
				if (text != null)
				{
					this.localIds.Remove(alias);
					x509CertificateEntry = (X509CertificateEntry)this.keyCerts[text];
				}
				if (x509CertificateEntry != null)
				{
					this.keyCerts.Remove(text);
					this.chainCerts.Remove(new Pkcs12Store.CertId(x509CertificateEntry.Certificate.GetPublicKey()));
				}
			}
			if (x509CertificateEntry == null && asymmetricKeyEntry == null)
			{
				throw new ArgumentException("no such entry as " + alias);
			}
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x00146721 File Offset: 0x00145721
		public bool IsEntryOfType(string alias, Type entryType)
		{
			if (entryType == typeof(X509CertificateEntry))
			{
				return this.IsCertificateEntry(alias);
			}
			return entryType == typeof(AsymmetricKeyEntry) && this.IsKeyEntry(alias) && this.GetCertificate(alias) != null;
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x0014675F File Offset: 0x0014575F
		[Obsolete("Use 'Count' property instead")]
		public int Size()
		{
			return this.Count;
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06003475 RID: 13429 RVA: 0x00146767 File Offset: 0x00145767
		public int Count
		{
			get
			{
				return this.GetAliasesTable().Count;
			}
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x00146774 File Offset: 0x00145774
		public void Save(Stream stream, char[] password, SecureRandom random)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			if (random == null)
			{
				throw new ArgumentNullException("random");
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in this.keys.Keys)
			{
				string text = (string)obj;
				byte[] array = new byte[20];
				random.NextBytes(array);
				AsymmetricKeyEntry asymmetricKeyEntry = (AsymmetricKeyEntry)this.keys[text];
				EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(this.keyAlgorithm, password, array, 1024, asymmetricKeyEntry.Key);
				Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
				foreach (object obj2 in asymmetricKeyEntry.BagAttributeKeys)
				{
					string text2 = (string)obj2;
					Asn1Encodable obj3 = asymmetricKeyEntry[text2];
					if (!text2.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName.Id))
					{
						asn1EncodableVector2.Add(new Asn1Encodable[]
						{
							new DerSequence(new Asn1Encodable[]
							{
								new DerObjectIdentifier(text2),
								new DerSet(obj3)
							})
						});
					}
				}
				asn1EncodableVector2.Add(new Asn1Encodable[]
				{
					new DerSequence(new Asn1Encodable[]
					{
						PkcsObjectIdentifiers.Pkcs9AtFriendlyName,
						new DerSet(new DerBmpString(text))
					})
				});
				if (asymmetricKeyEntry[PkcsObjectIdentifiers.Pkcs9AtLocalKeyID] == null)
				{
					X509CertificateEntry certificate = this.GetCertificate(text);
					AsymmetricKeyParameter publicKey = certificate.Certificate.GetPublicKey();
					SubjectKeyIdentifier obj4 = Pkcs12Store.CreateSubjectKeyID(publicKey);
					asn1EncodableVector2.Add(new Asn1Encodable[]
					{
						new DerSequence(new Asn1Encodable[]
						{
							PkcsObjectIdentifiers.Pkcs9AtLocalKeyID,
							new DerSet(obj4)
						})
					});
				}
				SafeBag safeBag = new SafeBag(PkcsObjectIdentifiers.Pkcs8ShroudedKeyBag, encryptedPrivateKeyInfo.ToAsn1Object(), new DerSet(asn1EncodableVector2));
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					safeBag
				});
			}
			byte[] derEncoded = new DerSequence(asn1EncodableVector).GetDerEncoded();
			BerOctetString content = new BerOctetString(derEncoded);
			byte[] array2 = new byte[20];
			random.NextBytes(array2);
			Asn1EncodableVector asn1EncodableVector3 = new Asn1EncodableVector(new Asn1Encodable[0]);
			Pkcs12PbeParams pkcs12PbeParams = new Pkcs12PbeParams(array2, 1024);
			AlgorithmIdentifier algorithmIdentifier = new AlgorithmIdentifier(this.certAlgorithm, pkcs12PbeParams.ToAsn1Object());
			ISet set = new HashSet();
			foreach (object obj5 in this.keys.Keys)
			{
				string text3 = (string)obj5;
				X509CertificateEntry certificate2 = this.GetCertificate(text3);
				CertBag certBag = new CertBag(PkcsObjectIdentifiers.X509Certificate, new DerOctetString(certificate2.Certificate.GetEncoded()));
				Asn1EncodableVector asn1EncodableVector4 = new Asn1EncodableVector(new Asn1Encodable[0]);
				foreach (object obj6 in certificate2.BagAttributeKeys)
				{
					string text4 = (string)obj6;
					Asn1Encodable obj7 = certificate2[text4];
					if (!text4.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName.Id))
					{
						asn1EncodableVector4.Add(new Asn1Encodable[]
						{
							new DerSequence(new Asn1Encodable[]
							{
								new DerObjectIdentifier(text4),
								new DerSet(obj7)
							})
						});
					}
				}
				asn1EncodableVector4.Add(new Asn1Encodable[]
				{
					new DerSequence(new Asn1Encodable[]
					{
						PkcsObjectIdentifiers.Pkcs9AtFriendlyName,
						new DerSet(new DerBmpString(text3))
					})
				});
				if (certificate2[PkcsObjectIdentifiers.Pkcs9AtLocalKeyID] == null)
				{
					AsymmetricKeyParameter publicKey2 = certificate2.Certificate.GetPublicKey();
					SubjectKeyIdentifier obj8 = Pkcs12Store.CreateSubjectKeyID(publicKey2);
					asn1EncodableVector4.Add(new Asn1Encodable[]
					{
						new DerSequence(new Asn1Encodable[]
						{
							PkcsObjectIdentifiers.Pkcs9AtLocalKeyID,
							new DerSet(obj8)
						})
					});
				}
				SafeBag safeBag2 = new SafeBag(PkcsObjectIdentifiers.CertBag, certBag.ToAsn1Object(), new DerSet(asn1EncodableVector4));
				asn1EncodableVector3.Add(new Asn1Encodable[]
				{
					safeBag2
				});
				set.Add(certificate2.Certificate);
			}
			foreach (object obj9 in this.certs.Keys)
			{
				string text5 = (string)obj9;
				X509CertificateEntry x509CertificateEntry = (X509CertificateEntry)this.certs[text5];
				if (this.keys[text5] == null)
				{
					CertBag certBag2 = new CertBag(PkcsObjectIdentifiers.X509Certificate, new DerOctetString(x509CertificateEntry.Certificate.GetEncoded()));
					Asn1EncodableVector asn1EncodableVector5 = new Asn1EncodableVector(new Asn1Encodable[0]);
					foreach (object obj10 in x509CertificateEntry.BagAttributeKeys)
					{
						string text6 = (string)obj10;
						if (!text6.Equals(PkcsObjectIdentifiers.Pkcs9AtLocalKeyID.Id))
						{
							Asn1Encodable obj11 = x509CertificateEntry[text6];
							if (!text6.Equals(PkcsObjectIdentifiers.Pkcs9AtFriendlyName.Id))
							{
								asn1EncodableVector5.Add(new Asn1Encodable[]
								{
									new DerSequence(new Asn1Encodable[]
									{
										new DerObjectIdentifier(text6),
										new DerSet(obj11)
									})
								});
							}
						}
					}
					asn1EncodableVector5.Add(new Asn1Encodable[]
					{
						new DerSequence(new Asn1Encodable[]
						{
							PkcsObjectIdentifiers.Pkcs9AtFriendlyName,
							new DerSet(new DerBmpString(text5))
						})
					});
					SafeBag safeBag3 = new SafeBag(PkcsObjectIdentifiers.CertBag, certBag2.ToAsn1Object(), new DerSet(asn1EncodableVector5));
					asn1EncodableVector3.Add(new Asn1Encodable[]
					{
						safeBag3
					});
					set.Add(x509CertificateEntry.Certificate);
				}
			}
			foreach (object obj12 in this.chainCerts.Keys)
			{
				Pkcs12Store.CertId key = (Pkcs12Store.CertId)obj12;
				X509CertificateEntry x509CertificateEntry2 = (X509CertificateEntry)this.chainCerts[key];
				if (!set.Contains(x509CertificateEntry2.Certificate))
				{
					CertBag certBag3 = new CertBag(PkcsObjectIdentifiers.X509Certificate, new DerOctetString(x509CertificateEntry2.Certificate.GetEncoded()));
					Asn1EncodableVector asn1EncodableVector6 = new Asn1EncodableVector(new Asn1Encodable[0]);
					foreach (object obj13 in x509CertificateEntry2.BagAttributeKeys)
					{
						string text7 = (string)obj13;
						if (!text7.Equals(PkcsObjectIdentifiers.Pkcs9AtLocalKeyID.Id))
						{
							asn1EncodableVector6.Add(new Asn1Encodable[]
							{
								new DerSequence(new Asn1Encodable[]
								{
									new DerObjectIdentifier(text7),
									new DerSet(x509CertificateEntry2[text7])
								})
							});
						}
					}
					SafeBag safeBag4 = new SafeBag(PkcsObjectIdentifiers.CertBag, certBag3.ToAsn1Object(), new DerSet(asn1EncodableVector6));
					asn1EncodableVector3.Add(new Asn1Encodable[]
					{
						safeBag4
					});
				}
			}
			derEncoded = new DerSequence(asn1EncodableVector3).GetDerEncoded();
			byte[] str = Pkcs12Store.CryptPbeData(true, algorithmIdentifier, password, false, derEncoded);
			EncryptedData encryptedData = new EncryptedData(PkcsObjectIdentifiers.Data, algorithmIdentifier, new BerOctetString(str));
			ContentInfo[] info = new ContentInfo[]
			{
				new ContentInfo(PkcsObjectIdentifiers.Data, content),
				new ContentInfo(PkcsObjectIdentifiers.EncryptedData, encryptedData.ToAsn1Object())
			};
			byte[] encoded = new AuthenticatedSafe(info).GetEncoded(this.useDerEncoding ? "DER" : "BER");
			ContentInfo contentInfo = new ContentInfo(PkcsObjectIdentifiers.Data, new BerOctetString(encoded));
			byte[] array3 = new byte[20];
			random.NextBytes(array3);
			byte[] digest = Pkcs12Store.CalculatePbeMac(OiwObjectIdentifiers.IdSha1, array3, 1024, password, false, encoded);
			AlgorithmIdentifier algID = new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1, DerNull.Instance);
			DigestInfo digInfo = new DigestInfo(algID, digest);
			MacData macData = new MacData(digInfo, array3, 1024);
			Pfx obj14 = new Pfx(contentInfo, macData);
			DerOutputStream derOutputStream;
			if (this.useDerEncoding)
			{
				derOutputStream = new DerOutputStream(stream);
			}
			else
			{
				derOutputStream = new BerOutputStream(stream);
			}
			derOutputStream.WriteObject(obj14);
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x001470EC File Offset: 0x001460EC
		internal static byte[] CalculatePbeMac(DerObjectIdentifier oid, byte[] salt, int itCount, char[] password, bool wrongPkcs12Zero, byte[] data)
		{
			Asn1Encodable pbeParameters = PbeUtilities.GenerateAlgorithmParameters(oid, salt, itCount);
			ICipherParameters parameters = PbeUtilities.GenerateCipherParameters(oid, password, wrongPkcs12Zero, pbeParameters);
			IMac mac = (IMac)PbeUtilities.CreateEngine(oid);
			mac.Init(parameters);
			mac.BlockUpdate(data, 0, data.Length);
			return MacUtilities.DoFinal(mac);
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x00147134 File Offset: 0x00146134
		private static byte[] CryptPbeData(bool forEncryption, AlgorithmIdentifier algId, char[] password, bool wrongPkcs12Zero, byte[] data)
		{
			Pkcs12PbeParams instance = Pkcs12PbeParams.GetInstance(algId.Parameters);
			ICipherParameters parameters = PbeUtilities.GenerateCipherParameters(algId.ObjectID, password, wrongPkcs12Zero, instance);
			IBufferedCipher bufferedCipher = PbeUtilities.CreateEngine(algId.ObjectID) as IBufferedCipher;
			if (bufferedCipher == null)
			{
				throw new Exception("Unknown encryption algorithm: " + algId.ObjectID);
			}
			bufferedCipher.Init(forEncryption, parameters);
			return bufferedCipher.DoFinal(data);
		}

		// Token: 0x04002346 RID: 9030
		private const int MinIterations = 1024;

		// Token: 0x04002347 RID: 9031
		private const int SaltSize = 20;

		// Token: 0x04002348 RID: 9032
		private readonly Pkcs12Store.IgnoresCaseHashtable keys = new Pkcs12Store.IgnoresCaseHashtable();

		// Token: 0x04002349 RID: 9033
		private readonly Hashtable localIds = new Hashtable();

		// Token: 0x0400234A RID: 9034
		private readonly Pkcs12Store.IgnoresCaseHashtable certs = new Pkcs12Store.IgnoresCaseHashtable();

		// Token: 0x0400234B RID: 9035
		private readonly Hashtable chainCerts = new Hashtable();

		// Token: 0x0400234C RID: 9036
		private readonly Hashtable keyCerts = new Hashtable();

		// Token: 0x0400234D RID: 9037
		private readonly DerObjectIdentifier keyAlgorithm;

		// Token: 0x0400234E RID: 9038
		private readonly DerObjectIdentifier certAlgorithm;

		// Token: 0x0400234F RID: 9039
		private readonly bool useDerEncoding;

		// Token: 0x02000602 RID: 1538
		internal class CertId
		{
			// Token: 0x06003479 RID: 13433 RVA: 0x00147196 File Offset: 0x00146196
			internal CertId(AsymmetricKeyParameter pubKey)
			{
				this.id = Pkcs12Store.CreateSubjectKeyID(pubKey).GetKeyIdentifier();
			}

			// Token: 0x0600347A RID: 13434 RVA: 0x001471AF File Offset: 0x001461AF
			internal CertId(byte[] id)
			{
				this.id = id;
			}

			// Token: 0x17000912 RID: 2322
			// (get) Token: 0x0600347B RID: 13435 RVA: 0x001471BE File Offset: 0x001461BE
			internal byte[] Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x0600347C RID: 13436 RVA: 0x001471C6 File Offset: 0x001461C6
			public override int GetHashCode()
			{
				return Arrays.GetHashCode(this.id);
			}

			// Token: 0x0600347D RID: 13437 RVA: 0x001471D4 File Offset: 0x001461D4
			public override bool Equals(object obj)
			{
				if (obj == this)
				{
					return true;
				}
				Pkcs12Store.CertId certId = obj as Pkcs12Store.CertId;
				return certId != null && Arrays.AreEqual(this.id, certId.id);
			}

			// Token: 0x04002350 RID: 9040
			private readonly byte[] id;
		}

		// Token: 0x02000603 RID: 1539
		private class IgnoresCaseHashtable : IEnumerable
		{
			// Token: 0x0600347E RID: 13438 RVA: 0x00147204 File Offset: 0x00146204
			public void Clear()
			{
				this.orig.Clear();
				this.keys.Clear();
			}

			// Token: 0x0600347F RID: 13439 RVA: 0x0014721C File Offset: 0x0014621C
			public IEnumerator GetEnumerator()
			{
				return this.orig.GetEnumerator();
			}

			// Token: 0x17000913 RID: 2323
			// (get) Token: 0x06003480 RID: 13440 RVA: 0x00147229 File Offset: 0x00146229
			public ICollection Keys
			{
				get
				{
					return this.orig.Keys;
				}
			}

			// Token: 0x06003481 RID: 13441 RVA: 0x00147238 File Offset: 0x00146238
			public object Remove(string alias)
			{
				string key = alias.ToLower(CultureInfo.InvariantCulture);
				string text = (string)this.keys[key];
				if (text == null)
				{
					return null;
				}
				this.keys.Remove(key);
				object result = this.orig[text];
				this.orig.Remove(text);
				return result;
			}

			// Token: 0x17000914 RID: 2324
			public object this[string alias]
			{
				get
				{
					string key = alias.ToLower(CultureInfo.InvariantCulture);
					string text = (string)this.keys[key];
					if (text == null)
					{
						return null;
					}
					return this.orig[text];
				}
				set
				{
					string key = alias.ToLower(CultureInfo.InvariantCulture);
					string text = (string)this.keys[key];
					if (text != null)
					{
						this.orig.Remove(text);
					}
					this.keys[key] = alias;
					this.orig[alias] = value;
				}
			}

			// Token: 0x17000915 RID: 2325
			// (get) Token: 0x06003484 RID: 13444 RVA: 0x00147320 File Offset: 0x00146320
			public ICollection Values
			{
				get
				{
					return this.orig.Values;
				}
			}

			// Token: 0x04002351 RID: 9041
			private readonly Hashtable orig = new Hashtable();

			// Token: 0x04002352 RID: 9042
			private readonly Hashtable keys = new Hashtable();
		}
	}
}
