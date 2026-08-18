using System;

namespace System.Web.UI
{
	// Token: 0x0200025C RID: 604
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ConstructorNeedsTagAttribute : Attribute
	{
		// Token: 0x06001BB5 RID: 7093 RVA: 0x00049A60 File Offset: 0x00047C60
		public ConstructorNeedsTagAttribute()
		{
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0005755A File Offset: 0x0005575A
		public ConstructorNeedsTagAttribute(bool needsTag)
		{
			this.needsTag = needsTag;
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x00057569 File Offset: 0x00055769
		public bool NeedsTag
		{
			get
			{
				return this.needsTag;
			}
		}

		// Token: 0x040018D5 RID: 6357
		private bool needsTag;
	}
}
