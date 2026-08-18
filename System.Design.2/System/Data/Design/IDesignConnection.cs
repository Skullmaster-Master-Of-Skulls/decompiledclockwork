using System;
using System.CodeDom;
using System.Collections;

namespace System.Data.Design
{
	// Token: 0x02000236 RID: 566
	internal interface IDesignConnection : IDataSourceNamedObject, INamedObject, ICloneable, IDataSourceInitAfterLoading, IDataSourceXmlSpecialOwner
	{
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001539 RID: 5433
		// (set) Token: 0x0600153A RID: 5434
		ConnectionString ConnectionStringObject { get; set; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600153B RID: 5435
		// (set) Token: 0x0600153C RID: 5436
		string ConnectionString { get; set; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600153D RID: 5437
		// (set) Token: 0x0600153E RID: 5438
		string Provider { get; set; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600153F RID: 5439
		// (set) Token: 0x06001540 RID: 5440
		bool IsAppSettingsProperty { get; set; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001541 RID: 5441
		// (set) Token: 0x06001542 RID: 5442
		string AppSettingsObjectName { get; set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001543 RID: 5443
		// (set) Token: 0x06001544 RID: 5444
		CodePropertyReferenceExpression PropertyReference { get; set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001545 RID: 5445
		IDictionary Properties { get; }

		// Token: 0x06001546 RID: 5446
		IDbConnection CreateEmptyDbConnection();
	}
}
