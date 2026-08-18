using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200021F RID: 543
	internal sealed class DotExpr : Node
	{
		// Token: 0x06001374 RID: 4980 RVA: 0x00050565 File Offset: 0x0004E765
		internal DotExpr(Node leftExpr, Identifier id)
		{
			this._leftExpr = leftExpr;
			this._identifier = id;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x0005057B File Offset: 0x0004E77B
		internal Node Left
		{
			get
			{
				return this._leftExpr;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x00050583 File Offset: 0x0004E783
		internal Identifier Identifier
		{
			get
			{
				return this._identifier;
			}
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0005058C File Offset: 0x0004E78C
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

		// Token: 0x040005E3 RID: 1507
		private readonly Node _leftExpr;

		// Token: 0x040005E4 RID: 1508
		private readonly Identifier _identifier;

		// Token: 0x040005E5 RID: 1509
		private bool? _isMultipartIdentifierComputed;

		// Token: 0x040005E6 RID: 1510
		private string[] _names;
	}
}
