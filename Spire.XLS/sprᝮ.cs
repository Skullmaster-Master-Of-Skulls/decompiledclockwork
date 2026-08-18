using System;
using Spire.Xls.Calculation;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003E0 RID: 992
internal class sprᝮ : sprᦶ
{
	// Token: 0x06003BE3 RID: 15331 RVA: 0x00217278 File Offset: 0x00216278
	public sprᝮ()
	{
		this.ᜀ = null;
	}

	// Token: 0x06003BE4 RID: 15332 RVA: 0x00217294 File Offset: 0x00216294
	public sprᝮ(IWorksheet A_0)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x06003BE5 RID: 15333 RVA: 0x002172B0 File Offset: 0x002162B0
	public override void ᜀ(int A_0, int A_1, string A_2)
	{
		while (base.ᜇ())
		{
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
				return;
			}
		}
		ValueChangedEventArgs a_ = new ValueChangedEventArgs(A_0, A_1, A_2);
		base.ᜁ(a_);
	}

	// Token: 0x06003BE6 RID: 15334 RVA: 0x00217308 File Offset: 0x00216308
	public override int ᜀ()
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
		return this.ᜀ.AllocatedRange.EndCell.Row;
	}

	// Token: 0x06003BE7 RID: 15335 RVA: 0x00217358 File Offset: 0x00216358
	public override int ᜁ()
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
		return this.ᜀ.AllocatedRange.EndCell.Column;
	}

	// Token: 0x06003BE8 RID: 15336 RVA: 0x002173A8 File Offset: 0x002163A8
	public override object ᜀ(int A_0, int A_1)
	{
		int a_ = 5;
		object obj;
		for (;;)
		{
			IL_25:
			obj = this.ᜀ[A_0, A_1].Formula;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A8;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_25;
					}
					if (false)
					{
					}
					if (obj != null)
					{
						num = 0;
						continue;
					}
					return obj;
				case 2:
					goto IL_6C;
				case 3:
					if (obj == null)
					{
						num = 4;
						continue;
					}
					goto IL_6C;
				case 4:
					if (true)
					{
					}
					obj = this.ᜀ[A_0, A_1].Value;
					num = 2;
					continue;
				}
				break;
				IL_6C:
				num = 1;
			}
		}
		IL_A8:
		return obj.ToString().Replace(RecordTableEnumerator.b("᰺", a_), "");
	}

	// Token: 0x06003BE9 RID: 15337 RVA: 0x00217488 File Offset: 0x00216488
	public override void ᜀ(object A_0, int A_1, int A_2)
	{
		int num = 6;
		bool formulaBoolValue;
		DateTime formulaDateTime;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (bool.TryParse(A_0.ToString(), out formulaBoolValue))
				{
					num = 5;
					continue;
				}
				num = 1;
				continue;
			case 1:
				if (DateTime.TryParse(A_0.ToString(), out formulaDateTime))
				{
					num = 7;
					continue;
				}
				this.ᜀ[A_1, A_2].FormulaStringValue = A_0.ToString();
				num = 8;
				continue;
			case 2:
				goto IL_11F;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 4:
			{
				if (true)
				{
				}
				double num2;
				if (double.TryParse(A_0.ToString(), out num2))
				{
					num = 2;
					continue;
				}
				num = 0;
				continue;
			}
			case 5:
				goto IL_16A;
			case 7:
				goto IL_77;
			case 8:
				goto IL_D1;
			}
			if (!this.ᜀ[A_1, A_2].HasFormula)
			{
				return;
			}
			num = 3;
		}
		IL_77:
		this.ᜀ[A_1, A_2].FormulaDateTime = formulaDateTime;
		return;
		IL_D1:
		return;
		IL_11F:
		this.ᜀ[A_1, A_2].FormulaNumberValue = double.Parse(A_0.ToString());
		return;
		IL_16A:
		this.ᜀ[A_1, A_2].FormulaBoolValue = formulaBoolValue;
	}

	// Token: 0x04001A0A RID: 6666
	private new IWorksheet ᜀ;
}
