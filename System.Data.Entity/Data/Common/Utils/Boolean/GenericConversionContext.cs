using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003A1 RID: 929
	internal sealed class GenericConversionContext<T_Identifier> : ConversionContext<T_Identifier>
	{
		// Token: 0x06003351 RID: 13137 RVA: 0x000C7B84 File Offset: 0x000C5D84
		internal override Vertex TranslateTermToVertex(TermExpr<T_Identifier> term)
		{
			int num;
			if (!this._variableMap.TryGetValue(term, out num))
			{
				num = this.Solver.CreateVariable();
				this._variableMap.Add(term, num);
			}
			return this.Solver.CreateLeafVertex(num, Solver.BooleanVariableChildren);
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000C7BCC File Offset: 0x000C5DCC
		internal override IEnumerable<LiteralVertexPair<T_Identifier>> GetSuccessors(Vertex vertex)
		{
			LiteralVertexPair<T_Identifier>[] array = new LiteralVertexPair<T_Identifier>[2];
			Vertex vertex2 = vertex.Children[0];
			Vertex vertex3 = vertex.Children[1];
			this.InitializeInverseVariableMap();
			TermExpr<T_Identifier> term = this._inverseVariableMap[vertex.Variable];
			Literal<T_Identifier> literal = new Literal<T_Identifier>(term, true);
			array[0] = new LiteralVertexPair<T_Identifier>(vertex2, literal);
			literal = literal.MakeNegated();
			array[1] = new LiteralVertexPair<T_Identifier>(vertex3, literal);
			return array;
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x000C7C34 File Offset: 0x000C5E34
		private void InitializeInverseVariableMap()
		{
			if (this._inverseVariableMap == null)
			{
				this._inverseVariableMap = this._variableMap.ToDictionary((KeyValuePair<TermExpr<T_Identifier>, int> kvp) => kvp.Value, (KeyValuePair<TermExpr<T_Identifier>, int> kvp) => kvp.Key);
			}
		}

		// Token: 0x04001679 RID: 5753
		private readonly Dictionary<TermExpr<T_Identifier>, int> _variableMap = new Dictionary<TermExpr<T_Identifier>, int>();

		// Token: 0x0400167A RID: 5754
		private Dictionary<int, TermExpr<T_Identifier>> _inverseVariableMap;
	}
}
