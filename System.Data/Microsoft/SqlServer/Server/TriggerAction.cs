using System;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x02000056 RID: 86
	public enum TriggerAction
	{
		// Token: 0x04000650 RID: 1616
		Invalid,
		// Token: 0x04000651 RID: 1617
		Insert,
		// Token: 0x04000652 RID: 1618
		Update,
		// Token: 0x04000653 RID: 1619
		Delete,
		// Token: 0x04000654 RID: 1620
		CreateTable = 21,
		// Token: 0x04000655 RID: 1621
		AlterTable,
		// Token: 0x04000656 RID: 1622
		DropTable,
		// Token: 0x04000657 RID: 1623
		CreateIndex,
		// Token: 0x04000658 RID: 1624
		AlterIndex,
		// Token: 0x04000659 RID: 1625
		DropIndex,
		// Token: 0x0400065A RID: 1626
		CreateSynonym = 34,
		// Token: 0x0400065B RID: 1627
		DropSynonym = 36,
		// Token: 0x0400065C RID: 1628
		CreateSecurityExpression = 31,
		// Token: 0x0400065D RID: 1629
		DropSecurityExpression = 33,
		// Token: 0x0400065E RID: 1630
		CreateView = 41,
		// Token: 0x0400065F RID: 1631
		AlterView,
		// Token: 0x04000660 RID: 1632
		DropView,
		// Token: 0x04000661 RID: 1633
		CreateProcedure = 51,
		// Token: 0x04000662 RID: 1634
		AlterProcedure,
		// Token: 0x04000663 RID: 1635
		DropProcedure,
		// Token: 0x04000664 RID: 1636
		CreateFunction = 61,
		// Token: 0x04000665 RID: 1637
		AlterFunction,
		// Token: 0x04000666 RID: 1638
		DropFunction,
		// Token: 0x04000667 RID: 1639
		CreateTrigger = 71,
		// Token: 0x04000668 RID: 1640
		AlterTrigger,
		// Token: 0x04000669 RID: 1641
		DropTrigger,
		// Token: 0x0400066A RID: 1642
		CreateEventNotification,
		// Token: 0x0400066B RID: 1643
		DropEventNotification = 76,
		// Token: 0x0400066C RID: 1644
		CreateType = 91,
		// Token: 0x0400066D RID: 1645
		DropType = 93,
		// Token: 0x0400066E RID: 1646
		CreateAssembly = 101,
		// Token: 0x0400066F RID: 1647
		AlterAssembly,
		// Token: 0x04000670 RID: 1648
		DropAssembly,
		// Token: 0x04000671 RID: 1649
		CreateUser = 131,
		// Token: 0x04000672 RID: 1650
		AlterUser,
		// Token: 0x04000673 RID: 1651
		DropUser,
		// Token: 0x04000674 RID: 1652
		CreateRole,
		// Token: 0x04000675 RID: 1653
		AlterRole,
		// Token: 0x04000676 RID: 1654
		DropRole,
		// Token: 0x04000677 RID: 1655
		CreateAppRole,
		// Token: 0x04000678 RID: 1656
		AlterAppRole,
		// Token: 0x04000679 RID: 1657
		DropAppRole,
		// Token: 0x0400067A RID: 1658
		CreateSchema = 141,
		// Token: 0x0400067B RID: 1659
		AlterSchema,
		// Token: 0x0400067C RID: 1660
		DropSchema,
		// Token: 0x0400067D RID: 1661
		CreateLogin,
		// Token: 0x0400067E RID: 1662
		AlterLogin,
		// Token: 0x0400067F RID: 1663
		DropLogin,
		// Token: 0x04000680 RID: 1664
		CreateMsgType = 151,
		// Token: 0x04000681 RID: 1665
		DropMsgType = 153,
		// Token: 0x04000682 RID: 1666
		CreateContract,
		// Token: 0x04000683 RID: 1667
		DropContract = 156,
		// Token: 0x04000684 RID: 1668
		CreateQueue,
		// Token: 0x04000685 RID: 1669
		AlterQueue,
		// Token: 0x04000686 RID: 1670
		DropQueue,
		// Token: 0x04000687 RID: 1671
		CreateService = 161,
		// Token: 0x04000688 RID: 1672
		AlterService,
		// Token: 0x04000689 RID: 1673
		DropService,
		// Token: 0x0400068A RID: 1674
		CreateRoute,
		// Token: 0x0400068B RID: 1675
		AlterRoute,
		// Token: 0x0400068C RID: 1676
		DropRoute,
		// Token: 0x0400068D RID: 1677
		GrantStatement,
		// Token: 0x0400068E RID: 1678
		DenyStatement,
		// Token: 0x0400068F RID: 1679
		RevokeStatement,
		// Token: 0x04000690 RID: 1680
		GrantObject,
		// Token: 0x04000691 RID: 1681
		DenyObject,
		// Token: 0x04000692 RID: 1682
		RevokeObject,
		// Token: 0x04000693 RID: 1683
		CreateBinding = 174,
		// Token: 0x04000694 RID: 1684
		AlterBinding,
		// Token: 0x04000695 RID: 1685
		DropBinding,
		// Token: 0x04000696 RID: 1686
		CreatePartitionFunction = 191,
		// Token: 0x04000697 RID: 1687
		AlterPartitionFunction,
		// Token: 0x04000698 RID: 1688
		DropPartitionFunction,
		// Token: 0x04000699 RID: 1689
		CreatePartitionScheme,
		// Token: 0x0400069A RID: 1690
		AlterPartitionScheme,
		// Token: 0x0400069B RID: 1691
		DropPartitionScheme
	}
}
