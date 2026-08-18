using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;

// Token: 0x0200050B RID: 1291
internal class spr\u2629
{
	// Token: 0x06004E85 RID: 20101 RVA: 0x002FB2C8 File Offset: 0x002FA2C8
	private spr\u2629()
	{
	}

	// Token: 0x06004E86 RID: 20102 RVA: 0x002FB2DC File Offset: 0x002FA2DC
	static spr\u2629()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_27:
				spr\u2629.ᜀ = new Dictionary<int, spr\u1D3B>();
				Type[] ᜑ = spr\u17FF.ᜑ;
				int num = 0;
				int num2 = ᜑ.Length;
				for (;;)
				{
					IL_3D:
					if (true)
					{
					}
					int num3 = 3;
					for (;;)
					{
						switch (num3)
						{
						case 0:
						{
							if (num >= num2)
							{
								num3 = 1;
								continue;
							}
							Type a_ = ᜑ[num];
							spr\u2629.ᜀ(a_);
							num++;
							num3 = 2;
							continue;
						}
						case 1:
							return;
						case 2:
							goto IL_6C;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_3D;
							default:
								if (false)
								{
								}
								goto IL_6C;
							}
							break;
						}
						goto IL_27;
						IL_6C:
						num3 = 0;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06004E87 RID: 20103 RVA: 0x002FB394 File Offset: 0x002FA394
	private static void ᜀ(Type A_0)
	{
		sprᵴ[] array;
		for (;;)
		{
			for (;;)
			{
				array = (sprᵴ[])A_0.GetCustomAttributes(typeof(sprᵴ), false);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (array.Length == 0)
						{
							num = 3;
							continue;
						}
						goto IL_8D;
					case 1:
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
							if (array != null)
							{
								num = 2;
								continue;
							}
							return;
						}
						break;
					case 2:
						num = 0;
						continue;
					case 3:
						goto IL_8B;
					}
					break;
				}
			}
		}
		return;
		IL_8B:
		return;
		IL_8D:
		spr\u1D3B value = (spr\u1D3B)Activator.CreateInstance(A_0);
		spr\u2629.ᜀ.Add((int)array[0].ᜀ(), value);
	}

	// Token: 0x06004E88 RID: 20104 RVA: 0x002FB450 File Offset: 0x002FA450
	[CLSCompliant(false)]
	public static spr\u1D3B ᜀ(MsoRecords A_0)
	{
		spr\u1D3B spr_u1D3B;
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_44:
				if (spr_u1D3B == null)
				{
					return spr_u1D3B;
				}
				num = 0;
				break;
			default:
				if (false)
				{
				}
				spr_u1D3B = spr\u2629.ᜀ[(int)A_0];
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					spr_u1D3B = (spr\u1D3B)spr_u1D3B.Clone();
					if (true)
					{
					}
					num = 1;
					continue;
				case 1:
					return spr_u1D3B;
				case 2:
					goto IL_44;
				}
				break;
			}
		}
		return spr_u1D3B;
	}

	// Token: 0x0400237F RID: 9087
	private static Dictionary<int, spr\u1D3B> ᜀ;
}
