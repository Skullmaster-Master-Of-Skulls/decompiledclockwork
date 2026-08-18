using System;
using System.Collections;

namespace OracleInternal.Network
{
	// Token: 0x02000159 RID: 345
	internal sealed class NVPair
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x00092358 File Offset: 0x00090558
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x00092360 File Offset: 0x00090560
		internal string Name
		{
			get
			{
				return this.m_sName;
			}
			set
			{
				this.m_sName = value;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0009236C File Offset: 0x0009056C
		internal NVPair Parent
		{
			get
			{
				return this.m_parent;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x00092374 File Offset: 0x00090574
		internal int RHSType
		{
			get
			{
				return this.m_rhsType;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x0009237C File Offset: 0x0009057C
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x00092384 File Offset: 0x00090584
		internal int ListType
		{
			get
			{
				return this.m_iListType;
			}
			set
			{
				this.m_iListType = value;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00092390 File Offset: 0x00090590
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x00092398 File Offset: 0x00090598
		internal string Atom
		{
			get
			{
				return this.m_sAtom;
			}
			set
			{
				this.m_rhsType = NVPair.RHS_ATOM;
				this.m_sAtom = value;
				this.m_list = null;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x000923B4 File Offset: 0x000905B4
		internal int ListSize
		{
			get
			{
				if (this.m_list == null)
				{
					return 0;
				}
				return this.m_list.Count;
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000923CC File Offset: 0x000905CC
		internal NVPair(string name)
		{
			this.m_sName = name;
			this.m_sAtom = null;
			this.m_list = null;
			this.m_iListType = NVPair.LIST_REGULAR;
			this.m_parent = null;
			this.m_rhsType = NVPair.RHS_NONE;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00092408 File Offset: 0x00090608
		internal NVPair(string name, string atom) : this(name)
		{
			this.Atom = atom;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00092418 File Offset: 0x00090618
		internal NVPair(string name, NVPair child) : this(name)
		{
			this.AddListElement(child);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00092428 File Offset: 0x00090628
		private void SetParent(NVPair parent)
		{
			this.m_parent = parent;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00092434 File Offset: 0x00090634
		private bool ContainsComment(string str)
		{
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == '#')
				{
					if (i == 0)
					{
						return true;
					}
					if (str[i - 1] != '\\')
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00092474 File Offset: 0x00090674
		internal NVPair GetListElement(int pos)
		{
			if (this.m_list == null)
			{
				return null;
			}
			return (NVPair)this.m_list[pos];
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00092494 File Offset: 0x00090694
		internal void AddListElement(NVPair pair)
		{
			if (this.m_list == null)
			{
				this.m_rhsType = NVPair.RHS_LIST;
				this.m_list = ArrayList.Synchronized(new ArrayList(3));
				this.m_sAtom = null;
			}
			this.m_list.Add(pair);
			pair.SetParent(this);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x000924E0 File Offset: 0x000906E0
		internal void RemoveListElement(int pos)
		{
			if (this.m_list != null)
			{
				this.m_list.RemoveAt(pos);
				if (this.ListSize == 0)
				{
					this.m_list = null;
					this.m_rhsType = NVPair.RHS_NONE;
				}
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00092510 File Offset: 0x00090710
		internal string ValueToString()
		{
			string text = "";
			if (this.m_rhsType == NVPair.RHS_ATOM)
			{
				text += this.m_sAtom;
			}
			else if (this.m_rhsType == NVPair.RHS_LIST)
			{
				if (this.m_iListType == NVPair.LIST_REGULAR)
				{
					for (int i = 0; i < this.ListSize; i++)
					{
						text += this.GetListElement(i).ToString();
					}
				}
				else if (this.m_iListType == NVPair.LIST_COMMASEP)
				{
					for (int j = 0; j < this.ListSize; j++)
					{
						NVPair listElement = this.GetListElement(j);
						text += listElement.Name;
						if (j != this.ListSize - 1)
						{
							text += ", ";
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000925D0 File Offset: 0x000907D0
		public override string ToString()
		{
			string str = "(" + this.m_sName + "=";
			if (this.m_rhsType == NVPair.RHS_ATOM)
			{
				str += this.m_sAtom;
			}
			else if (this.m_rhsType == NVPair.RHS_LIST)
			{
				if (this.m_iListType == NVPair.LIST_REGULAR)
				{
					for (int i = 0; i < this.ListSize; i++)
					{
						str += this.GetListElement(i).ToString();
					}
				}
				else if (this.m_iListType == NVPair.LIST_COMMASEP)
				{
					str += " (";
					for (int j = 0; j < this.ListSize; j++)
					{
						NVPair listElement = this.GetListElement(j);
						str += listElement.Name;
						if (j != this.ListSize - 1)
						{
							str += ", ";
						}
					}
					str += ")";
				}
			}
			return str + ")";
		}

		// Token: 0x04000F20 RID: 3872
		internal static int RHS_NONE = 0;

		// Token: 0x04000F21 RID: 3873
		internal static int RHS_ATOM = 1;

		// Token: 0x04000F22 RID: 3874
		internal static int RHS_LIST = 2;

		// Token: 0x04000F23 RID: 3875
		internal static int LIST_REGULAR = 3;

		// Token: 0x04000F24 RID: 3876
		internal static int LIST_COMMASEP = 4;

		// Token: 0x04000F25 RID: 3877
		private string m_sName;

		// Token: 0x04000F26 RID: 3878
		private int m_rhsType;

		// Token: 0x04000F27 RID: 3879
		private string m_sAtom;

		// Token: 0x04000F28 RID: 3880
		private ArrayList m_list;

		// Token: 0x04000F29 RID: 3881
		private int m_iListType;

		// Token: 0x04000F2A RID: 3882
		private NVPair m_parent;

		// Token: 0x04000F2B RID: 3883
		private static string LINESEP = Environment.NewLine;

		// Token: 0x04000F2C RID: 3884
		private static string ADDRESS = "ADDRESS";

		// Token: 0x04000F2D RID: 3885
		private static string RULE = "RULE";

		// Token: 0x04000F2E RID: 3886
		private static string COMMENT = "COMMENT";
	}
}
