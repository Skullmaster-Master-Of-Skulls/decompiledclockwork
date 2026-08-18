using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x02000652 RID: 1618
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		// Token: 0x06003A53 RID: 14931 RVA: 0x000C4044 File Offset: 0x000C3044
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_serializedPermission != null)
			{
				this.FromXml(SecurityElement.FromString(this.m_serializedPermission));
				this.m_serializedPermission = null;
				return;
			}
			if (this.m_site != null)
			{
				this.m_unrestricted = false;
				this.m_sites = new SiteString[1];
				this.m_sites[0] = this.m_site;
				this.m_site = null;
			}
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x000C40A4 File Offset: 0x000C30A4
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = this.ToXml().ToString();
				if (this.m_sites != null && this.m_sites.Length == 1)
				{
					this.m_site = this.m_sites[0];
				}
			}
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x000C40F2 File Offset: 0x000C30F2
		[OnSerialized]
		private void OnSerialized(StreamingContext ctx)
		{
			if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) != (StreamingContextStates)0)
			{
				this.m_serializedPermission = null;
				this.m_site = null;
			}
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x000C4114 File Offset: 0x000C3114
		public SiteIdentityPermission(PermissionState state)
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

		// Token: 0x06003A57 RID: 14935 RVA: 0x000C4164 File Offset: 0x000C3164
		public SiteIdentityPermission(string site)
		{
			this.Site = site;
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06003A59 RID: 14937 RVA: 0x000C4196 File Offset: 0x000C3196
		// (set) Token: 0x06003A58 RID: 14936 RVA: 0x000C4173 File Offset: 0x000C3173
		public string Site
		{
			get
			{
				if (this.m_sites == null)
				{
					return "";
				}
				if (this.m_sites.Length == 1)
				{
					return this.m_sites[0].ToString();
				}
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
			}
			set
			{
				this.m_unrestricted = false;
				this.m_sites = new SiteString[1];
				this.m_sites[0] = new SiteString(value);
			}
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x000C41D0 File Offset: 0x000C31D0
		public override IPermission Copy()
		{
			SiteIdentityPermission siteIdentityPermission = new SiteIdentityPermission(PermissionState.None);
			siteIdentityPermission.m_unrestricted = this.m_unrestricted;
			if (this.m_sites != null)
			{
				siteIdentityPermission.m_sites = new SiteString[this.m_sites.Length];
				for (int i = 0; i < this.m_sites.Length; i++)
				{
					siteIdentityPermission.m_sites[i] = this.m_sites[i].Copy();
				}
			}
			return siteIdentityPermission;
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x000C4234 File Offset: 0x000C3234
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return !this.m_unrestricted && (this.m_sites == null || this.m_sites.Length == 0);
			}
			SiteIdentityPermission siteIdentityPermission = target as SiteIdentityPermission;
			if (siteIdentityPermission == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			if (siteIdentityPermission.m_unrestricted)
			{
				return true;
			}
			if (this.m_unrestricted)
			{
				return false;
			}
			if (this.m_sites != null)
			{
				foreach (SiteString siteString in this.m_sites)
				{
					bool flag = false;
					if (siteIdentityPermission.m_sites != null)
					{
						foreach (SiteString operand in siteIdentityPermission.m_sites)
						{
							if (siteString.IsSubsetOf(operand))
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

		// Token: 0x06003A5C RID: 14940 RVA: 0x000C4324 File Offset: 0x000C3324
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			SiteIdentityPermission siteIdentityPermission = target as SiteIdentityPermission;
			if (siteIdentityPermission == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			if (this.m_unrestricted && siteIdentityPermission.m_unrestricted)
			{
				return new SiteIdentityPermission(PermissionState.None)
				{
					m_unrestricted = true
				};
			}
			if (this.m_unrestricted)
			{
				return siteIdentityPermission.Copy();
			}
			if (siteIdentityPermission.m_unrestricted)
			{
				return this.Copy();
			}
			if (this.m_sites == null || siteIdentityPermission.m_sites == null || this.m_sites.Length == 0 || siteIdentityPermission.m_sites.Length == 0)
			{
				return null;
			}
			ArrayList arrayList = new ArrayList();
			foreach (SiteString siteString in this.m_sites)
			{
				foreach (SiteString operand in siteIdentityPermission.m_sites)
				{
					SiteString siteString2 = siteString.Intersect(operand);
					if (siteString2 != null)
					{
						arrayList.Add(siteString2);
					}
				}
			}
			if (arrayList.Count == 0)
			{
				return null;
			}
			return new SiteIdentityPermission(PermissionState.None)
			{
				m_sites = (SiteString[])arrayList.ToArray(typeof(SiteString))
			};
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x000C4468 File Offset: 0x000C3468
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				if ((this.m_sites == null || this.m_sites.Length == 0) && !this.m_unrestricted)
				{
					return null;
				}
				return this.Copy();
			}
			else
			{
				SiteIdentityPermission siteIdentityPermission = target as SiteIdentityPermission;
				if (siteIdentityPermission == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
					{
						base.GetType().FullName
					}));
				}
				if (this.m_unrestricted || siteIdentityPermission.m_unrestricted)
				{
					return new SiteIdentityPermission(PermissionState.None)
					{
						m_unrestricted = true
					};
				}
				if (this.m_sites == null || this.m_sites.Length == 0)
				{
					if (siteIdentityPermission.m_sites == null || siteIdentityPermission.m_sites.Length == 0)
					{
						return null;
					}
					return siteIdentityPermission.Copy();
				}
				else
				{
					if (siteIdentityPermission.m_sites == null || siteIdentityPermission.m_sites.Length == 0)
					{
						return this.Copy();
					}
					ArrayList arrayList = new ArrayList();
					foreach (SiteString value in this.m_sites)
					{
						arrayList.Add(value);
					}
					foreach (SiteString siteString in siteIdentityPermission.m_sites)
					{
						bool flag = false;
						foreach (object obj in arrayList)
						{
							SiteString obj2 = (SiteString)obj;
							if (siteString.Equals(obj2))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							arrayList.Add(siteString);
						}
					}
					return new SiteIdentityPermission(PermissionState.None)
					{
						m_sites = (SiteString[])arrayList.ToArray(typeof(SiteString))
					};
				}
			}
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x000C4620 File Offset: 0x000C3620
		public override void FromXml(SecurityElement esd)
		{
			this.m_unrestricted = false;
			this.m_sites = null;
			CodeAccessPermission.ValidateElement(esd, this);
			string text = esd.Attribute("Unrestricted");
			if (text != null && string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_unrestricted = true;
				return;
			}
			string text2 = esd.Attribute("Site");
			ArrayList arrayList = new ArrayList();
			if (text2 != null)
			{
				arrayList.Add(new SiteString(text2));
			}
			ArrayList children = esd.Children;
			if (children != null)
			{
				foreach (object obj in children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					text2 = securityElement.Attribute("Site");
					if (text2 != null)
					{
						arrayList.Add(new SiteString(text2));
					}
				}
			}
			if (arrayList.Count != 0)
			{
				this.m_sites = (SiteString[])arrayList.ToArray(typeof(SiteString));
			}
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x000C471C File Offset: 0x000C371C
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.SiteIdentityPermission");
			if (this.m_unrestricted)
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			else if (this.m_sites != null)
			{
				if (this.m_sites.Length == 1)
				{
					securityElement.AddAttribute("Site", this.m_sites[0].ToString());
				}
				else
				{
					for (int i = 0; i < this.m_sites.Length; i++)
					{
						SecurityElement securityElement2 = new SecurityElement("Site");
						securityElement2.AddAttribute("Site", this.m_sites[i].ToString());
						securityElement.AddChild(securityElement2);
					}
				}
			}
			return securityElement;
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x000C47BA File Offset: 0x000C37BA
		int IBuiltInPermission.GetTokenIndex()
		{
			return SiteIdentityPermission.GetTokenIndex();
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x000C47C1 File Offset: 0x000C37C1
		internal static int GetTokenIndex()
		{
			return 11;
		}

		// Token: 0x04001E51 RID: 7761
		[OptionalField(VersionAdded = 2)]
		private bool m_unrestricted;

		// Token: 0x04001E52 RID: 7762
		[OptionalField(VersionAdded = 2)]
		private SiteString[] m_sites;

		// Token: 0x04001E53 RID: 7763
		[OptionalField(VersionAdded = 2)]
		private string m_serializedPermission;

		// Token: 0x04001E54 RID: 7764
		private SiteString m_site;
	}
}
