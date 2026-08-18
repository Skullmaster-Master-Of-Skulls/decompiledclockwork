using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x020004B6 RID: 1206
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteMembershipCondition : IConstantMembershipCondition, IReportMatchMembershipCondition, IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable
	{
		// Token: 0x06003004 RID: 12292 RVA: 0x000A49FC File Offset: 0x000A39FC
		internal SiteMembershipCondition()
		{
			this.m_site = null;
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x000A4A0B File Offset: 0x000A3A0B
		public SiteMembershipCondition(string site)
		{
			if (site == null)
			{
				throw new ArgumentNullException("site");
			}
			this.m_site = new SiteString(site);
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06003007 RID: 12295 RVA: 0x000A4A49 File Offset: 0x000A3A49
		// (set) Token: 0x06003006 RID: 12294 RVA: 0x000A4A2D File Offset: 0x000A3A2D
		public string Site
		{
			get
			{
				if (this.m_site == null && this.m_element != null)
				{
					this.ParseSite();
				}
				if (this.m_site != null)
				{
					return this.m_site.ToString();
				}
				return "";
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_site = new SiteString(value);
			}
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x000A4A7C File Offset: 0x000A3A7C
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x000A4A94 File Offset: 0x000A3A94
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			if (evidence == null)
			{
				return false;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				Site site = obj as Site;
				if (site != null)
				{
					if (this.m_site == null && this.m_element != null)
					{
						this.ParseSite();
					}
					if (site.GetSiteString().IsSubsetOf(this.m_site))
					{
						usedEvidence = site;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x000A4AF8 File Offset: 0x000A3AF8
		public IMembershipCondition Copy()
		{
			if (this.m_site == null && this.m_element != null)
			{
				this.ParseSite();
			}
			return new SiteMembershipCondition(this.m_site.ToString());
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000A4B20 File Offset: 0x000A3B20
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x000A4B29 File Offset: 0x000A3B29
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x000A4B34 File Offset: 0x000A3B34
		public SecurityElement ToXml(PolicyLevel level)
		{
			if (this.m_site == null && this.m_element != null)
			{
				this.ParseSite();
			}
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.SiteMembershipCondition");
			securityElement.AddAttribute("version", "1");
			if (this.m_site != null)
			{
				securityElement.AddAttribute("Site", this.m_site.ToString());
			}
			return securityElement;
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000A4BA4 File Offset: 0x000A3BA4
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (!e.Tag.Equals("IMembershipCondition"))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
			}
			lock (this)
			{
				this.m_site = null;
				this.m_element = e;
			}
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000A4C10 File Offset: 0x000A3C10
		private void ParseSite()
		{
			lock (this)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("Site");
					if (text == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_SiteCannotBeNull"));
					}
					this.m_site = new SiteString(text);
					this.m_element = null;
				}
			}
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000A4C80 File Offset: 0x000A3C80
		public override bool Equals(object o)
		{
			SiteMembershipCondition siteMembershipCondition = o as SiteMembershipCondition;
			if (siteMembershipCondition != null)
			{
				if (this.m_site == null && this.m_element != null)
				{
					this.ParseSite();
				}
				if (siteMembershipCondition.m_site == null && siteMembershipCondition.m_element != null)
				{
					siteMembershipCondition.ParseSite();
				}
				if (object.Equals(this.m_site, siteMembershipCondition.m_site))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000A4CD9 File Offset: 0x000A3CD9
		public override int GetHashCode()
		{
			if (this.m_site == null && this.m_element != null)
			{
				this.ParseSite();
			}
			if (this.m_site != null)
			{
				return this.m_site.GetHashCode();
			}
			return typeof(SiteMembershipCondition).GetHashCode();
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000A4D14 File Offset: 0x000A3D14
		public override string ToString()
		{
			if (this.m_site == null && this.m_element != null)
			{
				this.ParseSite();
			}
			if (this.m_site != null)
			{
				return string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Site_ToStringArg"), new object[]
				{
					this.m_site
				});
			}
			return Environment.GetResourceString("Site_ToString");
		}

		// Token: 0x04001859 RID: 6233
		private SiteString m_site;

		// Token: 0x0400185A RID: 6234
		private SecurityElement m_element;
	}
}
