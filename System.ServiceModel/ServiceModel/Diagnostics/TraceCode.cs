using System;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000AA1 RID: 2721
	internal static class TraceCode
	{
		// Token: 0x04003D00 RID: 15616
		public const int Administration = 65536;

		// Token: 0x04003D01 RID: 15617
		public const int WmiPut = 65537;

		// Token: 0x04003D02 RID: 15618
		public const int Diagnostics = 131072;

		// Token: 0x04003D03 RID: 15619
		public const int AppDomainUnload = 131073;

		// Token: 0x04003D04 RID: 15620
		public const int EventLog = 131074;

		// Token: 0x04003D05 RID: 15621
		public const int ThrowingException = 131075;

		// Token: 0x04003D06 RID: 15622
		public const int TraceHandledException = 131076;

		// Token: 0x04003D07 RID: 15623
		public const int UnhandledException = 131077;

		// Token: 0x04003D08 RID: 15624
		public const int FailedToAddAnActivityIdHeader = 131078;

		// Token: 0x04003D09 RID: 15625
		public const int FailedToReadAnActivityIdHeader = 131079;

		// Token: 0x04003D0A RID: 15626
		public const int FilterNotMatchedNodeQuotaExceeded = 131080;

		// Token: 0x04003D0B RID: 15627
		public const int MessageCountLimitExceeded = 131081;

		// Token: 0x04003D0C RID: 15628
		public const int DiagnosticsFailedMessageTrace = 131082;

		// Token: 0x04003D0D RID: 15629
		public const int MessageNotLoggedQuotaExceeded = 131083;

		// Token: 0x04003D0E RID: 15630
		public const int TraceTruncatedQuotaExceeded = 131084;

		// Token: 0x04003D0F RID: 15631
		public const int ActivityBoundary = 131085;

		// Token: 0x04003D10 RID: 15632
		public const int Serialization = 196608;

		// Token: 0x04003D11 RID: 15633
		public const int ElementIgnored = 196615;

		// Token: 0x04003D12 RID: 15634
		public const int Channels = 262144;

		// Token: 0x04003D13 RID: 15635
		public const int ConnectionAbandoned = 262145;

		// Token: 0x04003D14 RID: 15636
		public const int ConnectionPoolCloseException = 262146;

		// Token: 0x04003D15 RID: 15637
		public const int ConnectionPoolIdleTimeoutReached = 262147;

		// Token: 0x04003D16 RID: 15638
		public const int ConnectionPoolLeaseTimeoutReached = 262148;

		// Token: 0x04003D17 RID: 15639
		public const int ConnectionPoolMaxOutboundConnectionsPerEndpointQuotaReached = 262149;

		// Token: 0x04003D18 RID: 15640
		public const int ServerMaxPooledConnectionsQuotaReached = 262150;

		// Token: 0x04003D19 RID: 15641
		public const int EndpointListenerClose = 262151;

		// Token: 0x04003D1A RID: 15642
		public const int EndpointListenerOpen = 262152;

		// Token: 0x04003D1B RID: 15643
		public const int HttpResponseReceived = 262153;

		// Token: 0x04003D1C RID: 15644
		public const int HttpChannelConcurrentReceiveQuotaReached = 262154;

		// Token: 0x04003D1D RID: 15645
		public const int HttpChannelMessageReceiveFailed = 262155;

		// Token: 0x04003D1E RID: 15646
		public const int HttpChannelUnexpectedResponse = 262156;

		// Token: 0x04003D1F RID: 15647
		public const int HttpChannelRequestAborted = 262157;

		// Token: 0x04003D20 RID: 15648
		public const int HttpChannelResponseAborted = 262158;

		// Token: 0x04003D21 RID: 15649
		public const int HttpsClientCertificateInvalid = 262159;

		// Token: 0x04003D22 RID: 15650
		public const int HttpsClientCertificateNotPresent = 262160;

		// Token: 0x04003D23 RID: 15651
		public const int NamedPipeChannelMessageReceiveFailed = 262161;

		// Token: 0x04003D24 RID: 15652
		public const int NamedPipeChannelMessageReceived = 262162;

		// Token: 0x04003D25 RID: 15653
		public const int MessageReceived = 262163;

		// Token: 0x04003D26 RID: 15654
		public const int MessageSent = 262164;

		// Token: 0x04003D27 RID: 15655
		public const int RequestChannelReplyReceived = 262165;

		// Token: 0x04003D28 RID: 15656
		public const int TcpChannelMessageReceiveFailed = 262166;

		// Token: 0x04003D29 RID: 15657
		public const int TcpChannelMessageReceived = 262167;

		// Token: 0x04003D2A RID: 15658
		public const int ConnectToIPEndpoint = 262168;

		// Token: 0x04003D2B RID: 15659
		public const int SocketConnectionCreate = 262169;

		// Token: 0x04003D2C RID: 15660
		public const int SocketConnectionClose = 262170;

		// Token: 0x04003D2D RID: 15661
		public const int SocketConnectionAbort = 262171;

		// Token: 0x04003D2E RID: 15662
		public const int SocketConnectionAbortClose = 262172;

		// Token: 0x04003D2F RID: 15663
		public const int PipeConnectionAbort = 262173;

		// Token: 0x04003D30 RID: 15664
		public const int RequestContextAbort = 262174;

		// Token: 0x04003D31 RID: 15665
		public const int ChannelCreated = 262175;

		// Token: 0x04003D32 RID: 15666
		public const int ChannelDisposed = 262176;

		// Token: 0x04003D33 RID: 15667
		public const int ListenerCreated = 262177;

		// Token: 0x04003D34 RID: 15668
		public const int ListenerDisposed = 262178;

		// Token: 0x04003D35 RID: 15669
		public const int PrematureDatagramEof = 262179;

		// Token: 0x04003D36 RID: 15670
		public const int MaxPendingConnectionsReached = 262180;

		// Token: 0x04003D37 RID: 15671
		public const int MaxAcceptedChannelsReached = 262181;

		// Token: 0x04003D38 RID: 15672
		public const int ChannelConnectionDropped = 262182;

		// Token: 0x04003D39 RID: 15673
		public const int HttpAuthFailed = 262183;

		// Token: 0x04003D3A RID: 15674
		public const int NoExistingTransportManager = 262184;

		// Token: 0x04003D3B RID: 15675
		public const int IncompatibleExistingTransportManager = 262185;

		// Token: 0x04003D3C RID: 15676
		public const int InitiatingNamedPipeConnection = 262186;

		// Token: 0x04003D3D RID: 15677
		public const int InitiatingTcpConnection = 262187;

		// Token: 0x04003D3E RID: 15678
		public const int OpenedListener = 262188;

		// Token: 0x04003D3F RID: 15679
		public const int SslClientCertMissing = 262189;

		// Token: 0x04003D40 RID: 15680
		public const int StreamSecurityUpgradeAccepted = 262190;

		// Token: 0x04003D41 RID: 15681
		public const int TcpConnectError = 262191;

		// Token: 0x04003D42 RID: 15682
		public const int FailedAcceptFromPool = 262192;

		// Token: 0x04003D43 RID: 15683
		public const int FailedPipeConnect = 262193;

		// Token: 0x04003D44 RID: 15684
		public const int SystemTimeResolution = 262194;

		// Token: 0x04003D45 RID: 15685
		public const int PeerNeighborCloseFailed = 262195;

		// Token: 0x04003D46 RID: 15686
		public const int PeerNeighborClosingFailed = 262196;

		// Token: 0x04003D47 RID: 15687
		public const int PeerNeighborNotAccepted = 262197;

		// Token: 0x04003D48 RID: 15688
		public const int PeerNeighborNotFound = 262198;

		// Token: 0x04003D49 RID: 15689
		public const int PeerNeighborOpenFailed = 262199;

		// Token: 0x04003D4A RID: 15690
		public const int PeerNeighborStateChanged = 262200;

		// Token: 0x04003D4B RID: 15691
		public const int PeerNeighborStateChangeFailed = 262201;

		// Token: 0x04003D4C RID: 15692
		public const int PeerNeighborMessageReceived = 262202;

		// Token: 0x04003D4D RID: 15693
		public const int PeerNeighborManagerOffline = 262203;

		// Token: 0x04003D4E RID: 15694
		public const int PeerNeighborManagerOnline = 262204;

		// Token: 0x04003D4F RID: 15695
		public const int PeerChannelMessageReceived = 262205;

		// Token: 0x04003D50 RID: 15696
		public const int PeerChannelMessageSent = 262206;

		// Token: 0x04003D51 RID: 15697
		public const int PeerNodeAddressChanged = 262207;

		// Token: 0x04003D52 RID: 15698
		public const int PeerNodeOpening = 262208;

		// Token: 0x04003D53 RID: 15699
		public const int PeerNodeOpened = 262209;

		// Token: 0x04003D54 RID: 15700
		public const int PeerNodeOpenFailed = 262210;

		// Token: 0x04003D55 RID: 15701
		public const int PeerNodeClosing = 262211;

		// Token: 0x04003D56 RID: 15702
		public const int PeerNodeClosed = 262212;

		// Token: 0x04003D57 RID: 15703
		public const int PeerFloodedMessageReceived = 262213;

		// Token: 0x04003D58 RID: 15704
		public const int PeerFloodedMessageNotPropagated = 262214;

		// Token: 0x04003D59 RID: 15705
		public const int PeerFloodedMessageNotMatched = 262215;

		// Token: 0x04003D5A RID: 15706
		public const int PnrpRegisteredAddresses = 262216;

		// Token: 0x04003D5B RID: 15707
		public const int PnrpUnregisteredAddresses = 262217;

		// Token: 0x04003D5C RID: 15708
		public const int PnrpResolvedAddresses = 262218;

		// Token: 0x04003D5D RID: 15709
		public const int PnrpResolveException = 262219;

		// Token: 0x04003D5E RID: 15710
		public const int PeerReceiveMessageAuthenticationFailure = 262220;

		// Token: 0x04003D5F RID: 15711
		public const int PeerNodeAuthenticationFailure = 262221;

		// Token: 0x04003D60 RID: 15712
		public const int PeerNodeAuthenticationTimeout = 262222;

		// Token: 0x04003D61 RID: 15713
		public const int PeerFlooderReceiveMessageQuotaExceeded = 262223;

		// Token: 0x04003D62 RID: 15714
		public const int PeerServiceOpened = 262224;

		// Token: 0x04003D63 RID: 15715
		public const int PeerMaintainerActivity = 262225;

		// Token: 0x04003D64 RID: 15716
		public const int MsmqCannotPeekOnQueue = 262226;

		// Token: 0x04003D65 RID: 15717
		public const int MsmqCannotReadQueues = 262227;

		// Token: 0x04003D66 RID: 15718
		public const int MsmqDatagramSent = 262228;

		// Token: 0x04003D67 RID: 15719
		public const int MsmqDatagramReceived = 262229;

		// Token: 0x04003D68 RID: 15720
		public const int MsmqDetected = 262230;

		// Token: 0x04003D69 RID: 15721
		public const int MsmqEnteredBatch = 262231;

		// Token: 0x04003D6A RID: 15722
		public const int MsmqExpectedException = 262232;

		// Token: 0x04003D6B RID: 15723
		public const int MsmqFoundBaseAddress = 262233;

		// Token: 0x04003D6C RID: 15724
		public const int MsmqLeftBatch = 262234;

		// Token: 0x04003D6D RID: 15725
		public const int MsmqMatchedApplicationFound = 262235;

		// Token: 0x04003D6E RID: 15726
		public const int MsmqMessageDropped = 262236;

		// Token: 0x04003D6F RID: 15727
		public const int MsmqMessageLockedUnderTheTransaction = 262237;

		// Token: 0x04003D70 RID: 15728
		public const int MsmqMessageRejected = 262238;

		// Token: 0x04003D71 RID: 15729
		public const int MsmqMoveOrDeleteAttemptFailed = 262239;

		// Token: 0x04003D72 RID: 15730
		public const int MsmqPoisonMessageMovedPoison = 262240;

		// Token: 0x04003D73 RID: 15731
		public const int MsmqPoisonMessageMovedRetry = 262241;

		// Token: 0x04003D74 RID: 15732
		public const int MsmqPoisonMessageRejected = 262242;

		// Token: 0x04003D75 RID: 15733
		public const int MsmqPoolFull = 262243;

		// Token: 0x04003D76 RID: 15734
		public const int MsmqPotentiallyPoisonMessageDetected = 262244;

		// Token: 0x04003D77 RID: 15735
		public const int MsmqQueueClosed = 262245;

		// Token: 0x04003D78 RID: 15736
		public const int MsmqQueueOpened = 262246;

		// Token: 0x04003D79 RID: 15737
		public const int MsmqQueueTransactionalStatusUnknown = 262247;

		// Token: 0x04003D7A RID: 15738
		public const int MsmqScanStarted = 262248;

		// Token: 0x04003D7B RID: 15739
		public const int MsmqSessiongramReceived = 262249;

		// Token: 0x04003D7C RID: 15740
		public const int MsmqSessiongramSent = 262250;

		// Token: 0x04003D7D RID: 15741
		public const int MsmqStartingApplication = 262251;

		// Token: 0x04003D7E RID: 15742
		public const int MsmqStartingService = 262252;

		// Token: 0x04003D7F RID: 15743
		public const int MsmqUnexpectedAcknowledgment = 262253;

		// Token: 0x04003D80 RID: 15744
		public const int WsrmNegativeElapsedTimeDetected = 262254;

		// Token: 0x04003D81 RID: 15745
		public const int TcpTransferError = 262255;

		// Token: 0x04003D82 RID: 15746
		public const int TcpConnectionResetError = 262256;

		// Token: 0x04003D83 RID: 15747
		public const int TcpConnectionTimedOut = 262257;

		// Token: 0x04003D84 RID: 15748
		public const int ComIntegration = 327680;

		// Token: 0x04003D85 RID: 15749
		public const int ComIntegrationServiceHostStartingService = 327681;

		// Token: 0x04003D86 RID: 15750
		public const int ComIntegrationServiceHostStartedService = 327682;

		// Token: 0x04003D87 RID: 15751
		public const int ComIntegrationServiceHostCreatedServiceContract = 327683;

		// Token: 0x04003D88 RID: 15752
		public const int ComIntegrationServiceHostStartedServiceDetails = 327684;

		// Token: 0x04003D89 RID: 15753
		public const int ComIntegrationServiceHostCreatedServiceEndpoint = 327685;

		// Token: 0x04003D8A RID: 15754
		public const int ComIntegrationServiceHostStoppingService = 327686;

		// Token: 0x04003D8B RID: 15755
		public const int ComIntegrationServiceHostStoppedService = 327687;

		// Token: 0x04003D8C RID: 15756
		public const int ComIntegrationDllHostInitializerStarting = 327688;

		// Token: 0x04003D8D RID: 15757
		public const int ComIntegrationDllHostInitializerAddingHost = 327689;

		// Token: 0x04003D8E RID: 15758
		public const int ComIntegrationDllHostInitializerStarted = 327690;

		// Token: 0x04003D8F RID: 15759
		public const int ComIntegrationDllHostInitializerStopping = 327691;

		// Token: 0x04003D90 RID: 15760
		public const int ComIntegrationDllHostInitializerStopped = 327692;

		// Token: 0x04003D91 RID: 15761
		public const int ComIntegrationTLBImportStarting = 327693;

		// Token: 0x04003D92 RID: 15762
		public const int ComIntegrationTLBImportFromAssembly = 327694;

		// Token: 0x04003D93 RID: 15763
		public const int ComIntegrationTLBImportFromTypelib = 327695;

		// Token: 0x04003D94 RID: 15764
		public const int ComIntegrationTLBImportConverterEvent = 327696;

		// Token: 0x04003D95 RID: 15765
		public const int ComIntegrationTLBImportFinished = 327697;

		// Token: 0x04003D96 RID: 15766
		public const int ComIntegrationInstanceCreationRequest = 327698;

		// Token: 0x04003D97 RID: 15767
		public const int ComIntegrationInstanceCreationSuccess = 327699;

		// Token: 0x04003D98 RID: 15768
		public const int ComIntegrationInstanceReleased = 327700;

		// Token: 0x04003D99 RID: 15769
		public const int ComIntegrationEnteringActivity = 327701;

		// Token: 0x04003D9A RID: 15770
		public const int ComIntegrationExecutingCall = 327702;

		// Token: 0x04003D9B RID: 15771
		public const int ComIntegrationLeftActivity = 327703;

		// Token: 0x04003D9C RID: 15772
		public const int ComIntegrationInvokingMethod = 327704;

		// Token: 0x04003D9D RID: 15773
		public const int ComIntegrationInvokedMethod = 327705;

		// Token: 0x04003D9E RID: 15774
		public const int ComIntegrationInvokingMethodNewTransaction = 327706;

		// Token: 0x04003D9F RID: 15775
		public const int ComIntegrationInvokingMethodContextTransaction = 327707;

		// Token: 0x04003DA0 RID: 15776
		public const int ComIntegrationServiceMonikerParsed = 327708;

		// Token: 0x04003DA1 RID: 15777
		public const int ComIntegrationWsdlChannelBuilderLoaded = 327709;

		// Token: 0x04003DA2 RID: 15778
		public const int ComIntegrationTypedChannelBuilderLoaded = 327710;

		// Token: 0x04003DA3 RID: 15779
		public const int ComIntegrationChannelCreated = 327711;

		// Token: 0x04003DA4 RID: 15780
		public const int ComIntegrationDispatchMethod = 327712;

		// Token: 0x04003DA5 RID: 15781
		public const int ComIntegrationTxProxyTxCommitted = 327713;

		// Token: 0x04003DA6 RID: 15782
		public const int ComIntegrationTxProxyTxAbortedByContext = 327714;

		// Token: 0x04003DA7 RID: 15783
		public const int ComIntegrationTxProxyTxAbortedByTM = 327715;

		// Token: 0x04003DA8 RID: 15784
		public const int ComIntegrationMexMonikerMetadataExchangeComplete = 327716;

		// Token: 0x04003DA9 RID: 15785
		public const int ComIntegrationMexChannelBuilderLoaded = 327717;

		// Token: 0x04003DAA RID: 15786
		public const int Security = 458752;

		// Token: 0x04003DAB RID: 15787
		public const int SecurityIdentityVerificationSuccess = 458753;

		// Token: 0x04003DAC RID: 15788
		public const int SecurityIdentityVerificationFailure = 458754;

		// Token: 0x04003DAD RID: 15789
		public const int SecurityIdentityDeterminationSuccess = 458755;

		// Token: 0x04003DAE RID: 15790
		public const int SecurityIdentityDeterminationFailure = 458756;

		// Token: 0x04003DAF RID: 15791
		public const int SecurityIdentityHostNameNormalizationFailure = 458757;

		// Token: 0x04003DB0 RID: 15792
		public const int SecurityImpersonationSuccess = 458758;

		// Token: 0x04003DB1 RID: 15793
		public const int SecurityImpersonationFailure = 458759;

		// Token: 0x04003DB2 RID: 15794
		public const int SecurityNegotiationProcessingFailure = 458760;

		// Token: 0x04003DB3 RID: 15795
		public const int IssuanceTokenProviderRemovedCachedToken = 458761;

		// Token: 0x04003DB4 RID: 15796
		public const int IssuanceTokenProviderUsingCachedToken = 458762;

		// Token: 0x04003DB5 RID: 15797
		public const int IssuanceTokenProviderBeginSecurityNegotiation = 458763;

		// Token: 0x04003DB6 RID: 15798
		public const int IssuanceTokenProviderEndSecurityNegotiation = 458764;

		// Token: 0x04003DB7 RID: 15799
		public const int IssuanceTokenProviderRedirectApplied = 458765;

		// Token: 0x04003DB8 RID: 15800
		public const int IssuanceTokenProviderServiceTokenCacheFull = 458766;

		// Token: 0x04003DB9 RID: 15801
		public const int NegotiationTokenProviderAttached = 458767;

		// Token: 0x04003DBA RID: 15802
		public const int SpnegoClientNegotiationCompleted = 458784;

		// Token: 0x04003DBB RID: 15803
		public const int SpnegoServiceNegotiationCompleted = 458785;

		// Token: 0x04003DBC RID: 15804
		public const int SpnegoClientNegotiation = 458786;

		// Token: 0x04003DBD RID: 15805
		public const int SpnegoServiceNegotiation = 458787;

		// Token: 0x04003DBE RID: 15806
		public const int NegotiationAuthenticatorAttached = 458788;

		// Token: 0x04003DBF RID: 15807
		public const int ServiceSecurityNegotiationCompleted = 458789;

		// Token: 0x04003DC0 RID: 15808
		public const int SecurityContextTokenCacheFull = 458790;

		// Token: 0x04003DC1 RID: 15809
		public const int ExportSecurityChannelBindingEntry = 458791;

		// Token: 0x04003DC2 RID: 15810
		public const int ExportSecurityChannelBindingExit = 458792;

		// Token: 0x04003DC3 RID: 15811
		public const int ImportSecurityChannelBindingEntry = 458793;

		// Token: 0x04003DC4 RID: 15812
		public const int ImportSecurityChannelBindingExit = 458794;

		// Token: 0x04003DC5 RID: 15813
		public const int SecurityTokenProviderOpened = 458795;

		// Token: 0x04003DC6 RID: 15814
		public const int SecurityTokenProviderClosed = 458796;

		// Token: 0x04003DC7 RID: 15815
		public const int SecurityTokenAuthenticatorOpened = 458797;

		// Token: 0x04003DC8 RID: 15816
		public const int SecurityTokenAuthenticatorClosed = 458798;

		// Token: 0x04003DC9 RID: 15817
		public const int SecurityBindingOutgoingMessageSecured = 458799;

		// Token: 0x04003DCA RID: 15818
		public const int SecurityBindingIncomingMessageVerified = 458800;

		// Token: 0x04003DCB RID: 15819
		public const int SecurityBindingSecureOutgoingMessageFailure = 458801;

		// Token: 0x04003DCC RID: 15820
		public const int SecurityBindingVerifyIncomingMessageFailure = 458802;

		// Token: 0x04003DCD RID: 15821
		public const int SecuritySpnToSidMappingFailure = 458803;

		// Token: 0x04003DCE RID: 15822
		public const int SecuritySessionRedirectApplied = 458804;

		// Token: 0x04003DCF RID: 15823
		public const int SecurityClientSessionCloseSent = 458805;

		// Token: 0x04003DD0 RID: 15824
		public const int SecurityClientSessionCloseResponseSent = 458806;

		// Token: 0x04003DD1 RID: 15825
		public const int SecurityClientSessionCloseMessageReceived = 458807;

		// Token: 0x04003DD2 RID: 15826
		public const int SecuritySessionKeyRenewalFaultReceived = 458808;

		// Token: 0x04003DD3 RID: 15827
		public const int SecuritySessionAbortedFaultReceived = 458809;

		// Token: 0x04003DD4 RID: 15828
		public const int SecuritySessionClosedResponseReceived = 458810;

		// Token: 0x04003DD5 RID: 15829
		public const int SecurityClientSessionPreviousKeyDiscarded = 458811;

		// Token: 0x04003DD6 RID: 15830
		public const int SecurityClientSessionKeyRenewed = 458812;

		// Token: 0x04003DD7 RID: 15831
		public const int SecurityPendingServerSessionAdded = 458813;

		// Token: 0x04003DD8 RID: 15832
		public const int SecurityPendingServerSessionClosed = 458814;

		// Token: 0x04003DD9 RID: 15833
		public const int SecurityPendingServerSessionActivated = 458815;

		// Token: 0x04003DDA RID: 15834
		public const int SecurityActiveServerSessionRemoved = 458816;

		// Token: 0x04003DDB RID: 15835
		public const int SecurityNewServerSessionKeyIssued = 458817;

		// Token: 0x04003DDC RID: 15836
		public const int SecurityInactiveSessionFaulted = 458818;

		// Token: 0x04003DDD RID: 15837
		public const int SecurityServerSessionKeyUpdated = 458819;

		// Token: 0x04003DDE RID: 15838
		public const int SecurityServerSessionCloseReceived = 458820;

		// Token: 0x04003DDF RID: 15839
		public const int SecurityServerSessionRenewalFaultSent = 458821;

		// Token: 0x04003DE0 RID: 15840
		public const int SecurityServerSessionAbortedFaultSent = 458822;

		// Token: 0x04003DE1 RID: 15841
		public const int SecuritySessionCloseResponseSent = 458823;

		// Token: 0x04003DE2 RID: 15842
		public const int SecuritySessionServerCloseSent = 458824;

		// Token: 0x04003DE3 RID: 15843
		public const int SecurityServerSessionCloseResponseReceived = 458825;

		// Token: 0x04003DE4 RID: 15844
		public const int SecuritySessionRenewFaultSendFailure = 458826;

		// Token: 0x04003DE5 RID: 15845
		public const int SecuritySessionAbortedFaultSendFailure = 458827;

		// Token: 0x04003DE6 RID: 15846
		public const int SecuritySessionClosedResponseSendFailure = 458828;

		// Token: 0x04003DE7 RID: 15847
		public const int SecuritySessionServerCloseSendFailure = 458829;

		// Token: 0x04003DE8 RID: 15848
		public const int SecuritySessionRequestorStartOperation = 458830;

		// Token: 0x04003DE9 RID: 15849
		public const int SecuritySessionRequestorOperationSuccess = 458831;

		// Token: 0x04003DEA RID: 15850
		public const int SecuritySessionRequestorOperationFailure = 458832;

		// Token: 0x04003DEB RID: 15851
		public const int SecuritySessionResponderOperationFailure = 458833;

		// Token: 0x04003DEC RID: 15852
		public const int SecuritySessionDemuxFailure = 458834;

		// Token: 0x04003DED RID: 15853
		public const int SecurityAuditWrittenSuccess = 458835;

		// Token: 0x04003DEE RID: 15854
		public const int SecurityAuditWrittenFailure = 458836;

		// Token: 0x04003DEF RID: 15855
		public const int ServiceModel = 524288;

		// Token: 0x04003DF0 RID: 15856
		public const int AsyncCallbackThrewException = 524289;

		// Token: 0x04003DF1 RID: 15857
		public const int CommunicationObjectAborted = 524290;

		// Token: 0x04003DF2 RID: 15858
		public const int CommunicationObjectAbortFailed = 524291;

		// Token: 0x04003DF3 RID: 15859
		public const int CommunicationObjectCloseFailed = 524292;

		// Token: 0x04003DF4 RID: 15860
		public const int CommunicationObjectOpenFailed = 524293;

		// Token: 0x04003DF5 RID: 15861
		public const int CommunicationObjectClosing = 524294;

		// Token: 0x04003DF6 RID: 15862
		public const int CommunicationObjectClosed = 524295;

		// Token: 0x04003DF7 RID: 15863
		public const int CommunicationObjectCreated = 524296;

		// Token: 0x04003DF8 RID: 15864
		public const int CommunicationObjectDisposing = 524297;

		// Token: 0x04003DF9 RID: 15865
		public const int CommunicationObjectFaultReason = 524298;

		// Token: 0x04003DFA RID: 15866
		public const int CommunicationObjectFaulted = 524299;

		// Token: 0x04003DFB RID: 15867
		public const int CommunicationObjectOpening = 524300;

		// Token: 0x04003DFC RID: 15868
		public const int CommunicationObjectOpened = 524301;

		// Token: 0x04003DFD RID: 15869
		public const int DidNotUnderstandMessageHeader = 524302;

		// Token: 0x04003DFE RID: 15870
		public const int UnderstoodMessageHeader = 524303;

		// Token: 0x04003DFF RID: 15871
		public const int MessageClosed = 524304;

		// Token: 0x04003E00 RID: 15872
		public const int MessageClosedAgain = 524305;

		// Token: 0x04003E01 RID: 15873
		public const int MessageCopied = 524306;

		// Token: 0x04003E02 RID: 15874
		public const int MessageRead = 524307;

		// Token: 0x04003E03 RID: 15875
		public const int MessageWritten = 524308;

		// Token: 0x04003E04 RID: 15876
		public const int BeginExecuteMethod = 524309;

		// Token: 0x04003E05 RID: 15877
		public const int ConfigurationIsReadOnly = 524310;

		// Token: 0x04003E06 RID: 15878
		public const int ConfiguredExtensionTypeNotFound = 524311;

		// Token: 0x04003E07 RID: 15879
		public const int EvaluationContextNotFound = 524312;

		// Token: 0x04003E08 RID: 15880
		public const int EndExecuteMethod = 524313;

		// Token: 0x04003E09 RID: 15881
		public const int ExtensionCollectionDoesNotExist = 524314;

		// Token: 0x04003E0A RID: 15882
		public const int ExtensionCollectionNameNotFound = 524315;

		// Token: 0x04003E0B RID: 15883
		public const int ExtensionCollectionIsEmpty = 524316;

		// Token: 0x04003E0C RID: 15884
		public const int ExtensionElementAlreadyExistsInCollection = 524317;

		// Token: 0x04003E0D RID: 15885
		public const int ElementTypeDoesntMatchConfiguredType = 524318;

		// Token: 0x04003E0E RID: 15886
		public const int ErrorInvokingUserCode = 524319;

		// Token: 0x04003E0F RID: 15887
		public const int GetBehaviorElement = 524320;

		// Token: 0x04003E10 RID: 15888
		public const int GetCommonBehaviors = 524321;

		// Token: 0x04003E11 RID: 15889
		public const int GetConfiguredBinding = 524322;

		// Token: 0x04003E12 RID: 15890
		public const int GetChannelEndpointElement = 524323;

		// Token: 0x04003E13 RID: 15891
		public const int GetConfigurationSection = 524324;

		// Token: 0x04003E14 RID: 15892
		public const int GetDefaultConfiguredBinding = 524325;

		// Token: 0x04003E15 RID: 15893
		public const int GetServiceElement = 524326;

		// Token: 0x04003E16 RID: 15894
		public const int MessageProcessingPaused = 524327;

		// Token: 0x04003E17 RID: 15895
		public const int ManualFlowThrottleLimitReached = 524328;

		// Token: 0x04003E18 RID: 15896
		public const int OverridingDuplicateConfigurationKey = 524329;

		// Token: 0x04003E19 RID: 15897
		public const int RemoveBehavior = 524330;

		// Token: 0x04003E1A RID: 15898
		public const int ServiceChannelLifetime = 524331;

		// Token: 0x04003E1B RID: 15899
		public const int ServiceHostCreation = 524332;

		// Token: 0x04003E1C RID: 15900
		public const int ServiceHostBaseAddresses = 524333;

		// Token: 0x04003E1D RID: 15901
		public const int ServiceHostTimeoutOnClose = 524334;

		// Token: 0x04003E1E RID: 15902
		public const int ServiceHostFaulted = 524335;

		// Token: 0x04003E1F RID: 15903
		public const int ServiceHostErrorOnReleasePerformanceCounter = 524336;

		// Token: 0x04003E20 RID: 15904
		public const int ServiceThrottleLimitReached = 524337;

		// Token: 0x04003E21 RID: 15905
		public const int ServiceOperationMissingReply = 524338;

		// Token: 0x04003E22 RID: 15906
		public const int ServiceOperationMissingReplyContext = 524339;

		// Token: 0x04003E23 RID: 15907
		public const int ServiceOperationExceptionOnReply = 524340;

		// Token: 0x04003E24 RID: 15908
		public const int SkipBehavior = 524341;

		// Token: 0x04003E25 RID: 15909
		public const int TransportListen = 524342;

		// Token: 0x04003E26 RID: 15910
		public const int UnhandledAction = 524343;

		// Token: 0x04003E27 RID: 15911
		public const int PerformanceCounterFailedToLoad = 524344;

		// Token: 0x04003E28 RID: 15912
		public const int PerformanceCountersFailed = 524345;

		// Token: 0x04003E29 RID: 15913
		public const int PerformanceCountersFailedDuringUpdate = 524346;

		// Token: 0x04003E2A RID: 15914
		public const int PerformanceCountersFailedForService = 524347;

		// Token: 0x04003E2B RID: 15915
		public const int PerformanceCountersFailedOnRelease = 524348;

		// Token: 0x04003E2C RID: 15916
		public const int WsmexNonCriticalWsdlExportError = 524349;

		// Token: 0x04003E2D RID: 15917
		public const int WsmexNonCriticalWsdlImportError = 524350;

		// Token: 0x04003E2E RID: 15918
		public const int FailedToOpenIncomingChannel = 524351;

		// Token: 0x04003E2F RID: 15919
		public const int UnhandledExceptionInUserOperation = 524352;

		// Token: 0x04003E30 RID: 15920
		public const int DroppedAMessage = 524353;

		// Token: 0x04003E31 RID: 15921
		public const int CannotBeImportedInCurrentFormat = 524354;

		// Token: 0x04003E32 RID: 15922
		public const int GetConfiguredEndpoint = 524355;

		// Token: 0x04003E33 RID: 15923
		public const int GetDefaultConfiguredEndpoint = 524356;

		// Token: 0x04003E34 RID: 15924
		public const int ExtensionTypeNotFound = 524357;

		// Token: 0x04003E35 RID: 15925
		public const int DefaultEndpointsAdded = 524358;

		// Token: 0x04003E36 RID: 15926
		public const int MetadataExchangeClientSendRequest = 524379;

		// Token: 0x04003E37 RID: 15927
		public const int MetadataExchangeClientReceiveReply = 524380;

		// Token: 0x04003E38 RID: 15928
		public const int WarnHelpPageEnabledNoBaseAddress = 524381;

		// Token: 0x04003E39 RID: 15929
		public const int WarnServiceHealthEnabledNoBaseAddress = 524382;

		// Token: 0x04003E3A RID: 15930
		public const int PortSharing = 655360;

		// Token: 0x04003E3B RID: 15931
		public const int PortSharingClosed = 655361;

		// Token: 0x04003E3C RID: 15932
		public const int PortSharingDuplicatedPipe = 655362;

		// Token: 0x04003E3D RID: 15933
		public const int PortSharingDupHandleGranted = 655363;

		// Token: 0x04003E3E RID: 15934
		public const int PortSharingDuplicatedSocket = 655364;

		// Token: 0x04003E3F RID: 15935
		public const int PortSharingListening = 655365;

		// Token: 0x04003E40 RID: 15936
		public const int SharedManagerServiceEndpointNotExist = 655374;

		// Token: 0x04003E41 RID: 15937
		public const int ServiceModelTransaction = 917504;

		// Token: 0x04003E42 RID: 15938
		public const int TxSourceTxScopeRequiredIsTransactedTransport = 917505;

		// Token: 0x04003E43 RID: 15939
		public const int TxSourceTxScopeRequiredIsTransactionFlow = 917506;

		// Token: 0x04003E44 RID: 15940
		public const int TxSourceTxScopeRequiredIsAttachedTransaction = 917507;

		// Token: 0x04003E45 RID: 15941
		public const int TxSourceTxScopeRequiredIsCreateNewTransaction = 917508;

		// Token: 0x04003E46 RID: 15942
		public const int TxCompletionStatusCompletedForAutocomplete = 917509;

		// Token: 0x04003E47 RID: 15943
		public const int TxCompletionStatusCompletedForError = 917510;

		// Token: 0x04003E48 RID: 15944
		public const int TxCompletionStatusCompletedForSetComplete = 917511;

		// Token: 0x04003E49 RID: 15945
		public const int TxCompletionStatusCompletedForTACOSC = 917512;

		// Token: 0x04003E4A RID: 15946
		public const int TxCompletionStatusCompletedForAsyncAbort = 917513;

		// Token: 0x04003E4B RID: 15947
		public const int TxCompletionStatusRemainsAttached = 917514;

		// Token: 0x04003E4C RID: 15948
		public const int TxCompletionStatusAbortedOnSessionClose = 917515;

		// Token: 0x04003E4D RID: 15949
		public const int TxReleaseServiceInstanceOnCompletion = 917516;

		// Token: 0x04003E4E RID: 15950
		public const int TxAsyncAbort = 917517;

		// Token: 0x04003E4F RID: 15951
		public const int TxFailedToNegotiateOleTx = 917518;

		// Token: 0x04003E50 RID: 15952
		public const int TxSourceTxScopeRequiredUsingExistingTransaction = 917519;

		// Token: 0x04003E51 RID: 15953
		public const int NetFx35 = 983040;

		// Token: 0x04003E52 RID: 15954
		public const int ActivatingMessageReceived = 983040;

		// Token: 0x04003E53 RID: 15955
		public const int InstanceContextBoundToDurableInstance = 983041;

		// Token: 0x04003E54 RID: 15956
		public const int InstanceContextDetachedFromDurableInstance = 983042;

		// Token: 0x04003E55 RID: 15957
		public const int ContextChannelFactoryChannelCreated = 983043;

		// Token: 0x04003E56 RID: 15958
		public const int ContextChannelListenerChannelAccepted = 983044;

		// Token: 0x04003E57 RID: 15959
		public const int ContextProtocolContextAddedToMessage = 983045;

		// Token: 0x04003E58 RID: 15960
		public const int ContextProtocolContextRetrievedFromMessage = 983046;

		// Token: 0x04003E59 RID: 15961
		public const int DICPInstanceContextCached = 983047;

		// Token: 0x04003E5A RID: 15962
		public const int DICPInstanceContextRemovedFromCache = 983048;

		// Token: 0x04003E5B RID: 15963
		public const int ServiceDurableInstanceDeleted = 983049;

		// Token: 0x04003E5C RID: 15964
		public const int ServiceDurableInstanceDisposed = 983050;

		// Token: 0x04003E5D RID: 15965
		public const int ServiceDurableInstanceLoaded = 983051;

		// Token: 0x04003E5E RID: 15966
		public const int ServiceDurableInstanceSaved = 983052;

		// Token: 0x04003E5F RID: 15967
		public const int SqlPersistenceProviderSQLCallStart = 983053;

		// Token: 0x04003E60 RID: 15968
		public const int SqlPersistenceProviderSQLCallEnd = 983054;

		// Token: 0x04003E61 RID: 15969
		public const int SqlPersistenceProviderOpenParameters = 983055;

		// Token: 0x04003E62 RID: 15970
		public const int SyncContextSchedulerServiceTimerCancelled = 983056;

		// Token: 0x04003E63 RID: 15971
		public const int SyncContextSchedulerServiceTimerCreated = 983057;

		// Token: 0x04003E64 RID: 15972
		public const int WorkflowDurableInstanceLoaded = 983058;

		// Token: 0x04003E65 RID: 15973
		public const int WorkflowDurableInstanceAborted = 983059;

		// Token: 0x04003E66 RID: 15974
		public const int WorkflowDurableInstanceActivated = 983060;

		// Token: 0x04003E67 RID: 15975
		public const int WorkflowOperationInvokerItemQueued = 983061;

		// Token: 0x04003E68 RID: 15976
		public const int WorkflowRequestContextReplySent = 983062;

		// Token: 0x04003E69 RID: 15977
		public const int WorkflowRequestContextFaultSent = 983063;

		// Token: 0x04003E6A RID: 15978
		public const int WorkflowServiceHostCreated = 983064;

		// Token: 0x04003E6B RID: 15979
		public const int SyndicationReadFeedBegin = 983065;

		// Token: 0x04003E6C RID: 15980
		public const int SyndicationReadFeedEnd = 983066;

		// Token: 0x04003E6D RID: 15981
		public const int SyndicationReadItemBegin = 983067;

		// Token: 0x04003E6E RID: 15982
		public const int SyndicationReadItemEnd = 983068;

		// Token: 0x04003E6F RID: 15983
		public const int SyndicationWriteFeedBegin = 983069;

		// Token: 0x04003E70 RID: 15984
		public const int SyndicationWriteFeedEnd = 983070;

		// Token: 0x04003E71 RID: 15985
		public const int SyndicationWriteItemBegin = 983071;

		// Token: 0x04003E72 RID: 15986
		public const int SyndicationWriteItemEnd = 983072;

		// Token: 0x04003E73 RID: 15987
		public const int SyndicationProtocolElementIgnoredOnRead = 983073;

		// Token: 0x04003E74 RID: 15988
		public const int SyndicationProtocolElementIgnoredOnWrite = 983074;

		// Token: 0x04003E75 RID: 15989
		public const int SyndicationProtocolElementInvalid = 983075;

		// Token: 0x04003E76 RID: 15990
		public const int WebUnknownQueryParameterIgnored = 983076;

		// Token: 0x04003E77 RID: 15991
		public const int WebRequestMatchesOperation = 983077;

		// Token: 0x04003E78 RID: 15992
		public const int WebRequestDoesNotMatchOperations = 983078;

		// Token: 0x04003E79 RID: 15993
		public const int WebRequestRedirect = 983079;

		// Token: 0x04003E7A RID: 15994
		public const int SyndicationReadServiceDocumentBegin = 983080;

		// Token: 0x04003E7B RID: 15995
		public const int SyndicationReadServiceDocumentEnd = 983081;

		// Token: 0x04003E7C RID: 15996
		public const int SyndicationReadCategoriesDocumentBegin = 983082;

		// Token: 0x04003E7D RID: 15997
		public const int SyndicationReadCategoriesDocumentEnd = 983083;

		// Token: 0x04003E7E RID: 15998
		public const int SyndicationWriteServiceDocumentBegin = 983084;

		// Token: 0x04003E7F RID: 15999
		public const int SyndicationWriteServiceDocumentEnd = 983085;

		// Token: 0x04003E80 RID: 16000
		public const int SyndicationWriteCategoriesDocumentBegin = 983086;

		// Token: 0x04003E81 RID: 16001
		public const int SyndicationWriteCategoriesDocumentEnd = 983087;

		// Token: 0x04003E82 RID: 16002
		public const int AutomaticFormatSelectedOperationDefault = 983088;

		// Token: 0x04003E83 RID: 16003
		public const int AutomaticFormatSelectedRequestBased = 983089;

		// Token: 0x04003E84 RID: 16004
		public const int RequestFormatSelectedFromContentTypeMapper = 983090;

		// Token: 0x04003E85 RID: 16005
		public const int RequestFormatSelectedByEncoderDefaults = 983091;

		// Token: 0x04003E86 RID: 16006
		public const int AddingResponseToOutputCache = 983092;

		// Token: 0x04003E87 RID: 16007
		public const int AddingAuthenticatedResponseToOutputCache = 983093;

		// Token: 0x04003E88 RID: 16008
		public const int JsonpCallbackNameSet = 983095;
	}
}
