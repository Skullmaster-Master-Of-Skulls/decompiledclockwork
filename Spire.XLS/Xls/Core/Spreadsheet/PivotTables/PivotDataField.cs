using System;
using System.Collections.Generic;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.PivotTables
{
	// Token: 0x02000234 RID: 564
	public class PivotDataField : IPivotDataField, ICloneParent
	{
		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x0013295C File Offset: 0x0013195C
		// (set) Token: 0x0600224C RID: 8780 RVA: 0x001329A0 File Offset: 0x001319A0
		public string Name
		{
			get
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
			set
			{
				int a_ = 15;
				for (;;)
				{
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (value.Length == 0)
							{
								num = 2;
								continue;
							}
							goto IL_76;
						case 1:
							num = 0;
							continue;
						case 2:
							goto IL_74;
						}
						if (value == null)
						{
							goto IL_48;
						}
						if (true)
						{
						}
						num = 1;
					}
					IL_76:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_8C;
					}
				}
				IL_48:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㍄♆╈㹊⡌", a_));
				IL_74:
				goto IL_48;
				IL_8C:
				if (false)
				{
				}
				this.ᜀ = value;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x00132A48 File Offset: 0x00131A48
		// (set) Token: 0x0600224E RID: 8782 RVA: 0x00132A8C File Offset: 0x00131A8C
		public SubtotalTypes Subtotal
		{
			get
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
			set
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
				this.ᜁ = value;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x00132AD0 File Offset: 0x00131AD0
		// (set) Token: 0x06002250 RID: 8784 RVA: 0x00132B14 File Offset: 0x00131B14
		public int BaseField
		{
			get
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
			set
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
				this.ᜃ = value;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x00132B58 File Offset: 0x00131B58
		// (set) Token: 0x06002252 RID: 8786 RVA: 0x00132B9C File Offset: 0x00131B9C
		public int BaseItem
		{
			get
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
			set
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
				this.ᜄ = value;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x00132BE0 File Offset: 0x00131BE0
		internal XlsPivotField Field
		{
			get
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
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x00132C24 File Offset: 0x00131C24
		// (set) Token: 0x06002255 RID: 8789 RVA: 0x00132C68 File Offset: 0x00131C68
		internal PivotFieldFormatType ShowDataAs
		{
			get
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
				return this.ᜆ;
			}
			set
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
				this.ᜆ = value;
			}
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x00132CAC File Offset: 0x00131CAC
		internal PivotDataField(string A_0, SubtotalTypes A_1, XlsPivotField A_2)
		{
			int a_ = 3;
			this.ᜃ = -1;
			this.ᜆ = PivotFieldFormatType.Normal;
			base..ctor();
			if (A_0 != null)
			{
				if (A_0.Length != 0)
				{
					if (A_2 == null)
					{
						throw new ArgumentNullException(RecordTableEnumerator.b("䤸娺似娾⽀㝂̈́⹆ⱈ❊⥌", a_));
					}
					this.ᜀ = A_0;
					this.ᜁ = A_1;
					this.ᜂ = A_2;
					A_2.DataField = true;
					if (A_2.Axis == AxisTypes.None)
					{
						A_2.Axis = AxisTypes.Data;
					}
					this.ᜀ();
					this.ᜁ();
					return;
				}
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("圸娺值娾", a_));
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x00132D50 File Offset: 0x00131D50
		internal string ᜀ(PivotFieldFormatType A_0)
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
			return this.ᜇ[A_0];
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x00132D98 File Offset: 0x00131D98
		private void ᜁ()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_40;
				case 1:
					goto IL_99;
				}
				if (true)
				{
				}
				if (this.ᜅ == null)
				{
					num = 0;
					continue;
				}
				goto IL_99;
				IL_40:
				this.ᜅ = new List<PivotFieldFormatType>();
				this.ᜅ.Add(PivotFieldFormatType.PercentageOfParentColumn);
				this.ᜅ.Add(PivotFieldFormatType.PercentageOfParentRow);
				this.ᜅ.Add(PivotFieldFormatType.RankAscending);
				this.ᜅ.Add(PivotFieldFormatType.RankDecending);
				this.ᜅ.Add(PivotFieldFormatType.PercentageOfRunningTotal);
				num = 1;
				continue;
				IL_99:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_40;
				default:
					goto IL_AF;
				}
			}
			IL_AF:
			if (false)
			{
			}
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x00132E5C File Offset: 0x00131E5C
		private void ᜀ()
		{
			int a_ = 10;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_4C;
				case 1:
					goto IL_1D6;
				}
				if (true)
				{
				}
				if (this.ᜇ == null)
				{
					num = 0;
					continue;
				}
				goto IL_1D6;
				IL_4C:
				this.ᜇ = new Dictionary<PivotFieldFormatType, string>();
				this.ᜇ.Add(PivotFieldFormatType.Difference, RecordTableEnumerator.b("␿⭁≃⁅ⵇ㡉⥋⁍㍏㝑", a_));
				this.ᜇ.Add(PivotFieldFormatType.Index, RecordTableEnumerator.b("⤿ⱁ⁃⍅ぇ", a_));
				this.ᜇ.Add(PivotFieldFormatType.Normal, RecordTableEnumerator.b("⸿ⵁ㙃⭅⥇♉", a_));
				this.ᜇ.Add(PivotFieldFormatType.Percent, RecordTableEnumerator.b("〿❁㙃╅ⵇ⑉㡋", a_));
				this.ᜇ.Add(PivotFieldFormatType.PercentageOfDifference, RecordTableEnumerator.b("〿❁㙃╅ⵇ⑉㡋੍㥏㑑㉓", a_));
				this.ᜇ.Add(PivotFieldFormatType.PercentageOfColumn, RecordTableEnumerator.b("〿❁㙃╅ⵇ⑉㡋ō㙏ᅑ㭓㩕", a_));
				this.ᜇ.Add(PivotFieldFormatType.PercentageOfRow, RecordTableEnumerator.b("〿❁㙃╅ⵇ⑉㡋ō㙏Q㭓⅕", a_));
				this.ᜇ.Add(PivotFieldFormatType.PercentageOfTotal, RecordTableEnumerator.b("〿❁㙃╅ⵇ⑉㡋ō㙏ّ㭓≕㥗㙙", a_));
				this.ᜇ.Add(PivotFieldFormatType.RunTotal, RecordTableEnumerator.b("㈿㝁⩃ቅ❇㹉ⵋ≍", a_));
				this.ᜇ.Add(PivotFieldFormatType.PercentageOfParentColumn, RecordTableEnumerator.b("〿❁㙃╅ⵇ⑉㡋ō㙏ɑ㕓⑕㵗㑙⡛ᵝཟ๡", a_));
				this.ᜇ.Add(PivotFieldFormatType.PercentageOfParentRow, RecordTableEnumerator.b("〿❁㙃╅ⵇ⑉㡋ō㙏ɑ㕓⑕㵗㑙⡛ౝཟᕡ", a_));
				this.ᜇ.Add(PivotFieldFormatType.RankAscending, RecordTableEnumerator.b("㈿⍁⩃ⵅे㥉⽋⭍㹏㙑㵓㡕㽗", a_));
				this.ᜇ.Add(PivotFieldFormatType.RankDecending, RecordTableEnumerator.b("㈿⍁⩃ⵅే⽉㽋ⵍ㕏㱑こ㽕㙗㵙", a_));
				this.ᜇ.Add(PivotFieldFormatType.PercentageOfRunningTotal, RecordTableEnumerator.b("〿❁㙃╅ⵇ⑉㡋ō㙏Q⅓㡕㙗㍙㉛㥝㑟ൡၣݥѧ", a_));
				num = 1;
				continue;
				IL_1D6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4C;
				default:
					goto IL_1EC;
				}
			}
			IL_1EC:
			if (false)
			{
			}
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0013305C File Offset: 0x0013205C
		internal bool ᜂ()
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
			return Array.IndexOf<PivotFieldFormatType>(this.ᜅ.ToArray(), this.ShowDataAs) >= 0;
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x001330B4 File Offset: 0x001320B4
		internal PivotFieldFormatType ᜀ(string A_0)
		{
			if (true)
			{
			}
			Dictionary<PivotFieldFormatType, string>.Enumerator enumerator = this.ᜇ.GetEnumerator();
			goto IL_22;
			PivotFieldFormatType key;
			try
			{
				for (;;)
				{
					IL_22:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (!enumerator.MoveNext())
							{
								num = 6;
								continue;
							}
							KeyValuePair<PivotFieldFormatType, string> keyValuePair = enumerator.Current;
							num = 5;
							continue;
						}
						case 2:
							goto IL_CD;
						case 3:
						{
							KeyValuePair<PivotFieldFormatType, string> keyValuePair;
							key = keyValuePair.Key;
							num = 4;
							continue;
						}
						case 4:
							goto IL_A4;
						case 5:
						{
							KeyValuePair<PivotFieldFormatType, string> keyValuePair;
							if (keyValuePair.Value == A_0)
							{
								num = 3;
								continue;
							}
							break;
						}
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_22;
							default:
								if (false)
								{
								}
								num = 2;
								continue;
							}
							break;
						}
						IL_50:
						num = 0;
						continue;
						goto IL_50;
					}
				}
				IL_A4:
				return key;
				IL_CD:
				return PivotFieldFormatType.Normal;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return key;
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x001331B4 File Offset: 0x001321B4
		public object Clone(object parent)
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
			PivotDataField pivotDataField = (PivotDataField)base.MemberwiseClone();
			PivotDataFields pivotDataFields = (PivotDataFields)XlsObject.FindParent(parent, typeof(PivotDataFields));
			XlsPivotTable xlsPivotTable = (XlsPivotTable)XlsObject.FindParent(parent, typeof(XlsPivotTable));
			pivotDataField.ᜂ = (XlsPivotField)xlsPivotTable.PivotFields[this.ᜂ.Name];
			return pivotDataField;
		}

		// Token: 0x040011F0 RID: 4592
		private string ᜀ;

		// Token: 0x040011F1 RID: 4593
		private SubtotalTypes ᜁ;

		// Token: 0x040011F2 RID: 4594
		private XlsPivotField ᜂ;

		// Token: 0x040011F3 RID: 4595
		private int ᜃ;

		// Token: 0x040011F4 RID: 4596
		private float \u25D8\u0081\u008D\u00B0;

		// Token: 0x040011F5 RID: 4597
		private int ᜄ;

		// Token: 0x040011F6 RID: 4598
		private List<PivotFieldFormatType> ᜅ;

		// Token: 0x040011F7 RID: 4599
		private PivotFieldFormatType ᜆ;

		// Token: 0x040011F8 RID: 4600
		private float \u25D8\u008B\u00AE\u0092;

		// Token: 0x040011F9 RID: 4601
		private Dictionary<PivotFieldFormatType, string> ᜇ;
	}
}
