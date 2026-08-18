using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000645 RID: 1605
	[DebuggerDisplay("{{{ToString()}}}")]
	internal class VarList : List<Var>
	{
		// Token: 0x06003EEB RID: 16107 RVA: 0x0012035D File Offset: 0x0011E55D
		internal VarList()
		{
		}

		// Token: 0x06003EEC RID: 16108 RVA: 0x00120365 File Offset: 0x0011E565
		internal VarList(IEnumerable<Var> vars) : base(vars)
		{
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x00120370 File Offset: 0x0011E570
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			foreach (Var var in this)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}{1}", new object[]
				{
					text,
					var.Id
				});
				text = ",";
			}
			return stringBuilder.ToString();
		}
	}
}
