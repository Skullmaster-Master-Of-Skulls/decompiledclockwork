using System;
using System.Collections.Generic;
using System.IO;
using Spire.Doc;
using Spire.Doc.Core.Escher;

// Token: 0x0200030C RID: 780
internal class spr\u257B : spr\u2192
{
	// Token: 0x06002A72 RID: 10866 RVA: 0x002A07C8 File Offset: 0x0029F7C8
	internal spr\u24D8 ᜂ()
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

	// Token: 0x06002A73 RID: 10867 RVA: 0x002A080C File Offset: 0x0029F80C
	internal void ᜀ(spr\u24D8 A_0)
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

	// Token: 0x06002A74 RID: 10868 RVA: 0x002A0850 File Offset: 0x0029F850
	internal spr\u257B(Document A_0) : base(MSOFBT.msofbtSecondaryFOPT, 3, A_0)
	{
		this.ᜀ = new spr\u24D8();
	}

	// Token: 0x06002A75 RID: 10869 RVA: 0x002A0878 File Offset: 0x0029F878
	protected override void ᜁ(Stream A_0)
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
		this.ᜀ.Clear();
		this.ᜀ.ᜀ(A_0, base.\u1717().ᜂ(), base.\u1717().ᜇ());
	}

	// Token: 0x06002A76 RID: 10870 RVA: 0x002A08E0 File Offset: 0x0029F8E0
	protected override void ᜀ(Stream A_0)
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
		base.\u1717().ᜁ(this.ᜀ());
		this.ᜀ.ᜀ(A_0);
	}

	// Token: 0x06002A77 RID: 10871 RVA: 0x002A093C File Offset: 0x0029F93C
	internal virtual spr\u2192 ᜁ()
	{
		switch (0)
		{
		default:
		{
			spr\u257B spr_u257B = new spr\u257B(this.ᜁ);
			IEnumerator<spr\u25B1> enumerator = this.ᜀ.Values.GetEnumerator();
			try
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A1;
					case 1:
					{
						if (!enumerator.MoveNext())
						{
							num = 3;
							continue;
						}
						object obj = enumerator.Current;
						spr\u25B1 a_ = (spr\u25B1)obj;
						spr_u257B.ᜀ.ᜀ(a_);
						num = 4;
						continue;
					}
					case 3:
						num = 0;
						continue;
					}
					IL_7C:
					num = 1;
					continue;
					goto IL_7C;
				}
				IL_A1:;
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_102;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CC;
						default:
							if (false)
							{
							}
							enumerator.Dispose();
							num = 0;
							continue;
						}
						break;
					}
					goto IL_C1;
					IL_CC:
					num = 1;
					continue;
					IL_C1:
					if (true)
					{
					}
					if (enumerator != null)
					{
						goto IL_CC;
					}
					break;
				}
				IL_102:;
			}
			spr_u257B.ᜁ = this.ᜁ;
			return spr_u257B;
		}
		}
	}

	// Token: 0x06002A78 RID: 10872 RVA: 0x002A0A78 File Offset: 0x0029FA78
	internal override void \u170D()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_1E:
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (this.ᜀ != null)
					{
						num = 2;
						continue;
					}
					return;
				case 1:
					return;
				case 2:
					this.ᜀ.Clear();
					this.ᜀ = null;
					num = 1;
					continue;
				}
				goto IL_38;
			}
			return;
		default:
			if (false)
			{
			}
			break;
		}
		IL_38:
		base.\u170D();
		num = 0;
		goto IL_1E;
	}

	// Token: 0x06002A79 RID: 10873 RVA: 0x002A0B04 File Offset: 0x0029FB04
	public uint ᜀ(int A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_7F:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			num = 0;
			break;
		}
		sprẖ sprẖ;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_92;
			case 2:
				if (sprẖ != null)
				{
					num = 1;
					continue;
				}
				return uint.MaxValue;
			case 3:
				goto IL_5C;
			}
			if (!this.ᜂ().ContainsKey(A_0))
			{
				return uint.MaxValue;
			}
			num = 3;
		}
		IL_5C:
		sprẖ = (this.ᜂ()[A_0] as sprẖ);
		goto IL_7F;
		IL_92:
		if (true)
		{
		}
		return sprẖ.ᜁ();
	}

	// Token: 0x06002A7A RID: 10874 RVA: 0x002A0BA8 File Offset: 0x0029FBA8
	public new byte[] ᜁ(int A_0)
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
			if (!this.ᜂ().ContainsKey(A_0))
			{
				return null;
			}
			break;
		}
		sprἬ sprἬ = (sprἬ)this.ᜂ()[A_0];
		return sprἬ.ᜁ();
	}

	// Token: 0x06002A7B RID: 10875 RVA: 0x002A0C10 File Offset: 0x0029FC10
	private int ᜀ()
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
		int num = 0;
		IEnumerator<spr\u25B1> enumerator = this.ᜀ.Values.GetEnumerator();
		try
		{
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_DF;
				case 1:
				{
					if (!enumerator.MoveNext())
					{
						num2 = 6;
						continue;
					}
					object obj = enumerator.Current;
					spr\u25B1 spr_u25B = obj as spr\u25B1;
					num2 = 5;
					continue;
				}
				case 2:
					num++;
					num2 = 3;
					continue;
				case 5:
				{
					spr\u25B1 spr_u25B;
					if (spr_u25B.ᜂ() < 10000)
					{
						num2 = 2;
						continue;
					}
					break;
				}
				case 6:
					num2 = 0;
					continue;
				}
				IL_A8:
				num2 = 1;
				continue;
				goto IL_A8;
			}
			IL_DF:;
		}
		finally
		{
			if (true)
			{
			}
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					enumerator.Dispose();
					num2 = 2;
					continue;
				case 2:
					goto IL_124;
				}
				if (enumerator == null)
				{
					break;
				}
				num2 = 0;
			}
			IL_124:;
		}
		return num;
	}

	// Token: 0x04002505 RID: 9477
	private new spr\u24D8 ᜀ;
}
