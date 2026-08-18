using System;
using System.Collections;
using System.Text;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000020 RID: 32
	public class LdapAttributeSet : SupportClass.AbstractSetSupport, ICloneable
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000760C File Offset: 0x0000660C
		public override int Count
		{
			get
			{
				return this.map.Count;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007628 File Offset: 0x00006628
		public LdapAttributeSet()
		{
			this.map = new Hashtable();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007648 File Offset: 0x00006648
		public override object Clone()
		{
			object result;
			try
			{
				object obj = base.MemberwiseClone();
				foreach (object obj2 in this)
				{
					((LdapAttributeSet)obj).Add(((LdapAttribute)obj2).Clone());
				}
				result = obj;
			}
			catch (Exception ex)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return result;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000076B8 File Offset: 0x000066B8
		public virtual LdapAttribute getAttribute(string attrName)
		{
			return (LdapAttribute)this.map[attrName.ToUpper()];
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000076E0 File Offset: 0x000066E0
		public virtual LdapAttribute getAttribute(string attrName, string lang)
		{
			string text = attrName + ";" + lang;
			return (LdapAttribute)this.map[text.ToUpper()];
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007714 File Offset: 0x00006714
		public virtual LdapAttributeSet getSubset(string subtype)
		{
			LdapAttributeSet ldapAttributeSet = new LdapAttributeSet();
			foreach (object obj in this)
			{
				LdapAttribute ldapAttribute = (LdapAttribute)obj;
				if (ldapAttribute.hasSubtype(subtype))
				{
					ldapAttributeSet.Add(ldapAttribute.Clone());
				}
			}
			return ldapAttributeSet;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007760 File Offset: 0x00006760
		public override IEnumerator GetEnumerator()
		{
			return this.map.Values.GetEnumerator();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007784 File Offset: 0x00006784
		public override bool IsEmpty()
		{
			return this.map.Count == 0;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000077A4 File Offset: 0x000067A4
		public override bool Contains(object attr)
		{
			LdapAttribute ldapAttribute = (LdapAttribute)attr;
			return this.map.ContainsKey(ldapAttribute.Name.ToUpper());
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000077D4 File Offset: 0x000067D4
		public override bool Add(object attr)
		{
			LdapAttribute ldapAttribute = (LdapAttribute)attr;
			string key = ldapAttribute.Name.ToUpper();
			bool result;
			if (this.map.ContainsKey(key))
			{
				result = false;
			}
			else
			{
				SupportClass.PutElement(this.map, key, ldapAttribute);
				result = true;
			}
			return result;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000781C File Offset: 0x0000681C
		public override bool Remove(object object_Renamed)
		{
			string text;
			if (object_Renamed is string)
			{
				text = (string)object_Renamed;
			}
			else
			{
				text = ((LdapAttribute)object_Renamed).Name;
			}
			return text != null && SupportClass.HashtableRemove(this.map, text.ToUpper()) != null;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00007868 File Offset: 0x00006868
		public override void Clear()
		{
			this.map.Clear();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007880 File Offset: 0x00006880
		public override bool AddAll(ICollection c)
		{
			bool result = false;
			IEnumerator enumerator = c.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (this.Add(enumerator.Current))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000078B8 File Offset: 0x000068B8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("LdapAttributeSet: ");
			IEnumerator enumerator = this.GetEnumerator();
			bool flag = true;
			while (enumerator.MoveNext())
			{
				if (!flag)
				{
					stringBuilder.Append(" ");
				}
				flag = false;
				LdapAttribute ldapAttribute = (LdapAttribute)enumerator.Current;
				stringBuilder.Append(ldapAttribute.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000BF RID: 191
		private Hashtable map;
	}
}
