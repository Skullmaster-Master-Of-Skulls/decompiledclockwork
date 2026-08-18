using System;
using System.Collections;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Collections
{
	// Token: 0x0200003A RID: 58
	public class FontsCollection : XlsFontsCollection
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x00024694 File Offset: 0x00023694
		internal FontsCollection(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000246AC File Offset: 0x000236AC
		public void Add(XlsFontStyle font)
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
			base.Add(font);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000246F0 File Offset: 0x000236F0
		public void Add(ExcelFont font)
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
			base.Add(font.Wrapped);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00024738 File Offset: 0x00023738
		public IDictionary AddFonts(FontsCollection fonts)
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
			return base.AddRange(fonts);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0002477C File Offset: 0x0002377C
		public bool Contains(XlsFontStyle font)
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
			return base.Contains(font);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000247C0 File Offset: 0x000237C0
		public bool Contains(ExcelFont font)
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
			return base.Contains(font.Wrapped);
		}

		// Token: 0x17000153 RID: 339
		public ExcelFont this[int index]
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
				return new ExcelFont(base[index]);
			}
		}
	}
}
