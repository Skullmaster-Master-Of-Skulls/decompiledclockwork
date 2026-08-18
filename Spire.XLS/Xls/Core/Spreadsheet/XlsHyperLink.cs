using System;
using System.Collections;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000168 RID: 360
	public class XlsHyperLink : XlsObject, IHyperLink, ICloneParent
	{
		// Token: 0x06001122 RID: 4386 RVA: 0x000A8C70 File Offset: 0x000A7C70
		internal XlsHyperLink(spr\u1DF5 A_0, object A_1)
		{
			this.ᜁ = (spr\u2626)spr\u175E.ᜀ(TBIFFRecord.HLink);
			base..ctor(A_0, A_1);
			this.ᜀ();
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x000A8CA0 File Offset: 0x000A7CA0
		internal XlsHyperLink(spr\u1DF5 A_0, object A_1, IList A_2, ref int A_3) : this(A_0, A_1)
		{
			A_3 = this.ᜀ(A_2, A_3);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000A8CC4 File Offset: 0x000A7CC4
		internal XlsHyperLink(spr\u1DF5 A_0, object A_1, IXLSRange A_2)
		{
			int a_ = 10;
			this..ctor(A_0, A_1);
			if (A_2 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("㈿⍁⩃ⅅⵇ", a_));
			}
			A_2.CellStyleName = RecordTableEnumerator.b("࠿㭁㑃⍅㩇♉╋⁍㭏", a_);
			this.ᜁ.ᜂ((uint)(A_2.Row - 1));
			this.ᜁ.ᜃ((uint)(A_2.Column - 1));
			this.ᜁ.ᜀ((uint)(A_2.LastRow - 1));
			this.ᜁ.ᜁ((uint)(A_2.LastColumn - 1));
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x000A8D5C File Offset: 0x000A7D5C
		private void ᜀ()
		{
			int a_ = 3;
			this.ᜃ = (base.FindParent(typeof(XlsWorksheet)) as XlsWorksheet);
			if (this.ᜃ == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("椸娺似娾⽀㝂敄⡆⭈⅊⡌ⱎ═獒㙔㙖㝘㕚㉜⭞䅠Ţd䝦ཨѪᡬŮᕰ嵲", a_));
				}
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x000A8DDC File Offset: 0x000A7DDC
		// (set) Token: 0x06001127 RID: 4391 RVA: 0x000A8E24 File Offset: 0x000A7E24
		public string UnicodePath
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
				return this.ᜁ.\u1712();
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_34;
						default:
							goto IL_5E;
						}
						break;
					case 2:
						goto IL_34;
					}
					if (this.ᜁ.\u171C() == HyperLinkType.File)
					{
						num = 2;
						continue;
					}
					return;
					IL_34:
					this.ᜁ.ᜂ(value);
					num = 1;
				}
				IL_5E:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x000A8EAC File Offset: 0x000A7EAC
		// (set) Token: 0x06001129 RID: 4393 RVA: 0x000A8F90 File Offset: 0x000A7F90
		public string Address
		{
			get
			{
				int a_ = 9;
				for (;;)
				{
					HyperLinkType hyperLinkType = this.ᜁ.\u171C();
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_9E;
						case 1:
							num = 0;
							continue;
						case 2:
							switch (hyperLinkType)
							{
							case HyperLinkType.None:
								goto IL_A0;
							case HyperLinkType.Url:
								goto IL_B6;
							case HyperLinkType.File:
								goto IL_A2;
							case HyperLinkType.Unc:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								default:
									goto IL_81;
								}
								break;
							case HyperLinkType.Workbook:
								goto IL_55;
							default:
								num = 1;
								continue;
							}
							break;
						}
						break;
					}
				}
				IL_55:
				return this.ᜁ.ᜏ();
				IL_81:
				if (false)
				{
				}
				return this.ᜁ.ᜤ();
				IL_9E:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("樾⽀⡂⭄⡆㹈╊浌͎㡐㵒㹔͖⁘⭚㡜", a_));
				IL_A0:
				return null;
				IL_A2:
				if (true)
				{
				}
				return this.ᜁ.\u1717();
				IL_B6:
				return this.ᜁ.\u171D();
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
				this.SetAddress(value, true);
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x000A8FD4 File Offset: 0x000A7FD4
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
				return this.ᜁ.ᜌ();
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x000A901C File Offset: 0x000A801C
		// (set) Token: 0x0600112C RID: 4396 RVA: 0x000A90E8 File Offset: 0x000A80E8
		public IXLSRange Range
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_39;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_39;
						default:
							goto IL_A4;
						}
						break;
					}
					if (true)
					{
					}
					if (this.ᜄ == null)
					{
						num = 0;
						continue;
					}
					goto IL_B6;
					IL_39:
					this.ᜄ = this.ᜃ.AllocatedRange[(int)(this.ᜁ.\u1719() + 1U), (int)(this.ᜁ.ᜨ() + 1U), (int)(this.ᜁ.\u1715() + 1U), (int)(this.ᜁ.ᜢ() + 1U)];
					num = 1;
				}
				IL_A4:
				if (false)
				{
				}
				IL_B6:
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
				this.ᜁ.ᜂ((uint)(value.Row - 1));
				this.ᜁ.ᜃ((uint)(value.Column - 1));
				this.ᜁ.ᜀ((uint)(value.LastRow - 1));
				this.ᜁ.ᜁ((uint)(value.LastColumn - 1));
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x000A9170 File Offset: 0x000A8170
		// (set) Token: 0x0600112E RID: 4398 RVA: 0x000A91B4 File Offset: 0x000A81B4
		public string ScreenTip
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
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜂ = value;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x000A91F8 File Offset: 0x000A81F8
		// (set) Token: 0x06001130 RID: 4400 RVA: 0x000A9240 File Offset: 0x000A8240
		public string SubAddress
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
				return this.ᜁ.ᜏ();
			}
			set
			{
				int a_ = 7;
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
							goto IL_57;
						default:
							goto IL_8E;
						}
						break;
					case 2:
						goto IL_57;
					}
					if (true)
					{
					}
					if (this.Range.CellStyleName != RecordTableEnumerator.b("甼䘾ㅀ♂㝄⭆⁈╊♌", a_))
					{
						num = 2;
						continue;
					}
					goto IL_A0;
					IL_57:
					this.Range.CellStyleName = RecordTableEnumerator.b("甼䘾ㅀ♂㝄⭆⁈╊♌", a_);
					num = 0;
				}
				IL_8E:
				if (false)
				{
				}
				IL_A0:
				this.ᜁ.ᜁ(value);
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x000A92FC File Offset: 0x000A82FC
		// (set) Token: 0x06001132 RID: 4402 RVA: 0x000A9344 File Offset: 0x000A8344
		public string TextToDisplay
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
				return this.ᜁ.ᜌ();
			}
			set
			{
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
							break;
						default:
							goto IL_85;
						}
						break;
					case 1:
						this.TopLeftCell.Text = this.ᜁ.ᜌ();
						num = 0;
						continue;
					case 3:
						if (true)
						{
						}
						this.ᜁ.ᜃ(value);
						num = 4;
						continue;
					case 4:
						if (!this.ᜃ.ParentWorkbook.Loading)
						{
							num = 1;
							continue;
						}
						return;
					}
					IL_24:
					if (value != this.TextToDisplay)
					{
						num = 3;
						continue;
					}
					return;
					goto IL_24;
				}
				IL_85:
				if (false)
				{
				}
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x000A9414 File Offset: 0x000A8414
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x000A945C File Offset: 0x000A845C
		public HyperLinkType Type
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
				return this.ᜁ.\u171C();
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
				this.ᜁ.ᜀ(value);
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x000A94A4 File Offset: 0x000A84A4
		public int FirstRow
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
				return (int)this.ᜁ.\u1719();
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x000A94EC File Offset: 0x000A84EC
		public int FirstColumn
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
				return (int)this.ᜁ.ᜨ();
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x000A9534 File Offset: 0x000A8534
		public int LastRow
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
				return (int)this.ᜁ.\u1715();
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x000A957C File Offset: 0x000A857C
		public int LastColumn
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
				return (int)this.ᜁ.ᜢ();
			}
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x000A95C4 File Offset: 0x000A85C4
		private int ᜀ(IList A_0, int A_1)
		{
			int a_ = 6;
			for (;;)
			{
				IL_09:
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						BiffRecordRaw biffRecordRaw;
						if (biffRecordRaw.TypeCode == TBIFFRecord.QuickTip)
						{
							num = 8;
							continue;
						}
						return A_1;
					}
					case 1:
						goto IL_F8;
					case 3:
						if (A_1 >= 0)
						{
							num = 5;
							continue;
						}
						goto IL_15A;
					case 4:
						goto IL_139;
					case 5:
						num = 6;
						continue;
					case 6:
						if (A_1 > A_0.Count - 1)
						{
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
						{
							if (false)
							{
							}
							BiffRecordRaw biffRecordRaw = (BiffRecordRaw)A_0[A_1];
							biffRecordRaw.CheckTypeCode(TBIFFRecord.HLink);
							this.ᜁ = (spr\u2626)biffRecordRaw;
							A_1++;
							biffRecordRaw = (BiffRecordRaw)A_0[A_1];
							num = 0;
							continue;
						}
						}
						break;
					case 7:
						goto IL_55;
					case 8:
					{
						BiffRecordRaw biffRecordRaw;
						sprṲ sprṲ = (sprṲ)biffRecordRaw;
						this.ᜂ = sprṲ.ᜁ();
						A_1++;
						num = 4;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 7;
					}
					else
					{
						num = 3;
					}
				}
			}
			IL_55:
			throw new ArgumentNullException(RecordTableEnumerator.b("堻弽㐿⍁", a_));
			IL_F8:
			if (true)
			{
			}
			goto IL_15A;
			IL_139:
			return A_1;
			IL_15A:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("氻儽㌿", a_), RecordTableEnumerator.b("樻弽ⰿ㝁⅃晅⭇⭉≋⁍㽏♑瑓㑕㵗穙せ㭝፟ᅡ䑣ብg୩ɫ乭䁯剱ᕳᡵᱷ婹᭻౽慎ꪉﲑ뒓聯ﶛ뺝첟잡쪣솥\udca7슩芫", a_));
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x000A9750 File Offset: 0x000A8750
		internal void ᜀ(RecordArrayList A_0)
		{
			int a_ = 15;
			int num = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						if (this.ᜂ != null)
						{
							num = 1;
							continue;
						}
						return;
					case 1:
						num = 6;
						continue;
					case 3:
						goto IL_66;
					case 4:
						return;
					case 5:
					{
						sprṲ sprṲ = (sprṲ)spr\u175E.ᜀ(TBIFFRecord.QuickTip);
						sprṲ.ᜀ(this.ᜂ);
						sprṲ.ᜀ(new TAddr((int)this.ᜁ.\u1719(), (int)this.ᜁ.ᜨ(), (int)this.ᜁ.\u1715(), (int)this.ᜁ.ᜢ()));
						A_0.ᜀ(sprṲ);
						num = 4;
						continue;
					}
					case 6:
						if (this.ᜂ.Length > 0)
						{
							num = 5;
							continue;
						}
						return;
					}
					if (A_0 == null)
					{
						num = 3;
					}
					else
					{
						A_0.ᜀ(this.ᜁ);
						num = 0;
					}
					break;
				}
			}
			IL_66:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㝄≆⩈⑊㽌⭎≐", a_));
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x000A989C File Offset: 0x000A889C
		public void SetSubAddress(string strSubAddress)
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
			this.ᜁ.ᜁ(strSubAddress);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000A98E4 File Offset: 0x000A88E4
		public void SetAddress(string strAddress, bool bSetText)
		{
			int a_ = 10;
			int num = 6;
			for (;;)
			{
				HyperLinkType hyperLinkType;
				switch (num)
				{
				case 0:
					goto IL_18F;
				case 1:
					if (this.ᜁ.ᜌ().Length == 0)
					{
						num = 0;
						continue;
					}
					goto IL_1BD;
				case 2:
					if (strAddress.IndexOf('#') == 0)
					{
						num = 10;
						continue;
					}
					goto IL_88;
				case 3:
					switch (hyperLinkType)
					{
					case HyperLinkType.Url:
						goto IL_110;
					case HyperLinkType.File:
						goto IL_6A;
					case HyperLinkType.Unc:
						goto IL_15B;
					case HyperLinkType.Workbook:
						goto IL_168;
					default:
						num = 5;
						continue;
					}
					break;
				case 4:
					this.Range.CellStyleName = RecordTableEnumerator.b("࠿㭁㑃⍅㩇♉╋⁍㭏", a_);
					num = 7;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_18F;
					default:
						if (false)
						{
						}
						num = 12;
						continue;
					}
					break;
				case 7:
					if (this.ᜁ.ᜌ() != null)
					{
						num = 9;
						continue;
					}
					goto IL_18F;
				case 8:
					goto IL_88;
				case 9:
					num = 1;
					continue;
				case 10:
					strAddress = strAddress.Remove(0, 1);
					num = 8;
					continue;
				case 11:
					goto IL_1BD;
				case 12:
					goto IL_1BB;
				}
				if (bSetText)
				{
					num = 4;
					continue;
				}
				goto IL_1BD;
				IL_88:
				hyperLinkType = this.ᜁ.\u171C();
				num = 3;
				continue;
				IL_18F:
				if (true)
				{
				}
				this.TopLeftCell.Text = strAddress;
				num = 11;
				continue;
				IL_1BD:
				num = 2;
			}
			IL_6A:
			this.ᜁ.ᜆ(strAddress);
			this.ᜁ.ᜆ(Path.IsPathRooted(strAddress));
			return;
			IL_110:
			this.ᜁ.ᜄ(strAddress);
			return;
			IL_15B:
			this.ᜁ.ᜀ(strAddress);
			return;
			IL_168:
			this.ᜁ.ᜁ(strAddress);
			return;
			IL_1BB:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ి⭁⩃ⵅ᱇㍉㱋⭍", a_));
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x000A9AE8 File Offset: 0x000A8AE8
		protected IXLSRange TopLeftCell
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
				return this.ᜃ.AllocatedRange[(int)(this.ᜁ.\u1719() + 1U), (int)(this.ᜁ.ᜨ() + 1U)];
			}
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x000A9B50 File Offset: 0x000A8B50
		public object Clone(object parent)
		{
			int a_ = 19;
			if (parent != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_0C;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				XlsHyperLink xlsHyperLink = (XlsHyperLink)base.MemberwiseClone();
				xlsHyperLink.SetParent(parent);
				xlsHyperLink.ᜀ();
				xlsHyperLink.ᜁ = (spr\u2626)spr\u1CD3.ᜀ(this.ᜁ);
				xlsHyperLink.ᜄ = null;
				return xlsHyperLink;
			}
			IL_0C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㥈⩊㽌⩎㽐❒", a_));
		}

		// Token: 0x04000E0B RID: 3595
		internal const string ᜀ = "Hyperlink";

		// Token: 0x04000E0C RID: 3596
		private long[] \u25D8\u0084\u00A0\u00A1;

		// Token: 0x04000E0D RID: 3597
		private float \u25D8\u00AF\u0080\u009F;

		// Token: 0x04000E0E RID: 3598
		private spr\u2626 ᜁ;

		// Token: 0x04000E0F RID: 3599
		private long \u25D9\u00B0\u0096\u009D;

		// Token: 0x04000E10 RID: 3600
		private bool \u25D8\u00AF\u0083\u0099;

		// Token: 0x04000E11 RID: 3601
		private string ᜂ;

		// Token: 0x04000E12 RID: 3602
		private XlsWorksheet ᜃ;

		// Token: 0x04000E13 RID: 3603
		private long[] \u25D8\u008F\u00A2\u0094;

		// Token: 0x04000E14 RID: 3604
		private IXLSRange ᜄ;
	}
}
