using System;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x0200020B RID: 523
	internal abstract class WSTrustConstantsAdapter
	{
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00048311 File Offset: 0x00046511
		internal static WSTrustFeb2005ConstantsAdapter TrustFeb2005
		{
			get
			{
				return WSTrustFeb2005ConstantsAdapter.Instance;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x00048318 File Offset: 0x00046518
		internal static WSTrust13ConstantsAdapter Trust13
		{
			get
			{
				return WSTrust13ConstantsAdapter.Instance;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0004831F File Offset: 0x0004651F
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x00048327 File Offset: 0x00046527
		internal string NamespaceURI
		{
			get
			{
				return this.namespaceURI;
			}
			set
			{
				this.namespaceURI = value;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x00048330 File Offset: 0x00046530
		// (set) Token: 0x06001133 RID: 4403 RVA: 0x00048338 File Offset: 0x00046538
		internal string Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001134 RID: 4404
		internal abstract WSTrustConstantsAdapter.WSTrustActions Actions { get; }

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x00048341 File Offset: 0x00046541
		internal virtual WSTrustConstantsAdapter.WSTrustAttributeNames Attributes
		{
			get
			{
				if (WSTrustConstantsAdapter.attributeNames == null)
				{
					WSTrustConstantsAdapter.attributeNames = new WSTrustConstantsAdapter.WSTrustAttributeNames();
				}
				return WSTrustConstantsAdapter.attributeNames;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001136 RID: 4406
		internal abstract WSTrustConstantsAdapter.WSTrustComputedKeyAlgorithm ComputedKeyAlgorithm { get; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x00048359 File Offset: 0x00046559
		internal virtual WSTrustConstantsAdapter.WSTrustElementNames Elements
		{
			get
			{
				if (WSTrustConstantsAdapter.elementNames == null)
				{
					WSTrustConstantsAdapter.elementNames = new WSTrustConstantsAdapter.WSTrustElementNames();
				}
				return WSTrustConstantsAdapter.elementNames;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x00048371 File Offset: 0x00046571
		internal virtual WSTrustConstantsAdapter.FaultCodeValues FaultCodes
		{
			get
			{
				if (WSTrustConstantsAdapter.faultCodes == null)
				{
					WSTrustConstantsAdapter.faultCodes = new WSTrustConstantsAdapter.FaultCodeValues();
				}
				return WSTrustConstantsAdapter.faultCodes;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001139 RID: 4409
		internal abstract WSTrustConstantsAdapter.WSTrustRequestTypes RequestTypes { get; }

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600113A RID: 4410
		internal abstract WSTrustConstantsAdapter.WSTrustKeyTypes KeyTypes { get; }

		// Token: 0x0600113B RID: 4411 RVA: 0x00048389 File Offset: 0x00046589
		internal static WSTrustConstantsAdapter GetConstantsAdapter(string ns)
		{
			if (StringComparer.Ordinal.Equals(ns, "http://schemas.xmlsoap.org/ws/2005/02/trust"))
			{
				return WSTrustConstantsAdapter.TrustFeb2005;
			}
			if (StringComparer.Ordinal.Equals(ns, "http://docs.oasis-open.org/ws-sx/ws-trust/200512"))
			{
				return WSTrustConstantsAdapter.Trust13;
			}
			return null;
		}

		// Token: 0x04000EB5 RID: 3765
		private static WSTrustConstantsAdapter.WSTrustAttributeNames attributeNames;

		// Token: 0x04000EB6 RID: 3766
		private static WSTrustConstantsAdapter.WSTrustElementNames elementNames;

		// Token: 0x04000EB7 RID: 3767
		private static WSTrustConstantsAdapter.FaultCodeValues faultCodes;

		// Token: 0x04000EB8 RID: 3768
		private string namespaceURI;

		// Token: 0x04000EB9 RID: 3769
		private string prefix;

		// Token: 0x020002BC RID: 700
		internal abstract class WSTrustActions
		{
			// Token: 0x17000585 RID: 1413
			// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00054283 File Offset: 0x00052483
			// (set) Token: 0x060013D5 RID: 5077 RVA: 0x0005428B File Offset: 0x0005248B
			internal string Cancel { get; set; }

			// Token: 0x17000586 RID: 1414
			// (get) Token: 0x060013D6 RID: 5078 RVA: 0x00054294 File Offset: 0x00052494
			// (set) Token: 0x060013D7 RID: 5079 RVA: 0x0005429C File Offset: 0x0005249C
			internal string CancelResponse { get; set; }

			// Token: 0x17000587 RID: 1415
			// (get) Token: 0x060013D8 RID: 5080 RVA: 0x000542A5 File Offset: 0x000524A5
			// (set) Token: 0x060013D9 RID: 5081 RVA: 0x000542AD File Offset: 0x000524AD
			internal string Issue { get; set; }

			// Token: 0x17000588 RID: 1416
			// (get) Token: 0x060013DA RID: 5082 RVA: 0x000542B6 File Offset: 0x000524B6
			// (set) Token: 0x060013DB RID: 5083 RVA: 0x000542BE File Offset: 0x000524BE
			internal string IssueResponse { get; set; }

			// Token: 0x17000589 RID: 1417
			// (get) Token: 0x060013DC RID: 5084 RVA: 0x000542C7 File Offset: 0x000524C7
			// (set) Token: 0x060013DD RID: 5085 RVA: 0x000542CF File Offset: 0x000524CF
			internal string Renew { get; set; }

			// Token: 0x1700058A RID: 1418
			// (get) Token: 0x060013DE RID: 5086 RVA: 0x000542D8 File Offset: 0x000524D8
			// (set) Token: 0x060013DF RID: 5087 RVA: 0x000542E0 File Offset: 0x000524E0
			internal string RenewResponse { get; set; }

			// Token: 0x1700058B RID: 1419
			// (get) Token: 0x060013E0 RID: 5088 RVA: 0x000542E9 File Offset: 0x000524E9
			// (set) Token: 0x060013E1 RID: 5089 RVA: 0x000542F1 File Offset: 0x000524F1
			internal string RequestSecurityContextToken { get; set; }

			// Token: 0x1700058C RID: 1420
			// (get) Token: 0x060013E2 RID: 5090 RVA: 0x000542FA File Offset: 0x000524FA
			// (set) Token: 0x060013E3 RID: 5091 RVA: 0x00054302 File Offset: 0x00052502
			internal string RequestSecurityContextTokenCancel { get; set; }

			// Token: 0x1700058D RID: 1421
			// (get) Token: 0x060013E4 RID: 5092 RVA: 0x0005430B File Offset: 0x0005250B
			// (set) Token: 0x060013E5 RID: 5093 RVA: 0x00054313 File Offset: 0x00052513
			internal string RequestSecurityContextTokenResponse { get; set; }

			// Token: 0x1700058E RID: 1422
			// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0005431C File Offset: 0x0005251C
			// (set) Token: 0x060013E7 RID: 5095 RVA: 0x00054324 File Offset: 0x00052524
			internal string RequestSecurityContextTokenResponseCancel { get; set; }

			// Token: 0x1700058F RID: 1423
			// (get) Token: 0x060013E8 RID: 5096 RVA: 0x0005432D File Offset: 0x0005252D
			// (set) Token: 0x060013E9 RID: 5097 RVA: 0x00054335 File Offset: 0x00052535
			internal string Validate { get; set; }

			// Token: 0x17000590 RID: 1424
			// (get) Token: 0x060013EA RID: 5098 RVA: 0x0005433E File Offset: 0x0005253E
			// (set) Token: 0x060013EB RID: 5099 RVA: 0x00054346 File Offset: 0x00052546
			internal string ValidateResponse { get; set; }
		}

		// Token: 0x020002BD RID: 701
		internal class WSTrustAttributeNames
		{
			// Token: 0x17000591 RID: 1425
			// (get) Token: 0x060013ED RID: 5101 RVA: 0x0005434F File Offset: 0x0005254F
			internal string Allow
			{
				get
				{
					return this.allow;
				}
			}

			// Token: 0x17000592 RID: 1426
			// (get) Token: 0x060013EE RID: 5102 RVA: 0x00054357 File Offset: 0x00052557
			internal string Context
			{
				get
				{
					return this.context;
				}
			}

			// Token: 0x17000593 RID: 1427
			// (get) Token: 0x060013EF RID: 5103 RVA: 0x0005435F File Offset: 0x0005255F
			internal string Dialect
			{
				get
				{
					return this.dialect;
				}
			}

			// Token: 0x17000594 RID: 1428
			// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00054367 File Offset: 0x00052567
			internal string EncodingType
			{
				get
				{
					return this.encodingType;
				}
			}

			// Token: 0x17000595 RID: 1429
			// (get) Token: 0x060013F1 RID: 5105 RVA: 0x0005436F File Offset: 0x0005256F
			internal string OK
			{
				get
				{
					return this.oK;
				}
			}

			// Token: 0x17000596 RID: 1430
			// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00054377 File Offset: 0x00052577
			internal string Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17000597 RID: 1431
			// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0005437F File Offset: 0x0005257F
			internal string ValueType
			{
				get
				{
					return this.valueType;
				}
			}

			// Token: 0x040011E0 RID: 4576
			private string allow = "Allow";

			// Token: 0x040011E1 RID: 4577
			private string context = "Context";

			// Token: 0x040011E2 RID: 4578
			private string dialect = "Dialect";

			// Token: 0x040011E3 RID: 4579
			private string encodingType = "EncodingType";

			// Token: 0x040011E4 RID: 4580
			private string oK = "OK";

			// Token: 0x040011E5 RID: 4581
			private string type = "Type";

			// Token: 0x040011E6 RID: 4582
			private string valueType = "ValueType";
		}

		// Token: 0x020002BE RID: 702
		internal abstract class WSTrustComputedKeyAlgorithm
		{
			// Token: 0x17000598 RID: 1432
			// (get) Token: 0x060013F5 RID: 5109 RVA: 0x000543E8 File Offset: 0x000525E8
			// (set) Token: 0x060013F6 RID: 5110 RVA: 0x000543F0 File Offset: 0x000525F0
			internal string Psha1 { get; set; }
		}

		// Token: 0x020002BF RID: 703
		internal class WSTrustElementNames
		{
			// Token: 0x17000599 RID: 1433
			// (get) Token: 0x060013F8 RID: 5112 RVA: 0x000543F9 File Offset: 0x000525F9
			internal string AllowPostdating
			{
				get
				{
					return this.allowPostdating;
				}
			}

			// Token: 0x1700059A RID: 1434
			// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00054401 File Offset: 0x00052601
			internal string AuthenticationType
			{
				get
				{
					return this.authenticationType;
				}
			}

			// Token: 0x1700059B RID: 1435
			// (get) Token: 0x060013FA RID: 5114 RVA: 0x00054409 File Offset: 0x00052609
			internal string BinarySecret
			{
				get
				{
					return this.binarySecret;
				}
			}

			// Token: 0x1700059C RID: 1436
			// (get) Token: 0x060013FB RID: 5115 RVA: 0x00054411 File Offset: 0x00052611
			internal string BinaryExchange
			{
				get
				{
					return this.binaryExchange;
				}
			}

			// Token: 0x1700059D RID: 1437
			// (get) Token: 0x060013FC RID: 5116 RVA: 0x00054419 File Offset: 0x00052619
			internal string CancelTarget
			{
				get
				{
					return this.cancelTarget;
				}
			}

			// Token: 0x1700059E RID: 1438
			// (get) Token: 0x060013FD RID: 5117 RVA: 0x00054421 File Offset: 0x00052621
			internal string Claims
			{
				get
				{
					return this.claims;
				}
			}

			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x060013FE RID: 5118 RVA: 0x00054429 File Offset: 0x00052629
			internal string ComputedKey
			{
				get
				{
					return this.computedKey;
				}
			}

			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x060013FF RID: 5119 RVA: 0x00054431 File Offset: 0x00052631
			internal string ComputedKeyAlgorithm
			{
				get
				{
					return this.computedKeyAlgorithm;
				}
			}

			// Token: 0x170005A1 RID: 1441
			// (get) Token: 0x06001400 RID: 5120 RVA: 0x00054439 File Offset: 0x00052639
			internal string CanonicalizationAlgorithm
			{
				get
				{
					return this.canonicalizationAlgorithm;
				}
			}

			// Token: 0x170005A2 RID: 1442
			// (get) Token: 0x06001401 RID: 5121 RVA: 0x00054441 File Offset: 0x00052641
			internal string Code
			{
				get
				{
					return this.code;
				}
			}

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x06001402 RID: 5122 RVA: 0x00054449 File Offset: 0x00052649
			internal string Delegatable
			{
				get
				{
					return this.delegatable;
				}
			}

			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x06001403 RID: 5123 RVA: 0x00054451 File Offset: 0x00052651
			internal string DelegateTo
			{
				get
				{
					return this.delegateTo;
				}
			}

			// Token: 0x170005A5 RID: 1445
			// (get) Token: 0x06001404 RID: 5124 RVA: 0x00054459 File Offset: 0x00052659
			internal string Encryption
			{
				get
				{
					return this.encryption;
				}
			}

			// Token: 0x170005A6 RID: 1446
			// (get) Token: 0x06001405 RID: 5125 RVA: 0x00054461 File Offset: 0x00052661
			internal string EncryptionAlgorithm
			{
				get
				{
					return this.encryptionAlgorithm;
				}
			}

			// Token: 0x170005A7 RID: 1447
			// (get) Token: 0x06001406 RID: 5126 RVA: 0x00054469 File Offset: 0x00052669
			internal string EncryptWith
			{
				get
				{
					return this.encryptWith;
				}
			}

			// Token: 0x170005A8 RID: 1448
			// (get) Token: 0x06001407 RID: 5127 RVA: 0x00054471 File Offset: 0x00052671
			internal string Entropy
			{
				get
				{
					return this.entropy;
				}
			}

			// Token: 0x170005A9 RID: 1449
			// (get) Token: 0x06001408 RID: 5128 RVA: 0x00054479 File Offset: 0x00052679
			internal string Forwardable
			{
				get
				{
					return this.forwardable;
				}
			}

			// Token: 0x170005AA RID: 1450
			// (get) Token: 0x06001409 RID: 5129 RVA: 0x00054481 File Offset: 0x00052681
			internal string Issuer
			{
				get
				{
					return this.issuer;
				}
			}

			// Token: 0x170005AB RID: 1451
			// (get) Token: 0x0600140A RID: 5130 RVA: 0x00054489 File Offset: 0x00052689
			internal string KeySize
			{
				get
				{
					return this.keySize;
				}
			}

			// Token: 0x170005AC RID: 1452
			// (get) Token: 0x0600140B RID: 5131 RVA: 0x00054491 File Offset: 0x00052691
			internal string KeyType
			{
				get
				{
					return this.keyType;
				}
			}

			// Token: 0x170005AD RID: 1453
			// (get) Token: 0x0600140C RID: 5132 RVA: 0x00054499 File Offset: 0x00052699
			internal string Lifetime
			{
				get
				{
					return this.lifetime;
				}
			}

			// Token: 0x170005AE RID: 1454
			// (get) Token: 0x0600140D RID: 5133 RVA: 0x000544A1 File Offset: 0x000526A1
			internal string OnBehalfOf
			{
				get
				{
					return this.onBehalfOf;
				}
			}

			// Token: 0x170005AF RID: 1455
			// (get) Token: 0x0600140E RID: 5134 RVA: 0x000544A9 File Offset: 0x000526A9
			internal string Participant
			{
				get
				{
					return this.participant;
				}
			}

			// Token: 0x170005B0 RID: 1456
			// (get) Token: 0x0600140F RID: 5135 RVA: 0x000544B1 File Offset: 0x000526B1
			internal string Participants
			{
				get
				{
					return this.participants;
				}
			}

			// Token: 0x170005B1 RID: 1457
			// (get) Token: 0x06001410 RID: 5136 RVA: 0x000544B9 File Offset: 0x000526B9
			internal string Primary
			{
				get
				{
					return this.primary;
				}
			}

			// Token: 0x170005B2 RID: 1458
			// (get) Token: 0x06001411 RID: 5137 RVA: 0x000544C1 File Offset: 0x000526C1
			internal string ProofEncryption
			{
				get
				{
					return this.proofEncryption;
				}
			}

			// Token: 0x170005B3 RID: 1459
			// (get) Token: 0x06001412 RID: 5138 RVA: 0x000544C9 File Offset: 0x000526C9
			internal string Reason
			{
				get
				{
					return this.reason;
				}
			}

			// Token: 0x170005B4 RID: 1460
			// (get) Token: 0x06001413 RID: 5139 RVA: 0x000544D1 File Offset: 0x000526D1
			internal string Renewing
			{
				get
				{
					return this.renewing;
				}
			}

			// Token: 0x170005B5 RID: 1461
			// (get) Token: 0x06001414 RID: 5140 RVA: 0x000544D9 File Offset: 0x000526D9
			internal string RenewTarget
			{
				get
				{
					return this.renewTarget;
				}
			}

			// Token: 0x170005B6 RID: 1462
			// (get) Token: 0x06001415 RID: 5141 RVA: 0x000544E1 File Offset: 0x000526E1
			internal string RequestedAttachedReference
			{
				get
				{
					return this.requestedAttachedReference;
				}
			}

			// Token: 0x170005B7 RID: 1463
			// (get) Token: 0x06001416 RID: 5142 RVA: 0x000544E9 File Offset: 0x000526E9
			internal string RequestedProofToken
			{
				get
				{
					return this.requestedProofToken;
				}
			}

			// Token: 0x170005B8 RID: 1464
			// (get) Token: 0x06001417 RID: 5143 RVA: 0x000544F1 File Offset: 0x000526F1
			internal string RequestedSecurityToken
			{
				get
				{
					return this.requestedSecurityToken;
				}
			}

			// Token: 0x170005B9 RID: 1465
			// (get) Token: 0x06001418 RID: 5144 RVA: 0x000544F9 File Offset: 0x000526F9
			internal string RequestedTokenCancelled
			{
				get
				{
					return this.requestedTokenCancelled;
				}
			}

			// Token: 0x170005BA RID: 1466
			// (get) Token: 0x06001419 RID: 5145 RVA: 0x00054501 File Offset: 0x00052701
			internal string RequestedUnattachedReference
			{
				get
				{
					return this.requestedUnattachedReference;
				}
			}

			// Token: 0x170005BB RID: 1467
			// (get) Token: 0x0600141A RID: 5146 RVA: 0x00054509 File Offset: 0x00052709
			internal string RequestKeySize
			{
				get
				{
					return this.requestKeySize;
				}
			}

			// Token: 0x170005BC RID: 1468
			// (get) Token: 0x0600141B RID: 5147 RVA: 0x00054511 File Offset: 0x00052711
			internal string RequestSecurityToken
			{
				get
				{
					return this.requestSecurityToken;
				}
			}

			// Token: 0x170005BD RID: 1469
			// (get) Token: 0x0600141C RID: 5148 RVA: 0x00054519 File Offset: 0x00052719
			internal string RequestSecurityTokenResponse
			{
				get
				{
					return this.requestSecurityTokenResponse;
				}
			}

			// Token: 0x170005BE RID: 1470
			// (get) Token: 0x0600141D RID: 5149 RVA: 0x00054521 File Offset: 0x00052721
			internal string RequestType
			{
				get
				{
					return this.requestType;
				}
			}

			// Token: 0x170005BF RID: 1471
			// (get) Token: 0x0600141E RID: 5150 RVA: 0x00054529 File Offset: 0x00052729
			internal string SecurityContextToken
			{
				get
				{
					return this.securityContextToken;
				}
			}

			// Token: 0x170005C0 RID: 1472
			// (get) Token: 0x0600141F RID: 5151 RVA: 0x00054531 File Offset: 0x00052731
			internal string SignWith
			{
				get
				{
					return this.signWith;
				}
			}

			// Token: 0x170005C1 RID: 1473
			// (get) Token: 0x06001420 RID: 5152 RVA: 0x00054539 File Offset: 0x00052739
			internal string SignatureAlgorithm
			{
				get
				{
					return this.signatureAlgorithm;
				}
			}

			// Token: 0x170005C2 RID: 1474
			// (get) Token: 0x06001421 RID: 5153 RVA: 0x00054541 File Offset: 0x00052741
			internal string Status
			{
				get
				{
					return this.status;
				}
			}

			// Token: 0x170005C3 RID: 1475
			// (get) Token: 0x06001422 RID: 5154 RVA: 0x00054549 File Offset: 0x00052749
			internal string TokenType
			{
				get
				{
					return this.tokenType;
				}
			}

			// Token: 0x170005C4 RID: 1476
			// (get) Token: 0x06001423 RID: 5155 RVA: 0x00054551 File Offset: 0x00052751
			internal string UseKey
			{
				get
				{
					return this.useKey;
				}
			}

			// Token: 0x040011E8 RID: 4584
			private string allowPostdating = "AllowPostdating";

			// Token: 0x040011E9 RID: 4585
			private string authenticationType = "AuthenticationType";

			// Token: 0x040011EA RID: 4586
			private string binarySecret = "BinarySecret";

			// Token: 0x040011EB RID: 4587
			private string binaryExchange = "BinaryExchange";

			// Token: 0x040011EC RID: 4588
			private string cancelTarget = "CancelTarget";

			// Token: 0x040011ED RID: 4589
			private string claims = "Claims";

			// Token: 0x040011EE RID: 4590
			private string computedKey = "ComputedKey";

			// Token: 0x040011EF RID: 4591
			private string computedKeyAlgorithm = "ComputedKeyAlgorithm";

			// Token: 0x040011F0 RID: 4592
			private string canonicalizationAlgorithm = "CanonicalizationAlgorithm";

			// Token: 0x040011F1 RID: 4593
			private string code = "Code";

			// Token: 0x040011F2 RID: 4594
			private string delegatable = "Delegatable";

			// Token: 0x040011F3 RID: 4595
			private string delegateTo = "DelegateTo";

			// Token: 0x040011F4 RID: 4596
			private string encryption = "Encryption";

			// Token: 0x040011F5 RID: 4597
			private string encryptionAlgorithm = "EncryptionAlgorithm";

			// Token: 0x040011F6 RID: 4598
			private string encryptWith = "EncryptWith";

			// Token: 0x040011F7 RID: 4599
			private string entropy = "Entropy";

			// Token: 0x040011F8 RID: 4600
			private string forwardable = "Forwardable";

			// Token: 0x040011F9 RID: 4601
			private string issuer = "Issuer";

			// Token: 0x040011FA RID: 4602
			private string keySize = "KeySize";

			// Token: 0x040011FB RID: 4603
			private string keyType = "KeyType";

			// Token: 0x040011FC RID: 4604
			private string lifetime = "Lifetime";

			// Token: 0x040011FD RID: 4605
			private string onBehalfOf = "OnBehalfOf";

			// Token: 0x040011FE RID: 4606
			private string participant = "Participant";

			// Token: 0x040011FF RID: 4607
			private string participants = "Participants";

			// Token: 0x04001200 RID: 4608
			private string primary = "Primary";

			// Token: 0x04001201 RID: 4609
			private string proofEncryption = "ProofEncryption";

			// Token: 0x04001202 RID: 4610
			private string reason = "Reason";

			// Token: 0x04001203 RID: 4611
			private string renewing = "Renewing";

			// Token: 0x04001204 RID: 4612
			private string renewTarget = "RenewTarget";

			// Token: 0x04001205 RID: 4613
			private string requestedAttachedReference = "RequestedAttachedReference";

			// Token: 0x04001206 RID: 4614
			private string requestedProofToken = "RequestedProofToken";

			// Token: 0x04001207 RID: 4615
			private string requestedSecurityToken = "RequestedSecurityToken";

			// Token: 0x04001208 RID: 4616
			private string requestedTokenCancelled = "RequestedTokenCancelled";

			// Token: 0x04001209 RID: 4617
			private string requestedUnattachedReference = "RequestedUnattachedReference";

			// Token: 0x0400120A RID: 4618
			private string requestKeySize = "RequestKeySize";

			// Token: 0x0400120B RID: 4619
			private string requestSecurityToken = "RequestSecurityToken";

			// Token: 0x0400120C RID: 4620
			private string requestSecurityTokenResponse = "RequestSecurityTokenResponse";

			// Token: 0x0400120D RID: 4621
			private string requestType = "RequestType";

			// Token: 0x0400120E RID: 4622
			private string securityContextToken = "SecurityContextToken";

			// Token: 0x0400120F RID: 4623
			private string signWith = "SignWith";

			// Token: 0x04001210 RID: 4624
			private string signatureAlgorithm = "SignatureAlgorithm";

			// Token: 0x04001211 RID: 4625
			private string status = "Status";

			// Token: 0x04001212 RID: 4626
			private string tokenType = "TokenType";

			// Token: 0x04001213 RID: 4627
			private string useKey = "UseKey";
		}

		// Token: 0x020002C0 RID: 704
		internal abstract class WSTrustRequestTypes
		{
			// Token: 0x170005C5 RID: 1477
			// (get) Token: 0x06001425 RID: 5157 RVA: 0x00054753 File Offset: 0x00052953
			// (set) Token: 0x06001426 RID: 5158 RVA: 0x0005475B File Offset: 0x0005295B
			internal string Cancel { get; set; }

			// Token: 0x170005C6 RID: 1478
			// (get) Token: 0x06001427 RID: 5159 RVA: 0x00054764 File Offset: 0x00052964
			// (set) Token: 0x06001428 RID: 5160 RVA: 0x0005476C File Offset: 0x0005296C
			internal string Issue { get; set; }

			// Token: 0x170005C7 RID: 1479
			// (get) Token: 0x06001429 RID: 5161 RVA: 0x00054775 File Offset: 0x00052975
			// (set) Token: 0x0600142A RID: 5162 RVA: 0x0005477D File Offset: 0x0005297D
			internal string Renew { get; set; }

			// Token: 0x170005C8 RID: 1480
			// (get) Token: 0x0600142B RID: 5163 RVA: 0x00054786 File Offset: 0x00052986
			// (set) Token: 0x0600142C RID: 5164 RVA: 0x0005478E File Offset: 0x0005298E
			internal string Validate { get; set; }
		}

		// Token: 0x020002C1 RID: 705
		internal abstract class WSTrustKeyTypes
		{
			// Token: 0x170005C9 RID: 1481
			// (get) Token: 0x0600142E RID: 5166 RVA: 0x00054797 File Offset: 0x00052997
			// (set) Token: 0x0600142F RID: 5167 RVA: 0x0005479F File Offset: 0x0005299F
			internal string Asymmetric { get; set; }

			// Token: 0x170005CA RID: 1482
			// (get) Token: 0x06001430 RID: 5168 RVA: 0x000547A8 File Offset: 0x000529A8
			// (set) Token: 0x06001431 RID: 5169 RVA: 0x000547B0 File Offset: 0x000529B0
			internal string Bearer { get; set; }

			// Token: 0x170005CB RID: 1483
			// (get) Token: 0x06001432 RID: 5170 RVA: 0x000547B9 File Offset: 0x000529B9
			// (set) Token: 0x06001433 RID: 5171 RVA: 0x000547C1 File Offset: 0x000529C1
			internal string Symmetric { get; set; }
		}

		// Token: 0x020002C2 RID: 706
		internal class FaultCodeValues
		{
			// Token: 0x170005CC RID: 1484
			// (get) Token: 0x06001435 RID: 5173 RVA: 0x000547CA File Offset: 0x000529CA
			internal string AuthenticationBadElements
			{
				get
				{
					return "AuthenticationBadElements";
				}
			}

			// Token: 0x170005CD RID: 1485
			// (get) Token: 0x06001436 RID: 5174 RVA: 0x000547D1 File Offset: 0x000529D1
			internal string BadRequest
			{
				get
				{
					return "BadRequest";
				}
			}

			// Token: 0x170005CE RID: 1486
			// (get) Token: 0x06001437 RID: 5175 RVA: 0x000547D8 File Offset: 0x000529D8
			internal string ExpiredData
			{
				get
				{
					return "ExpiredData";
				}
			}

			// Token: 0x170005CF RID: 1487
			// (get) Token: 0x06001438 RID: 5176 RVA: 0x000547DF File Offset: 0x000529DF
			internal string FailedAuthentication
			{
				get
				{
					return "FailedAuthentication";
				}
			}

			// Token: 0x170005D0 RID: 1488
			// (get) Token: 0x06001439 RID: 5177 RVA: 0x000547E6 File Offset: 0x000529E6
			internal string InvalidRequest
			{
				get
				{
					return "InvalidRequest";
				}
			}

			// Token: 0x170005D1 RID: 1489
			// (get) Token: 0x0600143A RID: 5178 RVA: 0x000547ED File Offset: 0x000529ED
			internal string InvalidScope
			{
				get
				{
					return "InvalidScope";
				}
			}

			// Token: 0x170005D2 RID: 1490
			// (get) Token: 0x0600143B RID: 5179 RVA: 0x000547F4 File Offset: 0x000529F4
			internal string InvalidSecurityToken
			{
				get
				{
					return "InvalidSecurityToken";
				}
			}

			// Token: 0x170005D3 RID: 1491
			// (get) Token: 0x0600143C RID: 5180 RVA: 0x000547FB File Offset: 0x000529FB
			internal string InvalidTimeRange
			{
				get
				{
					return "InvalidTimeRange";
				}
			}

			// Token: 0x170005D4 RID: 1492
			// (get) Token: 0x0600143D RID: 5181 RVA: 0x00054802 File Offset: 0x00052A02
			internal string RenewNeeded
			{
				get
				{
					return "RenewNeeded";
				}
			}

			// Token: 0x170005D5 RID: 1493
			// (get) Token: 0x0600143E RID: 5182 RVA: 0x00054809 File Offset: 0x00052A09
			internal string RequestFailed
			{
				get
				{
					return "RequestFailed";
				}
			}

			// Token: 0x170005D6 RID: 1494
			// (get) Token: 0x0600143F RID: 5183 RVA: 0x00054810 File Offset: 0x00052A10
			internal string UnableToRenew
			{
				get
				{
					return "UnableToRenew";
				}
			}
		}
	}
}
