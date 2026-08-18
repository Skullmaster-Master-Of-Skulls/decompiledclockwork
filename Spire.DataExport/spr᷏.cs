using System;
using System.Runtime.InteropServices;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;

// Token: 0x0200009E RID: 158
internal struct spr\u1DCF
{
	// Token: 0x060004C5 RID: 1221
	[DllImport("kernel32")]
	private static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

	// Token: 0x060004C6 RID: 1222 RVA: 0x0002F020 File Offset: 0x0002E020
	public unsafe static byte[] ᜀ(spr\u1DCF A_0)
	{
		switch (0)
		{
		default:
		{
			byte[] array;
			byte* ptr;
			for (;;)
			{
				array = new byte[spr\u1DCF.ᜀ()];
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_84;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6E;
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
						byte[] array2;
						if (array2.Length == 0)
						{
							num = 4;
							continue;
						}
						fixed (byte* ptr = &array2[0])
						{
							num = 3;
							continue;
							break;
						}
					}
					case 3:
						goto IL_6C;
					case 4:
						goto IL_6E;
					case 5:
					{
						if (true)
						{
						}
						byte[] array2;
						if ((array2 = array) != null)
						{
							num = 1;
							continue;
						}
						goto IL_6E;
					}
					}
					break;
					IL_6E:
					ptr = null;
					num = 0;
				}
			}
			IL_6C:
			IL_84:
			void* value = (void*)(&A_0);
			spr\u1DCF.CopyMemory((IntPtr)((void*)ptr), (IntPtr)value, spr\u1DCF.ᜀ());
			ptr = null;
			return array;
		}
		}
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0002F110 File Offset: 0x0002E110
	public unsafe static void ᜀ(byte[] A_0, ref spr\u1DCF A_1)
	{
		int a_ = 18;
		int num = 2;
		byte* ptr;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 != null)
				{
					num = 5;
					continue;
				}
				goto IL_5A;
			case 1:
				goto IL_D6;
			case 3:
				goto IL_6D;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_72;
				default:
					if (false)
					{
					}
					goto IL_5A;
				}
				break;
			case 5:
				goto IL_72;
			case 6:
				if (A_0.Length == 0)
				{
					num = 4;
					continue;
				}
				fixed (byte* ptr = &A_0[0])
				{
					num = 1;
					continue;
					break;
				}
			case 7:
				goto IL_58;
			}
			if (A_0.Length != spr\u1DCF.ᜀ())
			{
				num = 7;
				continue;
			}
			num = 0;
			continue;
			IL_5A:
			ptr = null;
			if (true)
			{
			}
			num = 3;
			continue;
			IL_72:
			num = 6;
		}
		IL_58:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("漭䈯唱䜳椵稷伹娻堽┿ぁࡃ⍅♇ⵉ㡋♍", a_)));
		IL_6D:
		IL_D6:
		fixed (IntPtr* ptr2 = (IntPtr*)(&A_1))
		{
			spr\u1DCF.CopyMemory((IntPtr)((void*)ptr2), (IntPtr)((void*)ptr), spr\u1DCF.ᜀ());
		}
		ptr = null;
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x0002F238 File Offset: 0x0002E238
	public static int ᜀ()
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
		return sizeof(spr\u1DCF);
	}

	// Token: 0x040002DC RID: 732
	public ushort ᜀ;

	// Token: 0x040002DD RID: 733
	public ushort ᜁ;
}
