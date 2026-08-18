using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003A2 RID: 930
	internal sealed class DomainConstraintConversionContext<T_Variable, T_Element> : ConversionContext<DomainConstraint<T_Variable, T_Element>>
	{
		// Token: 0x06003355 RID: 13141 RVA: 0x000C7CAC File Offset: 0x000C5EAC
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

		// Token: 0x06003356 RID: 13142 RVA: 0x000C7D79 File Offset: 0x000C5F79
		internal override IEnumerable<LiteralVertexPair<DomainConstraint<T_Variable, T_Element>>> GetSuccessors(Vertex vertex)
		{
			this.InitializeInverseMap();
			DomainVariable<T_Variable, T_Element> domainVariable = this._inverseMap[vertex.Variable];
			T_Element[] array = domainVariable.Domain.ToArray();
			Dictionary<Vertex, Set<T_Element>> dictionary = new Dictionary<Vertex, Set<T_Element>>();
			for (int i = 0; i < vertex.Children.Length; i++)
			{
				Vertex key = vertex.Children[i];
				Set<T_Element> set;
				if (!dictionary.TryGetValue(key, out set))
				{
					set = new Set<T_Element>(domainVariable.Domain.Comparer);
					dictionary.Add(key, set);
				}
				set.Add(array[i]);
			}
			foreach (KeyValuePair<Vertex, Set<T_Element>> keyValuePair in dictionary)
			{
				Vertex key2 = keyValuePair.Key;
				Set<T_Element> value = keyValuePair.Value;
				DomainConstraint<T_Variable, T_Element> identifier = new DomainConstraint<T_Variable, T_Element>(domainVariable, value.MakeReadOnly());
				Literal<DomainConstraint<T_Variable, T_Element>> literal = new Literal<DomainConstraint<T_Variable, T_Element>>(new TermExpr<DomainConstraint<T_Variable, T_Element>>(identifier), true);
				yield return new LiteralVertexPair<DomainConstraint<T_Variable, T_Element>>(key2, literal);
			}
			Dictionary<Vertex, Set<T_Element>>.Enumerator enumerator = default(Dictionary<Vertex, Set<T_Element>>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x000C7D90 File Offset: 0x000C5F90
		private void InitializeInverseMap()
		{
			if (this._inverseMap == null)
			{
				this._inverseMap = this._domainVariableToRobddVariableMap.ToDictionary((KeyValuePair<DomainVariable<T_Variable, T_Element>, int> kvp) => kvp.Value, (KeyValuePair<DomainVariable<T_Variable, T_Element>, int> kvp) => kvp.Key);
			}
		}

		// Token: 0x0400167B RID: 5755
		private readonly Dictionary<DomainVariable<T_Variable, T_Element>, int> _domainVariableToRobddVariableMap = new Dictionary<DomainVariable<T_Variable, T_Element>, int>();

		// Token: 0x0400167C RID: 5756
		private Dictionary<int, DomainVariable<T_Variable, T_Element>> _inverseMap;
	}
}
