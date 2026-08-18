using System;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;
using Spire.XLS.File;

// Token: 0x0200006E RID: 110
internal class spr\u1DC2 : spr\u1DEE
{
	// Token: 0x06000382 RID: 898 RVA: 0x00020D68 File Offset: 0x0001FD68
	public spr\u1DC2(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x06000383 RID: 899 RVA: 0x00020D80 File Offset: 0x0001FD80
	private bool ᜃ()
	{
		string text;
		int num;
		for (;;)
		{
			text = string.Empty;
			num = (int)base.\u171C();
			int num2 = 16;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num <= base.ᜤ().ᜌ())
					{
						num2 = 12;
						continue;
					}
					return false;
				case 1:
					num2 = 15;
					continue;
				case 2:
					if (text.IndexOf('h') <= -1)
					{
						num2 = 3;
						continue;
					}
					return true;
				case 3:
					goto IL_13A;
				case 4:
					goto IL_2BD;
				case 5:
					num2 = 11;
					continue;
				case 6:
					num2 = 0;
					continue;
				case 7:
					if (num != 19)
					{
						num2 = 1;
						continue;
					}
					return true;
				case 8:
					num2 = 19;
					continue;
				case 9:
					if (text.IndexOf('d') <= -1)
					{
						num2 = 26;
						continue;
					}
					return true;
				case 10:
					if (num != 14)
					{
						num2 = 28;
						continue;
					}
					return true;
				case 11:
					if (num != 21)
					{
						num2 = 4;
						continue;
					}
					return true;
				case 12:
					text = base.ᜤ().ᜁ(num - 1);
					num2 = 22;
					continue;
				case 13:
					num2 = 20;
					continue;
				case 14:
					if (num != 15)
					{
						num2 = 8;
						continue;
					}
					return true;
				case 15:
					if (num != 20)
					{
						num2 = 5;
						continue;
					}
					return true;
				case 16:
					if (num <= spr\u2009.᠔.GetUpperBound(0))
					{
						num2 = 18;
						continue;
					}
					num -= spr\u2009.᠔.GetUpperBound(0);
					num2 = 21;
					continue;
				case 17:
					num2 = 7;
					continue;
				case 18:
					num2 = 10;
					continue;
				case 19:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EF;
					default:
						if (false)
						{
						}
						if (num != 16)
						{
							num2 = 13;
							continue;
						}
						return true;
					}
					break;
				case 20:
					if (num != 17)
					{
						num2 = 25;
						continue;
					}
					return true;
				case 21:
					if (num > 0)
					{
						num2 = 6;
						continue;
					}
					return false;
				case 22:
					if (text.IndexOf('y') <= -1)
					{
						num2 = 29;
						continue;
					}
					return true;
				case 23:
					if (text.IndexOf('m') <= -1)
					{
						num2 = 24;
						continue;
					}
					return true;
				case 24:
					num2 = 9;
					continue;
				case 25:
					num2 = 27;
					continue;
				case 26:
					num2 = 2;
					continue;
				case 27:
					if (num != 18)
					{
						num2 = 17;
						continue;
					}
					return true;
				case 28:
					num2 = 14;
					continue;
				case 29:
					goto IL_EF;
				}
				break;
				IL_EF:
				num2 = 23;
			}
		}
		return true;
		IL_13A:
		if (true)
		{
		}
		return text.IndexOf('s') > -1;
		IL_2BD:
		return num == 22;
	}

	// Token: 0x06000384 RID: 900 RVA: 0x000210A8 File Offset: 0x000200A8
	protected override BiffCellType ᜂ()
	{
		if (this.ᜃ())
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
				return BiffCellType.DateTime;
			}
		}
		return BiffCellType.Numeric;
	}

	// Token: 0x06000385 RID: 901 RVA: 0x000210F0 File Offset: 0x000200F0
	protected unsafe override double ᜄ()
	{
		switch (0)
		{
		default:
		{
			double result;
			for (;;)
			{
				fixed (IntPtr* ptr = (IntPtr*)(&base.ᜢ()[6]))
				{
					int* ptr2 = (int*)ptr;
					double num = 0.0;
					int num2 = 2;
					for (;;)
					{
						double num3;
						switch (num2)
						{
						case 0:
							goto IL_85;
						case 1:
							if ((*ptr2 & 1) == 1)
							{
								num2 = 9;
								continue;
							}
							goto IL_102;
						case 2:
						{
							if ((*ptr2 & 2) == 2)
							{
								num2 = 10;
								continue;
							}
							int* ptr3 = (int*)(&num);
							*ptr3 = 0;
							ptr3++;
							*ptr3 = (int)((long)(*ptr2) & (long)((ulong)-4));
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_EA;
							default:
								if (false)
								{
								}
								num2 = 8;
								continue;
							}
							break;
						}
						case 3:
							if ((*ptr2 & -2147483648) == -2147483648)
							{
								num2 = 4;
								continue;
							}
							num = (double)(*ptr2 >> 2);
							num2 = 7;
							continue;
						case 4:
							num = (double)(~(double)(~(*ptr2) >> 2));
							num2 = 0;
							continue;
						case 5:
							return result;
						case 6:
							goto IL_102;
						case 7:
							goto IL_85;
						case 8:
							goto IL_EA;
						case 9:
							if (true)
							{
							}
							num3 /= 100.0;
							num2 = 6;
							continue;
						case 10:
							num2 = 3;
							continue;
						}
						break;
						IL_85:
						num3 = num;
						num2 = 1;
						continue;
						IL_EA:
						goto IL_85;
						IL_102:
						result = num3;
						num2 = 5;
					}
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06000386 RID: 902 RVA: 0x00021278 File Offset: 0x00020278
	protected unsafe override void ᜀ(double A_0)
	{
		int a_ = 11;
		fixed (IntPtr* ptr = (IntPtr*)(&base.ᜢ()[6]))
		{
			int* a_2 = (int*)ptr;
			if (!sprᮌ.ᜀ(A_0, ref *a_2))
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
					throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("昦嬨䰪帬瀮爰嘲头嬶漸娺儼䨾⑀", a_)), A_0));
				}
			}
		}
	}

	// Token: 0x06000387 RID: 903 RVA: 0x00021304 File Offset: 0x00020304
	protected override object ᜀ()
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
		return this.ᜄ();
	}

	// Token: 0x06000388 RID: 904 RVA: 0x0002134C File Offset: 0x0002034C
	protected override void ᜀ(object A_0)
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
		this.ᜀ((double)A_0);
	}

	// Token: 0x06000389 RID: 905 RVA: 0x00021394 File Offset: 0x00020394
	protected override string ᜁ()
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
		return this.ᜄ().ToString();
	}

	// Token: 0x0600038A RID: 906 RVA: 0x000213E0 File Offset: 0x000203E0
	protected override void ᜀ(string A_0)
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
		double a_ = double.Parse(A_0);
		this.ᜀ(a_);
	}

	// Token: 0x0600038B RID: 907 RVA: 0x0002142C File Offset: 0x0002042C
	protected override DateTime ᜇ()
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
		return new DateTime((long)this.ᜄ());
	}

	// Token: 0x0600038C RID: 908 RVA: 0x00021474 File Offset: 0x00020474
	protected override void ᜀ(DateTime A_0)
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
		this.ᜀ((double)(A_0 - DateTime.MinValue).Ticks);
	}
}
