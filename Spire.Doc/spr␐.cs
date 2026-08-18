using System;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x020001C5 RID: 453
internal class spr\u2410
{
	// Token: 0x0600132D RID: 4909 RVA: 0x0013A6B0 File Offset: 0x001396B0
	internal spr\u2410()
	{
	}

	// Token: 0x0600132E RID: 4910 RVA: 0x0013A6C4 File Offset: 0x001396C4
	internal spr\u2410(BinaryReader A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x0600132F RID: 4911 RVA: 0x0013A6E0 File Offset: 0x001396E0
	public virtual string ᜃ()
	{
		int a_ = 13;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return string.Format(ClipboardData.b("㙲ٴ㽶ᱸ᩺᥼᩾꾂ꖄ펆ﮊ떎ꎒ꾔쾖랚붜즞쒠톢횤캦욨얪鞬풮肰캲馴鞶햺캼쮾ꃀ귂ꛄꋆ냊ￌ닎﷐駔닖럘볚꧜럞\udbe0飢훤髦", a_), new object[]
		{
			this.ᜂ,
			this.ᜀ,
			this.ᜁ,
			this.ᜃ
		});
	}

	// Token: 0x06001330 RID: 4912 RVA: 0x0013A778 File Offset: 0x00139778
	internal void ᜀ(BinaryReader A_0)
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
		int num = A_0.ReadInt32();
		this.ᜀ = (num & 15);
		this.ᜁ = (num & 65520) >> 4;
		this.ᜂ = (EsRecordType)(((long)num & (long)((ulong)-65536)) >> 16);
		this.ᜃ = A_0.ReadInt32();
	}

	// Token: 0x06001331 RID: 4913 RVA: 0x0013A7F4 File Offset: 0x001397F4
	internal void ᜀ(BinaryWriter A_0)
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
		int num = 0;
		num |= this.ᜀ;
		num |= this.ᜁ << 4;
		num |= (int)((int)this.ᜂ << 16);
		A_0.Write(num);
		A_0.Write(this.ᜃ);
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x0013A864 File Offset: 0x00139864
	internal bool ᜀ()
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
		return this.ᜀ == 15;
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x0013A8AC File Offset: 0x001398AC
	internal void ᜀ(bool A_0)
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
		this.ᜀ = (A_0 ? 15 : 0);
	}

	// Token: 0x06001334 RID: 4916 RVA: 0x0013A8FC File Offset: 0x001398FC
	internal int ᜁ()
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

	// Token: 0x06001335 RID: 4917 RVA: 0x0013A940 File Offset: 0x00139940
	internal void ᜂ(int A_0)
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

	// Token: 0x06001336 RID: 4918 RVA: 0x0013A984 File Offset: 0x00139984
	internal int ᜂ()
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
		return this.ᜁ;
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x0013A9C8 File Offset: 0x001399C8
	internal void ᜁ(int A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x0013AA0C File Offset: 0x00139A0C
	internal EsRecordType ᜅ()
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
		return this.ᜂ;
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x0013AA50 File Offset: 0x00139A50
	internal void ᜀ(EsRecordType A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x0013AA94 File Offset: 0x00139A94
	internal int ᜄ()
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
		return this.ᜃ;
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x0013AAD8 File Offset: 0x00139AD8
	internal void ᜀ(int A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x040018B4 RID: 6324
	private int ᜀ;

	// Token: 0x040018B5 RID: 6325
	private int ᜁ;

	// Token: 0x040018B6 RID: 6326
	private EsRecordType ᜂ;

	// Token: 0x040018B7 RID: 6327
	private int ᜃ;
}
