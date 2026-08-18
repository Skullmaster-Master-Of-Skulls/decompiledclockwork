using System;
using System.IO;
using System.Text;
using Spire.DataExport.Common;

// Token: 0x02000081 RID: 129
internal class spr\u25E9 : spr\u21B2
{
	// Token: 0x060003CE RID: 974 RVA: 0x00023624 File Offset: 0x00022624
	public spr\u25E9(ExportBase A_0, Stream A_1, TextWriter A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x060003CF RID: 975 RVA: 0x00023648 File Offset: 0x00022648
	public void ᜀ(string A_0, string A_1, bool A_2)
	{
		for (;;)
		{
			this.ᜀ.Length = 0;
			this.ᜀ.Append('<');
			this.ᜀ.Append(A_0);
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ.Append(' ');
					this.ᜀ.Append(A_1);
					num = 1;
					continue;
				case 1:
					goto IL_6C;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B7;
					default:
						if (false)
						{
						}
						this.ᜀ.Append('>');
						num = 4;
						continue;
					}
					break;
				case 3:
					if (A_2)
					{
						num = 2;
						continue;
					}
					goto IL_10E;
				case 4:
					goto IL_10C;
				case 5:
					if (A_1 != null)
					{
						num = 7;
						continue;
					}
					goto IL_6C;
				case 6:
					goto IL_B7;
				case 7:
					if (true)
					{
					}
					num = 6;
					continue;
				}
				break;
				IL_6C:
				num = 3;
				continue;
				IL_B7:
				if (A_1.Length <= 0)
				{
					goto IL_6C;
				}
				num = 0;
			}
		}
		IL_10C:
		IL_10E:
		base.ᜆ(this.ᜀ.ToString());
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x00023774 File Offset: 0x00022774
	public void ᜃ(string A_0, string A_1)
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
		this.ᜀ(A_0, A_1, true);
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x000237B8 File Offset: 0x000227B8
	public void ᜀ(bool A_0)
	{
		for (;;)
		{
			IL_14:
			this.ᜀ.Length = 0;
			for (;;)
			{
				IL_20:
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!A_0)
						{
							num = 1;
							continue;
						}
						goto IL_7B;
					case 1:
						this.ᜀ.Append('/');
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_20;
						}
						if (false)
						{
						}
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						goto IL_79;
					}
					goto IL_14;
				}
			}
		}
		IL_79:
		IL_7B:
		this.ᜀ.Append('>');
		base.ᜆ(this.ᜀ.ToString());
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00023860 File Offset: 0x00022860
	public void ᜁ(string A_0)
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
		this.ᜀ.Length = 0;
		this.ᜀ.Append('<');
		this.ᜀ.Append('/');
		this.ᜀ.Append(A_0);
		this.ᜀ.Append('>');
		base.ᜆ(this.ᜀ.ToString());
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x000238F0 File Offset: 0x000228F0
	public void ᜀ(string A_0, string A_1)
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
		this.ᜀ.Length = 0;
		this.ᜀ.Append('<');
		this.ᜀ.Append(A_0);
		this.ᜀ.Append(' ');
		this.ᜀ.Append(A_1);
		this.ᜀ.Append('/');
		this.ᜀ.Append('>');
		base.ᜆ(this.ᜀ.ToString());
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x0002399C File Offset: 0x0002299C
	public void ᜁ(string A_0, string A_1)
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
		this.ᜃ(A_0, A_1);
		base.ᜌ();
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x000239E8 File Offset: 0x000229E8
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
		this.ᜁ(A_0);
		base.ᜌ();
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00023A30 File Offset: 0x00022A30
	public void ᜂ(string A_0, string A_1)
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
		this.ᜀ(A_0, A_1);
		base.ᜌ();
	}

	// Token: 0x04000283 RID: 643
	private new StringBuilder ᜀ = new StringBuilder();
}
