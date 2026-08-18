using System;
using System.IO;
using System.Windows.Forms;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004F9 RID: 1273
internal class spr\u2322 : spr\u214D
{
	// Token: 0x06004DC8 RID: 19912 RVA: 0x002F799C File Offset: 0x002F699C
	public spr\u2322() : this(null)
	{
	}

	// Token: 0x06004DC9 RID: 19913 RVA: 0x002F79B0 File Offset: 0x002F69B0
	public spr\u2322(spr\u214D A_0)
	{
		int a_ = 7;
		base..ctor(null, A_0);
		this.ᜂ(RecordTableEnumerator.b("缼嘾❀╂組", a_));
	}

	// Token: 0x06004DCA RID: 19914 RVA: 0x002F79E4 File Offset: 0x002F69E4
	public spr\u2322(IWorksheet A_0, spr\u214D A_1)
	{
		int a_ = 4;
		base..ctor(A_0, A_1);
		this.ᜂ(RecordTableEnumerator.b("砹唻堽☿穁", a_));
	}

	// Token: 0x06004DCB RID: 19915 RVA: 0x002F7A18 File Offset: 0x002F6A18
	protected override IWorkbook ᜀ(IDataObject A_0, IWorkbooks A_1)
	{
		int a_ = 11;
		int num = 1;
		object data;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_63;
			case 2:
				if (data != null)
				{
					num = 3;
					continue;
				}
				goto IL_DC;
			case 3:
				goto IL_A1;
			case 4:
				data = A_0.GetData(RecordTableEnumerator.b("̀⩂⍄ⅆ煈", a_), true);
				num = 2;
				continue;
			case 5:
				if (A_0.GetDataPresent(RecordTableEnumerator.b("̀⩂⍄ⅆ煈", a_), true))
				{
					num = 4;
					continue;
				}
				goto IL_DC;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_63:
				num = 5;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (A_0 == null)
				{
					goto IL_DC;
				}
				num = 0;
				break;
			}
		}
		IL_A1:
		Stream stream = (Stream)data;
		return A_1.Open(stream, ExcelParseOptions.Default);
		IL_DC:
		return null;
	}

	// Token: 0x06004DCC RID: 19916 RVA: 0x002F7B04 File Offset: 0x002F6B04
	protected override void ᜀ(IDataObject A_0)
	{
		int a_ = 12;
		if (A_0 == null)
		{
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
				throw new ArgumentNullException(RecordTableEnumerator.b("♁╃㉅⥇Չ⹋⑍㕏ㅑ⁓", a_));
			}
		}
		A_0.SetData(this.ᜇ(), this.ᜁ());
	}

	// Token: 0x06004DCD RID: 19917 RVA: 0x002F7B74 File Offset: 0x002F6B74
	protected override void ᜀ(IDataObject A_0, IXLSRange A_1)
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
	}

	// Token: 0x06004DCE RID: 19918 RVA: 0x002F7BB0 File Offset: 0x002F6BB0
	private MemoryStream ᜁ()
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			MemoryStream memoryStream;
			for (;;)
			{
				XlsWorkbook xlsWorkbook = (XlsWorkbook)base.ᜅ();
				RecordArrayList recordArrayList = new RecordArrayList();
				xlsWorkbook.ᜀ(recordArrayList, (XlsWorksheet)base.ᜆ());
				recordArrayList.UpdateBiffRecordsOffsets();
				memoryStream = new MemoryStream();
				spr\u2496 spr_u = xlsWorkbook.AppImplementation.ᜄ();
				try
				{
					spr\u20C3 spr_u20C = spr_u.ᜀ();
					spr\u1FDC spr_u1FDC = spr_u20C.ᜀ(RecordTableEnumerator.b("樼倾㍀⡂❄⡆♈⁊", a_));
					try
					{
						sprᡄ sprᡄ = new sprᡄ(spr_u1FDC, false);
						try
						{
							sprᡄ.ᜀ(recordArrayList, null);
						}
						finally
						{
							int num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_CA;
								case 2:
									((IDisposable)sprᡄ).Dispose();
									num = 0;
									continue;
								}
								if (sprᡄ == null)
								{
									break;
								}
								num = 2;
							}
							IL_CA:;
						}
						spr_u1FDC.Flush();
					}
					finally
					{
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_113;
							case 2:
								((IDisposable)spr_u1FDC).Dispose();
								num = 1;
								continue;
							}
							if (spr_u1FDC == null)
							{
								break;
							}
							num = 2;
						}
						IL_113:;
					}
					spr_u.ᜀ(memoryStream);
				}
				finally
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							spr_u.Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_15A;
						}
						if (spr_u == null)
						{
							break;
						}
						num = 1;
					}
					IL_15A:;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_173;
				}
			}
			IL_173:
			if (true)
			{
			}
			if (false)
			{
			}
			return memoryStream;
		}
		}
	}

	// Token: 0x06004DCF RID: 19919 RVA: 0x002F7D74 File Offset: 0x002F6D74
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
		MemoryStream data = this.ᜁ();
		return new DataObject(this.ᜇ(), data);
	}

	// Token: 0x06004DD0 RID: 19920 RVA: 0x002F7DC4 File Offset: 0x002F6DC4
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
		return new DataObject();
	}

	// Token: 0x04002335 RID: 9013
	public new const string ᜀ = "Biff8";
}
