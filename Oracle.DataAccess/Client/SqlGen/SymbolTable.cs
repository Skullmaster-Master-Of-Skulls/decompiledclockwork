using System;
using System.Collections.Generic;

namespace Oracle.DataAccess.Client.SqlGen
{
	// Token: 0x0200004B RID: 75
	internal sealed class SymbolTable
	{
		// Token: 0x06000341 RID: 833 RVA: 0x00028612 File Offset: 0x00027612
		internal void EnterScope()
		{
			this.symbols.Add(new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase));
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00028629 File Offset: 0x00027629
		internal void ExitScope()
		{
			this.symbols.RemoveAt(this.symbols.Count - 1);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00028643 File Offset: 0x00027643
		internal void Add(string name, Symbol value)
		{
			this.symbols[this.symbols.Count - 1][name] = value;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00028664 File Offset: 0x00027664
		internal Symbol Lookup(string name)
		{
			for (int i = this.symbols.Count - 1; i >= 0; i--)
			{
				if (this.symbols[i].ContainsKey(name))
				{
					return this.symbols[i][name];
				}
			}
			return null;
		}

		// Token: 0x0400025D RID: 605
		private List<Dictionary<string, Symbol>> symbols = new List<Dictionary<string, Symbol>>();
	}
}
