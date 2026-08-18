using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000E8F RID: 3727
	public class FlipOperation : ImageOperation, IImageOperation
	{
		// Token: 0x17002C92 RID: 11410
		// (get) Token: 0x06008D4F RID: 36175 RVA: 0x00201175 File Offset: 0x001FF375
		// (set) Token: 0x06008D50 RID: 36176 RVA: 0x0020117D File Offset: 0x001FF37D
		public RotateFlipType Type { get; set; }

		// Token: 0x06008D51 RID: 36177 RVA: 0x00201186 File Offset: 0x001FF386
		public FlipOperation(RotateFlipType type) : this(type, -1)
		{
		}

		// Token: 0x06008D52 RID: 36178 RVA: 0x00201190 File Offset: 0x001FF390
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public FlipOperation(RotateFlipType type, int index)
		{
			this.Type = type;
			this.Index = index;
		}

		// Token: 0x06008D53 RID: 36179 RVA: 0x002011A8 File Offset: 0x001FF3A8
		public Image Apply(Image original)
		{
			Bitmap bitmap = new Bitmap(original);
			bitmap.RotateFlip(this.Type);
			return bitmap;
		}

		// Token: 0x17002C93 RID: 11411
		// (get) Token: 0x06008D54 RID: 36180 RVA: 0x002011C9 File Offset: 0x001FF3C9
		public string Name
		{
			get
			{
				return "Flip";
			}
		}
	}
}
