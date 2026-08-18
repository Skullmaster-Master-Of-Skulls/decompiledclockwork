using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.IO.Compression
{
	// Token: 0x02000012 RID: 18
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Messages
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00005182 File Offset: 0x00003382
		internal Messages()
		{
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000518C File Offset: 0x0000338C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Messages.resourceMan == null)
				{
					ResourceManager resourceManager = new ResourceManager("System.IO.Compression.Messages", typeof(Messages).Assembly);
					Messages.resourceMan = resourceManager;
				}
				return Messages.resourceMan;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000051C5 File Offset: 0x000033C5
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000051CC File Offset: 0x000033CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Messages.resourceCulture;
			}
			set
			{
				Messages.resourceCulture = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000051D4 File Offset: 0x000033D4
		internal static string ArgumentNeedNonNegative
		{
			get
			{
				return Messages.ResourceManager.GetString("ArgumentNeedNonNegative", Messages.resourceCulture);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000051EA File Offset: 0x000033EA
		internal static string CannotBeEmpty
		{
			get
			{
				return Messages.ResourceManager.GetString("CannotBeEmpty", Messages.resourceCulture);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00005200 File Offset: 0x00003400
		internal static string CDCorrupt
		{
			get
			{
				return Messages.ResourceManager.GetString("CDCorrupt", Messages.resourceCulture);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00005216 File Offset: 0x00003416
		internal static string CentralDirectoryInvalid
		{
			get
			{
				return Messages.ResourceManager.GetString("CentralDirectoryInvalid", Messages.resourceCulture);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000522C File Offset: 0x0000342C
		internal static string CreateInReadMode
		{
			get
			{
				return Messages.ResourceManager.GetString("CreateInReadMode", Messages.resourceCulture);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00005242 File Offset: 0x00003442
		internal static string CreateModeCapabilities
		{
			get
			{
				return Messages.ResourceManager.GetString("CreateModeCapabilities", Messages.resourceCulture);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00005258 File Offset: 0x00003458
		internal static string CreateModeCreateEntryWhileOpen
		{
			get
			{
				return Messages.ResourceManager.GetString("CreateModeCreateEntryWhileOpen", Messages.resourceCulture);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000526E File Offset: 0x0000346E
		internal static string CreateModeWriteOnceAndOneEntryAtATime
		{
			get
			{
				return Messages.ResourceManager.GetString("CreateModeWriteOnceAndOneEntryAtATime", Messages.resourceCulture);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00005284 File Offset: 0x00003484
		internal static string DateTimeInvalid
		{
			get
			{
				return Messages.ResourceManager.GetString("DateTimeInvalid", Messages.resourceCulture);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000529A File Offset: 0x0000349A
		internal static string DateTimeOutOfRange
		{
			get
			{
				return Messages.ResourceManager.GetString("DateTimeOutOfRange", Messages.resourceCulture);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000052B0 File Offset: 0x000034B0
		internal static string DeletedEntry
		{
			get
			{
				return Messages.ResourceManager.GetString("DeletedEntry", Messages.resourceCulture);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000052C6 File Offset: 0x000034C6
		internal static string DeleteOnlyInUpdate
		{
			get
			{
				return Messages.ResourceManager.GetString("DeleteOnlyInUpdate", Messages.resourceCulture);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000052DC File Offset: 0x000034DC
		internal static string DeleteOpenEntry
		{
			get
			{
				return Messages.ResourceManager.GetString("DeleteOpenEntry", Messages.resourceCulture);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000052F2 File Offset: 0x000034F2
		internal static string EntriesInCreateMode
		{
			get
			{
				return Messages.ResourceManager.GetString("EntriesInCreateMode", Messages.resourceCulture);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00005308 File Offset: 0x00003508
		internal static string EntryNameEncodingNotSupported
		{
			get
			{
				return Messages.ResourceManager.GetString("EntryNameEncodingNotSupported", Messages.resourceCulture);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000531E File Offset: 0x0000351E
		internal static string EntryNamesTooLong
		{
			get
			{
				return Messages.ResourceManager.GetString("EntryNamesTooLong", Messages.resourceCulture);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00005334 File Offset: 0x00003534
		internal static string EntryTooLarge
		{
			get
			{
				return Messages.ResourceManager.GetString("EntryTooLarge", Messages.resourceCulture);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000534A File Offset: 0x0000354A
		internal static string EOCDNotFound
		{
			get
			{
				return Messages.ResourceManager.GetString("EOCDNotFound", Messages.resourceCulture);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00005360 File Offset: 0x00003560
		internal static string FieldTooBigCompressedSize
		{
			get
			{
				return Messages.ResourceManager.GetString("FieldTooBigCompressedSize", Messages.resourceCulture);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00005376 File Offset: 0x00003576
		internal static string FieldTooBigLocalHeaderOffset
		{
			get
			{
				return Messages.ResourceManager.GetString("FieldTooBigLocalHeaderOffset", Messages.resourceCulture);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000538C File Offset: 0x0000358C
		internal static string FieldTooBigNumEntries
		{
			get
			{
				return Messages.ResourceManager.GetString("FieldTooBigNumEntries", Messages.resourceCulture);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000053A2 File Offset: 0x000035A2
		internal static string FieldTooBigOffsetToCD
		{
			get
			{
				return Messages.ResourceManager.GetString("FieldTooBigOffsetToCD", Messages.resourceCulture);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000053B8 File Offset: 0x000035B8
		internal static string FieldTooBigOffsetToZip64EOCD
		{
			get
			{
				return Messages.ResourceManager.GetString("FieldTooBigOffsetToZip64EOCD", Messages.resourceCulture);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000053CE File Offset: 0x000035CE
		internal static string FieldTooBigStartDiskNumber
		{
			get
			{
				return Messages.ResourceManager.GetString("FieldTooBigStartDiskNumber", Messages.resourceCulture);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000053E4 File Offset: 0x000035E4
		internal static string FieldTooBigUncompressedSize
		{
			get
			{
				return Messages.ResourceManager.GetString("FieldTooBigUncompressedSize", Messages.resourceCulture);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000053FA File Offset: 0x000035FA
		internal static string FrozenAfterWrite
		{
			get
			{
				return Messages.ResourceManager.GetString("FrozenAfterWrite", Messages.resourceCulture);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00005410 File Offset: 0x00003610
		internal static string HiddenStreamName
		{
			get
			{
				return Messages.ResourceManager.GetString("HiddenStreamName", Messages.resourceCulture);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005426 File Offset: 0x00003626
		internal static string LengthAfterWrite
		{
			get
			{
				return Messages.ResourceManager.GetString("LengthAfterWrite", Messages.resourceCulture);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000543C File Offset: 0x0000363C
		internal static string LocalFileHeaderCorrupt
		{
			get
			{
				return Messages.ResourceManager.GetString("LocalFileHeaderCorrupt", Messages.resourceCulture);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00005452 File Offset: 0x00003652
		internal static string NumEntriesWrong
		{
			get
			{
				return Messages.ResourceManager.GetString("NumEntriesWrong", Messages.resourceCulture);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00005468 File Offset: 0x00003668
		internal static string OffsetLengthInvalid
		{
			get
			{
				return Messages.ResourceManager.GetString("OffsetLengthInvalid", Messages.resourceCulture);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000547E File Offset: 0x0000367E
		internal static string ReadingNotSupported
		{
			get
			{
				return Messages.ResourceManager.GetString("ReadingNotSupported", Messages.resourceCulture);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00005494 File Offset: 0x00003694
		internal static string ReadModeCapabilities
		{
			get
			{
				return Messages.ResourceManager.GetString("ReadModeCapabilities", Messages.resourceCulture);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000054AA File Offset: 0x000036AA
		internal static string ReadOnlyArchive
		{
			get
			{
				return Messages.ResourceManager.GetString("ReadOnlyArchive", Messages.resourceCulture);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000054C0 File Offset: 0x000036C0
		internal static string SeekingNotSupported
		{
			get
			{
				return Messages.ResourceManager.GetString("SeekingNotSupported", Messages.resourceCulture);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000054D6 File Offset: 0x000036D6
		internal static string SetLengthRequiresSeekingAndWriting
		{
			get
			{
				return Messages.ResourceManager.GetString("SetLengthRequiresSeekingAndWriting", Messages.resourceCulture);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000054EC File Offset: 0x000036EC
		internal static string SplitSpanned
		{
			get
			{
				return Messages.ResourceManager.GetString("SplitSpanned", Messages.resourceCulture);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005502 File Offset: 0x00003702
		internal static string UnexpectedEndOfStream
		{
			get
			{
				return Messages.ResourceManager.GetString("UnexpectedEndOfStream", Messages.resourceCulture);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00005518 File Offset: 0x00003718
		internal static string UnsupportedCompression
		{
			get
			{
				return Messages.ResourceManager.GetString("UnsupportedCompression", Messages.resourceCulture);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000552E File Offset: 0x0000372E
		internal static string UpdateModeCapabilities
		{
			get
			{
				return Messages.ResourceManager.GetString("UpdateModeCapabilities", Messages.resourceCulture);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005544 File Offset: 0x00003744
		internal static string UpdateModeOneStream
		{
			get
			{
				return Messages.ResourceManager.GetString("UpdateModeOneStream", Messages.resourceCulture);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000555A File Offset: 0x0000375A
		internal static string WritingNotSupported
		{
			get
			{
				return Messages.ResourceManager.GetString("WritingNotSupported", Messages.resourceCulture);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005570 File Offset: 0x00003770
		internal static string Zip64EOCDNotWhereExpected
		{
			get
			{
				return Messages.ResourceManager.GetString("Zip64EOCDNotWhereExpected", Messages.resourceCulture);
			}
		}

		// Token: 0x0400008E RID: 142
		private static ResourceManager resourceMan;

		// Token: 0x0400008F RID: 143
		private static CultureInfo resourceCulture;
	}
}
