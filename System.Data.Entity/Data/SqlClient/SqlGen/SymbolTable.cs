using System;
using System.Collections.Generic;

namespace System.Data.SqlClient.SqlGen
{
	// Token: 0x02000037 RID: 55
	internal sealed class SymbolTable
	{
		// Token: 0x060004F8 RID: 1272 RVA: 0x000170A9 File Offset: 0x000152A9
		internal void EnterScope()
		{
			this.symbols.Add(new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase));
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000170C0 File Offset: 0x000152C0
		internal void ExitScope()
		{
			this.symbols.RemoveAt(this.symbols.Count - 1);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000170DA File Offset: 0x000152DA
		internal void Add(string name, Symbol value)
		{
			this.symbols[this.symbols.Count - 1][name] = value;
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000170FC File Offset: 0x000152FC
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

		// Token: 0x04000739 RID: 1849
		private List<Dictionary<string, Symbol>> symbols = new List<Dictionary<string, Symbol>>();
	}
}
