using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x0200001D RID: 29
	public class ModuleScope : ActivationObject
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000626F File Offset: 0x0000446F
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00006277 File Offset: 0x00004477
		public bool HasDefaultExport { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00006280 File Offset: 0x00004480
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00006288 File Offset: 0x00004488
		public bool IsNotComplete { get; set; }

		// Token: 0x06000219 RID: 537 RVA: 0x00006291 File Offset: 0x00004491
		public ModuleScope(ModuleDeclaration module, ActivationObject parent, CodeSettings settings) : base(parent, settings)
		{
			base.Owner = module;
			base.UseStrict = true;
			base.ScopeType = ScopeType.Module;
			this.m_knownExports = new Dictionary<string, JSVariableField>();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000062BC File Offset: 0x000044BC
		public override void DeclareScope()
		{
			base.DefineLexicalDeclarations();
			base.DefineVarDeclarations();
			foreach (JSVariableField jsvariableField in base.NameTable.Values)
			{
				if (jsvariableField.IsExported)
				{
					this.m_knownExports.Add(jsvariableField.Name, jsvariableField);
				}
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00006330 File Offset: 0x00004530
		internal override void AnalyzeScope()
		{
			base.AnalyzeScope();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00006338 File Offset: 0x00004538
		public override JSVariableField CreateField(string name, object value, FieldAttributes attributes)
		{
			return new JSVariableField(FieldType.Local, name, attributes, value);
		}

		// Token: 0x0400006E RID: 110
		private Dictionary<string, JSVariableField> m_knownExports;
	}
}
