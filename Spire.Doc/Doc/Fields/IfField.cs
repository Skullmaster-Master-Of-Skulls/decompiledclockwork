using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;
using Spire.Doc.Interface;

namespace Spire.Doc.Fields
{
	// Token: 0x02000523 RID: 1315
	public class IfField : Field
	{
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06004507 RID: 17671 RVA: 0x00405440 File Offset: 0x00404440
		internal spr\u23E3 Expression1
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7B;
					case 1:
						goto IL_5A;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5A;
						}
						if (false)
						{
						}
						if (true)
						{
						}
						break;
					}
					if (this.ᜅ == null)
					{
						num = 1;
						continue;
					}
					break;
					IL_5A:
					this.ᜁ();
					this.ᜅ = new spr\u23E3(this.ᜀ);
					num = 0;
				}
				IL_7B:
				return this.ᜅ;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06004508 RID: 17672 RVA: 0x004054D0 File Offset: 0x004044D0
		internal spr\u23E3 Expression2
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5A;
						}
						if (false)
						{
						}
						if (true)
						{
						}
						break;
					case 1:
						goto IL_5A;
					case 2:
						goto IL_7B;
					}
					if (this.ᜆ == null)
					{
						num = 1;
						continue;
					}
					break;
					IL_5A:
					this.ᜁ();
					this.ᜆ = new spr\u23E3(this.ᜁ);
					num = 2;
				}
				IL_7B:
				return this.ᜆ;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06004509 RID: 17673 RVA: 0x00405560 File Offset: 0x00404560
		internal spr\u23E3 TrueTextField
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_52;
						}
						if (false)
						{
						}
						break;
					case 1:
						goto IL_7B;
					case 2:
						goto IL_52;
					}
					if (this.ᜇ == null)
					{
						num = 2;
						continue;
					}
					break;
					IL_52:
					this.ᜁ();
					this.ᜇ = new spr\u23E3(this.ᜃ);
					if (true)
					{
					}
					num = 1;
				}
				IL_7B:
				return this.ᜇ;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600450A RID: 17674 RVA: 0x004055F0 File Offset: 0x004045F0
		internal spr\u23E3 FalseTextField
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_52;
						}
						if (false)
						{
						}
						break;
					case 1:
						goto IL_52;
					case 2:
						goto IL_7B;
					}
					if (this.ᜈ == null)
					{
						num = 1;
						continue;
					}
					break;
					IL_52:
					this.ᜁ();
					this.ᜈ = new spr\u23E3(this.ᜄ);
					if (true)
					{
					}
					num = 2;
				}
				IL_7B:
				return this.ᜈ;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600450B RID: 17675 RVA: 0x00405680 File Offset: 0x00404680
		private Regex OperatorExpression
		{
			get
			{
				int a_ = 13;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_86;
					case 1:
						goto IL_5B;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5B;
						}
						if (false)
						{
						}
						break;
					}
					if (this.ᜉ == null)
					{
						num = 1;
						continue;
					}
					break;
					IL_5B:
					if (true)
					{
					}
					this.ᜉ = new Regex(ClipboardData.b("孲⹴䭶䝸䙺⁼呾ꢀ", a_));
					num = 0;
				}
				IL_86:
				return this.ᜉ;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x0600450C RID: 17676 RVA: 0x0040571C File Offset: 0x0040471C
		private Regex FieldExpression
		{
			get
			{
				int a_ = 8;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_86;
					case 1:
						goto IL_63;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_63;
						}
						if (false)
						{
						}
						if (true)
						{
						}
						break;
					}
					if (this.ᜊ == null)
					{
						num = 1;
						continue;
					}
					break;
					IL_63:
					this.ᜊ = new Regex(ClipboardData.b("㉭ͯ奱噳䥵偷ⅹ≻屽\udd7fꢁ궃ꒅ", a_));
					num = 0;
				}
				IL_86:
				return this.ᜊ;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600450D RID: 17677 RVA: 0x004057B8 File Offset: 0x004047B8
		internal List<spr\u23E3> MergeFields
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5A;
						}
						if (false)
						{
						}
						if (true)
						{
						}
						break;
					case 1:
						goto IL_75;
					case 2:
						goto IL_5A;
					}
					if (this.ᜋ == null)
					{
						num = 2;
						continue;
					}
					break;
					IL_5A:
					this.ᜋ = new List<spr\u23E3>();
					this.ᜅ();
					num = 1;
				}
				IL_75:
				return this.ᜋ;
			}
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x00405844 File Offset: 0x00404844
		public IfField(IDocument doc) : base(doc)
		{
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x00405858 File Offset: 0x00404858
		protected internal override void ParseFieldCode(string fieldCode)
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
			this.UpdateFieldCode(fieldCode);
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x0040589C File Offset: 0x0040489C
		protected internal override void UpdateFieldCode(string fieldCode)
		{
			int a_ = 2;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			char[] separator = new char[]
			{
				'\\'
			};
			string[] array = fieldCode.Split(separator);
			base.ParseFieldFormat(array);
			this.m_fieldValue = array[0].Replace(ClipboardData.b("ⅧⱩ", a_), string.Empty);
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x00405920 File Offset: 0x00404920
		private new void ᜁ()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_40;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 2:
					this.ᜀ();
					if (true)
					{
					}
					num = 0;
					continue;
				}
				goto IL_38;
				IL_40:
				num = 2;
				continue;
				IL_38:
				if (this.ᜀ == null)
				{
					goto IL_40;
				}
				break;
			}
		}

		// Token: 0x06004512 RID: 17682 RVA: 0x0040599C File Offset: 0x0040499C
		private new void ᜀ()
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 3;
				MatchCollection matchCollection;
				string text;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_11F;
					case 1:
						num = 5;
						continue;
					case 2:
						if (matchCollection.Count != 3)
						{
							num = 4;
							continue;
						}
						goto IL_16C;
					case 3:
						IL_28:
						break;
					case 4:
						return;
					case 5:
					{
						if (this.m_fieldValue == string.Empty)
						{
							num = 0;
							continue;
						}
						Match match = this.OperatorExpression.Match(this.m_fieldValue);
						this.ᜂ = match.Groups[0].Value;
						int num2 = match.Index;
						this.ᜀ = this.m_fieldValue.Substring(0, num2).Replace(ClipboardData.b("⁨⵪", a_), string.Empty);
						num2 += this.ᜂ.Length;
						text = this.m_fieldValue.Substring(num2, this.m_fieldValue.Length - num2);
						matchCollection = this.FieldExpression.Matches(text);
						num = 2;
						continue;
					}
					}
					if (this.m_fieldValue != null)
					{
						num = 1;
						continue;
					}
					IL_11F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_28;
					default:
						goto IL_135;
					}
				}
				return;
				IL_135:
				if (false)
				{
				}
				return;
				IL_16C:
				int num3 = matchCollection[0].Index;
				int index = matchCollection[1].Index;
				this.ᜁ = text.Substring(num3, index - num3);
				num3 = matchCollection[1].Index;
				index = matchCollection[2].Index;
				this.ᜃ = text.Substring(num3, index - num3);
				num3 = index;
				this.ᜄ = text.Substring(num3, text.Length - num3);
				return;
			}
			}
		}

		// Token: 0x06004513 RID: 17683 RVA: 0x00405B90 File Offset: 0x00404B90
		internal void ᜇ()
		{
			int a_ = 15;
			for (;;)
			{
				string text = ClipboardData.b("啴", a_);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜅ != null)
						{
							num = 18;
							continue;
						}
						goto IL_294;
					case 1:
						if (this.ᜀ != null)
						{
							num = 26;
							continue;
						}
						return;
					case 2:
						return;
					case 3:
						if (this.ᜃ != null)
						{
							num = 6;
							continue;
						}
						return;
					case 4:
						goto IL_FC;
					case 5:
						num = 22;
						continue;
					case 6:
						num = 12;
						continue;
					case 7:
						this.ᜀ = ClipboardData.b("坴", a_) + this.ᜅ.ᜃ() + ClipboardData.b("坴", a_) + text;
						num = 16;
						continue;
					case 8:
						this.ᜃ = ClipboardData.b("坴", a_) + this.ᜇ.ᜃ() + ClipboardData.b("坴", a_) + text;
						num = 4;
						continue;
					case 9:
						goto IL_BA;
					case 10:
						if (this.ᜇ.ᜃ() != null)
						{
							num = 8;
							continue;
						}
						goto IL_FC;
					case 11:
						if (this.ᜁ != null)
						{
							num = 21;
							continue;
						}
						return;
					case 12:
						if (this.ᜄ != null)
						{
							num = 25;
							continue;
						}
						return;
					case 13:
						IL_107:
						if (this.ᜈ != null)
						{
							num = 24;
							continue;
						}
						goto IL_BA;
					case 14:
						if (this.ᜆ != null)
						{
							num = 5;
							continue;
						}
						goto IL_303;
					case 15:
						this.ᜁ = ClipboardData.b("坴", a_) + this.ᜆ.ᜃ() + ClipboardData.b("坴", a_) + text;
						num = 27;
						continue;
					case 16:
						goto IL_294;
					case 17:
						num = 10;
						continue;
					case 18:
						num = 28;
						continue;
					case 19:
						if (this.ᜈ.ᜃ() != null)
						{
							num = 23;
							continue;
						}
						goto IL_BA;
					case 20:
						if (this.ᜇ != null)
						{
							num = 17;
							continue;
						}
						goto IL_FC;
					case 21:
						if (true)
						{
						}
						num = 3;
						continue;
					case 22:
						if (this.ᜆ.ᜃ() != null)
						{
							num = 15;
							continue;
						}
						goto IL_303;
					case 23:
						this.ᜄ = ClipboardData.b("坴", a_) + this.ᜈ.ᜃ() + ClipboardData.b("坴", a_);
						num = 9;
						continue;
					case 24:
						num = 19;
						continue;
					case 25:
						this.m_fieldValue = string.Concat(new string[]
						{
							this.ᜀ,
							this.ᜂ,
							text,
							this.ᜁ,
							this.ᜃ,
							this.ᜄ
						});
						num = 2;
						continue;
					case 26:
						num = 11;
						continue;
					case 27:
						goto IL_303;
					case 28:
						if (this.ᜅ.ᜃ() != null)
						{
							num = 7;
							continue;
						}
						goto IL_294;
					}
					break;
					IL_BA:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_107;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					IL_FC:
					num = 13;
					continue;
					IL_294:
					num = 14;
					continue;
					IL_303:
					num = 20;
				}
			}
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x00405F94 File Offset: 0x00404F94
		internal void ᜅ()
		{
			int num = 6;
			for (;;)
			{
				IL_0A:
				switch (num)
				{
				case 0:
					goto IL_80;
				case 1:
					return;
				case 2:
					this.ᜋ.Add(this.Expression1);
					num = 9;
					continue;
				case 3:
					this.ᜋ.Add(this.TrueTextField);
					num = 8;
					continue;
				case 4:
					this.ᜋ.Add(this.Expression2);
					num = 0;
					continue;
				case 5:
					if (this.FalseTextField.ᜁ())
					{
						num = 11;
						continue;
					}
					return;
				case 7:
					while (this.TrueTextField.ᜁ())
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
							num = 3;
							goto IL_0A;
						}
					}
					goto IL_122;
				case 8:
					if (true)
					{
					}
					goto IL_122;
				case 9:
					goto IL_C1;
				case 10:
					if (this.Expression2.ᜁ())
					{
						num = 4;
						continue;
					}
					goto IL_80;
				case 11:
					this.ᜋ.Add(this.FalseTextField);
					num = 1;
					continue;
				}
				if (this.Expression1.ᜁ())
				{
					num = 2;
					continue;
				}
				goto IL_C1;
				IL_80:
				num = 7;
				continue;
				IL_C1:
				num = 10;
				continue;
				IL_122:
				num = 5;
			}
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x00406114 File Offset: 0x00405114
		protected override object CloneImpl()
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
			IfField ifField = base.CloneImpl() as IfField;
			ifField.ᜅ = null;
			ifField.ᜆ = null;
			ifField.ᜇ = null;
			ifField.ᜈ = null;
			return ifField;
		}

		// Token: 0x0400362C RID: 13868
		private new string ᜀ;

		// Token: 0x0400362D RID: 13869
		private byte[] \u2460\u0088\u0093\u008D;

		// Token: 0x0400362E RID: 13870
		private new string ᜁ;

		// Token: 0x0400362F RID: 13871
		private int[] \u2460\u00A9ª\u0087;

		// Token: 0x04003630 RID: 13872
		private string ᜂ;

		// Token: 0x04003631 RID: 13873
		private long[] \u2460\u009F\u009C\u009A;

		// Token: 0x04003632 RID: 13874
		private new string ᜃ;

		// Token: 0x04003633 RID: 13875
		private new string ᜄ;

		// Token: 0x04003634 RID: 13876
		private spr\u23E3 ᜅ;

		// Token: 0x04003635 RID: 13877
		private spr\u23E3 ᜆ;

		// Token: 0x04003636 RID: 13878
		private spr\u23E3 ᜇ;

		// Token: 0x04003637 RID: 13879
		private int[] \u2593\u009A\u008A\u0089;

		// Token: 0x04003638 RID: 13880
		private spr\u23E3 ᜈ;

		// Token: 0x04003639 RID: 13881
		private Regex ᜉ;

		// Token: 0x0400363A RID: 13882
		private Regex ᜊ;

		// Token: 0x0400363B RID: 13883
		private List<spr\u23E3> ᜋ;
	}
}
