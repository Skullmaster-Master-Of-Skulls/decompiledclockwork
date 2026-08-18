using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000076 RID: 118
	public struct InterfaceImplementation
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x0000ABFA File Offset: 0x00008DFA
		internal InterfaceImplementation(MetadataReader reader, InterfaceImplementationHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0000AC10 File Offset: 0x00008E10
		private InterfaceImplementationHandle Handle
		{
			get
			{
				return InterfaceImplementationHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000AC1D File Offset: 0x00008E1D
		public EntityHandle Interface
		{
			get
			{
				return this._reader.InterfaceImplTable.GetInterface(this._rowId);
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000AC35 File Offset: 0x00008E35
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x04000346 RID: 838
		private readonly MetadataReader _reader;

		// Token: 0x04000347 RID: 839
		private readonly int _rowId;
	}
}
