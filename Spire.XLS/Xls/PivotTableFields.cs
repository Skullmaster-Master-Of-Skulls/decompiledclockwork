using System;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.PivotTables;

namespace Spire.Xls
{
	// Token: 0x02000100 RID: 256
	public class PivotTableFields : CollectionExtended<PivotField>, IPivotFields
	{
		// Token: 0x170003EE RID: 1006
		IPivotField IPivotFields.this[int index]
		{
			get
			{
				int a_ = 4;
				int num = 3;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_92;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							if (index >= base.Count)
							{
								num = 1;
								continue;
							}
							goto IL_94;
						case 1:
							goto IL_92;
						case 2:
							num = 0;
							continue;
						}
						if (index < 0)
						{
							goto IL_5B;
						}
						num = 2;
						break;
					}
				}
				IL_5B:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("匹刻娽┿㩁", a_));
				IL_92:
				goto IL_5B;
				IL_94:
				return base.InnerList[index];
			}
		}

		// Token: 0x170003EF RID: 1007
		public IPivotField this[string name]
		{
			get
			{
				switch (0)
				{
				default:
				{
					IPivotField result;
					for (;;)
					{
						result = null;
						int num = 0;
						int count = base.Count;
						int num2 = 5;
						for (;;)
						{
							IPivotField pivotField;
							switch (num2)
							{
							case 0:
								return result;
							case 1:
								goto IL_A2;
							case 2:
								goto IL_A4;
							case 3:
								return result;
							case 4:
								if (pivotField.Name == name)
								{
									num2 = 1;
									continue;
								}
								num++;
								if (true)
								{
								}
								num2 = 2;
								continue;
							case 5:
								goto IL_A4;
							case 6:
								if (num < count)
								{
									pivotField = base[num];
									num2 = 4;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_A2;
								default:
									if (false)
									{
									}
									num2 = 3;
									continue;
								}
								break;
							}
							break;
							IL_A2:
							result = pivotField;
							num2 = 0;
							continue;
							IL_A4:
							num2 = 6;
						}
					}
					return result;
				}
				}
			}
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00071FF0 File Offset: 0x00070FF0
		internal PivotTableFields(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 10;
			base..ctor(A_0, A_1);
			this.ᜀ = (base.FindParent(typeof(XlsPivotTable)) as XlsPivotTable);
			if (this.ᜀ == null)
			{
				throw new ArgumentException(RecordTableEnumerator.b("〿⍁㙃⍅♇㹉", a_));
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00072048 File Offset: 0x00071048
		public PivotTableFields(XlsPivotTable table) : this(table.AppImplementation, table)
		{
			XlsPivotCache cache = table.Cache;
			sprᾷ sprᾷ = cache.CacheFields;
			int i = 0;
			int count = sprᾷ.Count;
			while (i < count)
			{
				this.ᜀ(sprᾷ.ᜀ(i), table.Workbook);
				i++;
			}
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0007209C File Offset: 0x0007109C
		private new void ᜀ(XlsPivotCacheField A_0, XlsWorkbook A_1)
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
			PivotField item = new PivotField(A_0, this.ᜀ);
			base.Add(item);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000720EC File Offset: 0x000710EC
		public override object Clone(object parent)
		{
			int a_ = 9;
			XlsPivotTable xlsPivotTable;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				xlsPivotTable = (parent as XlsPivotTable);
				if (xlsPivotTable == null)
				{
					if (true)
					{
					}
					throw new ArgumentException(RecordTableEnumerator.b("伾⁀ㅂ⁄⥆㵈", a_));
				}
				break;
			}
			new PivotTableFields(xlsPivotTable);
			return base.Clone(parent);
		}

		// Token: 0x040009EA RID: 2538
		private float \u25D9\u008D\u0084\u00B0;

		// Token: 0x040009EB RID: 2539
		private long[] \u2460\u008E\u0091\u00A8;

		// Token: 0x040009EC RID: 2540
		private new XlsPivotTable ᜀ;
	}
}
