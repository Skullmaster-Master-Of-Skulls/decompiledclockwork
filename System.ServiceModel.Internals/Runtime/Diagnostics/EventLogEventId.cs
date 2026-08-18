using System;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000048 RID: 72
	internal enum EventLogEventId : uint
	{
		// Token: 0x04000141 RID: 321
		FailedToSetupTracing = 3221291108U,
		// Token: 0x04000142 RID: 322
		FailedToInitializeTraceSource,
		// Token: 0x04000143 RID: 323
		FailFast,
		// Token: 0x04000144 RID: 324
		FailFastException,
		// Token: 0x04000145 RID: 325
		FailedToTraceEvent,
		// Token: 0x04000146 RID: 326
		FailedToTraceEventWithException,
		// Token: 0x04000147 RID: 327
		InvariantAssertionFailed,
		// Token: 0x04000148 RID: 328
		PiiLoggingOn,
		// Token: 0x04000149 RID: 329
		PiiLoggingNotAllowed,
		// Token: 0x0400014A RID: 330
		WebHostUnhandledException = 3221356545U,
		// Token: 0x0400014B RID: 331
		WebHostHttpError,
		// Token: 0x0400014C RID: 332
		WebHostFailedToProcessRequest,
		// Token: 0x0400014D RID: 333
		WebHostFailedToListen,
		// Token: 0x0400014E RID: 334
		FailedToLogMessage,
		// Token: 0x0400014F RID: 335
		RemovedBadFilter,
		// Token: 0x04000150 RID: 336
		FailedToCreateMessageLoggingTraceSource,
		// Token: 0x04000151 RID: 337
		MessageLoggingOn,
		// Token: 0x04000152 RID: 338
		MessageLoggingOff,
		// Token: 0x04000153 RID: 339
		FailedToLoadPerformanceCounter,
		// Token: 0x04000154 RID: 340
		FailedToRemovePerformanceCounter,
		// Token: 0x04000155 RID: 341
		WmiGetObjectFailed,
		// Token: 0x04000156 RID: 342
		WmiPutInstanceFailed,
		// Token: 0x04000157 RID: 343
		WmiDeleteInstanceFailed,
		// Token: 0x04000158 RID: 344
		WmiCreateInstanceFailed,
		// Token: 0x04000159 RID: 345
		WmiExecQueryFailed,
		// Token: 0x0400015A RID: 346
		WmiExecMethodFailed,
		// Token: 0x0400015B RID: 347
		WmiRegistrationFailed,
		// Token: 0x0400015C RID: 348
		WmiUnregistrationFailed,
		// Token: 0x0400015D RID: 349
		WmiAdminTypeMismatch,
		// Token: 0x0400015E RID: 350
		WmiPropertyMissing,
		// Token: 0x0400015F RID: 351
		ComPlusServiceHostStartingServiceError,
		// Token: 0x04000160 RID: 352
		ComPlusDllHostInitializerStartingError,
		// Token: 0x04000161 RID: 353
		ComPlusTLBImportError,
		// Token: 0x04000162 RID: 354
		ComPlusInvokingMethodFailed,
		// Token: 0x04000163 RID: 355
		ComPlusInstanceCreationError,
		// Token: 0x04000164 RID: 356
		ComPlusInvokingMethodFailedMismatchedTransactions,
		// Token: 0x04000165 RID: 357
		WebHostNotLoggingInsufficientMemoryExceptionsOnActivationForNextTimeInterval = 2147614748U,
		// Token: 0x04000166 RID: 358
		UnhandledStateMachineExceptionRecordDescription = 3221422081U,
		// Token: 0x04000167 RID: 359
		FatalUnexpectedStateMachineEvent,
		// Token: 0x04000168 RID: 360
		ParticipantRecoveryLogEntryCorrupt,
		// Token: 0x04000169 RID: 361
		CoordinatorRecoveryLogEntryCorrupt,
		// Token: 0x0400016A RID: 362
		CoordinatorRecoveryLogEntryCreationFailure,
		// Token: 0x0400016B RID: 363
		ParticipantRecoveryLogEntryCreationFailure,
		// Token: 0x0400016C RID: 364
		ProtocolInitializationFailure,
		// Token: 0x0400016D RID: 365
		ProtocolStartFailure,
		// Token: 0x0400016E RID: 366
		ProtocolRecoveryBeginningFailure,
		// Token: 0x0400016F RID: 367
		ProtocolRecoveryCompleteFailure,
		// Token: 0x04000170 RID: 368
		TransactionBridgeRecoveryFailure,
		// Token: 0x04000171 RID: 369
		ProtocolStopFailure,
		// Token: 0x04000172 RID: 370
		NonFatalUnexpectedStateMachineEvent,
		// Token: 0x04000173 RID: 371
		PerformanceCounterInitializationFailure,
		// Token: 0x04000174 RID: 372
		ProtocolRecoveryComplete,
		// Token: 0x04000175 RID: 373
		ProtocolStopped,
		// Token: 0x04000176 RID: 374
		ThumbPrintNotFound,
		// Token: 0x04000177 RID: 375
		ThumbPrintNotValidated,
		// Token: 0x04000178 RID: 376
		SslNoPrivateKey,
		// Token: 0x04000179 RID: 377
		SslNoAccessiblePrivateKey,
		// Token: 0x0400017A RID: 378
		MissingNecessaryKeyUsage,
		// Token: 0x0400017B RID: 379
		MissingNecessaryEnhancedKeyUsage,
		// Token: 0x0400017C RID: 380
		StartErrorPublish = 3221487617U,
		// Token: 0x0400017D RID: 381
		BindingError,
		// Token: 0x0400017E RID: 382
		LAFailedToListenForApp,
		// Token: 0x0400017F RID: 383
		UnknownListenerAdapterError,
		// Token: 0x04000180 RID: 384
		WasDisconnected,
		// Token: 0x04000181 RID: 385
		WasConnectionTimedout,
		// Token: 0x04000182 RID: 386
		ServiceStartFailed,
		// Token: 0x04000183 RID: 387
		MessageQueueDuplicatedSocketLeak,
		// Token: 0x04000184 RID: 388
		MessageQueueDuplicatedPipeLeak,
		// Token: 0x04000185 RID: 389
		SharingUnhandledException,
		// Token: 0x04000186 RID: 390
		ServiceAuthorizationSuccess = 1074135041U,
		// Token: 0x04000187 RID: 391
		ServiceAuthorizationFailure = 3221618690U,
		// Token: 0x04000188 RID: 392
		MessageAuthenticationSuccess = 1074135043U,
		// Token: 0x04000189 RID: 393
		MessageAuthenticationFailure = 3221618692U,
		// Token: 0x0400018A RID: 394
		SecurityNegotiationSuccess = 1074135045U,
		// Token: 0x0400018B RID: 395
		SecurityNegotiationFailure = 3221618694U,
		// Token: 0x0400018C RID: 396
		TransportAuthenticationSuccess = 1074135047U,
		// Token: 0x0400018D RID: 397
		TransportAuthenticationFailure = 3221618696U,
		// Token: 0x0400018E RID: 398
		ImpersonationSuccess = 1074135049U,
		// Token: 0x0400018F RID: 399
		ImpersonationFailure = 3221618698U
	}
}
