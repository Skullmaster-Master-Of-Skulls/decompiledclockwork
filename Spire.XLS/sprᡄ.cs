using System;
using System.Collections;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

// Token: 0x0200038C RID: 908
[CLSCompliant(false)]
internal class sprᡄ : IDisposable
{
	// Token: 0x06003771 RID: 14193 RVA: 0x001F29F0 File Offset: 0x001F19F0
	public Stream ᜀ()
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

	// Token: 0x06003772 RID: 14194 RVA: 0x001F2A34 File Offset: 0x001F1A34
	public byte[] ᜂ()
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
		return this.ᜅ;
	}

	// Token: 0x06003773 RID: 14195 RVA: 0x001F2A78 File Offset: 0x001F1A78
	private sprᡄ()
	{
		this.ᜆ = new spr\u24E5(this.ᜅ);
	}

	// Token: 0x06003774 RID: 14196 RVA: 0x001F2AAC File Offset: 0x001F1AAC
	public sprᡄ(Stream A_0) : this(A_0, false)
	{
	}

	// Token: 0x06003775 RID: 14197 RVA: 0x001F2AC4 File Offset: 0x001F1AC4
	public sprᡄ(Stream A_0, bool A_1)
	{
		this.ᜆ = new spr\u24E5(this.ᜅ);
		this.ᜃ = A_1;
		this.ᜁ = A_0;
		this.ᜄ = new BinaryWriter(this.ᜁ);
	}

	// Token: 0x06003776 RID: 14198 RVA: 0x001F2B18 File Offset: 0x001F1B18
	public void ᜁ()
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_62;
			case 1:
				this.ᜁ.SetLength(this.ᜁ.Position);
				((IDisposable)this.ᜁ).Dispose();
				num = 0;
				continue;
			case 2:
				if (this.ᜃ)
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				goto IL_C8;
			case 3:
				return;
			}
			if (this.ᜂ)
			{
				num = 3;
			}
			else
			{
				this.ᜂ = true;
				this.ᜄ.Flush();
				num = 2;
			}
		}
		return;
		IL_62:
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
		IL_C8:
		this.ᜁ = null;
		this.ᜆ = null;
	}

	// Token: 0x06003777 RID: 14199 RVA: 0x001F2BFC File Offset: 0x001F1BFC
	public void ᜀ(BiffRecordRaw A_0, IEncryptor A_1)
	{
		int a_ = 4;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_42;
			case 1:
				if (!A_0.NeedDataArray)
				{
					if (true)
					{
					}
					num = 4;
					continue;
				}
				return;
			case 3:
				return;
			case 4:
				A_0.ClearData();
				A_0.NeedInfill = true;
				num = 3;
				continue;
			}
			if (A_0 != null)
			{
				A_0.FillStream(this.ᜄ, this.ᜆ, A_1, (int)this.ᜄ.BaseStream.Position);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
			}
			num = 0;
		}
		IL_42:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠹崻䤽", a_));
	}

	// Token: 0x06003778 RID: 14200 RVA: 0x001F2CE4 File Offset: 0x001F1CE4
	[CLSCompliant(false)]
	public void ᜀ(RecordArrayList A_0, IEncryptor A_1)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num;
			int num2;
			int count;
			int num3;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_D6:
				num = (int)this.ᜄ.BaseStream.Position;
				num2 = 0;
				count = A_0.Count;
				num3 = 0;
				break;
			default:
				if (false)
				{
				}
				num3 = 4;
				break;
			}
			for (;;)
			{
				switch (num3)
				{
				case 0:
					goto IL_FF;
				case 1:
					num3 = 6;
					continue;
				case 2:
				{
					IRecordStorage recordStorage;
					spr\u251F spr_u251F = recordStorage as spr\u251F;
					spr_u251F.Data = new byte[0];
					spr_u251F.NeedInfill = true;
					num3 = 5;
					continue;
				}
				case 3:
					goto IL_FF;
				case 5:
					goto IL_139;
				case 6:
				{
					IRecordStorage recordStorage;
					if (recordStorage is spr\u251F)
					{
						num3 = 2;
						continue;
					}
					goto IL_139;
				}
				case 7:
				{
					if (num2 >= count)
					{
						num3 = 10;
						continue;
					}
					IRecordStorage recordStorage = A_0[num2];
					num += recordStorage.FillStream(this.ᜄ, this.ᜆ, A_1, num);
					num3 = 8;
					continue;
				}
				case 8:
				{
					IRecordStorage recordStorage;
					if (!recordStorage.NeedDataArray)
					{
						num3 = 1;
						continue;
					}
					goto IL_139;
				}
				case 9:
					goto IL_7D;
				case 10:
					goto IL_11B;
				}
				if (A_0 == null)
				{
					num3 = 9;
					continue;
				}
				goto IL_D6;
				IL_FF:
				num3 = 7;
				continue;
				IL_139:
				num2++;
				num3 = 3;
			}
			IL_7D:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙃⍅⭇╉㹋⩍⍏", a_));
			IL_11B:
			if (true)
			{
			}
			return;
		}
		}
	}

	// Token: 0x06003779 RID: 14201 RVA: 0x001F2E80 File Offset: 0x001F1E80
	public void ᜀ(ICollection A_0, IEncryptor A_1)
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				IEnumerator enumerator;
				int num2;
				switch (num)
				{
				case 0:
					goto IL_5B;
					try
					{
						for (;;)
						{
							IL_5B:
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									num = 4;
									continue;
								case 3:
								{
									BiffRecordRaw biffRecordRaw;
									biffRecordRaw.Data = new byte[0];
									biffRecordRaw.NeedInfill = true;
									num = 2;
									continue;
								}
								case 4:
									goto IL_132;
								case 5:
								{
									BiffRecordRaw biffRecordRaw;
									if (!biffRecordRaw.NeedDataArray)
									{
										num = 3;
										continue;
									}
									break;
								}
								case 6:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_5B;
									default:
									{
										if (false)
										{
										}
										if (!enumerator.MoveNext())
										{
											num = 0;
											continue;
										}
										BiffRecordRaw biffRecordRaw = (BiffRecordRaw)enumerator.Current;
										num2 += biffRecordRaw.FillStream(this.ᜄ, this.ᜆ, A_1, num2);
										num = 5;
										continue;
									}
									}
									break;
								}
								IL_CA:
								num = 6;
								continue;
								goto IL_CA;
							}
						}
						IL_132:
						return;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable = enumerator as IDisposable;
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									disposable.Dispose();
									num = 1;
									continue;
								case 1:
									goto IL_179;
								case 2:
									if (disposable != null)
									{
										num = 0;
										continue;
									}
									goto IL_17B;
								}
								break;
							}
						}
						IL_179:
						IL_17B:;
					}
					goto IL_17C;
				case 1:
					goto IL_56;
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					num = 1;
					continue;
				}
				IL_17C:
				num2 = (int)this.ᜄ.BaseStream.Position;
				enumerator = A_0.GetEnumerator();
				num = 0;
			}
			IL_56:
			throw new ArgumentNullException(RecordTableEnumerator.b("≀ⱂ⥄⭆ⱈ⡊㥌♎㹐㵒", a_));
		}
		}
	}

	// Token: 0x04001868 RID: 6248
	private const int ᜀ = 1048576;

	// Token: 0x04001869 RID: 6249
	private Stream ᜁ;

	// Token: 0x0400186A RID: 6250
	private bool ᜂ;

	// Token: 0x0400186B RID: 6251
	private bool ᜃ;

	// Token: 0x0400186C RID: 6252
	private BinaryWriter ᜄ;

	// Token: 0x0400186D RID: 6253
	private byte[] ᜅ = new byte[8228];

	// Token: 0x0400186E RID: 6254
	private spr\u24E5 ᜆ;
}
