using System;
using System.Collections;
using System.Globalization;

namespace System.Xml.Serialization
{
	// Token: 0x0200013A RID: 314
	public class CodeIdentifiers
	{
		// Token: 0x060016BD RID: 5821 RVA: 0x000644D3 File Offset: 0x000626D3
		public CodeIdentifiers() : this(true)
		{
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x000644DC File Offset: 0x000626DC
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

		// Token: 0x060016BF RID: 5823 RVA: 0x00064533 File Offset: 0x00062733
		public void Clear()
		{
			this.identifiers.Clear();
			this.list.Clear();
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x0006454B File Offset: 0x0006274B
		// (set) Token: 0x060016C1 RID: 5825 RVA: 0x00064553 File Offset: 0x00062753
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

		// Token: 0x060016C2 RID: 5826 RVA: 0x0006455C File Offset: 0x0006275C
		public string MakeRightCase(string identifier)
		{
			if (this.camelCase)
			{
				return CodeIdentifier.MakeCamel(identifier);
			}
			return CodeIdentifier.MakePascal(identifier);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00064574 File Offset: 0x00062774
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

		// Token: 0x060016C4 RID: 5828 RVA: 0x000645CD File Offset: 0x000627CD
		public void AddReserved(string identifier)
		{
			this.reservedIdentifiers.Add(identifier, identifier);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x000645DC File Offset: 0x000627DC
		public void RemoveReserved(string identifier)
		{
			this.reservedIdentifiers.Remove(identifier);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x000645EA File Offset: 0x000627EA
		public string AddUnique(string identifier, object value)
		{
			identifier = this.MakeUnique(identifier);
			this.Add(identifier, value);
			return identifier;
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x000645FE File Offset: 0x000627FE
		public bool IsInUse(string identifier)
		{
			return this.identifiers.Contains(identifier) || this.reservedIdentifiers.Contains(identifier);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0006461C File Offset: 0x0006281C
		public void Add(string identifier, object value)
		{
			this.identifiers.Add(identifier, value);
			this.list.Add(value);
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00064638 File Offset: 0x00062838
		public void Remove(string identifier)
		{
			this.list.Remove(this.identifiers[identifier]);
			this.identifiers.Remove(identifier);
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00064660 File Offset: 0x00062860
		public object ToArray(Type type)
		{
			Array array = Array.CreateInstance(type, this.list.Count);
			this.list.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00064690 File Offset: 0x00062890
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

		// Token: 0x04000A9A RID: 2714
		private Hashtable identifiers;

		// Token: 0x04000A9B RID: 2715
		private Hashtable reservedIdentifiers;

		// Token: 0x04000A9C RID: 2716
		private ArrayList list;

		// Token: 0x04000A9D RID: 2717
		private bool camelCase;
	}
}
