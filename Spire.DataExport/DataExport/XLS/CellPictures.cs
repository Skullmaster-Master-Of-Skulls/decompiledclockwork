using System;
using Spire.DataExport.Collections;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001E3 RID: 483
	public class CellPictures : Collection
	{
		// Token: 0x06000E9F RID: 3743 RVA: 0x000A1BD4 File Offset: 0x000A0BD4
		public CellPictures(object Holder)
		{
			this.m_holder = Holder;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x000A1BF0 File Offset: 0x000A0BF0
		public CellPicture Add(CellPicture Item)
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

		// Token: 0x06000EA1 RID: 3745 RVA: 0x000A1C34 File Offset: 0x000A0C34
		public bool Find(string Name, ref int Index)
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
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8D;
						default:
							if (false)
							{
							}
							if (string.Compare(this[num].Name, Name, true) == 0)
							{
								num2 = 2;
								continue;
							}
							num++;
							if (true)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					case 1:
						if (num >= base.Count)
						{
							num2 = 5;
							continue;
						}
						num2 = 0;
						continue;
					case 2:
						goto IL_8B;
					case 3:
						goto IL_8D;
					case 4:
						goto IL_8D;
					case 5:
						return false;
					}
					break;
					IL_8D:
					num2 = 1;
				}
			}
			IL_8B:
			Index = num;
			return true;
		}

		// Token: 0x170001E5 RID: 485
		public CellPicture this[int Index]
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
				return base[Index] as CellPicture;
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
