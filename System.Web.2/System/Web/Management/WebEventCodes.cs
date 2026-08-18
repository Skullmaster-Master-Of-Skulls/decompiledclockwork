using System;

namespace System.Web.Management
{
	// Token: 0x02000184 RID: 388
	public sealed class WebEventCodes
	{
		// Token: 0x06001514 RID: 5396 RVA: 0x000030B5 File Offset: 0x000012B5
		private WebEventCodes()
		{
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0004070D File Offset: 0x0003E90D
		static WebEventCodes()
		{
			WebEventCodes.InitEventArrayDimensions();
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x00040720 File Offset: 0x0003E920
		internal static string MessageFromEventCode(int eventCode, int eventDetailCode)
		{
			string text = null;
			if (eventDetailCode != 0)
			{
				switch (eventDetailCode)
				{
				case 50001:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownUnknown");
					break;
				case 50002:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownHostingEnvironment");
					break;
				case 50003:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownChangeInGlobalAsax");
					break;
				case 50004:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownConfigurationChange");
					break;
				case 50005:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownUnloadAppDomainCalled");
					break;
				case 50006:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownChangeInSecurityPolicyFile");
					break;
				case 50007:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownBinDirChangeOrDirectoryRename");
					break;
				case 50008:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownBrowsersDirChangeOrDirectoryRename");
					break;
				case 50009:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownCodeDirChangeOrDirectoryRename");
					break;
				case 50010:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownResourcesDirChangeOrDirectoryRename");
					break;
				case 50011:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownIdleTimeout");
					break;
				case 50012:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownPhysicalApplicationPathChanged");
					break;
				case 50013:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownHttpRuntimeClose");
					break;
				case 50014:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownInitializationError");
					break;
				case 50015:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownMaxRecompilationsReached");
					break;
				case 50016:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_StateServerConnectionError");
					break;
				case 50017:
					text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ApplicationShutdownBuildManagerChange");
					break;
				default:
					switch (eventDetailCode)
					{
					case 50201:
						text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_InvalidTicketFailure");
						break;
					case 50202:
						text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_ExpiredTicketFailure");
						break;
					case 50203:
						text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_InvalidViewStateMac");
						break;
					case 50204:
						text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_InvalidViewState");
						break;
					default:
						if (eventDetailCode == 50301)
						{
							text = WebBaseEvent.FormatResourceStringWithCache("Webevent_detail_SqlProviderEventsDropped");
						}
						break;
					}
					break;
				}
			}
			string text2;
			if (eventCode <= 2001)
			{
				switch (eventCode)
				{
				case 1001:
					text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_ApplicationStart");
					goto IL_3FA;
				case 1002:
					text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_ApplicationShutdown");
					goto IL_3FA;
				case 1003:
					text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_ApplicationCompilationStart");
					goto IL_3FA;
				case 1004:
					text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_ApplicationCompilationEnd");
					goto IL_3FA;
				case 1005:
					text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_ApplicationHeartbeat");
					goto IL_3FA;
				default:
					if (eventCode == 2001)
					{
						text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_RequestTransactionComplete");
						goto IL_3FA;
					}
					break;
				}
			}
			else
			{
				if (eventCode == 2002)
				{
					text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_RequestTransactionAbort");
					goto IL_3FA;
				}
				switch (eventCode)
				{
				case 3001:
					text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_RuntimeErrorRequestAbort");
					goto IL_3FA;
				case 3002:
					text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_RuntimeErrorViewStateFailure");
					goto IL_3FA;
				case 3003:
					text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_RuntimeErrorValidationFailure");
					goto IL_3FA;
				case 3004:
					text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_RuntimeErrorPostTooLarge");
					goto IL_3FA;
				case 3005:
					text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_RuntimeErrorUnhandledException");
					goto IL_3FA;
				case 3006:
					text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_WebErrorParserError");
					goto IL_3FA;
				case 3007:
					text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_WebErrorCompilationError");
					goto IL_3FA;
				case 3008:
					text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_WebErrorConfigurationError");
					goto IL_3FA;
				default:
					switch (eventCode)
					{
					case 4001:
						text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_AuditFormsAuthenticationSuccess");
						goto IL_3FA;
					case 4002:
						text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_AuditMembershipAuthenticationSuccess");
						goto IL_3FA;
					case 4003:
						text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_AuditUrlAuthorizationSuccess");
						goto IL_3FA;
					case 4004:
						text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_AuditFileAuthorizationSuccess");
						goto IL_3FA;
					case 4005:
						text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_AuditFormsAuthenticationFailure");
						goto IL_3FA;
					case 4006:
						text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_AuditMembershipAuthenticationFailure");
						goto IL_3FA;
					case 4007:
						text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_AuditUrlAuthorizationFailure");
						goto IL_3FA;
					case 4008:
						text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_AuditFileAuthorizationFailure");
						goto IL_3FA;
					case 4009:
						text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_AuditInvalidViewStateFailure");
						goto IL_3FA;
					case 4010:
						text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_AuditUnhandledSecurityException");
						goto IL_3FA;
					case 4011:
						text2 = WebBaseEvent.FormatResourceStringWithCache("Webevent_msg_AuditUnhandledAccessException");
						goto IL_3FA;
					}
					break;
				}
			}
			return string.Empty;
			IL_3FA:
			if (text != null)
			{
				text2 = text2 + " " + text;
			}
			return text2;
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x00040B38 File Offset: 0x0003ED38
		internal static int GetEventArrayDimensionSize(int dim)
		{
			return WebEventCodes.s_eventArrayDimensionSizes[dim];
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x00040B41 File Offset: 0x0003ED41
		internal static void GetEventArrayIndexsFromEventCode(int eventCode, out int index0, out int index1)
		{
			index0 = eventCode / 1000 - 1;
			index1 = eventCode - eventCode / 1000 * 1000 - 1;
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x00040B64 File Offset: 0x0003ED64
		private static void InitEventArrayDimensions()
		{
			int num = 0;
			int num2 = 5;
			if (num2 > num)
			{
				num = num2;
			}
			num2 = 2;
			if (num2 > num)
			{
				num = num2;
			}
			num2 = 12;
			if (num2 > num)
			{
				num = num2;
			}
			num2 = 11;
			if (num2 > num)
			{
				num = num2;
			}
			num2 = 1;
			if (num2 > num)
			{
				num = num2;
			}
			WebEventCodes.s_eventArrayDimensionSizes[0] = 6;
			WebEventCodes.s_eventArrayDimensionSizes[1] = num;
		}

		// Token: 0x040015C9 RID: 5577
		public const int InvalidEventCode = -1;

		// Token: 0x040015CA RID: 5578
		public const int UndefinedEventCode = 0;

		// Token: 0x040015CB RID: 5579
		public const int UndefinedEventDetailCode = 0;

		// Token: 0x040015CC RID: 5580
		public const int ApplicationCodeBase = 1000;

		// Token: 0x040015CD RID: 5581
		public const int ApplicationStart = 1001;

		// Token: 0x040015CE RID: 5582
		public const int ApplicationShutdown = 1002;

		// Token: 0x040015CF RID: 5583
		public const int ApplicationCompilationStart = 1003;

		// Token: 0x040015D0 RID: 5584
		public const int ApplicationCompilationEnd = 1004;

		// Token: 0x040015D1 RID: 5585
		public const int ApplicationHeartbeat = 1005;

		// Token: 0x040015D2 RID: 5586
		internal const int ApplicationCodeBaseLast = 1005;

		// Token: 0x040015D3 RID: 5587
		public const int RequestCodeBase = 2000;

		// Token: 0x040015D4 RID: 5588
		public const int RequestTransactionComplete = 2001;

		// Token: 0x040015D5 RID: 5589
		public const int RequestTransactionAbort = 2002;

		// Token: 0x040015D6 RID: 5590
		internal const int RequestCodeBaseLast = 2002;

		// Token: 0x040015D7 RID: 5591
		public const int ErrorCodeBase = 3000;

		// Token: 0x040015D8 RID: 5592
		public const int RuntimeErrorRequestAbort = 3001;

		// Token: 0x040015D9 RID: 5593
		public const int RuntimeErrorViewStateFailure = 3002;

		// Token: 0x040015DA RID: 5594
		public const int RuntimeErrorValidationFailure = 3003;

		// Token: 0x040015DB RID: 5595
		public const int RuntimeErrorPostTooLarge = 3004;

		// Token: 0x040015DC RID: 5596
		public const int RuntimeErrorUnhandledException = 3005;

		// Token: 0x040015DD RID: 5597
		public const int WebErrorParserError = 3006;

		// Token: 0x040015DE RID: 5598
		public const int WebErrorCompilationError = 3007;

		// Token: 0x040015DF RID: 5599
		public const int WebErrorConfigurationError = 3008;

		// Token: 0x040015E0 RID: 5600
		public const int WebErrorOtherError = 3009;

		// Token: 0x040015E1 RID: 5601
		public const int WebErrorPropertyDeserializationError = 3010;

		// Token: 0x040015E2 RID: 5602
		public const int WebErrorObjectStateFormatterDeserializationError = 3011;

		// Token: 0x040015E3 RID: 5603
		public const int RuntimeErrorWebResourceFailure = 3012;

		// Token: 0x040015E4 RID: 5604
		internal const int ErrorCodeBaseLast = 3012;

		// Token: 0x040015E5 RID: 5605
		public const int AuditCodeBase = 4000;

		// Token: 0x040015E6 RID: 5606
		public const int AuditFormsAuthenticationSuccess = 4001;

		// Token: 0x040015E7 RID: 5607
		public const int AuditMembershipAuthenticationSuccess = 4002;

		// Token: 0x040015E8 RID: 5608
		public const int AuditUrlAuthorizationSuccess = 4003;

		// Token: 0x040015E9 RID: 5609
		public const int AuditFileAuthorizationSuccess = 4004;

		// Token: 0x040015EA RID: 5610
		public const int AuditFormsAuthenticationFailure = 4005;

		// Token: 0x040015EB RID: 5611
		public const int AuditMembershipAuthenticationFailure = 4006;

		// Token: 0x040015EC RID: 5612
		public const int AuditUrlAuthorizationFailure = 4007;

		// Token: 0x040015ED RID: 5613
		public const int AuditFileAuthorizationFailure = 4008;

		// Token: 0x040015EE RID: 5614
		public const int AuditInvalidViewStateFailure = 4009;

		// Token: 0x040015EF RID: 5615
		public const int AuditUnhandledSecurityException = 4010;

		// Token: 0x040015F0 RID: 5616
		public const int AuditUnhandledAccessException = 4011;

		// Token: 0x040015F1 RID: 5617
		internal const int AuditCodeBaseLast = 4011;

		// Token: 0x040015F2 RID: 5618
		public const int MiscCodeBase = 6000;

		// Token: 0x040015F3 RID: 5619
		public const int WebEventProviderInformation = 6001;

		// Token: 0x040015F4 RID: 5620
		internal const int MiscCodeBaseLast = 6001;

		// Token: 0x040015F5 RID: 5621
		internal const int LastCodeBase = 6000;

		// Token: 0x040015F6 RID: 5622
		public const int ApplicationDetailCodeBase = 50000;

		// Token: 0x040015F7 RID: 5623
		public const int ApplicationShutdownUnknown = 50001;

		// Token: 0x040015F8 RID: 5624
		public const int ApplicationShutdownHostingEnvironment = 50002;

		// Token: 0x040015F9 RID: 5625
		public const int ApplicationShutdownChangeInGlobalAsax = 50003;

		// Token: 0x040015FA RID: 5626
		public const int ApplicationShutdownConfigurationChange = 50004;

		// Token: 0x040015FB RID: 5627
		public const int ApplicationShutdownUnloadAppDomainCalled = 50005;

		// Token: 0x040015FC RID: 5628
		public const int ApplicationShutdownChangeInSecurityPolicyFile = 50006;

		// Token: 0x040015FD RID: 5629
		public const int ApplicationShutdownBinDirChangeOrDirectoryRename = 50007;

		// Token: 0x040015FE RID: 5630
		public const int ApplicationShutdownBrowsersDirChangeOrDirectoryRename = 50008;

		// Token: 0x040015FF RID: 5631
		public const int ApplicationShutdownCodeDirChangeOrDirectoryRename = 50009;

		// Token: 0x04001600 RID: 5632
		public const int ApplicationShutdownResourcesDirChangeOrDirectoryRename = 50010;

		// Token: 0x04001601 RID: 5633
		public const int ApplicationShutdownIdleTimeout = 50011;

		// Token: 0x04001602 RID: 5634
		public const int ApplicationShutdownPhysicalApplicationPathChanged = 50012;

		// Token: 0x04001603 RID: 5635
		public const int ApplicationShutdownHttpRuntimeClose = 50013;

		// Token: 0x04001604 RID: 5636
		public const int ApplicationShutdownInitializationError = 50014;

		// Token: 0x04001605 RID: 5637
		public const int ApplicationShutdownMaxRecompilationsReached = 50015;

		// Token: 0x04001606 RID: 5638
		public const int StateServerConnectionError = 50016;

		// Token: 0x04001607 RID: 5639
		public const int ApplicationShutdownBuildManagerChange = 50017;

		// Token: 0x04001608 RID: 5640
		public const int AuditDetailCodeBase = 50200;

		// Token: 0x04001609 RID: 5641
		public const int InvalidTicketFailure = 50201;

		// Token: 0x0400160A RID: 5642
		public const int ExpiredTicketFailure = 50202;

		// Token: 0x0400160B RID: 5643
		public const int InvalidViewStateMac = 50203;

		// Token: 0x0400160C RID: 5644
		public const int InvalidViewState = 50204;

		// Token: 0x0400160D RID: 5645
		public const int WebEventDetailCodeBase = 50300;

		// Token: 0x0400160E RID: 5646
		public const int SqlProviderEventsDropped = 50301;

		// Token: 0x0400160F RID: 5647
		public const int WebExtendedBase = 100000;

		// Token: 0x04001610 RID: 5648
		internal static int[] s_eventArrayDimensionSizes = new int[2];
	}
}
