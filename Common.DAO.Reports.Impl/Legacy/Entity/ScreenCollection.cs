using System;
using System.Collections;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000026 RID: 38
	public class ScreenCollection : CollectionBase
	{
		// Token: 0x1700009C RID: 156
		public Screen this[int index]
		{
			get
			{
				return (Screen)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00029DE8 File Offset: 0x00027FE8
		public int Add(Screen screen)
		{
			return base.List.Add(screen);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00029E06 File Offset: 0x00028006
		public void Insert(int index, Screen screen)
		{
			base.List.Insert(index, screen);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00029E17 File Offset: 0x00028017
		public void Remove(Screen screen)
		{
			base.List.Remove(screen);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00029E28 File Offset: 0x00028028
		public bool Contains(Screen screen)
		{
			return base.List.Contains(screen);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00029E48 File Offset: 0x00028048
		public Screen Find(int screenNum)
		{
			foreach (object obj in base.List)
			{
				Screen screen = (Screen)obj;
				bool flag = screen.ScreenNum == screenNum;
				if (flag)
				{
					return screen;
				}
			}
			return null;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00029EB8 File Offset: 0x000280B8
		public override string ToString()
		{
			bool flag = base.List.Count <= 0;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = base.List.Count == 1;
				if (flag2)
				{
					result = ((Screen)base.List[0]).ScreenTitle;
				}
				else
				{
					string text = "";
					for (int i = 0; i < base.List.Count; i++)
					{
						bool flag3 = i > 0;
						if (flag3)
						{
							text += ", ";
						}
						text += ((Screen)base.List[0]).ScreenTitle;
					}
					result = text;
				}
			}
			return result;
		}
	}
}
