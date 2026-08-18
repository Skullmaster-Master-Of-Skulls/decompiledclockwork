using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

// Token: 0x02000313 RID: 787
internal class sprᱧ : IDisposable, IRecordStorage, spr\u2502
{
	// Token: 0x0600304B RID: 12363 RVA: 0x001B7944 File Offset: 0x001B6944
	public sprᱧ(int A_0, int A_1, int A_2)
	{
		int a_ = 19;
		this.ᜅ = -1;
		this.ᜆ = -1;
		this.ᜊ = -1;
		this.ᜋ = -1;
		this.ᜏ = new string[]
		{
			RecordTableEnumerator.b("ⵈ", a_),
			RecordTableEnumerator.b("ⵈ⽊", a_),
			RecordTableEnumerator.b("⑈", a_),
			RecordTableEnumerator.b("⑈♊", a_),
			RecordTableEnumerator.b("え㉊", a_),
			RecordTableEnumerator.b("え㉊㑌㙎", a_)
		};
		this.\u1712 = spr\u20BA.OptionFlags.ShowOutlineGroups;
		base..ctor();
		this.ᜑ = (ushort)A_1;
		this.ᜀ((ushort)A_2);
	}

	// Token: 0x0600304C RID: 12364 RVA: 0x001B7A0C File Offset: 0x001B6A0C
	public void ᜋ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_61;
			case 2:
				if (this.ᜈ != null)
				{
					num = 4;
					continue;
				}
				goto IL_61;
			case 3:
				num = 2;
				continue;
			case 4:
				this.ᜈ.Dispose();
				this.ᜈ = null;
				this.ᜅ = -1;
				this.ᜆ = -1;
				this.ᜇ = -1;
				num = 0;
				continue;
			case 5:
				return;
			}
			if (this.ᜄ())
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 3;
				continue;
			}
			IL_61:
			this.ᜀ(true);
			GC.SuppressFinalize(this);
			num = 5;
		}
	}

	// Token: 0x0600304D RID: 12365 RVA: 0x001B7AF0 File Offset: 0x001B6AF0
	protected virtual void ᜡ()
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
			this.ᜋ();
		}
		finally
		{
			base.Finalize();
		}
	}

	// Token: 0x0600304E RID: 12366 RVA: 0x001B7B4C File Offset: 0x001B6B4C
	public int \u171C()
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
		return this.ᜅ;
	}

	// Token: 0x0600304F RID: 12367 RVA: 0x001B7B90 File Offset: 0x001B6B90
	public void \u1715(int A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x06003050 RID: 12368 RVA: 0x001B7BD4 File Offset: 0x001B6BD4
	public int \u171E()
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
		return this.ᜆ;
	}

	// Token: 0x06003051 RID: 12369 RVA: 0x001B7C18 File Offset: 0x001B6C18
	public void ᜎ(int A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x06003052 RID: 12370 RVA: 0x001B7C5C File Offset: 0x001B6C5C
	public int ᜈ()
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
		return this.ᜇ;
	}

	// Token: 0x06003053 RID: 12371 RVA: 0x001B7CA0 File Offset: 0x001B6CA0
	public int ᜠ()
	{
		while (this.ᜈ == null)
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
		return this.ᜈ.Capacity;
	}

	// Token: 0x06003054 RID: 12372 RVA: 0x001B7CF4 File Offset: 0x001B6CF4
	public bool ᜤ()
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
		return (this.ᜉ & sprᱧ.StorageOptions.HasRKBlank) != sprᱧ.StorageOptions.None;
	}

	// Token: 0x06003055 RID: 12373 RVA: 0x001B7D40 File Offset: 0x001B6D40
	public void ᜆ(bool A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			if (!A_0)
			{
				goto IL_42;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_23;
			}
		}
		IL_23:
		if (false)
		{
		}
		this.ᜉ |= sprᱧ.StorageOptions.HasRKBlank;
		return;
		IL_42:
		this.ᜉ &= ~sprᱧ.StorageOptions.HasRKBlank;
	}

	// Token: 0x06003056 RID: 12374 RVA: 0x001B7DA0 File Offset: 0x001B6DA0
	public bool ᜏ()
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
		return (this.ᜉ & sprᱧ.StorageOptions.HasMultiRKBlank) != sprᱧ.StorageOptions.None;
	}

	// Token: 0x06003057 RID: 12375 RVA: 0x001B7DEC File Offset: 0x001B6DEC
	public void ᜈ(bool A_0)
	{
		while (A_0)
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
				this.ᜉ |= sprᱧ.StorageOptions.HasMultiRKBlank;
				return;
			}
		}
		this.ᜉ &= ~sprᱧ.StorageOptions.HasMultiRKBlank;
	}

	// Token: 0x06003058 RID: 12376 RVA: 0x001B7E4C File Offset: 0x001B6E4C
	private bool ᜄ()
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
		return (this.ᜉ & sprᱧ.StorageOptions.Disposed) != sprᱧ.StorageOptions.None;
	}

	// Token: 0x06003059 RID: 12377 RVA: 0x001B7E98 File Offset: 0x001B6E98
	private void ᜀ(bool A_0)
	{
		while (A_0)
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
				this.ᜉ |= sprᱧ.StorageOptions.Disposed;
				return;
			}
		}
		this.ᜉ &= ~sprᱧ.StorageOptions.Disposed;
	}

	// Token: 0x0600305A RID: 12378 RVA: 0x001B7EF8 File Offset: 0x001B6EF8
	public DataProvider \u171D()
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
		return this.ᜈ;
	}

	// Token: 0x0600305B RID: 12379 RVA: 0x001B7F3C File Offset: 0x001B6F3C
	public int \u1712()
	{
		while (this.ᜌ != ExcelVersion.Version97to2003)
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
				return 8;
			}
		}
		return 4;
	}

	// Token: 0x0600305C RID: 12380 RVA: 0x001B7F84 File Offset: 0x001B6F84
	public ExcelVersion ᜆ()
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
		return this.ᜌ;
	}

	// Token: 0x0600305D RID: 12381 RVA: 0x001B7FC8 File Offset: 0x001B6FC8
	internal bool ᜌ()
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
		return this.ᜐ;
	}

	// Token: 0x0600305E RID: 12382 RVA: 0x001B800C File Offset: 0x001B700C
	internal void ᜃ(bool A_0)
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
		this.ᜐ = A_0;
	}

	// Token: 0x0600305F RID: 12383 RVA: 0x001B8050 File Offset: 0x001B7050
	public IEnumerator ᜀ(RecordExtractor A_0)
	{
		int a_ = 16;
		while (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⥉⍋㱍㑏ᝑⱓ≕⩗㭙㽛⩝ཟၡ", a_));
			}
		}
		return new RowStorageEnumerator(this, A_0);
	}

	// Token: 0x06003060 RID: 12384 RVA: 0x001B80B4 File Offset: 0x001B70B4
	public void ᜁ(int A_0, int A_1, int A_2, int A_3)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				bool flag;
				num = this.ᜀ(A_1, out flag);
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.MulBlank)
						{
							num2 = 4;
							continue;
						}
						goto IL_E5;
					}
					case 1:
						goto IL_4E;
					case 2:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.MulRK)
						{
							num2 = 5;
							continue;
						}
						num2 = 0;
						continue;
					}
					case 3:
					{
						if (!flag)
						{
							num2 = 1;
							continue;
						}
						TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num);
						num2 = 2;
						continue;
					}
					case 4:
						goto IL_74;
					case 5:
						goto IL_DB;
					}
					break;
				}
			}
			IL_4E:
			spr\u23A5 spr_u23A = UtilityMethods.ᜀ(A_0, A_1, TBIFFRecord.Blank);
			spr_u23A.ᜀ((ushort)A_2);
			this.ᜁ(A_1, spr_u23A, A_3);
			return;
			IL_74:
			this.ᜀ(num, (ushort)A_2, A_1, 2);
			return;
			IL_76:
			this.ᜀ(num, (ushort)A_2, A_1, 6);
			return;
			IL_DB:
			if (true)
			{
			}
			goto IL_76;
			IL_E5:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_76;
			default:
				if (false)
				{
				}
				this.ᜀ(num, (ushort)A_2);
				return;
			}
			break;
		}
		}
	}

	// Token: 0x06003061 RID: 12385 RVA: 0x001B81CC File Offset: 0x001B71CC
	[CLSCompliant(false)]
	public BiffRecordRaw ᜈ(int A_0)
	{
		int a_ = 18;
		for (;;)
		{
			IL_09:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					break;
				case 1:
					goto IL_92;
				case 2:
					num = 3;
					continue;
				case 3:
					if (A_0 >= this.ᜇ)
					{
						num = 1;
						continue;
					}
					goto IL_94;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_09;
				default:
					if (false)
					{
					}
					if (A_0 < 0)
					{
						goto IL_65;
					}
					num = 2;
					break;
				}
			}
		}
		IL_65:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⅇՉ⩋⡍⍏㝑⁓", a_));
		IL_92:
		goto IL_65;
		IL_94:
		return spr\u175E.ᜀ(this.ᜈ, A_0, this.ᜆ());
	}

	// Token: 0x06003062 RID: 12386 RVA: 0x001B8280 File Offset: 0x001B7280
	[CLSCompliant(false)]
	public spr\u23A5 ᜆ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			BiffRecordRaw biffRecordRaw;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 10;
					continue;
				case 1:
					if (this.ᜅ >= 0)
					{
						num = 7;
						continue;
					}
					goto IL_66;
				case 2:
					goto IL_FA;
				case 3:
				{
					bool flag;
					if (flag)
					{
						num = 11;
						continue;
					}
					goto IL_191;
				}
				case 4:
					goto IL_86;
				case 5:
					goto IL_88;
				case 7:
					num = 8;
					continue;
				case 8:
					if (A_0 >= this.ᜅ)
					{
						num = 0;
						continue;
					}
					goto IL_66;
				case 9:
					this.ᜀ(false, A_1);
					num = 5;
					continue;
				case 10:
				{
					if (A_0 > this.ᜆ)
					{
						num = 4;
						continue;
					}
					bool flag;
					int num2 = this.ᜀ(A_0, out flag);
					biffRecordRaw = null;
					num = 3;
					continue;
				}
				case 11:
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
						int num2;
						int a_ = (int)this.ᜈ.ReadInt16(num2);
						int num3 = (int)this.ᜈ.ReadInt16(num2 + 2);
						biffRecordRaw = spr\u175E.ᜀ(a_);
						biffRecordRaw.Length = num3;
						biffRecordRaw.ParseStructure(this.ᜈ, num2 + 4, num3, this.ᜆ());
						num = 2;
						continue;
					}
					}
					break;
				}
				IL_50:
				if (this.ᜏ())
				{
					num = 9;
					continue;
				}
				goto IL_88;
				goto IL_50;
				IL_88:
				num = 1;
			}
			IL_66:
			return null;
			IL_86:
			goto IL_66;
			IL_FA:
			IL_191:
			return biffRecordRaw as spr\u23A5;
		}
		}
	}

	// Token: 0x06003063 RID: 12387 RVA: 0x001B8430 File Offset: 0x001B7430
	[CLSCompliant(false)]
	public spr\u23A5 ᜀ(int A_0, int A_1, RecordExtractor A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 8;
			BiffRecordRaw biffRecordRaw;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ(false, A_1);
					num = 5;
					continue;
				case 1:
					goto IL_FB;
				case 2:
				{
					if (A_0 > this.ᜆ)
					{
						num = 11;
						continue;
					}
					bool flag;
					int num2 = this.ᜀ(A_0, out flag);
					biffRecordRaw = null;
					num = 10;
					continue;
				}
				case 3:
					if (this.ᜅ >= 0)
					{
						num = 6;
						continue;
					}
					goto IL_66;
				case 4:
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
						int num2;
						int a_ = (int)this.ᜈ.ReadInt16(num2);
						int num3 = (int)this.ᜈ.ReadInt16(num2 + 2);
						biffRecordRaw = A_2.ᜀ(a_);
						biffRecordRaw.Length = num3;
						biffRecordRaw.ParseStructure(this.ᜈ, num2 + 4, num3, this.ᜆ());
						num = 1;
						continue;
					}
					}
					break;
				case 5:
					goto IL_88;
				case 6:
					num = 9;
					continue;
				case 7:
					if (true)
					{
					}
					num = 2;
					continue;
				case 9:
					if (A_0 >= this.ᜅ)
					{
						num = 7;
						continue;
					}
					goto IL_66;
				case 10:
				{
					bool flag;
					if (flag)
					{
						num = 4;
						continue;
					}
					goto IL_192;
				}
				case 11:
					goto IL_86;
				}
				IL_50:
				if (this.ᜏ())
				{
					num = 0;
					continue;
				}
				goto IL_88;
				goto IL_50;
				IL_88:
				num = 3;
			}
			IL_66:
			return null;
			IL_86:
			goto IL_66;
			IL_FB:
			IL_192:
			return biffRecordRaw as spr\u23A5;
		}
		}
	}

	// Token: 0x06003064 RID: 12388 RVA: 0x001B85E0 File Offset: 0x001B75E0
	[CLSCompliant(false)]
	public void ᜁ(int A_0, spr\u23A5 A_1, int A_2)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_40;
			case 1:
				this.ᜀ(false, A_2);
				num = 0;
				continue;
			}
			IL_1C:
			if (this.ᜏ())
			{
				num = 1;
				continue;
			}
			IL_40:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_1C;
			default:
				goto IL_60;
			}
		}
		IL_60:
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜀ(A_0, A_1, A_2);
	}

	// Token: 0x06003065 RID: 12389 RVA: 0x001B8664 File Offset: 0x001B7664
	public void \u1716()
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
		this.ᜅ = -1;
		this.ᜆ = -1;
		this.ᜇ = 0;
		this.ᜀ(0);
	}

	// Token: 0x06003066 RID: 12390 RVA: 0x001B86BC File Offset: 0x001B76BC
	public void ᜀ(int A_0, string A_1, int A_2)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int a_2;
			int num;
			for (;;)
			{
				num = this.ᜀ(A_0, out a_2);
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_AC;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AF;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							if (num < 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_AF;
						}
						break;
					case 2:
						return;
					case 3:
						if (A_1 == null)
						{
							num2 = 2;
							continue;
						}
						num2 = 1;
						continue;
					}
					break;
				}
			}
			return;
			IL_AC:
			throw new NotSupportedException(RecordTableEnumerator.b("ॆⱈ⹊⥌潎㝐㱒❔㩖ⱘ㝚㱜罞ɠ٢।୦䥨Ὢɬ佮ɰᙲŴ坶㽸ᑺོቾ풆ﶈ力얒ﮖﺚ뎜", a_));
			IL_AF:
			spr᱒.ᜆ(this.ᜈ, a_2, this.ᜆ());
			spr\u21DF spr_u21DF = (spr\u21DF)spr\u175E.ᜀ(TBIFFRecord.String);
			spr_u21DF.ᜀ(A_1);
			int a_3 = spr_u21DF.GetStoreSize(this.ᜆ()) + 4;
			this.ᜀ(num, 0, a_3, spr_u21DF, A_2);
			return;
		}
		}
	}

	// Token: 0x06003067 RID: 12391 RVA: 0x001B87BC File Offset: 0x001B77BC
	[CLSCompliant(false)]
	internal void ᜀ(int A_0, spr\u225F A_1, int A_2)
	{
		int a_ = 11;
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag;
				int num = this.ᜀ(A_0, out flag);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						this.ᜄ(num);
						num2 = 3;
						continue;
					case 1:
						if (A_1 != null)
						{
							num2 = 11;
							continue;
						}
						goto IL_1B5;
					case 2:
					{
						if (!flag)
						{
							num2 = 6;
							continue;
						}
						TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num);
						num2 = 10;
						continue;
					}
					case 3:
						IL_84:
						goto IL_140;
					case 4:
						goto IL_EE;
					case 5:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.Array)
						{
							num2 = 0;
							continue;
						}
						goto IL_140;
					}
					case 6:
						goto IL_6F;
					case 7:
						goto IL_1B5;
					case 8:
					{
						TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num);
						num2 = 5;
						continue;
					}
					case 9:
						if (num < this.ᜇ)
						{
							if (true)
							{
							}
							num2 = 8;
							continue;
						}
						goto IL_140;
					case 10:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord != TBIFFRecord.Formula)
						{
							num2 = 4;
							continue;
						}
						num = this.ᜃ(num);
						num2 = 9;
						continue;
					}
					case 11:
					{
						int a_2 = 4 + A_1.GetStoreSize(this.ᜆ());
						this.ᜀ(num, 0, a_2, A_1, A_2);
						num2 = 7;
						continue;
					}
					}
					break;
					IL_140:
					num2 = 1;
					continue;
					IL_1B5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_84;
					default:
						goto IL_1CB;
					}
				}
			}
			IL_6F:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⡀B⩄⭆㱈♊⍌َ㽐㝒ご⽖", a_), RecordTableEnumerator.b("ɀ≂⭄⥆♈㽊浌⥎㡐㵒ㅔ睖⭘㹚㹜ぞ፠ݢ䕤ၦhὪլ佮ɰͲၴᑶၸᵺᑼ᩾ꎂ", a_));
			IL_EE:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ፀ♂♄⡆㭈⽊์⁎㕐㙒", a_), RecordTableEnumerator.b("ɀ≂⭄⥆♈㽊浌⥎㡐㵒ㅔ睖὘㑚⽜㉞ᑠརѤ㕦౨ࡪɬᵮᕰ卲ɴṶ൸፺嵼౾놐杖ﮖ뾞좠춢솤슦톨", a_));
			IL_1CB:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06003068 RID: 12392 RVA: 0x001B899C File Offset: 0x001B799C
	[CLSCompliant(false)]
	public spr\u225F ᜑ(int A_0)
	{
		for (;;)
		{
			TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(A_0);
			if (true)
			{
			}
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7A;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7A;
					default:
						if (false)
						{
						}
						if (A_0 < this.ᜇ)
						{
							num = 5;
							continue;
						}
						goto IL_DC;
					}
					break;
				case 2:
					goto IL_49;
				case 3:
					if (tbiffrecord == TBIFFRecord.Array)
					{
						num = 0;
						continue;
					}
					goto IL_DC;
				case 4:
					if (tbiffrecord != TBIFFRecord.Formula)
					{
						num = 2;
						continue;
					}
					A_0 = this.ᜃ(A_0);
					num = 1;
					continue;
				case 5:
					tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(A_0);
					num = 3;
					continue;
				}
				break;
			}
		}
		IL_49:
		return null;
		IL_7A:
		return (spr\u225F)spr\u175E.ᜀ(this.ᜈ, A_0, this.ᜆ());
		IL_DC:
		return null;
	}

	// Token: 0x06003069 RID: 12393 RVA: 0x001B8A88 File Offset: 0x001B7A88
	[CLSCompliant(false)]
	internal spr\u225F \u1713(int A_0)
	{
		int a_ = 17;
		int a_2;
		for (;;)
		{
			if (true)
			{
			}
			bool flag;
			a_2 = this.ᜀ(A_0, out flag);
			if (!flag)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_62;
			}
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⹆ੈ⑊⅌㩎㱐㵒᱔㥖㵘㹚╜", a_), RecordTableEnumerator.b("ц⡈╊⍌⁎═獒㍔㹖㝘㽚絜ⵞѠb੤ᕦ൨䭪ᩬٮհ᭲啴ѶॸṺṼᙾꦈ", a_));
		IL_62:
		if (false)
		{
		}
		return this.ᜑ(a_2);
	}

	// Token: 0x0600306A RID: 12394 RVA: 0x001B8B04 File Offset: 0x001B7B04
	public object \u170D()
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
		IntPtr a_ = this.ᜃ();
		return this.ᜀ(a_);
	}

	// Token: 0x0600306B RID: 12395 RVA: 0x001B8B50 File Offset: 0x001B7B50
	private IntPtr ᜃ()
	{
		sprᰟ sprᰟ;
		for (;;)
		{
			sprᰟ = (this.ᜈ as sprᰟ);
			if (sprᰟ == null)
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_3F;
			}
		}
		if (true)
		{
		}
		return IntPtr.Zero;
		IL_3F:
		if (false)
		{
		}
		return sprᰟ.ᜁ();
	}

	// Token: 0x0600306C RID: 12396 RVA: 0x001B8BA8 File Offset: 0x001B7BA8
	public object ᜀ(IntPtr A_0)
	{
		sprᱧ sprᱧ;
		for (;;)
		{
			sprᱧ = new sprᱧ(0, (int)this.ᜑ, (int)this.\u1713);
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜅ >= 0)
					{
						goto IL_CE;
					}
					goto IL_109;
				case 1:
					if (this.ᜈ != null)
					{
						num = 6;
						continue;
					}
					goto IL_109;
				case 2:
					goto IL_B8;
				case 3:
					if (this.ᜇ > 0)
					{
						num = 4;
						continue;
					}
					goto IL_109;
				case 4:
					num = 0;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CE;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						sprᱧ.ᜈ = spr\u17FF.ᜀ(A_0);
						sprᱧ.ᜅ(this.ᜠ(), 1);
						this.ᜈ.CopyTo(0, sprᱧ.ᜈ, 0, this.ᜇ);
						num = 2;
						continue;
					}
					break;
				case 6:
					num = 3;
					continue;
				}
				break;
				IL_CE:
				num = 5;
			}
		}
		IL_B8:
		IL_109:
		sprᱧ.ᜅ = this.ᜅ;
		sprᱧ.ᜆ = this.ᜆ;
		sprᱧ.ᜇ = this.ᜇ;
		sprᱧ.ᜉ = this.ᜉ;
		sprᱧ.\u1712 = this.\u1712;
		sprᱧ.ᜌ = this.ᜌ;
		sprᱧ.\u1713 = this.\u1713;
		return sprᱧ;
	}

	// Token: 0x0600306D RID: 12397 RVA: 0x001B8D14 File Offset: 0x001B7D14
	public sprᱧ ᜃ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			sprᱧ sprᱧ;
			for (;;)
			{
				sprᱧ = new sprᱧ(0, (int)this.ᜑ, (int)this.\u1713);
				sprᱧ.ᜉ = this.ᜉ;
				sprᱧ.\u1712 = this.\u1712;
				sprᱧ.ᜌ = this.ᜌ;
				sprᱧ.\u1713 = this.\u1713;
				int num = 0;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						if (this.ᜇ > 0)
						{
							num = 7;
							continue;
						}
						goto IL_1D6;
					case 1:
						goto IL_144;
					case 2:
					{
						if (A_0 < 0)
						{
							num = 4;
							continue;
						}
						Point point;
						int x = point.X;
						int y = point.Y;
						num2 = y - x;
						num = 3;
						continue;
					}
					case 3:
						if (num2 > 0)
						{
							num = 9;
							continue;
						}
						goto IL_DD;
					case 4:
						goto IL_1D1;
					case 5:
						goto IL_FF;
					case 6:
						if (A_0 > A_1)
						{
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
						{
							if (false)
							{
							}
							this.ᜀ(false, A_2);
							Point point = this.ᜀ(A_0, A_1, out A_0, out A_1);
							num = 2;
							continue;
						}
						}
						break;
					case 7:
						A_0 = Math.Max(this.ᜅ, A_0);
						A_1 = Math.Min(this.ᜆ, A_1);
						if (true)
						{
						}
						num = 6;
						continue;
					case 8:
						goto IL_DD;
					case 9:
					{
						sprᱧ.ᜁ(this.ᜃ());
						sprᱧ.ᜅ(num2, A_2);
						int x;
						this.ᜈ.CopyTo(x, sprᱧ.ᜈ, 0, num2);
						num = 8;
						continue;
					}
					}
					break;
					IL_DD:
					sprᱧ.ᜅ = A_0;
					sprᱧ.ᜆ = A_1;
					sprᱧ.ᜇ = num2;
					num = 5;
				}
			}
			IL_FF:
			goto IL_1D6;
			IL_144:
			return null;
			IL_1D1:
			return null;
			IL_1D6:
			sprᱧ.ᜊ = -1;
			sprᱧ.ᜋ = -1;
			return sprᱧ;
		}
		}
	}

	// Token: 0x0600306E RID: 12398 RVA: 0x001B8F10 File Offset: 0x001B7F10
	public sprᱧ ᜁ(SSTDictionary A_0, SSTDictionary A_1, Dictionary<int, int> A_2, Dictionary<string, string> A_3, Dictionary<int, int> A_4, Dictionary<int, int> A_5, Dictionary<int, int> A_6)
	{
		sprᱧ sprᱧ;
		for (;;)
		{
			spr\u17FF spr_u17FF = A_1.Workbook.AppImplementation;
			sprᱧ = new sprᱧ(0, spr_u17FF.ᜅ(), (int)this.\u1713);
			sprᱧ.ᜅ = this.ᜅ;
			sprᱧ.ᜆ = this.ᜆ;
			sprᱧ.ᜇ = this.ᜇ;
			sprᱧ.ᜉ = this.ᜉ;
			sprᱧ.ᜑ = this.ᜑ;
			sprᱧ.\u1712 = this.\u1712;
			sprᱧ.ᜌ = this.ᜌ;
			sprᱧ.\u1713 = this.\u1713;
			if (true)
			{
			}
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜇ > 0)
					{
						num = 3;
						continue;
					}
					goto IL_189;
				case 1:
					num = 0;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_14E;
					default:
						if (false)
						{
						}
						sprᱧ.ᜈ = spr\u17FF.ᜀ(A_1.Workbook.HeapHandle);
						sprᱧ.ᜈ.EnsureCapacity(this.ᜇ);
						this.ᜈ.CopyTo(0, sprᱧ.ᜈ, 0, this.ᜇ);
						num = 5;
						continue;
					}
					break;
				case 3:
					num = 6;
					continue;
				case 4:
					if (this.ᜈ != null)
					{
						num = 1;
						continue;
					}
					goto IL_189;
				case 5:
					goto IL_138;
				case 6:
					if (this.ᜅ >= 0)
					{
						goto IL_14E;
					}
					goto IL_189;
				}
				break;
				IL_14E:
				num = 2;
			}
		}
		IL_138:
		IL_189:
		sprᱧ.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5, A_6);
		sprᱧ.ᜊ = -1;
		sprᱧ.ᜋ = -1;
		return sprᱧ;
	}

	// Token: 0x0600306F RID: 12399 RVA: 0x001B90C8 File Offset: 0x001B80C8
	public void ᜁ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (A_0 <= A_1)
					{
						this.ᜀ(false, A_2);
						Point point = this.ᜀ(A_0, A_1, out A_0, out A_1);
						int x = point.X;
						int y = point.Y;
						num2 = y - x;
						num = 4;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 2:
					goto IL_126;
				case 3:
				{
					int num3;
					if (num3 > 0)
					{
						num = 7;
						continue;
					}
					goto IL_153;
				}
				case 4:
				{
					if (num2 <= 0)
					{
						num = 8;
						continue;
					}
					int y;
					int num3 = this.ᜇ - y;
					num = 3;
					continue;
				}
				case 5:
					return;
				case 6:
					return;
				case 7:
				{
					int x;
					int y;
					int num3;
					this.ᜈ.MoveMemory(x, y, num3);
					num = 2;
					continue;
				}
				case 8:
					goto IL_AB;
				}
				if (this.ᜅ < 0)
				{
					num = 6;
				}
				else
				{
					A_0 = Math.Max(this.ᜅ, A_0);
					A_1 = Math.Min(this.ᜆ, A_1);
					num = 0;
				}
			}
			return;
			IL_AB:
			return;
			IL_126:
			IL_153:
			this.ᜇ -= num2;
			this.ᜂ();
			return;
		}
		}
	}

	// Token: 0x06003070 RID: 12400 RVA: 0x001B923C File Offset: 0x001B823C
	public void ᜀ(int A_0, int A_1, int A_2, int A_3)
	{
		for (;;)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_36;
				}
				break;
			}
		}
		IL_36:
		if (false)
		{
		}
		int num;
		Ptg[] array;
		spr\u252B spr_u252B;
		spr᱒ spr᱒;
		for (;;)
		{
			bool flag;
			num = this.ᜀ(A_0, out flag);
			int num2 = 6;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (array.Length == 0)
					{
						num2 = 8;
						continue;
					}
					spr_u252B = (array[0] as spr\u252B);
					num2 = 7;
					continue;
				case 1:
					return;
				case 2:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.Formula)
					{
						num2 = 9;
						continue;
					}
					spr᱒ = (spr\u175E.ᜀ(this.ᜈ, num, this.ᜆ()) as spr᱒);
					array = spr᱒.ᜑ();
					num2 = 5;
					continue;
				}
				case 3:
					return;
				case 4:
					num2 = 0;
					continue;
				case 5:
					if (array != null)
					{
						num2 = 4;
						continue;
					}
					return;
				case 6:
				{
					if (!flag)
					{
						num2 = 1;
						continue;
					}
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num);
					num2 = 2;
					continue;
				}
				case 7:
					if (spr_u252B == null)
					{
						num2 = 3;
						continue;
					}
					goto IL_144;
				case 8:
					goto IL_A7;
				case 9:
					return;
				}
				break;
			}
		}
		return;
		IL_A7:
		return;
		IL_144:
		spr_u252B.ᜂ(A_1);
		spr_u252B.ᜃ(A_2);
		spr᱒.ᜁ(array);
		int num3 = spr᱒.GetStoreSize(this.ᜆ()) + 4;
		this.ᜀ(num, num3, num3, spr᱒, A_3);
	}

	// Token: 0x06003071 RID: 12401 RVA: 0x001B93C4 File Offset: 0x001B83C4
	public bool \u1718(int A_0)
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
		bool result;
		this.ᜀ(A_0, out result);
		return result;
	}

	// Token: 0x06003072 RID: 12402 RVA: 0x001B940C File Offset: 0x001B840C
	public void ᜀ(sprᱧ A_0, int A_1, IntPtr A_2)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				int num4;
				int num5;
				switch (num)
				{
				case 0:
					goto IL_15A;
				case 1:
				{
					if (A_0.ᜇ <= 0)
					{
						num = 11;
						continue;
					}
					if (true)
					{
					}
					this.ᜌ = A_0.ᜌ;
					int num2 = A_0.\u171C();
					int num3 = A_0.\u171E();
					num = 7;
					continue;
				}
				case 2:
					goto IL_81;
				case 4:
				{
					int num6;
					this.ᜈ.MoveMemory(num4 + num5, num4, num6);
					num = 2;
					continue;
				}
				case 5:
				{
					int num6;
					if (num6 > 0)
					{
						num = 4;
						continue;
					}
					goto IL_81;
				}
				case 6:
					num = 13;
					continue;
				case 7:
					if (this.ᜈ != null)
					{
						num = 6;
						continue;
					}
					goto IL_231;
				case 8:
					goto IL_201;
				case 9:
				{
					if (this.ᜅ >= 0)
					{
						num = 15;
						continue;
					}
					int num2;
					this.ᜅ = num2;
					int num3;
					this.ᜆ = num3;
					num = 10;
					continue;
				}
				case 10:
					goto IL_1C2;
				case 11:
					return;
				case 12:
					goto IL_201;
				case 13:
				{
					if (this.ᜇ <= 0)
					{
						num = 16;
						continue;
					}
					int num2;
					int num3;
					this.ᜁ(num2, num3, A_1);
					bool flag;
					num4 = this.ᜀ(num2, out flag);
					int num6 = this.ᜇ - num4;
					num5 = A_0.ᜇ;
					this.ᜅ(this.ᜇ + num5, A_1);
					num = 5;
					continue;
				}
				case 14:
					goto IL_7C;
				case 15:
				{
					int num2;
					this.ᜅ = Math.Min(this.ᜅ, num2);
					int num3;
					this.ᜆ = Math.Max(this.ᜆ, num3);
					num = 0;
					continue;
				}
				case 16:
					goto IL_231;
				}
				if (A_0 == null)
				{
					num = 14;
					continue;
				}
				num = 1;
				continue;
				IL_81:
				A_0.ᜈ.CopyTo(0, this.ᜈ, num4, num5);
				this.ᜇ += num5;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 12;
					continue;
				}
				IL_201:
				num = 9;
				continue;
				IL_231:
				this.ᜇ = A_0.ᜇ;
				this.ᜁ(A_2);
				this.ᜅ(this.ᜇ, A_1);
				A_0.ᜈ.CopyTo(0, this.ᜈ, 0, this.ᜇ);
				num = 8;
			}
			IL_7C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㩈⑊㡌㵎㉐㙒ݔ㡖⹘", a_));
			IL_15A:
			IL_1C2:
			this.ᜋ = -1;
			this.ᜊ = -1;
			return;
		}
		}
	}

	// Token: 0x06003073 RID: 12403 RVA: 0x001B96E8 File Offset: 0x001B86E8
	public void ᜀ(int A_0, int A_1, Rectangle A_2, int A_3, Rectangle A_4, int A_5, XlsWorkbook A_6)
	{
		switch (0)
		{
		default:
		{
			int num = 14;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_227;
				case 2:
					if (this.ᜇ > 0)
					{
						num = 11;
						continue;
					}
					return;
				case 3:
					goto IL_227;
				case 4:
				{
					spr\u225F spr_u225F = spr\u175E.ᜀ(this.ᜈ, num2, this.ᜆ()) as spr\u225F;
					Ptg[] a_ = spr_u225F.ᜅ();
					spr_u225F.ᜀ(A_6.FormulaUtil.ᜀ(a_, A_0, A_1, A_2, A_3, A_4, spr_u225F.ᜉ() + 1, spr_u225F.ᜈ() + 1));
					int a_2 = num3 + 4;
					this.ᜀ(num2, a_2, spr_u225F.GetStoreSize(this.ᜆ()) + 4, spr_u225F, A_5);
					num3 = spr_u225F.GetStoreSize(this.ᜆ());
					num = 8;
					continue;
				}
				case 5:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord == TBIFFRecord.Array)
					{
						num = 4;
						continue;
					}
					goto IL_1C8;
				}
				case 6:
				{
					if (num2 >= this.ᜇ)
					{
						num = 0;
						continue;
					}
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num3 = (int)this.ᜈ.ReadInt16(num2 + 2);
					num = 12;
					continue;
				}
				case 7:
					if (this.ᜅ < 0)
					{
						num = 9;
						continue;
					}
					goto IL_269;
				case 8:
					goto IL_1C8;
				case 9:
					goto IL_1C3;
				case 10:
				{
					spr᱒ spr᱒ = spr\u175E.ᜀ(this.ᜈ, num2, this.ᜆ()) as spr᱒;
					Ptg[] a_3 = spr᱒.ᜑ();
					spr᱒.ᜁ(A_6.FormulaUtil.ᜀ(a_3, A_0, A_1, A_2, A_3, A_4, spr᱒.\u1714() + 1, spr᱒.\u1713() + 1));
					this.ᜀ(num2, num3 + 4, spr᱒.GetStoreSize(this.ᜆ()) + 4, spr᱒, A_5);
					num3 = spr᱒.GetStoreSize(this.ᜆ());
					num = 13;
					continue;
				}
				case 11:
					num = 7;
					continue;
				case 12:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord == TBIFFRecord.Formula)
					{
						num = 10;
						continue;
					}
					num = 5;
					continue;
				}
				case 13:
					goto IL_1C8;
				case 15:
					num = 2;
					continue;
				}
				if (this.ᜈ != null)
				{
					if (true)
					{
					}
					num = 15;
					continue;
				}
				break;
				IL_1C8:
				num2 += num3 + 4;
				num = 3;
				continue;
				IL_227:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_269:
					num2 = 0;
					int top = A_4.Top;
					int top2 = A_2.Top;
					int left = A_4.Left;
					int left2 = A_2.Left;
					num = 1;
					break;
				}
				default:
					if (false)
					{
					}
					num = 6;
					break;
				}
			}
			IL_1C3:
			return;
		}
		}
	}

	// Token: 0x06003074 RID: 12404 RVA: 0x001B99D4 File Offset: 0x001B89D4
	public void ᜂ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				this.ᜅ += A_1;
				this.ᜆ += A_1;
				int num2 = 12;
				for (;;)
				{
					int num6;
					switch (num2)
					{
					case 0:
						goto IL_13B;
					case 1:
					{
						int num3 = this.\u171A(num);
						this.ᜂ(num, num3 + A_0);
						int num4 = this.ᜉ(num);
						this.ᜁ(num, num4 + A_1);
						num2 = 2;
						continue;
					}
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
						{
							if (false)
							{
							}
							TBIFFRecord tbiffrecord;
							if (tbiffrecord != TBIFFRecord.MulBlank)
							{
								num2 = 5;
								continue;
							}
							goto IL_7B;
						}
						}
						break;
					case 3:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.Array)
						{
							num2 = 10;
							continue;
						}
						num2 = 13;
						continue;
					}
					case 4:
					{
						if (num >= this.ᜇ)
						{
							num2 = 9;
							continue;
						}
						short num5 = this.ᜈ.ReadInt16(num);
						TBIFFRecord tbiffrecord = (TBIFFRecord)num5;
						num6 = (int)this.ᜈ.ReadInt16(num + 2);
						if (true)
						{
						}
						num2 = 3;
						continue;
					}
					case 5:
						num2 = 8;
						continue;
					case 6:
						goto IL_1B2;
					case 7:
						goto IL_7B;
					case 8:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.MulRK)
						{
							num2 = 7;
							continue;
						}
						goto IL_1B2;
					}
					case 9:
						return;
					case 10:
					{
						spr\u225F spr_u225F = (spr\u225F)spr\u175E.ᜀ(this.ᜈ, num, this.ᜆ());
						spr\u225F spr_u225F2 = spr_u225F;
						spr_u225F2.ᜃ(spr_u225F2.ᜈ() + A_1);
						spr\u225F spr_u225F3 = spr_u225F;
						spr_u225F3.ᜁ(spr_u225F3.ᜀ() + A_1);
						spr\u225F spr_u225F4 = spr_u225F;
						spr_u225F4.ᜂ(spr_u225F4.ᜉ() + A_0);
						spr\u225F spr_u225F5 = spr_u225F;
						spr_u225F5.ᜀ(spr_u225F5.\u170D() + A_0);
						int num7 = num6 + 4;
						this.ᜀ(num, num7, num7, spr_u225F, A_2);
						num2 = 11;
						continue;
					}
					case 11:
						goto IL_1B2;
					case 12:
						goto IL_13B;
					case 13:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord != TBIFFRecord.String)
						{
							num2 = 1;
							continue;
						}
						goto IL_1B2;
					}
					}
					break;
					IL_7B:
					sprᲀ.ᜀ(this.ᜈ, num, num6, this.ᜆ(), A_1);
					num2 = 6;
					continue;
					IL_13B:
					num2 = 4;
					continue;
					IL_1B2:
					num += num6 + 4;
					num2 = 0;
				}
			}
			return;
		}
	}

	// Token: 0x06003075 RID: 12405 RVA: 0x001B9C48 File Offset: 0x001B8C48
	public void ᜀ(XlsWorkbook A_0, int[] A_1, int A_2)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				Ptg[] a_2;
				spr᥌ spr᥌;
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					goto IL_B5;
				case 2:
					goto IL_112;
				case 3:
					if (true)
					{
					}
					goto IL_75;
				case 4:
					goto IL_15B;
				case 5:
					if (A_0.FormulaUtil.ᜁ(a_2, A_1))
					{
						num = 9;
						continue;
					}
					goto IL_75;
				case 6:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.Formula)
					{
						num = 12;
						continue;
					}
					goto IL_B5;
				}
				case 7:
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
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.Array)
						{
							num = 0;
							continue;
						}
						goto IL_112;
					}
					}
					break;
				case 8:
					return;
				case 9:
					spr᥌.ᜀ(a_2);
					num = 3;
					continue;
				case 10:
					goto IL_15B;
				case 11:
					goto IL_70;
				case 12:
					num = 7;
					continue;
				case 13:
				{
					if (num2 >= this.ᜇ)
					{
						num = 8;
						continue;
					}
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num3 = (int)this.ᜈ.ReadInt16(num2 + 2);
					num = 6;
					continue;
				}
				}
				if (A_1 == null)
				{
					num = 11;
					continue;
				}
				num2 = 0;
				num = 4;
				continue;
				IL_75:
				BiffRecordRaw biffRecordRaw = (BiffRecordRaw)spr᥌;
				this.ᜀ(num2, num3 + 4, biffRecordRaw.GetStoreSize(this.ᜆ()) + 4, biffRecordRaw, A_2);
				num3 = biffRecordRaw.GetStoreSize(this.ᜆ());
				num = 2;
				continue;
				IL_B5:
				spr᥌ = (spr᥌)spr\u175E.ᜀ(this.ᜈ, num2, this.ᜆ());
				a_2 = spr᥌.ᜀ();
				num = 5;
				continue;
				IL_112:
				num2 += num3 + 4;
				num = 10;
				continue;
				IL_15B:
				num = 13;
			}
			IL_70:
			throw new ArgumentNullException(RecordTableEnumerator.b("⥇㡉㹋M㕏║ᵓ㡕㱗㽙⑛", a_));
		}
		}
	}

	// Token: 0x06003076 RID: 12406 RVA: 0x001B9E68 File Offset: 0x001B8E68
	public void ᜀ(XlsWorkbook A_0, IDictionary<int, int> A_1, int A_2)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 11;
			for (;;)
			{
				Ptg[] a_2;
				int num2;
				int num3;
				spr᥌ spr᥌;
				switch (num)
				{
				case 0:
					goto IL_112;
				case 1:
					goto IL_70;
				case 2:
					goto IL_B5;
				case 3:
					if (A_0.FormulaUtil.ᜀ(a_2, A_1))
					{
						num = 7;
						continue;
					}
					goto IL_75;
				case 4:
					num = 9;
					continue;
				case 5:
				{
					if (num2 >= this.ᜇ)
					{
						num = 6;
						continue;
					}
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num3 = (int)this.ᜈ.ReadInt16(num2 + 2);
					num = 10;
					continue;
				}
				case 6:
					return;
				case 7:
					spr᥌.ᜀ(a_2);
					num = 8;
					continue;
				case 8:
					if (true)
					{
					}
					goto IL_75;
				case 9:
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
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.Array)
						{
							num = 2;
							continue;
						}
						goto IL_112;
					}
					}
					break;
				case 10:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.Formula)
					{
						num = 4;
						continue;
					}
					goto IL_B5;
				}
				case 12:
					goto IL_15B;
				case 13:
					goto IL_15B;
				}
				if (A_1 == null)
				{
					num = 1;
					continue;
				}
				num2 = 0;
				num = 12;
				continue;
				IL_75:
				BiffRecordRaw biffRecordRaw = (BiffRecordRaw)spr᥌;
				this.ᜀ(num2, num3 + 4, biffRecordRaw.GetStoreSize(this.ᜆ()) + 4, biffRecordRaw, A_2);
				num3 = biffRecordRaw.GetStoreSize(this.ᜆ());
				num = 0;
				continue;
				IL_B5:
				spr᥌ = (spr᥌)spr\u175E.ᜀ(this.ᜈ, num2, this.ᜆ());
				a_2 = spr᥌.ᜀ();
				num = 3;
				continue;
				IL_112:
				num2 += num3 + 4;
				num = 13;
				continue;
				IL_15B:
				num = 5;
			}
			IL_70:
			throw new ArgumentNullException(RecordTableEnumerator.b("⅄⹆⩈Պ⡌㡎ᡐ㵒ㅔ㉖⅘", a_));
		}
		}
	}

	// Token: 0x06003077 RID: 12407 RVA: 0x001BA088 File Offset: 0x001B9088
	[CLSCompliant(false)]
	internal void ᜀ(XlsWorkbook A_0, int A_1, int A_2, spr\u1DE2 A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = A_0.ReservedHandle.\u171D();
				int num2 = 30;
				for (;;)
				{
					TBIFFRecord tbiffrecord;
					int num3;
					int num5;
					switch (num2)
					{
					case 0:
						return;
					case 1:
					{
						spr\u252B spr_u252B;
						if (spr_u252B.ᜆ() == A_2)
						{
							num2 = 21;
							continue;
						}
						goto IL_1B3;
					}
					case 2:
						if (tbiffrecord == TBIFFRecord.Formula)
						{
							num2 = 13;
							continue;
						}
						goto IL_1B3;
					case 3:
						num2 = 22;
						continue;
					case 4:
						goto IL_171;
					case 5:
						goto IL_271;
					case 6:
					{
						Ptg[] array;
						spr\u252B spr_u252B = (spr\u252B)array[0];
						num2 = 15;
						continue;
					}
					case 7:
						num2 = 18;
						continue;
					case 8:
						this.ᜀ(false, num);
						num2 = 9;
						continue;
					case 9:
						goto IL_32A;
					case 10:
						goto IL_27D;
					case 11:
						if (tbiffrecord != TBIFFRecord.Array)
						{
							num2 = 7;
							continue;
						}
						goto IL_1B3;
					case 12:
						if (this.ᜅ <= A_3.ᜃ())
						{
							num2 = 3;
							continue;
						}
						return;
					case 13:
					{
						spr᱒ spr᱒ = (spr᱒)spr\u175E.ᜀ(this.ᜈ, num3, this.ᜆ());
						spr᱒.ᜃ(true);
						spr᱒.ᜀ(true);
						spr᱒.ᜄ(false);
						Ptg[] array = spr᱒.ᜑ();
						num2 = 16;
						continue;
					}
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_27D;
						default:
							if (false)
							{
							}
							num2 = 23;
							continue;
						}
						break;
					case 15:
					{
						spr\u252B spr_u252B;
						if (spr_u252B.ᜇ() == A_1)
						{
							num2 = 26;
							continue;
						}
						goto IL_1B3;
					}
					case 16:
					{
						Ptg[] array;
						if (array != null)
						{
							num2 = 14;
							continue;
						}
						goto IL_1B3;
					}
					case 17:
						return;
					case 18:
						if (tbiffrecord != TBIFFRecord.String)
						{
							num2 = 25;
							continue;
						}
						goto IL_1B3;
					case 19:
						num2 = 31;
						continue;
					case 20:
						num2 = 2;
						continue;
					case 21:
					{
						spr᱒ spr᱒;
						int num4;
						Ptg[] array = FormulaUtil.ᜀ(A_3, A_0, spr᱒.\u1714(), num4);
						spr᱒.ᜁ(array);
						int storeSize = spr᱒.GetStoreSize(this.ᜆ());
						this.ᜀ(num3, num5 + 4, storeSize + 4, spr᱒, num);
						num5 = storeSize;
						num2 = 29;
						continue;
					}
					case 22:
					{
						if (this.ᜆ < A_3.ᜈ())
						{
							num2 = 4;
							continue;
						}
						bool flag;
						num3 = this.ᜀ(A_3.ᜈ(), out flag);
						int num4 = -1;
						if (true)
						{
						}
						num2 = 28;
						continue;
					}
					case 23:
					{
						Ptg[] array;
						if (array.Length == 1)
						{
							num2 = 19;
							continue;
						}
						goto IL_1B3;
					}
					case 24:
					{
						int num4;
						if (num4 >= A_3.ᜈ())
						{
							num2 = 20;
							continue;
						}
						goto IL_1B3;
					}
					case 25:
					{
						int num4 = this.ᜉ(num3);
						num2 = 27;
						continue;
					}
					case 26:
						num2 = 1;
						continue;
					case 27:
					{
						int num4;
						if (num4 > A_3.ᜃ())
						{
							num2 = 0;
							continue;
						}
						num2 = 24;
						continue;
					}
					case 28:
						goto IL_271;
					case 29:
						goto IL_1B3;
					case 30:
						if (this.ᜏ())
						{
							num2 = 8;
							continue;
						}
						goto IL_32A;
					case 31:
					{
						Ptg[] array;
						if (array[0].TokenCode == FormulaToken.tExp)
						{
							num2 = 6;
							continue;
						}
						goto IL_1B3;
					}
					}
					break;
					IL_1B3:
					num3 += num5 + 4;
					num2 = 5;
					continue;
					IL_271:
					num2 = 10;
					continue;
					IL_27D:
					if (num3 >= this.ᜇ)
					{
						num2 = 17;
						continue;
					}
					tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num3);
					num5 = (int)this.ᜈ.ReadInt16(num3 + 2);
					num2 = 11;
					continue;
					IL_32A:
					num2 = 12;
				}
			}
			return;
			IL_171:
			return;
		}
	}

	// Token: 0x06003078 RID: 12408 RVA: 0x001BA4B4 File Offset: 0x001B94B4
	public void ᜀ(List<int> A_0)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord == TBIFFRecord.LabelSST)
					{
						num = 3;
						continue;
					}
					goto IL_5E;
				}
				case 1:
					goto IL_5E;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (num2 < this.ᜇ)
						{
							TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
							num3 = (int)this.ᜈ.ReadInt16(num2 + 2);
							if (true)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					num = 5;
					continue;
				case 3:
				{
					int num4 = spr\u1C7C.ᜂ(this.ᜈ, num2, this.ᜆ());
					num4 = A_0[num4];
					spr\u1C7C.ᜀ(this.ᜈ, num2, num4, this.ᜆ());
					num = 1;
					continue;
				}
				case 5:
					return;
				case 6:
					goto IL_5C;
				case 7:
					goto IL_D3;
				case 8:
					goto IL_D3;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num2 = 0;
				num = 8;
				continue;
				IL_5E:
				num2 += num3 + 4;
				num = 7;
				continue;
				IL_D3:
				num = 2;
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("⥇㡉㹋M㕏║ᵓ㡕㱗㽙⑛㭝፟", a_));
		}
		}
	}

	// Token: 0x06003079 RID: 12409 RVA: 0x001BA628 File Offset: 0x001B9628
	public List<long> ᜀ(int A_0, int A_1, string A_2, FindType A_3, int A_4, bool A_5, XlsWorkbook A_6)
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
		return this.ᜀ(A_0, A_1, A_2, A_3, ExcelFindOptions.None, A_4, A_5, A_6);
	}

	// Token: 0x0600307A RID: 12410 RVA: 0x001BA678 File Offset: 0x001B9678
	public List<long> ᜀ(int A_0, int A_1, string A_2, FindType A_3, ExcelFindOptions A_4, int A_5, bool A_6, XlsWorkbook A_7)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			List<long> list;
			for (;;)
			{
				bool flag = (A_3 & FindType.Text) == FindType.Text;
				bool flag2 = (A_3 & FindType.Error) == FindType.Error;
				bool flag3 = (A_3 & FindType.Formula) == FindType.Formula;
				bool flag4 = (A_3 & FindType.FormulaStringValue) == FindType.FormulaStringValue;
				int num = 2;
				for (;;)
				{
					bool flag5;
					int num2;
					int num3;
					int num6;
					bool flag6;
					switch (num)
					{
					case 0:
					{
						spr\u21DF spr_u21DF;
						flag5 = (spr_u21DF.ᜁ() == A_2);
						num = 44;
						continue;
					}
					case 1:
						goto IL_845;
					case 2:
						if (!flag)
						{
							num = 26;
							continue;
						}
						goto IL_761;
					case 3:
						goto IL_724;
					case 4:
					{
						string text;
						flag5 = (text == A_2);
						num = 1;
						continue;
					}
					case 5:
						goto IL_39A;
					case 6:
						if (flag2)
						{
							num = 21;
							continue;
						}
						goto IL_21E;
					case 7:
						goto IL_726;
					case 8:
						if (flag)
						{
							num = 38;
							continue;
						}
						goto IL_21E;
					case 9:
					{
						if (num2 >= this.ᜇ)
						{
							num = 31;
							continue;
						}
						TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
						num3 = (int)this.ᜈ.ReadInt16(num2 + 2);
						int num4 = this.\u171A(num2);
						int num5 = this.ᜉ(num2);
						flag5 = false;
						num = 23;
						continue;
					}
					case 10:
						if (num6 < this.ᜇ)
						{
							num = 66;
							continue;
						}
						goto IL_21E;
					case 11:
					{
						spr\u249B spr_u249B;
						if (spr_u249B.ᜂ())
						{
							num = 29;
							continue;
						}
						num = 56;
						continue;
					}
					case 12:
					{
						spr\u2170 spr_u;
						flag5 = (spr_u.ᜁ() == A_2);
						num = 39;
						continue;
					}
					case 13:
						if (!A_6)
						{
							num = 7;
							continue;
						}
						return list;
					case 14:
						goto IL_21E;
					case 15:
						goto IL_309;
					case 16:
						goto IL_21E;
					case 17:
					{
						spr\u249B spr_u249B;
						flag6 = ((int)spr_u249B.ᜄ() == A_5);
						goto IL_4AB;
					}
					case 18:
					{
						TBIFFRecord tbiffrecord;
						TBIFFRecord tbiffrecord2 = tbiffrecord;
						num = 55;
						continue;
					}
					case 19:
						goto IL_22A;
					case 20:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_22A;
						default:
							if (false)
							{
							}
							goto IL_509;
						}
						break;
					case 21:
					{
						spr\u249B spr_u249B = (spr\u249B)spr\u175E.ᜀ(this.ᜈ, num2, this.ᜆ());
						if (true)
						{
						}
						num = 11;
						continue;
					}
					case 22:
						goto IL_21E;
					case 23:
					{
						int num5;
						if (num5 <= A_1)
						{
							num = 18;
							continue;
						}
						return list;
					}
					case 24:
					{
						TBIFFRecord tbiffrecord2;
						if (tbiffrecord2 != TBIFFRecord.LabelSST)
						{
							num = 63;
							continue;
						}
						int a_2 = spr\u1C7C.ᜂ(this.ᜈ, num2, this.ᜆ());
						string text2 = A_7.InnerSST[a_2].ᜏ();
						num = 41;
						continue;
					}
					case 25:
					{
						spr\u21DF spr_u21DF = (spr\u21DF)spr\u175E.ᜀ(this.ᜈ, num6, this.ᜆ());
						num = 48;
						continue;
					}
					case 26:
						num = 65;
						continue;
					case 27:
						num = 67;
						continue;
					case 28:
					{
						TBIFFRecord tbiffrecord2;
						switch (tbiffrecord2)
						{
						case TBIFFRecord.Label:
							num = 8;
							continue;
						case TBIFFRecord.BoolErr:
							num = 6;
							continue;
						default:
							num = 50;
							continue;
						}
						break;
					}
					case 29:
						num = 17;
						continue;
					case 30:
						goto IL_21E;
					case 31:
						goto IL_32E;
					case 32:
						if (this.ᜈ != null)
						{
							num = 58;
							continue;
						}
						return list;
					case 33:
						num = 62;
						continue;
					case 34:
					{
						spr᱒ a_3 = (spr᱒)spr\u175E.ᜀ(this.ᜈ, num2, this.ᜆ());
						string text = '=' + A_7.FormulaUtil.ᜀ(a_3);
						num = 37;
						continue;
					}
					case 35:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.Array)
						{
							num = 64;
							continue;
						}
						goto IL_509;
					}
					case 36:
						goto IL_309;
					case 37:
					{
						if (A_4 == ExcelFindOptions.None)
						{
							num = 4;
							continue;
						}
						string text;
						flag5 = this.ᜀ(text, A_2, A_4, A_7);
						num = 42;
						continue;
					}
					case 38:
					{
						spr\u2170 spr_u = (spr\u2170)spr\u175E.ᜀ(this.ᜈ, num2, this.ᜆ());
						num = 52;
						continue;
					}
					case 39:
						goto IL_21E;
					case 40:
						num = 68;
						continue;
					case 41:
					{
						if (A_4 == ExcelFindOptions.None)
						{
							num = 53;
							continue;
						}
						string text2;
						flag5 = this.ᜀ(text2, A_2, A_4, A_7);
						num = 45;
						continue;
					}
					case 42:
						goto IL_845;
					case 43:
						num = 60;
						continue;
					case 44:
						goto IL_21E;
					case 45:
						goto IL_21E;
					case 46:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.String)
						{
							num = 49;
							continue;
						}
						goto IL_21E;
					}
					case 47:
						if (flag3)
						{
							num = 34;
							continue;
						}
						goto IL_845;
					case 48:
					{
						if (A_4 == ExcelFindOptions.None)
						{
							num = 0;
							continue;
						}
						spr\u21DF spr_u21DF;
						flag5 = this.ᜀ(spr_u21DF.ᜁ(), A_2, A_4, A_7);
						num = 30;
						continue;
					}
					case 49:
						num2 = num6;
						num = 57;
						continue;
					case 50:
						num = 14;
						continue;
					case 51:
						goto IL_21E;
					case 52:
					{
						if (A_4 == ExcelFindOptions.None)
						{
							num = 12;
							continue;
						}
						spr\u2170 spr_u;
						flag5 = this.ᜀ(spr_u.ᜁ(), A_2, A_4, A_7);
						num = 16;
						continue;
					}
					case 53:
					{
						string text2;
						flag5 = text2.ToLower().Contains(A_2.ToLower());
						num = 51;
						continue;
					}
					case 54:
						if (this.ᜇ > 0)
						{
							num = 43;
							continue;
						}
						return list;
					case 55:
					{
						TBIFFRecord tbiffrecord2;
						if (tbiffrecord2 != TBIFFRecord.Formula)
						{
							num = 61;
							continue;
						}
						num = 47;
						continue;
					}
					case 56:
						flag6 = false;
						goto IL_4AB;
					case 57:
						if (flag4)
						{
							num = 25;
							continue;
						}
						goto IL_21E;
					case 58:
						num = 54;
						continue;
					case 59:
					{
						int num4;
						int num5;
						long item = sprṔ.ᜀ(num5 + 1, num4 + 1);
						list.Add(item);
						num = 13;
						continue;
					}
					case 60:
						if (A_1 >= this.ᜅ)
						{
							num = 40;
							continue;
						}
						return list;
					case 61:
						num = 24;
						continue;
					case 62:
						if (!flag4)
						{
							num = 3;
							continue;
						}
						goto IL_761;
					case 63:
						num = 28;
						continue;
					case 64:
					{
						num2 = num6;
						num6 = this.ᜃ(num6);
						TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num6);
						num = 20;
						continue;
					}
					case 65:
						if (!flag3)
						{
							num = 27;
							continue;
						}
						goto IL_761;
					case 66:
					{
						TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num6);
						num = 35;
						continue;
					}
					case 67:
						if (!flag2)
						{
							num = 33;
							continue;
						}
						goto IL_761;
					case 68:
						if (A_0 > this.ᜆ)
						{
							num = 5;
							continue;
						}
						num2 = this.ᜀ(A_0, out flag5);
						num = 36;
						continue;
					}
					break;
					IL_21E:
					num = 19;
					continue;
					IL_22A:
					if (flag5)
					{
						num = 59;
						continue;
					}
					goto IL_726;
					IL_309:
					num = 9;
					continue;
					IL_4AB:
					flag5 = flag6;
					num = 22;
					continue;
					IL_509:
					num = 46;
					continue;
					IL_726:
					num2 = this.ᜃ(num2);
					num = 15;
					continue;
					IL_761:
					list = new List<long>();
					num = 32;
					continue;
					IL_845:
					num6 = num2 + num3 + 4;
					num = 10;
				}
			}
			return list;
			IL_32E:
			return list;
			IL_39A:
			return list;
			IL_724:
			throw new ArgumentException(RecordTableEnumerator.b("ᑃ❅㩇⭉⅋⭍⑏㝑♓癕㹗㙙㵛㥝፟䉡ൣᕥ䡧ѩͫᩭ偯ѱᕳ᩵ᅷṹ剻", a_), RecordTableEnumerator.b("≃⩅⥇ⵉ㽋", a_));
		}
		}
	}

	// Token: 0x0600307B RID: 12411 RVA: 0x001BAF00 File Offset: 0x001B9F00
	private bool ᜀ(string A_0, string A_1, ExcelFindOptions A_2, XlsWorkbook A_3)
	{
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_FA:
				num = 12;
				break;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
			StringComparison comparisonType;
			for (;;)
			{
				StringComparison stringComparison;
				bool flag;
				bool? flag3;
				bool flag4;
				switch (num)
				{
				case 0:
					if (A_0.IndexOf(A_1, 0, comparisonType) != 0)
					{
						num = 8;
						continue;
					}
					return true;
				case 2:
					stringComparison = StringComparison.CurrentCultureIgnoreCase;
					goto IL_BB;
				case 3:
					flag = false;
					goto IL_171;
				case 4:
					num = 14;
					continue;
				case 5:
					num = 2;
					continue;
				case 6:
					goto IL_FA;
				case 7:
				{
					bool? flag2;
					if (flag2.GetValueOrDefault())
					{
						num = 4;
						continue;
					}
					num = 3;
					continue;
				}
				case 8:
					return false;
				case 9:
					stringComparison = StringComparison.CurrentCulture;
					goto IL_BB;
				case 10:
					if (flag3 != null)
					{
						num = 6;
						continue;
					}
					goto IL_181;
				case 11:
				{
					bool? flag2 = A_3.IsStartsOrEndsWith;
					num = 7;
					continue;
				}
				case 12:
					if (!flag4)
					{
						num = 11;
						continue;
					}
					goto IL_181;
				case 13:
					goto IL_17F;
				case 14:
				{
					bool? flag2;
					flag = (flag2 != null);
					goto IL_171;
				}
				}
				if ((A_2 & ExcelFindOptions.MatchCase) == ExcelFindOptions.None)
				{
					num = 5;
					continue;
				}
				num = 9;
				continue;
				IL_BB:
				comparisonType = stringComparison;
				flag4 = ((A_2 & ExcelFindOptions.MatchEntireCellContent) != ExcelFindOptions.None);
				flag3 = A_3.IsStartsOrEndsWith;
				if (true)
				{
				}
				num = 10;
				continue;
				IL_171:
				if (!flag)
				{
					num = 13;
					continue;
				}
				break;
				IL_181:
				num = 0;
			}
			return A_0.StartsWith(A_1, comparisonType);
			IL_17F:
			return A_0.EndsWith(A_1, comparisonType);
		}
		}
	}

	// Token: 0x0600307C RID: 12412 RVA: 0x001BB0B4 File Offset: 0x001BA0B4
	public List<long> ᜀ(int A_0, int A_1, double A_2, FindType A_3, bool A_4, XlsWorkbook A_5)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			List<long> list;
			for (;;)
			{
				bool flag = (A_3 & FindType.FormulaValue) != (FindType)0;
				bool flag2 = (A_3 & FindType.Number) != (FindType)0;
				int num = 17;
				for (;;)
				{
					TBIFFRecord tbiffrecord2;
					int num3;
					int num5;
					int num6;
					switch (num)
					{
					case 0:
						num = 34;
						continue;
					case 1:
						num = 16;
						continue;
					case 2:
					{
						if (true)
						{
						}
						TBIFFRecord tbiffrecord;
						if (tbiffrecord != TBIFFRecord.Number)
						{
							num = 6;
							continue;
						}
						goto IL_221;
					}
					case 3:
						if (tbiffrecord2 != TBIFFRecord.Array)
						{
							num = 19;
							continue;
						}
						goto IL_3EA;
					case 4:
					{
						int num2;
						if (num2 <= A_1)
						{
							num = 5;
							continue;
						}
						return list;
					}
					case 5:
					{
						TBIFFRecord tbiffrecord = tbiffrecord2;
						num = 14;
						continue;
					}
					case 6:
						num = 22;
						continue;
					case 7:
						goto IL_30A;
					case 8:
					{
						spr᱒ spr᱒ = (spr᱒)spr\u175E.ᜀ(this.ᜈ, num3, this.ᜆ());
						bool flag3 = spr᱒.ᜌ() == A_2;
						num = 31;
						continue;
					}
					case 9:
						goto IL_246;
					case 10:
						if (this.ᜇ > 0)
						{
							num = 0;
							continue;
						}
						return list;
					case 11:
						if (flag)
						{
							num = 8;
							continue;
						}
						goto IL_1D9;
					case 12:
						goto IL_246;
					case 13:
					{
						spr\u2230 spr_u = (spr\u2230)spr\u175E.ᜀ(this.ᜈ, num3, this.ᜆ());
						bool flag3 = spr_u.ᜀ() == A_2;
						num = 12;
						continue;
					}
					case 14:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord != TBIFFRecord.Formula)
						{
							num = 20;
							continue;
						}
						num = 11;
						continue;
					}
					case 15:
						goto IL_4F6;
					case 16:
					{
						if (A_0 > this.ᜆ)
						{
							num = 7;
							continue;
						}
						this.ᜀ(false, A_5.ReservedHandle.\u171D());
						bool flag3;
						num3 = this.ᜀ(A_0, out flag3);
						goto IL_4C3;
					}
					case 17:
						if (!flag)
						{
							num = 32;
							continue;
						}
						goto IL_1AE;
					case 18:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4C3;
						default:
							if (false)
							{
							}
							if (!A_4)
							{
								num = 37;
								continue;
							}
							return list;
						}
						break;
					case 19:
						num = 29;
						continue;
					case 20:
						num = 2;
						continue;
					case 21:
						goto IL_266;
					case 22:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.RK)
						{
							num = 27;
							continue;
						}
						goto IL_246;
					}
					case 23:
						goto IL_28B;
					case 24:
					{
						int num2;
						int num4;
						long item = sprṔ.ᜀ(num2 + 1, num4 + 1);
						list.Add(item);
						num = 18;
						continue;
					}
					case 25:
						if (!flag2)
						{
							num = 30;
							continue;
						}
						goto IL_1AE;
					case 26:
						tbiffrecord2 = (TBIFFRecord)this.ᜈ.ReadInt16(num5);
						num = 15;
						continue;
					case 27:
						goto IL_221;
					case 28:
						if (num5 < this.ᜇ)
						{
							num = 26;
							continue;
						}
						goto IL_246;
					case 29:
						if (tbiffrecord2 != TBIFFRecord.String)
						{
							num = 9;
							continue;
						}
						goto IL_3EA;
					case 30:
						goto IL_2D7;
					case 31:
						goto IL_1D9;
					case 32:
						num = 25;
						continue;
					case 33:
					{
						if (num3 >= this.ᜇ)
						{
							num = 23;
							continue;
						}
						tbiffrecord2 = (TBIFFRecord)this.ᜈ.ReadInt16(num3);
						num6 = (int)this.ᜈ.ReadInt16(num3 + 2);
						int num4 = this.\u171A(num3);
						int num2 = this.ᜉ(num3);
						bool flag3 = false;
						num = 4;
						continue;
					}
					case 34:
						if (A_1 >= this.ᜅ)
						{
							num = 1;
							continue;
						}
						return list;
					case 35:
						goto IL_266;
					case 36:
						if (flag2)
						{
							num = 13;
							continue;
						}
						goto IL_246;
					case 37:
						goto IL_209;
					case 38:
						goto IL_4F6;
					case 39:
						if (this.ᜈ != null)
						{
							num = 40;
							continue;
						}
						return list;
					case 40:
						num = 10;
						continue;
					case 41:
					{
						bool flag3;
						if (flag3)
						{
							num = 24;
							continue;
						}
						goto IL_209;
					}
					}
					break;
					IL_1AE:
					list = new List<long>();
					num = 39;
					continue;
					IL_1D9:
					num5 = num3 + num6 + 4;
					num = 28;
					continue;
					IL_209:
					num3 = this.ᜃ(num3);
					num = 21;
					continue;
					IL_221:
					num = 36;
					continue;
					IL_246:
					num = 41;
					continue;
					IL_266:
					num = 33;
					continue;
					IL_3EA:
					num3 = num5;
					num5 = this.ᜃ(num5);
					tbiffrecord2 = (TBIFFRecord)this.ᜈ.ReadInt16(num5);
					num = 38;
					continue;
					IL_4C3:
					num = 35;
					continue;
					IL_4F6:
					num = 3;
				}
			}
			return list;
			IL_28B:
			return list;
			IL_2D7:
			throw new ArgumentException(RecordTableEnumerator.b("ᝆ⡈㥊ⱌ≎㑐❒ご╖祘㵚ㅜ㹞٠ၢ䕤๦ᩨ䭪ͬnհ卲ʹᙶᕸቺ᥼兾", a_), RecordTableEnumerator.b("ⅆ╈⩊⩌㱎", a_));
			IL_30A:
			return list;
		}
		}
	}

	// Token: 0x0600307D RID: 12413 RVA: 0x001BB604 File Offset: 0x001BA604
	public List<long> ᜀ(int A_0, int A_1, byte A_2, bool A_3, bool A_4, XlsWorkbook A_5)
	{
		switch (0)
		{
		default:
		{
			List<long> list;
			for (;;)
			{
				list = new List<long>();
				int num = 20;
				for (;;)
				{
					TBIFFRecord tbiffrecord2;
					bool flag;
					int num2;
					int num5;
					bool flag2;
					switch (num)
					{
					case 0:
					{
						TBIFFRecord tbiffrecord = tbiffrecord2;
						num = 14;
						continue;
					}
					case 1:
						goto IL_E6;
					case 2:
						goto IL_2A4;
					case 3:
						goto IL_387;
					case 4:
					{
						if (true)
						{
						}
						spr\u249B spr_u249B;
						flag = (spr_u249B.ᜄ() == A_2);
						goto IL_3F4;
					}
					case 5:
						if (num2 < this.ᜇ)
						{
							num = 10;
							continue;
						}
						goto IL_176;
					case 6:
						goto IL_2A4;
					case 7:
						num = 17;
						continue;
					case 8:
						if (tbiffrecord2 != TBIFFRecord.Array)
						{
							num = 3;
							continue;
						}
						goto IL_1BC;
					case 9:
						goto IL_E6;
					case 10:
						tbiffrecord2 = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
						num = 9;
						continue;
					case 11:
						goto IL_1B7;
					case 12:
					{
						int num3;
						int num4;
						long item = sprṔ.ᜀ(num3 + 1, num4 + 1);
						list.Add(item);
						num = 35;
						continue;
					}
					case 13:
						num = 25;
						continue;
					case 14:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord != TBIFFRecord.Formula)
						{
							num = 16;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2A4;
						default:
						{
							if (false)
							{
							}
							int num6;
							num2 = num5 + num6 + 4;
							num = 5;
							continue;
						}
						}
						break;
					}
					case 15:
						flag = false;
						goto IL_3F4;
					case 16:
						num = 24;
						continue;
					case 17:
						if (A_0 > this.ᜆ)
						{
							num = 11;
							continue;
						}
						num5 = this.ᜀ(A_0, out flag2);
						num = 2;
						continue;
					case 18:
						num = 4;
						continue;
					case 19:
						goto IL_176;
					case 20:
						if (this.ᜈ != null)
						{
							num = 27;
							continue;
						}
						return list;
					case 21:
						if (this.ᜇ > 0)
						{
							num = 13;
							continue;
						}
						return list;
					case 22:
						if (flag2)
						{
							num = 12;
							continue;
						}
						goto IL_1E8;
					case 23:
						if (num2 < this.ᜇ)
						{
							num = 28;
							continue;
						}
						goto IL_387;
					case 24:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.BoolErr)
						{
							num = 36;
							continue;
						}
						goto IL_176;
					}
					case 25:
						if (A_1 >= this.ᜅ)
						{
							num = 7;
							continue;
						}
						return list;
					case 26:
						goto IL_176;
					case 27:
						num = 21;
						continue;
					case 28:
						num = 8;
						continue;
					case 29:
					{
						spr\u249B spr_u249B;
						if (spr_u249B.ᜂ() == A_3)
						{
							num = 18;
							continue;
						}
						num = 15;
						continue;
					}
					case 30:
					{
						int num3;
						if (num3 <= A_1)
						{
							num = 0;
							continue;
						}
						return list;
					}
					case 31:
					{
						if (num5 >= this.ᜇ)
						{
							num = 34;
							continue;
						}
						tbiffrecord2 = (TBIFFRecord)this.ᜈ.ReadInt16(num5);
						int num6 = (int)this.ᜈ.ReadInt16(num5 + 2);
						int num4 = this.\u171A(num5);
						int num3 = this.ᜉ(num5);
						flag2 = false;
						num = 30;
						continue;
					}
					case 32:
						goto IL_1E8;
					case 33:
						if (tbiffrecord2 != TBIFFRecord.String)
						{
							num = 19;
							continue;
						}
						goto IL_1BC;
					case 34:
						goto IL_2C8;
					case 35:
						if (!A_4)
						{
							num = 32;
							continue;
						}
						return list;
					case 36:
					{
						spr\u249B spr_u249B = (spr\u249B)spr\u175E.ᜀ(this.ᜈ, num5, this.ᜆ());
						num = 29;
						continue;
					}
					}
					break;
					IL_E6:
					num = 23;
					continue;
					IL_176:
					num = 22;
					continue;
					IL_1BC:
					num5 = num2;
					num2 = this.ᜃ(num2);
					tbiffrecord2 = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num = 1;
					continue;
					IL_1E8:
					num5 = this.ᜃ(num5);
					num = 6;
					continue;
					IL_2A4:
					num = 31;
					continue;
					IL_387:
					num = 33;
					continue;
					IL_3F4:
					flag2 = flag;
					num = 26;
				}
			}
			IL_1B7:
			return list;
			IL_2C8:
			return list;
		}
		}
	}

	// Token: 0x0600307E RID: 12414 RVA: 0x001BBA60 File Offset: 0x001BAA60
	public void ᜀ(Dictionary<int, object> A_0, List<long> A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_F4;
				case 1:
					if (this.ᜇ <= 0)
					{
						num = 0;
						continue;
					}
					goto IL_1C4;
				case 2:
					goto IL_F9;
				case 3:
				{
					int key;
					if (A_0.ContainsKey(key))
					{
						num = 12;
						continue;
					}
					goto IL_F9;
				}
				case 5:
					num = 6;
					continue;
				case 6:
					if (A_0.Count != 0)
					{
						num = 7;
						continue;
					}
					return;
				case 7:
					num = 1;
					continue;
				case 8:
				{
					int key = spr\u1C7C.ᜂ(this.ᜈ, num2, this.ᜆ());
					num = 3;
					continue;
				}
				case 9:
					goto IL_19E;
				case 10:
					return;
				case 11:
					if (true)
					{
					}
					goto IL_19E;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C4;
					default:
					{
						if (false)
						{
						}
						int a_ = this.\u171A(num2) + 1;
						int a_2 = this.ᜉ(num2) + 1;
						A_1.Add(sprṔ.ᜀ(a_2, a_));
						num = 2;
						continue;
					}
					}
					break;
				case 13:
				{
					if (num2 >= this.ᜇ)
					{
						num = 10;
						continue;
					}
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num = 14;
					continue;
				}
				case 14:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord == TBIFFRecord.LabelSST)
					{
						num = 8;
						continue;
					}
					goto IL_F9;
				}
				}
				if (A_0 != null)
				{
					num = 5;
					continue;
				}
				break;
				IL_F9:
				num2 = this.ᜃ(num2);
				num = 11;
				continue;
				IL_19E:
				num = 13;
				continue;
				IL_1C4:
				num2 = 0;
				num = 9;
			}
			IL_F4:
			return;
		}
		}
	}

	// Token: 0x0600307F RID: 12415 RVA: 0x001BBC44 File Offset: 0x001BAC44
	public int \u1717(int A_0)
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				num = 10;
				continue;
			case 2:
				if (A_0 < this.ᜇ)
				{
					num = 9;
					continue;
				}
				return A_0;
			case 3:
			{
				TBIFFRecord tbiffrecord;
				if (tbiffrecord != TBIFFRecord.String)
				{
					num = 4;
					continue;
				}
				goto IL_FA;
			}
			case 4:
				return A_0;
			case 6:
				goto IL_FA;
			case 7:
			{
				TBIFFRecord tbiffrecord = TBIFFRecord.Unknown;
				num = 6;
				continue;
			}
			case 8:
				if (A_0 < this.ᜇ)
				{
					num = 1;
					continue;
				}
				return A_0;
			case 9:
			{
				TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(A_0);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
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
			{
				TBIFFRecord tbiffrecord;
				if (tbiffrecord != TBIFFRecord.Array)
				{
					num = 0;
					continue;
				}
				goto IL_FA;
			}
			}
			if (A_0 < this.ᜇ)
			{
				if (true)
				{
				}
				num = 7;
				continue;
			}
			break;
			IL_FA:
			int num2 = (int)this.ᜈ.ReadInt16(A_0 + 2);
			A_0 += 4 + num2;
			num = 2;
		}
		return A_0;
	}

	// Token: 0x06003080 RID: 12416 RVA: 0x001BBD88 File Offset: 0x001BAD88
	public void ᜀ(Dictionary<int, int> A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 10;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
				{
					if (num2 >= this.ᜇ)
					{
						num = 17;
						continue;
					}
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num = 3;
					continue;
				}
				case 1:
					goto IL_176;
				case 2:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.Array)
					{
						num = 12;
						continue;
					}
					goto IL_8A;
				}
				case 3:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.MulRK)
					{
						num = 16;
						continue;
					}
					goto IL_23F;
				}
				case 4:
				{
					int key = (int)this.ᜀ(num2, false);
					num = 13;
					continue;
				}
				case 5:
					return;
				case 6:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord == TBIFFRecord.MulBlank)
					{
						num = 8;
						continue;
					}
					num = 2;
					continue;
				}
				case 7:
				{
					int key2;
					int num3;
					if (A_0.TryGetValue(key2, out num3))
					{
						num = 11;
						continue;
					}
					goto IL_245;
				}
				case 8:
					goto IL_1C3;
				case 9:
					goto IL_1C5;
				case 11:
				{
					int num3;
					this.ᜀ((ushort)num3);
					num = 1;
					continue;
				}
				case 12:
					num = 19;
					continue;
				case 13:
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
						int key;
						int num3;
						if (A_0.TryGetValue(key, out num3))
						{
							num = 15;
							continue;
						}
						goto IL_8A;
					}
					}
					break;
				case 14:
					goto IL_1C5;
				case 15:
				{
					int num3;
					this.ᜀ(num2, (ushort)num3);
					num = 18;
					continue;
				}
				case 16:
					num = 6;
					continue;
				case 17:
				{
					int key2 = (int)this.ᜇ();
					num = 7;
					continue;
				}
				case 18:
					goto IL_8A;
				case 19:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.String)
					{
						num = 4;
						continue;
					}
					goto IL_8A;
				}
				}
				IL_70:
				if (this.ᜇ <= 0)
				{
					num = 5;
					continue;
				}
				num2 = 0;
				num = 14;
				continue;
				goto IL_70;
				IL_8A:
				num2 = this.ᜃ(num2);
				num = 9;
				continue;
				IL_1C5:
				num = 0;
			}
			return;
			IL_176:
			goto IL_245;
			IL_1C3:
			IL_23F:
			throw new NotImplementedException();
			IL_245:
			if (true)
			{
			}
			return;
		}
		}
	}

	// Token: 0x06003081 RID: 12417 RVA: 0x001BBFE4 File Offset: 0x001BAFE4
	public void ᜀ(int[] A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 11;
			for (;;)
			{
				int num2;
				TBIFFRecord tbiffrecord;
				switch (num)
				{
				case 0:
					goto IL_128;
				case 1:
					goto IL_FF;
				case 2:
					if (true)
					{
					}
					if (num2 >= this.ᜇ)
					{
						num = 0;
						continue;
					}
					tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num = 8;
					continue;
				case 3:
					goto IL_173;
				case 4:
					if (tbiffrecord != TBIFFRecord.String)
					{
						num = 5;
						continue;
					}
					goto IL_C3;
				case 5:
				{
					int num3 = (int)this.ᜀ(num2, false);
					num3 = A_0[num3];
					this.ᜀ(num2, (ushort)num3);
					num = 7;
					continue;
				}
				case 6:
					return;
				case 7:
					goto IL_C3;
				case 8:
					goto IL_18E;
				case 9:
					num = 12;
					continue;
				case 10:
					num = 4;
					continue;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_18E;
					default:
						if (false)
						{
						}
						if (tbiffrecord == TBIFFRecord.MulBlank)
						{
							num = 3;
							continue;
						}
						num = 13;
						continue;
					}
					break;
				case 13:
					if (tbiffrecord != TBIFFRecord.Array)
					{
						num = 10;
						continue;
					}
					goto IL_C3;
				case 14:
					goto IL_FF;
				}
				if (this.ᜇ <= 0)
				{
					num = 6;
					continue;
				}
				num2 = 0;
				num = 1;
				continue;
				IL_C3:
				num2 = this.ᜃ(num2);
				num = 14;
				continue;
				IL_FF:
				num = 2;
				continue;
				IL_18E:
				if (tbiffrecord == TBIFFRecord.MulRK)
				{
					goto IL_1A4;
				}
				num = 9;
			}
			return;
			IL_128:
			int num4 = (int)this.ᜇ();
			this.ᜀ((ushort)A_0[num4]);
			return;
			IL_173:
			IL_1A4:
			throw new NotImplementedException();
		}
		}
	}

	// Token: 0x06003082 RID: 12418 RVA: 0x001BC1AC File Offset: 0x001BB1AC
	public void ᜈ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int num2 = 9;
				for (;;)
				{
					TBIFFRecord tbiffrecord;
					int num4;
					switch (num2)
					{
					case 0:
						if (tbiffrecord == TBIFFRecord.String)
						{
							goto IL_50;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B5;
						default:
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					case 1:
					{
						ushort num3 = this.ᜀ(num, false);
						num2 = 6;
						continue;
					}
					case 2:
						goto IL_B5;
					case 3:
						if (num >= this.ᜇ)
						{
							num2 = 8;
							continue;
						}
						tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num);
						num4 = (int)this.ᜈ.ReadInt16(num + 2);
						if (true)
						{
						}
						num2 = 2;
						continue;
					case 4:
						this.ᜀ(num, (ushort)A_1);
						num2 = 7;
						continue;
					case 5:
						num2 = 0;
						continue;
					case 6:
					{
						ushort num3;
						if ((int)num3 >= A_0)
						{
							num2 = 4;
							continue;
						}
						goto IL_50;
					}
					case 7:
						goto IL_50;
					case 8:
						return;
					case 9:
						goto IL_CB;
					case 10:
						goto IL_CB;
					}
					break;
					IL_50:
					num += num4 + 4;
					num2 = 10;
					continue;
					IL_CB:
					num2 = 3;
					continue;
					IL_B5:
					if (tbiffrecord == TBIFFRecord.Array)
					{
						goto IL_50;
					}
					num2 = 5;
				}
			}
			return;
		}
	}

	// Token: 0x06003083 RID: 12419 RVA: 0x001BC310 File Offset: 0x001BB310
	public void ᜀ(Dictionary<int, int> A_0, spr\u202C A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				int num3;
				int num5;
				switch (num)
				{
				case 0:
				{
					int num2 = spr\u1C7C.ᜂ(this.ᜈ, num3, this.ᜆ());
					num = 10;
					continue;
				}
				case 1:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord == TBIFFRecord.LabelSST)
					{
						num = 0;
						continue;
					}
					goto IL_130;
				}
				case 2:
				{
					int num4;
					int num2 = num4;
					spr\u1C7C.ᜀ(this.ᜈ, num3, num2, this.ᜆ());
					A_1(num2);
					num = 11;
					continue;
				}
				case 3:
				{
					if (num3 >= this.ᜇ)
					{
						num = 6;
						continue;
					}
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num3);
					num5 = (int)this.ᜈ.ReadInt16(num3 + 2);
					num = 1;
					continue;
				}
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_125;
					}
					break;
				case 6:
					return;
				case 7:
					if (A_0.Count == 0)
					{
						num = 5;
						continue;
					}
					num3 = 0;
					num = 9;
					continue;
				case 8:
					goto IL_76;
				case 9:
					goto IL_193;
				case 10:
				{
					int num2;
					int num4;
					if (A_0.TryGetValue(num2, out num4))
					{
						num = 2;
						continue;
					}
					goto IL_130;
				}
				case 11:
					if (true)
					{
					}
					goto IL_130;
				case 12:
					goto IL_193;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				num = 7;
				continue;
				IL_130:
				num3 += num5 + 4;
				num = 12;
				continue;
				IL_193:
				num = 3;
			}
			IL_76:
			throw new ArgumentNullException(RecordTableEnumerator.b("刵儷夹䠻欽〿♁╃㉅ⵇ⹉Ջ⁍㑏㝑ⱓ㍕⭗", a_));
			IL_125:
			if (false)
			{
			}
			return;
		}
		}
	}

	// Token: 0x06003084 RID: 12420 RVA: 0x001BC4E8 File Offset: 0x001BB4E8
	public void ᜀ(short A_0, short A_1, BiffRecordRaw A_2, int A_3)
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
		this.ᜅ(this.ᜇ + 4 + (int)A_1, A_3);
		this.ᜈ.WriteInt16(this.ᜇ, A_0);
		this.ᜇ += 2;
		this.ᜈ.WriteInt16(this.ᜇ, A_1);
		this.ᜇ += 2;
		A_2.InfillInternalData(this.ᜈ, this.ᜇ, this.ᜆ());
		this.ᜇ += (int)A_1;
	}

	// Token: 0x06003085 RID: 12421 RVA: 0x001BC59C File Offset: 0x001BB59C
	[CLSCompliant(false)]
	public void ᜀ(short A_0, short A_1, byte[] A_2, int A_3)
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
		this.ᜅ(this.ᜇ + 4 + (int)A_1, A_3);
		this.ᜈ.WriteInt16(this.ᜇ, A_0);
		this.ᜇ += 2;
		this.ᜈ.WriteInt16(this.ᜇ, A_1);
		this.ᜇ += 2;
		this.ᜈ.WriteBytes(this.ᜇ, A_2, 0, (int)A_1);
		this.ᜇ += (int)A_1;
	}

	// Token: 0x06003086 RID: 12422 RVA: 0x001BC64C File Offset: 0x001BB64C
	public void ᜀ(int A_0, byte[] A_1, int A_2)
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
		this.ᜅ(this.ᜇ + A_0, A_2);
		this.ᜈ.WriteBytes(this.ᜇ, A_1, 0, A_0);
		this.ᜇ += A_0;
	}

	// Token: 0x06003087 RID: 12423 RVA: 0x001BC6B8 File Offset: 0x001BB6B8
	public void ᜀ(int A_0, int A_1, byte[] A_2, int A_3)
	{
		if (true)
		{
		}
		this.ᜅ(this.ᜇ + A_1, A_3);
		bool flag;
		int num = this.ᜀ(A_0, out flag);
		if (!flag)
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
				this.ᜈ.MoveMemory(num + A_1, num, this.ᜇ - num);
				this.ᜈ.WriteBytes(num, A_2, 0, A_1);
				this.ᜇ += A_1;
				return;
			}
		}
		this.ᜄ(num);
		this.ᜀ(A_0, A_1, A_2, A_3);
	}

	// Token: 0x06003088 RID: 12424 RVA: 0x001BC75C File Offset: 0x001BB75C
	[CLSCompliant(false)]
	public void ᜀ(BiffRecordRaw[] A_0, byte[] A_1, bool A_2, int A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = A_0.Length;
				int num2 = 0;
				int num3 = 0;
				int num4 = 7;
				for (;;)
				{
					switch (num4)
					{
					case 0:
						return;
					case 1:
					{
						int num5;
						if (num5 >= num)
						{
							num4 = 0;
							continue;
						}
						BiffRecordRaw biffRecordRaw = A_0[num5];
						this.ᜀ((short)biffRecordRaw.RecordCode, (short)biffRecordRaw.GetStoreSize(this.ᜆ()), biffRecordRaw, A_3);
						num5++;
						num4 = 3;
						continue;
					}
					case 2:
					{
						this.ᜅ(this.ᜇ + num2, A_3);
						int num5 = 0;
						num4 = 4;
						continue;
					}
					case 3:
						IL_82:
						goto IL_DE;
					case 4:
						goto IL_DE;
					case 5:
						if (true)
						{
						}
						goto IL_11C;
					case 6:
					{
						if (num3 >= num)
						{
							num4 = 2;
							continue;
						}
						BiffRecordRaw biffRecordRaw2 = A_0[num3];
						num2 += 4 + biffRecordRaw2.GetStoreSize(this.ᜆ());
						num3++;
						num4 = 5;
						continue;
					}
					case 7:
						goto IL_11C;
					}
					break;
					IL_DE:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_82;
					default:
						if (false)
						{
						}
						num4 = 1;
						continue;
					}
					IL_11C:
					num4 = 6;
				}
			}
			return;
		}
	}

	// Token: 0x06003089 RID: 12425 RVA: 0x001BC8A8 File Offset: 0x001BB8A8
	public void ᜀ(bool A_0, int A_1)
	{
		switch (0)
		{
		default:
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
				int num = 2;
				int num2;
				sprᲀ sprᲀ;
				sprᨾ sprᨾ;
				List<int> a_;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						return;
					case 3:
						if (num2 == 0)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						goto IL_B5;
					}
					if (!this.ᜏ())
					{
						num = 1;
					}
					else
					{
						sprᲀ = (sprᲀ)spr\u175E.ᜀ(TBIFFRecord.MulBlank);
						sprᨾ = (sprᨾ)spr\u175E.ᜀ(TBIFFRecord.MulRK);
						a_ = this.ᜀ(sprᲀ, sprᨾ, out num2);
						num = 3;
					}
				}
				return;
				IL_B5:
				this.ᜅ(this.ᜇ + num2, A_1);
				this.ᜀ(a_, num2, sprᲀ, sprᨾ, A_0);
				this.ᜈ(false);
				this.ᜆ(true);
				this.ᜊ = -1;
				this.ᜋ = -1;
				return;
			}
			}
			return;
		}
	}

	// Token: 0x0600308A RID: 12426 RVA: 0x001BC9A0 File Offset: 0x001BB9A0
	public void \u171B()
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					goto IL_67;
				case 2:
					if (this.ᜤ())
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5E;
						}
						goto Block_2;
					}
					IL_5E:
					num = 1;
					continue;
				}
				if (this.ᜇ <= 0)
				{
					break;
				}
				num = 0;
			}
			return;
			IL_67:
			return;
			Block_2:
			if (false)
			{
			}
			sprᱧ.ᜂ ᜂ = new sprᱧ.ᜂ();
			ᜂ.ᜁ = 0;
			sprᱧ.ᜀ a_ = new sprᱧ.ᜀ(this.ᜁ);
			sprᱧ.ᜀ a_2 = new sprᱧ.ᜀ(this.ᜀ);
			sprᱧ.ᜀ a_3 = new sprᱧ.ᜀ(this.ᜂ);
			this.ᜀ(a_, a_2, a_3, ᜂ);
			this.ᜇ = ᜂ.ᜁ;
			this.ᜆ(false);
			return;
		}
		}
	}

	// Token: 0x0600308B RID: 12427 RVA: 0x001BCA90 File Offset: 0x001BBA90
	public bool ᜀ(SSTDictionary A_0, ref Dictionary<long, spr\u1DE2> A_1)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num = 10;
			for (;;)
			{
				List<int> list;
				int num3;
				bool result;
				int count;
				int num5;
				int a_2;
				int a_3;
				switch (num)
				{
				case 0:
				{
					int num2;
					if (num2 < 0)
					{
						num = 13;
						continue;
					}
					num3 = list[num2];
					this.ᜄ(num3);
					num2--;
					num = 5;
					continue;
				}
				case 1:
					result = false;
					num = 33;
					continue;
				case 2:
					goto IL_341;
				case 3:
					goto IL_40C;
				case 4:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord <= TBIFFRecord.MulBlank)
					{
						num = 8;
						continue;
					}
					num = 28;
					continue;
				}
				case 5:
					goto IL_28B;
				case 6:
					if (true)
					{
					}
					goto IL_19A;
				case 7:
					num = 32;
					continue;
				case 8:
					num = 20;
					continue;
				case 9:
					goto IL_40C;
				case 11:
					goto IL_C6;
				case 12:
					if (count > 0)
					{
						num = 27;
						continue;
					}
					return result;
				case 13:
					return result;
				case 14:
					num = 9;
					continue;
				case 15:
					A_1 = new Dictionary<long, spr\u1DE2>();
					num = 25;
					continue;
				case 16:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.SharedFormula2)
					{
						num = 29;
						continue;
					}
					list.Add(num3);
					num = 30;
					continue;
				}
				case 17:
					goto IL_28B;
				case 18:
					num = 16;
					continue;
				case 19:
				{
					if (num3 < 0)
					{
						num = 1;
						continue;
					}
					int num4 = this.ᜈ.ReadInt32(num3);
					TBIFFRecord tbiffrecord2 = (TBIFFRecord)(num4 & 65535);
					num5 = num4 >> 16;
					TBIFFRecord tbiffrecord = tbiffrecord2;
					num = 4;
					continue;
				}
				case 20:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.Formula)
					{
						num = 7;
						continue;
					}
					a_2 = this.\u171A(num3);
					a_3 = this.ᜉ(num3);
					num = 24;
					continue;
				}
				case 21:
					goto IL_40C;
				case 22:
					goto IL_40C;
				case 23:
					goto IL_341;
				case 24:
					goto IL_40C;
				case 25:
					goto IL_CB;
				case 26:
					if (num3 >= this.ᜇ)
					{
						num = 6;
						continue;
					}
					num = 19;
					continue;
				case 27:
				{
					int num2 = count - 1;
					num = 17;
					continue;
				}
				case 28:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.LabelSST)
					{
						num = 18;
						continue;
					}
					int index = this.ᜈ.ReadInt32(num3 + 6 + 4);
					A_0.AddIncrease(index);
					num = 22;
					continue;
				}
				case 29:
					num = 3;
					continue;
				case 30:
					if (A_1 == null)
					{
						num = 15;
						continue;
					}
					goto IL_CB;
				case 31:
					goto IL_40C;
				case 32:
				{
					TBIFFRecord tbiffrecord;
					switch (tbiffrecord)
					{
					case TBIFFRecord.MulRK:
					case TBIFFRecord.MulBlank:
						this.ᜉ |= sprᱧ.StorageOptions.HasMultiRKBlank;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_C6;
						default:
							if (false)
							{
							}
							num = 31;
							continue;
						}
						break;
					default:
						num = 14;
						continue;
					}
					break;
				}
				case 33:
					goto IL_19A;
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				a_2 = -1;
				a_3 = -1;
				num3 = 0;
				list = new List<int>();
				result = true;
				num = 2;
				continue;
				IL_CB:
				spr\u1DE2 value = (spr\u1DE2)spr\u175E.ᜀ(this.ᜈ, num3, this.ᜆ());
				long key = sprṔ.ᜀ(a_3, a_2);
				A_1.Add(key, value);
				num = 21;
				continue;
				IL_19A:
				count = list.Count;
				num = 12;
				continue;
				IL_28B:
				num = 0;
				continue;
				IL_341:
				num = 26;
				continue;
				IL_40C:
				num3 += num5 + 4;
				num = 23;
			}
			IL_C6:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴽㌿㙁", a_));
		}
		}
	}

	// Token: 0x0600308C RID: 12428 RVA: 0x001BCEC4 File Offset: 0x001BBEC4
	[CLSCompliant(false)]
	internal void ᜀ(spr\u20BA A_0, bool A_1)
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
					goto IL_49;
				default:
					goto IL_67;
				}
				break;
			case 1:
				this.ᜅ = (int)A_0.ᜌ();
				this.ᜆ = (int)A_0.ᜀ();
				goto IL_49;
			}
			if (A_1)
			{
				if (true)
				{
				}
				num = 1;
				continue;
			}
			goto IL_79;
			IL_49:
			num = 0;
		}
		IL_67:
		if (false)
		{
		}
		IL_79:
		this.\u1712 = (spr\u20BA.OptionFlags)A_0.ᜊ();
		this.ᜑ = A_0.ᜏ();
		this.\u1713 = A_0.ᜑ();
	}

	// Token: 0x0600308D RID: 12429 RVA: 0x001BCF70 File Offset: 0x001BBF70
	[CLSCompliant(false)]
	internal spr\u20BA ᜀ(XlsWorkbook A_0)
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
		spr\u20BA spr_u20BA = (spr\u20BA)spr\u175E.ᜀ(TBIFFRecord.Row);
		spr_u20BA.ᜅ((ushort)Math.Max(0, this.ᜅ));
		spr_u20BA.ᜀ((ushort)Math.Max(0, this.ᜆ));
		spr_u20BA.ᜄ(this.ᜑ);
		spr_u20BA.ᜀ((int)this.\u1712);
		spr_u20BA.ᜃ(((int)this.\u1713 > A_0.MaxXFCount) ? ((ushort)A_0.DefaultXFIndex) : this.\u1713);
		return spr_u20BA;
	}

	// Token: 0x0600308E RID: 12430 RVA: 0x001BD024 File Offset: 0x001BC024
	public void ᜀ(sprᱧ A_0)
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
		this.ᜑ = A_0.ᜑ;
		this.\u1712 = A_0.\u1712;
		this.\u1713 = A_0.ᜇ();
	}

	// Token: 0x0600308F RID: 12431 RVA: 0x001BD084 File Offset: 0x001BC084
	public void ᜢ()
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
		this.\u1712 = spr\u20BA.OptionFlags.ShowOutlineGroups;
	}

	// Token: 0x06003090 RID: 12432 RVA: 0x001BD0CC File Offset: 0x001BC0CC
	public void ᜇ(int A_0, int A_1)
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
		this.ᜅ = ((this.ᜇ == 0 || this.ᜅ < 0) ? A_0 : (this.ᜅ = Math.Min(this.ᜅ, A_0)));
		this.ᜆ = Math.Max(A_1, this.ᜆ);
	}

	// Token: 0x06003091 RID: 12433 RVA: 0x001BD150 File Offset: 0x001BC150
	public void ᜀ(int A_0, int A_1, ExcelVersion A_2)
	{
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_CE;
			case 1:
				goto IL_60;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_60;
				}
				if (false)
				{
				}
				goto IL_BC;
			case 3:
				num = 5;
				continue;
			case 4:
				goto IL_FF;
			case 5:
				if (A_0 != 4)
				{
					num = 9;
					continue;
				}
				this.ᜁ();
				num = 2;
				continue;
			case 7:
				num = 4;
				continue;
			case 8:
				if (A_0 != 8)
				{
					if (true)
					{
					}
					num = 7;
					continue;
				}
				this.ᜇ(A_1);
				num = 1;
				continue;
			case 9:
				num = 8;
				continue;
			}
			if (this.\u1712() != A_0)
			{
				num = 3;
				continue;
			}
			break;
			IL_BC:
			this.ᜌ = A_2;
			num = 0;
			continue;
			IL_60:
			goto IL_BC;
		}
		IL_CE:
		return;
		IL_FF:
		throw new NotSupportedException();
	}

	// Token: 0x06003092 RID: 12434 RVA: 0x001BD260 File Offset: 0x001BC260
	public int \u171C(int A_0)
	{
		bool flag;
		bool a_2;
		int a_ = this.ᜀ(A_0, out flag, out a_2, true);
		if (flag)
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
				return (int)this.ᜀ(a_, a_2);
			}
		}
		return int.MinValue;
	}

	// Token: 0x06003093 RID: 12435 RVA: 0x001BD2BC File Offset: 0x001BC2BC
	public void ᜀ(SSTDictionary A_0)
	{
		if (true)
		{
		}
		switch (0)
		{
		default:
			for (;;)
			{
				IL_57:
				int num = 0;
				int num2 = 253;
				int num3 = 1;
				for (;;)
				{
					int num5;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						switch (num3)
						{
						case 0:
						{
							int index = spr\u1C7C.ᜂ(this.ᜈ, num, this.ᜆ());
							A_0.AddIncrease(index);
							num3 = 5;
							continue;
						}
						case 1:
							goto IL_E9;
						case 2:
							return;
						case 3:
							goto IL_E9;
						case 4:
						{
							int num4;
							if (num4 == num2)
							{
								num3 = 0;
								continue;
							}
							goto IL_74;
						}
						case 5:
							goto IL_74;
						case 6:
						{
							if (num >= this.ᜇ)
							{
								num3 = 2;
								continue;
							}
							int num4 = (int)this.ᜈ.ReadInt16(num);
							num5 = (int)this.ᜈ.ReadInt16(num + 2);
							num3 = 4;
							continue;
						}
						}
						goto IL_57;
						IL_E9:
						num3 = 6;
						continue;
					}
					IL_74:
					num += num5 + 4;
					num3 = 3;
				}
			}
			return;
		}
	}

	// Token: 0x06003094 RID: 12436 RVA: 0x001BD3D8 File Offset: 0x001BC3D8
	public void ᜀ(ExcelVersion A_0, int A_1)
	{
		int a_ = 0;
		int num = 5;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				goto IL_E9;
			case 2:
				goto IL_AE;
			case 3:
				goto IL_DC;
			case 4:
				goto IL_AE;
			case 6:
				switch (A_0)
				{
				case ExcelVersion.Version97to2003:
					this.ᜁ();
					goto IL_71;
				case ExcelVersion.Version2007:
				case ExcelVersion.Version2010:
					this.ᜇ(A_1);
					num = 2;
					continue;
				default:
					num = 0;
					continue;
				}
				break;
			case 7:
				num = 6;
				continue;
			}
			if (this.ᜆ() != A_0)
			{
				num = 7;
				continue;
			}
			break;
			IL_71:
			num = 4;
			continue;
			IL_AE:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_71;
			default:
				if (false)
				{
				}
				this.ᜌ = A_0;
				num = 3;
				break;
			}
		}
		IL_DC:
		return;
		IL_E9:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䀵崷䠹伻圽⼿ⱁ", a_));
	}

	// Token: 0x06003095 RID: 12437 RVA: 0x001BD4E8 File Offset: 0x001BC4E8
	private void ᜂ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_55:
				this.ᜅ = -1;
				this.ᜆ = -1;
				int num = 0;
				for (;;)
				{
					int num2 = 11;
					for (;;)
					{
						int num4;
						switch (num2)
						{
						case 0:
							num2 = 9;
							continue;
						case 1:
						{
							TBIFFRecord tbiffrecord;
							if (tbiffrecord != TBIFFRecord.MulRK)
							{
								num2 = 8;
								continue;
							}
							goto IL_70;
						}
						case 2:
						{
							TBIFFRecord tbiffrecord;
							if (tbiffrecord == TBIFFRecord.MulBlank)
							{
								num2 = 10;
								continue;
							}
							goto IL_175;
						}
						case 3:
						{
							if (num >= this.ᜇ)
							{
								num2 = 5;
								continue;
							}
							short num3 = this.ᜈ.ReadInt16(num);
							TBIFFRecord tbiffrecord = (TBIFFRecord)num3;
							num4 = (int)this.ᜈ.ReadInt16(num + 2);
							int a_ = this.ᜉ(num);
							num2 = 7;
							continue;
						}
						case 4:
							goto IL_E7;
						case 5:
							goto IL_108;
						case 6:
							goto IL_175;
						case 7:
						{
							TBIFFRecord tbiffrecord;
							if (tbiffrecord != TBIFFRecord.Array)
							{
								num2 = 0;
								continue;
							}
							goto IL_175;
						}
						case 8:
							num2 = 2;
							continue;
						case 9:
						{
							TBIFFRecord tbiffrecord;
							if (tbiffrecord != TBIFFRecord.String)
							{
								num2 = 12;
								continue;
							}
							goto IL_175;
						}
						case 10:
							goto IL_70;
						case 11:
							goto IL_E7;
						case 12:
						{
							int a_;
							this.ᜆ(a_);
							num2 = 1;
							continue;
						}
						}
						goto IL_55;
						IL_70:
						int a_2 = (int)this.ᜈ.ReadInt16(num + num4 + 4 - 2);
						this.ᜆ(a_2);
						num2 = 6;
						continue;
						IL_E7:
						num2 = 3;
						continue;
						IL_175:
						num += num4 + 4;
						num2 = 4;
					}
					IL_108:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_11E;
					}
				}
			}
			IL_11E:
			if (false)
			{
			}
			if (true)
			{
			}
			return;
		}
	}

	// Token: 0x06003096 RID: 12438 RVA: 0x001BD6BC File Offset: 0x001BC6BC
	public void ᜀ(sprᱧ.ᜃ A_0, object A_1)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					goto IL_36;
				case 1:
					goto IL_3E;
				case 2:
					goto IL_36;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3E;
					default:
						goto IL_9F;
					}
					break;
				}
				break;
				IL_36:
				num2 = 1;
				continue;
				IL_3E:
				if (num >= this.ᜇ)
				{
					num2 = 3;
				}
				else
				{
					TBIFFRecord a_ = (TBIFFRecord)this.ᜈ.ReadInt16(num);
					int num3 = (int)this.ᜈ.ReadInt16(num + 2);
					A_0(a_, num, A_1);
					num += num3 + 4;
					num2 = 2;
				}
			}
		}
		IL_9F:
		if (false)
		{
		}
	}

	// Token: 0x06003097 RID: 12439 RVA: 0x001BD770 File Offset: 0x001BC770
	public void ᜀ(TBIFFRecord A_0, int A_1, object A_2)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 == TBIFFRecord.Array)
				{
					num = 4;
					continue;
				}
				return;
			case 2:
				return;
			case 3:
				num = 0;
				continue;
			case 4:
				goto IL_60;
			}
			IL_24:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_24;
			default:
				if (false)
				{
				}
				if (A_0 != TBIFFRecord.Formula)
				{
					num = 3;
					continue;
				}
				break;
			}
			IL_60:
			spr᥌ spr᥌ = spr\u175E.ᜀ(this.ᜈ, A_1, this.ᜆ()) as spr᥌;
			FormulaUtil.ᜀ(spr᥌.ᜀ(), (bool[])A_2);
			num = 2;
		}
	}

	// Token: 0x06003098 RID: 12440 RVA: 0x001BD834 File Offset: 0x001BC834
	public void ᜁ(TBIFFRecord A_0, int A_1, object A_2)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 == TBIFFRecord.Array)
				{
					num = 2;
					continue;
				}
				return;
			case 2:
				goto IL_60;
			case 3:
				return;
			case 4:
				num = 0;
				continue;
			}
			IL_24:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_24;
			default:
				if (false)
				{
				}
				if (A_0 != TBIFFRecord.Formula)
				{
					if (true)
					{
					}
					num = 4;
					continue;
				}
				break;
			}
			IL_60:
			spr᥌ spr᥌ = spr\u175E.ᜀ(this.ᜈ, A_1, this.ᜆ()) as spr᥌;
			Ptg[] a_ = spr᥌.ᜀ();
			FormulaUtil.ᜀ(a_, (int[])A_2);
			num = 3;
		}
	}

	// Token: 0x06003099 RID: 12441 RVA: 0x001BD8FC File Offset: 0x001BC8FC
	internal void ᜁ(IntPtr A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return;
			case 2:
				this.ᜈ = spr\u17FF.ᜀ(A_0);
				num = 1;
				continue;
			}
			IL_1C:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_1C;
			default:
				if (false)
				{
				}
				if (this.ᜈ != null)
				{
					return;
				}
				num = 2;
				break;
			}
		}
	}

	// Token: 0x0600309A RID: 12442 RVA: 0x001BD97C File Offset: 0x001BC97C
	public void ᜀ(bool[] A_0)
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
		this.ᜀ(new sprᱧ.ᜃ(this.ᜀ), A_0);
	}

	// Token: 0x0600309B RID: 12443 RVA: 0x001BD9CC File Offset: 0x001BC9CC
	public void ᜀ(int[] A_0)
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
		this.ᜀ(new sprᱧ.ᜃ(this.ᜁ), A_0);
	}

	// Token: 0x0600309C RID: 12444 RVA: 0x001BDA1C File Offset: 0x001BCA1C
	public int ᜀ(TBIFFRecord A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			int num2;
			for (;;)
			{
				TBIFFRecord tbiffrecord;
				int num3;
				int num4;
				switch (num)
				{
				case 0:
					num = 25;
					continue;
				case 1:
					goto IL_21D;
				case 2:
					if (tbiffrecord != A_0)
					{
						num = 14;
						continue;
					}
					goto IL_21D;
				case 3:
					this.ᜊ = num2;
					this.ᜋ = num3;
					num = 31;
					continue;
				case 5:
					goto IL_149;
				case 6:
					if (tbiffrecord != TBIFFRecord.String)
					{
						num = 8;
						continue;
					}
					goto IL_149;
				case 7:
					num = 9;
					continue;
				case 8:
					num = 10;
					continue;
				case 9:
					num4 = this.ᜉ(num3);
					goto IL_2D6;
				case 10:
					if (tbiffrecord != TBIFFRecord.Array)
					{
						num = 16;
						continue;
					}
					goto IL_149;
				case 11:
					goto IL_293;
				case 12:
					goto IL_2BC;
				case 13:
					num = 22;
					continue;
				case 14:
					num3 = this.ᜃ(num3);
					num = 27;
					continue;
				case 15:
					goto IL_21D;
				case 16:
					num2 = this.ᜉ(num3);
					num = 5;
					continue;
				case 17:
					num = 32;
					continue;
				case 18:
					goto IL_1DF;
				case 19:
					goto IL_149;
				case 20:
					num2 = A_2 + 1;
					num = 1;
					continue;
				case 21:
					num2 = A_2 + 1;
					num = 18;
					continue;
				case 22:
				{
					if (A_2 > this.ᜆ)
					{
						num = 11;
						continue;
					}
					bool flag;
					num3 = this.ᜀ(A_1, out flag);
					num = 30;
					continue;
				}
				case 23:
					if (num3 >= this.ᜇ)
					{
						num = 21;
						continue;
					}
					goto IL_1DF;
				case 24:
					if (num2 <= A_2)
					{
						num = 17;
						continue;
					}
					return num2;
				case 25:
					if (num3 >= this.ᜇ)
					{
						num = 12;
						continue;
					}
					goto IL_1FD;
				case 26:
					num4 = A_1;
					goto IL_2D6;
				case 27:
					if (num3 >= this.ᜇ)
					{
						num = 20;
						continue;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B3;
					}
					if (false)
					{
					}
					tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num3);
					num = 6;
					continue;
				case 28:
					if (num2 > A_2)
					{
						num = 15;
						continue;
					}
					num = 2;
					continue;
				case 29:
				{
					bool flag;
					if (!flag)
					{
						num = 7;
						continue;
					}
					num = 26;
					continue;
				}
				case 30:
				{
					bool flag;
					if (!flag)
					{
						num = 0;
						continue;
					}
					goto IL_1FD;
				}
				case 31:
					goto IL_25B;
				case 32:
					if (num3 < this.ᜇ)
					{
						num = 3;
						continue;
					}
					return num2;
				}
				goto IL_A7;
				IL_B3:
				num = 13;
				continue;
				IL_A7:
				if (A_1 <= this.ᜆ)
				{
					goto IL_B3;
				}
				goto IL_340;
				IL_149:
				num = 28;
				continue;
				IL_1DF:
				tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num3);
				num = 19;
				continue;
				IL_1FD:
				num = 29;
				continue;
				IL_21D:
				num = 24;
				continue;
				IL_2D6:
				num2 = num4;
				num = 23;
			}
			IL_25B:
			return num2;
			IL_293:
			goto IL_340;
			IL_2BC:
			return A_2 + 1;
			IL_340:
			return A_2 + 1;
		}
		}
	}

	// Token: 0x0600309D RID: 12445 RVA: 0x001BDD9C File Offset: 0x001BCD9C
	public int ᜉ(int A_0, int A_1)
	{
		int num;
		for (;;)
		{
			bool flag;
			num = this.ᜀ(A_0, out flag);
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (true)
					{
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
						if (num >= this.ᜈ())
						{
							num2 = 1;
							continue;
						}
						num2 = 2;
						continue;
					}
					break;
				case 1:
					num2 = 3;
					continue;
				case 2:
					goto IL_73;
				case 3:
					goto IL_87;
				}
				break;
			}
		}
		IL_73:
		return this.ᜉ(num);
		IL_87:
		return A_1 + 1;
	}

	// Token: 0x0600309E RID: 12446 RVA: 0x001BDE38 File Offset: 0x001BCE38
	internal void ᜀ(Dictionary<int, object> A_0)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					IL_177:
					num = 7;
					continue;
				case 1:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.Formula)
					{
						num = 0;
						continue;
					}
					goto IL_71;
				}
				case 3:
					goto IL_FE;
				case 4:
				{
					if (num2 >= this.ᜇ)
					{
						num = 8;
						continue;
					}
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num = 1;
					continue;
				}
				case 5:
					goto IL_135;
				case 6:
					goto IL_71;
				case 7:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord == TBIFFRecord.Array)
					{
						num = 6;
						continue;
					}
					goto IL_135;
				}
				case 8:
					return;
				case 9:
					goto IL_FE;
				case 10:
					goto IL_6C;
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				num2 = 0;
				num = 9;
				continue;
				IL_71:
				spr᥌ spr᥌ = (spr᥌)spr\u175E.ᜀ(this.ᜈ, num2, this.ᜆ());
				Ptg[] a_2 = spr᥌.ᜀ();
				this.ᜀ(A_0, a_2);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_177;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				IL_FE:
				num = 4;
				continue;
				IL_135:
				num2 = this.ᜃ(num2);
				num = 3;
			}
			IL_6C:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄㑆㱈❊㥌", a_));
		}
		}
	}

	// Token: 0x0600309F RID: 12447 RVA: 0x001BDFC4 File Offset: 0x001BCFC4
	private void ᜀ(Dictionary<int, object> A_0, Ptg[] A_1)
	{
		int a_ = 14;
		int num = 10;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
			{
				spr\u25A0 spr_u25A;
				A_0[(int)(spr_u25A.ᜀ() - 1)] = null;
				num = 3;
				continue;
			}
			case 1:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num2 = A_1.Length - 1;
				num = 9;
				continue;
			case 2:
				goto IL_DB;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					goto IL_10D;
				}
				break;
			case 4:
				return;
			case 5:
			{
				spr\u25A0 spr_u25A;
				if (spr_u25A != null)
				{
					num = 0;
					continue;
				}
				goto IL_10D;
			}
			case 6:
				goto IL_50;
			case 7:
				goto IL_DD;
			case 8:
			{
				if (num2 < 0)
				{
					num = 4;
					continue;
				}
				Ptg ptg = A_1[num2];
				spr\u25A0 spr_u25A = ptg as spr\u25A0;
				num = 5;
				continue;
			}
			case 9:
				goto IL_DD;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 1;
			continue;
			IL_DD:
			num = 8;
			continue;
			IL_10D:
			num2--;
			num = 7;
		}
		IL_50:
		throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅㭇㽉⁋㩍", a_));
		IL_DB:;
	}

	// Token: 0x060030A0 RID: 12448 RVA: 0x001BE104 File Offset: 0x001BD104
	private void ᜀ(int A_0, BiffRecordRaw[] A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				for (;;)
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
						int num = 0;
						int num2 = A_1.Length;
						int num3 = 2;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_66;
							case 1:
								return;
							case 2:
								if (true)
								{
								}
								goto IL_66;
							case 3:
							{
								if (num >= num2)
								{
									num3 = 1;
									continue;
								}
								BiffRecordRaw biffRecordRaw = A_1[num];
								this.ᜈ.WriteInt16(A_0, (short)biffRecordRaw.RecordCode);
								A_0 += 2;
								int storeSize = biffRecordRaw.GetStoreSize(this.ᜆ());
								this.ᜈ.WriteInt16(A_0, (short)storeSize);
								A_0 += 2;
								biffRecordRaw.InfillInternalData(this.ᜈ, A_0, this.ᜆ());
								A_0 += storeSize;
								num++;
								num3 = 0;
								continue;
							}
							}
							break;
							IL_66:
							num3 = 3;
						}
						break;
					}
					}
				}
			}
			return;
		}
	}

	// Token: 0x060030A1 RID: 12449 RVA: 0x001BE1F8 File Offset: 0x001BD1F8
	private void ᜁ()
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			int num6;
			for (;;)
			{
				int num2;
				int num3;
				int num5;
				int num9;
				switch (num)
				{
				case 0:
				{
					if (num2 >= this.ᜇ)
					{
						num = 9;
						continue;
					}
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num2 += 2;
					num3 = (int)this.ᜈ.ReadInt16(num2);
					num2 += 2;
					num = 1;
					continue;
				}
				case 1:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.Array)
					{
						num = 8;
						continue;
					}
					goto IL_A5;
				}
				case 3:
					goto IL_2E7;
				case 4:
				{
					int num4;
					if (num4 <= num5)
					{
						num = 20;
						continue;
					}
					goto IL_40A;
				}
				case 5:
					goto IL_1B7;
				case 6:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.Formula)
					{
						num = 7;
						continue;
					}
					spr᱒ spr᱒ = (spr᱒)spr\u175E.ᜀ(this.ᜈ, num2 - 4, ExcelVersion.Version2007);
					int storeSize = spr᱒.GetStoreSize(ExcelVersion.Version97to2003);
					this.ᜈ.WriteInt16(num6, (short)storeSize);
					num6 += 2;
					spr᱒.ᜀ(spr᱒.ᜑ(), true);
					spr᱒.InfillInternalData(this.ᜈ, num6, ExcelVersion.Version97to2003);
					num6 += storeSize;
					num = 3;
					continue;
				}
				case 7:
				{
					int num7 = num3 - 4;
					this.ᜈ.WriteInt16(num6, (short)num7);
					num6 += 2;
					int num4;
					this.ᜈ.WriteUInt16(num6, (ushort)num4);
					num6 += 2;
					int num8;
					this.ᜈ.WriteInt16(num6, (short)num8);
					num6 += 2;
					this.ᜈ.CopyTo(num2 + 8, this.ᜈ, num6, num7);
					num6 += num7 - 4;
					num = 22;
					continue;
				}
				case 8:
					num = 21;
					continue;
				case 9:
					goto IL_273;
				case 10:
					goto IL_2E7;
				case 11:
					goto IL_22B;
				case 12:
				{
					int num8;
					if (num8 <= num9)
					{
						num = 19;
						continue;
					}
					goto IL_40A;
				}
				case 13:
					return;
				case 14:
					goto IL_2E7;
				case 15:
				{
					BiffRecordRaw biffRecordRaw = spr\u175E.ᜀ(this.ᜈ, num2 - 4, ExcelVersion.Version2007);
					int storeSize2 = biffRecordRaw.GetStoreSize(ExcelVersion.Version97to2003);
					TBIFFRecord tbiffrecord;
					this.ᜈ.WriteInt16(num6, (short)tbiffrecord);
					num6 += 2;
					this.ᜈ.WriteInt16(num6, (short)storeSize2);
					num6 += 2;
					biffRecordRaw.InfillInternalData(this.ᜈ, num6, ExcelVersion.Version97to2003);
					num6 += storeSize2;
					num = 14;
					continue;
				}
				case 16:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord == TBIFFRecord.Array)
					{
						num = 15;
						continue;
					}
					this.ᜈ.CopyTo(num2 - 4, this.ᜈ, num6, num3 + 4);
					num6 += num3 + 4;
					num = 10;
					continue;
				}
				case 17:
					if (this.ᜅ > num9)
					{
						num = 5;
						continue;
					}
					goto IL_22B;
				case 18:
				{
					int num4 = this.ᜈ.ReadInt32(num2);
					int num8 = this.ᜈ.ReadInt32(num2 + 4);
					num = 12;
					continue;
				}
				case 19:
					num = 4;
					continue;
				case 20:
				{
					int num8;
					this.ᜆ = num8;
					TBIFFRecord tbiffrecord;
					this.ᜈ.WriteInt16(num6, (short)tbiffrecord);
					num6 += 2;
					num = 6;
					continue;
				}
				case 21:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.String)
					{
						num = 18;
						continue;
					}
					goto IL_A5;
				}
				case 22:
					goto IL_2E7;
				}
				if (this.\u1712() == 4 | this.ᜇ == 0)
				{
					num = 13;
					continue;
				}
				num2 = 0;
				num6 = 0;
				num5 = 65535;
				num9 = 255;
				num = 17;
				continue;
				IL_A5:
				num = 16;
				continue;
				IL_22B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_13C;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 0;
					continue;
				}
				IL_2E7:
				num2 += num3;
				num = 11;
			}
			return;
			IL_13C:
			this.ᜅ = 0;
			this.ᜆ = 0;
			this.ᜇ = num6;
			return;
			IL_1B7:
			goto IL_13C;
			IL_273:
			IL_40A:
			this.ᜇ = num6;
			return;
		}
		}
	}

	// Token: 0x060030A2 RID: 12450 RVA: 0x001BE618 File Offset: 0x001BD618
	private void ᜇ(int A_0)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 7;
			DataProvider dataProvider;
			int num3;
			for (;;)
			{
				int num2;
				int num4;
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.String)
					{
						num = 4;
						continue;
					}
					goto IL_C9;
				}
				case 2:
					goto IL_25C;
				case 3:
					goto IL_25C;
				case 4:
				{
					int a_2 = (int)this.ᜈ.ReadUInt16(num2);
					int a_3 = (int)this.ᜈ.ReadUInt16(num2 + 2);
					TBIFFRecord tbiffrecord;
					dataProvider.WriteInt16(num3, (short)tbiffrecord);
					num3 += 2;
					num = 15;
					continue;
				}
				case 5:
				{
					BiffRecordRaw biffRecordRaw = spr\u175E.ᜀ(this.ᜈ, num2 - 4, ExcelVersion.Version97to2003);
					int storeSize = biffRecordRaw.GetStoreSize(ExcelVersion.Version2007);
					TBIFFRecord tbiffrecord;
					dataProvider.WriteInt16(num3, (short)tbiffrecord);
					num3 += 2;
					dataProvider.WriteInt16(num3, (short)storeSize);
					num3 += 2;
					biffRecordRaw.InfillInternalData(dataProvider, num3, ExcelVersion.Version2007);
					num3 += storeSize;
					num = 3;
					continue;
				}
				case 6:
				{
					TBIFFRecord tbiffrecord;
					int a_2;
					int a_3;
					this.ᜀ(dataProvider, ref num3, num2, tbiffrecord, num4, a_2, a_3);
					num = 2;
					continue;
				}
				case 8:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.Array)
					{
						num = 0;
						continue;
					}
					goto IL_C9;
				}
				case 9:
					goto IL_25C;
				case 10:
					goto IL_2E1;
				case 11:
				{
					if (num2 >= this.ᜇ)
					{
						num = 16;
						continue;
					}
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num2 += 2;
					num4 = (int)this.ᜈ.ReadInt16(num2);
					num2 += 2;
					num = 8;
					continue;
				}
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_159;
					default:
						if (false)
						{
						}
						num = 19;
						continue;
					}
					break;
				case 13:
					goto IL_135;
				case 14:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord == TBIFFRecord.Array)
					{
						num = 5;
						continue;
					}
					int num5 = num4 + 4;
					this.ᜈ.CopyTo(num2 - 4, dataProvider, num3, num5);
					num3 += num5;
					num = 9;
					continue;
				}
				case 15:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.Formula)
					{
						num = 6;
						continue;
					}
					List<spr᱒> list;
					int num6;
					spr᱒ spr᱒ = list[num6];
					spr᱒.ᜀ(spr᱒.ᜑ(), false);
					num6++;
					this.ᜀ(dataProvider, ref num3, spr᱒);
					num = 21;
					continue;
				}
				case 16:
					goto IL_159;
				case 17:
					goto IL_257;
				case 18:
					if (true)
					{
					}
					goto IL_135;
				case 19:
				{
					if (this.ᜇ == 0)
					{
						num = 10;
						continue;
					}
					num2 = 0;
					num3 = 0;
					dataProvider = spr\u17FF.ᜀ(this.ᜃ());
					List<spr᱒> list = new List<spr᱒>();
					int num7 = this.ᜀ(list);
					dataProvider.EnsureCapacity((num7 / A_0 + 1) * A_0);
					int num6 = 0;
					num = 18;
					continue;
				}
				case 20:
				{
					int num7;
					if (num7 != num3)
					{
						num = 17;
						continue;
					}
					goto IL_36D;
				}
				case 21:
					goto IL_25C;
				}
				if (this.\u1712() != 8)
				{
					num = 12;
					continue;
				}
				break;
				IL_C9:
				num = 14;
				continue;
				IL_135:
				num = 11;
				continue;
				IL_159:
				num = 20;
				continue;
				IL_25C:
				num2 += num4;
				num = 13;
			}
			return;
			IL_257:
			throw new InvalidOperationException(RecordTableEnumerator.b("愵䨷唹刻夽怿ⵁ≃⁅㭇⽉㡋", a_));
			IL_2E1:
			return;
			IL_36D:
			this.ᜇ = num3;
			this.ᜊ = -1;
			this.ᜋ = -1;
			this.ᜈ.Dispose();
			this.ᜈ = dataProvider;
			return;
		}
		}
	}

	// Token: 0x060030A3 RID: 12451 RVA: 0x001BE9BC File Offset: 0x001BD9BC
	private void ᜀ(DataProvider A_0, ref int A_1, int A_2, TBIFFRecord A_3, int A_4, int A_5, int A_6)
	{
		int num = 5;
		for (;;)
		{
			bool flag;
			bool flag2;
			int num2;
			switch (num)
			{
			case 0:
				return;
			case 1:
			{
				int value = (int)this.ᜈ.ReadInt16(A_2 + A_4 - 2);
				A_0.WriteInt32(A_1 - 2, value);
				A_1 += 2;
				num = 0;
				continue;
			}
			case 2:
				goto IL_4B;
			case 3:
				flag = (A_3 == TBIFFRecord.MulBlank);
				goto IL_69;
			case 4:
				flag = true;
				goto IL_69;
			case 6:
				goto IL_F1;
			case 7:
				if (true)
				{
				}
				if (flag2)
				{
					num = 1;
					continue;
				}
				return;
			case 8:
				num2 += 2;
				num = 6;
				continue;
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4B;
				default:
					if (false)
					{
					}
					if (flag2)
					{
						num = 8;
						continue;
					}
					goto IL_F1;
				}
				break;
			}
			if (A_3 != TBIFFRecord.MulRK)
			{
				num = 2;
				continue;
			}
			num = 4;
			continue;
			IL_4B:
			num = 3;
			continue;
			IL_69:
			flag2 = flag;
			num2 = A_4 + 4;
			num = 9;
			continue;
			IL_F1:
			A_0.EnsureCapacity(A_1 + num2 + 2);
			A_0.WriteInt16(A_1, (short)num2);
			A_1 += 2;
			A_0.WriteInt32(A_1, A_5);
			A_1 += 4;
			A_0.WriteInt32(A_1, A_6);
			A_1 += 4;
			this.ᜈ.CopyTo(A_2 + 4, A_0, A_1, A_4);
			A_1 += A_4 - 4;
			num = 7;
		}
	}

	// Token: 0x060030A4 RID: 12452 RVA: 0x001BEB38 File Offset: 0x001BDB38
	private void ᜀ(DataProvider A_0, ref int A_1, spr᱒ A_2)
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
		int storeSize = A_2.GetStoreSize(ExcelVersion.Version2007);
		A_0.WriteInt16(A_1, (short)storeSize);
		A_1 += 2;
		A_0.EnsureCapacity(A_1 + storeSize);
		A_2.InfillInternalData(A_0, A_1, ExcelVersion.Version2007);
		A_1 += storeSize;
	}

	// Token: 0x060030A5 RID: 12453 RVA: 0x001BEBA8 File Offset: 0x001BDBA8
	private int ᜀ(List<spr᱒> A_0)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				int num2;
				int num3;
				int num4;
				switch (num)
				{
				case 0:
					num = 18;
					continue;
				case 1:
					goto IL_16D;
				case 2:
					goto IL_94;
				case 4:
					return num2;
				case 5:
					if (true)
					{
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
						num = 12;
						continue;
					}
					break;
				case 6:
				{
					if (num3 >= this.ᜇ)
					{
						num = 4;
						continue;
					}
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num3);
					num4 = (int)this.ᜈ.ReadInt16(num3 + 2);
					TBIFFRecord tbiffrecord2 = tbiffrecord;
					num = 22;
					continue;
				}
				case 7:
					num = 15;
					continue;
				case 8:
					goto IL_22D;
				case 9:
				{
					TBIFFRecord tbiffrecord2;
					if (tbiffrecord2 != TBIFFRecord.Formula)
					{
						num = 17;
						continue;
					}
					spr᱒ spr᱒ = (spr᱒)spr\u175E.ᜀ(this.ᜈ, num3, this.ᜆ());
					A_0.Add(spr᱒);
					num2 += spr᱒.GetStoreSize(ExcelVersion.Version2007) + 4;
					num = 11;
					continue;
				}
				case 10:
					num = 9;
					continue;
				case 11:
					goto IL_22D;
				case 12:
				{
					TBIFFRecord tbiffrecord2;
					if (tbiffrecord2 != TBIFFRecord.Array)
					{
						num = 7;
						continue;
					}
					spr\u225F spr_u225F = (spr\u225F)spr\u175E.ᜀ(this.ᜈ, num3, this.ᜆ());
					num2 += spr_u225F.GetStoreSize(ExcelVersion.Version2007) + 4;
					num = 8;
					continue;
				}
				case 13:
				{
					TBIFFRecord tbiffrecord2;
					switch (tbiffrecord2)
					{
					case TBIFFRecord.MulRK:
					case TBIFFRecord.MulBlank:
						num2 += num4 + 4 + 6;
						num = 20;
						continue;
					default:
						num = 0;
						continue;
					}
					break;
				}
				case 14:
					goto IL_22D;
				case 15:
					goto IL_108;
				case 16:
					goto IL_22D;
				case 17:
					num = 13;
					continue;
				case 18:
					goto IL_108;
				case 19:
				{
					TBIFFRecord tbiffrecord2;
					if (tbiffrecord2 != TBIFFRecord.String)
					{
						num = 5;
						continue;
					}
					num2 += num4 + 4;
					num = 16;
					continue;
				}
				case 20:
					goto IL_22D;
				case 21:
					goto IL_16D;
				case 22:
				{
					TBIFFRecord tbiffrecord2;
					if (tbiffrecord2 <= TBIFFRecord.MulBlank)
					{
						num = 10;
						continue;
					}
					num = 19;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num3 = 0;
				num2 = 0;
				num = 1;
				continue;
				IL_108:
				num2 += num4 + 4 + 4;
				num = 14;
				continue;
				IL_16D:
				num = 6;
				continue;
				IL_22D:
				num3 += num4 + 4;
				num = 21;
			}
			IL_94:
			throw new ArgumentNullException(RecordTableEnumerator.b("ℿぁ㙃E❇㡉⅋㭍㱏㍑❓", a_));
		}
		}
	}

	// Token: 0x060030A6 RID: 12454 RVA: 0x001BEEAC File Offset: 0x001BDEAC
	private int ᜀ()
	{
		int num2;
		for (;;)
		{
			int num = 0;
			num2 = 0;
			int num3 = 1;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_26;
				case 1:
					goto IL_26;
				case 2:
				{
					if (true)
					{
					}
					if (num >= this.ᜇ)
					{
						num3 = 3;
						continue;
					}
					int num4 = (int)this.ᜈ.ReadInt16(num + 2);
					num += num4 + 4;
					num2++;
					num3 = 0;
					continue;
				}
				case 3:
					goto IL_47;
				}
				break;
				IL_26:
				num3 = 2;
			}
		}
		IL_47:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return num2;
		}
		if (false)
		{
		}
		return num2;
	}

	// Token: 0x060030A7 RID: 12455 RVA: 0x001BEF50 File Offset: 0x001BDF50
	private void ᜀ(SSTDictionary A_0, SSTDictionary A_1, Dictionary<int, int> A_2, Dictionary<string, string> A_3, Dictionary<int, int> A_4, Dictionary<int, int> A_5, Dictionary<int, int> A_6)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 13;
			for (;;)
			{
				TBIFFRecord tbiffrecord;
				int num2;
				int num3;
				TBIFFRecord tbiffrecord2;
				switch (num)
				{
				case 0:
					if (tbiffrecord != TBIFFRecord.Formula)
					{
						num = 35;
						continue;
					}
					this.ᜀ(A_0, A_1, num2, A_3, A_4, num3, A_6);
					num = 21;
					continue;
				case 1:
					num = 26;
					continue;
				case 2:
					goto IL_426;
				case 3:
					goto IL_341;
				case 4:
					num = 15;
					continue;
				case 5:
					goto IL_421;
				case 6:
					this.ᜀ(A_2, num2, num3, false);
					num = 29;
					continue;
				case 7:
					if (tbiffrecord2 != TBIFFRecord.String)
					{
						if (true)
						{
						}
						num = 16;
						continue;
					}
					goto IL_341;
				case 8:
					goto IL_4BC;
				case 9:
					if (tbiffrecord2 != TBIFFRecord.Array)
					{
						num = 30;
						continue;
					}
					goto IL_341;
				case 10:
					this.ᜀ(A_2, num2, num3, true);
					goto IL_48B;
				case 11:
					goto IL_341;
				case 12:
				{
					int num4 = A_2[num4];
					this.ᜀ(num2, (ushort)num4);
					num = 38;
					continue;
				}
				case 14:
				{
					if (tbiffrecord2 == TBIFFRecord.MulBlank)
					{
						num = 6;
						continue;
					}
					int num4 = (int)this.ᜀ(num2, false);
					num = 31;
					continue;
				}
				case 15:
				{
					int key;
					XlsWorkbook workbook;
					this.ᜀ(this.\u1719() ? ((ushort)A_2[key]) : ((ushort)workbook.DefaultXFIndex));
					num = 8;
					continue;
				}
				case 16:
					num = 9;
					continue;
				case 17:
					if (tbiffrecord == TBIFFRecord.LabelSST)
					{
						num = 40;
						continue;
					}
					goto IL_341;
				case 18:
					if (tbiffrecord2 == TBIFFRecord.LabelSST)
					{
						num = 25;
						continue;
					}
					goto IL_341;
				case 19:
					goto IL_E6;
				case 20:
					goto IL_341;
				case 21:
					goto IL_341;
				case 22:
					num = 28;
					continue;
				case 23:
					if (A_2 != null)
					{
						num = 22;
						continue;
					}
					goto IL_426;
				case 24:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_48B;
					default:
						if (false)
						{
						}
						this.ᜀ(A_0, A_1, num2, A_3, A_4, num3, A_6);
						num = 20;
						continue;
					}
					break;
				case 25:
					this.ᜀ(A_0, A_1, num2, true, A_5);
					num = 3;
					continue;
				case 26:
					if (tbiffrecord2 == TBIFFRecord.MulRK)
					{
						num = 10;
						continue;
					}
					num = 14;
					continue;
				case 27:
					return;
				case 28:
					if (A_2.Count > 0)
					{
						num = 1;
						continue;
					}
					goto IL_426;
				case 29:
					goto IL_426;
				case 30:
					num = 37;
					continue;
				case 31:
				{
					int num4;
					if (A_2.ContainsKey(num4))
					{
						num = 12;
						continue;
					}
					goto IL_426;
				}
				case 32:
					num = 23;
					continue;
				case 33:
					if (num2 >= this.ᜇ)
					{
						num = 27;
						continue;
					}
					tbiffrecord2 = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num3 = (int)this.ᜈ.ReadInt16(num2 + 2);
					num = 7;
					continue;
				case 34:
					if (tbiffrecord2 == TBIFFRecord.Formula)
					{
						num = 24;
						continue;
					}
					num = 18;
					continue;
				case 35:
					num = 17;
					continue;
				case 36:
					goto IL_4BC;
				case 37:
				{
					bool flag;
					if (!flag)
					{
						num = 32;
						continue;
					}
					num = 34;
					continue;
				}
				case 38:
					goto IL_426;
				case 39:
				{
					if (A_1 == null)
					{
						num = 5;
						continue;
					}
					num2 = 0;
					int key = (int)this.ᜇ();
					bool flag = A_0 == A_1;
					XlsWorkbook workbook = A_1.Workbook;
					num = 41;
					continue;
				}
				case 40:
					this.ᜀ(A_0, A_1, num2, false, A_5);
					num = 11;
					continue;
				case 41:
				{
					int key;
					if (A_2.ContainsKey(key))
					{
						num = 4;
						continue;
					}
					goto IL_4BC;
				}
				}
				if (A_0 == null)
				{
					num = 19;
					continue;
				}
				num = 39;
				continue;
				IL_341:
				num2 += num3 + 4;
				num = 36;
				continue;
				IL_426:
				tbiffrecord = tbiffrecord2;
				num = 0;
				continue;
				IL_48B:
				num = 2;
				continue;
				IL_4BC:
				num = 33;
			}
			IL_E6:
			throw new ArgumentNullException(RecordTableEnumerator.b("㑆♈㹊㽌ⱎ㑐R͖ٔ", a_));
			IL_421:
			throw new ArgumentNullException(RecordTableEnumerator.b("⍆ⱈ㡊㥌ᱎɐݒ", a_));
		}
		}
	}

	// Token: 0x060030A8 RID: 12456 RVA: 0x001BF440 File Offset: 0x001BE440
	private void ᜀ(Dictionary<int, int> A_0, int A_1, int A_2, bool A_3)
	{
		for (;;)
		{
			IL_34:
			A_2 = A_1 + 4 + A_2 - 2;
			A_1 += 8;
			int num = 7;
			for (;;)
			{
				int num2;
				int num4;
				switch (num)
				{
				case 0:
					num = 6;
					continue;
				case 1:
				{
					if (num2 >= A_2)
					{
						num = 4;
						continue;
					}
					int num3 = (int)this.ᜈ.ReadInt16(num2);
					num = 9;
					continue;
				}
				case 2:
					goto IL_B4;
				case 3:
					goto IL_102;
				case 4:
					return;
				case 5:
					num4 = 6;
					goto IL_113;
				case 6:
					num4 = 2;
					goto IL_113;
				case 7:
					if (!A_3)
					{
						num = 0;
						continue;
					}
					num = 5;
					continue;
				case 8:
				{
					int num3;
					this.ᜈ.WriteInt16(num2, (short)num3);
					num = 3;
					continue;
				}
				case 9:
				{
					int num3;
					if (A_0.TryGetValue(num3, out num3))
					{
						num = 8;
						continue;
					}
					goto IL_102;
				}
				case 10:
					goto IL_B4;
				}
				break;
				IL_B4:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_34;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				IL_102:
				int num5;
				num2 += num5;
				num = 2;
				continue;
				IL_113:
				num5 = num4;
				num2 = A_1;
				num = 10;
			}
		}
	}

	// Token: 0x060030A9 RID: 12457 RVA: 0x001BF57C File Offset: 0x001BE57C
	private void ᜀ(SSTDictionary A_0, SSTDictionary A_1, int A_2, bool A_3, Dictionary<int, int> A_4)
	{
		int a_ = 19;
		int num = 0;
		int num2;
		spr\u223A spr_u223A;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_C1;
			case 2:
				goto IL_8B;
			case 3:
				goto IL_E8;
			case 4:
				if (A_1 == null)
				{
					num = 2;
					continue;
				}
				num2 = spr\u1C7C.ᜂ(this.ᜈ, A_2, this.ᜆ());
				num = 9;
				continue;
			case 5:
				goto IL_12A;
			case 6:
				num = 3;
				continue;
			case 7:
				if (spr_u223A.ᜆ() != 0)
				{
					num = 6;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_110;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 8:
				goto IL_4C;
			case 9:
				goto IL_110;
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			num = 4;
			continue;
			IL_110:
			if (true)
			{
			}
			if (A_3)
			{
				num = 5;
			}
			else
			{
				spr_u223A = A_0[num2];
				num = 7;
			}
		}
		IL_4C:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩈⑊㡌㵎㉐㙒ٔі൘", a_));
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("ⵈ⹊㹌㭎ɐRŔ", a_));
		IL_C1:
		object obj = spr_u223A.ᜏ();
		goto IL_143;
		IL_E8:
		obj = spr_u223A.ᜁ(A_4);
		goto IL_143;
		IL_12A:
		A_1.AddIncrease(num2);
		return;
		IL_143:
		object key = obj;
		num2 = A_1.AddIncrease(key);
		spr\u1C7C.ᜀ(this.ᜈ, A_2, num2, this.ᜆ());
	}

	// Token: 0x060030AA RID: 12458 RVA: 0x001BF6E8 File Offset: 0x001BE6E8
	private void ᜀ(SSTDictionary A_0, SSTDictionary A_1, int A_2, Dictionary<string, string> A_3, Dictionary<int, int> A_4, int A_5, Dictionary<int, int> A_6)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr᱒ spr᱒ = (spr᱒)spr\u175E.ᜀ(this.ᜈ, A_2, this.ᜆ());
				spr᱒.ParseStructure(this.ᜈ, A_2 + 4, A_5, this.ᜆ());
				XlsWorkbook workbook = A_1.Workbook;
				bool flag = this.ᜀ(spr᱒, A_3, A_0.Workbook, workbook, A_4, A_6);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AA;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							if (flag)
							{
								num = 1;
								continue;
							}
							return;
						}
						break;
					case 1:
						goto IL_AA;
					case 2:
						return;
					}
					break;
					IL_AA:
					spr᱒.ᜅ(true);
					int storeSize = spr᱒.GetStoreSize(this.ᜆ());
					this.ᜀ(A_2, A_5 + 4, storeSize + 4, spr᱒, workbook.ReservedHandle.\u171D());
					num = 2;
				}
			}
			return;
		}
	}

	// Token: 0x060030AB RID: 12459 RVA: 0x001BF7E8 File Offset: 0x001BE7E8
	private bool ᜀ(spr᱒ A_0, IDictionary A_1, XlsWorkbook A_2, XlsWorkbook A_3)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 5;
			for (;;)
			{
				bool result;
				int num2;
				string text;
				spr\u2086 spr_u;
				switch (num)
				{
				case 0:
				{
					if (A_2 == null)
					{
						num = 14;
						continue;
					}
					Ptg[] array = A_0.ᜑ();
					A_0.GetStoreSize(this.ᜆ());
					result = false;
					num2 = 0;
					int num3 = array.Length;
					num = 13;
					continue;
				}
				case 1:
					goto IL_141;
				case 2:
					goto IL_F6;
				case 3:
					text = (string)A_1[text];
					num = 11;
					continue;
				case 4:
					return result;
				case 6:
					goto IL_75;
				case 7:
				{
					Ptg ptg;
					if (!(ptg is spr\u2086))
					{
						goto IL_F6;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6C;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				}
				case 8:
				{
					Ptg ptg;
					spr_u = (spr\u2086)ptg;
					ushort reference = spr_u.ᜁ();
					text = A_2.GetSheetNameByReference((int)reference);
					num = 12;
					continue;
				}
				case 9:
					if (A_1.Contains(text))
					{
						num = 3;
						continue;
					}
					goto IL_11C;
				case 10:
					num = 9;
					continue;
				case 11:
					goto IL_11C;
				case 12:
					if (A_1 != null)
					{
						num = 10;
						continue;
					}
					goto IL_11C;
				case 13:
					goto IL_141;
				case 14:
					goto IL_F1;
				case 15:
				{
					int num3;
					if (num2 >= num3)
					{
						num = 4;
						continue;
					}
					Ptg[] array;
					Ptg ptg = array[num2];
					num = 7;
					continue;
				}
				}
				goto IL_69;
				IL_6C:
				num = 6;
				continue;
				IL_69:
				if (A_0 == null)
				{
					goto IL_6C;
				}
				num = 0;
				continue;
				IL_F6:
				num2++;
				num = 1;
				continue;
				IL_11C:
				int num4 = A_3.AddSheetReference(text);
				spr_u.ᜂ((ushort)num4);
				result = true;
				num = 2;
				continue;
				IL_141:
				num = 15;
			}
			IL_75:
			throw new ArgumentNullException(RecordTableEnumerator.b("≃⥅㩇❉㥋≍ㅏ", a_));
			IL_F1:
			throw new ArgumentNullException(RecordTableEnumerator.b("♃⥅❇ⅉ", a_));
		}
		}
	}

	// Token: 0x060030AC RID: 12460 RVA: 0x001BFA20 File Offset: 0x001BEA20
	private bool ᜀ(spr᱒ A_0, Dictionary<int, int> A_1)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				bool result;
				int num2;
				spr\u25A0 spr_u25A;
				int num4;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BB;
					default:
						if (false)
						{
						}
						goto IL_146;
					}
					break;
				case 2:
					num = 4;
					continue;
				case 3:
					return result;
				case 4:
				{
					int num3;
					num2 = num3;
					goto IL_128;
				}
				case 5:
				{
					int num3;
					if (!A_1.ContainsKey(num3))
					{
						num = 2;
						continue;
					}
					num = 9;
					continue;
				}
				case 6:
					goto IL_DE;
				case 7:
					if (spr_u25A != null)
					{
						num = 10;
						continue;
					}
					goto IL_DE;
				case 8:
					goto IL_146;
				case 9:
				{
					int num3;
					num2 = A_1[num3];
					goto IL_128;
				}
				case 10:
				{
					int num3 = (int)(spr_u25A.ᜀ() - 1);
					num = 5;
					continue;
				}
				case 11:
					goto IL_71;
				case 12:
				{
					if (A_1 == null)
					{
						num = 13;
						continue;
					}
					Ptg[] array = A_0.ᜑ();
					A_0.GetStoreSize(this.ᜆ());
					result = false;
					num4 = 0;
					int num5 = array.Length;
					num = 8;
					continue;
				}
				case 13:
					goto IL_D9;
				case 14:
				{
					int num5;
					if (num4 >= num5)
					{
						num = 3;
						continue;
					}
					Ptg[] array;
					spr_u25A = (array[num4] as spr\u25A0);
					num = 7;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				IL_BB:
				num = 12;
				continue;
				IL_DE:
				num4++;
				num = 0;
				continue;
				IL_128:
				int num6 = num2;
				spr_u25A.ᜀ((ushort)(num6 + 1));
				result = true;
				num = 6;
				continue;
				IL_146:
				num = 14;
			}
			IL_71:
			throw new ArgumentNullException(RecordTableEnumerator.b("帷唹主匽㔿⹁╃", a_));
			IL_D9:
			throw new ArgumentNullException(RecordTableEnumerator.b("尷匹弻瀽ℿ⽁⅃ཅ♇⹉⥋㙍㕏⅑", a_));
		}
		}
	}

	// Token: 0x060030AD RID: 12461 RVA: 0x001BFC24 File Offset: 0x001BEC24
	private bool ᜀ(spr᱒ A_0, Dictionary<string, string> A_1, XlsWorkbook A_2, XlsWorkbook A_3, Dictionary<int, int> A_4, Dictionary<int, int> A_5)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 15;
			for (;;)
			{
				string text;
				int num2;
				int num4;
				int num6;
				bool result;
				spr\u1B76 spr_u1B;
				spr\u25A0 spr_u25A;
				spr\u2086 spr_u;
				switch (num)
				{
				case 0:
					goto IL_3FB;
				case 1:
					goto IL_27C;
				case 2:
					if (A_1.ContainsKey(text))
					{
						num = 25;
						continue;
					}
					goto IL_3FB;
				case 3:
				{
					int num3;
					num2 = num3;
					goto IL_3DC;
				}
				case 4:
				{
					int num5;
					num4 = num5;
					goto IL_23F;
				}
				case 5:
					num = 4;
					continue;
				case 6:
					goto IL_2D8;
				case 7:
				{
					if (true)
					{
					}
					int num7;
					if (num6 >= num7)
					{
						num = 8;
						continue;
					}
					Ptg[] array;
					Ptg ptg = array[num6];
					num = 32;
					continue;
				}
				case 8:
					return result;
				case 9:
					goto IL_299;
				case 10:
					goto IL_C6;
				case 11:
				{
					if (A_4 == null)
					{
						num = 1;
						continue;
					}
					Ptg[] array = A_0.ᜑ();
					A_0.GetStoreSize(this.ᜆ());
					result = false;
					num6 = 0;
					int num7 = array.Length;
					num = 30;
					continue;
				}
				case 12:
					goto IL_29B;
				case 13:
				{
					ushort reference;
					if (!A_2.IsExternalReference((int)reference))
					{
						num = 20;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2D8;
					default:
					{
						if (false)
						{
						}
						Ptg ptg;
						spr_u1B = (ptg as spr\u1B76);
						num = 16;
						continue;
					}
					}
					break;
				}
				case 14:
					num = 3;
					continue;
				case 16:
					if (spr_u1B != null)
					{
						num = 24;
						continue;
					}
					goto IL_29B;
				case 17:
					if (A_2 == null)
					{
						num = 9;
						continue;
					}
					num = 11;
					continue;
				case 18:
				{
					int num3;
					num2 = A_5[num3];
					goto IL_3DC;
				}
				case 19:
				{
					int num5;
					num4 = A_4[num5];
					goto IL_23F;
				}
				case 20:
				{
					ushort reference;
					text = A_2.GetSheetNameByReference((int)reference);
					num = 29;
					continue;
				}
				case 21:
					goto IL_37D;
				case 22:
					if (spr_u25A != null)
					{
						num = 28;
						continue;
					}
					goto IL_29B;
				case 23:
				{
					int num5;
					if (!A_4.ContainsKey(num5))
					{
						num = 5;
						continue;
					}
					num = 19;
					continue;
				}
				case 24:
				{
					int num3 = (int)spr_u1B.ᜃ();
					num = 26;
					continue;
				}
				case 25:
					text = A_1[text];
					num = 0;
					continue;
				case 26:
				{
					int num3;
					if (!A_5.ContainsKey(num3))
					{
						num = 14;
						continue;
					}
					num = 18;
					continue;
				}
				case 27:
					goto IL_29B;
				case 28:
				{
					int num5 = (int)(spr_u25A.ᜀ() - 1);
					num = 23;
					continue;
				}
				case 29:
					if (A_1 != null)
					{
						num = 6;
						continue;
					}
					goto IL_3FB;
				case 30:
					goto IL_37D;
				case 31:
					goto IL_29B;
				case 32:
				{
					Ptg ptg;
					if (ptg is spr\u2086)
					{
						num = 33;
						continue;
					}
					spr_u25A = (ptg as spr\u25A0);
					num = 22;
					continue;
				}
				case 33:
				{
					Ptg ptg;
					spr_u = (spr\u2086)ptg;
					ushort reference = spr_u.ᜁ();
					num = 13;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				num = 17;
				continue;
				IL_23F:
				int num8 = num4;
				spr_u25A.ᜀ((ushort)(num8 + 1));
				result = true;
				num = 12;
				continue;
				IL_29B:
				num6++;
				num = 21;
				continue;
				IL_2D8:
				num = 2;
				continue;
				IL_37D:
				num = 7;
				continue;
				IL_3DC:
				int num9 = num2;
				spr_u1B.ᜁ((ushort)num9);
				result = true;
				num = 31;
				continue;
				IL_3FB:
				int num10 = A_3.AddSheetReference(text);
				spr_u.ᜂ((ushort)num10);
				result = true;
				num = 27;
			}
			IL_C6:
			throw new ArgumentNullException(RecordTableEnumerator.b("娻儽㈿⽁ㅃ⩅⥇", a_));
			IL_27C:
			throw new ArgumentNullException(RecordTableEnumerator.b("堻圽⌿ు╃⭅ⵇ͉≋⩍㕏⩑ㅓ╕", a_));
			IL_299:
			throw new ArgumentNullException(RecordTableEnumerator.b("帻儽⼿⥁", a_));
		}
		}
	}

	// Token: 0x060030AE RID: 12462 RVA: 0x001C0058 File Offset: 0x001BF058
	private bool ᜀ(spr\u23A5 A_0, int A_1)
	{
		int a_ = 12;
		int num = 6;
		int num2;
		for (;;)
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
			{
				if (false)
				{
				}
				int num3;
				TBIFFRecord tbiffrecord;
				switch (num)
				{
				case 0:
					goto IL_68;
				case 1:
					goto IL_B5;
				case 2:
					goto IL_80;
				case 3:
					return false;
				case 4:
					if (num2 >= num3)
					{
						num = 3;
						continue;
					}
					num = 7;
					continue;
				case 5:
					goto IL_9F;
				case 7:
					if (sprᱧ.ᜄ[num2] == tbiffrecord)
					{
						num = 5;
						continue;
					}
					num2 += 2;
					num = 2;
					continue;
				}
				if (A_0 == null)
				{
					num = 0;
					continue;
				}
				tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(A_1);
				num2 = 0;
				num3 = sprᱧ.ᜄ.Length;
				num = 1;
				continue;
			}
			}
			IL_B5:
			num = 4;
			continue;
			IL_80:
			goto IL_B5;
		}
		IL_68:
		throw new ArgumentNullException(RecordTableEnumerator.b("⅁⅃⩅⑇", a_));
		IL_9F:
		return sprᱧ.ᜄ[num2 + 1] == A_0.get_TypeCode();
	}

	// Token: 0x060030AF RID: 12463 RVA: 0x001C0170 File Offset: 0x001BF170
	private int ᜀ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_57:
				BiffRecordRaw biffRecordRaw = spr\u175E.ᜀ(this.ᜈ, A_0, this.ᜆ());
				sprᤞ sprᤞ = (sprᤞ)biffRecordRaw;
				int num = biffRecordRaw.GetStoreSize(this.ᜆ()) + 4;
				spr\u23A5[] array = sprᤞ.ᜂ(A_1);
				int num2 = array.Length;
				int num3 = 0;
				int num4 = 0;
				for (;;)
				{
					IL_93:
					int num5 = 1;
					for (;;)
					{
						int num7;
						switch (num5)
						{
						case 0:
							goto IL_1B0;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_93;
							default:
								if (false)
								{
								}
								goto IL_1D3;
							}
							break;
						case 2:
						{
							int num6 = num3 - num;
							this.ᜅ(this.ᜇ + num6, A_2);
							this.ᜀ(A_0, num, 0, null, A_2);
							num7 = 0;
							num5 = 12;
							continue;
						}
						case 3:
							num3 += biffRecordRaw.GetStoreSize(this.ᜆ()) + 4;
							num5 = 9;
							continue;
						case 4:
						{
							if (num7 >= num2)
							{
								num5 = 14;
								continue;
							}
							BiffRecordRaw biffRecordRaw2 = (BiffRecordRaw)array[num7];
							num5 = 5;
							continue;
						}
						case 5:
						{
							BiffRecordRaw biffRecordRaw2;
							if (biffRecordRaw2 != null)
							{
								num5 = 8;
								continue;
							}
							goto IL_133;
						}
						case 6:
							goto IL_1D3;
						case 7:
							if (num7 == 0)
							{
								num5 = 13;
								continue;
							}
							goto IL_133;
						case 8:
						{
							if (true)
							{
							}
							BiffRecordRaw biffRecordRaw2;
							int num8 = biffRecordRaw2.GetStoreSize(this.ᜆ()) + 4;
							this.ᜀ(A_0, 0, num8, biffRecordRaw2, A_2);
							num5 = 7;
							continue;
						}
						case 9:
							goto IL_1F6;
						case 10:
							goto IL_133;
						case 11:
						{
							if (num4 >= num2)
							{
								num5 = 2;
								continue;
							}
							BiffRecordRaw biffRecordRaw3 = (BiffRecordRaw)array[num4];
							num5 = 15;
							continue;
						}
						case 12:
							goto IL_1B0;
						case 13:
						{
							int num8;
							A_0 += num8;
							num5 = 10;
							continue;
						}
						case 14:
							return A_0;
						case 15:
							if (biffRecordRaw != null)
							{
								num5 = 3;
								continue;
							}
							goto IL_1F6;
						}
						goto IL_57;
						IL_133:
						num7++;
						num5 = 0;
						continue;
						IL_1B0:
						num5 = 4;
						continue;
						IL_1D3:
						num5 = 11;
						continue;
						IL_1F6:
						num4++;
						num5 = 6;
					}
				}
			}
			return A_0;
		}
	}

	// Token: 0x060030B0 RID: 12464 RVA: 0x001C03C0 File Offset: 0x001BF3C0
	private void ᜁ(int A_0, spr\u23A5 A_1)
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
		sprᤞ sprᤞ = (sprᤞ)spr\u175E.ᜀ(this.ᜈ, A_0, this.ᜆ());
		sprᤞ.ᜀ(A_1);
	}

	// Token: 0x060030B1 RID: 12465 RVA: 0x001C041C File Offset: 0x001BF41C
	private void ᜀ(int A_0, int A_1, int A_2, BiffRecordRaw A_3, int A_4)
	{
		for (;;)
		{
			int num = A_2 - A_1;
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_5F;
				case 1:
					goto IL_15B;
				case 2:
					this.ᜈ.WriteInt16(A_0, (short)A_3.TypeCode);
					this.ᜆ();
					this.ᜈ.WriteInt16(A_0 + 2, (short)A_3.GetStoreSize(this.ᜆ()));
					A_3.InfillInternalData(this.ᜈ, A_0 + 4, this.ᜆ());
					num2 = 11;
					continue;
				case 3:
					this.ᜅ(num + this.ᜇ, A_4);
					goto IL_14B;
				case 4:
					if (num > 0)
					{
						num2 = 3;
						continue;
					}
					goto IL_9B;
				case 5:
					if (A_1 != A_2)
					{
						num2 = 8;
						continue;
					}
					goto IL_15B;
				case 6:
					if (A_3 != null)
					{
						num2 = 2;
						continue;
					}
					return;
				case 7:
				{
					int num3;
					if (num3 > 0)
					{
						num2 = 10;
						continue;
					}
					goto IL_5F;
				}
				case 8:
				{
					int num3 = this.ᜇ - A_0 - A_1;
					num2 = 7;
					continue;
				}
				case 9:
					goto IL_9B;
				case 10:
				{
					int num3;
					this.ᜈ.MoveMemory(A_0 + A_2, A_0 + A_1, num3);
					num2 = 0;
					continue;
				}
				case 11:
					return;
				}
				break;
				IL_5F:
				this.ᜇ += A_2 - A_1;
				num2 = 1;
				continue;
				IL_9B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_14B:
					num2 = 9;
					continue;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num2 = 5;
					continue;
				}
				IL_15B:
				num2 = 6;
			}
		}
	}

	// Token: 0x060030B2 RID: 12466 RVA: 0x001C05D0 File Offset: 0x001BF5D0
	private void ᜀ(int A_0, int A_1, IList A_2, int A_3)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 11;
			for (;;)
			{
				int num5;
				switch (num)
				{
				case 0:
				{
					int count;
					if (count > 0)
					{
						num = 2;
						continue;
					}
					return;
				}
				case 1:
					goto IL_1BF;
				case 2:
				{
					int num2 = 0;
					num = 12;
					continue;
				}
				case 3:
				{
					int num3;
					if (num3 > 0)
					{
						num = 7;
						continue;
					}
					goto IL_99;
				}
				case 4:
				{
					int num4 = num5 - A_1;
					num = 19;
					continue;
				}
				case 5:
				{
					int num3 = this.ᜇ - A_0 - A_1;
					num = 3;
					continue;
				}
				case 6:
					return;
				case 7:
				{
					int num3;
					this.ᜈ.MoveMemory(A_0 + num5, A_0 + A_1, num3);
					num = 13;
					continue;
				}
				case 8:
					goto IL_BF;
				case 9:
					goto IL_94;
				case 10:
					goto IL_1E0;
				case 12:
					goto IL_19D;
				case 13:
					goto IL_99;
				case 14:
					goto IL_1E0;
				case 15:
				{
					int count;
					int num6;
					if (num6 >= count)
					{
						num = 4;
						continue;
					}
					BiffRecordRaw biffRecordRaw = (BiffRecordRaw)A_2[num6];
					int storeSize = biffRecordRaw.GetStoreSize(this.ᜆ());
					num5 += storeSize + 4;
					int val = Math.Max(storeSize, val);
					num6++;
					num = 10;
					continue;
				}
				case 16:
					if (A_1 != num5)
					{
						num = 5;
						continue;
					}
					goto IL_17C;
				case 17:
				{
					int num4;
					this.ᜅ(num4 + this.ᜇ, A_3);
					num = 1;
					continue;
				}
				case 18:
				{
					int count;
					int num2;
					if (num2 >= count)
					{
						num = 6;
						continue;
					}
					BiffRecordRaw biffRecordRaw2 = (BiffRecordRaw)A_2[num2];
					this.ᜈ.WriteInt16(A_0, (short)biffRecordRaw2.TypeCode);
					this.ᜈ.WriteInt16(A_0 + 2, (short)biffRecordRaw2.GetStoreSize(this.ᜆ()));
					biffRecordRaw2.InfillInternalData(this.ᜈ, A_0, this.ᜆ());
					A_0 += biffRecordRaw2.Length + 4;
					num2++;
					num = 20;
					continue;
				}
				case 19:
				{
					int num4;
					if (num4 > 0)
					{
						num = 17;
						continue;
					}
					goto IL_1BF;
				}
				case 20:
					goto IL_19D;
				}
				if (A_2 == null)
				{
					if (true)
					{
					}
					num = 9;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BF;
				default:
				{
					if (false)
					{
					}
					num5 = 0;
					int count = A_2.Count;
					int val = 0;
					int num6 = 0;
					num = 14;
					continue;
				}
				}
				IL_99:
				this.ᜇ += num5 - A_1;
				num = 8;
				continue;
				IL_17C:
				num = 0;
				continue;
				IL_BF:
				goto IL_17C;
				IL_19D:
				num = 18;
				continue;
				IL_1BF:
				num = 16;
				continue;
				IL_1E0:
				num = 15;
			}
			IL_94:
			throw new ArgumentNullException(RecordTableEnumerator.b("帾㍀ㅂᝄ≆⩈⑊㽌⭎≐", a_));
		}
		}
	}

	// Token: 0x060030B3 RID: 12467 RVA: 0x001C08DC File Offset: 0x001BF8DC
	private int ᜀ(int A_0, out bool A_1)
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
		bool flag;
		return this.ᜀ(A_0, out A_1, out flag, false);
	}

	// Token: 0x060030B4 RID: 12468 RVA: 0x001C0924 File Offset: 0x001BF924
	private int ᜀ(int A_0, out bool A_1, out bool A_2, bool A_3)
	{
		switch (0)
		{
		default:
		{
			int num2;
			for (;;)
			{
				A_1 = false;
				A_2 = false;
				int num = 30;
				for (;;)
				{
					int num4;
					bool flag;
					int num5;
					bool flag2;
					bool flag3;
					bool flag4;
					bool flag5;
					switch (num)
					{
					case 0:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord != TBIFFRecord.Array)
						{
							num = 5;
							continue;
						}
						goto IL_23F;
					}
					case 1:
						num = 12;
						continue;
					case 2:
						num = 11;
						continue;
					case 3:
					{
						if (num2 >= this.ᜇ)
						{
							num = 25;
							continue;
						}
						long num3 = this.ᜈ.ReadInt64(num2);
						TBIFFRecord tbiffrecord = (TBIFFRecord)(num3 & 65535L);
						num3 >>= 16;
						num4 = (int)(num3 & 65535L);
						num = 9;
						continue;
					}
					case 4:
						goto IL_1DA;
					case 5:
					{
						long num3;
						num3 >>= 32;
						num = 21;
						continue;
					}
					case 6:
						num = 0;
						continue;
					case 7:
						goto IL_1DA;
					case 8:
						if (!flag)
						{
							goto IL_23F;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_271;
						default:
							if (false)
							{
							}
							num = 32;
							continue;
						}
						break;
					case 9:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord != TBIFFRecord.String)
						{
							num = 6;
							continue;
						}
						goto IL_23F;
					}
					case 10:
					{
						TBIFFRecord tbiffrecord;
						if (!this.ᜀ(ref num2, num4, num5, A_0, ref A_2, tbiffrecord, A_3))
						{
							num = 20;
							continue;
						}
						goto IL_1DA;
					}
					case 11:
						if (!A_2)
						{
							num = 17;
							continue;
						}
						return num2;
					case 12:
						flag2 = (this.ᜊ >= 0);
						goto IL_305;
					case 13:
						num = 10;
						continue;
					case 14:
						if (A_0 >= this.ᜊ)
						{
							num = 1;
							continue;
						}
						num = 37;
						continue;
					case 15:
						flag3 = true;
						goto IL_350;
					case 16:
						num = 42;
						continue;
					case 17:
						this.ᜊ = num5;
						this.ᜋ = num2;
						goto IL_271;
					case 18:
						num2 = this.ᜋ;
						num5 = this.ᜊ;
						num = 39;
						continue;
					case 19:
						num2 = this.ᜇ;
						num5 = int.MaxValue;
						num = 27;
						continue;
					case 20:
						goto IL_23F;
					case 21:
						if (this.\u1712() == 4)
						{
							num = 34;
							continue;
						}
						num5 = this.ᜈ.ReadInt32(num2 + 4 + 4);
						num = 28;
						continue;
					case 22:
						if (flag4)
						{
							num = 13;
							continue;
						}
						goto IL_23F;
					case 23:
						goto IL_49A;
					case 24:
						goto IL_27D;
					case 25:
						num5 = int.MaxValue;
						num = 4;
						continue;
					case 26:
						goto IL_282;
					case 27:
						goto IL_1DA;
					case 28:
						goto IL_49A;
					case 29:
						if (num5 >= A_0)
						{
							num = 7;
							continue;
						}
						goto IL_282;
					case 30:
						if (A_0 >= this.ᜅ)
						{
							num = 36;
							continue;
						}
						return 0;
					case 31:
						return 0;
					case 32:
						num = 35;
						continue;
					case 33:
						if (A_1)
						{
							num = 2;
							continue;
						}
						return num2;
					case 34:
					{
						long num3;
						num5 = (int)(num3 & 65535L);
						num = 23;
						continue;
					}
					case 35:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord != TBIFFRecord.MulRK)
						{
							num = 16;
							continue;
						}
						num = 15;
						continue;
					}
					case 36:
						if (true)
						{
						}
						num = 38;
						continue;
					case 37:
						flag2 = false;
						goto IL_305;
					case 38:
						if (this.ᜇ <= 0)
						{
							num = 31;
							continue;
						}
						num = 41;
						continue;
					case 39:
						goto IL_282;
					case 40:
						if (flag5)
						{
							num = 18;
							continue;
						}
						num5 = this.ᜅ;
						num = 26;
						continue;
					case 41:
						if (A_0 > this.ᜆ)
						{
							num = 19;
							continue;
						}
						num2 = 0;
						num = 14;
						continue;
					case 42:
					{
						TBIFFRecord tbiffrecord;
						flag3 = (tbiffrecord == TBIFFRecord.MulBlank);
						goto IL_350;
					}
					}
					break;
					IL_1DA:
					A_1 = (num5 <= A_0);
					num = 33;
					continue;
					IL_23F:
					num = 29;
					continue;
					IL_271:
					num = 24;
					continue;
					IL_282:
					num2 += num4 + 4;
					num = 3;
					continue;
					IL_305:
					flag5 = flag2;
					num4 = -4;
					flag = this.ᜏ();
					num = 40;
					continue;
					IL_350:
					flag4 = flag3;
					num = 22;
					continue;
					IL_49A:
					num = 8;
				}
			}
			IL_27D:
			return num2;
		}
		}
	}

	// Token: 0x060030B5 RID: 12469 RVA: 0x001C0E0C File Offset: 0x001BFE0C
	private bool ᜀ(ref int A_0, int A_1, int A_2, int A_3, ref bool A_4, TBIFFRecord A_5, bool A_6)
	{
		switch (0)
		{
		default:
		{
			bool result;
			for (;;)
			{
				int num = this.\u1712();
				int iOffset = A_0 + 4 + A_1 - num / 2;
				int num2 = 10;
				for (;;)
				{
					int num3;
					int num4;
					int num6;
					int num7;
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						num2 = 5;
						continue;
					case 1:
						if (A_5 != TBIFFRecord.MulRK)
						{
							num2 = 11;
							continue;
						}
						num2 = 7;
						continue;
					case 2:
						A_4 = true;
						num2 = 1;
						continue;
					case 3:
						num3 = this.ᜈ.ReadInt32(iOffset);
						goto IL_D8;
					case 4:
						goto IL_84;
					case 5:
						if (num4 >= A_3)
						{
							num2 = 2;
							continue;
						}
						return result;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_108;
						default:
						{
							if (false)
							{
							}
							this.ᜊ = A_2;
							this.ᜋ = A_0;
							int num5 = A_3 - A_2;
							A_0 = A_0 + 4 + num + num5 * num6;
							num2 = 4;
							continue;
						}
						}
						break;
					case 7:
						num7 = 6;
						goto IL_18B;
					case 8:
						if (A_2 <= A_3)
						{
							num2 = 0;
							continue;
						}
						return result;
					case 9:
						num3 = (int)this.ᜈ.ReadInt16(iOffset);
						goto IL_D8;
					case 10:
						if (num != 4)
						{
							num2 = 12;
							continue;
						}
						goto IL_108;
					case 11:
						num2 = 15;
						continue;
					case 12:
						num2 = 3;
						continue;
					case 13:
						return result;
					case 14:
						if (A_6)
						{
							num2 = 6;
							continue;
						}
						goto IL_84;
					case 15:
						num7 = 2;
						goto IL_18B;
					}
					break;
					IL_84:
					result = true;
					num2 = 13;
					continue;
					IL_D8:
					num4 = num3;
					result = false;
					num2 = 8;
					continue;
					IL_108:
					num2 = 9;
					continue;
					IL_18B:
					num6 = num7;
					num2 = 14;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x060030B6 RID: 12470 RVA: 0x001C0FFC File Offset: 0x001BFFFC
	private void ᜅ(int A_0, int A_1)
	{
		int num = 2;
		int size;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_86;
				default:
					goto IL_4E;
				}
				break;
			case 1:
				if (this.\u170D != null)
				{
					goto IL_86;
				}
				goto IL_A0;
			case 2:
				if (true)
				{
				}
				break;
			case 3:
				goto IL_8E;
			}
			if (this.ᜈ == null)
			{
				num = 0;
				continue;
			}
			size = A_0 / A_1 * A_1 + A_1;
			num = 1;
			continue;
			IL_86:
			num = 3;
		}
		IL_4E:
		if (false)
		{
		}
		throw new NotImplementedException();
		IL_8E:
		this.ᜈ.EnsureCapacity(size, this.\u170D.MaxImportRows);
		return;
		IL_A0:
		this.ᜈ.EnsureCapacity(size);
	}

	// Token: 0x060030B7 RID: 12471 RVA: 0x001C10B8 File Offset: 0x001C00B8
	private void ᜆ(int A_0)
	{
		int a_ = 17;
		if (true)
		{
		}
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_39;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_31;
				}
				goto Block_1;
			}
			goto IL_2D;
			IL_31:
			num = 0;
			continue;
			IL_2D:
			if (A_0 < 0)
			{
				goto IL_31;
			}
			num = 2;
		}
		IL_39:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⹆ੈ⑊⅌㩎㱐㵒᱔㥖㵘㹚╜", a_), RecordTableEnumerator.b("ц♈❊㡌≎㽐獒㱔㥖㵘㹚╜罞ɠɢ୤०٨Ὢ䵬൮ᑰ卲ᥴቶ੸ࡺ嵼୾Ꞇ릈", a_));
		Block_1:
		if (false)
		{
		}
		this.ᜅ = ((this.ᜅ >= 0) ? Math.Min(this.ᜅ, A_0) : A_0);
		this.ᜆ = Math.Max(this.ᜆ, A_0);
	}

	// Token: 0x060030B8 RID: 12472 RVA: 0x001C1184 File Offset: 0x001C0184
	private void ᜀ(int A_0, spr\u23A5 A_1)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 3;
			int num2;
			for (;;)
			{
				int num3;
				int num5;
				switch (num)
				{
				case 0:
					goto IL_236;
				case 1:
					num = 23;
					continue;
				case 2:
					num = 10;
					continue;
				case 4:
					num = 27;
					continue;
				case 5:
					num = 15;
					continue;
				case 6:
					if (true)
					{
					}
					num2 = -1;
					num3 = 0;
					num = 7;
					continue;
				case 7:
					goto IL_1EC;
				case 8:
					if (A_0 == this.ᜅ)
					{
						num = 4;
						continue;
					}
					num = 19;
					continue;
				case 9:
					goto IL_190;
				case 10:
					goto IL_DA;
				case 11:
					goto IL_A9;
				case 12:
					num = 21;
					continue;
				case 13:
					goto IL_2EA;
				case 14:
					if (A_1 != null)
					{
						num = 2;
						continue;
					}
					num = 8;
					continue;
				case 15:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.String)
					{
						num = 1;
						continue;
					}
					goto IL_122;
				}
				case 16:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.Array)
					{
						num = 5;
						continue;
					}
					goto IL_122;
				}
				case 17:
					num = 26;
					continue;
				case 18:
					goto IL_122;
				case 19:
					if (A_0 == this.ᜆ)
					{
						num = 6;
						continue;
					}
					return;
				case 20:
					goto IL_1EC;
				case 21:
				{
					IL_106:
					TBIFFRecord tbiffrecord;
					if (tbiffrecord == TBIFFRecord.MulRK)
					{
						num = 13;
						continue;
					}
					num2 = this.ᜉ(num3);
					num = 24;
					continue;
				}
				case 22:
					goto IL_2D7;
				case 23:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.MulBlank)
					{
						num = 12;
						continue;
					}
					goto IL_2EA;
				}
				case 24:
					goto IL_122;
				case 25:
				{
					if (num3 >= this.ᜇ)
					{
						num = 17;
						continue;
					}
					short num4 = this.ᜈ.ReadInt16(num3);
					TBIFFRecord tbiffrecord = (TBIFFRecord)num4;
					num5 = (int)this.ᜈ.ReadInt16(num3 + 2);
					num = 16;
					continue;
				}
				case 26:
					if (num2 >= 0)
					{
						num = 9;
						continue;
					}
					this.ᜅ = -1;
					this.ᜆ = -1;
					this.ᜇ = 0;
					num = 0;
					continue;
				case 27:
					if (A_0 == this.ᜆ)
					{
						num = 22;
						continue;
					}
					goto IL_2DC;
				}
				if (A_0 < 0)
				{
					num = 11;
					continue;
				}
				num = 14;
				continue;
				IL_2EA:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_106;
				default:
					if (false)
					{
					}
					num2 = this.ᜀ(num3, num5);
					num = 18;
					continue;
				}
				IL_122:
				num3 += 4 + num5;
				num = 20;
				continue;
				IL_1EC:
				num = 25;
			}
			IL_A9:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⅇॉ⍋≍╏㽑㩓ὕ㙗㹙㥛♝", a_), RecordTableEnumerator.b("େ╉⁋㭍㵏㱑瑓㽕㙗㹙㥛♝䁟šգࡥ٧թᡫ乭ቯ᝱味᩵ᵷॹཻ幽ꢇ몉", a_));
			IL_DA:
			this.ᜅ = ((this.ᜅ >= 0) ? Math.Min(this.ᜅ, A_0) : A_0);
			this.ᜆ = Math.Max(this.ᜆ, A_0);
			return;
			IL_190:
			this.ᜆ = num2;
			return;
			IL_236:
			return;
			IL_2D7:
			this.ᜆ = (this.ᜅ = -1);
			this.ᜇ = 0;
			return;
			IL_2DC:
			this.ᜅ = this.ᜉ(0);
			return;
		}
		}
	}

	// Token: 0x060030B9 RID: 12473 RVA: 0x001C152C File Offset: 0x001C052C
	private int ᜅ(int A_0)
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
		int num;
		return this.ᜀ(A_0, out num);
	}

	// Token: 0x060030BA RID: 12474 RVA: 0x001C1570 File Offset: 0x001C0570
	private int ᜀ(int A_0, out int A_1)
	{
		int num2;
		for (;;)
		{
			A_1 = -1;
			int num = 13;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜄ(num2);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return num2;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 1:
					return num2;
				case 2:
					goto IL_110;
				case 3:
					num = 9;
					continue;
				case 4:
					goto IL_1FC;
				case 5:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord == TBIFFRecord.String)
					{
						num = 0;
						continue;
					}
					return num2;
				}
				case 6:
				{
					bool flag;
					if (!flag)
					{
						num = 7;
						continue;
					}
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num = 15;
					continue;
				}
				case 7:
					return -1;
				case 8:
					return num2;
				case 9:
				{
					if (A_0 > this.ᜆ)
					{
						num = 4;
						continue;
					}
					bool flag;
					num2 = this.ᜀ(A_0, out flag);
					num = 6;
					continue;
				}
				case 10:
				{
					if (num2 >= this.ᜇ)
					{
						num = 8;
						continue;
					}
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num = 14;
					continue;
				}
				case 11:
					return -1;
				case 12:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord == TBIFFRecord.Array)
					{
						num = 16;
						continue;
					}
					goto IL_146;
				}
				case 13:
					if (A_0 >= this.ᜅ)
					{
						num = 3;
						continue;
					}
					return -1;
				case 14:
					goto IL_146;
				case 15:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord != TBIFFRecord.Formula)
					{
						num = 11;
						continue;
					}
					A_1 = num2;
					num2 = this.ᜃ(num2);
					num = 17;
					continue;
				}
				case 16:
					if (true)
					{
					}
					num2 = this.ᜃ(num2);
					num = 10;
					continue;
				case 17:
				{
					if (num2 >= this.ᜇ)
					{
						num = 1;
						continue;
					}
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num = 12;
					continue;
				}
				}
				break;
				IL_146:
				num = 5;
			}
		}
		return num2;
		IL_110:
		return num2;
		IL_1FC:
		return -1;
	}

	// Token: 0x060030BB RID: 12475 RVA: 0x001C1784 File Offset: 0x001C0784
	private void ᜄ(int A_0)
	{
		int num = 5;
		int num3;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				this.ᜋ = -1;
				this.ᜊ = -1;
				if (true)
				{
				}
				num = 3;
				continue;
			case 1:
				goto IL_53;
			case 2:
				if (num2 > 0)
				{
					num = 4;
					continue;
				}
				goto IL_D0;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_53;
				default:
					if (false)
					{
					}
					goto IL_55;
				}
				break;
			case 4:
				this.ᜈ.MoveMemory(A_0, A_0 + num3, num2);
				num = 1;
				continue;
			}
			if (A_0 < this.ᜋ)
			{
				num = 0;
				continue;
			}
			IL_55:
			num3 = (int)(this.ᜈ.ReadInt16(A_0 + 2) + 4);
			num2 = this.ᜇ - A_0 - num3;
			num = 2;
		}
		IL_53:
		IL_D0:
		this.ᜇ -= num3;
	}

	// Token: 0x060030BC RID: 12476 RVA: 0x001C1870 File Offset: 0x001C0870
	private int ᜃ(int A_0)
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
		int num = (int)this.ᜈ.ReadInt16(A_0 + 2);
		return A_0 + 4 + num;
	}

	// Token: 0x060030BD RID: 12477 RVA: 0x001C18C0 File Offset: 0x001C08C0
	private Point ᜀ(int A_0, int A_1, out int A_2, out int A_3)
	{
		switch (0)
		{
		default:
		{
			int num3;
			int num4;
			for (;;)
			{
				A_2 = -1;
				A_3 = -1;
				int num = 12;
				for (;;)
				{
					bool flag;
					bool flag2;
					switch (num)
					{
					case 0:
						if (A_0 > A_1)
						{
							num = 1;
							continue;
						}
						num = 26;
						continue;
					case 1:
						goto IL_351;
					case 2:
					{
						TBIFFRecord tbiffrecord;
						flag = (tbiffrecord == TBIFFRecord.MulBlank);
						goto IL_141;
					}
					case 3:
					{
						int num2;
						if (num2 <= A_1)
						{
							num = 20;
							continue;
						}
						goto IL_44C;
					}
					case 4:
						if (num3 >= this.ᜇ)
						{
							num = 24;
							continue;
						}
						A_0 = this.ᜉ(num3);
						A_2 = A_0;
						A_3 = A_0;
						num4 = num3;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2AE;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 5:
						goto IL_171;
					case 6:
						num4 = this.ᜂ(num4);
						num = 5;
						continue;
					case 7:
						num = 2;
						continue;
					case 8:
						if (A_3 == A_1)
						{
							num = 6;
							continue;
						}
						goto IL_205;
					case 9:
					{
						TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num4);
						num = 28;
						continue;
					}
					case 10:
						goto IL_430;
					case 11:
						goto IL_CD;
					case 12:
						if (this.ᜅ < 0)
						{
							num = 11;
							continue;
						}
						A_0 = Math.Max(this.ᜅ, A_0);
						A_1 = Math.Min(this.ᜆ, A_1);
						num = 0;
						continue;
					case 13:
					{
						if (num4 >= this.ᜇ)
						{
							num = 10;
							continue;
						}
						short num5 = this.ᜈ.ReadInt16(num4);
						TBIFFRecord tbiffrecord = (TBIFFRecord)num5;
						int num6 = (int)this.ᜈ.ReadInt16(num4 + 2);
						int num2 = this.ᜉ(num4);
						num = 3;
						continue;
					}
					case 14:
						goto IL_205;
					case 15:
						flag = true;
						goto IL_141;
					case 16:
						num = 33;
						continue;
					case 17:
						if (num4 < this.ᜇ)
						{
							num = 9;
							continue;
						}
						goto IL_205;
					case 18:
						num4 = this.ᜃ(num4);
						num = 32;
						continue;
					case 19:
						num4 = this.ᜃ(num4);
						num = 14;
						continue;
					case 20:
					{
						int num6;
						num4 += 4 + num6;
						num = 30;
						continue;
					}
					case 21:
						if (A_3 < A_1)
						{
							num = 27;
							continue;
						}
						goto IL_44C;
					case 22:
					{
						int num2;
						A_3 = (flag2 ? ((int)this.ᜈ.ReadInt16(num4 - 2)) : num2);
						goto IL_2AE;
					}
					case 23:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.String)
						{
							num = 19;
							continue;
						}
						goto IL_205;
					}
					case 24:
						goto IL_2A8;
					case 25:
						goto IL_2D7;
					case 26:
						if (A_0 == this.ᜅ)
						{
							num = 16;
							continue;
						}
						goto IL_27A;
					case 27:
						num = 13;
						continue;
					case 28:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.Array)
						{
							num = 18;
							continue;
						}
						goto IL_2D7;
					}
					case 29:
					{
						TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num4);
						num = 25;
						continue;
					}
					case 30:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord != TBIFFRecord.MulRK)
						{
							num = 7;
							continue;
						}
						num = 15;
						continue;
					}
					case 31:
						goto IL_381;
					case 32:
						if (num4 < this.ᜇ)
						{
							num = 29;
							continue;
						}
						goto IL_2D7;
					case 33:
						if (A_1 == this.ᜆ)
						{
							num = 31;
							continue;
						}
						goto IL_27A;
					}
					break;
					IL_141:
					flag2 = flag;
					num = 22;
					continue;
					IL_205:
					num = 21;
					continue;
					IL_27A:
					bool flag3;
					num3 = this.ᜀ(A_0, out flag3);
					num = 4;
					continue;
					IL_2AE:
					num = 17;
					continue;
					IL_2D7:
					num = 23;
				}
			}
			IL_CD:
			return Point.Empty;
			IL_171:
			goto IL_44C;
			IL_2A8:
			return Point.Empty;
			IL_351:
			return Point.Empty;
			IL_381:
			A_2 = A_0;
			A_3 = A_1;
			return new Point(0, this.ᜇ);
			IL_430:
			IL_44C:
			return new Point(num3, num4);
		}
		}
	}

	// Token: 0x060030BE RID: 12478 RVA: 0x001C1D20 File Offset: 0x001C0D20
	private int ᜂ(int A_0)
	{
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
					goto IL_13F;
				default:
					if (false)
					{
					}
					A_0 = this.ᜃ(A_0);
					num = 4;
					continue;
				}
				break;
			case 1:
				A_0 = this.ᜃ(A_0);
				num = 6;
				continue;
			case 2:
			{
				TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(A_0);
				goto IL_13F;
			}
			case 4:
				if (A_0 < this.ᜇ)
				{
					num = 10;
					continue;
				}
				return A_0;
			case 5:
				A_0 = this.ᜃ(A_0);
				num = 9;
				continue;
			case 6:
				if (A_0 < this.ᜇ)
				{
					num = 2;
					continue;
				}
				goto IL_72;
			case 7:
			{
				TBIFFRecord tbiffrecord;
				if (tbiffrecord == TBIFFRecord.String)
				{
					num = 5;
					continue;
				}
				return A_0;
			}
			case 8:
			{
				TBIFFRecord tbiffrecord;
				if (tbiffrecord == TBIFFRecord.Array)
				{
					num = 1;
					continue;
				}
				goto IL_72;
			}
			case 9:
				return A_0;
			case 10:
			{
				TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(A_0);
				num = 8;
				continue;
			}
			case 11:
				if (true)
				{
				}
				goto IL_72;
			}
			if (A_0 < this.ᜇ)
			{
				num = 0;
				continue;
			}
			break;
			IL_72:
			num = 7;
			continue;
			IL_13F:
			num = 11;
		}
		return A_0;
	}

	// Token: 0x060030BF RID: 12479 RVA: 0x001C1E90 File Offset: 0x001C0E90
	private sprᤞ ᜀ(TBIFFRecord A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 1:
				goto IL_68;
			case 2:
				goto IL_77;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_68;
			default:
				if (false)
				{
				}
				if (A_0 != TBIFFRecord.RK)
				{
					num = 0;
				}
				else
				{
					num = 1;
				}
				break;
			}
		}
		IL_68:
		TBIFFRecord tbiffrecord = TBIFFRecord.MulRK;
		goto IL_7E;
		IL_77:
		tbiffrecord = TBIFFRecord.MulBlank;
		IL_7E:
		TBIFFRecord a_ = tbiffrecord;
		return (sprᤞ)spr\u175E.ᜀ(a_);
	}

	// Token: 0x060030C0 RID: 12480 RVA: 0x001C1F28 File Offset: 0x001C0F28
	private spr\u23A5 ᜀ(int A_0, spr\u23A5 A_1, ref int A_2, bool A_3)
	{
		spr\u23A5 result;
		for (;;)
		{
			int num = A_0;
			result = null;
			int num2 = 0;
			for (;;)
			{
				int num3;
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11D;
					default:
						if (false)
						{
						}
						if (A_3)
						{
							num2 = 11;
							continue;
						}
						goto IL_CB;
					}
					break;
				case 1:
					result = A_1;
					num2 = 13;
					continue;
				case 2:
					if (true)
					{
					}
					if (num3 == A_0 + 1)
					{
						num2 = 5;
						continue;
					}
					return result;
				case 3:
					A_2 += (int)(4 + this.ᜈ.ReadInt16(A_2 + 2));
					num2 = 12;
					continue;
				case 4:
					goto IL_11D;
				case 5:
					result = (spr\u23A5)this.ᜈ(A_2);
					num2 = 10;
					continue;
				case 6:
					if (num >= A_0 + 1)
					{
						num2 = 1;
						continue;
					}
					num2 = 7;
					continue;
				case 7:
					if (A_1 != null)
					{
						num2 = 3;
						continue;
					}
					goto IL_79;
				case 8:
					if (A_2 < this.ᜇ)
					{
						num2 = 4;
						continue;
					}
					return result;
				case 9:
					goto IL_CB;
				case 10:
					return result;
				case 11:
					num = this.ᜁ(A_2);
					num2 = 9;
					continue;
				case 12:
					goto IL_79;
				case 13:
					return result;
				}
				break;
				IL_79:
				num2 = 8;
				continue;
				IL_CB:
				num2 = 6;
				continue;
				IL_11D:
				num3 = this.ᜉ(A_2);
				num2 = 2;
			}
		}
		return result;
	}

	// Token: 0x060030C1 RID: 12481 RVA: 0x001C20B4 File Offset: 0x001C10B4
	private int ᜁ(int A_0)
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
		int num = (int)this.ᜈ.ReadInt16(A_0 + 2);
		return (int)this.ᜈ.ReadInt16(A_0 + 4 + num - 2);
	}

	// Token: 0x060030C2 RID: 12482 RVA: 0x001C2110 File Offset: 0x001C1110
	private void ᜀ(int A_0, spr\u23A5 A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			BiffRecordRaw biffRecordRaw;
			int num4;
			int num3;
			for (;;)
			{
				int num2;
				bool flag;
				switch (num)
				{
				case 0:
					num = 25;
					continue;
				case 1:
					if (biffRecordRaw == null)
					{
						num = 16;
						continue;
					}
					num = 9;
					continue;
				case 2:
					goto IL_D8;
				case 4:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord == TBIFFRecord.String)
					{
						num = 19;
						continue;
					}
					goto IL_128;
				}
				case 5:
					if (num2 < this.ᜇ)
					{
						num = 27;
						continue;
					}
					goto IL_128;
				case 6:
					num3 = (int)(this.ᜈ.ReadInt16(num4 + 2) + 4);
					num2 = num4 + num3;
					num = 5;
					continue;
				case 7:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord == TBIFFRecord.Array)
					{
						num = 29;
						continue;
					}
					goto IL_D8;
				}
				case 8:
					if (!this.ᜤ())
					{
						num = 22;
						continue;
					}
					goto IL_202;
				case 9:
					goto IL_275;
				case 10:
					goto IL_FE;
				case 11:
					goto IL_17D;
				case 12:
					goto IL_202;
				case 13:
					if (num2 < this.ᜇ)
					{
						num = 24;
						continue;
					}
					goto IL_D8;
				case 14:
				{
					TBIFFRecord typeCode;
					if (typeCode != TBIFFRecord.RK)
					{
						num = 0;
						continue;
					}
					goto IL_FE;
				}
				case 15:
					if (A_1 != null)
					{
						num = 18;
						continue;
					}
					goto IL_17D;
				case 16:
					goto IL_14F;
				case 17:
					num = 8;
					continue;
				case 18:
					num = 28;
					continue;
				case 19:
					num = 15;
					continue;
				case 20:
					if (flag)
					{
						num = 6;
						continue;
					}
					this.ᜋ = -1;
					this.ᜊ = -1;
					num = 26;
					continue;
				case 21:
					goto IL_1AF;
				case 22:
				{
					TBIFFRecord typeCode = A_1.get_TypeCode();
					num = 14;
					continue;
				}
				case 23:
					goto IL_128;
				case 24:
				{
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num = 2;
					continue;
				}
				case 25:
				{
					TBIFFRecord typeCode;
					if (typeCode == TBIFFRecord.Blank)
					{
						num = 10;
						continue;
					}
					goto IL_202;
				}
				case 26:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_14F;
					default:
						if (false)
						{
						}
						goto IL_128;
					}
					break;
				case 27:
				{
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num = 7;
					continue;
				}
				case 28:
					if (A_1.get_TypeCode() != TBIFFRecord.Formula)
					{
						num = 11;
						continue;
					}
					goto IL_128;
				case 29:
				{
					int num5 = (int)this.ᜈ.ReadInt16(num2 + 2);
					int num6 = num5 + 4;
					num2 += num6;
					num3 += num6;
					num = 13;
					continue;
				}
				}
				if (A_1 != null)
				{
					num = 17;
					continue;
				}
				goto IL_202;
				IL_D8:
				num = 4;
				continue;
				IL_FE:
				if (true)
				{
				}
				this.ᜆ(true);
				num = 12;
				continue;
				IL_128:
				biffRecordRaw = (BiffRecordRaw)A_1;
				num = 1;
				continue;
				IL_14F:
				num = 21;
				continue;
				IL_17D:
				int num7 = (int)this.ᜈ.ReadInt16(num2 + 2);
				num3 += num7 + 4;
				num = 23;
				continue;
				IL_202:
				num4 = this.ᜀ(A_0, out flag);
				num3 = 0;
				num = 20;
			}
			IL_1AF:
			int num8 = 0;
			goto IL_391;
			IL_275:
			num8 = biffRecordRaw.GetStoreSize(this.ᜆ()) + 4;
			IL_391:
			int a_ = num8;
			this.ᜀ(num4, num3, a_, biffRecordRaw, A_2);
			this.ᜀ(A_0, A_1);
			return;
		}
		}
	}

	// Token: 0x060030C3 RID: 12483 RVA: 0x001C24C8 File Offset: 0x001C14C8
	private int ᜀ(sprᱧ.ᜀ A_0, sprᱧ.ᜀ A_1, sprᱧ.ᜀ A_2, object A_3)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_13A;
					case 1:
						goto IL_C9;
					case 2:
						goto IL_13A;
					case 3:
						if (true)
						{
						}
						num2 = 5;
						continue;
					case 4:
					{
						if (num >= this.ᜇ)
						{
							num2 = 6;
							continue;
						}
						TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num);
						int num3 = (int)this.ᜈ.ReadInt16(num + 2);
						TBIFFRecord tbiffrecord2 = tbiffrecord;
						num2 = 7;
						continue;
					}
					case 5:
					{
						TBIFFRecord tbiffrecord2;
						if (tbiffrecord2 == TBIFFRecord.RK)
						{
							num2 = 9;
							continue;
						}
						goto IL_C9;
					}
					case 6:
						return num;
					case 7:
					{
						TBIFFRecord tbiffrecord2;
						if (tbiffrecord2 <= TBIFFRecord.String)
						{
							num2 = 15;
							continue;
						}
						num2 = 11;
						continue;
					}
					case 8:
					{
						TBIFFRecord tbiffrecord2;
						if (tbiffrecord2 != TBIFFRecord.Blank)
						{
							num2 = 10;
							continue;
						}
						num = A_1(A_3);
						num2 = 12;
						continue;
					}
					case 9:
						num = A_0(A_3);
						num2 = 2;
						continue;
					case 10:
						num2 = 13;
						continue;
					case 11:
					{
						TBIFFRecord tbiffrecord2;
						if (tbiffrecord2 != TBIFFRecord.Array)
						{
							num2 = 3;
							continue;
						}
						goto IL_C9;
					}
					case 12:
						goto IL_13A;
					case 13:
					{
						TBIFFRecord tbiffrecord2;
						if (tbiffrecord2 != TBIFFRecord.String)
						{
							goto IL_1AE;
						}
						goto IL_C9;
					}
					case 14:
						num2 = 1;
						continue;
					case 15:
						num2 = 8;
						continue;
					case 16:
						goto IL_13A;
					}
					break;
					IL_C9:
					num = A_2(A_3);
					num2 = 16;
					continue;
					IL_13A:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_1AE:
						num2 = 14;
						continue;
					}
					if (false)
					{
					}
					num2 = 4;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x060030C4 RID: 12484 RVA: 0x001C26C4 File Offset: 0x001C16C4
	private int ᜅ(object A_0)
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
		sprᱧ.ᜁ ᜁ = (sprᱧ.ᜁ)A_0;
		int num = ᜁ.ᜀ;
		int num2 = (int)this.ᜈ.ReadInt16(num + 2);
		int num3 = num2 + 4;
		ᜁ.ᜁ += num3;
		num += num3;
		ᜁ.ᜀ = num;
		return num;
	}

	// Token: 0x060030C5 RID: 12485 RVA: 0x001C273C File Offset: 0x001C173C
	private int ᜄ(object A_0)
	{
		switch (0)
		{
		default:
		{
			sprᱧ.ᜁ ᜁ;
			int num;
			int num5;
			for (;;)
			{
				ᜁ = (sprᱧ.ᜁ)A_0;
				num = ᜁ.ᜀ;
				int num2 = this.ᜉ(num);
				int num3 = num2;
				int num4 = (int)(4 + this.ᜈ.ReadInt16(num + 2));
				num += num4;
				num5 = ᜁ.ᜁ;
				int num6 = 5;
				for (;;)
				{
					int num7;
					switch (num6)
					{
					case 0:
						if (num7 > 1)
						{
							num6 = 1;
							continue;
						}
						num5 += num4;
						num6 = 2;
						continue;
					case 1:
						num5 += 6 * num7 + 6 + 4;
						num6 = 10;
						continue;
					case 2:
						goto IL_131;
					case 3:
					{
						if (num >= this.ᜇ)
						{
							num6 = 8;
							continue;
						}
						TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num);
						num6 = 11;
						continue;
					}
					case 4:
					{
						if (true)
						{
						}
						num += (int)(4 + this.ᜈ.ReadInt16(num + 2));
						int num8;
						num3 = num8;
						goto IL_1B0;
					}
					case 5:
						goto IL_B2;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1B0;
						default:
						{
							if (false)
							{
							}
							int num8;
							if (num3 + 1 == num8)
							{
								num6 = 4;
								continue;
							}
							goto IL_89;
						}
						}
						break;
					case 7:
						goto IL_B2;
					case 8:
						goto IL_89;
					case 9:
					{
						int num8 = this.ᜉ(num);
						num6 = 6;
						continue;
					}
					case 10:
						goto IL_190;
					case 11:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.RK)
						{
							num6 = 9;
							continue;
						}
						goto IL_89;
					}
					}
					break;
					IL_89:
					num7 = num3 - num2 + 1;
					num6 = 0;
					continue;
					IL_B2:
					num6 = 3;
					continue;
					IL_1B0:
					num6 = 7;
				}
			}
			IL_131:
			IL_190:
			ᜁ.ᜁ = num5;
			ᜁ.ᜀ = num;
			return num;
		}
		}
	}

	// Token: 0x060030C6 RID: 12486 RVA: 0x001C291C File Offset: 0x001C191C
	private int ᜃ(object A_0)
	{
		switch (0)
		{
		default:
		{
			sprᱧ.ᜁ ᜁ;
			int num;
			int num5;
			for (;;)
			{
				ᜁ = (sprᱧ.ᜁ)A_0;
				num = ᜁ.ᜀ;
				int num2 = this.ᜉ(num);
				int num3 = num2;
				int num4 = (int)(4 + this.ᜈ.ReadInt16(num + 2));
				num += num4;
				num5 = ᜁ.ᜁ;
				int num6 = 5;
				for (;;)
				{
					int num8;
					switch (num6)
					{
					case 0:
						goto IL_89;
					case 1:
					{
						int num7;
						if (num3 + 1 == num7)
						{
							num6 = 3;
							continue;
						}
						goto IL_89;
					}
					case 2:
						goto IL_BC;
					case 3:
					{
						int num7;
						num3 = num7;
						num += (int)(4 + this.ᜈ.ReadInt16(num + 2));
						num6 = 2;
						continue;
					}
					case 4:
					{
						int num7 = this.ᜉ(num);
						num6 = 1;
						continue;
					}
					case 5:
						goto IL_BC;
					case 6:
						goto IL_13B;
					case 7:
						if (num8 > 1)
						{
							num6 = 9;
							continue;
						}
						num5 += num4;
						num6 = 6;
						continue;
					case 8:
						goto IL_192;
					case 9:
						goto IL_179;
					case 10:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.Blank)
						{
							num6 = 4;
							continue;
						}
						goto IL_89;
					}
					case 11:
					{
						if (num >= this.ᜇ)
						{
							num6 = 0;
							continue;
						}
						TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num);
						num6 = 10;
						continue;
					}
					}
					break;
					IL_89:
					num8 = num3 - num2 + 1;
					num6 = 7;
					continue;
					IL_BC:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_179:
						num5 += 2 * num8 + 6 + 4;
						num6 = 8;
						break;
					default:
						if (false)
						{
						}
						num6 = 11;
						break;
					}
				}
			}
			IL_13B:
			if (true)
			{
			}
			IL_192:
			ᜁ.ᜁ = num5;
			ᜁ.ᜀ = num;
			return num;
		}
		}
	}

	// Token: 0x060030C7 RID: 12487 RVA: 0x001C2AF4 File Offset: 0x001C1AF4
	private int ᜂ(object A_0)
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
		sprᱧ.ᜂ ᜂ = (sprᱧ.ᜂ)A_0;
		int num = ᜂ.ᜀ;
		int num2 = (int)this.ᜈ.ReadInt16(num + 2);
		int num3 = num2 + 4;
		this.ᜈ.MoveMemory(ᜂ.ᜁ, num, num3);
		ᜂ.ᜁ += num3;
		num += num3;
		ᜂ.ᜀ = num;
		return num;
	}

	// Token: 0x060030C8 RID: 12488 RVA: 0x001C2B80 File Offset: 0x001C1B80
	private int ᜁ(object A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				if (true)
				{
				}
				sprᱧ.ᜂ ᜂ = (sprᱧ.ᜂ)A_0;
				num = ᜂ.ᜀ;
				int num2 = ᜂ.ᜁ;
				int num3 = this.ᜉ(num);
				int num4 = num3;
				int num5 = (int)(4 + this.ᜈ.ReadInt16(num + 2));
				num += num5;
				int num6 = 9;
				for (;;)
				{
					int num8;
					switch (num6)
					{
					case 0:
					{
						int num7;
						if (num4 + 1 == num7)
						{
							num6 = 3;
							continue;
						}
						goto IL_90;
					}
					case 1:
					{
						if (num >= this.ᜇ)
						{
							num6 = 5;
							continue;
						}
						TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num);
						num6 = 8;
						continue;
					}
					case 2:
						return num;
					case 3:
					{
						num += (int)(4 + this.ᜈ.ReadInt16(num + 2));
						int num7;
						num4 = num7;
						num6 = 6;
						continue;
					}
					case 4:
						return num;
					case 5:
						goto IL_90;
					case 6:
						goto IL_C3;
					case 7:
						if (num8 > 1)
						{
							num6 = 11;
							continue;
						}
						num = this.ᜂ(A_0);
						num6 = 4;
						continue;
					case 8:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.RK)
						{
							num6 = 10;
							continue;
						}
						goto IL_90;
					}
					case 9:
						goto IL_C3;
					case 10:
					{
						int num7 = this.ᜉ(num);
						num6 = 0;
						continue;
					}
					case 11:
						goto IL_179;
					}
					break;
					IL_90:
					num8 = num4 - num3 + 1;
					num6 = 7;
					continue;
					IL_C3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_179:
						this.ᜀ(ᜂ, num8);
						this.ᜈ(true);
						num6 = 2;
						break;
					default:
						if (false)
						{
						}
						num6 = 1;
						break;
					}
				}
			}
			return num;
		}
		}
	}

	// Token: 0x060030C9 RID: 12489 RVA: 0x001C2D50 File Offset: 0x001C1D50
	private int ᜀ(object A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				sprᱧ.ᜂ ᜂ = (sprᱧ.ᜂ)A_0;
				num = ᜂ.ᜀ;
				int num2 = ᜂ.ᜁ;
				int num3 = this.ᜉ(num);
				int num4 = num3;
				int num5 = (int)(4 + this.ᜈ.ReadInt16(num + 2));
				num += num5;
				int num6 = 8;
				for (;;)
				{
					int num8;
					switch (num6)
					{
					case 0:
						return num;
					case 1:
						goto IL_144;
					case 2:
					{
						num += (int)(4 + this.ᜈ.ReadInt16(num + 2));
						int num7;
						num4 = num7;
						num6 = 4;
						continue;
					}
					case 3:
					{
						if (num >= this.ᜇ)
						{
							num6 = 5;
							continue;
						}
						TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num);
						num6 = 10;
						continue;
					}
					case 4:
						goto IL_C0;
					case 5:
						goto IL_89;
					case 6:
						if (num8 > 1)
						{
							num6 = 11;
							continue;
						}
						num = this.ᜂ(A_0);
						num6 = 1;
						continue;
					case 7:
					{
						int num7;
						if (num4 + 1 == num7)
						{
							num6 = 2;
							continue;
						}
						goto IL_89;
					}
					case 8:
						goto IL_C0;
					case 9:
					{
						int num7 = this.ᜉ(num);
						num6 = 7;
						continue;
					}
					case 10:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.Blank)
						{
							num6 = 9;
							continue;
						}
						goto IL_89;
					}
					case 11:
						goto IL_188;
					}
					break;
					IL_89:
					num8 = num4 - num3 + 1;
					num6 = 6;
					continue;
					IL_C0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_188:
						int num9 = 2 * num8 + 6;
						BiffRecordRaw biffRecordRaw = this.ᜃ(ᜂ.ᜀ, num8);
						this.ᜈ.WriteInt16(num2, (short)biffRecordRaw.RecordCode);
						num2 += 2;
						this.ᜈ.WriteInt16(num2, (short)num9);
						num2 += 2;
						biffRecordRaw.InfillInternalData(this.ᜈ, num2, this.ᜆ());
						num2 += num9;
						num9 += 4;
						ᜂ.ᜁ += num9;
						ᜂ.ᜀ = num;
						this.ᜈ(true);
						num6 = 0;
						break;
					}
					default:
						if (false)
						{
						}
						num6 = 3;
						break;
					}
				}
			}
			IL_144:
			if (true)
			{
			}
			return num;
		}
		}
	}

	// Token: 0x060030CA RID: 12490 RVA: 0x001C2F9C File Offset: 0x001C1F9C
	private sprᨾ ᜀ(sprᱧ.ᜂ A_0, int A_1)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num2;
			int num4;
			short value;
			for (;;)
			{
				IL_17:
				int num = 7;
				for (;;)
				{
					int num3;
					byte[] array;
					switch (num)
					{
					case 0:
						goto IL_7D;
					case 1:
						if (num2 < this.ᜋ)
						{
							num = 8;
							continue;
						}
						goto IL_154;
					case 2:
						goto IL_F1;
					case 3:
						goto IL_F1;
					case 4:
						goto IL_111;
					case 5:
						if (num3 >= A_1)
						{
							num = 4;
							continue;
						}
						this.ᜈ.ReadArray(num2, array);
						this.ᜈ.WriteBytes(num4, array, 0, 6);
						num2 += 14;
						num4 += 6;
						num3++;
						num = 2;
						continue;
					case 6:
						goto IL_154;
					case 8:
						if (true)
						{
						}
						this.ᜋ = -1;
						this.ᜊ = -1;
						num = 6;
						continue;
					}
					if (this.ᜆ() == ExcelVersion.Version97to2003)
					{
						num2 = A_0.ᜀ;
						num4 = A_0.ᜁ;
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_17;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					IL_F1:
					num = 5;
					continue;
					IL_154:
					this.ᜈ.WriteInt16(num4, 189);
					num2 += 2;
					num4 += 2;
					int num5 = 6 + 6 * A_1;
					this.ᜈ.WriteInt16(num4, (short)num5);
					num2 += 2;
					num4 += 2;
					array = new byte[6];
					this.ᜈ.ReadArray(num2, array, 4);
					short num6 = this.ᜈ.ReadInt16(num2 + 2);
					value = (short)((int)num6 + A_1 - 1);
					this.ᜈ.WriteBytes(num4, array, 0, 4);
					num4 += 4;
					num2 = A_0.ᜀ + 4 + 4;
					num3 = 0;
					num = 3;
				}
			}
			IL_7D:
			throw new NotSupportedException(RecordTableEnumerator.b("樽⠿⭁㝃晅╇⽉㡋♍㽏㙑瑓㽕⭗穙⽛⭝ၟቡୣᑥᱧཀྵ࡫乭Ὧᱱᡳཱུ塷ᱹ፻౽ꁿ잁ﲃ떋릍붏ꂑ꒓ꚕꮗ몙瀞첟잡蒣삥잧\ud8a9솫쾭쒯", a_));
			IL_111:
			this.ᜈ.WriteInt16(num4, value);
			num4 += 2;
			A_0.ᜁ = num4;
			A_0.ᜀ = num2 - 4 - 4;
			return null;
		}
		}
	}

	// Token: 0x060030CB RID: 12491 RVA: 0x001C31B8 File Offset: 0x001C21B8
	private sprᨾ ᜄ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			List<sprᨾ.ᜀ> list;
			sprᨾ sprᨾ;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
				{
					if (num2 >= A_1)
					{
						num = 2;
						continue;
					}
					ushort a_ = this.ᜀ(A_0, false);
					int a_2 = this.ᜈ.ReadInt32(A_0 + 10);
					sprᨾ.ᜀ item = new sprᨾ.ᜀ(a_, a_2);
					list.Add(item);
					A_0 += 14;
					num2++;
					num = 5;
					continue;
				}
				case 2:
					goto IL_157;
				case 3:
					goto IL_138;
				case 4:
					goto IL_A2;
				case 5:
					goto IL_138;
				case 6:
					this.ᜋ = -1;
					this.ᜊ = -1;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				if (true)
				{
				}
				if (A_0 < this.ᜋ)
				{
					num = 6;
					continue;
				}
				IL_A2:
				sprᨾ = (sprᨾ)spr\u175E.ᜀ(TBIFFRecord.MulRK);
				sprᨾ.ᜇ(this.\u171A(A_0));
				sprᨾ.ᜂ(this.ᜉ(A_0));
				sprᨾ.ᜁ(sprᨾ.ᜅ() + A_1 - 1);
				list = new List<sprᨾ.ᜀ>(A_1);
				num2 = 0;
				num = 3;
				continue;
				IL_138:
				num = 0;
			}
			IL_157:
			sprᨾ.ᜀ(list);
			return sprᨾ;
		}
		}
	}

	// Token: 0x060030CC RID: 12492 RVA: 0x001C3328 File Offset: 0x001C2328
	private sprᲀ ᜃ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			List<ushort> list;
			sprᲀ sprᲀ;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_119;
				case 1:
					if (true)
					{
					}
					goto IL_119;
				case 2:
					goto IL_138;
				case 4:
					this.ᜋ = -1;
					this.ᜊ = -1;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 5:
					goto IL_83;
				case 6:
				{
					if (num2 >= A_1)
					{
						num = 2;
						continue;
					}
					ushort item = this.ᜀ(A_0, false);
					list.Add(item);
					A_0 += 10;
					num2++;
					num = 1;
					continue;
				}
				}
				if (A_0 < this.ᜋ)
				{
					num = 4;
					continue;
				}
				IL_83:
				sprᲀ = (sprᲀ)spr\u175E.ᜀ(TBIFFRecord.MulBlank);
				sprᲀ.ᜇ(this.\u171A(A_0));
				sprᲀ.ᜂ(this.ᜉ(A_0));
				sprᲀ.ᜁ(sprᲀ.ᜆ() + A_1 - 1);
				list = new List<ushort>(A_1);
				num2 = 0;
				num = 0;
				continue;
				IL_119:
				num = 6;
			}
			IL_138:
			sprᲀ.ᜀ(list);
			return sprᲀ;
		}
		}
	}

	// Token: 0x060030CD RID: 12493 RVA: 0x001C3478 File Offset: 0x001C2478
	private List<int> ᜀ(sprᲀ A_0, sprᨾ A_1, out int A_2)
	{
		switch (0)
		{
		default:
		{
			List<int> list;
			for (;;)
			{
				A_2 = 0;
				int num = 0;
				list = new List<int>();
				int num2 = 1;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return list;
						default:
							if (false)
							{
							}
							goto IL_ED;
						}
						break;
					case 1:
						goto IL_ED;
					case 2:
						goto IL_56;
					case 3:
					{
						if (num >= this.ᜇ)
						{
							num2 = 6;
							continue;
						}
						sprᤞ sprᤞ = this.ᜀ(num, A_0, A_1, out num3);
						num2 = 5;
						continue;
					}
					case 4:
					{
						sprᤞ sprᤞ;
						A_2 += (sprᤞ.ᜁ() - sprᤞ.ᜀ() + 1) * sprᤞ.ᜀ(this.ᜆ()) - num3;
						list.Add(num);
						num2 = 2;
						continue;
					}
					case 5:
					{
						sprᤞ sprᤞ;
						if (sprᤞ != null)
						{
							num2 = 4;
							continue;
						}
						goto IL_56;
					}
					case 6:
						return list;
					}
					break;
					IL_56:
					num += num3;
					if (true)
					{
					}
					num2 = 0;
					continue;
					IL_ED:
					num2 = 3;
				}
			}
			return list;
		}
		}
	}

	// Token: 0x060030CE RID: 12494 RVA: 0x001C3598 File Offset: 0x001C2598
	private void ᜀ(List<int> A_0, int A_1, sprᲀ A_2, sprᨾ A_3, bool A_4)
	{
		int num2;
		int num;
		int num3;
		sprᤞ sprᤞ;
		int num4;
		int num6;
		int num7;
		int num8;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_FA:
			num = A_0[num2];
			sprᤞ = this.ᜀ(num, A_2, A_3, out num3);
			num4 = num + num3;
			int num5 = num6 - A_1;
			num7 = num5 - num4;
			num8 = 2;
			break;
		}
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_59;
			}
			break;
		}
		for (;;)
		{
			IL_36:
			switch (num8)
			{
			case 0:
				goto IL_85;
			case 1:
				if (num2 < 0)
				{
					num8 = 3;
					continue;
				}
				goto IL_FA;
			case 2:
				if (num7 > 0)
				{
					if (true)
					{
					}
					num8 = 4;
					continue;
				}
				goto IL_85;
			case 3:
				return;
			case 4:
				this.ᜈ.MoveMemory(num4 + A_1, num4, num7);
				num8 = 0;
				continue;
			case 5:
				goto IL_14B;
			case 6:
				goto IL_14B;
			}
			goto IL_59;
			IL_85:
			int num9 = (sprᤞ.ᜁ() - sprᤞ.ᜀ() + 1) * sprᤞ.ᜀ(this.ᜆ());
			BiffRecordRaw[] a_ = sprᤞ.ᜀ(A_4);
			this.ᜀ(num, a_);
			num6 = num4 + A_1;
			A_1 -= num9 - num3;
			num2--;
			num8 = 6;
			continue;
			IL_14B:
			num8 = 1;
		}
		return;
		IL_59:
		this.ᜇ += A_1;
		num6 = this.ᜇ;
		num2 = A_0.Count - 1;
		num8 = 5;
		goto IL_36;
	}

	// Token: 0x060030CF RID: 12495 RVA: 0x001C3714 File Offset: 0x001C2714
	private sprᤞ ᜀ(int A_0, sprᲀ A_1, sprᨾ A_2, out int A_3)
	{
		sprᤞ result;
		for (;;)
		{
			for (;;)
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
					result = null;
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(A_0);
					int num = (int)this.ᜈ.ReadInt16(A_0 + 2);
					A_3 = num + 4;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (tbiffrecord == TBIFFRecord.MulRK)
							{
								num2 = 5;
								continue;
							}
							num2 = 3;
							continue;
						case 1:
							return result;
						case 2:
							goto IL_AE;
						case 3:
							if (tbiffrecord == TBIFFRecord.MulBlank)
							{
								num2 = 4;
								continue;
							}
							return result;
						case 4:
							A_1.Length = num;
							A_1.ParseStructure(this.ᜈ, A_0 + 4, 0, this.ᜆ());
							result = A_1;
							num2 = 2;
							continue;
						case 5:
							A_2.Length = num;
							A_2.ParseStructure(this.ᜈ, A_0 + 4, 0, this.ᜆ());
							result = A_2;
							num2 = 1;
							continue;
						}
						break;
					}
					break;
				}
				}
			}
		}
		IL_AE:
		if (true)
		{
		}
		return result;
	}

	// Token: 0x060030D0 RID: 12496 RVA: 0x001C3828 File Offset: 0x001C2828
	private void ᜀ(int A_0, sprᤞ A_1, bool A_2)
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
		BiffRecordRaw[] a_ = A_1.ᜀ(A_2);
		this.ᜀ(A_0, a_);
	}

	// Token: 0x060030D1 RID: 12497 RVA: 0x001C3874 File Offset: 0x001C2874
	public int \u171A(int A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			ExcelVersion excelVersion = this.ᜆ();
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_96;
				case 1:
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						break;
					}
					IL_85:
					if (false)
					{
					}
					num = 0;
					continue;
					goto IL_85;
				case 2:
					switch (excelVersion)
					{
					case ExcelVersion.Version97to2003:
						goto IL_56;
					case ExcelVersion.Version2007:
					case ExcelVersion.Version2010:
						goto IL_47;
					default:
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_47:
		return this.ᜈ.ReadInt32(A_0 + 4);
		IL_56:
		return (int)this.ᜈ.ReadUInt16(A_0 + 4);
		IL_96:
		throw new NotImplementedException();
	}

	// Token: 0x060030D2 RID: 12498 RVA: 0x001C3920 File Offset: 0x001C2920
	private void ᜂ(int A_0, int A_1)
	{
		for (;;)
		{
			ExcelVersion excelVersion = this.ᜆ();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (excelVersion)
					{
					case ExcelVersion.Version97to2003:
						goto IL_4F;
					case ExcelVersion.Version2007:
					case ExcelVersion.Version2010:
						goto IL_3F;
					default:
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_99;
				case 2:
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_76;
						}
					}
					IL_76:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_3F:
		this.ᜈ.WriteInt32(A_0 + 4, A_1);
		return;
		IL_4F:
		this.ᜈ.WriteUInt16(A_0 + 4, (ushort)A_1);
		return;
		IL_99:
		throw new NotImplementedException();
	}

	// Token: 0x060030D3 RID: 12499 RVA: 0x001C39D0 File Offset: 0x001C29D0
	public int ᜉ(int A_0)
	{
		for (;;)
		{
			ExcelVersion excelVersion = this.ᜆ();
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9A;
				case 1:
					for (;;)
					{
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						break;
					}
					IL_89:
					if (false)
					{
					}
					num = 0;
					continue;
					goto IL_89;
				case 2:
					switch (excelVersion)
					{
					case ExcelVersion.Version97to2003:
						goto IL_50;
					case ExcelVersion.Version2007:
					case ExcelVersion.Version2010:
						goto IL_3F;
					default:
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_3F:
		return this.ᜈ.ReadInt32(A_0 + 4 + 4);
		IL_50:
		return (int)this.ᜈ.ReadUInt16(A_0 + 4 + 2);
		IL_9A:
		throw new NotImplementedException();
	}

	// Token: 0x060030D4 RID: 12500 RVA: 0x001C3A80 File Offset: 0x001C2A80
	private void ᜁ(int A_0, int A_1)
	{
		for (;;)
		{
			if (true)
			{
			}
			ExcelVersion excelVersion = this.ᜆ();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (excelVersion)
					{
					case ExcelVersion.Version97to2003:
						goto IL_59;
					case ExcelVersion.Version2007:
					case ExcelVersion.Version2010:
						goto IL_47;
					default:
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_9D;
				case 2:
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						break;
					}
					IL_8C:
					if (false)
					{
					}
					num = 1;
					continue;
					goto IL_8C;
				}
				break;
			}
		}
		IL_47:
		this.ᜈ.WriteInt32(A_0 + 4 + 4, A_1);
		return;
		IL_59:
		this.ᜈ.WriteUInt16(A_0 + 4 + 2, (ushort)A_1);
		return;
		IL_9D:
		throw new NotImplementedException();
	}

	// Token: 0x060030D5 RID: 12501 RVA: 0x001C3B34 File Offset: 0x001C2B34
	private int ᜀ(int A_0, int A_1)
	{
		for (;;)
		{
			ExcelVersion excelVersion = this.ᜆ();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9E;
				case 1:
					switch (excelVersion)
					{
					case ExcelVersion.Version97to2003:
						goto IL_52;
					case ExcelVersion.Version2007:
					case ExcelVersion.Version2010:
						goto IL_3F;
					default:
						num = 2;
						continue;
					}
					break;
				case 2:
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_7B;
						}
					}
					IL_7B:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_3F:
		return this.ᜈ.ReadInt32(A_0 + 4 + A_1 - 4);
		IL_52:
		return (int)this.ᜈ.ReadInt16(A_0 + 4 + A_1 - 2);
		IL_9E:
		throw new NotImplementedException();
	}

	// Token: 0x060030D6 RID: 12502 RVA: 0x001C3BE8 File Offset: 0x001C2BE8
	[CLSCompliant(false)]
	public ushort ᜀ(int A_0, bool A_1)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_58;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_50;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 2:
			{
				ExcelVersion excelVersion;
				if (excelVersion == ExcelVersion.Version2010)
				{
					num = 5;
					continue;
				}
				goto IL_AE;
			}
			case 4:
			{
				ExcelVersion excelVersion;
				if (excelVersion != ExcelVersion.Version2007)
				{
					num = 1;
					continue;
				}
				goto IL_4B;
			}
			case 5:
				goto IL_4B;
			case 6:
			{
				A_0 += 8;
				ExcelVersion excelVersion = this.ᜆ();
				num = 4;
				continue;
			}
			}
			if (true)
			{
			}
			if (!A_1)
			{
				num = 6;
				continue;
			}
			break;
			IL_50:
			num = 0;
			continue;
			IL_4B:
			A_0 += 4;
			goto IL_50;
		}
		IL_58:
		IL_AE:
		return this.ᜈ.ReadUInt16(A_0);
	}

	// Token: 0x060030D7 RID: 12503 RVA: 0x001C3CB0 File Offset: 0x001C2CB0
	private void ᜀ(int A_0, ushort A_1)
	{
		if (true)
		{
		}
		ExcelVersion excelVersion;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_55:
			if (excelVersion == ExcelVersion.Version2007)
			{
				goto IL_63;
			}
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_40;
		}
		for (;;)
		{
			IL_26:
			switch (num)
			{
			case 0:
				goto IL_6F;
			case 1:
				num = 4;
				continue;
			case 2:
				goto IL_85;
			case 3:
				goto IL_55;
			case 4:
				if (excelVersion == ExcelVersion.Version2010)
				{
					num = 2;
					continue;
				}
				goto IL_91;
			}
			goto IL_40;
		}
		IL_6F:
		goto IL_91;
		IL_85:
		goto IL_63;
		IL_91:
		int num2;
		this.ᜈ.WriteUInt16(num2, A_1);
		return;
		IL_40:
		num2 = A_0 + 4 + 4;
		excelVersion = this.ᜆ();
		num = 3;
		goto IL_26;
		IL_63:
		num2 += 4;
		num = 0;
		goto IL_26;
	}

	// Token: 0x060030D8 RID: 12504 RVA: 0x001C3D5C File Offset: 0x001C2D5C
	internal void ᜎ()
	{
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_4C:
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A9;
				case 1:
					spr᱒.ᜀ(this.ᜈ, num2);
					num = 6;
					continue;
				case 2:
					return;
				case 3:
				{
					if (num2 >= this.ᜇ)
					{
						num = 2;
						continue;
					}
					TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num2);
					num = 5;
					continue;
				}
				case 4:
					goto IL_A9;
				case 5:
				{
					TBIFFRecord tbiffrecord;
					if (tbiffrecord == TBIFFRecord.Formula)
					{
						num = 1;
						continue;
					}
					goto IL_56;
				}
				case 6:
					goto IL_56;
				}
				goto IL_4A;
				IL_56:
				if (true)
				{
				}
				num2 = this.ᜃ(num2);
				num = 0;
				continue;
				IL_A9:
				num = 3;
			}
			return;
		}
		default:
			if (false)
			{
			}
			break;
		}
		IL_4A:
		num2 = 0;
		goto IL_4C;
	}

	// Token: 0x060030D9 RID: 12505 RVA: 0x001C3E34 File Offset: 0x001C2E34
	private void ᜀ(int A_0, ushort A_1, int A_2, int A_3)
	{
		ExcelVersion excelVersion;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_5C:
			if (excelVersion == ExcelVersion.Version2007)
			{
				goto IL_6A;
			}
			num = 4;
			break;
		default:
			if (false)
			{
			}
			goto IL_38;
		}
		for (;;)
		{
			IL_1E:
			switch (num)
			{
			case 0:
				goto IL_77;
			case 1:
				if (excelVersion == ExcelVersion.Version2010)
				{
					num = 3;
					continue;
				}
				goto IL_99;
			case 2:
				goto IL_5C;
			case 3:
				goto IL_8D;
			case 4:
				num = 1;
				continue;
			}
			goto IL_38;
		}
		IL_77:
		goto IL_99;
		IL_8D:
		goto IL_6A;
		IL_99:
		int num2;
		A_0 += A_3 * (A_2 - num2);
		this.ᜈ.WriteUInt16(A_0, A_1);
		return;
		IL_38:
		if (true)
		{
		}
		num2 = this.ᜉ(A_0);
		A_0 += 8;
		excelVersion = this.ᜆ();
		num = 2;
		goto IL_1E;
		IL_6A:
		A_0 += 4;
		num = 0;
		goto IL_1E;
	}

	// Token: 0x060030DA RID: 12506 RVA: 0x001C3EF4 File Offset: 0x001C2EF4
	public TBIFFRecord ᜉ()
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
		return (TBIFFRecord)(-1);
	}

	// Token: 0x060030DB RID: 12507 RVA: 0x001C3F30 File Offset: 0x001C2F30
	public int ᜊ()
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
		return -1;
	}

	// Token: 0x060030DC RID: 12508 RVA: 0x001C3F6C File Offset: 0x001C2F6C
	public bool ᜐ()
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
		return true;
	}

	// Token: 0x060030DD RID: 12509 RVA: 0x001C3FA8 File Offset: 0x001C2FA8
	public long \u171A()
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
		return -1L;
	}

	// Token: 0x060030DE RID: 12510 RVA: 0x001C3FE8 File Offset: 0x001C2FE8
	public void ᜀ(long A_0)
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
	}

	// Token: 0x060030DF RID: 12511 RVA: 0x001C4024 File Offset: 0x001C3024
	public int ᜀ(ExcelVersion A_0)
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
		this.\u171B();
		return Math.Max(0, this.ᜇ - 4);
	}

	// Token: 0x060030E0 RID: 12512 RVA: 0x001C4074 File Offset: 0x001C3074
	public int ᜀ(BinaryWriter A_0, DataProvider A_1, IEncryptor A_2, int A_3)
	{
		int a_ = 12;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				int num2;
				short value;
				int num3;
				spr\u24E5 spr_u24E;
				int num4;
				byte[] array;
				switch (num)
				{
				case 1:
					num = 5;
					continue;
				case 2:
					if (num2 >= this.ᜇ)
					{
						num = 1;
						continue;
					}
					value = this.ᜈ.ReadInt16(num2);
					num3 = (int)this.ᜈ.ReadUInt16(num2 + 2);
					A_3 += 4;
					num2 += 4;
					num = 11;
					continue;
				case 3:
					if (true)
					{
					}
					goto IL_176;
				case 4:
					spr_u24E.WriteInto(A_0, 0, num4, array);
					num4 = 0;
					num = 14;
					continue;
				case 5:
					if (num4 != 0)
					{
						num = 7;
						continue;
					}
					goto IL_252;
				case 6:
					goto IL_153;
				case 7:
					spr_u24E.WriteInto(A_0, 0, num4, array);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_110;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 8:
				{
					num2 = 0;
					num4 = 0;
					int num5 = array.Length;
					num = 3;
					continue;
				}
				case 9:
					goto IL_1BF;
				case 10:
					goto IL_176;
				case 11:
				{
					int num5;
					if (num5 < num4 + num3 + 4)
					{
						num = 4;
						continue;
					}
					goto IL_96;
				}
				case 12:
					goto IL_74;
				case 13:
					if (A_2 != null)
					{
						goto IL_110;
					}
					this.ᜈ.WriteInto(A_0, 0, this.ᜇ, array);
					num = 9;
					continue;
				case 14:
					goto IL_96;
				}
				if (A_0 == null)
				{
					num = 12;
					continue;
				}
				spr_u24E = (spr\u24E5)A_1;
				array = spr_u24E.ᜅ();
				num = 13;
				continue;
				IL_96:
				spr_u24E.WriteInt16(num4, value);
				num4 += 2;
				spr_u24E.WriteInt16(num4, (short)num3);
				num4 += 2;
				this.ᜈ.CopyTo(num2, array, num4, num3);
				A_2.Encrypt(spr_u24E, num4, num3, (long)A_3);
				num4 += num3;
				A_3 += num3;
				num2 += num3;
				num = 10;
				continue;
				IL_110:
				num = 8;
				continue;
				IL_176:
				num = 2;
			}
			IL_74:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕁㙃⽅㱇⽉㹋", a_));
			IL_153:
			IL_1BF:
			IL_252:
			return this.ᜇ;
		}
		}
	}

	// Token: 0x060030E1 RID: 12513 RVA: 0x001C42DC File Offset: 0x001C32DC
	public int \u1719(int A_0)
	{
		int num2;
		int num;
		int num3;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_DD:
			num = spr\u249B.ᜂ(this.ᜈ, num2, this.ᜆ());
			num3 = 5;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_55;
			}
			break;
		}
		bool flag;
		for (;;)
		{
			IL_36:
			switch (num3)
			{
			case 0:
				goto IL_DD;
			case 1:
				return num;
			case 2:
			{
				int num4;
				if (num4 == 517)
				{
					num3 = 0;
					continue;
				}
				return 0;
			}
			case 3:
				if (flag)
				{
					num3 = 4;
					continue;
				}
				return 0;
			case 4:
			{
				int num4 = (int)this.ᜈ.ReadInt16(num2);
				num3 = 2;
				continue;
			}
			case 5:
				if ((num & 65280) == 0)
				{
					num3 = 1;
					continue;
				}
				return 0;
			}
			goto IL_55;
		}
		return num;
		IL_55:
		num2 = this.ᜀ(A_0, out flag);
		if (true)
		{
		}
		num3 = 3;
		goto IL_36;
	}

	// Token: 0x060030E2 RID: 12514 RVA: 0x001C43CC File Offset: 0x001C33CC
	public int ᜏ(int A_0)
	{
		if (true)
		{
		}
		int num2;
		ulong num;
		int num3;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_F4:
			num = (ulong)spr᱒.ᜃ(this.ᜈ, num2, this.ᜆ());
			num3 = 0;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_5D;
			}
			break;
		}
		bool flag;
		for (;;)
		{
			IL_3E:
			switch (num3)
			{
			case 0:
				if ((num & 18446462598732841215UL) == 18446462598732840961UL)
				{
					num3 = 2;
					continue;
				}
				return 0;
			case 1:
			{
				int num4;
				if (num4 == 6)
				{
					num3 = 3;
					continue;
				}
				return 0;
			}
			case 2:
				goto IL_C9;
			case 3:
				goto IL_F4;
			case 4:
				if (flag)
				{
					num3 = 5;
					continue;
				}
				return 0;
			case 5:
			{
				int num4 = (int)this.ᜈ.ReadInt16(num2);
				num3 = 1;
				continue;
			}
			}
			goto IL_5D;
		}
		IL_C9:
		return (int)(num & 16711680UL);
		IL_5D:
		num2 = this.ᜀ(A_0, out flag);
		num3 = 4;
		goto IL_3E;
	}

	// Token: 0x060030E3 RID: 12515 RVA: 0x001C44D0 File Offset: 0x001C34D0
	public string \u170D(int A_0)
	{
		int num2;
		int num;
		int num3;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_EF:
			num = spr\u249B.ᜂ(this.ᜈ, num2, this.ᜆ());
			num3 = 5;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_55;
			}
			break;
		}
		bool flag;
		for (;;)
		{
			IL_36:
			switch (num3)
			{
			case 0:
			{
				int num4 = (int)this.ᜈ.ReadInt16(num2);
				num3 = 4;
				continue;
			}
			case 1:
				if (flag)
				{
					num3 = 0;
					continue;
				}
				goto IL_F1;
			case 2:
				goto IL_EF;
			case 3:
				goto IL_C0;
			case 4:
			{
				int num4;
				if (num4 == 517)
				{
					num3 = 2;
					continue;
				}
				goto IL_F1;
			}
			case 5:
				if ((num & 65280) != 0)
				{
					if (true)
					{
					}
					num3 = 3;
					continue;
				}
				goto IL_F1;
			}
			goto IL_55;
		}
		IL_C0:
		return this.\u171B(num & 255);
		IL_F1:
		return null;
		IL_55:
		num2 = this.ᜀ(A_0, out flag);
		num3 = 1;
		goto IL_36;
	}

	// Token: 0x060030E4 RID: 12516 RVA: 0x001C45D0 File Offset: 0x001C35D0
	public string ᜋ(int A_0)
	{
		int num2;
		ulong num;
		int num3;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_FD:
			num = (ulong)spr᱒.ᜃ(this.ᜈ, num2, this.ᜆ());
			num3 = 1;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_55;
			}
			break;
		}
		bool flag;
		for (;;)
		{
			IL_36:
			switch (num3)
			{
			case 0:
				goto IL_FD;
			case 1:
				if (true)
				{
				}
				if ((num & 18446462598732841215UL) == 18446462598732840962UL)
				{
					num3 = 3;
					continue;
				}
				goto IL_FF;
			case 2:
			{
				int num4;
				if (num4 == 6)
				{
					num3 = 0;
					continue;
				}
				goto IL_FF;
			}
			case 3:
				goto IL_D2;
			case 4:
				if (flag)
				{
					num3 = 5;
					continue;
				}
				goto IL_FF;
			case 5:
			{
				int num4 = (int)this.ᜈ.ReadInt16(num2);
				num3 = 2;
				continue;
			}
			}
			goto IL_55;
		}
		IL_D2:
		return this.\u171B((int)((num & 16711680UL) >> 16));
		IL_FF:
		return null;
		IL_55:
		num2 = this.ᜀ(A_0, out flag);
		num3 = 4;
		goto IL_36;
	}

	// Token: 0x060030E5 RID: 12517 RVA: 0x001C46E0 File Offset: 0x001C36E0
	public double \u171D(int A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			DateTime dateTime;
			for (;;)
			{
				bool flag;
				bool flag2;
				num = this.ᜀ(A_0, out flag, out flag2, true);
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						int a_ = spr\u1C7C.ᜂ(this.ᜈ, num, this.ᜆ());
						string text = this.\u170D.InnerSST[a_].ᜏ();
						string text2 = this.\u170D.ActiveSheet[this.ᜎ, A_0 + 1].NumberFormat;
						XlsRange xlsRange = this.\u170D.ActiveSheet[this.ᜎ, A_0 + 1] as XlsRange;
						num2 = 20;
						continue;
					}
					case 1:
						goto IL_276;
					case 2:
					{
						string text2;
						int num3 = text2.IndexOf(';');
						num2 = 18;
						continue;
					}
					case 3:
						if (flag)
						{
							if (true)
							{
							}
							num2 = 6;
							continue;
						}
						goto IL_32B;
					case 4:
					{
						DateTime dateTime2;
						dateTime = dateTime2;
						num2 = 8;
						continue;
					}
					case 5:
						num2 = 7;
						continue;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num2 = 10;
							continue;
						}
						break;
					case 7:
					{
						XlsRange xlsRange;
						if (xlsRange != null)
						{
							num2 = 11;
							continue;
						}
						goto IL_1A1;
					}
					case 8:
						goto IL_1F4;
					case 9:
					{
						string text;
						XlsRange xlsRange;
						DateTime dateTime2;
						if (xlsRange.ᜁ(text, out dateTime2))
						{
							num2 = 4;
							continue;
						}
						goto IL_1A1;
					}
					case 10:
					{
						if (flag2)
						{
							num2 = 1;
							continue;
						}
						int num4 = (int)this.ᜈ.ReadInt16(num);
						num2 = 19;
						continue;
					}
					case 11:
						num2 = 9;
						continue;
					case 12:
					{
						string text;
						string text2;
						if (DateTime.TryParseExact(text, text2, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
						{
							num2 = 5;
							continue;
						}
						goto IL_32B;
					}
					case 13:
					{
						int num4;
						if (num4 == 253)
						{
							num2 = 0;
							continue;
						}
						goto IL_32B;
					}
					case 14:
						goto IL_131;
					case 15:
						goto IL_23A;
					case 16:
						goto IL_A4;
					case 17:
					{
						int num4;
						if (num4 == 638)
						{
							num2 = 15;
							continue;
						}
						num2 = 13;
						continue;
					}
					case 18:
					{
						int num3;
						if (num3 != -1)
						{
							num2 = 21;
							continue;
						}
						goto IL_A4;
					}
					case 19:
					{
						int num4;
						if (num4 == 515)
						{
							num2 = 14;
							continue;
						}
						num2 = 17;
						continue;
					}
					case 20:
					{
						string text2;
						if (this.ᜀ(text2))
						{
							num2 = 2;
							continue;
						}
						goto IL_32B;
					}
					case 21:
					{
						int num3;
						string text2 = text2.Remove(num3, text2.Length - num3);
						num2 = 16;
						continue;
					}
					}
					break;
					IL_A4:
					num2 = 12;
				}
			}
			IL_131:
			return spr\u19FF.ᜂ(this.ᜈ, num, this.ᜆ());
			IL_1A1:
			return dateTime.ToOADate();
			IL_1F4:
			goto IL_1A1;
			IL_23A:
			int a_2 = sprỔ.ᜂ(this.ᜈ, num, this.ᜆ());
			return sprỔ.ᜂ(a_2);
			IL_276:
			return sprỔ.ᜂ(this.ᜈ.ReadInt32(num + 2));
			IL_32B:
			return double.NaN;
		}
		}
	}

	// Token: 0x060030E6 RID: 12518 RVA: 0x001C4A24 File Offset: 0x001C3A24
	private bool ᜀ(string A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				string[] array = this.ᜏ;
				int num2 = 0;
				int num3 = 0;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_E7;
					case 1:
						goto IL_54;
					case 2:
					{
						if (num2 >= array.Length)
						{
							num3 = 1;
							continue;
						}
						string value = array[num2];
						num3 = 7;
						continue;
					}
					case 3:
						if (num > 1)
						{
							num3 = 8;
							continue;
						}
						return false;
					case 4:
						if (true)
						{
						}
						num++;
						num3 = 6;
						continue;
					case 5:
						goto IL_E7;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_54;
						default:
							if (false)
							{
							}
							goto IL_6F;
						}
						break;
					case 7:
					{
						string value;
						if (A_0.Contains(value))
						{
							num3 = 4;
							continue;
						}
						goto IL_6F;
					}
					case 8:
						return true;
					}
					break;
					IL_54:
					num3 = 3;
					continue;
					IL_6F:
					num2++;
					num3 = 5;
					continue;
					IL_E7:
					num3 = 2;
				}
			}
			return true;
		}
	}

	// Token: 0x060030E7 RID: 12519 RVA: 0x001C4B3C File Offset: 0x001C3B3C
	public double \u1714(int A_0)
	{
		int num;
		for (;;)
		{
			IL_34:
			bool flag;
			num = this.ᜀ(A_0, out flag);
			int num2 = 1;
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
					switch (num2)
					{
					case 0:
						goto IL_9F;
					case 1:
						if (flag)
						{
							num2 = 3;
							continue;
						}
						goto IL_A1;
					case 2:
					{
						int num3;
						if (num3 == 6)
						{
							goto IL_94;
						}
						goto IL_A1;
					}
					case 3:
					{
						if (true)
						{
						}
						int num3 = (int)this.ᜈ.ReadInt16(num);
						num2 = 2;
						continue;
					}
					}
					goto IL_34;
				}
				IL_94:
				num2 = 0;
			}
		}
		IL_9F:
		return spr᱒.ᜂ(this.ᜈ, num, this.ᜆ());
		IL_A1:
		return double.NaN;
	}

	// Token: 0x060030E8 RID: 12520 RVA: 0x001C4BF4 File Offset: 0x001C3BF4
	public string ᜀ(int A_0, SSTDictionary A_1)
	{
		switch (0)
		{
		default:
		{
			int num;
			int num4;
			for (;;)
			{
				bool flag;
				num = this.ᜀ(A_0, out flag);
				int num2 = 9;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_128;
					case 1:
					{
						int num3;
						if (num3 == 214)
						{
							num2 = 2;
							continue;
						}
						goto IL_DD;
					}
					case 2:
						goto IL_7B;
					case 3:
					{
						int num3;
						if (num3 == 253)
						{
							num2 = 10;
							continue;
						}
						num2 = 6;
						continue;
					}
					case 4:
						if (this.ᜆ() != ExcelVersion.Version97to2003)
						{
							num2 = 5;
							continue;
						}
						goto IL_10E;
					case 5:
						num4 += 4;
						num2 = 8;
						continue;
					case 6:
					{
						int num3;
						if (num3 != 516)
						{
							num2 = 0;
							continue;
						}
						goto IL_7B;
					}
					case 7:
					{
						int num3 = (int)this.ᜈ.ReadInt16(num);
						num2 = 3;
						continue;
					}
					case 8:
						goto IL_76;
					case 9:
						if (flag)
						{
							num2 = 7;
							continue;
						}
						goto IL_17C;
					case 10:
						goto IL_10C;
					}
					break;
					IL_7B:
					num4 = 10;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_128:
						num2 = 1;
						break;
					default:
						if (false)
						{
						}
						num2 = 4;
						break;
					}
				}
			}
			IL_76:
			goto IL_10E;
			IL_DD:
			return null;
			IL_10C:
			int a_ = spr\u1C7C.ᜂ(this.ᜈ, num, this.ᜆ());
			return spr\u223A.ᜀ(A_1[a_]);
			IL_10E:
			if (true)
			{
			}
			int num5;
			return this.ᜈ.ReadString16Bit(num + num4, out num5);
			IL_17C:
			return null;
		}
		}
	}

	// Token: 0x060030E9 RID: 12521 RVA: 0x001C4D80 File Offset: 0x001C3D80
	public string ᜌ(int A_0)
	{
		bool flag;
		int a_ = this.ᜀ(A_0, out flag);
		if (!flag)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return null;
		}
		return this.ᜐ(a_);
	}

	// Token: 0x060030EA RID: 12522 RVA: 0x001C4DD4 File Offset: 0x001C3DD4
	public string ᜐ(int A_0)
	{
		switch (0)
		{
		default:
		{
			for (;;)
			{
				TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(A_0);
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_69:
					if (tbiffrecord != TBIFFRecord.Formula)
					{
						num = 1;
					}
					else
					{
						int num2 = (int)this.ᜈ.ReadInt16(A_0 + 2);
						A_0 += 4 + num2;
						num = 0;
					}
					break;
				default:
					if (false)
					{
					}
					num = 3;
					break;
				}
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (A_0 >= this.ᜇ)
						{
							num = 4;
							continue;
						}
						tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(A_0);
						num = 5;
						continue;
					case 1:
						goto IL_76;
					case 2:
						goto IL_A2;
					case 3:
						goto IL_69;
					case 4:
						goto IL_EB;
					case 5:
						if (tbiffrecord != TBIFFRecord.String)
						{
							num = 2;
							continue;
						}
						goto IL_ED;
					}
					break;
				}
			}
			IL_76:
			return null;
			IL_A2:
			return null;
			IL_EB:
			return null;
			IL_ED:
			A_0 += 4;
			int iStrLen = (int)this.ᜈ.ReadInt16(A_0);
			int num3;
			return this.ᜈ.ReadString(A_0 + 2, iStrLen, out num3, false);
		}
		}
	}

	// Token: 0x060030EB RID: 12523 RVA: 0x001C4EF4 File Offset: 0x001C3EF4
	public Ptg[] \u1712(int A_0)
	{
		int num;
		for (;;)
		{
			IL_34:
			bool flag;
			num = this.ᜀ(A_0, out flag);
			if (true)
			{
			}
			int num2 = 3;
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
					switch (num2)
					{
					case 0:
						goto IL_9F;
					case 1:
					{
						int num3;
						if (num3 == 6)
						{
							goto IL_94;
						}
						goto IL_A1;
					}
					case 2:
					{
						int num3 = (int)this.ᜈ.ReadInt16(num);
						num2 = 1;
						continue;
					}
					case 3:
						if (flag)
						{
							num2 = 2;
							continue;
						}
						goto IL_A1;
					}
					goto IL_34;
				}
				IL_94:
				num2 = 0;
			}
		}
		IL_9F:
		return spr᱒.ᜄ(this.ᜈ, num, this.ᜆ());
		IL_A1:
		return null;
	}

	// Token: 0x060030EC RID: 12524 RVA: 0x001C4FA4 File Offset: 0x001C3FA4
	public XlsWorksheet.TRangeValueType ᜁ(int A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				bool flag;
				num = this.ᜀ(A_0, out flag);
				int num2 = 8;
				for (;;)
				{
					if (true)
					{
					}
					int num4;
					switch (num2)
					{
					case 0:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord != TBIFFRecord.RK)
						{
							num2 = 3;
							continue;
						}
						return XlsWorksheet.TRangeValueType.Number;
					}
					case 1:
						goto IL_C3;
					case 2:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord != TBIFFRecord.MulRK)
						{
							num2 = 25;
							continue;
						}
						return XlsWorksheet.TRangeValueType.Number;
					}
					case 3:
						num2 = 18;
						continue;
					case 4:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord != TBIFFRecord.Formula)
						{
							num2 = 5;
							continue;
						}
						num2 = 10;
						continue;
					}
					case 5:
						num2 = 2;
						continue;
					case 6:
						num2 = 21;
						continue;
					case 7:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord != TBIFFRecord.RString)
						{
							num2 = 6;
							continue;
						}
						return XlsWorksheet.TRangeValueType.String;
					}
					case 8:
						if (flag)
						{
							num2 = 14;
							continue;
						}
						return XlsWorksheet.TRangeValueType.Blank;
					case 9:
					{
						TBIFFRecord tbiffrecord;
						switch (tbiffrecord)
						{
						case TBIFFRecord.Number:
							return XlsWorksheet.TRangeValueType.Number;
						case TBIFFRecord.Label:
							return XlsWorksheet.TRangeValueType.String;
						case TBIFFRecord.BoolErr:
							goto IL_128;
						default:
							num2 = 11;
							continue;
						}
						break;
					}
					case 10:
						if (!A_1)
						{
							num2 = 13;
							continue;
						}
						num2 = 15;
						continue;
					case 11:
						num2 = 0;
						continue;
					case 12:
						goto IL_FB;
					case 13:
						num2 = 12;
						continue;
					case 14:
					{
						int num3 = (int)this.ᜈ.ReadInt16(num);
						TBIFFRecord tbiffrecord = (TBIFFRecord)num3;
						num2 = 20;
						continue;
					}
					case 15:
						goto IL_19A;
					case 16:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord != TBIFFRecord.LabelSST)
						{
							num2 = 19;
							continue;
						}
						return XlsWorksheet.TRangeValueType.String;
					}
					case 17:
						num2 = 1;
						continue;
					case 18:
						goto IL_25F;
					case 19:
						num2 = 9;
						continue;
					case 20:
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord <= TBIFFRecord.RString)
						{
							num2 = 22;
							continue;
						}
						num2 = 16;
						continue;
					}
					case 21:
						goto IL_18C;
					case 22:
						num2 = 4;
						continue;
					case 23:
						goto IL_2CE;
					case 24:
						if ((num4 & 65280) == 0)
						{
							num2 = 23;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_128;
						default:
							if (false)
							{
							}
							num2 = 17;
							continue;
						}
						break;
					case 25:
						num2 = 7;
						continue;
					}
					break;
					IL_128:
					num4 = spr\u249B.ᜂ(this.ᜈ, num, this.ᜆ());
					num2 = 24;
				}
			}
			return XlsWorksheet.TRangeValueType.String;
			IL_C3:
			return XlsWorksheet.TRangeValueType.Error;
			IL_FB:
			return XlsWorksheet.TRangeValueType.Formula;
			IL_18C:
			return XlsWorksheet.TRangeValueType.Blank;
			IL_19A:
			return this.ᜀ(num);
			IL_25F:
			return XlsWorksheet.TRangeValueType.Blank;
			IL_2CE:
			return XlsWorksheet.TRangeValueType.Boolean;
		}
		}
	}

	// Token: 0x060030ED RID: 12525 RVA: 0x001C5284 File Offset: 0x001C4284
	private XlsWorksheet.TRangeValueType ᜀ(int A_0)
	{
		switch (0)
		{
		default:
		{
			XlsWorksheet.TRangeValueType trangeValueType;
			for (;;)
			{
				trangeValueType = XlsWorksheet.TRangeValueType.Formula;
				ulong num = (ulong)spr᱒.ᜃ(this.ᜈ, A_0, this.ᜆ());
				ulong num2 = num & 18446462598732841215UL;
				int num3 = 9;
				for (;;)
				{
					bool flag;
					bool flag2;
					switch (num3)
					{
					case 0:
						goto IL_102;
					case 1:
						if (num2 == 18446462598732840962UL)
						{
							goto IL_1E9;
						}
						num3 = 5;
						continue;
					case 2:
						goto IL_126;
					case 3:
					{
						int num4;
						flag = (this.ᜈ.ReadInt16(num4) == 519);
						goto IL_12B;
					}
					case 4:
						trangeValueType = trangeValueType;
						num3 = 0;
						continue;
					case 5:
					{
						if (num2 == 18446462598732840963UL)
						{
							num3 = 4;
							continue;
						}
						int num5 = (int)this.ᜈ.ReadInt16(A_0 + 2);
						int num4 = A_0 + 4 + num5;
						flag2 = (num4 < this.ᜇ);
						num3 = 7;
						continue;
					}
					case 6:
						num3 = 3;
						continue;
					case 7:
						if (flag2)
						{
							num3 = 6;
							continue;
						}
						num3 = 12;
						continue;
					case 8:
						trangeValueType |= XlsWorksheet.TRangeValueType.Boolean;
						num3 = 2;
						continue;
					case 9:
						if (num2 == 18446462598732840961UL)
						{
							num3 = 8;
							continue;
						}
						num3 = 1;
						continue;
					case 10:
						goto IL_1C4;
					case 11:
						trangeValueType |= (flag2 ? XlsWorksheet.TRangeValueType.String : XlsWorksheet.TRangeValueType.Number);
						num3 = 10;
						continue;
					case 12:
						flag = false;
						goto IL_12B;
					case 13:
						goto IL_EF;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1E9;
						default:
							if (false)
							{
							}
							trangeValueType |= XlsWorksheet.TRangeValueType.Error;
							num3 = 13;
							continue;
						}
						break;
					}
					break;
					IL_12B:
					flag2 = flag;
					num3 = 11;
					continue;
					IL_1E9:
					num3 = 14;
				}
			}
			IL_EF:
			IL_102:
			IL_126:
			return trangeValueType;
			IL_1C4:
			if (true)
			{
			}
			return trangeValueType;
		}
		}
	}

	// Token: 0x060030EE RID: 12526 RVA: 0x001C5490 File Offset: 0x001C4490
	public bool \u1716(int A_0)
	{
		for (;;)
		{
			IL_34:
			bool flag;
			int iOffset = this.ᜀ(A_0, out flag);
			int num = 2;
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
					{
						TBIFFRecord tbiffrecord;
						if (tbiffrecord == TBIFFRecord.Formula)
						{
							goto IL_80;
						}
						return false;
					}
					case 1:
						return true;
					case 2:
						if (flag)
						{
							num = 3;
							continue;
						}
						return false;
					case 3:
					{
						if (true)
						{
						}
						TBIFFRecord tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(iOffset);
						num = 0;
						continue;
					}
					}
					goto IL_34;
				}
				IL_80:
				num = 1;
			}
		}
		return true;
	}

	// Token: 0x060030EF RID: 12527 RVA: 0x001C552C File Offset: 0x001C452C
	public bool ᜊ(int A_0)
	{
		for (;;)
		{
			bool flag;
			int num = this.ᜀ(A_0, out flag);
			int num2 = 6;
			for (;;)
			{
				TBIFFRecord tbiffrecord;
				switch (num2)
				{
				case 0:
					return true;
				case 1:
					if (num < this.ᜇ)
					{
						num2 = 7;
						continue;
					}
					return false;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6E;
					default:
						if (false)
						{
						}
						tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num);
						num2 = 5;
						continue;
					}
					break;
				case 3:
					if (tbiffrecord == TBIFFRecord.Array)
					{
						num2 = 0;
						continue;
					}
					return false;
				case 4:
					num = this.ᜃ(num);
					num2 = 1;
					continue;
				case 5:
					if (tbiffrecord == TBIFFRecord.Formula)
					{
						num2 = 4;
						continue;
					}
					return false;
				case 6:
					if (true)
					{
					}
					if (flag)
					{
						num2 = 2;
						continue;
					}
					return false;
				case 7:
					goto IL_6E;
				}
				break;
				IL_6E:
				tbiffrecord = (TBIFFRecord)this.ᜈ.ReadInt16(num);
				num2 = 3;
			}
		}
		return true;
	}

	// Token: 0x060030F0 RID: 12528 RVA: 0x001C5634 File Offset: 0x001C4634
	internal string \u171B(int A_0)
	{
		IDictionary errorCodeToName = FormulaUtil.ErrorCodeToName;
		if (!errorCodeToName.Contains(A_0))
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return null;
		}
		return (string)errorCodeToName[A_0];
	}

	// Token: 0x060030F1 RID: 12529 RVA: 0x001C5698 File Offset: 0x001C4698
	internal void ᜀ(XlsWorkbook A_0, int A_1)
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
		this.\u170D = A_0;
		this.ᜎ = A_1;
	}

	// Token: 0x060030F2 RID: 12530 RVA: 0x001C56E4 File Offset: 0x001C46E4
	[CLSCompliant(false)]
	internal void ᜀ(int A_0, double A_1, spr\u21DF A_2, int A_3)
	{
		int a_ = 7;
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag;
				int num = this.ᜀ(A_0, out flag);
				int num2 = 7;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_12F;
					case 1:
					{
						int num3;
						if (num3 == 519)
						{
							num2 = 12;
							continue;
						}
						goto IL_150;
					}
					case 2:
						goto IL_97;
					case 3:
						goto IL_150;
					case 4:
					{
						int a_2 = A_2.GetStoreSize(this.ᜆ()) + 4;
						this.ᜀ(num, 0, a_2, A_2, A_3);
						num2 = 0;
						continue;
					}
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6A;
						default:
						{
							if (false)
							{
							}
							int num3;
							if (num3 == 545)
							{
								num2 = 11;
								continue;
							}
							goto IL_97;
						}
						}
						break;
					case 6:
						goto IL_73;
					case 7:
						if (!flag)
						{
							goto IL_6A;
						}
						spr᱒.ᜀ(this.ᜈ, num, this.ᜆ(), A_1);
						num += (int)(this.ᜈ.ReadInt16(num + 2) + 4);
						num2 = 10;
						continue;
					case 8:
					{
						int num3 = (int)this.ᜈ.ReadInt16(num);
						num2 = 5;
						continue;
					}
					case 9:
						if (A_2 != null)
						{
							num2 = 4;
							continue;
						}
						return;
					case 10:
						if (num < this.ᜇ)
						{
							num2 = 8;
							continue;
						}
						goto IL_150;
					case 11:
					{
						num = this.ᜃ(num);
						int num3 = (int)this.ᜈ.ReadInt16(num);
						num2 = 2;
						continue;
					}
					case 12:
						this.ᜄ(num);
						num2 = 3;
						continue;
					}
					break;
					IL_6A:
					num2 = 6;
					continue;
					IL_97:
					num2 = 1;
					continue;
					IL_150:
					num2 = 9;
				}
			}
			IL_73:
			throw new ApplicationException(RecordTableEnumerator.b("縼帾⽀ⵂ⩄㍆楈㡊⡌㭎煐㕒㩔╖㑘⹚ㅜ㹞䅠ൢၤ੦୨๪Ὤ䅮", a_));
			IL_12F:
			if (true)
			{
			}
			return;
		}
	}

	// Token: 0x060030F3 RID: 12531 RVA: 0x001C58D8 File Offset: 0x001C48D8
	public ushort \u1718()
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
		return this.ᜑ;
	}

	// Token: 0x060030F4 RID: 12532 RVA: 0x001C591C File Offset: 0x001C491C
	public void ᜃ(ushort A_0)
	{
		int a_ = 8;
		if ((double)A_0 > 8190.0)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("瘽┿⭁⍃⹅㱇", a_));
		}
		this.ᜑ = A_0;
	}

	// Token: 0x060030F5 RID: 12533 RVA: 0x001C598C File Offset: 0x001C498C
	public ushort ᜇ()
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
		return this.\u1713;
	}

	// Token: 0x060030F6 RID: 12534 RVA: 0x001C59D0 File Offset: 0x001C49D0
	public void ᜀ(ushort A_0)
	{
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_67;
			default:
			{
				if (false)
				{
				}
				this.\u1713 = A_0;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_67;
					case 1:
						this.ᜇ(true);
						num = 0;
						continue;
					case 2:
						if (A_0 != 15)
						{
							num = 1;
							continue;
						}
						goto IL_69;
					}
					break;
				}
				break;
			}
			}
		}
		IL_67:
		IL_69:
		if (true)
		{
		}
	}

	// Token: 0x060030F7 RID: 12535 RVA: 0x001C5A50 File Offset: 0x001C4A50
	public ushort \u1717()
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
		return (ushort)(this.\u1712 & (spr\u20BA.OptionFlags)7);
	}

	// Token: 0x060030F8 RID: 12536 RVA: 0x001C5A94 File Offset: 0x001C4A94
	public void ᜂ(ushort A_0)
	{
		if (A_0 > 7)
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
				throw new ArgumentOutOfRangeException();
			}
		}
		int num = (int)this.\u1712;
		num &= -8;
		num |= (int)(A_0 & 7);
		this.\u1712 = (spr\u20BA.OptionFlags)num;
	}

	// Token: 0x060030F9 RID: 12537 RVA: 0x001C5AF4 File Offset: 0x001C4AF4
	public bool ᜣ()
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
		return (this.\u1712 & spr\u20BA.OptionFlags.Colapsed) != (spr\u20BA.OptionFlags)0;
	}

	// Token: 0x060030FA RID: 12538 RVA: 0x001C5B40 File Offset: 0x001C4B40
	public void ᜁ(bool A_0)
	{
		if (A_0)
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
				this.\u1712 |= spr\u20BA.OptionFlags.Colapsed;
				return;
			}
		}
		this.\u1712 &= (spr\u20BA.OptionFlags)(-17);
	}

	// Token: 0x060030FB RID: 12539 RVA: 0x001C5BA0 File Offset: 0x001C4BA0
	public bool ᜅ()
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
		return (this.\u1712 & spr\u20BA.OptionFlags.ZeroHeight) != (spr\u20BA.OptionFlags)0;
	}

	// Token: 0x060030FC RID: 12540 RVA: 0x001C5BEC File Offset: 0x001C4BEC
	public void ᜅ(bool A_0)
	{
		if (A_0)
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
				this.\u1712 |= spr\u20BA.OptionFlags.ZeroHeight;
				return;
			}
		}
		this.\u1712 &= (spr\u20BA.OptionFlags)(-33);
	}

	// Token: 0x060030FD RID: 12541 RVA: 0x001C5C4C File Offset: 0x001C4C4C
	public bool \u1713()
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
		return (this.\u1712 & spr\u20BA.OptionFlags.BadFontHeight) != (spr\u20BA.OptionFlags)0;
	}

	// Token: 0x060030FE RID: 12542 RVA: 0x001C5C98 File Offset: 0x001C4C98
	public void ᜊ(bool A_0)
	{
		if (A_0)
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
				this.\u1712 |= spr\u20BA.OptionFlags.BadFontHeight;
				return;
			}
		}
		this.\u1712 &= (spr\u20BA.OptionFlags)(-65);
	}

	// Token: 0x060030FF RID: 12543 RVA: 0x001C5CF8 File Offset: 0x001C4CF8
	public bool \u1719()
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
		return (this.\u1712 & spr\u20BA.OptionFlags.Formatted) != (spr\u20BA.OptionFlags)0;
	}

	// Token: 0x06003100 RID: 12544 RVA: 0x001C5D48 File Offset: 0x001C4D48
	public void ᜇ(bool A_0)
	{
		if (A_0)
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
				this.\u1712 |= spr\u20BA.OptionFlags.Formatted;
				return;
			}
		}
		this.\u1712 &= (spr\u20BA.OptionFlags)(-129);
	}

	// Token: 0x06003101 RID: 12545 RVA: 0x001C5DB0 File Offset: 0x001C4DB0
	public bool \u171F()
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
		return (this.\u1712 & spr\u20BA.OptionFlags.SpaceAbove) != (spr\u20BA.OptionFlags)0;
	}

	// Token: 0x06003102 RID: 12546 RVA: 0x001C5E00 File Offset: 0x001C4E00
	public void ᜂ(bool A_0)
	{
		if (A_0)
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
				this.\u1712 |= spr\u20BA.OptionFlags.SpaceAbove;
				return;
			}
		}
		this.\u1712 &= (spr\u20BA.OptionFlags)(-268435457);
	}

	// Token: 0x06003103 RID: 12547 RVA: 0x001C5E68 File Offset: 0x001C4E68
	public bool ᜑ()
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
		return (this.\u1712 & spr\u20BA.OptionFlags.SpaceBelow) != (spr\u20BA.OptionFlags)0;
	}

	// Token: 0x06003104 RID: 12548 RVA: 0x001C5EB8 File Offset: 0x001C4EB8
	public void ᜄ(bool A_0)
	{
		if (A_0)
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
				this.\u1712 |= spr\u20BA.OptionFlags.SpaceBelow;
				return;
			}
		}
		this.\u1712 &= (spr\u20BA.OptionFlags)(-536870913);
	}

	// Token: 0x06003105 RID: 12549 RVA: 0x001C5F20 File Offset: 0x001C4F20
	public bool \u1715()
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
		return (this.\u1712 & spr\u20BA.OptionFlags.ShowOutlineGroups) != (spr\u20BA.OptionFlags)0;
	}

	// Token: 0x06003106 RID: 12550 RVA: 0x001C5F70 File Offset: 0x001C4F70
	public void ᜉ(bool A_0)
	{
		if (A_0)
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
				this.\u1712 |= spr\u20BA.OptionFlags.ShowOutlineGroups;
				return;
			}
		}
		if (true)
		{
		}
		this.\u1712 &= (spr\u20BA.OptionFlags)(-257);
	}

	// Token: 0x06003107 RID: 12551 RVA: 0x001C5FD8 File Offset: 0x001C4FD8
	ushort spr\u2502.\u1714()
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
		throw new NotImplementedException();
	}

	// Token: 0x06003108 RID: 12552 RVA: 0x001C6018 File Offset: 0x001C5018
	void spr\u2502.ᜁ(ushort A_0)
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
		throw new NotImplementedException();
	}

	// Token: 0x06003109 RID: 12553 RVA: 0x001C6058 File Offset: 0x001C5058
	// Note: this type is marked as 'beforefieldinit'.
	static sprᱧ()
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
		sprᱧ.ᜃ = new short[]
		{
			190,
			189
		};
		sprᱧ.ᜄ = new TBIFFRecord[]
		{
			TBIFFRecord.MulBlank,
			TBIFFRecord.Blank,
			TBIFFRecord.MulRK,
			TBIFFRecord.RK
		};
	}

	// Token: 0x04001592 RID: 5522
	private const int ᜀ = 128;

	// Token: 0x04001593 RID: 5523
	private const int ᜁ = 6;

	// Token: 0x04001594 RID: 5524
	private const int ᜂ = 2;

	// Token: 0x04001595 RID: 5525
	private static readonly short[] ᜃ;

	// Token: 0x04001596 RID: 5526
	private static readonly TBIFFRecord[] ᜄ;

	// Token: 0x04001597 RID: 5527
	private int ᜅ;

	// Token: 0x04001598 RID: 5528
	private int ᜆ;

	// Token: 0x04001599 RID: 5529
	private int ᜇ;

	// Token: 0x0400159A RID: 5530
	private DataProvider ᜈ;

	// Token: 0x0400159B RID: 5531
	private sprᱧ.StorageOptions ᜉ;

	// Token: 0x0400159C RID: 5532
	private int ᜊ;

	// Token: 0x0400159D RID: 5533
	private int ᜋ;

	// Token: 0x0400159E RID: 5534
	private ExcelVersion ᜌ;

	// Token: 0x0400159F RID: 5535
	private XlsWorkbook \u170D;

	// Token: 0x040015A0 RID: 5536
	private int ᜎ;

	// Token: 0x040015A1 RID: 5537
	private string[] ᜏ;

	// Token: 0x040015A2 RID: 5538
	private bool ᜐ;

	// Token: 0x040015A3 RID: 5539
	private ushort ᜑ;

	// Token: 0x040015A4 RID: 5540
	private spr\u20BA.OptionFlags \u1712;

	// Token: 0x040015A5 RID: 5541
	private ushort \u1713;

	// Token: 0x02000314 RID: 788
	[Flags]
	private enum StorageOptions
	{
		// Token: 0x040015A7 RID: 5543
		None = 0,
		// Token: 0x040015A8 RID: 5544
		HasRKBlank = 1,
		// Token: 0x040015A9 RID: 5545
		HasMultiRKBlank = 2,
		// Token: 0x040015AA RID: 5546
		Disposed = 4
	}

	// Token: 0x02000315 RID: 789
	// (Invoke) Token: 0x0600310B RID: 12555
	public delegate void ᜃ(TBIFFRecord A_0, int A_1, object A_2);

	// Token: 0x02000316 RID: 790
	private class ᜁ
	{
		// Token: 0x040015AB RID: 5547
		public int ᜀ;

		// Token: 0x040015AC RID: 5548
		public int ᜁ;
	}

	// Token: 0x02000317 RID: 791
	private class ᜂ
	{
		// Token: 0x040015AD RID: 5549
		public int ᜀ;

		// Token: 0x040015AE RID: 5550
		public int ᜁ;
	}

	// Token: 0x02000318 RID: 792
	// (Invoke) Token: 0x06003111 RID: 12561
	private delegate int ᜀ(object A_0);
}
