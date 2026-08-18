using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200012D RID: 301
	public class XlsAutoFilterCondition : IAutoFilterCondition
	{
		// Token: 0x06000EBB RID: 3771 RVA: 0x000997CC File Offset: 0x000987CC
		internal XlsAutoFilterCondition(XlsAutoFiltersCollection A_0)
		{
			this.ᜆ = A_0;
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x000997E8 File Offset: 0x000987E8
		// (set) Token: 0x06000EBD RID: 3773 RVA: 0x0009982C File Offset: 0x0009882C
		public FilterDataType DataType
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
				for (;;)
				{
					this.ᜀ = value;
					int num = 0;
					int count = this.ᜆ.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (true)
							{
							}
							goto IL_6C;
						case 1:
							if (this.ᜆ[num].SecondCondition.DataType == FilterDataType.MatchAllBlanks)
							{
								num2 = 7;
								continue;
							}
							goto IL_190;
						case 2:
							if (num >= count)
							{
								num2 = 13;
								continue;
							}
							num2 = 12;
							continue;
						case 3:
							goto IL_129;
						case 4:
							goto IL_190;
						case 5:
							if (this.ᜆ[num].FirstCondition.DataType == FilterDataType.MatchAllNonBlanks)
							{
								goto IL_6C;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_B1;
							default:
								if (false)
								{
								}
								num2 = 11;
								continue;
							}
							break;
						case 6:
							goto IL_129;
						case 7:
							goto IL_1E3;
						case 8:
							goto IL_B1;
						case 9:
							if (this.ᜆ[num].SecondCondition.DataType == FilterDataType.MatchAllNonBlanks)
							{
								num2 = 0;
								continue;
							}
							goto IL_17F;
						case 10:
							num2 = 1;
							continue;
						case 11:
							num2 = 9;
							continue;
						case 12:
							if (this.ᜆ[num].FirstCondition.DataType != FilterDataType.MatchAllBlanks)
							{
								num2 = 10;
								continue;
							}
							goto IL_1E3;
						case 13:
							return;
						}
						break;
						IL_6C:
						this.ᜇ = (XlsAutoFilter)this.ᜆ[num];
						this.ᜇ.ᜀ();
						this.ᜇ.ᜀ(FilterConditionType.NotEqual, this.ᜀ, string.Empty, num);
						num2 = 8;
						continue;
						IL_129:
						num2 = 2;
						continue;
						IL_17F:
						num++;
						num2 = 6;
						continue;
						IL_B1:
						goto IL_17F;
						IL_190:
						num2 = 5;
						continue;
						IL_1E3:
						this.ᜇ = (XlsAutoFilter)this.ᜆ[num];
						this.ᜇ.ᜀ();
						this.ᜇ.ᜀ(FilterConditionType.Equal, this.ᜀ, string.Empty, num);
						num2 = 4;
					}
				}
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00099A68 File Offset: 0x00098A68
		// (set) Token: 0x06000EBF RID: 3775 RVA: 0x00099AAC File Offset: 0x00098AAC
		public FilterConditionType ConditionOperator
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

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x00099AF0 File Offset: 0x00098AF0
		// (set) Token: 0x06000EC1 RID: 3777 RVA: 0x00099B34 File Offset: 0x00098B34
		public string String
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
			set
			{
				for (;;)
				{
					this.ᜂ = value;
					int num = 0;
					int count = this.ᜆ.Count;
					int num2 = 13;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							this.ᜇ = (XlsAutoFilter)this.ᜆ[num];
							this.ᜇ.ᜀ();
							object a_;
							this.ᜇ.ᜀ(FilterConditionType.Equal, this.ᜆ[num].FirstCondition.DataType, a_, num);
							num2 = 7;
							continue;
						}
						case 1:
							if (this.ᜆ[num].FirstCondition.String == this.ᜂ)
							{
								num2 = 8;
								continue;
							}
							goto IL_1D9;
						case 2:
							goto IL_212;
						case 3:
							if (this.ᜆ[num].SecondCondition.DataType == this.ᜀ)
							{
								num2 = 0;
								continue;
							}
							goto IL_1C5;
						case 4:
							goto IL_138;
						case 5:
						{
							if (num >= count)
							{
								num2 = 9;
								continue;
							}
							object a_ = this.ᜂ;
							num2 = 1;
							continue;
						}
						case 6:
							if (this.ᜆ[num].FirstCondition.DataType == this.ᜀ)
							{
								num2 = 12;
								continue;
							}
							goto IL_1D9;
						case 7:
							goto IL_1C5;
						case 8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_212;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								num2 = 6;
								continue;
							}
							break;
						case 9:
							return;
						case 10:
							goto IL_1D9;
						case 11:
							if (this.ᜆ[num].SecondCondition.String == this.ᜂ)
							{
								num2 = 2;
								continue;
							}
							goto IL_1C5;
						case 12:
						{
							this.ᜇ = (XlsAutoFilter)this.ᜆ[num];
							this.ᜇ.ᜀ();
							object a_;
							this.ᜇ.ᜀ(FilterConditionType.Equal, this.ᜆ[num].FirstCondition.DataType, a_, num);
							num2 = 10;
							continue;
						}
						case 13:
							goto IL_138;
						}
						break;
						IL_138:
						num2 = 5;
						continue;
						IL_1C5:
						num++;
						num2 = 4;
						continue;
						IL_1D9:
						num2 = 11;
						continue;
						IL_212:
						num2 = 3;
					}
				}
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x00099DB0 File Offset: 0x00098DB0
		// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x00099DF4 File Offset: 0x00098DF4
		public bool Boolean
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

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x00099E38 File Offset: 0x00098E38
		// (set) Token: 0x06000EC5 RID: 3781 RVA: 0x00099E7C File Offset: 0x00098E7C
		public byte ErrorCode
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜄ = value;
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00099EC0 File Offset: 0x00098EC0
		// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x00099F04 File Offset: 0x00098F04
		public double Double
		{
			get
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
				return this.ᜅ;
			}
			set
			{
				for (;;)
				{
					this.ᜅ = value;
					object a_ = this.ᜅ;
					int num = 0;
					int count = this.ᜆ.Count;
					int num2 = 11;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (this.ᜆ[num].FirstCondition.Double == this.ᜅ)
							{
								num2 = 14;
								continue;
							}
							goto IL_2D2;
						case 1:
							goto IL_2DD;
						case 2:
							if (num >= count)
							{
								num2 = 9;
								continue;
							}
							num2 = 0;
							continue;
						case 3:
							if (this.ᜆ[num].FirstCondition.ConditionOperator == this.ᜁ)
							{
								num2 = 6;
								continue;
							}
							goto IL_2D2;
						case 4:
							if (this.ᜆ[num].SecondCondition.DataType == this.ᜀ)
							{
								num2 = 7;
								continue;
							}
							goto IL_163;
						case 5:
							this.ᜇ = (XlsAutoFilter)this.ᜆ[num];
							this.ᜇ.ᜀ();
							this.ᜇ.ᜀ(this.ᜆ[num].SecondCondition.ConditionOperator, this.ᜆ[num].SecondCondition.DataType, a_, num);
							num2 = 17;
							continue;
						case 6:
							this.ᜇ = (XlsAutoFilter)this.ᜆ[num];
							this.ᜇ.ᜀ();
							this.ᜇ.ᜀ(this.ᜆ[num].FirstCondition.ConditionOperator, this.ᜆ[num].FirstCondition.DataType, a_, num);
							num2 = 15;
							continue;
						case 7:
							num2 = 8;
							continue;
						case 8:
							if (this.ᜆ[num].SecondCondition.ConditionOperator == this.ᜁ)
							{
								num2 = 5;
								continue;
							}
							goto IL_163;
						case 9:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2DD;
							default:
								goto IL_21C;
							}
							break;
						case 10:
							goto IL_1E9;
						case 11:
							goto IL_1E9;
						case 12:
							if (this.ᜆ[num].FirstCondition.DataType == this.ᜀ)
							{
								if (true)
								{
								}
								num2 = 16;
								continue;
							}
							goto IL_2D2;
						case 13:
							num2 = 4;
							continue;
						case 14:
							num2 = 12;
							continue;
						case 15:
							goto IL_2D2;
						case 16:
							num2 = 3;
							continue;
						case 17:
							goto IL_163;
						}
						break;
						IL_163:
						num++;
						num2 = 10;
						continue;
						IL_2DD:
						if (this.ᜆ[num].SecondCondition.Double == this.ᜅ)
						{
							num2 = 13;
							continue;
						}
						goto IL_163;
						IL_1E9:
						num2 = 2;
						continue;
						IL_2D2:
						num2 = 1;
					}
				}
				IL_21C:
				if (false)
				{
				}
			}
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0009A220 File Offset: 0x00099220
		internal void ᜁ(sprᱠ.ᜀ A_0)
		{
			int a_ = 10;
			for (;;)
			{
				sprᱠ.ᜀ.DOPERDataType doperdataType = A_0.ᜅ();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_145;
					case 1:
						switch (doperdataType)
						{
						case sprᱠ.ᜀ.DOPERDataType.FilterNotUsed:
							this.ᜀ = FilterDataType.NotUsed;
							num = 14;
							continue;
						case (sprᱠ.ᜀ.DOPERDataType)1:
						case (sprᱠ.ᜀ.DOPERDataType)3:
						case (sprᱠ.ᜀ.DOPERDataType)5:
						case (sprᱠ.ᜀ.DOPERDataType)7:
							goto IL_14A;
						case sprᱠ.ᜀ.DOPERDataType.RKNumber:
							this.ᜀ = FilterDataType.FloatingPoint;
							this.ᜅ = sprỔ.ᜃ(A_0.ᜁ());
							num = 0;
							continue;
						case sprᱠ.ᜀ.DOPERDataType.Number:
							this.ᜀ = FilterDataType.FloatingPoint;
							this.ᜅ = A_0.ᜃ();
							num = 7;
							continue;
						case sprᱠ.ᜀ.DOPERDataType.String:
							this.ᜀ = FilterDataType.String;
							this.ᜂ = A_0.ᜊ();
							num = 4;
							continue;
						case sprᱠ.ᜀ.DOPERDataType.BoolOrError:
							num = 5;
							continue;
						default:
							num = 13;
							continue;
						}
						break;
					case 2:
						switch (doperdataType)
						{
						case sprᱠ.ᜀ.DOPERDataType.MatchBlanks:
							this.ᜀ = FilterDataType.MatchAllBlanks;
							num = 9;
							continue;
						case (sprᱠ.ᜀ.DOPERDataType)13:
							goto IL_14A;
						case sprᱠ.ᜀ.DOPERDataType.MatchNonBlanks:
							this.ᜀ = FilterDataType.MatchAllNonBlanks;
							num = 3;
							continue;
						default:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_216;
							default:
								if (false)
								{
								}
								num = 10;
								continue;
							}
							break;
						}
						break;
					case 3:
						goto IL_10D;
					case 4:
						goto IL_B4;
					case 5:
						goto IL_216;
					case 6:
						goto IL_17C;
					case 7:
						goto IL_D7;
					case 8:
						goto IL_209;
					case 9:
						goto IL_1E9;
					case 10:
						num = 11;
						continue;
					case 11:
						goto IL_11D;
					case 12:
						this.ᜀ = FilterDataType.Boolean;
						this.ᜃ = A_0.ᜂ();
						num = 6;
						continue;
					case 13:
						num = 2;
						continue;
					case 14:
						goto IL_F6;
					}
					break;
					IL_216:
					if (A_0.ᜋ())
					{
						num = 12;
					}
					else
					{
						this.ᜀ = FilterDataType.ErrorCode;
						this.ᜄ = A_0.ᜉ();
						num = 8;
					}
				}
			}
			IL_B4:
			goto IL_22E;
			IL_D7:
			if (true)
			{
			}
			IL_F6:
			IL_10D:
			goto IL_22E;
			IL_11D:
			goto IL_14A;
			IL_145:
			goto IL_22E;
			IL_14A:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⌿ⵁ⩃≅ⅇ㹉╋⅍㹏籑ၓ㝕ⱗ㭙࡛❝ၟݡ", a_));
			IL_17C:
			IL_1E9:
			IL_209:
			IL_22E:
			this.ᜁ = (FilterConditionType)A_0.ᜆ();
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0009A468 File Offset: 0x00099468
		internal void ᜀ(sprᱠ.ᜀ A_0)
		{
			int a_ = 8;
			for (;;)
			{
				FilterDataType filterDataType = this.ᜀ;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_AF;
					case 1:
						goto IL_C6;
					case 2:
						goto IL_10E;
					case 3:
						goto IL_188;
					case 4:
						switch (filterDataType)
						{
						case FilterDataType.NotUsed:
							A_0.ᜀ(sprᱠ.ᜀ.DOPERDataType.FilterNotUsed);
							num = 1;
							continue;
						case FilterDataType.FloatingPoint:
							if (true)
							{
							}
							A_0.ᜀ(sprᱠ.ᜀ.DOPERDataType.Number);
							A_0.ᜀ(this.ᜅ);
							num = 0;
							continue;
						case FilterDataType.String:
							A_0.ᜀ(sprᱠ.ᜀ.DOPERDataType.String);
							A_0.ᜀ(this.ᜂ);
							num = 2;
							continue;
						case FilterDataType.Boolean:
							A_0.ᜀ(sprᱠ.ᜀ.DOPERDataType.BoolOrError);
							A_0.ᜀ(this.ᜃ);
							num = 3;
							continue;
						case FilterDataType.ErrorCode:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								A_0.ᜀ(sprᱠ.ᜀ.DOPERDataType.BoolOrError);
								A_0.ᜁ(this.ᜄ);
								num = 7;
								continue;
							}
							break;
						case FilterDataType.MatchAllBlanks:
							A_0.ᜀ(sprᱠ.ᜀ.DOPERDataType.MatchBlanks);
							break;
						case FilterDataType.MatchAllNonBlanks:
							A_0.ᜀ(sprᱠ.ᜀ.DOPERDataType.MatchNonBlanks);
							num = 9;
							continue;
						default:
							num = 5;
							continue;
						}
						num = 6;
						continue;
					case 5:
						num = 8;
						continue;
					case 6:
						goto IL_84;
					case 7:
						goto IL_168;
					case 8:
						goto IL_EE;
					case 9:
						goto IL_DE;
					}
					break;
				}
			}
			IL_84:
			IL_AF:
			IL_C6:
			IL_DE:
			goto IL_18A;
			IL_EE:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("匽Ἷ♁╃㉅⥇ṉ㕋㹍㕏", a_));
			IL_10E:
			IL_168:
			IL_188:
			IL_18A:
			A_0.ᜀ((sprᱠ.ᜀ.DOPERComparisonSign)this.ᜁ);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0009A60C File Offset: 0x0009960C
		public XlsAutoFilterCondition Clone(object parent)
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
			return (XlsAutoFilterCondition)base.MemberwiseClone();
		}

		// Token: 0x04000BC0 RID: 3008
		private FilterDataType ᜀ;

		// Token: 0x04000BC1 RID: 3009
		private FilterConditionType ᜁ;

		// Token: 0x04000BC2 RID: 3010
		private byte \u25D8\u009E\u00A6\u0096;

		// Token: 0x04000BC3 RID: 3011
		private string ᜂ;

		// Token: 0x04000BC4 RID: 3012
		private bool ᜃ;

		// Token: 0x04000BC5 RID: 3013
		private byte ᜄ;

		// Token: 0x04000BC6 RID: 3014
		private double ᜅ;

		// Token: 0x04000BC7 RID: 3015
		private XlsAutoFiltersCollection ᜆ;

		// Token: 0x04000BC8 RID: 3016
		private XlsAutoFilter ᜇ;
	}
}
