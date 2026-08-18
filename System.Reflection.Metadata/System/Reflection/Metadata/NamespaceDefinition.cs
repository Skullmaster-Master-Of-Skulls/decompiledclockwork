using System;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;

namespace System.Reflection.Metadata
{
	// Token: 0x02000087 RID: 135
	public struct NamespaceDefinition
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x0000EBDA File Offset: 0x0000CDDA
		internal NamespaceDefinition(NamespaceData data)
		{
			this._data = data;
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0000EBE3 File Offset: 0x0000CDE3
		public StringHandle Name
		{
			get
			{
				return this._data.Name;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		public NamespaceDefinitionHandle Parent
		{
			get
			{
				return this._data.Parent;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000EBFD File Offset: 0x0000CDFD
		public ImmutableArray<NamespaceDefinitionHandle> NamespaceDefinitions
		{
			get
			{
				return this._data.NamespaceDefinitions;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0000EC0A File Offset: 0x0000CE0A
		public ImmutableArray<TypeDefinitionHandle> TypeDefinitions
		{
			get
			{
				return this._data.TypeDefinitions;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0000EC17 File Offset: 0x0000CE17
		public ImmutableArray<ExportedTypeHandle> ExportedTypes
		{
			get
			{
				return this._data.ExportedTypes;
			}
		}

		// Token: 0x040003C8 RID: 968
		private NamespaceData _data;
	}
}
