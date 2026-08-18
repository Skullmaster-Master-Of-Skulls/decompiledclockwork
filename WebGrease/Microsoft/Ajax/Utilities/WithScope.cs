using System;
using System.Reflection;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000D1 RID: 209
	public sealed class WithScope : BlockScope
	{
		// Token: 0x06000E12 RID: 3602 RVA: 0x00041DEE File Offset: 0x0003FFEE
		public WithScope(ActivationObject parent, CodeSettings settings) : base(parent, settings, ScopeType.With)
		{
			base.IsInWithScope = true;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00041E7C File Offset: 0x0004007C
		public override JSVariableField CreateInnerField(JSVariableField outerField)
		{
			return outerField.IfNotNull(delegate(JSVariableField o)
			{
				JSVariableField result = this.AddField(this.CreateField(outerField));
				outerField.CanCrunch = false;
				if (outerField.FieldType == FieldType.UndefinedGlobal)
				{
					do
					{
						outerField.Attributes |= FieldAttributes.RTSpecialName;
					}
					while ((outerField = outerField.OuterField) != null);
				}
				return result;
			});
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00041EB4 File Offset: 0x000400B4
		public override void DeclareScope()
		{
			base.DefineLexicalDeclarations();
			foreach (INameDeclaration nameDeclaration in base.VarDeclaredNames)
			{
				if (nameDeclaration.VariableField != null)
				{
					nameDeclaration.VariableField.CanCrunch = false;
				}
			}
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00041F14 File Offset: 0x00040114
		public override JSVariableField CreateField(JSVariableField outerField)
		{
			return new JSVariableField(FieldType.WithField, outerField);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00041F1D File Offset: 0x0004011D
		public override JSVariableField CreateField(string name, object value, FieldAttributes attributes)
		{
			return new JSVariableField(FieldType.WithField, name, attributes, null);
		}
	}
}
