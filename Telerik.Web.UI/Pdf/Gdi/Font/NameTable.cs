using System;
using System.Text;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001625 RID: 5669
	internal class NameTable : FontTable
	{
		// Token: 0x0600DC79 RID: 56441 RVA: 0x00303010 File Offset: 0x00301210
		public NameTable(DirectoryEntry entry) : base("name", entry)
		{
		}

		// Token: 0x17004376 RID: 17270
		// (get) Token: 0x0600DC7A RID: 56442 RVA: 0x00303034 File Offset: 0x00301234
		public string FamilyName
		{
			get
			{
				return this.familyName;
			}
		}

		// Token: 0x17004377 RID: 17271
		// (get) Token: 0x0600DC7B RID: 56443 RVA: 0x0030303C File Offset: 0x0030123C
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x0600DC7C RID: 56444 RVA: 0x00303044 File Offset: 0x00301244
		protected internal override void Read(FontFileReader reader)
		{
			FontFileStream stream = reader.Stream;
			stream.ReadUShort();
			int num = stream.ReadUShort();
			this.storageOffset = stream.ReadUShort();
			for (int i = 0; i < num; i++)
			{
				int num2 = stream.ReadUShort();
				int num3 = stream.ReadUShort();
				int num4 = stream.ReadUShort();
				int num5 = stream.ReadUShort();
				int length = stream.ReadUShort();
				int stringOffset = stream.ReadUShort();
				if (num2 == 3 && (num3 == 0 || num3 == 1) && num4 == 1033)
				{
					int num6 = num5;
					if (num6 != 1)
					{
						if (num6 == 4)
						{
							this.fullName = this.ReadString(stream, stringOffset, length);
						}
					}
					else
					{
						this.familyName = this.ReadString(stream, stringOffset, length);
					}
					if (!string.IsNullOrEmpty(this.familyName) && !string.IsNullOrEmpty(this.fullName))
					{
						return;
					}
				}
			}
		}

		// Token: 0x0600DC7D RID: 56445 RVA: 0x00303118 File Offset: 0x00301318
		private string ReadString(FontFileStream stream, int stringOffset, int length)
		{
			stream.SetRestorePoint();
			stream.Position = (long)(base.Entry.Offset + this.storageOffset + stringOffset);
			byte[] array = new byte[length];
			stream.Read(array, 0, length);
			stream.Restore();
			return Encoding.BigEndianUnicode.GetString(array);
		}

		// Token: 0x0600DC7E RID: 56446 RVA: 0x0030316A File Offset: 0x0030136A
		protected internal override void Write(FontFileWriter writer)
		{
			throw new NotImplementedException("Write is not implemented.");
		}

		// Token: 0x04003DF3 RID: 15859
		private const int MicrosoftPlatformID = 3;

		// Token: 0x04003DF4 RID: 15860
		private const int SymbolEncoding = 0;

		// Token: 0x04003DF5 RID: 15861
		private const int UnicodeEncoding = 1;

		// Token: 0x04003DF6 RID: 15862
		private const int EnglishAmericanLanguage = 1033;

		// Token: 0x04003DF7 RID: 15863
		private const int FamilyNameID = 1;

		// Token: 0x04003DF8 RID: 15864
		private const int SubFamilyNameID = 2;

		// Token: 0x04003DF9 RID: 15865
		private const int UniqueNameNameID = 3;

		// Token: 0x04003DFA RID: 15866
		private const int FullNameID = 4;

		// Token: 0x04003DFB RID: 15867
		private const int VersionNameID = 5;

		// Token: 0x04003DFC RID: 15868
		private const int PostscriptNameID = 6;

		// Token: 0x04003DFD RID: 15869
		private string familyName = string.Empty;

		// Token: 0x04003DFE RID: 15870
		private string fullName = string.Empty;

		// Token: 0x04003DFF RID: 15871
		private int storageOffset;
	}
}
