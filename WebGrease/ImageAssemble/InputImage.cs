using System;
using System.Collections.Generic;

namespace WebGrease.ImageAssemble
{
	// Token: 0x020001B1 RID: 433
	internal class InputImage
	{
		// Token: 0x06001641 RID: 5697 RVA: 0x00080ED5 File Offset: 0x0007F0D5
		internal InputImage()
		{
			this.Position = ImagePosition.Left;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00080EEF File Offset: 0x0007F0EF
		internal InputImage(string imagePath)
		{
			this.AbsoluteImagePath = imagePath;
			this.Position = ImagePosition.Left;
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x00080F10 File Offset: 0x0007F110
		// (set) Token: 0x06001644 RID: 5700 RVA: 0x00080F18 File Offset: 0x0007F118
		internal string AbsoluteImagePath { get; set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x00080F21 File Offset: 0x0007F121
		// (set) Token: 0x06001646 RID: 5702 RVA: 0x00080F29 File Offset: 0x0007F129
		internal string OriginalImagePath { get; set; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x00080F32 File Offset: 0x0007F132
		// (set) Token: 0x06001648 RID: 5704 RVA: 0x00080F3A File Offset: 0x0007F13A
		internal ImagePosition Position { get; set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x00080F43 File Offset: 0x0007F143
		internal IList<string> DuplicateImagePaths
		{
			get
			{
				return this.duplicateImagePaths;
			}
		}

		// Token: 0x04000BC8 RID: 3016
		private readonly List<string> duplicateImagePaths = new List<string>();
	}
}
