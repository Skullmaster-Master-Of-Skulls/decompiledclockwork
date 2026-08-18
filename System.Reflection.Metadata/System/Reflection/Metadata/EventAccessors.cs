using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200004F RID: 79
	public struct EventAccessors
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00008C8F File Offset: 0x00006E8F
		public MethodDefinitionHandle Adder
		{
			get
			{
				return MethodDefinitionHandle.FromRowId(this._adderRowId);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00008C9C File Offset: 0x00006E9C
		public MethodDefinitionHandle Remover
		{
			get
			{
				return MethodDefinitionHandle.FromRowId(this._removerRowId);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00008CA9 File Offset: 0x00006EA9
		public MethodDefinitionHandle Raiser
		{
			get
			{
				return MethodDefinitionHandle.FromRowId(this._raiserRowId);
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00008CB6 File Offset: 0x00006EB6
		internal EventAccessors(int adderRowId, int removerRowId, int raiserRowId)
		{
			this._adderRowId = adderRowId;
			this._removerRowId = removerRowId;
			this._raiserRowId = raiserRowId;
		}

		// Token: 0x040002C1 RID: 705
		private readonly int _adderRowId;

		// Token: 0x040002C2 RID: 706
		private readonly int _removerRowId;

		// Token: 0x040002C3 RID: 707
		private readonly int _raiserRowId;
	}
}
