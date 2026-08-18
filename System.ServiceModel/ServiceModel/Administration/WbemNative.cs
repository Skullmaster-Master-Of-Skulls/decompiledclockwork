using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000456 RID: 1110
	[SuppressUnmanagedCodeSecurity]
	internal class WbemNative
	{
		// Token: 0x02000C1B RID: 3099
		internal enum WbemStatus
		{
			// Token: 0x04004334 RID: 17204
			WBEM_NO_ERROR,
			// Token: 0x04004335 RID: 17205
			WBEM_S_NO_ERROR = 0,
			// Token: 0x04004336 RID: 17206
			WBEM_S_SAME = 0,
			// Token: 0x04004337 RID: 17207
			WBEM_S_FALSE,
			// Token: 0x04004338 RID: 17208
			WBEM_S_ALREADY_EXISTS = 262145,
			// Token: 0x04004339 RID: 17209
			WBEM_S_RESET_TO_DEFAULT,
			// Token: 0x0400433A RID: 17210
			WBEM_S_DIFFERENT,
			// Token: 0x0400433B RID: 17211
			WBEM_S_TIMEDOUT,
			// Token: 0x0400433C RID: 17212
			WBEM_S_NO_MORE_DATA,
			// Token: 0x0400433D RID: 17213
			WBEM_S_OPERATION_CANCELLED,
			// Token: 0x0400433E RID: 17214
			WBEM_S_PENDING,
			// Token: 0x0400433F RID: 17215
			WBEM_S_DUPLICATE_OBJECTS,
			// Token: 0x04004340 RID: 17216
			WBEM_S_ACCESS_DENIED,
			// Token: 0x04004341 RID: 17217
			WBEM_S_PARTIAL_RESULTS = 262160,
			// Token: 0x04004342 RID: 17218
			WBEM_S_NO_POSTHOOK,
			// Token: 0x04004343 RID: 17219
			WBEM_S_POSTHOOK_WITH_BOTH,
			// Token: 0x04004344 RID: 17220
			WBEM_S_POSTHOOK_WITH_NEW,
			// Token: 0x04004345 RID: 17221
			WBEM_S_POSTHOOK_WITH_STATUS,
			// Token: 0x04004346 RID: 17222
			WBEM_S_POSTHOOK_WITH_OLD,
			// Token: 0x04004347 RID: 17223
			WBEM_S_REDO_PREHOOK_WITH_ORIGINAL_OBJECT,
			// Token: 0x04004348 RID: 17224
			WBEM_S_SOURCE_NOT_AVAILABLE,
			// Token: 0x04004349 RID: 17225
			WBEM_E_FAILED = -2147217407,
			// Token: 0x0400434A RID: 17226
			WBEM_E_NOT_FOUND,
			// Token: 0x0400434B RID: 17227
			WBEM_E_ACCESS_DENIED,
			// Token: 0x0400434C RID: 17228
			WBEM_E_PROVIDER_FAILURE,
			// Token: 0x0400434D RID: 17229
			WBEM_E_TYPE_MISMATCH,
			// Token: 0x0400434E RID: 17230
			WBEM_E_OUT_OF_MEMORY,
			// Token: 0x0400434F RID: 17231
			WBEM_E_INVALID_CONTEXT,
			// Token: 0x04004350 RID: 17232
			WBEM_E_INVALID_PARAMETER,
			// Token: 0x04004351 RID: 17233
			WBEM_E_NOT_AVAILABLE,
			// Token: 0x04004352 RID: 17234
			WBEM_E_CRITICAL_ERROR,
			// Token: 0x04004353 RID: 17235
			WBEM_E_INVALID_STREAM,
			// Token: 0x04004354 RID: 17236
			WBEM_E_NOT_SUPPORTED,
			// Token: 0x04004355 RID: 17237
			WBEM_E_INVALID_SUPERCLASS,
			// Token: 0x04004356 RID: 17238
			WBEM_E_INVALID_NAMESPACE,
			// Token: 0x04004357 RID: 17239
			WBEM_E_INVALID_OBJECT,
			// Token: 0x04004358 RID: 17240
			WBEM_E_INVALID_CLASS,
			// Token: 0x04004359 RID: 17241
			WBEM_E_PROVIDER_NOT_FOUND,
			// Token: 0x0400435A RID: 17242
			WBEM_E_INVALID_PROVIDER_REGISTRATION,
			// Token: 0x0400435B RID: 17243
			WBEM_E_PROVIDER_LOAD_FAILURE,
			// Token: 0x0400435C RID: 17244
			WBEM_E_INITIALIZATION_FAILURE,
			// Token: 0x0400435D RID: 17245
			WBEM_E_TRANSPORT_FAILURE,
			// Token: 0x0400435E RID: 17246
			WBEM_E_INVALID_OPERATION,
			// Token: 0x0400435F RID: 17247
			WBEM_E_INVALID_QUERY,
			// Token: 0x04004360 RID: 17248
			WBEM_E_INVALID_QUERY_TYPE,
			// Token: 0x04004361 RID: 17249
			WBEM_E_ALREADY_EXISTS,
			// Token: 0x04004362 RID: 17250
			WBEM_E_OVERRIDE_NOT_ALLOWED,
			// Token: 0x04004363 RID: 17251
			WBEM_E_PROPAGATED_QUALIFIER,
			// Token: 0x04004364 RID: 17252
			WBEM_E_PROPAGATED_PROPERTY,
			// Token: 0x04004365 RID: 17253
			WBEM_E_UNEXPECTED,
			// Token: 0x04004366 RID: 17254
			WBEM_E_ILLEGAL_OPERATION,
			// Token: 0x04004367 RID: 17255
			WBEM_E_CANNOT_BE_KEY,
			// Token: 0x04004368 RID: 17256
			WBEM_E_INCOMPLETE_CLASS,
			// Token: 0x04004369 RID: 17257
			WBEM_E_INVALID_SYNTAX,
			// Token: 0x0400436A RID: 17258
			WBEM_E_NONDECORATED_OBJECT,
			// Token: 0x0400436B RID: 17259
			WBEM_E_READ_ONLY,
			// Token: 0x0400436C RID: 17260
			WBEM_E_PROVIDER_NOT_CAPABLE,
			// Token: 0x0400436D RID: 17261
			WBEM_E_CLASS_HAS_CHILDREN,
			// Token: 0x0400436E RID: 17262
			WBEM_E_CLASS_HAS_INSTANCES,
			// Token: 0x0400436F RID: 17263
			WBEM_E_QUERY_NOT_IMPLEMENTED,
			// Token: 0x04004370 RID: 17264
			WBEM_E_ILLEGAL_NULL,
			// Token: 0x04004371 RID: 17265
			WBEM_E_INVALID_QUALIFIER_TYPE,
			// Token: 0x04004372 RID: 17266
			WBEM_E_INVALID_PROPERTY_TYPE,
			// Token: 0x04004373 RID: 17267
			WBEM_E_VALUE_OUT_OF_RANGE,
			// Token: 0x04004374 RID: 17268
			WBEM_E_CANNOT_BE_SINGLETON,
			// Token: 0x04004375 RID: 17269
			WBEM_E_INVALID_CIM_TYPE,
			// Token: 0x04004376 RID: 17270
			WBEM_E_INVALID_METHOD,
			// Token: 0x04004377 RID: 17271
			WBEM_E_INVALID_METHOD_PARAMETERS,
			// Token: 0x04004378 RID: 17272
			WBEM_E_SYSTEM_PROPERTY,
			// Token: 0x04004379 RID: 17273
			WBEM_E_INVALID_PROPERTY,
			// Token: 0x0400437A RID: 17274
			WBEM_E_CALL_CANCELLED,
			// Token: 0x0400437B RID: 17275
			WBEM_E_SHUTTING_DOWN,
			// Token: 0x0400437C RID: 17276
			WBEM_E_PROPAGATED_METHOD,
			// Token: 0x0400437D RID: 17277
			WBEM_E_UNSUPPORTED_PARAMETER,
			// Token: 0x0400437E RID: 17278
			WBEM_E_MISSING_PARAMETER_ID,
			// Token: 0x0400437F RID: 17279
			WBEM_E_INVALID_PARAMETER_ID,
			// Token: 0x04004380 RID: 17280
			WBEM_E_NONCONSECUTIVE_PARAMETER_IDS,
			// Token: 0x04004381 RID: 17281
			WBEM_E_PARAMETER_ID_ON_RETVAL,
			// Token: 0x04004382 RID: 17282
			WBEM_E_INVALID_OBJECT_PATH,
			// Token: 0x04004383 RID: 17283
			WBEM_E_OUT_OF_DISK_SPACE,
			// Token: 0x04004384 RID: 17284
			WBEM_E_BUFFER_TOO_SMALL,
			// Token: 0x04004385 RID: 17285
			WBEM_E_UNSUPPORTED_PUT_EXTENSION,
			// Token: 0x04004386 RID: 17286
			WBEM_E_UNKNOWN_OBJECT_TYPE,
			// Token: 0x04004387 RID: 17287
			WBEM_E_UNKNOWN_PACKET_TYPE,
			// Token: 0x04004388 RID: 17288
			WBEM_E_MARSHAL_VERSION_MISMATCH,
			// Token: 0x04004389 RID: 17289
			WBEM_E_MARSHAL_INVALID_SIGNATURE,
			// Token: 0x0400438A RID: 17290
			WBEM_E_INVALID_QUALIFIER,
			// Token: 0x0400438B RID: 17291
			WBEM_E_INVALID_DUPLICATE_PARAMETER,
			// Token: 0x0400438C RID: 17292
			WBEM_E_TOO_MUCH_DATA,
			// Token: 0x0400438D RID: 17293
			WBEM_E_SERVER_TOO_BUSY,
			// Token: 0x0400438E RID: 17294
			WBEM_E_INVALID_FLAVOR,
			// Token: 0x0400438F RID: 17295
			WBEM_E_CIRCULAR_REFERENCE,
			// Token: 0x04004390 RID: 17296
			WBEM_E_UNSUPPORTED_CLASS_UPDATE,
			// Token: 0x04004391 RID: 17297
			WBEM_E_CANNOT_CHANGE_KEY_INHERITANCE,
			// Token: 0x04004392 RID: 17298
			WBEM_E_CANNOT_CHANGE_INDEX_INHERITANCE = -2147217328,
			// Token: 0x04004393 RID: 17299
			WBEM_E_TOO_MANY_PROPERTIES,
			// Token: 0x04004394 RID: 17300
			WBEM_E_UPDATE_TYPE_MISMATCH,
			// Token: 0x04004395 RID: 17301
			WBEM_E_UPDATE_OVERRIDE_NOT_ALLOWED,
			// Token: 0x04004396 RID: 17302
			WBEM_E_UPDATE_PROPAGATED_METHOD,
			// Token: 0x04004397 RID: 17303
			WBEM_E_METHOD_NOT_IMPLEMENTED,
			// Token: 0x04004398 RID: 17304
			WBEM_E_METHOD_DISABLED,
			// Token: 0x04004399 RID: 17305
			WBEM_E_REFRESHER_BUSY,
			// Token: 0x0400439A RID: 17306
			WBEM_E_UNPARSABLE_QUERY,
			// Token: 0x0400439B RID: 17307
			WBEM_E_NOT_EVENT_CLASS,
			// Token: 0x0400439C RID: 17308
			WBEM_E_MISSING_GROUP_WITHIN,
			// Token: 0x0400439D RID: 17309
			WBEM_E_MISSING_AGGREGATION_LIST,
			// Token: 0x0400439E RID: 17310
			WBEM_E_PROPERTY_NOT_AN_OBJECT,
			// Token: 0x0400439F RID: 17311
			WBEM_E_AGGREGATING_BY_OBJECT,
			// Token: 0x040043A0 RID: 17312
			WBEM_E_UNINTERPRETABLE_PROVIDER_QUERY = -2147217313,
			// Token: 0x040043A1 RID: 17313
			WBEM_E_BACKUP_RESTORE_WINMGMT_RUNNING,
			// Token: 0x040043A2 RID: 17314
			WBEM_E_QUEUE_OVERFLOW,
			// Token: 0x040043A3 RID: 17315
			WBEM_E_PRIVILEGE_NOT_HELD,
			// Token: 0x040043A4 RID: 17316
			WBEM_E_INVALID_OPERATOR,
			// Token: 0x040043A5 RID: 17317
			WBEM_E_LOCAL_CREDENTIALS,
			// Token: 0x040043A6 RID: 17318
			WBEM_E_CANNOT_BE_ABSTRACT,
			// Token: 0x040043A7 RID: 17319
			WBEM_E_AMENDED_OBJECT,
			// Token: 0x040043A8 RID: 17320
			WBEM_E_CLIENT_TOO_SLOW,
			// Token: 0x040043A9 RID: 17321
			WBEM_E_NULL_SECURITY_DESCRIPTOR,
			// Token: 0x040043AA RID: 17322
			WBEM_E_TIMED_OUT,
			// Token: 0x040043AB RID: 17323
			WBEM_E_INVALID_ASSOCIATION,
			// Token: 0x040043AC RID: 17324
			WBEM_E_AMBIGUOUS_OPERATION,
			// Token: 0x040043AD RID: 17325
			WBEM_E_QUOTA_VIOLATION,
			// Token: 0x040043AE RID: 17326
			WBEM_E_RESERVED_001,
			// Token: 0x040043AF RID: 17327
			WBEM_E_RESERVED_002,
			// Token: 0x040043B0 RID: 17328
			WBEM_E_UNSUPPORTED_LOCALE,
			// Token: 0x040043B1 RID: 17329
			WBEM_E_HANDLE_OUT_OF_DATE,
			// Token: 0x040043B2 RID: 17330
			WBEM_E_CONNECTION_FAILED,
			// Token: 0x040043B3 RID: 17331
			WBEM_E_INVALID_HANDLE_REQUEST,
			// Token: 0x040043B4 RID: 17332
			WBEM_E_PROPERTY_NAME_TOO_WIDE,
			// Token: 0x040043B5 RID: 17333
			WBEM_E_CLASS_NAME_TOO_WIDE,
			// Token: 0x040043B6 RID: 17334
			WBEM_E_METHOD_NAME_TOO_WIDE,
			// Token: 0x040043B7 RID: 17335
			WBEM_E_QUALIFIER_NAME_TOO_WIDE,
			// Token: 0x040043B8 RID: 17336
			WBEM_E_RERUN_COMMAND,
			// Token: 0x040043B9 RID: 17337
			WBEM_E_DATABASE_VER_MISMATCH,
			// Token: 0x040043BA RID: 17338
			WBEM_E_VETO_DELETE,
			// Token: 0x040043BB RID: 17339
			WBEM_E_VETO_PUT,
			// Token: 0x040043BC RID: 17340
			WBEM_E_INVALID_LOCALE = -2147217280,
			// Token: 0x040043BD RID: 17341
			WBEM_E_PROVIDER_SUSPENDED,
			// Token: 0x040043BE RID: 17342
			WBEM_E_SYNCHRONIZATION_REQUIRED,
			// Token: 0x040043BF RID: 17343
			WBEM_E_NO_SCHEMA,
			// Token: 0x040043C0 RID: 17344
			WBEM_E_PROVIDER_ALREADY_REGISTERED,
			// Token: 0x040043C1 RID: 17345
			WBEM_E_PROVIDER_NOT_REGISTERED,
			// Token: 0x040043C2 RID: 17346
			WBEM_E_FATAL_TRANSPORT_ERROR,
			// Token: 0x040043C3 RID: 17347
			WBEM_E_ENCRYPTED_CONNECTION_REQUIRED,
			// Token: 0x040043C4 RID: 17348
			WBEM_E_PROVIDER_TIMED_OUT,
			// Token: 0x040043C5 RID: 17349
			WBEM_E_NO_KEY,
			// Token: 0x040043C6 RID: 17350
			WBEMESS_E_REGISTRATION_TOO_BROAD = -2147213311,
			// Token: 0x040043C7 RID: 17351
			WBEMESS_E_REGISTRATION_TOO_PRECISE,
			// Token: 0x040043C8 RID: 17352
			WBEMMOF_E_EXPECTED_QUALIFIER_NAME = -2147205119,
			// Token: 0x040043C9 RID: 17353
			WBEMMOF_E_EXPECTED_SEMI,
			// Token: 0x040043CA RID: 17354
			WBEMMOF_E_EXPECTED_OPEN_BRACE,
			// Token: 0x040043CB RID: 17355
			WBEMMOF_E_EXPECTED_CLOSE_BRACE,
			// Token: 0x040043CC RID: 17356
			WBEMMOF_E_EXPECTED_CLOSE_BRACKET,
			// Token: 0x040043CD RID: 17357
			WBEMMOF_E_EXPECTED_CLOSE_PAREN,
			// Token: 0x040043CE RID: 17358
			WBEMMOF_E_ILLEGAL_CONSTANT_VALUE,
			// Token: 0x040043CF RID: 17359
			WBEMMOF_E_EXPECTED_TYPE_IDENTIFIER,
			// Token: 0x040043D0 RID: 17360
			WBEMMOF_E_EXPECTED_OPEN_PAREN,
			// Token: 0x040043D1 RID: 17361
			WBEMMOF_E_UNRECOGNIZED_TOKEN,
			// Token: 0x040043D2 RID: 17362
			WBEMMOF_E_UNRECOGNIZED_TYPE,
			// Token: 0x040043D3 RID: 17363
			WBEMMOF_E_EXPECTED_PROPERTY_NAME,
			// Token: 0x040043D4 RID: 17364
			WBEMMOF_E_TYPEDEF_NOT_SUPPORTED,
			// Token: 0x040043D5 RID: 17365
			WBEMMOF_E_UNEXPECTED_ALIAS,
			// Token: 0x040043D6 RID: 17366
			WBEMMOF_E_UNEXPECTED_ARRAY_INIT,
			// Token: 0x040043D7 RID: 17367
			WBEMMOF_E_INVALID_AMENDMENT_SYNTAX,
			// Token: 0x040043D8 RID: 17368
			WBEMMOF_E_INVALID_DUPLICATE_AMENDMENT,
			// Token: 0x040043D9 RID: 17369
			WBEMMOF_E_INVALID_PRAGMA,
			// Token: 0x040043DA RID: 17370
			WBEMMOF_E_INVALID_NAMESPACE_SYNTAX,
			// Token: 0x040043DB RID: 17371
			WBEMMOF_E_EXPECTED_CLASS_NAME,
			// Token: 0x040043DC RID: 17372
			WBEMMOF_E_TYPE_MISMATCH,
			// Token: 0x040043DD RID: 17373
			WBEMMOF_E_EXPECTED_ALIAS_NAME,
			// Token: 0x040043DE RID: 17374
			WBEMMOF_E_INVALID_CLASS_DECLARATION,
			// Token: 0x040043DF RID: 17375
			WBEMMOF_E_INVALID_INSTANCE_DECLARATION,
			// Token: 0x040043E0 RID: 17376
			WBEMMOF_E_EXPECTED_DOLLAR,
			// Token: 0x040043E1 RID: 17377
			WBEMMOF_E_CIMTYPE_QUALIFIER,
			// Token: 0x040043E2 RID: 17378
			WBEMMOF_E_DUPLICATE_PROPERTY,
			// Token: 0x040043E3 RID: 17379
			WBEMMOF_E_INVALID_NAMESPACE_SPECIFICATION,
			// Token: 0x040043E4 RID: 17380
			WBEMMOF_E_OUT_OF_RANGE,
			// Token: 0x040043E5 RID: 17381
			WBEMMOF_E_INVALID_FILE,
			// Token: 0x040043E6 RID: 17382
			WBEMMOF_E_ALIASES_IN_EMBEDDED,
			// Token: 0x040043E7 RID: 17383
			WBEMMOF_E_NULL_ARRAY_ELEM,
			// Token: 0x040043E8 RID: 17384
			WBEMMOF_E_DUPLICATE_QUALIFIER,
			// Token: 0x040043E9 RID: 17385
			WBEMMOF_E_EXPECTED_FLAVOR_TYPE,
			// Token: 0x040043EA RID: 17386
			WBEMMOF_E_INCOMPATIBLE_FLAVOR_TYPES,
			// Token: 0x040043EB RID: 17387
			WBEMMOF_E_MULTIPLE_ALIASES,
			// Token: 0x040043EC RID: 17388
			WBEMMOF_E_INCOMPATIBLE_FLAVOR_TYPES2,
			// Token: 0x040043ED RID: 17389
			WBEMMOF_E_NO_ARRAYS_RETURNED,
			// Token: 0x040043EE RID: 17390
			WBEMMOF_E_MUST_BE_IN_OR_OUT,
			// Token: 0x040043EF RID: 17391
			WBEMMOF_E_INVALID_FLAGS_SYNTAX,
			// Token: 0x040043F0 RID: 17392
			WBEMMOF_E_EXPECTED_BRACE_OR_BAD_TYPE,
			// Token: 0x040043F1 RID: 17393
			WBEMMOF_E_UNSUPPORTED_CIMV22_QUAL_VALUE,
			// Token: 0x040043F2 RID: 17394
			WBEMMOF_E_UNSUPPORTED_CIMV22_DATA_TYPE,
			// Token: 0x040043F3 RID: 17395
			WBEMMOF_E_INVALID_DELETEINSTANCE_SYNTAX,
			// Token: 0x040043F4 RID: 17396
			WBEMMOF_E_INVALID_QUALIFIER_SYNTAX,
			// Token: 0x040043F5 RID: 17397
			WBEMMOF_E_QUALIFIER_USED_OUTSIDE_SCOPE,
			// Token: 0x040043F6 RID: 17398
			WBEMMOF_E_ERROR_CREATING_TEMP_FILE,
			// Token: 0x040043F7 RID: 17399
			WBEMMOF_E_ERROR_INVALID_INCLUDE_FILE,
			// Token: 0x040043F8 RID: 17400
			WBEMMOF_E_INVALID_DELETECLASS_SYNTAX
		}

		// Token: 0x02000C1C RID: 3100
		public enum CIMTYPE
		{
			// Token: 0x040043FA RID: 17402
			CIM_ILLEGAL = 4095,
			// Token: 0x040043FB RID: 17403
			CIM_EMPTY = 0,
			// Token: 0x040043FC RID: 17404
			CIM_SINT8 = 16,
			// Token: 0x040043FD RID: 17405
			CIM_UINT8,
			// Token: 0x040043FE RID: 17406
			CIM_SINT16 = 2,
			// Token: 0x040043FF RID: 17407
			CIM_UINT16 = 18,
			// Token: 0x04004400 RID: 17408
			CIM_SINT32 = 3,
			// Token: 0x04004401 RID: 17409
			CIM_UINT32 = 19,
			// Token: 0x04004402 RID: 17410
			CIM_SINT64,
			// Token: 0x04004403 RID: 17411
			CIM_UINT64,
			// Token: 0x04004404 RID: 17412
			CIM_REAL32 = 4,
			// Token: 0x04004405 RID: 17413
			CIM_REAL64,
			// Token: 0x04004406 RID: 17414
			CIM_BOOLEAN = 11,
			// Token: 0x04004407 RID: 17415
			CIM_STRING = 8,
			// Token: 0x04004408 RID: 17416
			CIM_DATETIME = 101,
			// Token: 0x04004409 RID: 17417
			CIM_REFERENCE,
			// Token: 0x0400440A RID: 17418
			CIM_CHAR16,
			// Token: 0x0400440B RID: 17419
			CIM_OBJECT = 13,
			// Token: 0x0400440C RID: 17420
			CIM_FLAG_ARRAY = 8192
		}

		// Token: 0x02000C1D RID: 3101
		internal enum tag_WBEM_STATUS_TYPE
		{
			// Token: 0x0400440E RID: 17422
			WBEM_STATUS_COMPLETE,
			// Token: 0x0400440F RID: 17423
			WBEM_STATUS_REQUIREMENTS,
			// Token: 0x04004410 RID: 17424
			WBEM_STATUS_PROGRESS
		}

		// Token: 0x02000C1E RID: 3102
		internal enum tag_WBEM_EXTRA_RETURN_CODES
		{
			// Token: 0x04004412 RID: 17426
			WBEM_S_INITIALIZED,
			// Token: 0x04004413 RID: 17427
			WBEM_S_LIMITED_SERVICE = 274433,
			// Token: 0x04004414 RID: 17428
			WBEM_S_INDIRECTLY_UPDATED,
			// Token: 0x04004415 RID: 17429
			WBEM_S_SUBJECT_TO_SDS,
			// Token: 0x04004416 RID: 17430
			WBEM_E_RETRY_LATER = -2147209215,
			// Token: 0x04004417 RID: 17431
			WBEM_E_RESOURCE_CONTENTION
		}

		// Token: 0x02000C1F RID: 3103
		[Guid("4CFC7932-0F9D-4BEF-9C32-8EA2A6B56FCB")]
		[ComImport]
		internal class WbemDecoupledRegistrar
		{
			// Token: 0x060076C2 RID: 30402
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern WbemDecoupledRegistrar();
		}

		// Token: 0x02000C20 RID: 3104
		[Guid("1BE41572-91DD-11D1-AEB2-00C04FB68820")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IWbemProviderInit
		{
			// Token: 0x060076C3 RID: 30403
			[PreserveSig]
			int Initialize([MarshalAs(UnmanagedType.LPWStr)] [In] string wszUser, [In] int lFlags, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszNamespace, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszLocale, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemServices pNamespace, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemProviderInitSink pInitSink);
		}

		// Token: 0x02000C21 RID: 3105
		[Guid("1005CBCF-E64F-4646-BCD3-3A089D8A84B4")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IWbemDecoupledRegistrar
		{
			// Token: 0x060076C4 RID: 30404
			[PreserveSig]
			int Register([In] int flags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext context, [MarshalAs(UnmanagedType.LPWStr)] [In] string user, [MarshalAs(UnmanagedType.LPWStr)] [In] string locale, [MarshalAs(UnmanagedType.LPWStr)] [In] string scope, [MarshalAs(UnmanagedType.LPWStr)] [In] string registration, [MarshalAs(UnmanagedType.IUnknown)] [In] object unknown);

			// Token: 0x060076C5 RID: 30405
			[PreserveSig]
			int UnRegister();
		}

		// Token: 0x02000C22 RID: 3106
		[Guid("9556DC99-828C-11CF-A37E-00AA003240C7")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IWbemServices
		{
			// Token: 0x060076C6 RID: 30406
			[PreserveSig]
			int OpenNamespace([MarshalAs(UnmanagedType.BStr)] [In] string strNamespace, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] [In] [Out] ref WbemNative.IWbemServices ppWorkingNamespace, [In] IntPtr ppCallResult);

			// Token: 0x060076C7 RID: 30407
			[PreserveSig]
			int CancelAsyncCall([MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemObjectSink pSink);

			// Token: 0x060076C8 RID: 30408
			[PreserveSig]
			int QueryObjectSink([In] int lFlags, [MarshalAs(UnmanagedType.Interface)] out WbemNative.IWbemObjectSink ppResponseHandler);

			// Token: 0x060076C9 RID: 30409
			[PreserveSig]
			int GetObject([MarshalAs(UnmanagedType.BStr)] [In] string strObjectPath, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] [In] [Out] ref WbemNative.IWbemClassObject ppObject, [In] IntPtr ppCallResult);

			// Token: 0x060076CA RID: 30410
			[PreserveSig]
			int GetObjectAsync([MarshalAs(UnmanagedType.BStr)] [In] string strObjectPath, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemObjectSink pResponseHandler);

			// Token: 0x060076CB RID: 30411
			[PreserveSig]
			int PutClass([MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemClassObject pObject, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [In] IntPtr ppCallResult);

			// Token: 0x060076CC RID: 30412
			[PreserveSig]
			int PutClassAsync([MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemClassObject pObject, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemObjectSink pResponseHandler);

			// Token: 0x060076CD RID: 30413
			[PreserveSig]
			int DeleteClass([MarshalAs(UnmanagedType.BStr)] [In] string strClass, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [In] IntPtr ppCallResult);

			// Token: 0x060076CE RID: 30414
			[PreserveSig]
			int DeleteClassAsync([MarshalAs(UnmanagedType.BStr)] [In] string strClass, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemObjectSink pResponseHandler);

			// Token: 0x060076CF RID: 30415
			[PreserveSig]
			int CreateClassEnum([MarshalAs(UnmanagedType.BStr)] [In] string strSuperclass, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] out WbemNative.IEnumWbemClassObject ppEnum);

			// Token: 0x060076D0 RID: 30416
			[PreserveSig]
			int CreateClassEnumAsync([MarshalAs(UnmanagedType.BStr)] [In] string strSuperclass, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemObjectSink pResponseHandler);

			// Token: 0x060076D1 RID: 30417
			[PreserveSig]
			int PutInstance([MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemClassObject pInst, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [In] IntPtr ppCallResult);

			// Token: 0x060076D2 RID: 30418
			[PreserveSig]
			int PutInstanceAsync([MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemClassObject pInst, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemObjectSink pResponseHandler);

			// Token: 0x060076D3 RID: 30419
			[PreserveSig]
			int DeleteInstance([MarshalAs(UnmanagedType.BStr)] [In] string strObjectPath, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [In] IntPtr ppCallResult);

			// Token: 0x060076D4 RID: 30420
			[PreserveSig]
			int DeleteInstanceAsync([MarshalAs(UnmanagedType.BStr)] [In] string strObjectPath, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemObjectSink pResponseHandler);

			// Token: 0x060076D5 RID: 30421
			[PreserveSig]
			int CreateInstanceEnum([MarshalAs(UnmanagedType.BStr)] [In] string strFilter, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] out WbemNative.IEnumWbemClassObject ppEnum);

			// Token: 0x060076D6 RID: 30422
			[PreserveSig]
			int CreateInstanceEnumAsync([MarshalAs(UnmanagedType.BStr)] [In] string strFilter, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemObjectSink pResponseHandler);

			// Token: 0x060076D7 RID: 30423
			[PreserveSig]
			int ExecQuery([MarshalAs(UnmanagedType.BStr)] [In] string strQueryLanguage, [MarshalAs(UnmanagedType.BStr)] [In] string strQuery, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] out WbemNative.IEnumWbemClassObject ppEnum);

			// Token: 0x060076D8 RID: 30424
			[PreserveSig]
			int ExecQueryAsync([MarshalAs(UnmanagedType.BStr)] [In] string strQueryLanguage, [MarshalAs(UnmanagedType.BStr)] [In] string strQuery, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemObjectSink pResponseHandler);

			// Token: 0x060076D9 RID: 30425
			[PreserveSig]
			int ExecNotificationQuery([MarshalAs(UnmanagedType.BStr)] [In] string strQueryLanguage, [MarshalAs(UnmanagedType.BStr)] [In] string strQuery, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] out WbemNative.IEnumWbemClassObject ppEnum);

			// Token: 0x060076DA RID: 30426
			[PreserveSig]
			int ExecNotificationQueryAsync([MarshalAs(UnmanagedType.BStr)] [In] string strQueryLanguage, [MarshalAs(UnmanagedType.BStr)] [In] string strQuery, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemObjectSink pResponseHandler);

			// Token: 0x060076DB RID: 30427
			[PreserveSig]
			int ExecMethod([MarshalAs(UnmanagedType.BStr)] [In] string strObjectPath, [MarshalAs(UnmanagedType.BStr)] [In] string strMethodName, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemClassObject pInParams, [MarshalAs(UnmanagedType.Interface)] [In] [Out] ref WbemNative.IWbemClassObject ppOutParams, [In] IntPtr ppCallResult);

			// Token: 0x060076DC RID: 30428
			[PreserveSig]
			int ExecMethodAsync([MarshalAs(UnmanagedType.BStr)] [In] string strObjectPath, [MarshalAs(UnmanagedType.BStr)] [In] string strMethodName, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemContext pCtx, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemClassObject pInParams, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemObjectSink pResponseHandler);
		}

		// Token: 0x02000C23 RID: 3107
		[Guid("DC12A681-737F-11CF-884D-00AA004B2E24")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IWbemClassObject
		{
			// Token: 0x060076DD RID: 30429
			[PreserveSig]
			int GetQualifierSet([MarshalAs(UnmanagedType.Interface)] out WbemNative.IWbemQualifierSet ppQualSet);

			// Token: 0x060076DE RID: 30430
			[PreserveSig]
			int Get([MarshalAs(UnmanagedType.LPWStr)] [In] string wszName, [In] int lFlags, [In] [Out] ref object pVal, [In] [Out] ref int pType, [In] [Out] ref int plFlavor);

			// Token: 0x060076DF RID: 30431
			[PreserveSig]
			int Put([MarshalAs(UnmanagedType.LPWStr)] [In] string wszName, [In] int lFlags, [In] ref object pVal, [In] int Type);

			// Token: 0x060076E0 RID: 30432
			[PreserveSig]
			int Delete([MarshalAs(UnmanagedType.LPWStr)] [In] string wszName);

			// Token: 0x060076E1 RID: 30433
			[PreserveSig]
			int GetNames([MarshalAs(UnmanagedType.LPWStr)] [In] string wszQualifierName, [In] int lFlags, [In] ref object pQualifierVal, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] pNames);

			// Token: 0x060076E2 RID: 30434
			[PreserveSig]
			int BeginEnumeration([In] int lEnumFlags);

			// Token: 0x060076E3 RID: 30435
			[PreserveSig]
			int Next([In] int lFlags, [MarshalAs(UnmanagedType.BStr)] [In] [Out] ref string strName, [In] [Out] ref object pVal, [In] [Out] ref int pType, [In] [Out] ref int plFlavor);

			// Token: 0x060076E4 RID: 30436
			[PreserveSig]
			int EndEnumeration();

			// Token: 0x060076E5 RID: 30437
			[PreserveSig]
			int GetPropertyQualifierSet([MarshalAs(UnmanagedType.LPWStr)] [In] string wszProperty, [MarshalAs(UnmanagedType.Interface)] out WbemNative.IWbemQualifierSet ppQualSet);

			// Token: 0x060076E6 RID: 30438
			[PreserveSig]
			int Clone([MarshalAs(UnmanagedType.Interface)] out WbemNative.IWbemClassObject ppCopy);

			// Token: 0x060076E7 RID: 30439
			[PreserveSig]
			int GetObjectText([In] int lFlags, [MarshalAs(UnmanagedType.BStr)] out string pstrObjectText);

			// Token: 0x060076E8 RID: 30440
			[PreserveSig]
			int SpawnDerivedClass([In] int lFlags, [MarshalAs(UnmanagedType.Interface)] out WbemNative.IWbemClassObject ppNewClass);

			// Token: 0x060076E9 RID: 30441
			[PreserveSig]
			int SpawnInstance([In] int lFlags, [MarshalAs(UnmanagedType.Interface)] out WbemNative.IWbemClassObject ppNewInstance);

			// Token: 0x060076EA RID: 30442
			[PreserveSig]
			int CompareTo([In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemClassObject pCompareTo);

			// Token: 0x060076EB RID: 30443
			[PreserveSig]
			int GetPropertyOrigin([MarshalAs(UnmanagedType.LPWStr)] [In] string wszName, [MarshalAs(UnmanagedType.BStr)] out string pstrClassName);

			// Token: 0x060076EC RID: 30444
			[PreserveSig]
			int InheritsFrom([MarshalAs(UnmanagedType.LPWStr)] [In] string strAncestor);

			// Token: 0x060076ED RID: 30445
			[PreserveSig]
			int GetMethod([MarshalAs(UnmanagedType.LPWStr)] [In] string wszName, [In] int lFlags, [In] IntPtr ppInSignature, [MarshalAs(UnmanagedType.Interface)] out WbemNative.IWbemClassObject ppOutSignature);

			// Token: 0x060076EE RID: 30446
			[PreserveSig]
			int PutMethod([MarshalAs(UnmanagedType.LPWStr)] [In] string wszName, [In] int lFlags, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemClassObject pInSignature, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemClassObject pOutSignature);

			// Token: 0x060076EF RID: 30447
			[PreserveSig]
			int DeleteMethod([MarshalAs(UnmanagedType.LPWStr)] [In] string wszName);

			// Token: 0x060076F0 RID: 30448
			[PreserveSig]
			int BeginMethodEnumeration([In] int lEnumFlags);

			// Token: 0x060076F1 RID: 30449
			[PreserveSig]
			int NextMethod([In] int lFlags, [MarshalAs(UnmanagedType.BStr)] [In] [Out] ref string pstrName, [MarshalAs(UnmanagedType.Interface)] [In] [Out] ref WbemNative.IWbemClassObject ppInSignature, [MarshalAs(UnmanagedType.Interface)] [In] [Out] ref WbemNative.IWbemClassObject ppOutSignature);

			// Token: 0x060076F2 RID: 30450
			[PreserveSig]
			int EndMethodEnumeration();

			// Token: 0x060076F3 RID: 30451
			[PreserveSig]
			int GetMethodQualifierSet([MarshalAs(UnmanagedType.LPWStr)] [In] string wszMethod, [MarshalAs(UnmanagedType.Interface)] out WbemNative.IWbemQualifierSet ppQualSet);

			// Token: 0x060076F4 RID: 30452
			[PreserveSig]
			int GetMethodOrigin([MarshalAs(UnmanagedType.LPWStr)] [In] string wszMethodName, [MarshalAs(UnmanagedType.BStr)] out string pstrClassName);
		}

		// Token: 0x02000C24 RID: 3108
		[Guid("44ACA674-E8FC-11D0-A07C-00C04FB68820")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IWbemContext
		{
			// Token: 0x060076F5 RID: 30453
			[PreserveSig]
			int Clone([MarshalAs(UnmanagedType.Interface)] out WbemNative.IWbemContext ppNewCopy);

			// Token: 0x060076F6 RID: 30454
			[PreserveSig]
			int GetNames([In] int lFlags, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] pNames);

			// Token: 0x060076F7 RID: 30455
			[PreserveSig]
			int BeginEnumeration([In] int lFlags);

			// Token: 0x060076F8 RID: 30456
			[PreserveSig]
			int Next([In] int lFlags, [MarshalAs(UnmanagedType.BStr)] out string pstrName, out object pValue);

			// Token: 0x060076F9 RID: 30457
			[PreserveSig]
			int EndEnumeration();

			// Token: 0x060076FA RID: 30458
			[PreserveSig]
			int SetValue([MarshalAs(UnmanagedType.LPWStr)] [In] string wszName, [In] int lFlags, [In] ref object pValue);

			// Token: 0x060076FB RID: 30459
			[PreserveSig]
			int GetValue([MarshalAs(UnmanagedType.LPWStr)] [In] string wszName, [In] int lFlags, out object pValue);

			// Token: 0x060076FC RID: 30460
			[PreserveSig]
			int DeleteValue([MarshalAs(UnmanagedType.LPWStr)] [In] string wszName, [In] int lFlags);

			// Token: 0x060076FD RID: 30461
			[PreserveSig]
			int DeleteAll();
		}

		// Token: 0x02000C25 RID: 3109
		[Guid("1BE41571-91DD-11D1-AEB2-00C04FB68820")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IWbemProviderInitSink
		{
			// Token: 0x060076FE RID: 30462
			[PreserveSig]
			int SetStatus([In] int lStatus, [In] int lFlags);
		}

		// Token: 0x02000C26 RID: 3110
		[Guid("7C857801-7381-11CF-884D-00AA004B2E24")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IWbemObjectSink
		{
			// Token: 0x060076FF RID: 30463
			[PreserveSig]
			int Indicate([In] int lObjectCount, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [In] WbemNative.IWbemClassObject[] apObjArray);

			// Token: 0x06007700 RID: 30464
			[PreserveSig]
			int SetStatus([In] int lFlags, [MarshalAs(UnmanagedType.Error)] [In] int hResult, [MarshalAs(UnmanagedType.BStr)] [In] string strParam, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemClassObject pObjParam);
		}

		// Token: 0x02000C27 RID: 3111
		[Guid("027947E1-D731-11CE-A357-000000000001")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IEnumWbemClassObject
		{
			// Token: 0x06007701 RID: 30465
			[PreserveSig]
			int Reset();

			// Token: 0x06007702 RID: 30466
			[PreserveSig]
			int Next([In] int lTimeout, [In] uint uCount, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [In] [Out] WbemNative.IWbemClassObject[] apObjects, out uint puReturned);

			// Token: 0x06007703 RID: 30467
			[PreserveSig]
			int NextAsync([In] uint uCount, [MarshalAs(UnmanagedType.Interface)] [In] WbemNative.IWbemObjectSink pSink);

			// Token: 0x06007704 RID: 30468
			[PreserveSig]
			int Clone([MarshalAs(UnmanagedType.Interface)] out WbemNative.IEnumWbemClassObject ppEnum);

			// Token: 0x06007705 RID: 30469
			[PreserveSig]
			int Skip([In] int lTimeout, [In] uint nCount);
		}

		// Token: 0x02000C28 RID: 3112
		[Guid("DC12A680-737F-11CF-884D-00AA004B2E24")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IWbemQualifierSet
		{
			// Token: 0x06007706 RID: 30470
			[PreserveSig]
			int Get([MarshalAs(UnmanagedType.LPWStr)] [In] string wszName, [In] int lFlags, [In] [Out] ref object pVal, [In] [Out] ref int plFlavor);

			// Token: 0x06007707 RID: 30471
			[PreserveSig]
			int Put([MarshalAs(UnmanagedType.LPWStr)] [In] string wszName, [In] ref object pVal, [In] int lFlavor);

			// Token: 0x06007708 RID: 30472
			[PreserveSig]
			int Delete([MarshalAs(UnmanagedType.LPWStr)] [In] string wszName);

			// Token: 0x06007709 RID: 30473
			[PreserveSig]
			int GetNames([In] int lFlags, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] pNames);

			// Token: 0x0600770A RID: 30474
			[PreserveSig]
			int BeginEnumeration([In] int lFlags);

			// Token: 0x0600770B RID: 30475
			[PreserveSig]
			int Next([In] int lFlags, [MarshalAs(UnmanagedType.BStr)] [In] [Out] ref string pstrName, [In] [Out] ref object pVal, [In] [Out] ref int plFlavor);

			// Token: 0x0600770C RID: 30476
			[PreserveSig]
			int EndEnumeration();
		}
	}
}
