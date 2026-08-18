using System;
using System.Diagnostics;
using System.IdentityModel.Selectors;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000168 RID: 360
	public class SecurityKeyElement : SecurityKey
	{
		// Token: 0x06000B55 RID: 2901 RVA: 0x00036398 File Offset: 0x00034598
		public SecurityKeyElement(SecurityKeyIdentifierClause securityKeyIdentifierClause, SecurityTokenResolver securityTokenResolver)
		{
			if (securityKeyIdentifierClause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityKeyIdentifierClause");
			}
			this.Initialize(new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
			{
				securityKeyIdentifierClause
			}), securityTokenResolver);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x000363C9 File Offset: 0x000345C9
		public SecurityKeyElement(SecurityKeyIdentifier securityKeyIdentifier, SecurityTokenResolver securityTokenResolver)
		{
			if (securityKeyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("securityKeyIdentifier");
			}
			this.Initialize(securityKeyIdentifier, securityTokenResolver);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x000363EC File Offset: 0x000345EC
		private void Initialize(SecurityKeyIdentifier securityKeyIdentifier, SecurityTokenResolver securityTokenResolver)
		{
			this._keyLock = new object();
			this._securityKeyIdentifier = securityKeyIdentifier;
			this._securityTokenResolver = securityTokenResolver;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00036407 File Offset: 0x00034607
		public override byte[] DecryptKey(string algorithm, byte[] keyData)
		{
			if (this._securityKey == null)
			{
				this.ResolveKey();
			}
			return this._securityKey.DecryptKey(algorithm, keyData);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00036424 File Offset: 0x00034624
		public override byte[] EncryptKey(string algorithm, byte[] keyData)
		{
			if (this._securityKey == null)
			{
				this.ResolveKey();
			}
			return this._securityKey.EncryptKey(algorithm, keyData);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00036444 File Offset: 0x00034644
		public override bool IsAsymmetricAlgorithm(string algorithm)
		{
			return algorithm == "http://www.w3.org/2000/09/xmldsig#dsa-sha1" || algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1" || algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256" || algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p" || algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-1_5";
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00036495 File Offset: 0x00034695
		public override bool IsSupportedAlgorithm(string algorithm)
		{
			if (this._securityKey == null)
			{
				this.ResolveKey();
			}
			return this._securityKey.IsSupportedAlgorithm(algorithm);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x000364B4 File Offset: 0x000346B4
		public override bool IsSymmetricAlgorithm(string algorithm)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(algorithm);
			if (num <= 877368883U)
			{
				if (num <= 636766351U)
				{
					if (num <= 550229268U)
					{
						if (num != 376408642U)
						{
							if (num != 550229268U)
							{
								return false;
							}
							if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"))
							{
								return false;
							}
							return true;
						}
						else if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p"))
						{
							return false;
						}
					}
					else if (num != 600251407U)
					{
						if (num != 636766351U)
						{
							return false;
						}
						if (!(algorithm == "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1"))
						{
							return false;
						}
						return true;
					}
					else
					{
						if (!(algorithm == "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1"))
						{
							return false;
						}
						return true;
					}
				}
				else if (num <= 712490267U)
				{
					if (num != 699966473U)
					{
						if (num != 712490267U)
						{
							return false;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes256-cbc"))
						{
							return false;
						}
						return true;
					}
					else if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#dsa-sha1"))
					{
						return false;
					}
				}
				else if (num != 811041755U)
				{
					if (num != 877368883U)
					{
						return false;
					}
					if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1"))
					{
						return false;
					}
				}
				else
				{
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes128-cbc"))
					{
						return false;
					}
					return true;
				}
			}
			else if (num <= 2551777632U)
			{
				if (num <= 1735592375U)
				{
					if (num != 1318943838U)
					{
						if (num != 1735592375U)
						{
							return false;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc"))
						{
							return false;
						}
						return true;
					}
					else
					{
						if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#hmac-sha1"))
						{
							return false;
						}
						return true;
					}
				}
				else if (num != 2323908233U)
				{
					if (num != 2551777632U)
					{
						return false;
					}
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes192"))
					{
						return false;
					}
					return true;
				}
				else
				{
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes256"))
					{
						return false;
					}
					return true;
				}
			}
			else if (num <= 3225656034U)
			{
				if (num != 2888462845U)
				{
					if (num != 3225656034U)
					{
						return false;
					}
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes192-cbc"))
					{
						return false;
					}
					return true;
				}
				else
				{
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes128"))
					{
						return false;
					}
					return true;
				}
			}
			else if (num != 3487232831U)
			{
				if (num != 3654423024U)
				{
					if (num != 3880483293U)
					{
						return false;
					}
					if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"))
					{
						return false;
					}
				}
				else if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-1_5"))
				{
					return false;
				}
			}
			else
			{
				if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-tripledes"))
				{
					return false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x00036714 File Offset: 0x00034914
		public override int KeySize
		{
			get
			{
				if (this._securityKey == null)
				{
					this.ResolveKey();
				}
				return this._securityKey.KeySize;
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00036730 File Offset: 0x00034930
		private void ResolveKey()
		{
			if (this._securityKeyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ski");
			}
			if (this._securityKey == null)
			{
				object keyLock = this._keyLock;
				lock (keyLock)
				{
					if (this._securityKey == null)
					{
						if (this._securityTokenResolver != null)
						{
							for (int i = 0; i < this._securityKeyIdentifier.Count; i++)
							{
								if (this._securityTokenResolver.TryResolveSecurityKey(this._securityKeyIdentifier[i], out this._securityKey))
								{
									return;
								}
							}
						}
						if (!this._securityKeyIdentifier.CanCreateKey)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new SecurityTokenException(SR.GetString("ID2080", new object[]
							{
								(this._securityTokenResolver == null) ? "null" : this._securityTokenResolver.ToString(),
								(this._securityKeyIdentifier == null) ? "null" : this._securityKeyIdentifier.ToString()
							})), TraceEventType.Error);
						}
						this._securityKey = this._securityKeyIdentifier.CreateKey();
					}
				}
			}
		}

		// Token: 0x04000C1A RID: 3098
		private SecurityKey _securityKey;

		// Token: 0x04000C1B RID: 3099
		private object _keyLock;

		// Token: 0x04000C1C RID: 3100
		private SecurityTokenResolver _securityTokenResolver;

		// Token: 0x04000C1D RID: 3101
		private SecurityKeyIdentifier _securityKeyIdentifier;
	}
}
