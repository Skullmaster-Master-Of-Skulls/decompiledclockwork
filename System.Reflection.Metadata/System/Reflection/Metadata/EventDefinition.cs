using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000038 RID: 56
	public struct EventDefinition
	{
		// Token: 0x060002CB RID: 715 RVA: 0x000080D1 File Offset: 0x000062D1
		internal EventDefinition(MetadataReader reader, EventDefinitionHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060002CC RID: 716 RVA: 0x000080E7 File Offset: 0x000062E7
		private EventDefinitionHandle Handle
		{
			get
			{
				return EventDefinitionHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060002CD RID: 717 RVA: 0x000080F4 File Offset: 0x000062F4
		public StringHandle Name
		{
			get
			{
				return this._reader.EventTable.GetName(this.Handle);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000810C File Offset: 0x0000630C
		public EventAttributes Attributes
		{
			get
			{
				return this._reader.EventTable.GetFlags(this.Handle);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00008124 File Offset: 0x00006324
		public EntityHandle Type
		{
			get
			{
				return this._reader.EventTable.GetEventType(this.Handle);
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000813C File Offset: 0x0000633C
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00008154 File Offset: 0x00006354
		public EventAccessors GetAccessors()
		{
			int adderRowId = 0;
			int removerRowId = 0;
			int raiserRowId = 0;
			ushort num2;
			int num = this._reader.MethodSemanticsTable.FindSemanticMethodsForEvent(this.Handle, out num2);
			for (ushort num3 = 0; num3 < num2; num3 += 1)
			{
				int rowId = num + (int)num3;
				MethodSemanticsAttributes semantics = this._reader.MethodSemanticsTable.GetSemantics(rowId);
				if (semantics != MethodSemanticsAttributes.Adder)
				{
					if (semantics != MethodSemanticsAttributes.Remover)
					{
						if (semantics == MethodSemanticsAttributes.Raiser)
						{
							raiserRowId = this._reader.MethodSemanticsTable.GetMethod(rowId).RowId;
						}
					}
					else
					{
						removerRowId = this._reader.MethodSemanticsTable.GetMethod(rowId).RowId;
					}
				}
				else
				{
					adderRowId = this._reader.MethodSemanticsTable.GetMethod(rowId).RowId;
				}
			}
			return new EventAccessors(adderRowId, removerRowId, raiserRowId);
		}

		// Token: 0x04000288 RID: 648
		private readonly MetadataReader _reader;

		// Token: 0x04000289 RID: 649
		private readonly int _rowId;
	}
}
