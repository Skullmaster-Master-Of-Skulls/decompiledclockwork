using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x02000659 RID: 1625
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06003A95 RID: 14997 RVA: 0x000C5864 File Offset: 0x000C4864
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_serializedPermission != null)
			{
				this.FromXml(SecurityElement.FromString(this.m_serializedPermission));
				this.m_serializedPermission = null;
				return;
			}
			if (this.m_url != null)
			{
				this.m_unrestricted = false;
				this.m_urls = new URLString[1];
				this.m_urls[0] = this.m_url;
				this.m_url = null;
			}
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x000C58C4 File Offset: 0x000C48C4
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = this.ToXml().ToString();
				if (this.m_urls != null && this.m_urls.Length == 1)
				{
					this.m_url = this.m_urls[0];
				}
			}
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x000C5912 File Offset: 0x000C4912
		[OnSerialized]
		private void OnSerialized(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = null;
				this.m_url = null;
			}
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x000C5934 File Offset: 0x000C4934
		public UrlIdentityPermission(PermissionState state)
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

		// Token: 0x06003A99 RID: 15001 RVA: 0x000C5984 File Offset: 0x000C4984
		public UrlIdentityPermission(string site)
		{
			if (site == null)
			{
				throw new ArgumentNullException("site");
			}
			this.Url = site;
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000C59A1 File Offset: 0x000C49A1
		internal UrlIdentityPermission(URLString site)
		{
			this.m_unrestricted = false;
			this.m_urls = new URLString[1];
			this.m_urls[0] = site;
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06003A9C RID: 15004 RVA: 0x000C59FB File Offset: 0x000C49FB
		// (set) Token: 0x06003A9B RID: 15003 RVA: 0x000C59C5 File Offset: 0x000C49C5
		public string Url
		{
			get
			{
				if (this.m_urls == null)
				{
					return "";
				}
				if (this.m_urls.Length == 1)
				{
					return this.m_urls[0].ToString();
				}
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
			}
			set
			{
				this.m_unrestricted = false;
				if (value == null || value.Length == 0)
				{
					this.m_urls = null;
					return;
				}
				this.m_urls = new URLString[1];
				this.m_urls[0] = new URLString(value);
			}
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x000C5A34 File Offset: 0x000C4A34
		public override IPermission Copy()
		{
			UrlIdentityPermission urlIdentityPermission = new UrlIdentityPermission(PermissionState.None);
			urlIdentityPermission.m_unrestricted = this.m_unrestricted;
			if (this.m_urls != null)
			{
				urlIdentityPermission.m_urls = new URLString[this.m_urls.Length];
				for (int i = 0; i < this.m_urls.Length; i++)
				{
					urlIdentityPermission.m_urls[i] = (URLString)this.m_urls[i].Copy();
				}
			}
			return urlIdentityPermission;
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x000C5AA0 File Offset: 0x000C4AA0
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_unrestricted && (this.m_urls == null || this.m_urls.Length == 0);
			}
			UrlIdentityPermission urlIdentityPermission = target as UrlIdentityPermission;
			if (urlIdentityPermission == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			if (urlIdentityPermission.m_unrestricted)
			{
				return true;
			}
			if (this.m_unrestricted)
			{
				return false;
			}
			if (this.m_urls != null)
			{
				foreach (URLString urlstring in this.m_urls)
				{
					bool flag = false;
					if (urlIdentityPermission.m_urls != null)
					{
						foreach (URLString operand in urlIdentityPermission.m_urls)
						{
							if (urlstring.IsSubsetOf(operand))
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

		// Token: 0x06003A9F RID: 15007 RVA: 0x000C5B90 File Offset: 0x000C4B90
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			UrlIdentityPermission urlIdentityPermission = target as UrlIdentityPermission;
			if (urlIdentityPermission == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			if (this.m_unrestricted && urlIdentityPermission.m_unrestricted)
			{
				return new UrlIdentityPermission(PermissionState.None)
				{
					m_unrestricted = true
				};
			}
			if (this.m_unrestricted)
			{
				return urlIdentityPermission.Copy();
			}
			if (urlIdentityPermission.m_unrestricted)
			{
				return this.Copy();
			}
			if (this.m_urls == null || urlIdentityPermission.m_urls == null || this.m_urls.Length == 0 || urlIdentityPermission.m_urls.Length == 0)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList();
			foreach (URLString urlstring in this.m_urls)
			{
				foreach (URLString operand in urlIdentityPermission.m_urls)
				{
					URLString urlstring2 = (URLString)urlstring.Intersect(operand);
					if (urlstring2 != null)
					{
						arrayList.Add(urlstring2);
					}
				}
			}
			if (arrayList.Count == 0)
			{
				return null;
			}
			return new UrlIdentityPermission(PermissionState.None)
			{
				m_urls = (URLString[])arrayList.ToArray(typeof(URLString))
			};
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x000C5CD8 File Offset: 0x000C4CD8
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				if ((this.m_urls == null || this.m_urls.Length == 0) && !this.m_unrestricted)
				{
					return null;
				}
				return this.Copy();
			}
			else
			{
				UrlIdentityPermission urlIdentityPermission = target as UrlIdentityPermission;
				if (urlIdentityPermission == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
					{
						base.GetType().FullName
					}));
				}
				if (this.m_unrestricted || urlIdentityPermission.m_unrestricted)
				{
					return new UrlIdentityPermission(PermissionState.None)
					{
						m_unrestricted = true
					};
				}
				if (this.m_urls == null || this.m_urls.Length == 0)
				{
					if (urlIdentityPermission.m_urls == null || urlIdentityPermission.m_urls.Length == 0)
					{
						return null;
					}
					return urlIdentityPermission.Copy();
				}
				else
				{
					if (urlIdentityPermission.m_urls == null || urlIdentityPermission.m_urls.Length == 0)
					{
						return this.Copy();
					}
					ArrayList arrayList = new ArrayList();
					foreach (URLString value in this.m_urls)
					{
						arrayList.Add(value);
					}
					foreach (URLString urlstring in urlIdentityPermission.m_urls)
					{
						bool flag = false;
						foreach (object obj in arrayList)
						{
							URLString url = (URLString)obj;
							if (urlstring.Equals(url))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							arrayList.Add(urlstring);
						}
					}
					return new UrlIdentityPermission(PermissionState.None)
					{
						m_urls = (URLString[])arrayList.ToArray(typeof(URLString))
					};
				}
			}
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x000C5E90 File Offset: 0x000C4E90
		public override void FromXml(SecurityElement esd)
		{
			this.m_unrestricted = false;
			this.m_urls = null;
			CodeAccessPermission.ValidateElement(esd, this);
			string text = esd.Attribute("Unrestricted");
			if (text != null && string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_unrestricted = true;
				return;
			}
			string text2 = esd.Attribute("Url");
			ArrayList arrayList = new ArrayList();
			if (text2 != null)
			{
				arrayList.Add(new URLString(text2, true));
			}
			ArrayList children = esd.Children;
			if (children != null)
			{
				foreach (object obj in children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					text2 = securityElement.Attribute("Url");
					if (text2 != null)
					{
						arrayList.Add(new URLString(text2, true));
					}
				}
			}
			if (arrayList.Count != 0)
			{
				this.m_urls = (URLString[])arrayList.ToArray(typeof(URLString));
			}
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000C5F90 File Offset: 0x000C4F90
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.UrlIdentityPermission");
			if (this.m_unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else if (this.m_urls != null)
			{
				if (this.m_urls.Length == 1)
				{
					securityElement.AddAttribute("Url", this.m_urls[0].ToString());
				}
				else
				{
					for (int i = 0; i < this.m_urls.Length; i++)
					{
						SecurityElement securityElement2 = new SecurityElement("Url");
						securityElement2.AddAttribute("Url", this.m_urls[i].ToString());
						securityElement.AddChild(securityElement2);
					}
				}
			}
			return securityElement;
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000C602E File Offset: 0x000C502E
		int IBuiltInPermission.GetTokenIndex()
		{
			return UrlIdentityPermission.GetTokenIndex();
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x000C6035 File Offset: 0x000C5035
		internal static int GetTokenIndex()
		{
			return 13;
		}

		// Token: 0x04001E66 RID: 7782
		[OptionalField(VersionAdded = 2)]
		private bool m_unrestricted;

		// Token: 0x04001E67 RID: 7783
		[OptionalField(VersionAdded = 2)]
		private URLString[] m_urls;

		// Token: 0x04001E68 RID: 7784
		[OptionalField(VersionAdded = 2)]
		private string m_serializedPermission;

		// Token: 0x04001E69 RID: 7785
		private URLString m_url;
	}
}
