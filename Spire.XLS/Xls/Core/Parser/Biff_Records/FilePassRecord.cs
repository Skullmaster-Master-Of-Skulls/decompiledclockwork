using System;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Parser.Biff_Records
{
	// Token: 0x020005E8 RID: 1512
	[spr\u2593(TBIFFRecord.FilePass)]
	public class FilePassRecord : BiffRecordRaw
	{
		// Token: 0x060059BD RID: 22973 RVA: 0x00385030 File Offset: 0x00384030
		public FilePassRecord()
		{
		}

		// Token: 0x060059BE RID: 22974 RVA: 0x00385044 File Offset: 0x00384044
		public FilePassRecord(Stream stream, out int itemSize) : base(stream, out itemSize)
		{
		}

		// Token: 0x060059BF RID: 22975 RVA: 0x0038505C File Offset: 0x0038405C
		public FilePassRecord(int iReserve) : base(iReserve)
		{
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x060059C0 RID: 22976 RVA: 0x00385070 File Offset: 0x00384070
		// (set) Token: 0x060059C1 RID: 22977 RVA: 0x003850B4 File Offset: 0x003840B4
		public bool IsWeakEncryption
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
				return this.ᜂ == 0;
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
				this.ᜂ = (value ? 0 : 1);
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x060059C2 RID: 22978 RVA: 0x00385104 File Offset: 0x00384104
		// (set) Token: 0x060059C3 RID: 22979 RVA: 0x00385148 File Offset: 0x00384148
		[CLSCompliant(false)]
		public ushort Key
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
				return this.ᜃ;
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
				this.ᜃ = value;
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x060059C4 RID: 22980 RVA: 0x0038518C File Offset: 0x0038418C
		// (set) Token: 0x060059C5 RID: 22981 RVA: 0x003851D0 File Offset: 0x003841D0
		[CLSCompliant(false)]
		public ushort Hash
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

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x060059C6 RID: 22982 RVA: 0x00385214 File Offset: 0x00384214
		internal sprṺ StandardBlock
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
		}

		// Token: 0x060059C7 RID: 22983 RVA: 0x00385258 File Offset: 0x00384258
		public void CreateStandardBlock()
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
			this.ᜅ = new sprṺ();
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x060059C8 RID: 22984 RVA: 0x003852A0 File Offset: 0x003842A0
		public override bool NeedDecoding
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

		// Token: 0x060059C9 RID: 22985 RVA: 0x003852DC File Offset: 0x003842DC
		public override void ParseStructure(DataProvider provider, int iOffset, int iLength, ExcelVersion version)
		{
			int a_ = 8;
			int num = 5;
			for (;;)
			{
				ushort num2;
				switch (num)
				{
				case 0:
					goto IL_4A;
				case 1:
					if (this.IsWeakEncryption)
					{
						num = 3;
						continue;
					}
					goto IL_5C;
				case 2:
					goto IL_57;
				case 3:
					goto IL_F8;
				case 4:
					num = 2;
					continue;
				case 6:
					switch (num2)
					{
					case 1:
						goto IL_131;
					case 2:
						goto IL_102;
					default:
						num = 4;
						continue;
					}
					break;
				}
				if (provider == null)
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
					if (false)
					{
					}
					this.ᜂ = provider.ReadUInt16(iOffset);
					iOffset += 2;
					this.ᜃ = provider.ReadUInt16(iOffset);
					iOffset += 2;
					this.ᜄ = provider.ReadUInt16(iOffset);
					iOffset += 2;
					num = 1;
					continue;
				}
				IL_5C:
				num2 = this.ᜄ;
				num = 6;
			}
			IL_4A:
			throw new ArgumentNullException(RecordTableEnumerator.b("丽㈿ⵁ㉃⽅ⱇ⽉㹋", a_));
			IL_57:
			throw new spr\u2313(RecordTableEnumerator.b("紽ℿⱁ⩃⥅㱇橉㱋⽍≏⅑ㅓ癕ṗ㍙せ㭝た͡ᝣᕥ䡧ᡩ५൭Ὧqၳ", a_));
			IL_F8:
			if (true)
			{
			}
			return;
			IL_102:
			this.ᜆ = new spr\u21B1();
			this.ᜆ.ᜀ(provider, iOffset, iLength);
			return;
			IL_131:
			this.ᜅ = new sprṺ();
			this.ᜅ.ᜀ(provider, iOffset, iLength);
		}

		// Token: 0x060059CA RID: 22986 RVA: 0x00385448 File Offset: 0x00384448
		public override void InfillInternalData(DataProvider provider, int iOffset, ExcelVersion version)
		{
			for (;;)
			{
				this.m_iLength = this.GetStoreSize(version);
				provider.WriteUInt16(iOffset, this.ᜂ);
				iOffset += 2;
				provider.WriteUInt16(iOffset, this.ᜃ);
				iOffset += 2;
				provider.WriteUInt16(iOffset, this.ᜄ);
				iOffset += 2;
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
					int num = 0;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							if (!this.IsWeakEncryption)
							{
								num = 2;
								continue;
							}
							return;
						case 1:
							goto IL_E1;
						case 2:
							num = 3;
							continue;
						case 3:
							if (this.ᜄ == 1)
							{
								num = 1;
								continue;
							}
							goto IL_A9;
						}
						break;
					}
					break;
				}
				}
			}
			IL_A9:
			throw new NotImplementedException();
			IL_E1:
			this.ᜅ.ᜁ(provider, iOffset, int.MaxValue);
		}

		// Token: 0x060059CB RID: 22987 RVA: 0x00385538 File Offset: 0x00384538
		public override int GetStoreSize(ExcelVersion version)
		{
			int num;
			for (;;)
			{
				IL_1C:
				num = 6;
				for (;;)
				{
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num2 = 4;
							continue;
						case 1:
							num += sprṺ.ᜀ(version);
							num2 = 3;
							continue;
						case 2:
							if (!this.IsWeakEncryption)
							{
								if (true)
								{
								}
								num2 = 0;
								continue;
							}
							return num;
						case 3:
							goto IL_61;
						case 4:
							if (this.ᜄ == 1)
							{
								num2 = 1;
								continue;
							}
							goto IL_4A;
						}
						goto IL_1C;
					}
					IL_61:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_77;
					}
				}
			}
			IL_4A:
			throw new NotImplementedException();
			IL_77:
			if (false)
			{
			}
			return num;
		}

		// Token: 0x04002AE9 RID: 10985
		internal new const int ᜀ = 1;

		// Token: 0x04002AEA RID: 10986
		internal const int ᜁ = 2;

		// Token: 0x04002AEB RID: 10987
		private ushort ᜂ;

		// Token: 0x04002AEC RID: 10988
		private new ushort ᜃ;

		// Token: 0x04002AED RID: 10989
		private ushort ᜄ;

		// Token: 0x04002AEE RID: 10990
		private sprṺ ᜅ;

		// Token: 0x04002AEF RID: 10991
		private bool \u25D8\u0083\u0087\u0087;

		// Token: 0x04002AF0 RID: 10992
		private spr\u21B1 ᜆ;
	}
}
