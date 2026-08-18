using System;
using System.Collections;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000066 RID: 102
	public class ScreenCollection : CollectionBase
	{
		// Token: 0x1700019F RID: 415
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

		// Token: 0x0600050A RID: 1290 RVA: 0x000225C4 File Offset: 0x000207C4
		public int Add(Screen screen)
		{
			return base.List.Add(screen);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000365E File Offset: 0x0000185E
		public void Insert(int index, Screen screen)
		{
			base.List.Insert(index, screen);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000366F File Offset: 0x0000186F
		public void Remove(Screen screen)
		{
			base.List.Remove(screen);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000225E4 File Offset: 0x000207E4
		public bool Contains(Screen screen)
		{
			return base.List.Contains(screen);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00022604 File Offset: 0x00020804
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

		// Token: 0x0600050F RID: 1295 RVA: 0x00022674 File Offset: 0x00020874
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
