using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004F8 RID: 1272
internal class spr\u1C2C : spr\u214D
{
	// Token: 0x06004DB5 RID: 19893 RVA: 0x002F6D9C File Offset: 0x002F5D9C
	public spr\u1C2C() : this(null)
	{
	}

	// Token: 0x06004DB6 RID: 19894 RVA: 0x002F6DB0 File Offset: 0x002F5DB0
	public spr\u1C2C(IWorksheet A_0) : this(A_0, null)
	{
	}

	// Token: 0x06004DB7 RID: 19895 RVA: 0x002F6DC8 File Offset: 0x002F5DC8
	public spr\u1C2C(IWorksheet A_0, spr\u214D A_1)
	{
		int a_ = 6;
		this.ᜃ = new StringBuilder();
		this.ᜄ = RecordTableEnumerator.b("㔻", a_);
		this.ᜅ = RecordTableEnumerator.b("ㄻ㐽", a_);
		base..ctor(A_0, A_1);
		this.ᜂ(RecordTableEnumerator.b("椻倽⤿⅁⭃≅ⵇṉ⥋㙍⑏", a_));
	}

	// Token: 0x06004DB8 RID: 19896 RVA: 0x002F6E30 File Offset: 0x002F5E30
	public override IDataObject ᜀ()
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
		this.ᜃ();
		return new DataObject(this.ᜇ(), this.ᜃ.ToString());
	}

	// Token: 0x06004DB9 RID: 19897 RVA: 0x002F6E88 File Offset: 0x002F5E88
	public override IDataObject ᜀ(IXLSRange A_0)
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
		this.ᜁ(A_0);
		return new DataObject(this.ᜇ(), this.ᜃ.ToString());
	}

	// Token: 0x06004DBA RID: 19898 RVA: 0x002F6EE0 File Offset: 0x002F5EE0
	protected virtual void ᜃ()
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
		IXLSRange allocatedRange = base.ᜆ().AllocatedRange;
		this.ᜁ(allocatedRange);
	}

	// Token: 0x06004DBB RID: 19899 RVA: 0x002F6F30 File Offset: 0x002F5F30
	protected virtual void ᜁ(IXLSRange A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜃ.Length = 0;
				int column = A_0.Column;
				int lastColumn = A_0.LastColumn;
				int lastRow = A_0.LastRow;
				XlsWorksheet xlsWorksheet = (XlsWorksheet)base.ᜆ();
				XlsCellRecordCollection cellRecords = xlsWorksheet.CellRecords;
				int num = A_0.Row;
				int num2 = 3;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						return;
					case 1:
						this.ᜃ.Append(this.ᜂ());
						num2 = 11;
						continue;
					case 2:
						if (num > lastRow)
						{
							num2 = 0;
							continue;
						}
						num3 = column;
						num2 = 6;
						continue;
					case 3:
						goto IL_180;
					case 4:
						goto IL_9D;
					case 5:
						if (cellRecords.Contains(num, num3))
						{
							num2 = 9;
							continue;
						}
						goto IL_19F;
					case 6:
						goto IL_9D;
					case 7:
						goto IL_180;
					case 8:
						if (num3 > lastColumn)
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
								num2 = 12;
								continue;
							}
						}
						num2 = 5;
						continue;
					case 9:
						this.ᜃ.Append(base.ᜆ().AllocatedRange[num, num3].Value);
						num2 = 13;
						continue;
					case 10:
						if (true)
						{
						}
						if (num3 != lastColumn)
						{
							num2 = 1;
							continue;
						}
						goto IL_169;
					case 11:
						goto IL_169;
					case 12:
						this.ᜃ.Append(this.ᜁ());
						num++;
						num2 = 7;
						continue;
					case 13:
						goto IL_19F;
					}
					break;
					IL_9D:
					num2 = 8;
					continue;
					IL_169:
					num3++;
					num2 = 4;
					continue;
					IL_180:
					num2 = 2;
					continue;
					IL_19F:
					num2 = 10;
				}
			}
			return;
		}
	}

	// Token: 0x06004DBC RID: 19900 RVA: 0x002F713C File Offset: 0x002F613C
	protected override IWorkbook ᜀ(IDataObject A_0, IWorkbooks A_1)
	{
		object data;
		for (;;)
		{
			IL_00:
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 6;
					continue;
				case 2:
					goto IL_5B;
				case 3:
					if (data is string)
					{
						num = 4;
						continue;
					}
					num = 5;
					continue;
				case 4:
					goto IL_F5;
				case 5:
					if (data is MemoryStream)
					{
						num = 2;
						continue;
					}
					goto IL_FA;
				case 6:
					if (A_0.GetDataPresent(this.ᜇ()))
					{
						num = 7;
						continue;
					}
					goto IL_FA;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						data = A_0.GetData(this.ᜇ());
						num = 3;
						continue;
					}
					break;
				}
				if (A_0 == null)
				{
					goto IL_FA;
				}
				num = 0;
			}
		}
		IL_5B:
		return this.ᜀ((MemoryStream)data, A_1);
		IL_F5:
		return this.ᜀ((string)data, A_1);
		IL_FA:
		return null;
	}

	// Token: 0x06004DBD RID: 19901 RVA: 0x002F7244 File Offset: 0x002F6244
	protected override void ᜀ(IDataObject A_0)
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
		this.ᜃ();
		A_0.SetData(this.ᜇ(), this.ᜃ.ToString());
	}

	// Token: 0x06004DBE RID: 19902 RVA: 0x002F729C File Offset: 0x002F629C
	protected override void ᜀ(IDataObject A_0, IXLSRange A_1)
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
		this.ᜁ(A_1);
		A_0.SetData(this.ᜇ(), this.ᜃ.ToString());
	}

	// Token: 0x06004DBF RID: 19903 RVA: 0x002F72F8 File Offset: 0x002F62F8
	private IWorksheet ᜀ(string A_0, spr\u1DF5 A_1, object A_2)
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
		IWorksheet worksheet = new Worksheet((spr\u2158)A_1, A_2);
		this.ᜀ(worksheet, A_0);
		return worksheet;
	}

	// Token: 0x06004DC0 RID: 19904 RVA: 0x002F734C File Offset: 0x002F634C
	private IWorkbook ᜀ(string A_0, IWorkbooks A_1)
	{
		int a_ = 6;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5C;
			case 1:
				goto IL_44;
			case 3:
				if (A_1 == null)
				{
					num = 0;
					continue;
				}
				goto IL_F1;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_E6;
				}
				break;
			case 5:
				if (A_0.Length == 0)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 1;
			}
			else
			{
				num = 5;
			}
		}
		IL_44:
		throw new ArgumentNullException(RecordTableEnumerator.b("堻弽㐿⍁", a_));
		IL_5C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬻儽㈿⥁♃⥅❇ⅉ㽋", a_));
		IL_E6:
		if (false)
		{
		}
		throw new ArgumentException(RecordTableEnumerator.b("伻䨽㈿⭁⩃ⅅ桇⥉ⵋ⁍㹏㵑⁓癕㩗㽙籛㭝ൟቡၣὥ䙧", a_), RecordTableEnumerator.b("堻弽㐿⍁", a_));
		IL_F1:
		IWorkbook workbook = A_1.Create(1);
		IWorksheet a_2 = workbook.Worksheets[0];
		this.ᜀ(a_2, A_0);
		return workbook;
	}

	// Token: 0x06004DC1 RID: 19905 RVA: 0x002F7468 File Offset: 0x002F6468
	private IWorkbook ᜀ(MemoryStream A_0, IWorkbooks A_1)
	{
		int a_ = 12;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_34;
			case 2:
				goto IL_8B;
			case 3:
				if (A_1 != null)
				{
					goto IL_A1;
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
					if (true)
					{
					}
					num = 2;
					continue;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 3;
			}
		}
		IL_34:
		throw new ArgumentNullException(RecordTableEnumerator.b("ㅁぃ㑅ⵇ⭉⅋੍ㅏ♑㕓", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕁⭃㑅⍇⡉⍋⅍㭏⅑", a_));
		IL_A1:
		string @string = Encoding.Unicode.GetString(A_0.ToArray());
		return this.ᜀ(@string, A_1);
	}

	// Token: 0x06004DC2 RID: 19906 RVA: 0x002F7530 File Offset: 0x002F6530
	private void ᜀ(IWorksheet A_0, string A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				if (true)
				{
				}
				int num2;
				int num3;
				int num4;
				int num5;
				int num6;
				int num7;
				switch (num)
				{
				case 0:
					if (A_1[num2] == '\'')
					{
						num = 12;
						continue;
					}
					num = 13;
					continue;
				case 1:
					if (num2 <= num3)
					{
						num = 7;
						continue;
					}
					goto IL_15B;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_15F;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 3:
					if (A_1.Substring(num2, this.ᜂ().Length) == this.ᜂ())
					{
						num = 17;
						continue;
					}
					goto IL_1AE;
				case 5:
					if (A_1[num2] != '"')
					{
						num = 19;
						continue;
					}
					goto IL_2E5;
				case 6:
					goto IL_1DA;
				case 7:
					num = 20;
					continue;
				case 8:
					goto IL_1DA;
				case 9:
					goto IL_1DA;
				case 10:
					goto IL_1DA;
				case 11:
					return;
				case 12:
					goto IL_2E5;
				case 13:
					if (num2 <= num4)
					{
						num = 2;
						continue;
					}
					goto IL_1AE;
				case 14:
					goto IL_1DA;
				case 15:
					A_0.AllocatedRange[num5, num6].Value = A_1.Substring(num7, num2 - num7);
					num7 = num2 + this.ᜁ().Length;
					num2 = num7;
					num5++;
					num6 = 1;
					num = 14;
					continue;
				case 16:
					goto IL_94;
				case 17:
					A_0.AllocatedRange[num5, num6].Value = A_1.Substring(num7, num2 - num7);
					num7 = num2 + this.ᜂ().Length;
					num2 = num7;
					num6++;
					num = 10;
					continue;
				case 18:
					if (num2 >= A_1.Length)
					{
						num = 11;
						continue;
					}
					num = 5;
					continue;
				case 19:
					num = 0;
					continue;
				case 20:
					if (A_1.Substring(num2, this.ᜁ().Length) == this.ᜁ())
					{
						num = 15;
						continue;
					}
					goto IL_15B;
				}
				if (A_0 == null)
				{
					num = 16;
					continue;
				}
				num2 = 0;
				num6 = 1;
				num5 = 1;
				num7 = 0;
				num4 = A_1.Length - this.ᜂ().Length;
				num3 = A_1.Length - this.ᜁ().Length;
				num = 9;
				continue;
				IL_15F:
				num = 6;
				continue;
				IL_15B:
				num2++;
				goto IL_15F;
				IL_1AE:
				num = 1;
				continue;
				IL_1DA:
				num = 18;
				continue;
				IL_2E5:
				num2 = this.ᜀ(A_1, num2);
				num = 8;
			}
			IL_94:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷弹夻䨽", a_));
		}
		}
	}

	// Token: 0x06004DC3 RID: 19907 RVA: 0x002F783C File Offset: 0x002F683C
	private int ᜀ(string A_0, int A_1)
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
		return A_0.IndexOf(A_0[A_1], A_1 + 1) + 1;
	}

	// Token: 0x06004DC4 RID: 19908 RVA: 0x002F788C File Offset: 0x002F688C
	public string ᜂ()
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
		return this.ᜄ;
	}

	// Token: 0x06004DC5 RID: 19909 RVA: 0x002F78D0 File Offset: 0x002F68D0
	public void ᜀ(string A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06004DC6 RID: 19910 RVA: 0x002F7914 File Offset: 0x002F6914
	public string ᜁ()
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

	// Token: 0x06004DC7 RID: 19911 RVA: 0x002F7958 File Offset: 0x002F6958
	public void ᜁ(string A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x0400232F RID: 9007
	public new const string ᜀ = "UnicodeText";

	// Token: 0x04002330 RID: 9008
	public const string ᜁ = "\t";

	// Token: 0x04002331 RID: 9009
	public new const string ᜂ = "\r\n";

	// Token: 0x04002332 RID: 9010
	private StringBuilder ᜃ;

	// Token: 0x04002333 RID: 9011
	private new string ᜄ;

	// Token: 0x04002334 RID: 9012
	private string ᜅ;
}
