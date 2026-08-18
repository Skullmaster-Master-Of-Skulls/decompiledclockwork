using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EAA RID: 3754
	public class RotateFlipOperation : ImageOperation, IImageOperation
	{
		// Token: 0x17002D41 RID: 11585
		// (get) Token: 0x06008F25 RID: 36645 RVA: 0x00203A3F File Offset: 0x00201C3F
		// (set) Token: 0x06008F26 RID: 36646 RVA: 0x00203A47 File Offset: 0x00201C47
		public RotateFlipType Type { get; set; }

		// Token: 0x06008F27 RID: 36647 RVA: 0x00203A50 File Offset: 0x00201C50
		public RotateFlipOperation(RotateFlipType type) : this(type, -1)
		{
		}

		// Token: 0x06008F28 RID: 36648 RVA: 0x00203A5A File Offset: 0x00201C5A
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public RotateFlipOperation(RotateFlipType type, int index)
		{
			this.Type = type;
			this.Index = index;
		}

		// Token: 0x06008F29 RID: 36649 RVA: 0x00203A70 File Offset: 0x00201C70
		public Image Apply(Image original)
		{
			Bitmap bitmap = new Bitmap(original);
			bitmap.RotateFlip(this.Type);
			return bitmap;
		}

		// Token: 0x17002D42 RID: 11586
		// (get) Token: 0x06008F2A RID: 36650 RVA: 0x00203A91 File Offset: 0x00201C91
		public string Name
		{
			get
			{
				return "Rotate";
			}
		}
	}
}
