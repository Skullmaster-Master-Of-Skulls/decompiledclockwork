using System;

namespace System.Web
{
	// Token: 0x020000BB RID: 187
	internal class HttpServerVarsCollectionEntry
	{
		// Token: 0x06000D1D RID: 3357 RVA: 0x00024C7F File Offset: 0x00022E7F
		internal HttpServerVarsCollectionEntry(string name, string value)
		{
			this.Name = name;
			this.Value = value;
			this.IsDynamic = false;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00024C9C File Offset: 0x00022E9C
		internal HttpServerVarsCollectionEntry(string name, DynamicServerVariable var)
		{
			this.Name = name;
			this.Var = var;
			this.IsDynamic = true;
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00024CBC File Offset: 0x00022EBC
		internal string GetValue(HttpRequest request)
		{
			string result = null;
			if (this.IsDynamic)
			{
				if (request != null)
				{
					result = request.CalcDynamicServerVariable(this.Var);
				}
			}
			else
			{
				result = this.Value;
			}
			return result;
		}

		// Token: 0x040004DF RID: 1247
		internal readonly string Name;

		// Token: 0x040004E0 RID: 1248
		internal readonly bool IsDynamic;

		// Token: 0x040004E1 RID: 1249
		internal readonly string Value;

		// Token: 0x040004E2 RID: 1250
		internal readonly DynamicServerVariable Var;
	}
}
