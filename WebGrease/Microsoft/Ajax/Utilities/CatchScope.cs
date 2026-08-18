using System;
using System.Reflection;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200006C RID: 108
	public sealed class CatchScope : BlockScope
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x00022485 File Offset: 0x00020685
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x0002248D File Offset: 0x0002068D
		public ParameterDeclaration CatchParameter { get; set; }

		// Token: 0x06000713 RID: 1811 RVA: 0x00022496 File Offset: 0x00020696
		internal CatchScope(ActivationObject parent, CodeSettings settings) : base(parent, settings, ScopeType.Catch)
		{
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000224A1 File Offset: 0x000206A1
		public override JSVariableField CreateField(string name, object value, FieldAttributes attributes)
		{
			return new JSVariableField(FieldType.Local, name, attributes, value);
		}
	}
}
