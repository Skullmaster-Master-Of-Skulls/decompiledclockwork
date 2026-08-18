using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

// Token: 0x02000594 RID: 1428
[DefaultMember("Item")]
internal class sprủ : ICloneable, IDisposable
{
	// Token: 0x06005697 RID: 22167 RVA: 0x00371EA0 File Offset: 0x00370EA0
	internal sprủ(int A_0, IInternalWorksheet A_1)
	{
		this.ᜇ = A_1;
		this.ᜆ = A_1.ParentWorkbook;
		this.ᜀ = A_0;
	}

	// Token: 0x06005698 RID: 22168 RVA: 0x00371EE8 File Offset: 0x00370EE8
	internal sprủ(sprủ A_0, bool A_1, IInternalWorksheet A_2)
	{
		this.ᜀ = A_0.ᜀ;
		this.ᜇ = A_2;
		this.ᜆ = A_2.ParentWorkbook;
		if (A_1)
		{
			this.ᜂ = A_0.ᜂ;
			this.ᜃ = A_0.ᜃ;
			spr\u223C spr_u223C = A_0.ᜁ;
			for (int i = 0; i < this.ᜀ; i++)
			{
				sprᱧ sprᱧ = A_0.ᜄ().ᜁ(i);
				if (sprᱧ != null)
				{
					this.ᜁ.ᜀ(i, (sprᱧ)sprᱧ.ᜀ(this.ᜆ.HeapHandle));
				}
			}
		}
	}

