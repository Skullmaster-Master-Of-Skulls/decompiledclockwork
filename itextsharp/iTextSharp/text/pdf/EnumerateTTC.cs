using System;
using System.Collections.Generic;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000EB RID: 235
	internal class EnumerateTTC : TrueTypeFont
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x0002FCCD File Offset: 0x0002ECCD
		internal EnumerateTTC(string ttcFile)
		{
			this.fileName = ttcFile;
			this.rf = new RandomAccessFileOrArray(ttcFile);
			this.FindNames();
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0002FCEE File Offset: 0x0002ECEE
		internal EnumerateTTC(byte[] ttcArray)
		{
			this.fileName = "Byte array TTC";
			this.rf = new RandomAccessFileOrArray(ttcArray);
			this.FindNames();
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002FD14 File Offset: 0x0002ED14
		internal void FindNames()
		{
			this.tables = new Dictionary<string, int[]>();
			try
			{
				string text = base.ReadStandardString(4);
				if (!text.Equals("ttcf"))
				{
					throw new DocumentException(MessageLocalization.GetComposedMessage("1.is.not.a.valid.ttc.file", this.fileName));
				}
				this.rf.SkipBytes(4);
				int num = this.rf.ReadInt();
				this.names = new string[num];
				int filePointer = this.rf.FilePointer;
				for (int i = 0; i < num; i++)
				{
					this.tables.Clear();
					this.rf.Seek(filePointer);
					this.rf.SkipBytes(i * 4);
					this.directoryOffset = this.rf.ReadInt();
					this.rf.Seek(this.directoryOffset);
					if (this.rf.ReadInt() != 65536)
					{
						throw new DocumentException(MessageLocalization.GetComposedMessage("1.is.not.a.valid.ttf.file", this.fileName));
					}
					int num2 = this.rf.ReadUnsignedShort();
					this.rf.SkipBytes(6);
					for (int j = 0; j < num2; j++)
					{
						string key = base.ReadStandardString(4);
						this.rf.SkipBytes(4);
						int[] value = new int[]
						{
							this.rf.ReadInt(),
							this.rf.ReadInt()
						};
						this.tables[key] = value;
					}
					this.names[i] = base.BaseFont;
				}
			}
			finally
			{
				if (this.rf != null)
				{
					this.rf.Close();
				}
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0002FEC0 File Offset: 0x0002EEC0
		internal string[] Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x0400078B RID: 1931
		protected string[] names;
	}
}
