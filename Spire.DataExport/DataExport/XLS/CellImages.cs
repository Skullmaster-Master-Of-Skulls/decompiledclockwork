using System;
using Spire.DataExport.Collections;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001DB RID: 475
	public class CellImages : Collection
	{
		// Token: 0x06000E5E RID: 3678 RVA: 0x0009FD20 File Offset: 0x0009ED20
		public CellImages(object Holder)
		{
			this.m_holder = Holder;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0009FD3C File Offset: 0x0009ED3C
		public CellImage Add(CellImage Item)
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
			base.Add(Item);
			return Item;
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0009FD80 File Offset: 0x0009ED80
		public bool Find(string Title, ref int Index)
		{
			int num;
			for (;;)
			{
				num = 0;
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= base.Count)
						{
							num2 = 2;
							continue;
						}
						num2 = 3;
						continue;
					case 1:
						goto IL_78;
					case 2:
						return false;
					case 3:
						if (string.Compare(this[num].Title, Title, true) == 0)
						{
							num2 = 5;
							continue;
						}
						num++;
						if (true)
						{
						}
						num2 = 1;
						continue;
					case 4:
						goto IL_78;
					case 5:
						goto IL_6C;
					}
					break;
					IL_78:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_42;
					default:
						if (false)
						{
						}
						num2 = 0;
						break;
					}
				}
			}
			IL_42:
			Index = num;
			return true;
			IL_6C:
			goto IL_42;
		}

		// Token: 0x170001CF RID: 463
		public CellImage this[int Index]
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
				return base[Index] as CellImage;
			}
			set
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
				base[Index] = value;
			}
		}
	}
}
