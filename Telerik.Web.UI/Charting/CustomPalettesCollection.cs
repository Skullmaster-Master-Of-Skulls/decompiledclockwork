using System;

namespace Telerik.Charting
{
	// Token: 0x020017AE RID: 6062
	public class CustomPalettesCollection : ChartingStateManagedCollection<Palette>
	{
		// Token: 0x0600EC0B RID: 60427 RVA: 0x0035AAFC File Offset: 0x00358CFC
		public bool Contains(string paletteName)
		{
			bool result = false;
			foreach (Palette palette in base.List)
			{
				if (string.Compare(palette.Name, paletteName, true) == 0)
				{
					return true;
				}
			}
			return result;
		}

		// Token: 0x0600EC0C RID: 60428 RVA: 0x0035AB5C File Offset: 0x00358D5C
		public int IndexOf(string paletteName)
		{
			int num = 0;
			foreach (Palette palette in base.List)
			{
				if (string.Compare(palette.Name, paletteName, true) == 0)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x0600EC0D RID: 60429 RVA: 0x0035ABC0 File Offset: 0x00358DC0
		public void Remove(string paletteName)
		{
			int num = 0;
			foreach (Palette palette in base.List)
			{
				if (string.Compare(palette.Name, paletteName, true) == 0)
				{
					base.List.RemoveAt(num);
					break;
				}
				num++;
			}
		}

		// Token: 0x0600EC0E RID: 60430 RVA: 0x0035AC2C File Offset: 0x00358E2C
		public Palette GetPalette(int index)
		{
			return base.List[index];
		}

		// Token: 0x0600EC0F RID: 60431 RVA: 0x0035AC3C File Offset: 0x00358E3C
		public Palette GetPalette(string name)
		{
			int num = this.IndexOf(name);
			if (num != -1)
			{
				return this[num];
			}
			return null;
		}
	}
}
