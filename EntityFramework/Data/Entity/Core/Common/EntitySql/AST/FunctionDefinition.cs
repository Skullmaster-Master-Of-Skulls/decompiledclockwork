using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000223 RID: 547
	internal sealed class FunctionDefinition : Node
	{
		// Token: 0x0600137F RID: 4991 RVA: 0x000506DA File Offset: 0x0004E8DA
		internal FunctionDefinition(Identifier name, NodeList<PropDefinition> argDefList, Node body, int startPosition, int endPosition)
		{
			this._name = name;
			this._paramDefList = argDefList;
			this._body = body;
			this._startPosition = startPosition;
			this._endPosition = endPosition;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x00050707 File Offset: 0x0004E907
		internal string Name
		{
			get
			{
				return this._name.Name;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x00050714 File Offset: 0x0004E914
		internal NodeList<PropDefinition> Parameters
		{
			get
			{
				return this._paramDefList;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x0005071C File Offset: 0x0004E91C
		internal Node Body
		{
			get
			{
				return this._body;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x00050724 File Offset: 0x0004E924
		internal int StartPosition
		{
			get
			{
				return this._startPosition;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x0005072C File Offset: 0x0004E92C
		internal int EndPosition
		{
			get
			{
				return this._endPosition;
			}
		}

		// Token: 0x040005EE RID: 1518
		private readonly Identifier _name;

		// Token: 0x040005EF RID: 1519
		private readonly NodeList<PropDefinition> _paramDefList;

		// Token: 0x040005F0 RID: 1520
		private readonly Node _body;

		// Token: 0x040005F1 RID: 1521
		private readonly int _startPosition;

		// Token: 0x040005F2 RID: 1522
		private readonly int _endPosition;
	}
}
