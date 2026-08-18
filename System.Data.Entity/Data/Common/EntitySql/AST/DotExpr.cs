using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200036E RID: 878
	internal sealed class DotExpr : Node
	{
		// Token: 0x06003217 RID: 12823 RVA: 0x000C4C45 File Offset: 0x000C2E45
		internal DotExpr(Node leftExpr, Identifier id)
		{
			this._leftExpr = leftExpr;
			this._identifier = id;
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06003218 RID: 12824 RVA: 0x000C4C5B File Offset: 0x000C2E5B
		internal Node Left
		{
			get
			{
				return this._leftExpr;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x000C4C63 File Offset: 0x000C2E63
		internal Identifier Identifier
		{
			get
			{
				return this._identifier;
			}
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x000C4C6C File Offset: 0x000C2E6C
		internal bool IsMultipartIdentifier(out string[] names)
		{
			if (this._isMultipartIdentifierComputed != null)
			{
				names = this._names;
				return this._isMultipartIdentifierComputed.Value;
			}
			this._names = null;
			Identifier identifier = this._leftExpr as Identifier;
			if (identifier != null)
			{
				this._names = new string[]
				{
					identifier.Name,
					this._identifier.Name
				};
			}
			DotExpr dotExpr = this._leftExpr as DotExpr;
			string[] array;
			if (dotExpr != null && dotExpr.IsMultipartIdentifier(out array))
			{
				this._names = new string[array.Length + 1];
				array.CopyTo(this._names, 0);
				this._names[this._names.Length - 1] = this._identifier.Name;
			}
			this._isMultipartIdentifierComputed = new bool?(this._names != null);
			names = this._names;
			return this._isMultipartIdentifierComputed.Value;
		}

		// Token: 0x040015FC RID: 5628
		private readonly Node _leftExpr;

		// Token: 0x040015FD RID: 5629
		private readonly Identifier _identifier;

		// Token: 0x040015FE RID: 5630
		private bool? _isMultipartIdentifierComputed;

		// Token: 0x040015FF RID: 5631
		private string[] _names;
	}
}
