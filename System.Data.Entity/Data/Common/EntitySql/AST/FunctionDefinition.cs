using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200036F RID: 879
	internal sealed class FunctionDefinition : Node
	{
		// Token: 0x0600321B RID: 12827 RVA: 0x000C4D4C File Offset: 0x000C2F4C
		internal FunctionDefinition(Identifier name, NodeList<PropDefinition> argDefList, Node body, int startPosition, int endPosition)
		{
			this._name = name;
			this._paramDefList = argDefList;
			this._body = body;
			this._startPosition = startPosition;
			this._endPosition = endPosition;
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x000C4D79 File Offset: 0x000C2F79
		internal string Name
		{
			get
			{
				return this._name.Name;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x0600321D RID: 12829 RVA: 0x000C4D86 File Offset: 0x000C2F86
		internal NodeList<PropDefinition> Parameters
		{
			get
			{
				return this._paramDefList;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x000C4D8E File Offset: 0x000C2F8E
		internal Node Body
		{
			get
			{
				return this._body;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x0600321F RID: 12831 RVA: 0x000C4D96 File Offset: 0x000C2F96
		internal int StartPosition
		{
			get
			{
				return this._startPosition;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x000C4D9E File Offset: 0x000C2F9E
		internal int EndPosition
		{
			get
			{
				return this._endPosition;
			}
		}

		// Token: 0x04001600 RID: 5632
		private readonly Identifier _name;

		// Token: 0x04001601 RID: 5633
		private readonly NodeList<PropDefinition> _paramDefList;

		// Token: 0x04001602 RID: 5634
		private readonly Node _body;

		// Token: 0x04001603 RID: 5635
		private readonly int _startPosition;

		// Token: 0x04001604 RID: 5636
		private readonly int _endPosition;
	}
}
