using System;
using System.Collections.Generic;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200003F RID: 63
	internal sealed class SymbolTable
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x00014503 File Offset: 0x00012703
		internal void EnterScope()
		{
			this.symbols.Add(new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase));
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001451A File Offset: 0x0001271A
		internal void ExitScope()
		{
			this.symbols.RemoveAt(this.symbols.Count - 1);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00014534 File Offset: 0x00012734
		internal void Add(string name, Symbol value)
		{
			this.symbols[this.symbols.Count - 1][name] = value;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00014558 File Offset: 0x00012758
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

		// Token: 0x040000F5 RID: 245
		private readonly List<Dictionary<string, Symbol>> symbols = new List<Dictionary<string, Symbol>>();
	}
}
