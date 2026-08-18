using System;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x02000254 RID: 596
internal class spr\u19ED : spr\u23AC
{
	// Token: 0x06001DEB RID: 7659 RVA: 0x001D9750 File Offset: 0x001D8750
	internal spr\u19ED(OdtPersist1 A_0, OdtPersist2 A_1, OdtClipboardFormat A_2)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
		this.ᜂ = A_2;
	}

	// Token: 0x06001DEC RID: 7660 RVA: 0x001D9778 File Offset: 0x001D8778
	internal spr\u19ED(spr\u1B02 A_0)
	{
		int a_ = 4;
		base..ctor();
		MemoryStream memoryStream = A_0.ᜃ(ClipboardData.b("楩⍫౭ᩯ㭱ᩳၵ᝷", a_));
		if (memoryStream != null)
		{
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			this.ᜀ = (OdtPersist1)binaryReader.ReadUInt16();
			this.ᜂ = (OdtClipboardFormat)binaryReader.ReadUInt16();
			if (spr\u1CC6.ᜀ(binaryReader, 2))
			{
				this.ᜁ = (OdtPersist2)(binaryReader.ReadUInt16() & 15);
			}
		}
	}

	// Token: 0x06001DED RID: 7661 RVA: 0x001D97EC File Offset: 0x001D87EC
	void spr\u23AC.ᜀ(BinaryWriter A_0)
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
		A_0.Write((ushort)this.ᜀ);
		A_0.Write((ushort)this.ᜂ);
		A_0.Write((ushort)this.ᜁ);
	}

	// Token: 0x06001DEE RID: 7662 RVA: 0x001D9850 File Offset: 0x001D8850
	string spr\u23AC.ᜃ()
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
		return ClipboardData.b("楩⍫౭ᩯ㭱ᩳၵ᝷", a_);
	}

	// Token: 0x06001DEF RID: 7663 RVA: 0x001D98A4 File Offset: 0x001D88A4
	internal OdtPersist1 ᜀ()
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
		return this.ᜀ;
	}

	// Token: 0x06001DF0 RID: 7664 RVA: 0x001D98E8 File Offset: 0x001D88E8
	internal OdtPersist2 ᜂ()
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

	// Token: 0x06001DF1 RID: 7665 RVA: 0x001D992C File Offset: 0x001D892C
	internal OdtClipboardFormat ᜁ()
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
		return this.ᜂ;
	}

	// Token: 0x06001DF2 RID: 7666 RVA: 0x001D9970 File Offset: 0x001D8970
	internal bool ᜄ()
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
		return (this.ᜀ() & OdtPersist1.Icon) != OdtPersist1.None;
	}

	// Token: 0x04001F88 RID: 8072
	private readonly OdtPersist1 ᜀ;

	// Token: 0x04001F89 RID: 8073
	private readonly OdtPersist2 ᜁ;

	// Token: 0x04001F8A RID: 8074
	private readonly OdtClipboardFormat ᜂ;
}
