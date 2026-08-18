using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x02000662 RID: 1634
	[ComVisible(true)]
	[Serializable]
	public sealed class PublisherIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06003AFE RID: 15102 RVA: 0x000C7670 File Offset: 0x000C6670
		public PublisherIdentityPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				if (CodeAccessSecurityEngine.DoesFullTrustMeanFullTrust())
				{
					this.m_unrestricted = true;
					return;
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_UnrestrictedIdentityPermission"));
			}
			else
			{
				if (state == PermissionState.None)
				{
					this.m_unrestricted = false;
					return;
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
			}
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x000C76C0 File Offset: 0x000C66C0
		public PublisherIdentityPermission(X509Certificate certificate)
		{
			this.Certificate = certificate;
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06003B01 RID: 15105 RVA: 0x000C76F8 File Offset: 0x000C66F8
		// (set) Token: 0x06003B00 RID: 15104 RVA: 0x000C76CF File Offset: 0x000C66CF
		public X509Certificate Certificate
		{
			get
			{
				if (this.m_certs == null || this.m_certs.Length < 1)
				{
					return null;
				}
				if (this.m_certs.Length > 1)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
				}
				if (this.m_certs[0] == null)
				{
					return null;
				}
				return new X509Certificate(this.m_certs[0]);
			}
			set
			{
				PublisherIdentityPermission.CheckCertificate(value);
				this.m_unrestricted = false;
				this.m_certs = new X509Certificate[1];
				this.m_certs[0] = new X509Certificate(value);
			}
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x000C774E File Offset: 0x000C674E
		private static void CheckCertificate(X509Certificate certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (certificate.GetRawCertData() == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_UninitializedCertificate"));
			}
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x000C7778 File Offset: 0x000C6778
		public override IPermission Copy()
		{
			PublisherIdentityPermission publisherIdentityPermission = new PublisherIdentityPermission(PermissionState.None);
			publisherIdentityPermission.m_unrestricted = this.m_unrestricted;
			if (this.m_certs != null)
			{
				publisherIdentityPermission.m_certs = new X509Certificate[this.m_certs.Length];
				for (int i = 0; i < this.m_certs.Length; i++)
				{
					publisherIdentityPermission.m_certs[i] = ((this.m_certs[i] == null) ? null : new X509Certificate(this.m_certs[i]));
				}
			}
			return publisherIdentityPermission;
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x000C77EC File Offset: 0x000C67EC
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_unrestricted && (this.m_certs == null || this.m_certs.Length == 0);
			}
			PublisherIdentityPermission publisherIdentityPermission = target as PublisherIdentityPermission;
			if (publisherIdentityPermission == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			if (publisherIdentityPermission.m_unrestricted)
			{
				return true;
			}
			if (this.m_unrestricted)
			{
				return false;
			}
			if (this.m_certs != null)
			{
				foreach (X509Certificate x509Certificate in this.m_certs)
				{
					bool flag = false;
					if (publisherIdentityPermission.m_certs != null)
					{
						foreach (X509Certificate other in publisherIdentityPermission.m_certs)
						{
							if (x509Certificate.Equals(other))
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x000C78DC File Offset: 0x000C68DC
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			PublisherIdentityPermission publisherIdentityPermission = target as PublisherIdentityPermission;
			if (publisherIdentityPermission == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			if (this.m_unrestricted && publisherIdentityPermission.m_unrestricted)
			{
				return new PublisherIdentityPermission(PermissionState.None)
				{
					m_unrestricted = true
				};
			}
			if (this.m_unrestricted)
			{
				return publisherIdentityPermission.Copy();
			}
			if (publisherIdentityPermission.m_unrestricted)
			{
				return this.Copy();
			}
			if (this.m_certs == null || publisherIdentityPermission.m_certs == null || this.m_certs.Length == 0 || publisherIdentityPermission.m_certs.Length == 0)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList();
			foreach (X509Certificate x509Certificate in this.m_certs)
			{
				foreach (X509Certificate other in publisherIdentityPermission.m_certs)
				{
					if (x509Certificate.Equals(other))
					{
						arrayList.Add(new X509Certificate(x509Certificate));
					}
				}
			}
			if (arrayList.Count == 0)
			{
				return null;
			}
			return new PublisherIdentityPermission(PermissionState.None)
			{
				m_certs = (X509Certificate[])arrayList.ToArray(typeof(X509Certificate))
			};
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x000C7A20 File Offset: 0x000C6A20
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				if ((this.m_certs == null || this.m_certs.Length == 0) && !this.m_unrestricted)
				{
					return null;
				}
				return this.Copy();
			}
			else
			{
				PublisherIdentityPermission publisherIdentityPermission = target as PublisherIdentityPermission;
				if (publisherIdentityPermission == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
					{
						base.GetType().FullName
					}));
				}
				if (this.m_unrestricted || publisherIdentityPermission.m_unrestricted)
				{
					return new PublisherIdentityPermission(PermissionState.None)
					{
						m_unrestricted = true
					};
				}
				if (this.m_certs == null || this.m_certs.Length == 0)
				{
					if (publisherIdentityPermission.m_certs == null || publisherIdentityPermission.m_certs.Length == 0)
					{
						return null;
					}
					return publisherIdentityPermission.Copy();
				}
				else
				{
					if (publisherIdentityPermission.m_certs == null || publisherIdentityPermission.m_certs.Length == 0)
					{
						return this.Copy();
					}
					ArrayList arrayList = new ArrayList();
					foreach (X509Certificate value in this.m_certs)
					{
						arrayList.Add(value);
					}
					foreach (X509Certificate x509Certificate in publisherIdentityPermission.m_certs)
					{
						bool flag = false;
						foreach (object obj in arrayList)
						{
							X509Certificate other = (X509Certificate)obj;
							if (x509Certificate.Equals(other))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							arrayList.Add(x509Certificate);
						}
					}
					return new PublisherIdentityPermission(PermissionState.None)
					{
						m_certs = (X509Certificate[])arrayList.ToArray(typeof(X509Certificate))
					};
				}
			}
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x000C7BD8 File Offset: 0x000C6BD8
		public override void FromXml(SecurityElement esd)
		{
			this.m_unrestricted = false;
			this.m_certs = null;
			CodeAccessPermission.ValidateElement(esd, this);
			string text = esd.Attribute("Unrestricted");
			if (text != null && string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_unrestricted = true;
				return;
			}
			string text2 = esd.Attribute("X509v3Certificate");
			ArrayList arrayList = new ArrayList();
			if (text2 != null)
			{
				arrayList.Add(new X509Certificate(Hex.DecodeHexString(text2)));
			}
			ArrayList children = esd.Children;
			if (children != null)
			{
				foreach (object obj in children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					text2 = securityElement.Attribute("X509v3Certificate");
					if (text2 != null)
					{
						arrayList.Add(new X509Certificate(Hex.DecodeHexString(text2)));
					}
				}
			}
			if (arrayList.Count != 0)
			{
				this.m_certs = (X509Certificate[])arrayList.ToArray(typeof(X509Certificate));
			}
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x000C7CE0 File Offset: 0x000C6CE0
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.PublisherIdentityPermission");
			if (this.m_unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else if (this.m_certs != null)
			{
				if (this.m_certs.Length == 1)
				{
					securityElement.AddAttribute("X509v3Certificate", this.m_certs[0].GetRawCertDataString());
				}
				else
				{
					for (int i = 0; i < this.m_certs.Length; i++)
					{
						SecurityElement securityElement2 = new SecurityElement("Cert");
						securityElement2.AddAttribute("X509v3Certificate", this.m_certs[i].GetRawCertDataString());
						securityElement.AddChild(securityElement2);
					}
				}
			}
			return securityElement;
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x000C7D7E File Offset: 0x000C6D7E
		int IBuiltInPermission.GetTokenIndex()
		{
			return PublisherIdentityPermission.GetTokenIndex();
		}

		// Token: 0x06003B0A RID: 15114 RVA: 0x000C7D85 File Offset: 0x000C6D85
		internal static int GetTokenIndex()
		{
			return 10;
		}

		// Token: 0x04001E86 RID: 7814
		private bool m_unrestricted;

		// Token: 0x04001E87 RID: 7815
		private X509Certificate[] m_certs;
	}
}
