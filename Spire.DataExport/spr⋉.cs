using System;
using System.Runtime.InteropServices;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;

// Token: 0x020000AA RID: 170
internal struct spr\u22C9
{
	// Token: 0x060004C9 RID: 1225
	[DllImport("kernel32")]
	private static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

	// Token: 0x060004CA RID: 1226 RVA: 0x0002F27C File Offset: 0x0002E27C
	public unsafe static byte[] ᜀ(spr\u22C9 A_0)
	{
		switch (0)
		{
		default:
		{
			byte[] array;
			byte* ptr;
			for (;;)
			{
				for (;;)
				{
					array = new byte[spr\u22C9.ᜀ()];
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							byte[] array2;
							if (array2.Length == 0)
							{
								num = 3;
								continue;
							}
							fixed (byte* ptr = &array2[0])
							{
								num = 4;
								continue;
								break;
							}
						}
						case 1:
						{
							if (true)
							{
							}
							byte[] array2;
							if ((array2 = array) != null)
							{
								num = 5;
								continue;
							}
							goto IL_6E;
						}
						case 2:
							goto IL_7A;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								goto IL_6E;
							}
							break;
						case 4:
							goto IL_6C;
						case 5:
							num = 0;
							continue;
						}
						break;
						IL_6E:
						ptr = null;
						num = 2;
					}
				}
			}
			IL_6C:
			IL_7A:
			void* value = (void*)(&A_0);
			spr\u22C9.CopyMemory((IntPtr)((void*)ptr), (IntPtr)value, spr\u22C9.ᜀ());
			ptr = null;
			return array;
		}
		}
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x0002F368 File Offset: 0x0002E368
	public unsafe static void ᜀ(byte[] A_0, ref spr\u22C9 A_1)
	{
		int a_ = 4;
		int num = 5;
		byte* ptr;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_63;
			case 1:
				if (A_0 != null)
				{
					num = 6;
					continue;
				}
				goto IL_50;
			case 2:
				if (A_0.Length == 0)
				{
					num = 4;
					continue;
				}
				fixed (byte* ptr = &A_0[0])
				{
					num = 7;
					continue;
					break;
				}
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				goto Block_3;
			case 4:
				goto IL_50;
			case 6:
				num = 2;
				continue;
			case 7:
				goto IL_D6;
			}
			if (A_0.Length != spr\u22C9.ᜀ())
			{
				num = 3;
				continue;
			}
			num = 1;
			continue;
			IL_50:
			ptr = null;
			if (true)
			{
			}
			num = 0;
		}
		IL_63:
		goto IL_FB;
		Block_3:
		if (false)
		{
		}
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("感倡䌣唥眧栩夫䠭嘯圱䘳稵崷吹嬻䨽⠿", a_)));
		IL_D6:
		IL_FB:
		fixed (IntPtr* ptr2 = (IntPtr*)(&A_1))
		{
			spr\u22C9.CopyMemory((IntPtr)((void*)ptr2), (IntPtr)((void*)ptr), spr\u22C9.ᜀ());
		}
		ptr = null;
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x0002F490 File Offset: 0x0002E490
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
		return sizeof(spr\u22C9);
	}

	// Token: 0x04000323 RID: 803
	public ushort ᜀ;

	// Token: 0x04000324 RID: 804
	public ushort ᜁ;

	// Token: 0x04000325 RID: 805
	public ushort ᜂ;
}
