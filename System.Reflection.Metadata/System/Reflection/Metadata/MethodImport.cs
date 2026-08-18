using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000083 RID: 131
	public struct MethodImport
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x0000EA8A File Offset: 0x0000CC8A
		internal MethodImport(MethodImportAttributes attributes, StringHandle name, ModuleReferenceHandle module)
		{
			this._attributes = attributes;
			this._name = name;
			this._module = module;
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000EAA1 File Offset: 0x0000CCA1
		public MethodImportAttributes Attributes
		{
			get
			{
				return this._attributes;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000EAA9 File Offset: 0x0000CCA9
		public StringHandle Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0000EAB1 File Offset: 0x0000CCB1
		public ModuleReferenceHandle Module
		{
			get
			{
				return this._module;
			}
		}

		// Token: 0x040003C0 RID: 960
		private readonly MethodImportAttributes _attributes;

		// Token: 0x040003C1 RID: 961
		private readonly StringHandle _name;

		// Token: 0x040003C2 RID: 962
		private readonly ModuleReferenceHandle _module;
	}
}