	// Token: 0x06005699 RID: 22169 RVA: 0x00371FA4 File Offset: 0x00370FA4
	public void ᜉ()
	{
		int num = 6;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_12B;
			case 2:
				num2 = this.ᜂ;
				num = 10;
				continue;
			case 3:
				goto IL_14F;
			case 4:
				goto IL_108;
			case 5:
			{
				sprᱧ sprᱧ;
				if (sprᱧ != null)
				{
					num = 9;
					continue;
				}
				goto IL_B1;
			}
			case 7:
			{
				if (num2 > this.ᜃ)
				{
					num = 13;
					continue;
				}
				sprᱧ sprᱧ = this.ᜁ.ᜁ(num2);
				num = 5;
				continue;
			}
			case 8:
				goto IL_B1;
			case 9:
			{
				sprᱧ sprᱧ;
				sprᱧ.ᜋ();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_108;
				default:
					if (false)
					{
					}
					num = 8;
					continue;
				}
				break;
			}
			case 10:
				goto IL_12B;
			case 11:
				num = 4;
				continue;
			case 12:
				if (this.ᜅ != null)
				{
					num = 14;
					continue;
				}
				goto IL_14F;
			case 13:
				goto IL_64;
			case 14:
				this.ᜅ.Clear();
				this.ᜅ = null;
				num = 3;
				continue;
			}
			if (!this.ᜄ)
			{
				num = 11;
				continue;
			}
			break;
			IL_64:
			this.ᜂ = -1;
			this.ᜃ = -1;
			this.ᜀ = -1;
			this.ᜁ = null;
			this.ᜄ = true;
			this.ᜆ = null;
			num = 12;
			continue;
			IL_108:
			if (this.ᜂ >= 0)
			{
				num = 2;
				continue;
			}
			goto IL_64;
			IL_B1:
			if (true)
			{
			}
			num2++;
			num = 1;
			continue;
			IL_12B:
			num = 7;
			continue;
			IL_14F:
			GC.SuppressFinalize(this);
			num = 0;
		}
	}

	// Token: 0x0600569A RID: 22170 RVA: 0x00372170 File Offset: 0x00371170
	protected virtual void ᜋ()
	{
		try
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
			this.ᜉ();
		}
		finally
		{
			base.Finalize();
		}
	}

	// Token: 0x0600569B RID: 22171 RVA: 0x003721CC File Offset: 0x003711CC
	public virtual object ᜂ()
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
		return new sprủ(this, true, this.ᜇ);
	}

	// Token: 0x0600569C RID: 22172 RVA: 0x00372214 File Offset: 0x00371214
	public virtual object ᜀ(IInternalWorksheet A_0)
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
		return new sprủ(this, true, A_0);
	}

	// Token: 0x0600569D RID: 22173 RVA: 0x00372258 File Offset: 0x00371258
	internal spr\u1DF5 ᜌ()
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
		return this.ᜆ.AppImplementation;
	}

	// Token: 0x0600569E RID: 22174 RVA: 0x003722A0 File Offset: 0x003712A0
	public spr\u223C ᜄ()
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
		return this.ᜁ;
	}

	// Token: 0x0600569F RID: 22175 RVA: 0x003722E4 File Offset: 0x003712E4
	public int ᜆ()
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

	// Token: 0x060056A0 RID: 22176 RVA: 0x00372328 File Offset: 0x00371328
	public void ᜂ(int A_0)
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
		this.ᜁ.ᜀ(A_0);
		this.ᜃ = Math.Min(this.ᜃ, A_0);
		this.ᜂ = Math.Min(this.ᜂ, A_0);
	}

	// Token: 0x060056A1 RID: 22177 RVA: 0x0037239C File Offset: 0x0037139C
	public int ᜁ()
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
		return this.ᜂ;
	}

	// Token: 0x060056A2 RID: 22178 RVA: 0x003723E0 File Offset: 0x003713E0
	public int ᜇ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return this.ᜃ;
	}

	// Token: 0x060056A3 RID: 22179 RVA: 0x00372424 File Offset: 0x00371424
	public object ᜋ(int A_0, int A_1)
	{
		int num = 3;
		sprᱧ sprᱧ;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_A7;
			case 1:
				num = 4;
				continue;
			case 2:
				if (sprᱧ != null)
				{
					num = 5;
					continue;
				}
				goto IL_BC;
			case 4:
				if (A_0 < 0)
				{
					num = 0;
					continue;
				}
				sprᱧ = this.ᜄ().ᜁ(A_0);
				goto IL_76;
			case 5:
				goto IL_89;
			}
			if (A_0 >= this.ᜀ)
			{
				goto IL_8B;
			}
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
				num = 1;
				continue;
			}
			IL_76:
			num = 2;
		}
		IL_89:
		return sprᱧ.ᜆ(A_1, this.ᜌ().\u171D());
		IL_8B:
		return null;
		IL_A7:
		goto IL_8B;
		IL_BC:
		return null;
	}

	// Token: 0x060056A4 RID: 22180 RVA: 0x003724F0 File Offset: 0x003714F0
	public void ᜀ(int A_0, int A_1, object A_2)
	{
		int a_ = 0;
		int num = 4;
		for (;;)
		{
			sprᱧ sprᱧ;
			switch (num)
			{
			case 0:
				goto IL_E6;
			case 1:
				if (A_0 < 0)
				{
					num = 5;
					continue;
				}
				sprᱧ = this.ᜀ(A_0, this.ᜇ.DefaultPrintRowHeight, A_2 != null, this.ᜆ.Version);
				num = 6;
				continue;
			case 2:
				if (true)
				{
				}
				num = 1;
				continue;
			case 3:
				return;
			case 5:
				goto IL_E4;
			case 6:
				if (sprᱧ == null)
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E6;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			if (A_0 < this.ᜀ)
			{
				num = 2;
				continue;
			}
			break;
			IL_E6:
			sprᱧ.ᜀ(this.ᜆ, A_0);
			sprᱧ.ᜁ(A_1, (spr\u23A5)A_2, this.ᜌ().\u171D());
			num = 3;
		}
		IL_B6:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䐵圷䴹画倽␿❁㱃", a_));
		IL_E4:
		goto IL_B6;
	}

	// Token: 0x060056A5 RID: 22181 RVA: 0x00372618 File Offset: 0x00371618
	public Dictionary<long, spr\u1DE2> ᜅ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6F;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C;
				default:
					if (false)
					{
					}
					this.ᜅ = new Dictionary<long, spr\u1DE2>();
					num = 0;
					continue;
				}
				break;
			}
			goto IL_1C;
			IL_2C:
			num = 1;
			continue;
			IL_1C:
			if (true)
			{
			}
			if (this.ᜅ == null)
			{
				goto IL_2C;
			}
			break;
		}
		IL_6F:
		return this.ᜅ;
	}

	// Token: 0x060056A6 RID: 22182 RVA: 0x0037269C File Offset: 0x0037169C
	private int ᜀ()
	{
		if (this.ᜅ == null)
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
				return 0;
			}
		}
		return this.ᜅ.Count;
	}

	// Token: 0x060056A7 RID: 22183 RVA: 0x003726F0 File Offset: 0x003716F0
	public XlsWorkbook ᜈ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return this.ᜆ;
	}

	// Token: 0x060056A8 RID: 22184 RVA: 0x00372734 File Offset: 0x00371734
	public void ᜊ()
	{
		int num = 3;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
			{
				if (num2 > this.ᜃ)
				{
					num = 1;
					continue;
				}
				sprᱧ sprᱧ = this.ᜁ.ᜁ(num2);
				this.ᜁ.ᜀ(num2, null);
				num = 5;
				continue;
			}
			case 1:
				goto IL_D4;
			case 2:
				goto IL_79;
			case 4:
			{
				sprᱧ sprᱧ;
				sprᱧ.ᜋ();
				num = 8;
				continue;
			}
			case 5:
			{
				sprᱧ sprᱧ;
				if (sprᱧ != null)
				{
					if (true)
					{
					}
					num = 4;
					continue;
				}
				goto IL_4A;
			}
			case 6:
				goto IL_79;
			case 7:
				num2 = this.ᜂ;
				num = 2;
				continue;
			case 8:
				goto IL_4A;
			}
			if (this.ᜂ != -1)
			{
				num = 7;
				continue;
			}
			goto IL_D4;
			IL_4A:
			num2++;
			num = 6;
			continue;
			IL_79:
			num = 0;
			continue;
			IL_D4:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_79;
			}
			break;
		}
		if (false)
		{
		}
	}

	// Token: 0x060056A9 RID: 22185 RVA: 0x0037283C File Offset: 0x0037183C
	public virtual sprᱧ ᜀ(int A_0, int A_1, ExcelVersion A_2)
	{
		sprᱧ sprᱧ;
		for (;;)
		{
			sprᱧ = new sprᱧ(A_0, A_1, this.ᜆ.DefaultXFIndex);
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7D;
				case 1:
					goto IL_CB;
				case 2:
					if (true)
					{
					}
					num = 1;
					continue;
				case 3:
					goto IL_BE;
				case 4:
					switch (A_2)
					{
					case ExcelVersion.Version97to2003:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CB;
						default:
							if (false)
							{
							}
							sprᱧ.ᜀ(4, ((spr\u17FF)this.ᜌ()).ᜨ(), A_2);
							num = 3;
							continue;
						}
						break;
					case ExcelVersion.Version2007:
					case ExcelVersion.Version2010:
						sprᱧ.ᜀ(8, ((spr\u17FF)this.ᜌ()).ᜨ(), A_2);
						num = 0;
						continue;
					default:
						num = 2;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_7D:
		IL_BE:
		return sprᱧ;
		IL_CB:
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x060056AA RID: 22186 RVA: 0x00372928 File Offset: 0x00371928
	public bool ᜊ(int A_0, int A_1)
	{
		int num = 1;
		sprᱧ sprᱧ;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜁ == null)
				{
					num = 6;
					continue;
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
					sprᱧ = this.ᜁ.ᜁ(A_0);
					num = 2;
					continue;
				}
				break;
			case 2:
				if (sprᱧ != null)
				{
					num = 7;
					continue;
				}
				goto IL_D6;
			case 3:
				num = 5;
				continue;
			case 4:
				return false;
			case 5:
				if (A_0 >= this.ᜀ)
				{
					num = 4;
					continue;
				}
				num = 0;
				continue;
			case 6:
				return false;
			case 7:
				goto IL_87;
			}
			IL_3A:
			if (A_0 >= 0)
			{
				num = 3;
				continue;
			}
			return false;
			goto IL_3A;
		}
		IL_87:
		return sprᱧ.\u1718(A_1);
		IL_D6:
		if (true)
		{
		}
		return false;
	}

	// Token: 0x060056AB RID: 22187 RVA: 0x00372A14 File Offset: 0x00371A14
	[CLSCompliant(false)]
	internal spr\u225F ᜀ(spr\u23A5 A_0)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 7;
			sprᱧ sprᱧ;
			int a_3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_91;
				case 1:
					goto IL_6D;
				case 2:
					goto IL_10C;
				case 3:
					goto IL_D2;
				case 4:
				{
					Ptg[] array;
					if (array != null)
					{
						num = 5;
						continue;
					}
					goto IL_71;
				}
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D2;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 6:
					if (sprᱧ == null)
					{
						num = 3;
						continue;
					}
					goto IL_18F;
				case 8:
					goto IL_18A;
				case 9:
				{
					spr\u252B spr_u252B;
					if (spr_u252B == null)
					{
						num = 2;
						continue;
					}
					int a_2 = spr_u252B.ᜇ();
					a_3 = spr_u252B.ᜆ();
					sprᱧ = this.ᜄ().ᜁ(a_2);
					num = 6;
					continue;
				}
				case 10:
				{
					Ptg[] array;
					if (array.Length != 1)
					{
						num = 8;
						continue;
					}
					spr\u252B spr_u252B = array[0] as spr\u252B;
					num = 9;
					continue;
				}
				case 11:
				{
					if (A_0.get_TypeCode() != TBIFFRecord.Formula)
					{
						num = 0;
						continue;
					}
					spr᱒ spr᱒ = (spr᱒)A_0;
					Ptg[] array = spr᱒.ᜑ();
					num = 4;
					continue;
				}
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 1;
				}
				else
				{
					num = 11;
				}
			}
			IL_6D:
			throw new ArgumentNullException(RecordTableEnumerator.b("帼娾ⵀ⽂", a_));
			IL_71:
			return null;
			IL_91:
			return null;
			IL_D2:
			return null;
			IL_10C:
			return null;
			IL_18A:
			goto IL_71;
			IL_18F:
			return sprᱧ.\u1713(a_3);
		}
		}
	}

	// Token: 0x060056AC RID: 22188 RVA: 0x00372BBC File Offset: 0x00371BBC
	public void ᜃ(int A_0)
	{
		if (this.ᜂ < 0)
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
				if (true)
				{
				}
				this.ᜂ = A_0;
				this.ᜃ = A_0;
				return;
			}
		}
		this.ᜂ = Math.Min(this.ᜂ, A_0);
		this.ᜃ = Math.Max(this.ᜃ, A_0);
	}

	// Token: 0x060056AD RID: 22189 RVA: 0x00372C38 File Offset: 0x00371C38
	public void ᜀ(int A_0, sprᱧ A_1)
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
		this.ᜄ().ᜀ(A_0, A_1);
		this.ᜃ(A_0);
	}

	// Token: 0x060056AE RID: 22190 RVA: 0x00372C88 File Offset: 0x00371C88
	public void ᜀ(int A_0, int A_1, Rectangle A_2, int A_3, Rectangle A_4)
	{
		int num = 1;
		for (;;)
		{
			if (true)
			{
			}
			int num2;
			switch (num)
			{
			case 0:
			{
				sprᱧ sprᱧ;
				if (sprᱧ != null)
				{
					num = 5;
					continue;
				}
				goto IL_DC;
			}
			case 2:
				goto IL_9B;
			case 3:
				goto IL_9B;
			case 4:
				goto IL_BA;
			case 5:
			{
				sprᱧ sprᱧ;
				int a_;
				sprᱧ.ᜀ(A_0, A_1, A_2, A_3, A_4, a_, this.ᜆ);
				num = 6;
				continue;
			}
			case 6:
				goto IL_DC;
			case 7:
				num = 9;
				continue;
			case 8:
			{
				if (num2 > this.ᜃ)
				{
					num = 4;
					continue;
				}
				sprᱧ sprᱧ = this.ᜁ.ᜁ(num2);
				num = 0;
				continue;
			}
			case 9:
			{
				if (this.ᜁ == null)
				{
					num = 10;
					continue;
				}
				int a_ = this.ᜌ().\u171D();
				num2 = this.ᜂ;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			}
			case 10:
				goto IL_DA;
			}
			if (this.ᜂ >= 0)
			{
				num = 7;
				continue;
			}
			break;
			IL_9B:
			num = 8;
			continue;
			IL_DC:
			num2++;
			num = 3;
		}
		return;
		IL_BA:
		return;
		IL_DA:;
	}

	// Token: 0x060056AF RID: 22191 RVA: 0x00372DCC File Offset: 0x00371DCC
	public void ᜅ(int A_0)
	{
		int num = 3;
		for (;;)
		{
			int a_;
			int num2;
			switch (num)
			{
			case 0:
				return;
			case 1:
			{
				sprᱧ sprᱧ;
				sprᱧ.ᜁ(A_0, A_0 + 1, a_);
				num = 2;
				continue;
			}
			case 2:
				goto IL_46;
			case 4:
				if (num2 <= this.ᜃ)
				{
					sprᱧ sprᱧ = this.ᜁ.ᜁ(num2);
					num = 6;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 5:
				goto IL_77;
			case 6:
			{
				sprᱧ sprᱧ;
				if (sprᱧ != null)
				{
					num = 1;
					continue;
				}
				goto IL_46;
			}
			case 7:
				return;
			case 8:
				goto IL_77;
			}
			if (this.ᜁ == null)
			{
				num = 0;
				continue;
			}
			if (true)
			{
			}
			a_ = this.ᜆ.AppImplementation.ᜨ();
			num2 = this.ᜂ;
			num = 5;
			continue;
			IL_46:
			num2++;
			num = 8;
			continue;
			IL_77:
			num = 4;
		}
	}

	// Token: 0x060056B0 RID: 22192 RVA: 0x00372EDC File Offset: 0x00371EDC
	public void ᜄ(int A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				bool flag;
				int num2;
				sprᱧ sprᱧ;
				int num3;
				switch (num)
				{
				case 0:
					goto IL_193;
				case 1:
					goto IL_1B5;
				case 3:
					this.ᜂ = -1;
					this.ᜃ = -1;
					num = 5;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						flag = true;
						this.ᜂ = num2;
						num = 18;
						continue;
					}
					break;
				case 5:
					goto IL_179;
				case 6:
					goto IL_24C;
				case 7:
					sprᱧ.ᜋ();
					num = 0;
					continue;
				case 8:
					flag = true;
					this.ᜃ = num3;
					num = 1;
					continue;
				case 9:
					goto IL_8C;
				case 10:
					if (num3 < this.ᜂ)
					{
						num = 11;
						continue;
					}
					num = 17;
					continue;
				case 11:
					goto IL_1B5;
				case 12:
					if (!flag)
					{
						num = 3;
						continue;
					}
					return;
				case 13:
					if (sprᱧ != null)
					{
						num = 7;
						continue;
					}
					goto IL_193;
				case 14:
					if (num2 > this.ᜃ)
					{
						if (true)
						{
						}
						num = 21;
						continue;
					}
					num = 19;
					continue;
				case 15:
					goto IL_E1;
				case 16:
					goto IL_E1;
				case 17:
					if (this.ᜁ.ᜁ(num3) != null)
					{
						num = 8;
						continue;
					}
					num3--;
					num = 16;
					continue;
				case 18:
					goto IL_B5;
				case 19:
					if (this.ᜁ.ᜁ(num2) != null)
					{
						num = 4;
						continue;
					}
					num2++;
					num = 20;
					continue;
				case 20:
					goto IL_24C;
				case 21:
					goto IL_B5;
				}
				if (this.ᜁ == null)
				{
					num = 9;
					continue;
				}
				sprᱧ = this.ᜁ.ᜁ(A_0);
				num = 13;
				continue;
				IL_B5:
				num3 = this.ᜃ;
				num = 15;
				continue;
				IL_E1:
				num = 10;
				continue;
				IL_193:
				this.ᜀ(A_0, null);
				flag = false;
				num2 = this.ᜂ;
				num = 6;
				continue;
				IL_1B5:
				num = 12;
				continue;
				IL_24C:
				num = 14;
			}
			IL_8C:
			return;
			IL_179:
			return;
		}
		}
	}

	// Token: 0x060056B1 RID: 22193 RVA: 0x00373164 File Offset: 0x00372164
	public void ᜀ(XlsWorkbook A_0, int[] A_1)
	{
		int a_ = 4;
		int num = 9;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
			{
				if (this.ᜂ < 0)
				{
					num = 10;
					continue;
				}
				if (true)
				{
				}
				int a_2 = this.ᜌ().\u171D();
				num2 = this.ᜂ;
				num = 6;
				continue;
			}
			case 1:
			{
				int a_2;
				sprᱧ sprᱧ;
				sprᱧ.ᜀ(A_0, A_1, a_2);
				num = 4;
				continue;
			}
			case 2:
			{
				if (num2 > this.ᜃ)
				{
					num = 5;
					continue;
				}
				sprᱧ sprᱧ = this.ᜁ.ᜁ(num2);
				num = 7;
				continue;
			}
			case 3:
				goto IL_D0;
			case 4:
				goto IL_105;
			case 5:
				return;
			case 6:
				goto IL_D0;
			case 7:
			{
				sprᱧ sprᱧ;
				if (sprᱧ != null)
				{
					num = 1;
					continue;
				}
				goto IL_105;
			}
			case 8:
				goto IL_50;
			case 10:
				return;
			}
			if (A_1 == null)
			{
				num = 8;
				continue;
			}
			num = 0;
			continue;
			IL_D0:
			num = 2;
			continue;
			IL_105:
			num2++;
			num = 3;
		}
		IL_50:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			break;
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("嬹主䰽฿❁㍃ཅ♇⹉⥋㙍", a_));
	}

	// Token: 0x060056B2 RID: 22194 RVA: 0x003732BC File Offset: 0x003722BC
	public void ᜀ(XlsWorkbook A_0, IDictionary<int, int> A_1)
	{
		int a_ = 16;
		int num = 5;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				return;
			case 1:
			{
				sprᱧ sprᱧ;
				if (sprᱧ != null)
				{
					num = 6;
					continue;
				}
				goto IL_10D;
			}
			case 2:
				goto IL_D8;
			case 3:
				return;
			case 4:
			{
				if (this.ᜂ < 0)
				{
					num = 3;
					continue;
				}
				int a_2 = this.ᜌ().\u171D();
				num2 = this.ᜂ;
				num = 2;
				continue;
			}
			case 6:
			{
				sprᱧ sprᱧ;
				int a_2;
				sprᱧ.ᜀ(A_0, A_1, a_2);
				num = 9;
				continue;
			}
			case 7:
			{
				if (num2 > this.ᜃ)
				{
					num = 0;
					continue;
				}
				sprᱧ sprᱧ = this.ᜁ.ᜁ(num2);
				num = 1;
				continue;
			}
			case 8:
				goto IL_D8;
			case 9:
				goto IL_10D;
			case 10:
				goto IL_50;
			}
			if (A_1 == null)
			{
				num = 10;
				continue;
			}
			num = 4;
			continue;
			IL_D8:
			num = 7;
			continue;
			IL_10D:
			num2++;
			num = 8;
		}
		IL_50:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			break;
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("≅ⅇ⥉ɋ⭍❏᭑㩓㉕㵗≙", a_));
	}

	// Token: 0x060056B3 RID: 22195 RVA: 0x00373414 File Offset: 0x00372414
	public void ᜀ(XlsWorkbook A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				Dictionary<long, spr\u1DE2>.Enumerator enumerator;
				switch (num)
				{
				case 1:
					try
					{
						num = 4;
						for (;;)
						{
							switch (num)
							{
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_FD;
								default:
									if (false)
									{
									}
									num = 3;
									continue;
								}
								break;
							case 2:
							{
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								KeyValuePair<long, spr\u1DE2> keyValuePair = enumerator.Current;
								long key = keyValuePair.Key;
								spr\u1DE2 value = keyValuePair.Value;
								int a_ = sprṔ.ᜁ(key);
								int a_2 = sprṔ.ᜀ(key);
								this.ᜀ(A_0, a_, a_2, value);
								num = 0;
								continue;
							}
							case 3:
								goto IL_FD;
							}
							IL_78:
							num = 2;
							continue;
							goto IL_78;
						}
						IL_FD:
						goto IL_133;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_10D;
				case 2:
					goto IL_10D;
				}
				if (this.ᜀ() > 0)
				{
					num = 2;
					continue;
				}
				break;
				IL_10D:
				if (true)
				{
				}
				enumerator = this.ᜅ().GetEnumerator();
				num = 1;
			}
			IL_133:
			this.ᜅ = null;
			return;
		}
		}
	}

	// Token: 0x060056B4 RID: 22196 RVA: 0x0037356C File Offset: 0x0037256C
	[CLSCompliant(false)]
	internal void ᜀ(XlsWorkbook A_0, int A_1, int A_2, spr\u1DE2 A_3)
	{
		for (;;)
		{
			int num = A_3.ᜀ();
			int num2 = A_3.ᜅ();
			int num3 = 4;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (num <= num2)
					{
						sprᱧ sprᱧ = this.ᜁ.ᜁ(num);
						num3 = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num3 = 6;
						continue;
					}
					break;
				case 1:
					goto IL_3E;
				case 2:
					goto IL_83;
				case 3:
				{
					sprᱧ sprᱧ;
					if (sprᱧ != null)
					{
						num3 = 5;
						continue;
					}
					goto IL_3E;
				}
				case 4:
					goto IL_83;
				case 5:
				{
					sprᱧ sprᱧ;
					sprᱧ.ᜀ(A_0, A_1, A_2, A_3);
					num3 = 1;
					continue;
				}
				case 6:
					return;
				}
				break;
				IL_3E:
				num++;
				num3 = 2;
				continue;
				IL_83:
				num3 = 0;
			}
		}
	}

	// Token: 0x060056B5 RID: 22197 RVA: 0x00373648 File Offset: 0x00372648
	public void ᜀ(List<int> A_0)
	{
		int a_ = 3;
		int num = 8;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				goto IL_10B;
			case 1:
				return;
			case 2:
				if (this.ᜂ < 0)
				{
					num = 4;
					continue;
				}
				num2 = this.ᜂ;
				num = 6;
				continue;
			case 3:
				goto IL_D6;
			case 4:
				return;
			case 5:
			{
				if (num2 > this.ᜃ)
				{
					num = 1;
					continue;
				}
				sprᱧ sprᱧ = this.ᜁ.ᜁ(num2);
				num = 9;
				continue;
			}
			case 6:
				goto IL_D6;
			case 7:
				goto IL_58;
			case 9:
			{
				sprᱧ sprᱧ;
				if (sprᱧ != null)
				{
					num = 10;
					continue;
				}
				goto IL_10B;
			}
			case 10:
			{
				sprᱧ sprᱧ;
				sprᱧ.ᜀ(A_0);
				num = 0;
				continue;
			}
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 7;
				continue;
			}
			num = 2;
			continue;
			IL_D6:
			num = 5;
			continue;
			IL_10B:
			num2++;
			num = 3;
		}
		IL_58:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			break;
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("堸䤺似焾⑀㑂ౄ⥆ⵈ⹊㕌⩎≐", a_));
	}

	// Token: 0x060056B6 RID: 22198 RVA: 0x00373790 File Offset: 0x00372790
	public void ᜀ(sprủ A_0, SSTDictionary A_1, SSTDictionary A_2, Dictionary<int, int> A_3, Dictionary<string, string> A_4, Dictionary<int, int> A_5, Dictionary<int, int> A_6, Dictionary<int, int> A_7)
	{
		for (;;)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_EC:
				num = 5;
				break;
			default:
				if (false)
				{
				}
				this.ᜂ = A_0.ᜂ;
				this.ᜃ = A_0.ᜃ;
				this.ᜁ = new spr\u223C();
				num = 6;
				break;
			}
			for (;;)
			{
				if (true)
				{
				}
				int num2;
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					sprᱧ sprᱧ;
					sprᱧ a_ = sprᱧ.ᜁ(A_1, A_2, A_3, A_4, A_5, A_6, A_7);
					this.ᜀ(num2, a_);
					num = 7;
					continue;
				}
				case 2:
					goto IL_D8;
				case 3:
				{
					sprᱧ sprᱧ;
					if (sprᱧ != null)
					{
						num = 1;
						continue;
					}
					goto IL_9E;
				}
				case 4:
					goto IL_D8;
				case 5:
					return;
				case 6:
					if (A_0.ᜂ < 0)
					{
						num = 0;
						continue;
					}
					num2 = this.ᜂ;
					num = 4;
					continue;
				case 7:
					goto IL_9E;
				case 8:
				{
					if (num2 > this.ᜃ)
					{
						goto IL_EC;
					}
					sprᱧ sprᱧ = A_0.ᜁ.ᜁ(num2);
					num = 3;
					continue;
				}
				}
				break;
				IL_9E:
				num2++;
				num = 2;
				continue;
				IL_D8:
				num = 8;
			}
		}
	}

	// Token: 0x060056B7 RID: 22199 RVA: 0x003738D8 File Offset: 0x003728D8
	public List<long> ᜀ(IXLSRange A_0, string A_1, FindType A_2, bool A_3, XlsWorkbook A_4)
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
		return this.ᜀ(A_0, A_1, A_2, ExcelFindOptions.None, A_3, A_4);
	}

	// Token: 0x060056B8 RID: 22200 RVA: 0x00373924 File Offset: 0x00372924
	public List<long> ᜀ(IXLSRange A_0, string A_1, FindType A_2, ExcelFindOptions A_3, bool A_4, XlsWorkbook A_5)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 5;
			List<long> list;
			for (;;)
			{
				int num2;
				int a_2;
				int a_3;
				int a_4;
				bool flag;
				bool flag5;
				switch (num)
				{
				case 0:
					goto IL_273;
				case 1:
				{
					num2 = A_0.Row - 1;
					int num3 = A_0.LastRow - 1;
					num = 29;
					continue;
				}
				case 2:
				{
					sprᱧ sprᱧ;
					list.AddRange(sprᱧ.ᜀ(a_2, a_3, A_1, A_2, A_3, a_4, A_4, A_5));
					num = 21;
					continue;
				}
				case 3:
					goto IL_236;
				case 4:
				{
					sprᱧ sprᱧ;
					if (sprᱧ != null)
					{
						num = 2;
						continue;
					}
					goto IL_183;
				}
				case 6:
					if (!flag)
					{
						num = 28;
						continue;
					}
					goto IL_19A;
				case 7:
					if (this.ᜁ != null)
					{
						num = 1;
						continue;
					}
					goto IL_3A1;
				case 8:
					if (flag)
					{
						goto IL_354;
					}
					goto IL_BB;
				case 9:
				{
					bool flag2;
					if (!flag2)
					{
						num = 17;
						continue;
					}
					goto IL_19A;
				}
				case 10:
				{
					bool flag3;
					if (!flag3)
					{
						num = 12;
						continue;
					}
					goto IL_19A;
				}
				case 11:
				{
					bool flag4;
					if (!flag4)
					{
						num = 22;
						continue;
					}
					goto IL_19A;
				}
				case 12:
					num = 6;
					continue;
				case 13:
					if (flag)
					{
						num = 15;
						continue;
					}
					num = 20;
					continue;
				case 14:
					goto IL_163;
				case 15:
					num = 18;
					continue;
				case 16:
					num = 26;
					continue;
				case 17:
					goto IL_297;
				case 18:
				{
					bool flag6;
					flag5 = flag6;
					goto IL_33A;
				}
				case 19:
					goto IL_B6;
				case 20:
					flag5 = false;
					goto IL_33A;
				case 21:
					goto IL_183;
				case 22:
					num = 10;
					continue;
				case 23:
					a_4 = FormulaUtil.ErrorNameToCode[A_1];
					num = 27;
					continue;
				case 24:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_354;
					default:
					{
						if (false)
						{
						}
						int num3;
						if (num2 > num3)
						{
							num = 0;
							continue;
						}
						sprᱧ sprᱧ = this.ᜁ.ᜁ(num2);
						num = 4;
						continue;
					}
					}
					break;
				case 25:
					if (A_1 != null)
					{
						num = 16;
						continue;
					}
					goto IL_109;
				case 26:
				{
					if (true)
					{
					}
					if (A_1.Length == 0)
					{
						num = 14;
						continue;
					}
					bool flag6 = FormulaUtil.ErrorNameToCode.ContainsKey(A_1);
					bool flag4 = (A_2 & FindType.Text) == FindType.Text;
					flag = ((A_2 & FindType.Error) == FindType.Error);
					bool flag3 = (A_2 & FindType.Formula) == FindType.Formula;
					bool flag2 = (A_2 & FindType.FormulaStringValue) == FindType.FormulaStringValue;
					num = 11;
					continue;
				}
				case 27:
					goto IL_BB;
				case 28:
					num = 9;
					continue;
				case 29:
					goto IL_236;
				}
				if (A_0 == null)
				{
					num = 19;
					continue;
				}
				num = 25;
				continue;
				IL_BB:
				a_2 = A_0.Column - 1;
				a_3 = A_0.LastColumn - 1;
				num = 7;
				continue;
				IL_183:
				num2++;
				num = 3;
				continue;
				IL_19A:
				a_4 = 0;
				num = 13;
				continue;
				IL_236:
				num = 24;
				continue;
				IL_33A:
				flag = flag5;
				list = new List<long>();
				num = 8;
				continue;
				IL_354:
				num = 23;
			}
			IL_B6:
			throw new ArgumentNullException(RecordTableEnumerator.b("䐵夷吹嬻嬽", a_));
			IL_109:
			return null;
			IL_163:
			goto IL_109;
			IL_273:
			goto IL_3A1;
			IL_297:
			throw new ArgumentException(RecordTableEnumerator.b("昵夷䠹崻匽┿㙁⅃㑅桇ⱉ⁋⽍㝏⅑瑓㽕⭗穙㉛ㅝᑟ䉡ባݥѧͩ࡫䁭", a_), RecordTableEnumerator.b("倵吷嬹嬻䴽", a_));
			IL_3A1:
			A_5.IsStartsOrEndsWith = null;
			return list;
		}
		}
	}

	// Token: 0x060056B9 RID: 22201 RVA: 0x00373CE8 File Offset: 0x00372CE8
	public List<long> ᜀ(IXLSRange A_0, double A_1, FindType A_2, bool A_3, XlsWorkbook A_4)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 4;
			List<long> list;
			for (;;)
			{
				IL_2C:
				bool flag;
				int i;
				bool flag2;
				int a_2;
				int a_3;
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (!flag)
					{
						num = 11;
						continue;
					}
					goto IL_197;
				case 2:
					goto IL_11E;
				case 3:
					num = 17;
					continue;
				case 5:
					goto IL_87;
				case 6:
				{
					sprᱧ sprᱧ;
					if (sprᱧ != null)
					{
						num = 16;
						continue;
					}
					goto IL_11E;
				}
				case 7:
				{
					i = A_0.Row - 1;
					int num2 = A_0.LastRow - 1;
					num = 15;
					continue;
				}
				case 8:
					goto IL_174;
				case 9:
					if (!flag2)
					{
						num = 0;
						continue;
					}
					goto IL_197;
				case 10:
				{
					int num2;
					while (i <= num2)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
						{
							if (false)
							{
							}
							sprᱧ sprᱧ = this.ᜁ.ᜁ(i);
							num = 6;
							goto IL_2C;
						}
						}
					}
					num = 12;
					continue;
				}
				case 11:
					goto IL_1F3;
				case 12:
					goto IL_192;
				case 13:
					if (A_3)
					{
						num = 3;
						continue;
					}
					goto IL_11E;
				case 14:
					if (this.ᜁ != null)
					{
						num = 7;
						continue;
					}
					return list;
				case 15:
					goto IL_174;
				case 16:
				{
					sprᱧ sprᱧ;
					list.AddRange(sprᱧ.ᜀ(a_2, a_3, A_1, A_2, A_3, A_4));
					num = 13;
					continue;
				}
				case 17:
					if (true)
					{
					}
					if (list.Count <= 0)
					{
						num = 2;
						continue;
					}
					return list;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				flag2 = ((A_2 & FindType.FormulaValue) != (FindType)0);
				flag = ((A_2 & FindType.Number) != (FindType)0);
				num = 9;
				continue;
				IL_11E:
				i++;
				num = 8;
				continue;
				IL_174:
				num = 10;
				continue;
				IL_197:
				list = new List<long>();
				a_2 = A_0.Column - 1;
				a_3 = A_0.LastColumn - 1;
				num = 14;
			}
			IL_87:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰽ℿⱁ⍃⍅", a_));
			IL_192:
			return list;
			IL_1F3:
			throw new ArgumentException(RecordTableEnumerator.b("渽ℿぁ╃⭅ⵇ㹉⥋㱍灏㑑㡓㝕㽗⥙籛㝝፟䉡੣॥ᱧ䩩ᩫ཭ᱯ᭱ၳ塵", a_), RecordTableEnumerator.b("堽ⰿ⍁⍃㕅", a_));
		}
		}
	}

	// Token: 0x060056BA RID: 22202 RVA: 0x00373F5C File Offset: 0x00372F5C
	public List<long> ᜀ(IXLSRange A_0, byte A_1, bool A_2, bool A_3, XlsWorkbook A_4)
	{
		int a_ = 12;
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 4;
				for (;;)
				{
					List<long> list;
					int num2;
					int a_2;
					int a_3;
					switch (num)
					{
					case 0:
					{
						sprᱧ sprᱧ;
						if (sprᱧ != null)
						{
							num = 10;
							continue;
						}
						goto IL_156;
					}
					case 1:
						goto IL_120;
					case 2:
						return list;
					case 3:
						goto IL_156;
					case 5:
						goto IL_6B;
					case 6:
						if (this.ᜁ != null)
						{
							num = 7;
							continue;
						}
						return list;
					case 7:
					{
						num2 = A_0.Row - 1;
						int num3 = A_0.LastRow - 1;
						num = 1;
						continue;
					}
					case 8:
					{
						int num3;
						if (num2 > num3)
						{
							num = 2;
							continue;
						}
						sprᱧ sprᱧ = this.ᜁ.ᜁ(num2);
						num = 0;
						continue;
					}
					case 9:
						goto IL_120;
					case 10:
					{
						sprᱧ sprᱧ;
						list.AddRange(sprᱧ.ᜀ(a_2, a_3, A_1, A_2, A_3, A_4));
						num = 3;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 5;
						continue;
					}
					list = new List<long>();
					a_2 = A_0.Column - 1;
					a_3 = A_0.LastColumn - 1;
					if (true)
					{
					}
					num = 6;
					continue;
					IL_120:
					num = 8;
					continue;
					IL_156:
					num2++;
					num = 9;
				}
				IL_6B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_81;
				}
			}
			IL_81:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ぁ╃⡅⽇⽉", a_));
		}
	}

	// Token: 0x060056BB RID: 22203 RVA: 0x003740F4 File Offset: 0x003730F4
	public int ᜀ(int A_0, int A_1)
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
				for (;;)
				{
					bool flag = false;
					int num = int.MaxValue;
					int num2 = 6;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
						{
							sprᱧ sprᱧ;
							num = Math.Min(num, sprᱧ.\u171C());
							flag = true;
							num2 = 11;
							continue;
						}
						case 1:
							num2 = 8;
							continue;
						case 2:
							if (!flag)
							{
								num2 = 9;
								continue;
							}
							return num;
						case 3:
							goto IL_7A;
						case 4:
						{
							if (num3 > A_1)
							{
								num2 = 12;
								continue;
							}
							sprᱧ sprᱧ = this.ᜁ.ᜁ(num3);
							num2 = 7;
							continue;
						}
						case 5:
							goto IL_7A;
						case 6:
							if (this.ᜁ == null)
							{
								if (true)
								{
								}
								num2 = 10;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num3 = A_0;
								num2 = 3;
								continue;
							}
							break;
						case 7:
						{
							sprᱧ sprᱧ;
							if (sprᱧ != null)
							{
								num2 = 1;
								continue;
							}
							goto IL_10D;
						}
						case 8:
						{
							sprᱧ sprᱧ;
							if (sprᱧ.ᜈ() > 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_10D;
						}
						case 9:
							return -1;
						case 10:
							return -1;
						case 11:
							goto IL_10D;
						case 12:
							num2 = 2;
							continue;
						}
						break;
						IL_7A:
						num2 = 4;
						continue;
						IL_10D:
						num3++;
						num2 = 5;
					}
				}
				break;
			}
		}
		return -1;
	}

	// Token: 0x060056BC RID: 22204 RVA: 0x00374278 File Offset: 0x00373278
	public int ᜉ(int A_0, int A_1)
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
				for (;;)
				{
					bool flag = false;
					int num = int.MinValue;
					int num2 = 5;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
							num2 = 10;
							continue;
						case 1:
							if (!flag)
							{
								num2 = 7;
								continue;
							}
							return num;
						case 2:
						{
							sprᱧ sprᱧ;
							num = Math.Max(num, sprᱧ.\u171E());
							flag = true;
							num2 = 4;
							continue;
						}
						case 3:
							num2 = 1;
							continue;
						case 4:
							goto IL_10A;
						case 5:
							if (this.ᜁ == null)
							{
								num2 = 6;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num3 = A_0;
								if (true)
								{
								}
								num2 = 9;
								continue;
							}
							break;
						case 6:
							return -1;
						case 7:
							return -1;
						case 8:
							goto IL_72;
						case 9:
							goto IL_72;
						case 10:
						{
							sprᱧ sprᱧ;
							if (sprᱧ.ᜈ() > 0)
							{
								num2 = 2;
								continue;
							}
							goto IL_10A;
						}
						case 11:
						{
							sprᱧ sprᱧ;
							if (sprᱧ != null)
							{
								num2 = 0;
								continue;
							}
							goto IL_10A;
						}
						case 12:
						{
							if (num3 > A_1)
							{
								num2 = 3;
								continue;
							}
							sprᱧ sprᱧ = this.ᜁ.ᜁ(num3);
							num2 = 11;
							continue;
						}
						}
						break;
						IL_72:
						num2 = 12;
						continue;
						IL_10A:
						num3++;
						num2 = 8;
					}
				}
				break;
			}
		}
		return -1;
	}

	// Token: 0x060056BD RID: 22205 RVA: 0x003743F8 File Offset: 0x003733F8
	public bool ᜆ(int A_0)
	{
		if (true)
		{
		}
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_A7:
			num = 5;
			break;
		default:
			if (false)
			{
			}
			num = 2;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 <= this.ᜃ)
				{
					num = 1;
					continue;
				}
				return false;
			case 1:
				goto IL_94;
			case 3:
				num = 4;
				continue;
			case 4:
				goto IL_9E;
			case 5:
				num = 0;
				continue;
			}
			if (this.ᜁ == null)
			{
				return false;
			}
			num = 3;
		}
		IL_94:
		return this.ᜁ.ᜁ(A_0) != null;
		IL_9E:
		if (A_0 >= this.ᜂ)
		{
			goto IL_A7;
		}
		return false;
	}

	// Token: 0x060056BE RID: 22206 RVA: 0x003744B8 File Offset: 0x003734B8
	public List<long> ᜀ(Dictionary<int, object> A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_123:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_58;
		}
		List<long> list;
		int num2;
		for (;;)
		{
			IL_1E:
			switch (num)
			{
			case 0:
			{
				sprᱧ sprᱧ;
				if (sprᱧ != null)
				{
					num = 8;
					continue;
				}
				goto IL_11F;
			}
			case 1:
				goto IL_DE;
			case 2:
				if (A_0.Count != 0)
				{
					num = 12;
					continue;
				}
				return list;
			case 3:
				if (this.ᜁ == null)
				{
					num = 9;
					continue;
				}
				num2 = this.ᜂ;
				num = 7;
				continue;
			case 4:
				num = 2;
				continue;
			case 5:
				goto IL_89;
			case 6:
				if (A_0 != null)
				{
					num = 4;
					continue;
				}
				return list;
			case 7:
				goto IL_DE;
			case 8:
			{
				sprᱧ sprᱧ;
				sprᱧ.ᜀ(A_0, list);
				num = 5;
				continue;
			}
			case 9:
				goto IL_DC;
			case 10:
				return list;
			case 11:
			{
				if (num2 > this.ᜃ)
				{
					num = 10;
					continue;
				}
				sprᱧ sprᱧ = this.ᜁ.ᜁ(num2);
				num = 0;
				continue;
			}
			case 12:
				num = 3;
				continue;
			}
			goto IL_58;
			IL_DE:
			num = 11;
		}
		IL_89:
		if (true)
		{
		}
		goto IL_11F;
		IL_DC:
		return list;
		IL_11F:
		num2++;
		goto IL_123;
		IL_58:
		list = new List<long>();
		num = 6;
		goto IL_1E;
	}

	// Token: 0x060056BF RID: 22207 RVA: 0x00374618 File Offset: 0x00373618
	public sprủ ᜀ(Rectangle A_0, int A_1, int A_2, ref int A_3, ref int A_4)
	{
		switch (0)
		{
		default:
		{
			sprủ sprủ;
			for (;;)
			{
				sprủ = new sprủ(this.ᜀ, this.ᜇ);
				int num = Math.Max(A_0.Y, this.ᜂ);
				int num2 = Math.Min(A_0.Bottom, this.ᜃ);
				int x = A_0.X;
				int right = A_0.Right;
				int a_ = ((spr\u17FF)this.ᜌ()).ᜨ();
				int num3 = num;
				int num4 = 1;
				for (;;)
				{
					sprᱧ sprᱧ;
					switch (num4)
					{
					case 0:
						goto IL_CA;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1B5;
						default:
							if (false)
							{
							}
							goto IL_154;
						}
						break;
					case 2:
						goto IL_1B5;
					case 3:
						goto IL_17B;
					case 4:
						if (sprᱧ != null)
						{
							num4 = 6;
							continue;
						}
						goto IL_1B5;
					case 5:
					{
						sprᱧ sprᱧ2;
						sprᱧ = sprᱧ2.ᜃ(x, right, this.ᜌ().\u171D());
						num4 = 4;
						continue;
					}
					case 6:
					{
						sprᱧ.ᜂ(A_1, A_2, a_);
						sprᱧ sprᱧ2;
						sprᱧ2.ᜁ(x, right, a_);
						A_3 = Math.Max(A_3, num3 + A_1);
						A_4 = Math.Max(A_4, sprᱧ.\u171E());
						num4 = 2;
						continue;
					}
					case 7:
						goto IL_154;
					case 8:
					{
						if (num3 > num2)
						{
							num4 = 3;
							continue;
						}
						sprᱧ sprᱧ2 = this.ᜁ.ᜁ(num3);
						num4 = 9;
						continue;
					}
					case 9:
					{
						sprᱧ sprᱧ2;
						if (sprᱧ2 != null)
						{
							num4 = 5;
							continue;
						}
						goto IL_CA;
					}
					}
					break;
					IL_CA:
					num3++;
					num4 = 7;
					continue;
					IL_154:
					num4 = 8;
					continue;
					IL_1B5:
					if (true)
					{
					}
					sprủ.ᜀ(num3 + A_1, sprᱧ);
					num4 = 0;
				}
			}
			IL_17B:
			sprủ.ᜂ = A_0.Y + A_1;
			sprủ.ᜃ = A_0.Bottom + A_1;
			return sprủ;
		}
		}
	}

	// Token: 0x060056C0 RID: 22208 RVA: 0x00374820 File Offset: 0x00373820
	public void ᜀ(Dictionary<int, int> A_0)
	{
		int num = 1;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_117;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 2:
				return;
			case 3:
			{
				sprᱧ sprᱧ;
				if (sprᱧ != null)
				{
					num = 10;
					continue;
				}
				goto IL_106;
			}
			case 4:
				goto IL_BA;
			case 5:
				if (this.ᜂ < 0)
				{
					num = 8;
					continue;
				}
				goto IL_117;
			case 6:
				goto IL_106;
			case 7:
				goto IL_BA;
			case 8:
				goto IL_104;
			case 9:
			{
				if (num2 > this.ᜃ)
				{
					num = 2;
					continue;
				}
				sprᱧ sprᱧ = this.ᜁ.ᜁ(num2);
				num = 3;
				continue;
			}
			case 10:
			{
				sprᱧ sprᱧ;
				sprᱧ.ᜀ(A_0, this.ᜌ().\u171D());
				num = 6;
				continue;
			}
			}
			if (this.ᜁ != null)
			{
				if (true)
				{
				}
				num = 0;
				continue;
			}
			break;
			IL_BA:
			num = 9;
			continue;
			IL_106:
			num2++;
			num = 4;
			continue;
			IL_117:
			num2 = this.ᜂ;
			num = 7;
		}
		return;
		IL_104:;
	}

	// Token: 0x060056C1 RID: 22209 RVA: 0x00374958 File Offset: 0x00373958
	public void ᜀ(int[] A_0)
	{
		int num = 7;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				if (this.ᜂ < 0)
				{
					num = 5;
					continue;
				}
				goto IL_114;
			case 1:
				return;
			case 2:
			{
				sprᱧ sprᱧ;
				sprᱧ.ᜀ(A_0, this.ᜌ().\u171D());
				num = 10;
				continue;
			}
			case 3:
				goto IL_B7;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_114;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 5:
				goto IL_101;
			case 6:
			{
				if (num2 > this.ᜃ)
				{
					num = 1;
					continue;
				}
				sprᱧ sprᱧ = this.ᜁ.ᜁ(num2);
				num = 8;
				continue;
			}
			case 8:
			{
				sprᱧ sprᱧ;
				if (sprᱧ != null)
				{
					num = 2;
					continue;
				}
				goto IL_103;
			}
			case 9:
				goto IL_B7;
			case 10:
				goto IL_103;
			}
			if (this.ᜁ != null)
			{
				num = 4;
				continue;
			}
			break;
			IL_B7:
			num = 6;
			continue;
			IL_103:
			num2++;
			num = 3;
			continue;
			IL_114:
			num2 = this.ᜂ;
			num = 9;
		}
		IL_AE:
		if (true)
		{
		}
		return;
		IL_101:
		goto IL_AE;
	}

	// Token: 0x060056C2 RID: 22210 RVA: 0x00374A90 File Offset: 0x00373A90
	public void ᜀ(int A_0)
	{
		int num = 5;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				IL_102:
				goto IL_8D;
			case 1:
			{
				sprᱧ sprᱧ;
				int defaultXFIndex;
				sprᱧ.ᜈ(A_0, defaultXFIndex);
				num = 4;
				continue;
			}
			case 2:
				num = 8;
				continue;
			case 3:
				goto IL_AC;
			case 4:
				goto IL_D7;
			case 6:
				goto IL_D5;
			case 7:
			{
				if (num2 > this.ᜃ)
				{
					num = 3;
					continue;
				}
				sprᱧ sprᱧ = this.ᜁ.ᜁ(num2);
				num = 9;
				continue;
			}
			case 8:
			{
				if (this.ᜂ < 0)
				{
					num = 6;
					continue;
				}
				int defaultXFIndex = this.ᜆ.DefaultXFIndex;
				num2 = this.ᜂ;
				num = 10;
				continue;
			}
			case 9:
			{
				sprᱧ sprᱧ;
				if (sprᱧ != null)
				{
					num = 1;
					continue;
				}
				goto IL_D7;
			}
			case 10:
				goto IL_8D;
			}
			if (this.ᜁ != null)
			{
				num = 2;
				continue;
			}
			break;
			IL_8D:
			num = 7;
			continue;
			IL_D7:
			num2++;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_102;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
		}
		return;
		IL_AC:
		if (true)
		{
		}
		return;
		IL_D5:;
	}

	// Token: 0x060056C3 RID: 22211 RVA: 0x00374BC4 File Offset: 0x00373BC4
	internal void ᜀ(Dictionary<int, int> A_0, spr\u202C A_1)
	{
		int num = 3;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_49;
			case 2:
			{
				if (num2 > this.ᜃ)
				{
					num = 0;
					continue;
				}
				sprᱧ sprᱧ = this.ᜁ.ᜁ(num2);
				num = 8;
				continue;
			}
			case 4:
			{
				if (true)
				{
				}
				sprᱧ sprᱧ;
				sprᱧ.ᜀ(A_0, A_1);
				num = 1;
				continue;
			}
			case 5:
				return;
			case 6:
				goto IL_A1;
			case 7:
				goto IL_A1;
			case 8:
			{
				sprᱧ sprᱧ;
				if (sprᱧ != null)
				{
					num = 4;
					continue;
				}
				goto IL_49;
			}
			}
			if (this.ᜁ == null)
			{
				num = 5;
				continue;
			}
			num2 = this.ᜂ;
			num = 6;
			continue;
			IL_49:
			num2++;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (false)
				{
				}
				num = 7;
				continue;
			}
			IL_A1:
			num = 2;
		}
	}

	// Token: 0x060056C4 RID: 22212 RVA: 0x00374CCC File Offset: 0x00373CCC
	[CLSCompliant(false)]
	internal void ᜀ(sprἛ A_0, bool A_1, SSTDictionary A_2, XlsWorksheet A_3, IDecryptor A_4)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 51;
			Stream baseStream;
			long position;
			for (;;)
			{
				sprᱧ sprᱧ;
				int num2;
				short num3;
				byte[] array;
				TBIFFRecord tbiffrecord2;
				int num4;
				int num5;
				spr\u24E5 spr_u24E;
				switch (num)
				{
				case 0:
					num = 16;
					continue;
				case 1:
					if (sprᱧ.\u171E() < num2)
					{
						num = 41;
						continue;
					}
					goto IL_3F3;
				case 2:
					goto IL_6BF;
				case 3:
					num = 25;
					continue;
				case 4:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.RK)
					{
						num = 60;
						continue;
					}
					goto IL_7FE;
				}
				case 5:
				{
					TBIFFRecord tbiffrecord;
					switch (tbiffrecord)
					{
					case TBIFFRecord.RString:
						goto IL_7FE;
					case TBIFFRecord.DBCell:
						baseStream.Position += (long)num3;
						num = 2;
						continue;
					default:
						num = 30;
						continue;
					}
					break;
				}
				case 6:
				{
					int index = BitConverter.ToInt32(array, 10);
					A_2.AddIncrease(index);
					num = 62;
					continue;
				}
				case 7:
					if (A_1)
					{
						num = 8;
						continue;
					}
					num = 43;
					continue;
				case 8:
					goto IL_92B;
				case 9:
					if (tbiffrecord2 == TBIFFRecord.LabelSST)
					{
						num = 6;
						continue;
					}
					goto IL_7D7;
				case 10:
					goto IL_207;
				case 11:
					goto IL_62A;
				case 12:
					goto IL_6BF;
				case 13:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.LabelSST)
					{
						num = 22;
						continue;
					}
					goto IL_7FE;
				}
				case 14:
					if (num4 != num5)
					{
						num = 40;
						continue;
					}
					goto IL_207;
				case 15:
					goto IL_264;
				case 16:
					goto IL_455;
				case 17:
					goto IL_488;
				case 18:
					goto IL_5D3;
				case 19:
					goto IL_499;
				case 20:
					goto IL_906;
				case 21:
					A_4.Decrypt(spr_u24E, 4, (int)num3, position + 4L);
					num = 37;
					continue;
				case 22:
					num = 31;
					continue;
				case 23:
					goto IL_703;
				case 24:
					goto IL_4BC;
				case 25:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.Formula)
					{
						num = 58;
						continue;
					}
					goto IL_7FE;
				}
				case 26:
					goto IL_6BF;
				case 27:
					goto IL_6BF;
				case 28:
					sprᱧ.ᜀ((int)num3, array, this.ᜌ().\u171D());
					num = 53;
					continue;
				case 29:
					sprᱧ = this.ᜀ(num5, 0, true, ExcelVersion.Version97to2003);
					num4 = num5;
					num = 20;
					continue;
				case 30:
					num = 13;
					continue;
				case 31:
					goto IL_28B;
				case 32:
					goto IL_6BF;
				case 33:
					if (A_4 != null)
					{
						num = 59;
						continue;
					}
					goto IL_2B2;
				case 34:
					goto IL_6BF;
				case 35:
					goto IL_6BF;
				case 36:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord > TBIFFRecord.MulBlank)
					{
						num = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3F3;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				case 37:
					goto IL_3CE;
				case 38:
				{
					if (tbiffrecord2 == TBIFFRecord.Unknown)
					{
						num = 23;
						continue;
					}
					TBIFFRecord tbiffrecord = tbiffrecord2;
					num = 68;
					continue;
				}
				case 39:
					if (sprᱧ == null)
					{
						num = 24;
						continue;
					}
					baseStream.Read(array, 4, (int)num3);
					num3 += 4;
					num = 57;
					continue;
				case 40:
				{
					int a_2 = this.ᜆ.AppImplementation.ᜅ();
					sprᱧ = this.ᜀ(num5, a_2, true, ExcelVersion.Version97to2003);
					num4 = num5;
					num = 10;
					continue;
				}
				case 41:
					sprᱧ.ᜀ((int)num3, array, this.ᜌ().\u171D());
					num = 11;
					continue;
				case 42:
					goto IL_8BF;
				case 43:
					if (sprᱧ.\u171E() < num2)
					{
						num = 28;
						continue;
					}
					sprᱧ.ᜀ(num2, (int)num3, array, this.ᜌ().\u171D());
					num = 15;
					continue;
				case 44:
					A_4.Decrypt(spr_u24E, 4, (int)num3, position + 4L);
					num = 42;
					continue;
				case 45:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.SharedFormula2)
					{
						num = 0;
						continue;
					}
					baseStream.Position = position;
					spr\u1DE2 a_3 = (spr\u1DE2)A_0.ᜀ(A_4);
					this.ᜀ(num5, num2, a_3);
					num = 35;
					continue;
				}
				case 46:
					goto IL_156;
				case 47:
				{
					TBIFFRecord tbiffrecord;
					switch (tbiffrecord)
					{
					case TBIFFRecord.MulRK:
					case TBIFFRecord.MulBlank:
						baseStream.Read(array, 4, (int)num3);
						num = 49;
						continue;
					default:
						num = 63;
						continue;
					}
					break;
				}
				case 48:
					goto IL_2B2;
				case 49:
					if (A_4 != null)
					{
						num = 56;
						continue;
					}
					goto IL_5D3;
				case 50:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord <= TBIFFRecord.Array)
					{
						num = 61;
						continue;
					}
					num = 4;
					continue;
				}
				case 52:
					num = 17;
					continue;
				case 53:
					goto IL_264;
				case 54:
				{
					TBIFFRecord tbiffrecord;
					switch (tbiffrecord)
					{
					case TBIFFRecord.Blank:
					case TBIFFRecord.Number:
					case TBIFFRecord.Label:
					case TBIFFRecord.BoolErr:
						goto IL_7FE;
					case (TBIFFRecord)514:
					case (TBIFFRecord)518:
						goto IL_930;
					case TBIFFRecord.String:
						goto IL_49E;
					case TBIFFRecord.Row:
						baseStream.Read(array, 4, (int)num3);
						num = 33;
						continue;
					default:
						num = 55;
						continue;
					}
					break;
				}
				case 55:
					num = 67;
					continue;
				case 56:
					A_4.Decrypt(spr_u24E, 4, (int)num3, position + 4L);
					num = 18;
					continue;
				case 57:
					if (A_4 != null)
					{
						num = 21;
						continue;
					}
					goto IL_3CE;
				case 58:
					num = 47;
					continue;
				case 59:
					A_4.Decrypt(spr_u24E, 4, (int)num3, position + 4L);
					num = 48;
					continue;
				case 60:
					num = 45;
					continue;
				case 61:
					num = 54;
					continue;
				case 62:
					goto IL_7D7;
				case 63:
					num = 19;
					continue;
				case 64:
					goto IL_62A;
				case 65:
					if (A_4 != null)
					{
						num = 44;
						continue;
					}
					goto IL_8BF;
				case 66:
					num = 36;
					continue;
				case 67:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.Array)
					{
						num = 52;
						continue;
					}
					goto IL_49E;
				}
				case 68:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord <= TBIFFRecord.LabelSST)
					{
						num = 66;
						continue;
					}
					if (true)
					{
					}
					num = 50;
					continue;
				}
				case 69:
					if (num4 != num5)
					{
						num = 29;
						continue;
					}
					goto IL_906;
				}
				if (A_0 == null)
				{
					num = 46;
					continue;
				}
				BinaryReader binaryReader = A_0.ᜄ();
				baseStream = binaryReader.BaseStream;
				array = A_0.ᜇ();
				sprᱧ = null;
				num4 = -1;
				num5 = 0;
				num2 = 0;
				spr_u24E = new spr\u24E5(array);
				num = 32;
				continue;
				IL_207:
				num = 9;
				continue;
				IL_264:
				int num6;
				sprᱧ.ᜇ(num2, num6);
				num = 12;
				continue;
				IL_2B2:
				spr\u20BA spr_u20BA = (spr\u20BA)spr\u175E.ᜀ(TBIFFRecord.Row);
				spr_u20BA.ParseStructure(spr_u24E, 4, 0, ExcelVersion.Version97to2003);
				A_3.ᜀ(spr_u20BA, A_1);
				num = 26;
				continue;
				IL_3CE:
				sprᱧ.ᜀ((int)num3, array, this.ᜌ().\u171D());
				num = 27;
				continue;
				IL_3F3:
				sprᱧ.ᜀ(num2, (int)num3, array, this.ᜌ().\u171D());
				num = 64;
				continue;
				IL_49E:
				num = 39;
				continue;
				IL_5D3:
				num5 = (int)BitConverter.ToUInt16(array, 4);
				num2 = (int)BitConverter.ToUInt16(array, 6);
				num3 += 4;
				num6 = (int)BitConverter.ToUInt16(array, (int)(num3 - 2));
				sprᜑ.ᜁ(this.ᜇ, num6 + 1);
				num = 69;
				continue;
				IL_62A:
				sprᱧ.ᜇ(num2, num2);
				num = 34;
				continue;
				IL_6BF:
				position = baseStream.Position;
				baseStream.Read(array, 0, 4);
				short num7 = BitConverter.ToInt16(array, 0);
				tbiffrecord2 = (TBIFFRecord)num7;
				num3 = BitConverter.ToInt16(array, 2);
				num = 38;
				continue;
				IL_7D7:
				num = 1;
				continue;
				IL_7FE:
				baseStream.Read(array, 4, (int)num3);
				num3 += 4;
				num = 65;
				continue;
				IL_8BF:
				num5 = (int)BitConverter.ToUInt16(array, 4);
				num2 = (int)BitConverter.ToUInt16(array, 6);
				sprᜑ.ᜁ(this.ᜇ, num2 + 1);
				num = 14;
				continue;
				IL_906:
				sprᱧ.ᜈ(true);
				num = 7;
			}
			IL_156:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹崻娽┿ぁ", a_));
			IL_28B:
			IL_455:
			IL_488:
			IL_499:
			goto IL_930;
			IL_4BC:
			throw new ArgumentNullException(RecordTableEnumerator.b("嬷伹主䰽┿ⱁぃᑅ❇㵉", a_));
			IL_703:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䨷弹弻儽㈿♁၃㽅㡇⽉", a_));
			IL_92B:
			throw new NotImplementedException();
			IL_930:
			baseStream.Position = position;
			return;
		}
		}
	}

	// Token: 0x060056C5 RID: 22213 RVA: 0x00375614 File Offset: 0x00374614
	internal void ᜀ(int A_0, int A_1, spr\u1DE2 A_2)
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
		long key = sprṔ.ᜀ(A_1, A_0);
		this.ᜅ().Add(key, A_2);
	}

	// Token: 0x060056C6 RID: 22214 RVA: 0x00375664 File Offset: 0x00374664
	[CLSCompliant(false)]
	internal bool ᜀ(spr\u218B A_0, sprἛ A_1, bool A_2, SSTDictionary A_3, XlsWorksheet A_4)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 14;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7A;
				case 1:
				{
					bool flag;
					if (!flag)
					{
						num = 5;
						continue;
					}
					return flag;
				}
				case 2:
				{
					bool flag;
					return flag;
				}
				case 3:
				{
					bool flag = false;
					num = 4;
					continue;
				}
				case 4:
					goto IL_CD;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CD;
					default:
					{
						if (false)
						{
						}
						Stream stream;
						long position;
						stream.Position = position;
						this.ᜊ();
						num = 2;
						continue;
					}
					}
					break;
				case 6:
				{
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					Stream stream = A_1.ᜈ();
					long position = stream.Position;
					A_1.ᜄ();
					DataProvider arrData = A_1.ᜋ();
					byte[] array = A_1.ᜇ();
					spr\u2466 spr_u = (spr\u2466)spr\u175E.ᜀ(TBIFFRecord.DBCell);
					bool flag = true;
					int[] array2 = A_0.ᜁ();
					int num2 = 0;
					int num3 = array2.Length;
					num = 0;
					continue;
				}
				case 7:
					goto IL_C0;
				case 8:
					num = 12;
					continue;
				case 9:
				{
					int num2;
					int num3;
					if (num2 < num3)
					{
						num = 8;
						continue;
					}
					goto IL_CD;
				}
				case 10:
					goto IL_7A;
				case 11:
					goto IL_CD;
				case 12:
				{
					bool flag;
					if (!flag)
					{
						num = 11;
						continue;
					}
					int[] array2;
					int num2;
					int num4 = array2[num2];
					Stream stream;
					stream.Position = (long)num4;
					byte[] array;
					stream.Read(array, 0, 4);
					int num5 = (int)BitConverter.ToInt16(array, 0);
					int num6 = (int)BitConverter.ToInt16(array, 2);
					num = 13;
					continue;
				}
				case 13:
				{
					int num5;
					if (num5 != 215)
					{
						num = 3;
						continue;
					}
					Stream stream;
					byte[] array;
					int num6;
					stream.Read(array, 4, num6);
					spr\u2466 spr_u;
					spr_u.Length = num6;
					DataProvider arrData;
					spr_u.ParseStructure(arrData, 4, num6, ExcelVersion.Version97to2003);
					int num4;
					spr_u.StreamPos = (long)num4;
					bool flag = this.ᜀ(spr_u, A_1, A_2, A_3, A_4);
					int num2;
					num2++;
					num = 10;
					continue;
				}
				case 15:
					return false;
				}
				if (A_0 == null)
				{
					num = 15;
					continue;
				}
				num = 6;
				continue;
				IL_7A:
				num = 9;
				continue;
				IL_CD:
				num = 1;
			}
			return false;
			IL_C0:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㭈⹊ⱌ⭎㑐⅒", a_));
		}
		}
	}

	// Token: 0x060056C7 RID: 22215 RVA: 0x003758D8 File Offset: 0x003748D8
	public sprᱧ ᜀ(int A_0, int A_1, bool A_2, ExcelVersion A_3)
	{
		sprᱧ sprᱧ;
		for (;;)
		{
			sprᱧ = this.ᜄ().ᜁ(A_0);
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_2)
					{
						num = 2;
						continue;
					}
					goto IL_8C;
				case 1:
					goto IL_8C;
				case 2:
					this.ᜁ.ᜀ(A_0, sprᱧ = this.ᜀ(A_0, A_1, A_3));
					this.ᜃ(A_0);
					sprᜑ.ᜀ(this.ᜇ, A_0 + 1);
					num = 1;
					continue;
				case 3:
					if (A_2)
					{
						num = 9;
						continue;
					}
					return sprᱧ;
				case 4:
					return sprᱧ;
				case 5:
					if (sprᱧ == null)
					{
						return sprᱧ;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_74;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				case 6:
					if (sprᱧ == null)
					{
						if (true)
						{
						}
						num = 10;
						continue;
					}
					goto IL_8C;
				case 7:
					num = 3;
					continue;
				case 8:
					goto IL_74;
				case 9:
					num = 8;
					continue;
				case 10:
					num = 0;
					continue;
				case 11:
					sprᱧ.ᜁ(this.ᜆ.HeapHandle);
					num = 4;
					continue;
				}
				break;
				IL_74:
				if (sprᱧ.\u171D() == null)
				{
					num = 11;
					continue;
				}
				return sprᱧ;
				IL_8C:
				num = 5;
			}
		}
		return sprᱧ;
	}

	// Token: 0x060056C8 RID: 22216 RVA: 0x00375A40 File Offset: 0x00374A40
	public void ᜁ(int A_0)
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
		this.ᜁ.ᜂ(A_0);
	}

	// Token: 0x060056C9 RID: 22217 RVA: 0x00375A88 File Offset: 0x00374A88
	public void ᜌ(int A_0, int A_1)
	{
		int num = 2;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			case 1:
				goto IL_2A;
			default:
				goto IL_2A;
			}
			IL_4B:
			num = 1;
			continue;
			IL_2A:
			if (false)
			{
			}
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (true)
				{
				}
				this.ᜁ(this.ᜃ + A_1 + 1);
				this.ᜁ.ᜀ(A_0, A_1, this.ᜃ - A_0 + 1);
				this.ᜃ += A_1;
				num = 0;
				continue;
			}
			if (A_0 <= this.ᜃ)
			{
				goto IL_4B;
			}
			break;
		}
	}

	// Token: 0x060056CA RID: 22218 RVA: 0x00375B34 File Offset: 0x00374B34
	private bool ᜀ(spr\u2466 A_0, sprἛ A_1, bool A_2, SSTDictionary A_3, XlsWorksheet A_4)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			bool flag;
			for (;;)
			{
				IL_17:
				int num = 20;
				for (;;)
				{
					spr\u20BA a_2;
					int num3;
					long num4;
					Stream stream;
					int num6;
					byte[] array2;
					long num8;
					DataProvider a_3;
					switch (num)
					{
					case 0:
					{
						if (A_1 == null)
						{
							num = 11;
							continue;
						}
						a_2 = (spr\u20BA)spr\u175E.ᜀ(TBIFFRecord.Row);
						ushort[] array = A_0.ᜂ();
						int num2 = array.Length;
						num = 14;
						continue;
					}
					case 1:
					{
						if (num3 < 0)
						{
							num = 7;
							continue;
						}
						if (true)
						{
						}
						num4 += (long)(num3 + 4);
						A_4.ᜀ(a_2, A_2);
						long num5;
						stream.Position = num5;
						ushort[] array;
						int num7;
						num6 = (int)array[num7];
						num5 += (long)num6;
						flag = this.ᜀ(a_2, stream, num6, array2, A_3);
						num7++;
						num = 13;
						continue;
					}
					case 2:
						goto IL_94;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17;
						default:
						{
							if (false)
							{
							}
							int num2;
							if (num2 <= 1)
							{
								num = 4;
								continue;
							}
							num = 5;
							continue;
						}
						}
						break;
					case 4:
						num = 16;
						continue;
					case 5:
						num8 = stream.Position;
						goto IL_18A;
					case 6:
						goto IL_230;
					case 7:
						return false;
					case 8:
					{
						A_4.ᜀ(a_2, A_2);
						long num5;
						stream.Position = num5;
						flag &= this.ᜀ(a_2, stream, num6, array2, A_3);
						num = 6;
						continue;
					}
					case 9:
						if (flag)
						{
							num = 12;
							continue;
						}
						return flag;
					case 10:
						goto IL_99;
					case 11:
						goto IL_14B;
					case 12:
						num = 3;
						continue;
					case 13:
						goto IL_BA;
					case 14:
					{
						int num2;
						if (num2 == 0)
						{
							num = 22;
							continue;
						}
						num4 = A_0.StreamPos - (long)A_0.ᜁ();
						array2 = A_1.ᜇ();
						a_3 = A_1.ᜋ();
						stream = A_1.ᜈ();
						ushort[] array;
						long num5 = num4 + (long)((ulong)array[0]) + 16L + 4L;
						flag = true;
						int num7 = 1;
						int num9 = num2;
						num = 21;
						continue;
					}
					case 15:
						if (!flag)
						{
							num = 10;
							continue;
						}
						stream.Position = num4;
						num3 = this.ᜀ(a_2, stream, array2, a_3);
						num = 1;
						continue;
					case 16:
						num8 = num4 + 16L + 4L;
						goto IL_18A;
					case 17:
					{
						int num7;
						int num9;
						if (num7 < num9)
						{
							num = 19;
							continue;
						}
						goto IL_99;
					}
					case 18:
						if (flag)
						{
							num = 8;
							continue;
						}
						return flag;
					case 19:
						num = 15;
						continue;
					case 21:
						goto IL_BA;
					case 22:
						return true;
					}
					if (A_0 == null)
					{
						num = 2;
						continue;
					}
					num = 0;
					continue;
					IL_99:
					num = 9;
					continue;
					IL_BA:
					num = 17;
					continue;
					IL_18A:
					long num10 = num8;
					num6 = (int)(A_0.StreamPos - num10);
					stream.Position = num4;
					num3 = this.ᜀ(a_2, stream, array2, a_3);
					flag &= (num3 > 0);
					num = 18;
				}
			}
			IL_94:
			throw new ArgumentNullException(RecordTableEnumerator.b("╀⅂ل≆╈❊", a_));
			IL_14B:
			throw new ArgumentNullException(RecordTableEnumerator.b("㍀♂⑄⍆ⱈ㥊", a_));
			IL_230:
			return flag;
		}
		}
	}

	// Token: 0x060056CB RID: 22219 RVA: 0x00375EA8 File Offset: 0x00374EA8
	private int ᜀ(spr\u20BA A_0, Stream A_1, byte[] A_2, DataProvider A_3)
	{
		int num2;
		for (;;)
		{
			IL_1C:
			A_1.Read(A_2, 0, 20);
			int num = (int)BitConverter.ToInt16(A_2, 0);
			num2 = (int)BitConverter.ToInt16(A_2, 2);
			for (;;)
			{
				IL_37:
				int num3 = 4;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						A_1.Read(A_2, 20, num2 - 16);
						num3 = 2;
						continue;
					case 1:
						if (16 < num2)
						{
							num3 = 0;
							continue;
						}
						goto IL_B9;
					case 2:
						goto IL_8E;
					case 3:
						goto IL_6B;
					case 4:
						if (num == 520)
						{
							num3 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_37;
						default:
							if (false)
							{
							}
							num3 = 3;
							continue;
						}
						break;
					}
					goto IL_1C;
				}
			}
		}
		IL_6B:
		if (true)
		{
		}
		return -1;
		IL_8E:
		IL_B9:
		A_0.ParseStructure(A_3, 4, num2, ExcelVersion.Version97to2003);
		return num2;
	}

	// Token: 0x060056CC RID: 22220 RVA: 0x00375F7C File Offset: 0x00374F7C
	private bool ᜀ(spr\u20BA A_0, Stream A_1, int A_2, byte[] A_3, SSTDictionary A_4)
	{
		switch (0)
		{
		default:
		{
			bool result;
			for (;;)
			{
				if (true)
				{
				}
				result = true;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_58;
						default:
						{
							if (false)
							{
							}
							sprᱧ sprᱧ = this.ᜀ((int)A_0.ᜇ(), 0, true, ExcelVersion.Version97to2003);
							sprᱧ.ᜀ(A_0, this.ᜌ().\u171C());
							int val = A_3.Length;
							num = 5;
							continue;
						}
						}
						break;
					case 1:
					{
						sprᱧ sprᱧ;
						result = sprᱧ.ᜀ(A_4, ref this.ᜅ);
						num = 4;
						continue;
					}
					case 2:
					{
						if (A_2 <= 0)
						{
							num = 1;
							continue;
						}
						int val;
						int num2 = Math.Min(A_2, val);
						A_1.Read(A_3, 0, num2);
						sprᱧ sprᱧ;
						sprᱧ.ᜀ(num2, A_3, this.ᜌ().\u171D());
						A_2 -= num2;
						num = 6;
						continue;
					}
					case 3:
						if (A_2 > 0)
						{
							num = 0;
							continue;
						}
						return result;
					case 4:
						return result;
					case 5:
						goto IL_58;
					case 6:
						goto IL_58;
					}
					break;
					IL_58:
					num = 2;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x060056CD RID: 22221 RVA: 0x003760B4 File Offset: 0x003750B4
	internal void ᜃ()
	{
		for (;;)
		{
			int num = this.ᜂ;
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_9E;
				case 1:
				{
					if (num > this.ᜃ)
					{
						num2 = 3;
						continue;
					}
					sprᱧ sprᱧ = this.ᜁ.ᜁ(num);
					num2 = 5;
					continue;
				}
				case 2:
					goto IL_51;
				case 3:
					goto IL_BD;
				case 4:
					goto IL_9E;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_51;
					default:
					{
						if (false)
						{
						}
						sprᱧ sprᱧ;
						if (sprᱧ != null)
						{
							num2 = 6;
							continue;
						}
						goto IL_35;
					}
					}
					break;
				case 6:
				{
					sprᱧ sprᱧ;
					sprᱧ.ᜎ();
					num2 = 2;
					continue;
				}
				}
				break;
				IL_35:
				num++;
				num2 = 0;
				continue;
				IL_51:
				goto IL_35;
				IL_9E:
				num2 = 1;
			}
		}
		IL_BD:
		if (true)
		{
		}
	}

	// Token: 0x060056CE RID: 22222 RVA: 0x00376188 File Offset: 0x00375188
	public int ᜇ(int A_0, int A_1)
	{
		sprᱧ sprᱧ = this.ᜁ.ᜁ(A_0 - 1);
		if (sprᱧ == null)
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
				return 0;
			}
		}
		return sprᱧ.\u1719(A_1 - 1);
	}

	// Token: 0x060056CF RID: 22223 RVA: 0x003761E4 File Offset: 0x003751E4
	public int ᜃ(int A_0, int A_1)
	{
		sprᱧ sprᱧ = this.ᜁ.ᜁ(A_0 - 1);
		if (sprᱧ == null)
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
				return 0;
			}
		}
		return sprᱧ.ᜏ(A_1 - 1);
	}

	// Token: 0x060056D0 RID: 22224 RVA: 0x00376240 File Offset: 0x00375240
	public string ᜂ(int A_0, int A_1)
	{
		sprᱧ sprᱧ = this.ᜄ().ᜁ(A_0 - 1);
		if (sprᱧ == null)
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
				return null;
			}
		}
		if (true)
		{
		}
		return sprᱧ.\u170D(A_1 - 1);
	}

	// Token: 0x060056D1 RID: 22225 RVA: 0x0037629C File Offset: 0x0037529C
	internal string ᜀ(byte A_0, int A_1)
	{
		if (true)
		{
		}
		sprᱧ sprᱧ = this.ᜄ().ᜁ(A_1 - 1);
		if (sprᱧ == null)
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
				return null;
			}
		}
		return sprᱧ.\u171B((int)(A_0 & byte.MaxValue));
	}

	// Token: 0x060056D2 RID: 22226 RVA: 0x003762FC File Offset: 0x003752FC
	public string ᜁ(int A_0, int A_1)
	{
		sprᱧ sprᱧ = this.ᜄ().ᜁ(A_0 - 1);
		if (sprᱧ == null)
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
				if (true)
				{
				}
				return null;
			}
		}
		return sprᱧ.ᜋ(A_1 - 1);
	}

	// Token: 0x060056D3 RID: 22227 RVA: 0x00376358 File Offset: 0x00375358
	public double \u170D(int A_0, int A_1)
	{
		sprᱧ sprᱧ = this.ᜁ.ᜁ(A_0 - 1);
		if (sprᱧ != null)
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
				sprᱧ.ᜀ(this.ᜆ, A_0);
				return sprᱧ.\u171D(A_1 - 1);
			}
		}
		return double.NaN;
	}

	// Token: 0x060056D4 RID: 22228 RVA: 0x003763C8 File Offset: 0x003753C8
	public double ᜆ(int A_0, int A_1)
	{
		if (true)
		{
		}
		sprᱧ sprᱧ = this.ᜁ.ᜁ(A_0 - 1);
		if (sprᱧ == null)
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
				return double.NaN;
			}
		}
		return sprᱧ.\u1714(A_1 - 1);
	}

	// Token: 0x060056D5 RID: 22229 RVA: 0x0037642C File Offset: 0x0037542C
	public bool ᜈ(int A_0, int A_1)
	{
		sprᱧ sprᱧ = this.ᜄ().ᜁ(A_0 - 1);
		if (sprᱧ == null)
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
				return false;
			}
		}
		return sprᱧ.\u1716(A_1 - 1);
	}

	// Token: 0x060056D6 RID: 22230 RVA: 0x00376488 File Offset: 0x00375488
	public string ᜁ(int A_0, int A_1, SSTDictionary A_2)
	{
		sprᱧ sprᱧ = this.ᜁ.ᜁ(A_0 - 1);
		if (sprᱧ == null)
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
				return null;
			}
		}
		return sprᱧ.ᜀ(A_1 - 1, A_2);
	}

	// Token: 0x060056D7 RID: 22231 RVA: 0x003764E4 File Offset: 0x003754E4
	public string ᜀ(int A_0, int A_1, SSTDictionary A_2)
	{
		if (true)
		{
		}
		sprᱧ sprᱧ = this.ᜁ.ᜁ(A_0 - 1);
		if (sprᱧ == null)
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
				return null;
			}
		}
		return sprᱧ.ᜌ(A_1 - 1);
	}

	// Token: 0x060056D8 RID: 22232 RVA: 0x00376540 File Offset: 0x00375540
	public Ptg[] ᜅ(int A_0, int A_1)
	{
		sprᱧ sprᱧ = this.ᜁ.ᜁ(A_0 - 1);
		if (sprᱧ == null)
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
				return null;
			}
		}
		return sprᱧ.\u1712(A_1 - 1);
	}

	// Token: 0x060056D9 RID: 22233 RVA: 0x0037659C File Offset: 0x0037559C
	public bool ᜄ(int A_0, int A_1)
	{
		sprᱧ sprᱧ = this.ᜄ().ᜁ(A_0);
		if (sprᱧ == null)
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
				return false;
			}
		}
		if (true)
		{
		}
		return sprᱧ.ᜊ(A_1);
	}

	// Token: 0x060056DA RID: 22234 RVA: 0x003765F4 File Offset: 0x003755F4
	public XlsWorksheet.TRangeValueType ᜀ(int A_0, int A_1, bool A_2)
	{
		if (true)
		{
		}
		sprᱧ sprᱧ = this.ᜄ().ᜁ(A_0 - 1);
		if (sprᱧ == null)
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
				return XlsWorksheet.TRangeValueType.Blank;
			}
		}
		return sprᱧ.ᜁ(A_1 - 1, A_2);
	}

	// Token: 0x060056DB RID: 22235 RVA: 0x00376650 File Offset: 0x00375650
	[CLSCompliant(false)]
	internal void ᜀ(int A_0, int A_1, double A_2, spr\u21DF A_3)
	{
		int a_ = 12;
		sprᱧ sprᱧ = this.ᜁ.ᜁ(A_0 - 1);
		if (sprᱧ != null)
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
				sprᱧ.ᜀ(A_1 - 1, A_2, A_3, this.ᜌ().\u171D());
				return;
			}
		}
		throw new ApplicationException(RecordTableEnumerator.b("Ł╃⡅♇╉㡋湍⍏㝑⁓╕硗㱙㍛ⱝൟᝡࡣݥ䡧ᱩ൫ɭկ᝱婳", a_));
	}

	// Token: 0x060056DC RID: 22236 RVA: 0x003766D4 File Offset: 0x003756D4
	internal void ᜀ(bool[] A_0)
	{
		for (;;)
		{
			int num = this.ᜂ;
			int num2 = 6;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					if (true)
					{
					}
					sprᱧ sprᱧ;
					sprᱧ.ᜀ(A_0);
					num2 = 2;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
					{
						if (false)
						{
						}
						sprᱧ sprᱧ;
						if (sprᱧ != null)
						{
							num2 = 0;
							continue;
						}
						goto IL_35;
					}
					}
					break;
				case 2:
					goto IL_52;
				case 3:
					return;
				case 4:
				{
					if (num > this.ᜃ)
					{
						num2 = 3;
						continue;
					}
					sprᱧ sprᱧ = this.ᜁ.ᜁ(num);
					num2 = 1;
					continue;
				}
				case 5:
					goto IL_A7;
				case 6:
					goto IL_A7;
				}
				break;
				IL_35:
				num++;
				num2 = 5;
				continue;
				IL_52:
				goto IL_35;
				IL_A7:
				num2 = 4;
			}
		}
	}

	// Token: 0x060056DD RID: 22237 RVA: 0x003767AC File Offset: 0x003757AC
	internal void ᜁ(int[] A_0)
	{
		for (;;)
		{
			int num = this.ᜂ;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_A7;
				case 1:
					goto IL_A7;
				case 2:
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5A;
					}
					if (false)
					{
					}
					sprᱧ sprᱧ;
					if (sprᱧ != null)
					{
						num2 = 6;
						continue;
					}
					goto IL_35;
				}
				case 3:
					return;
				case 4:
				{
					if (num > this.ᜃ)
					{
						num2 = 3;
						continue;
					}
					sprᱧ sprᱧ = this.ᜁ.ᜁ(num);
					num2 = 2;
					continue;
				}
				case 5:
					goto IL_5A;
				case 6:
				{
					sprᱧ sprᱧ;
					sprᱧ.ᜀ(A_0);
					num2 = 5;
					continue;
				}
				}
				break;
				IL_35:
				num++;
				if (true)
				{
				}
				num2 = 0;
				continue;
				IL_5A:
				goto IL_35;
				IL_A7:
				num2 = 4;
			}
		}
	}

	// Token: 0x04002941 RID: 10561
	private int ᜀ;

	// Token: 0x04002942 RID: 10562
	private spr\u223C ᜁ = new spr\u223C();

	// Token: 0x04002943 RID: 10563
	private int ᜂ = -1;

	// Token: 0x04002944 RID: 10564
	private int ᜃ = -1;

	// Token: 0x04002945 RID: 10565
	private bool ᜄ;

	// Token: 0x04002946 RID: 10566
	private Dictionary<long, spr\u1DE2> ᜅ;

	// Token: 0x04002947 RID: 10567
	private XlsWorkbook ᜆ;

	// Token: 0x04002948 RID: 10568
	private IInternalWorksheet ᜇ;
}
