using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000720 RID: 1824
	internal class AdomdDisctinctValuesProvider : DistinctValuesProvider
	{
		// Token: 0x060040BC RID: 16572 RVA: 0x000CC0E8 File Offset: 0x000CA2E8
		internal AdomdDisctinctValuesProvider(IAdomdClient client, AdomdConnectionSettings connectionSettings, OlapHierarchyFieldInfo fieldInfo)
		{
			if (client == null)
			{
				throw new ArgumentNullException("client");
			}
			if (fieldInfo == null)
			{
				throw new ArgumentNullException("fieldInfo");
			}
			this.client = client;
			this.connectionSettings = connectionSettings;
			this.fieldInfo = fieldInfo;
			this.disctinctValues = new List<object>();
		}

		// Token: 0x060040BD RID: 16573 RVA: 0x000CC137 File Offset: 0x000CA337
		public AdomdDisctinctValuesProvider(AdomdConnectionSettings connectionSettings, OlapHierarchyFieldInfo fieldInfo, int setConditionListCapacity) : this(new DefaultAdomdClient(), connectionSettings, fieldInfo)
		{
			this.SetConditionListCapacity = setConditionListCapacity;
		}

		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x060040BE RID: 16574 RVA: 0x000CC14D File Offset: 0x000CA34D
		public override IEnumerable<object> DisctinctValues
		{
			get
			{
				return this.disctinctValues;
			}
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x000CC158 File Offset: 0x000CA358
		public override void Refresh()
		{
			OlapPivotConfiguration pivotConfiguration = new OlapPivotConfiguration();
			string mdxRequest = this.GetMdxRequest(this.fieldInfo.Name);
			AdomdClientRequestInfo requestInfo = new AdomdClientRequestInfo(mdxRequest, this.connectionSettings, pivotConfiguration);
			this.client.SendRequestCompleted += this.ClientSendRequestCompleted;
			this.client.SendRequestAsync(requestInfo);
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x000CC1B0 File Offset: 0x000CA3B0
		private string GetMdxRequest(string name)
		{
			string text = string.Empty;
			if (this.fieldInfo.SupportsMembersFunction)
			{
				text = ".Children";
			}
			return string.Concat(new string[]
			{
				"SELECT {TOPCOUNT(AddCalculatedMembers({",
				name,
				text,
				"}), ",
				this.SetConditionListCapacity.ToString(),
				")} DIMENSION PROPERTIES PARENT_UNIQUE_NAME,HIERARCHY_UNIQUE_NAME ON COLUMNS FROM [",
				this.connectionSettings.Cube,
				"]"
			});
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x000CC230 File Offset: 0x000CA430
		private void ClientSendRequestCompleted(object sender, AdomdClientRequestCompletedEventArgs e)
		{
			this.client.SendRequestCompleted -= this.ClientSendRequestCompleted;
			if (e.Error != null)
			{
				return;
			}
			this.CreateDistinctValuesForValidResponse(e);
			base.OnUpdated();
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x000CC260 File Offset: 0x000CA460
		private void CreateDistinctValuesForValidResponse(AdomdClientRequestCompletedEventArgs e)
		{
			AdomdResponseData adomdResponseData = new AdomdResponseData(e.RequestInfo.PivotConfiguration, e.Result);
			List<object> list = new List<object>();
			if (adomdResponseData.ColumnAxisTuples.Count == 0)
			{
				return;
			}
			foreach (IOlapTuple tuple in adomdResponseData.ColumnAxisTuples)
			{
				MemberDistinctValue distinctValueFromTuple = AdomdDisctinctValuesProvider.GetDistinctValueFromTuple(tuple);
				if (distinctValueFromTuple != null)
				{
					list.Add(distinctValueFromTuple);
				}
			}
			this.disctinctValues = list;
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x000CC2F0 File Offset: 0x000CA4F0
		private static MemberDistinctValue GetDistinctValueFromTuple(IOlapTuple tuple)
		{
			MemberDistinctValue memberDistinctValue = null;
			using (IEnumerator enumerator = tuple.Members.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					IOlapMember olapMember = (IOlapMember)enumerator.Current;
					memberDistinctValue = new MemberDistinctValue(olapMember.UniqueName);
					memberDistinctValue.Caption = olapMember.Caption;
				}
			}
			return memberDistinctValue;
		}

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x060040C4 RID: 16580 RVA: 0x000CC360 File Offset: 0x000CA560
		// (set) Token: 0x060040C5 RID: 16581 RVA: 0x000CC368 File Offset: 0x000CA568
		public int SetConditionListCapacity { get; set; }

		// Token: 0x0400112C RID: 4396
		private readonly AdomdConnectionSettings connectionSettings;

		// Token: 0x0400112D RID: 4397
		private readonly OlapHierarchyFieldInfo fieldInfo;

		// Token: 0x0400112E RID: 4398
		private readonly IAdomdClient client;

		// Token: 0x0400112F RID: 4399
		private IEnumerable<object> disctinctValues;
	}
}
