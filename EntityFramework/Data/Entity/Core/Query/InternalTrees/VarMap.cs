using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000648 RID: 1608
	internal class VarMap : Dictionary<Var, Var>
	{
		// Token: 0x06003EFC RID: 16124 RVA: 0x001204FC File Offset: 0x0011E6FC
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

		// Token: 0x06003EFD RID: 16125 RVA: 0x00120570 File Offset: 0x0011E770
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
	}
}
