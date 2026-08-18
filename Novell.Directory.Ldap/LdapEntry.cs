using System;
using System.Text;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000030 RID: 48
	public class LdapEntry : IComparable
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000B028 File Offset: 0x0000A028
		[CLSCompliant(false)]
		public virtual string DN
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B040 File Offset: 0x0000A040
		public LdapEntry() : this(null, null)
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000B058 File Offset: 0x0000A058
		public LdapEntry(string dn) : this(dn, null)
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000B070 File Offset: 0x0000A070
		public LdapEntry(string dn, LdapAttributeSet attrs)
		{
			if (dn == null)
			{
				dn = "";
			}
			if (attrs == null)
			{
				attrs = new LdapAttributeSet();
			}
			this.dn = dn;
			this.attrs = attrs;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000B0A8 File Offset: 0x0000A0A8
		public virtual LdapAttribute getAttribute(string attrName)
		{
			return this.attrs.getAttribute(attrName);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000B0C8 File Offset: 0x0000A0C8
		public virtual LdapAttributeSet getAttributeSet()
		{
			return this.attrs;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000B0E0 File Offset: 0x0000A0E0
		public virtual LdapAttributeSet getAttributeSet(string subtype)
		{
			return this.attrs.getSubset(subtype);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000B100 File Offset: 0x0000A100
		public virtual int CompareTo(object entry)
		{
			return LdapDN.normalize(this.dn).CompareTo(LdapDN.normalize(((LdapEntry)entry).dn));
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000B134 File Offset: 0x0000A134
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("LdapEntry: ");
			if (this.dn != null)
			{
				stringBuilder.Append(this.dn + "; ");
			}
			if (this.attrs != null)
			{
				stringBuilder.Append(this.attrs.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000107 RID: 263
		protected internal string dn;

		// Token: 0x04000108 RID: 264
		protected internal LdapAttributeSet attrs;
	}
}
