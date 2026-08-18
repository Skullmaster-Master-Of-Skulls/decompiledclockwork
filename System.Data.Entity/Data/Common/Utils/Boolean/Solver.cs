using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003A7 RID: 935
	internal sealed class Solver
	{
		// Token: 0x06003374 RID: 13172 RVA: 0x000C82D4 File Offset: 0x000C64D4
		internal int CreateVariable()
		{
			int num = this._variableCount + 1;
			this._variableCount = num;
			return num;
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000C82F2 File Offset: 0x000C64F2
		internal Vertex Not(Vertex vertex)
		{
			return this.IfThenElse(vertex, Vertex.Zero, Vertex.One);
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x000C8305 File Offset: 0x000C6505
		internal Vertex And(IEnumerable<Vertex> children)
		{
			return (from child in children
			orderby child.Variable descending
			select child).Aggregate(Vertex.One, (Vertex left, Vertex right) => this.IfThenElse(left, right, Vertex.Zero));
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000C8342 File Offset: 0x000C6542
		internal Vertex And(Vertex left, Vertex right)
		{
			return this.IfThenElse(left, right, Vertex.Zero);
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000C8351 File Offset: 0x000C6551
		internal Vertex Or(IEnumerable<Vertex> children)
		{
			return (from child in children
			orderby child.Variable descending
			select child).Aggregate(Vertex.Zero, (Vertex left, Vertex right) => this.IfThenElse(left, Vertex.One, right));
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x000C838E File Offset: 0x000C658E
		internal Vertex CreateLeafVertex(int variable, Vertex[] children)
		{
			return this.GetUniqueVertex(variable, children);
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x000C8398 File Offset: 0x000C6598
		private Vertex GetUniqueVertex(int variable, Vertex[] children)
		{
			Vertex vertex = new Vertex(variable, children);
			Vertex result;
			if (this._knownVertices.TryGetValue(vertex, out result))
			{
				return result;
			}
			this._knownVertices.Add(vertex, vertex);
			return vertex;
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000C83D0 File Offset: 0x000C65D0
		private Vertex IfThenElse(Vertex condition, Vertex then, Vertex @else)
		{
			if (condition.IsOne())
			{
				return then;
			}
			if (condition.IsZero())
			{
				return @else;
			}
			if (then.IsOne() && @else.IsZero())
			{
				return condition;
			}
			if (then.Equals(@else))
			{
				return then;
			}
			Triple<Vertex, Vertex, Vertex> key = new Triple<Vertex, Vertex, Vertex>(condition, then, @else);
			Vertex uniqueVertex;
			if (this._computedIfThenElseValues.TryGetValue(key, out uniqueVertex))
			{
				return uniqueVertex;
			}
			int num;
			int variable = Solver.DetermineTopVariable(condition, then, @else, out num);
			Vertex[] array = new Vertex[num];
			bool flag = true;
			for (int i = 0; i < num; i++)
			{
				array[i] = this.IfThenElse(Solver.EvaluateFor(condition, variable, i), Solver.EvaluateFor(then, variable, i), Solver.EvaluateFor(@else, variable, i));
				if (i > 0 && flag && !array[i].Equals(array[0]))
				{
					flag = false;
				}
			}
			if (flag)
			{
				return array[0];
			}
			uniqueVertex = this.GetUniqueVertex(variable, array);
			this._computedIfThenElseValues.Add(key, uniqueVertex);
			return uniqueVertex;
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000C84B4 File Offset: 0x000C66B4
		private static int DetermineTopVariable(Vertex condition, Vertex then, Vertex @else, out int topVariableDomainCount)
		{
			int variable;
			if (condition.Variable < then.Variable)
			{
				variable = condition.Variable;
				topVariableDomainCount = condition.Children.Length;
			}
			else
			{
				variable = then.Variable;
				topVariableDomainCount = then.Children.Length;
			}
			if (@else.Variable < variable)
			{
				variable = @else.Variable;
				topVariableDomainCount = @else.Children.Length;
			}
			return variable;
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000C850E File Offset: 0x000C670E
		private static Vertex EvaluateFor(Vertex vertex, int variable, int variableAssigment)
		{
			if (variable < vertex.Variable)
			{
				return vertex;
			}
			return vertex.Children[variableAssigment];
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000C8524 File Offset: 0x000C6724
		[Conditional("DEBUG")]
		private void AssertVerticesValid(IEnumerable<Vertex> vertices)
		{
			foreach (Vertex vertex in vertices)
			{
			}
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x000C8568 File Offset: 0x000C6768
		[Conditional("DEBUG")]
		private void AssertVertexValid(Vertex vertex)
		{
			vertex.IsSink();
		}

		// Token: 0x04001689 RID: 5769
		private readonly Dictionary<Triple<Vertex, Vertex, Vertex>, Vertex> _computedIfThenElseValues = new Dictionary<Triple<Vertex, Vertex, Vertex>, Vertex>();

		// Token: 0x0400168A RID: 5770
		private readonly Dictionary<Vertex, Vertex> _knownVertices = new Dictionary<Vertex, Vertex>(Solver.VertexValueComparer.Instance);

		// Token: 0x0400168B RID: 5771
		private int _variableCount;

		// Token: 0x0400168C RID: 5772
		internal static readonly Vertex[] BooleanVariableChildren = new Vertex[]
		{
			Vertex.One,
			Vertex.Zero
		};

		// Token: 0x02000686 RID: 1670
		private class VertexValueComparer : IEqualityComparer<Vertex>
		{
			// Token: 0x060044FF RID: 17663 RVA: 0x00002050 File Offset: 0x00000250
			private VertexValueComparer()
			{
			}

			// Token: 0x06004500 RID: 17664 RVA: 0x000F8D1C File Offset: 0x000F6F1C
			public bool Equals(Vertex x, Vertex y)
			{
				if (x.IsSink())
				{
					return x.Equals(y);
				}
				if (x.Variable != y.Variable || x.Children.Length != y.Children.Length)
				{
					return false;
				}
				for (int i = 0; i < x.Children.Length; i++)
				{
					if (!x.Children[i].Equals(y.Children[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06004501 RID: 17665 RVA: 0x000F8D88 File Offset: 0x000F6F88
			public int GetHashCode(Vertex vertex)
			{
				if (vertex.IsSink())
				{
					return vertex.GetHashCode();
				}
				return (vertex.Children[0].GetHashCode() << 5) + 1 + vertex.Children[1].GetHashCode();
			}

			// Token: 0x04001FD0 RID: 8144
			internal static readonly Solver.VertexValueComparer Instance = new Solver.VertexValueComparer();
		}
	}
}
