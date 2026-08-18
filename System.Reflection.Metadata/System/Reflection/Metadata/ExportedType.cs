using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200003B RID: 59
	public struct ExportedType
	{
		// Token: 0x060002DA RID: 730 RVA: 0x000082BA File Offset: 0x000064BA
		internal ExportedType(MetadataReader reader, int rowId)
		{
			this.reader = reader;
			this.rowId = rowId;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060002DB RID: 731 RVA: 0x000082CA File Offset: 0x000064CA
		private ExportedTypeHandle Handle
		{
			get
			{
				return ExportedTypeHandle.FromRowId(this.rowId);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060002DC RID: 732 RVA: 0x000082D7 File Offset: 0x000064D7
		public TypeAttributes Attributes
		{
			get
			{
				return this.reader.ExportedTypeTable.GetFlags(this.rowId);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060002DD RID: 733 RVA: 0x000082F0 File Offset: 0x000064F0
		public bool IsForwarder
		{
			get
			{
				return this.Attributes.IsForwarder() && this.Implementation.Kind == HandleKind.AssemblyReference;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000831E File Offset: 0x0000651E
		public StringHandle Name
		{
			get
			{
				return this.reader.ExportedTypeTable.GetTypeName(this.rowId);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00008336 File Offset: 0x00006536
		public StringHandle Namespace
		{
			get
			{
				return this.reader.ExportedTypeTable.GetTypeNamespaceString(this.rowId);
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000834E File Offset: 0x0000654E
		public NamespaceDefinitionHandle NamespaceDefinition
		{
			get
			{
				return this.reader.ExportedTypeTable.GetTypeNamespace(this.rowId);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00008366 File Offset: 0x00006566
		public EntityHandle Implementation
		{
			get
			{
				return this.reader.ExportedTypeTable.GetImplementation(this.rowId);
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000837E File Offset: 0x0000657E
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this.reader, this.Handle);
		}

		// Token: 0x04000295 RID: 661
		internal readonly MetadataReader reader;

		// Token: 0x04000296 RID: 662
		internal readonly int rowId;
	}
}
