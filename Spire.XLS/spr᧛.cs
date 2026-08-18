using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000506 RID: 1286
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.PageItem)]
internal class spr\u19DB : spr\u251F
{
	// Token: 0x06004E64 RID: 20068 RVA: 0x002FA714 File Offset: 0x002F9714
	public spr\u19DB()
	{
	}

	// Token: 0x06004E65 RID: 20069 RVA: 0x002FA734 File Offset: 0x002F9734
	public spr\u19DB(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004E66 RID: 20070 RVA: 0x002FA754 File Offset: 0x002F9754
	public spr\u19DB(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004E67 RID: 20071 RVA: 0x002FA774 File Offset: 0x002F9774
	public List<spr\u19DB.ᜀ> ᜁ()
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

	// Token: 0x06004E68 RID: 20072 RVA: 0x002FA7B8 File Offset: 0x002F97B8
	public override void ᜂ()
	{
		for (;;)
		{
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_A1;
				case 1:
				{
					if (num >= this.m_iLength)
					{
						num2 = 2;
						continue;
					}
					spr\u19DB.ᜀ ᜀ = new spr\u19DB.ᜀ();
					ᜀ.ᜀ = base.ᜌ(num);
					num += 2;
					ᜀ.ᜁ = base.ᜌ(num);
					num += 2;
					ᜀ.ᜂ = base.ᜌ(num);
					num += 2;
					this.ᜀ.Add(ᜀ);
					num2 = 0;
					continue;
				}
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A1;
					default:
						goto IL_B9;
					}
					break;
				case 3:
					if (true)
					{
					}
					goto IL_2C;
				}
				break;
				IL_2C:
				num2 = 1;
				continue;
				IL_A1:
				goto IL_2C;
			}
		}
		IL_B9:
		if (false)
		{
		}
	}

	// Token: 0x06004E69 RID: 20073 RVA: 0x002FA884 File Offset: 0x002F9884
	public override void ᜀ(ExcelVersion A_0)
	{
		for (;;)
		{
			int count = this.ᜀ.Count;
			this.m_iLength = 0;
			this.ᜀ = new byte[count * 6];
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_45;
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E4;
					default:
						goto IL_107;
					}
					break;
				case 2:
				{
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					spr\u19DB.ᜀ ᜀ = this.ᜀ[num];
					base.ᜀ(this.m_iLength, ᜀ.ᜀ);
					this.m_iLength += 2;
					base.ᜀ(this.m_iLength, ᜀ.ᜁ);
					this.m_iLength += 2;
					base.ᜀ(this.m_iLength, ᜀ.ᜂ);
					this.m_iLength += 2;
					num++;
					num2 = 3;
					continue;
				}
				case 3:
					goto IL_E4;
				}
				break;
				IL_45:
				num2 = 2;
				continue;
				IL_E4:
				goto IL_45;
			}
		}
		IL_107:
		if (false)
		{
		}
	}

	// Token: 0x06004E6A RID: 20074 RVA: 0x002FA9A0 File Offset: 0x002F99A0
	public virtual object ᜀ()
	{
		spr\u19DB spr_u19DB;
		for (;;)
		{
			spr_u19DB = (spr\u19DB)base.Clone();
			spr_u19DB.ᜀ = new List<spr\u19DB.ᜀ>();
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					spr_u19DB.ᜀ[num] = this.ᜀ[num].ᜀ();
					num++;
					if (true)
					{
					}
					num2 = 3;
					continue;
				case 1:
					return spr_u19DB;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return spr_u19DB;
					default:
						if (false)
						{
						}
						goto IL_63;
					}
					break;
				case 3:
					goto IL_63;
				}
				break;
				IL_63:
				num2 = 0;
			}
		}
		return spr_u19DB;
	}

	// Token: 0x04002373 RID: 9075
	private new List<spr\u19DB.ᜀ> ᜀ = new List<spr\u19DB.ᜀ>();

	// Token: 0x02000507 RID: 1287
	internal new class ᜀ
	{
		// Token: 0x06004E6B RID: 20075 RVA: 0x002FAA68 File Offset: 0x002F9A68
		internal spr\u19DB.ᜀ ᜀ()
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
			return (spr\u19DB.ᜀ)base.MemberwiseClone();
		}

		// Token: 0x04002374 RID: 9076
		[spr\u2429(0, 2)]
		public ushort ᜀ;

		// Token: 0x04002375 RID: 9077
		[spr\u2429(2, 2)]
		public ushort ᜁ;

		// Token: 0x04002376 RID: 9078
		[spr\u2429(4, 2)]
		public ushort ᜂ;
	}
}
