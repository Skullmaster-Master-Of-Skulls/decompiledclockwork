using System;
using System.IO;
using System.Text;
using System.Xml;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

// Token: 0x0200029E RID: 670
internal class spr\u2256
{
	// Token: 0x06002762 RID: 10082 RVA: 0x001680A4 File Offset: 0x001670A4
	internal EncryptedKeyInfo ᜄ()
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

	// Token: 0x06002763 RID: 10083 RVA: 0x001680E8 File Offset: 0x001670E8
	internal void ᜀ(EncryptedKeyInfo A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06002764 RID: 10084 RVA: 0x0016812C File Offset: 0x0016712C
	internal spr\u1928 ᜂ()
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
		return this.ᜅ;
	}

	// Token: 0x06002765 RID: 10085 RVA: 0x00168170 File Offset: 0x00167170
	internal void ᜀ(spr\u1928 A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x06002766 RID: 10086 RVA: 0x001681B4 File Offset: 0x001671B4
	internal spr\u20F8 ᜅ()
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
		return this.ᜆ;
	}

	// Token: 0x06002767 RID: 10087 RVA: 0x001681F8 File Offset: 0x001671F8
	internal void ᜀ(spr\u20F8 A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x06002768 RID: 10088 RVA: 0x0016823C File Offset: 0x0016723C
	public int ᜃ()
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

	// Token: 0x06002769 RID: 10089 RVA: 0x00168280 File Offset: 0x00167280
	public void ᜁ(int A_0)
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

	// Token: 0x0600276A RID: 10090 RVA: 0x001682C4 File Offset: 0x001672C4
	public int ᜆ()
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

	// Token: 0x0600276B RID: 10091 RVA: 0x00168308 File Offset: 0x00167308
	public void ᜀ(int A_0)
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

	// Token: 0x0600276C RID: 10092 RVA: 0x0016834C File Offset: 0x0016734C
	public spr\u21E7 ᜀ()
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
		return this.ᜂ;
	}

	// Token: 0x0600276D RID: 10093 RVA: 0x00168390 File Offset: 0x00167390
	public spr\u241D ᜁ()
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

	// Token: 0x0600276E RID: 10094 RVA: 0x001683D4 File Offset: 0x001673D4
	public spr\u2256()
	{
		this.ᜄ = new EncryptedKeyInfo();
		this.ᜅ = new spr\u1928();
		this.ᜆ = new spr\u20F8();
	}

	// Token: 0x0600276F RID: 10095 RVA: 0x00168420 File Offset: 0x00167420
	public spr\u2256(Stream A_0)
	{
		byte[] a_ = new byte[4];
		this.ᜀ = sprṯ.ᜀ(A_0, a_);
		this.ᜁ = sprṯ.ᜀ(A_0, a_);
		if (this.ᜀ == 262148)
		{
			XmlReader xmlReader = UtilityMethods.ᜀ(A_0);
			xmlReader.Read();
			this.ᜆ = new spr\u20F8();
			this.ᜆ.ᜀ(xmlReader);
			xmlReader.Read();
			this.ᜅ = new spr\u1928();
			this.ᜅ.ᜀ(xmlReader);
			xmlReader.Read();
			xmlReader.Read();
			xmlReader.Read();
			this.ᜄ = new EncryptedKeyInfo();
			this.ᜄ.ᜀ(xmlReader);
			return;
		}
		this.ᜂ.ᜁ(A_0);
		this.ᜃ.ᜁ(A_0);
	}

	// Token: 0x06002770 RID: 10096 RVA: 0x00168504 File Offset: 0x00167504
	public void ᜀ(Stream A_0)
	{
		int a_ = 19;
		MemoryStream memoryStream;
		XmlWriter xmlWriter;
		for (;;)
		{
			sprṯ.ᜀ(A_0, this.ᜀ);
			sprṯ.ᜀ(A_0, this.ᜁ);
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_E6;
				case 1:
					memoryStream = new MemoryStream();
					xmlWriter = UtilityMethods.ᜀ(memoryStream, Encoding.UTF8);
					xmlWriter.WriteStartDocument();
					xmlWriter.WriteStartElement(RecordTableEnumerator.b("ⱈ╊⹌㵎⡐⍒⅔㹖㙘㕚", a_), RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤੦hࡪὬnɰᱲ፴Ͷ坸᡺ቼቾ꺀ꂎꎐꎒꖔꆖ뚘ﺚﲞ펠\udaa2햤펦삨쒪쎬", a_));
					xmlWriter.WriteAttributeString(RecordTableEnumerator.b("ㅈ♊⅌ⅎ≐", a_), RecordTableEnumerator.b("㥈", a_), null, RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤੦hࡪὬnɰᱲ፴Ͷ坸᡺ቼቾ꺀ꂎꎐꎒꖔꆖ뚘춢욤햦킨\udbaa\ud9ac삮쎰鲲어횶쪸좺쪼킾돀Ꟃ", a_));
					this.ᜆ.ᜀ(xmlWriter);
					num = 2;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5D;
					default:
						if (false)
						{
						}
						if (this.ᜅ != null)
						{
							num = 3;
							continue;
						}
						goto IL_6A;
					}
					break;
				case 3:
					this.ᜅ.ᜀ(xmlWriter);
					num = 0;
					continue;
				case 4:
					if (true)
					{
					}
					if (this.ᜀ == 262148)
					{
						goto IL_5D;
					}
					goto IL_1AB;
				}
				break;
				IL_5D:
				num = 1;
			}
		}
		IL_6A:
		xmlWriter.WriteStartElement(RecordTableEnumerator.b("≈⹊㑌੎㽐げ❔⹖⥘⽚㉜ⵞበ", a_));
		this.ᜄ.ᜀ(xmlWriter);
		xmlWriter.WriteEndElement();
		xmlWriter.WriteEndDocument();
		xmlWriter.Flush();
		memoryStream.Position = 0L;
		byte[] array = new byte[memoryStream.Length];
		memoryStream.Read(array, 0, array.Length);
		memoryStream.Close();
		A_0.Write(array, 0, array.Length);
		return;
		IL_E6:
		goto IL_6A;
		IL_1AB:
		this.ᜂ.ᜀ(A_0);
		this.ᜃ.ᜀ(A_0);
	}

	// Token: 0x0400136E RID: 4974
	private int ᜀ;

	// Token: 0x0400136F RID: 4975
	private int ᜁ;

	// Token: 0x04001370 RID: 4976
	private spr\u21E7 ᜂ = new spr\u21E7();

	// Token: 0x04001371 RID: 4977
	private spr\u241D ᜃ = new spr\u241D();

	// Token: 0x04001372 RID: 4978
	private EncryptedKeyInfo ᜄ;

	// Token: 0x04001373 RID: 4979
	private spr\u1928 ᜅ;

	// Token: 0x04001374 RID: 4980
	private spr\u20F8 ᜆ;
}
