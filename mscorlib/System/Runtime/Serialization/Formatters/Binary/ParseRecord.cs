using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007EB RID: 2027
	internal sealed class ParseRecord
	{
		// Token: 0x060047A3 RID: 18339 RVA: 0x000F5950 File Offset: 0x000F4950
		internal ParseRecord()
		{
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x000F5958 File Offset: 0x000F4958
		internal void Init()
		{
			this.PRparseTypeEnum = InternalParseTypeE.Empty;
			this.PRobjectTypeEnum = InternalObjectTypeE.Empty;
			this.PRarrayTypeEnum = InternalArrayTypeE.Empty;
			this.PRmemberTypeEnum = InternalMemberTypeE.Empty;
			this.PRmemberValueEnum = InternalMemberValueE.Empty;
			this.PRobjectPositionEnum = InternalObjectPositionE.Empty;
			this.PRname = null;
			this.PRvalue = null;
			this.PRkeyDt = null;
			this.PRdtType = null;
			this.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
			this.PRisEnum = false;
			this.PRobjectId = 0L;
			this.PRidRef = 0L;
			this.PRarrayElementTypeString = null;
			this.PRarrayElementType = null;
			this.PRisArrayVariant = false;
			this.PRarrayElementTypeCode = InternalPrimitiveTypeE.Invalid;
			this.PRrank = 0;
			this.PRlengthA = null;
			this.PRpositionA = null;
			this.PRlowerBoundA = null;
			this.PRupperBoundA = null;
			this.PRindexMap = null;
			this.PRmemberIndex = 0;
			this.PRlinearlength = 0;
			this.PRrectangularMap = null;
			this.PRisLowerBound = false;
			this.PRtopId = 0L;
			this.PRheaderId = 0L;
			this.PRisValueTypeFixup = false;
			this.PRnewObj = null;
			this.PRobjectA = null;
			this.PRprimitiveArray = null;
			this.PRobjectInfo = null;
			this.PRisRegistered = false;
			this.PRmemberData = null;
			this.PRsi = null;
			this.PRnullCount = 0;
		}

		// Token: 0x0400245A RID: 9306
		internal static int parseRecordIdCount = 1;

		// Token: 0x0400245B RID: 9307
		internal int PRparseRecordId;

		// Token: 0x0400245C RID: 9308
		internal InternalParseTypeE PRparseTypeEnum;

		// Token: 0x0400245D RID: 9309
		internal InternalObjectTypeE PRobjectTypeEnum;

		// Token: 0x0400245E RID: 9310
		internal InternalArrayTypeE PRarrayTypeEnum;

		// Token: 0x0400245F RID: 9311
		internal InternalMemberTypeE PRmemberTypeEnum;

		// Token: 0x04002460 RID: 9312
		internal InternalMemberValueE PRmemberValueEnum;

		// Token: 0x04002461 RID: 9313
		internal InternalObjectPositionE PRobjectPositionEnum;

		// Token: 0x04002462 RID: 9314
		internal string PRname;

		// Token: 0x04002463 RID: 9315
		internal string PRvalue;

		// Token: 0x04002464 RID: 9316
		internal object PRvarValue;

		// Token: 0x04002465 RID: 9317
		internal string PRkeyDt;

		// Token: 0x04002466 RID: 9318
		internal Type PRdtType;

		// Token: 0x04002467 RID: 9319
		internal InternalPrimitiveTypeE PRdtTypeCode;

		// Token: 0x04002468 RID: 9320
		internal bool PRisVariant;

		// Token: 0x04002469 RID: 9321
		internal bool PRisEnum;

		// Token: 0x0400246A RID: 9322
		internal long PRobjectId;

		// Token: 0x0400246B RID: 9323
		internal long PRidRef;

		// Token: 0x0400246C RID: 9324
		internal string PRarrayElementTypeString;

		// Token: 0x0400246D RID: 9325
		internal Type PRarrayElementType;

		// Token: 0x0400246E RID: 9326
		internal bool PRisArrayVariant;

		// Token: 0x0400246F RID: 9327
		internal InternalPrimitiveTypeE PRarrayElementTypeCode;

		// Token: 0x04002470 RID: 9328
		internal int PRrank;

		// Token: 0x04002471 RID: 9329
		internal int[] PRlengthA;

		// Token: 0x04002472 RID: 9330
		internal int[] PRpositionA;

		// Token: 0x04002473 RID: 9331
		internal int[] PRlowerBoundA;

		// Token: 0x04002474 RID: 9332
		internal int[] PRupperBoundA;

		// Token: 0x04002475 RID: 9333
		internal int[] PRindexMap;

		// Token: 0x04002476 RID: 9334
		internal int PRmemberIndex;

		// Token: 0x04002477 RID: 9335
		internal int PRlinearlength;

		// Token: 0x04002478 RID: 9336
		internal int[] PRrectangularMap;

		// Token: 0x04002479 RID: 9337
		internal bool PRisLowerBound;

		// Token: 0x0400247A RID: 9338
		internal long PRtopId;

		// Token: 0x0400247B RID: 9339
		internal long PRheaderId;

		// Token: 0x0400247C RID: 9340
		internal ReadObjectInfo PRobjectInfo;

		// Token: 0x0400247D RID: 9341
		internal bool PRisValueTypeFixup;

		// Token: 0x0400247E RID: 9342
		internal object PRnewObj;

		// Token: 0x0400247F RID: 9343
		internal object[] PRobjectA;

		// Token: 0x04002480 RID: 9344
		internal PrimitiveArray PRprimitiveArray;

		// Token: 0x04002481 RID: 9345
		internal bool PRisRegistered;

		// Token: 0x04002482 RID: 9346
		internal object[] PRmemberData;

		// Token: 0x04002483 RID: 9347
		internal SerializationInfo PRsi;

		// Token: 0x04002484 RID: 9348
		internal int PRnullCount;
	}
}
