using System;
using System.Collections;
using System.Globalization;

namespace System.Xml.Serialization
{
	// Token: 0x020002B1 RID: 689
	public class CodeIdentifiers
	{
		// Token: 0x06002111 RID: 8465 RVA: 0x0009C68F File Offset: 0x0009B68F
		public CodeIdentifiers() : this(true)
		{
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x0009C698 File Offset: 0x0009B698
		public CodeIdentifiers(bool caseSensitive)
		{
			if (caseSensitive)
			{
				this.identifiers = new Hashtable();
				this.reservedIdentifiers = new Hashtable();
			}
			else
			{
				IEqualityComparer equalityComparer = new CaseInsensitiveKeyComparer();
				this.identifiers = new Hashtable(equalityComparer);
				this.reservedIdentifiers = new Hashtable(equalityComparer);
			}
			this.list = new ArrayList();
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x0009C6EF File Offset: 0x0009B6EF
		public void Clear()
		{
			this.identifiers.Clear();
			this.list.Clear();
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x0009C707 File Offset: 0x0009B707
		// (set) Token: 0x06002115 RID: 8469 RVA: 0x0009C70F File Offset: 0x0009B70F
		public bool UseCamelCasing
		{
			get
			{
				return this.camelCase;
			}
			set
			{
				this.camelCase = value;
			}
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x0009C718 File Offset: 0x0009B718
		public string MakeRightCase(string identifier)
		{
			if (this.camelCase)
			{
				return CodeIdentifier.MakeCamel(identifier);
			}
			return CodeIdentifier.MakePascal(identifier);
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0009C730 File Offset: 0x0009B730
		public string MakeUnique(string identifier)
		{
			if (this.IsInUse(identifier))
			{
				int num = 1;
				string text;
				for (;;)
				{
					text = identifier + num.ToString(CultureInfo.InvariantCulture);
					if (!this.IsInUse(text))
					{
						break;
					}
					num++;
				}
				identifier = text;
			}
			if (identifier.Length > 511)
			{
				return this.MakeUnique("Item");
			}
			return identifier;
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x0009C789 File Offset: 0x0009B789
		public void AddReserved(string identifier)
		{
			this.reservedIdentifiers.Add(identifier, identifier);
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x0009C798 File Offset: 0x0009B798
		public void RemoveReserved(string identifier)
		{
			this.reservedIdentifiers.Remove(identifier);
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x0009C7A6 File Offset: 0x0009B7A6
		public string AddUnique(string identifier, object value)
		{
			identifier = this.MakeUnique(identifier);
			this.Add(identifier, value);
			return identifier;
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x0009C7BA File Offset: 0x0009B7BA
		public bool IsInUse(string identifier)
		{
			return this.identifiers.Contains(identifier) || this.reservedIdentifiers.Contains(identifier);
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x0009C7D8 File Offset: 0x0009B7D8
		public void Add(string identifier, object value)
		{
			this.identifiers.Add(identifier, value);
			this.list.Add(value);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0009C7F4 File Offset: 0x0009B7F4
		public void Remove(string identifier)
		{
			this.list.Remove(this.identifiers[identifier]);
			this.identifiers.Remove(identifier);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0009C81C File Offset: 0x0009B81C
		public object ToArray(Type type)
		{
			Array array = Array.CreateInstance(type, this.list.Count);
			this.list.CopyTo(array, 0);
			return array;
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x0009C84C File Offset: 0x0009B84C
		internal CodeIdentifiers Clone()
		{
			return new CodeIdentifiers
			{
				identifiers = (Hashtable)this.identifiers.Clone(),
				reservedIdentifiers = (Hashtable)this.reservedIdentifiers.Clone(),
				list = (ArrayList)this.list.Clone(),
				camelCase = this.camelCase
			};
		}

		// Token: 0x0400142F RID: 5167
		private Hashtable identifiers;

		// Token: 0x04001430 RID: 5168
		private Hashtable reservedIdentifiers;

		// Token: 0x04001431 RID: 5169
		private ArrayList list;

		// Token: 0x04001432 RID: 5170
		private bool camelCase;
	}
}
