using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Charts;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.PivotTables;
using Spire.Xls.Core.Spreadsheet.XmlSerialization;

// Token: 0x0200049A RID: 1178
[DefaultMember("Item")]
internal class sprវ : IWorkbookSerializator, IDisposable
{
	// Token: 0x06004882 RID: 18562 RVA: 0x002BE220 File Offset: 0x002BD220
	private sprវ()
	{
		int a_ = 7;
		this.\u1719 = new Dictionary<string, MemoryStream>();
		this.\u171A = new spr\u249E();
		this.\u171D = new Dictionary<string, string>();
		this.\u171E = new Dictionary<string, string>(System.StringComparer.InvariantCultureIgnoreCase);
		this.ᜠ = RecordTableEnumerator.b("䔼匾湀㑂⩄㕆≈⥊≌⁎㩐絒ⵔ㩖㕘", a_);
		this.ᜡ = RecordTableEnumerator.b("ሼ䜾ⵀ求㙄⽆⡈㥊⡌⭎ɐ❒❔㹖㝘㱚⹜煞ᥠ๢।", a_);
		this.ᜢ = RecordTableEnumerator.b("䔼匾湀あㅄ㹆╈⹊㹌慎⥐㹒㥔", a_);
		this.ᜣ = RecordTableEnumerator.b("䔼匾湀㝂ⵄ≆⑈⹊扌㭎㥐㙒㡔㉖桘畚╜㉞ൠ", a_);
		this.ᜪ = RecordTableEnumerator.b("尼伾ㅀ⽂ⱄ⑆⡈㽊⑌⁎㽐籒⍔㥖㵘畚㉜⽞Ѡൢᵤ੦ը൪ɬᵮᱰቲŴѶ呸ᑺ᭼᥾ﶒ릖爵삠잢횤쾦첨캪\ud9ac슮\uddb0鶲운\udfb6\udcb8\udeba즼醾곀ꋂ계꧆돊ꃌꏎ", a_);
		this.ᜫ = new MemoryStream();
		this.ᜬ = new MemoryStream();
		this.\u1738 = new Dictionary<string, object>();
		this.\u173A = new FileVersion();
		this.\u173B = RecordTableEnumerator.b("఼ാ瑀瑂睄牆", a_);
		this.\u173C = new Dictionary<string, string>();
		base..ctor();
	}

