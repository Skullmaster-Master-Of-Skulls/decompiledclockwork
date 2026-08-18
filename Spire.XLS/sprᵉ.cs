using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Spire.CompoundFile.XLS;
using Spire.CompoundFile.XLS.Native;
using Spire.CompoundFile.XLS.Net;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004EA RID: 1258
internal class spr\u1D49 : IDisposable, IPropertyData
{
	// Token: 0x06004D06 RID: 19718 RVA: 0x002EF810 File Offset: 0x002EE810
	public spr\u1D49()
	{
		this.ᜎ.ᜀ = (IntPtr)1L;
		int i = 0;
		int num = 0;
		while (i < spr\u1D49.ᜉ / 4)
		{
			Marshal.WriteInt32(this.\u170D, num, 0);
			i++;
			num += 4;
		}
	}

	// Token: 0x06004D07 RID: 19719 RVA: 0x002EF8A4 File Offset: 0x002EE8A4
	public spr\u1D49(IntPtr A_0) : this()
	{
		this.ᜄ(A_0);
	}

	// Token: 0x06004D08 RID: 19720 RVA: 0x002EF8C0 File Offset: 0x002EE8C0
	internal spr\u1D49(spr\u24F0 A_0, spr\u17B9 A_1, bool A_2) : this()
	{
		this.ᜀ(A_0, A_1, A_2);
	}

