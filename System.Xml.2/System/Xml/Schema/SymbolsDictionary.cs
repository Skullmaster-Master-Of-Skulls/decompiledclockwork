using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001EB RID: 491
	internal class SymbolsDictionary
	{
		// Token: 0x06002086 RID: 8326 RVA: 0x000B262A File Offset: 0x000B082A
		public SymbolsDictionary()
		{
			this.names = new Hashtable();
			this.particles = new ArrayList();
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x000B264F File Offset: 0x000B084F
		public int Count
		{
			get
			{
				return this.last + 1;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x000B2659 File Offset: 0x000B0859
		public int CountOfNames
		{
			get
			{
				return this.names.Count;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x000B2666 File Offset: 0x000B0866
		// (set) Token: 0x0600208A RID: 8330 RVA: 0x000B266E File Offset: 0x000B086E
		public bool IsUpaEnforced
		{
			get
			{
				return this.isUpaEnforced;
			}
			set
			{
				this.isUpaEnforced = value;
			}
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x000B2678 File Offset: 0x000B0878
		public int AddName(XmlQualifiedName name, object particle)
		{
			object obj = this.names[name];
			if (obj != null)
			{
				int num = (int)obj;
				if (this.particles[num] != particle)
				{
					this.isUpaEnforced = false;
				}
				return num;
			}
			this.names.Add(name, this.last);
			this.particles.Add(particle);
			int num2 = this.last;
			this.last = num2 + 1;
			return num2;
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x000B26EC File Offset: 0x000B08EC
		public void AddNamespaceList(NamespaceList list, object particle, bool allowLocal)
		{
			switch (list.Type)
			{
			case NamespaceList.ListType.Any:
				this.particleLast = particle;
				return;
			case NamespaceList.ListType.Other:
				this.AddWildcard(list.Excluded, null);
				if (!allowLocal)
				{
					this.AddWildcard(string.Empty, null);
					return;
				}
				break;
			case NamespaceList.ListType.Set:
				foreach (object obj in list.Enumerate)
				{
					string wildcard = (string)obj;
					this.AddWildcard(wildcard, particle);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x000B2788 File Offset: 0x000B0988
		private void AddWildcard(string wildcard, object particle)
		{
			if (this.wildcards == null)
			{
				this.wildcards = new Hashtable();
			}
			object obj = this.wildcards[wildcard];
			if (obj == null)
			{
				this.wildcards.Add(wildcard, this.last);
				this.particles.Add(particle);
				this.last++;
				return;
			}
			if (particle != null)
			{
				this.particles[(int)obj] = particle;
			}
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x000B2800 File Offset: 0x000B0A00
		public ICollection GetNamespaceListSymbols(NamespaceList list)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.names.Keys)
			{
				XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)obj;
				if (xmlQualifiedName != XmlQualifiedName.Empty && list.Allows(xmlQualifiedName))
				{
					arrayList.Add(this.names[xmlQualifiedName]);
				}
			}
			if (this.wildcards != null)
			{
				foreach (object obj2 in this.wildcards.Keys)
				{
					string text = (string)obj2;
					if (list.Allows(text))
					{
						arrayList.Add(this.wildcards[text]);
					}
				}
			}
			if (list.Type == NamespaceList.ListType.Any || list.Type == NamespaceList.ListType.Other)
			{
				arrayList.Add(this.last);
			}
			return arrayList;
		}

		// Token: 0x170006BB RID: 1723
		public int this[XmlQualifiedName name]
		{
			get
			{
				object obj = this.names[name];
				if (obj != null)
				{
					return (int)obj;
				}
				if (this.wildcards != null)
				{
					obj = this.wildcards[name.Namespace];
					if (obj != null)
					{
						return (int)obj;
					}
				}
				return this.last;
			}
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000B296C File Offset: 0x000B0B6C
		public bool Exists(XmlQualifiedName name)
		{
			return this.names[name] != null;
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000B298C File Offset: 0x000B0B8C
		public object GetParticle(int symbol)
		{
			if (symbol != this.last)
			{
				return this.particles[symbol];
			}
			return this.particleLast;
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000B29AC File Offset: 0x000B0BAC
		public string NameOf(int symbol)
		{
			foreach (object obj in this.names)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if ((int)dictionaryEntry.Value == symbol)
				{
					return ((XmlQualifiedName)dictionaryEntry.Key).ToString();
				}
			}
			if (this.wildcards != null)
			{
				foreach (object obj2 in this.wildcards)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if ((int)dictionaryEntry2.Value == symbol)
					{
						return (string)dictionaryEntry2.Key + ":*";
					}
				}
			}
			return "##other:*";
		}

		// Token: 0x04000DB1 RID: 3505
		private int last;

		// Token: 0x04000DB2 RID: 3506
		private Hashtable names;

		// Token: 0x04000DB3 RID: 3507
		private Hashtable wildcards;

		// Token: 0x04000DB4 RID: 3508
		private ArrayList particles;

		// Token: 0x04000DB5 RID: 3509
		private object particleLast;

		// Token: 0x04000DB6 RID: 3510
		private bool isUpaEnforced = true;
	}
}
