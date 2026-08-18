using System;

namespace System.Web.UI
{
	// Token: 0x02000081 RID: 129
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class TargetControlTypeAttribute : Attribute
	{
		// Token: 0x0600058E RID: 1422 RVA: 0x00019F7C File Offset: 0x0001817C
		public TargetControlTypeAttribute(Type targetControlType)
		{
			if (targetControlType == null)
			{
				throw new ArgumentNullException("targetControlType");
			}
			this._targetControlType = targetControlType;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00019F9F File Offset: 0x0001819F
		public Type TargetControlType
		{
			get
			{
				return this._targetControlType;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x00019F9F File Offset: 0x0001819F
		public override object TypeId
		{
			get
			{
				return this._targetControlType;
			}
		}

		// Token: 0x04000202 RID: 514
		private Type _targetControlType;
	}
}
