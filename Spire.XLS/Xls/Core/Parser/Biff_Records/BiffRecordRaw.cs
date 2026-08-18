using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls.Core.Parser.Biff_Records
{
	// Token: 0x0200025C RID: 604
	public abstract class BiffRecordRaw : ICloneable, IRecordStorage
	{
		// Token: 0x0600240D RID: 9229 RVA: 0x0014F840 File Offset: 0x0014E840
		public static int SkipBeginEndBlock(IList<BiffRecordRaw> recordList, int iPos)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_C9:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				goto IL_48;
			}
			int num2;
			BiffRecordRaw biffRecordRaw;
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
				{
					if (num2 <= 0)
					{
						num = 2;
						continue;
					}
					biffRecordRaw = recordList[iPos];
					TBIFFRecord typeCode = biffRecordRaw.TypeCode;
					num = 3;
					continue;
				}
				case 1:
					num = 4;
					continue;
				case 2:
					return iPos;
				case 3:
				{
					TBIFFRecord typeCode;
					switch (typeCode)
					{
					case TBIFFRecord.Begin:
						num2++;
						num = 7;
						continue;
					case TBIFFRecord.End:
						num2--;
						num = 8;
						continue;
					}
					goto IL_C9;
				}
				case 4:
					goto IL_7E;
				case 5:
					if (true)
					{
					}
					goto IL_D6;
				case 6:
					goto IL_D6;
				case 7:
					goto IL_7E;
				case 8:
					goto IL_7E;
				}
				goto IL_48;
				IL_7E:
				iPos++;
				num = 6;
				continue;
				IL_D6:
				num = 0;
			}
			return iPos;
			IL_48:
			biffRecordRaw = recordList[iPos];
			biffRecordRaw.CheckTypeCode(TBIFFRecord.Begin);
			num2 = 1;
			iPos++;
			num = 5;
			goto IL_1E;
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x0014F964 File Offset: 0x0014E964
		public TBIFFRecord TypeCode
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
				return (TBIFFRecord)this.m_iCode;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x0600240F RID: 9231 RVA: 0x0014F9A8 File Offset: 0x0014E9A8
		public int RecordCode
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
				return this.m_iCode;
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x0014F9EC File Offset: 0x0014E9EC
		// (set) Token: 0x06002411 RID: 9233 RVA: 0x0014FA30 File Offset: 0x0014EA30
		public int Length
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
				return this.m_iLength;
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
				this.m_iLength = value;
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06002412 RID: 9234 RVA: 0x0014FA74 File Offset: 0x0014EA74
		// (set) Token: 0x06002413 RID: 9235 RVA: 0x0014FADC File Offset: 0x0014EADC
		public virtual byte[] Data
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
				this.m_iLength = this.GetStoreSize(ExcelVersion.Version97to2003);
				byte[] array = new byte[this.m_iLength];
				spr\u24E5 provider = new spr\u24E5(array);
				this.InfillInternalData(provider, 0, ExcelVersion.Version97to2003);
				return array;
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
					{
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_47;
							}
						}
						IL_47:
						if (false)
						{
						}
						int iLength = value.Length;
						this.ParseStructure(new spr\u24E5(value), 0, iLength, ExcelVersion.Version97to2003);
						num = 0;
						continue;
					}
					}
					if (true)
					{
					}
					if (value == null)
					{
						break;
					}
					num = 1;
				}
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06002414 RID: 9236 RVA: 0x0014FB60 File Offset: 0x0014EB60
		// (set) Token: 0x06002415 RID: 9237 RVA: 0x0014FBA0 File Offset: 0x0014EBA0
		public virtual bool AutoGrowData
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
				throw new NotImplementedException();
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x0014FBE0 File Offset: 0x0014EBE0
		// (set) Token: 0x06002417 RID: 9239 RVA: 0x0014FC20 File Offset: 0x0014EC20
		public virtual long StreamPos
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
				return -1L;
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
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x0014FC5C File Offset: 0x0014EC5C
		public virtual int MinimumRecordSize
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
				return 0;
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x0014FC98 File Offset: 0x0014EC98
		public virtual int MaximumRecordSize
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
				return 8224;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x0014FCD8 File Offset: 0x0014ECD8
		public virtual int MaximumMemorySize
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
				return int.MaxValue;
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x0600241B RID: 9243 RVA: 0x0014FD18 File Offset: 0x0014ED18
		// (set) Token: 0x0600241C RID: 9244 RVA: 0x0014FD5C File Offset: 0x0014ED5C
		public bool NeedInfill
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
				return this.ᜅ;
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
				this.ᜅ = value;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x0600241D RID: 9245 RVA: 0x0014FDA0 File Offset: 0x0014EDA0
		public virtual bool NeedDataArray
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
				return false;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x0014FDDC File Offset: 0x0014EDDC
		public virtual bool IsAllowShortData
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
				return false;
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x0600241F RID: 9247 RVA: 0x0014FE18 File Offset: 0x0014EE18
		public virtual bool NeedDecoding
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
				return true;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06002420 RID: 9248 RVA: 0x0014FE54 File Offset: 0x0014EE54
		public virtual int StartDecodingOffset
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
				return 0;
			}
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x0014FE90 File Offset: 0x0014EE90
		internal static ushort ᜀ(ushort A_0, ushort A_1)
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
			return A_0 & A_1;
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x0014FED0 File Offset: 0x0014EED0
		internal static void ᜀ(ref ushort A_0, ushort A_1, ushort A_2)
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
			A_0 &= ~A_1;
			A_0 += (A_2 & A_1);
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x0014FF20 File Offset: 0x0014EF20
		internal static uint ᜀ(uint A_0, uint A_1)
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
			return A_0 & A_1;
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x0014FF60 File Offset: 0x0014EF60
		internal static void ᜀ(ref uint A_0, uint A_1, uint A_2)
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
			A_0 &= ~A_1;
			A_0 += (A_2 & A_1);
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x0014FFAC File Offset: 0x0014EFAC
		protected BiffRecordRaw()
		{
			Type type = base.GetType();
			object[] customAttributes = type.GetCustomAttributes(typeof(spr\u2593), true);
			if (customAttributes.Length != 0)
			{
				spr\u2593 spr_u = (spr\u2593)customAttributes[0];
				this.m_iCode = (int)spr_u.ᜀ();
			}
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x0015000C File Offset: 0x0014F00C
		protected BiffRecordRaw(Stream stream, out int itemSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x0015003C File Offset: 0x0014F03C
		protected BiffRecordRaw(BinaryReader reader, out int itemSize)
		{
			this.FillRecord(reader, null, null, null);
			itemSize = this.m_iLength;
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x00150078 File Offset: 0x0014F078
		protected BiffRecordRaw(int iReserve)
		{
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x001500A0 File Offset: 0x0014F0A0
		public virtual int FillRecord(BinaryReader reader, DataProvider provider, IDecryptor decryptor, byte[] arrBuffer)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 2;
				int result;
				for (;;)
				{
					switch (num)
					{
					case 0:
						try
						{
							for (;;)
							{
								long length;
								long position;
								long num2 = length - position - 4L;
								num = 7;
								for (;;)
								{
									switch (num)
									{
									case 0:
										this.m_iLength = this.MaximumRecordSize;
										num = 16;
										continue;
									case 1:
										if (this.NeedDecoding)
										{
											num = 3;
											continue;
										}
										goto IL_115;
									case 2:
										if (this.m_iCode == 0)
										{
											num = 6;
											continue;
										}
										num = 9;
										continue;
									case 3:
									{
										int startDecodingOffset = this.StartDecodingOffset;
										decryptor.Decrypt(provider, startDecodingOffset, this.m_iLength - startDecodingOffset, (long)((int)(position + 4L + (long)startDecodingOffset)));
										num = 11;
										continue;
									}
									case 4:
										num = 14;
										continue;
									case 5:
										goto IL_32B;
									case 6:
										goto IL_EB;
									case 7:
										if (num2 < 0L)
										{
											num = 8;
											continue;
										}
										num = 2;
										continue;
									case 8:
										goto IL_C6;
									case 9:
										if (this.m_iLength < this.MinimumRecordSize)
										{
											num = 4;
											continue;
										}
										goto IL_14A;
									case 10:
										goto IL_172;
									case 11:
										goto IL_115;
									case 12:
										if (this.m_iLength > this.MaximumRecordSize)
										{
											num = 0;
											continue;
										}
										goto IL_305;
									case 13:
										num = 1;
										continue;
									case 14:
										if (!this.IsAllowShortData)
										{
											num = 17;
											continue;
										}
										goto IL_14A;
									case 15:
										if (decryptor != null)
										{
											num = 13;
											continue;
										}
										goto IL_115;
									case 16:
										goto IL_305;
									case 17:
										goto IL_214;
									case 18:
										if (num2 - (long)this.m_iLength < 0L)
										{
											num = 10;
											continue;
										}
										provider.Read(reader, 0, this.m_iLength, null);
										num = 15;
										continue;
									}
									break;
									IL_115:
									this.StreamPos = position;
									num = 12;
									continue;
									IL_14A:
									num = 18;
									continue;
									IL_305:
									this.ParseStructure(provider, 0, this.m_iLength, ExcelVersion.Version97to2003);
									Stream baseStream;
									result = (int)(baseStream.Position - position);
									num = 5;
								}
							}
							IL_C6:
							throw new ApplicationException(RecordTableEnumerator.b("ᙂ⭄≆ㅈ㭊⡌ⱎ═㙒ㅔ睖㱘㕚㥜罞๠բ䕤ᕦ౨ࡪɬᵮᕰr啴Ѷ൸ॺ᡼ṾꎂꢄꞆﮈ戀랖ﲘ列뾞캠얢薤풦\udda8\ud9aa좬캮\udcb0鶲", a_));
							IL_EB:
							throw new ApplicationException(RecordTableEnumerator.b("łⱄⅆ⽈歊㽌⩎㉐㱒❔㍖祘㉚㥜㩞འᝢ౤Ŧhࡪ౬᭮ᡰᱲ᭴坶᩸ᑺ᥼᩾ꆀꞆﺈ力붒", a_));
							IL_172:
							throw new ApplicationException(RecordTableEnumerator.b("ᙂ⭄≆ㅈ㭊⡌ⱎ═㙒ㅔ睖㱘㕚㥜罞๠բ䕤ᕦ౨ࡪɬᵮᕰr啴Ѷ൸ॺ᡼Ṿ궂ꖄ햆ﶎ뎒漢붜ﲞ삠춢쮤좦\udda8讪쾬쪮醰솲킴횶\uddb8\udeba\ud9bc龾럄ꋆ꣈꣊ꗌ꫎뗐냔맖뷘ﯚ닜맞쇠郢釤闦賨諪胬쇮", a_));
							IL_214:
							throw new spr\u1AEA(string.Concat(new object[]
							{
								RecordTableEnumerator.b("B⩄⍆ⱈ歊睌", a_),
								((TBIFFRecord)this.m_iCode).ToString(),
								RecordTableEnumerator.b("䥂敄ᕆⱈ⩊⅌潎≐㩒⽔㉖捘筚", a_),
								this.m_iLength,
								RecordTableEnumerator.b("浂敄Ɇㅈ㭊⡌ⱎ═㙒ㅔ睖⩘㉚❜㩞孠䍢", a_),
								this.MinimumRecordSize.ToString()
							}));
							IL_32B:
							goto IL_3B5;
						}
						catch (ApplicationException)
						{
							long position;
							Stream baseStream;
							baseStream.Position = position;
							throw;
						}
						goto IL_33A;
					case 1:
						goto IL_4E;
					}
					if (reader == null)
					{
						num = 1;
						continue;
					}
					IL_33A:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4E;
					default:
					{
						if (false)
						{
						}
						Stream baseStream = reader.BaseStream;
						long position = baseStream.Position;
						long length = baseStream.Length;
						provider.Read(reader, 0, 4, arrBuffer);
						this.m_iCode = (int)provider.ReadInt16(0);
						this.m_iLength = (int)provider.ReadInt16(2);
						num = 0;
						break;
					}
					}
				}
				IL_4E:
				throw new ArgumentNullException(RecordTableEnumerator.b("ㅂ⁄♆ⵈ⹊㽌", a_));
				IL_3B5:
				if (true)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00150488 File Offset: 0x0014F488
		public virtual int FillStream(BinaryWriter writer, DataProvider provider, IEncryptor encryptor, int streamPosition)
		{
			int a_ = 5;
			int num = 6;
			int num2;
			byte[] arrBuffer;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_54;
				case 1:
					goto IL_1B5;
				case 2:
				{
					int startDecodingOffset = this.StartDecodingOffset;
					encryptor.Encrypt(provider, 4 + startDecodingOffset, this.m_iLength - startDecodingOffset, (long)(streamPosition + 4 + startDecodingOffset));
					num = 1;
					continue;
				}
				case 3:
					if (this.m_iLength < 0)
					{
						num = 9;
						continue;
					}
					provider.WriteInt16(0, (short)this.m_iCode);
					provider.WriteInt16(2, (short)this.m_iLength);
					num2 = this.m_iLength + 4;
					num = 4;
					continue;
				case 4:
					if (this.m_iLength > 0)
					{
						num = 10;
						continue;
					}
					goto IL_59;
				case 5:
					goto IL_59;
				case 7:
					if (this.NeedDecoding)
					{
						num = 2;
						continue;
					}
					goto IL_1B5;
				case 8:
					num = 7;
					continue;
				case 9:
					goto IL_C2;
				case 10:
					this.InfillInternalData(provider, 4, ExcelVersion.Version97to2003);
					goto IL_89;
				case 11:
					if (encryptor != null)
					{
						num = 8;
						continue;
					}
					goto IL_1B5;
				}
				if (writer == null)
				{
					num = 0;
					continue;
				}
				this.m_iLength = this.GetStoreSize(ExcelVersion.Version97to2003);
				num = 3;
				continue;
				IL_59:
				arrBuffer = ((spr\u24E5)provider).ᜅ();
				num = 11;
				continue;
				IL_89:
				num = 5;
				continue;
				IL_1B5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_89;
				default:
					goto IL_1CB;
				}
			}
			IL_54:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰺似嘾㕀♂㝄", a_));
			IL_C2:
			if (true)
			{
			}
			throw new ApplicationException(RecordTableEnumerator.b("氺似倾⽀⑂敄ᕆⱈ⡊≌㵎㕐獒ㅔ㙖ⵘ㩚絜㙞འբ౤୦ը䕪䵬", a_) + this.TypeCode.ToString());
			IL_1CB:
			if (false)
			{
			}
			provider.WriteInto(writer, 0, num2, arrBuffer);
			return num2;
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x00150674 File Offset: 0x0014F674
		public virtual void UpdateOffsets(List<BiffRecordRaw> records)
		{
			int a_ = 1;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ApplicationException(RecordTableEnumerator.b("琶唸娺丼䰾慀⹂⑄㕆≈⹊⥌潎ぐ⁒畔㡖㽘㵚⹜㩞ᕠ䍢٤ࡦݨὪ౬ٮὰr啴ᅶၸṺᅼ᭾ꆀꦈ뎒ﮔ뮚캠햢첤쎦첨讪슬\ud9ae풰솲잴\udeb6\uddb8\udeba鶼킾Ꟁ郄럆귈꫊만꫎黐뗒돔ꓖ볘꿚﷜닞蓠韢跤裦跨엪췬ꃮ菰폲賴飶賸\udbfa觼跾砀⌂焄栆⤈栊氌挎紐㌒攔瘖欘縚猜欞Ġ䀢䤤䘦娨堪ബ央堰䄲䄴䈶堸场ᴼ刾⑀㝂ⵄ⡆ⵈ敊浌὎㵐㙒㑔⑖㱘筚㹜㝞Ѡb๤䝦੨Ѫ६੮彰", a_));
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x001506CC File Offset: 0x0014F6CC
		public virtual void ParseStructure(DataProvider arrData, int iOffset, int iLength, ExcelVersion version)
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
			throw new NotImplementedException(this.TypeCode.ToString());
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x0015071C File Offset: 0x0014F71C
		public virtual void InfillInternalData(DataProvider provider, int iOffset, ExcelVersion version)
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
			throw new NotImplementedException(this.TypeCode.ToString());
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x0015076C File Offset: 0x0014F76C
		public virtual int GetStoreSize(ExcelVersion version)
		{
			int a_ = 7;
			int minimumRecordSize;
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
				minimumRecordSize = this.MinimumRecordSize;
				if (minimumRecordSize != this.MaximumRecordSize)
				{
					throw new ApplicationException(RecordTableEnumerator.b("渼䬾⹀ㅂ⁄ᑆ⁈ㅊ⡌潎≐㭒㩔≖㕘㽚絜㵞Ѡ䍢੤ᅦ౨ᥪŬnၰᝲၴ፶奸", a_) + this.TypeCode.ToString());
				}
				break;
			}
			return minimumRecordSize;
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x001507EC File Offset: 0x0014F7EC
		public static void CheckOffsetAndLength(byte[] arrData, int offset, int length)
		{
			int a_ = 5;
			for (;;)
			{
				int num = arrData.Length;
				int num2 = 3;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						num2 = 4;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B3;
						default:
							if (false)
							{
							}
							if (offset > num)
							{
								num2 = 7;
								continue;
							}
							num2 = 9;
							continue;
						}
						break;
					case 2:
						num2 = 1;
						continue;
					case 3:
						if (offset >= 0)
						{
							num2 = 2;
							continue;
						}
						goto IL_9A;
					case 4:
						if (length > num)
						{
							num2 = 5;
							continue;
						}
						num2 = 8;
						continue;
					case 5:
						goto IL_98;
					case 6:
						goto IL_129;
					case 7:
						goto IL_10B;
					case 8:
						if (length + offset > num)
						{
							num2 = 6;
							continue;
						}
						return;
					case 9:
						if (length >= 0)
						{
							num2 = 0;
							continue;
						}
						goto IL_5B;
					}
					break;
				}
			}
			IL_5B:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("场堼儾♀㝂ⵄ", a_), "");
			IL_98:
			goto IL_5B;
			IL_9A:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("吺嬼夾㉀♂ㅄ", a_), "");
			IL_B3:
			throw new ArgumentException(RecordTableEnumerator.b("眺堼儾♀㝂ⵄ杆♈㥊浌⁎㝐㕒♔㉖ⵘ筚㕜㹞በ䍢ቤᕦ٨ժ੬佮ݰቲᥴɶᱸ啺", a_), RecordTableEnumerator.b("场堼儾♀㝂ⵄ杆潈歊≌⥎㝐⁒ご⍖", a_));
			IL_10B:
			goto IL_9A;
			IL_129:
			goto IL_B3;
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x00150948 File Offset: 0x0014F948
		public static byte[] GetBytes(byte[] arrData, int offset, int length)
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
			BiffRecordRaw.CheckOffsetAndLength(arrData, offset, length);
			byte[] array = new byte[length];
			Buffer.BlockCopy(arrData, offset, array, 0, length);
			return array;
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x001509A0 File Offset: 0x0014F9A0
		public static byte GetByte(byte[] arrData, int offset)
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
			BiffRecordRaw.CheckOffsetAndLength(arrData, offset, 1);
			return arrData[offset];
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x001509E8 File Offset: 0x0014F9E8
		[CLSCompliant(false)]
		public static ushort GetUInt16(byte[] arrData, int offset)
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
			BiffRecordRaw.CheckOffsetAndLength(arrData, offset, 2);
			return BitConverter.ToUInt16(arrData, offset);
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x00150A34 File Offset: 0x0014FA34
		[CLSCompliant(false)]
		public static short GetInt16(byte[] arrData, int offset)
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
			BiffRecordRaw.CheckOffsetAndLength(arrData, offset, 2);
			return BitConverter.ToInt16(arrData, offset);
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x00150A80 File Offset: 0x0014FA80
		public static int GetInt32(byte[] arrData, int offset)
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
			BiffRecordRaw.CheckOffsetAndLength(arrData, offset, 4);
			return BitConverter.ToInt32(arrData, offset);
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x00150ACC File Offset: 0x0014FACC
		[CLSCompliant(false)]
		public static uint GetUInt32(byte[] arrData, int offset)
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
			BiffRecordRaw.CheckOffsetAndLength(arrData, offset, 4);
			return BitConverter.ToUInt32(arrData, offset);
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x00150B18 File Offset: 0x0014FB18
		public static long GetInt64(byte[] arrData, int offset)
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
			BiffRecordRaw.CheckOffsetAndLength(arrData, offset, 28);
			return BitConverter.ToInt64(arrData, offset);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x00150B64 File Offset: 0x0014FB64
		[CLSCompliant(false)]
		public static ulong GetUInt64(byte[] arrData, int offset)
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
			BiffRecordRaw.CheckOffsetAndLength(arrData, offset, 8);
			return BitConverter.ToUInt64(arrData, offset);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x00150BB0 File Offset: 0x0014FBB0
		public static float GetFloat(byte[] arrData, int offset)
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
			BiffRecordRaw.CheckOffsetAndLength(arrData, offset, 4);
			return BitConverter.ToSingle(arrData, offset);
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x00150BFC File Offset: 0x0014FBFC
		public static double GetDouble(byte[] arrData, int offset)
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
			BiffRecordRaw.CheckOffsetAndLength(arrData, offset, 8);
			return BitConverter.ToDouble(arrData, offset);
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x00150C48 File Offset: 0x0014FC48
		public static bool GetBit(byte[] arrData, int offset, int bitPos)
		{
			int a_ = 19;
			for (;;)
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_62;
					case 2:
						num = 3;
						continue;
					case 3:
						if (bitPos > 7)
						{
							num = 5;
							continue;
						}
						num = 4;
						continue;
					case 4:
						if (arrData.Length <= offset)
						{
							num = 0;
							continue;
						}
						goto IL_DA;
					case 5:
						goto IL_C4;
					}
					if (bitPos < 0)
					{
						goto IL_88;
					}
					num = 2;
				}
				IL_62:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_78;
				}
			}
			IL_78:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("♈ⵊ⭌㱎㑐❒", a_));
			IL_88:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭈≊㥌὎㹐⁒", a_), RecordTableEnumerator.b("ୈ≊㥌潎Ő㱒♔㹖ⵘ㉚㉜ㅞ䅠bѤ०ݨѪᥬ佮፰ᙲ啴᭶ᱸࡺ๼彾ꦈ뮊권뎒ﲘ漢爵펠莢톤쾦좨얪趬颮龰", a_));
			IL_C4:
			goto IL_88;
			IL_DA:
			return ((int)arrData[offset] & 1 << bitPos) == 1 << bitPos;
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x00150D44 File Offset: 0x0014FD44
		public static bool GetBit(IntPtr ptrData, int offset, int bitPos)
		{
			int a_ = 12;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (bitPos > 7)
					{
						num = 3;
						continue;
					}
					goto IL_9D;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_41;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 3:
					goto IL_9B;
				}
				if (bitPos < 0)
				{
					break;
				}
				num = 1;
			}
			IL_41:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⁁ⵃ㉅ᡇ╉㽋", a_), RecordTableEnumerator.b("Aⵃ㉅桇ᩉ⍋㵍㥏♑㵓㥕㙗穙㽛㽝๟ౡୣብ䡧ࡩ५乭ᱯ᝱ݳյ塷๹ᑻώꊁ뒃ꚅ겋ﶗ벛좟쎡쪣蚥龧蒩", a_));
			IL_9B:
			goto IL_41;
			IL_9D:
			byte b = Marshal.ReadByte(ptrData, offset);
			return ((int)b & 1 << bitPos) == 1 << bitPos;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x00150E08 File Offset: 0x0014FE08
		public static string GetString16BitUpdateOffset(byte[] arrData, ref int offset)
		{
			int @uint;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				@uint = (int)BiffRecordRaw.GetUInt16(arrData, offset);
				offset += 2;
				if (@uint <= 0)
				{
					return string.Empty;
				}
				if (true)
				{
				}
				break;
			}
			int num;
			string @string = BiffRecordRaw.GetString(arrData, offset, @uint, out num);
			offset += num;
			return @string;
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x00150E74 File Offset: 0x0014FE74
		public static string GetStringUpdateOffset(byte[] arrData, ref int offset, int iStrLen)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_34;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			if (iStrLen <= 0)
			{
				return string.Empty;
			}
			IL_34:
			int num;
			string @string = BiffRecordRaw.GetString(arrData, offset, iStrLen, out num);
			offset += num;
			return @string;
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x00150ED0 File Offset: 0x0014FED0
		public static string GetStringByteLen(byte[] arrData, int offset)
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
			int @byte = (int)BiffRecordRaw.GetByte(arrData, offset);
			return BiffRecordRaw.GetString(arrData, offset + 1, @byte);
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x00150F20 File Offset: 0x0014FF20
		public static string GetString(byte[] arrData, int offset, int iStrLen)
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
			int num;
			return BiffRecordRaw.GetString(arrData, offset, iStrLen, out num);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x00150F68 File Offset: 0x0014FF68
		public static string GetString(byte[] arrData, int offset, int iStrLen, out int iBytesInString, bool isByteCounted)
		{
			int a_ = 2;
			for (;;)
			{
				byte @byte = BiffRecordRaw.GetByte(arrData, offset);
				int num = 4;
				for (;;)
				{
					int num2;
					int num3;
					switch (num)
					{
					case 0:
						num2 = 2 * iStrLen;
						goto IL_110;
					case 1:
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						if (isByteCounted)
						{
							num = 3;
							continue;
						}
						num = 0;
						continue;
					case 3:
						goto IL_B6;
					case 4:
						if (@byte != 0)
						{
							num = 1;
							continue;
						}
						goto IL_B6;
					case 5:
						if (num3 > arrData.Length)
						{
							num = 6;
							continue;
						}
						num = 10;
						continue;
					case 6:
						goto IL_136;
					case 7:
						goto IL_6E;
					case 8:
						goto IL_F6;
					case 9:
						iBytesInString = iStrLen;
						num = 8;
						continue;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_136;
						default:
							if (false)
							{
							}
							if (@byte == 0)
							{
								num = 9;
								continue;
							}
							num = 7;
							continue;
						}
						break;
					case 11:
						num2 = iStrLen;
						goto IL_110;
					}
					break;
					IL_B6:
					num = 11;
					continue;
					IL_110:
					num3 = num2;
					num3 += offset + 1;
					num = 5;
				}
			}
			IL_6E:
			iBytesInString = (isByteCounted ? iStrLen : (iStrLen * 2));
			return Encoding.Unicode.GetString(BiffRecordRaw.GetBytes(arrData, offset + 1, iBytesInString), 0, iBytesInString);
			IL_F6:
			return BiffRecordRaw.LatinEncoding.GetString(BiffRecordRaw.GetBytes(arrData, offset + 1, iStrLen), 0, iStrLen);
			IL_136:
			throw new sprῩ(string.Format(RecordTableEnumerator.b("欷丹主圽⸿╁摃❅♇⹉汋⽍≏⁑ၓ㝕ⱗ㭙籛㽝቟ၡգὥ䡧๩ͫ乭ṯᵱs噵ṷ፹ࡻ幽ꢇ뒓ꢗ늛", a_), new object[0]));
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x001510F8 File Offset: 0x001500F8
		public static string GetUnkTypeString(byte[] arrData, int offset, int[] continuePos, out int length, out byte[] rich, out byte[] extended)
		{
			switch (0)
			{
			default:
			{
				string text;
				int num;
				for (;;)
				{
					text = string.Empty;
					num = 3;
					rich = null;
					extended = null;
					ushort @uint = BiffRecordRaw.GetUInt16(arrData, offset);
					byte @byte = BiffRecordRaw.GetByte(arrData, offset + 2);
					bool flag = (@byte & 1) == 1;
					int num2 = 37;
					for (;;)
					{
						int num4;
						int num3;
						bool flag2;
						int num5;
						bool flag3;
						int num6;
						int num7;
						bool flag4;
						bool flag5;
						bool flag6;
						short num8;
						int num9;
						bool flag7;
						switch (num2)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_214;
							default:
								if (false)
								{
								}
								num2 = 42;
								continue;
							}
							break;
						case 1:
							num3 = ((int)@uint - num4) * 2;
							goto IL_369;
						case 2:
							num2 = 18;
							continue;
						case 3:
							goto IL_523;
						case 4:
							num2 = 17;
							continue;
						case 5:
							num2 = 11;
							continue;
						case 6:
							flag2 = (@byte == 9);
							goto IL_14C;
						case 7:
							if (num5 > 0)
							{
								num2 = 2;
								continue;
							}
							goto IL_49E;
						case 8:
							goto IL_4E5;
						case 9:
							flag2 = true;
							goto IL_14C;
						case 10:
							if (flag3)
							{
								num2 = 35;
								continue;
							}
							goto IL_588;
						case 11:
							text += (flag ? Encoding.Unicode.GetString(BiffRecordRaw.GetBytes(arrData, num6, num7), 0, num7) : BiffRecordRaw.LatinEncoding.GetString(BiffRecordRaw.GetBytes(arrData, num6, num7), 0, num7));
							num += num7;
							num2 = 15;
							continue;
						case 12:
							if (flag4)
							{
								num2 = 33;
								continue;
							}
							if (true)
							{
							}
							num2 = 26;
							continue;
						case 13:
							goto IL_214;
						case 14:
							num2 = 20;
							continue;
						case 15:
							goto IL_545;
						case 16:
							flag5 = true;
							goto IL_18F;
						case 17:
							flag5 = (@byte == 5);
							goto IL_18F;
						case 18:
							text += (flag ? Encoding.Unicode.GetString(BiffRecordRaw.GetBytes(arrData, num6, num5), 0, num5) : BiffRecordRaw.LatinEncoding.GetString(BiffRecordRaw.GetBytes(arrData, num6, num5), 0, num5));
							num2 = 25;
							continue;
						case 19:
							num3 = (int)@uint - num4;
							goto IL_369;
						case 20:
							if (arrData[num6 + num5] == 1)
							{
								num2 = 8;
								continue;
							}
							goto IL_509;
						case 21:
							goto IL_308;
						case 22:
							goto IL_545;
						case 23:
							if (@byte != 4)
							{
								num2 = 4;
								continue;
							}
							num2 = 16;
							continue;
						case 24:
							if (num7 <= num5)
							{
								num2 = 5;
								continue;
							}
							num2 = 7;
							continue;
						case 25:
							num4 += (flag ? (num5 / 2) : num5);
							num2 = 32;
							continue;
						case 26:
							if (flag6)
							{
								num2 = 27;
								continue;
							}
							goto IL_308;
						case 27:
						{
							num8 = BiffRecordRaw.GetInt16(arrData, offset + 3);
							int @int = BiffRecordRaw.GetInt32(arrData, offset + 5);
							num9 = 9;
							num += 6;
							rich = BiffRecordRaw.GetBytes(arrData, num, (int)(num8 * 4));
							num += (int)(num8 * 4);
							extended = BiffRecordRaw.GetBytes(arrData, num, @int);
							num += @int;
							num2 = 34;
							continue;
						}
						case 28:
							goto IL_586;
						case 29:
							goto IL_308;
						case 30:
							goto IL_509;
						case 31:
							if (num4 >= (int)@uint)
							{
								num2 = 22;
								continue;
							}
							num2 = 39;
							continue;
						case 32:
							goto IL_49E;
						case 33:
						{
							int int2 = BiffRecordRaw.GetInt32(arrData, offset + 3);
							num9 = 7;
							rich = null;
							num += 4;
							extended = BiffRecordRaw.GetBytes(arrData, num, int2);
							num += int2;
							num2 = 21;
							continue;
						}
						case 34:
							goto IL_308;
						case 35:
							rich = BiffRecordRaw.GetBytes(arrData, offset + num, (int)(num8 * 4));
							num += (int)(num8 * 4);
							num2 = 28;
							continue;
						case 36:
							num2 = 19;
							continue;
						case 37:
							if (@byte != 8)
							{
								num2 = 43;
								continue;
							}
							num2 = 9;
							continue;
						case 38:
							if (@byte != 12)
							{
								num2 = 0;
								continue;
							}
							num2 = 44;
							continue;
						case 39:
							if (!flag)
							{
								num2 = 36;
								continue;
							}
							num2 = 1;
							continue;
						case 40:
							if (flag3)
							{
								num2 = 13;
								continue;
							}
							num2 = 12;
							continue;
						case 41:
							if (arrData[num6 + num5] != 0)
							{
								num2 = 14;
								continue;
							}
							goto IL_4E5;
						case 42:
							flag7 = (@byte == 13);
							goto IL_1F0;
						case 43:
							num2 = 6;
							continue;
						case 44:
							flag7 = true;
							goto IL_1F0;
						case 45:
							goto IL_523;
						}
						break;
						IL_14C:
						flag3 = flag2;
						num2 = 23;
						continue;
						IL_18F:
						flag4 = flag5;
						num2 = 38;
						continue;
						IL_1F0:
						flag6 = flag7;
						num9 = 3;
						num8 = 0;
						num2 = 40;
						continue;
						IL_214:
						num8 = BiffRecordRaw.GetInt16(arrData, offset + 3);
						num9 = 5;
						extended = null;
						num += 2;
						num2 = 29;
						continue;
						IL_308:
						num6 = offset + num9;
						num4 = 0;
						int num10 = 0;
						num2 = 3;
						continue;
						IL_369:
						num7 = num3;
						int num11 = BiffRecordRaw.FindNextBreak(continuePos, continuePos.Length, num6, ref num10);
						num5 = num11 - num6;
						num2 = 24;
						continue;
						IL_49E:
						num2 = 41;
						continue;
						IL_4E5:
						flag = (arrData[num6 + num5] == 1);
						num6++;
						num++;
						num2 = 30;
						continue;
						IL_509:
						num6 += num5;
						num += num5;
						num2 = 45;
						continue;
						IL_523:
						num2 = 31;
						continue;
						IL_545:
						num2 = 10;
					}
				}
				IL_586:
				IL_588:
				length = num;
				return text;
			}
			}
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x00151694 File Offset: 0x00150694
		[CLSCompliant(false)]
		public static TAddr GetAddr(byte[] arrData, int offset)
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
			return new TAddr
			{
				FirstRow = (int)BiffRecordRaw.GetUInt16(arrData, offset),
				LastRow = (int)BiffRecordRaw.GetUInt16(arrData, offset + 2),
				FirstCol = (int)BiffRecordRaw.GetUInt16(arrData, offset + 4),
				LastCol = (int)BiffRecordRaw.GetUInt16(arrData, offset + 6)
			};
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x00151718 File Offset: 0x00150718
		public static byte[] GetRPNData(byte[] arrData, int offset, int length)
		{
			switch (0)
			{
			default:
			{
				int num = 8;
				List<byte> list;
				for (;;)
				{
					byte b;
					int num2;
					int num4;
					int num5;
					switch (num)
					{
					case 0:
						if (b == 96)
						{
							num = 12;
							continue;
						}
						goto IL_2C0;
					case 1:
						num = 4;
						continue;
					case 2:
						num2 += 3;
						num = 20;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_82;
						}
						if (false)
						{
						}
						goto IL_1D7;
					case 4:
						if (b != 64)
						{
							num = 13;
							continue;
						}
						goto IL_248;
					case 5:
					{
						int num3 = offset + num4;
						num = 6;
						continue;
					}
					case 6:
						goto IL_1D7;
					case 7:
					{
						if (num5 <= 0)
						{
							num = 5;
							continue;
						}
						byte @byte = BiffRecordRaw.GetByte(arrData, offset + length + num2);
						num = 15;
						continue;
					}
					case 9:
						num2 += (int)(BiffRecordRaw.GetInt16(arrData, offset + length + num2 + 1) + 4);
						num = 17;
						continue;
					case 10:
						if (true)
						{
						}
						goto IL_90;
					case 11:
						if (b != 32)
						{
							num = 1;
							continue;
						}
						goto IL_248;
					case 12:
						goto IL_248;
					case 13:
						num = 0;
						continue;
					case 14:
						goto IL_1FC;
					case 15:
					{
						byte @byte;
						if (@byte == 2)
						{
							num = 9;
							continue;
						}
						num = 19;
						continue;
					}
					case 16:
						goto IL_8B;
					case 17:
						goto IL_170;
					case 18:
					{
						int num3;
						if (num3 >= offset + length + num2)
						{
							num = 14;
							continue;
						}
						list.Add(arrData[num3]);
						num3++;
						num = 3;
						continue;
					}
					case 19:
					{
						byte @byte;
						if (@byte == 4)
						{
							num = 2;
							continue;
						}
						num2 += 9;
						num = 21;
						continue;
					}
					case 20:
						goto IL_170;
					case 21:
						goto IL_170;
					case 22:
						goto IL_90;
					}
					goto IL_7C;
					IL_82:
					num = 16;
					continue;
					IL_7C:
					if (length == 0)
					{
						goto IL_82;
					}
					list = new List<byte>(length * 2);
					num4 = 0;
					byte byte2 = BiffRecordRaw.GetByte(arrData, offset + num4);
					b = byte2;
					num = 11;
					continue;
					IL_90:
					num = 7;
					continue;
					IL_170:
					num5--;
					num = 22;
					continue;
					IL_1D7:
					num = 18;
					continue;
					IL_248:
					num5 = (int)((short)(BiffRecordRaw.GetByte(arrData, offset + num4 + 1) + 1) * (BiffRecordRaw.GetInt16(arrData, offset + num4 + 2) + 1) + 1);
					num2 = 0;
					num = 10;
				}
				IL_8B:
				return new byte[0];
				IL_1FC:
				return list.ToArray();
				IL_2C0:
				return BiffRecordRaw.GetBytes(arrData, offset, length);
			}
			}
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x001519F0 File Offset: 0x001509F0
		protected static int FindNextBreak(IList<int> arrBreaks, int iCount, int curPos, ref int iStartIndex)
		{
			int num;
			int num3;
			for (;;)
			{
				num = iStartIndex;
				int num2 = 5;
				for (;;)
				{
					IL_02:
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						goto IL_8F;
					case 1:
						return -1;
					case 2:
						if (num >= iCount)
						{
							num2 = 1;
							continue;
						}
						num3 = arrBreaks[num];
						num2 = 4;
						continue;
					case 3:
						goto IL_8D;
					case 4:
						while (curPos > num3)
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
								num++;
								num2 = 0;
								goto IL_02;
							}
						}
						num2 = 3;
						continue;
					case 5:
						goto IL_8F;
					}
					break;
					IL_8F:
					num2 = 2;
				}
			}
			IL_8D:
			iStartIndex = num;
			return num3;
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x00151AAC File Offset: 0x00150AAC
		[CLSCompliant(false)]
		public static void SetUInt16(byte[] arrData, int offset, ushort value)
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
			byte b = (byte)(value & 255);
			byte b2 = (byte)(value >> 8 & 255);
			arrData[offset] = b;
			arrData[offset + 1] = b2;
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x00151B08 File Offset: 0x00150B08
		public static void SetBit(byte[] arrData, int offset, bool value, int bitPos)
		{
			int a_ = 3;
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5E;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 1:
					goto IL_4E;
				case 2:
					if (bitPos > 7)
					{
						num = 4;
						continue;
					}
					num = 6;
					continue;
				case 3:
					goto IL_66;
				case 4:
					goto IL_7C;
				case 5:
					if (bitPos >= 0)
					{
						num = 0;
						continue;
					}
					goto IL_E6;
				case 6:
					if (value)
					{
						goto IL_5E;
					}
					goto IL_108;
				}
				if (arrData == null)
				{
					num = 1;
					continue;
				}
				num = 5;
				continue;
				IL_5E:
				num = 3;
			}
			IL_4E:
			throw new ArgumentNullException(RecordTableEnumerator.b("堸䤺似笾⁀㝂⑄", a_));
			IL_66:
			arrData[offset] |= (byte)(1 << bitPos);
			return;
			IL_7C:
			IL_E6:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("嬸刺䤼漾⹀あ", a_), RecordTableEnumerator.b("笸刺䤼Ἶᅀⱂ㙄⹆㵈≊≌ⅎ煐げ㑔㥖祘㥚㡜罞᭠٢ᝤࡦ䥨ѪὬ佮ᙰŲၴᙶ൸Ṻོ彾ꦈ벊ꎌ", a_));
			IL_108:
			if (true)
			{
			}
			arrData[offset] &= (byte)(~(byte)(1 << bitPos));
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x00151C44 File Offset: 0x00150C44
		public static void SetInt16(byte[] arrData, int offset, short value)
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
			Buffer.BlockCopy(BitConverter.GetBytes(value), 0, arrData, offset, 2);
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x00151C90 File Offset: 0x00150C90
		public static void SetInt32(byte[] arrData, int offset, int value)
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
			Buffer.BlockCopy(BitConverter.GetBytes(value), 0, arrData, offset, 4);
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x00151CDC File Offset: 0x00150CDC
		[CLSCompliant(false)]
		public static void SetUInt32(byte[] arrData, int offset, uint value)
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
			Buffer.BlockCopy(BitConverter.GetBytes(value), 0, arrData, offset, 4);
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x00151D28 File Offset: 0x00150D28
		public static void SetDouble(byte[] arrData, int offset, double value)
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
			Buffer.BlockCopy(BitConverter.GetBytes(value), 0, arrData, offset, 8);
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x00151D74 File Offset: 0x00150D74
		public static void SetStringNoLenUpdateOffset(byte[] arrData, ref int offset, string value)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_6C;
				case 1:
					if (value.Length == 0)
					{
						num = 0;
						continue;
					}
					goto IL_76;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_76;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				if (value == null)
				{
					break;
				}
				num = 2;
			}
			return;
			IL_6C:
			if (true)
			{
			}
			return;
			IL_76:
			byte[] bytes = Encoding.Unicode.GetBytes(value);
			arrData[offset] = 1;
			BiffRecordRaw.SetBytes(arrData, offset + 1, bytes, 0, bytes.Length);
			offset += bytes.Length + 1;
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x00151E24 File Offset: 0x00150E24
		public static void SetStringByteLen(byte[] arrData, int offset, string value)
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
			arrData[offset] = (byte)value.Length;
			BiffRecordRaw.SetStringNoLen(arrData, offset + 1, value);
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x00151E74 File Offset: 0x00150E74
		protected internal static void SetBytes(byte[] arrBuffer, int offset, byte[] value, int pos, int length)
		{
			int a_ = 16;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_4C;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					goto IL_92;
				case 3:
					if (pos >= 0)
					{
						num = 4;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8A;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 4:
					if (length < 0)
					{
						goto IL_8A;
					}
					num = 7;
					continue;
				case 5:
					goto IL_121;
				case 6:
					goto IL_CA;
				case 7:
					if (pos + length > value.Length)
					{
						num = 5;
						continue;
					}
					goto IL_145;
				}
				if (value == null)
				{
					num = 0;
					continue;
				}
				num = 3;
				continue;
				IL_8A:
				num = 2;
			}
			IL_4C:
			throw new ArgumentNullException(RecordTableEnumerator.b("ぅ⥇♉㥋⭍", a_));
			IL_92:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⩅ⵇ⑉⭋㩍㡏", a_), RecordTableEnumerator.b("੅ⵇ⑉⭋㩍㡏牑㭓さ硗㹙㵛⩝ş䉡ၣ॥䡧३ͫṭ९剱ᥳ͵୷๹屻ᱽꊁ늑ﺕﶗ벛얟킡쮣袥", a_));
			IL_CA:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㙅❇㥉", a_), RecordTableEnumerator.b("ᙅ❇㥉╋㩍㥏㵑㩓癕㭗㭙㉛そཟᙡ䑣ѥ൧䩩ᙫ୭ɯᵱᡳ፵୷ॹ剻", a_));
			IL_121:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ぅ⥇♉㥋⭍", a_), RecordTableEnumerator.b("ᙅ❇㥉╋㩍㥏㵑㩓癕㝗⡙籛㉝՟ౡͣብg䩩ѫ཭ͯ剱ͳѵ᝷ᑹ᭻幽ꒉ", a_));
			IL_145:
			Buffer.BlockCopy(value, pos, arrBuffer, offset, length);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x00151FD4 File Offset: 0x00150FD4
		[CLSCompliant(false)]
		protected internal void SetBitInVar(ref ushort variable, bool value, int bitPos)
		{
			int a_ = 10;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (true)
					{
					}
					if (value)
					{
						num = 5;
						continue;
					}
					goto IL_CF;
				case 2:
					if (bitPos > 15)
					{
						num = 4;
						continue;
					}
					num = 1;
					continue;
				case 3:
					num = 2;
					continue;
				case 4:
					goto IL_BF;
				case 5:
					goto IL_76;
				}
				if (bitPos < 0)
				{
					goto IL_78;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_76;
				default:
					if (false)
					{
					}
					num = 3;
					break;
				}
			}
			IL_76:
			variable |= (ushort)(1 << bitPos);
			return;
			IL_78:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("∿⭁ぃᙅ❇㥉", a_), RecordTableEnumerator.b("ȿ⭁ぃ晅ᡇ╉㽋❍⑏㭑㭓㡕硗㥙㵛そ䁟aţ䙥ቧཀྵṫŭ偯ᵱٳ噵ίࡹ᥻ώꚅﲇ낏ꖑ몓", a_));
			IL_BF:
			goto IL_78;
			IL_CF:
			variable &= (ushort)(~(ushort)(1 << bitPos));
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x001520C0 File Offset: 0x001510C0
		[CLSCompliant(false)]
		protected internal void SetBitInVar(ref uint variable, bool value, int bitPos)
		{
			int a_ = 11;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 2:
					goto IL_BF;
				case 3:
					if (value)
					{
						num = 4;
						continue;
					}
					goto IL_CD;
				case 4:
					goto IL_6E;
				case 5:
					if (true)
					{
					}
					if (bitPos > 31)
					{
						num = 2;
						continue;
					}
					num = 3;
					continue;
				}
				if (bitPos < 0)
				{
					goto IL_70;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6E;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
			}
			IL_6E:
			variable |= 1U << bitPos;
			return;
			IL_70:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⍀⩂ㅄᝆ♈㡊", a_), RecordTableEnumerator.b("̀⩂ㅄ杆᥈⑊㹌♎═㩒㩔㥖祘㡚㱜ㅞ䅠Ţd䝦፨๪Ὤn兰ᱲݴ坶Ṹॺ᡼ṾꞆﶈ놐꒒뮔", a_));
			IL_BF:
			goto IL_70;
			IL_CD:
			variable &= ~(1U << bitPos);
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x001521A8 File Offset: 0x001511A8
		public int Get16BitStringSize(string strValue, bool isCompressed)
		{
			int num = 5;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (strValue.Length == 0)
					{
						num = 4;
						continue;
					}
					num = 7;
					continue;
				case 1:
					goto IL_6C;
				case 2:
					goto IL_C0;
				case 3:
					goto IL_93;
				case 4:
					goto IL_B3;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_93;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				case 7:
					if (!isCompressed)
					{
						num = 3;
						continue;
					}
					num = 2;
					continue;
				}
				if (strValue != null)
				{
					num = 6;
					continue;
				}
				return 2;
				IL_93:
				num = 1;
			}
			IL_6C:
			Encoding encoding = Encoding.Unicode;
			goto IL_C9;
			IL_B3:
			return 2;
			IL_C0:
			encoding = Encoding.ASCII;
			IL_C9:
			Encoding encoding2 = encoding;
			return 3 + encoding2.GetByteCount(strValue);
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x00152288 File Offset: 0x00151288
		public virtual void ClearData()
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
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x001522C4 File Offset: 0x001512C4
		public virtual bool IsEqual(BiffRecordRaw raw)
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
			throw new NotImplementedException();
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x00152304 File Offset: 0x00151304
		public virtual void CopyTo(BiffRecordRaw raw)
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
			throw new NotImplementedException();
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x00152344 File Offset: 0x00151344
		public void CheckTypeCode(TBIFFRecord typeCode)
		{
			int a_ = 0;
			if (this.TypeCode != typeCode)
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
					throw new ArgumentOutOfRangeException(typeCode.ToString() + RecordTableEnumerator.b("ᘵ䨷弹弻儽㈿♁摃ㅅ⥇㥉汋⭍⡏≑ㅓ㕕ⱗ㽙㡛", a_));
				}
			}
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x001523B8 File Offset: 0x001513B8
		public static bool CompareArrays(byte[] array1, int iStartIndex1, byte[] array2, int iStartIndex2, int iLength)
		{
			for (;;)
			{
				int num = 0;
				int num2 = iStartIndex1;
				int num3 = iStartIndex2;
				int num4 = 5;
				for (;;)
				{
					switch (num4)
					{
					case 0:
						if (num == iLength)
						{
							num4 = 3;
							continue;
						}
						return false;
					case 1:
						if (num2 < array1.Length)
						{
							num4 = 11;
							continue;
						}
						goto IL_53;
					case 2:
						if (array1[num2] != array2[num3])
						{
							num4 = 8;
							continue;
						}
						num++;
						num2++;
						num3++;
						num4 = 6;
						continue;
					case 3:
						num4 = 12;
						continue;
					case 4:
						if (num < iLength)
						{
							num4 = 9;
							continue;
						}
						goto IL_53;
					case 5:
						goto IL_119;
					case 6:
						goto IL_119;
					case 7:
						goto IL_10A;
					case 8:
						return false;
					case 9:
						num4 = 1;
						continue;
					case 10:
						goto IL_C2;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_10A;
						default:
							if (false)
							{
							}
							num4 = 13;
							continue;
						}
						break;
					case 12:
						if (num != 0)
						{
							num4 = 10;
							continue;
						}
						return false;
					case 13:
						if (num3 >= array2.Length)
						{
							num4 = 7;
							continue;
						}
						num4 = 2;
						continue;
					}
					break;
					IL_53:
					num4 = 0;
					continue;
					IL_119:
					num4 = 4;
					continue;
					IL_10A:
					goto IL_53;
				}
			}
			IL_C2:
			if (true)
			{
			}
			return true;
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x0015251C File Offset: 0x0015151C
		public static bool CompareArrays(byte[] array1, byte[] array2)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 11;
					continue;
				case 1:
					if (array2 == null)
					{
						goto IL_E3;
					}
					goto IL_112;
				case 2:
					num = 8;
					continue;
				case 3:
					if (array1 != null)
					{
						num = 2;
						continue;
					}
					return false;
				case 4:
					return true;
				case 6:
					num = 1;
					continue;
				case 7:
					return true;
				case 8:
				{
					if (array2 == null)
					{
						num = 13;
						continue;
					}
					if (true)
					{
					}
					int num2 = array1.Length;
					int num3 = array2.Length;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E3;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				}
				case 9:
				{
					int num2;
					int num3;
					if (num2 != num3)
					{
						num = 10;
						continue;
					}
					num = 12;
					continue;
				}
				case 10:
					return false;
				case 11:
				{
					int num3;
					if (num3 == 0)
					{
						num = 7;
						continue;
					}
					goto IL_139;
				}
				case 12:
				{
					int num2;
					if (num2 == 0)
					{
						num = 0;
						continue;
					}
					goto IL_139;
				}
				case 13:
					return false;
				}
				if (array1 == null)
				{
					num = 6;
					continue;
				}
				goto IL_112;
				IL_E3:
				num = 4;
				continue;
				IL_112:
				num = 3;
			}
			return false;
			IL_139:
			return BiffRecordRaw.CompareArrays(array1, 0, array2, 0, array1.Length);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x00152670 File Offset: 0x00151670
		internal void \u1717(int A_0)
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
			this.m_iCode = A_0;
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x001526B4 File Offset: 0x001516B4
		public virtual object Clone()
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
			return base.MemberwiseClone();
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06002459 RID: 9305 RVA: 0x001526F8 File Offset: 0x001516F8
		public static Encoding LatinEncoding
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
				return BiffRecordRaw.ᜄ;
			}
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x00152738 File Offset: 0x00151738
		public static byte[] CombineArrays(int iCombinedLength, List<byte[]> arrCombined)
		{
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_EE;
					case 1:
					{
						if (arrCombined.Count == 0)
						{
							num = 0;
							continue;
						}
						int count = arrCombined.Count;
						byte[] array = new byte[iCombinedLength];
						int num2 = 0;
						int num3 = 0;
						num = 5;
						continue;
					}
					case 2:
						goto IL_90;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_90;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 4:
					{
						byte[] array;
						return array;
					}
					case 5:
						goto IL_F0;
					case 6:
					{
						int count;
						int num3;
						if (num3 >= count)
						{
							num = 4;
							continue;
						}
						byte[] array2 = arrCombined[num3];
						int num4 = array2.Length;
						byte[] array;
						int num2;
						Buffer.BlockCopy(array2, 0, array, num2, num4);
						num2 += num4;
						num3++;
						num = 2;
						continue;
					}
					case 7:
						if (true)
						{
						}
						break;
					}
					if (arrCombined != null)
					{
						num = 3;
						continue;
					}
					break;
					IL_F0:
					num = 6;
					continue;
					IL_90:
					goto IL_F0;
				}
				IL_EE:
				return new byte[0];
			}
			}
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00152860 File Offset: 0x00151860
		public static string GetString(byte[] arrData, int iOffset, int iLength, out int iReadBytes)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_180:
					num = 2;
					break;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
				Encoding encoding;
				for (;;)
				{
					int num2;
					int num3;
					switch (num)
					{
					case 0:
						goto IL_9C;
					case 2:
						if (iOffset + iLength > arrData.Length)
						{
							num = 15;
							continue;
						}
						num = 6;
						continue;
					case 3:
						goto IL_216;
					case 4:
						num2 = iLength;
						goto IL_1EE;
					case 5:
					{
						byte b;
						if (b == 0)
						{
							num = 13;
							continue;
						}
						iReadBytes = iLength * 2;
						encoding = Encoding.Unicode;
						num = 10;
						continue;
					}
					case 6:
					{
						if (iLength < 0)
						{
							num = 8;
							continue;
						}
						byte b = arrData[iOffset];
						num = 12;
						continue;
					}
					case 7:
						if (iOffset >= 0)
						{
							num = 9;
							continue;
						}
						goto IL_1AA;
					case 8:
						goto IL_F5;
					case 9:
						goto IL_180;
					case 10:
						goto IL_199;
					case 11:
						if (num3 > arrData.Length)
						{
							num = 3;
							continue;
						}
						num = 5;
						continue;
					case 12:
					{
						byte b;
						if (b == 0)
						{
							num = 16;
							continue;
						}
						num = 14;
						continue;
					}
					case 13:
						iReadBytes = iLength;
						encoding = BiffRecordRaw.LatinEncoding;
						num = 17;
						continue;
					case 14:
						num2 = 2 * iLength;
						goto IL_1EE;
					case 15:
						goto IL_162;
					case 16:
						num = 4;
						continue;
					case 17:
						goto IL_13D;
					}
					if (arrData == null)
					{
						num = 0;
						continue;
					}
					num = 7;
					continue;
					IL_1EE:
					num3 = num2;
					num3 += iOffset + 1;
					num = 11;
				}
				IL_9C:
				throw new ArgumentNullException(RecordTableEnumerator.b("堸䤺似笾⁀㝂⑄", a_));
				IL_F5:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("倸眺堼儾♀㝂ⵄ", a_));
				IL_13D:
				goto IL_218;
				IL_162:
				goto IL_1AA;
				IL_199:
				goto IL_218;
				IL_1AA:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("倸琺嬼夾㉀♂ㅄ", a_));
				IL_216:
				throw new sprῩ(string.Format(RecordTableEnumerator.b("樸伺似嘾⽀⑂敄♆❈⽊浌≎๐㝒㑔⍖㡘筚㱜ⵞ፠ɢᱤ䝦൨Ѫ䵬ŮṰݲ啴ᅶၸེ嵼᩾Ꞇﾊ", a_), new object[0]));
				IL_218:
				if (true)
				{
				}
				string @string = encoding.GetString(arrData, iOffset + 1, iReadBytes);
				iReadBytes++;
				return @string;
			}
			}
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x00152AAC File Offset: 0x00151AAC
		public static int SetStringNoLen(byte[] arrData, int iOffset, string strValue)
		{
			int a_ = 13;
			int num = 6;
			byte[] bytes;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						if (iOffset >= 0)
						{
							num = 5;
							continue;
						}
						goto IL_AA;
					}
					break;
				case 1:
					num = 9;
					continue;
				case 2:
					if (arrData == null)
					{
						num = 7;
						continue;
					}
					bytes = Encoding.Unicode.GetBytes(strValue);
					num = 0;
					continue;
				case 3:
					goto IL_10A;
				case 4:
					if (iOffset + bytes.Length + 1 > arrData.Length)
					{
						num = 3;
						continue;
					}
					goto IL_12D;
				case 5:
					num = 4;
					continue;
				case 7:
					goto IL_128;
				case 8:
					goto IL_DC;
				case 9:
					if (strValue.Length == 0)
					{
						num = 8;
						continue;
					}
					num = 2;
					continue;
				}
				if (strValue == null)
				{
					break;
				}
				num = 1;
			}
			return 0;
			IL_AA:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⩂੄ⅆ⽈㡊⡌㭎", a_));
			IL_DC:
			return 0;
			IL_10A:
			goto IL_AA;
			IL_128:
			throw new ArgumentNullException(RecordTableEnumerator.b("≂㝄㕆ൈ⩊㥌⹎", a_));
			IL_12D:
			arrData[iOffset] = 1;
			iOffset++;
			bytes.CopyTo(arrData, iOffset);
			return bytes.Length + 1;
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x00152BFC File Offset: 0x00151BFC
		public static void SetString16BitUpdateOffset(byte[] arrData, ref int offset, string value)
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
			BiffRecordRaw.SetUInt16(arrData, offset, (ushort)value.Length);
			offset += 2;
			BiffRecordRaw.SetStringNoLenUpdateOffset(arrData, ref offset, value);
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x00152C54 File Offset: 0x00151C54
		public static bool GetBitFromVar(byte btOptions, int bitPos)
		{
			int a_ = 11;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_75;
				case 2:
					if (bitPos >= 8)
					{
						num = 1;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_97;
					}
					break;
				case 3:
					num = 2;
					continue;
				}
				if (bitPos < 0)
				{
					break;
				}
				num = 3;
			}
			IL_37:
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⍀⩂ㅄᝆ♈㡊", a_), RecordTableEnumerator.b("̀⩂ㅄ杆᥈⑊㹌♎═㩒㩔㥖祘㡚㱜ㅞའౢᅤ䝦୨๪䵬ͮᑰrٴ坶൸፺ᱼᅾꆀ뎂ꖄﮈꮊﶎ뮚ꪜ놞", a_));
			IL_75:
			goto IL_37;
			IL_97:
			if (false)
			{
			}
			return BiffRecordRaw.GetBitFromVar((int)btOptions, bitPos);
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x00152D08 File Offset: 0x00151D08
		public static bool GetBitFromVar(short sOptions, int bitPos)
		{
			int a_ = 1;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (bitPos == 15)
					{
						num = 4;
						continue;
					}
					goto IL_C5;
				case 2:
					goto IL_BE;
				case 3:
					goto IL_8A;
				case 4:
					goto IL_5C;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8A;
					default:
						if (false)
						{
						}
						if (bitPos >= 16)
						{
							num = 2;
							continue;
						}
						num = 0;
						continue;
					}
					break;
				}
				if (bitPos >= 0)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				goto IL_68;
				IL_8A:
				num = 5;
			}
			IL_5C:
			return sOptions < 0;
			IL_68:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唶倸伺洼倾㉀", a_), RecordTableEnumerator.b("甶倸伺ᴼ漾⹀あⱄ㍆⁈⑊⍌潎㉐㉒㭔㥖㙘⽚絜㵞Ѡ䍢।ɦᩨᡪ䵬᭮ᥰቲ᭴坶䥸孺ቼൾꆀﾊﶎ놐ꊒꂔ릖", a_));
			IL_BE:
			goto IL_68;
			IL_C5:
			return BiffRecordRaw.GetBitFromVar((int)sOptions, bitPos);
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x00152DE4 File Offset: 0x00151DE4
		[CLSCompliant(false)]
		public static bool GetBitFromVar(ushort usOptions, int bitPos)
		{
			int a_ = 7;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_76;
				case 1:
					if (true)
					{
					}
					num = 3;
					continue;
				case 3:
					if (bitPos >= 16)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_98;
					}
					break;
				}
				if (bitPos < 0)
				{
					break;
				}
				num = 1;
			}
			IL_37:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("弼嘾㕀ፂ⩄㑆", a_), RecordTableEnumerator.b("缼嘾㕀捂ᕄ⡆㩈≊㥌♎㹐㵒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨ݪ࡬ᱮɰ卲Ŵὶᡸᕺ嵼佾ꆀꞆ力랖ꢘ꺚뎜", a_));
			IL_76:
			goto IL_37;
			IL_98:
			if (false)
			{
			}
			return BiffRecordRaw.GetBitFromVar((int)usOptions, bitPos);
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x00152E98 File Offset: 0x00151E98
		public static bool GetBitFromVar(int iOptions, int bitPos)
		{
			int a_ = 10;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (bitPos == 31)
					{
						num = 1;
						continue;
					}
					goto IL_C5;
				case 1:
					goto IL_54;
				case 3:
					goto IL_8A;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8A;
					default:
						if (false)
						{
						}
						if (bitPos >= 32)
						{
							num = 5;
							continue;
						}
						num = 0;
						continue;
					}
					break;
				case 5:
					goto IL_BE;
				}
				if (bitPos >= 0)
				{
					num = 3;
					continue;
				}
				goto IL_68;
				IL_8A:
				num = 4;
			}
			IL_54:
			if (true)
			{
			}
			return iOptions < 0;
			IL_68:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("∿⭁ぃᙅ❇㥉", a_), RecordTableEnumerator.b("ȿ⭁ぃ晅ᡇ╉㽋❍⑏㭑㭓㡕硗㥙㵛そ๟ൡၣ䙥੧ཀྵ䱫ɭᕯűݳ噵౷ቹᵻၽꁿ늁ꒃ慎ꪉﲍ몙꾛꾝躟", a_));
			IL_BE:
			goto IL_68;
			IL_C5:
			return (iOptions & 1 << bitPos) != 0;
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x00152F78 File Offset: 0x00151F78
		[CLSCompliant(false)]
		public static bool GetBitFromVar(uint uiOptions, int bitPos)
		{
			int a_ = 19;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					break;
				case 1:
					if (bitPos >= 32)
					{
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_98;
					}
					break;
				case 2:
					num = 1;
					continue;
				case 3:
					goto IL_76;
				}
				if (bitPos < 0)
				{
					break;
				}
				num = 2;
			}
			IL_3F:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭈≊㥌὎㹐⁒", a_), RecordTableEnumerator.b("ୈ≊㥌潎Ő㱒♔㹖ⵘ㉚㉜ㅞ䅠bѤ०ݨѪᥬ佮፰ᙲ啴᭶ᱸࡺ๼彾ꦈ뮊권뎒ﲘ漢爵펠莢隤隦螨", a_));
			IL_76:
			goto IL_3F;
			IL_98:
			if (false)
			{
			}
			return ((ulong)uiOptions & (ulong)(1L << (bitPos & 31))) != 0UL;
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x00153034 File Offset: 0x00152034
		public static int SetBit(int iValue, int bitPos, bool value)
		{
			int a_ = 8;
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (bitPos == 31)
					{
						num = 12;
						continue;
					}
					num = 10;
					continue;
				case 1:
					goto IL_103;
				case 2:
					goto IL_8A;
				case 3:
					iValue |= 1 << bitPos;
					num = 7;
					continue;
				case 4:
					if (bitPos >= 32)
					{
						num = 1;
						continue;
					}
					num = 0;
					continue;
				case 5:
					if (!value)
					{
						num = 11;
						continue;
					}
					return iValue;
				case 7:
					return iValue;
				case 8:
					num = 4;
					continue;
				case 9:
					goto IL_E3;
				case 10:
					if (value)
					{
						num = 3;
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
						iValue &= ~(1 << bitPos);
						num = 9;
						continue;
					}
					break;
				case 11:
					iValue = -iValue;
					if (true)
					{
					}
					num = 2;
					continue;
				case 12:
					iValue = Math.Abs(iValue);
					num = 5;
					continue;
				}
				if (bitPos < 0)
				{
					goto IL_8F;
				}
				num = 8;
			}
			IL_8A:
			return iValue;
			IL_8F:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("尽⤿㙁ᑃ⥅㭇", a_), RecordTableEnumerator.b("簽⤿㙁摃ᙅ❇㥉╋㩍㥏㵑㩓癕㭗㭙㉛そཟᙡ䑣ѥ൧䩩k୭ͯű味ɵၷ᭹ቻ幽끿ꊁꢇﺋ뢗ꦙ꺛낝", a_));
			IL_E3:
			return iValue;
			IL_103:
			goto IL_8F;
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x001531B0 File Offset: 0x001521B0
		public static int ReadArray(byte[] arrSource, int iOffset, byte[] arrDest)
		{
			int a_ = 16;
			if (true)
			{
			}
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_3C;
				case 1:
					if (arrDest == null)
					{
						num = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						goto IL_9B;
					}
					break;
				case 2:
					goto IL_65;
				}
				goto IL_31;
				IL_34:
				num = 0;
				continue;
				IL_31:
				if (arrSource == null)
				{
					goto IL_34;
				}
				num = 1;
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("❅㩇㡉Ὃ⅍╏⁑㝓㍕", a_));
			IL_65:
			throw new ArgumentNullException(RecordTableEnumerator.b("❅㩇㡉ࡋ⭍⍏♑", a_));
			IL_9B:
			if (false)
			{
			}
			int num2 = arrDest.Length;
			Buffer.BlockCopy(arrSource, iOffset, arrDest, 0, num2);
			return iOffset + num2;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x00153270 File Offset: 0x00152270
		// Note: this type is marked as 'beforefieldinit'.
		static BiffRecordRaw()
		{
			int a_ = 5;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			BiffRecordRaw.ᜃ = new Dictionary<int, SortedList<spr\u2429, FieldInfo>>(100);
			BiffRecordRaw.ᜄ = Encoding.GetEncoding(RecordTableEnumerator.b("场尼䬾⡀ⵂ瑄", a_));
		}

		// Token: 0x04001269 RID: 4713
		private const int ᜀ = 100;

		// Token: 0x0400126A RID: 4714
		public const int DEF_RECORD_MAX_SIZE = 8224;

		// Token: 0x0400126B RID: 4715
		private int \u2609\u00A0\u0081\u00AF;

		// Token: 0x0400126C RID: 4716
		public const int DEF_RECORD_MAX_SIZE_WITH_HADER = 8228;

		// Token: 0x0400126D RID: 4717
		private int[] \u2593\u0091\u0081\u0081;

		// Token: 0x0400126E RID: 4718
		public const int DEF_HEADER_SIZE = 4;

		// Token: 0x0400126F RID: 4719
		public const int DEF_BITS_IN_BYTE = 8;

		// Token: 0x04001270 RID: 4720
		private byte[] \u2460\u0085\u0097\u009E;

		// Token: 0x04001271 RID: 4721
		private const int ᜁ = 16;

		// Token: 0x04001272 RID: 4722
		private const int ᜂ = 32;

		// Token: 0x04001273 RID: 4723
		internal static Dictionary<int, SortedList<spr\u2429, FieldInfo>> ᜃ;

		// Token: 0x04001274 RID: 4724
		private static readonly Encoding ᜄ;

		// Token: 0x04001275 RID: 4725
		protected int m_iCode = -1;

		// Token: 0x04001276 RID: 4726
		protected int m_iLength = -1;

		// Token: 0x04001277 RID: 4727
		private bool ᜅ = true;
	}
}
