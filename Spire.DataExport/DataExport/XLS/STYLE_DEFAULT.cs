using System;
using System.Runtime.InteropServices;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001CB RID: 459
	public abstract class STYLE_DEFAULT
	{
		// Token: 0x06000D7B RID: 3451 RVA: 0x0009504C File Offset: 0x0009404C
		static STYLE_DEFAULT()
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
			STYLE_DEFAULT.ᜀ = new STYLE_DEFAULT.BiffStyleInternal[6];
			STYLE_DEFAULT.ᜀ[0] = new STYLE_DEFAULT.BiffStyleInternal(32784, 3, byte.MaxValue);
			STYLE_DEFAULT.ᜀ[1] = new STYLE_DEFAULT.BiffStyleInternal(32785, 6, byte.MaxValue);
			STYLE_DEFAULT.ᜀ[2] = new STYLE_DEFAULT.BiffStyleInternal(32786, 4, byte.MaxValue);
			STYLE_DEFAULT.ᜀ[3] = new STYLE_DEFAULT.BiffStyleInternal(32787, 7, byte.MaxValue);
			STYLE_DEFAULT.ᜀ[4] = new STYLE_DEFAULT.BiffStyleInternal(32768, 0, byte.MaxValue);
			STYLE_DEFAULT.ᜀ[5] = new STYLE_DEFAULT.BiffStyleInternal(32788, 5, byte.MaxValue);
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0009511C File Offset: 0x0009411C
		public static STYLE_DEFAULT.BiffStyleInternal[] BiffStyleArray
		{
			get
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
				return STYLE_DEFAULT.ᜀ;
			}
		}

		// Token: 0x04000A66 RID: 2662
		private static readonly STYLE_DEFAULT.BiffStyleInternal[] ᜀ;

		// Token: 0x020001CC RID: 460
		public class BiffStyleInternal
		{
			// Token: 0x06000D7E RID: 3454 RVA: 0x00095170 File Offset: 0x00094170
			public BiffStyleInternal(ushort Index, byte BuiltIn, byte Level)
			{
				this.ᜀ.ᜀ = Index;
				this.ᜀ.ᜁ = BuiltIn;
				this.ᜀ.ᜂ = Level;
			}

			// Token: 0x06000D7F RID: 3455
			[DllImport("kernel32")]
			private static extern void CopyMemory(IntPtr A_0, IntPtr A_1, int A_2);

			// Token: 0x06000D80 RID: 3456 RVA: 0x000951A8 File Offset: 0x000941A8
			public unsafe byte[] GetBytes()
			{
				switch (0)
				{
				default:
				{
					byte[] array;
					byte* ptr;
					for (;;)
					{
						array = new byte[sizeof(sprᣭ)];
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 3;
								continue;
							case 1:
							{
								if (true)
								{
								}
								byte[] array2;
								if ((array2 = array) != null)
								{
									num = 0;
									continue;
								}
								goto IL_6F;
							}
							case 2:
								goto IL_6D;
							case 3:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_6F;
								default:
								{
									if (false)
									{
									}
									byte[] array2;
									if (array2.Length == 0)
									{
										num = 4;
										continue;
									}
									fixed (byte* ptr = &array2[0])
									{
										num = 2;
										continue;
										break;
									}
								}
								}
								break;
							case 4:
								goto IL_6F;
							case 5:
								goto IL_7B;
							}
							break;
							IL_6F:
							ptr = null;
							num = 5;
						}
					}
					IL_6D:
					IL_7B:
					fixed (IntPtr* ptr2 = (IntPtr*)(&this.ᜀ))
					{
						STYLE_DEFAULT.BiffStyleInternal.CopyMemory((IntPtr)((void*)ptr), (IntPtr)((void*)ptr2), sizeof(sprᣭ));
					}
					ptr = null;
					return array;
				}
				}
			}

			// Token: 0x04000A67 RID: 2663
			private int \u2593\u0083\u00A6\u0081;

			// Token: 0x04000A68 RID: 2664
			private long[] \u2593\u009D\u0094\u00A5;

			// Token: 0x04000A69 RID: 2665
			private string \u2460\u00A3\u0099\u0099;

			// Token: 0x04000A6A RID: 2666
			private int \u25D8\u0098\u008B\u008C;

			// Token: 0x04000A6B RID: 2667
			private sprᣭ ᜀ;
		}
	}
}
