using System;
using System.Collections.Generic;

namespace Spire.Xls.Core.Spreadsheet.Tables
{
	// Token: 0x02000179 RID: 377
	public class ListObjectCollection : List<IListObject>, IListObjects
	{
		// Token: 0x060011FC RID: 4604 RVA: 0x000B0264 File Offset: 0x000AF264
		public IListObject Create(string name, IXLSRange range)
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
			range = this.ᜀ(range);
			spr\u1C4A spr_u1C4A = new spr\u1C4A(name, range, base.Count + 2);
			spr_u1C4A.ᜂ(name);
			spr_u1C4A.ᜀ(range);
			XlsWorkbook xlsWorkbook = range.Worksheet.Workbook as XlsWorkbook;
			spr_u1C4A.ᜁ(++xlsWorkbook.MaxTableIndex);
			base.Add(spr_u1C4A);
			return spr_u1C4A;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x000B02F8 File Offset: 0x000AF2F8
		private IXLSRange ᜀ(IXLSRange A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_2F:
					int row = A_0.Row;
					for (;;)
					{
						IL_36:
						int num = 4;
						for (;;)
						{
							IWorksheet worksheet;
							int column;
							switch (num)
							{
							case 0:
							{
								IXLSRange destination = worksheet[row + 2, column];
								IXLSRange allocatedRange;
								XlsRange xlsRange = (XlsRange)worksheet[row + 1, column, allocatedRange.LastRow, A_0.LastColumn];
								xlsRange.MoveTo(destination);
								num = 2;
								continue;
							}
							case 1:
								return A_0;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_36;
								default:
									if (false)
									{
									}
									goto IL_5E;
								}
								break;
							case 3:
							{
								worksheet = A_0.Worksheet;
								IXLSRange allocatedRange = worksheet.AllocatedRange;
								column = A_0.Column;
								num = 5;
								continue;
							}
							case 4:
								if (row == A_0.LastRow)
								{
									if (true)
									{
									}
									num = 3;
									continue;
								}
								return A_0;
							case 5:
							{
								IXLSRange allocatedRange;
								if (allocatedRange.LastRow > A_0.LastRow)
								{
									num = 0;
									continue;
								}
								goto IL_5E;
							}
							}
							goto IL_2F;
							IL_5E:
							A_0 = worksheet[row, column, row + 1, A_0.LastColumn];
							num = 1;
						}
					}
				}
				return A_0;
			}
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x000B0430 File Offset: 0x000AF430
		internal ListObjectCollection ᜀ(XlsWorksheet A_0, Dictionary<string, string> A_1)
		{
			switch (0)
			{
			default:
			{
				ListObjectCollection listObjectCollection;
				for (;;)
				{
					listObjectCollection = new ListObjectCollection();
					XlsWorkbook parentWorkbook = A_0.ParentWorkbook;
					int num = 0;
					int count = base.Count;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (false)
								{
								}
								goto IL_6E;
							}
							break;
						case 1:
							return listObjectCollection;
						case 2:
						{
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							if (true)
							{
							}
							spr\u1C4A spr_u1C4A = (spr\u1C4A)base[num];
							spr_u1C4A = spr_u1C4A.ᜀ(A_0, A_1);
							spr_u1C4A.ᜁ(++parentWorkbook.MaxTableIndex);
							listObjectCollection.Add(spr_u1C4A);
							num++;
							num2 = 3;
							continue;
						}
						case 3:
							goto IL_6E;
						}
						break;
						IL_6E:
						num2 = 2;
					}
				}
				return listObjectCollection;
			}
			}
		}

		// Token: 0x17000655 RID: 1621
		public IListObject this[string name]
		{
			get
			{
				switch (0)
				{
				default:
				{
					IListObject result;
					for (;;)
					{
						result = null;
						int num = 0;
						int count = base.Count;
						int num2 = 5;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								return result;
							case 1:
							{
								IListObject listObject;
								result = listObject;
								num2 = 0;
								continue;
							}
							case 2:
							{
								if (num >= count)
								{
									num2 = 6;
									continue;
								}
								IListObject listObject = base[num];
								num2 = 3;
								continue;
							}
							case 3:
							{
								IListObject listObject;
								if (listObject.Name == name)
								{
									num2 = 1;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								default:
									if (false)
									{
									}
									num++;
									num2 = 4;
									continue;
								}
								break;
							}
							case 4:
								goto IL_BB;
							case 5:
								goto IL_BB;
							case 6:
								return result;
							}
							break;
							IL_BB:
							if (true)
							{
							}
							num2 = 2;
						}
					}
					return result;
				}
				}
			}
		}
	}
}
