using System;
using System.Runtime.InteropServices;
using Spire.DataExport.CollectionEditors;

// Token: 0x020000F6 RID: 246
internal class spr\u222A : sprᡙ
{
	// Token: 0x06000549 RID: 1353 RVA: 0x00033BA0 File Offset: 0x00032BA0
	public spr\u222A(sprᲤ A_0, ushort A_1, ushort A_2, byte[] A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00033BB8 File Offset: 0x00032BB8
	public override bool ᜁ()
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
		return 4 + (this.ᜀ + 1) * sizeof(spr\u222A.ᜀ) >= this.ᜌ();
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00033C10 File Offset: 0x00032C10
	public unsafe override spr\u1DEE ᜀ()
	{
		int a_ = 5;
		int num = 10;
		byte[] array = new byte[num];
		spr\u1DEE result;
		try
		{
			sprᮌ.ᜀ(array, 0, sprᮌ.ᜁ(base.ᜢ(), 0));
			sprᮌ.ᜀ(array, 2, (ushort)((int)sprᮌ.ᜁ(base.ᜢ(), 2) + this.ᜀ));
			try
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
					fixed (byte* ptr = &base.ᜢ()[4 + this.ᜀ * sizeof(spr\u222A.ᜀ)])
					{
						spr\u222A.ᜀ* ptr2 = (spr\u222A.ᜀ*)ptr;
						try
						{
							fixed (byte* ptr3 = &array[4])
							{
								spr\u222A.ᜀ* ptr4 = (spr\u222A.ᜀ*)ptr3;
								*ptr4 = *ptr2;
							}
						}
						finally
						{
							byte* ptr3 = null;
						}
						break;
					}
				}
			}
			finally
			{
				byte* ptr = null;
			}
			if (true)
			{
			}
			this.ᜀ++;
			result = new spr\u1DC2(base.ᜦ(), 638, (ushort)num, array);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message + HyperlinksCollectionEditor.b("Ⱐ⤢唤唦夨ᄪ測䨮崰弲ᤴ᜶娸场丼Ծ̀⩂⍄ⅆш㹊⅌ᵎᩐ", a_));
		}
		return result;
	}

	// Token: 0x020000F7 RID: 247
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	private new struct ᜀ
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x00033D4C File Offset: 0x00032D4C
		public ᜀ(ushort A_0, int A_1)
		{
			this.ᜀ = A_0;
			this.ᜁ = A_1;
		}

		// Token: 0x04000573 RID: 1395
		public ushort ᜀ;

		// Token: 0x04000574 RID: 1396
		public int ᜁ;
	}
}
