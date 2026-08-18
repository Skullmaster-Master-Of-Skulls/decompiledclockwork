using System;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000538 RID: 1336
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class EdmScalarPropertyAttribute : EdmPropertyAttribute
	{
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060032EF RID: 13039 RVA: 0x000F07F8 File Offset: 0x000EE9F8
		// (set) Token: 0x060032F0 RID: 13040 RVA: 0x000F0800 File Offset: 0x000EEA00
		public bool IsNullable
		{
			get
			{
				return this._isNullable;
			}
			set
			{
				this._isNullable = value;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060032F1 RID: 13041 RVA: 0x000F0809 File Offset: 0x000EEA09
		// (set) Token: 0x060032F2 RID: 13042 RVA: 0x000F0811 File Offset: 0x000EEA11
		public bool EntityKeyProperty { get; set; }

		// Token: 0x04001379 RID: 4985
		private bool _isNullable = true;
	}
}
