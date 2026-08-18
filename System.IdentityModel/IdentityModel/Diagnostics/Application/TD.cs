using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.Security;

namespace System.IdentityModel.Diagnostics.Application
{
	// Token: 0x020001EC RID: 492
	internal class TD
	{
		// Token: 0x0600105C RID: 4188 RVA: 0x00004469 File Offset: 0x00002669
		private TD()
		{
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x000465F0 File Offset: 0x000447F0
		private static ResourceManager ResourceManager
		{
			get
			{
				if (TD.resourceManager == null)
				{
					TD.resourceManager = new ResourceManager("System.IdentityModel.Diagnostics.Application.TD", typeof(TD).Assembly);
				}
				return TD.resourceManager;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0004661C File Offset: 0x0004481C
		// (set) Token: 0x0600105F RID: 4191 RVA: 0x00046623 File Offset: 0x00044823
		internal static CultureInfo Culture
		{
			get
			{
				return TD.resourceCulture;
			}
			set
			{
				TD.resourceCulture = value;
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0004662B File Offset: 0x0004482B
		internal static bool GetIssuerNameFailureIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(0);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0004663C File Offset: 0x0004483C
		internal static void GetIssuerNameFailure(EventTraceActivity eventTraceActivity, string tokenID)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(0))
			{
				TD.WriteEtwEvent(0, eventTraceActivity, tokenID, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00046677 File Offset: 0x00044877
		internal static bool GetIssuerNameSuccessIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(1);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00046688 File Offset: 0x00044888
		internal static void GetIssuerNameSuccess(EventTraceActivity eventTraceActivity, string issuerName, string tokenID)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(1))
			{
				TD.WriteEtwEvent(1, eventTraceActivity, issuerName, tokenID, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x000466C4 File Offset: 0x000448C4
		internal static bool TokenValidationFailureIsEnabled()
		{
			return FxTrace.ShouldTraceError && TD.IsEtwEventEnabled(2);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x000466D8 File Offset: 0x000448D8
		internal static void TokenValidationFailure(EventTraceActivity eventTraceActivity, string tokenType, string tokenID, string errorMessage)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(2))
			{
				TD.WriteEtwEvent(2, eventTraceActivity, tokenType, tokenID, errorMessage, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00046715 File Offset: 0x00044915
		internal static bool TokenValidationStartedIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(3);
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00046728 File Offset: 0x00044928
		internal static void TokenValidationStarted(EventTraceActivity eventTraceActivity, string tokenType, string tokenID)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(3))
			{
				TD.WriteEtwEvent(3, eventTraceActivity, tokenType, tokenID, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00046764 File Offset: 0x00044964
		internal static bool TokenValidationSuccessIsEnabled()
		{
			return FxTrace.ShouldTraceVerbose && TD.IsEtwEventEnabled(4);
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00046778 File Offset: 0x00044978
		internal static void TokenValidationSuccess(EventTraceActivity eventTraceActivity, string tokenType, string tokenID)
		{
			TracePayload serializedPayload = FxTrace.Trace.GetSerializedPayload(null, null, null, true);
			if (TD.IsEtwEventEnabled(4))
			{
				TD.WriteEtwEvent(4, eventTraceActivity, tokenType, tokenID, serializedPayload.HostReference, serializedPayload.AppDomainFriendlyName);
			}
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x000467B4 File Offset: 0x000449B4
		[SecuritySafeCritical]
		private static void CreateEventDescriptors()
		{
			EventDescriptor[] array = new EventDescriptor[]
			{
				new EventDescriptor(5406, 0, 19, 2, 0, 2613, 1152921504606846992L),
				new EventDescriptor(5405, 0, 19, 5, 0, 2613, 1152921504606846992L),
				new EventDescriptor(5404, 0, 19, 2, 0, 2612, 1152921504606846992L),
				new EventDescriptor(5402, 0, 19, 5, 0, 2612, 1152921504606846992L),
				new EventDescriptor(5403, 0, 19, 5, 0, 2612, 1152921504606846992L)
			};
			FxTrace.UpdateEventDefinitions(array, new List<ushort>(5)
			{
				5402,
				5403,
				5404,
				5405,
				5406
			}.ToArray());
			TD.eventDescriptors = array;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x000468CC File Offset: 0x00044ACC
		private static void EnsureEventDescriptors()
		{
			if (TD.eventDescriptorsCreated)
			{
				return;
			}
			lock (TD.syncLock)
			{
				if (!TD.eventDescriptorsCreated)
				{
					TD.CreateEventDescriptors();
					TD.eventDescriptorsCreated = true;
				}
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00046924 File Offset: 0x00044B24
		private static bool IsEtwEventEnabled(int eventIndex)
		{
			if (FxTrace.Trace.IsEtwProviderEnabled)
			{
				TD.EnsureEventDescriptors();
				return FxTrace.IsEventEnabled(eventIndex);
			}
			return false;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0004693F File Offset: 0x00044B3F
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3);
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x00046965 File Offset: 0x00044B65
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3, string eventParam4)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3, eventParam4);
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0004698D File Offset: 0x00044B8D
		[SecuritySafeCritical]
		private static bool WriteEtwEvent(int eventIndex, EventTraceActivity eventParam0, string eventParam1, string eventParam2, string eventParam3, string eventParam4, string eventParam5)
		{
			TD.EnsureEventDescriptors();
			return FxTrace.Trace.EtwProvider.WriteEvent(ref TD.eventDescriptors[eventIndex], eventParam0, eventParam1, eventParam2, eventParam3, eventParam4, eventParam5);
		}

		// Token: 0x04000E48 RID: 3656
		private static ResourceManager resourceManager;

		// Token: 0x04000E49 RID: 3657
		private static CultureInfo resourceCulture;

		// Token: 0x04000E4A RID: 3658
		[SecurityCritical]
		private static EventDescriptor[] eventDescriptors;

		// Token: 0x04000E4B RID: 3659
		private static object syncLock = new object();

		// Token: 0x04000E4C RID: 3660
		private static volatile bool eventDescriptorsCreated;
	}
}
