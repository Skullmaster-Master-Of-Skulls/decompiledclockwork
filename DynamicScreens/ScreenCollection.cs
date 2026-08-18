using System;
using System.Collections;

namespace DynamicScreens
{
	// Token: 0x02000002 RID: 2
	public class ScreenCollection : CollectionBase
	{
		// Token: 0x17000001 RID: 1
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

		// Token: 0x06000003 RID: 3 RVA: 0x00002084 File Offset: 0x00001084
		public int Add(Screen screen)
		{
			return base.List.Add(screen);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020A2 File Offset: 0x000010A2
		public void Insert(int index, Screen screen)
		{
			base.List.Insert(index, screen);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020B3 File Offset: 0x000010B3
		public void Remove(Screen screen)
		{
			base.List.Remove(screen);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020C4 File Offset: 0x000010C4
		public bool Contains(Screen screen)
		{
			return base.List.Contains(screen);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E4 File Offset: 0x000010E4
		public Screen Find(int screenNum)
		{
			foreach (object obj in base.List)
			{
				Screen screen = (Screen)obj;
				if (screen.ScreenNum == screenNum)
				{
					return screen;
				}
			}
			return null;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002160 File Offset: 0x00001160
		public override string ToString()
		{
			string result;
			if (base.List.Count <= 0)
			{
				result = "";
			}
			else if (base.List.Count == 1)
			{
				result = ((Screen)base.List[0]).ScreenTitle;
			}
			else
			{
				string text = "";
				for (int i = 0; i < base.List.Count; i++)
				{
					if (i > 0)
					{
						text += ", ";
					}
					text += ((Screen)base.List[0]).ScreenTitle;
				}
				result = text;
			}
			return result;
		}
	}
}
