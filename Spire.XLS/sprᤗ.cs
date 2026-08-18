using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000353 RID: 851
[DefaultMember("Item")]
internal class sprᤗ : CollectionExtended<INamedRange>, INameRanges
{
	// Token: 0x060033A4 RID: 13220 RVA: 0x001DCA88 File Offset: 0x001DBA88
	internal sprᤗ(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
		this.ᜀ();
	}

	// Token: 0x060033A5 RID: 13221 RVA: 0x001DCAB0 File Offset: 0x001DBAB0
	public INamedRange ᜅ(string A_0)
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
		INamedRange result;
		this.ᜀ.TryGetValue(A_0, out result);
		return result;
	}

	// Token: 0x060033A6 RID: 13222 RVA: 0x001DCAFC File Offset: 0x001DBAFC
	public new IWorksheet ᜁ()
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

	// Token: 0x060033A7 RID: 13223 RVA: 0x001DCB40 File Offset: 0x001DBB40
	public new void ᜀ(INamedRange A_0, string A_1)
	{
		for (;;)
		{
			IL_00:
			int num = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						this.ᜀ.Remove(A_1);
						this.ᜀ.Add(A_0.Name, A_0);
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						return;
					}
					if (!this.ᜄ(A_1))
					{
						return;
					}
					num = 0;
					break;
				}
			}
		}
	}

	// Token: 0x060033A8 RID: 13224 RVA: 0x001DCBD4 File Offset: 0x001DBBD4
	public new INamedRange ᜀ(string A_0)
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
		INamedRange namedRange = new XlsName(base.ReservedHandle, this, A_0, base.Count, true);
		this.ᜁ(namedRange);
		return namedRange;
	}

	// Token: 0x060033A9 RID: 13225 RVA: 0x001DCC30 File Offset: 0x001DBC30
	public new INamedRange ᜀ(string A_0, IXLSRange A_1)
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
		XlsName xlsName = new XlsName(base.ReservedHandle, this, A_0, A_1, base.Count, true);
		this.ᜁ(xlsName);
		return xlsName;
	}

	// Token: 0x060033AA RID: 13226 RVA: 0x001DCC8C File Offset: 0x001DBC8C
	public new INamedRange ᜁ(INamedRange A_0)
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
		return this.ᜀ(A_0, true);
	}

	// Token: 0x060033AB RID: 13227 RVA: 0x001DCCD0 File Offset: 0x001DBCD0
	public new INamedRange ᜀ(INamedRange A_0, bool A_1)
	{
		int a_ = 11;
		while (A_0 == null)
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
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("⽀≂⡄≆", a_));
			}
		}
		base.Add(A_0);
		sprឦ sprឦ = (sprឦ)this.ᜁ.Names;
		sprឦ.ᜀ(A_0, A_1);
		return A_0;
	}

	// Token: 0x060033AC RID: 13228 RVA: 0x001DCD50 File Offset: 0x001DBD50
	public new void ᜂ(string A_0)
	{
		for (;;)
		{
			IL_00:
			int num = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
				{
					if (false)
					{
					}
					INamedRange namedRange;
					switch (num)
					{
					case 0:
						return;
					case 2:
						this.ᜀ.Remove(A_0);
						this.ᜁ.Names.RemoveAt(namedRange.Index);
						base.Remove(namedRange);
						num = 0;
						continue;
					}
					if (!this.ᜀ.TryGetValue(A_0, out namedRange))
					{
						return;
					}
					if (true)
					{
					}
					num = 2;
					break;
				}
				}
			}
		}
	}

	// Token: 0x060033AD RID: 13229 RVA: 0x001DCDFC File Offset: 0x001DBDFC
	public new void ᜂ()
	{
		for (;;)
		{
			int num = base.Count - 1;
			if (true)
			{
			}
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num < 0)
					{
						num2 = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_31;
					default:
						if (false)
						{
						}
						this.ᜂ(base[num].Name);
						num--;
						num2 = 3;
						continue;
					}
					break;
				case 1:
					goto IL_31;
				case 2:
					return;
				case 3:
					goto IL_33;
				}
				break;
				IL_33:
				num2 = 0;
				continue;
				IL_31:
				goto IL_33;
			}
		}
	}

	// Token: 0x060033AE RID: 13230 RVA: 0x001DCE9C File Offset: 0x001DBE9C
	public bool ᜄ(string A_0)
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
		return this.ᜀ.ContainsKey(A_0);
	}

	// Token: 0x060033AF RID: 13231 RVA: 0x001DCEE4 File Offset: 0x001DBEE4
	public INamedRange ᜃ(string A_0)
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
		return this.ᜅ(A_0);
	}

	// Token: 0x060033B0 RID: 13232 RVA: 0x001DCF28 File Offset: 0x001DBF28
	public new INamedRange ᜀ(INamedRange A_0)
	{
		int a_ = 5;
		while (A_0 == null)
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
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("唺尼刾⑀", a_));
			}
		}
		base.Add(A_0);
		return A_0;
	}

	// Token: 0x060033B1 RID: 13233 RVA: 0x001DCF90 File Offset: 0x001DBF90
	[CLSCompliant(false)]
	public new INamedRange ᜀ(sprῚ A_0)
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
		return this.ᜀ(A_0, true);
	}

	// Token: 0x060033B2 RID: 13234 RVA: 0x001DCFD4 File Offset: 0x001DBFD4
	[CLSCompliant(false)]
	public new INamedRange ᜀ(sprῚ A_0, bool A_1)
	{
		int a_ = 8;
		while (A_0 == null)
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
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("倽ℿ⽁⅃", a_));
			}
		}
		XlsName xlsName = new XlsName((spr\u2158)base.ReservedHandle, this, A_0.ᜊ(), base.Count);
		xlsName.ᜀ(A_0);
		((spr\u1D46)xlsName).ᜀ();
		this.ᜀ(xlsName, A_1);
		return xlsName;
	}

	// Token: 0x060033B3 RID: 13235 RVA: 0x001DD068 File Offset: 0x001DC068
	internal new void ᜀ(sprῚ[] A_0)
	{
		int a_ = 5;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				string value;
				int num2;
				int num3;
				FormulaUtil formulaUtil;
				switch (num)
				{
				case 0:
					goto IL_EE;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_171;
					default:
					{
						if (true)
						{
						}
						if (false)
						{
						}
						string text;
						if (text.StartsWith(value))
						{
							num = 8;
							continue;
						}
						goto IL_5E;
					}
					}
					break;
				case 2:
					goto IL_171;
				case 3:
					goto IL_EE;
				case 5:
				{
					if (num2 >= num3)
					{
						num = 7;
						continue;
					}
					sprῚ sprῚ = A_0[num2];
					string text = formulaUtil.ᜀ(sprῚ.ᜈ(), 0, 0, false, false);
					num = 1;
					continue;
				}
				case 6:
					goto IL_5C;
				case 7:
					return;
				case 8:
				{
					sprῚ sprῚ;
					this.ᜀ(sprῚ);
					num = 2;
					continue;
				}
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				value = RecordTableEnumerator.b("᰺", a_) + this.ᜂ.Name + RecordTableEnumerator.b("᰺", a_);
				formulaUtil = this.ᜁ.FormulaUtil;
				num2 = 0;
				num3 = A_0.Length;
				num = 3;
				continue;
				IL_5E:
				num2++;
				num = 0;
				continue;
				IL_171:
				goto IL_5E;
				IL_EE:
				num = 5;
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("唺尼刾⑀あ", a_));
		}
		}
	}

	// Token: 0x060033B4 RID: 13236 RVA: 0x001DD1EC File Offset: 0x001DC1EC
	internal new void ᜀ(sprᤗ A_0, IDictionary A_1, IDictionary A_2, NamesMergeOptionsType A_3, Dictionary<int, int> A_4)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 5;
			for (;;)
			{
				XlsWorkbook a_2;
				int num2;
				switch (num)
				{
				case 0:
					goto IL_10B;
				case 1:
					goto IL_10B;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_65;
					default:
					{
						if (false)
						{
						}
						if (A_4 == null)
						{
							num = 7;
							continue;
						}
						a_2 = A_0.ᜁ;
						num2 = 0;
						int count = A_0.Count;
						num = 1;
						continue;
					}
					}
					break;
				case 3:
				{
					int count;
					if (num2 >= count)
					{
						num = 4;
						continue;
					}
					goto IL_65;
				}
				case 4:
					return;
				case 6:
					goto IL_60;
				case 7:
					goto IL_16E;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num = 2;
				continue;
				IL_65:
				XlsName xlsName = (XlsName)A_0[num2];
				sprῚ sprῚ = (sprῚ)xlsName.Record.Clone();
				sprῚ.ᜀ((ushort)(this.ᜂ.RealIndex + 1));
				sprᤗ.ᜀ(sprῚ, a_2, A_1, A_4, this.ᜁ);
				INamedRange namedRange = this.ᜀ(sprῚ);
				A_2.Add(xlsName.Index, namedRange.Index);
				num2++;
				num = 0;
				continue;
				IL_10B:
				num = 3;
			}
			IL_60:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴽⼿㝁㙃╅ⵇщⵋ⍍㕏⅑", a_));
			IL_16E:
			throw new ArgumentNullException(RecordTableEnumerator.b("嘽ℿㅁⱃͅぇ㹉⥋㱍㹏ő㱓㍕㵗⹙ᕛそџݡᱣͥ᭧", a_));
		}
		}
	}

	// Token: 0x060033B5 RID: 13237 RVA: 0x001DD390 File Offset: 0x001DC390
	internal new static void ᜀ(sprῚ A_0, XlsWorkbook A_1, IDictionary A_2, Dictionary<int, int> A_3, XlsWorkbook A_4)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 19;
			for (;;)
			{
				int num2;
				Ptg[] array;
				switch (num)
				{
				case 0:
				{
					string text;
					if (text != null)
					{
						num = 1;
						continue;
					}
					goto IL_AC;
				}
				case 1:
					num = 21;
					continue;
				case 2:
				{
					int num3;
					if (num2 >= num3)
					{
						num = 22;
						continue;
					}
					sprẄ sprẄ = array[num2] as sprẄ;
					num = 5;
					continue;
				}
				case 3:
					goto IL_AC;
				case 4:
					if (array.Length == 0)
					{
						num = 6;
						continue;
					}
					num = 23;
					continue;
				case 5:
				{
					sprẄ sprẄ;
					if (sprẄ != null)
					{
						num = 8;
						continue;
					}
					goto IL_177;
				}
				case 6:
					goto IL_155;
				case 7:
					goto IL_1CD;
				case 8:
				{
					sprẄ sprẄ;
					int num4 = (int)sprẄ.ᜁ();
					string text = A_1.GetSheetNameByReference(num4);
					num = 0;
					continue;
				}
				case 9:
					if (array != null)
					{
						num = 25;
						continue;
					}
					return;
				case 10:
				{
					string text;
					if (text == RecordTableEnumerator.b("ᨸ椺砼社", a_))
					{
						num = 15;
						continue;
					}
					int num4 = A_4.AddSheetReference(text);
					sprẄ sprẄ;
					sprẄ.ᜂ((ushort)num4);
					num = 20;
					continue;
				}
				case 11:
					return;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_31D;
					default:
					{
						if (false)
						{
						}
						int num4;
						int num5 = A_3[num4];
						sprẄ sprẄ;
						sprẄ.ᜂ((ushort)num5);
						num = 24;
						continue;
					}
					}
					break;
				case 13:
				{
					if (true)
					{
					}
					int num4;
					if (A_3.ContainsKey(num4))
					{
						num = 12;
						continue;
					}
					goto IL_177;
				}
				case 14:
					goto IL_23E;
				case 15:
					num = 13;
					continue;
				case 16:
				{
					if (A_4 == null)
					{
						num = 14;
						continue;
					}
					num2 = 0;
					int num3 = array.Length;
					num = 18;
					continue;
				}
				case 17:
					goto IL_1F1;
				case 18:
					goto IL_31D;
				case 20:
					goto IL_177;
				case 21:
				{
					string text;
					if (A_2.Contains(text))
					{
						num = 26;
						continue;
					}
					goto IL_AC;
				}
				case 22:
					return;
				case 23:
					if (A_1 == null)
					{
						num = 7;
						continue;
					}
					num = 16;
					continue;
				case 24:
					goto IL_177;
				case 25:
					num = 4;
					continue;
				case 26:
				{
					string text = (string)A_2[text];
					num = 3;
					continue;
				}
				}
				if (A_3 == null)
				{
					num = 11;
					continue;
				}
				array = A_0.ᜈ();
				num = 9;
				continue;
				IL_AC:
				num = 10;
				continue;
				IL_177:
				num2++;
				num = 17;
				continue;
				IL_1F1:
				num = 2;
				continue;
				IL_31D:
				goto IL_1F1;
			}
			return;
			IL_155:
			return;
			IL_1CD:
			throw new ArgumentException(RecordTableEnumerator.b("嘸场夼紾⹀ⱂ⹄", a_));
			IL_23E:
			throw new ArgumentNullException(RecordTableEnumerator.b("圸帺䨼紾⹀ⱂ⹄", a_));
		}
		}
	}

	// Token: 0x060033B6 RID: 13238 RVA: 0x001DD6C0 File Offset: 0x001DC6C0
	public new void ᜀ(int A_0)
	{
		for (;;)
		{
			for (;;)
			{
				int num = base.Count - 1;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							goto IL_35;
						}
						break;
					case 1:
					{
						if (num < 0)
						{
							num2 = 2;
							continue;
						}
						XlsName xlsName = (XlsName)base.InnerList[num];
						xlsName.SetSheetIndex(A_0);
						num--;
						num2 = 0;
						continue;
					}
					case 2:
						return;
					case 3:
						goto IL_35;
					}
					break;
					IL_35:
					if (true)
					{
					}
					num2 = 1;
				}
			}
		}
	}

	// Token: 0x060033B7 RID: 13239 RVA: 0x001DD764 File Offset: 0x001DC764
	internal new XlsName ᜁ(string A_0)
	{
		int a_ = 7;
		int num = 1;
		XlsName xlsName;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_E2;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					if (A_0.Length == 0)
					{
						num = 5;
						continue;
					}
					xlsName = (this.ᜅ(A_0) as XlsName);
					num = 3;
					continue;
				case 2:
					goto IL_C0;
				case 3:
					if (xlsName == null)
					{
						num = 6;
						continue;
					}
					goto IL_E2;
				case 4:
					goto IL_5C;
				case 5:
					goto IL_E0;
				case 6:
					xlsName = (XlsName)this.ᜀ(A_0);
					num = 2;
					continue;
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num = 0;
				}
				break;
			}
		}
		IL_5C:
		throw new ArgumentNullException(RecordTableEnumerator.b("匼帾ⱀ♂", a_));
		IL_C0:
		goto IL_E2;
		IL_E0:
		throw new ArgumentException(RecordTableEnumerator.b("猼帾ⱀ♂敄⑆⡈╊浌ⅎ㹐❒畔㕖㱘筚㡜㉞ᅠᝢᱤ䥦", a_));
		IL_E2:
		if (true)
		{
		}
		return xlsName;
	}

	// Token: 0x060033B8 RID: 13240 RVA: 0x001DD868 File Offset: 0x001DC868
	internal new void ᜀ(ExcelVersion A_0)
	{
		for (;;)
		{
			for (;;)
			{
				if (true)
				{
				}
				int num = 0;
				int count = base.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return;
					case 1:
						goto IL_3D;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							goto IL_3D;
						}
						break;
					case 3:
					{
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						XlsName xlsName = (XlsName)base[num];
						xlsName.ConvertFullRowColumnName(A_0);
						num++;
						num2 = 2;
						continue;
					}
					}
					break;
					IL_3D:
					num2 = 3;
				}
			}
		}
	}

	// Token: 0x060033B9 RID: 13241 RVA: 0x001DD908 File Offset: 0x001DC908
	private new void ᜀ()
	{
		int a_ = 1;
		for (;;)
		{
			this.ᜂ = (base.FindParent(typeof(XlsWorksheet)) as XlsWorksheet);
			if (this.ᜂ != null)
			{
				goto IL_70;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_42;
			}
		}
		IL_42:
		if (false)
		{
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("朶堸䤺堼儾㕀捂⩄╆⍈⹊⹌㭎煐げ㑔㥖㝘㑚⥜罞͠٢䕤Ŧ٨Ṫͬ୮彰", a_));
		IL_70:
		this.ᜁ = this.ᜂ.ParentWorkbook;
	}

	// Token: 0x060033BA RID: 13242 RVA: 0x001DD998 File Offset: 0x001DC998
	protected virtual void ᜀ(int A_0, INamedRange A_1)
	{
		for (;;)
		{
			XlsName xlsName = (XlsName)A_1;
			string name = xlsName.Name;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!this.ᜁ.Loading)
					{
						goto IL_65;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_91;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 1:
					goto IL_65;
				case 2:
					goto IL_7A;
				case 3:
					goto IL_91;
				case 4:
					num = 3;
					continue;
				}
				break;
				IL_65:
				this.ᜀ[name] = A_1;
				num = 2;
				continue;
				IL_91:
				if (true)
				{
				}
				if (this.ᜀ.ContainsKey(name))
				{
					goto IL_B4;
				}
				num = 1;
			}
		}
		IL_7A:
		IL_B4:
		base.OnInsertComplete(A_0, A_1);
	}

	// Token: 0x040016E0 RID: 5856
	private new Dictionary<string, INamedRange> ᜀ = new Dictionary<string, INamedRange>();

	// Token: 0x040016E1 RID: 5857
	private new XlsWorkbook ᜁ;

	// Token: 0x040016E2 RID: 5858
	private new XlsWorksheet ᜂ;
}
