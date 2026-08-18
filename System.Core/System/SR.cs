using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace System
{
	// Token: 0x02000039 RID: 57
	internal sealed class SR
	{
		// Token: 0x0600015F RID: 351 RVA: 0x00003E00 File Offset: 0x00002000
		internal SR()
		{
			this.resources = new ResourceManager("System.Core", base.GetType().Assembly);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00003E24 File Offset: 0x00002024
		private static SR GetLoader()
		{
			if (SR.loader == null)
			{
				SR value = new SR();
				Interlocked.CompareExchange<SR>(ref SR.loader, value, null);
			}
			return SR.loader;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00003E50 File Offset: 0x00002050
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00003E53 File Offset: 0x00002053
		public static ResourceManager Resources
		{
			get
			{
				return SR.GetLoader().resources;
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00003E60 File Offset: 0x00002060
		public static string GetString(string name, params object[] args)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			string @string = sr.resources.GetString(name, SR.Culture);
			if (args != null && args.Length != 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00003EE0 File Offset: 0x000020E0
		public static string GetString(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetString(name, SR.Culture);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00003F09 File Offset: 0x00002109
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return SR.GetString(name);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00003F14 File Offset: 0x00002114
		public static object GetObject(string name)
		{
			SR sr = SR.GetLoader();
			if (sr == null)
			{
				return null;
			}
			return sr.resources.GetObject(name, SR.Culture);
		}

		// Token: 0x040000E5 RID: 229
		internal const string ArgumentOutOfRange_NeedNonNegNum = "ArgumentOutOfRange_NeedNonNegNum";

		// Token: 0x040000E6 RID: 230
		internal const string Argument_WrongAsyncResult = "Argument_WrongAsyncResult";

		// Token: 0x040000E7 RID: 231
		internal const string Argument_InvalidOffLen = "Argument_InvalidOffLen";

		// Token: 0x040000E8 RID: 232
		internal const string Argument_NeedNonemptyPipeName = "Argument_NeedNonemptyPipeName";

		// Token: 0x040000E9 RID: 233
		internal const string Argument_EmptyServerName = "Argument_EmptyServerName";

		// Token: 0x040000EA RID: 234
		internal const string Argument_NonContainerInvalidAnyFlag = "Argument_NonContainerInvalidAnyFlag";

		// Token: 0x040000EB RID: 235
		internal const string Argument_InvalidHandle = "Argument_InvalidHandle";

		// Token: 0x040000EC RID: 236
		internal const string ArgumentNull_Buffer = "ArgumentNull_Buffer";

		// Token: 0x040000ED RID: 237
		internal const string ArgumentNull_ServerName = "ArgumentNull_ServerName";

		// Token: 0x040000EE RID: 238
		internal const string ArgumentOutOfRange_AdditionalAccessLimited = "ArgumentOutOfRange_AdditionalAccessLimited";

		// Token: 0x040000EF RID: 239
		internal const string ArgumentOutOfRange_AnonymousReserved = "ArgumentOutOfRange_AnonymousReserved";

		// Token: 0x040000F0 RID: 240
		internal const string ArgumentOutOfRange_TransmissionModeByteOrMsg = "ArgumentOutOfRange_TransmissionModeByteOrMsg";

		// Token: 0x040000F1 RID: 241
		internal const string ArgumentOutOfRange_DirectionModeInOrOut = "ArgumentOutOfRange_DirectionModeInOrOut";

		// Token: 0x040000F2 RID: 242
		internal const string ArgumentOutOfRange_DirectionModeInOutOrInOut = "ArgumentOutOfRange_DirectionModeInOutOrInOut";

		// Token: 0x040000F3 RID: 243
		internal const string ArgumentOutOfRange_ImpersonationInvalid = "ArgumentOutOfRange_ImpersonationInvalid";

		// Token: 0x040000F4 RID: 244
		internal const string ArgumentOutOfRange_ImpersonationOptionsInvalid = "ArgumentOutOfRange_ImpersonationOptionsInvalid";

		// Token: 0x040000F5 RID: 245
		internal const string ArgumentOutOfRange_OptionsInvalid = "ArgumentOutOfRange_OptionsInvalid";

		// Token: 0x040000F6 RID: 246
		internal const string ArgumentOutOfRange_HandleInheritabilityNoneOrInheritable = "ArgumentOutOfRange_HandleInheritabilityNoneOrInheritable";

		// Token: 0x040000F7 RID: 247
		internal const string ArgumentOutOfRange_InvalidPipeAccessRights = "ArgumentOutOfRange_InvalidPipeAccessRights";

		// Token: 0x040000F8 RID: 248
		internal const string ArgumentOutOfRange_InvalidTimeout = "ArgumentOutOfRange_InvalidTimeout";

		// Token: 0x040000F9 RID: 249
		internal const string ArgumentOutOfRange_MaxNumServerInstances = "ArgumentOutOfRange_MaxNumServerInstances";

		// Token: 0x040000FA RID: 250
		internal const string ArgumentOutOfRange_NeedValidPipeAccessRights = "ArgumentOutOfRange_NeedValidPipeAccessRights";

		// Token: 0x040000FB RID: 251
		internal const string IndexOutOfRange_IORaceCondition = "IndexOutOfRange_IORaceCondition";

		// Token: 0x040000FC RID: 252
		internal const string InvalidOperation_EndReadCalledMultiple = "InvalidOperation_EndReadCalledMultiple";

		// Token: 0x040000FD RID: 253
		internal const string InvalidOperation_EndWriteCalledMultiple = "InvalidOperation_EndWriteCalledMultiple";

		// Token: 0x040000FE RID: 254
		internal const string InvalidOperation_EndWaitForConnectionCalledMultiple = "InvalidOperation_EndWaitForConnectionCalledMultiple";

		// Token: 0x040000FF RID: 255
		internal const string InvalidOperation_PipeNotYetConnected = "InvalidOperation_PipeNotYetConnected";

		// Token: 0x04000100 RID: 256
		internal const string InvalidOperation_PipeDisconnected = "InvalidOperation_PipeDisconnected";

		// Token: 0x04000101 RID: 257
		internal const string InvalidOperation_PipeHandleNotSet = "InvalidOperation_PipeHandleNotSet";

		// Token: 0x04000102 RID: 258
		internal const string InvalidOperation_PipeNotAsync = "InvalidOperation_PipeNotAsync";

		// Token: 0x04000103 RID: 259
		internal const string InvalidOperation_PipeReadModeNotMessage = "InvalidOperation_PipeReadModeNotMessage";

		// Token: 0x04000104 RID: 260
		internal const string InvalidOperation_PipeMessageTypeNotSupported = "InvalidOperation_PipeMessageTypeNotSupported";

		// Token: 0x04000105 RID: 261
		internal const string InvalidOperation_PipeAlreadyConnected = "InvalidOperation_PipeAlreadyConnected";

		// Token: 0x04000106 RID: 262
		internal const string InvalidOperation_PipeAlreadyDisconnected = "InvalidOperation_PipeAlreadyDisconnected";

		// Token: 0x04000107 RID: 263
		internal const string InvalidOperation_PipeClosed = "InvalidOperation_PipeClosed";

		// Token: 0x04000108 RID: 264
		internal const string IO_FileTooLongOrHandleNotSync = "IO_FileTooLongOrHandleNotSync";

		// Token: 0x04000109 RID: 265
		internal const string IO_EOF_ReadBeyondEOF = "IO_EOF_ReadBeyondEOF";

		// Token: 0x0400010A RID: 266
		internal const string IO_FileNotFound = "IO_FileNotFound";

		// Token: 0x0400010B RID: 267
		internal const string IO_FileNotFound_FileName = "IO_FileNotFound_FileName";

		// Token: 0x0400010C RID: 268
		internal const string IO_IO_AlreadyExists_Name = "IO_IO_AlreadyExists_Name";

		// Token: 0x0400010D RID: 269
		internal const string IO_IO_BindHandleFailed = "IO_IO_BindHandleFailed";

		// Token: 0x0400010E RID: 270
		internal const string IO_IO_FileExists_Name = "IO_IO_FileExists_Name";

		// Token: 0x0400010F RID: 271
		internal const string IO_IO_NoPermissionToDirectoryName = "IO_IO_NoPermissionToDirectoryName";

		// Token: 0x04000110 RID: 272
		internal const string IO_IO_SharingViolation_File = "IO_IO_SharingViolation_File";

		// Token: 0x04000111 RID: 273
		internal const string IO_IO_SharingViolation_NoFileName = "IO_IO_SharingViolation_NoFileName";

		// Token: 0x04000112 RID: 274
		internal const string IO_IO_PipeBroken = "IO_IO_PipeBroken";

		// Token: 0x04000113 RID: 275
		internal const string IO_IO_InvalidPipeHandle = "IO_IO_InvalidPipeHandle";

		// Token: 0x04000114 RID: 276
		internal const string IO_OperationAborted = "IO_OperationAborted";

		// Token: 0x04000115 RID: 277
		internal const string IO_DriveNotFound_Drive = "IO_DriveNotFound_Drive";

		// Token: 0x04000116 RID: 278
		internal const string IO_PathNotFound_Path = "IO_PathNotFound_Path";

		// Token: 0x04000117 RID: 279
		internal const string IO_PathNotFound_NoPathName = "IO_PathNotFound_NoPathName";

		// Token: 0x04000118 RID: 280
		internal const string IO_PathTooLong = "IO_PathTooLong";

		// Token: 0x04000119 RID: 281
		internal const string NotSupported_IONonFileDevices = "NotSupported_IONonFileDevices";

		// Token: 0x0400011A RID: 282
		internal const string NotSupported_MemStreamNotExpandable = "NotSupported_MemStreamNotExpandable";

		// Token: 0x0400011B RID: 283
		internal const string NotSupported_UnreadableStream = "NotSupported_UnreadableStream";

		// Token: 0x0400011C RID: 284
		internal const string NotSupported_UnseekableStream = "NotSupported_UnseekableStream";

		// Token: 0x0400011D RID: 285
		internal const string NotSupported_UnwritableStream = "NotSupported_UnwritableStream";

		// Token: 0x0400011E RID: 286
		internal const string NotSupported_AnonymousPipeUnidirectional = "NotSupported_AnonymousPipeUnidirectional";

		// Token: 0x0400011F RID: 287
		internal const string NotSupported_AnonymousPipeMessagesNotSupported = "NotSupported_AnonymousPipeMessagesNotSupported";

		// Token: 0x04000120 RID: 288
		internal const string ObjectDisposed_FileClosed = "ObjectDisposed_FileClosed";

		// Token: 0x04000121 RID: 289
		internal const string ObjectDisposed_PipeClosed = "ObjectDisposed_PipeClosed";

		// Token: 0x04000122 RID: 290
		internal const string ObjectDisposed_ReaderClosed = "ObjectDisposed_ReaderClosed";

		// Token: 0x04000123 RID: 291
		internal const string ObjectDisposed_StreamClosed = "ObjectDisposed_StreamClosed";

		// Token: 0x04000124 RID: 292
		internal const string ObjectDisposed_WriterClosed = "ObjectDisposed_WriterClosed";

		// Token: 0x04000125 RID: 293
		internal const string PlatformNotSupported_NamedPipeServers = "PlatformNotSupported_NamedPipeServers";

		// Token: 0x04000126 RID: 294
		internal const string UnauthorizedAccess_IODenied_Path = "UnauthorizedAccess_IODenied_Path";

		// Token: 0x04000127 RID: 295
		internal const string UnauthorizedAccess_IODenied_NoPathName = "UnauthorizedAccess_IODenied_NoPathName";

		// Token: 0x04000128 RID: 296
		internal const string TraceAsTraceSource = "TraceAsTraceSource";

		// Token: 0x04000129 RID: 297
		internal const string ArgumentOutOfRange_NeedValidLogRetention = "ArgumentOutOfRange_NeedValidLogRetention";

		// Token: 0x0400012A RID: 298
		internal const string ArgumentOutOfRange_NeedMaxFileSizeGEBufferSize = "ArgumentOutOfRange_NeedMaxFileSizeGEBufferSize";

		// Token: 0x0400012B RID: 299
		internal const string ArgumentOutOfRange_NeedValidMaxNumFiles = "ArgumentOutOfRange_NeedValidMaxNumFiles";

		// Token: 0x0400012C RID: 300
		internal const string ArgumentOutOfRange_NeedValidId = "ArgumentOutOfRange_NeedValidId";

		// Token: 0x0400012D RID: 301
		internal const string ArgumentOutOfRange_MaxArgExceeded = "ArgumentOutOfRange_MaxArgExceeded";

		// Token: 0x0400012E RID: 302
		internal const string ArgumentOutOfRange_MaxStringsExceeded = "ArgumentOutOfRange_MaxStringsExceeded";

		// Token: 0x0400012F RID: 303
		internal const string NotSupported_DownLevelVista = "NotSupported_DownLevelVista";

		// Token: 0x04000130 RID: 304
		internal const string Argument_NeedNonemptyDelimiter = "Argument_NeedNonemptyDelimiter";

		// Token: 0x04000131 RID: 305
		internal const string NotSupported_SetTextWriter = "NotSupported_SetTextWriter";

		// Token: 0x04000132 RID: 306
		internal const string Perflib_PlatformNotSupported = "Perflib_PlatformNotSupported";

		// Token: 0x04000133 RID: 307
		internal const string Perflib_Argument_CounterSetAlreadyRegister = "Perflib_Argument_CounterSetAlreadyRegister";

		// Token: 0x04000134 RID: 308
		internal const string Perflib_Argument_InvalidCounterType = "Perflib_Argument_InvalidCounterType";

		// Token: 0x04000135 RID: 309
		internal const string Perflib_Argument_InvalidCounterSetInstanceType = "Perflib_Argument_InvalidCounterSetInstanceType";

		// Token: 0x04000136 RID: 310
		internal const string Perflib_Argument_InstanceAlreadyExists = "Perflib_Argument_InstanceAlreadyExists";

		// Token: 0x04000137 RID: 311
		internal const string Perflib_Argument_CounterAlreadyExists = "Perflib_Argument_CounterAlreadyExists";

		// Token: 0x04000138 RID: 312
		internal const string Perflib_Argument_CounterNameAlreadyExists = "Perflib_Argument_CounterNameAlreadyExists";

		// Token: 0x04000139 RID: 313
		internal const string Perflib_Argument_ProviderNotFound = "Perflib_Argument_ProviderNotFound";

		// Token: 0x0400013A RID: 314
		internal const string Perflib_Argument_InvalidInstance = "Perflib_Argument_InvalidInstance";

		// Token: 0x0400013B RID: 315
		internal const string Perflib_Argument_EmptyInstanceName = "Perflib_Argument_EmptyInstanceName";

		// Token: 0x0400013C RID: 316
		internal const string Perflib_Argument_EmptyCounterName = "Perflib_Argument_EmptyCounterName";

		// Token: 0x0400013D RID: 317
		internal const string Perflib_InsufficientMemory_InstanceCounterBlock = "Perflib_InsufficientMemory_InstanceCounterBlock";

		// Token: 0x0400013E RID: 318
		internal const string Perflib_InsufficientMemory_CounterSetTemplate = "Perflib_InsufficientMemory_CounterSetTemplate";

		// Token: 0x0400013F RID: 319
		internal const string Perflib_InvalidOperation_CounterRefValue = "Perflib_InvalidOperation_CounterRefValue";

		// Token: 0x04000140 RID: 320
		internal const string Perflib_InvalidOperation_CounterSetNotInstalled = "Perflib_InvalidOperation_CounterSetNotInstalled";

		// Token: 0x04000141 RID: 321
		internal const string Perflib_InvalidOperation_InstanceNotFound = "Perflib_InvalidOperation_InstanceNotFound";

		// Token: 0x04000142 RID: 322
		internal const string Perflib_InvalidOperation_AddCounterAfterInstance = "Perflib_InvalidOperation_AddCounterAfterInstance";

		// Token: 0x04000143 RID: 323
		internal const string Perflib_InvalidOperation_NoActiveProvider = "Perflib_InvalidOperation_NoActiveProvider";

		// Token: 0x04000144 RID: 324
		internal const string Perflib_InvalidOperation_CounterSetContainsNoCounter = "Perflib_InvalidOperation_CounterSetContainsNoCounter";

		// Token: 0x04000145 RID: 325
		internal const string Arg_ArrayPlusOffTooSmall = "Arg_ArrayPlusOffTooSmall";

		// Token: 0x04000146 RID: 326
		internal const string Arg_HSCapacityOverflow = "Arg_HSCapacityOverflow";

		// Token: 0x04000147 RID: 327
		internal const string InvalidOperation_EnumFailedVersion = "InvalidOperation_EnumFailedVersion";

		// Token: 0x04000148 RID: 328
		internal const string InvalidOperation_EnumOpCantHappen = "InvalidOperation_EnumOpCantHappen";

		// Token: 0x04000149 RID: 329
		internal const string Serialization_MissingKeys = "Serialization_MissingKeys";

		// Token: 0x0400014A RID: 330
		internal const string LockRecursionException_RecursiveReadNotAllowed = "LockRecursionException_RecursiveReadNotAllowed";

		// Token: 0x0400014B RID: 331
		internal const string LockRecursionException_RecursiveWriteNotAllowed = "LockRecursionException_RecursiveWriteNotAllowed";

		// Token: 0x0400014C RID: 332
		internal const string LockRecursionException_RecursiveUpgradeNotAllowed = "LockRecursionException_RecursiveUpgradeNotAllowed";

		// Token: 0x0400014D RID: 333
		internal const string LockRecursionException_ReadAfterWriteNotAllowed = "LockRecursionException_ReadAfterWriteNotAllowed";

		// Token: 0x0400014E RID: 334
		internal const string LockRecursionException_WriteAfterReadNotAllowed = "LockRecursionException_WriteAfterReadNotAllowed";

		// Token: 0x0400014F RID: 335
		internal const string LockRecursionException_UpgradeAfterReadNotAllowed = "LockRecursionException_UpgradeAfterReadNotAllowed";

		// Token: 0x04000150 RID: 336
		internal const string LockRecursionException_UpgradeAfterWriteNotAllowed = "LockRecursionException_UpgradeAfterWriteNotAllowed";

		// Token: 0x04000151 RID: 337
		internal const string SynchronizationLockException_MisMatchedRead = "SynchronizationLockException_MisMatchedRead";

		// Token: 0x04000152 RID: 338
		internal const string SynchronizationLockException_MisMatchedWrite = "SynchronizationLockException_MisMatchedWrite";

		// Token: 0x04000153 RID: 339
		internal const string SynchronizationLockException_MisMatchedUpgrade = "SynchronizationLockException_MisMatchedUpgrade";

		// Token: 0x04000154 RID: 340
		internal const string SynchronizationLockException_IncorrectDispose = "SynchronizationLockException_IncorrectDispose";

		// Token: 0x04000155 RID: 341
		internal const string Cryptography_ArgECDHKeySizeMismatch = "Cryptography_ArgECDHKeySizeMismatch";

		// Token: 0x04000156 RID: 342
		internal const string Cryptography_ArgECDHRequiresECDHKey = "Cryptography_ArgECDHRequiresECDHKey";

		// Token: 0x04000157 RID: 343
		internal const string Cryptography_ArgECDsaRequiresECDsaKey = "Cryptography_ArgECDsaRequiresECDsaKey";

		// Token: 0x04000158 RID: 344
		internal const string Cryptography_ArgExpectedECDiffieHellmanCngPublicKey = "Cryptography_ArgExpectedECDiffieHellmanCngPublicKey";

		// Token: 0x04000159 RID: 345
		internal const string Cryptography_ArgMustBeCngAlgorithm = "Cryptography_ArgMustBeCngAlgorithm";

		// Token: 0x0400015A RID: 346
		internal const string Cryptography_ArgMustBeCngAlgorithmGroup = "Cryptography_ArgMustBeCngAlgorithmGroup";

		// Token: 0x0400015B RID: 347
		internal const string Cryptography_ArgMustBeCngKeyBlobFormat = "Cryptography_ArgMustBeCngKeyBlobFormat";

		// Token: 0x0400015C RID: 348
		internal const string Cryptography_ArgMustBeCngProvider = "Cryptography_ArgMustBeCngProvider";

		// Token: 0x0400015D RID: 349
		internal const string Cryptography_DecryptWithNoKey = "Cryptography_DecryptWithNoKey";

		// Token: 0x0400015E RID: 350
		internal const string Cryptography_ECXmlSerializationFormatRequired = "Cryptography_ECXmlSerializationFormatRequired";

		// Token: 0x0400015F RID: 351
		internal const string Cryptography_InvalidAlgorithmGroup = "Cryptography_InvalidAlgorithmGroup";

		// Token: 0x04000160 RID: 352
		internal const string Cryptography_InvalidAlgorithmName = "Cryptography_InvalidAlgorithmName";

		// Token: 0x04000161 RID: 353
		internal const string Cryptography_InvalidCipherMode = "Cryptography_InvalidCipherMode";

		// Token: 0x04000162 RID: 354
		internal const string Cryptography_InvalidIVSize = "Cryptography_InvalidIVSize";

		// Token: 0x04000163 RID: 355
		internal const string Cryptography_InvalidKeyBlobFormat = "Cryptography_InvalidKeyBlobFormat";

		// Token: 0x04000164 RID: 356
		internal const string Cryptography_InvalidKeySize = "Cryptography_InvalidKeySize";

		// Token: 0x04000165 RID: 357
		internal const string Cryptography_InvalidPadding = "Cryptography_InvalidPadding";

		// Token: 0x04000166 RID: 358
		internal const string Cryptography_InvalidProviderName = "Cryptography_InvalidProviderName";

		// Token: 0x04000167 RID: 359
		internal const string Cryptography_MissingDomainParameters = "Cryptography_MissingDomainParameters";

		// Token: 0x04000168 RID: 360
		internal const string Cryptography_MissingPublicKey = "Cryptography_MissingPublicKey";

		// Token: 0x04000169 RID: 361
		internal const string Cryptography_MissingIV = "Cryptography_MissingIV";

		// Token: 0x0400016A RID: 362
		internal const string Cryptography_MustTransformWholeBlock = "Cryptography_MustTransformWholeBlock";

		// Token: 0x0400016B RID: 363
		internal const string Cryptography_NonCompliantFIPSAlgorithm = "Cryptography_NonCompliantFIPSAlgorithm";

		// Token: 0x0400016C RID: 364
		internal const string Cryptography_OpenInvalidHandle = "Cryptography_OpenInvalidHandle";

		// Token: 0x0400016D RID: 365
		internal const string Cryptography_OpenEphemeralKeyHandleWithoutEphemeralFlag = "Cryptography_OpenEphemeralKeyHandleWithoutEphemeralFlag";

		// Token: 0x0400016E RID: 366
		internal const string Cryptography_PartialBlock = "Cryptography_PartialBlock";

		// Token: 0x0400016F RID: 367
		internal const string Cryptography_PlatformNotSupported = "Cryptography_PlatformNotSupported";

		// Token: 0x04000170 RID: 368
		internal const string Cryptography_TlsRequiresLabelAndSeed = "Cryptography_TlsRequiresLabelAndSeed";

		// Token: 0x04000171 RID: 369
		internal const string Cryptography_TransformBeyondEndOfBuffer = "Cryptography_TransformBeyondEndOfBuffer";

		// Token: 0x04000172 RID: 370
		internal const string Cryptography_UnknownEllipticCurve = "Cryptography_UnknownEllipticCurve";

		// Token: 0x04000173 RID: 371
		internal const string Cryptography_UnknownEllipticCurveAlgorithm = "Cryptography_UnknownEllipticCurveAlgorithm";

		// Token: 0x04000174 RID: 372
		internal const string Cryptography_UnknownPaddingMode = "Cryptography_UnknownPaddingMode";

		// Token: 0x04000175 RID: 373
		internal const string Cryptography_UnexpectedXmlNamespace = "Cryptography_UnexpectedXmlNamespace";

		// Token: 0x04000176 RID: 374
		internal const string ArgumentException_RangeMinRangeMaxRangeType = "ArgumentException_RangeMinRangeMaxRangeType";

		// Token: 0x04000177 RID: 375
		internal const string ArgumentException_RangeNotIComparable = "ArgumentException_RangeNotIComparable";

		// Token: 0x04000178 RID: 376
		internal const string ArgumentException_RangeMaxRangeSmallerThanMinRange = "ArgumentException_RangeMaxRangeSmallerThanMinRange";

		// Token: 0x04000179 RID: 377
		internal const string ArgumentException_CountMaxLengthSmallerThanMinLength = "ArgumentException_CountMaxLengthSmallerThanMinLength";

		// Token: 0x0400017A RID: 378
		internal const string ArgumentException_LengthMaxLengthSmallerThanMinLength = "ArgumentException_LengthMaxLengthSmallerThanMinLength";

		// Token: 0x0400017B RID: 379
		internal const string ArgumentException_UnregisteredParameterName = "ArgumentException_UnregisteredParameterName";

		// Token: 0x0400017C RID: 380
		internal const string ArgumentException_InvalidParameterName = "ArgumentException_InvalidParameterName";

		// Token: 0x0400017D RID: 381
		internal const string ArgumentException_DuplicateName = "ArgumentException_DuplicateName";

		// Token: 0x0400017E RID: 382
		internal const string ArgumentException_DuplicatePosition = "ArgumentException_DuplicatePosition";

		// Token: 0x0400017F RID: 383
		internal const string ArgumentException_NoParametersFound = "ArgumentException_NoParametersFound";

		// Token: 0x04000180 RID: 384
		internal const string ArgumentException_HelpMessageBaseNameNullOrEmpty = "ArgumentException_HelpMessageBaseNameNullOrEmpty";

		// Token: 0x04000181 RID: 385
		internal const string ArgumentException_HelpMessageResourceIdNullOrEmpty = "ArgumentException_HelpMessageResourceIdNullOrEmpty";

		// Token: 0x04000182 RID: 386
		internal const string ArgumentException_HelpMessageNullOrEmpty = "ArgumentException_HelpMessageNullOrEmpty";

		// Token: 0x04000183 RID: 387
		internal const string ArgumentException_RegexPatternNullOrEmpty = "ArgumentException_RegexPatternNullOrEmpty";

		// Token: 0x04000184 RID: 388
		internal const string ArgumentException_RequiredPositionalAfterOptionalPositional = "ArgumentException_RequiredPositionalAfterOptionalPositional";

		// Token: 0x04000185 RID: 389
		internal const string ArgumentException_DuplicateParameterAttribute = "ArgumentException_DuplicateParameterAttribute";

		// Token: 0x04000186 RID: 390
		internal const string ArgumentException_MissingBaseNameOrResourceId = "ArgumentException_MissingBaseNameOrResourceId";

		// Token: 0x04000187 RID: 391
		internal const string ArgumentException_DuplicateRemainingArgumets = "ArgumentException_DuplicateRemainingArgumets";

		// Token: 0x04000188 RID: 392
		internal const string ArgumentException_TypeMismatchForRemainingArguments = "ArgumentException_TypeMismatchForRemainingArguments";

		// Token: 0x04000189 RID: 393
		internal const string ArgumentException_ValidationParameterTypeMismatch = "ArgumentException_ValidationParameterTypeMismatch";

		// Token: 0x0400018A RID: 394
		internal const string ArgumentException_ParserBuiltWithValueType = "ArgumentException_ParserBuiltWithValueType";

		// Token: 0x0400018B RID: 395
		internal const string InvalidOperationException_GetParameterTypeMismatch = "InvalidOperationException_GetParameterTypeMismatch";

		// Token: 0x0400018C RID: 396
		internal const string InvalidOperationException_GetParameterValueBeforeParse = "InvalidOperationException_GetParameterValueBeforeParse";

		// Token: 0x0400018D RID: 397
		internal const string InvalidOperationException_SetRemainingArgumentsParameterAfterParse = "InvalidOperationException_SetRemainingArgumentsParameterAfterParse";

		// Token: 0x0400018E RID: 398
		internal const string InvalidOperationException_AddParameterAfterParse = "InvalidOperationException_AddParameterAfterParse";

		// Token: 0x0400018F RID: 399
		internal const string InvalidOperationException_BindAfterBind = "InvalidOperationException_BindAfterBind";

		// Token: 0x04000190 RID: 400
		internal const string InvalidOperationException_GetRemainingArgumentsNotAllowed = "InvalidOperationException_GetRemainingArgumentsNotAllowed";

		// Token: 0x04000191 RID: 401
		internal const string InvalidOperationException_ParameterSetBeforeParse = "InvalidOperationException_ParameterSetBeforeParse";

		// Token: 0x04000192 RID: 402
		internal const string CommandLineParser_Aliases = "CommandLineParser_Aliases";

		// Token: 0x04000193 RID: 403
		internal const string CommandLineParser_ErrorMessagePrefix = "CommandLineParser_ErrorMessagePrefix";

		// Token: 0x04000194 RID: 404
		internal const string CommandLineParser_HelpMessagePrefix = "CommandLineParser_HelpMessagePrefix";

		// Token: 0x04000195 RID: 405
		internal const string ParameterBindingException_AmbiguousParameterName = "ParameterBindingException_AmbiguousParameterName";

		// Token: 0x04000196 RID: 406
		internal const string ParameterBindingException_ParameterValueAlreadySpecified = "ParameterBindingException_ParameterValueAlreadySpecified";

		// Token: 0x04000197 RID: 407
		internal const string ParameterBindingException_UnknownParameteName = "ParameterBindingException_UnknownParameteName";

		// Token: 0x04000198 RID: 408
		internal const string ParameterBindingException_RequiredParameterMissingCommandLineValue = "ParameterBindingException_RequiredParameterMissingCommandLineValue";

		// Token: 0x04000199 RID: 409
		internal const string ParameterBindingException_UnboundCommandLineArguments = "ParameterBindingException_UnboundCommandLineArguments";

		// Token: 0x0400019A RID: 410
		internal const string ParameterBindingException_UnboundMandatoryParameter = "ParameterBindingException_UnboundMandatoryParameter";

		// Token: 0x0400019B RID: 411
		internal const string ParameterBindingException_ResponseFileException = "ParameterBindingException_ResponseFileException";

		// Token: 0x0400019C RID: 412
		internal const string ParameterBindingException_ValididationError = "ParameterBindingException_ValididationError";

		// Token: 0x0400019D RID: 413
		internal const string ParameterBindingException_TransformationError = "ParameterBindingException_TransformationError";

		// Token: 0x0400019E RID: 414
		internal const string ParameterBindingException_AmbiguousParameterSet = "ParameterBindingException_AmbiguousParameterSet";

		// Token: 0x0400019F RID: 415
		internal const string ParameterBindingException_UnknownParameterSet = "ParameterBindingException_UnknownParameterSet";

		// Token: 0x040001A0 RID: 416
		internal const string ParameterBindingException_NestedResponseFiles = "ParameterBindingException_NestedResponseFiles";

		// Token: 0x040001A1 RID: 417
		internal const string ValidateMetadataException_RangeGreaterThanMaxRangeFailure = "ValidateMetadataException_RangeGreaterThanMaxRangeFailure";

		// Token: 0x040001A2 RID: 418
		internal const string ValidateMetadataException_RangeSmallerThanMinRangeFailure = "ValidateMetadataException_RangeSmallerThanMinRangeFailure";

		// Token: 0x040001A3 RID: 419
		internal const string ValidateMetadataException_PatternFailure = "ValidateMetadataException_PatternFailure";

		// Token: 0x040001A4 RID: 420
		internal const string ValidateMetadataException_CountMinLengthFailure = "ValidateMetadataException_CountMinLengthFailure";

		// Token: 0x040001A5 RID: 421
		internal const string ValidateMetadataException_CountMaxLengthFailure = "ValidateMetadataException_CountMaxLengthFailure";

		// Token: 0x040001A6 RID: 422
		internal const string ValidateMetadataException_LengthMinLengthFailure = "ValidateMetadataException_LengthMinLengthFailure";

		// Token: 0x040001A7 RID: 423
		internal const string ValidateMetadataException_LengthMaxLengthFailure = "ValidateMetadataException_LengthMaxLengthFailure";

		// Token: 0x040001A8 RID: 424
		internal const string Argument_MapNameEmptyString = "Argument_MapNameEmptyString";

		// Token: 0x040001A9 RID: 425
		internal const string Argument_EmptyFile = "Argument_EmptyFile";

		// Token: 0x040001AA RID: 426
		internal const string Argument_NewMMFWriteAccessNotAllowed = "Argument_NewMMFWriteAccessNotAllowed";

		// Token: 0x040001AB RID: 427
		internal const string Argument_ReadAccessWithLargeCapacity = "Argument_ReadAccessWithLargeCapacity";

		// Token: 0x040001AC RID: 428
		internal const string Argument_NewMMFAppendModeNotAllowed = "Argument_NewMMFAppendModeNotAllowed";

		// Token: 0x040001AD RID: 429
		internal const string ArgumentNull_MapName = "ArgumentNull_MapName";

		// Token: 0x040001AE RID: 430
		internal const string ArgumentNull_FileStream = "ArgumentNull_FileStream";

		// Token: 0x040001AF RID: 431
		internal const string ArgumentOutOfRange_CapacityLargerThanLogicalAddressSpaceNotAllowed = "ArgumentOutOfRange_CapacityLargerThanLogicalAddressSpaceNotAllowed";

		// Token: 0x040001B0 RID: 432
		internal const string ArgumentOutOfRange_NeedPositiveNumber = "ArgumentOutOfRange_NeedPositiveNumber";

		// Token: 0x040001B1 RID: 433
		internal const string ArgumentOutOfRange_PositiveOrDefaultCapacityRequired = "ArgumentOutOfRange_PositiveOrDefaultCapacityRequired";

		// Token: 0x040001B2 RID: 434
		internal const string ArgumentOutOfRange_PositiveOrDefaultSizeRequired = "ArgumentOutOfRange_PositiveOrDefaultSizeRequired";

		// Token: 0x040001B3 RID: 435
		internal const string ArgumentOutOfRange_PositionLessThanCapacityRequired = "ArgumentOutOfRange_PositionLessThanCapacityRequired";

		// Token: 0x040001B4 RID: 436
		internal const string ArgumentOutOfRange_CapacityGEFileSizeRequired = "ArgumentOutOfRange_CapacityGEFileSizeRequired";

		// Token: 0x040001B5 RID: 437
		internal const string IO_NotEnoughMemory = "IO_NotEnoughMemory";

		// Token: 0x040001B6 RID: 438
		internal const string InvalidOperation_CalledTwice = "InvalidOperation_CalledTwice";

		// Token: 0x040001B7 RID: 439
		internal const string InvalidOperation_CantCreateFileMapping = "InvalidOperation_CantCreateFileMapping";

		// Token: 0x040001B8 RID: 440
		internal const string InvalidOperation_ViewIsNull = "InvalidOperation_ViewIsNull";

		// Token: 0x040001B9 RID: 441
		internal const string NotSupported_DelayAllocateFileBackedNotAllowed = "NotSupported_DelayAllocateFileBackedNotAllowed";

		// Token: 0x040001BA RID: 442
		internal const string NotSupported_MMViewStreamsFixedLength = "NotSupported_MMViewStreamsFixedLength";

		// Token: 0x040001BB RID: 443
		internal const string ObjectDisposed_ViewAccessorClosed = "ObjectDisposed_ViewAccessorClosed";

		// Token: 0x040001BC RID: 444
		internal const string ObjectDisposed_StreamIsClosed = "ObjectDisposed_StreamIsClosed";

		// Token: 0x040001BD RID: 445
		internal const string NotSupported_Method = "NotSupported_Method";

		// Token: 0x040001BE RID: 446
		internal const string NotSupported_SubclassOverride = "NotSupported_SubclassOverride";

		// Token: 0x040001BF RID: 447
		internal const string Cryptography_ArgDSARequiresDSAKey = "Cryptography_ArgDSARequiresDSAKey";

		// Token: 0x040001C0 RID: 448
		internal const string Cryptography_ArgRSAaRequiresRSAKey = "Cryptography_ArgRSAaRequiresRSAKey";

		// Token: 0x040001C1 RID: 449
		internal const string Cryptography_CngKeyWrongAlgorithm = "Cryptography_CngKeyWrongAlgorithm";

		// Token: 0x040001C2 RID: 450
		internal const string Cryptography_DSA_HashTooShort = "Cryptography_DSA_HashTooShort";

		// Token: 0x040001C3 RID: 451
		internal const string Cryptography_HashAlgorithmNameNullOrEmpty = "Cryptography_HashAlgorithmNameNullOrEmpty";

		// Token: 0x040001C4 RID: 452
		internal const string Cryptography_InvalidDsaParameters_MissingFields = "Cryptography_InvalidDsaParameters_MissingFields";

		// Token: 0x040001C5 RID: 453
		internal const string Cryptography_InvalidDsaParameters_MismatchedPGY = "Cryptography_InvalidDsaParameters_MismatchedPGY";

		// Token: 0x040001C6 RID: 454
		internal const string Cryptography_InvalidDsaParameters_MismatchedQX = "Cryptography_InvalidDsaParameters_MismatchedQX";

		// Token: 0x040001C7 RID: 455
		internal const string Cryptography_InvalidDsaParameters_MismatchedPJ = "Cryptography_InvalidDsaParameters_MismatchedPJ";

		// Token: 0x040001C8 RID: 456
		internal const string Cryptography_InvalidDsaParameters_SeedRestriction_ShortKey = "Cryptography_InvalidDsaParameters_SeedRestriction_ShortKey";

		// Token: 0x040001C9 RID: 457
		internal const string Cryptography_InvalidDsaParameters_QRestriction_ShortKey = "Cryptography_InvalidDsaParameters_QRestriction_ShortKey";

		// Token: 0x040001CA RID: 458
		internal const string Cryptography_InvalidDsaParameters_QRestriction_LargeKey = "Cryptography_InvalidDsaParameters_QRestriction_LargeKey";

		// Token: 0x040001CB RID: 459
		internal const string Cryptography_InvalidRsaParameters = "Cryptography_InvalidRsaParameters";

		// Token: 0x040001CC RID: 460
		internal const string Cryptography_InvalidSignatureAlgorithm = "Cryptography_InvalidSignatureAlgorithm";

		// Token: 0x040001CD RID: 461
		internal const string Cryptography_KeyBlobParsingError = "Cryptography_KeyBlobParsingError";

		// Token: 0x040001CE RID: 462
		internal const string Cryptography_NotSupportedKeyAlgorithm = "Cryptography_NotSupportedKeyAlgorithm";

		// Token: 0x040001CF RID: 463
		internal const string Cryptography_NotValidPublicOrPrivateKey = "Cryptography_NotValidPublicOrPrivateKey";

		// Token: 0x040001D0 RID: 464
		internal const string Cryptography_NotValidPrivateKey = "Cryptography_NotValidPrivateKey";

		// Token: 0x040001D1 RID: 465
		internal const string Cryptography_UnexpectedTransformTruncation = "Cryptography_UnexpectedTransformTruncation";

		// Token: 0x040001D2 RID: 466
		internal const string Cryptography_UnsupportedPaddingMode = "Cryptography_UnsupportedPaddingMode";

		// Token: 0x040001D3 RID: 467
		internal const string Cryptography_WeakKey = "Cryptography_WeakKey";

		// Token: 0x040001D4 RID: 468
		internal const string Cryptography_CurveNotSupported = "Cryptography_CurveNotSupported";

		// Token: 0x040001D5 RID: 469
		internal const string Cryptography_InvalidCurve = "Cryptography_InvalidCurve";

		// Token: 0x040001D6 RID: 470
		internal const string Cryptography_InvalidCurveOid = "Cryptography_InvalidCurveOid";

		// Token: 0x040001D7 RID: 471
		internal const string Cryptography_InvalidCurveKeyParameters = "Cryptography_InvalidCurveKeyParameters";

		// Token: 0x040001D8 RID: 472
		internal const string Cryptography_InvalidECCharacteristic2Curve = "Cryptography_InvalidECCharacteristic2Curve";

		// Token: 0x040001D9 RID: 473
		internal const string Cryptography_InvalidECPrimeCurve = "Cryptography_InvalidECPrimeCurve";

		// Token: 0x040001DA RID: 474
		internal const string Cryptography_InvalidECNamedCurve = "Cryptography_InvalidECNamedCurve";

		// Token: 0x040001DB RID: 475
		internal const string Cryptography_UnknownHashAlgorithm = "Cryptography_UnknownHashAlgorithm";

		// Token: 0x040001DC RID: 476
		internal const string Argument_Invalid_SafeHandleInvalidOrClosed = "Argument_Invalid_SafeHandleInvalidOrClosed";

		// Token: 0x040001DD RID: 477
		internal const string Arg_EmptyOrNullArray = "Arg_EmptyOrNullArray";

		// Token: 0x040001DE RID: 478
		internal const string Arg_EmptyOrNullString = "Arg_EmptyOrNullString";

		// Token: 0x040001DF RID: 479
		internal const string Argument_InvalidOidValue = "Argument_InvalidOidValue";

		// Token: 0x040001E0 RID: 480
		internal const string Cryptography_Cert_AlreadyHasPrivateKey = "Cryptography_Cert_AlreadyHasPrivateKey";

		// Token: 0x040001E1 RID: 481
		internal const string Cryptography_CertReq_AlgorithmMustMatch = "Cryptography_CertReq_AlgorithmMustMatch";

		// Token: 0x040001E2 RID: 482
		internal const string Cryptography_CertReq_BasicConstraintsRequired = "Cryptography_CertReq_BasicConstraintsRequired";

		// Token: 0x040001E3 RID: 483
		internal const string Cryptography_CertReq_DatesReversed = "Cryptography_CertReq_DatesReversed";

		// Token: 0x040001E4 RID: 484
		internal const string Cryptography_CertReq_DateTooOld = "Cryptography_CertReq_DateTooOld";

		// Token: 0x040001E5 RID: 485
		internal const string Cryptography_CertReq_DuplicateExtension = "Cryptography_CertReq_DuplicateExtension";

		// Token: 0x040001E6 RID: 486
		internal const string Cryptography_CertReq_IssuerBasicConstraintsInvalid = "Cryptography_CertReq_IssuerBasicConstraintsInvalid";

		// Token: 0x040001E7 RID: 487
		internal const string Cryptography_CertReq_IssuerKeyUsageInvalid = "Cryptography_CertReq_IssuerKeyUsageInvalid";

		// Token: 0x040001E8 RID: 488
		internal const string Cryptography_CertReq_IssuerRequiresPrivateKey = "Cryptography_CertReq_IssuerRequiresPrivateKey";

		// Token: 0x040001E9 RID: 489
		internal const string Cryptography_CertReq_NoKeyProvided = "Cryptography_CertReq_NoKeyProvided";

		// Token: 0x040001EA RID: 490
		internal const string Cryptography_CertReq_NotAfterNotNested = "Cryptography_CertReq_NotAfterNotNested";

		// Token: 0x040001EB RID: 491
		internal const string Cryptography_CertReq_NotBeforeNotNested = "Cryptography_CertReq_NotBeforeNotNested";

		// Token: 0x040001EC RID: 492
		internal const string Cryptography_CertReq_RSAPaddingRequired = "Cryptography_CertReq_RSAPaddingRequired";

		// Token: 0x040001ED RID: 493
		internal const string Cryptography_Der_Invalid_Encoding = "Cryptography_Der_Invalid_Encoding";

		// Token: 0x040001EE RID: 494
		internal const string Cryptography_ECC_NamedCurvesOnly = "Cryptography_ECC_NamedCurvesOnly";

		// Token: 0x040001EF RID: 495
		internal const string Cryptography_Invalid_IA5String = "Cryptography_Invalid_IA5String";

		// Token: 0x040001F0 RID: 496
		internal const string Cryptography_InvalidPaddingMode = "Cryptography_InvalidPaddingMode";

		// Token: 0x040001F1 RID: 497
		internal const string Cryptography_InvalidPublicKey_Object = "Cryptography_InvalidPublicKey_Object";

		// Token: 0x040001F2 RID: 498
		internal const string Cryptography_PrivateKey_DoesNotMatch = "Cryptography_PrivateKey_DoesNotMatch";

		// Token: 0x040001F3 RID: 499
		internal const string Cryptography_PrivateKey_WrongAlgorithm = "Cryptography_PrivateKey_WrongAlgorithm";

		// Token: 0x040001F4 RID: 500
		internal const string Cryptography_UnknownKeyAlgorithm = "Cryptography_UnknownKeyAlgorithm";

		// Token: 0x040001F5 RID: 501
		private static SR loader;

		// Token: 0x040001F6 RID: 502
		private ResourceManager resources;
	}
}
