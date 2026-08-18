using System;
using System.Collections.Generic;
using System.IO;
using Spire.Doc;
using Spire.Doc.Core.Escher;

// Token: 0x020002E4 RID: 740
internal class spr\u22B7 : spr\u2192
{
	// Token: 0x060028A0 RID: 10400 RVA: 0x00286E4C File Offset: 0x00285E4C
	internal spr\u24A8 ᜂ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					this.ᜅ = new spr\u24A8(this.ᜄ, 511);
					num = 1;
					continue;
				}
				break;
			case 1:
				goto IL_7A;
			}
			if (true)
			{
			}
			if (this.ᜅ != null)
			{
				break;
			}
			num = 0;
		}
		IL_7A:
		return this.ᜅ;
	}

	// Token: 0x060028A1 RID: 10401 RVA: 0x00286EDC File Offset: 0x00285EDC
	internal sprẖ ᜄ()
	{
		while (this.ᜄ.ContainsKey(260))
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
				return this.ᜄ[260] as sprẖ;
			}
		}
		return null;
	}

	// Token: 0x060028A2 RID: 10402 RVA: 0x00286F44 File Offset: 0x00285F44
	internal sprẖ ᜆ()
	{
		sprẖ result;
		for (;;)
		{
			IL_14:
			result = null;
			for (;;)
			{
				IL_20:
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜄ.ContainsKey(128))
						{
							num = 2;
							continue;
						}
						return result;
					case 1:
						return result;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_20;
						default:
							if (false)
							{
							}
							result = (this.ᜄ[128] as sprẖ);
							if (true)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					goto IL_14;
				}
			}
		}
		return result;
	}

	// Token: 0x060028A3 RID: 10403 RVA: 0x00286FE0 File Offset: 0x00285FE0
	internal spr\u24D8 ᜅ()
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

	// Token: 0x060028A4 RID: 10404 RVA: 0x00287024 File Offset: 0x00286024
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
		this.ᜄ = A_0;
	}

	// Token: 0x060028A5 RID: 10405 RVA: 0x00287068 File Offset: 0x00286068
	internal spr\u22B7(Document A_0) : base(MSOFBT.msofbtOPT, 3, A_0)
	{
		this.ᜄ = new spr\u24D8();
	}

	// Token: 0x060028A6 RID: 10406 RVA: 0x00287090 File Offset: 0x00286090
	protected override void ᜁ(Stream A_0)
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
		this.ᜄ.Clear();
		this.ᜄ.ᜀ(A_0, base.\u1717().ᜂ(), base.\u1717().ᜇ());
	}

	// Token: 0x060028A7 RID: 10407 RVA: 0x002870F8 File Offset: 0x002860F8
	protected override void ᜀ(Stream A_0)
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
		base.\u1717().ᜁ(this.ᜀ());
		this.ᜄ.ᜀ(A_0);
	}

	// Token: 0x060028A8 RID: 10408 RVA: 0x00287154 File Offset: 0x00286154
	internal virtual spr\u2192 ᜁ()
	{
		spr\u22B7 spr_u22B;
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
			default:
			{
				if (true)
				{
				}
				spr_u22B = new spr\u22B7(this.ᜁ);
				IEnumerator<spr\u25B1> enumerator = this.ᜄ.Values.GetEnumerator();
				try
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_C5;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							object obj = enumerator.Current;
							spr\u25B1 a_ = (spr\u25B1)obj;
							spr_u22B.ᜄ.ᜀ(a_);
							num = 3;
							continue;
						}
						case 4:
							num = 1;
							continue;
						}
						IL_A0:
						num = 2;
						continue;
						goto IL_A0;
					}
					IL_C5:;
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
							enumerator.Dispose();
							num = 0;
							continue;
						}
						if (enumerator == null)
						{
							break;
						}
						num = 1;
					}
					IL_102:;
				}
				break;
			}
			}
			break;
		}
		spr_u22B.ᜁ = this.ᜁ;
		return spr_u22B;
	}

	// Token: 0x060028A9 RID: 10409 RVA: 0x00287290 File Offset: 0x00286290
	internal override void \u170D()
	{
		for (;;)
		{
			IL_14:
			if (true)
			{
			}
			base.\u170D();
			for (;;)
			{
				IL_2C:
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
							goto IL_2C;
						default:
							if (false)
							{
							}
							this.ᜄ.Clear();
							this.ᜄ = null;
							num = 2;
							continue;
						}
						break;
					case 1:
						if (this.ᜄ != null)
						{
							num = 0;
							continue;
						}
						return;
					case 2:
						return;
					}
					goto IL_14;
				}
			}
		}
	}

	// Token: 0x060028AA RID: 10410 RVA: 0x0028731C File Offset: 0x0028631C
	public uint ᜀ(int A_0)
	{
		sprẖ sprẖ;
		for (;;)
		{
			IL_00:
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
						goto IL_00;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						if (sprẖ != null)
						{
							num = 2;
							continue;
						}
						return uint.MaxValue;
					}
					break;
				case 2:
					goto IL_95;
				case 3:
					sprẖ = (this.ᜅ()[A_0] as sprẖ);
					num = 0;
					continue;
				}
				if (!this.ᜅ().ContainsKey(A_0))
				{
					return uint.MaxValue;
				}
				num = 3;
			}
		}
		IL_95:
		return sprẖ.ᜁ();
	}

	// Token: 0x060028AB RID: 10411 RVA: 0x002873C4 File Offset: 0x002863C4
	internal void ᜀ(int A_0, uint A_1)
	{
		for (;;)
		{
			if (true)
			{
			}
			if (this.ᜄ.ContainsKey(A_0))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			goto IL_56;
		}
		if (false)
		{
		}
		(this.ᜄ[A_0] as sprẖ).ᜀ(A_1);
		return;
		IL_56:
		this.ᜄ.Add(A_0, new sprẖ(A_0, false, A_1));
	}

	// Token: 0x060028AC RID: 10412 RVA: 0x0028743C File Offset: 0x0028643C
	public new byte[] ᜁ(int A_0)
	{
		while (this.ᜅ().ContainsKey(A_0))
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				sprἬ sprἬ = (sprἬ)this.ᜅ()[A_0];
				return sprἬ.ᜁ();
			}
			}
		}
		return null;
	}

	// Token: 0x060028AD RID: 10413 RVA: 0x002874A4 File Offset: 0x002864A4
	private int ᜀ()
	{
		int num;
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
			switch (0)
			{
			default:
			{
				num = 0;
				IEnumerator<spr\u25B1> enumerator = this.ᜄ.Values.GetEnumerator();
				try
				{
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_DD;
						case 1:
						{
							spr\u25B1 spr_u25B;
							if (spr_u25B.ᜂ() < 10000)
							{
								num2 = 5;
								continue;
							}
							break;
						}
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 4;
								continue;
							}
							object obj = enumerator.Current;
							spr\u25B1 spr_u25B = obj as spr\u25B1;
							num2 = 1;
							continue;
						}
						case 4:
							num2 = 0;
							continue;
						case 5:
							num++;
							num2 = 6;
							continue;
						}
						IL_A6:
						num2 = 3;
						continue;
						goto IL_A6;
					}
					IL_DD:;
				}
				finally
				{
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_11A;
						case 1:
							enumerator.Dispose();
							num2 = 0;
							continue;
						}
						if (enumerator == null)
						{
							break;
						}
						num2 = 1;
					}
					IL_11A:;
				}
				break;
			}
			}
			break;
		}
		return num;
	}

	// Token: 0x04002368 RID: 9064
	public new const int ᜀ = 260;

	// Token: 0x04002369 RID: 9065
	public new const int ᜁ = 262;

	// Token: 0x0400236A RID: 9066
	public new const int ᜂ = 128;

	// Token: 0x0400236B RID: 9067
	public new const int ᜃ = 133;

	// Token: 0x0400236C RID: 9068
	private new spr\u24D8 ᜄ;

	// Token: 0x0400236D RID: 9069
	private new spr\u24A8 ᜅ;
}
