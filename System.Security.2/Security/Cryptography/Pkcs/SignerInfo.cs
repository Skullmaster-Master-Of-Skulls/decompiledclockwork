using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000086 RID: 134
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SignerInfo
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x000044A9 File Offset: 0x000026A9
		private SignerInfo()
		{
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001A824 File Offset: 0x00018A24
		[SecurityCritical]
		internal SignerInfo(SignedCms signedCms, SafeLocalAllocHandle pbCmsgSignerInfo)
		{
			this.m_signedCms = signedCms;
			this.m_parentSignerInfo = null;
			this.m_encodedSignerInfo = null;
			this.m_pbCmsgSignerInfo = pbCmsgSignerInfo;
			this.m_cmsgSignerInfo = (CAPI.CMSG_SIGNER_INFO)Marshal.PtrToStructure(pbCmsgSignerInfo.DangerousGetHandle(), typeof(CAPI.CMSG_SIGNER_INFO));
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001A874 File Offset: 0x00018A74
		[SecuritySafeCritical]
		internal unsafe SignerInfo(SignedCms signedCms, SignerInfo parentSignerInfo, byte[] encodedSignerInfo)
		{
			uint num = 0U;
			SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
			fixed (byte* ptr = &encodedSignerInfo[0])
			{
				byte* value = ptr;
				if (!CAPI.DecodeObject(new IntPtr(500L), new IntPtr((void*)value), (uint)encodedSignerInfo.Length, out invalidHandle, out num))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			this.m_signedCms = signedCms;
			this.m_parentSignerInfo = parentSignerInfo;
			this.m_encodedSignerInfo = (byte[])encodedSignerInfo.Clone();
			this.m_pbCmsgSignerInfo = invalidHandle;
			this.m_cmsgSignerInfo = (CAPI.CMSG_SIGNER_INFO)Marshal.PtrToStructure(invalidHandle.DangerousGetHandle(), typeof(CAPI.CMSG_SIGNER_INFO));
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001A90D File Offset: 0x00018B0D
		public int Version
		{
			get
			{
				return (int)this.m_cmsgSignerInfo.dwVersion;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0001A91A File Offset: 0x00018B1A
		public X509Certificate2 Certificate
		{
			get
			{
				if (this.m_certificate == null)
				{
					this.m_certificate = PkcsUtils.FindCertificate(this.SignerIdentifier, this.m_signedCms.Certificates);
				}
				return this.m_certificate;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0001A946 File Offset: 0x00018B46
		public SubjectIdentifier SignerIdentifier
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_signerIdentifier == null)
				{
					this.m_signerIdentifier = new SubjectIdentifier(this.m_cmsgSignerInfo);
				}
				return this.m_signerIdentifier;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0001A967 File Offset: 0x00018B67
		public Oid DigestAlgorithm
		{
			get
			{
				return new Oid(this.m_cmsgSignerInfo.HashAlgorithm.pszObjId);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0001A97E File Offset: 0x00018B7E
		public CryptographicAttributeObjectCollection SignedAttributes
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_signedAttributes == null)
				{
					this.m_signedAttributes = new CryptographicAttributeObjectCollection(this.m_cmsgSignerInfo.AuthAttrs);
				}
				return this.m_signedAttributes;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001A9A4 File Offset: 0x00018BA4
		public CryptographicAttributeObjectCollection UnsignedAttributes
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_unsignedAttributes == null)
				{
					this.m_unsignedAttributes = new CryptographicAttributeObjectCollection(this.m_cmsgSignerInfo.UnauthAttrs);
				}
				return this.m_unsignedAttributes;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0001A9CA File Offset: 0x00018BCA
		public SignerInfoCollection CounterSignerInfos
		{
			get
			{
				if (this.m_parentSignerInfo != null)
				{
					return new SignerInfoCollection();
				}
				return new SignerInfoCollection(this.m_signedCms, this);
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001A9E6 File Offset: 0x00018BE6
		public void ComputeCounterSignature()
		{
			this.ComputeCounterSignature(new CmsSigner((this.m_signedCms.Version == 2) ? SubjectIdentifierType.SubjectKeyIdentifier : SubjectIdentifierType.IssuerAndSerialNumber));
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001AA08 File Offset: 0x00018C08
		public void ComputeCounterSignature(CmsSigner signer)
		{
			if (this.m_parentSignerInfo != null)
			{
				throw new CryptographicException(-2147483647);
			}
			if (signer == null)
			{
				throw new ArgumentNullException("signer");
			}
			if (signer.Certificate == null)
			{
				signer.Certificate = PkcsUtils.SelectSignerCertificate();
			}
			if (!signer.Certificate.HasPrivateKey)
			{
				throw new CryptographicException(-2146893811);
			}
			this.CounterSign(signer);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001AA68 File Offset: 0x00018C68
		[SecuritySafeCritical]
		public void RemoveCounterSignature(int index)
		{
			if (this.m_parentSignerInfo != null)
			{
				throw new CryptographicException(-2147483647);
			}
			this.RemoveCounterSignature(PkcsUtils.GetSignerIndex(this.m_signedCms.GetCryptMsgHandle(), this, 0), index);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001AA98 File Offset: 0x00018C98
		[SecuritySafeCritical]
		public void RemoveCounterSignature(SignerInfo counterSignerInfo)
		{
			if (this.m_parentSignerInfo != null)
			{
				throw new CryptographicException(-2147483647);
			}
			if (counterSignerInfo == null)
			{
				throw new ArgumentNullException("counterSignerInfo");
			}
			foreach (CryptographicAttributeObject cryptographicAttributeObject in this.UnsignedAttributes)
			{
				if (string.Compare(cryptographicAttributeObject.Oid.Value, "1.2.840.113549.1.9.6", StringComparison.OrdinalIgnoreCase) == 0)
				{
					for (int i = 0; i < cryptographicAttributeObject.Values.Count; i++)
					{
						AsnEncodedData asnEncodedData = cryptographicAttributeObject.Values[i];
						SignerInfo signerInfo = new SignerInfo(this.m_signedCms, this.m_parentSignerInfo, asnEncodedData.RawData);
						if (counterSignerInfo.SignerIdentifier.Type == SubjectIdentifierType.IssuerAndSerialNumber && signerInfo.SignerIdentifier.Type == SubjectIdentifierType.IssuerAndSerialNumber)
						{
							X509IssuerSerial x509IssuerSerial = (X509IssuerSerial)counterSignerInfo.SignerIdentifier.Value;
							X509IssuerSerial x509IssuerSerial2 = (X509IssuerSerial)signerInfo.SignerIdentifier.Value;
							if (string.Compare(x509IssuerSerial.IssuerName, x509IssuerSerial2.IssuerName, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(x509IssuerSerial.SerialNumber, x509IssuerSerial2.SerialNumber, StringComparison.OrdinalIgnoreCase) == 0)
							{
								this.RemoveCounterSignature(PkcsUtils.GetSignerIndex(this.m_signedCms.GetCryptMsgHandle(), this, 0), i);
								return;
							}
						}
						else if (counterSignerInfo.SignerIdentifier.Type == SubjectIdentifierType.SubjectKeyIdentifier && signerInfo.SignerIdentifier.Type == SubjectIdentifierType.SubjectKeyIdentifier)
						{
							string strA = counterSignerInfo.SignerIdentifier.Value as string;
							string strB = signerInfo.SignerIdentifier.Value as string;
							if (string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) == 0)
							{
								this.RemoveCounterSignature(PkcsUtils.GetSignerIndex(this.m_signedCms.GetCryptMsgHandle(), this, 0), i);
								return;
							}
						}
					}
				}
			}
			throw new CryptographicException(-2146889714);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001AC49 File Offset: 0x00018E49
		public void CheckSignature(bool verifySignatureOnly)
		{
			this.CheckSignature(new X509Certificate2Collection(), verifySignatureOnly);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001AC58 File Offset: 0x00018E58
		public void CheckSignature(X509Certificate2Collection extraStore, bool verifySignatureOnly)
		{
			if (extraStore == null)
			{
				throw new ArgumentNullException("extraStore");
			}
			X509Certificate2 x509Certificate = this.Certificate;
			if (x509Certificate == null)
			{
				x509Certificate = PkcsUtils.FindCertificate(this.SignerIdentifier, extraStore);
				if (x509Certificate == null)
				{
					throw new CryptographicException(-2146889714);
				}
			}
			this.Verify(extraStore, x509Certificate, verifySignatureOnly);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001ACA4 File Offset: 0x00018EA4
		[SecuritySafeCritical]
		public unsafe void CheckHash()
		{
			int size = Marshal.SizeOf(typeof(CAPI.CMSG_CTRL_VERIFY_SIGNATURE_EX_PARA));
			CAPI.CMSG_CTRL_VERIFY_SIGNATURE_EX_PARA cmsg_CTRL_VERIFY_SIGNATURE_EX_PARA = new CAPI.CMSG_CTRL_VERIFY_SIGNATURE_EX_PARA(size);
			cmsg_CTRL_VERIFY_SIGNATURE_EX_PARA.dwSignerType = 4U;
			cmsg_CTRL_VERIFY_SIGNATURE_EX_PARA.dwSignerIndex = (uint)PkcsUtils.GetSignerIndex(this.m_signedCms.GetCryptMsgHandle(), this, 0);
			if (!CAPI.CryptMsgControl(this.m_signedCms.GetCryptMsgHandle(), 0U, 19U, new IntPtr((void*)(&cmsg_CTRL_VERIFY_SIGNATURE_EX_PARA))))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001AD14 File Offset: 0x00018F14
		[SecuritySafeCritical]
		public byte[] GetSignature()
		{
			byte[] array = new byte[this.m_cmsgSignerInfo.EncryptedHash.cbData];
			Marshal.Copy(this.m_cmsgSignerInfo.EncryptedHash.pbData, array, 0, array.Length);
			return array;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001AD52 File Offset: 0x00018F52
		public Oid SignatureAlgorithm
		{
			get
			{
				return new Oid(this.m_cmsgSignerInfo.HashEncryptionAlgorithm.pszObjId);
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001AD69 File Offset: 0x00018F69
		internal CAPI.CMSG_SIGNER_INFO GetCmsgSignerInfo()
		{
			return this.m_cmsgSignerInfo;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001AD74 File Offset: 0x00018F74
		[SecuritySafeCritical]
		private void CounterSign(CmsSigner signer)
		{
			CspParameters parameters = new CspParameters();
			if (!X509Utils.GetPrivateKeyInfo(X509Utils.GetCertContext(signer.Certificate), ref parameters))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
			KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.Open | KeyContainerPermissionFlags.Sign);
			keyContainerPermission.AccessEntries.Add(accessEntry);
			keyContainerPermission.Demand();
			uint signerIndex = (uint)PkcsUtils.GetSignerIndex(this.m_signedCms.GetCryptMsgHandle(), this, 0);
			SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CMSG_SIGNER_ENCODE_INFO))));
			SafeCryptProvHandle safeCryptProvHandle;
			CAPI.CMSG_SIGNER_ENCODE_INFO cmsg_SIGNER_ENCODE_INFO = PkcsUtils.CreateSignerEncodeInfo(signer, out safeCryptProvHandle);
			try
			{
				Marshal.StructureToPtr(cmsg_SIGNER_ENCODE_INFO, safeLocalAllocHandle.DangerousGetHandle(), false);
				if (!CAPI.CryptMsgCountersign(this.m_signedCms.GetCryptMsgHandle(), signerIndex, 1U, safeLocalAllocHandle.DangerousGetHandle()))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				this.m_signedCms.ReopenToDecode();
			}
			finally
			{
				Marshal.DestroyStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPI.CMSG_SIGNER_ENCODE_INFO));
				safeLocalAllocHandle.Dispose();
				cmsg_SIGNER_ENCODE_INFO.Dispose();
				safeCryptProvHandle.Dispose();
			}
			PkcsUtils.AddCertsToMessage(this.m_signedCms.GetCryptMsgHandle(), this.m_signedCms.Certificates, PkcsUtils.CreateBagOfCertificates(signer));
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001AEAC File Offset: 0x000190AC
		[SecuritySafeCritical]
		private unsafe void Verify(X509Certificate2Collection extraStore, X509Certificate2 certificate, bool verifySignatureOnly)
		{
			SafeLocalAllocHandle safeLocalAllocHandle = SafeLocalAllocHandle.InvalidHandle;
			CAPI.CERT_CONTEXT cert_CONTEXT = (CAPI.CERT_CONTEXT)Marshal.PtrToStructure(X509Utils.GetCertContext(certificate).DangerousGetHandle(), typeof(CAPI.CERT_CONTEXT));
			checked
			{
				IntPtr value = new IntPtr((long)cert_CONTEXT.pCertInfo + (long)Marshal.OffsetOf(typeof(CAPI.CERT_INFO), "SubjectPublicKeyInfo"));
				IntPtr intPtr = new IntPtr((long)value + (long)Marshal.OffsetOf(typeof(CAPI.CERT_PUBLIC_KEY_INFO), "Algorithm"));
				IntPtr value2 = new IntPtr((long)intPtr + (long)Marshal.OffsetOf(typeof(CAPI.CRYPT_ALGORITHM_IDENTIFIER), "Parameters"));
				IntPtr pvKey = Marshal.ReadIntPtr(intPtr);
				CAPI.CRYPT_OID_INFO crypt_OID_INFO = CAPI.CryptFindOIDInfo(1U, pvKey, 3U);
				if (crypt_OID_INFO.Algid == 8704U)
				{
					bool flag = false;
					IntPtr ptr = new IntPtr((long)value2 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "cbData"));
					IntPtr ptr2 = new IntPtr((long)value2 + (long)Marshal.OffsetOf(typeof(CAPI.CRYPTOAPI_BLOB), "pbData"));
					if (Marshal.ReadInt32(ptr) == 0)
					{
						flag = true;
					}
					else if (Marshal.ReadIntPtr(ptr2) == IntPtr.Zero)
					{
						flag = true;
					}
					else
					{
						IntPtr ptr3 = Marshal.ReadIntPtr(ptr2);
						if ((uint)Marshal.ReadInt32(ptr3) == 5U)
						{
							flag = true;
						}
					}
					if (flag)
					{
						SafeCertChainHandle invalidHandle = SafeCertChainHandle.InvalidHandle;
						X509Utils.BuildChain(new IntPtr(0L), X509Utils.GetCertContext(certificate), null, null, null, X509RevocationMode.NoCheck, X509RevocationFlag.ExcludeRoot, DateTime.Now, new TimeSpan(0, 0, 0), ref invalidHandle);
						invalidHandle.Dispose();
						uint num = 0U;
						if (!CAPI.CAPISafe.CertGetCertificateContextProperty(X509Utils.GetCertContext(certificate), 22U, safeLocalAllocHandle, ref num))
						{
							throw new CryptographicException(Marshal.GetLastWin32Error());
						}
						if (num > 0U)
						{
							safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr((long)(unchecked((ulong)num))));
							if (!CAPI.CAPISafe.CertGetCertificateContextProperty(X509Utils.GetCertContext(certificate), 22U, safeLocalAllocHandle, ref num))
							{
								throw new CryptographicException(Marshal.GetLastWin32Error());
							}
							Marshal.WriteInt32(ptr, (int)num);
							Marshal.WriteIntPtr(ptr2, safeLocalAllocHandle.DangerousGetHandle());
						}
					}
				}
				if (this.m_parentSignerInfo == null)
				{
					if (!CAPI.CryptMsgControl(this.m_signedCms.GetCryptMsgHandle(), 0U, 1U, cert_CONTEXT.pCertInfo))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
				}
				else
				{
					int num2 = -1;
					int num3 = 0;
					SafeLocalAllocHandle invalidHandle2;
					for (;;)
					{
						try
						{
							num2 = PkcsUtils.GetSignerIndex(this.m_signedCms.GetCryptMsgHandle(), this.m_parentSignerInfo, num2 + 1);
						}
						catch (CryptographicException)
						{
							if (num3 == 0)
							{
								throw;
							}
							throw new CryptographicException(num3);
						}
						uint num4 = 0U;
						invalidHandle2 = SafeLocalAllocHandle.InvalidHandle;
						PkcsUtils.GetParam(this.m_signedCms.GetCryptMsgHandle(), 28U, (uint)num2, out invalidHandle2, out num4);
						if (num4 != 0U)
						{
							try
							{
								byte[] array;
								byte* value3;
								if ((array = this.m_encodedSignerInfo) == null || array.Length == 0)
								{
									value3 = null;
								}
								else
								{
									value3 = &array[0];
								}
								if (!CAPI.CAPISafe.CryptMsgVerifyCountersignatureEncoded(IntPtr.Zero, 65537U, invalidHandle2.DangerousGetHandle(), num4, new IntPtr((void*)value3), (uint)this.m_encodedSignerInfo.Length, cert_CONTEXT.pCertInfo))
								{
									num3 = Marshal.GetLastWin32Error();
									continue;
								}
							}
							finally
							{
								byte[] array = null;
							}
							break;
						}
						num3 = -2146885618;
					}
					invalidHandle2.Dispose();
				}
				if (!verifySignatureOnly)
				{
					int num5 = SignerInfo.VerifyCertificate(certificate, extraStore);
					if (num5 != 0)
					{
						throw new CryptographicException(num5);
					}
				}
				safeLocalAllocHandle.Dispose();
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001B1EC File Offset: 0x000193EC
		[SecuritySafeCritical]
		private unsafe void RemoveCounterSignature(int parentIndex, int childIndex)
		{
			if (parentIndex < 0)
			{
				throw new ArgumentOutOfRangeException("parentIndex");
			}
			if (childIndex < 0)
			{
				throw new ArgumentOutOfRangeException("childIndex");
			}
			uint num = 0U;
			SafeLocalAllocHandle invalidHandle = SafeLocalAllocHandle.InvalidHandle;
			uint num2 = 0U;
			SafeLocalAllocHandle invalidHandle2 = SafeLocalAllocHandle.InvalidHandle;
			IntPtr zero = IntPtr.Zero;
			SafeCryptMsgHandle cryptMsgHandle = this.m_signedCms.GetCryptMsgHandle();
			uint cAttr;
			if (PkcsUtils.CmsSupported())
			{
				PkcsUtils.GetParam(cryptMsgHandle, 39U, (uint)parentIndex, out invalidHandle, out num);
				CAPI.CMSG_CMS_SIGNER_INFO cmsg_CMS_SIGNER_INFO = (CAPI.CMSG_CMS_SIGNER_INFO)Marshal.PtrToStructure(invalidHandle.DangerousGetHandle(), typeof(CAPI.CMSG_CMS_SIGNER_INFO));
				cAttr = cmsg_CMS_SIGNER_INFO.UnauthAttrs.cAttr;
				zero = new IntPtr((long)cmsg_CMS_SIGNER_INFO.UnauthAttrs.rgAttr);
			}
			else
			{
				PkcsUtils.GetParam(cryptMsgHandle, 6U, (uint)parentIndex, out invalidHandle2, out num2);
				CAPI.CMSG_SIGNER_INFO cmsg_SIGNER_INFO = (CAPI.CMSG_SIGNER_INFO)Marshal.PtrToStructure(invalidHandle2.DangerousGetHandle(), typeof(CAPI.CMSG_SIGNER_INFO));
				cAttr = cmsg_SIGNER_INFO.UnauthAttrs.cAttr;
				zero = new IntPtr((long)cmsg_SIGNER_INFO.UnauthAttrs.rgAttr);
			}
			for (uint num3 = 0U; num3 < cAttr; num3 += 1U)
			{
				CAPI.CRYPT_ATTRIBUTE crypt_ATTRIBUTE = (CAPI.CRYPT_ATTRIBUTE)Marshal.PtrToStructure(zero, typeof(CAPI.CRYPT_ATTRIBUTE));
				checked
				{
					if (string.Compare(crypt_ATTRIBUTE.pszObjId, "1.2.840.113549.1.9.6", StringComparison.OrdinalIgnoreCase) == 0 && crypt_ATTRIBUTE.cValue > 0U)
					{
						if (childIndex < (int)crypt_ATTRIBUTE.cValue)
						{
							CAPI.CMSG_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA cmsg_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA = new CAPI.CMSG_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA(Marshal.SizeOf(typeof(CAPI.CMSG_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA)));
							cmsg_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA.dwSignerIndex = (uint)parentIndex;
							cmsg_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA.dwUnauthAttrIndex = num3;
							if (!CAPI.CryptMsgControl(cryptMsgHandle, 0U, 9U, new IntPtr(unchecked((void*)(&cmsg_CTRL_DEL_SIGNER_UNAUTH_ATTR_PARA)))))
							{
								throw new CryptographicException(Marshal.GetLastWin32Error());
							}
							if (crypt_ATTRIBUTE.cValue > 1U)
							{
								try
								{
									uint num4 = (uint)(unchecked((ulong)(checked(crypt_ATTRIBUTE.cValue - 1U))) * (ulong)(unchecked((long)Marshal.SizeOf(typeof(CAPI.CRYPTOAPI_BLOB)))));
									SafeLocalAllocHandle safeLocalAllocHandle = CAPI.LocalAlloc(64U, new IntPtr((long)(unchecked((ulong)num4))));
									CAPI.CRYPTOAPI_BLOB* ptr = (CAPI.CRYPTOAPI_BLOB*)((void*)crypt_ATTRIBUTE.rgValue);
									CAPI.CRYPTOAPI_BLOB* ptr2 = (CAPI.CRYPTOAPI_BLOB*)((void*)safeLocalAllocHandle.DangerousGetHandle());
									int i = 0;
									while (i < (int)crypt_ATTRIBUTE.cValue)
									{
										if (i != childIndex)
										{
											*ptr2 = *ptr;
										}
										i++;
										ptr++;
										ptr2++;
									}
									CAPI.CRYPT_ATTRIBUTE crypt_ATTRIBUTE2 = default(CAPI.CRYPT_ATTRIBUTE);
									crypt_ATTRIBUTE2.pszObjId = crypt_ATTRIBUTE.pszObjId;
									crypt_ATTRIBUTE2.cValue = crypt_ATTRIBUTE.cValue - 1U;
									crypt_ATTRIBUTE2.rgValue = safeLocalAllocHandle.DangerousGetHandle();
									SafeLocalAllocHandle safeLocalAllocHandle2 = CAPI.LocalAlloc(64U, new IntPtr(Marshal.SizeOf(typeof(CAPI.CRYPT_ATTRIBUTE))));
									Marshal.StructureToPtr(crypt_ATTRIBUTE2, safeLocalAllocHandle2.DangerousGetHandle(), false);
									byte[] array;
									try
									{
										if (!CAPI.EncodeObject(new IntPtr(22L), safeLocalAllocHandle2.DangerousGetHandle(), out array))
										{
											throw new CryptographicException(Marshal.GetLastWin32Error());
										}
									}
									finally
									{
										Marshal.DestroyStructure(safeLocalAllocHandle2.DangerousGetHandle(), typeof(CAPI.CRYPT_ATTRIBUTE));
										safeLocalAllocHandle2.Dispose();
									}
									try
									{
										fixed (byte* ptr3 = &array[0])
										{
											byte* value = ptr3;
											CAPI.CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA cmsg_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA = new CAPI.CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA(Marshal.SizeOf(typeof(CAPI.CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA)));
											cmsg_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA.dwSignerIndex = (uint)parentIndex;
											cmsg_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA.blob.cbData = (uint)array.Length;
											cmsg_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA.blob.pbData = new IntPtr((void*)value);
											if (!CAPI.CryptMsgControl(cryptMsgHandle, 0U, 8U, new IntPtr(unchecked((void*)(&cmsg_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA)))))
											{
												throw new CryptographicException(Marshal.GetLastWin32Error());
											}
										}
									}
									finally
									{
										byte* ptr3 = null;
									}
									safeLocalAllocHandle.Dispose();
								}
								catch (CryptographicException)
								{
									byte[] array2;
									if (CAPI.EncodeObject(new IntPtr(22L), zero, out array2))
									{
										fixed (byte* ptr4 = &array2[0])
										{
											byte* value2 = ptr4;
											CAPI.CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA cmsg_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA2 = new CAPI.CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA(Marshal.SizeOf(typeof(CAPI.CMSG_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA)));
											cmsg_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA2.dwSignerIndex = (uint)parentIndex;
											cmsg_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA2.blob.cbData = (uint)array2.Length;
											cmsg_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA2.blob.pbData = new IntPtr((void*)value2);
											CAPI.CryptMsgControl(cryptMsgHandle, 0U, 8U, new IntPtr(unchecked((void*)(&cmsg_CTRL_ADD_SIGNER_UNAUTH_ATTR_PARA2))));
										}
									}
									throw;
								}
							}
							return;
						}
						else
						{
							childIndex -= (int)crypt_ATTRIBUTE.cValue;
						}
					}
					zero = new IntPtr((long)zero + unchecked((long)Marshal.SizeOf(typeof(CAPI.CRYPT_ATTRIBUTE))));
				}
			}
			if (invalidHandle != null && !invalidHandle.IsInvalid)
			{
				invalidHandle.Dispose();
			}
			if (invalidHandle2 != null && !invalidHandle2.IsInvalid)
			{
				invalidHandle2.Dispose();
			}
			throw new CryptographicException(-2146885618);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001B674 File Offset: 0x00019874
		[SecuritySafeCritical]
		private unsafe static int VerifyCertificate(X509Certificate2 certificate, X509Certificate2Collection extraStore)
		{
			int result;
			int num = X509Utils.VerifyCertificate(X509Utils.GetCertContext(certificate), null, null, X509RevocationMode.Online, X509RevocationFlag.ExcludeRoot, DateTime.Now, new TimeSpan(0, 0, 0), extraStore, new IntPtr(1L), new IntPtr((void*)(&result)));
			if (num != 0)
			{
				return result;
			}
			foreach (X509Extension x509Extension in certificate.Extensions)
			{
				if (string.Compare(x509Extension.Oid.Value, "2.5.29.15", StringComparison.OrdinalIgnoreCase) == 0)
				{
					X509KeyUsageExtension x509KeyUsageExtension = new X509KeyUsageExtension();
					x509KeyUsageExtension.CopyFrom(x509Extension);
					if ((x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.DigitalSignature) == X509KeyUsageFlags.None && (x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.NonRepudiation) == X509KeyUsageFlags.None)
					{
						num = -2146762480;
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x04000517 RID: 1303
		private X509Certificate2 m_certificate;

		// Token: 0x04000518 RID: 1304
		private SubjectIdentifier m_signerIdentifier;

		// Token: 0x04000519 RID: 1305
		private CryptographicAttributeObjectCollection m_signedAttributes;

		// Token: 0x0400051A RID: 1306
		private CryptographicAttributeObjectCollection m_unsignedAttributes;

		// Token: 0x0400051B RID: 1307
		private SignedCms m_signedCms;

		// Token: 0x0400051C RID: 1308
		private SignerInfo m_parentSignerInfo;

		// Token: 0x0400051D RID: 1309
		private byte[] m_encodedSignerInfo;

		// Token: 0x0400051E RID: 1310
		[SecurityCritical]
		private SafeLocalAllocHandle m_pbCmsgSignerInfo;

		// Token: 0x0400051F RID: 1311
		private CAPI.CMSG_SIGNER_INFO m_cmsgSignerInfo;
	}
}
