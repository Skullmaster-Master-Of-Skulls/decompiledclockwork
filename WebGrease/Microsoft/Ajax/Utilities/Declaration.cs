using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200007D RID: 125
	public abstract class Declaration : AstNode, IEnumerable<VariableDeclaration>, IEnumerable
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x000237B4 File Offset: 0x000219B4
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x000237BC File Offset: 0x000219BC
		public JSToken StatementToken { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x000237C5 File Offset: 0x000219C5
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x000237CD File Offset: 0x000219CD
		public Context KeywordContext { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x000237D6 File Offset: 0x000219D6
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x170001D2 RID: 466
		public VariableDeclaration this[int index]
		{
			get
			{
				return this.m_list[index];
			}
			set
			{
				this.m_list[index].IfNotNull((VariableDeclaration n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				if (value != null)
				{
					this.m_list[index] = value;
					this.m_list[index].Parent = this;
					return;
				}
				this.m_list.RemoveAt(index);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00023876 File Offset: 0x00021A76
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x0002387E File Offset: 0x00021A7E
		public ActivationObject Scope { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x00023887 File Offset: 0x00021A87
		public override bool IsDeclaration
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0002388A File Offset: 0x00021A8A
		protected Declaration(Context context) : base(context)
		{
			this.m_list = new List<VariableDeclaration>();
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0002389E File Offset: 0x00021A9E
		public override IEnumerable<AstNode> Children
		{
			get
			{
				return AstNode.EnumerateNonNullNodes<VariableDeclaration>(this.m_list);
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000238FC File Offset: 0x00021AFC
		public override bool ReplaceChild(AstNode oldNode, AstNode newNode)
		{
			int i = 0;
			while (i < this.m_list.Count)
			{
				if (this.m_list[i] == oldNode)
				{
					if (newNode == null)
					{
						this.m_list[i].IfNotNull((VariableDeclaration n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
						this.m_list.RemoveAt(i);
						break;
					}
					VariableDeclaration variableDeclaration = newNode as VariableDeclaration;
					if (newNode == null || variableDeclaration != null)
					{
						this.m_list[i].IfNotNull((VariableDeclaration n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
						this.m_list[i] = variableDeclaration;
						variableDeclaration.Parent = this;
						return true;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x000239B4 File Offset: 0x00021BB4
		public void Append(AstNode element)
		{
			VariableDeclaration variableDeclaration = element as VariableDeclaration;
			if (variableDeclaration != null)
			{
				if (this.HandleDuplicates(variableDeclaration.Binding) || variableDeclaration.Initializer != null)
				{
					variableDeclaration.Parent = this;
					this.m_list.Add(variableDeclaration);
					base.UpdateWith(variableDeclaration.Context);
					return;
				}
			}
			else
			{
				Declaration declaration = element as Declaration;
				if (declaration != null)
				{
					for (int i = 0; i < declaration.m_list.Count; i++)
					{
						this.Append(declaration.m_list[i]);
					}
				}
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00023A34 File Offset: 0x00021C34
		public void InsertAt(int index, AstNode element)
		{
			VariableDeclaration variableDeclaration = element as VariableDeclaration;
			if (variableDeclaration != null)
			{
				if (this.HandleDuplicates(variableDeclaration.Binding) || variableDeclaration.Initializer != null)
				{
					variableDeclaration.Parent = this;
					this.m_list.Insert(index, variableDeclaration);
					return;
				}
			}
			else
			{
				Declaration declaration = element as Declaration;
				if (declaration != null)
				{
					for (int i = declaration.m_list.Count - 1; i >= 0; i--)
					{
						this.InsertAt(index, declaration.m_list[i]);
					}
				}
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00023AB4 File Offset: 0x00021CB4
		private bool HandleDuplicates(AstNode binding)
		{
			bool result = true;
			string text = (binding as BindingIdentifier).IfNotNull((BindingIdentifier b) => b.Name);
			if (!text.IsNullOrWhiteSpace())
			{
				for (int i = this.m_list.Count - 1; i >= 0; i--)
				{
					VariableDeclaration variableDeclaration = this.m_list[i];
					BindingIdentifier bindingIdentifier = variableDeclaration.Binding as BindingIdentifier;
					if (bindingIdentifier != null && string.CompareOrdinal(bindingIdentifier.Name, text) == 0)
					{
						if (variableDeclaration.Initializer == null)
						{
							variableDeclaration.Parent = null;
							this.m_list.RemoveAt(i);
						}
						else
						{
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00023B84 File Offset: 0x00021D84
		public void RemoveAt(int index)
		{
			if (0 <= index & index < this.m_list.Count)
			{
				this.m_list[index].IfNotNull((VariableDeclaration n) => n.Parent = ((n.Parent == this) ? null : n.Parent));
				this.m_list.RemoveAt(index);
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00023BDA File Offset: 0x00021DDA
		public void Remove(VariableDeclaration variableDeclaration)
		{
			if (variableDeclaration != null && this.m_list.Remove(variableDeclaration) && variableDeclaration.Parent == this)
			{
				variableDeclaration.Parent = null;
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00023C00 File Offset: 0x00021E00
		public bool Contains(string name)
		{
			if (!name.IsNullOrWhiteSpace())
			{
				foreach (VariableDeclaration node in this.m_list)
				{
					foreach (BindingIdentifier bindingIdentifier in BindingsVisitor.Bindings(node))
					{
						if (string.CompareOrdinal(name, bindingIdentifier.Name) == 0)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00023CA4 File Offset: 0x00021EA4
		public override bool ContainsInOperator
		{
			get
			{
				foreach (VariableDeclaration variableDeclaration in this.m_list)
				{
					if (variableDeclaration.Initializer != null && variableDeclaration.Initializer.ContainsInOperator)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00023D0C File Offset: 0x00021F0C
		public IEnumerator<VariableDeclaration> GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00023D1E File Offset: 0x00021F1E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x040002E7 RID: 743
		private List<VariableDeclaration> m_list;
	}
}
