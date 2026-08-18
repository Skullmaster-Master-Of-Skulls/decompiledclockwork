using System;
using System.Collections;
using System.Globalization;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000026 RID: 38
	public class LdapCompareAttrNames : IComparer
	{
		// Token: 0x0600015F RID: 351 RVA: 0x000079EC File Offset: 0x000069EC
		private void InitBlock()
		{
			this.location = CultureInfo.CurrentCulture;
			this.collator = CultureInfo.CurrentCulture.CompareInfo;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00007A14 File Offset: 0x00006A14
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00007A2C File Offset: 0x00006A2C
		public virtual CultureInfo Locale
		{
			get
			{
				return this.location;
			}
			set
			{
				this.collator = value.CompareInfo;
				this.location = value;
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007A4C File Offset: 0x00006A4C
		public LdapCompareAttrNames(string attrName)
		{
			this.InitBlock();
			this.sortByNames = new string[1];
			this.sortByNames[0] = attrName;
			this.sortAscending = new bool[1];
			this.sortAscending[0] = true;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007A90 File Offset: 0x00006A90
		public LdapCompareAttrNames(string attrName, bool ascendingFlag)
		{
			this.InitBlock();
			this.sortByNames = new string[1];
			this.sortByNames[0] = attrName;
			this.sortAscending = new bool[1];
			this.sortAscending[0] = ascendingFlag;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00007AD4 File Offset: 0x00006AD4
		public LdapCompareAttrNames(string[] attrNames)
		{
			this.InitBlock();
			this.sortByNames = new string[attrNames.Length];
			this.sortAscending = new bool[attrNames.Length];
			for (int i = 0; i < attrNames.Length; i++)
			{
				this.sortByNames[i] = attrNames[i];
				this.sortAscending[i] = true;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00007B2C File Offset: 0x00006B2C
		public LdapCompareAttrNames(string[] attrNames, bool[] ascendingFlags)
		{
			this.InitBlock();
			if (attrNames.Length != ascendingFlags.Length)
			{
				throw new LdapException("UNEQUAL_LENGTHS", 18, null);
			}
			this.sortByNames = new string[attrNames.Length];
			this.sortAscending = new bool[ascendingFlags.Length];
			for (int i = 0; i < attrNames.Length; i++)
			{
				this.sortByNames[i] = attrNames[i];
				this.sortAscending[i] = ascendingFlags[i];
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00007B9C File Offset: 0x00006B9C
		public virtual int Compare(object object1, object object2)
		{
			LdapEntry ldapEntry = (LdapEntry)object1;
			LdapEntry ldapEntry2 = (LdapEntry)object2;
			int num = 0;
			if (this.collator == null)
			{
				this.collator = CultureInfo.CurrentCulture.CompareInfo;
			}
			int num2;
			do
			{
				LdapAttribute attribute = ldapEntry.getAttribute(this.sortByNames[num]);
				LdapAttribute attribute2 = ldapEntry2.getAttribute(this.sortByNames[num]);
				if (attribute != null && attribute2 != null)
				{
					string[] stringValueArray = attribute.StringValueArray;
					string[] stringValueArray2 = attribute2.StringValueArray;
					num2 = this.collator.Compare(stringValueArray[0], stringValueArray2[0]);
				}
				else if (attribute != null)
				{
					num2 = -1;
				}
				else if (attribute2 != null)
				{
					num2 = 1;
				}
				else
				{
					num2 = 0;
				}
				num++;
			}
			while (num2 == 0 && num < this.sortByNames.Length);
			int result;
			if (this.sortAscending[num - 1])
			{
				result = num2;
			}
			else
			{
				result = -num2;
			}
			return result;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007C64 File Offset: 0x00006C64
		public override bool Equals(object comparator)
		{
			bool result;
			if (!(comparator is LdapCompareAttrNames))
			{
				result = false;
			}
			else
			{
				LdapCompareAttrNames ldapCompareAttrNames = (LdapCompareAttrNames)comparator;
				if (ldapCompareAttrNames.sortByNames.Length != this.sortByNames.Length || ldapCompareAttrNames.sortAscending.Length != this.sortAscending.Length)
				{
					result = false;
				}
				else
				{
					for (int i = 0; i < this.sortByNames.Length; i++)
					{
						if (ldapCompareAttrNames.sortAscending[i] != this.sortAscending[i])
						{
							return false;
						}
						if (!ldapCompareAttrNames.sortByNames[i].ToUpper().Equals(this.sortByNames[i].ToUpper()))
						{
							return false;
						}
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x040000C2 RID: 194
		private string[] sortByNames;

		// Token: 0x040000C3 RID: 195
		private bool[] sortAscending;

		// Token: 0x040000C4 RID: 196
		private CultureInfo location;

		// Token: 0x040000C5 RID: 197
		private CompareInfo collator;
	}
}
