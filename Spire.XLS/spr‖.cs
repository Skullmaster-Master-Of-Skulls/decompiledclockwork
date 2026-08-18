using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003CC RID: 972
[CLSCompliant(false)]
[sprᵴ(MsoRecords.msofbtClientTextbox)]
internal class spr\u2016 : spr\u1D3B
{
	// Token: 0x06003B07 RID: 15111 RVA: 0x00211880 File Offset: 0x00210880
	public new spr\u1FF0 ᜄ()
	{
		if (this.ᜀ.Count <= 0)
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
				break;
			}
			return null;
		}
		return this.ᜀ[0] as spr\u1FF0;
	}

	// Token: 0x06003B08 RID: 15112 RVA: 0x002118E0 File Offset: 0x002108E0
	public new void ᜀ(spr\u1FF0 A_0)
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
		this.ᜀ.Insert(0, A_0);
	}

	// Token: 0x06003B09 RID: 15113 RVA: 0x00211928 File Offset: 0x00210928
	public new string ᜃ()
	{
		switch (0)
		{
		default:
		{
			int num3;
			for (;;)
			{
				spr\u1FF0 spr_u1FF = this.ᜄ();
				int num = 10;
				for (;;)
				{
					int count;
					int num4;
					int num6;
					switch (num)
					{
					case 0:
					{
						if (count <= 1)
						{
							num = 6;
							continue;
						}
						int num2 = 0;
						num3 = 1;
						num = 3;
						continue;
					}
					case 1:
						goto IL_15A;
					case 2:
						if (num4 != 0)
						{
							goto IL_93;
						}
						goto IL_BD;
					case 3:
						goto IL_15A;
					case 4:
						num = 0;
						continue;
					case 5:
						num = 8;
						continue;
					case 6:
						goto IL_142;
					case 7:
					{
						int num2;
						bool flag;
						int num5;
						num2 += (flag ? (num5 / 2) : num5);
						num3++;
						num = 1;
						continue;
					}
					case 8:
						num6 = 0;
						goto IL_7A;
					case 9:
						goto IL_19D;
					case 10:
						if (spr_u1FF == null)
						{
							num = 5;
							continue;
						}
						num = 11;
						continue;
					case 11:
						num6 = (int)spr_u1FF.ᜈ();
						goto IL_7A;
					case 12:
					{
						int num2;
						if (num2 >= num4)
						{
							num = 9;
							continue;
						}
						spr\u2553 spr_u = this.ᜀ[num3] as spr\u2553;
						byte[] data = spr_u.Data;
						bool flag = data[0] != 0;
						int num5 = spr_u.Length - 1;
						num = 7;
						continue;
					}
					}
					break;
					IL_7A:
					num4 = num6;
					count = this.ᜀ.Count;
					num = 2;
					continue;
					IL_93:
					num = 4;
					continue;
					IL_15A:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_93;
					default:
						if (false)
						{
						}
						num = 12;
						break;
					}
				}
			}
			IL_BD:
			return null;
			IL_142:
			goto IL_BD;
			IL_19D:
			return this.ᜀ(1, num3);
		}
		}
	}

	// Token: 0x06003B0A RID: 15114 RVA: 0x00211AE0 File Offset: 0x00210AE0
	private new string ᜀ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			string text;
			for (;;)
			{
				text = string.Empty;
				int num = A_0;
				int num2 = 4;
				for (;;)
				{
					int num3;
					spr\u2553 spr_u;
					switch (num2)
					{
					case 0:
						goto IL_4A;
					case 1:
						num2 = 6;
						continue;
					case 2:
					{
						int length;
						num3 = (length - 1) / 2;
						goto IL_DC;
					}
					case 3:
					{
						bool flag;
						if (!flag)
						{
							num2 = 1;
							continue;
						}
						num2 = 2;
						continue;
					}
					case 4:
						IL_48:
						goto IL_4A;
					case 5:
						return text;
					case 6:
					{
						int length;
						num3 = length - 1;
						goto IL_DC;
					}
					case 7:
					{
						if (num >= A_1)
						{
							num2 = 5;
							continue;
						}
						spr_u = (spr\u2553)this.ᜀ[num];
						byte[] data = spr_u.Data;
						int length = spr_u.Length;
						bool flag = data[0] != 0;
						num2 = 3;
						continue;
					}
					}
					break;
					IL_4A:
					num2 = 7;
					continue;
					IL_DC:
					int a_ = num3;
					text += spr_u.ᜅ(0, a_);
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_48;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num2 = 0;
						break;
					}
				}
			}
			return text;
		}
		}
	}

	// Token: 0x06003B0B RID: 15115 RVA: 0x00211C18 File Offset: 0x00210C18
	public new byte[] ᜀ()
	{
		switch (0)
		{
		default:
		{
			byte[] array;
			for (;;)
			{
				spr\u1FF0 spr_u1FF = this.ᜄ();
				array = null;
				int num = 9;
				for (;;)
				{
					int num2;
					int num3;
					int num6;
					byte[] array2;
					switch (num)
					{
					case 0:
						num = 8;
						continue;
					case 1:
						goto IL_18B;
					case 2:
					{
						int count;
						if (num2 >= count)
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
								num = 7;
								continue;
							}
						}
						BiffRecordRaw biffRecordRaw = this.ᜀ[num2];
						int length = biffRecordRaw.Length;
						Buffer.BlockCopy(biffRecordRaw.Data, 0, array, num3, length);
						num3 += length;
						num2++;
						num = 11;
						continue;
					}
					case 3:
					{
						int num4;
						int num5;
						if (num4 >= num5)
						{
							num = 0;
							continue;
						}
						BiffRecordRaw biffRecordRaw2 = this.ᜀ[num6];
						num4 += biffRecordRaw2.Length;
						num6--;
						num = 4;
						continue;
					}
					case 4:
						if (true)
						{
						}
						goto IL_18B;
					case 5:
						goto IL_C4;
					case 6:
					{
						int num5 = (int)this.ᜄ().ᜉ();
						int num4 = 0;
						int count = this.ᜀ.Count;
						num6 = count - 1;
						num = 1;
						continue;
					}
					case 7:
						return array;
					case 8:
					{
						int num5;
						if (num5 <= 0)
						{
							num = 10;
							continue;
						}
						num = 13;
						continue;
					}
					case 9:
						if (spr_u1FF != null)
						{
							num = 6;
							continue;
						}
						return array;
					case 10:
						num = 12;
						continue;
					case 11:
						goto IL_C4;
					case 12:
						array2 = null;
						goto IL_1BB;
					case 13:
					{
						int num5;
						array2 = new byte[num5];
						goto IL_1BB;
					}
					}
					break;
					IL_C4:
					num = 2;
					continue;
					IL_18B:
					num = 3;
					continue;
					IL_1BB:
					array = array2;
					num2 = num6 + 1;
					num3 = 0;
					num = 5;
				}
			}
			return array;
		}
		}
	}

	// Token: 0x06003B0C RID: 15116 RVA: 0x00211E10 File Offset: 0x00210E10
	public BiffRecordRaw[] ᜁ()
	{
		if (this.ᜀ == null)
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
				break;
			}
			return null;
		}
		return this.ᜀ.ToArray();
	}

	// Token: 0x06003B0D RID: 15117 RVA: 0x00211E64 File Offset: 0x00210E64
	public spr\u2016(spr\u1D3B A_0)
	{
		this.ᜀ = new List<BiffRecordRaw>(3);
		base..ctor(A_0);
	}

	// Token: 0x06003B0E RID: 15118 RVA: 0x00211E84 File Offset: 0x00210E84
	public spr\u2016(spr\u1D3B A_0, byte[] A_1, int A_2)
	{
		this.ᜀ = new List<BiffRecordRaw>(3);
		base..ctor(A_0, A_1, A_2);
	}

	// Token: 0x06003B0F RID: 15119 RVA: 0x00211EA8 File Offset: 0x00210EA8
	public spr\u2016(spr\u1D3B A_0, byte[] A_1, int A_2, spr\u24C9 A_3)
	{
		int a_ = 10;
		this.ᜀ = new List<BiffRecordRaw>(3);
		base..ctor(A_0, A_1, A_2, A_3);
		BiffRecordRaw[] array = A_3();
		if (array == null)
		{
			throw new ArgumentException(RecordTableEnumerator.b("Ŀ♁⁃⽅㱇⍉⍋⁍ㅏ㹑瑓㉕㥗⹙㵛繝͟͡੣䅥ᱧ䩩๫୭偯ᱱų᩵ᑷ", a_));
		}
		this.ᜀ.Clear();
		this.ᜀ.AddRange(array);
	}

	// Token: 0x06003B10 RID: 15120 RVA: 0x00211F10 File Offset: 0x00210F10
	public override void ᜀ(Stream A_0, int A_1, List<int> A_2, List<List<BiffRecordRaw>> A_3)
	{
		for (;;)
		{
			IL_1C:
			this.m_iLength = 0;
			for (;;)
			{
				IL_23:
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_3 != null)
						{
							num = 3;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_23;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 3:
						A_2.Add(this.m_iLength + A_1);
						A_3.Add(this.ᜀ);
						num = 1;
						continue;
					case 4:
						if (true)
						{
						}
						if (A_2 != null)
						{
							num = 2;
							continue;
						}
						return;
					}
					goto IL_1C;
				}
			}
		}
	}

	// Token: 0x06003B11 RID: 15121 RVA: 0x00211FC4 File Offset: 0x00210FC4
	public override void ᜀ(Stream A_0)
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
	}

	// Token: 0x06003B12 RID: 15122 RVA: 0x00212000 File Offset: 0x00211000
	protected override object ᜅ()
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
		spr\u2016 spr_u = (spr\u2016)base.ᜅ();
		spr_u.ᜀ = spr\u1CD3.ᜀ(this.ᜀ);
		return spr_u;
	}

	// Token: 0x06003B13 RID: 15123 RVA: 0x0021205C File Offset: 0x0021105C
	public override void ᜏ()
	{
		int a_ = 2;
		BiffRecordRaw[] array = base.\u1716()();
		if (array == null)
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
				break;
			}
			if (true)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("礷帹堻圽㐿⭁⭃⡅⥇♉汋⩍ㅏ♑㕓癕㭗㭙㉛祝ᑟ䉡٣ͥ䡧ѩᥫɭᱯ", a_));
		}
		this.ᜀ.Clear();
		this.ᜀ.AddRange(array);
	}

	// Token: 0x06003B14 RID: 15124 RVA: 0x002120DC File Offset: 0x002110DC
	public new void ᜀ(BiffRecordRaw A_0)
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
		this.ᜀ.Add(A_0);
	}

	// Token: 0x040019B2 RID: 6578
	private new List<BiffRecordRaw> ᜀ;
}
