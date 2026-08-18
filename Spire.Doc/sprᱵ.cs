using System;
using System.Runtime.InteropServices;
using System.Text;
using Spire.CompoundFile.Doc;
using Spire.Doc.Utilities;

// Token: 0x02000425 RID: 1061
internal class sprᱵ
{
	// Token: 0x06003B13 RID: 15123 RVA: 0x0036F710 File Offset: 0x0036E710
	public void ᜀ(spr\u23A4 A_0)
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
		object obj = Marshal.PtrToStructure(A_0.ᜁ, typeof(CLIPDATA));
		this.ᜀ = (CLIPDATA)obj;
		this.ᜁ = new byte[this.ᜀ.ᜀ];
		Marshal.Copy(this.ᜀ.ᜁ, this.ᜁ, 0, this.ᜁ.Length);
	}

	// Token: 0x06003B14 RID: 15124 RVA: 0x0036F7A4 File Offset: 0x0036E7A4
	public void ᜀ(string A_0)
	{
		int a_ = 10;
		int num = 2;
		string[] array;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_86;
				}
				break;
			case 1:
				num = 3;
				continue;
			case 3:
				if (A_0.Length == 0)
				{
					num = 5;
					continue;
				}
				if (true)
				{
				}
				array = A_0.Split(new char[]
				{
					' '
				});
				num = 4;
				continue;
			case 4:
				if (array.Length < 2)
				{
					num = 0;
					continue;
				}
				goto IL_E3;
			case 5:
				goto IL_CD;
			}
			if (A_0 == null)
			{
				goto IL_98;
			}
			num = 1;
		}
		IL_86:
		if (false)
		{
		}
		throw new ArgumentException(ClipboardData.b("ᑯ፱s᝵", a_));
		IL_98:
		throw new ArgumentException(ClipboardData.b("ᑯ፱s᝵", a_));
		IL_CD:
		goto IL_98;
		IL_E3:
		byte[] value = Convert.FromBase64String(array[0]);
		this.ᜀ.ᜂ = Convert.ToInt32(value);
		this.ᜁ = Convert.FromBase64String(array[1]);
		this.ᜀ.ᜀ = (uint)this.ᜁ.Length;
	}

	// Token: 0x06003B15 RID: 15125 RVA: 0x0036F8D0 File Offset: 0x0036E8D0
	public void ᜁ(spr\u23A4 A_0)
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
		this.ᜀ.ᜁ = Marshal.AllocHGlobal(this.ᜁ.Length);
		Marshal.Copy(this.ᜁ, 0, this.ᜀ.ᜁ, this.ᜁ.Length);
		this.ᜀ.ᜀ = (uint)this.ᜁ.Length;
		Marshal.StructureToPtr(this.ᜀ, A_0.ᜁ, true);
	}

	// Token: 0x06003B16 RID: 15126 RVA: 0x0036F970 File Offset: 0x0036E970
	public string ᜀ()
	{
		int a_ = 1;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		StringBuilder stringBuilder = new StringBuilder();
		byte[] bytes = BitConverter.GetBytes(this.ᜀ.ᜂ);
		stringBuilder.Append(Convert.ToBase64String(bytes));
		stringBuilder.Append(ClipboardData.b("䝦", a_));
		stringBuilder.Append(Convert.ToBase64String(this.ᜁ));
		return stringBuilder.ToString();
	}

	// Token: 0x04002B7E RID: 11134
	private CLIPDATA ᜀ = default(CLIPDATA);

	// Token: 0x04002B7F RID: 11135
	private byte[] ᜁ;
}
