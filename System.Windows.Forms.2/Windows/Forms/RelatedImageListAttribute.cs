using System;

namespace System.Windows.Forms
{
	// Token: 0x02000341 RID: 833
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class RelatedImageListAttribute : Attribute
	{
		// Token: 0x060035D6 RID: 13782 RVA: 0x000F375C File Offset: 0x000F195C
		public RelatedImageListAttribute(string relatedImageList)
		{
			this.relatedImageList = relatedImageList;
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x060035D7 RID: 13783 RVA: 0x000F376B File Offset: 0x000F196B
		public string RelatedImageList
		{
			get
			{
				return this.relatedImageList;
			}
		}

		// Token: 0x04001F71 RID: 8049
		private string relatedImageList;
	}
}
