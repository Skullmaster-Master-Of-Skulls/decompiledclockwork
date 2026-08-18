using System;
using System.IO;
using System.Text;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.Common;

// Token: 0x02000016 RID: 22
internal class spr\u21B2 : spr\u1BFE
{
	// Token: 0x060000CD RID: 205 RVA: 0x00008F84 File Offset: 0x00007F84
	public spr\u21B2(ExportBase A_0, Stream A_1, TextWriter A_2) : base(A_0, A_1)
	{
		this.ᜀ = A_2;
		if (this.ᜀ is StreamWriter)
		{
			(this.ᜀ as StreamWriter).AutoFlush = true;
		}
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00008FC4 File Offset: 0x00007FC4
	public void ᜆ(string A_0)
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
		this.ᜀ.Write(A_0);
	}

	// Token: 0x060000CF RID: 207 RVA: 0x0000900C File Offset: 0x0000800C
	public void ᜇ(string A_0)
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
		this.ᜀ.WriteLine(A_0);
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00009054 File Offset: 0x00008054
	public void ᜌ()
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
		this.ᜇ(string.Empty);
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x0000909C File Offset: 0x0000809C
	public void ᜀ(char A_0, int A_1)
	{
		StringBuilder stringBuilder;
		for (;;)
		{
			stringBuilder = new StringBuilder(A_1);
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_2B;
				case 1:
					goto IL_2B;
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
						if (num >= A_1)
						{
							num2 = 3;
							continue;
						}
						stringBuilder.Append(A_0);
						num++;
						break;
					}
					if (true)
					{
					}
					num2 = 0;
					continue;
				case 3:
					goto IL_65;
				}
				break;
				IL_2B:
				num2 = 2;
			}
		}
		IL_65:
		this.ᜇ(stringBuilder.ToString());
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00009140 File Offset: 0x00008140
	public string ᜀ(string A_0, char A_1, int A_2)
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
		return A_0.PadLeft(A_2, A_1);
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00009184 File Offset: 0x00008184
	public string ᜁ(string A_0, char A_1, int A_2)
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
		return A_0.PadRight(A_2, A_1);
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x000091C8 File Offset: 0x000081C8
	public string ᜂ(string A_0, char A_1, int A_2)
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
		int num = (A_2 - A_0.Length) / 2;
		int num2 = A_2 - A_0.Length - num;
		string text = this.ᜀ(A_0, A_1, A_0.Length + num);
		return this.ᜁ(text, A_1, text.Length + num2);
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x0000923C File Offset: 0x0000823C
	public virtual string ᜀ(ColumAlign A_0)
	{
		int a_ = 12;
		for (;;)
		{
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
						goto IL_6B;
					default:
						goto IL_91;
					}
					break;
				case 1:
					switch (A_0)
					{
					case ColumAlign.Left:
						goto IL_5C;
					case ColumAlign.Center:
						goto IL_4D;
					case ColumAlign.Right:
						goto IL_99;
					default:
						num = 2;
						continue;
					}
					break;
				case 2:
					goto IL_6B;
				}
				break;
				IL_6B:
				if (true)
				{
				}
				num = 0;
			}
		}
		IL_4D:
		return HyperlinksCollectionEditor.b("欧伩䈫娭唯䀱ᐳ圵吷匹嬻倽ⴿ❁⩃㉅", a_);
		IL_5C:
		return HyperlinksCollectionEditor.b("搧伩䨫娭ု匱堳張強吹儻嬽⸿㙁", a_);
		IL_91:
		if (false)
		{
		}
		return string.Empty;
		IL_99:
		return HyperlinksCollectionEditor.b("稧䌩䬫䘭䐯ሱ唳娵儷崹刻匽┿ⱁぃ", a_);
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x000092F8 File Offset: 0x000082F8
	public TextWriter ᜋ()
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

	// Token: 0x060000D7 RID: 215 RVA: 0x0000933C File Offset: 0x0000833C
	public void ᜀ(TextWriter A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x0400002B RID: 43
	private TextWriter ᜀ;
}
