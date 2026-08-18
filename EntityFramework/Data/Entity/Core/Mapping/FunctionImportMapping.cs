using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003B4 RID: 948
	public abstract class FunctionImportMapping : MappingItem
	{
		// Token: 0x0600227A RID: 8826 RVA: 0x000A0F0C File Offset: 0x0009F10C
		internal FunctionImportMapping(EdmFunction functionImport, EdmFunction targetFunction)
		{
			this._functionImport = functionImport;
			this._targetFunction = targetFunction;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x000A0F22 File Offset: 0x0009F122
		public EdmFunction FunctionImport
		{
			get
			{
				return this._functionImport;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x000A0F2A File Offset: 0x0009F12A
		public EdmFunction TargetFunction
		{
			get
			{
				return this._targetFunction;
			}
		}

		// Token: 0x04000C29 RID: 3113
		private readonly EdmFunction _functionImport;

		// Token: 0x04000C2A RID: 3114
		private readonly EdmFunction _targetFunction;
	}
}
