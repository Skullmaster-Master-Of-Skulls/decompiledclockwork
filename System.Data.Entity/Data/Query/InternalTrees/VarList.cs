using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000113 RID: 275
	[DebuggerDisplay("{{{ToString()}}}")]
	internal class VarList : List<Var>
	{
		// Token: 0x06000DC3 RID: 3523 RVA: 0x0003D62D File Offset: 0x0003B82D
		internal VarList()
		{
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0003D635 File Offset: 0x0003B835
		internal VarList(IEnumerable<Var> vars) : base(vars)
		{
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0003D640 File Offset: 0x0003B840
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
