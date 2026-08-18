using System;
using System.Drawing;

namespace a.b
{
	// Token: 0x02000341 RID: 833
	internal class it : ix
	{
		// Token: 0x06001E3A RID: 7738 RVA: 0x00081C3C File Offset: 0x00080C3C
		public virtual ja pi(c8 A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("visualText");
			}
			gd gd = new gd();
			ej ej = A_0.jh();
			Color c = ej.g6().j6();
			if (c.R != 0 || c.G != 0 || c.B != 0)
			{
				gd.k9(ColorTranslator.ToHtml(c));
			}
			Color c2 = ej.g7().j6();
			if (c2.R != 0 || c2.G != 0 || c2.B != 0)
			{
				gd.k7(ColorTranslator.ToHtml(c2));
			}
			gd.lb(ej.gy().e9());
			if (ej.gz() > 0)
			{
				gd.ld(ej.gz() / 2 + "pt");
			}
			return gd;
		}
	}
}
