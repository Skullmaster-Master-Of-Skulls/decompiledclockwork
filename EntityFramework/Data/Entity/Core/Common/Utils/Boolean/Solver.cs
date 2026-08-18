using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200031C RID: 796
	internal sealed class Solver
	{
		// Token: 0x06001B77 RID: 7031 RVA: 0x00087C70 File Offset: 0x00085E70
		internal int CreateVariable()
		{
			return ++this._variableCount;
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x00087C8E File Offset: 0x00085E8E
		internal Vertex Not(Vertex vertex)
		{
			return this.IfThenElse(vertex, Vertex.Zero, Vertex.One);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x00087CB8 File Offset: 0x00085EB8
		internal Vertex And(IEnumerable<Vertex> children)
		{
			return (from child in children
			orderby child.Variable descending
			select child).Aggregate(Vertex.One, (Vertex left, Vertex right) => this.IfThenElse(left, right, Vertex.Zero));
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00087CF3 File Offset: 0x00085EF3
		internal Vertex And(Vertex left, Vertex right)
		{
			return this.IfThenElse(left, right, Vertex.Zero);
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00087D19 File Offset: 0x00085F19
		internal Vertex Or(IEnumerable<Vertex> children)
		{
			return (from child in children
			orderby child.Variable descending
			select child).Aggregate(Vertex.Zero, (Vertex left, Vertex right) => this.IfThenElse(left, Vertex.One, right));
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00087D54 File Offset: 0x00085F54
		internal Vertex CreateLeafVertex(int variable, Vertex[] children)
		{
			return this.GetUniqueVertex(variable, children);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00087D60 File Offset: 0x00085F60
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

		// Token: 0x06001B7E RID: 7038 RVA: 0x00087D98 File Offset: 0x00085F98
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

		// Token: 0x06001B7F RID: 7039 RVA: 0x00087E7C File Offset: 0x0008607C
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

		// Token: 0x06001B80 RID: 7040 RVA: 0x00087ED6 File Offset: 0x000860D6
		private static Vertex EvaluateFor(Vertex vertex, int variable, int variableAssigment)
		{
			if (variable < vertex.Variable)
			{
				return vertex;
			}
			return vertex.Children[variableAssigment];
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00087EEC File Offset: 0x000860EC
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		private void AssertVerticesValid(IEnumerable<Vertex> vertices)
		{
			foreach (Vertex vertex in vertices)
			{
			}
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x00087F30 File Offset: 0x00086130
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[Conditional("DEBUG")]
		private void AssertVertexValid(Vertex vertex)
		{
			vertex.IsSink();
		}

		// Token: 0x040009A4 RID: 2468
		private readonly Dictionary<Triple<Vertex, Vertex, Vertex>, Vertex> _computedIfThenElseValues = new Dictionary<Triple<Vertex, Vertex, Vertex>, Vertex>();

		// Token: 0x040009A5 RID: 2469
		private readonly Dictionary<Vertex, Vertex> _knownVertices = new Dictionary<Vertex, Vertex>(Solver.VertexValueComparer.Instance);

		// Token: 0x040009A6 RID: 2470
		private int _variableCount;

		// Token: 0x040009A7 RID: 2471
		internal static readonly Vertex[] BooleanVariableChildren = new Vertex[]
		{
			Vertex.One,
			Vertex.Zero
		};

		// Token: 0x0200031D RID: 797
		private class VertexValueComparer : IEqualityComparer<Vertex>
		{
			// Token: 0x06001B89 RID: 7049 RVA: 0x00087F89 File Offset: 0x00086189
			private VertexValueComparer()
			{
			}

			// Token: 0x06001B8A RID: 7050 RVA: 0x00087F94 File Offset: 0x00086194
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

			// Token: 0x06001B8B RID: 7051 RVA: 0x00088000 File Offset: 0x00086200
			public int GetHashCode(Vertex vertex)
			{
				if (vertex.IsSink())
				{
					return vertex.GetHashCode();
				}
				return (vertex.Children[0].GetHashCode() << 5) + 1 + vertex.Children[1].GetHashCode();
			}

			// Token: 0x040009AA RID: 2474
			internal static readonly Solver.VertexValueComparer Instance = new Solver.VertexValueComparer();
		}
	}
}
