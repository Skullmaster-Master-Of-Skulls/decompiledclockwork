using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200004E RID: 78
	public struct PropertyAccessors
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00008C65 File Offset: 0x00006E65
		public MethodDefinitionHandle Getter
		{
			get
			{
				return MethodDefinitionHandle.FromRowId(this._getterRowId);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00008C72 File Offset: 0x00006E72
		public MethodDefinitionHandle Setter
		{
			get
			{
				return MethodDefinitionHandle.FromRowId(this._setterRowId);
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00008C7F File Offset: 0x00006E7F
		internal PropertyAccessors(int getterRowId, int setterRowId)
		{
			this._getterRowId = getterRowId;
			this._setterRowId = setterRowId;
		}

		// Token: 0x040002BF RID: 703
		private readonly int _getterRowId;

		// Token: 0x040002C0 RID: 704
		private readonly int _setterRowId;
	}
}
