using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000008 RID: 8
	public class BindingIdentifier : AstNode, INameDeclaration, IRenameable
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002813 File Offset: 0x00000A13
		// (set) Token: 0x06000041 RID: 65 RVA: 0x0000281B File Offset: 0x00000A1B
		public string Name { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002824 File Offset: 0x00000A24
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000282C File Offset: 0x00000A2C
		public JSVariableField VariableField { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002835 File Offset: 0x00000A35
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000283D File Offset: 0x00000A3D
		public bool RenameNotAllowed { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002846 File Offset: 0x00000A46
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000284E File Offset: 0x00000A4E
		public ScopeType ScopeType { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000285F File Offset: 0x00000A5F
		public AstNode Initializer
		{
			get
			{
				return (base.Parent as InitializerNode).IfNotNull((InitializerNode v) => v.Initializer);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000288E File Offset: 0x00000A8E
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002896 File Offset: 0x00000A96
		public bool IsParameter { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000289F File Offset: 0x00000A9F
		public string OriginalName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000028B7 File Offset: 0x00000AB7
		public bool WasRenamed
		{
			get
			{
				return this.VariableField.IfNotNull((JSVariableField f) => !f.CrunchedName.IsNullOrWhiteSpace());
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000028E1 File Offset: 0x00000AE1
		public BindingIdentifier(Context context) : base(context)
		{
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000028EA File Offset: 0x00000AEA
		public override void Accept(IVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000290C File Offset: 0x00000B0C
		public override bool IsEquivalentTo(AstNode otherNode)
		{
			BindingIdentifier bindingIdentifier = otherNode as BindingIdentifier;
			if (bindingIdentifier != null)
			{
				return bindingIdentifier.VariableField.IfNotNull((JSVariableField v) => v == this.VariableField);
			}
			Lookup lookup = otherNode as Lookup;
			if (lookup != null)
			{
				return lookup.VariableField.IfNotNull((JSVariableField v) => v == this.VariableField);
			}
			return false;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000296C File Offset: 0x00000B6C
		public override string ToString()
		{
			return this.Name;
		}
	}
}
