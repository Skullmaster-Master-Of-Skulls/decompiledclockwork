using System;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records.Formula;

// Token: 0x020004F4 RID: 1268
internal class sprᠮ
{
	// Token: 0x06004D76 RID: 19830 RVA: 0x002F4EA0 File Offset: 0x002F3EA0
	public object ᜀ(Ptg[] A_0, IWorksheet A_1)
	{
		int num = 2;
		for (;;)
		{
			IL_0A:
			switch (num)
			{
			case 0:
				goto IL_53;
			case 1:
				while (A_0.Length == 0)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					if (false)
					{
					}
					num = 4;
					goto IL_0A;
				}
				num = 3;
				continue;
			case 3:
				if (A_0.Length == 1)
				{
					num = 0;
					continue;
				}
				goto IL_A4;
			case 4:
				goto IL_95;
			case 5:
				if (true)
				{
				}
				num = 1;
				continue;
			}
			if (A_0 == null)
			{
				goto IL_55;
			}
			num = 5;
		}
		IL_53:
		Ptg a_ = A_0[0];
		return this.ᜀ(a_, A_1);
		IL_55:
		return null;
		IL_95:
		goto IL_55;
		IL_A4:
		return null;
	}

	// Token: 0x06004D77 RID: 19831 RVA: 0x002F4F54 File Offset: 0x002F3F54
	private object ᜀ(Ptg A_0, IWorksheet A_1)
	{
		object result;
		for (;;)
		{
			IL_48:
			result = null;
			FormulaToken tokenCode = A_0.TokenCode;
			int num = 4;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return result;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						return result;
					case 1:
						return result;
					case 2:
						return result;
					case 3:
						return result;
					case 4:
						if (tokenCode != FormulaToken.tStringConstant)
						{
							if (true)
							{
							}
							num = 7;
							continue;
						}
						result = (A_0 as spr\u24A7).ᜀ();
						num = 0;
						continue;
					case 5:
						return result;
					case 6:
						switch (tokenCode)
						{
						case FormulaToken.tBoolean:
							result = (A_0 as sprᥒ).ᜀ();
							num = 5;
							continue;
						case FormulaToken.tInteger:
							result = (double)(A_0 as sprℿ).ᜀ();
							num = 2;
							continue;
						case FormulaToken.tNumber:
							result = (A_0 as spr\u180B).ᜀ();
							num = 1;
							continue;
						default:
							num = 8;
							continue;
						}
						break;
					case 7:
						num = 6;
						continue;
					case 8:
						num = 3;
						continue;
					}
					goto IL_48;
				}
			}
		}
		return result;
	}
}
