using System;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000BB RID: 187
	public struct EditAndContinueLogEntry : IEquatable<EditAndContinueLogEntry>
	{
		// Token: 0x06000808 RID: 2056 RVA: 0x0001576B File Offset: 0x0001396B
		public EditAndContinueLogEntry(EntityHandle handle, EditAndContinueOperation operation)
		{
			this._handle = handle;
			this._operation = operation;
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0001577B File Offset: 0x0001397B
		public EntityHandle Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00015783 File Offset: 0x00013983
		public EditAndContinueOperation Operation
		{
			get
			{
				return this._operation;
			}
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001578B File Offset: 0x0001398B
		public override bool Equals(object obj)
		{
			return obj is EditAndContinueLogEntry && this.Equals((EditAndContinueLogEntry)obj);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000157A3 File Offset: 0x000139A3
		public bool Equals(EditAndContinueLogEntry other)
		{
			return this.Operation == other.Operation && this.Handle == other.Handle;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000157C8 File Offset: 0x000139C8
		public override int GetHashCode()
		{
			return (int)(this.Operation ^ (EditAndContinueOperation)this.Handle.GetHashCode());
		}

		// Token: 0x040004F3 RID: 1267
		private readonly EntityHandle _handle;

		// Token: 0x040004F4 RID: 1268
		private readonly EditAndContinueOperation _operation;
	}
}
