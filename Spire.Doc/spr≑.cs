using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape.Ps.Wrapping;

// Token: 0x02000406 RID: 1030
internal class spr\u2251
{
	// Token: 0x0600394A RID: 14666 RVA: 0x00354AE8 File Offset: 0x00353AE8
	internal spr\u2251(PointF A_0) : this(A_0, VertexType.Simple)
	{
	}

	// Token: 0x0600394B RID: 14667 RVA: 0x00354B00 File Offset: 0x00353B00
	internal spr\u2251(PointF A_0, VertexType A_1)
	{
		this.ᜀ(A_0);
		this.ᜀ(A_1);
	}

	// Token: 0x0600394C RID: 14668 RVA: 0x00354B24 File Offset: 0x00353B24
	internal VertexType ᜃ()
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

	// Token: 0x0600394D RID: 14669 RVA: 0x00354B68 File Offset: 0x00353B68
	internal void ᜀ(VertexType A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x0600394E RID: 14670 RVA: 0x00354BAC File Offset: 0x00353BAC
	internal PointF ᜁ()
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

	// Token: 0x0600394F RID: 14671 RVA: 0x00354BF0 File Offset: 0x00353BF0
	internal void ᜀ(PointF A_0)
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
	}

	// Token: 0x06003950 RID: 14672 RVA: 0x00354C34 File Offset: 0x00353C34
	public virtual bool ᜀ(object A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.GetType() != typeof(spr\u2251))
				{
					num = 5;
					continue;
				}
				goto IL_A4;
			case 1:
				if (true)
				{
				}
				if (object.ReferenceEquals(this, A_0))
				{
					num = 2;
					continue;
				}
				num = 0;
				continue;
			case 2:
				goto IL_86;
			case 4:
				return false;
			case 5:
				return false;
			}
			if (object.ReferenceEquals(null, A_0))
			{
				num = 4;
			}
			else
			{
				num = 1;
			}
		}
		return false;
		IL_86:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			return false;
		default:
			if (false)
			{
			}
			return true;
		}
		IL_A4:
		return this.ᜀ((spr\u2251)A_0);
	}

	// Token: 0x06003951 RID: 14673 RVA: 0x00354CFC File Offset: 0x00353CFC
	public bool ᜀ(spr\u2251 A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_B2;
			case 2:
				if (object.ReferenceEquals(this, A_0))
				{
					num = 1;
					continue;
				}
				if (true)
				{
				}
				num = 5;
				continue;
			case 3:
				return false;
			case 4:
				goto IL_70;
			case 5:
				if (object.Equals(A_0.ᜀ, this.ᜀ))
				{
					num = 4;
					continue;
				}
				return false;
			}
			if (object.ReferenceEquals(null, A_0))
			{
				num = 3;
			}
			else
			{
				num = 2;
			}
		}
		return false;
		IL_70:
		return A_0.ᜁ.Equals(this.ᜁ);
		IL_B2:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_70;
		default:
			if (false)
			{
			}
			return true;
		}
		return false;
	}

	// Token: 0x06003952 RID: 14674 RVA: 0x00354DE4 File Offset: 0x00353DE4
	public virtual int ᜀ()
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
		return (int)(this.ᜀ * (VertexType)397 ^ (VertexType)this.ᜁ.GetHashCode());
	}

	// Token: 0x06003953 RID: 14675 RVA: 0x00354E40 File Offset: 0x00353E40
	public virtual string ᜂ()
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜀ + ClipboardData.b("佴", a_) + this.ᜁ;
	}

	// Token: 0x04002AB7 RID: 10935
	private VertexType ᜀ;

	// Token: 0x04002AB8 RID: 10936
	private PointF ᜁ;
}
