using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000B4 RID: 180
	public sealed class Lookup : Expression, INameReference, IRenameable
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x00037415 File Offset: 0x00035615
		// (set) Token: 0x06000B84 RID: 2948 RVA: 0x0003741D File Offset: 0x0003561D
		public JSVariableField VariableField { get; set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x00037426 File Offset: 0x00035626
		// (set) Token: 0x06000B86 RID: 2950 RVA: 0x0003742E File Offset: 0x0003562E
		public bool IsGenerated { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x00037437 File Offset: 0x00035637
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x0003743F File Offset: 0x0003563F
		public ReferenceType RefType { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x00037448 File Offset: 0x00035648
		// (set) Token: 0x06000B8A RID: 2954 RVA: 0x00037450 File Offset: 0x00035650
		public string Name { get; set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0003745C File Offset: 0x0003565C
		public bool IsAssignment
		{
			get
			{
				BinaryOperator binaryOperator = base.Parent as BinaryOperator;
				bool flag;
				if (binaryOperator != null)
				{
					flag = (binaryOperator.IsAssign && binaryOperator.Operand1 == this);
				}
				else
				{
					UnaryOperator unaryOperator = base.Parent as UnaryOperator;
					flag = (unaryOperator != null && (unaryOperator.OperatorToken == JSToken.Increment || unaryOperator.OperatorToken == JSToken.Decrement));
					if (!flag)
					{
						ForIn forIn = base.Parent as ForIn;
						flag = (forIn != null && this == forIn.Variable);
					}
				}
				return flag;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x000374DC File Offset: 0x000356DC
		public AstNode AssignmentValue
		{
			get
			{
				AstNode result = null;
				BinaryOperator binaryOperator = base.Parent as BinaryOperator;
				if (binaryOperator != null)
				{
					result = ((binaryOperator.OperatorToken == JSToken.Assign && binaryOperator.Operand1 == this) ? binaryOperator.Operand2 : null);
				}
				return result;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x00037518 File Offset: 0x00035718
		public string OriginalName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x00037530 File Offset: 0x00035730
		public bool WasRenamed
		{
			get
			{
				return this.VariableField.IfNotNull((JSVariableField f) => !f.CrunchedName.IsNullOrWhiteSpace());
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0003755A File Offset: 0x0003575A
		public Lookup(Context context) : base(context)
		{
			this.RefType = ReferenceType.Variable;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0003756A File Offset: 0x0003576A
		public override void Accept(IVisitor visitor)
		{
			if (visitor == null)
			{
				return;
			}
			visitor.Visit(this);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00037578 File Offset: 0x00035778
		public override bool IsEquivalentTo(AstNode otherNode)
		{
			Lookup lookup = otherNode as Lookup;
			if (lookup == null)
			{
				return false;
			}
			if (this.VariableField != null)
			{
				return this.VariableField.IsSameField(lookup.VariableField);
			}
			return string.CompareOrdinal(this.Name, lookup.Name) == 0;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000375BF File Offset: 0x000357BF
		internal override string GetFunctionGuess(AstNode target)
		{
			return this.Name;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000375C7 File Offset: 0x000357C7
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x000375D0 File Offset: 0x000357D0
		public ActivationObject VariableScope
		{
			get
			{
				ActivationObject activationObject = this.EnclosingScope;
				while (activationObject is BlockScope)
				{
					activationObject = activationObject.Parent;
				}
				return activationObject;
			}
		}
	}
}
