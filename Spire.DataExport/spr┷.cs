using System;
using System.Collections;
using System.Text;
using Spire.DataExport.CollectionEditors;

// Token: 0x0200001E RID: 30
internal class spr\u2537
{
	// Token: 0x0600010D RID: 269 RVA: 0x0000ABE8 File Offset: 0x00009BE8
	private string ᜀ(int A_0)
	{
		int a_ = 4;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return string.Format(HyperlinksCollectionEditor.b("帟夡ᐣ嬥", a_), A_0);
	}

	// Token: 0x0600010E RID: 270 RVA: 0x0000AC48 File Offset: 0x00009C48
	public string ᜀ(string A_0)
	{
		spr\u2537.ᜁ ᜁ;
		int num;
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6E:
			A_0 = A_0.Remove(10, A_0.Length - 10);
			ᜁ = new spr\u2537.ᜁ(A_0);
			num = 0;
			num2 = 4;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num2 = 2;
				break;
			}
			break;
		}
		for (;;)
		{
			if (true)
			{
			}
			switch (num2)
			{
			case 0:
				return A_0;
			case 1:
				goto IL_6E;
			case 3:
				this.ᜀ.Insert(~num, ᜁ);
				num2 = 0;
				continue;
			case 4:
				if ((num = this.ᜀ.BinarySearch(ᜁ, new spr\u2537.ᜀ())) < 0)
				{
					num2 = 3;
					continue;
				}
				goto IL_73;
			}
			if (A_0.Length <= 10)
			{
				return A_0;
			}
			num2 = 1;
		}
		IL_73:
		ᜁ = (spr\u2537.ᜁ)this.ᜀ[num];
		ᜁ.ᜁ++;
		string text = this.ᜀ(ᜁ.ᜁ);
		StringBuilder stringBuilder = new StringBuilder(A_0);
		stringBuilder.Remove(stringBuilder.Length - text.Length - 1, text.Length);
		stringBuilder.Append(text);
		return stringBuilder.ToString();
	}

	// Token: 0x04000052 RID: 82
	private ArrayList ᜀ = new ArrayList();

	// Token: 0x0200001F RID: 31
	private class ᜁ
	{
		// Token: 0x0600010F RID: 271 RVA: 0x0000ADA0 File Offset: 0x00009DA0
		public ᜁ(string A_0)
		{
			this.ᜀ = A_0;
		}

		// Token: 0x04000053 RID: 83
		public string ᜀ = string.Empty;

		// Token: 0x04000054 RID: 84
		public int ᜁ;
	}

	// Token: 0x02000020 RID: 32
	private class ᜀ : IComparer
	{
		// Token: 0x06000110 RID: 272 RVA: 0x0000ADC8 File Offset: 0x00009DC8
		int IComparer.ᜀ(object A_0, object A_1)
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
			return string.Compare((A_0 as spr\u2537.ᜁ).ᜀ, (A_1 as spr\u2537.ᜁ).ᜀ, true);
		}
	}
}
