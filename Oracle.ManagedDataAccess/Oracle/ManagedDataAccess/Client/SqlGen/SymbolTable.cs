using System;
using System.Collections.Generic;

namespace Oracle.ManagedDataAccess.Client.SqlGen
{
	// Token: 0x020000F7 RID: 247
	internal sealed class SymbolTable
	{
		// Token: 0x06000A5B RID: 2651 RVA: 0x00075404 File Offset: 0x00073604
		internal void EnterScope()
		{
			this.symbols.Add(new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase));
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0007541C File Offset: 0x0007361C
		internal void ExitScope()
		{
			this.symbols.RemoveAt(this.symbols.Count - 1);
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00075438 File Offset: 0x00073638
		internal void Add(string name, Symbol value)
		{
			this.symbols[this.symbols.Count - 1][name] = value;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0007545C File Offset: 0x0007365C
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

		// Token: 0x04000C83 RID: 3203
		private List<Dictionary<string, Symbol>> symbols = new List<Dictionary<string, Symbol>>();
	}
}
