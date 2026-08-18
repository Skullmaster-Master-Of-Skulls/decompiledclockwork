using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000400 RID: 1024
[CLSCompliant(false)]
internal class spr\u231F
{
	// Token: 0x06003D93 RID: 15763 RVA: 0x00224CC8 File Offset: 0x00223CC8
	private spr\u231F()
	{
	}

	// Token: 0x06003D94 RID: 15764 RVA: 0x00224CDC File Offset: 0x00223CDC
	static spr\u231F()
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
		spr\u231F.ᜀ = new Dictionary<int, spr\u1D3B>();
		spr\u231F.ᜀ();
	}

	// Token: 0x06003D95 RID: 15765 RVA: 0x00224D28 File Offset: 0x00223D28
	public static spr\u1D3B ᜀ(spr\u1D3B A_0, MsoRecords A_1, byte[] A_2, ref int A_3)
	{
		spr\u1D3B spr_u1D3B;
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_98;
				case 1:
					if (true)
					{
					}
					if (!spr\u231F.ᜀ.ContainsKey((int)A_1))
					{
						num = 2;
						continue;
					}
					spr_u1D3B = spr\u231F.ᜀ[(int)A_1];
					num = 3;
					continue;
				case 2:
					spr_u1D3B = spr\u231F.ᜀ[65535];
					num = 0;
					continue;
				case 3:
					goto IL_5F;
				}
				break;
			}
		}
		IL_5F:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_98:
			break;
		default:
			if (false)
			{
			}
			break;
		}
		spr_u1D3B = (spr\u1D3B)spr_u1D3B.Clone();
		spr_u1D3B.ᜀ(A_2, A_3);
		A_3 += spr_u1D3B.Length + 8;
		return spr_u1D3B;
	}

	// Token: 0x06003D96 RID: 15766 RVA: 0x00224DF4 File Offset: 0x00223DF4
	public static spr\u1D3B ᜀ(spr\u1D3B A_0, MsoRecords A_1, byte[] A_2, ref int A_3, spr\u24C9 A_4)
	{
		int num = 3;
		spr\u1D3B spr_u1D3B;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_67;
			case 1:
				if (true)
				{
				}
				spr_u1D3B = spr\u231F.ᜀ[65535];
				num = 2;
				continue;
			case 2:
				goto IL_96;
			}
			if (!spr\u231F.ᜀ.ContainsKey((int)A_1))
			{
				num = 1;
			}
			else
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_67;
				default:
					if (false)
					{
					}
					spr_u1D3B = spr\u231F.ᜀ[(int)A_1];
					num = 0;
					break;
				}
			}
		}
		IL_67:
		IL_96:
		spr_u1D3B = (spr\u1D3B)spr_u1D3B.Clone();
		spr_u1D3B.ᜀ(A_4);
		spr_u1D3B.ᜀ(A_2, A_3);
		spr_u1D3B.ᜏ();
		A_3 += spr_u1D3B.Length + 8;
		return spr_u1D3B;
	}

	// Token: 0x06003D97 RID: 15767 RVA: 0x00224ECC File Offset: 0x00223ECC
	public static spr\u1D3B ᜀ(spr\u1D3B A_0, byte[] A_1, ref int A_2)
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
		MsoRecords a_ = (MsoRecords)BitConverter.ToUInt16(A_1, A_2 + 2);
		return spr\u231F.ᜀ(A_0, a_, A_1, ref A_2);
	}

	// Token: 0x06003D98 RID: 15768 RVA: 0x00224F1C File Offset: 0x00223F1C
	public static spr\u1D3B ᜀ(spr\u1D3B A_0, byte[] A_1, ref int A_2, spr\u24C9 A_3)
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
		MsoRecords a_ = (MsoRecords)BitConverter.ToUInt16(A_1, A_2 + 2);
		return spr\u231F.ᜀ(A_0, a_, A_1, ref A_2, A_3);
	}

	// Token: 0x06003D99 RID: 15769 RVA: 0x00224F6C File Offset: 0x00223F6C
	public static spr\u1D3B ᜀ(spr\u1D3B A_0, Stream A_1)
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
		byte[] array = new byte[4];
		A_1.Read(array, 0, 4);
		A_1.Position -= 4L;
		MsoRecords a_ = (MsoRecords)BitConverter.ToUInt16(array, 2);
		return spr\u231F.ᜀ(A_0, a_, A_1);
	}

	// Token: 0x06003D9A RID: 15770 RVA: 0x00224FD8 File Offset: 0x00223FD8
	public static spr\u1D3B ᜀ(spr\u1D3B A_0, MsoRecords A_1, Stream A_2)
	{
		spr\u1D3B spr_u1D3B;
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					spr_u1D3B = spr\u231F.ᜀ[65535];
					num = 2;
					continue;
				case 1:
					goto IL_5F;
				case 2:
					goto IL_98;
				case 3:
					if (true)
					{
					}
					if (!spr\u231F.ᜀ.ContainsKey((int)A_1))
					{
						num = 0;
						continue;
					}
					spr_u1D3B = spr\u231F.ᜀ[(int)A_1];
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_5F:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_98:
			break;
		default:
			if (false)
			{
			}
			break;
		}
		spr_u1D3B = (spr\u1D3B)spr_u1D3B.Clone();
		spr_u1D3B.ᜅ(A_2);
		return spr_u1D3B;
	}

	// Token: 0x06003D9B RID: 15771 RVA: 0x00225094 File Offset: 0x00224094
	public static spr\u1D3B ᜀ(spr\u1D3B A_0, Stream A_1, spr\u24C9 A_2)
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
		byte[] array = new byte[4];
		A_1.Read(array, 0, 4);
		A_1.Position -= 4L;
		MsoRecords a_ = (MsoRecords)BitConverter.ToUInt16(array, 2);
		return spr\u231F.ᜀ(A_0, a_, A_1, A_2);
	}

	// Token: 0x06003D9C RID: 15772 RVA: 0x00225100 File Offset: 0x00224100
	public static spr\u1D3B ᜀ(spr\u1D3B A_0, MsoRecords A_1, Stream A_2, spr\u24C9 A_3)
	{
		int num = 2;
		spr\u1D3B spr_u1D3B;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_79;
			case 1:
				goto IL_96;
			case 3:
				spr_u1D3B = spr\u231F.ᜀ[65535];
				num = 1;
				continue;
			}
			if (true)
			{
			}
			if (!spr\u231F.ᜀ.ContainsKey((int)A_1))
			{
				num = 3;
			}
			else
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_79;
				default:
					if (false)
					{
					}
					spr_u1D3B = spr\u231F.ᜀ[(int)A_1];
					num = 0;
					break;
				}
			}
		}
		IL_79:
		IL_96:
		spr_u1D3B = (spr\u1D3B)spr_u1D3B.Clone();
		spr_u1D3B.ᜀ(A_3);
		spr_u1D3B.ᜅ(A_2);
		spr_u1D3B.ᜏ();
		return spr_u1D3B;
	}

	// Token: 0x06003D9D RID: 15773 RVA: 0x002251C8 File Offset: 0x002241C8
	public static spr\u1D3B ᜀ(MsoRecords A_0)
	{
		spr\u1D3B spr_u1D3B;
		for (;;)
		{
			spr_u1D3B = spr\u231F.ᜀ[(int)A_0];
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					spr_u1D3B = (spr\u1D3B)spr_u1D3B.Clone();
					num = 2;
					continue;
				case 1:
					if (spr_u1D3B != null)
					{
						num = 0;
						continue;
					}
					goto IL_55;
				case 2:
					goto IL_53;
				}
				break;
			}
		}
		IL_53:
		IL_55:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_53;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			return spr_u1D3B;
		}
	}

	// Token: 0x06003D9E RID: 15774 RVA: 0x00225250 File Offset: 0x00224250
	private static void ᜀ(Type A_0, sprᵴ[] A_1)
	{
		int a_ = 16;
		if (true)
		{
		}
		ConstructorInfo constructor = A_0.GetConstructor(new Type[]
		{
			typeof(spr\u1D3B)
		});
		if (constructor == null)
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
				throw new ApplicationException(RecordTableEnumerator.b("Յ⥇⑉≋⅍⑏牑㉓㽕㙗㹙籛㵝ཟౡᝣብᩧὩཫᩭὯq", a_));
			}
		}
		ConstructorInfo constructorInfo = constructor;
		object[] parameters = new object[1];
		object obj = constructorInfo.Invoke(parameters);
		spr\u231F.ᜀ.Add((int)A_1[0].ᜀ(), (spr\u1D3B)obj);
	}

	// Token: 0x06003D9F RID: 15775 RVA: 0x002252F0 File Offset: 0x002242F0
	private static void ᜀ()
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
		spr\u1D3B value = new spr\u2016(null);
		spr\u231F.ᜀ.Add(61453, value);
		value = new sprἼ(null);
		spr\u231F.ᜀ.Add(61450, value);
		value = new spr\u21EB(null);
		spr\u231F.ᜀ.Add(61443, value);
		value = new spr\u232D(null);
		spr\u231F.ᜀ.Add(61454, value);
		value = new sprᮋ(null);
		spr\u231F.ᜀ.Add(61456, value);
		value = new spr\u20A0(null);
		spr\u231F.ᜀ.Add(61442, value);
		value = new spr\u262B(null);
		spr\u231F.ᜀ.Add(61720, value);
		value = new spr\u2608(null);
		spr\u231F.ᜀ.Add(61448, value);
		value = new sprᬈ(null);
		spr\u231F.ᜀ.Add(61440, value);
		value = new spr\u23E7(null);
		spr\u231F.ᜀ.Add(61451, value);
		value = new sprὙ(null);
		spr\u231F.ᜀ.Add(61444, value);
		value = new spr\u227E(null);
		spr\u231F.ᜀ.Add(61726, value);
		value = new spr\u2412(null);
		spr\u231F.ᜀ.Add(61446, value);
		value = new sprᜪ(null);
		spr\u231F.ᜀ.Add(61447, value);
		value = new spr\u1B5C(null);
		spr\u231F.ᜀ.Add(61449, value);
		value = new spr\u1C27(null);
		spr\u231F.ᜀ.Add(61441, value);
		value = new spr᪙(null);
		spr\u231F.ᜀ.Add(61457, value);
		value = new sprᢦ(null);
		spr\u231F.ᜀ.Add(65535, value);
		value = new spr\u23CF(null);
		spr\u231F.ᜀ.Add(61455, value);
	}

	// Token: 0x04001A85 RID: 6789
	private static Dictionary<int, spr\u1D3B> ᜀ;
}
