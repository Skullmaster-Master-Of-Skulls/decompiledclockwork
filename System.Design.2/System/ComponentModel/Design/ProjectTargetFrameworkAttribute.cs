using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020001BD RID: 445
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class ProjectTargetFrameworkAttribute : Attribute
	{
		// Token: 0x06001028 RID: 4136 RVA: 0x0005B675 File Offset: 0x00059875
		public ProjectTargetFrameworkAttribute(string targetFrameworkMoniker)
		{
			this._targetFrameworkMoniker = targetFrameworkMoniker;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001029 RID: 4137 RVA: 0x0005B684 File Offset: 0x00059884
		public string TargetFrameworkMoniker
		{
			get
			{
				return this._targetFrameworkMoniker;
			}
		}

		// Token: 0x04000961 RID: 2401
		private string _targetFrameworkMoniker;
	}
}
