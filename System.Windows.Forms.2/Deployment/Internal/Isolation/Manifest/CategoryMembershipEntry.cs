using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200009C RID: 156
	[StructLayout(LayoutKind.Sequential)]
	internal class CategoryMembershipEntry
	{
		// Token: 0x0400029F RID: 671
		public IDefinitionIdentity Identity;

		// Token: 0x040002A0 RID: 672
		public ISection SubcategoryMembership;
	}
}
