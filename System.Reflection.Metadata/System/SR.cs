using System;
using System.Resources;
using System.Runtime.CompilerServices;
using FxResources.System.Reflection.Metadata;

namespace System
{
	// Token: 0x02000003 RID: 3
	internal static class SR
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static ResourceManager ResourceManager
		{
			get
			{
				if (System.SR.s_resourceManager == null)
				{
					System.SR.s_resourceManager = new ResourceManager(System.SR.ResourceType);
				}
				return System.SR.s_resourceManager;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206D File Offset: 0x0000026D
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool UsingResourceKeys()
		{
			return false;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002070 File Offset: 0x00000270
		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string text = null;
			try
			{
				text = System.SR.ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			if (defaultString != null && resourceKey.Equals(text, StringComparison.Ordinal))
			{
				return defaultString;
			}
			return text;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B0 File Offset: 0x000002B0
		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (System.SR.UsingResourceKeys())
			{
				return resourceFormat + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D7 File Offset: 0x000002D7
		internal static string Format(string resourceFormat, object p1)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1
				});
			}
			return string.Format(resourceFormat, new object[]
			{
				p1
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002109 File Offset: 0x00000309
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2
				});
			}
			return string.Format(resourceFormat, new object[]
			{
				p1,
				p2
			});
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002144 File Offset: 0x00000344
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[]
				{
					resourceFormat,
					p1,
					p2,
					p3
				});
			}
			return string.Format(resourceFormat, new object[]
			{
				p1,
				p2,
				p3
			});
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002191 File Offset: 0x00000391
		internal static string ImageTooSmall
		{
			get
			{
				return System.SR.GetResourceString("ImageTooSmall", null);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000219E File Offset: 0x0000039E
		internal static string InvalidCorHeaderSize
		{
			get
			{
				return System.SR.GetResourceString("InvalidCorHeaderSize", null);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021AB File Offset: 0x000003AB
		internal static string InvalidHandle
		{
			get
			{
				return System.SR.GetResourceString("InvalidHandle", null);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021B8 File Offset: 0x000003B8
		internal static string InvalidLocalSignatureToken
		{
			get
			{
				return System.SR.GetResourceString("InvalidLocalSignatureToken", null);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021C5 File Offset: 0x000003C5
		internal static string InvalidMetadataSectionSpan
		{
			get
			{
				return System.SR.GetResourceString("InvalidMetadataSectionSpan", null);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021D2 File Offset: 0x000003D2
		internal static string InvalidMethodHeader1
		{
			get
			{
				return System.SR.GetResourceString("InvalidMethodHeader1", null);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021DF File Offset: 0x000003DF
		internal static string InvalidMethodHeader2
		{
			get
			{
				return System.SR.GetResourceString("InvalidMethodHeader2", null);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021EC File Offset: 0x000003EC
		internal static string InvalidPESignature
		{
			get
			{
				return System.SR.GetResourceString("InvalidPESignature", null);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021F9 File Offset: 0x000003F9
		internal static string InvalidSehHeader
		{
			get
			{
				return System.SR.GetResourceString("InvalidSehHeader", null);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002206 File Offset: 0x00000406
		internal static string InvalidToken
		{
			get
			{
				return System.SR.GetResourceString("InvalidToken", null);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002213 File Offset: 0x00000413
		internal static string MetadataImageDoesNotRepresentAnAssembly
		{
			get
			{
				return System.SR.GetResourceString("MetadataImageDoesNotRepresentAnAssembly", null);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002220 File Offset: 0x00000420
		internal static string StandaloneDebugMetadataImageDoesNotContainModuleTable
		{
			get
			{
				return System.SR.GetResourceString("StandaloneDebugMetadataImageDoesNotContainModuleTable", null);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000222D File Offset: 0x0000042D
		internal static string PEImageNotAvailable
		{
			get
			{
				return System.SR.GetResourceString("PEImageNotAvailable", null);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000223A File Offset: 0x0000043A
		internal static string MissingDataDirectory
		{
			get
			{
				return System.SR.GetResourceString("MissingDataDirectory", null);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002247 File Offset: 0x00000447
		internal static string NotMetadataHeapHandle
		{
			get
			{
				return System.SR.GetResourceString("NotMetadataHeapHandle", null);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002254 File Offset: 0x00000454
		internal static string NotMetadataTableOrUserStringHandle
		{
			get
			{
				return System.SR.GetResourceString("NotMetadataTableOrUserStringHandle", null);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002261 File Offset: 0x00000461
		internal static string SectionTooSmall
		{
			get
			{
				return System.SR.GetResourceString("SectionTooSmall", null);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000226E File Offset: 0x0000046E
		internal static string StreamMustSupportReadAndSeek
		{
			get
			{
				return System.SR.GetResourceString("StreamMustSupportReadAndSeek", null);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000227B File Offset: 0x0000047B
		internal static string UnknownFileFormat
		{
			get
			{
				return System.SR.GetResourceString("UnknownFileFormat", null);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002288 File Offset: 0x00000488
		internal static string UnknownPEMagicValue
		{
			get
			{
				return System.SR.GetResourceString("UnknownPEMagicValue", null);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002295 File Offset: 0x00000495
		internal static string MetadataTableNotSorted
		{
			get
			{
				return System.SR.GetResourceString("MetadataTableNotSorted", null);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022A2 File Offset: 0x000004A2
		internal static string AssemblyTableInvalidNumberOfRows
		{
			get
			{
				return System.SR.GetResourceString("AssemblyTableInvalidNumberOfRows", null);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000022AF File Offset: 0x000004AF
		internal static string ModuleTableInvalidNumberOfRows
		{
			get
			{
				return System.SR.GetResourceString("ModuleTableInvalidNumberOfRows", null);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000022BC File Offset: 0x000004BC
		internal static string UnknownTables
		{
			get
			{
				return System.SR.GetResourceString("UnknownTables", null);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000022C9 File Offset: 0x000004C9
		internal static string IllegalTablesInCompressedMetadataStream
		{
			get
			{
				return System.SR.GetResourceString("IllegalTablesInCompressedMetadataStream", null);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000022D6 File Offset: 0x000004D6
		internal static string TableRowCountSpaceTooSmall
		{
			get
			{
				return System.SR.GetResourceString("TableRowCountSpaceTooSmall", null);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000022E3 File Offset: 0x000004E3
		internal static string OutOfBoundsRead
		{
			get
			{
				return System.SR.GetResourceString("OutOfBoundsRead", null);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000022F0 File Offset: 0x000004F0
		internal static string MetadataHeaderTooSmall
		{
			get
			{
				return System.SR.GetResourceString("MetadataHeaderTooSmall", null);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000022FD File Offset: 0x000004FD
		internal static string MetadataSignature
		{
			get
			{
				return System.SR.GetResourceString("MetadataSignature", null);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000230A File Offset: 0x0000050A
		internal static string NotEnoughSpaceForVersionString
		{
			get
			{
				return System.SR.GetResourceString("NotEnoughSpaceForVersionString", null);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002317 File Offset: 0x00000517
		internal static string StreamHeaderTooSmall
		{
			get
			{
				return System.SR.GetResourceString("StreamHeaderTooSmall", null);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002324 File Offset: 0x00000524
		internal static string NotEnoughSpaceForStreamHeaderName
		{
			get
			{
				return System.SR.GetResourceString("NotEnoughSpaceForStreamHeaderName", null);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002331 File Offset: 0x00000531
		internal static string NotEnoughSpaceForStringStream
		{
			get
			{
				return System.SR.GetResourceString("NotEnoughSpaceForStringStream", null);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000233E File Offset: 0x0000053E
		internal static string NotEnoughSpaceForBlobStream
		{
			get
			{
				return System.SR.GetResourceString("NotEnoughSpaceForBlobStream", null);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000234B File Offset: 0x0000054B
		internal static string NotEnoughSpaceForGUIDStream
		{
			get
			{
				return System.SR.GetResourceString("NotEnoughSpaceForGUIDStream", null);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002358 File Offset: 0x00000558
		internal static string NotEnoughSpaceForMetadataStream
		{
			get
			{
				return System.SR.GetResourceString("NotEnoughSpaceForMetadataStream", null);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002365 File Offset: 0x00000565
		internal static string InvalidMetadataStreamFormat
		{
			get
			{
				return System.SR.GetResourceString("InvalidMetadataStreamFormat", null);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002372 File Offset: 0x00000572
		internal static string MetadataTablesTooSmall
		{
			get
			{
				return System.SR.GetResourceString("MetadataTablesTooSmall", null);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000237F File Offset: 0x0000057F
		internal static string MetadataTableHeaderTooSmall
		{
			get
			{
				return System.SR.GetResourceString("MetadataTableHeaderTooSmall", null);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000238C File Offset: 0x0000058C
		internal static string WinMDMissingMscorlibRef
		{
			get
			{
				return System.SR.GetResourceString("WinMDMissingMscorlibRef", null);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002399 File Offset: 0x00000599
		internal static string UnableToReadMetadataFile
		{
			get
			{
				return System.SR.GetResourceString("UnableToReadMetadataFile", null);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000023A6 File Offset: 0x000005A6
		internal static string UnexpectedStreamEnd
		{
			get
			{
				return System.SR.GetResourceString("UnexpectedStreamEnd", null);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000023B3 File Offset: 0x000005B3
		internal static string InvalidMethodRva
		{
			get
			{
				return System.SR.GetResourceString("InvalidMethodRva", null);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000023C0 File Offset: 0x000005C0
		internal static string CantGetOffsetForVirtualHeapHandle
		{
			get
			{
				return System.SR.GetResourceString("CantGetOffsetForVirtualHeapHandle", null);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000023CD File Offset: 0x000005CD
		internal static string InvalidSectionName
		{
			get
			{
				return System.SR.GetResourceString("InvalidSectionName", null);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000023DA File Offset: 0x000005DA
		internal static string InvalidNumberOfSections
		{
			get
			{
				return System.SR.GetResourceString("InvalidNumberOfSections", null);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000023E7 File Offset: 0x000005E7
		internal static string InvalidSignature
		{
			get
			{
				return System.SR.GetResourceString("InvalidSignature", null);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000023F4 File Offset: 0x000005F4
		internal static string PEImageDoesNotHaveMetadata
		{
			get
			{
				return System.SR.GetResourceString("PEImageDoesNotHaveMetadata", null);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002401 File Offset: 0x00000601
		internal static string InvalidCodedIndex
		{
			get
			{
				return System.SR.GetResourceString("InvalidCodedIndex", null);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000240E File Offset: 0x0000060E
		internal static string InvalidCompressedInteger
		{
			get
			{
				return System.SR.GetResourceString("InvalidCompressedInteger", null);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000241B File Offset: 0x0000061B
		internal static string InvalidDocumentName
		{
			get
			{
				return System.SR.GetResourceString("InvalidDocumentName", null);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002428 File Offset: 0x00000628
		internal static string RowIdOrHeapOffsetTooLarge
		{
			get
			{
				return System.SR.GetResourceString("RowIdOrHeapOffsetTooLarge", null);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002435 File Offset: 0x00000635
		internal static string EnCMapNotSorted
		{
			get
			{
				return System.SR.GetResourceString("EnCMapNotSorted", null);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002442 File Offset: 0x00000642
		internal static string InvalidSerializedString
		{
			get
			{
				return System.SR.GetResourceString("InvalidSerializedString", null);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000244F File Offset: 0x0000064F
		internal static string StreamTooLarge
		{
			get
			{
				return System.SR.GetResourceString("StreamTooLarge", null);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000245C File Offset: 0x0000065C
		internal static string NegativeByteCountOrOffset
		{
			get
			{
				return System.SR.GetResourceString("NegativeByteCountOrOffset", null);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002469 File Offset: 0x00000669
		internal static string ImageTooSmallOrContainsInvalidOffsetOrCount
		{
			get
			{
				return System.SR.GetResourceString("ImageTooSmallOrContainsInvalidOffsetOrCount", null);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002476 File Offset: 0x00000676
		internal static string LitteEndianArchitectureRequired
		{
			get
			{
				return System.SR.GetResourceString("LitteEndianArchitectureRequired", null);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002483 File Offset: 0x00000683
		internal static string MetadataStringDecoderEncodingMustBeUtf8
		{
			get
			{
				return System.SR.GetResourceString("MetadataStringDecoderEncodingMustBeUtf8", null);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002490 File Offset: 0x00000690
		internal static string InvalidConstantValue
		{
			get
			{
				return System.SR.GetResourceString("InvalidConstantValue", null);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000249D File Offset: 0x0000069D
		internal static string InvalidImportDefinitionKind
		{
			get
			{
				return System.SR.GetResourceString("InvalidImportDefinitionKind", null);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000024AA File Offset: 0x000006AA
		internal static string ValueTooLarge
		{
			get
			{
				return System.SR.GetResourceString("ValueTooLarge", null);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000024B7 File Offset: 0x000006B7
		internal static string InvalidTypeSize
		{
			get
			{
				return System.SR.GetResourceString("InvalidTypeSize", null);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000024C4 File Offset: 0x000006C4
		internal static string HandleBelongsToFutureGeneration
		{
			get
			{
				return System.SR.GetResourceString("HandleBelongsToFutureGeneration", null);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000024D1 File Offset: 0x000006D1
		internal static string InvalidRowCount
		{
			get
			{
				return System.SR.GetResourceString("InvalidRowCount", null);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000024DE File Offset: 0x000006DE
		internal static string InvalidEntryPointToken
		{
			get
			{
				return System.SR.GetResourceString("InvalidEntryPointToken", null);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000024EB File Offset: 0x000006EB
		internal static string TooManySubnamespaces
		{
			get
			{
				return System.SR.GetResourceString("TooManySubnamespaces", null);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000024F8 File Offset: 0x000006F8
		internal static string SequencePointValueOutOfRange
		{
			get
			{
				return System.SR.GetResourceString("SequencePointValueOutOfRange", null);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002505 File Offset: 0x00000705
		internal static string InvalidDirectoryRVA
		{
			get
			{
				return System.SR.GetResourceString("InvalidDirectoryRVA", null);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002512 File Offset: 0x00000712
		internal static string InvalidDirectorySize
		{
			get
			{
				return System.SR.GetResourceString("InvalidDirectorySize", null);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000251F File Offset: 0x0000071F
		internal static string InvalidDebugDirectoryEntryCharacteristics
		{
			get
			{
				return System.SR.GetResourceString("InvalidDebugDirectoryEntryCharacteristics", null);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0000252C File Offset: 0x0000072C
		internal static string UnexpectedCodeViewDataSignature
		{
			get
			{
				return System.SR.GetResourceString("UnexpectedCodeViewDataSignature", null);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002539 File Offset: 0x00000739
		internal static string InvalidPathPadding
		{
			get
			{
				return System.SR.GetResourceString("InvalidPathPadding", null);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002546 File Offset: 0x00000746
		internal static string UnexpectedSignatureHeader
		{
			get
			{
				return System.SR.GetResourceString("UnexpectedSignatureHeader", null);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002553 File Offset: 0x00000753
		internal static string UnexpectedSignatureHeader2
		{
			get
			{
				return System.SR.GetResourceString("UnexpectedSignatureHeader2", null);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002560 File Offset: 0x00000760
		internal static string NotTypeDefOrRefHandle
		{
			get
			{
				return System.SR.GetResourceString("NotTypeDefOrRefHandle", null);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000256D File Offset: 0x0000076D
		internal static string UnexpectedSignatureTypeCode
		{
			get
			{
				return System.SR.GetResourceString("UnexpectedSignatureTypeCode", null);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000257A File Offset: 0x0000077A
		internal static string SignatureTypeSequenceMustHaveAtLeastOneElement
		{
			get
			{
				return System.SR.GetResourceString("SignatureTypeSequenceMustHaveAtLeastOneElement", null);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002587 File Offset: 0x00000787
		internal static string NotTypeDefOrRefOrSpecHandle
		{
			get
			{
				return System.SR.GetResourceString("NotTypeDefOrRefOrSpecHandle", null);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002594 File Offset: 0x00000794
		internal static string NotCodeViewEntry
		{
			get
			{
				return System.SR.GetResourceString("NotCodeViewEntry", null);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000025A1 File Offset: 0x000007A1
		internal static Type ResourceType
		{
			get
			{
				return typeof(FxResources.System.Reflection.Metadata.SR);
			}
		}

		// Token: 0x04000001 RID: 1
		private static ResourceManager s_resourceManager;

		// Token: 0x04000002 RID: 2
		private const string s_resourcesName = "FxResources.System.Reflection.Metadata.SR";
	}
}