	// Token: 0x06004883 RID: 18563 RVA: 0x002BE31C File Offset: 0x002BD31C
	public sprវ(XlsWorkbook A_0)
	{
		int a_ = 10;
		this.\u1719 = new Dictionary<string, MemoryStream>();
		this.\u171A = new spr\u249E();
		this.\u171D = new Dictionary<string, string>();
		this.\u171E = new Dictionary<string, string>(System.StringComparer.InvariantCultureIgnoreCase);
		this.ᜠ = RecordTableEnumerator.b("㠿⹁歃ㅅ❇㡉❋ⱍ㽏㵑㽓硕⁗㝙せ", a_);
		this.ᜡ = RecordTableEnumerator.b("漿㩁⡃楅㭇≉ⵋ㱍㕏㙑ݓ≕⩗㍙㉛㥝፟䱡ᱣ୥ѧ", a_);
		this.ᜢ = RecordTableEnumerator.b("㠿⹁歃㕅㱇㍉⁋⭍⍏籑ⱓ㭕㑗", a_);
		this.ᜣ = RecordTableEnumerator.b("㠿⹁歃㉅⁇⽉⅋⭍罏♑㱓㍕㕗㽙浛灝ᡟཡࡣ", a_);
		this.ᜪ = RecordTableEnumerator.b("ℿ㉁㑃⩅ⅇ⥉ⵋ㩍㥏㵑㩓祕⹗㑙㡛灝ཟቡţࡥၧݩk࡭Ὧqᥳ᝵౷ॹ养ᅽﾑ뒙튟잡얣슥\udba7슩즫쮭쒯\udfb1\ud8b3颵쮷특\ud9bb\udbbd뒿꧃Ʂꇇ꓉뛍뷏뻑", a_);
		this.ᜫ = new MemoryStream();
		this.ᜬ = new MemoryStream();
		this.\u1738 = new Dictionary<string, object>();
		this.\u173A = new FileVersion();
		this.\u173B = RecordTableEnumerator.b("焿灁煃煅穇罉", a_);
		this.\u173C = new Dictionary<string, string>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("∿ⵁ⭃ⵅ", a_));
		}
		this.\u171B = A_0;
		this.\u173D = A_0.Version;
		this.\u171A.ᜆ = new spr\u249E.ᜀ(A_0.AppImplementation.ᜂ);
	}

	// Token: 0x06004884 RID: 18564 RVA: 0x002BE464 File Offset: 0x002BD464
	public sprវ(XlsWorkbook A_0, ExcelVersion A_1)
	{
		int a_ = 14;
		this.\u1719 = new Dictionary<string, MemoryStream>();
		this.\u171A = new spr\u249E();
		this.\u171D = new Dictionary<string, string>();
		this.\u171E = new Dictionary<string, string>(System.StringComparer.InvariantCultureIgnoreCase);
		this.ᜠ = RecordTableEnumerator.b("㱃⩅杇㵉⍋㱍㭏け㭓㥕㍗瑙⑛㍝౟", a_);
		this.ᜡ = RecordTableEnumerator.b("歃㹅⑇敉㽋♍ㅏ⁑ㅓ㉕ୗ⹙⹛㝝๟աᝣ䡥ၧݩk", a_);
		this.ᜢ = RecordTableEnumerator.b("㱃⩅杇㥉㡋㝍㱏㝑❓硕⁗㝙せ", a_);
		this.ᜣ = RecordTableEnumerator.b("㱃⩅杇㹉⑋⭍㵏㝑筓≕し㽙ㅛ㭝兟䱡ᱣ୥ѧ", a_);
		this.ᜪ = RecordTableEnumerator.b("╃㙅㡇♉╋ⵍㅏ♑㵓㥕㙗留⩛そџ䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ굿ﾏﮕﶗ낝펟튡횣쎥즧캩\udfab욭햯ힱ삳\udbb5풷钹쾻횽ꖿꟁ냃ꗇꯉꗋꃍ﯏꫑맓뫕", a_);
		this.ᜫ = new MemoryStream();
		this.ᜬ = new MemoryStream();
		this.\u1738 = new Dictionary<string, object>();
		this.\u173A = new FileVersion();
		this.\u173B = RecordTableEnumerator.b("畃瑅絇絉繋筍", a_);
		this.\u173C = new Dictionary<string, string>();
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("♃⥅❇ⅉ", a_));
		}
		this.\u171B = A_0;
		this.\u173D = A_1;
	}

	// Token: 0x06004885 RID: 18565 RVA: 0x002BE588 File Offset: 0x002BD588
	public sprវ(XlsWorkbook A_0, string A_1, string A_2)
	{
		int a_ = 13;
		this..ctor(A_0);
		if (A_1 != null)
		{
			if (A_1.Length != 0)
			{
				this.\u171A.ᜄ(A_1);
				return;
			}
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("╂ⱄ⭆ⱈ╊ⱌ≎㑐", a_));
	}

	// Token: 0x06004886 RID: 18566 RVA: 0x002BE5D4 File Offset: 0x002BD5D4
	public sprវ(XlsWorkbook A_0, Stream A_1, string A_2)
	{
		int a_ = 8;
		this..ctor(A_0);
		if (A_1 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("䴽㐿ぁ⅃❅╇", a_));
		}
		long position = A_1.Position;
		bool flag = false;
		if (spr\u2604.ᜁ(A_1))
		{
			for (;;)
			{
				flag = true;
				using (spr\u2496 spr_u = A_0.AppImplementation.ᜁ(A_1))
				{
					spr\u20C3 a_2 = spr_u.ᜀ();
					if (flag = spr\u2389.ᜃ(a_2))
					{
						spr\u2389 spr_u2 = (this.ᜀ(a_2) == ExcelVersion.Version2007) ? new spr\u2389() : new spr᮳();
						spr_u2.ᜄ(spr_u.ᜀ());
						spr\u17FF a_3 = A_0.AppImplementation;
						bool flag2 = false;
						if (A_2 == null)
						{
							flag2 = spr_u2.ᜀ(RecordTableEnumerator.b("栽┿⹁㉃⍅㱇᥉㭋⭍ㅏ♑❓㹕㝗⩙", a_));
						}
						if (!flag2)
						{
							this.ᜀ(ref A_2, a_3);
							while (!spr_u2.ᜀ(A_2))
							{
								this.ᜁ(ref A_2, a_3);
							}
						}
						A_1 = spr_u2.ᜀ();
						this.\u171B.\u1773 = EncryptionType.Standard;
					}
					break;
				}
			}
			if (!flag)
			{
				throw new ApplicationException(RecordTableEnumerator.b("椽㈿ⵁ⩃ⅅ桇⽉㑋ⵍ㕏㹑瑓⁕㵗⡙⽛㝝ཟౡ", a_));
			}
		}
		if (flag)
		{
			this.\u171B.PasswordToOpen = A_2;
		}
		this.\u171A.ᜁ(A_1, false);
	}

	// Token: 0x06004887 RID: 18567 RVA: 0x002BE740 File Offset: 0x002BD740
	private ExcelVersion ᜀ(spr\u20C3 A_0)
	{
		int a_ = 15;
		for (;;)
		{
			Stream stream = A_0.ᜁ(RecordTableEnumerator.b("D⥆⩈㥊㑌㽎═㩒㩔㥖ၘ㕚㭜ぞ", a_));
			byte[] a_2 = new byte[4];
			int num = sprṯ.ᜀ(stream, a_2);
			stream.Close();
			if (num != 262148)
			{
				return ExcelVersion.Version2007;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_51;
			}
		}
		IL_51:
		if (true)
		{
		}
		if (false)
		{
		}
		return ExcelVersion.Version2010;
	}

	// Token: 0x06004888 RID: 18568 RVA: 0x002BE7BC File Offset: 0x002BD7BC
	private void ᜁ(ref string A_0, spr\u17FF A_1)
	{
		int a_ = 6;
		for (;;)
		{
			PasswordRequiredEventArgs passwordRequiredEventArgs = new PasswordRequiredEventArgs();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1.ᜀ(this, passwordRequiredEventArgs))
					{
						num = 5;
						continue;
					}
					passwordRequiredEventArgs = null;
					num = 9;
					continue;
				case 1:
					goto IL_88;
				case 2:
					num = 8;
					continue;
				case 3:
					if (true)
					{
					}
					if (passwordRequiredEventArgs.StopParsing)
					{
						num = 6;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_88;
					default:
						goto IL_10F;
					}
					break;
				case 4:
					goto IL_80;
				case 5:
					A_0 = passwordRequiredEventArgs.NewPassword;
					num = 4;
					continue;
				case 6:
					goto IL_7E;
				case 7:
					num = 3;
					continue;
				case 8:
					if (passwordRequiredEventArgs != null)
					{
						num = 7;
						continue;
					}
					goto IL_AE;
				case 9:
					goto IL_80;
				}
				break;
				IL_80:
				num = 1;
				continue;
				IL_88:
				if (A_0 == null)
				{
					goto IL_AE;
				}
				num = 2;
			}
		}
		IL_7E:
		IL_AE:
		throw new ArgumentException(RecordTableEnumerator.b("欻儽㈿⥁♃⥅❇ⅉ汋❍⍏牑⑓⑕㝗⹙㥛㵝ᑟݡc䙥१ѩ࡫乭o፱ݳյཷᕹ๻᩽ꁿ궉꺍ﲙﮝ쒟財", a_));
		IL_10F:
		if (false)
		{
		}
	}

	// Token: 0x06004889 RID: 18569 RVA: 0x002BE8E0 File Offset: 0x002BD8E0
	private void ᜀ(ref string A_0, spr\u17FF A_1)
	{
		int a_ = 12;
		for (;;)
		{
			PasswordRequiredEventArgs passwordRequiredEventArgs = new PasswordRequiredEventArgs();
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (passwordRequiredEventArgs.StopParsing)
					{
						num = 4;
						continue;
					}
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7C;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_7C;
				case 3:
					if (passwordRequiredEventArgs != null)
					{
						num = 1;
						continue;
					}
					goto IL_AA;
				case 4:
					goto IL_122;
				case 5:
					goto IL_7C;
				case 6:
					if (A_0 == null)
					{
						num = 9;
						continue;
					}
					goto IL_7C;
				case 7:
					A_0 = passwordRequiredEventArgs.NewPassword;
					num = 2;
					continue;
				case 8:
					if (A_1.ᜁ(this, passwordRequiredEventArgs))
					{
						num = 7;
						continue;
					}
					passwordRequiredEventArgs = null;
					num = 5;
					continue;
				case 9:
					num = 8;
					continue;
				case 10:
					num = 3;
					continue;
				case 11:
					if (A_0 != null)
					{
						num = 10;
						continue;
					}
					goto IL_AA;
				}
				break;
				IL_7C:
				num = 11;
			}
		}
		IL_AA:
		throw new ArgumentException(RecordTableEnumerator.b("ᕁ⭃㑅⍇⡉⍋⅍㭏牑㵓╕硗⩙⹛ㅝᑟݡݣብ൧๩䱫཭ṯᙱ味ٵ᥷ॹཻॽꚅﾇﾋ랏뒓ﾙﾛ욟쮡솣슥蚧", a_));
		IL_122:
		goto IL_AA;
	}

	// Token: 0x0600488A RID: 18570 RVA: 0x002BEA24 File Offset: 0x002BDA24
	public XlsWorkbook \u171C()
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
		return this.\u171B;
	}

	// Token: 0x0600488B RID: 18571 RVA: 0x002BEA68 File Offset: 0x002BDA68
	public spr\u2306 \u1718()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_75;
			case 1:
				if (true)
				{
				}
				this.\u171C = new spr\u2306(this.\u171B);
				num = 0;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4A;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			}
			goto IL_42;
			IL_4A:
			num = 1;
			continue;
			IL_42:
			if (this.\u171C == null)
			{
				goto IL_4A;
			}
			break;
		}
		IL_75:
		return this.\u171C;
	}

	// Token: 0x0600488C RID: 18572 RVA: 0x002BEAF4 File Offset: 0x002BDAF4
	public spr\u2570 ᜀ(sprᦨ A_0, string A_1)
	{
		if (A_0 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				return null;
			}
		}
		return this.\u171A.ᜃ(sprវ.ᜀ(A_1, A_0.ᜂ()));
	}

	// Token: 0x0600488D RID: 18573 RVA: 0x002BEB50 File Offset: 0x002BDB50
	public spr\u1B7A \u170D()
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				ExcelVersion u173D = this.\u173D;
				num = 2;
				continue;
			}
			case 1:
				goto IL_80;
			case 2:
			{
				ExcelVersion u173D;
				switch (u173D)
				{
				case ExcelVersion.Version2007:
					this.ᜤ = new spr\u1B7A(this.\u171B);
					num = 1;
					continue;
				case ExcelVersion.Version2010:
					if (true)
					{
					}
					this.ᜤ = new spr\u1CC3(this.\u171B);
					num = 3;
					continue;
				default:
					num = 6;
					continue;
				}
				break;
			}
			case 3:
				goto IL_62;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C;
				}
				goto Block_3;
			case 6:
				num = 5;
				continue;
			}
			IL_2C:
			if (this.ᜤ != null)
			{
				break;
			}
			num = 0;
		}
		IL_62:
		IL_80:
		goto IL_EA;
		Block_3:
		if (false)
		{
		}
		throw new NotImplementedException();
		IL_EA:
		return this.ᜤ;
	}

	// Token: 0x0600488E RID: 18574 RVA: 0x002BEC50 File Offset: 0x002BDC50
	public List<int> \u1717()
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
		return this.ᜥ;
	}

	// Token: 0x0600488F RID: 18575 RVA: 0x002BEC94 File Offset: 0x002BDC94
	public spr\u249E \u1714()
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
		return this.\u171A;
	}

	// Token: 0x06004890 RID: 18576 RVA: 0x002BECD8 File Offset: 0x002BDCD8
	public int ᜐ()
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
		return this.ᜮ;
	}

	// Token: 0x06004891 RID: 18577 RVA: 0x002BED1C File Offset: 0x002BDD1C
	public void ᜁ(int A_0)
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
		this.ᜮ = A_0;
	}

	// Token: 0x06004892 RID: 18578 RVA: 0x002BED60 File Offset: 0x002BDD60
	public int \u1715()
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
		return this.ᜯ;
	}

	// Token: 0x06004893 RID: 18579 RVA: 0x002BEDA4 File Offset: 0x002BDDA4
	public void ᜄ(int A_0)
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
		this.ᜯ = A_0;
	}

	// Token: 0x06004894 RID: 18580 RVA: 0x002BEDE8 File Offset: 0x002BDDE8
	public int ᜤ()
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
		return this.ᜰ;
	}

	// Token: 0x06004895 RID: 18581 RVA: 0x002BEE2C File Offset: 0x002BDE2C
	public void ᜂ(int A_0)
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
		this.ᜰ = A_0;
	}

	// Token: 0x06004896 RID: 18582 RVA: 0x002BEE70 File Offset: 0x002BDE70
	public int ᜠ()
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
		return this.ᜱ;
	}

	// Token: 0x06004897 RID: 18583 RVA: 0x002BEEB4 File Offset: 0x002BDEB4
	public void ᜆ(int A_0)
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
		this.ᜱ = A_0;
	}

	// Token: 0x06004898 RID: 18584 RVA: 0x002BEEF8 File Offset: 0x002BDEF8
	public int \u171E()
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
		return this.\u1732;
	}

	// Token: 0x06004899 RID: 18585 RVA: 0x002BEF3C File Offset: 0x002BDF3C
	public void ᜅ(int A_0)
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
		this.\u1732 = A_0;
	}

	// Token: 0x0600489A RID: 18586 RVA: 0x002BEF80 File Offset: 0x002BDF80
	public int ᜎ()
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
		return this.\u1733;
	}

	// Token: 0x0600489B RID: 18587 RVA: 0x002BEFC4 File Offset: 0x002BDFC4
	public void ᜇ(int A_0)
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
		this.\u1733 = A_0;
	}

	// Token: 0x0600489C RID: 18588 RVA: 0x002BF008 File Offset: 0x002BE008
	public IDictionary<string, string> \u171A()
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
		return this.\u171D;
	}

	// Token: 0x0600489D RID: 18589 RVA: 0x002BF04C File Offset: 0x002BE04C
	public IDictionary<string, string> ᜡ()
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
		return this.\u171E;
	}

	// Token: 0x0600489E RID: 18590 RVA: 0x002BF090 File Offset: 0x002BE090
	public int ᜏ()
	{
		if (this.\u1736 == null)
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
				if (true)
				{
				}
				return int.MinValue;
			}
		}
		return this.\u1736.Count;
	}

	// Token: 0x0600489F RID: 18591 RVA: 0x002BF0E8 File Offset: 0x002BE0E8
	public Dictionary<string, object> ᜣ()
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
		return this.\u1738;
	}

	// Token: 0x060048A0 RID: 18592 RVA: 0x002BF12C File Offset: 0x002BE12C
	public string \u1719()
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
		return this.\u173B;
	}

	// Token: 0x060048A1 RID: 18593 RVA: 0x002BF170 File Offset: 0x002BE170
	public void ᜊ(string A_0)
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
		this.\u173B = A_0;
	}

	// Token: 0x060048A2 RID: 18594 RVA: 0x002BF1B4 File Offset: 0x002BE1B4
	public FileVersion \u171B()
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
		return this.\u173A;
	}

	// Token: 0x060048A3 RID: 18595 RVA: 0x002BF1F8 File Offset: 0x002BE1F8
	internal Dictionary<string, string> ᜢ()
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
		return this.\u173C;
	}

	// Token: 0x060048A4 RID: 18596 RVA: 0x002BF23C File Offset: 0x002BE23C
	public Stream \u1712()
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
		return this.\u173E;
	}

	// Token: 0x060048A5 RID: 18597 RVA: 0x002BF280 File Offset: 0x002BE280
	public void ᜀ(Stream A_0)
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
		this.\u173E = A_0;
	}

	// Token: 0x060048A6 RID: 18598 RVA: 0x002BF2C4 File Offset: 0x002BE2C4
	public void ᜃ(string A_0, string A_1)
	{
		int a_ = 2;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_D5;
			case 1:
				if (A_0.Length == 0)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				num = 4;
				continue;
			case 2:
				num = 1;
				continue;
			case 3:
				goto IL_9D;
			case 4:
				if (A_0[0] != '/')
				{
					num = 5;
					continue;
				}
				goto IL_D7;
			case 5:
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_B5;
					}
				}
				IL_B5:
				if (false)
				{
				}
				A_0 = '/' + A_0;
				num = 0;
				continue;
			}
			if (A_0 == null)
			{
				break;
			}
			num = 2;
		}
		IL_69:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帷匹倻嬽฿⍁⥃⍅", a_));
		IL_9D:
		goto IL_69;
		IL_D5:
		IL_D7:
		this.ᜡ()[A_0] = A_1;
	}

	// Token: 0x060048A7 RID: 18599 RVA: 0x002BF3B8 File Offset: 0x002BE3B8
	public List<spr\u21A7> \u1713()
	{
		int a_ = 0;
		List<spr\u21A7> list;
		for (;;)
		{
			XmlReader xmlReader;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_F1:
				xmlReader.Read();
				num = 6;
				break;
			default:
				if (false)
				{
				}
				list = null;
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u1736 == null)
					{
						num = 7;
						continue;
					}
					list = this.\u1736;
					num = 8;
					continue;
				case 1:
					if (true)
					{
					}
					if (this.ᜭ == null)
					{
						num = 3;
						continue;
					}
					num = 0;
					continue;
				case 2:
					return list;
				case 3:
					return list;
				case 4:
					if (xmlReader.LocalName != RecordTableEnumerator.b("刵䀷尹伻", a_))
					{
						num = 5;
						continue;
					}
					goto IL_78;
				case 5:
					goto IL_F1;
				case 6:
					goto IL_78;
				case 7:
					this.ᜭ.Position = 0L;
					xmlReader = UtilityMethods.ᜀ(this.ᜭ);
					num = 4;
					continue;
				case 8:
					return list;
				}
				break;
				IL_78:
				list = this.\u1718().ᜣ(xmlReader);
				this.ᜭ.Flush();
				this.\u1736 = list;
				num = 2;
			}
		}
		return list;
	}

	// Token: 0x060048A8 RID: 18600 RVA: 0x002BF514 File Offset: 0x002BE514
	public sprᡟ ᜌ(string A_0)
	{
		int a_ = 18;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.Length == 0)
				{
					num = 2;
					continue;
				}
				goto IL_92;
			case 2:
				goto IL_36;
			case 3:
				num = 0;
				continue;
			}
			if (A_0 != null)
			{
				num = 3;
				continue;
			}
			IL_36:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_56;
			}
		}
		IL_56:
		if (true)
		{
		}
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㭇≉⥋⭍⑏ɑ㕓≕し", a_));
		IL_92:
		throw new NotImplementedException();
	}

	// Token: 0x060048A9 RID: 18601 RVA: 0x002BF5B8 File Offset: 0x002BE5B8
	public XlsWorksheetBase ᜎ(string A_0)
	{
		int a_ = 10;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_36;
			case 2:
				num = 3;
				continue;
			case 3:
				if (A_0.Length == 0)
				{
					num = 1;
					continue;
				}
				goto IL_92;
			}
			if (A_0 != null)
			{
				num = 2;
				continue;
			}
			IL_36:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_56;
			}
		}
		IL_56:
		if (true)
		{
		}
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㌿⩁⅃⍅㱇щⵋ⍍㕏", a_));
		IL_92:
		return this.\u171B.Objects[A_0] as XlsWorksheetBase;
	}

	// Token: 0x060048AA RID: 18602 RVA: 0x002BF670 File Offset: 0x002BE670
	public void ᜁ(ref List<Color> A_0)
	{
		int a_ = 10;
		bool throwOnUnknownNames;
		for (;;)
		{
			this.\u171B.Loading = true;
			throwOnUnknownNames = this.\u171B.ThrowOnUnknownNames;
			this.\u171B.ThrowOnUnknownNames = false;
			this.\u171D();
			this.\u171F = this.ᜇ(RecordTableEnumerator.b("Ἷぁ⅃⩅㭇敉手㱍㕏㹑❓", a_));
			this.ᜠ = this.ᜄ(RecordTableEnumerator.b("ℿ㉁㑃⩅ⅇ⥉ⵋ㩍㥏㵑㩓祕⹗㑙㡛灝ཟቡţࡥၧݩk࡭Ὧqᥳ᝵౷ॹ养ᅽﾑ뒙튟잡얣슥\udba7슩즫쮭쒯\udfb1\ud8b3颵쮷특\ud9bb\udbbd뒿꧃Ʂꇇ꓉뛍뷏뻑", a_));
			this.\u1738.Add(RecordTableEnumerator.b("Ἷぁ⅃⩅㭇敉手㱍㕏㹑❓", a_), null);
			int num = 3;
			for (;;)
			{
				Dictionary<string, object>.KeyCollection.Enumerator enumerator;
				switch (num)
				{
				case 0:
					goto IL_23D;
				case 1:
					if (this.ᜠ[0] == '/')
					{
						num = 2;
						continue;
					}
					goto IL_29D;
				case 2:
					if (true)
					{
					}
					this.ᜠ = UtilityMethods.ᜀ(this.ᜠ);
					num = 11;
					continue;
				case 3:
					if (this.ᜠ == null)
					{
						num = 4;
						continue;
					}
					goto IL_2F1;
				case 4:
					num = 13;
					continue;
				case 5:
					if (this.ᜆ(RecordTableEnumerator.b("ℿ㉁㑃⩅ⅇ⥉ⵋ㩍㥏㵑㩓祕⹗㑙㡛灝ൟᅡ䥣ͥၧ३५ɭ幯ٱᅳ᭵ࡷᙹᵻ੽겁쮍ﺏ歹ﶗﺙ늛솟쮡쪣趥킧잩삫", a_)))
					{
						num = 14;
						continue;
					}
					goto IL_2F1;
				case 6:
					goto IL_2EC;
				case 7:
					goto IL_1DE;
				case 8:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_1CB;
							case 3:
								num = 0;
								continue;
							case 4:
							{
								if (!enumerator.MoveNext())
								{
									num = 3;
									continue;
								}
								string a_2 = enumerator.Current;
								this.\u171A.ᜀ(a_2);
								num = 2;
								continue;
							}
							}
							IL_1A8:
							num = 4;
							continue;
							goto IL_1A8;
						}
						IL_1CB:
						goto IL_314;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_1DE;
				case 9:
					if (this.ᜠ == null)
					{
						num = 6;
						continue;
					}
					num = 1;
					continue;
				case 10:
					goto IL_2F1;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_23D;
					default:
						if (false)
						{
						}
						goto IL_29D;
					}
					break;
				case 12:
					if (this.ᜠ == null)
					{
						num = 0;
						continue;
					}
					goto IL_2CB;
				case 13:
					if (!this.ᜆ(RecordTableEnumerator.b("ℿ㉁㑃⩅ⅇ⥉ⵋ㩍㥏㵑㩓祕⹗㑙㡛灝ൟᅡ䥣ͥၧ३५ɭ幯űᱳ፵ᵷ๹剻፽춇ﲏ뢕ﮙ讟\udaa1즣쪥", a_)))
					{
						num = 7;
						continue;
					}
					goto IL_148;
				case 14:
					goto IL_148;
				case 15:
					goto IL_2CB;
				}
				break;
				IL_148:
				this.\u171B.HasMacros = true;
				num = 10;
				continue;
				IL_1DE:
				num = 5;
				continue;
				IL_23D:
				this.ᜆ(RecordTableEnumerator.b("ℿ㉁㑃⩅ⅇ⥉ⵋ㩍㥏㵑㩓祕⹗㑙㡛灝ཟቡţࡥၧݩk࡭Ὧqᥳ᝵౷ॹ养ᅽﾑ뒙튟잡얣슥\udba7슩즫쮭쒯\udfb1\ud8b3颵첷\udfb9톻캽겿ꏁ냃ꏅꟉ귋ꟍ뻏六곓믕듗", a_));
				num = 15;
				continue;
				IL_29D:
				this.ᜑ();
				this.ᜀ(ref A_0);
				enumerator = this.\u1738.Keys.GetEnumerator();
				num = 8;
				continue;
				IL_2CB:
				num = 9;
				continue;
				IL_2F1:
				num = 12;
			}
		}
		IL_2EC:
		throw new NotSupportedException(RecordTableEnumerator.b("ؿ⭁⡃⍅桇ⱉ⍋㱍㵏㍑⁓癕ㅗ⥙籛そཟᙡ䑣ѥ൧䩩Ὣ᭭oɱ᭳ѵ౷ό᡻", a_));
		IL_314:
		this.\u1738.Clear();
		this.\u171B.ThrowOnUnknownNames = throwOnUnknownNames;
		this.\u171B.Loading = false;
	}

	// Token: 0x060048AB RID: 18603 RVA: 0x002BF9C4 File Offset: 0x002BE9C4
	private bool ᜆ(string A_0)
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
		this.ᜪ = A_0;
		this.ᜠ = this.ᜄ(A_0);
		return this.ᜠ != null;
	}

	// Token: 0x060048AC RID: 18604 RVA: 0x002BFA20 File Offset: 0x002BEA20
	public void \u171D()
	{
		int a_ = 16;
		if (true)
		{
		}
		this.\u171D.Clear();
		this.\u171E.Clear();
		spr\u2570 spr_u = this.\u171A.ᜃ(RecordTableEnumerator.b("ᵅେ╉≋㩍㕏㱑⁓ॕ౗⍙ⱛ㭝፟㽡䩣ṥէ٩", a_));
		if (spr_u == null)
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
				throw new NotSupportedException(RecordTableEnumerator.b("Eⅇ♉⥋湍㍏㍑㩓㡕㝗⹙籛㱝՟䉡ୣᙥ൧ѩ५੭偯影味ၵ᝷ࡹᅻώꊁꢇ揄낏얟욡", a_));
			}
		}
		XmlReader a_2 = UtilityMethods.ᜀ(spr_u.ᜐ());
		this.\u1718().ᜀ(a_2, this.\u171D, this.\u171E);
		this.\u1738.Add(RecordTableEnumerator.b("ᵅେ╉≋㩍㕏㱑⁓ॕ౗⍙ⱛ㭝፟㽡䩣ṥէ٩", a_), null);
	}

	// Token: 0x060048AD RID: 18605 RVA: 0x002BFAEC File Offset: 0x002BEAEC
	public void ᜑ()
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.\u170D(RecordTableEnumerator.b("⁀㍂㕄⭆⁈⡊ⱌ㭎㡐㱒㭔硖⽘㕚㥜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ偼ཾꎌﺐ몖쒠톢톤캦첨\ud8aa蚬힮\udcb0\udfb2", a_));
		this.\u170D(RecordTableEnumerator.b("⁀㍂㕄⭆⁈⡊ⱌ㭎㡐㱒㭔硖⽘㕚㥜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ偼ၾﺒ練떚햠욢쮤쎦첨쾪肬\udfae쎰\udcb2어튶쮸쾺풼\udabe닀뷄꫆ꗈ", a_));
		this.\u170D(RecordTableEnumerator.b("⁀㍂㕄⭆⁈⡊ⱌ㭎㡐㱒㭔硖⽘㕚㥜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ偼ၾﺒ練떚ﺜ튠힢쪤쪦蒨\udbaa\udfac삮솰횲잴쎶킸\udeba캼钾맀껂꧄", a_));
	}

	// Token: 0x060048AE RID: 18606 RVA: 0x002BFB6C File Offset: 0x002BEB6C
	public void \u170D(string A_0)
	{
		int a_ = 4;
		string a_2;
		for (;;)
		{
			XmlReader xmlReader = this.ᜁ(A_0, out a_2);
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!(A_0 == RecordTableEnumerator.b("嬹䰻丽ⰿ⭁❃❅㱇⍉⍋⁍罏⑑㩓㉕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ孵᝷ᱹ᩻᝽ﾉﺏ몓鍊쒟잡삣讥\ud8a7\ud8a9쎫\udead햯삱삳\udfb5\uddb7즹鞻욽궿껁", a_)))
					{
						num = 12;
						continue;
					}
					this.\u1718().ᜦ(xmlReader);
					this.\u171F.RemoveByContentType(RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻ᅽ캉ﾑ떙꺛꺝邟钡讣풥춧용춫\udaad\ud9af\uddb1\udab3억킷펹첻춽ꟁ볃닅귇꓉꣋ꯍ듏￑ꓓꓕ럗꫙맛곝铟诡臣闥", a_));
					num = 7;
					continue;
				case 1:
					goto IL_15F;
				case 2:
					if (A_0 != null)
					{
						num = 10;
						continue;
					}
					goto IL_164;
				case 3:
					return;
				case 4:
					goto IL_B5;
				case 5:
					goto IL_211;
				case 6:
					if (!(A_0 == RecordTableEnumerator.b("嬹䰻丽ⰿ⭁❃❅㱇⍉⍋⁍罏⑑㩓㉕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ孵᝷ᱹ᩻᝽ﾉﺏ몓춟辡풣풥잧\udaa9즫\udcad쒯\udbb1톳억鎷승톻튽", a_)))
					{
						num = 5;
						continue;
					}
					this.\u1718().ᜨ(xmlReader);
					this.\u171F.RemoveByContentType(RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻ᅽ캉ﾑ떙꺛꺝邟钡讣풥춧용춫\udaad\ud9af\uddb1\udab3억킷펹첻춽ꇁ뇃뗅볇ꗉꇋꃏꃑ믓ꛕ뷗꣙꣛럝藟釡", a_));
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_211;
					default:
						if (false)
						{
						}
						num = 13;
						continue;
					}
					break;
				case 7:
					goto IL_9B;
				case 8:
					if (!(A_0 == RecordTableEnumerator.b("嬹䰻丽ⰿ⭁❃❅㱇⍉⍋⁍罏⑑㩓㉕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ孵ࡷ᭹ύᕽꢅﺋ붏秊ﾙ즟잡힣趥킧잩삫", a_)))
					{
						num = 11;
						continue;
					}
					this.\u1718().ᜮ(xmlReader);
					this.\u171F.RemoveByContentType(RecordTableEnumerator.b("刹䠻䨽〿硁歃楅㭇⥉⑋⭍㵏㍑❓硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű婳᥵੷ᵹ卻๽ꎋ벍ꂏꊑꊓ릕ﾙﾝ풟쮡쮣좥\udba7슩얫\udead쎯鶱\ud9b3펵첷\udbb9\ud8bb\udfbd뒿ꏁꗅꟇ룉꧋ꃏꃑ믓ꛕ뷗꣙꣛럝藟釡", a_));
					num = 1;
					continue;
				case 9:
					if (xmlReader == null)
					{
						num = 3;
						continue;
					}
					num = 2;
					continue;
				case 10:
					num = 8;
					continue;
				case 11:
					num = 0;
					continue;
				case 12:
					if (true)
					{
					}
					num = 6;
					continue;
				case 13:
					goto IL_129;
				}
				break;
				IL_211:
				num = 4;
			}
		}
		return;
		IL_9B:
		goto IL_216;
		IL_B5:
		goto IL_164;
		IL_129:
		IL_15F:
		goto IL_216;
		IL_164:
		throw new ArgumentException(RecordTableEnumerator.b("䤹䠻䰽̿ⵁ⩃㉅ⵇ⑉㡋ᩍ⥏≑ㅓ", a_));
		IL_216:
		this.\u171A.ᜀ(a_2);
	}

	// Token: 0x060048AF RID: 18607 RVA: 0x002BFD9C File Offset: 0x002BED9C
	public XmlReader ᜁ(string A_0, out string A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			string text;
			for (;;)
			{
				if (true)
				{
				}
				text = this.ᜄ(A_0);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (text == null)
						{
							num = 3;
							continue;
						}
						this.\u171E.Remove(text);
						num = 1;
						continue;
					case 1:
						if (text.StartsWith(RecordTableEnumerator.b("ጻ", a_)))
						{
							num = 4;
							continue;
						}
						goto IL_D6;
					case 2:
						goto IL_6B;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						goto Block_3;
					case 4:
						text = UtilityMethods.ᜀ(text);
						num = 2;
						continue;
					}
					break;
				}
			}
			IL_6B:
			goto IL_D6;
			Block_3:
			if (false)
			{
			}
			A_1 = string.Empty;
			return null;
			IL_D6:
			spr\u2570 spr_u = this.\u171A.ᜃ(text);
			A_1 = spr_u.ᜇ();
			Stream stream = spr_u.ᜐ();
			stream.Position = 0L;
			return UtilityMethods.ᜀ(stream);
		}
		}
	}

	// Token: 0x060048B0 RID: 18608 RVA: 0x002BFEAC File Offset: 0x002BEEAC
	public void ᜀ(string A_0, ExcelSaveType A_1)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			if (true)
			{
			}
			FileStream fileStream = new FileStream(A_0, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
			try
			{
				this.ᜀ(fileStream, A_1);
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)fileStream).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_7B;
					}
					if (fileStream == null)
					{
						break;
					}
					num = 0;
				}
				IL_7B:;
			}
			break;
		}
		}
	}

	// Token: 0x060048B1 RID: 18609 RVA: 0x002BFF48 File Offset: 0x002BEF48
	public void ᜀ(Stream A_0, ExcelSaveType A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜂ(A_1);
				int num = 4;
				for (;;)
				{
					MemoryStream memoryStream;
					spr\u2496 spr_u;
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						goto IL_1F1;
					case 1:
						if (this.\u171B.\u1773 != EncryptionType.None)
						{
							num = 0;
							continue;
						}
						this.\u171A.ᜀ(A_0, false);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 2:
						goto IL_181;
					case 3:
						return;
					case 4:
						if (this.\u171B.PasswordToOpen == null)
						{
							num = 2;
							continue;
						}
						goto IL_1F1;
					case 5:
						try
						{
							num = 7;
							for (;;)
							{
								string text;
								spr\u237F spr_u237F;
								switch (num)
								{
								case 0:
									if (text == null)
									{
										num = 6;
										continue;
									}
									goto IL_11D;
								case 1:
									goto IL_11D;
								case 2:
									spr_u237F = new spr\u237F();
									goto IL_D1;
								case 3:
									spr_u237F = new sprᮆ();
									goto IL_D1;
								case 4:
									num = 3;
									continue;
								case 5:
									goto IL_13E;
								case 6:
									text = RecordTableEnumerator.b("怵崷嘹䨻嬽㐿ᅁ㍃⍅⥇㹉㽋♍㽏≑", a_);
									num = 1;
									continue;
								}
								if (this.\u171B.Version != ExcelVersion.Version2007)
								{
									num = 4;
									continue;
								}
								num = 2;
								continue;
								IL_D1:
								spr\u237F spr_u237F2 = spr_u237F;
								memoryStream.Position = 0L;
								text = this.\u171B.PasswordToOpen;
								num = 0;
								continue;
								IL_11D:
								spr_u237F2.ᜀ(memoryStream, text, spr_u.ᜀ());
								spr_u.ᜀ(A_0);
								num = 5;
							}
							IL_13E:
							return;
						}
						finally
						{
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_17E;
								case 2:
									spr_u.Dispose();
									num = 0;
									continue;
								}
								if (spr_u == null)
								{
									break;
								}
								num = 2;
							}
							IL_17E:;
						}
						goto IL_181;
					}
					break;
					IL_181:
					num = 1;
					continue;
					IL_1F1:
					memoryStream = new MemoryStream();
					this.\u171A.ᜀ(memoryStream, false);
					spr_u = this.\u171B.AppImplementation.ᜄ();
					num = 5;
				}
			}
			return;
		}
	}

	// Token: 0x060048B2 RID: 18610 RVA: 0x002C018C File Offset: 0x002BF18C
	public void ᜂ(ExcelSaveType A_0)
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
		this.ᜯ = 0;
		this.ᜮ = 0;
		this.ᜰ = 0;
		this.ᜱ = 0;
		this.\u1732 = 0;
		this.\u1734 = 0;
		this.\u1733 = 0;
		this.ᜁ(A_0);
		this.ᜋ();
		this.ᜌ();
		this.ᜉ();
	}

	// Token: 0x060048B3 RID: 18611 RVA: 0x002C0214 File Offset: 0x002BF214
	public string ᜁ(ImageFormat A_0)
	{
		int a_ = 12;
		while (A_0 != null)
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
			{
				if (false)
				{
				}
				string text;
				string value = sprវ.ᜀ(A_0, out text);
				this.\u171D[text] = value;
				return text;
			}
			}
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("⭁⥃❅⽇⽉ੋ⅍≏㽑㕓≕", a_));
	}

	// Token: 0x060048B4 RID: 18612 RVA: 0x002C0288 File Offset: 0x002BF288
	public static string ᜀ(ImageFormat A_0, out string A_1)
	{
		int a_ = 7;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string result = RecordTableEnumerator.b("吼刾⁀⑂⁄框㥈╊⩌", a_);
				A_1 = RecordTableEnumerator.b("䴼儾♀", a_);
				num = 13;
				continue;
			}
			case 1:
			{
				string result;
				return result;
			}
			case 2:
			{
				string result;
				return result;
			}
			case 3:
				if (A_0.Equals(ImageFormat.Png))
				{
					num = 0;
					continue;
				}
				num = 8;
				continue;
			case 4:
			{
				string result;
				return result;
			}
			case 5:
			{
				if (A_0.Equals(ImageFormat.Gif))
				{
					goto IL_241;
				}
				string result = RecordTableEnumerator.b("吼刾⁀⑂⁄框㥈╊⩌", a_);
				A_1 = RecordTableEnumerator.b("䴼儾♀", a_);
				num = 7;
				continue;
			}
			case 6:
				if (A_0.Equals(ImageFormat.Bmp))
				{
					num = 10;
					continue;
				}
				num = 15;
				continue;
			case 7:
			{
				string result;
				return result;
			}
			case 8:
				if (A_0.Equals(ImageFormat.Emf))
				{
					num = 16;
					continue;
				}
				num = 5;
				continue;
			case 10:
			{
				string result = RecordTableEnumerator.b("吼刾⁀⑂⁄框⭈♊㵌", a_);
				A_1 = RecordTableEnumerator.b("弼刾ㅀ", a_);
				num = 1;
				continue;
			}
			case 11:
			{
				string result;
				return result;
			}
			case 12:
				goto IL_8B;
			case 13:
			{
				string result;
				return result;
			}
			case 14:
			{
				string result = RecordTableEnumerator.b("吼刾⁀⑂⁄框⍈㭊⡌⡎", a_);
				A_1 = RecordTableEnumerator.b("圼伾⑀⑂", a_);
				num = 4;
				continue;
			}
			case 15:
				if (A_0.Equals(ImageFormat.Jpeg))
				{
					num = 14;
					continue;
				}
				num = 3;
				continue;
			case 16:
			{
				string result = RecordTableEnumerator.b("吼刾⁀⑂⁄框ㅈ晊⡌≎㝐", a_);
				A_1 = RecordTableEnumerator.b("堼刾❀", a_);
				num = 2;
				continue;
			}
			case 17:
			{
				string result = RecordTableEnumerator.b("吼刾⁀⑂⁄框⹈≊⭌", a_);
				A_1 = RecordTableEnumerator.b("娼嘾❀", a_);
				if (true)
				{
				}
				num = 11;
				continue;
			}
			}
			if (A_0 != null)
			{
				num = 6;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 12;
				continue;
			}
			IL_241:
			num = 17;
		}
		IL_8B:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬼倾㍀⹂⑄㍆", a_));
	}

	// Token: 0x060048B5 RID: 18613 RVA: 0x002C053C File Offset: 0x002BF53C
	public string ᜀ(Image A_0, string A_1)
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
		return this.ᜀ(A_0, A_0.RawFormat, A_1);
	}

	// Token: 0x060048B6 RID: 18614 RVA: 0x002C0588 File Offset: 0x002BF588
	public string ᜀ(Image A_0, ImageFormat A_1, string A_2)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			int num = 6;
			MemoryStream memoryStream;
			string text;
			for (;;)
			{
				MemoryStream memoryStream2;
				string text2;
				Regex a_2;
				ImageFormat rawFormat;
				switch (num)
				{
				case 0:
					goto IL_2A5;
				case 1:
					goto IL_1EE;
				case 2:
					goto IL_91;
				case 3:
					if (memoryStream.Length != memoryStream2.Length)
					{
						num = 16;
						continue;
					}
					goto IL_343;
				case 4:
					if (this.\u171A.ᜁ(new Regex(A_2.Split(new char[]
					{
						'.'
					})[0])) != -1)
					{
						num = 12;
						continue;
					}
					this.ᜱ++;
					text = A_2;
					num = 0;
					continue;
				case 5:
					goto IL_2A5;
				case 7:
					text += text2;
					num = 5;
					continue;
				case 8:
					if (this.\u171A.ᜁ(a_2) == -1)
					{
						num = 7;
						continue;
					}
					goto IL_100;
				case 9:
					memoryStream2 = this.\u1719[text];
					num = 3;
					continue;
				case 10:
					goto IL_8C;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2F8;
					default:
						if (false)
						{
						}
						goto IL_2A5;
					}
					break;
				case 12:
					this.ᜱ++;
					text = string.Format(RecordTableEnumerator.b("㹅⑇敉⅋⭍㑏㭑㕓祕ㅗ㝙㵛㥝՟ᥡ呣᭥䙧", a_), this.ᜱ);
					text += text2;
					num = 11;
					continue;
				case 13:
					if (this.\u1719.ContainsKey(text))
					{
						if (true)
						{
						}
						num = 9;
						continue;
					}
					goto IL_343;
				case 14:
					goto IL_100;
				case 15:
					if (rawFormat.Equals(ImageFormat.Wmf))
					{
						num = 2;
						continue;
					}
					memoryStream = new MemoryStream();
					A_0.Save(memoryStream, A_1);
					num = 1;
					continue;
				case 16:
					memoryStream = memoryStream2;
					num = 17;
					continue;
				case 17:
					goto IL_16F;
				case 18:
					if (!rawFormat.Equals(ImageFormat.Emf))
					{
						num = 20;
						continue;
					}
					goto IL_91;
				case 19:
					if (A_2 == null)
					{
						num = 14;
						continue;
					}
					goto IL_2F8;
				case 20:
					num = 15;
					continue;
				}
				if (A_0 == null)
				{
					num = 10;
					continue;
				}
				text2 = this.ᜀ(A_1);
				this.ᜁ(A_1);
				text = null;
				num = 19;
				continue;
				IL_91:
				memoryStream2 = new MemoryStream();
				memoryStream = spr\u17B7.ᜀ(A_0);
				num = 13;
				continue;
				IL_100:
				this.ᜱ++;
				text = string.Format(RecordTableEnumerator.b("㹅⑇敉⅋⭍㑏㭑㕓祕ㅗ㝙㵛㥝՟ᥡ呣᭥䙧", a_), this.ᜱ);
				a_2 = new Regex(text);
				num = 8;
				continue;
				IL_2A5:
				A_1 = this.ᜅ(text2);
				rawFormat = A_0.RawFormat;
				num = 18;
				continue;
				IL_2F8:
				num = 4;
			}
			IL_8C:
			throw new ArgumentNullException(RecordTableEnumerator.b("⽅╇⭉⭋⭍", a_));
			IL_16F:
			IL_1EE:
			IL_343:
			this.\u171A.ᜀ(text, memoryStream, true, FileAttributes.Archive);
			return text;
		}
		}
	}

	// Token: 0x060048B7 RID: 18615 RVA: 0x002C08EC File Offset: 0x002BF8EC
	private ImageFormat ᜅ(string A_0)
	{
		int a_ = 0;
		int num = 20;
		for (;;)
		{
			ImageFormat result;
			switch (num)
			{
			case 0:
				goto IL_270;
			case 1:
				return result;
			case 2:
				goto IL_231;
			case 3:
				if (true)
				{
				}
				num = 11;
				continue;
			case 4:
				num = 0;
				continue;
			case 5:
				return result;
			case 6:
				if (A_0 == null)
				{
					goto IL_270;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_84;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 7:
				return result;
			case 8:
			{
				int num2;
				switch (num2)
				{
				case 0:
					result = ImageFormat.Bmp;
					num = 5;
					continue;
				case 1:
					result = ImageFormat.Jpeg;
					num = 7;
					continue;
				case 2:
					result = ImageFormat.Tiff;
					num = 21;
					continue;
				case 3:
					result = ImageFormat.Exif;
					num = 1;
					continue;
				case 4:
					goto IL_84;
				case 5:
					result = ImageFormat.Emf;
					num = 14;
					continue;
				case 6:
					result = ImageFormat.Jpeg;
					num = 13;
					continue;
				case 7:
					result = ImageFormat.Wmf;
					num = 16;
					continue;
				case 8:
					result = ImageFormat.Gif;
					num = 9;
					continue;
				default:
					num = 4;
					continue;
				}
				break;
			}
			case 9:
				return result;
			case 10:
				goto IL_7F;
			case 11:
				if (spr\u22D2.\u1771 == null)
				{
					num = 15;
					continue;
				}
				goto IL_231;
			case 12:
				num = 8;
				continue;
			case 13:
				return result;
			case 14:
				return result;
			case 15:
				spr\u22D2.\u1771 = new Dictionary<string, int>(9)
				{
					{
						RecordTableEnumerator.b("吵唷䨹", a_),
						0
					},
					{
						RecordTableEnumerator.b("尵䠷弹嬻", a_),
						1
					},
					{
						RecordTableEnumerator.b("䈵儷尹娻", a_),
						2
					},
					{
						RecordTableEnumerator.b("匵䀷匹娻", a_),
						3
					},
					{
						RecordTableEnumerator.b("䘵嘷崹", a_),
						4
					},
					{
						RecordTableEnumerator.b("匵唷尹", a_),
						5
					},
					{
						RecordTableEnumerator.b("張嬷唹刻", a_),
						6
					},
					{
						RecordTableEnumerator.b("䄵唷尹", a_),
						7
					},
					{
						RecordTableEnumerator.b("儵儷尹", a_),
						8
					}
				};
				num = 2;
				continue;
			case 16:
				return result;
			case 17:
				return result;
			case 18:
				return result;
			case 19:
			{
				int num2;
				if (spr\u22D2.\u1771.TryGetValue(A_0, out num2))
				{
					num = 12;
					continue;
				}
				goto IL_270;
			}
			case 21:
				return result;
			}
			if (A_0 == null)
			{
				num = 10;
				continue;
			}
			num = 6;
			continue;
			IL_84:
			result = ImageFormat.Png;
			num = 18;
			continue;
			IL_231:
			num = 19;
			continue;
			IL_270:
			result = ImageFormat.Png;
			num = 17;
		}
		IL_7F:
		throw new ArgumentNullException(RecordTableEnumerator.b("倵圷䠹儻弽㐿", a_));
	}

	// Token: 0x060048B8 RID: 18616 RVA: 0x002C0C34 File Offset: 0x002BFC34
	internal string ᜃ(int A_0)
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
		return this.\u1735[A_0];
	}

	// Token: 0x060048B9 RID: 18617 RVA: 0x002C0C78 File Offset: 0x002BFC78
	public string ᜀ(string A_0, string A_1, string A_2, RelationsCollection A_3, string A_4, ref int A_5, out spr\u2570 A_6)
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
		this.\u171D[A_1] = A_2;
		string text = this.ᜀ(ref A_5, A_0, A_1);
		A_6 = this.\u171A.ᜁ(text, new MemoryStream(), true, FileAttributes.Archive);
		string text2 = A_3.GenerateRelationId();
		sprᦨ a_ = new sprᦨ('/' + text, A_4);
		A_3[text2] = a_;
		return text2;
	}

	// Token: 0x060048BA RID: 18618 RVA: 0x002C0D0C File Offset: 0x002BFD0C
	private string ᜀ(ImageFormat A_0)
	{
		int a_ = 17;
		int num = 13;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string result = RecordTableEnumerator.b("≆⑈ⵊ", a_);
				num = 4;
				continue;
			}
			case 1:
				if (A_0.Equals(ImageFormat.Bmp))
				{
					num = 16;
					continue;
				}
				num = 21;
				continue;
			case 2:
				if (A_0.Equals(ImageFormat.Wmf))
				{
					num = 6;
					continue;
				}
				num = 26;
				continue;
			case 3:
			{
				string result;
				return result;
			}
			case 4:
			{
				string result;
				return result;
			}
			case 5:
			{
				string result;
				return result;
			}
			case 6:
			{
				string result = RecordTableEnumerator.b("う⑈ⵊ", a_);
				goto IL_2EE;
			}
			case 7:
				if (A_0.Equals(ImageFormat.Png))
				{
					num = 9;
					continue;
				}
				num = 14;
				continue;
			case 8:
				goto IL_A5;
			case 9:
			{
				string result = RecordTableEnumerator.b("㝆❈ⱊ", a_);
				num = 23;
				continue;
			}
			case 10:
			{
				string result;
				return result;
			}
			case 11:
			{
				string result;
				return result;
			}
			case 12:
				if (A_0.Equals(ImageFormat.Exif))
				{
					num = 19;
					continue;
				}
				num = 2;
				continue;
			case 14:
				if (A_0.Equals(ImageFormat.Emf))
				{
					num = 0;
					continue;
				}
				num = 18;
				continue;
			case 15:
			{
				string result = RecordTableEnumerator.b("⁆⁈ⵊ", a_);
				num = 3;
				continue;
			}
			case 16:
			{
				string result = RecordTableEnumerator.b("╆⑈㭊", a_);
				num = 27;
				continue;
			}
			case 17:
			{
				string result;
				return result;
			}
			case 18:
				if (!A_0.Equals(ImageFormat.Gif))
				{
					string result = RecordTableEnumerator.b("㝆❈ⱊ", a_);
					num = 29;
					continue;
				}
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2EE;
				default:
					if (false)
					{
					}
					num = 15;
					continue;
				}
				break;
			case 19:
			{
				string result = RecordTableEnumerator.b("≆ㅈ≊⭌", a_);
				num = 11;
				continue;
			}
			case 20:
				if (A_0.Equals(ImageFormat.Jpeg))
				{
					num = 25;
					continue;
				}
				num = 7;
				continue;
			case 21:
				if (A_0.Equals(ImageFormat.Tiff))
				{
					num = 24;
					continue;
				}
				num = 12;
				continue;
			case 22:
			{
				string result = RecordTableEnumerator.b("⹆⩈⑊⍌", a_);
				num = 28;
				continue;
			}
			case 23:
			{
				string result;
				return result;
			}
			case 24:
			{
				string result = RecordTableEnumerator.b("㍆⁈ⵊ⭌", a_);
				num = 17;
				continue;
			}
			case 25:
			{
				string result = RecordTableEnumerator.b("ⵆ㥈⹊⩌", a_);
				num = 10;
				continue;
			}
			case 26:
				if (A_0.Equals(ImageFormat.Icon))
				{
					num = 22;
					continue;
				}
				num = 20;
				continue;
			case 27:
			{
				string result;
				return result;
			}
			case 28:
			{
				string result;
				return result;
			}
			case 29:
			{
				string result;
				return result;
			}
			}
			if (A_0 == null)
			{
				num = 8;
				continue;
			}
			num = 1;
			continue;
			IL_2EE:
			num = 5;
		}
		IL_A5:
		throw new ArgumentNullException(RecordTableEnumerator.b("ⅆ♈㥊⁌⹎═", a_));
	}

	// Token: 0x060048BB RID: 18619 RVA: 0x002C10B0 File Offset: 0x002C00B0
	private void ᜀ(ref List<Color> A_0)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			string text;
			string text2;
			Dictionary<int, int> a_3;
			for (;;)
			{
				text = this.ᜠ;
				int num = 20;
				for (;;)
				{
					sprᦨ sprᦨ;
					sprᦨ sprᦨ2;
					sprᦨ sprᦨ3;
					XmlReader a_2;
					MemoryStream memoryStream;
					spr\u2570 spr_u;
					switch (num)
					{
					case 0:
						if (text.Length == 0)
						{
							num = 21;
							continue;
						}
						num = 14;
						continue;
					case 1:
						this.\u171C().ᜭ();
						this.\u171C().\u171E();
						num = 11;
						continue;
					case 2:
						if (sprᦨ == null)
						{
							num = 1;
							continue;
						}
						goto IL_27D;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1E9;
						default:
							if (false)
							{
							}
							if (sprᦨ2 != null)
							{
								num = 15;
								continue;
							}
							goto IL_195;
						}
						break;
					case 4:
						this.ᜡ = ((sprᦨ3.ᜂ()[0] != '/') ? (text2 + sprᦨ3.ᜂ()) : sprᦨ3.ᜂ());
						a_2 = this.ᜁ(sprᦨ3, text2);
						a_3 = this.\u1718().ᜤ(a_2);
						this.\u1738.Add(this.ᜡ, null);
						num = 10;
						continue;
					case 5:
						goto IL_25E;
					case 6:
						text = UtilityMethods.ᜀ(text);
						num = 23;
						continue;
					case 7:
						goto IL_195;
					case 8:
						goto IL_447;
					case 9:
						num = 0;
						continue;
					case 10:
						goto IL_4B9;
					case 11:
						goto IL_27D;
					case 12:
						goto IL_C2;
					case 13:
						this.ᜢ = text2 + sprᦨ.ᜂ();
						a_2 = this.ᜁ(sprᦨ, text2);
						this.ᜥ = this.\u1718().ᜀ(a_2, ref this.ᜭ);
						this.\u1738.Add(this.ᜢ, null);
						num = 8;
						continue;
					case 14:
						if (text[0] == '/')
						{
							num = 6;
							continue;
						}
						goto IL_3C2;
					case 15:
						if (true)
						{
						}
						this.ᜣ = text2 + sprᦨ2.ᜂ();
						a_2 = this.ᜁ(sprᦨ2, text2);
						A_0 = this.\u1718().ᜪ(a_2);
						num = 7;
						continue;
					case 16:
						if (sprᦨ != null)
						{
							num = 13;
							continue;
						}
						goto IL_447;
					case 17:
						if (sprᦨ3 != null)
						{
							num = 24;
							continue;
						}
						goto IL_4B9;
					case 18:
						if (memoryStream.Length != 0L)
						{
							num = 19;
							continue;
						}
						memoryStream.Close();
						num = 12;
						continue;
					case 19:
						this.\u1739 = memoryStream;
						num = 5;
						continue;
					case 20:
						if (text != null)
						{
							num = 9;
							continue;
						}
						goto IL_47E;
					case 21:
						goto IL_17D;
					case 22:
						goto IL_3ED;
					case 23:
						goto IL_3C2;
					case 24:
						num = 4;
						continue;
					case 25:
						if (spr_u == null)
						{
							num = 22;
							continue;
						}
						goto IL_1E9;
					}
					break;
					IL_195:
					int num2 = 1;
					ITabSheets objects = this.\u171B.Objects;
					spr\u17FF spr_u17FF = this.\u171B.AppImplementation;
					spr_u17FF.ᜀ((long)num2, (long)(objects.Count + 4));
					num = 16;
					continue;
					IL_1E9:
					string text3 = sprវ.ᜁ(text);
					this.ᜦ = this.ᜇ(text3);
					this.\u1738.Add(text3, null);
					sprᦨ = this.ᜦ.ᜀ(RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊ朗\udd98ﺜ철욢쮤펦蚨馪鶬龮螰鲲잴튶햸\udaba즼횾껀귂뛄꿆ꃈ믊뻌ꋐ꟒곔믖볘꣚", a_), out this.ᜧ);
					num = 2;
					continue;
					IL_27D:
					sprᦨ3 = this.ᜦ.ᜀ(RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊ朗\udd98ﺜ철욢쮤펦蚨馪鶬龮螰鲲잴튶햸\udaba즼횾껀귂뛄꿆ꃈ믊뻌ꋐ믒듔ꗖ볘뿚軜ꯞ鏠諢诤胦髨", a_), out this.ᜨ);
					sprᦨ2 = this.ᜦ.ᜀ(RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊ朗\udd98ﺜ철욢쮤펦蚨馪鶬龮螰鲲잴튶햸\udaba즼횾껀귂뛄꿆ꃈ믊뻌ꗐ믒냔뫖볘", a_), out this.ᜩ);
					sprវ.ᜀ(text, out text2);
					memoryStream = new MemoryStream();
					a_2 = sprវ.ᜀ(spr_u);
					this.\u1718().ᜀ(a_2, this.ᜦ, this, text2, this.ᜬ, this.ᜫ, ref this.\u1737, memoryStream);
					int num3 = this.\u171B.TabSheets.Count + 4;
					num = 3;
					continue;
					IL_3C2:
					spr_u = this.\u171A.ᜃ(text);
					num = 25;
					continue;
					IL_447:
					num2++;
					spr_u17FF.ᜀ((long)num2, (long)num3);
					a_3 = null;
					num = 17;
					continue;
					IL_4B9:
					num2++;
					spr_u17FF.ᜀ((long)num2, (long)num3);
					num = 18;
				}
			}
			IL_C2:
			goto IL_4F7;
			IL_17D:
			goto IL_47E;
			IL_25E:
			goto IL_4F7;
			IL_3ED:
			throw new XmlException(RecordTableEnumerator.b("ੈ⩊⍌ⅎ㹐❒畔㭖㙘㡚㱜⭞Ѡ䍢ቤࡦ᭨jཬnṰᡲ啴Ṷ൸Ṻၼ䕾ꆀ", a_) + text);
			IL_47E:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㹈⑊㽌⑎㍐㱒㩔㱖ၘ⽚㡜㉞⽠ɢࡤɦ", a_));
			IL_4F7:
			this.\u1718().ᜀ(a_3);
			this.ᜏ(text2);
			this.\u1718().ᜂ();
			this.ᜄ();
			return;
		}
		}
	}

	// Token: 0x060048BC RID: 18620 RVA: 0x002C15DC File Offset: 0x002C05DC
	internal void ᜏ(string A_0)
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
			switch (0)
			{
			}
			break;
		}
		new PivotCacheCollection(this.\u171B.AppImplementation, this.\u171B);
		string key = "";
		using (Dictionary<string, string>.Enumerator enumerator = this.\u173C.GetEnumerator())
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
				{
					if (!enumerator.MoveNext())
					{
						num = 0;
						continue;
					}
					KeyValuePair<string, string> keyValuePair = enumerator.Current;
					string value = keyValuePair.Value;
					XlsPivotCache a_ = this.ᜀ(A_0, value, out key);
					int a_2 = Convert.ToInt32(keyValuePair.Key);
					this.\u171B.PivotCaches.ᜀ(a_2, a_);
					this.ᜣ().Add(key, null);
					this.ᜦ.Remove(value);
					num = 3;
					continue;
				}
				case 2:
					goto IL_117;
				}
				IL_E8:
				num = 1;
				continue;
				goto IL_E8;
			}
			IL_117:;
		}
		if (true)
		{
		}
		this.\u173C.Clear();
	}

	// Token: 0x060048BD RID: 18621 RVA: 0x002C1734 File Offset: 0x002C0734
	internal XlsPivotCache ᜀ(string A_0, string A_1, out string A_2)
	{
		XmlReader a_2;
		string a_3;
		RelationsCollection relationsCollection;
		XlsPivotCache xlsPivotCache;
		for (;;)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					sprᦨ a_ = this.ᜦ[A_1];
					a_2 = this.ᜂ(a_, A_0, out A_2);
					a_3 = null;
					sprវ.ᜀ(A_2, out a_3);
					string a_4 = sprវ.ᜁ(A_2);
					relationsCollection = this.ᜇ(a_4);
					xlsPivotCache = new XlsPivotCache(this.\u171B.AppImplementation, this.\u171B);
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (relationsCollection == null)
							{
								num = 3;
								continue;
							}
							xlsPivotCache.HasCacheRecords = true;
							num = 1;
							continue;
						case 1:
							goto IL_B0;
						case 2:
							goto IL_CE;
						case 3:
							if (true)
							{
							}
							xlsPivotCache.HasCacheRecords = false;
							num = 2;
							continue;
						}
						break;
					}
				}
				IL_D0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_E6;
				}
				IL_B0:
				IL_CE:
				goto IL_D0;
			}
		}
		IL_E6:
		if (false)
		{
		}
		sprℳ.ᜀ(a_2, xlsPivotCache, this.\u171B, a_3, relationsCollection);
		return xlsPivotCache;
	}

	// Token: 0x060048BE RID: 18622 RVA: 0x002C1840 File Offset: 0x002C0840
	internal RelationsCollection ᜇ(string A_0)
	{
		RelationsCollection relationsCollection;
		for (;;)
		{
			relationsCollection = null;
			spr\u2570 spr_u = this.\u171A.ᜃ(A_0);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2B;
					default:
					{
						if (false)
						{
						}
						XmlReader a_ = sprវ.ᜀ(spr_u);
						relationsCollection = this.\u1718().ᜧ(a_);
						relationsCollection.ItemPath = A_0;
						num = 1;
						continue;
					}
					}
					break;
				case 1:
					return relationsCollection;
				case 2:
					goto IL_2B;
				}
				break;
				IL_2B:
				if (spr_u == null)
				{
					return relationsCollection;
				}
				if (true)
				{
				}
				num = 0;
			}
		}
		return relationsCollection;
	}

	// Token: 0x060048BF RID: 18623 RVA: 0x002C18DC File Offset: 0x002C08DC
	private string ᜄ(string A_0)
	{
		string text;
		for (;;)
		{
			if (true)
			{
			}
			text = this.ᜂ(A_0);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2C;
					default:
						if (false)
						{
						}
						text = this.ᜃ(A_0);
						num = 1;
						continue;
					}
					break;
				case 1:
					return text;
				case 2:
					goto IL_2C;
				}
				break;
				IL_2C:
				if (text != null)
				{
					return text;
				}
				num = 0;
			}
		}
		return text;
	}

	// Token: 0x060048C0 RID: 18624 RVA: 0x002C195C File Offset: 0x002C095C
	private string ᜃ(string A_0)
	{
		if (true)
		{
		}
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_3C;
		}
		if (false)
		{
		}
		switch (0)
		{
		default:
		{
			IL_3C:
			string result = null;
			IEnumerator<KeyValuePair<string, string>> enumerator = this.\u171D.GetEnumerator();
			try
			{
				int num = 16;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
					{
						if (!enumerator.MoveNext())
						{
							num = 8;
							continue;
						}
						KeyValuePair<string, string> keyValuePair = enumerator.Current;
						string value = keyValuePair.Value;
						num = 12;
						continue;
					}
					case 1:
						goto IL_255;
					case 2:
						goto IL_1AF;
					case 3:
						goto IL_249;
					case 4:
					{
						string text;
						if (text[0] != '/')
						{
							num = 17;
							continue;
						}
						goto IL_21F;
					}
					case 5:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 11;
							continue;
						}
						spr\u2570 spr_u = this.\u171A.ᜀ(num2);
						string text = spr_u.ᜇ();
						num = 4;
						continue;
					}
					case 6:
					{
						string text;
						if (!this.\u171E.ContainsKey(text))
						{
							num = 13;
							continue;
						}
						goto IL_12B;
					}
					case 7:
					{
						string text;
						string key;
						if (text.EndsWith(key))
						{
							num = 10;
							continue;
						}
						goto IL_12B;
					}
					case 8:
						goto IL_249;
					case 9:
						goto IL_21F;
					case 10:
						num = 6;
						continue;
					case 11:
						num = 3;
						continue;
					case 12:
					{
						string value;
						if (value == A_0)
						{
							num = 18;
							continue;
						}
						break;
					}
					case 13:
					{
						string text;
						result = text;
						num = 14;
						continue;
					}
					case 14:
						goto IL_249;
					case 15:
						goto IL_1AF;
					case 17:
					{
						string text = '/' + text;
						num = 9;
						continue;
					}
					case 18:
					{
						KeyValuePair<string, string> keyValuePair;
						string key = keyValuePair.Key;
						num2 = 0;
						int num3 = this.\u171A.ᜇ();
						num = 15;
						continue;
					}
					}
					goto IL_AB;
					IL_12B:
					num2++;
					num = 2;
					continue;
					IL_13F:
					num = 0;
					continue;
					IL_AB:
					goto IL_13F;
					IL_1AF:
					num = 5;
					continue;
					IL_21F:
					num = 7;
					continue;
					IL_249:
					num = 1;
				}
				IL_255:;
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_294;
					case 2:
						enumerator.Dispose();
						num = 1;
						continue;
					}
					if (enumerator == null)
					{
						break;
					}
					num = 2;
				}
				IL_294:;
			}
			return result;
		}
		}
	}

	// Token: 0x060048C1 RID: 18625 RVA: 0x002C1C20 File Offset: 0x002C0C20
	private string ᜂ(string A_0)
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
			switch (0)
			{
			}
			break;
		}
		string result = null;
		IEnumerator<KeyValuePair<string, string>> enumerator = this.\u171E.GetEnumerator();
		try
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_DB;
				case 1:
				{
					KeyValuePair<string, string> keyValuePair;
					result = keyValuePair.Key;
					num = 5;
					continue;
				}
				case 2:
				{
					if (!enumerator.MoveNext())
					{
						num = 6;
						continue;
					}
					KeyValuePair<string, string> keyValuePair = enumerator.Current;
					string value = keyValuePair.Value;
					num = 3;
					continue;
				}
				case 3:
				{
					string value;
					if (value == A_0)
					{
						num = 1;
						continue;
					}
					break;
				}
				case 5:
					goto IL_CF;
				case 6:
					goto IL_CF;
				}
				IL_A0:
				num = 2;
				continue;
				goto IL_A0;
				IL_CF:
				num = 0;
			}
			IL_DB:;
		}
		finally
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					enumerator.Dispose();
					num = 1;
					continue;
				case 1:
					goto IL_118;
				}
				if (enumerator == null)
				{
					break;
				}
				num = 0;
			}
			IL_118:;
		}
		if (true)
		{
		}
		return result;
	}

	// Token: 0x060048C2 RID: 18626 RVA: 0x002C1D64 File Offset: 0x002C0D64
	internal static string ᜁ(string A_0)
	{
		int a_ = 15;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_62:
			num = 3;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 1;
				break;
			}
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				if (A_0.Length == 0)
				{
					num = 2;
					continue;
				}
				goto IL_A5;
			case 2:
				goto IL_A3;
			case 3:
				num = 0;
				continue;
			}
			break;
		}
		if (A_0 != null)
		{
			goto IL_62;
		}
		IL_6D:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⱄ㍆ⱈ♊͌⹎㱐㙒", a_));
		IL_A3:
		goto IL_6D;
		IL_A5:
		string text2;
		string text = sprវ.ᜀ(A_0, out text2);
		return string.Concat(new object[]
		{
			text2,
			RecordTableEnumerator.b("ᩄ㕆ⱈ❊㹌", a_),
			'/',
			text,
			RecordTableEnumerator.b("歄㕆ⱈ❊㹌", a_)
		});
	}

	// Token: 0x060048C3 RID: 18627 RVA: 0x002C1E64 File Offset: 0x002C0E64
	internal static string ᜀ(string A_0, out string A_1)
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
		int num = A_0.LastIndexOf('/');
		A_1 = ((num >= 0) ? A_0.Substring(0, num + 1) : string.Empty);
		return A_0.Substring(num + 1);
	}

	// Token: 0x060048C4 RID: 18628 RVA: 0x002C1ECC File Offset: 0x002C0ECC
	internal Image ᜋ(string A_0)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 4;
			Image image;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (image.RawFormat.Equals(ImageFormat.Wmf))
					{
						num = 11;
						continue;
					}
					return image;
				case 1:
					goto IL_135;
				case 2:
				{
					MemoryStream memoryStream;
					this.\u1719.Add(A_0, memoryStream);
					num = 1;
					continue;
				}
				case 3:
				{
					spr\u2570 spr_u;
					MemoryStream memoryStream2 = (MemoryStream)spr_u.ᜐ();
					MemoryStream memoryStream = new MemoryStream((int)memoryStream2.Length);
					memoryStream2.WriteTo(memoryStream);
					memoryStream.Position = 0L;
					image = spr\u17FF.ᜀ(memoryStream);
					this.\u1738[A_0] = null;
					num = 12;
					continue;
				}
				case 5:
					goto IL_162;
				case 6:
					if (this.\u1719.ContainsKey(A_0))
					{
						return image;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_135;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 7:
				{
					if (A_0.Length == 0)
					{
						num = 5;
						continue;
					}
					image = null;
					spr\u2570 spr_u = this.\u171A.ᜃ(A_0);
					num = 10;
					continue;
				}
				case 8:
					if (true)
					{
					}
					num = 7;
					continue;
				case 9:
					num = 0;
					continue;
				case 10:
				{
					spr\u2570 spr_u;
					if (spr_u != null)
					{
						num = 3;
						continue;
					}
					return image;
				}
				case 11:
					goto IL_164;
				case 12:
					if (!image.RawFormat.Equals(ImageFormat.Emf))
					{
						num = 9;
						continue;
					}
					goto IL_164;
				}
				if (A_0 != null)
				{
					num = 8;
					continue;
				}
				break;
				IL_164:
				num = 6;
			}
			IL_108:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䠺䤼䴾݀㙂⥄⭆᥈⩊㥌❎", a_));
			IL_135:
			return image;
			IL_162:
			goto IL_108;
		}
		}
	}

	// Token: 0x060048C5 RID: 18629 RVA: 0x002C20C0 File Offset: 0x002C10C0
	private static XmlReader ᜀ(spr\u2570 A_0)
	{
		int a_ = 6;
		int num = 1;
		Stream stream;
		for (;;)
		{
			switch (num)
			{
			case 0:
				stream.Position = 0L;
				goto IL_42;
			case 2:
				goto IL_4A;
			case 3:
				goto IL_38;
			case 4:
				if (stream.CanSeek)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_42;
					}
					if (true)
					{
					}
					if (false)
					{
					}
					num = 0;
					continue;
				}
				goto IL_B2;
			}
			if (A_0 == null)
			{
				num = 3;
				continue;
			}
			stream = A_0.ᜐ();
			num = 4;
			continue;
			IL_42:
			num = 2;
		}
		IL_38:
		throw new ArgumentNullException(RecordTableEnumerator.b("唻䨽┿⽁", a_));
		IL_4A:
		IL_B2:
		return UtilityMethods.ᜀ(stream);
	}

	// Token: 0x060048C6 RID: 18630 RVA: 0x002C2188 File Offset: 0x002C1188
	internal XmlReader ᜁ(sprᦨ A_0, string A_1)
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
		string text;
		return this.ᜂ(A_0, A_1, out text);
	}

	// Token: 0x060048C7 RID: 18631 RVA: 0x002C21D0 File Offset: 0x002C11D0
	internal XmlReader ᜁ(sprᦨ A_0, string A_1, out string A_2)
	{
		int a_ = 5;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		spr\u2570 spr_u = this.ᜀ(A_0, A_1, out A_2);
		StreamReader streamReader = new StreamReader(spr_u.ᜐ());
		string text = streamReader.ReadToEnd();
		text = text.Replace(RecordTableEnumerator.b("ܺ弼䴾罀罂橄╆㭈畊", a_), RecordTableEnumerator.b("ܺ弼䴾湀終", a_));
		text = text.Replace(RecordTableEnumerator.b("ܺ弼䴾罀", a_), RecordTableEnumerator.b("ܺ弼䴾湀終", a_));
		byte[] bytes = Encoding.UTF8.GetBytes(text);
		MemoryStream a_2 = new MemoryStream(bytes);
		return UtilityMethods.ᜀ(a_2);
	}

	// Token: 0x060048C8 RID: 18632 RVA: 0x002C2294 File Offset: 0x002C1294
	internal XmlReader ᜂ(sprᦨ A_0, string A_1, out string A_2)
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
		spr\u2570 a_ = this.ᜀ(A_0, A_1, out A_2);
		return sprវ.ᜀ(a_);
	}

	// Token: 0x060048C9 RID: 18633 RVA: 0x002C22E0 File Offset: 0x002C12E0
	private spr\u2570 ᜀ(sprᦨ A_0, string A_1, out string A_2)
	{
		int a_ = 17;
		int num = 4;
		string text;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 == null)
				{
					goto IL_B8;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_55;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 1:
				goto IL_5D;
			case 2:
				goto IL_40;
			case 3:
				text = sprវ.ᜀ(A_1, text);
				text.Replace('\\', '/');
				goto IL_55;
			case 4:
				if (true)
				{
				}
				break;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			text = A_0.ᜂ();
			num = 0;
			continue;
			IL_55:
			num = 1;
		}
		IL_40:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕆ⱈ❊ⱌ㭎㡐㱒㭔", a_));
		IL_5D:
		IL_B8:
		spr\u2570 result = this.\u171A.ᜃ(text);
		A_2 = text;
		return result;
	}

	// Token: 0x060048CA RID: 18634 RVA: 0x002C23B8 File Offset: 0x002C13B8
	internal byte[] ᜀ(sprᦨ A_0, string A_1, bool A_2)
	{
		int a_ = 2;
		switch (0)
		{
		default:
		{
			byte[] array;
			for (;;)
			{
				IL_17:
				int num = 2;
				for (;;)
				{
					string text;
					switch (num)
					{
					case 0:
						this.\u171A.ᜀ(text);
						num = 3;
						continue;
					case 1:
						if (A_2)
						{
							num = 0;
							continue;
						}
						return array;
					case 3:
						goto IL_126;
					case 4:
						text = sprវ.ᜀ(A_1, text);
						text.Replace('\\', '/');
						num = 7;
						continue;
					case 5:
						if (A_1 != null)
						{
							num = 4;
							continue;
						}
						goto IL_76;
					case 6:
						goto IL_74;
					case 7:
						goto IL_76;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_17;
					default:
						if (false)
						{
						}
						if (A_0 == null)
						{
							num = 6;
							continue;
						}
						text = A_0.ᜂ();
						num = 5;
						continue;
					}
					IL_76:
					spr\u2570 spr_u = this.\u171A.ᜃ(text);
					Stream stream = spr_u.ᜐ();
					array = new byte[stream.Length];
					stream.Position = 0L;
					stream.Read(array, 0, (int)stream.Length);
					num = 1;
				}
			}
			IL_74:
			throw new ArgumentNullException(RecordTableEnumerator.b("䨷弹倻弽㐿⭁⭃⡅", a_));
			IL_126:
			if (true)
			{
			}
			return array;
		}
		}
	}

	// Token: 0x060048CB RID: 18635 RVA: 0x002C2520 File Offset: 0x002C1520
	internal void ᜈ(string A_0)
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
		sprᦨ a_ = this.ᜦ[A_0];
		string a_2;
		sprវ.ᜀ(this.ᜠ, out a_2);
		string text;
		XmlReader a_3 = this.ᜂ(a_, a_2, out text);
		string a_4 = sprវ.ᜁ(text);
		RelationsCollection a_5 = this.ᜇ(a_4);
		this.\u1718().ᜃ(a_3, a_5);
		this.\u1738.Add(text, null);
		this.ᜦ.Remove(A_0);
	}

	// Token: 0x060048CC RID: 18636 RVA: 0x002C25BC File Offset: 0x002C15BC
	internal static string ᜀ(string A_0, string A_1)
	{
		int a_ = 14;
		int num = 15;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1 != null)
				{
					num = 16;
					continue;
				}
				goto IL_16F;
			case 1:
				goto IL_1C5;
			case 2:
			{
				int num2;
				if (num2 >= 0)
				{
					num = 13;
					continue;
				}
				goto IL_F3;
			}
			case 3:
				goto IL_1C5;
			case 4:
				if (!A_1.StartsWith(RecordTableEnumerator.b("歃", a_)))
				{
					num = 5;
					continue;
				}
				goto IL_249;
			case 5:
				goto IL_122;
			case 6:
				goto IL_1C0;
			case 7:
				goto IL_F3;
			case 8:
				num = 10;
				continue;
			case 9:
				if (A_0[A_0.Length - 1] == '/')
				{
					num = 17;
					continue;
				}
				goto IL_1C5;
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_226;
				default:
					if (false)
					{
					}
					if (A_0.Length == 0)
					{
						num = 6;
						continue;
					}
					if (true)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 11:
				goto IL_244;
			case 12:
				if (A_1.Length == 0)
				{
					num = 11;
					continue;
				}
				num = 9;
				continue;
			case 13:
			{
				A_1 = A_1.Substring(3, A_1.Length - 3);
				int num2;
				A_0 = A_0.Substring(0, num2);
				num = 1;
				continue;
			}
			case 14:
			{
				if (!A_1.StartsWith(RecordTableEnumerator.b("橃桅杇", a_)))
				{
					num = 7;
					continue;
				}
				int num2 = A_0.LastIndexOf('/');
				num = 2;
				continue;
			}
			case 16:
				goto IL_226;
			case 17:
				A_0 = A_0.Substring(0, A_0.Length - 1);
				num = 3;
				continue;
			}
			if (A_0 != null)
			{
				num = 8;
				continue;
			}
			goto IL_124;
			IL_F3:
			num = 4;
			continue;
			IL_1C5:
			num = 14;
			continue;
			IL_226:
			num = 12;
		}
		IL_122:
		return A_0 + '/' + A_1;
		IL_124:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㝃㉅⥇㡉㡋ṍㅏ♑㱓", a_));
		IL_16F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⅃⡅ⱇᩉⵋ㩍㡏", a_));
		IL_1C0:
		goto IL_124;
		IL_244:
		goto IL_16F;
		IL_249:
		return UtilityMethods.ᜀ(A_1);
	}

	// Token: 0x060048CD RID: 18637 RVA: 0x002C2818 File Offset: 0x002C1818
	private void ᜌ()
	{
		int a_ = 0;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜊ();
		this.ᜀ(RecordTableEnumerator.b("洵笷唹刻䨽┿ⱁぃ᥅᱇㍉㱋⭍⍏ད穓⹕㕗㙙", a_));
	}

	// Token: 0x060048CE RID: 18638 RVA: 0x002C2878 File Offset: 0x002C1878
	private void ᜋ()
	{
		int a_ = 2;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		this.ᜀ(RecordTableEnumerator.b("尷唹弻渽㈿ⵁ㑃㕅杇⭉㱋㹍繏⩑㥓㩕", a_), RecordTableEnumerator.b("夷䨹䰻刽⤿⅁╃㉅ⅇ╉≋慍♏㱑こ硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű女᥵ṷᱹᕻᵽﶇ벑ﾙ瞧얟욡覣횥\udaa7얩\udcab쮭슯욱\uddb3펵쮷醹쒻펽겿", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹፻᡽첇ﮍﶏ望랗ꢙ겛꺝隟趡횣쎥쒧쮩\ud8ab잭\udfaf\udcb1잳\udeb5톷쪹쾻醽ꖿ뫁냃ꏅꛇ껉꧋꫍﷏ꋑꛓ맕꣗뿙껛ꫝ觟蟡韣", a_));
		this.ᜀ(RecordTableEnumerator.b("尷唹弻渽㈿ⵁ㑃㕅杇⥉⍋㱍㕏籑ⱓ㭕㑗", a_), RecordTableEnumerator.b("夷䨹䰻刽⤿⅁╃㉅ⅇ╉≋慍♏㱑こ硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű女ٵ᥷᥹᝻ώꪃꎍﮓﶗ얟톡辣\udea5얧용", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹౻ώꖉ뺋뺍ꂏ꒑뮓ﶗﶛ즟춡쪣향삧쎩\udcab\uddad龯\udfb1톳습\ud9b7\udeb9\uddbb쪽ꆿꟃ꧅뫇꿉뻍ꋏ뷑ꓓ돕꫗껙뗛믝鏟", a_));
		this.ᜀ(RecordTableEnumerator.b("尷唹弻渽㈿ⵁ㑃㕅杇⥉㥋㵍⑏㵑㥓硕⁗㝙せ", a_), RecordTableEnumerator.b("夷䨹䰻刽⤿⅁╃㉅ⅇ╉≋慍♏㱑こ硕㝗⩙㥛そᡟཡࡣeݧᡩū཭ѯű女᥵ṷᱹᕻᵽﶇ벑趟튡횣즥\ud8a7쾩\udeab\udaad\ud9afힱ잳鶵삷ힹ킻", a_), RecordTableEnumerator.b("倷丹䠻丽稿流歃㕅⭇≉⥋⍍ㅏ⅑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ山᭳ѵί啹፻᡽첇ﮍﶏ望랗ꢙ겛꺝隟趡횣쎥쒧쮩\ud8ab잭\udfaf\udcb1잳\udeb5톷쪹쾻醽ꎿ럁럃닅ꟇꟉ뻍ꋏ뷑ꓓ돕꫗껙뗛믝鏟", a_));
	}

	// Token: 0x060048CF RID: 18639 RVA: 0x002C294C File Offset: 0x002C194C
	private void ᜀ(string A_0, string A_1, string A_2)
	{
		int a_ = 1;
		if (true)
		{
		}
		string text;
		for (;;)
		{
			this.\u171E[RecordTableEnumerator.b("ᠶ", a_) + A_0] = A_1;
			this.\u171F.ᜀ(A_2, out text);
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A8;
					default:
						if (false)
						{
						}
						text = this.\u171F.GenerateRelationId();
						num = 2;
						continue;
					}
					break;
				case 1:
					if (text == null)
					{
						num = 0;
						continue;
					}
					goto IL_A8;
				case 2:
					goto IL_A6;
				}
				break;
			}
		}
		IL_A6:
		IL_A8:
		this.\u171F[text] = new sprᦨ(A_0, A_2);
		this.ᜀ(A_0);
	}

	// Token: 0x060048D0 RID: 18640 RVA: 0x002C2A1C File Offset: 0x002C1A1C
	private void ᜀ(string A_0)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			MemoryStream memoryStream;
			spr\u2570 spr_u;
			for (;;)
			{
				if (true)
				{
				}
				int num;
				XmlWriter xmlWriter;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_12F:
					num = 14;
					break;
				default:
				{
					if (false)
					{
					}
					memoryStream = new MemoryStream();
					StreamWriter a_2 = new StreamWriter(memoryStream);
					xmlWriter = UtilityMethods.ᜀ(a_2);
					num = 16;
					break;
				}
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_165;
					case 1:
						num = 10;
						continue;
					case 2:
						num = 13;
						continue;
					case 3:
						if (!(A_0 == RecordTableEnumerator.b("ᬿŁ⭃⡅㱇⽉≋㩍ཏّⵓ♕㵗⥙ś灝ᡟཡࡣ", a_)))
						{
							num = 12;
							continue;
						}
						this.\u170D().ᜀ(xmlWriter, this.\u171D, this.\u171E);
						num = 9;
						continue;
					case 4:
						goto IL_134;
					case 5:
						if (!(A_0 == RecordTableEnumerator.b("␿ⵁ❃ᙅ㩇╉㱋㵍罏㍑⑓♕癗≙ㅛ㉝", a_)))
						{
							num = 1;
							continue;
						}
						this.\u170D().\u1712(xmlWriter);
						num = 7;
						continue;
					case 6:
						if (spr_u != null)
						{
							num = 0;
							continue;
						}
						goto IL_27A;
					case 7:
						goto IL_134;
					case 8:
						goto IL_134;
					case 9:
						goto IL_134;
					case 10:
						if (!(A_0 == RecordTableEnumerator.b("␿ⵁ❃ᙅ㩇╉㱋㵍罏ㅑ㭓⑕㵗瑙⑛㍝౟", a_)))
						{
							num = 11;
							continue;
						}
						this.\u170D().ᜐ(xmlWriter);
						num = 8;
						continue;
					case 11:
						num = 3;
						continue;
					case 12:
						goto IL_12F;
					case 13:
						goto IL_1DC;
					case 14:
						if (!(A_0 == RecordTableEnumerator.b("␿ⵁ❃ᙅ㩇╉㱋㵍罏ㅑ⅓╕ⱗ㕙ㅛ灝ᡟཡࡣ", a_)))
						{
							num = 2;
							continue;
						}
						this.\u170D().ᜎ(xmlWriter);
						num = 4;
						continue;
					case 15:
						num = 5;
						continue;
					case 16:
						if (A_0 != null)
						{
							num = 15;
							continue;
						}
						goto IL_212;
					}
					break;
					IL_134:
					xmlWriter.Flush();
					spr_u = this.\u171A.ᜃ(A_0);
					num = 6;
				}
			}
			IL_165:
			spr_u.ᜁ(memoryStream, true);
			return;
			IL_1DC:
			IL_212:
			throw new ArgumentException(RecordTableEnumerator.b("㌿㙁㙃ཅ㱇⽉⅋ṍㅏ⁑⁓ᡕ㥗㝙㥛", a_));
			IL_27A:
			this.\u171A.ᜁ(A_0, memoryStream, true, FileAttributes.Archive);
			return;
		}
		}
	}

	// Token: 0x060048D1 RID: 18641 RVA: 0x002C2CB4 File Offset: 0x002C1CB4
	private void ᜊ()
	{
		int a_ = 4;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		this.\u171D[RecordTableEnumerator.b("䈹儻刽", a_)] = RecordTableEnumerator.b("嬹䰻丽ⰿ⭁❃❅㱇⍉⍋⁍罏⩑㥓㩕", a_);
		this.\u171D[RecordTableEnumerator.b("䠹夻刽㌿", a_)] = RecordTableEnumerator.b("嬹䰻丽ⰿ⭁❃❅㱇⍉⍋⁍罏⑑㩓㉕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ孵ࡷ᭹ύᕽꢅ慎ﮑﮓ펟覡\udca3쮥쒧", a_);
	}

	// Token: 0x060048D2 RID: 18642 RVA: 0x002C2D48 File Offset: 0x002C1D48
	private void ᜉ()
	{
		int a_ = 2;
		if (true)
		{
		}
		MemoryStream memoryStream;
		spr\u2570 spr_u;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			memoryStream = new MemoryStream();
			StreamWriter a_2 = new StreamWriter(memoryStream);
			XmlWriter xmlWriter = UtilityMethods.ᜀ(a_2);
			this.\u170D().ᜁ(xmlWriter, this.\u171F);
			xmlWriter.Flush();
			spr_u = this.\u171A.ᜃ(RecordTableEnumerator.b("朷䠹夻刽㌿流橃㑅ⵇ♉㽋", a_));
			if (spr_u == null)
			{
				this.\u171A.ᜁ(RecordTableEnumerator.b("朷䠹夻刽㌿流橃㑅ⵇ♉㽋", a_), memoryStream, true, FileAttributes.Archive);
				return;
			}
			break;
		}
		}
		spr_u.ᜁ(memoryStream, true);
	}

	// Token: 0x060048D3 RID: 18643 RVA: 0x002C2E00 File Offset: 0x002C1E00
	private static string ᜀ(int A_0)
	{
		int a_ = 7;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		return string.Format(RecordTableEnumerator.b("似瘾╀㡂畄㩆", a_), A_0);
	}

	// Token: 0x060048D4 RID: 18644 RVA: 0x002C2E60 File Offset: 0x002C1E60
	private void ᜁ(ExcelSaveType A_0)
	{
		int a_ = 17;
		for (;;)
		{
			this.ᜪ = this.ᜀ(A_0);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B4;
					default:
						if (false)
						{
						}
						this.\u171F = new RelationsCollection();
						this.\u171F[sprវ.ᜀ(1)] = new sprᦨ(this.ᜠ, RecordTableEnumerator.b("⽆㵈㽊㵌畎繐籒♔㑖ㅘ㹚ぜ㹞በ䵢੤ᝦ౨ժᕬɮᵰᕲᩴնᑸ᩺ॼ౾꾀Ꚉ펖쒠춢톤袦鮨鮪鶬馮麰솲킴\udbb6\ud8b8쾺풼킾꿀냂귄껆마룊ꃎ럐뗒볔듖볘鿚닜볞铠転胤触鷨", a_));
						num = 1;
						continue;
					}
					break;
				case 1:
					goto IL_B2;
				case 2:
					if (this.\u171F == null)
					{
						num = 0;
						continue;
					}
					goto IL_B4;
				}
				break;
			}
		}
		IL_B2:
		IL_B4:
		Dictionary<int, int> a_2 = this.ᜆ();
		Dictionary<XlsPivotCache, string> a_3 = this.ᜈ();
		this.ᜅ();
		this.\u1735 = this.ᜇ();
		this.ᜀ(a_2, a_3);
		this.\u1735 = null;
	}

	// Token: 0x060048D5 RID: 18645 RVA: 0x002C2F50 File Offset: 0x002C1F50
	private string ᜀ(ExcelSaveType A_0)
	{
		int a_ = 4;
		int num = 10;
		string result;
		for (;;)
		{
			string text;
			string text2;
			switch (num)
			{
			case 0:
				return result;
			case 1:
				num = 9;
				continue;
			case 2:
				return result;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_12D;
				default:
					if (false)
					{
					}
					text = RecordTableEnumerator.b("嬹䰻丽ⰿ⭁❃❅㱇⍉⍋⁍罏⑑㩓㉕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ孵᝷ᱹ᩻᝽ﾉﺏ몓鍊ﾝ쒟톡첣쎥춧\udea9솫슭麯욱톳\udbb5좷횹\uddbb쪽ꖿ꧃Ʂꇇ꓉뛍뷏뻑", a_);
					goto IL_B4;
				}
				break;
			case 4:
				num = 5;
				continue;
			case 5:
				text = RecordTableEnumerator.b("嬹䰻丽ⰿ⭁❃❅㱇⍉⍋⁍罏⑑㩓㉕癗㕙ⱛ㭝๟ᩡॣ੥๧թṫͭᅯٱݳ孵᝷ᱹ᩻᝽ﾉﺏ몓鍊ﾝ쒟톡첣쎥춧\udea9솫슭麯솱\udcb3펵\uddb7캹銻펽ꆿꯁ꫃냇Ꟊꃋ", a_);
				goto IL_B4;
			case 6:
				text2 = RecordTableEnumerator.b("嬹䰻丽ⰿ⭁❃❅㱇⍉⍋⁍罏⑑㩓㉕癗㝙⽛獝՟ᩡݣͥѧ䑩ᡫ୭ᵯɱᡳ᝵౷ό剻፽춇ﲏ뢕ﮙ讟\udaa1즣쪥", a_);
				goto IL_11F;
			case 7:
				num = 11;
				continue;
			case 8:
				if (A_0 != ExcelSaveType.SaveAsTemplate)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
			case 9:
				text2 = RecordTableEnumerator.b("嬹䰻丽ⰿ⭁❃❅㱇⍉⍋⁍罏⑑㩓㉕癗㝙⽛獝՟ᩡݣͥѧ䑩Ὣ٭ᕯ᝱s塵ᕷ᭹ύ౽잁뺏ﾑﾕ놙첟", a_);
				goto IL_11F;
			case 11:
				if (A_0 != ExcelSaveType.SaveAsTemplate)
				{
					num = 1;
					continue;
				}
				goto IL_12D;
			}
			if (this.\u171B.HasMacros)
			{
				num = 7;
				continue;
			}
			if (true)
			{
			}
			num = 8;
			continue;
			IL_B4:
			result = text;
			num = 0;
			continue;
			IL_11F:
			result = text2;
			num = 2;
			continue;
			IL_12D:
			num = 6;
		}
		return result;
	}

	// Token: 0x060048D6 RID: 18646 RVA: 0x002C30A8 File Offset: 0x002C20A8
	private Dictionary<XlsPivotCache, string> ᜈ()
	{
		switch (0)
		{
		default:
		{
			Dictionary<XlsPivotCache, string> dictionary;
			IEnumerator<XlsPivotCache> enumerator;
			for (;;)
			{
				dictionary = new Dictionary<XlsPivotCache, string>();
				XlsPivotCachesCollection xlsPivotCachesCollection = this.\u171B.PivotCaches;
				int num = 2;
				for (;;)
				{
					int num2;
					int num3;
					switch (num)
					{
					case 0:
						goto IL_BE;
					case 1:
						num2 = xlsPivotCachesCollection.Count;
						goto IL_79;
					case 2:
						if (xlsPivotCachesCollection == null)
						{
							num = 5;
							continue;
						}
						num = 1;
						continue;
					case 3:
						if (num3 > 0)
						{
							num = 4;
							continue;
						}
						return dictionary;
					case 4:
						enumerator = xlsPivotCachesCollection.GetEnumerator();
						num = 0;
						continue;
					case 5:
						num = 6;
						continue;
					case 6:
						num2 = 0;
						goto IL_79;
					}
					break;
					IL_79:
					num3 = num2;
					if (true)
					{
					}
					num = 3;
				}
			}
			IL_BE:
			try
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (!enumerator.MoveNext())
						{
							num = 2;
							continue;
						}
						XlsPivotCache xlsPivotCache = enumerator.Current;
						string value = this.ᜃ(xlsPivotCache);
						dictionary[xlsPivotCache] = value;
						num = 3;
						continue;
					}
					case 2:
						IL_144:
						num = 4;
						continue;
					case 4:
						goto IL_152;
					}
					IL_10D:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_144;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					goto IL_10D;
				}
				IL_152:;
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						enumerator.Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_191;
					}
					if (enumerator == null)
					{
						break;
					}
					num = 0;
				}
				IL_191:;
			}
			return dictionary;
		}
		}
	}

	// Token: 0x060048D7 RID: 18647 RVA: 0x002C325C File Offset: 0x002C225C
	private string ᜃ(XlsPivotCache A_0)
	{
		string a_;
		for (;;)
		{
			a_ = null;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6E;
				case 1:
					if (A_0.HasCacheRecords)
					{
						num = 2;
						continue;
					}
					goto IL_70;
				case 2:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_70;
					default:
						if (false)
						{
						}
						a_ = this.ᜂ(A_0);
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_6E:
		IL_70:
		return this.ᜀ(A_0, a_);
	}

	// Token: 0x060048D8 RID: 18648 RVA: 0x002C32E4 File Offset: 0x002C22E4
	private string ᜀ(XlsPivotCache A_0, string A_1)
	{
		int a_ = 19;
		switch (0)
		{
		default:
		{
			string text;
			RelationsCollection relationsCollection;
			string text2;
			for (;;)
			{
				text = this.ᜀ(A_0);
				relationsCollection = new RelationsCollection();
				text2 = null;
				int num = 0;
				for (;;)
				{
					IL_19:
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (A_1 != null)
						{
							num = 2;
							continue;
						}
						goto IL_8E;
					case 1:
						goto IL_82;
					case 2:
						text2 = relationsCollection.GenerateRelationId();
						relationsCollection[text2] = new sprᦨ('/' + A_1, RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊ朗\udd98ﺜ철욢쮤펦蚨馪鶬龮螰鲲잴튶햸\udaba즼횾껀귂뛄꿆ꃈ믊뻌ꇐ뫒ꏔ룖귘飚볜볞觠蛢럤苦諨蓪鿬诮苰", a_));
						num = 3;
						continue;
					case 3:
						goto IL_8E;
					case 4:
						while (A_0.PreservedExtenalRelation != null)
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
								num = 5;
								goto IL_19;
							}
						}
						goto IL_105;
					case 5:
						relationsCollection[A_0.RelationId] = A_0.PreservedExtenalRelation;
						num = 1;
						continue;
					}
					break;
					IL_8E:
					num = 4;
				}
			}
			IL_82:
			IL_105:
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter xmlWriter = UtilityMethods.ᜀ(memoryStream, Encoding.UTF8);
			spr\u2171.ᜀ(xmlWriter, A_0, this.\u171B, text2, relationsCollection);
			xmlWriter.Flush();
			this.ᜃ(text, RecordTableEnumerator.b("⡈㭊㵌⍎㡐げ㑔⍖じ㑚㍜灞ᝠൢŤ䥦٨᭪࡬Ů॰ṲᥴᅶᙸॺၼṾꢄ杖햠趢횤힦\udba8캪첬쮮슰\udbb2킴튶춸횺톼醾뇀ꫂ도꣆뷈裊곌곎말뛒釔닖뿘닚돜뛞闠諢諤触싨鏪胬菮", a_));
			this.\u171A.ᜀ(text, memoryStream, true, FileAttributes.Archive);
			this.ᜀ(text, relationsCollection);
			return text;
		}
		}
	}

	// Token: 0x060048D9 RID: 18649 RVA: 0x002C3450 File Offset: 0x002C2450
	private string ᜂ(XlsPivotCache A_0)
	{
		int a_ = 8;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		string text = this.ᜁ(A_0);
		MemoryStream memoryStream = new MemoryStream();
		XmlWriter xmlWriter = UtilityMethods.ᜀ(memoryStream, Encoding.UTF8);
		spr\u2171.ᜀ(xmlWriter, A_0, memoryStream);
		xmlWriter.Flush();
		this.\u171A.ᜀ(text, memoryStream, true, FileAttributes.Archive);
		this.ᜃ(text, RecordTableEnumerator.b("弽〿㉁⡃⽅⭇⭉㡋❍㽏㱑筓⁕㙗㹙牛ㅝၟݡ੣ṥէ٩੫ŭɯάᕳɵ୷坹፻᡽ﮍﶏ望뚗얟쎡삣향삧쾩즫\udaad\uddaf\udeb1骳욵톷첹펻쪽莿ꏁꟃ껅귇飉꧋귍뿏ꃑ냓ꗕꋙ뇛닝", a_));
		return text;
	}

	// Token: 0x060048DA RID: 18650 RVA: 0x002C34E4 File Offset: 0x002C24E4
	private string ᜁ(XlsPivotCache A_0)
	{
		int a_ = 12;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		int num = 0;
		return this.ᜀ(ref num, RecordTableEnumerator.b("㩁⡃楅㡇⍉㩋⅍⑏ᅑ㕓㕕し㽙獛⹝य़ᑡୣብ⭧୩ཫ٭ᕯⁱᅳᕵ᝷ࡹ᡻ൽﭿ늁旅ꢅ", a_));
	}

	// Token: 0x060048DB RID: 18651 RVA: 0x002C3540 File Offset: 0x002C2540
	private string ᜀ(XlsPivotCache A_0)
	{
		int a_ = 12;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		int num = 0;
		return this.ᜀ(ref num, RecordTableEnumerator.b("㩁⡃楅㡇⍉㩋⅍⑏ᅑ㕓㕕し㽙獛⹝य़ᑡୣብ⭧୩ཫ٭ᕯ㙱ᅳၵᅷᑹᕻ੽ﶅ뢇ꊋﶏﺑ", a_));
	}

	// Token: 0x060048DC RID: 18652 RVA: 0x002C359C File Offset: 0x002C259C
	private string[] ᜇ()
	{
		switch (0)
		{
		default:
		{
			string[] array;
			for (;;)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
					{
						if (false)
						{
						}
						XlsWorkbookShapeData shapesData = this.\u171B.ShapesData;
						List<sprᜪ> list = shapesData.Pictures;
						int num = 15;
						for (;;)
						{
							int num2;
							List<int> list2;
							int num5;
							int num6;
							switch (num)
							{
							case 0:
								num2 = 0;
								goto IL_1A7;
							case 1:
								return array;
							case 2:
							{
								int num3;
								int count;
								if (num3 >= count)
								{
									num = 1;
									continue;
								}
								int num4 = list2[num3];
								sprᜪ a_ = list[num4];
								array[num4] = this.ᜀ(a_);
								num3++;
								num = 3;
								continue;
							}
							case 3:
								goto IL_16A;
							case 4:
								goto IL_B0;
							case 5:
								goto IL_1CD;
							case 6:
								num2 = list.Count;
								goto IL_1A7;
							case 7:
								goto IL_1CD;
							case 8:
							{
								sprᜪ sprᜪ;
								array[num5] = this.ᜀ(sprᜪ);
								num = 7;
								continue;
							}
							case 9:
								num = 0;
								continue;
							case 10:
								goto IL_16A;
							case 11:
							{
								sprᜪ sprᜪ;
								if (sprᜪ.ᜈ() != null)
								{
									num = 8;
									continue;
								}
								list2.Add(num5);
								num = 5;
								continue;
							}
							case 12:
							{
								if (num5 >= num6)
								{
									num = 14;
									continue;
								}
								sprᜪ sprᜪ = list[num5];
								num = 11;
								continue;
							}
							case 13:
								goto IL_B0;
							case 14:
							{
								if (true)
								{
								}
								int num3 = 0;
								int count = list2.Count;
								num = 10;
								continue;
							}
							case 15:
								if (list == null)
								{
									num = 9;
									continue;
								}
								num = 6;
								continue;
							}
							break;
							IL_B0:
							num = 12;
							continue;
							IL_16A:
							num = 2;
							continue;
							IL_1A7:
							int num7 = num2;
							array = new string[num7];
							list2 = new List<int>();
							num5 = 0;
							num6 = num7;
							num = 13;
							continue;
							IL_1CD:
							num5++;
							num = 4;
						}
						break;
					}
					}
				}
			}
			return array;
		}
		}
	}

	// Token: 0x060048DD RID: 18653 RVA: 0x002C37C4 File Offset: 0x002C27C4
	private string ᜀ(sprᜪ A_0)
	{
		int a_ = 2;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_34;
			case 2:
				goto IL_7E;
			case 3:
				if (A_0.ᜉ() == MsoBlipType.msoblipERROR)
				{
					num = 2;
					continue;
				}
				goto IL_94;
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_7E;
				}
				if (false)
				{
				}
				num = 3;
			}
		}
		IL_34:
		throw new ArgumentNullException(RecordTableEnumerator.b("娷䤹夻", a_));
		IL_7E:
		if (true)
		{
		}
		return null;
		IL_94:
		return this.ᜀ(A_0.ᜄ().ᜀ(), A_0.ᜈ());
	}

	// Token: 0x060048DE RID: 18654 RVA: 0x002C387C File Offset: 0x002C287C
	private Dictionary<int, int> ᜆ()
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			Dictionary<int, int> dictionary;
			for (;;)
			{
				spr\u23DF spr_u23DF = new spr\u23DF();
				this.\u171E[spr_u23DF.ᜀ(this.ᜢ)] = RecordTableEnumerator.b("♆㥈㭊⅌♎㉐㉒⅔㹖㙘㕚牜⥞འݢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾꺂ﲒﺚ辠킢햤햦첨쪪즬\udcae\ud9b0횲킴쎶풸ힺ鎼첾뗀뫂꧄ꋆ뫈뗌ꋎ뷐", a_);
				MemoryStream memoryStream = new MemoryStream();
				StreamWriter a_2 = new StreamWriter(memoryStream);
				XmlWriter xmlWriter = UtilityMethods.ᜀ(a_2);
				dictionary = this.\u170D().ᜀ(xmlWriter, ref this.ᜭ);
				xmlWriter.Flush();
				string a_3 = this.ᜢ;
				int num = 2;
				for (;;)
				{
					IL_19:
					switch (num)
					{
					case 0:
						return dictionary;
					case 1:
						goto IL_F4;
					case 2:
						if (true)
						{
						}
						if (this.ᜢ[0] == '/')
						{
							num = 3;
							continue;
						}
						goto IL_F4;
					case 3:
						a_3 = UtilityMethods.ᜀ(this.ᜢ);
						num = 1;
						continue;
					case 4:
						this.\u171A.ᜀ(a_3, memoryStream, true, FileAttributes.Archive);
						num = 0;
						continue;
					case 5:
						while (dictionary.Count > 0)
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
								num = 4;
								goto IL_19;
							}
						}
						return dictionary;
					}
					break;
					IL_F4:
					num = 5;
				}
			}
			return dictionary;
		}
		}
	}

	// Token: 0x060048DF RID: 18655 RVA: 0x002C39DC File Offset: 0x002C29DC
	private void ᜅ()
	{
		int a_ = 11;
		switch (0)
		{
		default:
		{
			spr\u2570 spr_u;
			spr\u1F5E spr_u1F5E;
			for (;;)
			{
				SSTDictionary sstdictionary = this.\u171B.InnerSST;
				int activeCount = sstdictionary.ActiveCount;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_F5;
					case 1:
						goto IL_6E;
					case 2:
						IL_A5:
						if (spr_u == null)
						{
							num = 6;
							continue;
						}
						goto IL_1B1;
					case 3:
						if (true)
						{
						}
						if (this.ᜡ[0] == '/')
						{
							num = 5;
							continue;
						}
						goto IL_6E;
					case 4:
					{
						if (activeCount <= 0)
						{
							num = 7;
							continue;
						}
						spr\u23DF spr_u23DF = new spr\u23DF();
						this.\u171E[spr_u23DF.ᜀ(this.ᜡ)] = RecordTableEnumerator.b("⁀㍂㕄⭆⁈⡊ⱌ㭎㡐㱒㭔硖⽘㕚㥜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ偼ၾﺒ練떚펠욢쒤쎦\udaa8쎪좬쪮얰\udeb2\ud9b4馶쪸펺\udcbc춾꓀Ꟃ雄돆믈ꋊꏌ꣎ꋐ귔뫖뗘", a_);
						spr_u1F5E = new spr\u1F5E(new spr\u249E.ᜀ(this.\u171B.AppImplementation.ᜂ));
						StreamWriter a_2 = new StreamWriter(spr_u1F5E);
						XmlWriter xmlWriter = UtilityMethods.ᜀ(a_2);
						this.\u170D().ᜏ(xmlWriter);
						xmlWriter.Flush();
						string a_3 = this.ᜡ;
						num = 3;
						continue;
					}
					case 5:
					{
						string a_3 = UtilityMethods.ᜀ(this.ᜡ);
						num = 1;
						continue;
					}
					case 6:
					{
						string a_3;
						spr_u = this.\u171A.ᜁ(a_3, null, false, FileAttributes.Archive);
						num = 0;
						continue;
					}
					case 7:
						return;
					}
					break;
					IL_6E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A5;
					default:
					{
						if (false)
						{
						}
						string a_3;
						spr_u = this.\u171A.ᜃ(a_3);
						num = 2;
						break;
					}
					}
				}
			}
			return;
			IL_F5:
			IL_1B1:
			spr_u.ᜀ(spr_u1F5E);
			return;
		}
		}
	}

	// Token: 0x060048E0 RID: 18656 RVA: 0x002C3BA4 File Offset: 0x002C2BA4
	private void ᜀ(Dictionary<int, int> A_0, Dictionary<XlsPivotCache, string> A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u23DF spr_u23DF = new spr\u23DF();
				this.\u171E[spr_u23DF.ᜀ(this.ᜠ)] = this.ᜪ;
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (this.\u171B.InnerSST.ActiveCount > 0)
						{
							num = 3;
							continue;
						}
						goto IL_1DD;
					case 1:
						if (this.ᜦ != null)
						{
							goto IL_DD;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9E;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						this.ᜦ = new RelationsCollection();
						num = 4;
						continue;
					case 3:
						goto IL_9E;
					case 4:
						goto IL_DD;
					case 5:
						goto IL_D8;
					}
					break;
					IL_9E:
					string a_2;
					this.ᜨ = this.ᜀ(this.ᜦ, this.ᜡ, a_2, RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺ቼ᥾춈搜ﲐﮔ뚘ꦚ궜꾞鞠貢힤슦얨쪪\ud9ac욮\udeb0\uddb2운\udfb6킸쮺캼邾닀ꯂ꓄뗆곈꿊黌믎ꏐ뫒믔냖꫘", a_), this.ᜨ);
					num = 5;
					continue;
					IL_DD:
					this.ᜀ(this.ᜦ, this.ᜠ, A_0, A_1);
					Stream stream = new MemoryStream();
					StreamWriter a_3 = new StreamWriter(stream);
					XmlWriter xmlWriter = UtilityMethods.ᜀ(a_3);
					this.\u170D().ᜀ(xmlWriter, this.ᜬ, this.ᜫ, this.\u1737, this.ᜦ, A_1, this.\u1739);
					xmlWriter.Flush();
					this.\u171A.ᜀ(this.ᜠ, stream, true, FileAttributes.Archive);
					sprវ.ᜀ(this.ᜠ, out a_2);
					this.ᜧ = this.ᜀ(this.ᜦ, this.ᜢ, a_2, RecordTableEnumerator.b("儸伺䤼伾筀求橄㑆⩈⍊⡌≎ぐ⁒答㡖⥘㹚㍜❞ౠརͤࡦ᭨٪౬᭮ɰ嵲ᩴնṸ呺ቼ᥾춈搜ﲐﮔ뚘ꦚ궜꾞鞠貢힤슦얨쪪\ud9ac욮\udeb0\uddb2운\udfb6킸쮺캼邾닀럂별ꯆ곈룊", a_), this.ᜧ);
					num = 0;
				}
			}
			IL_D8:
			IL_1DD:
			this.ᜀ(this.ᜠ, this.ᜦ);
			return;
		}
	}

	// Token: 0x060048E1 RID: 18657 RVA: 0x002C3DA0 File Offset: 0x002C2DA0
	private string ᜀ(RelationsCollection A_0, string A_1, string A_2, string A_3, string A_4)
	{
		int num = 4;
		for (;;)
		{
			sprᦨ sprᦨ;
			switch (num)
			{
			case 0:
				A_1 = A_1.Substring(A_2.Length);
				goto IL_71;
			case 1:
				if (A_1.StartsWith(A_2))
				{
					num = 0;
					continue;
				}
				goto IL_D1;
			case 2:
				goto IL_B0;
			case 3:
				goto IL_D1;
			case 5:
				A_1 = UtilityMethods.ᜀ(A_1);
				num = 2;
				continue;
			case 6:
				return A_4;
			case 7:
				if (A_4 == null)
				{
					A_4 = A_0.ᜀ(sprᦨ);
					num = 8;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_71;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 9;
					continue;
				}
				break;
			case 8:
				return A_4;
			case 9:
				A_0[A_4] = sprᦨ;
				num = 6;
				continue;
			}
			if (A_1[0] == '/')
			{
				num = 5;
				continue;
			}
			goto IL_B0;
			IL_71:
			num = 3;
			continue;
			IL_B0:
			num = 1;
			continue;
			IL_D1:
			sprᦨ = new sprᦨ(A_1, A_3);
			num = 7;
		}
		return A_4;
	}

	// Token: 0x060048E2 RID: 18658 RVA: 0x002C3ECC File Offset: 0x002C2ECC
	internal void ᜀ(string A_0, RelationsCollection A_1)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Length == 0)
					{
						goto IL_99;
					}
					goto IL_F8;
				case 1:
					if (true)
					{
					}
					num = 0;
					continue;
				case 2:
					goto IL_A2;
				case 4:
					goto IL_E1;
				case 5:
					if (A_1.Count == 0)
					{
						num = 4;
						continue;
					}
					num = 6;
					continue;
				case 6:
					if (A_0 != null)
					{
						num = 1;
						continue;
					}
					goto IL_E3;
				case 7:
					num = 5;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_99:
					num = 2;
					break;
				default:
					if (false)
					{
					}
					if (A_1 == null)
					{
						return;
					}
					num = 7;
					break;
				}
			}
			IL_A2:
			goto IL_E3;
			IL_E1:
			return;
			IL_E3:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䬺尼䴾⑀ⵂㅄᝆ⡈㥊㥌Ŏぐ㹒ご", a_));
			IL_F8:
			string a_2 = sprវ.ᜁ(A_0);
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter a_3 = new StreamWriter(memoryStream);
			XmlWriter xmlWriter = UtilityMethods.ᜀ(a_3);
			this.\u170D().ᜁ(xmlWriter, A_1);
			xmlWriter.Flush();
			this.\u171A.ᜀ(a_2, memoryStream, true, FileAttributes.Archive);
			return;
		}
		}
	}

	// Token: 0x060048E3 RID: 18659 RVA: 0x002C4010 File Offset: 0x002C3010
	private void ᜀ(RelationsCollection A_0, string A_1, Dictionary<int, int> A_2, Dictionary<XlsPivotCache, string> A_3)
	{
		int a_ = 5;
		switch (0)
		{
		default:
			for (;;)
			{
				if (true)
				{
				}
				string a_2;
				sprវ.ᜀ(A_1, out a_2);
				XlsWorkbookObjectsCollection objects = this.\u171B.Objects;
				this.ᜀ(objects, A_0);
				int num = 0;
				int count = objects.Count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
				{
					if (false)
					{
					}
					int num2 = 6;
					for (;;)
					{
						string text;
						XlsWorksheetBase xlsWorksheetBase;
						switch (num2)
						{
						case 0:
							text = RecordTableEnumerator.b("䌺儼ှ㙀ⱂ㝄ⱆ㩈⍊⡌⩎═⁒穔⑖ㅘ㹚㡜⭞᩠卢ᡤ䥦ᅨ٪Ŭ", a_);
							goto IL_12C;
						case 1:
							num2 = 5;
							continue;
						case 2:
							if (num >= count)
							{
								num2 = 3;
								continue;
							}
							xlsWorksheetBase = (XlsWorksheetBase)objects[num];
							num2 = 7;
							continue;
						case 3:
							return;
						case 4:
							goto IL_A2;
						case 5:
							text = RecordTableEnumerator.b("䌺儼ှ≀⭂⑄㕆㵈㡊╌⩎㑐❒♔硖⩘㍚㡜㩞ᕠᡢ啤ᩦ䝨፪lͮ", a_);
							goto IL_12C;
						case 6:
							goto IL_A2;
						case 7:
							if (!(xlsWorksheetBase is XlsWorksheet))
							{
								num2 = 1;
								continue;
							}
							num2 = 0;
							continue;
						}
						break;
						IL_A2:
						num2 = 2;
						continue;
						IL_12C:
						string format = text;
						string a_3 = string.Format(format, xlsWorksheetBase.Index + 1);
						this.ᜀ(xlsWorksheetBase, a_3, A_0, a_2, A_2, A_3);
						num++;
						num2 = 4;
					}
					break;
				}
				}
			}
			return;
		}
	}

	// Token: 0x060048E4 RID: 18660 RVA: 0x002C4188 File Offset: 0x002C3188
	private void ᜀ(XlsWorkbookObjectsCollection A_0, RelationsCollection A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int count = A_0.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						string text;
						if (text != null)
						{
							num2 = 4;
							continue;
						}
						goto IL_52;
					}
					case 1:
					{
						if (num >= count)
						{
							num2 = 7;
							continue;
						}
						if (true)
						{
						}
						XlsWorksheetBase xlsWorksheetBase = (XlsWorksheetBase)A_0[num];
						sprᡟ sprᡟ = xlsWorksheetBase.DataHolder;
						num2 = 5;
						continue;
					}
					case 2:
						goto IL_D4;
					case 3:
						goto IL_D4;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
						{
							if (false)
							{
							}
							string text;
							A_1[text] = null;
							num2 = 8;
							continue;
						}
						}
						break;
					case 5:
					{
						sprᡟ sprᡟ;
						if (sprᡟ != null)
						{
							num2 = 6;
							continue;
						}
						goto IL_52;
					}
					case 6:
					{
						sprᡟ sprᡟ;
						string text = sprᡟ.ᜌ();
						num2 = 0;
						continue;
					}
					case 7:
						return;
					case 8:
						goto IL_52;
					}
					break;
					IL_52:
					num++;
					num2 = 3;
					continue;
					IL_D4:
					num2 = 1;
				}
			}
			return;
		}
	}

	// Token: 0x060048E5 RID: 18661 RVA: 0x002C42B4 File Offset: 0x002C32B4
	private void ᜀ(XlsWorksheetBase A_0, string A_1, RelationsCollection A_2, string A_3, Dictionary<int, int> A_4, Dictionary<XlsPivotCache, string> A_5)
	{
		int a_ = 16;
		int num = 5;
		string a_2;
		string text;
		for (;;)
		{
			switch (num)
			{
			case 0:
				a_2 = RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇ憐튕蓮얟첡킣覥骧骩鲫颭龯삱톳\udab5\ud9b7캹햻톽꺿뇁곃꿅룇막귍룏돑ꛓꋕꯗ닙맛믝铟", a_);
				this.ᜀ((XlsChart)A_0, A_1);
				num = 15;
				continue;
			case 1:
				if (text == null)
				{
					num = 8;
					continue;
				}
				goto IL_27A;
			case 2:
				goto IL_81;
			case 3:
				if (A_2 == null)
				{
					num = 7;
					continue;
				}
				a_2 = null;
				num = 19;
				continue;
			case 4:
				if (A_0 is XlsChart)
				{
					num = 0;
					continue;
				}
				goto IL_13F;
			case 6:
				goto IL_17F;
			case 7:
				goto IL_250;
			case 8:
				text = A_2.GenerateRelationId();
				A_0.ᜠ.ᜂ(text);
				num = 6;
				continue;
			case 9:
				if (A_1 != null)
				{
					num = 16;
					continue;
				}
				goto IL_1A9;
			case 10:
				goto IL_100;
			case 11:
				A_1 = A_1.Substring(A_3.Length);
				num = 18;
				continue;
			case 12:
				if (A_1.Length == 0)
				{
					num = 10;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_199;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 13:
				a_2 = RecordTableEnumerator.b("⹅㱇㹉㱋瑍罏絑❓㕕し㽙ㅛ㽝፟䱡ୣᙥ൧ѩᑫͭᱯᑱ᭳ѵᕷ᭹ࡻൽ깿ꞇ憐튕蓮얟첡킣覥骧骩鲫颭龯삱톳\udab5\ud9b7캹햻톽꺿뇁곃꿅룇막맍뿏ꃑ뿓ꗕ냗뿙맛ꫝ", a_);
				this.ᜀ((XlsWorksheet)A_0, A_1, A_4, A_5);
				num = 17;
				continue;
			case 14:
				if (A_1.StartsWith(A_3))
				{
					num = 11;
					continue;
				}
				goto IL_1BD;
			case 15:
				goto IL_13F;
			case 16:
				num = 12;
				continue;
			case 17:
				goto IL_13F;
			case 18:
				goto IL_1BD;
			case 19:
				if (A_0 is XlsWorksheet)
				{
					goto IL_199;
				}
				num = 4;
				continue;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 9;
			continue;
			IL_13F:
			num = 14;
			continue;
			IL_199:
			num = 13;
			continue;
			IL_1BD:
			text = A_0.ᜠ.ᜌ();
			num = 1;
		}
		IL_81:
		throw new ArgumentNullException(RecordTableEnumerator.b("㕅⁇⽉⥋㩍", a_));
		IL_100:
		goto IL_1A9;
		IL_17F:
		goto IL_27A;
		IL_1A9:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⽅㱇⽉⅋Mㅏ㽑ㅓ", a_));
		IL_250:
		throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ♉ⵋ㩍㥏㵑㩓╕", a_));
		IL_27A:
		sprᦨ a_3 = new sprᦨ(A_1, a_2);
		A_2[text] = a_3;
	}

	// Token: 0x060048E6 RID: 18662 RVA: 0x002C454C File Offset: 0x002C354C
	private void ᜀ(XlsWorksheet A_0, string A_1, Dictionary<int, int> A_2, Dictionary<XlsPivotCache, string> A_3)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				bool flag;
				string key;
				switch (num)
				{
				case 1:
					num = 11;
					continue;
				case 2:
					goto IL_9C;
				case 3:
					goto IL_34F;
				case 4:
					if (A_1[0] != '/')
					{
						num = 22;
						continue;
					}
					goto IL_2CE;
				case 5:
					goto IL_1DE;
				case 6:
				{
					this.\u171A.ᜀ(A_1);
					spr\u2570 a_2 = this.\u171A.ᜁ(A_1, null, false, FileAttributes.Archive);
					A_0.ᜠ = new sprᡟ(this, a_2);
					num = 3;
					continue;
				}
				case 7:
					num = 20;
					continue;
				case 8:
				{
					if (flag)
					{
						num = 6;
						continue;
					}
					spr\u2570 spr_u = A_0.ᜠ.ᜉ();
					num = 21;
					continue;
				}
				case 9:
					this.\u171A.ᜀ(A_1, null, false, FileAttributes.Archive);
					num = 13;
					continue;
				case 10:
					goto IL_2C9;
				case 11:
					if (A_1.Length == 0)
					{
						num = 10;
						continue;
					}
					key = A_1;
					if (true)
					{
					}
					num = 4;
					continue;
				case 12:
					if (A_0.IsSaved)
					{
						num = 7;
						continue;
					}
					goto IL_D0;
				case 13:
					goto IL_244;
				case 14:
				{
					spr\u2570 spr_u;
					if (spr_u.ᜇ() != A_1)
					{
						num = 16;
						continue;
					}
					goto IL_351;
				}
				case 15:
					goto IL_244;
				case 16:
					goto IL_215;
				case 17:
					if (this.\u171A.ᜆ(A_1) >= 0)
					{
						num = 9;
						continue;
					}
					for (;;)
					{
						this.\u171A.ᜁ(A_1, null, false, FileAttributes.Archive);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_1A4;
						}
					}
					IL_1A4:
					if (false)
					{
					}
					num = 15;
					continue;
				case 18:
					num = 14;
					continue;
				case 19:
					if (A_1 != null)
					{
						num = 1;
						continue;
					}
					goto IL_FA;
				case 20:
					if (A_0.ᜠ != null)
					{
						num = 5;
						continue;
					}
					goto IL_D0;
				case 21:
				{
					spr\u2570 spr_u;
					if (spr_u != null)
					{
						num = 18;
						continue;
					}
					goto IL_215;
				}
				case 22:
					key = '/' + A_1;
					num = 24;
					continue;
				case 23:
					goto IL_267;
				case 24:
					goto IL_2CE;
				}
				if (A_0 == null)
				{
					num = 2;
					continue;
				}
				num = 19;
				continue;
				IL_D0:
				flag = (A_0.ᜠ == null);
				num = 8;
				continue;
				IL_215:
				num = 17;
				continue;
				IL_244:
				A_0.ᜠ.ᜀ(this.\u171A.ᜃ(A_1));
				num = 23;
				continue;
				IL_2CE:
				this.\u171E[key] = RecordTableEnumerator.b("圵䠷䨹倻圽⌿⍁ぃ⽅❇⑉捋㡍㹏㙑穓㥕⡗㽙㉛♝ൟ๡ɣ॥ᩧݩ൫ᩭͯ影᭳ၵṷ፹ύ᭽揄뺏ﶗﮙ좟잡솣튥얧용芫\ud9ad\udfaf삱\udfb3억킷\udfb9\ud9bb쪽뫁꧃꫅", a_);
				num = 12;
			}
			IL_9C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵倷弹夻䨽", a_));
			IL_FA:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("張䰷弹儻瀽ℿ⽁⅃", a_));
			IL_1DE:
			this.ᜀ(A_0, A_1);
			return;
			IL_267:
			goto IL_351;
			IL_2C9:
			goto IL_FA;
			IL_34F:
			IL_351:
			A_0.ᜠ.ᜀ(A_0, A_2, A_3);
			return;
		}
		}
	}

	// Token: 0x060048E7 RID: 18663 RVA: 0x002C48BC File Offset: 0x002C38BC
	private void ᜀ(XlsChart A_0, string A_1)
	{
		int a_ = 11;
		int num = 13;
		for (;;)
		{
			string key;
			bool flag;
			switch (num)
			{
			case 0:
				goto IL_1B7;
			case 1:
				if (A_0.ᜠ != null)
				{
					num = 5;
					continue;
				}
				goto IL_DE;
			case 2:
				if (A_1[0] != '/')
				{
					num = 11;
					continue;
				}
				goto IL_1B7;
			case 3:
				if (A_0.IsSaved)
				{
					num = 7;
					continue;
				}
				goto IL_DE;
			case 4:
				goto IL_D9;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D9;
				default:
					goto IL_97;
				}
				break;
			case 6:
			{
				this.\u171A.ᜀ(A_1);
				spr\u2570 a_2 = this.\u171A.ᜁ(A_1, null, false, FileAttributes.Archive);
				A_0.ᜠ = new sprᡟ(this, a_2);
				num = 9;
				continue;
			}
			case 7:
				num = 1;
				continue;
			case 8:
				if (A_1.Length == 0)
				{
					num = 15;
					continue;
				}
				key = A_1;
				num = 2;
				continue;
			case 9:
				goto IL_158;
			case 10:
				if (A_1 != null)
				{
					num = 4;
					continue;
				}
				goto IL_1F7;
			case 11:
				if (true)
				{
				}
				key = '/' + A_1;
				num = 0;
				continue;
			case 12:
				if (flag)
				{
					num = 6;
					continue;
				}
				goto IL_20B;
			case 14:
				goto IL_64;
			case 15:
				goto IL_1B5;
			}
			if (A_0 == null)
			{
				num = 14;
				continue;
			}
			num = 10;
			continue;
			IL_D9:
			num = 8;
			continue;
			IL_DE:
			flag = (A_0.ᜠ == null);
			num = 12;
			continue;
			IL_1B7:
			this.\u171E[key] = RecordTableEnumerator.b("⁀㍂㕄⭆⁈⡊ⱌ㭎㡐㱒㭔硖⽘㕚㥜煞๠።d०ᅨ٪Ŭ८ṰŲᡴᙶ൸ࡺ偼ၾﺒ練떚펠욢쒤쎦\udaa8쎪좬쪮얰\udeb2\ud9b4馶\udab8펺\udcbc춾뗀냂귄ꋆ곈뿊럎볐뿒", a_);
			num = 3;
		}
		IL_64:
		throw new ArgumentNullException(RecordTableEnumerator.b("≀⭂⑄㕆㵈", a_));
		IL_97:
		if (false)
		{
		}
		this.ᜀ(A_0, A_1);
		return;
		IL_158:
		goto IL_20B;
		IL_1B5:
		IL_1F7:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⡀㝂⁄⩆݈⩊⁌⩎", a_));
		IL_20B:
		A_0.ᜠ.ᜂ(A_0);
	}

	// Token: 0x060048E8 RID: 18664 RVA: 0x002C4AE0 File Offset: 0x002C3AE0
	private void ᜀ(XlsWorksheetBase A_0, string A_1)
	{
		int a_ = 9;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				spr\u2570 spr_u;
				if (spr_u.ᜇ() != A_1)
				{
					num = 2;
					continue;
				}
				return;
			}
			case 1:
			{
				if (A_1.Length == 0)
				{
					num = 5;
					continue;
				}
				sprᡟ sprᡟ = A_0.ᜠ;
				num = 7;
				continue;
			}
			case 2:
			{
				if (true)
				{
				}
				spr\u2570 spr_u;
				this.\u171A.ᜀ(spr_u.ᜇ());
				spr_u.ᜀ(A_1);
				this.\u171A.ᜀ(spr_u);
				num = 10;
				continue;
			}
			case 3:
				num = 1;
				continue;
			case 4:
				goto IL_91;
			case 5:
				goto IL_18D;
			case 7:
			{
				sprᡟ sprᡟ;
				if (sprᡟ == null)
				{
					num = 4;
					continue;
				}
				spr\u2570 spr_u = sprᡟ.ᜉ();
				num = 0;
				continue;
			}
			case 8:
				if (A_1 != null)
				{
					num = 3;
					continue;
				}
				goto IL_158;
			case 9:
				goto IL_50;
			case 10:
				goto IL_EC;
			}
			if (A_0 == null)
			{
				num = 9;
			}
			else
			{
				num = 8;
			}
		}
		IL_50:
		goto IL_114;
		IL_91:
		throw new ApplicationException(RecordTableEnumerator.b("簾⁀ⵂ⭄⡆㵈歊㹌⩎⍐㩒㑔㭖じ⅚㡜罞በୢdɦᵨ䭪", a_) + A_0.Name);
		IL_EC:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_114:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾⥀♂⁄㍆", a_));
		default:
			if (false)
			{
			}
			return;
		}
		IL_158:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嘾㕀♂⡄ॆ⡈♊⡌", a_));
		IL_18D:
		goto IL_158;
	}

	// Token: 0x060048E9 RID: 18665 RVA: 0x002C4C7C File Offset: 0x002C3C7C
	private void ᜄ()
	{
		int a_ = 19;
		switch (0)
		{
		default:
			for (;;)
			{
				IL_2C:
				string id;
				sprᦨ sprᦨ = this.ᜦ.ᜀ(RecordTableEnumerator.b("ⅈ㽊㥌㽎歐籒穔⑖㩘㍚㡜㉞`ၢ䭤ࡦᥨ๪ͬᝮᱰὲ፴ᡶ୸ᙺᱼ୾궂ꒊ朗\udd98ﺜ철욢쮤펦蚨馪鶬龮螰鲲잴튶햸\udaba즼횾껀귂뛄꿆ꃈ믊뻌닐닒맔듖高돚볜뛞迠", a_), out id);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_B5;
					case 1:
					{
						string str;
						sprវ.ᜀ(this.ᜠ, out str);
						string text = str + sprᦨ.ᜂ();
						this.\u171A.ᜀ(text);
						this.\u171F.Remove(id);
						this.\u171E.Remove(text);
						num = 0;
						continue;
					}
					case 2:
						if (sprᦨ != null)
						{
							num = 1;
							continue;
						}
						goto IL_B7;
					}
					goto IL_2C;
				}
				IL_B7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					goto IL_CD;
				}
				IL_B5:
				goto IL_B7;
			}
			IL_CD:
			if (true)
			{
			}
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x060048EA RID: 18666 RVA: 0x002C4D64 File Offset: 0x002C3D64
	internal void ᜂ(string A_0, string A_1)
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
		this.ᜣ().Add(A_0, null);
		this.ᜦ.Remove(A_1);
	}

	// Token: 0x060048EB RID: 18667 RVA: 0x002C4DB8 File Offset: 0x002C3DB8
	internal string ᜀ(XlsExternWorkbook A_0)
	{
		int a_ = 9;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		MemoryStream memoryStream = new MemoryStream();
		StreamWriter streamWriter = new StreamWriter(memoryStream);
		XmlWriter xmlWriter = UtilityMethods.ᜀ(streamWriter);
		RelationsCollection a_2 = this.\u170D().ᜅ(xmlWriter, A_0);
		xmlWriter.Flush();
		streamWriter.Flush();
		string text = this.ᜃ();
		this.\u171A.ᜀ(text, memoryStream, true, FileAttributes.Archive);
		this.ᜀ(text, a_2);
		this.\u171E['/' + text] = RecordTableEnumerator.b("帾ㅀ㍂⥄⹆⩈⩊㥌♎㹐㵒穔⅖㝘㽚獜ぞᅠ٢୤ὦѨݪ୬nͰṲᑴͶ੸噺ቼ᥾搜ﲐﮔ래쒠슢솤풦솨캪좬\udbae\udcb0\udfb2鮴튶솸쾺\ud8bc춾꿀ꋂ꧄识ꃈꗊꛌ꧐뻒맔", a_);
		return text;
	}

	// Token: 0x060048EC RID: 18668 RVA: 0x002C4E78 File Offset: 0x002C3E78
	private string ᜃ()
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
		return this.ᜀ(ref this.\u1734, RecordTableEnumerator.b("㵄⭆晈⹊㕌㭎㑐⅒㭔㙖㕘᝚㑜ㅞ੠ၢ䩤ɦᅨὪ࡬ᵮὰቲᥴ㭶ၸᕺᙼ", a_), RecordTableEnumerator.b("㵄⩆╈", a_));
	}

	// Token: 0x060048ED RID: 18669 RVA: 0x002C4EE4 File Offset: 0x002C3EE4
	private string ᜀ(ref int A_0, string A_1, string A_2)
	{
		int a_ = 11;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		string a_2 = A_1 + RecordTableEnumerator.b("㩀獂㡄楆", a_) + A_2;
		return this.ᜀ(ref A_0, a_2);
	}

	// Token: 0x060048EE RID: 18670 RVA: 0x002C4F48 File Offset: 0x002C3F48
	private string ᜀ(ref int A_0, string A_1)
	{
		string text;
		for (;;)
		{
			text = null;
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (this.\u171A.ᜆ(text) < 0)
					{
						num = 1;
						continue;
					}
					goto IL_28;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_28;
					default:
						goto IL_7D;
					}
					break;
				case 2:
					goto IL_28;
				}
				break;
				IL_28:
				A_0++;
				text = string.Format(A_1, A_0);
				num = 0;
			}
		}
		IL_7D:
		if (false)
		{
		}
		return text;
	}

	// Token: 0x060048EF RID: 18671 RVA: 0x002C4FDC File Offset: 0x002C3FDC
	internal string \u1716()
	{
		int a_ = 13;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		int num = 0;
		return this.ᜀ(ref num, RecordTableEnumerator.b("㭂⥄框㥈≊㭌⁎═ݒ㑔㕖㕘㹚⹜灞ᅠ੢፤ࡦᵨ㽪౬൮ᵰᙲ๴䝶Ѹ啺ռቾ", a_));
	}

	// Token: 0x060048F0 RID: 18672 RVA: 0x002C5038 File Offset: 0x002C4038
	internal void ᜁ(XlsWorksheetBase A_0, string A_1)
	{
		int a_ = 6;
		int num = 7;
		int num2;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				if (num2 < 0)
				{
					goto IL_81;
				}
				num = 8;
				continue;
			case 1:
				goto IL_189;
			case 2:
				num = 5;
				continue;
			case 3:
				num = 10;
				continue;
			case 4:
				A_1 = UtilityMethods.ᜀ(A_1);
				num = 12;
				continue;
			case 5:
				if (A_1.Length == 0)
				{
					num = 1;
					continue;
				}
				num = 9;
				continue;
			case 6:
				if (A_1 != null)
				{
					num = 2;
					continue;
				}
				goto IL_154;
			case 8:
				goto IL_146;
			case 9:
				if (A_1[0] == '/')
				{
					num = 4;
					continue;
				}
				goto IL_65;
			case 10:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_81;
				default:
					goto IL_ED;
				}
				break;
			case 11:
				goto IL_60;
			case 12:
				goto IL_65;
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			num = 6;
			continue;
			IL_65:
			num2 = this.\u171A.ᜆ(A_1);
			num = 0;
			continue;
			IL_81:
			num = 3;
		}
		IL_60:
		throw new ArgumentNullException(RecordTableEnumerator.b("䠻弽∿ᅁⱃ⍅ⵇ㹉", a_));
		IL_ED:
		if (false)
		{
		}
		spr\u2570 spr_u = this.\u171A.ᜁ(A_1, null, false, FileAttributes.Archive);
		goto IL_18B;
		IL_146:
		spr_u = this.\u171A.ᜀ(num2);
		goto IL_18B;
		IL_154:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("娻圽ⰿ❁੃❅╇⽉", a_));
		IL_189:
		goto IL_154;
		IL_18B:
		spr\u2570 a_2 = spr_u;
		A_0.DataHolder = new sprᡟ(this, a_2);
	}

	// Token: 0x060048F1 RID: 18673 RVA: 0x002C51E0 File Offset: 0x002C41E0
	internal string ᜀ(IListObject A_0)
	{
		int a_ = 16;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		MemoryStream memoryStream = new MemoryStream();
		XmlWriter xmlWriter = UtilityMethods.ᜀ(memoryStream, Encoding.UTF8);
		spr\u2458 spr_u = new spr\u2458();
		spr_u.ᜂ(xmlWriter, A_0);
		xmlWriter.Flush();
		memoryStream.Flush();
		string text = this.ᜂ();
		this.ᜡ()['/' + text] = RecordTableEnumerator.b("❅㡇㩉⁋❍㍏㍑⁓㽕㝗㑙獛⡝๟١䩣॥ᡧཀྵɫ᙭ᵯṱታ᥵੷᝹ᵻ੽꾁﶑ﾙ躟톡풣풥춧쮩좫\uddad\ud8afힱ톳습햷횹銻쪽ꆿꃁꣃꏅ닉ꇋꋍ", a_);
		this.\u171A.ᜀ(text, memoryStream, true, FileAttributes.Archive);
		return text;
	}

	// Token: 0x060048F2 RID: 18674 RVA: 0x002C5290 File Offset: 0x002C4290
	private string ᜂ()
	{
		int a_ = 8;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		int num = 0;
		return this.ᜀ(ref num, RecordTableEnumerator.b("䘽ⰿ流ぃ❅⩇♉⥋㵍罏♑㕓㑕㑗㽙❛湝ᵟ䱡ᱣ୥ѧ", a_));
	}

	// Token: 0x060048F3 RID: 18675 RVA: 0x002C52EC File Offset: 0x002C42EC
	public string ᜉ(string A_0)
	{
		int num = 2;
		string result;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_0 = UtilityMethods.ᜀ(Path.GetExtension(A_0));
				result = this.\u171D[A_0];
				num = 1;
				continue;
			case 1:
				goto IL_5A;
			}
			goto IL_1C;
			IL_2C:
			num = 0;
			continue;
			IL_1C:
			if (!this.\u171E.TryGetValue(A_0, out result))
			{
				goto IL_2C;
			}
			IL_5A:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_2C;
			default:
				goto IL_70;
			}
		}
		IL_70:
		if (false)
		{
		}
		if (true)
		{
		}
		return result;
	}

	// Token: 0x060048F4 RID: 18676 RVA: 0x002C5384 File Offset: 0x002C4384
	public void \u171F()
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
		this.\u171A = null;
		this.\u1739 = null;
		this.\u173E = null;
		this.ᜭ = null;
		this.ᜬ = null;
		this.ᜫ = null;
		GC.SuppressFinalize(this);
	}

	// Token: 0x060048F5 RID: 18677 RVA: 0x002C53F0 File Offset: 0x002C43F0
	public void ᜀ(string A_0, XlsWorkbook A_1, ExcelSaveType A_2)
	{
		int a_ = 19;
		if (A_1 != this.\u171B)
		{
			for (;;)
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_3C;
				}
			}
			IL_3C:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭈⑊≌⑎", a_));
		}
		this.ᜀ(A_0, A_2);
	}

	// Token: 0x060048F6 RID: 18678 RVA: 0x002C545C File Offset: 0x002C445C
	public void ᜀ(Stream A_0, XlsWorkbook A_1, ExcelSaveType A_2)
	{
		int a_ = 5;
		if (A_1 != this.\u171B)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("夺刼倾⩀", a_));
		}
		this.ᜀ(A_0, A_2);
	}

	// Token: 0x060048F7 RID: 18679 RVA: 0x002C54C8 File Offset: 0x002C44C8
	internal sprវ ᜀ(XlsWorkbook A_0)
	{
		sprវ sprវ;
		for (;;)
		{
			sprវ = (sprវ)base.MemberwiseClone();
			sprវ.\u171B = A_0;
			sprវ.\u171C = null;
			sprវ.ᜤ = null;
			sprវ.ᜦ = this.ᜦ.ᜀ();
			sprវ.\u171F = this.\u171F.ᜀ();
			sprវ.\u1735 = sprἽ.ᜀ(this.\u1735);
			sprវ.ᜫ = sprἽ.ᜀ(this.ᜫ);
			sprវ.ᜬ = sprἽ.ᜀ(this.ᜬ);
			sprវ.ᜭ = sprἽ.ᜀ(this.ᜭ);
			sprវ.\u1739 = sprἽ.ᜀ(this.\u1739);
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u171D != null)
					{
						num = 2;
						continue;
					}
					goto IL_114;
				case 1:
					goto IL_10F;
				case 2:
					sprវ.\u171D = new Dictionary<string, string>(this.\u171D);
					num = 5;
					continue;
				case 3:
					sprវ.\u171E = new Dictionary<string, string>(this.\u171E);
					num = 1;
					continue;
				case 4:
					if (this.\u171E != null)
					{
						num = 3;
						continue;
					}
					goto IL_1C7;
				case 5:
					goto IL_114;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10F;
					default:
						goto IL_18C;
					}
					break;
				case 7:
					sprវ.ᜥ = new List<int>(this.ᜥ);
					num = 6;
					continue;
				case 8:
					sprវ.\u1738 = new Dictionary<string, object>(this.\u1738);
					num = 11;
					continue;
				case 9:
					if (this.\u1738 != null)
					{
						num = 8;
						continue;
					}
					goto IL_137;
				case 10:
					if (this.ᜥ != null)
					{
						num = 7;
						continue;
					}
					goto IL_20B;
				case 11:
					if (true)
					{
					}
					goto IL_137;
				}
				break;
				IL_114:
				num = 4;
				continue;
				IL_137:
				num = 0;
				continue;
				IL_1C7:
				num = 10;
				continue;
				IL_10F:
				goto IL_1C7;
			}
		}
		IL_18C:
		if (false)
		{
		}
		IL_20B:
		sprវ.\u1736 = this.ᜀ();
		sprវ.\u1737 = this.ᜁ();
		sprវ.\u171A = this.\u171A.ᜀ();
		return sprវ;
	}

	// Token: 0x060048F8 RID: 18680 RVA: 0x002C570C File Offset: 0x002C470C
	private List<Dictionary<string, string>> ᜁ()
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			List<Dictionary<string, string>> list;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_E9;
				case 2:
					goto IL_E9;
				case 3:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 6;
						continue;
					}
					Dictionary<string, string> dictionary = this.\u1737[num2];
					list.Add(new Dictionary<string, string>(dictionary));
					num2++;
					num = 4;
					continue;
				}
				case 4:
					goto IL_69;
				case 5:
				{
					int count = this.\u1737.Count;
					list = new List<Dictionary<string, string>>(count);
					int num2 = 0;
					num = 7;
					continue;
				}
				case 6:
					if (true)
					{
					}
					num = 2;
					continue;
				case 7:
					goto IL_69;
				}
				if (this.\u1737 != null)
				{
					num = 5;
					continue;
				}
				list = null;
				goto IL_B0;
				IL_69:
				num = 3;
				continue;
				IL_B0:
				num = 1;
				continue;
				IL_E9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B0;
				default:
					goto IL_FF;
				}
			}
			IL_FF:
			if (false)
			{
			}
			return list;
		}
		}
	}

	// Token: 0x060048F9 RID: 18681 RVA: 0x002C5820 File Offset: 0x002C4820
	private List<spr\u21A7> ᜀ()
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			List<spr\u21A7> list;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					spr\u21A7 spr_u21A = this.\u1736[num2];
					list.Add(spr_u21A.ᜀ(this.\u171B));
					num2++;
					num = 7;
					continue;
				}
				case 1:
					goto IL_69;
				case 2:
					num = 5;
					continue;
				case 4:
				{
					int count = this.\u1736.Count;
					list = new List<spr\u21A7>(count);
					int num2 = 0;
					num = 1;
					continue;
				}
				case 5:
					if (true)
					{
					}
					goto IL_F2;
				case 6:
					goto IL_F2;
				case 7:
					goto IL_69;
				}
				if (this.\u1736 != null)
				{
					num = 4;
					continue;
				}
				list = null;
				goto IL_B0;
				IL_69:
				num = 0;
				continue;
				IL_B0:
				num = 6;
				continue;
				IL_F2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B0;
				default:
					goto IL_108;
				}
			}
			IL_108:
			if (false)
			{
			}
			return list;
		}
		}
	}

	// Token: 0x060048FA RID: 18682 RVA: 0x002C593C File Offset: 0x002C493C
	internal void ᜁ(string A_0, string A_1)
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
		this.\u173C.Add(A_0, A_1);
	}

	// Token: 0x040020E5 RID: 8421
	private const string ᜀ = "[Content_Types].xml";

	// Token: 0x040020E6 RID: 8422
	internal const string ᜁ = "_rels";

	// Token: 0x040020E7 RID: 8423
	internal const string ᜂ = ".rels";

	// Token: 0x040020E8 RID: 8424
	private const string ᜃ = "_rels/.rels";

	// Token: 0x040020E9 RID: 8425
	private const string ᜄ = "xml";

	// Token: 0x040020EA RID: 8426
	private const string ᜅ = "rels";

	// Token: 0x040020EB RID: 8427
	public const string ᜆ = "bin";

	// Token: 0x040020EC RID: 8428
	private const string ᜇ = "xl/workbook.xml";

	// Token: 0x040020ED RID: 8429
	private const string ᜈ = "/xl/sharedStrings.xml";

	// Token: 0x040020EE RID: 8430
	private const string ᜉ = "xl/styles.xml";

	// Token: 0x040020EF RID: 8431
	private const string ᜊ = "xl/theme/theme1.xml";

	// Token: 0x040020F0 RID: 8432
	private const string ᜋ = "xl/worksheets/sheet{0}.xml";

	// Token: 0x040020F1 RID: 8433
	private const string ᜌ = "xl/chartsheets/sheet{0}.xml";

	// Token: 0x040020F2 RID: 8434
	public const string \u170D = "xl/media/image{0}.";

	// Token: 0x040020F3 RID: 8435
	public const string ᜎ = "docProps/app.xml";

	// Token: 0x040020F4 RID: 8436
	public const string ᜏ = "docProps/core.xml";

	// Token: 0x040020F5 RID: 8437
	public const string ᜐ = "docProps/custom.xml";

	// Token: 0x040020F6 RID: 8438
	private const string ᜑ = "rId{0}";

	// Token: 0x040020F7 RID: 8439
	public const string \u1712 = "xl/externalLinks/externalLink{0}.xml";

	// Token: 0x040020F8 RID: 8440
	private const string \u1713 = "xl/externalLinks/externalLink";

	// Token: 0x040020F9 RID: 8441
	public const string \u1714 = "xl/customProperty";

	// Token: 0x040020FA RID: 8442
	public const string \u1715 = "xl/pivotCache/pivotCacheDefinition{0}.xml";

	// Token: 0x040020FB RID: 8443
	public const string \u1716 = "xl/pivotCache/pivotCacheRecords{0}.xml";

	// Token: 0x040020FC RID: 8444
	public const string \u1717 = "xl/pivotTables/pivotTable{0}.xml";

	// Token: 0x040020FD RID: 8445
	private const string \u1718 = "xl/tables/table{0}.xml";

	// Token: 0x040020FE RID: 8446
	private Dictionary<string, MemoryStream> \u1719;

	// Token: 0x040020FF RID: 8447
	private spr\u249E \u171A;

	// Token: 0x04002100 RID: 8448
	private XlsWorkbook \u171B;

	// Token: 0x04002101 RID: 8449
	private spr\u2306 \u171C;

	// Token: 0x04002102 RID: 8450
	private IDictionary<string, string> \u171D;

	// Token: 0x04002103 RID: 8451
	private IDictionary<string, string> \u171E;

	// Token: 0x04002104 RID: 8452
	private RelationsCollection \u171F;

	// Token: 0x04002105 RID: 8453
	private string ᜠ;

	// Token: 0x04002106 RID: 8454
	private string ᜡ;

	// Token: 0x04002107 RID: 8455
	private string ᜢ;

	// Token: 0x04002108 RID: 8456
	private string ᜣ;

	// Token: 0x04002109 RID: 8457
	private spr\u1B7A ᜤ;

	// Token: 0x0400210A RID: 8458
	private List<int> ᜥ;

	// Token: 0x0400210B RID: 8459
	private RelationsCollection ᜦ;

	// Token: 0x0400210C RID: 8460
	private string ᜧ;

	// Token: 0x0400210D RID: 8461
	private string ᜨ;

	// Token: 0x0400210E RID: 8462
	private string ᜩ;

	// Token: 0x0400210F RID: 8463
	private string ᜪ;

	// Token: 0x04002110 RID: 8464
	private Stream ᜫ;

	// Token: 0x04002111 RID: 8465
	private Stream ᜬ;

	// Token: 0x04002112 RID: 8466
	private Stream ᜭ;

	// Token: 0x04002113 RID: 8467
	private int ᜮ;

	// Token: 0x04002114 RID: 8468
	private int ᜯ;

	// Token: 0x04002115 RID: 8469
	private int ᜰ;

	// Token: 0x04002116 RID: 8470
	private int ᜱ;

	// Token: 0x04002117 RID: 8471
	private int \u1732;

	// Token: 0x04002118 RID: 8472
	private int \u1733;

	// Token: 0x04002119 RID: 8473
	private int \u1734;

	// Token: 0x0400211A RID: 8474
	private string[] \u1735;

	// Token: 0x0400211B RID: 8475
	private List<spr\u21A7> \u1736;

	// Token: 0x0400211C RID: 8476
	private List<Dictionary<string, string>> \u1737;

	// Token: 0x0400211D RID: 8477
	private Dictionary<string, object> \u1738;

	// Token: 0x0400211E RID: 8478
	private Stream \u1739;

	// Token: 0x0400211F RID: 8479
	private FileVersion \u173A;

	// Token: 0x04002120 RID: 8480
	private string \u173B;

	// Token: 0x04002121 RID: 8481
	private Dictionary<string, string> \u173C;

	// Token: 0x04002122 RID: 8482
	private ExcelVersion \u173D;

	// Token: 0x04002123 RID: 8483
	private Stream \u173E;
}
