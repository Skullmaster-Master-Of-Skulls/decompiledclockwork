using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000114 RID: 276
	internal class VarMap : Dictionary<Var, Var>
	{
		// Token: 0x06000DC6 RID: 3526 RVA: 0x0003D6C8 File Offset: 0x0003B8C8
		internal VarMap GetReverseMap()
		{
			VarMap varMap = new VarMap();
			foreach (KeyValuePair<Var, Var> keyValuePair in this)
			{
				Var var;
				if (!varMap.TryGetValue(keyValuePair.Value, out var))
				{
					varMap[keyValuePair.Value] = keyValuePair.Key;
				}
			}
			return varMap;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0003D73C File Offset: 0x0003B93C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			foreach (Var var in base.Keys)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}({1},{2})", new object[]
				{
					text,
					var.Id,
					base[var].Id
				});
				text = ",";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0003D7E0 File Offset: 0x0003B9E0
		internal VarMap()
		{
		}
	}
}
