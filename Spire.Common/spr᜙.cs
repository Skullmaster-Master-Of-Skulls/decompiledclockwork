using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Spire.Xls;
using Spire.Xls.Conversion.Element.HeaderFooter;

// Token: 0x02000010 RID: 16
internal class spr\u1719
{
	// Token: 0x06000058 RID: 88 RVA: 0x00004AB4 File Offset: 0x00002CB4
	[CompilerGenerated]
	internal sprṶ ᜂ()
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

	// Token: 0x06000059 RID: 89 RVA: 0x00004AF8 File Offset: 0x00002CF8
	[CompilerGenerated]
	private void ᜂ(sprṶ A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00004B3C File Offset: 0x00002D3C
	[CompilerGenerated]
	internal sprṶ ᜀ()
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
		return this.ᜁ;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00004B80 File Offset: 0x00002D80
	[CompilerGenerated]
	private void ᜁ(sprṶ A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00004BC4 File Offset: 0x00002DC4
	[CompilerGenerated]
	internal sprṶ ᜁ()
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
		return this.ᜂ;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00004C08 File Offset: 0x00002E08
	[CompilerGenerated]
	private void ᜀ(sprṶ A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00004C4C File Offset: 0x00002E4C
	[CompilerGenerated]
	internal bool ᜃ()
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
		return this.ᜃ;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00004C90 File Offset: 0x00002E90
	[CompilerGenerated]
	private void ᜀ(bool A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00004CD4 File Offset: 0x00002ED4
	private spr\u1719()
	{
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00004CE8 File Offset: 0x00002EE8
	internal static spr\u1719[] ᜀ(Worksheet A_0)
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
			switch (0)
			{
			}
			break;
		}
		PageSetup pageSetup;
		Dictionary<HFFieldType, string> dictionary;
		for (;;)
		{
			Workbook workbook = A_0.Workbook;
			pageSetup = A_0.PageSetup;
			dictionary = new Dictionary<HFFieldType, string>();
			DateTime now = DateTime.Now;
			dictionary[HFFieldType.Date] = now.ToShortDateString();
			dictionary[HFFieldType.Time] = now.ToShortTimeString();
			dictionary[HFFieldType.SheetName] = A_0.Name;
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (!string.IsNullOrEmpty(workbook.FileName))
					{
						num = 3;
						continue;
					}
					goto IL_138;
				case 1:
					num = 0;
					continue;
				case 2:
					if (workbook != null)
					{
						num = 1;
						continue;
					}
					goto IL_138;
				case 3:
				{
					FileInfo fileInfo = new FileInfo(workbook.FileName);
					string text = fileInfo.FullName;
					string name = fileInfo.Name;
					text = text.Substring(0, text.Length - name.Length);
					dictionary[HFFieldType.FilePath] = text;
					dictionary[HFFieldType.FileName] = name;
					num = 4;
					continue;
				}
				case 4:
					goto IL_10F;
				}
				break;
			}
		}
		IL_10F:
		IL_138:
		spr\u1719[] array = new spr\u1719[2];
		spr\u1719[] array2 = array;
		int num2 = 0;
		spr\u1719 spr_u = new spr\u1719();
		spr_u.ᜀ(true);
		spr_u.ᜂ(sprṶ.ᜀ(pageSetup.LeftHeader, pageSetup.LeftHeaderImage, dictionary));
		spr_u.ᜁ(sprṶ.ᜀ(pageSetup.CenterHeader, pageSetup.CenterHeaderImage, dictionary));
		spr_u.ᜀ(sprṶ.ᜀ(pageSetup.RightHeader, pageSetup.RightHeaderImage, dictionary));
		array2[num2] = spr_u;
		spr\u1719[] array3 = array;
		int num3 = 1;
		spr\u1719 spr_u2 = new spr\u1719();
		spr_u2.ᜀ(false);
		spr_u2.ᜂ(sprṶ.ᜀ(pageSetup.LeftFooter, pageSetup.LeftFooterImage, dictionary));
		spr_u2.ᜁ(sprṶ.ᜀ(pageSetup.CenterFooter, pageSetup.CenterFooterImage, dictionary));
		spr_u2.ᜀ(sprṶ.ᜀ(pageSetup.RightFooter, pageSetup.RightFooterImage, dictionary));
		array3[num3] = spr_u2;
		return array;
	}

	// Token: 0x04000027 RID: 39
	[CompilerGenerated]
	private sprṶ ᜀ;

	// Token: 0x04000028 RID: 40
	[CompilerGenerated]
	private sprṶ ᜁ;

	// Token: 0x04000029 RID: 41
	[CompilerGenerated]
	private sprṶ ᜂ;

	// Token: 0x0400002A RID: 42
	[CompilerGenerated]
	private bool ᜃ;
}
