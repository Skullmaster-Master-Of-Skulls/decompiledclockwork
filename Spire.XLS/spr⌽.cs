using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Spire.Xls;
using Spire.Xls.Calculation;
using Spire.Xls.Collections;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002E1 RID: 737
[DefaultMember("Item")]
internal class spr\u233D : CollectionExtended<IWorksheet>, IWorksheetGroup, ICloneParent
{
	// Token: 0x06002D20 RID: 11552 RVA: 0x001973A8 File Offset: 0x001963A8
	public new void ᜁ(XlsRange.CellValueChangedEventHandler A_0)
	{
		for (;;)
		{
			XlsRange.CellValueChangedEventHandler cellValueChangedEventHandler = this.ᜆ;
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						goto IL_80;
					}
					break;
				case 1:
					goto IL_2D;
				case 2:
					goto IL_52;
				}
				break;
				IL_2D:
				XlsRange.CellValueChangedEventHandler cellValueChangedEventHandler2 = cellValueChangedEventHandler;
				XlsRange.CellValueChangedEventHandler value = (XlsRange.CellValueChangedEventHandler)Delegate.Combine(cellValueChangedEventHandler2, A_0);
				cellValueChangedEventHandler = Interlocked.CompareExchange<XlsRange.CellValueChangedEventHandler>(ref this.ᜆ, value, cellValueChangedEventHandler2);
				num = 2;
				continue;
				IL_52:
				if (cellValueChangedEventHandler != cellValueChangedEventHandler2)
				{
					goto IL_2D;
				}
				num = 0;
			}
		}
		IL_80:
		if (false)
		{
		}
	}

	// Token: 0x06002D21 RID: 11553 RVA: 0x0019743C File Offset: 0x0019643C
	public new void ᜀ(XlsRange.CellValueChangedEventHandler A_0)
	{
		for (;;)
		{
			XlsRange.CellValueChangedEventHandler cellValueChangedEventHandler = this.ᜆ;
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_2D;
				case 1:
					goto IL_52;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						goto IL_80;
					}
					break;
				}
				break;
				IL_2D:
				XlsRange.CellValueChangedEventHandler cellValueChangedEventHandler2 = cellValueChangedEventHandler;
				XlsRange.CellValueChangedEventHandler value = (XlsRange.CellValueChangedEventHandler)Delegate.Remove(cellValueChangedEventHandler2, A_0);
				cellValueChangedEventHandler = Interlocked.CompareExchange<XlsRange.CellValueChangedEventHandler>(ref this.ᜆ, value, cellValueChangedEventHandler2);
				num = 1;
				continue;
				IL_52:
				if (cellValueChangedEventHandler != cellValueChangedEventHandler2)
				{
					goto IL_2D;
				}
				num = 2;
			}
		}
		IL_80:
		if (false)
		{
		}
	}

	// Token: 0x06002D22 RID: 11554 RVA: 0x001974D0 File Offset: 0x001964D0
	internal spr\u233D(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
		this.ᜂ();
		base.Inserted += this.ᜁ;
		base.Removing += this.ᜀ;
		base.Clearing += this.ᜀ;
		IWorksheets worksheets = this.ᜀ.Worksheets;
		int i = 0;
		int count = worksheets.Count;
		while (i < count)
		{
			IWorksheet worksheet = worksheets[i];
			if (worksheet.IsSelected)
			{
				base.Add(worksheet);
			}
			i++;
		}
	}

	// Token: 0x06002D23 RID: 11555 RVA: 0x00197560 File Offset: 0x00196560
	private new void ᜂ()
	{
		int a_ = 1;
		this.ᜀ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
		if (this.ᜀ == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䜶堸䤺堼儾㕀", a_), RecordTableEnumerator.b("朶堸䤺堼儾㕀捂⩄╆⍈⹊⹌㭎煐げ㑔㥖㝘㑚⥜罞͠٢䕤Ŧ٨Ṫͬ୮彰", a_));
		}
	}

	// Token: 0x06002D24 RID: 11556 RVA: 0x001975EC File Offset: 0x001965EC
	public new int ᜂ(ITabSheet A_0)
	{
		int a_ = 10;
		int num = 13;
		int result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_E1;
			case 1:
				if (A_0 is IWorksheet)
				{
					num = 14;
					continue;
				}
				result = -1;
				num = 11;
				continue;
			case 2:
				goto IL_60;
			case 3:
				goto IL_A3;
			case 4:
				return -1;
			case 5:
				if (A_0.IsSelected)
				{
					num = 7;
					continue;
				}
				goto IL_16B;
			case 6:
				if (this.ᜀ.Loading)
				{
					goto IL_16B;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1CB;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 7:
				goto IL_1CB;
			case 8:
				if (A_0.Workbook != this.ᜀ)
				{
					num = 3;
					continue;
				}
				num = 5;
				continue;
			case 9:
				goto IL_C8;
			case 10:
				this.ᜀ.SetActiveWorksheet(A_0 as XlsWorksheetBase);
				num = 0;
				continue;
			case 11:
				goto IL_10E;
			case 12:
				if (((XlsWorksheetBase)A_0).WindowTwo.ᜃ())
				{
					if (true)
					{
					}
					num = 10;
					continue;
				}
				goto IL_E1;
			case 14:
				base.Add(A_0 as IWorksheet);
				result = base.Count - 1;
				num = 9;
				continue;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 8;
			continue;
			IL_E1:
			num = 1;
			continue;
			IL_16B:
			num = 12;
			continue;
			IL_1CB:
			num = 6;
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("㌿⩁⅃⍅㱇", a_));
		IL_A3:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㌿⩁⅃⍅㱇", a_));
		IL_C8:
		IL_10E:
		return result;
	}

	// Token: 0x06002D25 RID: 11557 RVA: 0x001977CC File Offset: 0x001967CC
	public new void ᜁ(string A_0)
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
	}

	// Token: 0x06002D26 RID: 11558 RVA: 0x00197808 File Offset: 0x00196808
	public new void ᜀ(Stream A_0)
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
	}

	// Token: 0x06002D27 RID: 11559 RVA: 0x00197844 File Offset: 0x00196844
	public new void ᜀ(string A_0, HTMLOptions A_1)
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
	}

	// Token: 0x06002D28 RID: 11560 RVA: 0x00197880 File Offset: 0x00196880
	public new void ᜀ(Stream A_0, HTMLOptions A_1)
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
	}

	// Token: 0x06002D29 RID: 11561 RVA: 0x001978BC File Offset: 0x001968BC
	public new void ᜀ(ITabSheet A_0)
	{
		int a_ = 4;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				base.Remove(A_0 as IWorksheet);
				XlsWorksheetBase xlsWorksheetBase = base.List[0] as XlsWorksheetBase;
				this.ᜀ.SetActiveWorksheet(xlsWorksheetBase);
				base.AppImplementation.ᜀ(xlsWorksheetBase);
				num = 3;
				continue;
			}
			case 2:
				if (base.Count > 1)
				{
					num = 0;
					continue;
				}
				return;
			case 3:
				return;
			case 4:
				goto IL_40;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 4;
			}
			else
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
		}
		IL_40:
		throw new ArgumentNullException(RecordTableEnumerator.b("䤹吻嬽┿㙁", a_));
	}

	// Token: 0x06002D2A RID: 11562 RVA: 0x001979B0 File Offset: 0x001969B0
	public new void ᜁ(ITabSheet A_0)
	{
		int a_ = 5;
		if (A_0 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䠺唼娾⑀㝂", a_));
		}
		base.Clear();
		this.ᜂ(A_0);
	}

	// Token: 0x06002D2B RID: 11563 RVA: 0x00197A1C File Offset: 0x00196A1C
	private new void ᜁ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜃ = new sprᳺ((spr\u2158)base.ReservedHandle, this);
	}

	// Token: 0x06002D2C RID: 11564 RVA: 0x00197A70 File Offset: 0x00196A70
	public bool ᝀ()
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
		return base.Count == 0;
	}

	// Token: 0x06002D2D RID: 11565 RVA: 0x00197AB4 File Offset: 0x00196AB4
	public XlsWorkbook \u1733()
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
		return this.ᜀ;
	}

	// Token: 0x06002D2E RID: 11566 RVA: 0x00197AF8 File Offset: 0x00196AF8
	public FormulaEngine \u1735()
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
		return this.ᜅ;
	}

	// Token: 0x06002D2F RID: 11567 RVA: 0x00197B3C File Offset: 0x00196B3C
	public new void ᜀ(FormulaEngine A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x06002D30 RID: 11568 RVA: 0x00197B80 File Offset: 0x00196B80
	public object ᜉ(int A_0, int A_1)
	{
		if (true)
		{
		}
		IXLSRange ixlsrange = this.\u170D(A_0, A_1);
		if (ixlsrange.HasFormula)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			return ixlsrange.Formula;
		}
		return ixlsrange.Value;
	}

	// Token: 0x06002D31 RID: 11569 RVA: 0x00197BDC File Offset: 0x00196BDC
	public new void ᜀ(object A_0, int A_1, int A_2)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
				goto IL_4F;
			}
			if (A_0 == null)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 2;
				continue;
			}
			IL_4F:
			this.ᜀ(A_1, A_2, A_0.ToString());
			if (true)
			{
			}
			num = 0;
		}
	}

	// Token: 0x06002D32 RID: 11570 RVA: 0x00197C58 File Offset: 0x00196C58
	public new void ᜀ(ValueChangedEventHandler A_0)
	{
		for (;;)
		{
			IL_3A:
			if (true)
			{
			}
			ValueChangedEventHandler valueChangedEventHandler = this.ᜇ;
			int num = 1;
			for (;;)
			{
				ValueChangedEventHandler valueChangedEventHandler2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						if (valueChangedEventHandler == valueChangedEventHandler2)
						{
							num = 2;
							continue;
						}
						goto IL_53;
					case 1:
						goto IL_53;
					case 2:
						return;
					}
					goto IL_3A;
				}
				IL_53:
				valueChangedEventHandler2 = valueChangedEventHandler;
				ValueChangedEventHandler value = (ValueChangedEventHandler)Delegate.Combine(valueChangedEventHandler2, A_0);
				valueChangedEventHandler = Interlocked.CompareExchange<ValueChangedEventHandler>(ref this.ᜇ, value, valueChangedEventHandler2);
				num = 0;
			}
		}
	}

	// Token: 0x06002D33 RID: 11571 RVA: 0x00197CF0 File Offset: 0x00196CF0
	public new void ᜁ(ValueChangedEventHandler A_0)
	{
		for (;;)
		{
			IL_3A:
			ValueChangedEventHandler valueChangedEventHandler = this.ᜇ;
			int num = 2;
			for (;;)
			{
				ValueChangedEventHandler valueChangedEventHandler2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						if (valueChangedEventHandler == valueChangedEventHandler2)
						{
							num = 1;
							continue;
						}
						goto IL_4B;
					case 1:
						goto IL_7C;
					case 2:
						goto IL_4B;
					}
					goto IL_3A;
				}
				IL_4B:
				valueChangedEventHandler2 = valueChangedEventHandler;
				ValueChangedEventHandler value = (ValueChangedEventHandler)Delegate.Remove(valueChangedEventHandler2, A_0);
				valueChangedEventHandler = Interlocked.CompareExchange<ValueChangedEventHandler>(ref this.ᜇ, value, valueChangedEventHandler2);
				num = 0;
			}
		}
		IL_7C:
		if (true)
		{
		}
	}

	// Token: 0x06002D34 RID: 11572 RVA: 0x00197D84 File Offset: 0x00196D84
	public void ᜄ(int A_0, int A_1, string A_2)
	{
		int num = 0;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 1:
			{
				ValueChangedEventArgs e = new ValueChangedEventArgs(A_0, A_1, A_2);
				this.ᜇ(this, e);
				num = 2;
				continue;
			}
			case 2:
				return;
			}
			IL_2E:
			if (this.ᜇ == null)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 1;
				continue;
			}
			goto IL_2E;
		}
	}

	// Token: 0x06002D35 RID: 11573 RVA: 0x00197E10 File Offset: 0x00196E10
	public IAutoFilters ᝁ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D36 RID: 11574 RVA: 0x00197E50 File Offset: 0x00196E50
	public IWorkbook ᜠ()
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
		return this.ᜀ;
	}

	// Token: 0x06002D37 RID: 11575 RVA: 0x00197E94 File Offset: 0x00196E94
	public IXLSRange[] ᜫ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return null;
	}

	// Token: 0x06002D38 RID: 11576 RVA: 0x00197ED0 File Offset: 0x00196ED0
	public ViewMode ᜰ()
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
		return this.ᜄ;
	}

	// Token: 0x06002D39 RID: 11577 RVA: 0x00197F14 File Offset: 0x00196F14
	public new void ᜀ(ViewMode A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06002D3A RID: 11578 RVA: 0x00197F58 File Offset: 0x00196F58
	public bool ᝃ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_2F:
				int num;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_7D:
					num++;
					num2 = 2;
					break;
				case 1:
					goto IL_4F;
				default:
					goto IL_4F;
				}
				bool displayPageBreaks2;
				int count;
				IList<IWorksheet> innerList;
				for (;;)
				{
					IL_10:
					switch (num2)
					{
					case 0:
						goto IL_BF;
					case 1:
					{
						bool displayPageBreaks;
						if (displayPageBreaks != displayPageBreaks2)
						{
							num2 = 4;
							continue;
						}
						goto IL_7D;
					}
					case 2:
						goto IL_BF;
					case 3:
					{
						if (true)
						{
						}
						if (num >= count)
						{
							num2 = 5;
							continue;
						}
						IWorksheet worksheet = innerList[num];
						bool displayPageBreaks = worksheet.DisplayPageBreaks;
						num2 = 1;
						continue;
					}
					case 4:
						return false;
					case 5:
						return displayPageBreaks2;
					}
					goto IL_2F;
					IL_BF:
					num2 = 3;
				}
				IL_4F:
				if (false)
				{
				}
				innerList = base.InnerList;
				displayPageBreaks2 = innerList[0].DisplayPageBreaks;
				num = 1;
				count = innerList.Count;
				num2 = 0;
				goto IL_10;
			}
			return false;
		}
	}

	// Token: 0x06002D3B RID: 11579 RVA: 0x0019804C File Offset: 0x0019704C
	public void ᜊ(bool A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_55:
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 2;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							IWorksheet worksheet = innerList[num];
							worksheet.DisplayPageBreaks = A_0;
							num++;
							goto IL_9B;
						}
						case 1:
							goto IL_70;
						case 2:
							goto IL_70;
						case 3:
							return;
						}
						goto IL_55;
						IL_70:
						num2 = 0;
						continue;
					}
					IL_9B:
					num2 = 1;
				}
			}
			return;
		}
	}

	// Token: 0x06002D3C RID: 11580 RVA: 0x00198104 File Offset: 0x00197104
	public bool ᜄ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D3D RID: 11581 RVA: 0x00198144 File Offset: 0x00197144
	public void ᜈ(bool A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D3E RID: 11582 RVA: 0x00198184 File Offset: 0x00197184
	public int \u1717()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return -1;
	}

	// Token: 0x06002D3F RID: 11583 RVA: 0x001981C0 File Offset: 0x001971C0
	public int ᜢ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return -1;
	}

	// Token: 0x06002D40 RID: 11584 RVA: 0x001981FC File Offset: 0x001971FC
	public bool ᜭ()
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
		return false;
	}

	// Token: 0x06002D41 RID: 11585 RVA: 0x00198238 File Offset: 0x00197238
	public bool \u1715()
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
		return false;
	}

	// Token: 0x06002D42 RID: 11586 RVA: 0x00198274 File Offset: 0x00197274
	public IXLSRange[] ᜣ()
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
		return null;
	}

	// Token: 0x06002D43 RID: 11587 RVA: 0x001982B0 File Offset: 0x001972B0
	public string ᝅ()
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
		return null;
	}

	// Token: 0x06002D44 RID: 11588 RVA: 0x001982EC File Offset: 0x001972EC
	public void ᜃ(string A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002D45 RID: 11589 RVA: 0x0019832C File Offset: 0x0019732C
	public INameRanges ᜃ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D46 RID: 11590 RVA: 0x0019836C File Offset: 0x0019736C
	public string ᜤ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return null;
	}

	// Token: 0x06002D47 RID: 11591 RVA: 0x001983A8 File Offset: 0x001973A8
	public new void ᜂ(string A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D48 RID: 11592 RVA: 0x001983E8 File Offset: 0x001973E8
	public IPageSetup \u173A()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_6E;
			case 2:
				this.ᜁ = new spr\u207E(base.ReservedHandle, this);
				num = 1;
				continue;
			}
			IL_26:
			if (this.ᜁ != null)
			{
				goto IL_78;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 2;
				continue;
			}
			goto IL_26;
		}
		IL_6E:
		if (true)
		{
		}
		IL_78:
		return this.ᜁ;
	}

	// Token: 0x06002D49 RID: 11593 RVA: 0x00198474 File Offset: 0x00197474
	public IXLSRange ᝇ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return this.ᝇ();
	}

	// Token: 0x06002D4A RID: 11594 RVA: 0x001984B8 File Offset: 0x001974B8
	public IXLSRange[] \u171E()
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
		return null;
	}

	// Token: 0x06002D4B RID: 11595 RVA: 0x001984F4 File Offset: 0x001974F4
	public IXLSRange[] \u1714()
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
		return null;
	}

	// Token: 0x06002D4C RID: 11596 RVA: 0x00198530 File Offset: 0x00197530
	public double ᜪ()
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
		return 0.0;
	}

	// Token: 0x06002D4D RID: 11597 RVA: 0x00198574 File Offset: 0x00197574
	public new void ᜁ(double A_0)
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
	}

	// Token: 0x06002D4E RID: 11598 RVA: 0x001985B0 File Offset: 0x001975B0
	public bool ᜏ()
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
		return false;
	}

	// Token: 0x06002D4F RID: 11599 RVA: 0x001985EC File Offset: 0x001975EC
	public void ᜅ(bool A_0)
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
	}

	// Token: 0x06002D50 RID: 11600 RVA: 0x00198628 File Offset: 0x00197628
	public double ᜆ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return 0.0;
	}

	// Token: 0x06002D51 RID: 11601 RVA: 0x0019866C File Offset: 0x0019766C
	public void ᜃ(double A_0)
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
	}

	// Token: 0x06002D52 RID: 11602 RVA: 0x001986A8 File Offset: 0x001976A8
	public ExcelSheetType \u173F()
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
		throw new NotImplementedException();
	}

	// Token: 0x06002D53 RID: 11603 RVA: 0x001986E8 File Offset: 0x001976E8
	public XlsRange ᝆ()
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				int firstColumn;
				int firstRow;
				int lastRow;
				int lastColumn;
				switch (num)
				{
				case 0:
					if (this.ᜂ.Column == firstColumn)
					{
						num = 6;
						continue;
					}
					goto IL_10B;
				case 2:
					if (this.ᜂ.Row != firstRow)
					{
						goto IL_10B;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F5;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 3:
					if (this.ᜂ.LastRow == lastRow)
					{
						num = 8;
						continue;
					}
					goto IL_10B;
				case 4:
					goto IL_65;
				case 5:
					num = 0;
					continue;
				case 6:
					num = 3;
					continue;
				case 7:
					goto IL_10B;
				case 8:
					num = 12;
					continue;
				case 9:
					goto IL_F5;
				case 10:
					num = 2;
					continue;
				case 11:
					goto IL_12E;
				case 12:
					if (this.ᜂ.LastColumn != lastColumn)
					{
						num = 7;
						continue;
					}
					goto IL_1B7;
				}
				if (this.ᝀ())
				{
					num = 4;
					continue;
				}
				XlsWorksheet xlsWorksheet = (XlsWorksheet)base.InnerList[0];
				firstRow = xlsWorksheet.FirstRow;
				lastRow = xlsWorksheet.LastRow;
				firstColumn = xlsWorksheet.FirstColumn;
				lastColumn = xlsWorksheet.LastColumn;
				num = 9;
				continue;
				IL_F5:
				if (this.ᜂ != null)
				{
					num = 10;
					continue;
				}
				IL_10B:
				this.ᜂ = new spr\u1CCF(base.ReservedHandle, this, firstRow, firstColumn, lastRow, lastColumn);
				num = 11;
			}
			IL_65:
			return null;
			IL_12E:
			IL_1B7:
			return this.ᜂ as XlsRange;
		}
		}
	}

	// Token: 0x06002D54 RID: 11604 RVA: 0x001988B8 File Offset: 0x001978B8
	public int ᜅ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_2F:
				int num;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_7D:
					num++;
					num2 = 4;
					break;
				case 1:
					goto IL_4F;
				default:
					goto IL_4F;
				}
				int zoom2;
				int count;
				IList<IWorksheet> innerList;
				for (;;)
				{
					IL_10:
					switch (num2)
					{
					case 0:
					{
						int zoom;
						if (zoom != zoom2)
						{
							num2 = 2;
							continue;
						}
						goto IL_7D;
					}
					case 1:
						return zoom2;
					case 2:
						return int.MinValue;
					case 3:
						goto IL_CB;
					case 4:
						if (true)
						{
						}
						goto IL_CB;
					case 5:
					{
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						IWorksheet worksheet = innerList[num];
						int zoom = worksheet.Zoom;
						num2 = 0;
						continue;
					}
					}
					goto IL_2F;
					IL_CB:
					num2 = 5;
				}
				IL_4F:
				if (false)
				{
				}
				innerList = base.InnerList;
				zoom2 = innerList[0].Zoom;
				num = 1;
				count = innerList.Count;
				num2 = 3;
				goto IL_10;
			}
			return int.MinValue;
		}
	}

	// Token: 0x06002D55 RID: 11605 RVA: 0x001989B0 File Offset: 0x001979B0
	public void \u1712(int A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_4B:
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 0;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_66;
						case 1:
							return;
						case 2:
							goto IL_66;
						case 3:
						{
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							IWorksheet worksheet = innerList[num];
							worksheet.Zoom = A_0;
							num++;
							goto IL_9B;
						}
						}
						goto IL_4B;
						IL_66:
						num2 = 3;
						continue;
					}
					IL_9B:
					num2 = 2;
				}
			}
			return;
		}
	}

	// Token: 0x06002D56 RID: 11606 RVA: 0x00198A68 File Offset: 0x00197A68
	public WorksheetVisibility ᜉ()
	{
		switch (0)
		{
		default:
		{
			WorksheetVisibility visibility;
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				visibility = innerList[0].Visibility;
				int num = 1;
				int count = innerList.Count;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						WorksheetVisibility visibility2;
						if (visibility2 != visibility)
						{
							num2 = 4;
							continue;
						}
						num++;
						num2 = 2;
						continue;
					}
					case 1:
					{
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						IWorksheet worksheet = innerList[num];
						WorksheetVisibility visibility2 = worksheet.Visibility;
						num2 = 0;
						continue;
					}
					case 2:
						goto IL_B5;
					case 3:
						goto IL_DB;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B5;
						default:
							goto IL_7C;
						}
						break;
					case 5:
						goto IL_B5;
					}
					break;
					IL_B5:
					num2 = 1;
				}
			}
			IL_7C:
			if (false)
			{
			}
			return WorksheetVisibility.Visible;
			IL_DB:
			if (true)
			{
			}
			return visibility;
		}
		}
	}

	// Token: 0x06002D57 RID: 11607 RVA: 0x00198B5C File Offset: 0x00197B5C
	public new void ᜀ(WorksheetVisibility A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				for (;;)
				{
					IList<IWorksheet> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							if (true)
							{
							}
							IWorksheet worksheet = innerList[num];
							worksheet.Visibility = A_0;
							num++;
							num2 = 2;
							continue;
						}
						case 1:
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								goto IL_42;
							}
							break;
						case 3:
							goto IL_42;
						}
						break;
						IL_42:
						num2 = 0;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06002D58 RID: 11608 RVA: 0x00198C10 File Offset: 0x00197C10
	public int ᜋ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D59 RID: 11609 RVA: 0x00198C50 File Offset: 0x00197C50
	public void \u1716(int A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D5A RID: 11610 RVA: 0x00198C90 File Offset: 0x00197C90
	public int \u171D()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002D5B RID: 11611 RVA: 0x00198CD0 File Offset: 0x00197CD0
	public void ᜎ(int A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D5C RID: 11612 RVA: 0x00198D10 File Offset: 0x00197D10
	public int ᜈ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D5D RID: 11613 RVA: 0x00198D50 File Offset: 0x00197D50
	public void ᜐ(int A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002D5E RID: 11614 RVA: 0x00198D90 File Offset: 0x00197D90
	public int ᝋ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D5F RID: 11615 RVA: 0x00198DD0 File Offset: 0x00197DD0
	public void ᜇ(int A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D60 RID: 11616 RVA: 0x00198E10 File Offset: 0x00197E10
	public int ᜥ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002D61 RID: 11617 RVA: 0x00198E50 File Offset: 0x00197E50
	public void ᜋ(int A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D62 RID: 11618 RVA: 0x00198E90 File Offset: 0x00197E90
	public bool \u1713()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D63 RID: 11619 RVA: 0x00198ED0 File Offset: 0x00197ED0
	public void ᜇ(bool A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002D64 RID: 11620 RVA: 0x00198F10 File Offset: 0x00197F10
	public bool \u173D()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				bool gridLinesVisible = innerList[0].GridLinesVisible;
				int num = 1;
				int count = innerList.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_BD;
					case 1:
						return gridLinesVisible;
					case 2:
					{
						bool gridLinesVisible2;
						if (gridLinesVisible2 != gridLinesVisible)
						{
							num2 = 5;
							continue;
						}
						num++;
						num2 = 3;
						continue;
					}
					case 3:
						goto IL_BD;
					case 4:
					{
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						IWorksheet worksheet = innerList[num];
						bool gridLinesVisible2 = worksheet.GridLinesVisible;
						num2 = 2;
						continue;
					}
					case 5:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BD;
						default:
							goto IL_7C;
						}
						break;
					}
					break;
					IL_BD:
					num2 = 4;
				}
			}
			IL_7C:
			if (false)
			{
			}
			return false;
		}
	}

	// Token: 0x06002D65 RID: 11621 RVA: 0x00199004 File Offset: 0x00198004
	public new void ᜁ(bool A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				for (;;)
				{
					IList<IWorksheet> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return;
						case 1:
							goto IL_42;
						case 2:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								goto IL_42;
							}
							break;
						case 3:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							IWorksheet worksheet = innerList[num];
							worksheet.GridLinesVisible = A_0;
							num++;
							num2 = 2;
							continue;
						}
						}
						break;
						IL_42:
						num2 = 3;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06002D66 RID: 11622 RVA: 0x001990B8 File Offset: 0x001980B8
	public ExcelColors \u1732()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D67 RID: 11623 RVA: 0x001990F8 File Offset: 0x001980F8
	public new void ᜀ(ExcelColors A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D68 RID: 11624 RVA: 0x00199138 File Offset: 0x00198138
	public bool ᝌ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				bool rowColumnHeadersVisible = innerList[0].RowColumnHeadersVisible;
				int num = 1;
				int count = innerList.Count;
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						bool rowColumnHeadersVisible2;
						if (rowColumnHeadersVisible2 != rowColumnHeadersVisible)
						{
							num2 = 1;
							continue;
						}
						num++;
						num2 = 2;
						continue;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B5;
						default:
							goto IL_7C;
						}
						break;
					case 2:
						goto IL_B5;
					case 3:
						return rowColumnHeadersVisible;
					case 4:
						goto IL_B5;
					case 5:
					{
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						IWorksheet worksheet = innerList[num];
						bool rowColumnHeadersVisible2 = worksheet.RowColumnHeadersVisible;
						num2 = 0;
						continue;
					}
					}
					break;
					IL_B5:
					if (true)
					{
					}
					num2 = 5;
				}
			}
			IL_7C:
			if (false)
			{
			}
			return false;
		}
	}

	// Token: 0x06002D69 RID: 11625 RVA: 0x0019922C File Offset: 0x0019822C
	public void ᜄ(bool A_0)
	{
		if (true)
		{
		}
		switch (0)
		{
		default:
			for (;;)
			{
				for (;;)
				{
					IList<IWorksheet> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								goto IL_4A;
							}
							break;
						case 1:
							return;
						case 2:
							goto IL_4A;
						case 3:
						{
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							IWorksheet worksheet = innerList[num];
							worksheet.RowColumnHeadersVisible = A_0;
							num++;
							num2 = 0;
							continue;
						}
						}
						break;
						IL_4A:
						num2 = 3;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06002D6A RID: 11626 RVA: 0x001992E0 File Offset: 0x001982E0
	public IVPageBreaks ᜨ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return null;
	}

	// Token: 0x06002D6B RID: 11627 RVA: 0x0019931C File Offset: 0x0019831C
	public IHPageBreaks ᜮ()
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
		return null;
	}

	// Token: 0x06002D6C RID: 11628 RVA: 0x00199358 File Offset: 0x00198358
	public bool ᝈ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				bool isStringsPreserved = innerList[0].IsStringsPreserved;
				int num = 1;
				int count = innerList.Count;
				int num2 = 5;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_BD;
					case 1:
					{
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						IWorksheet worksheet = innerList[num];
						bool isStringsPreserved2 = worksheet.IsStringsPreserved;
						num2 = 4;
						continue;
					}
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BD;
						default:
							goto IL_84;
						}
						break;
					case 3:
						return isStringsPreserved;
					case 4:
					{
						bool isStringsPreserved2;
						if (isStringsPreserved2 != isStringsPreserved)
						{
							num2 = 2;
							continue;
						}
						num++;
						num2 = 0;
						continue;
					}
					case 5:
						goto IL_BD;
					}
					break;
					IL_BD:
					num2 = 1;
				}
			}
			IL_84:
			if (false)
			{
			}
			return false;
		}
	}

	// Token: 0x06002D6D RID: 11629 RVA: 0x0019944C File Offset: 0x0019844C
	public void ᜋ(bool A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				for (;;)
				{
					if (true)
					{
					}
					IList<IWorksheet> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								goto IL_4A;
							}
							break;
						case 1:
							goto IL_4A;
						case 2:
							return;
						case 3:
						{
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							IWorksheet worksheet = innerList[num];
							worksheet.IsStringsPreserved = A_0;
							num++;
							num2 = 0;
							continue;
						}
						}
						break;
						IL_4A:
						num2 = 3;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06002D6E RID: 11630 RVA: 0x00199500 File Offset: 0x00198500
	public bool \u171F()
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
		return false;
	}

	// Token: 0x06002D6F RID: 11631 RVA: 0x0019953C File Offset: 0x0019853C
	public IComments \u170D()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D70 RID: 11632 RVA: 0x0019957C File Offset: 0x0019857C
	public IXLSRange \u170D(int A_0, int A_1)
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
		return null;
	}

	// Token: 0x06002D71 RID: 11633 RVA: 0x001995B8 File Offset: 0x001985B8
	public new IXLSRange ᜀ(int A_0, int A_1, int A_2, int A_3)
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
		return null;
	}

	// Token: 0x06002D72 RID: 11634 RVA: 0x001995F4 File Offset: 0x001985F4
	public IXLSRange ᜅ(string A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return null;
	}

	// Token: 0x06002D73 RID: 11635 RVA: 0x00199630 File Offset: 0x00198630
	public new IXLSRange ᜀ(string A_0, bool A_1)
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
		return null;
	}

	// Token: 0x06002D74 RID: 11636 RVA: 0x0019966C File Offset: 0x0019866C
	public IHyperLinks ᝂ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002D75 RID: 11637 RVA: 0x001996AC File Offset: 0x001986AC
	internal IXLSRange[] ᜦ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D76 RID: 11638 RVA: 0x001996EC File Offset: 0x001986EC
	public IWorksheetCustomProperties \u171C()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D77 RID: 11639 RVA: 0x0019972C File Offset: 0x0019872C
	public IMigrantRange ᜯ()
	{
		int num = 1;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_60;
				case 2:
					goto IL_52;
				}
				if (true)
				{
				}
				if (this.ᜃ == null)
				{
					num = 2;
					continue;
				}
				goto IL_6C;
			}
			IL_52:
			this.ᜁ();
			num = 0;
		}
		IL_60:
		IL_6C:
		return this.ᜃ;
	}

	// Token: 0x06002D78 RID: 11640 RVA: 0x001997AC File Offset: 0x001987AC
	public bool ᜩ()
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				int num2;
				int count;
				IList<IWorksheet> innerList;
				bool useRangesCache2;
				switch (num)
				{
				case 0:
					return false;
				case 1:
				{
					if (num2 >= count)
					{
						num = 5;
						continue;
					}
					IWorksheet worksheet = innerList[num2];
					bool useRangesCache = worksheet.UseRangesCache;
					num = 3;
					continue;
				}
				case 3:
				{
					bool useRangesCache;
					if (useRangesCache != useRangesCache2)
					{
						num = 4;
						continue;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E4;
					default:
						if (false)
						{
						}
						num2++;
						num = 6;
						continue;
					}
					break;
				}
				case 4:
					return false;
				case 5:
					return useRangesCache2;
				case 6:
					goto IL_C6;
				case 7:
					goto IL_C6;
				}
				if (base.Count == 0)
				{
					num = 0;
					continue;
				}
				goto IL_E4;
				IL_C6:
				num = 1;
				continue;
				IL_E4:
				innerList = base.InnerList;
				useRangesCache2 = innerList[0].UseRangesCache;
				num2 = 1;
				count = innerList.Count;
				num = 7;
			}
			return false;
		}
		}
	}

	// Token: 0x06002D79 RID: 11641 RVA: 0x001998CC File Offset: 0x001988CC
	public new void ᜂ(bool A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_0E:
				for (;;)
				{
					IList<IWorksheet> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							IWorksheet worksheet = innerList[num];
							worksheet.UseRangesCache = A_0;
							num++;
							num2 = 2;
							continue;
						}
						case 1:
							goto IL_42;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_0E;
							}
							if (true)
							{
							}
							if (false)
							{
							}
							goto IL_42;
						case 3:
							return;
						}
						break;
						IL_42:
						num2 = 0;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06002D7A RID: 11642 RVA: 0x00199980 File Offset: 0x00198980
	public SheetProtectionType ᜱ()
	{
		int a_ = 6;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("栻嘽⤿ㅁ摃㙅㩇╉㱋⭍≏♑ⵓ癕㱗㕙㥛ⵝ๟ൡၣ䙥᭧ὩᱫṭὯqs噵ᅷᑹ屻੽ꚅﾋ뺏", a_));
	}

	// Token: 0x06002D7B RID: 11643 RVA: 0x001999D8 File Offset: 0x001989D8
	public bool \u1719()
	{
		int a_ = 18;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("᱇≉╋㵍灏≑♓㥕⡗㽙⹛⩝ᥟ䉡c॥൧ᥩɫŭѯ剱ݳ͵ࡷ੹፻౽ꚅꂍ", a_));
	}

	// Token: 0x06002D7C RID: 11644 RVA: 0x00199A30 File Offset: 0x00198A30
	public int ᜇ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D7D RID: 11645 RVA: 0x00199A70 File Offset: 0x00198A70
	public void ᜌ(int A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002D7E RID: 11646 RVA: 0x00199AB0 File Offset: 0x00198AB0
	public int ᜬ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D7F RID: 11647 RVA: 0x00199AF0 File Offset: 0x00198AF0
	public void ᜄ(int A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D80 RID: 11648 RVA: 0x00199B30 File Offset: 0x00198B30
	public bool ᜊ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D81 RID: 11649 RVA: 0x00199B70 File Offset: 0x00198B70
	public new void ᜀ(bool A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D82 RID: 11650 RVA: 0x00199BB0 File Offset: 0x00198BB0
	public PivotTablesCollection \u171A()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D83 RID: 11651 RVA: 0x00199BF0 File Offset: 0x00198BF0
	public IListObjects ᜑ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D84 RID: 11652 RVA: 0x00199C30 File Offset: 0x00198C30
	public IOleObjects ᝊ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002D85 RID: 11653 RVA: 0x00199C70 File Offset: 0x00198C70
	public void ᜌ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D86 RID: 11654 RVA: 0x00199CB0 File Offset: 0x00198CB0
	public void ᜧ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002D87 RID: 11655 RVA: 0x00199CF0 File Offset: 0x00198CF0
	void IWorksheet.\u1734()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				for (;;)
				{
					IList<IWorksheet> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								goto IL_42;
							}
							break;
						case 1:
						{
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							IWorksheet worksheet = innerList[num];
							worksheet.Clear();
							num++;
							num2 = 0;
							continue;
						}
						case 2:
							goto IL_42;
						case 3:
							return;
						}
						break;
						IL_42:
						if (true)
						{
						}
						num2 = 1;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06002D88 RID: 11656 RVA: 0x00199DA4 File Offset: 0x00198DA4
	public void ᝐ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				for (;;)
				{
					IList<IWorksheet> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 3;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							return;
						case 1:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							IWorksheet worksheet = innerList[num];
							worksheet.ClearData();
							num++;
							num2 = 2;
							continue;
						}
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								goto IL_4A;
							}
							break;
						case 3:
							goto IL_4A;
						}
						break;
						IL_4A:
						num2 = 1;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06002D89 RID: 11657 RVA: 0x00199E58 File Offset: 0x00198E58
	public bool ᜑ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			bool flag;
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				flag = innerList[0].CheckExistence(A_0, A_1);
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_10C;
					case 1:
						goto IL_B3;
					case 2:
						return flag;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_10C;
						default:
						{
							if (false)
							{
							}
							int num2;
							int count;
							if (num2 >= count)
							{
								num = 2;
								continue;
							}
							IWorksheet worksheet = innerList[num2];
							bool flag2 = worksheet.CheckExistence(A_0, A_1);
							num = 6;
							continue;
						}
						}
						break;
					case 4:
						return flag;
					case 5:
						return false;
					case 6:
					{
						bool flag2;
						if (flag2 != flag)
						{
							num = 5;
							continue;
						}
						int num2;
						num2++;
						num = 1;
						continue;
					}
					case 7:
					{
						if (true)
						{
						}
						if (!flag)
						{
							num = 4;
							continue;
						}
						int num2 = 1;
						int count = innerList.Count;
						num = 0;
						continue;
					}
					}
					break;
					IL_B3:
					num = 3;
					continue;
					IL_10C:
					goto IL_B3;
				}
			}
			return flag;
		}
		}
	}

	// Token: 0x06002D8A RID: 11658 RVA: 0x00199F78 File Offset: 0x00198F78
	public new void ᜀ(string A_0, string A_1, bool A_2)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotImplementedException();
	}

	// Token: 0x06002D8B RID: 11659 RVA: 0x00199FB8 File Offset: 0x00198FB8
	public IXLSRanges \u173C()
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
		return null;
	}

	// Token: 0x06002D8C RID: 11660 RVA: 0x00199FF4 File Offset: 0x00198FF4
	public IMarkersDesigner ᝄ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotImplementedException();
	}

	// Token: 0x06002D8D RID: 11661 RVA: 0x0019A034 File Offset: 0x00199034
	public bool ᜏ(int A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_82:
				num = 0;
				break;
			default:
				if (false)
				{
				}
				goto IL_55;
			}
			int num2;
			int count;
			IList<IWorksheet> innerList;
			bool flag2;
			for (;;)
			{
				IL_36:
				switch (num)
				{
				case 0:
					goto IL_BE;
				case 1:
					goto IL_BE;
				case 2:
				{
					if (num2 >= count)
					{
						num = 3;
						continue;
					}
					IWorksheet worksheet = innerList[num2];
					bool flag = worksheet.IsColumnVisible(A_0);
					num = 4;
					continue;
				}
				case 3:
					goto IL_DA;
				case 4:
				{
					bool flag;
					if (flag != flag2)
					{
						num = 5;
						continue;
					}
					goto IL_7E;
				}
				case 5:
					return false;
				}
				goto IL_55;
				IL_BE:
				num = 2;
			}
			IL_7E:
			num2++;
			goto IL_82;
			IL_DA:
			if (true)
			{
			}
			return flag2;
			IL_55:
			innerList = base.InnerList;
			flag2 = innerList[0].IsColumnVisible(A_0);
			num2 = 1;
			count = innerList.Count;
			num = 1;
			goto IL_36;
		}
		}
	}

	// Token: 0x06002D8E RID: 11662 RVA: 0x0019A128 File Offset: 0x00199128
	public new void ᜁ(int A_0, bool A_1)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A1:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				goto IL_66;
			case 1:
				goto IL_66;
			case 2:
				if (num2 >= count)
				{
					num = 3;
					continue;
				}
				goto IL_7E;
			case 3:
				return;
			}
			goto IL_43;
			IL_66:
			num = 2;
		}
		return;
		IL_7E:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		((XlsWorksheet)worksheet).ShowColumn(A_0, A_1);
		num2++;
		goto IL_A1;
		IL_43:
		innerList = base.InnerList;
		num2 = 0;
		count = innerList.Count;
		num = 0;
		goto IL_2C;
	}

	// Token: 0x06002D8F RID: 11663 RVA: 0x0019A1E4 File Offset: 0x001991E4
	public void ᜄ(int A_0, int A_1)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A1:
			num = 3;
			break;
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			switch (0)
			{
			default:
				goto IL_4B;
			}
			break;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_34:
			switch (num)
			{
			case 0:
				goto IL_66;
			case 1:
				if (num2 >= count)
				{
					num = 2;
					continue;
				}
				goto IL_7E;
			case 2:
				return;
			case 3:
				goto IL_66;
			}
			goto IL_4B;
			IL_66:
			num = 1;
		}
		return;
		IL_7E:
		List<IWorksheet> innerList;
		XlsWorksheet xlsWorksheet = (XlsWorksheet)innerList[num2];
		xlsWorksheet.SetColumnWidthInPixels(A_0, A_1);
		num2++;
		goto IL_A1;
		IL_4B:
		innerList = base.InnerList;
		num2 = 0;
		count = innerList.Count;
		num = 0;
		goto IL_34;
	}

	// Token: 0x06002D90 RID: 11664 RVA: 0x0019A2A0 File Offset: 0x001992A0
	public new void ᜀ(int A_0, int A_1, int A_2)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A2:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 1:
				if (num2 >= count)
				{
					num = 3;
					continue;
				}
				goto IL_76;
			case 2:
				goto IL_5E;
			case 3:
				return;
			}
			goto IL_43;
			IL_5E:
			num = 1;
		}
		return;
		IL_76:
		if (true)
		{
		}
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		((XlsWorksheet)worksheet).SetColumnWidthInPixels(A_0, A_1, A_2);
		num2++;
		goto IL_A2;
		IL_43:
		innerList = base.InnerList;
		num2 = 0;
		count = innerList.Count;
		num = 2;
		goto IL_2C;
	}

	// Token: 0x06002D91 RID: 11665 RVA: 0x0019A35C File Offset: 0x0019935C
	public bool \u1715(int A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_82:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				goto IL_55;
			}
			int num2;
			int count;
			IList<IWorksheet> innerList;
			bool flag2;
			for (;;)
			{
				IL_36:
				switch (num)
				{
				case 0:
				{
					if (num2 >= count)
					{
						num = 3;
						continue;
					}
					IWorksheet worksheet = innerList[num2];
					bool flag = worksheet.IsRowVisible(A_0);
					num = 5;
					continue;
				}
				case 1:
					goto IL_BC;
				case 2:
					goto IL_C6;
				case 3:
					return flag2;
				case 4:
					goto IL_C6;
				case 5:
				{
					bool flag;
					if (flag != flag2)
					{
						num = 1;
						continue;
					}
					goto IL_7E;
				}
				}
				goto IL_55;
				IL_C6:
				num = 0;
			}
			IL_7E:
			num2++;
			goto IL_82;
			IL_BC:
			if (true)
			{
			}
			return false;
			IL_55:
			innerList = base.InnerList;
			flag2 = innerList[0].IsRowVisible(A_0);
			num2 = 1;
			count = innerList.Count;
			num = 4;
			goto IL_36;
		}
		}
	}

	// Token: 0x06002D92 RID: 11666 RVA: 0x0019A450 File Offset: 0x00199450
	public new void ᜀ(int A_0, bool A_1)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A1:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_4B;
			}
			break;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_2C:
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				if (num2 >= count)
				{
					num = 1;
					continue;
				}
				goto IL_7E;
			case 1:
				return;
			case 2:
				goto IL_66;
			case 3:
				goto IL_66;
			}
			goto IL_4B;
			IL_66:
			num = 0;
		}
		return;
		IL_7E:
		IList<IWorksheet> innerList;
		XlsWorksheet xlsWorksheet = innerList[num2] as XlsWorksheet;
		xlsWorksheet.ᜂ(A_0, A_1);
		num2++;
		goto IL_A1;
		IL_4B:
		innerList = base.InnerList;
		num2 = 0;
		count = innerList.Count;
		num = 3;
		goto IL_2C;
	}

	// Token: 0x06002D93 RID: 11667 RVA: 0x0019A50C File Offset: 0x0019950C
	public void \u170D(int A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A0:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_66;
			case 2:
				goto IL_66;
			case 3:
				if (num2 >= count)
				{
					num = 0;
					continue;
				}
				goto IL_7E;
			}
			goto IL_43;
			IL_66:
			num = 3;
		}
		return;
		IL_7E:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		((XlsWorksheet)worksheet).InsertRow(A_0);
		num2++;
		goto IL_A0;
		IL_43:
		if (true)
		{
		}
		innerList = base.InnerList;
		num2 = 0;
		count = innerList.Count;
		num = 2;
		goto IL_2C;
	}

	// Token: 0x06002D94 RID: 11668 RVA: 0x0019A5C4 File Offset: 0x001995C4
	public void ᜌ(int A_0, int A_1)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_8F:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				goto IL_74;
			case 1:
				goto IL_5E;
			case 2:
				goto IL_5E;
			case 3:
				if (num2 >= count)
				{
					num = 0;
					continue;
				}
				goto IL_76;
			}
			goto IL_43;
			IL_5E:
			num = 3;
		}
		IL_74:
		if (true)
		{
		}
		return;
		IL_76:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		((XlsWorksheet)worksheet).InsertRow(A_0, A_1);
		num2++;
		goto IL_8F;
		IL_43:
		innerList = base.InnerList;
		num2 = 0;
		count = innerList.Count;
		num = 2;
		goto IL_2C;
	}

	// Token: 0x06002D95 RID: 11669 RVA: 0x0019A680 File Offset: 0x00199680
	public new void ᜁ(int A_0, int A_1, InsertOptionsType A_2)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A2:
			num = 3;
			break;
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			switch (0)
			{
			default:
				goto IL_4B;
			}
			break;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_34:
			switch (num)
			{
			case 0:
				if (num2 >= count)
				{
					num = 2;
					continue;
				}
				goto IL_7E;
			case 1:
				goto IL_66;
			case 2:
				return;
			case 3:
				goto IL_66;
			}
			goto IL_4B;
			IL_66:
			num = 0;
		}
		return;
		IL_7E:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		((XlsWorksheet)worksheet).ᜄ(A_0, A_1, A_2);
		num2++;
		goto IL_A2;
		IL_4B:
		innerList = base.InnerList;
		num2 = 0;
		count = innerList.Count;
		num = 1;
		goto IL_34;
	}

	// Token: 0x06002D96 RID: 11670 RVA: 0x0019A73C File Offset: 0x0019973C
	public void \u1713(int A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A0:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 1:
				goto IL_5E;
			case 2:
				goto IL_74;
			case 3:
				if (num2 >= count)
				{
					num = 2;
					continue;
				}
				goto IL_7E;
			}
			goto IL_43;
			IL_5E:
			num = 3;
		}
		IL_74:
		if (true)
		{
		}
		return;
		IL_7E:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		((XlsWorksheet)worksheet).InsertColumn(A_0);
		num2++;
		goto IL_A0;
		IL_43:
		innerList = base.InnerList;
		num2 = 0;
		count = innerList.Count;
		num = 1;
		goto IL_2C;
	}

	// Token: 0x06002D97 RID: 11671 RVA: 0x0019A7F4 File Offset: 0x001997F4
	public void ᜏ(int A_0, int A_1)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A1:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				if (num2 >= count)
				{
					num = 3;
					continue;
				}
				goto IL_76;
			case 1:
				goto IL_5E;
			case 2:
				goto IL_5E;
			case 3:
				return;
			}
			goto IL_43;
			IL_5E:
			num = 0;
		}
		return;
		IL_76:
		if (true)
		{
		}
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		((XlsWorksheet)worksheet).InsertColumn(A_0, A_1);
		num2++;
		goto IL_A1;
		IL_43:
		innerList = base.InnerList;
		num2 = 0;
		count = innerList.Count;
		num = 2;
		goto IL_2C;
	}

	// Token: 0x06002D98 RID: 11672 RVA: 0x0019A8B0 File Offset: 0x001998B0
	public new void ᜀ(int A_0, int A_1, InsertOptionsType A_2)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A2:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			switch (0)
			{
			default:
				goto IL_4B;
			}
			break;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_34:
			switch (num)
			{
			case 0:
				goto IL_66;
			case 1:
				return;
			case 2:
				if (num2 >= count)
				{
					num = 1;
					continue;
				}
				goto IL_7E;
			case 3:
				goto IL_66;
			}
			goto IL_4B;
			IL_66:
			num = 2;
		}
		return;
		IL_7E:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		((XlsWorksheet)worksheet).ᜃ(A_0, A_1, A_2);
		num2++;
		goto IL_A2;
		IL_4B:
		innerList = base.InnerList;
		num2 = 0;
		count = innerList.Count;
		num = 3;
		goto IL_34;
	}

	// Token: 0x06002D99 RID: 11673 RVA: 0x0019A96C File Offset: 0x0019996C
	public new void ᜁ(int A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_89:
			num = 3;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				goto IL_74;
			case 1:
				if (num2 >= count)
				{
					num = 0;
					continue;
				}
				goto IL_76;
			case 2:
				goto IL_5E;
			case 3:
				goto IL_5E;
			}
			goto IL_43;
			IL_5E:
			num = 1;
		}
		IL_74:
		if (true)
		{
		}
		return;
		IL_76:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		worksheet.DeleteRow(A_0);
		num2++;
		goto IL_89;
		IL_43:
		innerList = base.InnerList;
		num2 = 0;
		count = innerList.Count;
		num = 2;
		goto IL_2C;
	}

	// Token: 0x06002D9A RID: 11674 RVA: 0x0019AA20 File Offset: 0x00199A20
	public new void ᜀ(int A_0, int A_1)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A1:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 1:
				goto IL_74;
			case 2:
				goto IL_5E;
			case 3:
				if (num2 >= count)
				{
					num = 1;
					continue;
				}
				goto IL_7E;
			}
			goto IL_43;
			IL_5E:
			num = 3;
		}
		IL_74:
		if (true)
		{
		}
		return;
		IL_7E:
		IList<IWorksheet> innerList;
		XlsWorksheet xlsWorksheet = innerList[num2] as XlsWorksheet;
		xlsWorksheet.DeleteRow(A_0, A_1);
		num2++;
		goto IL_A1;
		IL_43:
		innerList = base.InnerList;
		num2 = 0;
		count = innerList.Count;
		num = 0;
		goto IL_2C;
	}

	// Token: 0x06002D9B RID: 11675 RVA: 0x0019AADC File Offset: 0x00199ADC
	public void ᜉ(int A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_89:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				goto IL_5E;
			case 1:
				goto IL_5E;
			case 2:
				return;
			case 3:
				if (num2 >= count)
				{
					num = 2;
					continue;
				}
				goto IL_76;
			}
			goto IL_43;
			IL_5E:
			num = 3;
		}
		return;
		IL_76:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		worksheet.DeleteColumn(A_0);
		num2++;
		goto IL_89;
		IL_43:
		innerList = base.InnerList;
		num2 = 0;
		count = innerList.Count;
		num = 1;
		goto IL_2C;
	}

	// Token: 0x06002D9C RID: 11676 RVA: 0x0019AB90 File Offset: 0x00199B90
	public void ᜐ(int A_0, int A_1)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A1:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 1:
				if (num2 >= count)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				goto IL_7E;
			case 2:
				return;
			case 3:
				goto IL_5E;
			}
			goto IL_43;
			IL_5E:
			num = 1;
		}
		return;
		IL_7E:
		IList<IWorksheet> innerList;
		XlsWorksheet xlsWorksheet = (XlsWorksheet)innerList[num2];
		xlsWorksheet.DeleteColumn(A_0, A_1);
		num2++;
		goto IL_A1;
		IL_43:
		innerList = base.InnerList;
		num2 = 0;
		count = innerList.Count;
		num = 3;
		goto IL_2C;
	}

	// Token: 0x06002D9D RID: 11677 RVA: 0x0019AC4C File Offset: 0x00199C4C
	public new int ᜀ(object[] A_0, int A_1, int A_2, bool A_3)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A4:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		int result;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				goto IL_68;
			case 1:
				if (num2 >= count)
				{
					num = 3;
					continue;
				}
				goto IL_80;
			case 2:
				goto IL_68;
			case 3:
				return result;
			}
			goto IL_43;
			IL_68:
			num = 1;
		}
		return result;
		IL_80:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		result = worksheet.InsertArray(A_0, A_1, A_2, A_3);
		num2++;
		goto IL_A4;
		IL_43:
		if (true)
		{
		}
		innerList = base.InnerList;
		result = 0;
		num2 = 0;
		count = innerList.Count;
		num = 2;
		goto IL_2C;
	}

	// Token: 0x06002D9E RID: 11678 RVA: 0x0019AD0C File Offset: 0x00199D0C
	public new int ᜀ(string[] A_0, int A_1, int A_2, bool A_3)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A4:
			num = 0;
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_4B;
			}
			break;
		}
		int num2;
		int count;
		int result;
		for (;;)
		{
			IL_34:
			switch (num)
			{
			case 0:
				goto IL_68;
			case 1:
				if (num2 >= count)
				{
					num = 3;
					continue;
				}
				goto IL_80;
			case 2:
				goto IL_68;
			case 3:
				return result;
			}
			goto IL_4B;
			IL_68:
			num = 1;
		}
		return result;
		IL_80:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		result = worksheet.InsertArray(A_0, A_1, A_2, A_3);
		num2++;
		goto IL_A4;
		IL_4B:
		innerList = base.InnerList;
		result = 0;
		num2 = 0;
		count = innerList.Count;
		num = 2;
		goto IL_34;
	}

	// Token: 0x06002D9F RID: 11679 RVA: 0x0019ADCC File Offset: 0x00199DCC
	public new int ᜀ(int[] A_0, int A_1, int A_2, bool A_3)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A4:
			num = 3;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		int result;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				if (num2 >= count)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				goto IL_80;
			case 1:
				goto IL_60;
			case 2:
				return result;
			case 3:
				goto IL_60;
			}
			goto IL_43;
			IL_60:
			num = 0;
		}
		return result;
		IL_80:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		result = worksheet.InsertArray(A_0, A_1, A_2, A_3);
		num2++;
		goto IL_A4;
		IL_43:
		innerList = base.InnerList;
		result = 0;
		num2 = 0;
		count = innerList.Count;
		num = 1;
		goto IL_2C;
	}

	// Token: 0x06002DA0 RID: 11680 RVA: 0x0019AE8C File Offset: 0x00199E8C
	public new int ᜀ(double[] A_0, int A_1, int A_2, bool A_3)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A4:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		int result;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				goto IL_68;
			case 1:
				goto IL_68;
			case 2:
				if (num2 >= count)
				{
					num = 3;
					continue;
				}
				goto IL_80;
			case 3:
				return result;
			}
			goto IL_43;
			IL_68:
			num = 2;
		}
		return result;
		IL_80:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		result = worksheet.InsertArray(A_0, A_1, A_2, A_3);
		num2++;
		goto IL_A4;
		IL_43:
		innerList = base.InnerList;
		result = 0;
		num2 = 0;
		count = innerList.Count;
		if (true)
		{
		}
		num = 0;
		goto IL_2C;
	}

	// Token: 0x06002DA1 RID: 11681 RVA: 0x0019AF4C File Offset: 0x00199F4C
	public new int ᜀ(DateTime[] A_0, int A_1, int A_2, bool A_3)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A4:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				if (true)
				{
				}
				goto IL_4B;
			}
			break;
		}
		int result;
		int num2;
		int count;
		for (;;)
		{
			IL_34:
			switch (num)
			{
			case 0:
				return result;
			case 1:
				goto IL_68;
			case 2:
				goto IL_68;
			case 3:
				if (num2 >= count)
				{
					num = 0;
					continue;
				}
				goto IL_80;
			}
			goto IL_4B;
			IL_68:
			num = 3;
		}
		return result;
		IL_80:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		result = worksheet.InsertArray(A_0, A_1, A_2, A_3);
		num2++;
		goto IL_A4;
		IL_4B:
		innerList = base.InnerList;
		result = 0;
		num2 = 0;
		count = innerList.Count;
		num = 1;
		goto IL_34;
	}

	// Token: 0x06002DA2 RID: 11682 RVA: 0x0019B00C File Offset: 0x0019A00C
	public new int ᜀ(object[,] A_0, int A_1, int A_2)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A2:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		int result;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				if (num2 >= count)
				{
					num = 3;
					continue;
				}
				goto IL_80;
			case 1:
				goto IL_68;
			case 2:
				goto IL_68;
			case 3:
				return result;
			}
			goto IL_43;
			IL_68:
			num = 0;
		}
		return result;
		IL_80:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		result = worksheet.InsertArray(A_0, A_1, A_2);
		num2++;
		goto IL_A2;
		IL_43:
		if (true)
		{
		}
		innerList = base.InnerList;
		result = 0;
		num2 = 0;
		count = innerList.Count;
		num = 2;
		goto IL_2C;
	}

	// Token: 0x06002DA3 RID: 11683 RVA: 0x0019B0C8 File Offset: 0x0019A0C8
	public new int ᜀ(DataColumn A_0, bool A_1, int A_2, int A_3)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A4:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int result;
		int num2;
		int count;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				goto IL_68;
			case 1:
				return result;
			case 2:
				if (num2 >= count)
				{
					num = 1;
					continue;
				}
				goto IL_80;
			case 3:
				goto IL_68;
			}
			goto IL_43;
			IL_68:
			num = 2;
		}
		return result;
		IL_80:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		result = worksheet.InsertDataColumn(A_0, A_1, A_2, A_3);
		num2++;
		goto IL_A4;
		IL_43:
		if (true)
		{
		}
		innerList = base.InnerList;
		result = 0;
		num2 = 0;
		count = innerList.Count;
		num = 3;
		goto IL_2C;
	}

	// Token: 0x06002DA4 RID: 11684 RVA: 0x0019B188 File Offset: 0x0019A188
	public new int ᜀ(DataTable A_0, bool A_1, int A_2, int A_3)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A4:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_43;
			}
			break;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
				if (num2 >= count)
				{
					num = 2;
					continue;
				}
				goto IL_80;
			case 1:
				goto IL_60;
			case 2:
				goto IL_76;
			case 3:
				goto IL_60;
			}
			goto IL_43;
			IL_60:
			num = 0;
		}
		IL_76:
		if (true)
		{
		}
		int result;
		return result;
		IL_80:
		IList<IWorksheet> innerList;
		IWorksheet worksheet = innerList[num2];
		result = worksheet.InsertDataTable(A_0, A_1, A_2, A_3);
		num2++;
		goto IL_A4;
		IL_43:
		innerList = base.InnerList;
		result = 0;
		num2 = 0;
		count = innerList.Count;
		num = 3;
		goto IL_2C;
	}

	// Token: 0x06002DA5 RID: 11685 RVA: 0x0019B248 File Offset: 0x0019A248
	public new int ᜀ(DataTable A_0, bool A_1, int A_2, int A_3, bool A_4)
	{
		int num = 0;
		switch (num)
		{
		default:
		{
			int result;
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					result = 0;
					num2 = 0;
					count = innerList.Count;
					num = 3;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (true)
						{
						}
						if (num2 >= count)
						{
							num = 2;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						result = worksheet.InsertDataTable(A_0, A_1, A_2, A_3, A_4);
						num2++;
						num = 1;
						continue;
					}
					case 1:
						goto IL_6A;
					case 2:
						return result;
					case 3:
						goto IL_6A;
					}
					break;
					IL_6A:
					num = 0;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06002DA6 RID: 11686 RVA: 0x0019B30C File Offset: 0x0019A30C
	public new int ᜀ(DataTable A_0, bool A_1, int A_2, int A_3, int A_4, int A_5)
	{
		int num = 0;
		switch (num)
		{
		default:
		{
			int result;
			for (;;)
			{
				if (true)
				{
				}
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					result = 0;
					num2 = 0;
					count = innerList.Count;
					num = 3;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
					{
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						result = worksheet.InsertDataTable(A_0, A_1, A_2, A_3, A_4, A_5);
						num2++;
						num = 2;
						continue;
					}
					case 2:
						goto IL_72;
					case 3:
						goto IL_72;
					}
					break;
					IL_72:
					num = 1;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06002DA7 RID: 11687 RVA: 0x0019B3D0 File Offset: 0x0019A3D0
	public new int ᜀ(DataTable A_0, bool A_1, int A_2, int A_3, int A_4, int A_5, bool A_6)
	{
		int num = 0;
		switch (num)
		{
		default:
		{
			int result;
			for (;;)
			{
				if (true)
				{
				}
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					result = 0;
					num2 = 0;
					count = innerList.Count;
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (num2 >= count)
						{
							num = 3;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						result = (worksheet as XlsWorksheet).InsertDataTable(A_0, A_1, A_2, A_3, A_4, A_5, A_6);
						num2++;
						num = 1;
						continue;
					}
					case 1:
						goto IL_72;
					case 2:
						goto IL_72;
					case 3:
						return result;
					}
					break;
					IL_72:
					num = 0;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06002DA8 RID: 11688 RVA: 0x0019B49C File Offset: 0x0019A49C
	public new int ᜀ(DataView A_0, bool A_1, int A_2, int A_3)
	{
		int num = 0;
		switch (num)
		{
		default:
		{
			int result;
			for (;;)
			{
				List<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					result = 0;
					num2 = 0;
					count = innerList.Count;
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6A;
					case 1:
						goto IL_6A;
					case 2:
						return result;
					case 3:
					{
						if (num2 >= count)
						{
							num = 2;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						result = worksheet.InsertDataView(A_0, A_1, A_2, A_3);
						num2++;
						if (true)
						{
						}
						num = 0;
						continue;
					}
					}
					break;
					IL_6A:
					num = 3;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06002DA9 RID: 11689 RVA: 0x0019B55C File Offset: 0x0019A55C
	public new int ᜀ(DataView A_0, bool A_1, int A_2, int A_3, bool A_4)
	{
		int num = 0;
		switch (num)
		{
		default:
		{
			int result;
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					result = 0;
					num2 = 0;
					count = innerList.Count;
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6A;
					case 1:
						return result;
					case 2:
					{
						if (num2 >= count)
						{
							if (true)
							{
							}
							num = 1;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						result = worksheet.InsertDataView(A_0, A_1, A_2, A_3, A_4);
						num2++;
						num = 3;
						continue;
					}
					case 3:
						goto IL_6A;
					}
					break;
					IL_6A:
					num = 2;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06002DAA RID: 11690 RVA: 0x0019B620 File Offset: 0x0019A620
	public new int ᜀ(DataView A_0, bool A_1, int A_2, int A_3, int A_4, int A_5)
	{
		int num = 0;
		switch (num)
		{
		default:
		{
			int result;
			for (;;)
			{
				if (true)
				{
				}
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					result = 0;
					num2 = 0;
					count = innerList.Count;
					num = 3;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (num2 >= count)
						{
							num = 2;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						result = worksheet.InsertDataView(A_0, A_1, A_2, A_3, A_4, A_5);
						num2++;
						num = 1;
						continue;
					}
					case 1:
						goto IL_72;
					case 2:
						return result;
					case 3:
						goto IL_72;
					}
					break;
					IL_72:
					num = 0;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06002DAB RID: 11691 RVA: 0x0019B6E4 File Offset: 0x0019A6E4
	public new int ᜀ(DataView A_0, bool A_1, int A_2, int A_3, int A_4, int A_5, bool A_6)
	{
		int num = 0;
		switch (num)
		{
		default:
		{
			int result;
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					result = 0;
					num2 = 0;
					count = innerList.Count;
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						goto IL_6A;
					case 2:
						goto IL_6A;
					case 3:
					{
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						if (true)
						{
						}
						IWorksheet worksheet = innerList[num2];
						result = worksheet.InsertDataView(A_0, A_1, A_2, A_3, A_4, A_5, A_6);
						num2++;
						num = 1;
						continue;
					}
					}
					break;
					IL_6A:
					num = 3;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06002DAC RID: 11692 RVA: 0x0019B7AC File Offset: 0x0019A7AC
	public void \u1718()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002DAD RID: 11693 RVA: 0x0019B7EC File Offset: 0x0019A7EC
	public void ᜄ(string A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002DAE RID: 11694 RVA: 0x0019B82C File Offset: 0x0019A82C
	public new void ᜀ(string A_0, SheetProtectionType A_1)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002DAF RID: 11695 RVA: 0x0019B86C File Offset: 0x0019A86C
	public new void ᜀ(string A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002DB0 RID: 11696 RVA: 0x0019B8AC File Offset: 0x0019A8AC
	public new IXLSRange ᜁ(IXLSRange A_0, IXLSRange A_1)
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
		return null;
	}

	// Token: 0x06002DB1 RID: 11697 RVA: 0x0019B8E8 File Offset: 0x0019A8E8
	public new IXLSRange ᜀ(IXLSRange A_0, IXLSRange A_1)
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
		return null;
	}

	// Token: 0x06002DB2 RID: 11698 RVA: 0x0019B924 File Offset: 0x0019A924
	public void ᜆ(int A_0)
	{
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 3;
					break;
				}
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_70;
					case 2:
					{
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						worksheet.AutoFitRow(A_0);
						num2++;
						num = 1;
						continue;
					}
					case 3:
						goto IL_70;
					}
					break;
					IL_70:
					num = 2;
				}
			}
			return;
		}
	}

	// Token: 0x06002DB3 RID: 11699 RVA: 0x0019B9DC File Offset: 0x0019A9DC
	public void ᜑ(int A_0)
	{
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
					{
						if (num2 >= count)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						worksheet.AutoFitColumn(A_0);
						num2++;
						num = 3;
						continue;
					}
					case 2:
						goto IL_68;
					case 3:
						goto IL_68;
					}
					break;
					IL_68:
					num = 1;
				}
			}
			return;
		}
	}

	// Token: 0x06002DB4 RID: 11700 RVA: 0x0019BA94 File Offset: 0x0019AA94
	public new void ᜀ(string A_0, string A_1)
	{
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 3;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (num2 >= count)
						{
							num = 2;
							continue;
						}
						if (true)
						{
						}
						IWorksheet worksheet = innerList[num2];
						worksheet.Replace(A_0, A_1);
						num2++;
						num = 1;
						continue;
					}
					case 1:
						goto IL_68;
					case 2:
						return;
					case 3:
						goto IL_68;
					}
					break;
					IL_68:
					num = 0;
				}
			}
			return;
		}
	}

	// Token: 0x06002DB5 RID: 11701 RVA: 0x0019BB4C File Offset: 0x0019AB4C
	public new void ᜀ(string A_0, double A_1)
	{
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				if (true)
				{
				}
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_70;
					case 1:
						return;
					case 2:
						goto IL_70;
					case 3:
					{
						if (num2 >= count)
						{
							num = 1;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						worksheet.Replace(A_0, A_1);
						num2++;
						num = 2;
						continue;
					}
					}
					break;
					IL_70:
					num = 3;
				}
			}
			return;
		}
	}

	// Token: 0x06002DB6 RID: 11702 RVA: 0x0019BC04 File Offset: 0x0019AC04
	public new void ᜀ(string A_0, DateTime A_1)
	{
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
					{
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						worksheet.Replace(A_0, A_1);
						num2++;
						num = 3;
						continue;
					}
					case 2:
						goto IL_68;
					case 3:
						if (true)
						{
						}
						goto IL_68;
					}
					break;
					IL_68:
					num = 1;
				}
			}
			return;
		}
	}

	// Token: 0x06002DB7 RID: 11703 RVA: 0x0019BCBC File Offset: 0x0019ACBC
	public new void ᜀ(string A_0, string[] A_1, bool A_2)
	{
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_70;
					case 1:
					{
						if (num2 >= count)
						{
							num = 3;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						worksheet.Replace(A_0, A_1, A_2);
						num2++;
						num = 0;
						continue;
					}
					case 2:
						goto IL_70;
					case 3:
						return;
					}
					break;
					IL_70:
					num = 1;
				}
			}
			return;
		}
	}

	// Token: 0x06002DB8 RID: 11704 RVA: 0x0019BD74 File Offset: 0x0019AD74
	public new void ᜀ(string A_0, int[] A_1, bool A_2)
	{
		if (true)
		{
		}
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_70;
					case 1:
						goto IL_70;
					case 2:
						return;
					case 3:
					{
						if (num2 >= count)
						{
							num = 2;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						worksheet.Replace(A_0, A_1, A_2);
						num2++;
						num = 0;
						continue;
					}
					}
					break;
					IL_70:
					num = 3;
				}
			}
			return;
		}
	}

	// Token: 0x06002DB9 RID: 11705 RVA: 0x0019BE2C File Offset: 0x0019AE2C
	public new void ᜀ(string A_0, double[] A_1, bool A_2)
	{
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_68;
					case 2:
					{
						if (num2 >= count)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						worksheet.Replace(A_0, A_1, A_2);
						num2++;
						num = 3;
						continue;
					}
					case 3:
						goto IL_68;
					}
					break;
					IL_68:
					num = 2;
				}
			}
			return;
		}
	}

	// Token: 0x06002DBA RID: 11706 RVA: 0x0019BEE4 File Offset: 0x0019AEE4
	public new void ᜀ(string A_0, DataTable A_1, bool A_2)
	{
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 3;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_68;
					case 2:
					{
						if (true)
						{
						}
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						worksheet.Replace(A_0, A_1, A_2);
						num2++;
						num = 1;
						continue;
					}
					case 3:
						goto IL_68;
					}
					break;
					IL_68:
					num = 2;
				}
			}
			return;
		}
	}

	// Token: 0x06002DBB RID: 11707 RVA: 0x0019BF9C File Offset: 0x0019AF9C
	public new void ᜀ(string A_0, DataColumn A_1, bool A_2)
	{
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 3;
					break;
				}
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_70;
					case 1:
						return;
					case 2:
					{
						if (num2 >= count)
						{
							num = 1;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						worksheet.Replace(A_0, A_1, A_2);
						num2++;
						num = 0;
						continue;
					}
					case 3:
						goto IL_70;
					}
					break;
					IL_70:
					num = 2;
				}
			}
			return;
		}
	}

	// Token: 0x06002DBC RID: 11708 RVA: 0x0019C054 File Offset: 0x0019B054
	public void ᜐ()
	{
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_68;
					case 1:
						goto IL_68;
					case 2:
					{
						if (num2 >= count)
						{
							num = 3;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						worksheet.Remove();
						num2++;
						num = 0;
						continue;
					}
					case 3:
						goto IL_86;
					}
					break;
					IL_68:
					if (true)
					{
					}
					num = 2;
				}
			}
			IL_86:
			base.Clear();
			return;
		}
	}

	// Token: 0x06002DBD RID: 11709 RVA: 0x0019C110 File Offset: 0x0019B110
	public new void ᜂ(int A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002DBE RID: 11710 RVA: 0x0019C150 File Offset: 0x0019B150
	public new int ᜀ(double A_0)
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
		IWorksheet worksheet = base.List[0];
		return worksheet.ColumnWidthToPixels(A_0);
	}

	// Token: 0x06002DBF RID: 11711 RVA: 0x0019C1A0 File Offset: 0x0019B1A0
	public new double ᜂ(double A_0)
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
		IWorksheet worksheet = base.List[0];
		return worksheet.PixelsToColumnWidth(A_0);
	}

	// Token: 0x06002DC0 RID: 11712 RVA: 0x0019C1F0 File Offset: 0x0019B1F0
	public new void ᜁ(int A_0, double A_1)
	{
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 2;
					break;
				}
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
					{
						if (num2 >= count)
						{
							num = 3;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						((XlsWorksheet)worksheet).SetColumnWidth(A_0, A_1);
						num2++;
						num = 1;
						continue;
					}
					case 1:
						goto IL_70;
					case 2:
						goto IL_70;
					case 3:
						return;
					}
					break;
					IL_70:
					num = 0;
				}
			}
			return;
		}
	}

	// Token: 0x06002DC1 RID: 11713 RVA: 0x0019C2AC File Offset: 0x0019B2AC
	public new void ᜂ(int A_0, double A_1)
	{
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 2;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_70;
					case 2:
						goto IL_70;
					case 3:
					{
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						((XlsWorksheet)worksheet).InnerSetRowHeight(A_0, A_1);
						num2++;
						num = 1;
						continue;
					}
					}
					break;
					IL_70:
					num = 3;
				}
			}
			return;
		}
	}

	// Token: 0x06002DC2 RID: 11714 RVA: 0x0019C368 File Offset: 0x0019B368
	public new void ᜀ(int A_0, double A_1)
	{
		if (true)
		{
		}
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_70;
					case 1:
					{
						if (num2 >= count)
						{
							num = 2;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						((XlsWorksheet)worksheet).SetRowHeightInPixels(A_0, A_1);
						num2++;
						num = 3;
						continue;
					}
					case 2:
						return;
					case 3:
						goto IL_70;
					}
					break;
					IL_70:
					num = 1;
				}
			}
			return;
		}
	}

	// Token: 0x06002DC3 RID: 11715 RVA: 0x0019C424 File Offset: 0x0019B424
	public new void ᜂ(int A_0, int A_1, double A_2)
	{
		int num = 0;
		switch (num)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 3;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_68;
					case 2:
					{
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						((XlsWorksheet)worksheet).SetRowHeightInPixels(A_0, A_1, A_2);
						num2++;
						if (true)
						{
						}
						num = 1;
						continue;
					}
					case 3:
						goto IL_68;
					}
					break;
					IL_68:
					num = 2;
				}
			}
			return;
		}
	}

	// Token: 0x06002DC4 RID: 11716 RVA: 0x0019C4E4 File Offset: 0x0019B4E4
	public new int ᜀ(int A_0)
	{
		switch (0)
		{
		default:
		{
			double columnWidth;
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				columnWidth = (innerList[0] as XlsWorksheet).GetColumnWidth(A_0);
				int num = 1;
				int count = innerList.Count;
				int num2;
				int columnWidthPixels;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_E4:
					if (num >= count)
					{
						num2 = 3;
					}
					else
					{
						if (true)
						{
						}
						IWorksheet worksheet = innerList[num];
						columnWidthPixels = ((XlsWorksheet)worksheet).GetColumnWidthPixels(A_0);
						num2 = 0;
					}
					break;
				default:
					if (false)
					{
					}
					num2 = 5;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if ((double)columnWidthPixels != columnWidth)
						{
							num2 = 1;
							continue;
						}
						num++;
						num2 = 4;
						continue;
					case 1:
						return int.MinValue;
					case 2:
						goto IL_E4;
					case 3:
						goto IL_F4;
					case 4:
						goto IL_D8;
					case 5:
						goto IL_D8;
					}
					break;
					IL_D8:
					num2 = 2;
				}
			}
			return int.MinValue;
			IL_F4:
			return (int)columnWidth;
		}
		}
	}

	// Token: 0x06002DC5 RID: 11717 RVA: 0x0019C5EC File Offset: 0x0019B5EC
	public double ᜊ(int A_0)
	{
		switch (0)
		{
		default:
		{
			double num;
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				num = ((XlsWorksheet)innerList[0]).GetColumnWidth(A_0);
				int num2 = 1;
				int count = innerList.Count;
				if (true)
				{
				}
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
					{
						double columnWidth;
						if (columnWidth != num)
						{
							num3 = 2;
							continue;
						}
						num2++;
						num3 = 1;
						continue;
					}
					case 1:
						goto IL_F0;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F0;
						default:
							if (false)
							{
							}
							num = double.NaN;
							num3 = 4;
							continue;
						}
						break;
					case 3:
						goto IL_F0;
					case 4:
						return num;
					case 5:
					{
						if (num2 >= count)
						{
							num3 = 6;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						double columnWidth = ((XlsWorksheet)worksheet).GetColumnWidth(A_0);
						num3 = 0;
						continue;
					}
					case 6:
						return num;
					}
					break;
					IL_F0:
					num3 = 5;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x06002DC6 RID: 11718 RVA: 0x0019C708 File Offset: 0x0019B708
	public double ᜈ(int A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				double rowHeight = ((XlsWorksheet)innerList[0]).GetRowHeight(A_0);
				int num = 1;
				int count = innerList.Count;
				int num2;
				double rowHeight2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_EA:
					if (num >= count)
					{
						num2 = 3;
					}
					else
					{
						IWorksheet worksheet = innerList[num];
						rowHeight2 = ((XlsWorksheet)worksheet).GetRowHeight(A_0);
						num2 = 1;
					}
					break;
				default:
					if (false)
					{
					}
					num2 = 2;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_DE;
					case 1:
						if (rowHeight2 != rowHeight)
						{
							num2 = 5;
							continue;
						}
						if (true)
						{
						}
						num++;
						num2 = 0;
						continue;
					case 2:
						goto IL_DE;
					case 3:
						return rowHeight;
					case 4:
						goto IL_EA;
					case 5:
						goto IL_DC;
					}
					break;
					IL_DE:
					num2 = 4;
				}
			}
			IL_DC:
			return double.NaN;
		}
	}

	// Token: 0x06002DC7 RID: 11719 RVA: 0x0019C814 File Offset: 0x0019B814
	public int \u1714(int A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				if (true)
				{
				}
				IList<IWorksheet> innerList = base.InnerList;
				int rowHeightPixels = ((XlsWorksheet)innerList[0]).GetRowHeightPixels(A_0);
				int num = 1;
				int count = innerList.Count;
				int num2;
				int rowHeightPixels2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_E6:
					if (num >= count)
					{
						num2 = 2;
					}
					else
					{
						IWorksheet worksheet = innerList[num];
						rowHeightPixels2 = ((XlsWorksheet)worksheet).GetRowHeightPixels(A_0);
						num2 = 0;
					}
					break;
				default:
					if (false)
					{
					}
					num2 = 3;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (rowHeightPixels2 != rowHeightPixels)
						{
							num2 = 4;
							continue;
						}
						num++;
						num2 = 5;
						continue;
					case 1:
						goto IL_E6;
					case 2:
						return rowHeightPixels;
					case 3:
						goto IL_DA;
					case 4:
						return int.MinValue;
					case 5:
						goto IL_DA;
					}
					break;
					IL_DA:
					num2 = 1;
				}
			}
			return int.MinValue;
		}
	}

	// Token: 0x06002DC8 RID: 11720 RVA: 0x0019C91C File Offset: 0x0019B91C
	public new IXLSRange ᜀ(string A_0, FindType A_1)
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
		throw new NotImplementedException();
	}

	// Token: 0x06002DC9 RID: 11721 RVA: 0x0019C95C File Offset: 0x0019B95C
	public new IXLSRange ᜀ(double A_0, FindType A_1)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotImplementedException();
	}

	// Token: 0x06002DCA RID: 11722 RVA: 0x0019C99C File Offset: 0x0019B99C
	public IXLSRange ᜉ(bool A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x06002DCB RID: 11723 RVA: 0x0019C9DC File Offset: 0x0019B9DC
	public new IXLSRange ᜀ(DateTime A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x06002DCC RID: 11724 RVA: 0x0019CA1C File Offset: 0x0019BA1C
	public new IXLSRange ᜀ(TimeSpan A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x06002DCD RID: 11725 RVA: 0x0019CA5C File Offset: 0x0019BA5C
	public new IXLSRange[] ᜁ(string A_0, FindType A_1)
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
		throw new NotImplementedException();
	}

	// Token: 0x06002DCE RID: 11726 RVA: 0x0019CA9C File Offset: 0x0019BA9C
	public new IXLSRange[] ᜁ(double A_0, FindType A_1)
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
		throw new NotImplementedException();
	}

	// Token: 0x06002DCF RID: 11727 RVA: 0x0019CADC File Offset: 0x0019BADC
	public IXLSRange[] ᜃ(bool A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotImplementedException();
	}

	// Token: 0x06002DD0 RID: 11728 RVA: 0x0019CB1C File Offset: 0x0019BB1C
	public new IXLSRange[] ᜁ(DateTime A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x06002DD1 RID: 11729 RVA: 0x0019CB5C File Offset: 0x0019BB5C
	public new IXLSRange[] ᜁ(TimeSpan A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x06002DD2 RID: 11730 RVA: 0x0019CB9C File Offset: 0x0019BB9C
	public new void ᜁ(string A_0, string A_1)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002DD3 RID: 11731 RVA: 0x0019CBDC File Offset: 0x0019BBDC
	public new void ᜀ(string A_0, string A_1, Encoding A_2)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002DD4 RID: 11732 RVA: 0x0019CC1C File Offset: 0x0019BC1C
	public new void ᜀ(Stream A_0, string A_1)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002DD5 RID: 11733 RVA: 0x0019CC5C File Offset: 0x0019BC5C
	public new void ᜀ(Stream A_0, string A_1, Encoding A_2)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002DD6 RID: 11734 RVA: 0x0019CC9C File Offset: 0x0019BC9C
	public new void ᜀ(int A_0, IStyle A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_40;
					case 1:
						if (num < count)
						{
							IWorksheet worksheet = innerList[num];
							worksheet.SetDefaultColumnStyle(A_0, A_1);
							num++;
							if (true)
							{
							}
							num2 = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_40;
						default:
							if (false)
							{
							}
							num2 = 2;
							continue;
						}
						break;
					case 2:
						return;
					case 3:
						goto IL_42;
					}
					break;
					IL_42:
					num2 = 1;
					continue;
					IL_40:
					goto IL_42;
				}
			}
			return;
		}
	}

	// Token: 0x06002DD7 RID: 11735 RVA: 0x0019CD54 File Offset: 0x0019BD54
	public new void ᜀ(int A_0, int A_1, IStyle A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				if (true)
				{
				}
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num < count)
						{
							IWorksheet worksheet = innerList[num];
							worksheet.SetDefaultColumnStyle(A_0, A_1, A_2);
							num++;
							num2 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_48;
						default:
							if (false)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					case 1:
						goto IL_4A;
					case 2:
						goto IL_48;
					case 3:
						return;
					}
					break;
					IL_4A:
					num2 = 0;
					continue;
					IL_48:
					goto IL_4A;
				}
			}
			return;
		}
	}

	// Token: 0x06002DD8 RID: 11736 RVA: 0x0019CE0C File Offset: 0x0019BE0C
	public new void ᜁ(int A_0, IStyle A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_4A;
					case 1:
						return;
					case 2:
						if (num < count)
						{
							IWorksheet worksheet = innerList[num];
							worksheet.SetDefaultRowStyle(A_0, A_1);
							num++;
							num2 = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_40;
						default:
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					case 3:
						goto IL_40;
					}
					break;
					IL_4A:
					num2 = 2;
					continue;
					IL_40:
					if (true)
					{
					}
					goto IL_4A;
				}
			}
			return;
		}
	}

	// Token: 0x06002DD9 RID: 11737 RVA: 0x0019CEC4 File Offset: 0x0019BEC4
	public new void ᜁ(int A_0, int A_1, IStyle A_2)
	{
		if (true)
		{
		}
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num < count)
						{
							IWorksheet worksheet = innerList[num];
							worksheet.SetDefaultRowStyle(A_0, A_1, A_2);
							num++;
							num2 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_48;
						default:
							if (false)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					case 1:
						goto IL_4A;
					case 2:
						goto IL_48;
					case 3:
						return;
					}
					break;
					IL_4A:
					num2 = 0;
					continue;
					IL_48:
					goto IL_4A;
				}
			}
			return;
		}
	}

	// Token: 0x06002DDA RID: 11738 RVA: 0x0019CF7C File Offset: 0x0019BF7C
	public IStyle ᜃ(int A_0)
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
		return null;
	}

	// Token: 0x06002DDB RID: 11739 RVA: 0x0019CFB8 File Offset: 0x0019BFB8
	public IStyle ᜅ(int A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return null;
	}

	// Token: 0x06002DDC RID: 11740 RVA: 0x0019CFF4 File Offset: 0x0019BFF4
	public new void ᜀ(IXLSRange A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x06002DDD RID: 11741 RVA: 0x0019D034 File Offset: 0x0019C034
	public void ᜃ(int A_0, int A_1)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotImplementedException();
	}

	// Token: 0x06002DDE RID: 11742 RVA: 0x0019D074 File Offset: 0x0019C074
	public new void ᜀ(int A_0, int A_1, string A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_42;
					case 1:
						return;
					case 2:
						if (num < count)
						{
							IWorksheet worksheet = innerList[num];
							worksheet.SetValue(A_0, A_1, A_2);
							num++;
							num2 = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_40;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					case 3:
						goto IL_40;
					}
					break;
					IL_42:
					num2 = 2;
					continue;
					IL_40:
					goto IL_42;
				}
			}
			return;
		}
	}

	// Token: 0x06002DDF RID: 11743 RVA: 0x0019D12C File Offset: 0x0019C12C
	public new void ᜀ(int A_0, int A_1, double A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_4A;
					case 1:
						return;
					case 2:
						goto IL_48;
					case 3:
						if (num < count)
						{
							IWorksheet worksheet = innerList[num];
							worksheet.SetNumber(A_0, A_1, A_2);
							num++;
							num2 = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_48;
						default:
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					}
					break;
					IL_4A:
					num2 = 3;
					continue;
					IL_48:
					goto IL_4A;
				}
			}
			return;
		}
	}

	// Token: 0x06002DE0 RID: 11744 RVA: 0x0019D1E4 File Offset: 0x0019C1E4
	public new void ᜂ(int A_0, int A_1, bool A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num < count)
						{
							IWorksheet worksheet = innerList[num];
							worksheet.SetBoolean(A_0, A_1, A_2);
							num++;
							num2 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_40;
						default:
							if (false)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					case 1:
						if (true)
						{
						}
						goto IL_42;
					case 2:
						goto IL_40;
					case 3:
						return;
					}
					break;
					IL_42:
					num2 = 0;
					continue;
					IL_40:
					goto IL_42;
				}
			}
			return;
		}
	}

	// Token: 0x06002DE1 RID: 11745 RVA: 0x0019D29C File Offset: 0x0019C29C
	public new void ᜂ(int A_0, int A_1, string A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return;
					case 1:
						if (true)
						{
						}
						if (num < count)
						{
							IWorksheet worksheet = innerList[num];
							worksheet.SetText(A_0, A_1, A_2);
							num++;
							num2 = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_40;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					case 2:
						goto IL_42;
					case 3:
						goto IL_40;
					}
					break;
					IL_42:
					num2 = 1;
					continue;
					IL_40:
					goto IL_42;
				}
			}
			return;
		}
	}

	// Token: 0x06002DE2 RID: 11746 RVA: 0x0019D354 File Offset: 0x0019C354
	public new void ᜁ(int A_0, int A_1, string A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_40;
					case 1:
						return;
					case 2:
						if (num < count)
						{
							IWorksheet worksheet = innerList[num];
							worksheet.SetFormula(A_0, A_1, A_2);
							num++;
							num2 = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_40;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					case 3:
						goto IL_42;
					}
					break;
					IL_42:
					num2 = 2;
					continue;
					IL_40:
					goto IL_42;
				}
			}
			return;
		}
	}

	// Token: 0x06002DE3 RID: 11747 RVA: 0x0019D40C File Offset: 0x0019C40C
	public void ᜆ(int A_0, int A_1, string A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_40;
					case 1:
						goto IL_42;
					case 2:
						return;
					case 3:
						if (num < count)
						{
							if (true)
							{
							}
							IWorksheet worksheet = innerList[num];
							worksheet.SetError(A_0, A_1, A_2);
							num++;
							num2 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_40;
						default:
							if (false)
							{
							}
							num2 = 2;
							continue;
						}
						break;
					}
					break;
					IL_42:
					num2 = 3;
					continue;
					IL_40:
					goto IL_42;
				}
			}
			return;
		}
	}

	// Token: 0x06002DE4 RID: 11748 RVA: 0x0019D4C4 File Offset: 0x0019C4C4
	public void ᜊ(int A_0, int A_1)
	{
		if (true)
		{
		}
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_48;
					case 1:
						if (num < count)
						{
							IWorksheet worksheet = innerList[num];
							worksheet.SetBlank(A_0, A_1);
							num++;
							num2 = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_48;
						default:
							if (false)
							{
							}
							num2 = 3;
							continue;
						}
						break;
					case 2:
						goto IL_4A;
					case 3:
						return;
					}
					break;
					IL_4A:
					num2 = 1;
					continue;
					IL_48:
					goto IL_4A;
				}
			}
			return;
		}
	}

	// Token: 0x06002DE5 RID: 11749 RVA: 0x0019D57C File Offset: 0x0019C57C
	public new void ᜁ(int A_0, int A_1, double A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_40;
					case 1:
						return;
					case 2:
						goto IL_42;
					case 3:
						if (true)
						{
						}
						if (num < count)
						{
							IWorksheet worksheet = innerList[num];
							worksheet.SetFormulaNumberValue(A_0, A_1, A_2);
							num++;
							num2 = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_40;
						default:
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					}
					break;
					IL_42:
					num2 = 3;
					continue;
					IL_40:
					goto IL_42;
				}
			}
			return;
		}
	}

	// Token: 0x06002DE6 RID: 11750 RVA: 0x0019D634 File Offset: 0x0019C634
	public void ᜅ(int A_0, int A_1, string A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_42;
					case 1:
						return;
					case 2:
						goto IL_40;
					case 3:
						if (num < count)
						{
							IWorksheet worksheet = innerList[num];
							worksheet.SetFormulaErrorValue(A_0, A_1, A_2);
							num++;
							num2 = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_40;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					}
					break;
					IL_42:
					num2 = 3;
					continue;
					IL_40:
					goto IL_42;
				}
			}
			return;
		}
	}

	// Token: 0x06002DE7 RID: 11751 RVA: 0x0019D6EC File Offset: 0x0019C6EC
	public new void ᜁ(int A_0, int A_1, bool A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						return;
					case 1:
						goto IL_48;
					case 2:
						goto IL_4A;
					case 3:
						if (num < count)
						{
							IWorksheet worksheet = innerList[num];
							worksheet.SetFormulaBoolValue(A_0, A_1, A_2);
							num++;
							num2 = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_48;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					}
					break;
					IL_4A:
					num2 = 3;
					continue;
					IL_48:
					goto IL_4A;
				}
			}
			return;
		}
	}

	// Token: 0x06002DE8 RID: 11752 RVA: 0x0019D7A4 File Offset: 0x0019C7A4
	public void ᜃ(int A_0, int A_1, string A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_27:
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				for (;;)
				{
					IL_37:
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_37;
							default:
							{
								if (false)
								{
								}
								IWorksheet worksheet = innerList[num];
								worksheet.SetFormulaStringValue(A_0, A_1, A_2);
								num++;
								num2 = 2;
								continue;
							}
							}
							break;
						case 1:
							goto IL_42;
						case 2:
							goto IL_42;
						case 3:
							return;
						}
						goto IL_27;
						IL_42:
						if (true)
						{
						}
						num2 = 0;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06002DE9 RID: 11753 RVA: 0x0019D85C File Offset: 0x0019C85C
	public string ᜇ(int A_0, int A_1)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002DEA RID: 11754 RVA: 0x0019D89C File Offset: 0x0019C89C
	public new double ᜁ(int A_0, int A_1)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002DEB RID: 11755 RVA: 0x0019D8DC File Offset: 0x0019C8DC
	public new string ᜀ(int A_0, int A_1, bool A_2)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002DEC RID: 11756 RVA: 0x0019D91C File Offset: 0x0019C91C
	public string ᜋ(int A_0, int A_1)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002DED RID: 11757 RVA: 0x0019D95C File Offset: 0x0019C95C
	public bool ᜎ(int A_0, int A_1)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002DEE RID: 11758 RVA: 0x0019D99C File Offset: 0x0019C99C
	public string ᜈ(int A_0, int A_1)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002DEF RID: 11759 RVA: 0x0019D9DC File Offset: 0x0019C9DC
	public double ᜆ(int A_0, int A_1)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002DF0 RID: 11760 RVA: 0x0019DA1C File Offset: 0x0019CA1C
	public new string ᜂ(int A_0, int A_1)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002DF1 RID: 11761 RVA: 0x0019DA5C File Offset: 0x0019CA5C
	public bool ᜅ(int A_0, int A_1)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002DF2 RID: 11762 RVA: 0x0019DA9C File Offset: 0x0019CA9C
	public bool \u1737()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_A8;
					case 1:
						goto IL_A8;
					case 2:
						return true;
					case 3:
					{
						IWorksheet worksheet;
						if (!((XlsWorksheet)worksheet).IsFreezePanes)
						{
							num2 = 5;
							continue;
						}
						num++;
						num2 = 1;
						continue;
					}
					case 4:
					{
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						IWorksheet worksheet = innerList[num];
						goto IL_63;
					}
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_63;
						default:
							goto IL_A0;
						}
						break;
					}
					break;
					IL_63:
					if (true)
					{
					}
					num2 = 3;
					continue;
					IL_A8:
					num2 = 4;
				}
			}
			IL_A0:
			if (false)
			{
			}
			return false;
		}
	}

	// Token: 0x06002DF3 RID: 11763 RVA: 0x0019DB7C File Offset: 0x0019CB7C
	public IXLSRange \u1716()
	{
		int a_ = 11;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotImplementedException(RecordTableEnumerator.b("ቀ㍂⥄⹆㵈歊์⩎㵐㽒", a_));
	}

	// Token: 0x06002DF4 RID: 11764 RVA: 0x0019DBD4 File Offset: 0x0019CBD4
	public new Image ᜁ(int A_0, int A_1, int A_2, int A_3)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002DF5 RID: 11765 RVA: 0x0019DC14 File Offset: 0x0019CC14
	public new Image ᜀ(Stream A_0, int A_1, int A_2, int A_3, int A_4, ImageType A_5)
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
		throw new NotSupportedException();
	}

	// Token: 0x06002DF6 RID: 11766 RVA: 0x0019DC54 File Offset: 0x0019CC54
	public new Image ᜀ(Stream A_0, int A_1, int A_2, int A_3, int A_4, EmfType A_5)
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
		throw new NotImplementedException();
	}

	// Token: 0x06002DF7 RID: 11767 RVA: 0x0019DC94 File Offset: 0x0019CC94
	public new Image ᜀ(Stream A_0, int A_1, int A_2, int A_3, int A_4, ImageType A_5, EmfType A_6)
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
		throw new NotImplementedException();
	}

	// Token: 0x06002DF8 RID: 11768 RVA: 0x0019DCD4 File Offset: 0x0019CCD4
	public ExcelColors \u1738()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				ExcelColors tabKnownColor = innerList[0].TabKnownColor;
				int num = 1;
				int count = innerList.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_BA;
					case 1:
						return tabKnownColor;
					case 2:
					{
						if (true)
						{
						}
						ExcelColors tabKnownColor2;
						if (tabKnownColor2 != tabKnownColor)
						{
							num2 = 4;
							continue;
						}
						num++;
						num2 = 5;
						continue;
					}
					case 3:
					{
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						ITabSheet tabSheet = innerList[num];
						ExcelColors tabKnownColor2 = tabSheet.TabKnownColor;
						goto IL_7A;
					}
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7A;
						default:
							goto IL_B2;
						}
						break;
					case 5:
						goto IL_BA;
					}
					break;
					IL_7A:
					num2 = 2;
					continue;
					IL_BA:
					num2 = 3;
				}
			}
			IL_B2:
			if (false)
			{
			}
			return ExcelColors.Black;
		}
	}

	// Token: 0x06002DF9 RID: 11769 RVA: 0x0019DDC4 File Offset: 0x0019CDC4
	public new void ᜁ(ExcelColors A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_27:
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				for (;;)
				{
					IL_37:
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_42;
						case 1:
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_37;
							default:
							{
								if (true)
								{
								}
								if (false)
								{
								}
								ITabSheet tabSheet = innerList[num];
								tabSheet.TabKnownColor = A_0;
								num++;
								num2 = 2;
								continue;
							}
							}
							break;
						case 2:
							goto IL_42;
						case 3:
							return;
						}
						goto IL_27;
						IL_42:
						num2 = 1;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06002DFA RID: 11770 RVA: 0x0019DE7C File Offset: 0x0019CE7C
	public Color \u1712()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				Color tabColor = innerList[0].TabColor;
				int num = 1;
				int count = innerList.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_C3;
					case 1:
					{
						if (num >= count)
						{
							num2 = 5;
							continue;
						}
						ITabSheet tabSheet = innerList[num];
						Color tabColor2 = tabSheet.TabColor;
						goto IL_7E;
					}
					case 2:
					{
						Color tabColor2;
						if (tabColor2 != tabColor)
						{
							num2 = 4;
							continue;
						}
						num++;
						num2 = 0;
						continue;
					}
					case 3:
						goto IL_C3;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7E;
						default:
							goto IL_BB;
						}
						break;
					case 5:
						return tabColor;
					}
					break;
					IL_7E:
					if (true)
					{
					}
					num2 = 2;
					continue;
					IL_C3:
					num2 = 1;
				}
			}
			IL_BB:
			if (false)
			{
			}
			return spr\u1D39.ᜂ;
		}
	}

	// Token: 0x06002DFB RID: 11771 RVA: 0x0019DF78 File Offset: 0x0019CF78
	public new void ᜀ(Color A_0)
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
	}

	// Token: 0x06002DFC RID: 11772 RVA: 0x0019DFB4 File Offset: 0x0019CFB4
	public IChartShapes ᝎ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002DFD RID: 11773 RVA: 0x0019DFF4 File Offset: 0x0019CFF4
	public IPictures ᝍ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002DFE RID: 11774 RVA: 0x0019E034 File Offset: 0x0019D034
	public IShapes ᝉ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002DFF RID: 11775 RVA: 0x0019E074 File Offset: 0x0019D074
	public void ᝏ()
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
	}

	// Token: 0x06002E00 RID: 11776 RVA: 0x0019E0B0 File Offset: 0x0019D0B0
	public void \u171B()
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
	}

	// Token: 0x06002E01 RID: 11777 RVA: 0x0019E0EC File Offset: 0x0019D0EC
	public bool \u173B()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IWorksheet> innerList = base.InnerList;
				bool isRightToLeft = innerList[0].IsRightToLeft;
				int num = 1;
				int count = innerList.Count;
				int num2 = 7;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (!isRightToLeft)
						{
							num2 = 4;
							continue;
						}
						num++;
						num2 = 1;
						continue;
					case 1:
						if (true)
						{
						}
						goto IL_B3;
					case 2:
						num2 = 0;
						continue;
					case 3:
						return isRightToLeft;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_94;
						default:
							goto IL_105;
						}
						break;
					case 5:
					{
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						ITabSheet tabSheet = innerList[num];
						bool isRightToLeft2 = tabSheet.IsRightToLeft;
						goto IL_94;
					}
					case 6:
					{
						bool isRightToLeft2;
						if (isRightToLeft2 == isRightToLeft)
						{
							num2 = 2;
							continue;
						}
						return false;
					}
					case 7:
						goto IL_B3;
					}
					break;
					IL_94:
					num2 = 6;
					continue;
					IL_B3:
					num2 = 5;
				}
			}
			return false;
			IL_105:
			if (false)
			{
			}
			return false;
		}
	}

	// Token: 0x06002E02 RID: 11778 RVA: 0x0019E20C File Offset: 0x0019D20C
	public void ᜆ(bool A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_27:
				IList<IWorksheet> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				for (;;)
				{
					IL_37:
					if (true)
					{
					}
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_37;
							default:
							{
								if (false)
								{
								}
								ITabSheet tabSheet = innerList[num];
								tabSheet.IsRightToLeft = A_0;
								num++;
								num2 = 3;
								continue;
							}
							}
							break;
						case 1:
							goto IL_4A;
						case 2:
							return;
						case 3:
							goto IL_4A;
						}
						goto IL_27;
						IL_4A:
						num2 = 0;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06002E03 RID: 11779 RVA: 0x0019E2C4 File Offset: 0x0019D2C4
	public bool ᜡ()
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
		return true;
	}

	// Token: 0x06002E04 RID: 11780 RVA: 0x0019E300 File Offset: 0x0019D300
	public ITextBoxes \u1739()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002E05 RID: 11781 RVA: 0x0019E340 File Offset: 0x0019D340
	public ICheckBoxes ᜎ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06002E06 RID: 11782 RVA: 0x0019E380 File Offset: 0x0019D380
	public IRadioButtons \u173E()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002E07 RID: 11783 RVA: 0x0019E3C0 File Offset: 0x0019D3C0
	public IComboBoxes \u1736()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException();
	}

	// Token: 0x06002E08 RID: 11784 RVA: 0x0019E400 File Offset: 0x0019D400
	private new void ᜁ(object A_0, CollectionChangeEventArgs<IWorksheet> A_1)
	{
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
			{
				if (false)
				{
				}
				XlsWorksheetBase xlsWorksheetBase = A_1.Value as XlsWorksheetBase;
				xlsWorksheetBase.SelectTab();
				this.ᜀ.WindowOne.ᜁ((ushort)base.Count);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (base.Count == 1)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						if (true)
						{
						}
						this.ᜀ.WindowOne.ᜇ((ushort)xlsWorksheetBase.RealIndex);
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
			}
		}
	}

	// Token: 0x06002E09 RID: 11785 RVA: 0x0019E4B8 File Offset: 0x0019D4B8
	private new void ᜀ(object A_0, CollectionChangeEventArgs<IWorksheet> A_1)
	{
		int a_ = 10;
		if (base.Count != 1)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_12;
			}
			if (false)
			{
			}
			ITabSheet value = A_1.Value;
			value.Unselect();
			return;
		}
		IL_12:
		if (true)
		{
		}
		throw new ApplicationException(RecordTableEnumerator.b("ి⍁㝃㉅桇㵉⍋㱍㭏⅑㱓㍕㵗⹙籛㵝şౡ੣॥ᱧ䩩๫୭偯qᅳ᭵᝷౹᥻᩽깿", a_));
	}

	// Token: 0x06002E0A RID: 11786 RVA: 0x0019E528 File Offset: 0x0019D528
	private new void ᜀ()
	{
		for (;;)
		{
			int num = 0;
			int count = base.Count;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_6D;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						XlsWorksheetBase xlsWorksheetBase = base.List[num] as XlsWorksheetBase;
						xlsWorksheetBase.Unselect(false);
						num++;
						num2 = 3;
						continue;
					}
					case 1:
						goto IL_6D;
					case 2:
						goto IL_4F;
					case 3:
						goto IL_4F;
					}
					break;
					IL_4F:
					num2 = 0;
				}
				break;
			}
			}
		}
		IL_6D:
		this.ᜀ.WindowOne.ᜁ(0);
	}

	// Token: 0x06002E0B RID: 11787 RVA: 0x0019E5E0 File Offset: 0x0019D5E0
	public virtual object ᜀ(object A_0)
	{
		switch (0)
		{
		default:
		{
			spr\u233D spr_u233D;
			for (;;)
			{
				IL_27:
				if (true)
				{
				}
				spr_u233D = new spr\u233D((spr\u2158)base.ReservedHandle, A_0);
				IList<IWorksheet> innerList = base.InnerList;
				IList<IWorksheet> innerList2 = spr_u233D.InnerList;
				XlsWorkbookObjectsCollection objects = spr_u233D.ᜀ.Objects;
				int num = 0;
				int count = base.Count;
				for (;;)
				{
					IL_66:
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_66;
							default:
							{
								if (false)
								{
								}
								XlsWorksheetBase xlsWorksheetBase = (XlsWorksheetBase)innerList[num];
								int realIndex = xlsWorksheetBase.RealIndex;
								innerList2.Add(objects[realIndex] as IWorksheet);
								num++;
								num2 = 1;
								continue;
							}
							}
							break;
						case 1:
							goto IL_71;
						case 2:
							return spr_u233D;
						case 3:
							goto IL_71;
						}
						goto IL_27;
						IL_71:
						num2 = 0;
					}
				}
			}
			return spr_u233D;
		}
		}
	}

	// Token: 0x06002E0C RID: 11788 RVA: 0x0019E6E0 File Offset: 0x0019D6E0
	internal new bool? ᜀ(spr\u1CCF A_0)
	{
		int a_ = 13;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new Exception(RecordTableEnumerator.b("ᝂⵄ≆楈♊⡌㭎㥐㱒ㅔ睖㙘⥚絜ぞᅠ٢ᝤ٦ᵨɪɬŮ兰ᩲٴ坶᝸ᑺॼ彾릖", a_));
	}

	// Token: 0x06002E0D RID: 11789 RVA: 0x0019E738 File Offset: 0x0019D738
	internal new void ᜀ(spr\u1CCF A_0, bool? A_1)
	{
		int a_ = 0;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new Exception(RecordTableEnumerator.b("戵倷弹᰻匽┿㙁ⱃ⥅ⱇ橉⍋㱍灏㵑⑓㍕⩗㭙⡛㝝ཟౡ䑣ཥ᭧䩩ɫŭѯ剱ᵳ᭵ࡷᙹ᥻፽ꒉ", a_));
	}

	// Token: 0x040014CB RID: 5323
	private new XlsWorkbook ᜀ;

	// Token: 0x040014CC RID: 5324
	private new spr\u207E ᜁ;

	// Token: 0x040014CD RID: 5325
	private new IXLSRange ᜂ;

	// Token: 0x040014CE RID: 5326
	private IMigrantRange ᜃ;

	// Token: 0x040014CF RID: 5327
	private ViewMode ᜄ;

	// Token: 0x040014D0 RID: 5328
	private FormulaEngine ᜅ;

	// Token: 0x040014D1 RID: 5329
	private XlsRange.CellValueChangedEventHandler ᜆ;

	// Token: 0x040014D2 RID: 5330
	private ValueChangedEventHandler ᜇ;
}
