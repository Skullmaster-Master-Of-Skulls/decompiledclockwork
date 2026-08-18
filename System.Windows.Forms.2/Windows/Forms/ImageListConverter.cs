using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000296 RID: 662
	internal class ImageListConverter : ComponentConverter
	{
		// Token: 0x060029F8 RID: 10744 RVA: 0x000BF127 File Offset: 0x000BD327
		public ImageListConverter() : base(typeof(ImageList))
		{
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x00013062 File Offset: 0x00011262
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
