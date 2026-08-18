using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200058D RID: 1421
internal class sprᜭ : List<IOleObject>, IOleObjects
{
	// Token: 0x06005629 RID: 22057 RVA: 0x0036D480 File Offset: 0x0036C480
	public sprᜭ(XlsWorksheet A_0)
	{
		int a_ = 17;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("㑆ⅈ⹊⡌㭎", a_));
		}
		this.ᜀ = A_0;
	}

	// Token: 0x0600562A RID: 22058 RVA: 0x0036D4BC File Offset: 0x0036C4BC
	public void ᜁ(sprᰑ A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_0.ᜆ(spr\u20E9.ᜁ());
				A_0.ᜁ(A_0.ᜇ(), A_0.ᜁ());
				sprᜭ.ᜀ(A_0);
				num = 1;
				continue;
			case 1:
				goto IL_10A;
			case 3:
				if (true)
				{
				}
				if (A_0.ᜊ())
				{
					num = 6;
					continue;
				}
				goto IL_10C;
			case 4:
				num = 7;
				continue;
			case 5:
				num = 3;
				continue;
			case 6:
				goto IL_C9;
			case 7:
				if (A_0.ᜉ() == OleLinkType.Embed)
				{
					num = 0;
					continue;
				}
				goto IL_6F;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_CF;
				default:
					if (false)
					{
					}
					if (A_0.ᜉ() != OleLinkType.Link)
					{
						num = 5;
						continue;
					}
					goto IL_C9;
				}
				break;
			case 9:
				goto IL_DA;
			}
			if (A_0.ᜎ() == null)
			{
				num = 4;
				continue;
			}
			IL_6F:
			num = 8;
			continue;
			IL_CF:
			num = 9;
			continue;
			IL_C9:
			sprᜭ.ᜀ(A_0);
			goto IL_CF;
		}
		IL_DA:
		IL_10A:
		IL_10C:
		base.Add(A_0);
	}

	// Token: 0x0600562B RID: 22059 RVA: 0x0036D5E8 File Offset: 0x0036C5E8
	public IOleObject ᜀ(string A_0, Image A_1, OleLinkType A_2)
	{
		IPictureShape a_;
		for (;;)
		{
			a_ = this.ᜀ.Pictures.Add(1, 1, A_1);
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_87;
				case 1:
					A_0 = Path.GetFullPath(A_0);
					this.ᜀ(A_0);
					num = 0;
					continue;
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
						if (A_2 != OleLinkType.Link)
						{
							goto IL_89;
						}
						break;
					}
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_87:
		IL_89:
		sprᰑ sprᰑ = new sprᰑ(A_0, a_, A_2);
		this.ᜁ(sprᰑ);
		return sprᰑ;
	}

	// Token: 0x0600562C RID: 22060 RVA: 0x0036D690 File Offset: 0x0036C690
	private XlsExternWorkbook ᜀ(string A_0)
	{
		int a_ = 16;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		XlsWorkbook parentWorkbook = this.ᜀ.ParentWorkbook;
		string fileName = Path.GetFileName(A_0);
		string filePath = A_0.Substring(0, A_0.Length - fileName.Length);
		int index = parentWorkbook.ExternWorkbooks.Add(filePath, fileName, null, new string[]
		{
			RecordTableEnumerator.b("慅", a_)
		});
		XlsExternWorkbook xlsExternWorkbook = parentWorkbook.ExternWorkbooks[index];
		xlsExternWorkbook.ProgramId = RecordTableEnumerator.b("ᙅ⥇⥉❋⽍㝏㝑", a_);
		spr\u2141 spr_u = xlsExternWorkbook.ExternNames.ᜀ(0).ᜄ();
		spr_u.ᜄ(true);
		spr_u.ᜃ(false);
		spr_u.ᜁ(true);
		spr_u.ᜀ(true);
		spr_u.ᜂ(false);
		return xlsExternWorkbook;
	}

	// Token: 0x0600562D RID: 22061 RVA: 0x0036D788 File Offset: 0x0036C788
	internal static void ᜀ(sprᰑ A_0)
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

	// Token: 0x0400293A RID: 10554
	private XlsWorksheet ᜀ;
}
