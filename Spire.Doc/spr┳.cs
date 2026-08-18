using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using Spire.CompoundFile.Doc;

// Token: 0x02000230 RID: 560
internal class spr\u2533 : spr\u2509
{
	// Token: 0x06001AC6 RID: 6854 RVA: 0x001BFA68 File Offset: 0x001BEA68
	public new static XmlSchema ᜀ()
	{
		int a_ = 14;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		Stream stream = spr\u2533.ᜀ(ClipboardData.b("ͳ᥵੷ṹ⍻ൽ", a_));
		return XmlSchema.Read(stream, new ValidationEventHandler(spr\u2509.ᜀ));
	}

	// Token: 0x06001AC7 RID: 6855 RVA: 0x001BFAD4 File Offset: 0x001BEAD4
	public XmlSchema ᜁ()
	{
		int a_ = 18;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		Stream a_2 = spr\u2533.ᜀ(ClipboardData.b("ཷᕹ๻᩽\udf7f黎ﾑ뢕", a_));
		XmlDocument a_3 = spr\u2509.ᜀ(a_2);
		return base.ᜀ(a_3);
	}

	// Token: 0x06001AC8 RID: 6856 RVA: 0x001BFB3C File Offset: 0x001BEB3C
	protected new static Stream ᜀ(string A_0)
	{
		int a_ = 16;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		return executingAssembly.GetManifestResourceStream(ClipboardData.b("╵ࡷ፹๻᭽깿욁ꚇ\ud889ﶍﾏﶗ늛", a_) + A_0);
	}

	// Token: 0x06001AC9 RID: 6857 RVA: 0x001BFBA0 File Offset: 0x001BEBA0
	protected override Stream ᜀ(string A_0, string A_1)
	{
		int a_ = 2;
		if (A_1 == ClipboardData.b("㭧ᩩիᱭᕯ山び᥵᭷", a_))
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_37;
				}
			}
			IL_37:
			if (false)
			{
			}
			if (true)
			{
			}
			return spr\u2533.ᜀ(A_0);
		}
		return base.ᜀ(A_0, A_1);
	}

	// Token: 0x04001E6A RID: 7786
	protected new const string ᜀ = "Spire.Doc.Resources";
}
