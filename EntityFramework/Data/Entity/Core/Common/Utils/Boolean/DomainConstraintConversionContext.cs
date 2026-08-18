using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200030A RID: 778
	internal sealed class DomainConstraintConversionContext<T_Variable, T_Element> : ConversionContext<DomainConstraint<T_Variable, T_Element>>
	{
		// Token: 0x06001B1A RID: 6938 RVA: 0x00086E50 File Offset: 0x00085050
		internal override Vertex TranslateTermToVertex(TermExpr<DomainConstraint<T_Variable, T_Element>> term)
		{
			Set<T_Element> range = term.Identifier.Range;
			DomainVariable<T_Variable, T_Element> variable = term.Identifier.Variable;
			Set<T_Element> domain = variable.Domain;
			if (range.All((T_Element element) => !domain.Contains(element)))
			{
				return Vertex.Zero;
			}
			if (domain.All((T_Element element) => range.Contains(element)))
			{
				return Vertex.One;
			}
			Vertex[] children = domain.Select(delegate(T_Element element)
			{
				if (!range.Contains(element))
				{
					return Vertex.Zero;
				}
				return Vertex.One;
			}).ToArray<Vertex>();
			int num;
			if (!this._domainVariableToRobddVariableMap.TryGetValue(variable, out num))
			{
				num = this.Solver.CreateVariable();
				this._domainVariableToRobddVariableMap[variable] = num;
			}
			return this.Solver.CreateLeafVertex(num, children);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x000871E4 File Offset: 0x000853E4
		internal override IEnumerable<LiteralVertexPair<DomainConstraint<T_Variable, T_Element>>> GetSuccessors(Vertex vertex)
		{
			this.InitializeInverseMap();
			DomainVariable<T_Variable, T_Element> domainVariable = this._inverseMap[vertex.Variable];
			T_Element[] domain = domainVariable.Domain.ToArray();
			Dictionary<Vertex, Set<T_Element>> vertexToRange = new Dictionary<Vertex, Set<T_Element>>();
			for (int i = 0; i < vertex.Children.Length; i++)
			{
				Vertex key = vertex.Children[i];
				Set<T_Element> set;
				if (!vertexToRange.TryGetValue(key, out set))
				{
					set = new Set<T_Element>(domainVariable.Domain.Comparer);
					vertexToRange.Add(key, set);
				}
				set.Add(domain[i]);
			}
			foreach (KeyValuePair<Vertex, Set<T_Element>> vertexRange in vertexToRange)
			{
				KeyValuePair<Vertex, Set<T_Element>> keyValuePair = vertexRange;
				Vertex successorVertex = keyValuePair.Key;
				KeyValuePair<Vertex, Set<T_Element>> keyValuePair2 = vertexRange;
				Set<T_Element> range = keyValuePair2.Value;
				DomainConstraint<T_Variable, T_Element> constraint = new DomainConstraint<T_Variable, T_Element>(domainVariable, range.MakeReadOnly());
				Literal<DomainConstraint<T_Variable, T_Element>> literal = new Literal<DomainConstraint<T_Variable, T_Element>>(new TermExpr<DomainConstraint<T_Variable, T_Element>>(constraint), true);
				yield return new LiteralVertexPair<DomainConstraint<T_Variable, T_Element>>(successorVertex, literal);
			}
			yield break;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0008721C File Offset: 0x0008541C
		private void InitializeInverseMap()
		{
			if (this._inverseMap == null)
			{
				this._inverseMap = this._domainVariableToRobddVariableMap.ToDictionary((KeyValuePair<DomainVariable<T_Variable, T_Element>, int> kvp) => kvp.Value, (KeyValuePair<DomainVariable<T_Variable, T_Element>, int> kvp) => kvp.Key);
			}
		}

		// Token: 0x04000984 RID: 2436
		private readonly Dictionary<DomainVariable<T_Variable, T_Element>, int> _domainVariableToRobddVariableMap = new Dictionary<DomainVariable<T_Variable, T_Element>, int>();

		// Token: 0x04000985 RID: 2437
		private Dictionary<int, DomainVariable<T_Variable, T_Element>> _inverseMap;
	}
}
