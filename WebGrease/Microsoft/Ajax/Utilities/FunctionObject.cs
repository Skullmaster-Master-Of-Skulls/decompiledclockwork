using System;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000092 RID: 146
	public class FunctionObject : AstNode
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00028EC7 File Offset: 0x000270C7
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00028ECF File Offset: 0x000270CF
		public bool IsStatic { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00028ED8 File Offset: 0x000270D8
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x00028EE0 File Offset: 0x000270E0
		public Context StaticContext { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00028EE9 File Offset: 0x000270E9
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00028F33 File Offset: 0x00027133
		public BindingIdentifier Binding
		{
			get
			{
				return this.m_binding;
			}
			set
			{
				this.m_binding.IfNotNull((BindingIdentifier n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_binding = value;
				this.m_binding.IfNotNull(delegate(BindingIdentifier n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00028F6C File Offset: 0x0002716C
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00028F74 File Offset: 0x00027174
		public string NameGuess { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00028F7D File Offset: 0x0002717D
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x00028FC7 File Offset: 0x000271C7
		public AstNodeList ParameterDeclarations
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters.IfNotNull((AstNodeList n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_parameters = value;
				this.m_parameters.IfNotNull(delegate(AstNodeList n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00029000 File Offset: 0x00027200
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x00029047 File Offset: 0x00027247
		public Block Body
		{
			get
			{
				return this.m_body;
			}
			set
			{
				this.m_body.IfNotNull((Block n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_body = value;
				this.m_body.IfNotNull(delegate(Block n)
				{
					n.Parent = this;
					return this;
				});
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00029080 File Offset: 0x00027280
		public override bool IsDeclaration
		{
			get
			{
				return this.FunctionType == FunctionType.Declaration;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0002908B File Offset: 0x0002728B
		// (set) Token: 0x060008EF RID: 2287 RVA: 0x00029093 File Offset: 0x00027293
		public FunctionType FunctionType { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x0002909C File Offset: 0x0002729C
		public override bool IsExpression
		{
			get
			{
				return this.FunctionType != FunctionType.Declaration && this.FunctionType != FunctionType.Method;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x000290B4 File Offset: 0x000272B4
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x000290BC File Offset: 0x000272BC
		public bool IsGenerator { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x000290C5 File Offset: 0x000272C5
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x000290CD File Offset: 0x000272CD
		public bool IsSourceElement { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x000290D6 File Offset: 0x000272D6
		public override OperatorPrecedence Precedence
		{
			get
			{
				if (this.FunctionType != FunctionType.ArrowFunction)
				{
					return OperatorPrecedence.Primary;
				}
				return OperatorPrecedence.Assignment;
			}
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000290E5 File Offset: 0x000272E5
		public FunctionObject(Context functionContext) : base(functionContext)
		{
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x000290EE File Offset: 0x000272EE
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x000290FA File Offset: 0x000272FA
		public bool IsReferenced
		{
			get
			{
				return this.SafeIsReferenced(new HashSet<FunctionObject>());
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002911C File Offset: 0x0002731C
		private bool SafeIsReferenced(HashSet<FunctionObject> visited)
		{
			if (!visited.Contains(this))
			{
				visited.Add(this);
				if (this.FunctionType == FunctionType.Declaration)
				{
					if (this.Binding.VariableField.IfNotNull((JSVariableField v) => v.FieldType == FieldType.Global || v.IsExported))
					{
						return true;
					}
					using (IEnumerator<INameReference> enumerator = this.Binding.VariableField.References.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							INameReference nameReference = enumerator.Current;
							ActivationObject variableScope = nameReference.VariableScope;
							if (variableScope == null || variableScope is GlobalScope)
							{
								return true;
							}
							FunctionObject functionObject = variableScope.Owner as FunctionObject;
							if (functionObject != null && functionObject.SafeIsReferenced(visited))
							{
								return true;
							}
						}
						return false;
					}
					return true;
				}
				return true;
			}
			return false;
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x000291F8 File Offset: 0x000273F8
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes(this.Binding, this.ParameterDeclarations, this.Body, null);
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002921C File Offset: 0x0002741C
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			if (this.Binding == oldNode)
			{
				this.Binding = (newNode as BindingIdentifier);
				return true;
			}
			if (this.Body == oldNode)
			{
				this.Body = AstNode.ForceToBlock(newNode);
				return true;
			}
			if (this.ParameterDeclarations == oldNode)
			{
				return (newNode as AstNodeList).IfNotNull(delegate(AstNodeList list)
				{
					this.ParameterDeclarations = list;
					return true;
				});
			}
			return false;
		}

		// Token: 0x0400032C RID: 812
		private BindingIdentifier m_binding;

		// Token: 0x0400032D RID: 813
		private AstNodeList m_parameters;

		// Token: 0x0400032E RID: 814
		private Block m_body;
	}
}
