using System;
using System.Reflection;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000069 RID: 105
	public class BlockScope : ActivationObject
	{
		// Token: 0x060006F2 RID: 1778 RVA: 0x000221D2 File Offset: 0x000203D2
		public BlockScope(ActivationObject parent, CodeSettings settings, ScopeType scopeType) : base(parent, settings)
		{
			base.ScopeType = scopeType;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000221E3 File Offset: 0x000203E3
		public override void DeclareScope()
		{
			base.DefineLexicalDeclarations();
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x000221EB File Offset: 0x000203EB
		public override JSVariableField CreateField(string name, object value, FieldAttributes attributes)
		{
			return new JSVariableField(FieldType.Local, name, attributes, value);
		}
	}
}
