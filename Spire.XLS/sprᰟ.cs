using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000443 RID: 1091
internal class sprᰟ : DataProvider, IDisposable
{
	// Token: 0x060041B2 RID: 16818 RVA: 0x0024E9F8 File Offset: 0x0024D9F8
	public sprᰟ(IntPtr A_0)
	{
		this.ᜀ = A_0;
		this.ᜃ = true;
	}

	// Token: 0x060041B3 RID: 16819 RVA: 0x0024EA1C File Offset: 0x0024DA1C
	protected virtual void ᜇ()
	{
		try
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_54;
				case 1:
					base.Dispose();
					num = 0;
					continue;
				case 2:
					goto IL_78;
				}
				if (true)
				{
				}
				if (this.ᜁ != IntPtr.Zero)
				{
					num = 1;
					continue;
				}
				IL_54:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_78;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
			IL_78:;
		}
		finally
		{
			base.Finalize();
		}
	}

	// Token: 0x060041B4 RID: 16820 RVA: 0x0024EAC4 File Offset: 0x0024DAC4
	public IntPtr ᜃ()
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
		return this.ᜁ;
	}

	// Token: 0x060041B5 RID: 16821 RVA: 0x0024EB08 File Offset: 0x0024DB08
	public void ᜀ(IntPtr A_0)
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
		this.ᜁ = A_0;
		this.ᜂ = this.ᜁ.ToInt64();
	}

	// Token: 0x060041B6 RID: 16822 RVA: 0x0024EB5C File Offset: 0x0024DB5C
	public virtual int ᜄ()
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
		return this.ᜄ;
	}

	// Token: 0x060041B7 RID: 16823 RVA: 0x0024EBA0 File Offset: 0x0024DBA0
	public IntPtr ᜁ()
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

	// Token: 0x060041B8 RID: 16824 RVA: 0x0024EBE4 File Offset: 0x0024DBE4
	public virtual bool ᜅ()
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
		return this.ᜁ == IntPtr.Zero;
	}

	// Token: 0x060041B9 RID: 16825 RVA: 0x0024EC30 File Offset: 0x0024DC30
	public virtual byte ᜁ(int A_0)
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
		return Marshal.ReadByte(this.ᜁ, A_0);
	}

	// Token: 0x060041BA RID: 16826 RVA: 0x0024EC78 File Offset: 0x0024DC78
	public virtual short ᜃ(int A_0)
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
		return Marshal.ReadInt16(this.ᜁ, A_0);
	}

	// Token: 0x060041BB RID: 16827 RVA: 0x0024ECC0 File Offset: 0x0024DCC0
	public virtual int ᜂ(int A_0)
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
		return Marshal.ReadInt32(this.ᜁ, A_0);
	}

	// Token: 0x060041BC RID: 16828 RVA: 0x0024ED08 File Offset: 0x0024DD08
	public virtual long ᜀ(int A_0)
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
		return Marshal.ReadInt64(this.ᜁ, A_0);
	}

	// Token: 0x060041BD RID: 16829 RVA: 0x0024ED50 File Offset: 0x0024DD50
	public virtual void ᜀ(int A_0, byte[] A_1, int A_2, int A_3)
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
		IntPtr source = (IntPtr)(this.ᜂ + (long)A_0);
		Marshal.Copy(source, A_1, A_2, A_3);
	}

	// Token: 0x060041BE RID: 16830 RVA: 0x0024EDA4 File Offset: 0x0024DDA4
	public virtual void ᜀ(BinaryReader A_0, int A_1, int A_2, byte[] A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_53:
				if (true)
				{
				}
				int num = A_3.Length;
				long num2 = this.ᜂ;
				int num3 = 5;
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
						int num4;
						switch (num3)
						{
						case 0:
							if (num <= A_2)
							{
								num3 = 6;
								continue;
							}
							num3 = 3;
							continue;
						case 1:
							num4 = num;
							goto IL_D3;
						case 2:
							goto IL_72;
						case 3:
							num4 = A_2;
							goto IL_D3;
						case 4:
							return;
						case 5:
							goto IL_72;
						case 6:
							num3 = 1;
							continue;
						case 7:
							if (A_2 <= 0)
							{
								goto IL_7F;
							}
							num3 = 0;
							continue;
						}
						goto IL_53;
						IL_72:
						num3 = 7;
						continue;
						IL_D3:
						int num5 = num4;
						A_0.Read(A_3, 0, num5);
						IntPtr destination = (IntPtr)(num2 + (long)A_1);
						Marshal.Copy(A_3, 0, destination, num5);
						A_2 -= num5;
						A_1 += num5;
						num3 = 2;
						continue;
					}
					}
					IL_7F:
					num3 = 4;
				}
			}
			return;
		}
	}

	// Token: 0x060041BF RID: 16831 RVA: 0x0024EEC0 File Offset: 0x0024DEC0
	public virtual string ᜀ(int A_0, int A_1, Encoding A_2, bool A_3)
	{
		string result;
		for (;;)
		{
			IL_24:
			int num;
			IntPtr ptr;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_75:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				ptr = (IntPtr)(this.ᜂ + (long)A_0);
				num = 4;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					return result;
				case 1:
					goto IL_9D;
				case 2:
					return result;
				case 3:
				{
					if (A_2 == Encoding.Default)
					{
						num = 1;
						continue;
					}
					byte[] array = new byte[A_1];
					base.ReadArray(A_0, array, A_1);
					result = A_2.GetString(array);
					num = 0;
					continue;
				}
				case 4:
					if (true)
					{
					}
					if (A_3)
					{
						num = 6;
						continue;
					}
					num = 3;
					continue;
				case 5:
					return result;
				case 6:
					result = Marshal.PtrToStringUni(ptr, A_1 / 2);
					num = 5;
					continue;
				}
				goto IL_24;
			}
			IL_9D:
			result = Marshal.PtrToStringAnsi(ptr, A_1);
			goto IL_75;
		}
		return result;
	}

	// Token: 0x060041C0 RID: 16832 RVA: 0x0024EFB4 File Offset: 0x0024DFB4
	public virtual void ᜀ(int A_0, byte A_1)
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
		Marshal.WriteByte(this.ᜁ, A_0, A_1);
	}

	// Token: 0x060041C1 RID: 16833 RVA: 0x0024EFFC File Offset: 0x0024DFFC
	public virtual void ᜀ(int A_0, short A_1)
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
		Marshal.WriteInt16(this.ᜁ, A_0, A_1);
	}

	// Token: 0x060041C2 RID: 16834 RVA: 0x0024F044 File Offset: 0x0024E044
	[CLSCompliant(false)]
	public virtual void ᜀ(int A_0, ushort A_1)
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
		this.WriteInt16(A_0, (short)A_1);
	}

	// Token: 0x060041C3 RID: 16835 RVA: 0x0024F088 File Offset: 0x0024E088
	public virtual void ᜀ(int A_0, int A_1)
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
		Marshal.WriteInt32(this.ᜁ, A_0, A_1);
	}

	// Token: 0x060041C4 RID: 16836 RVA: 0x0024F0D0 File Offset: 0x0024E0D0
	public virtual void ᜀ(int A_0, long A_1)
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
		Marshal.WriteInt64(this.ᜁ, A_0, A_1);
	}

	// Token: 0x060041C5 RID: 16837 RVA: 0x0024F118 File Offset: 0x0024E118
	public virtual void ᜀ(int A_0, bool A_1, int A_2)
	{
		int a_ = 14;
		int num = 3;
		byte b;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_E3;
			case 1:
				goto IL_8B;
			case 2:
				goto IL_6C;
			case 4:
				if (A_2 > 7)
				{
					num = 5;
					continue;
				}
				b = this.ReadByte(A_0);
				num = 7;
				continue;
			case 5:
				goto IL_CA;
			case 6:
				b |= (byte)(1 << A_2);
				num = 1;
				continue;
			case 7:
				if (A_1)
				{
					num = 6;
					continue;
				}
				b &= (byte)(~(byte)(1 << A_2));
				num = 0;
				continue;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_6C:
				num = 4;
				break;
			default:
				if (false)
				{
				}
				if (A_2 < 0)
				{
					goto IL_E5;
				}
				num = 2;
				break;
			}
		}
		IL_8B:
		goto IL_107;
		IL_CA:
		goto IL_E5;
		IL_E3:
		goto IL_107;
		IL_E5:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("♃⽅㱇ᩉ⍋㵍", a_), RecordTableEnumerator.b("ك⽅㱇橉᱋⅍⍏㭑⁓㽕㝗㑙籛㵝şౡ䑣ѥ൧䩩ᙫ୭ɯᵱ味᥵੷婹᭻౽慎ꪉﲑ뒓ꆕ뚗", a_));
		IL_107:
		this.WriteByte(A_0, b);
	}

	// Token: 0x060041C6 RID: 16838 RVA: 0x0024F234 File Offset: 0x0024E234
	public virtual void ᜀ(int A_0, double A_1)
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
		byte[] bytes = BitConverter.GetBytes(A_1);
		IntPtr destination = (IntPtr)(this.ᜂ + (long)A_0);
		Marshal.Copy(bytes, 0, destination, 8);
	}

	// Token: 0x060041C7 RID: 16839 RVA: 0x0024F290 File Offset: 0x0024E290
	public virtual void ᜀ(ref int A_0, string A_1, bool A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			byte[] bytes;
			for (;;)
			{
				Encoding encoding;
				switch (num)
				{
				case 0:
					encoding = Encoding.Unicode;
					goto IL_96;
				case 1:
					num = 4;
					continue;
				case 2:
					encoding = Encoding.ASCII;
					goto IL_96;
				case 3:
					if (!A_2)
					{
						num = 5;
						continue;
					}
					num = 0;
					continue;
				case 4:
					goto IL_CE;
				case 5:
					num = 2;
					continue;
				case 6:
					goto IL_AB;
				case 8:
					goto IL_E2;
				}
				if (A_1 == null)
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
				IL_CE:
				if (A_1.Length == 0)
				{
					num = 8;
					continue;
				}
				num = 3;
				continue;
				IL_96:
				Encoding encoding2 = encoding;
				bytes = encoding2.GetBytes(A_1);
				num = 6;
			}
			return;
			IL_AB:
			Marshal.WriteByte(this.ᜁ, A_0, A_2 ? 1 : 0);
			A_0++;
			int num2 = bytes.Length;
			IntPtr destination = (IntPtr)(this.ᜂ + (long)A_0);
			Marshal.Copy(bytes, 0, destination, num2);
			A_0 += num2;
			return;
			IL_E2:
			if (true)
			{
			}
			return;
		}
		}
	}

	// Token: 0x060041C8 RID: 16840 RVA: 0x0024F3D0 File Offset: 0x0024E3D0
	public virtual void ᜁ(int A_0, byte[] A_1, int A_2, int A_3)
	{
		if (true)
		{
		}
		if (A_3 == 0)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_2E;
				}
			}
			IL_2E:
			if (false)
			{
			}
			return;
		}
		IntPtr destination = (IntPtr)(this.ᜂ + (long)A_0);
		Marshal.Copy(A_1, A_2, destination, A_3);
	}

	// Token: 0x060041C9 RID: 16841 RVA: 0x0024F42C File Offset: 0x0024E42C
	protected virtual void ᜂ()
	{
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 8;
				continue;
			case 1:
				goto IL_D0;
			case 2:
				return;
			case 3:
				goto IL_102;
			case 4:
				goto IL_61;
			case 5:
				if (true)
				{
				}
				if (this.ᜀ != IntPtr.Zero)
				{
					num = 9;
					continue;
				}
				Marshal.FreeHGlobal(this.ᜁ);
				num = 4;
				continue;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_102;
				default:
					if (false)
					{
					}
					goto IL_61;
				}
				break;
			case 8:
				if (this.ᜃ)
				{
					num = 3;
					continue;
				}
				goto IL_D0;
			case 9:
				Heap.HeapFree(this.ᜀ, 0, this.ᜁ);
				num = 6;
				continue;
			}
			if (this.ᜁ != IntPtr.Zero)
			{
				num = 0;
				continue;
			}
			break;
			IL_61:
			GC.RemoveMemoryPressure((long)this.ᜄ);
			num = 1;
			continue;
			IL_D0:
			this.ᜀ = IntPtr.Zero;
			this.ᜁ = IntPtr.Zero;
			this.ᜂ = 0L;
			this.ᜄ = 0;
			num = 2;
			continue;
			IL_102:
			num = 5;
		}
	}

	// Token: 0x060041CA RID: 16842 RVA: 0x0024F58C File Offset: 0x0024E58C
	public virtual void ᜅ(int A_0)
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
		this.EnsureCapacity(A_0, 0);
	}

	// Token: 0x060041CB RID: 16843 RVA: 0x0024F5D0 File Offset: 0x0024E5D0
	public virtual void ᜁ(int A_0, int A_1)
	{
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_1A7;
			case 1:
				goto IL_D2;
			case 2:
				num = 10;
				continue;
			case 3:
				goto IL_D2;
			case 4:
				if (this.ᜂ == 0L)
				{
					num = 9;
					continue;
				}
				GC.AddMemoryPressure((long)(A_0 - this.ᜄ));
				this.ᜄ = A_0;
				num = 0;
				continue;
			case 5:
				this.ᜁ = ((this.ᜄ > 0) ? Heap.HeapReAlloc(this.ᜀ, 0, this.ᜁ, A_0) : Heap.HeapAlloc(this.ᜀ, 0, A_0));
				num = 3;
				continue;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D2;
				default:
					if (false)
					{
					}
					num = 12;
					continue;
				}
				break;
			case 8:
				num = 11;
				continue;
			case 9:
				goto IL_106;
			case 10:
				if (A_0 > this.ᜄ)
				{
					num = 6;
					continue;
				}
				return;
			case 11:
				this.ᜁ = ((this.ᜄ > 0) ? Marshal.ReAllocHGlobal(this.ᜁ, (IntPtr)A_0) : Marshal.AllocHGlobal(A_0));
				num = 1;
				continue;
			case 12:
				if (this.ᜀ == IntPtr.Zero)
				{
					num = 8;
					continue;
				}
				A_0 += A_1;
				num = 5;
				continue;
			}
			if (this.ᜃ)
			{
				num = 2;
				continue;
			}
			return;
			IL_D2:
			this.ᜂ = this.ᜁ.ToInt64();
			num = 4;
		}
		IL_106:
		throw new OutOfMemoryException();
		IL_1A7:
		if (true)
		{
		}
	}

	// Token: 0x060041CC RID: 16844 RVA: 0x0024F7B4 File Offset: 0x0024E7B4
	public virtual void ᜆ()
	{
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
					num = 8;
					continue;
				case 1:
					return;
				case 3:
					Heap.HeapFree(this.ᜀ, 0, this.ᜁ);
					num = 4;
					continue;
				case 4:
					goto IL_CD;
				case 5:
					num = 7;
					continue;
				case 6:
					goto IL_CD;
				case 7:
					if (this.ᜀ != IntPtr.Zero)
					{
						num = 3;
						continue;
					}
					Marshal.FreeHGlobal(this.ᜁ);
					num = 6;
					continue;
				case 8:
					if (this.ᜄ > 0)
					{
						num = 5;
						continue;
					}
					return;
				}
				if (true)
				{
				}
				if (this.ᜃ)
				{
					num = 0;
					continue;
				}
				return;
				IL_CD:
				this.ᜁ = IntPtr.Zero;
				this.ᜂ = 0L;
				this.ᜄ = 0;
				break;
			}
			num = 1;
		}
	}

	// Token: 0x060041CD RID: 16845 RVA: 0x0024F8E8 File Offset: 0x0024E8E8
	public virtual void ᜀ(int A_0, DataProvider A_1, int A_2, int A_3)
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
		long value = this.ᜂ + (long)A_0;
		IntPtr ptrSource = (IntPtr)value;
		sprᰟ sprᰟ = (sprᰟ)A_1;
		long value2 = sprᰟ.ᜂ + (long)A_2;
		IntPtr ptrDest = (IntPtr)value2;
		Memory.CopyMemory(ptrDest, ptrSource, A_3);
	}

	// Token: 0x060041CE RID: 16846 RVA: 0x0024F958 File Offset: 0x0024E958
	public virtual void ᜈ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_48;
			case 2:
				Memory.RtlZeroMemory(this.ᜁ, this.ᜄ);
				num = 0;
				continue;
			}
			if (this.ᜄ <= 0)
			{
				break;
			}
			num = 2;
		}
		IL_48:
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
			return;
		}
	}

	// Token: 0x060041CF RID: 16847 RVA: 0x0024F9E0 File Offset: 0x0024E9E0
	public virtual void ᜁ(int A_0, int A_1, int A_2)
	{
		int num = 2;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				Debugger.Break();
				num = 1;
				continue;
			case 1:
				goto IL_3F;
			}
			if (A_2 >= 0)
			{
				break;
			}
			num = 0;
		}
		IL_3F:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_3F;
		default:
		{
			if (false)
			{
			}
			IntPtr ptrDest = (IntPtr)(this.ᜂ + (long)A_0);
			IntPtr ptrSource = (IntPtr)(this.ᜂ + (long)A_1);
			Memory.RtlMoveMemory(ptrDest, ptrSource, A_2);
			return;
		}
		}
	}

	// Token: 0x060041D0 RID: 16848 RVA: 0x0024FA7C File Offset: 0x0024EA7C
	public virtual void ᜀ(int A_0, int A_1, int A_2)
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
		IntPtr ptrDest = (IntPtr)(this.ᜂ + (long)A_0);
		IntPtr ptrSource = (IntPtr)(this.ᜂ + (long)A_1);
		Memory.CopyMemory(ptrDest, ptrSource, A_2);
	}

	// Token: 0x060041D1 RID: 16849 RVA: 0x0024FAE0 File Offset: 0x0024EAE0
	public virtual DataProvider ᜀ()
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
		return new sprᰟ(this.ᜁ());
	}

	// Token: 0x04001D27 RID: 7463
	protected IntPtr ᜀ;

	// Token: 0x04001D28 RID: 7464
	protected IntPtr ᜁ;

	// Token: 0x04001D29 RID: 7465
	protected long ᜂ;

	// Token: 0x04001D2A RID: 7466
	private bool ᜃ;

	// Token: 0x04001D2B RID: 7467
	private int ᜄ;
}
