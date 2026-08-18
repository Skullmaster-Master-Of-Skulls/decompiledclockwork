using System;
using System.Collections.Generic;
using Spire.Xls.Core;

namespace Spire.Xls
{
	// Token: 0x02000050 RID: 80
	public class ColorScale
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00052E94 File Offset: 0x00051E94
		internal sprᝠ Wrapped
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ as sprᝠ;
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00052EDC File Offset: 0x00051EDC
		internal ColorScale(IColorScale A_0)
		{
			this.ᜀ = A_0;
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00052EF8 File Offset: 0x00051EF8
		public IList<ColorConditionValue> Criteria
		{
			get
			{
				IList<ColorConditionValue> list = new List<ColorConditionValue>();
				IEnumerator<IColorConditionValue> enumerator = this.ᜀ.Criteria.GetEnumerator();
				try
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_A2;
						case 1:
							num = 0;
							continue;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							IColorConditionValue a_ = enumerator.Current;
							list.Add(new ColorConditionValue(a_));
							goto IL_52;
						}
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_52;
							default:
								if (false)
								{
								}
								break;
							}
							break;
						}
						goto IL_3D;
						IL_52:
						num = 4;
						continue;
						IL_78:
						num = 3;
						continue;
						IL_3D:
						goto IL_78;
					}
					IL_A2:;
				}
				finally
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							enumerator.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_DB;
						}
						if (enumerator == null)
						{
							break;
						}
						num = 1;
					}
					IL_DB:;
				}
				return list;
			}
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00053000 File Offset: 0x00052000
		public void SetConditionCount(int count)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜀ.SetConditionCount(count);
		}

		// Token: 0x0400014A RID: 330
		private int \u25D8\u00B0\u00A4\u0081;

		// Token: 0x0400014B RID: 331
		private long \u25D9\u00AD\u00A7\u00A2;

		// Token: 0x0400014C RID: 332
		private string \u2593\u00B0\u009D\u00A8;

		// Token: 0x0400014D RID: 333
		private byte[] \u25D9\u0087\u00AD\u00A5;

		// Token: 0x0400014E RID: 334
		private byte \u25D8\u0084\u00A0\u00AC;

		// Token: 0x0400014F RID: 335
		private IColorScale ᜀ;
	}
}
