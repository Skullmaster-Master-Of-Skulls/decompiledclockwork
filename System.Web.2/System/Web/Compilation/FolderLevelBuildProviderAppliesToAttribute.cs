using System;

namespace System.Web.Compilation
{
	// Token: 0x0200083F RID: 2111
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class FolderLevelBuildProviderAppliesToAttribute : Attribute
	{
		// Token: 0x06006496 RID: 25750 RVA: 0x0016093B File Offset: 0x0015EB3B
		public FolderLevelBuildProviderAppliesToAttribute(FolderLevelBuildProviderAppliesTo appliesTo)
		{
			this._appliesTo = appliesTo;
		}

		// Token: 0x17001C57 RID: 7255
		// (get) Token: 0x06006497 RID: 25751 RVA: 0x0016094A File Offset: 0x0015EB4A
		public FolderLevelBuildProviderAppliesTo AppliesTo
		{
			get
			{
				return this._appliesTo;
			}
		}

		// Token: 0x040033EE RID: 13294
		private FolderLevelBuildProviderAppliesTo _appliesTo;
	}
}