	// Token: 0x06004D09 RID: 19721 RVA: 0x002EF8DC File Offset: 0x002EE8DC
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
		return this.ᜄ().ToInt32();
	}

	// Token: 0x06004D0A RID: 19722 RVA: 0x002EF928 File Offset: 0x002EE928
	public void ᜀ(int A_0)
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
		this.ᜂ();
		this.ᜀ(VarEnum.VT_INT);
		this.ᜁ((IntPtr)A_0);
		this.ᜀ(IntPtr.Zero);
	}

	// Token: 0x06004D0B RID: 19723 RVA: 0x002EF988 File Offset: 0x002EE988
	public int ᜎ()
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
		return this.ᜄ().ToInt32();
	}

	// Token: 0x06004D0C RID: 19724 RVA: 0x002EF9D4 File Offset: 0x002EE9D4
	public void ᜂ(int A_0)
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
		this.ᜂ();
		this.ᜀ(VarEnum.VT_I4);
		this.ᜁ((IntPtr)A_0);
		this.ᜀ(IntPtr.Zero);
	}

	// Token: 0x06004D0D RID: 19725 RVA: 0x002EFA34 File Offset: 0x002EEA34
	public IntPtr ᜈ()
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
		return this.\u170D;
	}

	// Token: 0x06004D0E RID: 19726 RVA: 0x002EFA78 File Offset: 0x002EEA78
	public void ᜄ(IntPtr A_0)
	{
		for (;;)
		{
			this.ᜂ();
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.\u170D = A_0;
					this.ᜐ = false;
					num = 2;
					continue;
				case 1:
					if (!(this.\u170D != A_0))
					{
						return;
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
						num = 0;
						continue;
					}
					break;
				case 2:
					return;
				}
				break;
			}
		}
	}

	// Token: 0x06004D0F RID: 19727 RVA: 0x002EFB08 File Offset: 0x002EEB08
	public PIDSI \u1714()
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
		return (PIDSI)((int)this.ᜎ.ᜁ);
	}

	// Token: 0x06004D10 RID: 19728 RVA: 0x002EFB54 File Offset: 0x002EEB54
	public void ᜀ(PIDSI A_0)
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
		this.ᜎ.ᜀ = (IntPtr)1L;
		this.ᜎ.ᜁ = (IntPtr)((long)A_0);
	}

	// Token: 0x06004D11 RID: 19729 RVA: 0x002EFBB4 File Offset: 0x002EEBB4
	public PIDDSI \u1717()
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
		return (PIDDSI)((int)this.ᜎ.ᜁ);
	}

	// Token: 0x06004D12 RID: 19730 RVA: 0x002EFC00 File Offset: 0x002EEC00
	public void ᜀ(PIDDSI A_0)
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
		this.ᜎ.ᜁ = (IntPtr)((long)A_0);
	}

	// Token: 0x06004D13 RID: 19731 RVA: 0x002EFC50 File Offset: 0x002EEC50
	public System.Runtime.InteropServices.ComTypes.FILETIME ᜐ()
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
		System.Runtime.InteropServices.ComTypes.FILETIME result = default(System.Runtime.InteropServices.ComTypes.FILETIME);
		long num = Marshal.ReadInt64(this.\u170D, 8);
		result.dwLowDateTime = (int)(num & (long)((ulong)-1));
		result.dwHighDateTime = (int)(num >> 32 & (long)((ulong)-1));
		return result;
	}

	// Token: 0x06004D14 RID: 19732 RVA: 0x002EFCBC File Offset: 0x002EECBC
	public void ᜀ(System.Runtime.InteropServices.ComTypes.FILETIME A_0)
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
		this.ᜀ(VarEnum.VT_FILETIME);
		long val = ((long)A_0.dwHighDateTime << 32) + (long)((ulong)A_0.dwLowDateTime);
		Marshal.WriteInt64(this.\u170D, 8, val);
	}

	// Token: 0x06004D15 RID: 19733 RVA: 0x002EFD24 File Offset: 0x002EED24
	public bool \u1713()
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
		return this.ᜄ() != IntPtr.Zero;
	}

	// Token: 0x06004D16 RID: 19734 RVA: 0x002EFD70 File Offset: 0x002EED70
	public void ᜀ(bool A_0)
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
		this.ᜀ(VarEnum.VT_BOOL);
		this.ᜁ((IntPtr)(A_0 ? 1 : 0));
		this.ᜀ(IntPtr.Zero);
	}

	// Token: 0x06004D17 RID: 19735 RVA: 0x002EFDD4 File Offset: 0x002EEDD4
	public string ᜏ()
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
		return Marshal.PtrToStringUni(this.ᜄ());
	}

	// Token: 0x06004D18 RID: 19736 RVA: 0x002EFE1C File Offset: 0x002EEE1C
	public void ᜁ(string A_0)
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
		IntPtr intPtr = Marshal.StringToHGlobalUni(A_0);
		this.ᜀ(VarEnum.VT_LPWSTR);
		this.ᜁ(intPtr);
		this.ᜀ(IntPtr.Zero);
		this.ᜋ.Add(intPtr);
	}

	// Token: 0x06004D19 RID: 19737 RVA: 0x002EFE84 File Offset: 0x002EEE84
	public DateTime ᜋ()
	{
		DateTime result;
		for (;;)
		{
			System.Runtime.InteropServices.ComTypes.FILETIME filetime = this.ᜐ();
			long num = ((long)filetime.dwHighDateTime << 32) + (long)((ulong)filetime.dwLowDateTime);
			num += 504911232000000000L;
			result = new DateTime(num);
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (this.\u1714() == PIDSI.EditTime)
					{
						return result;
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
						num2 = 2;
						continue;
					}
					break;
				case 1:
					return result;
				case 2:
					result = result.ToLocalTime();
					if (true)
					{
					}
					num2 = 1;
					continue;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x06004D1A RID: 19738 RVA: 0x002EFF38 File Offset: 0x002EEF38
	public void ᜀ(DateTime A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				A_0 = A_0.ToUniversalTime();
				num = 2;
				continue;
			case 2:
				goto IL_6F;
			}
			IL_1C:
			if (true)
			{
			}
			if (this.\u1714() == PIDSI.EditTime)
			{
				break;
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
				num = 1;
				continue;
			}
			goto IL_1C;
		}
		IL_6F:
		ulong num2 = (ulong)(A_0.Ticks - 504911232000000000L);
		this.ᜀ(new System.Runtime.InteropServices.ComTypes.FILETIME
		{
			dwHighDateTime = (int)((num2 & 18446744069414584320UL) >> 32),
			dwLowDateTime = (int)(num2 & (ulong)-1)
		});
	}

	// Token: 0x06004D1B RID: 19739 RVA: 0x002EFFFC File Offset: 0x002EEFFC
	public double \u1715()
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
		long value = Marshal.ReadInt64(this.\u170D, 8);
		return BitConverter.Int64BitsToDouble(value);
	}

	// Token: 0x06004D1C RID: 19740 RVA: 0x002F004C File Offset: 0x002EF04C
	public void ᜀ(double A_0)
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
		this.ᜀ(VarEnum.VT_R8);
		long val = BitConverter.DoubleToInt64Bits(A_0);
		Marshal.WriteInt64(this.\u170D, 8, val);
	}

	// Token: 0x06004D1D RID: 19741 RVA: 0x002F00A4 File Offset: 0x002EF0A4
	public string \u1716()
	{
		while (this.ᜎ.ᜀ == (IntPtr)0L)
		{
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
				IntPtr ptr = this.ᜎ.ᜁ;
				return Marshal.PtrToStringUni(ptr);
			}
			}
		}
		return null;
	}

	// Token: 0x06004D1E RID: 19742 RVA: 0x002F0110 File Offset: 0x002EF110
	public void ᜂ(string A_0)
	{
		int a_ = 7;
		while (A_0 == null)
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
				throw new ArgumentNullException(RecordTableEnumerator.b("䬼帾ⵀ㙂⁄", a_));
			}
		}
		this.ᜀ(A_0);
	}

	// Token: 0x06004D1F RID: 19743 RVA: 0x002F0174 File Offset: 0x002EF174
	public object \u1712()
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
		return this.ᜀ();
	}

	// Token: 0x06004D20 RID: 19744 RVA: 0x002F01B8 File Offset: 0x002EF1B8
	public bool ᜑ()
	{
		while (this.ᜎ.ᜀ == (IntPtr)1L)
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
				return (this.ᜎ.ᜁ.ToInt32() & 16777216) != 0;
			}
		}
		return false;
	}

	// Token: 0x06004D21 RID: 19745 RVA: 0x002F022C File Offset: 0x002EF22C
	public int \u1718()
	{
		while (!this.ᜑ())
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
				return this.ᜎ.ᜁ.ToInt32();
			}
		}
		if (true)
		{
		}
		return this.ᜎ.ᜁ.ToInt32() - 16777216;
	}

	// Token: 0x06004D22 RID: 19746 RVA: 0x002F0298 File Offset: 0x002EF298
	public int ᜇ()
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
		return this.ᜎ.ᜁ.ToInt32();
	}

	// Token: 0x06004D23 RID: 19747 RVA: 0x002F02E4 File Offset: 0x002EF2E4
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
		this.ᜎ.ᜀ = (IntPtr)1L;
		this.ᜎ.ᜁ = (IntPtr)A_0;
	}

	// Token: 0x06004D24 RID: 19748 RVA: 0x002F0344 File Offset: 0x002EF344
	public string[] ᜌ()
	{
		switch (0)
		{
		default:
		{
			string[] array;
			for (;;)
			{
				int num = this.ᜄ().ToInt32();
				array = new string[num];
				IntPtr ptr = this.ᜃ();
				int num2 = 0;
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_5C;
					case 1:
						return array;
					case 2:
						goto IL_5C;
					case 3:
						if (num2 >= num)
						{
							num3 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							IntPtr a_ = Marshal.ReadIntPtr(ptr, num2 * IntPtr.Size);
							array[num2] = this.ᜃ(a_);
							num2++;
							num3 = 0;
							continue;
						}
						}
						break;
					}
					break;
					IL_5C:
					num3 = 3;
				}
			}
			return array;
		}
		}
	}

	// Token: 0x06004D25 RID: 19749 RVA: 0x002F0418 File Offset: 0x002EF418
	private string ᜃ(IntPtr A_0)
	{
		int a_ = 9;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_48;
			case 1:
			{
				int num2;
				int num3;
				if (num2 == num3)
				{
					goto IL_B2;
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
					if (true)
					{
					}
					num = 3;
					continue;
				}
				break;
			}
			case 3:
				goto IL_9C;
			}
			if (A_0 == IntPtr.Zero)
			{
				num = 0;
			}
			else
			{
				int num2 = (int)(this.ᜉ() & (VarEnum)255);
				int num3 = 30;
				num = 1;
			}
		}
		IL_48:
		throw new ArgumentNullException(RecordTableEnumerator.b("伾㕀ㅂᙄ㍆㭈≊⍌⡎", a_));
		IL_9C:
		return Marshal.PtrToStringUni(A_0);
		IL_B2:
		return this.ᜂ(A_0);
	}

	// Token: 0x06004D26 RID: 19750 RVA: 0x002F04E0 File Offset: 0x002EF4E0
	private string ᜂ(IntPtr A_0)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 3;
			int num2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_D8;
				case 1:
					goto IL_7B;
				case 2:
					if (Marshal.ReadByte(A_0, num2) == 0)
					{
						num = 0;
						continue;
					}
					num2++;
					num = 5;
					continue;
				case 4:
					goto IL_B7;
				case 5:
					goto IL_B7;
				}
				if (A_0 == IntPtr.Zero)
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
						num = 1;
						continue;
					}
				}
				else
				{
					num2 = 0;
				}
				num = 4;
				continue;
				IL_B7:
				num = 2;
			}
			IL_7B:
			throw new ArgumentNullException(RecordTableEnumerator.b("㕄㍆㭈ᡊ㥌㵎㡐㵒㉔", a_));
			IL_D8:
			byte[] array = new byte[num2];
			Marshal.Copy(A_0, array, 0, num2);
			Encoding @default = Encoding.Default;
			return @default.GetString(array, 0, num2);
		}
		}
	}

	// Token: 0x06004D27 RID: 19751 RVA: 0x002F05E8 File Offset: 0x002EF5E8
	public object[] ᜊ()
	{
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			object[] array;
			for (;;)
			{
				int num = this.ᜄ().ToInt32();
				array = new object[num];
				this.ᜃ();
				int num2 = 0;
				int num3 = 0;
				int num4 = 3;
				for (;;)
				{
					switch (num4)
					{
					case 0:
						if (num2 >= num)
						{
							num4 = 2;
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
							spr\u1D49 spr_u1D = new spr\u1D49((IntPtr)(this.ᜃ().ToInt64() + (long)num3));
							array[num2] = spr_u1D.ᜀ();
							num2++;
							num3 += spr\u1D49.ᜉ;
							num4 = 1;
							continue;
						}
						}
						break;
					case 1:
						goto IL_66;
					case 2:
						return array;
					case 3:
						goto IL_66;
					}
					break;
					IL_66:
					num4 = 0;
				}
			}
			return array;
		}
		}
	}

	// Token: 0x06004D28 RID: 19752 RVA: 0x002F06D4 File Offset: 0x002EF6D4
	public void ᜀ(string[] A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			IntPtr intPtr;
			for (;;)
			{
				this.ᜂ();
				num = A_0.Length;
				IntPtr[] array = new IntPtr[num];
				intPtr = Marshal.AllocHGlobal(num * IntPtr.Size);
				this.ᜌ.Add(intPtr);
				int num2 = 0;
				int num3 = 2;
				for (;;)
				{
					int num4;
					int num5;
					switch (num3)
					{
					case 0:
						goto IL_146;
					case 1:
						goto IL_126;
					case 2:
						goto IL_148;
					case 3:
						if (num2 >= num)
						{
							num3 = 6;
							continue;
						}
						array[num2] = Marshal.StringToHGlobalUni(A_0[num2]);
						this.ᜋ.Add(array[num2]);
						num2++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_167;
						default:
							if (false)
							{
							}
							num3 = 4;
							continue;
						}
						break;
					case 4:
						if (true)
						{
						}
						goto IL_148;
					case 5:
						goto IL_126;
					case 6:
						goto IL_167;
					case 7:
						if (num4 >= num)
						{
							num3 = 0;
							continue;
						}
						Marshal.WriteIntPtr(intPtr, num5, array[num4]);
						num5 += IntPtr.Size;
						num4++;
						num3 = 5;
						continue;
					}
					break;
					IL_126:
					num3 = 7;
					continue;
					IL_148:
					num3 = 3;
					continue;
					IL_167:
					num5 = 0;
					num4 = 0;
					num3 = 1;
				}
			}
			IL_146:
			this.ᜀ((VarEnum)4127);
			this.ᜁ((IntPtr)num);
			this.ᜀ(intPtr);
			return;
		}
		}
	}

	// Token: 0x06004D29 RID: 19753 RVA: 0x002F086C File Offset: 0x002EF86C
	public void ᜀ(object[] A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			IntPtr intPtr;
			for (;;)
			{
				this.ᜂ();
				this.ᜀ((VarEnum)4108);
				num = A_0.Length;
				intPtr = Marshal.AllocHGlobal(spr\u1D49.ᜉ * num);
				this.ᜌ.Add(intPtr);
				int num2 = 0;
				int num3 = 0;
				int num4 = 6;
				for (;;)
				{
					switch (num4)
					{
					case 0:
						if (num2 >= num)
						{
							num4 = 3;
							continue;
						}
						num4 = 7;
						continue;
					case 1:
						goto IL_8A;
					case 2:
						goto IL_8A;
					case 3:
						goto IL_134;
					case 4:
						goto IL_F9;
					case 5:
					{
						spr\u1D49 spr_u1D = new spr\u1D49((IntPtr)(intPtr.ToInt64() + (long)num3));
						spr_u1D.ᜀ((int)A_0[num2]);
						spr_u1D.ᜀ(VarEnum.VT_I4);
						this.ᜏ.Add(spr_u1D);
						num4 = 2;
						continue;
					}
					case 6:
						goto IL_F9;
					case 7:
						if (A_0[num2] is int)
						{
							num4 = 5;
							continue;
						}
						num4 = 9;
						continue;
					case 8:
					{
						spr\u1D49 spr_u1D = new spr\u1D49((IntPtr)(intPtr.ToInt64() + (long)num3));
						spr_u1D.ᜁ((string)A_0[num2]);
						this.ᜏ.Add(spr_u1D);
						num4 = 1;
						continue;
					}
					case 9:
						IL_BA:
						if (A_0[num2] is string)
						{
							num4 = 8;
							continue;
						}
						goto IL_8A;
					}
					break;
					IL_8A:
					num2++;
					num3 += spr\u1D49.ᜉ;
					if (true)
					{
					}
					num4 = 4;
					continue;
					IL_F9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BA;
					default:
						if (false)
						{
						}
						num4 = 0;
						break;
					}
				}
			}
			IL_134:
			this.ᜁ((IntPtr)num);
			this.ᜀ(intPtr);
			return;
		}
		}
	}

	// Token: 0x06004D2A RID: 19754 RVA: 0x002F0A50 File Offset: 0x002EFA50
	public void ᜀ(byte[] A_0)
	{
		int a_ = 2;
		if (A_0 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3C;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("丷嬹倻䬽┿", a_));
		}
		this.ᜂ();
		this.ᜀ(VarEnum.VT_BLOB);
		int num = A_0.Length;
		this.ᜁ((IntPtr)num);
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.Copy(A_0, 0, intPtr, num);
		this.ᜀ(intPtr);
		this.ᜌ.Add(intPtr);
	}

	// Token: 0x06004D2B RID: 19755 RVA: 0x002F0AF0 File Offset: 0x002EFAF0
	public void ᜀ(string A_0)
	{
		int a_ = 18;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (A_0.Length == 0)
				{
					num = 3;
					continue;
				}
				goto IL_A6;
			case 1:
				goto IL_34;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_54;
				default:
					goto IL_8A;
				}
				break;
			}
			if (A_0 == null)
			{
				num = 1;
				continue;
			}
			IL_54:
			num = 0;
		}
		IL_34:
		throw new ArgumentNullException(RecordTableEnumerator.b("㭇㹉㹋Mㅏ㽑ㅓ", a_));
		IL_8A:
		if (false)
		{
		}
		throw new ArgumentException(RecordTableEnumerator.b("㭇㹉㹋Mㅏ㽑ㅓ癕畗穙⽛⩝቟ୡ੣ť䡧३൫mṯᵱs噵᩷ό屻᭽ﾅꚇ", a_));
		IL_A6:
		this.ᜁ();
		this.ᜎ.ᜀ = (IntPtr)0L;
		IntPtr intPtr = Marshal.StringToHGlobalUni(A_0);
		this.ᜎ.ᜁ = intPtr;
	}

	// Token: 0x06004D2C RID: 19756 RVA: 0x002F0BD0 File Offset: 0x002EFBD0
	public bool ᜀ(object A_0, PropertyType A_1)
	{
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_194;
				case 1:
					if (A_1 != PropertyType.Int)
					{
						num = 8;
						continue;
					}
					this.ᜀ((int)A_0);
					num = 19;
					continue;
				case 2:
					return true;
				case 3:
					if (A_1 <= PropertyType.Int)
					{
						num = 5;
						continue;
					}
					if (true)
					{
					}
					num = 9;
					continue;
				case 4:
					if (A_1 != PropertyType.ObjectArray)
					{
						num = 16;
						continue;
					}
					this.ᜀ((object[])A_0);
					num = 10;
					continue;
				case 5:
					num = 28;
					continue;
				case 6:
					goto IL_1CD;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_213;
					default:
						if (false)
						{
						}
						num = 25;
						continue;
					}
					break;
				case 8:
					goto IL_213;
				case 9:
					if (A_1 <= PropertyType.Blob)
					{
						num = 20;
						continue;
					}
					num = 4;
					continue;
				case 10:
					goto IL_184;
				case 11:
					goto IL_1DF;
				case 12:
					goto IL_21E;
				case 13:
					goto IL_C6;
				case 14:
					num = 21;
					continue;
				case 15:
					if (A_1 != PropertyType.StringArray)
					{
						num = 22;
						continue;
					}
					this.ᜀ((string[])A_0);
					num = 6;
					continue;
				case 16:
					num = 15;
					continue;
				case 17:
					goto IL_168;
				case 18:
					goto IL_E2;
				case 19:
					return true;
				case 20:
					num = 24;
					continue;
				case 21:
					if (A_1 != PropertyType.Bool)
					{
						num = 26;
						continue;
					}
					this.ᜀ((bool)A_0);
					num = 18;
					continue;
				case 22:
					num = 0;
					continue;
				case 23:
					goto IL_129;
				case 24:
					if (A_1 != PropertyType.String)
					{
						num = 7;
						continue;
					}
					this.ᜁ(A_0.ToString());
					num = 27;
					continue;
				case 25:
					switch (A_1)
					{
					case PropertyType.DateTime:
						this.ᜀ((DateTime)A_0);
						num = 17;
						continue;
					case PropertyType.Blob:
						this.ᜀ((byte[])A_0);
						num = 23;
						continue;
					default:
						num = 29;
						continue;
					}
					break;
				case 26:
					num = 1;
					continue;
				case 27:
					return true;
				case 28:
					switch (A_1)
					{
					case PropertyType.Int32:
						this.ᜂ((int)A_0);
						num = 2;
						continue;
					case (PropertyType)4:
						return false;
					case PropertyType.Double:
						this.ᜀ((double)A_0);
						num = 13;
						continue;
					default:
						num = 14;
						continue;
					}
					break;
				case 29:
					num = 11;
					continue;
				}
				break;
				IL_213:
				num = 12;
			}
		}
		IL_C6:
		IL_E2:
		IL_129:
		IL_168:
		IL_184:
		return true;
		IL_194:
		return false;
		IL_1CD:
		return true;
		IL_1DF:
		IL_21E:
		return false;
	}

	// Token: 0x06004D2D RID: 19757 RVA: 0x002F0F00 File Offset: 0x002EFF00
	private IntPtr ᜄ()
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
		return Marshal.ReadIntPtr(this.\u170D, 8);
	}

	// Token: 0x06004D2E RID: 19758 RVA: 0x002F0F48 File Offset: 0x002EFF48
	private void ᜁ(IntPtr A_0)
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
		Marshal.WriteIntPtr(this.\u170D, 8, A_0);
	}

	// Token: 0x06004D2F RID: 19759 RVA: 0x002F0F90 File Offset: 0x002EFF90
	private IntPtr ᜃ()
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
		return Marshal.ReadIntPtr(this.\u170D, spr\u1D49.ᜊ);
	}

	// Token: 0x06004D30 RID: 19760 RVA: 0x002F0FDC File Offset: 0x002EFFDC
	private void ᜀ(IntPtr A_0)
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
		Marshal.WriteIntPtr(this.\u170D, spr\u1D49.ᜊ, A_0);
	}

	// Token: 0x06004D31 RID: 19761 RVA: 0x002F1028 File Offset: 0x002F0028
	public VarEnum ᜉ()
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
		return (VarEnum)Marshal.ReadInt16(this.\u170D, 0);
	}

	// Token: 0x06004D32 RID: 19762 RVA: 0x002F1070 File Offset: 0x002F0070
	public void ᜀ(VarEnum A_0)
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
		Marshal.WriteInt16(this.\u170D, 0, (short)A_0);
	}

	// Token: 0x06004D33 RID: 19763 RVA: 0x002F10BC File Offset: 0x002F00BC
	private void ᜂ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int count = this.ᜋ.Count;
				int num2 = 6;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						int num3;
						int count2;
						if (num3 >= count2)
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
								num2 = 8;
								continue;
							}
						}
						IntPtr intPtr = this.ᜌ[num3];
						Marshal.FreeHGlobal(intPtr);
						num3++;
						num2 = 1;
						continue;
					}
					case 1:
						goto IL_83;
					case 2:
						goto IL_BE;
					case 3:
					{
						if (true)
						{
						}
						int num4;
						if (num4 >= this.ᜏ.Count)
						{
							num2 = 4;
							continue;
						}
						this.ᜏ[num4].ᜅ();
						num4++;
						num2 = 5;
						continue;
					}
					case 4:
						goto IL_10E;
					case 5:
						goto IL_DF;
					case 6:
						goto IL_BE;
					case 7:
					{
						this.ᜋ.Clear();
						int num3 = 0;
						int count2 = this.ᜌ.Count;
						num2 = 10;
						continue;
					}
					case 8:
					{
						this.ᜌ.Clear();
						int num4 = 0;
						num2 = 9;
						continue;
					}
					case 9:
						goto IL_DF;
					case 10:
						goto IL_83;
					case 11:
					{
						if (num >= count)
						{
							num2 = 7;
							continue;
						}
						IntPtr intPtr = this.ᜋ[num];
						Marshal.FreeCoTaskMem(intPtr);
						num++;
						num2 = 2;
						continue;
					}
					}
					break;
					IL_83:
					num2 = 0;
					continue;
					IL_BE:
					num2 = 11;
					continue;
					IL_DF:
					num2 = 3;
				}
			}
			IL_10E:
			this.ᜏ.Clear();
			return;
		}
	}

	// Token: 0x06004D34 RID: 19764 RVA: 0x002F1288 File Offset: 0x002F0288
	private void ᜁ()
	{
		for (;;)
		{
			int num = (int)this.ᜎ.ᜁ;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_35;
					default:
					{
						if (false)
						{
						}
						if (true)
						{
						}
						IntPtr hglobal = (IntPtr)num;
						Marshal.FreeHGlobal(hglobal);
						this.ᜎ.ᜁ = IntPtr.Zero;
						num2 = 0;
						continue;
					}
					}
					break;
				case 2:
					goto IL_35;
				case 3:
					if (num != 0)
					{
						num2 = 1;
						continue;
					}
					return;
				case 4:
					num2 = 3;
					continue;
				}
				break;
				IL_35:
				if (!(this.ᜎ.ᜀ == (IntPtr)0L))
				{
					return;
				}
				num2 = 4;
			}
		}
	}

	// Token: 0x06004D35 RID: 19765 RVA: 0x002F1360 File Offset: 0x002F0360
	private object ᜀ()
	{
		switch (0)
		{
		default:
		{
			IntPtr ptr;
			string text;
			for (;;)
			{
				VarEnum varEnum = this.ᜉ();
				ptr = (IntPtr)(this.\u170D.ToInt64() + 8L);
				VarEnum varEnum2 = varEnum;
				int num = 16;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (varEnum == VarEnum.VT_LPSTR)
						{
							num = 13;
							continue;
						}
						return text;
					case 1:
						num = 4;
						continue;
					case 2:
						num = 15;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_13E;
						default:
							if (false)
							{
							}
							num = 22;
							continue;
						}
						break;
					case 4:
						switch (varEnum2)
						{
						case VarEnum.VT_I4:
							goto IL_335;
						case VarEnum.VT_R4:
							goto IL_341;
						case VarEnum.VT_R8:
							goto IL_E7;
						default:
							num = 2;
							continue;
						}
						break;
					case 5:
						switch (varEnum2)
						{
						case VarEnum.VT_LPSTR:
						case VarEnum.VT_LPWSTR:
							text = this.ᜃ(this.ᜄ());
							num = 0;
							continue;
						default:
							num = 21;
							continue;
						}
						break;
					case 6:
						if (varEnum2 != VarEnum.VT_INT)
						{
							goto IL_13E;
						}
						goto IL_335;
					case 7:
						num = 11;
						continue;
					case 8:
						num = 6;
						continue;
					case 9:
						num = 19;
						continue;
					case 10:
						num = 5;
						continue;
					case 11:
						goto IL_209;
					case 12:
						goto IL_189;
					case 13:
						this.ᜁ(text);
						num = 20;
						continue;
					case 14:
						if (varEnum2 != (VarEnum)4108)
						{
							num = 9;
							continue;
						}
						goto IL_1F6;
					case 15:
						if (varEnum2 != VarEnum.VT_BOOL)
						{
							num = 8;
							continue;
						}
						goto IL_2CE;
					case 16:
						if (varEnum2 <= VarEnum.VT_INT)
						{
							num = 1;
							continue;
						}
						num = 23;
						continue;
					case 17:
						num = 12;
						continue;
					case 18:
						switch (varEnum2)
						{
						case VarEnum.VT_FILETIME:
							goto IL_11D;
						case VarEnum.VT_BLOB:
							goto IL_1C5;
						default:
							num = 7;
							continue;
						}
						break;
					case 19:
						switch (varEnum2)
						{
						case (VarEnum)4126:
						case (VarEnum)4127:
							goto IL_167;
						default:
							num = 17;
							continue;
						}
						break;
					case 20:
						goto IL_162;
					case 21:
						if (true)
						{
						}
						num = 18;
						continue;
					case 22:
						goto IL_268;
					case 23:
						if (varEnum2 <= VarEnum.VT_BLOB)
						{
							num = 10;
							continue;
						}
						num = 14;
						continue;
					}
					break;
					IL_13E:
					num = 3;
				}
			}
			IL_E7:
			long value = Marshal.ReadInt64(ptr);
			return BitConverter.Int64BitsToDouble(value);
			IL_11D:
			return this.ᜋ();
			IL_162:
			return text;
			IL_167:
			object result = this.ᜌ();
			this.ᜀ((VarEnum)4127);
			return result;
			IL_189:
			goto IL_341;
			IL_1C5:
			int num2 = this.ᜄ().ToInt32();
			byte[] array = new byte[num2];
			IntPtr source = this.ᜃ();
			Marshal.Copy(source, array, 0, num2);
			return array;
			IL_1F6:
			return this.ᜊ();
			IL_209:
			IL_268:
			goto IL_341;
			IL_2CE:
			return Marshal.ReadInt32(ptr) != 0;
			IL_335:
			return Marshal.ReadInt32(ptr);
			IL_341:
			return null;
		}
		}
	}

	// Token: 0x06004D36 RID: 19766 RVA: 0x002F16B0 File Offset: 0x002F06B0
	[CLSCompliant(false)]
	public void ᜀ(spr\u17B9 A_0)
	{
		PID a_;
		for (;;)
		{
			IL_14:
			a_ = PID.PID_FIRST_USABLE;
			for (;;)
			{
				IL_16:
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜎ.ᜀ == (IntPtr)1L)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						goto IL_8D;
					case 1:
						goto IL_8B;
					case 2:
						a_ = (PID)((int)this.ᜎ.ᜁ);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_16;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					goto IL_14;
				}
			}
		}
		IL_8B:
		IL_8D:
		A_0.ᜀ(1U, ref this.ᜎ, this.ᜈ(), a_);
	}

	// Token: 0x06004D37 RID: 19767 RVA: 0x002F1760 File Offset: 0x002F0760
	internal void ᜀ(spr\u24F0 A_0, spr\u17B9 A_1, bool A_2)
	{
		for (;;)
		{
			this.ᜎ.ᜁ = (IntPtr)((long)((ulong)A_0.ᜁ));
			this.ᜎ.ᜀ = (IntPtr)1L;
			A_1.ᜀ(1U, ref this.ᜎ, this.\u170D);
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_61;
					}
					if (false)
					{
					}
					this.ᜂ(A_0.ᜀ);
					num = 3;
					continue;
				case 1:
					if (true)
					{
					}
					num = 2;
					continue;
				case 2:
					if ((this.ᜎ.ᜁ.ToInt32() & 16777216) == 0)
					{
						num = 0;
						continue;
					}
					return;
				case 3:
					return;
				case 4:
					goto IL_61;
				}
				break;
				IL_61:
				if (A_2)
				{
					return;
				}
				num = 1;
			}
		}
	}

	// Token: 0x06004D38 RID: 19768 RVA: 0x002F1858 File Offset: 0x002F0858
	[CLSCompliant(false)]
	public void ᜀ(spr\u17B9 A_0, bool A_1)
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
		A_0.ᜀ(1U, ref this.ᜎ, this.\u170D);
	}

	// Token: 0x06004D39 RID: 19769 RVA: 0x002F18A8 File Offset: 0x002F08A8
	public void ᜅ()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜂ();
				this.ᜁ();
				num = 4;
				continue;
			case 1:
				goto IL_92;
			case 2:
				if (true)
				{
				}
				Marshal.FreeHGlobal(this.\u170D);
				this.\u170D = IntPtr.Zero;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_39;
				}
				if (false)
				{
				}
				num = 1;
				continue;
			case 4:
				if (this.ᜐ)
				{
					num = 2;
					continue;
				}
				goto IL_C0;
			}
			goto IL_24;
			IL_39:
			num = 0;
			continue;
			IL_24:
			if (this.\u170D != IntPtr.Zero)
			{
				goto IL_39;
			}
			break;
		}
		IL_92:
		IL_C0:
		GC.SuppressFinalize(this);
	}

	// Token: 0x06004D3A RID: 19770 RVA: 0x002F197C File Offset: 0x002F097C
	protected virtual void \u170D()
	{
		try
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜅ();
		}
		finally
		{
			base.Finalize();
		}
		if (true)
		{
		}
	}

	// Token: 0x06004D3B RID: 19771 RVA: 0x002F19D8 File Offset: 0x002F09D8
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u1D49()
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
		spr\u1D49.ᜉ = 8 + IntPtr.Size * 2;
		spr\u1D49.ᜊ = 8 + IntPtr.Size;
	}

	// Token: 0x0400230F RID: 8975
	public const int ᜀ = 0;

	// Token: 0x04002310 RID: 8976
	public const int ᜁ = 8;

	// Token: 0x04002311 RID: 8977
	private const int ᜂ = 4;

	// Token: 0x04002312 RID: 8978
	private const int ᜃ = 255;

	// Token: 0x04002313 RID: 8979
	private const long ᜄ = 4294967295L;

	// Token: 0x04002314 RID: 8980
	private const ulong ᜅ = 18446744069414584320UL;

	// Token: 0x04002315 RID: 8981
	private const int ᜆ = 32;

	// Token: 0x04002316 RID: 8982
	internal const long ᜇ = 504911232000000000L;

	// Token: 0x04002317 RID: 8983
	internal const int ᜈ = 16777216;

	// Token: 0x04002318 RID: 8984
	public static readonly int ᜉ;

	// Token: 0x04002319 RID: 8985
	public static readonly int ᜊ;

	// Token: 0x0400231A RID: 8986
	private List<IntPtr> ᜋ = new List<IntPtr>();

	// Token: 0x0400231B RID: 8987
	private List<IntPtr> ᜌ = new List<IntPtr>();

	// Token: 0x0400231C RID: 8988
	private IntPtr \u170D = Marshal.AllocHGlobal(spr\u1D49.ᜉ);

	// Token: 0x0400231D RID: 8989
	private sprḩ ᜎ = default(sprḩ);

	// Token: 0x0400231E RID: 8990
	private List<spr\u1D49> ᜏ = new List<spr\u1D49>();

	// Token: 0x0400231F RID: 8991
	private bool ᜐ = true;
}
