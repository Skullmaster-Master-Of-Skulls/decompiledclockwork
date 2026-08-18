using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x0200013F RID: 319
[CLSCompliant(false)]
internal class spr\u243B : spr\u2276
{
	// Token: 0x06000846 RID: 2118 RVA: 0x0005C3D4 File Offset: 0x0005B3D4
	internal new int ᜄ()
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
		return this.ᜉ;
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x0005C418 File Offset: 0x0005B418
	internal new void ᜁ(int A_0)
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
		this.ᜉ = A_0;
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x0005C45C File Offset: 0x0005B45C
	internal spr\u243B()
	{
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x0005C470 File Offset: 0x0005B470
	internal spr\u243B(Stream A_0, sprᾱ A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x0005C488 File Offset: 0x0005B488
	internal new void ᜀ(int A_0, bool A_1)
	{
		if (true)
		{
		}
		for (;;)
		{
			IL_3C:
			this.ᜂ.Add(A_0);
			for (;;)
			{
				IL_48:
				int num = 1;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_48;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							this.ᜊ++;
							num = 3;
							continue;
						case 1:
							if (A_1)
							{
								num = 0;
								continue;
							}
							goto IL_7F;
						case 2:
							goto IL_87;
						case 3:
							goto IL_7F;
						}
						goto IL_3C;
						IL_7F:
						num = 2;
						break;
					}
				}
			}
		}
		IL_87:
		this.ᜄ.Add((short)(A_1 ? this.ᜊ : 0));
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x0005C538 File Offset: 0x0005B538
	internal new int ᜀ(int A_0)
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
		return (int)this.ᜄ[A_0];
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x0005C580 File Offset: 0x0005B580
	protected override void ᜂ()
	{
		int num = 1;
		for (;;)
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
				switch (num)
				{
				case 0:
					return;
				case 2:
					if (true)
					{
					}
					this.ᜀ.ឌ((int)this.ᜆ.BaseStream.Position);
					base.ᜆ();
					this.ᜀ.\u171D((int)(this.ᜆ.BaseStream.Position - (long)this.ᜀ.\u1738()));
					num = 0;
					continue;
				}
				break;
			}
			if (this.ᜁ.Count <= 0)
			{
				break;
			}
			num = 2;
		}
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x0005C648 File Offset: 0x0005B648
	protected override void ᜃ()
	{
		int num = 2;
		for (;;)
		{
			List<short>.Enumerator enumerator;
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					try
					{
						num = 3;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_B4;
							case 2:
							{
								if (!enumerator.MoveNext())
								{
									num = 4;
									continue;
								}
								short value = enumerator.Current;
								this.ᜆ.Write(value);
								num = 0;
								continue;
							}
							case 4:
								num = 1;
								continue;
							}
							IL_91:
							num = 2;
							continue;
							goto IL_91;
						}
						IL_B4:
						goto IL_124;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_C4;
					IL_124:
					this.ᜀ.ឥ((int)(this.ᜆ.BaseStream.Position - (long)this.ᜀ.ᝤ()));
					num = 3;
					continue;
				}
				break;
			case 1:
				if (true)
				{
				}
				goto IL_C4;
			case 3:
				return;
			}
			IL_2A:
			if (this.ᜄ.Count > 0)
			{
				num = 1;
				continue;
			}
			break;
			goto IL_2A;
			IL_C4:
			this.ᜀ.ᝂ((int)this.ᜆ.BaseStream.Position);
			this.ᜇ(this.ᜋ);
			enumerator = this.ᜄ.GetEnumerator();
			num = 0;
		}
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x0005C7C0 File Offset: 0x0005B7C0
	protected override void ᜀ()
	{
		for (;;)
		{
			int num = this.ᜀ.\u17BD();
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
			{
				if (false)
				{
				}
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						this.ᜅ.BaseStream.Position = (long)this.ᜀ.\u1738();
						int a_ = num / 4;
						base.ᜅ(a_);
						num2 = 2;
						continue;
					}
					case 1:
						if (num > 0)
						{
							num2 = 0;
							continue;
						}
						return;
					case 2:
						return;
					}
					break;
				}
				break;
			}
			}
		}
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x0005C868 File Offset: 0x0005B868
	protected override void ᜁ()
	{
		int num = 0;
		for (;;)
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
				switch (num)
				{
				case 1:
					this.ᜅ.BaseStream.Position = (long)this.ᜀ.ᝤ();
					this.ᜀ.ក();
					this.ᜅ.ReadBytes(this.ᜀ.ក());
					this.ᜅ.BaseStream.Position = (long)this.ᜀ.ᝤ();
					base.ᜁ(this.ᜀ.ក(), 2);
					base.ᜁ();
					num = 2;
					continue;
				case 2:
					return;
				}
				break;
			}
			if (this.ᜀ.ក() <= 0)
			{
				break;
			}
			if (true)
			{
			}
			num = 1;
		}
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0005C95C File Offset: 0x0005B95C
	protected override void ᜀ(BinaryReader A_0, int A_1, int A_2)
	{
		int num = 0;
		for (;;)
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
				switch (num)
				{
				case 1:
					return;
				case 2:
					this.ᜄ.Add(A_0.ReadInt16());
					base.ᜀ(A_0, A_1, A_2);
					num = 1;
					continue;
				}
				break;
			}
			if (A_0.BaseStream.Position >= A_0.BaseStream.Length)
			{
				break;
			}
			num = 2;
		}
	}
}
