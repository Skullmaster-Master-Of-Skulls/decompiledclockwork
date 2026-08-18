using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Collections
{
	// Token: 0x02000016 RID: 22
	public class StylesCollection : XlsStylesCollection
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00005C58 File Offset: 0x00004C58
		internal StylesCollection(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x1700007D RID: 125
		public CellStyle this[int Index]
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
				return new CellStyle(base.InnerList[Index]);
			}
		}

		// Token: 0x1700007E RID: 126
		public CellStyle this[string name]
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
				return new CellStyle(base[name]);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005D04 File Offset: 0x00004D04
		public new CellStyle Add(string name)
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
			return new CellStyle(base.Add(name));
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005D4C File Offset: 0x00004D4C
		public void Add(CellStyle style)
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
			base.Add(style.Wrapped);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005D94 File Offset: 0x00004D94
		public CellStyle Contains(CellStyle style)
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
			return new CellStyle(base.ContainsSameStyle(style.Wrapped));
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005DE0 File Offset: 0x00004DE0
		public static bool Compare(CellStyle source, CellStyle destination)
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
			return XlsStylesCollection.CompareStyles(source.Wrapped, destination.Wrapped);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005E2C File Offset: 0x00004E2C
		public void Replace(CellStyle style)
		{
			int num = 2;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (base.List[num2].Name == style.Wrapped.Name)
					{
						num = 4;
						continue;
					}
					if (true)
					{
					}
					num2++;
					num = 7;
					continue;
				case 1:
				{
					int count;
					if (num2 >= count)
					{
						num = 5;
						continue;
					}
					goto IL_E3;
				}
				case 3:
					goto IL_BD;
				case 4:
					goto IL_11F;
				case 5:
					goto IL_D7;
				case 6:
				{
					CellStyle cellStyle = this[style.Name];
					num2 = 0;
					int count = base.List.Count;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E3;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				case 7:
					goto IL_BD;
				}
				if (base.ᜁ(style.Wrapped.Name))
				{
					num = 6;
					continue;
				}
				break;
				IL_BD:
				num = 1;
				continue;
				IL_E3:
				num = 0;
			}
			IL_D7:
			return;
			IL_11F:
			base.List[num2] = style.Wrapped;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005F60 File Offset: 0x00004F60
		public CellStyle GetDefaultStyle(string styleName)
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
			return new CellStyle(base.CreateBuiltInStyle(styleName));
		}
	}
}
