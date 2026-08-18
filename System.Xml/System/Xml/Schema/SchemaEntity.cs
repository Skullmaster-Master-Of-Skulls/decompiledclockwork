using System;

namespace System.Xml.Schema
{
	// Token: 0x02000212 RID: 530
	internal sealed class SchemaEntity
	{
		// Token: 0x06001973 RID: 6515 RVA: 0x00079D68 File Offset: 0x00078D68
		internal SchemaEntity(XmlQualifiedName name, bool isParameter)
		{
			this.name = name;
			this.isParameter = isParameter;
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00079D8C File Offset: 0x00078D8C
		internal static bool IsPredefinedEntity(string n)
		{
			return n == "lt" || n == "gt" || n == "amp" || n == "apos" || n == "quot";
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x00079DDA File Offset: 0x00078DDA
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x00079DE2 File Offset: 0x00078DE2
		// (set) Token: 0x06001977 RID: 6519 RVA: 0x00079DEA File Offset: 0x00078DEA
		internal string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
				this.isExternal = true;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x00079DFA File Offset: 0x00078DFA
		// (set) Token: 0x06001979 RID: 6521 RVA: 0x00079E02 File Offset: 0x00078E02
		internal string Pubid
		{
			get
			{
				return this.pubid;
			}
			set
			{
				this.pubid = value;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600197A RID: 6522 RVA: 0x00079E0B File Offset: 0x00078E0B
		// (set) Token: 0x0600197B RID: 6523 RVA: 0x00079E13 File Offset: 0x00078E13
		internal bool IsProcessed
		{
			get
			{
				return this.isProcessed;
			}
			set
			{
				this.isProcessed = value;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600197C RID: 6524 RVA: 0x00079E1C File Offset: 0x00078E1C
		// (set) Token: 0x0600197D RID: 6525 RVA: 0x00079E24 File Offset: 0x00078E24
		internal bool IsExternal
		{
			get
			{
				return this.isExternal;
			}
			set
			{
				this.isExternal = value;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600197E RID: 6526 RVA: 0x00079E2D File Offset: 0x00078E2D
		// (set) Token: 0x0600197F RID: 6527 RVA: 0x00079E35 File Offset: 0x00078E35
		internal bool DeclaredInExternal
		{
			get
			{
				return this.isDeclaredInExternal;
			}
			set
			{
				this.isDeclaredInExternal = value;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x00079E3E File Offset: 0x00078E3E
		// (set) Token: 0x06001981 RID: 6529 RVA: 0x00079E46 File Offset: 0x00078E46
		internal bool IsParEntity
		{
			get
			{
				return this.isParameter;
			}
			set
			{
				this.isParameter = value;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x00079E4F File Offset: 0x00078E4F
		// (set) Token: 0x06001983 RID: 6531 RVA: 0x00079E57 File Offset: 0x00078E57
		internal XmlQualifiedName NData
		{
			get
			{
				return this.ndata;
			}
			set
			{
				this.ndata = value;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x00079E60 File Offset: 0x00078E60
		// (set) Token: 0x06001985 RID: 6533 RVA: 0x00079E68 File Offset: 0x00078E68
		internal string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
				this.isExternal = false;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x00079E78 File Offset: 0x00078E78
		// (set) Token: 0x06001987 RID: 6535 RVA: 0x00079E80 File Offset: 0x00078E80
		internal int Line
		{
			get
			{
				return this.lineNumber;
			}
			set
			{
				this.lineNumber = value;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x00079E89 File Offset: 0x00078E89
		// (set) Token: 0x06001989 RID: 6537 RVA: 0x00079E91 File Offset: 0x00078E91
		internal int Pos
		{
			get
			{
				return this.linePosition;
			}
			set
			{
				this.linePosition = value;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x00079E9A File Offset: 0x00078E9A
		// (set) Token: 0x0600198B RID: 6539 RVA: 0x00079EB0 File Offset: 0x00078EB0
		internal string BaseURI
		{
			get
			{
				if (this.baseURI != null)
				{
					return this.baseURI;
				}
				return string.Empty;
			}
			set
			{
				this.baseURI = value;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x00079EB9 File Offset: 0x00078EB9
		// (set) Token: 0x0600198D RID: 6541 RVA: 0x00079ECF File Offset: 0x00078ECF
		internal string DeclaredURI
		{
			get
			{
				if (this.declaredURI != null)
				{
					return this.declaredURI;
				}
				return string.Empty;
			}
			set
			{
				this.declaredURI = value;
			}
		}

		// Token: 0x04000ED6 RID: 3798
		private XmlQualifiedName name;

		// Token: 0x04000ED7 RID: 3799
		private string url;

		// Token: 0x04000ED8 RID: 3800
		private string pubid;

		// Token: 0x04000ED9 RID: 3801
		private string text;

		// Token: 0x04000EDA RID: 3802
		private XmlQualifiedName ndata = XmlQualifiedName.Empty;

		// Token: 0x04000EDB RID: 3803
		private int lineNumber;

		// Token: 0x04000EDC RID: 3804
		private int linePosition;

		// Token: 0x04000EDD RID: 3805
		private bool isParameter;

		// Token: 0x04000EDE RID: 3806
		private bool isExternal;

		// Token: 0x04000EDF RID: 3807
		private bool isProcessed;

		// Token: 0x04000EE0 RID: 3808
		private bool isDeclaredInExternal;

		// Token: 0x04000EE1 RID: 3809
		private string baseURI;

		// Token: 0x04000EE2 RID: 3810
		private string declaredURI;
	}
}
