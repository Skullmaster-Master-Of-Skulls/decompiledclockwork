using System;
using System.Globalization;
using System.Resources;

namespace System.Runtime
{
	// Token: 0x02000038 RID: 56
	internal class InternalSR
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x000023D6 File Offset: 0x000005D6
		private InternalSR()
		{
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00008050 File Offset: 0x00006250
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (InternalSR.resourceManager == null)
				{
					ResourceManager resourceManager = new ResourceManager("System.Runtime.InternalSR", typeof(InternalSR).Assembly);
					InternalSR.resourceManager = resourceManager;
				}
				return InternalSR.resourceManager;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00008089 File Offset: 0x00006289
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00008090 File Offset: 0x00006290
		internal static CultureInfo Culture
		{
			get
			{
				return InternalSR.resourceCulture;
			}
			set
			{
				InternalSR.resourceCulture = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00008098 File Offset: 0x00006298
		internal static string ActionItemIsAlreadyScheduled
		{
			get
			{
				return InternalSR.ResourceManager.GetString("ActionItemIsAlreadyScheduled", InternalSR.Culture);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000080AE File Offset: 0x000062AE
		internal static string AsyncCallbackThrewException
		{
			get
			{
				return InternalSR.ResourceManager.GetString("AsyncCallbackThrewException", InternalSR.Culture);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000080C4 File Offset: 0x000062C4
		internal static string AsyncResultAlreadyEnded
		{
			get
			{
				return InternalSR.ResourceManager.GetString("AsyncResultAlreadyEnded", InternalSR.Culture);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000080DA File Offset: 0x000062DA
		internal static string DictionaryIsReadOnly
		{
			get
			{
				return InternalSR.ResourceManager.GetString("DictionaryIsReadOnly", InternalSR.Culture);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000080F0 File Offset: 0x000062F0
		internal static string InvalidAsyncResult
		{
			get
			{
				return InternalSR.ResourceManager.GetString("InvalidAsyncResult", InternalSR.Culture);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00008106 File Offset: 0x00006306
		internal static string InvalidSemaphoreExit
		{
			get
			{
				return InternalSR.ResourceManager.GetString("InvalidSemaphoreExit", InternalSR.Culture);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000811C File Offset: 0x0000631C
		internal static string MustCancelOldTimer
		{
			get
			{
				return InternalSR.ResourceManager.GetString("MustCancelOldTimer", InternalSR.Culture);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00008132 File Offset: 0x00006332
		internal static string BufferIsNotRightSizeForBufferManager
		{
			get
			{
				return InternalSR.ResourceManager.GetString("BufferIsNotRightSizeForBufferManager", InternalSR.Culture);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00008148 File Offset: 0x00006348
		internal static string ReadNotSupported
		{
			get
			{
				return InternalSR.ResourceManager.GetString("ReadNotSupported", InternalSR.Culture);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000815E File Offset: 0x0000635E
		internal static string SeekNotSupported
		{
			get
			{
				return InternalSR.ResourceManager.GetString("SeekNotSupported", InternalSR.Culture);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00008174 File Offset: 0x00006374
		internal static string ThreadNeutralSemaphoreAborted
		{
			get
			{
				return InternalSR.ResourceManager.GetString("ThreadNeutralSemaphoreAborted", InternalSR.Culture);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000818A File Offset: 0x0000638A
		internal static string ValueMustBeNonNegative
		{
			get
			{
				return InternalSR.ResourceManager.GetString("ValueMustBeNonNegative", InternalSR.Culture);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000081A0 File Offset: 0x000063A0
		internal static string BadCopyToArray
		{
			get
			{
				return InternalSR.ResourceManager.GetString("BadCopyToArray", InternalSR.Culture);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000081B6 File Offset: 0x000063B6
		internal static string KeyNotFoundInDictionary
		{
			get
			{
				return InternalSR.ResourceManager.GetString("KeyNotFoundInDictionary", InternalSR.Culture);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000081CC File Offset: 0x000063CC
		internal static string InvalidAsyncResultImplementationGeneric
		{
			get
			{
				return InternalSR.ResourceManager.GetString("InvalidAsyncResultImplementationGeneric", InternalSR.Culture);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000081E2 File Offset: 0x000063E2
		internal static string InvalidNullAsyncResult
		{
			get
			{
				return InternalSR.ResourceManager.GetString("InvalidNullAsyncResult", InternalSR.Culture);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x000081F8 File Offset: 0x000063F8
		internal static string NullKeyAlreadyPresent
		{
			get
			{
				return InternalSR.ResourceManager.GetString("NullKeyAlreadyPresent", InternalSR.Culture);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000820E File Offset: 0x0000640E
		internal static string KeyCollectionUpdatesNotAllowed
		{
			get
			{
				return InternalSR.ResourceManager.GetString("KeyCollectionUpdatesNotAllowed", InternalSR.Culture);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00008224 File Offset: 0x00006424
		internal static string ValueCollectionUpdatesNotAllowed
		{
			get
			{
				return InternalSR.ResourceManager.GetString("ValueCollectionUpdatesNotAllowed", InternalSR.Culture);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000823A File Offset: 0x0000643A
		internal static string SFxTaskNotStarted
		{
			get
			{
				return InternalSR.ResourceManager.GetString("SFxTaskNotStarted", InternalSR.Culture);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008250 File Offset: 0x00006450
		internal static string ArgumentNullOrEmpty(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("ArgumentNullOrEmpty", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000827A File Offset: 0x0000647A
		internal static string FailFastMessage(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("FailFastMessage", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000082A4 File Offset: 0x000064A4
		internal static string IncompatibleArgumentType(object param0, object param1)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("IncompatibleArgumentType", InternalSR.Culture), new object[]
			{
				param0,
				param1
			});
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000082D2 File Offset: 0x000064D2
		internal static string LockTimeoutExceptionMessage(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("LockTimeoutExceptionMessage", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000082FC File Offset: 0x000064FC
		internal static string BufferAllocationFailed(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("BufferAllocationFailed", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008326 File Offset: 0x00006526
		internal static string BufferedOutputStreamQuotaExceeded(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("BufferedOutputStreamQuotaExceeded", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00008350 File Offset: 0x00006550
		internal static string ShipAssertExceptionMessage(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("ShipAssertExceptionMessage", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000837A File Offset: 0x0000657A
		internal static string TimeoutInputQueueDequeue(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("TimeoutInputQueueDequeue", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000083A4 File Offset: 0x000065A4
		internal static string TimeoutMustBeNonNegative(object param0, object param1)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("TimeoutMustBeNonNegative", InternalSR.Culture), new object[]
			{
				param0,
				param1
			});
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000083D2 File Offset: 0x000065D2
		internal static string TimeoutMustBePositive(object param0, object param1)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("TimeoutMustBePositive", InternalSR.Culture), new object[]
			{
				param0,
				param1
			});
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00008400 File Offset: 0x00006600
		internal static string TimeoutOnOperation(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("TimeoutOnOperation", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000842A File Offset: 0x0000662A
		internal static string CannotConvertObject(object param0, object param1)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("CannotConvertObject", InternalSR.Culture), new object[]
			{
				param0,
				param1
			});
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008458 File Offset: 0x00006658
		internal static string EtwAPIMaxStringCountExceeded(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("EtwAPIMaxStringCountExceeded", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008482 File Offset: 0x00006682
		internal static string EtwMaxNumberArgumentsExceeded(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("EtwMaxNumberArgumentsExceeded", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x060001EA RID: 490 RVA: 0x000084AC File Offset: 0x000066AC
		internal static string EtwRegistrationFailed(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("EtwRegistrationFailed", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000084D6 File Offset: 0x000066D6
		internal static string InvalidAsyncResultImplementation(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("InvalidAsyncResultImplementation", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008500 File Offset: 0x00006700
		internal static string AsyncResultCompletedTwice(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("AsyncResultCompletedTwice", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000852A File Offset: 0x0000672A
		internal static string AsyncEventArgsCompletedTwice(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("AsyncEventArgsCompletedTwice", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008554 File Offset: 0x00006754
		internal static string AsyncEventArgsCompletionPending(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("AsyncEventArgsCompletionPending", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000857E File Offset: 0x0000677E
		internal static string TaskTimedOutError(object param0)
		{
			return string.Format(InternalSR.Culture, InternalSR.ResourceManager.GetString("TaskTimedOutError", InternalSR.Culture), new object[]
			{
				param0
			});
		}

		// Token: 0x040000E2 RID: 226
		private static ResourceManager resourceManager;

		// Token: 0x040000E3 RID: 227
		private static CultureInfo resourceCulture;
	}
}
