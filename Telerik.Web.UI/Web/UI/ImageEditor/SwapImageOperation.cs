using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Web;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000E90 RID: 3728
	public class SwapImageOperation : ImageOperation, IImageOperation
	{
		// Token: 0x17002C94 RID: 11412
		// (get) Token: 0x06008D55 RID: 36181 RVA: 0x002011D0 File Offset: 0x001FF3D0
		// (set) Token: 0x06008D56 RID: 36182 RVA: 0x002011D8 File Offset: 0x001FF3D8
		public string Src { get; set; }

		// Token: 0x06008D57 RID: 36183 RVA: 0x002011E1 File Offset: 0x001FF3E1
		public SwapImageOperation(string src) : this(src, "SwapImage", -1)
		{
		}

		// Token: 0x06008D58 RID: 36184 RVA: 0x002011F0 File Offset: 0x001FF3F0
		public SwapImageOperation(string src, string name) : this(src, name, -1)
		{
		}

		// Token: 0x06008D59 RID: 36185 RVA: 0x002011FB File Offset: 0x001FF3FB
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public SwapImageOperation(string src, string name, int index)
		{
			this.Src = src;
			this.Name = name;
			this.Index = index;
		}

		// Token: 0x17002C95 RID: 11413
		// (get) Token: 0x06008D5A RID: 36186 RVA: 0x00201218 File Offset: 0x001FF418
		// (set) Token: 0x06008D5B RID: 36187 RVA: 0x00201220 File Offset: 0x001FF420
		public string Name { get; set; }

		// Token: 0x06008D5C RID: 36188 RVA: 0x00201229 File Offset: 0x001FF429
		public Image Apply(Image image)
		{
			return new Bitmap(HttpContext.Current.Request.MapPath(this.Src));
		}
	}
}
