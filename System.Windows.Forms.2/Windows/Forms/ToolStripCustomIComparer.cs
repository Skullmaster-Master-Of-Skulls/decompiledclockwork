using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x020003E3 RID: 995
	internal class ToolStripCustomIComparer : IComparer
	{
		// Token: 0x060043CF RID: 17359 RVA: 0x0011EF54 File Offset: 0x0011D154
		int IComparer.Compare(object x, object y)
		{
			if (x.GetType() == y.GetType())
			{
				return 0;
			}
			if (x.GetType().IsAssignableFrom(y.GetType()))
			{
				return 1;
			}
			if (y.GetType().IsAssignableFrom(x.GetType()))
			{
				return -1;
			}
			return 0;
		}
	}
}
