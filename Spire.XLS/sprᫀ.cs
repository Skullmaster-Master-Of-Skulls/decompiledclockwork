using System;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020004BE RID: 1214
[Serializable]
internal class spr\u1AC0 : ApplicationException
{
	// Token: 0x06004AD2 RID: 19154 RVA: 0x002D6D74 File Offset: 0x002D5D74
	public spr\u1AC0()
	{
		int a_ = 5;
		base..ctor(RecordTableEnumerator.b("渺匼娾㥀㍂⁄⑆㵈⹊⥌潎⍐㙒㙔㡖⭘㽚獜", a_));
	}

	// Token: 0x06004AD3 RID: 19155 RVA: 0x002D6DA0 File Offset: 0x002D5DA0
	internal spr\u1AC0(TBIFFRecord A_0)
	{
		int a_ = 12;
		base..ctor(string.Format(RecordTableEnumerator.b("ᝁ⩃⍅ぇ㩉⥋ⵍ⑏㝑こ癕⩗㽙㽛ㅝ቟١䑣ᵥ塧ᝩ䉫", a_), A_0));
	}

	// Token: 0x06004AD4 RID: 19156 RVA: 0x002D6DD8 File Offset: 0x002D5DD8
	public spr\u1AC0(string A_0)
	{
		int a_ = 4;
		base..ctor(string.Format(RecordTableEnumerator.b("漹刻嬽㠿㉁⅃╅㱇⽉⡋湍≏㝑㝓㥕⩗㹙籛╝偟ὡ䩣", a_), A_0));
	}

	// Token: 0x06004AD5 RID: 19157 RVA: 0x002D6E08 File Offset: 0x002D5E08
	public spr\u1AC0(string A_0, Exception A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x040021FA RID: 8698
	private const string ᜀ = "Unexpected record.";

	// Token: 0x040021FB RID: 8699
	private const string ᜁ = "Unexpected record {0}.";
}
