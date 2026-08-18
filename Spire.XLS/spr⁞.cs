using System;
using System.Collections.Generic;
using System.Reflection;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.PivotTables;

// Token: 0x02000470 RID: 1136
[DefaultMember("Item")]
internal class spr\u205E : List<XlsPivotField>, IPivotCalculatedFields
{
	// Token: 0x0600458D RID: 17805 RVA: 0x002A6F4C File Offset: 0x002A5F4C
	public spr\u205E(XlsPivotTable A_0)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x0600458E RID: 17806 RVA: 0x002A6F68 File Offset: 0x002A5F68
	IPivotField IPivotCalculatedFields.ᜀ(string A_0, string A_1)
	{
		int a_ = 17;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		XlsPivotCache cache = this.ᜀ.Cache;
		XlsPivotCacheField a_2 = cache.CacheFields.ᜀ(A_0, A_1);
		XlsPivotField xlsPivotField = new XlsPivotField(a_2, this.ᜀ);
		this.ᜀ.ᜀ(AxisTypes.None, xlsPivotField, true);
		int count = this.ᜀ.DataFields.Count;
		RecordTableEnumerator.b("ᑆ㱈♊浌⁎㝐獒", a_) + count;
		xlsPivotField.CanDragToColumn = false;
		xlsPivotField.CanDragToPage = false;
		xlsPivotField.CanDragToRow = false;
		xlsPivotField.Axis = AxisTypes.None;
		this.ᜀ.ᜀ(true);
		return xlsPivotField;
	}

	// Token: 0x0600458F RID: 17807 RVA: 0x002A7038 File Offset: 0x002A6038
	public IPivotField ᜀ(int A_0)
	{
		int a_ = 7;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 >= base.Count)
				{
					num = 1;
					continue;
				}
				goto IL_94;
			case 1:
				goto IL_3F;
			case 2:
				IL_3D:
				num = 0;
				continue;
			}
			if (A_0 >= 0)
			{
				if (true)
				{
				}
				num = 2;
				continue;
			}
			IL_3F:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3D;
			default:
				goto IL_55;
			}
		}
		IL_55:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("吼儾╀♂㵄", a_));
		IL_94:
		return base[A_0];
	}

	// Token: 0x06004590 RID: 17808 RVA: 0x002A70E0 File Offset: 0x002A60E0
	IPivotField IPivotCalculatedFields.ᜀ(string A_0)
	{
		for (;;)
		{
			List<XlsPivotField>.Enumerator enumerator = base.GetEnumerator();
			IPivotField result;
			try
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_AC;
					case 2:
					{
						if (!enumerator.MoveNext())
						{
							num = 3;
							continue;
						}
						XlsPivotField xlsPivotField = enumerator.Current;
						num = 6;
						continue;
					}
					case 3:
						num = 1;
						continue;
					case 4:
						goto IL_A2;
					case 5:
					{
						XlsPivotField xlsPivotField;
						result = xlsPivotField;
						num = 4;
						continue;
					}
					case 6:
					{
						XlsPivotField xlsPivotField;
						if (xlsPivotField.Name == A_0)
						{
							num = 5;
							continue;
						}
						break;
					}
					}
					IL_55:
					num = 2;
					continue;
					goto IL_55;
				}
				IL_A2:
				return result;
				IL_AC:
				goto IL_09;
			}
			finally
			{
				if (true)
				{
				}
				((IDisposable)enumerator).Dispose();
			}
			return result;
			IL_09:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_1F;
			}
		}
		IL_1F:
		if (false)
		{
		}
		return null;
	}

	// Token: 0x04001FC0 RID: 8128
	private XlsPivotTable ᜀ;
}
