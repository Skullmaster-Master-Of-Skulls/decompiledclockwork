using System;
using System.Data.Entity.Core.Query.PlanCompiler;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020000D3 RID: 211
	internal abstract class BasicOpVisitorOfT<TResultType>
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x00023C20 File Offset: 0x00021E20
		protected virtual void VisitChildren(Node n)
		{
			for (int i = 0; i < n.Children.Count; i++)
			{
				this.VisitNode(n.Children[i]);
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00023C58 File Offset: 0x00021E58
		protected virtual void VisitChildrenReverse(Node n)
		{
			for (int i = n.Children.Count - 1; i >= 0; i--)
			{
				this.VisitNode(n.Children[i]);
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00023C90 File Offset: 0x00021E90
		internal TResultType VisitNode(Node n)
		{
			return n.Op.Accept<TResultType>(this, n);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00023CA0 File Offset: 0x00021EA0
		protected virtual TResultType VisitDefault(Node n)
		{
			this.VisitChildren(n);
			return default(TResultType);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00023CC0 File Offset: 0x00021EC0
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		internal virtual TResultType Unimplemented(Node n)
		{
			PlanCompiler.Assert(false, "Not implemented op type");
			return default(TResultType);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00023CE1 File Offset: 0x00021EE1
		public virtual TResultType Visit(Op op, Node n)
		{
			return this.Unimplemented(n);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00023CEA File Offset: 0x00021EEA
		protected virtual TResultType VisitAncillaryOpDefault(AncillaryOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00023CF3 File Offset: 0x00021EF3
		public virtual TResultType Visit(VarDefOp op, Node n)
		{
			return this.VisitAncillaryOpDefault(op, n);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00023CFD File Offset: 0x00021EFD
		public virtual TResultType Visit(VarDefListOp op, Node n)
		{
			return this.VisitAncillaryOpDefault(op, n);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00023D07 File Offset: 0x00021F07
		protected virtual TResultType VisitPhysicalOpDefault(PhysicalOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00023D10 File Offset: 0x00021F10
		public virtual TResultType Visit(PhysicalProjectOp op, Node n)
		{
			return this.VisitPhysicalOpDefault(op, n);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00023D1A File Offset: 0x00021F1A
		protected virtual TResultType VisitNestOp(NestBaseOp op, Node n)
		{
			return this.VisitPhysicalOpDefault(op, n);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00023D24 File Offset: 0x00021F24
		public virtual TResultType Visit(SingleStreamNestOp op, Node n)
		{
			return this.VisitNestOp(op, n);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00023D2E File Offset: 0x00021F2E
		public virtual TResultType Visit(MultiStreamNestOp op, Node n)
		{
			return this.VisitNestOp(op, n);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00023D38 File Offset: 0x00021F38
		protected virtual TResultType VisitRelOpDefault(RelOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00023D41 File Offset: 0x00021F41
		protected virtual TResultType VisitApplyOp(ApplyBaseOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00023D4B File Offset: 0x00021F4B
		public virtual TResultType Visit(CrossApplyOp op, Node n)
		{
			return this.VisitApplyOp(op, n);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00023D55 File Offset: 0x00021F55
		public virtual TResultType Visit(OuterApplyOp op, Node n)
		{
			return this.VisitApplyOp(op, n);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00023D5F File Offset: 0x00021F5F
		protected virtual TResultType VisitJoinOp(JoinBaseOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00023D69 File Offset: 0x00021F69
		public virtual TResultType Visit(CrossJoinOp op, Node n)
		{
			return this.VisitJoinOp(op, n);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00023D73 File Offset: 0x00021F73
		public virtual TResultType Visit(FullOuterJoinOp op, Node n)
		{
			return this.VisitJoinOp(op, n);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00023D7D File Offset: 0x00021F7D
		public virtual TResultType Visit(LeftOuterJoinOp op, Node n)
		{
			return this.VisitJoinOp(op, n);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00023D87 File Offset: 0x00021F87
		public virtual TResultType Visit(InnerJoinOp op, Node n)
		{
			return this.VisitJoinOp(op, n);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00023D91 File Offset: 0x00021F91
		protected virtual TResultType VisitSetOp(SetOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00023D9B File Offset: 0x00021F9B
		public virtual TResultType Visit(ExceptOp op, Node n)
		{
			return this.VisitSetOp(op, n);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00023DA5 File Offset: 0x00021FA5
		public virtual TResultType Visit(IntersectOp op, Node n)
		{
			return this.VisitSetOp(op, n);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00023DAF File Offset: 0x00021FAF
		public virtual TResultType Visit(UnionAllOp op, Node n)
		{
			return this.VisitSetOp(op, n);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00023DB9 File Offset: 0x00021FB9
		public virtual TResultType Visit(DistinctOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00023DC3 File Offset: 0x00021FC3
		public virtual TResultType Visit(FilterOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00023DCD File Offset: 0x00021FCD
		protected virtual TResultType VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00023DD7 File Offset: 0x00021FD7
		public virtual TResultType Visit(GroupByOp op, Node n)
		{
			return this.VisitGroupByOp(op, n);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00023DE1 File Offset: 0x00021FE1
		public virtual TResultType Visit(GroupByIntoOp op, Node n)
		{
			return this.VisitGroupByOp(op, n);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00023DEB File Offset: 0x00021FEB
		public virtual TResultType Visit(ProjectOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00023DF5 File Offset: 0x00021FF5
		protected virtual TResultType VisitTableOp(ScanTableBaseOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00023DFF File Offset: 0x00021FFF
		public virtual TResultType Visit(ScanTableOp op, Node n)
		{
			return this.VisitTableOp(op, n);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00023E09 File Offset: 0x00022009
		public virtual TResultType Visit(ScanViewOp op, Node n)
		{
			return this.VisitTableOp(op, n);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00023E13 File Offset: 0x00022013
		public virtual TResultType Visit(SingleRowOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00023E1D File Offset: 0x0002201D
		public virtual TResultType Visit(SingleRowTableOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00023E27 File Offset: 0x00022027
		protected virtual TResultType VisitSortOp(SortBaseOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00023E31 File Offset: 0x00022031
		public virtual TResultType Visit(SortOp op, Node n)
		{
			return this.VisitSortOp(op, n);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00023E3B File Offset: 0x0002203B
		public virtual TResultType Visit(ConstrainedSortOp op, Node n)
		{
			return this.VisitSortOp(op, n);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00023E45 File Offset: 0x00022045
		public virtual TResultType Visit(UnnestOp op, Node n)
		{
			return this.VisitRelOpDefault(op, n);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00023E4F File Offset: 0x0002204F
		protected virtual TResultType VisitScalarOpDefault(ScalarOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00023E58 File Offset: 0x00022058
		protected virtual TResultType VisitConstantOp(ConstantBaseOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00023E62 File Offset: 0x00022062
		public virtual TResultType Visit(AggregateOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00023E6C File Offset: 0x0002206C
		public virtual TResultType Visit(ArithmeticOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00023E76 File Offset: 0x00022076
		public virtual TResultType Visit(CaseOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00023E80 File Offset: 0x00022080
		public virtual TResultType Visit(CastOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00023E8A File Offset: 0x0002208A
		public virtual TResultType Visit(SoftCastOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00023E94 File Offset: 0x00022094
		public virtual TResultType Visit(CollectOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00023E9E File Offset: 0x0002209E
		public virtual TResultType Visit(ComparisonOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00023EA8 File Offset: 0x000220A8
		public virtual TResultType Visit(ConditionalOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00023EB2 File Offset: 0x000220B2
		public virtual TResultType Visit(ConstantOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00023EBC File Offset: 0x000220BC
		public virtual TResultType Visit(ConstantPredicateOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00023EC6 File Offset: 0x000220C6
		public virtual TResultType Visit(ElementOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00023ED0 File Offset: 0x000220D0
		public virtual TResultType Visit(ExistsOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00023EDA File Offset: 0x000220DA
		public virtual TResultType Visit(FunctionOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00023EE4 File Offset: 0x000220E4
		public virtual TResultType Visit(GetEntityRefOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00023EEE File Offset: 0x000220EE
		public virtual TResultType Visit(GetRefKeyOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00023EF8 File Offset: 0x000220F8
		public virtual TResultType Visit(InternalConstantOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00023F02 File Offset: 0x00022102
		public virtual TResultType Visit(IsOfOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00023F0C File Offset: 0x0002210C
		public virtual TResultType Visit(LikeOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00023F16 File Offset: 0x00022116
		public virtual TResultType Visit(NewEntityOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00023F20 File Offset: 0x00022120
		public virtual TResultType Visit(NewInstanceOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00023F2A File Offset: 0x0002212A
		public virtual TResultType Visit(DiscriminatedNewEntityOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00023F34 File Offset: 0x00022134
		public virtual TResultType Visit(NewMultisetOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00023F3E File Offset: 0x0002213E
		public virtual TResultType Visit(NewRecordOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00023F48 File Offset: 0x00022148
		public virtual TResultType Visit(NullOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00023F52 File Offset: 0x00022152
		public virtual TResultType Visit(NullSentinelOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00023F5C File Offset: 0x0002215C
		public virtual TResultType Visit(PropertyOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00023F66 File Offset: 0x00022166
		public virtual TResultType Visit(RelPropertyOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00023F70 File Offset: 0x00022170
		public virtual TResultType Visit(RefOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00023F7A File Offset: 0x0002217A
		public virtual TResultType Visit(TreatOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00023F84 File Offset: 0x00022184
		public virtual TResultType Visit(VarRefOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00023F8E File Offset: 0x0002218E
		public virtual TResultType Visit(DerefOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00023F98 File Offset: 0x00022198
		public virtual TResultType Visit(NavigateOp op, Node n)
		{
			return this.VisitScalarOpDefault(op, n);
		}
	}
}
