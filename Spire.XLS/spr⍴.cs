using System;
using System.Drawing;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200034E RID: 846
internal abstract class spr\u2374 : ISortedRule
{
	// Token: 0x0600336C RID: 13164 RVA: 0x001DBA88 File Offset: 0x001DAA88
	public object[][] ᜁ()
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

	// Token: 0x0600336D RID: 13165 RVA: 0x001DBACC File Offset: 0x001DAACC
	public IXLSRange ᜀ()
	{
		int a_ = 4;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("根崻倽✿❁", a_));
	}

	// Token: 0x0600336E RID: 13166 RVA: 0x001DBB24 File Offset: 0x001DAB24
	public void ᜀ(IXLSRange A_0)
	{
		int a_ = 7;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("漼帾⽀⑂⁄", a_));
	}

	// Token: 0x0600336F RID: 13167 RVA: 0x001DBB7C File Offset: 0x001DAB7C
	public spr\u2374(object[][] A_0, Type[] A_1, OrderBy[] A_2, Color[] A_3)
	{
		this.ᜀ = A_0;
		this.ᜂ = A_1;
		this.ᜃ = A_2;
		this.ᜆ = A_3;
		this.ᜁ = A_1.Length;
		this.ᜄ = 0;
		this.ᜅ = A_0.Length - 1;
	}

	// Token: 0x06003370 RID: 13168
	public abstract void ᜄ(int A_0, int A_1, int A_2);

	// Token: 0x06003371 RID: 13169 RVA: 0x001DBBC8 File Offset: 0x001DABC8
	protected object[] ᜁ(int A_0)
	{
		object[] array;
		for (;;)
		{
			array = new object[this.ᜀ[0].Length];
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return array;
				case 1:
					if (num >= array.Length)
					{
						num2 = 0;
						continue;
					}
					array[num] = this.ᜀ[A_0][num];
					num++;
					num2 = 2;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_32;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_34;
					}
					break;
				case 3:
					goto IL_32;
				}
				break;
				IL_34:
				num2 = 1;
				continue;
				IL_32:
				goto IL_34;
			}
		}
		return array;
	}

	// Token: 0x06003372 RID: 13170 RVA: 0x001DBC6C File Offset: 0x001DAC6C
	protected object[] ᜀ(int A_0)
	{
		object[] array;
		for (;;)
		{
			array = new object[this.ᜀ.Length];
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_30;
				case 1:
					return array;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_30;
					default:
						if (false)
						{
						}
						goto IL_32;
					}
					break;
				case 3:
					if (num >= array.Length)
					{
						num2 = 1;
						continue;
					}
					array[num] = this.ᜀ[num][A_0];
					num++;
					if (true)
					{
					}
					num2 = 2;
					continue;
				}
				break;
				IL_32:
				num2 = 3;
				continue;
				IL_30:
				goto IL_32;
			}
		}
		return array;
	}

	// Token: 0x06003373 RID: 13171 RVA: 0x001DBD10 File Offset: 0x001DAD10
	protected void ᜃ(int A_0, int A_1)
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
		this.ᜁ(this.ᜀ[A_0], this.ᜀ[A_1]);
	}

	// Token: 0x06003374 RID: 13172 RVA: 0x001DBD64 File Offset: 0x001DAD64
	private void ᜁ(object[] A_0, object[] A_1)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					goto IL_22;
				case 2:
				{
					if (num >= A_0.Length)
					{
						num2 = 0;
						continue;
					}
					object obj = A_1[num];
					A_1[num] = A_0[num];
					A_0[num] = obj;
					num++;
					num2 = 3;
					continue;
				}
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_22;
					default:
						if (false)
						{
						}
						goto IL_2C;
					}
					break;
				}
				break;
				IL_2C:
				num2 = 2;
				continue;
				IL_22:
				if (true)
				{
				}
				goto IL_2C;
			}
		}
	}

	// Token: 0x06003375 RID: 13173 RVA: 0x001DBDF8 File Offset: 0x001DADF8
	protected void ᜂ(int A_0, int A_1)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_22;
					default:
						if (false)
						{
						}
						goto IL_24;
					}
					break;
				case 1:
					goto IL_22;
				case 2:
				{
					if (num >= this.ᜀ[0].Length)
					{
						num2 = 3;
						continue;
					}
					if (true)
					{
					}
					object obj = this.ᜀ[num][A_0];
					this.ᜀ[num][A_0] = this.ᜀ[num][A_1];
					this.ᜀ[num][A_1] = obj;
					num++;
					num2 = 0;
					continue;
				}
				case 3:
					return;
				}
				break;
				IL_24:
				num2 = 2;
				continue;
				IL_22:
				goto IL_24;
			}
		}
	}

	// Token: 0x06003376 RID: 13174 RVA: 0x001DBEB4 File Offset: 0x001DAEB4
	private void ᜀ(object[] A_0, object[] A_1)
	{
		for (;;)
		{
			int num = 0;
			if (true)
			{
			}
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2A;
					default:
						if (false)
						{
						}
						goto IL_2C;
					}
					break;
				case 1:
					goto IL_2A;
				case 2:
				{
					if (num >= A_0.Length)
					{
						num2 = 3;
						continue;
					}
					object obj = A_1[num];
					A_1[num] = A_0[num];
					A_0[num] = obj;
					num++;
					num2 = 0;
					continue;
				}
				case 3:
					return;
				}
				break;
				IL_2C:
				num2 = 2;
				continue;
				IL_2A:
				goto IL_2C;
			}
		}
	}

	// Token: 0x06003377 RID: 13175 RVA: 0x001DBF48 File Offset: 0x001DAF48
	public virtual void ᜉ(int A_0, int A_1, int A_2)
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
		throw new NotImplementedException();
	}

	// Token: 0x06003378 RID: 13176 RVA: 0x001DBF88 File Offset: 0x001DAF88
	public virtual void ᜂ(int A_0, int A_1, int A_2)
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
		throw new NotImplementedException();
	}

	// Token: 0x06003379 RID: 13177 RVA: 0x001DBFC8 File Offset: 0x001DAFC8
	public virtual void ᜅ(int A_0, int A_1, int A_2)
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
		throw new NotImplementedException();
	}

	// Token: 0x0600337A RID: 13178 RVA: 0x001DC008 File Offset: 0x001DB008
	public virtual void ᜃ(int A_0, int A_1, int A_2)
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
		throw new NotImplementedException();
	}

	// Token: 0x0600337B RID: 13179 RVA: 0x001DC048 File Offset: 0x001DB048
	public virtual void ᜁ(int A_0, int A_1, int A_2)
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
		throw new NotImplementedException();
	}

	// Token: 0x0600337C RID: 13180 RVA: 0x001DC088 File Offset: 0x001DB088
	public virtual void ᜀ(int A_0, int A_1, int A_2)
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
		throw new NotImplementedException();
	}

	// Token: 0x0600337D RID: 13181 RVA: 0x001DC0C8 File Offset: 0x001DB0C8
	public virtual void ᜆ(int A_0, int A_1, int A_2)
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
		throw new NotImplementedException();
	}

	// Token: 0x0600337E RID: 13182 RVA: 0x001DC108 File Offset: 0x001DB108
	public virtual void ᜇ(int A_0, int A_1, int A_2)
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
		throw new NotImplementedException();
	}

	// Token: 0x0600337F RID: 13183 RVA: 0x001DC148 File Offset: 0x001DB148
	public virtual void ᜈ(int A_0, int A_1, int A_2)
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
		throw new NotImplementedException();
	}

	// Token: 0x040016D7 RID: 5847
	protected object[][] ᜀ;

	// Token: 0x040016D8 RID: 5848
	protected int ᜁ;

	// Token: 0x040016D9 RID: 5849
	protected Type[] ᜂ;

	// Token: 0x040016DA RID: 5850
	protected OrderBy[] ᜃ;

	// Token: 0x040016DB RID: 5851
	protected int ᜄ;

	// Token: 0x040016DC RID: 5852
	protected int ᜅ;

	// Token: 0x040016DD RID: 5853
	protected Color[] ᜆ;
}
