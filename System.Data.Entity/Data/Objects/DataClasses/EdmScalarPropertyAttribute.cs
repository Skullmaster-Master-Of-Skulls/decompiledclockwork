using System;

namespace System.Data.Objects.DataClasses
{
	// Token: 0x02000189 RID: 393
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class EdmScalarPropertyAttribute : EdmPropertyAttribute
	{
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x0005FBF1 File Offset: 0x0005DDF1
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x0005FBF9 File Offset: 0x0005DDF9
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

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x0005FC02 File Offset: 0x0005DE02
		// (set) Token: 0x06001C2E RID: 7214 RVA: 0x0005FC0A File Offset: 0x0005DE0A
		public bool EntityKeyProperty
		{
			get
			{
				return this._entityKeyProperty;
			}
			set
			{
				this._entityKeyProperty = value;
			}
		}

		// Token: 0x04000BA4 RID: 2980
		private bool _isNullable = true;

		// Token: 0x04000BA5 RID: 2981
		private bool _entityKeyProperty;
	}
}
