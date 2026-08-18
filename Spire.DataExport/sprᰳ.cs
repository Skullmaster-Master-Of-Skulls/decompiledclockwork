using System;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;
using Spire.DataExport.XLS;
using Spire.DataExport.XLS.Formula;

// Token: 0x02000048 RID: 72
internal sealed class spr\u1C33
{
	// Token: 0x06000255 RID: 597 RVA: 0x00015828 File Offset: 0x00014828
	public static sprạ ᜀ(WorkSheet A_0, byte[] A_1, int A_2)
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
		byte a_ = A_1[A_2];
		sprạ sprạ = spr\u1C33.ᜀ(A_0, a_);
		sprạ.ᜀ(A_1, A_2 + 1);
		return sprạ;
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0001587C File Offset: 0x0001487C
	public static sprạ ᜀ(string A_0, FormulaTokenClass A_1, byte A_2)
	{
		switch (0)
		{
		default:
		{
			sprạ sprạ;
			for (;;)
			{
				sprạ = null;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch (A_1)
						{
						case FormulaTokenClass.Reference:
							sprạ = new sprᮺ(FormulaTokenCode.Func1);
							num = 15;
							continue;
						case FormulaTokenClass.Variable:
							sprạ = new sprᮺ(FormulaTokenCode.Func2);
							num = 5;
							continue;
						case FormulaTokenClass.Array:
							sprạ = new sprᮺ(FormulaTokenCode.Func3);
							num = 7;
							continue;
						}
						goto IL_1B7;
					case 1:
						return sprạ;
					case 2:
						goto IL_13F;
					case 3:
						if (spr\u2006.ᜀ().ᜀ(A_0).ᜃ())
						{
							num = 13;
							continue;
						}
						if (true)
						{
						}
						num = 12;
						continue;
					case 4:
						num = 14;
						continue;
					case 5:
						goto IL_1E1;
					case 6:
						goto IL_13F;
					case 7:
						goto IL_1E1;
					case 8:
						return sprạ;
					case 9:
						goto IL_13F;
					case 10:
						goto IL_13F;
					case 11:
						num = 10;
						continue;
					case 12:
						switch (A_1)
						{
						case FormulaTokenClass.Reference:
							sprạ = new spr\u2341(FormulaTokenCode.FuncVar1);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1B7;
							default:
								if (false)
								{
								}
								num = 2;
								continue;
							}
							break;
						case FormulaTokenClass.Variable:
							sprạ = new spr\u2341(FormulaTokenCode.FuncVar2);
							num = 6;
							continue;
						case FormulaTokenClass.Array:
							sprạ = new spr\u2341(FormulaTokenCode.FuncVar3);
							num = 9;
							continue;
						default:
							num = 11;
							continue;
						}
						break;
					case 13:
						num = 0;
						continue;
					case 14:
						goto IL_1E1;
					case 15:
						goto IL_1E1;
					}
					break;
					IL_13F:
					object[] a_ = new object[]
					{
						A_0,
						A_2
					};
					sprạ.ᜀ(a_);
					num = 8;
					continue;
					IL_1B7:
					num = 4;
					continue;
					IL_1E1:
					object[] a_2 = new object[]
					{
						A_0
					};
					sprạ.ᜀ(a_2);
					num = 1;
				}
			}
			return sprạ;
		}
		}
	}

	// Token: 0x06000257 RID: 599 RVA: 0x00015AA8 File Offset: 0x00014AA8
	public static sprạ ᜀ(WorkSheet A_0, FormulaTokenCode A_1)
	{
		int a_ = 4;
		sprạ result;
		for (;;)
		{
			result = null;
			int num = 16;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return result;
				case 1:
					goto IL_345;
				case 2:
					return result;
				case 3:
					num = 12;
					continue;
				case 4:
					return result;
				case 5:
					return result;
				case 6:
					goto IL_32E;
				case 7:
					goto IL_27D;
				case 8:
					goto IL_2AB;
				case 9:
					goto IL_318;
				case 10:
					goto IL_294;
				case 11:
					goto IL_384;
				case 12:
					goto IL_394;
				case 13:
					return result;
				case 14:
					goto IL_265;
				case 15:
					return result;
				case 16:
					switch (A_1)
					{
					case FormulaTokenCode.Exp:
					case FormulaTokenCode.Tbl:
					case FormulaTokenCode.Attr:
						result = new spr\u2407(A_1);
						num = 1;
						continue;
					case FormulaTokenCode.Add:
					case FormulaTokenCode.Sub:
					case FormulaTokenCode.Mul:
					case FormulaTokenCode.Div:
					case FormulaTokenCode.Power:
					case FormulaTokenCode.Concat:
					case FormulaTokenCode.Lt:
					case FormulaTokenCode.Le:
					case FormulaTokenCode.Eq:
					case FormulaTokenCode.Ge:
					case FormulaTokenCode.Gt:
					case FormulaTokenCode.Ne:
					case FormulaTokenCode.Isect:
					case FormulaTokenCode.List:
					case FormulaTokenCode.Range:
						result = new sprᦤ(A_1);
						num = 17;
						continue;
					case FormulaTokenCode.Uplus:
					case FormulaTokenCode.Uminus:
					case FormulaTokenCode.Percent:
					case FormulaTokenCode.Parentheses:
						result = new sprᡊ(A_1);
						num = 5;
						continue;
					case FormulaTokenCode.MissArg:
						result = new sprᴗ();
						num = 6;
						continue;
					case FormulaTokenCode.Str:
						result = new spr\u258C();
						num = 15;
						continue;
					case FormulaTokenCode.Extended:
					case FormulaTokenCode.Sheet:
					case FormulaTokenCode.EndSheet:
					case FormulaTokenCode.Name1:
					case (FormulaTokenCode)38:
					case (FormulaTokenCode)39:
					case (FormulaTokenCode)40:
					case (FormulaTokenCode)41:
					case (FormulaTokenCode)43:
					case (FormulaTokenCode)44:
					case (FormulaTokenCode)45:
					case (FormulaTokenCode)46:
					case (FormulaTokenCode)47:
					case (FormulaTokenCode)48:
					case (FormulaTokenCode)49:
					case (FormulaTokenCode)50:
					case (FormulaTokenCode)51:
					case (FormulaTokenCode)52:
					case (FormulaTokenCode)53:
					case (FormulaTokenCode)54:
					case (FormulaTokenCode)55:
					case (FormulaTokenCode)56:
					case FormulaTokenCode.NameX1:
					case (FormulaTokenCode)62:
					case (FormulaTokenCode)63:
					case FormulaTokenCode.Name2:
					case (FormulaTokenCode)70:
					case (FormulaTokenCode)71:
					case (FormulaTokenCode)72:
					case (FormulaTokenCode)73:
					case (FormulaTokenCode)75:
					case (FormulaTokenCode)76:
					case (FormulaTokenCode)77:
					case (FormulaTokenCode)78:
					case (FormulaTokenCode)79:
					case (FormulaTokenCode)80:
					case (FormulaTokenCode)81:
					case (FormulaTokenCode)82:
					case (FormulaTokenCode)83:
					case (FormulaTokenCode)84:
					case (FormulaTokenCode)85:
					case (FormulaTokenCode)86:
					case (FormulaTokenCode)87:
					case (FormulaTokenCode)88:
					case FormulaTokenCode.NameX2:
					case (FormulaTokenCode)94:
					case (FormulaTokenCode)95:
					case FormulaTokenCode.Name3:
					case (FormulaTokenCode)102:
					case (FormulaTokenCode)103:
					case (FormulaTokenCode)104:
					case (FormulaTokenCode)105:
						goto IL_2DE;
					case FormulaTokenCode.Err:
						result = new spr\u22D3();
						num = 21;
						continue;
					case FormulaTokenCode.Bool:
						result = new spr\u255D();
						num = 11;
						continue;
					case FormulaTokenCode.Int:
						if (true)
						{
						}
						result = new spr\u25EB();
						break;
					case FormulaTokenCode.Num:
						result = new spr\u242E();
						num = 9;
						continue;
					case FormulaTokenCode.Array1:
					case FormulaTokenCode.Array2:
					case FormulaTokenCode.Array3:
						result = new sprᲦ(A_1);
						num = 13;
						continue;
					case FormulaTokenCode.Func1:
					case FormulaTokenCode.Func2:
					case FormulaTokenCode.Func3:
						result = new sprᮺ(A_1);
						num = 20;
						continue;
					case FormulaTokenCode.FuncVar1:
					case FormulaTokenCode.FuncVar2:
					case FormulaTokenCode.FuncVar3:
						result = new spr\u2341(A_1);
						num = 10;
						continue;
					case FormulaTokenCode.Ref1:
					case FormulaTokenCode.Ref2:
					case FormulaTokenCode.Ref3:
						result = new sprᣴ(A_1);
						num = 0;
						continue;
					case FormulaTokenCode.Area1:
					case FormulaTokenCode.Area2:
					case FormulaTokenCode.Area3:
						result = new sprὶ(A_1);
						num = 8;
						continue;
					case FormulaTokenCode.RefErr1:
					case FormulaTokenCode.RefErr2:
					case FormulaTokenCode.RefErr3:
						result = new spr\u1C37(A_1);
						num = 2;
						continue;
					case FormulaTokenCode.Ref3d1:
					case FormulaTokenCode.Ref3d2:
						result = new spr\u203A(A_0, A_1);
						num = 7;
						continue;
					case FormulaTokenCode.Area3d1:
					case FormulaTokenCode.Area3d2:
						result = new spr\u2373(A_0, A_1);
						num = 4;
						continue;
					case FormulaTokenCode.RefErr3d1:
					case FormulaTokenCode.RefErr3d2:
						result = new sprᩀ(A_1);
						num = 18;
						continue;
					case FormulaTokenCode.AreaErr3d1:
					case FormulaTokenCode.AreaErr3d2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							result = new sprἻ(A_1);
							num = 14;
							continue;
						}
						break;
					default:
						num = 3;
						continue;
					}
					num = 19;
					continue;
				case 17:
					goto IL_2C2;
				case 18:
					goto IL_2D9;
				case 19:
					return result;
				case 20:
					return result;
				case 21:
					return result;
				}
				break;
			}
		}
		IL_265:
		IL_27D:
		IL_294:
		IL_2AB:
		IL_2C2:
		IL_2D9:
		return result;
		IL_2DE:
		throw new ArgumentException(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("感倡䌣唥眧挩䈫堭儯帱崳刵氷唹圻嬽⸿", a_)), A_1));
		IL_318:
		IL_32E:
		IL_345:
		IL_384:
		return result;
		IL_394:
		throw new ArgumentException(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("感倡䌣唥眧挩䈫堭儯帱崳刵氷唹圻嬽⸿", a_)), A_1));
	}

	// Token: 0x06000258 RID: 600 RVA: 0x00015F28 File Offset: 0x00014F28
	public static sprạ ᜀ(WorkSheet A_0, byte A_1)
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
		return spr\u1C33.ᜀ(A_0, (FormulaTokenCode)A_1);
	}
}
