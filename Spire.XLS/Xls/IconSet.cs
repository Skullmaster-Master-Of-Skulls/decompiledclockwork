using System;
using System.Collections.Generic;
using Spire.Xls.Core;

namespace Spire.Xls
{
	// Token: 0x02000125 RID: 293
	public class IconSet
	{
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0007CFB4 File Offset: 0x0007BFB4
		internal IIconSet Wrapped
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
				return this.ᜀ;
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0007CFF8 File Offset: 0x0007BFF8
		internal IconSet(IIconSet A_0)
		{
			this.ᜀ = A_0;
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0007D014 File Offset: 0x0007C014
		public IList<ConditionValue> IconCriteria
		{
			get
			{
				IList<ConditionValue> list = new List<ConditionValue>();
				IEnumerator<IConditionValue> enumerator = this.ᜀ.IconCriteria.GetEnumerator();
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
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							IConditionValue a_ = enumerator.Current;
							list.Add(new ConditionValue(a_));
							num = 4;
							continue;
						}
						}
						IL_5C:
						num = 3;
						continue;
						goto IL_5C;
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
							goto IL_DB;
						case 2:
							enumerator.Dispose();
							num = 1;
							continue;
						}
						if (enumerator == null)
						{
							break;
						}
						num = 2;
					}
					IL_DB:;
				}
				return list;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0007D11C File Offset: 0x0007C11C
		// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x0007D164 File Offset: 0x0007C164
		private IconSetType IconSetType
		{
			get
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
				return this.ᜀ.IconSet;
			}
			set
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
				this.ᜀ.IconSet = value;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0007D1AC File Offset: 0x0007C1AC
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x0007D1F4 File Offset: 0x0007C1F4
		public bool PercentileValues
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
				return this.ᜀ.PercentileValues;
			}
			set
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
				this.ᜀ.PercentileValues = value;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0007D23C File Offset: 0x0007C23C
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x0007D284 File Offset: 0x0007C284
		public bool IsReverseOrder
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
				return this.ᜀ.IsReverseOrder;
			}
			set
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
				this.ᜀ.IsReverseOrder = value;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0007D2CC File Offset: 0x0007C2CC
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x0007D314 File Offset: 0x0007C314
		public bool ShowIconOnly
		{
			get
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
				return this.ᜀ.ShowIconOnly;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜀ.ShowIconOnly = value;
			}
		}

		// Token: 0x04000B7B RID: 2939
		private bool \u2460\u008A\u0098\u009C;

		// Token: 0x04000B7C RID: 2940
		private string \u2460\u009B\u009A\u008F;

		// Token: 0x04000B7D RID: 2941
		private IIconSet ᜀ;
	}
}
