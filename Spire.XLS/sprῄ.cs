using System;
using System.Collections.Generic;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200025D RID: 605
[CLSCompliant(false)]
internal abstract class sprῄ : spr\u191F, IDisposable
{
	// Token: 0x06002466 RID: 9318 RVA: 0x001532D8 File Offset: 0x001522D8
	public sprῄ()
	{
	}

	// Token: 0x06002467 RID: 9319 RVA: 0x001532EC File Offset: 0x001522EC
	protected virtual void ᜎ()
	{
		try
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
			this.\u170D();
		}
		finally
		{
			base.Finalize();
		}
	}

	// Token: 0x06002468 RID: 9320
	public new abstract void ᜀ();

	// Token: 0x06002469 RID: 9321 RVA: 0x00153348 File Offset: 0x00152348
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		for (;;)
		{
			this.ᜀ = spr\u17FF.ᜀ();
			this.ᜀ.EnsureCapacity(A_2);
			this.m_iLength = A_2;
			A_0.CopyTo(A_1, this.ᜀ, 0, A_2);
			this.ᜀ();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ.Clear();
					this.AutoGrowData = true;
					num = 2;
					continue;
				case 1:
					if (!this.NeedDataArray)
					{
						num = 0;
						continue;
					}
					return;
				case 2:
					goto IL_88;
				}
				break;
			}
		}
		IL_88:
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
			break;
		}
	}

	// Token: 0x0600246A RID: 9322
	public new abstract void ᜀ(ExcelVersion A_0);

	// Token: 0x0600246B RID: 9323 RVA: 0x00153404 File Offset: 0x00152404
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		int a_ = 7;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_5E;
			case 2:
				return;
			case 3:
				this.ᜀ.CopyTo(0, A_0, A_1, this.m_iLength);
				num = 2;
				continue;
			case 4:
				if (this.m_iLength > 0)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				return;
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
				if (A_0 == null)
				{
					num = 1;
				}
				else
				{
					this.ᜀ(A_2);
					num = 4;
				}
				break;
			}
		}
		IL_5E:
		throw new ArgumentNullException(RecordTableEnumerator.b("䴼䴾⹀㕂ⱄ⍆ⱈ㥊", a_));
	}

	// Token: 0x0600246C RID: 9324 RVA: 0x001534D0 File Offset: 0x001524D0
	public virtual object ᜂ()
	{
		sprῄ sprῄ;
		IntPtr intPtr;
		for (;;)
		{
			IL_18:
			sprῄ = (sprῄ)base.Clone();
			intPtr = IntPtr.Zero;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜀ is sprᰟ)
					{
						num = 3;
						continue;
					}
					goto IL_6E;
				case 1:
					goto IL_76;
				case 2:
					goto IL_6E;
				case 3:
					intPtr = (this.ᜀ as sprᰟ).ᜁ();
					num = 2;
					continue;
				}
				goto IL_18;
				IL_6E:
				num = 1;
			}
			IL_76:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_8C;
			}
		}
		IL_8C:
		if (true)
		{
		}
		if (false)
		{
		}
		sprῄ.ᜀ = ((intPtr != IntPtr.Zero) ? spr\u17FF.ᜀ(intPtr) : spr\u17FF.ᜀ());
		return sprῄ;
	}

	// Token: 0x0600246D RID: 9325 RVA: 0x0015359C File Offset: 0x0015259C
	protected internal new string ᜀ(int A_0, int A_1)
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
		int num;
		return this.ᜀ(A_0, A_1, out num);
	}

	// Token: 0x0600246E RID: 9326 RVA: 0x001535E4 File Offset: 0x001525E4
	protected internal new string ᜀ(int A_0, int A_1, out int A_2)
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
		return this.ᜀ(A_0, A_1, out A_2, false);
	}

	// Token: 0x0600246F RID: 9327 RVA: 0x0015362C File Offset: 0x0015262C
	protected internal new string ᜀ(int A_0, int A_1, out int A_2, bool A_3)
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
		return this.ᜀ.ReadString(A_0, A_1, out A_2, A_3);
	}

	// Token: 0x06002470 RID: 9328 RVA: 0x00153678 File Offset: 0x00152678
	protected new string ᜀ(int A_0, IList<int> A_1, int A_2, ref int A_3, out int A_4, out byte[] A_5, out byte[] A_6)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				string text = null;
				int num = 3;
				A_5 = null;
				A_6 = null;
				ushort num2 = this.ᜀ.ReadUInt16(A_0);
				byte b = this.ᜀ.ReadByte(A_0 + 2);
				bool flag = (b & 1) == 1;
				bool flag2 = (b & 4) != 0;
				bool flag3 = (b & 8) != 0;
				int num3 = 3;
				short num4 = 0;
				int num5 = 0;
				int num6 = 17;
				for (;;)
				{
					string text2;
					int num7;
					Encoding encoding;
					int num8;
					int num9;
					int num10;
					Encoding encoding2;
					byte b2;
					Encoding encoding3;
					int num12;
					string text4;
					switch (num6)
					{
					case 0:
						num4 = this.ᜀ.ReadInt16(A_0 + num3);
						num3 += 2;
						num += 2;
						num6 = 24;
						continue;
					case 1:
						goto IL_534;
					case 2:
						if (!flag)
						{
							num6 = 36;
							continue;
						}
						num6 = 26;
						continue;
					case 3:
						if (flag3)
						{
							num6 = 23;
							continue;
						}
						goto IL_4E5;
					case 4:
						num6 = 44;
						continue;
					case 5:
						if (!flag)
						{
							num6 = 33;
							continue;
						}
						num6 = 25;
						continue;
					case 6:
						if (text != null)
						{
							num6 = 43;
							continue;
						}
						num6 = 8;
						continue;
					case 7:
						goto IL_46E;
					case 8:
					{
						string text3;
						text2 = text3;
						goto IL_37E;
					}
					case 9:
						if (num7 >= (int)num2)
						{
							num6 = 28;
							continue;
						}
						num6 = 11;
						continue;
					case 10:
						goto IL_1B9;
					case 11:
						goto IL_225;
					case 12:
						encoding = BiffRecordRaw.LatinEncoding;
						goto IL_43A;
					case 13:
					{
						if (num8 <= num9)
						{
							num6 = 42;
							continue;
						}
						string text3 = this.ᜀ.ReadString(num10, num9, encoding2, flag);
						num6 = 6;
						continue;
					}
					case 14:
						goto IL_490;
					case 15:
						num6 = 27;
						continue;
					case 16:
					{
						string text3;
						text2 = text + text3;
						goto IL_37E;
					}
					case 17:
						if (flag3)
						{
							num6 = 0;
							continue;
						}
						goto IL_172;
					case 18:
						goto IL_454;
					case 19:
						if (b2 != 0)
						{
							num6 = 15;
							continue;
						}
						goto IL_3EF;
					case 20:
						num6 = 29;
						continue;
					case 21:
						if (text != null)
						{
							num6 = 4;
							continue;
						}
						num6 = 31;
						continue;
					case 22:
						encoding3 = BiffRecordRaw.LatinEncoding;
						goto IL_206;
					case 23:
					{
						int num11 = (int)(num4 * 4);
						A_5 = new byte[num11];
						this.ᜀ.ReadArray(A_0 + num, A_5, num11);
						num += num11;
						num6 = 39;
						continue;
					}
					case 24:
						goto IL_172;
					case 25:
						encoding3 = Encoding.Unicode;
						goto IL_206;
					case 26:
						encoding = Encoding.Unicode;
						goto IL_43A;
					case 27:
						if (b2 == 1)
						{
							num6 = 40;
							continue;
						}
						goto IL_454;
					case 28:
						goto IL_490;
					case 29:
						num12 = (int)num2 - num7;
						goto IL_265;
					case 30:
						goto IL_55D;
					case 31:
					{
						string text5;
						text4 = text5;
						goto IL_2F3;
					}
					case 32:
						A_6 = new byte[num5];
						this.ᜀ.ReadArray(A_0 + num, A_6, num5);
						num += num5;
						num6 = 1;
						continue;
					case 33:
						num6 = 22;
						continue;
					case 34:
						num7 += (flag ? (num9 / 2) : num9);
						b2 = this.ᜀ.ReadByte(num10 + num9);
						num6 = 19;
						continue;
					case 35:
						if (flag2)
						{
							num6 = 32;
							continue;
						}
						goto IL_534;
					case 36:
						num6 = 12;
						continue;
					case 37:
						if (flag2)
						{
							num6 = 45;
							continue;
						}
						goto IL_1B9;
					case 38:
						if (text == null)
						{
							num6 = 30;
							continue;
						}
						return text;
					case 39:
						goto IL_4E5;
					case 40:
						goto IL_3EF;
					case 41:
						num12 = ((int)num2 - num7) * 2;
						goto IL_265;
					case 42:
					{
						string text5 = this.ᜀ.ReadString(num10, num8, encoding2, flag);
						num6 = 21;
						continue;
					}
					case 43:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_225;
						default:
							if (false)
							{
							}
							num6 = 16;
							continue;
						}
						break;
					case 44:
					{
						string text5;
						text4 = text + text5;
						goto IL_2F3;
					}
					case 45:
						num5 = this.ᜀ.ReadInt32(A_0 + num3);
						num3 += 4;
						num += 4;
						num6 = 10;
						continue;
					case 46:
						goto IL_46E;
					}
					break;
					IL_172:
					num6 = 37;
					continue;
					IL_1B9:
					num10 = A_0 + num3;
					num7 = 0;
					num6 = 5;
					continue;
					IL_206:
					encoding2 = encoding3;
					num6 = 46;
					continue;
					IL_225:
					if (true)
					{
					}
					if (!flag)
					{
						num6 = 20;
						continue;
					}
					num6 = 41;
					continue;
					IL_265:
					num8 = num12;
					int num13 = BiffRecordRaw.FindNextBreak(A_1, A_2, num10, ref A_3);
					num9 = num13 - num10;
					num6 = 13;
					continue;
					IL_2F3:
					text = text4;
					num += num8;
					num6 = 14;
					continue;
					IL_37E:
					text = text2;
					num6 = 34;
					continue;
					IL_3EF:
					flag = (b2 == 1);
					num6 = 2;
					continue;
					IL_43A:
					encoding2 = encoding;
					num10++;
					num++;
					num6 = 18;
					continue;
					IL_454:
					num10 += num9;
					num += num9;
					num6 = 7;
					continue;
					IL_46E:
					num6 = 9;
					continue;
					IL_490:
					num6 = 3;
					continue;
					IL_4E5:
					num6 = 35;
					continue;
					IL_534:
					A_4 = num;
					num6 = 38;
				}
			}
			IL_55D:
			return string.Empty;
		}
	}

	// Token: 0x06002471 RID: 9329 RVA: 0x00153BEC File Offset: 0x00152BEC
	protected internal new void ᜀ(int A_0, byte A_1)
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
		this.ᜀ.WriteByte(A_0, A_1);
	}

	// Token: 0x06002472 RID: 9330 RVA: 0x00153C34 File Offset: 0x00152C34
	protected internal new void ᜀ(int A_0, ushort A_1)
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
		this.ᜀ.WriteUInt16(A_0, A_1);
	}

	// Token: 0x06002473 RID: 9331 RVA: 0x00153C7C File Offset: 0x00152C7C
	protected internal new void ᜀ(int A_0, byte[] A_1, int A_2, int A_3)
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
		this.ᜀ.WriteBytes(A_0, A_1, A_2, A_3);
	}

	// Token: 0x06002474 RID: 9332 RVA: 0x00153CC8 File Offset: 0x00152CC8
	protected internal new void ᜀ(int A_0, byte[] A_1)
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
		this.ᜀ(A_0, A_1, 0, A_1.Length);
	}

	// Token: 0x06002475 RID: 9333 RVA: 0x00153D10 File Offset: 0x00152D10
	protected internal new int ᜀ(int A_0, string A_1)
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
		return this.ᜀ(A_0, A_1, false);
	}

	// Token: 0x06002476 RID: 9334 RVA: 0x00153D54 File Offset: 0x00152D54
	protected internal new int ᜀ(int A_0, string A_1, bool A_2)
	{
		int num = 0;
		byte[] bytes;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_5E;
			case 2:
				num = 7;
				continue;
			case 3:
				if (A_2)
				{
					num = 2;
					continue;
				}
				return 0;
			case 4:
				goto IL_91;
			case 5:
				goto IL_127;
			case 6:
				this.ᜀ.EnsureCapacity(A_0 + bytes.Length);
				num = 4;
				continue;
			case 7:
				if (this.AutoGrowData)
				{
					num = 11;
					continue;
				}
				goto IL_4F;
			case 8:
				if (A_1.Length == 0)
				{
					num = 1;
					continue;
				}
				bytes = Encoding.Unicode.GetBytes(A_1);
				goto IL_CA;
			case 9:
				if (this.AutoGrowData)
				{
					num = 6;
					continue;
				}
				goto IL_14F;
			case 10:
				if (true)
				{
				}
				num = 8;
				continue;
			case 11:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_CA;
				default:
					if (false)
					{
					}
					this.ᜀ.EnsureCapacity(A_0);
					num = 5;
					continue;
				}
				break;
			}
			if (A_1 != null)
			{
				num = 10;
				continue;
			}
			IL_5E:
			num = 3;
			continue;
			IL_CA:
			num = 9;
		}
		return 0;
		IL_4F:
		this.ᜀ.WriteByte(A_0, 0);
		return 1;
		IL_91:
		goto IL_14F;
		IL_127:
		goto IL_4F;
		IL_14F:
		this.ᜀ.WriteByte(A_0, 1);
		this.ᜀ.WriteBytes(A_0 + 1, bytes, 0, bytes.Length);
		return bytes.Length + 1;
	}

	// Token: 0x06002477 RID: 9335 RVA: 0x00153ED8 File Offset: 0x00152ED8
	public void \u170D()
	{
		if (true)
		{
		}
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5C;
			case 1:
				this.ᜀ.Dispose();
				this.ᜀ = null;
				num = 0;
				continue;
			}
			IL_2E:
			if (this.ᜀ != null)
			{
				num = 1;
				continue;
			}
			IL_5C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_2E;
			default:
				goto IL_72;
			}
		}
		IL_72:
		if (false)
		{
		}
		GC.SuppressFinalize(this);
	}

	// Token: 0x04001278 RID: 4728
	protected new DataProvider ᜀ;
}
