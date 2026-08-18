using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200030E RID: 782
	internal sealed class GenericConversionContext<T_Identifier> : ConversionContext<T_Identifier>
	{
		// Token: 0x06001B2E RID: 6958 RVA: 0x000873E8 File Offset: 0x000855E8
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

		// Token: 0x06001B2F RID: 6959 RVA: 0x00087430 File Offset: 0x00085630
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

		// Token: 0x06001B30 RID: 6960 RVA: 0x000874AC File Offset: 0x000856AC
		private void InitializeInverseVariableMap()
		{
			if (this._inverseVariableMap == null)
			{
				this._inverseVariableMap = this._variableMap.ToDictionary((KeyValuePair<TermExpr<T_Identifier>, int> kvp) => kvp.Value, (KeyValuePair<TermExpr<T_Identifier>, int> kvp) => kvp.Key);
			}
		}

		// Token: 0x04000994 RID: 2452
		private readonly Dictionary<TermExpr<T_Identifier>, int> _variableMap = new Dictionary<TermExpr<T_Identifier>, int>();

		// Token: 0x04000995 RID: 2453
		private Dictionary<int, TermExpr<T_Identifier>> _inverseVariableMap;
	}
}
