using System;
using Spire.DataExport.Collections;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001BA RID: 442
	public class ColumnFormats : Collection
	{
		// Token: 0x06000C75 RID: 3189 RVA: 0x00082BB4 File Offset: 0x00081BB4
		public ColumnFormats(object Holder)
		{
			this.m_holder = Holder;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00082BD0 File Offset: 0x00081BD0
		public ColumnFormat Add(ColumnFormat Item)
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

		// Token: 0x06000C77 RID: 3191 RVA: 0x00082C14 File Offset: 0x00081C14
		public int IndexByName(string FieldName)
		{
			int num;
			for (;;)
			{
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 0;
					if (true)
					{
					}
					num2 = 0;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_94;
					case 1:
						return num;
					case 2:
						return -1;
					case 3:
						if (string.Compare(FieldName, this[num].FieldName, true) == 0)
						{
							num2 = 1;
							continue;
						}
						num++;
						num2 = 4;
						continue;
					case 4:
						goto IL_94;
					case 5:
						if (num >= base.Count)
						{
							num2 = 2;
							continue;
						}
						num2 = 3;
						continue;
					}
					break;
					IL_94:
					num2 = 5;
				}
			}
			return num;
		}

		// Token: 0x17000145 RID: 325
		public ColumnFormat this[int Index]
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
				return base[Index] as ColumnFormat;
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
