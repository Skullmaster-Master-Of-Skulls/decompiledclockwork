using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Web
{
	// Token: 0x020000FE RID: 254
	internal class StringResourceBuilder
	{
		// Token: 0x06000F44 RID: 3908 RVA: 0x000030B5 File Offset: 0x000012B5
		internal StringResourceBuilder()
		{
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0002BE4C File Offset: 0x0002A04C
		internal void AddString(string s, out int offset, out int size, out bool fAsciiOnly)
		{
			if (this._literalStrings == null)
			{
				this._literalStrings = new ArrayList();
			}
			this._literalStrings.Add(s);
			size = Encoding.UTF8.GetByteCount(s);
			fAsciiOnly = (size == s.Length);
			offset = this._offset;
			this._offset += size;
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x0002BEAA File Offset: 0x0002A0AA
		internal bool HasStrings
		{
			get
			{
				return this._literalStrings != null;
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0002BEB8 File Offset: 0x0002A0B8
		internal void CreateResourceFile(string resFileName)
		{
			using (Stream stream = new FileStream(resFileName, FileMode.Create))
			{
				Encoding utf = Encoding.UTF8;
				BinaryWriter binaryWriter = new BinaryWriter(stream, utf);
				binaryWriter.Write(0);
				binaryWriter.Write(32);
				binaryWriter.Write(65535);
				binaryWriter.Write(65535);
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(this._offset);
				binaryWriter.Write(32);
				binaryWriter.Write(247201791);
				binaryWriter.Write(6684671);
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				foreach (object obj in this._literalStrings)
				{
					string s = (string)obj;
					byte[] bytes = utf.GetBytes(s);
					binaryWriter.Write(bytes);
				}
			}
		}

		// Token: 0x040005D8 RID: 1496
		private ArrayList _literalStrings;

		// Token: 0x040005D9 RID: 1497
		private int _offset;
	}
}
