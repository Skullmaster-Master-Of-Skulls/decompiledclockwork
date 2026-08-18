using System;
using System.Drawing;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000E8E RID: 3726
	public class EmptyOperation : ImageOperation, IImageOperation
	{
		// Token: 0x17002C90 RID: 11408
		// (get) Token: 0x06008D4A RID: 36170 RVA: 0x00201152 File Offset: 0x001FF352
		public string Name
		{
			get
			{
				return "Empty";
			}
		}

		// Token: 0x17002C91 RID: 11409
		// (get) Token: 0x06008D4B RID: 36171 RVA: 0x00201159 File Offset: 0x001FF359
		// (set) Token: 0x06008D4C RID: 36172 RVA: 0x0020115C File Offset: 0x001FF35C
		public override int Index
		{
			get
			{
				return -1;
			}
			set
			{
				base.Index = value;
			}
		}

		// Token: 0x06008D4D RID: 36173 RVA: 0x00201165 File Offset: 0x001FF365
		public Image Apply(Image image)
		{
			return new Bitmap(image);
		}
	}
}
