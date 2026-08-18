using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x0200017A RID: 378
	public class DataFormats
	{
		// Token: 0x0600142B RID: 5163 RVA: 0x00002843 File Offset: 0x00000A43
		private DataFormats()
		{
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00043D2C File Offset: 0x00041F2C
		public static DataFormats.Format GetFormat(string format)
		{
			object obj = DataFormats.internalSyncObject;
			DataFormats.Format result;
			lock (obj)
			{
				DataFormats.EnsurePredefined();
				for (int i = 0; i < DataFormats.formatCount; i++)
				{
					if (DataFormats.formatList[i].Name.Equals(format))
					{
						return DataFormats.formatList[i];
					}
				}
				for (int j = 0; j < DataFormats.formatCount; j++)
				{
					if (string.Equals(DataFormats.formatList[j].Name, format, StringComparison.OrdinalIgnoreCase))
					{
						return DataFormats.formatList[j];
					}
				}
				int num = SafeNativeMethods.RegisterClipboardFormat(format);
				if (num == 0)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error(), SR.GetString("RegisterCFFailed"));
				}
				DataFormats.EnsureFormatSpace(1);
				DataFormats.formatList[DataFormats.formatCount] = new DataFormats.Format(format, num);
				result = DataFormats.formatList[DataFormats.formatCount++];
			}
			return result;
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00043E20 File Offset: 0x00042020
		public static DataFormats.Format GetFormat(int id)
		{
			return DataFormats.InternalGetFormat(null, (ushort)(id & 65535));
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x00043E30 File Offset: 0x00042030
		private static DataFormats.Format InternalGetFormat(string strName, ushort id)
		{
			object obj = DataFormats.internalSyncObject;
			DataFormats.Format result;
			lock (obj)
			{
				DataFormats.EnsurePredefined();
				for (int i = 0; i < DataFormats.formatCount; i++)
				{
					if (DataFormats.formatList[i].Id == (int)id)
					{
						return DataFormats.formatList[i];
					}
				}
				StringBuilder stringBuilder = new StringBuilder(128);
				if (SafeNativeMethods.GetClipboardFormatName((int)id, stringBuilder, stringBuilder.Capacity) == 0)
				{
					stringBuilder.Length = 0;
					if (strName == null)
					{
						stringBuilder.Append("Format").Append(id);
					}
					else
					{
						stringBuilder.Append(strName);
					}
				}
				DataFormats.EnsureFormatSpace(1);
				DataFormats.formatList[DataFormats.formatCount] = new DataFormats.Format(stringBuilder.ToString(), (int)id);
				result = DataFormats.formatList[DataFormats.formatCount++];
			}
			return result;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x00043F10 File Offset: 0x00042110
		private static void EnsureFormatSpace(int size)
		{
			if (DataFormats.formatList == null || DataFormats.formatList.Length <= DataFormats.formatCount + size)
			{
				int num = DataFormats.formatCount + 20;
				DataFormats.Format[] array = new DataFormats.Format[num];
				for (int i = 0; i < DataFormats.formatCount; i++)
				{
					array[i] = DataFormats.formatList[i];
				}
				DataFormats.formatList = array;
			}
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x00043F64 File Offset: 0x00042164
		private static void EnsurePredefined()
		{
			if (DataFormats.formatCount == 0)
			{
				DataFormats.formatList = new DataFormats.Format[]
				{
					new DataFormats.Format(DataFormats.UnicodeText, 13),
					new DataFormats.Format(DataFormats.Text, 1),
					new DataFormats.Format(DataFormats.Bitmap, 2),
					new DataFormats.Format(DataFormats.MetafilePict, 3),
					new DataFormats.Format(DataFormats.EnhancedMetafile, 14),
					new DataFormats.Format(DataFormats.Dif, 5),
					new DataFormats.Format(DataFormats.Tiff, 6),
					new DataFormats.Format(DataFormats.OemText, 7),
					new DataFormats.Format(DataFormats.Dib, 8),
					new DataFormats.Format(DataFormats.Palette, 9),
					new DataFormats.Format(DataFormats.PenData, 10),
					new DataFormats.Format(DataFormats.Riff, 11),
					new DataFormats.Format(DataFormats.WaveAudio, 12),
					new DataFormats.Format(DataFormats.SymbolicLink, 4),
					new DataFormats.Format(DataFormats.FileDrop, 15),
					new DataFormats.Format(DataFormats.Locale, 16)
				};
				DataFormats.formatCount = DataFormats.formatList.Length;
			}
		}

		// Token: 0x04000979 RID: 2425
		public static readonly string Text = "Text";

		// Token: 0x0400097A RID: 2426
		public static readonly string UnicodeText = "UnicodeText";

		// Token: 0x0400097B RID: 2427
		public static readonly string Dib = "DeviceIndependentBitmap";

		// Token: 0x0400097C RID: 2428
		public static readonly string Bitmap = "Bitmap";

		// Token: 0x0400097D RID: 2429
		public static readonly string EnhancedMetafile = "EnhancedMetafile";

		// Token: 0x0400097E RID: 2430
		public static readonly string MetafilePict = "MetaFilePict";

		// Token: 0x0400097F RID: 2431
		public static readonly string SymbolicLink = "SymbolicLink";

		// Token: 0x04000980 RID: 2432
		public static readonly string Dif = "DataInterchangeFormat";

		// Token: 0x04000981 RID: 2433
		public static readonly string Tiff = "TaggedImageFileFormat";

		// Token: 0x04000982 RID: 2434
		public static readonly string OemText = "OEMText";

		// Token: 0x04000983 RID: 2435
		public static readonly string Palette = "Palette";

		// Token: 0x04000984 RID: 2436
		public static readonly string PenData = "PenData";

		// Token: 0x04000985 RID: 2437
		public static readonly string Riff = "RiffAudio";

		// Token: 0x04000986 RID: 2438
		public static readonly string WaveAudio = "WaveAudio";

		// Token: 0x04000987 RID: 2439
		public static readonly string FileDrop = "FileDrop";

		// Token: 0x04000988 RID: 2440
		public static readonly string Locale = "Locale";

		// Token: 0x04000989 RID: 2441
		public static readonly string Html = "HTML Format";

		// Token: 0x0400098A RID: 2442
		public static readonly string Rtf = "Rich Text Format";

		// Token: 0x0400098B RID: 2443
		public static readonly string CommaSeparatedValue = "Csv";

		// Token: 0x0400098C RID: 2444
		public static readonly string StringFormat = typeof(string).FullName;

		// Token: 0x0400098D RID: 2445
		public static readonly string Serializable = Application.WindowsFormsVersion + "PersistentObject";

		// Token: 0x0400098E RID: 2446
		private static DataFormats.Format[] formatList;

		// Token: 0x0400098F RID: 2447
		private static int formatCount = 0;

		// Token: 0x04000990 RID: 2448
		private static object internalSyncObject = new object();

		// Token: 0x02000645 RID: 1605
		public class Format
		{
			// Token: 0x17001595 RID: 5525
			// (get) Token: 0x060064BE RID: 25790 RVA: 0x001770BE File Offset: 0x001752BE
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17001596 RID: 5526
			// (get) Token: 0x060064BF RID: 25791 RVA: 0x001770C6 File Offset: 0x001752C6
			public int Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x060064C0 RID: 25792 RVA: 0x001770CE File Offset: 0x001752CE
			public Format(string name, int id)
			{
				this.name = name;
				this.id = id;
			}

			// Token: 0x040039B3 RID: 14771
			private readonly string name;

			// Token: 0x040039B4 RID: 14772
			private readonly int id;
		}
	}
}
