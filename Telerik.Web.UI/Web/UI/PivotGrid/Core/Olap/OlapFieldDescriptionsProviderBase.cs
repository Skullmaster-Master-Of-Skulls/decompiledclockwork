using System;
using System.Diagnostics.CodeAnalysis;
using Telerik.Web.UI.PivotGrid.Core.Fields;
using Telerik.Web.UI.PivotGrid.Core.Internal;
using Telerik.Web.UI.PivotGrid.Core.Olap.NodeBuilders;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D0B RID: 3339
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Will fix soon.")]
	public abstract class OlapFieldDescriptionsProviderBase : FieldDescriptionProviderBase
	{
		// Token: 0x170027B0 RID: 10160
		// (get) Token: 0x06007C62 RID: 31842 RVA: 0x001C96D1 File Offset: 0x001C78D1
		// (set) Token: 0x06007C63 RID: 31843 RVA: 0x001C96D9 File Offset: 0x001C78D9
		internal DescriptionsDataRequestInfo CurrentRequestInfo { get; private set; }

		// Token: 0x170027B1 RID: 10161
		// (get) Token: 0x06007C64 RID: 31844 RVA: 0x001C96E2 File Offset: 0x001C78E2
		// (set) Token: 0x06007C65 RID: 31845 RVA: 0x001C96EA File Offset: 0x001C78EA
		private protected IFieldInfoData Data { protected get; private set; }

		// Token: 0x06007C66 RID: 31846 RVA: 0x001C96F4 File Offset: 0x001C78F4
		private void LoaderDataLoaded(object sender, MetadataLoadedEventsArgs e)
		{
			OlapMetadataLoader olapMetadataLoader = sender as OlapMetadataLoader;
			olapMetadataLoader.DataLoaded -= this.LoaderDataLoaded;
			IFieldInfoData data;
			if (e.Error != null)
			{
				data = new EmptyFieldInfoData();
			}
			else
			{
				this.InitializeFieldInfoData(e.CatalogInfo);
				data = this.Data;
			}
			GetDescriptionsDataCompletedEventArgs args = new GetDescriptionsDataCompletedEventArgs(e.Error, this.CurrentRequestInfo.State, data);
			this.OnDescriptionsDataCompleted(args);
		}

		// Token: 0x06007C67 RID: 31847 RVA: 0x001C9760 File Offset: 0x001C7960
		internal ContainerNode InitializeFromCubeInfo(OlapCubeInfo cube)
		{
			if (cube == null)
			{
				throw new InvalidOperationException("Cannot initialize when cube information is null");
			}
			this.infosHierarchyRoot = ContainerNode.CreateRootNode();
			OlapFieldDescriptionsProviderBase.GenerateDimensionNodes(this.infosHierarchyRoot, cube);
			OlapFieldDescriptionsProviderBase.GenerateKpiNodes(this.infosHierarchyRoot, cube);
			OlapFieldDescriptionsProviderBase.GenerateMeasureNodes(this.infosHierarchyRoot, cube);
			return this.infosHierarchyRoot;
		}

		// Token: 0x06007C68 RID: 31848 RVA: 0x001C97B0 File Offset: 0x001C79B0
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNode.#ctor(System.String,System.String,Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNodeRole)", Justification = "Will fix in the future.")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNode.#ctor(System.String,Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNodeRole)", Justification = "Design choice.")]
		private static void GenerateDimensionNodes(ContainerNode root, OlapCubeInfo cube)
		{
			DimensionNodesBuilder dimensionNodesBuilder = new DimensionNodesBuilder(cube);
			dimensionNodesBuilder.BuildNodes(root);
		}

		// Token: 0x06007C69 RID: 31849 RVA: 0x001C97CC File Offset: 0x001C79CC
		private static void GenerateKpiNodes(ContainerNode root, OlapCubeInfo cube)
		{
			KpiNodesBuilder kpiNodesBuilder = new KpiNodesBuilder(cube);
			kpiNodesBuilder.BuildNodes(root);
		}

		// Token: 0x06007C6A RID: 31850 RVA: 0x001C97E8 File Offset: 0x001C79E8
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNode.#ctor(System.String,System.String,Telerik.Web.UI.PivotGrid.Core.Fields.ContainerNodeRole)", Justification = "Will fix with localization.")]
		private static void GenerateMeasureNodes(ContainerNode root, OlapCubeInfo cube)
		{
			MeasureNodesBuilder measureNodesBuilder = new MeasureNodesBuilder(cube.Measures);
			measureNodesBuilder.BuildNodes(root);
		}

		// Token: 0x06007C6B RID: 31851 RVA: 0x001C9808 File Offset: 0x001C7A08
		internal void InitializeFieldInfoData(OlapCatalogInfo catalogInfo)
		{
			OlapElementsFilterCondition filterCondition = this.CurrentRequestInfo.FilterCondition;
			OlapCubeInfo olapCubeInfo;
			if (string.IsNullOrEmpty(filterCondition.CubeName))
			{
				olapCubeInfo = OlapFieldDescriptionsProviderBase.GetDefaultCube(catalogInfo);
			}
			else
			{
				olapCubeInfo = OlapFieldDescriptionsProviderBase.FindCubeByName(catalogInfo, filterCondition.CubeName);
			}
			if (olapCubeInfo == null)
			{
				olapCubeInfo = new OlapCubeInfo();
			}
			ContainerNode root = this.InitializeFromCubeInfo(olapCubeInfo);
			FieldInfoData data = new FieldInfoData(root);
			this.Data = data;
		}

		// Token: 0x06007C6C RID: 31852 RVA: 0x001C9868 File Offset: 0x001C7A68
		private static OlapCubeInfo FindCubeByName(OlapCatalogInfo catalogInfo, string cubeName)
		{
			OlapCubeInfo result = null;
			foreach (OlapCubeInfo olapCubeInfo in catalogInfo.Cubes)
			{
				if (olapCubeInfo.Name == cubeName)
				{
					result = olapCubeInfo;
					break;
				}
			}
			return result;
		}

		// Token: 0x06007C6D RID: 31853 RVA: 0x001C98C4 File Offset: 0x001C7AC4
		private static OlapCubeInfo GetDefaultCube(OlapCatalogInfo catalogInfo)
		{
			if (catalogInfo.Cubes.Count > 0)
			{
				return catalogInfo.Cubes[0];
			}
			return null;
		}

		// Token: 0x06007C6E RID: 31854 RVA: 0x001C98E4 File Offset: 0x001C7AE4
		public sealed override void GetDescriptionsDataAsync(object state)
		{
			DescriptionsDataRequestInfo descriptionsDataRequestInfo = OlapFieldDescriptionsProviderBase.PrepareRequestInfo(state);
			DescriptionsDataRequestInfo descriptionsDataRequestInfo2 = descriptionsDataRequestInfo;
			object state2 = descriptionsDataRequestInfo.State;
			if (this.ShouldGetData(descriptionsDataRequestInfo2))
			{
				this.CurrentRequestInfo = descriptionsDataRequestInfo2;
				base.IsBusy = true;
				this.ExecuteWorkOnContext();
				return;
			}
			if (!base.IsBusy)
			{
				this.OnDescriptionsDataCompleted(new GetDescriptionsDataCompletedEventArgs(null, state2, this.Data));
			}
		}

		// Token: 0x06007C6F RID: 31855 RVA: 0x001C993C File Offset: 0x001C7B3C
		private void ExecuteWorkOnContext()
		{
			WorkExecutionContext contextForCurrentExecutionStrategy = WorkExecutionContext.GetContextForCurrentExecutionStrategy();
			contextForCurrentExecutionStrategy.ActionToExecute = new Action(this.GetDescriptionsData);
			contextForCurrentExecutionStrategy.Execute();
		}

		// Token: 0x06007C70 RID: 31856 RVA: 0x001C9968 File Offset: 0x001C7B68
		private static DescriptionsDataRequestInfo PrepareRequestInfo(object state)
		{
			DescriptionsDataRequestInfo descriptionsDataRequestInfo = state as DescriptionsDataRequestInfo;
			if (descriptionsDataRequestInfo == null)
			{
				descriptionsDataRequestInfo = new DescriptionsDataRequestInfo(state, default(OlapElementsFilterCondition));
			}
			return descriptionsDataRequestInfo;
		}

		// Token: 0x06007C71 RID: 31857 RVA: 0x001C9990 File Offset: 0x001C7B90
		private bool ShouldGetData(DescriptionsDataRequestInfo newRequestInfo)
		{
			return this.CurrentRequestInfo == null || this.CurrentRequestInfo.State != newRequestInfo.State || this.Data == null;
		}

		// Token: 0x06007C72 RID: 31858
		internal abstract OlapMetadataLoader GetLoader();

		// Token: 0x06007C73 RID: 31859 RVA: 0x001C99BC File Offset: 0x001C7BBC
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		private void GetDescriptionsData()
		{
			OlapMetadataLoader loader = this.GetLoader();
			loader.DataLoaded += this.LoaderDataLoaded;
			try
			{
				loader.LoadData();
			}
			catch (Exception error)
			{
				this.CompleteWithError(error);
			}
		}

		// Token: 0x06007C74 RID: 31860 RVA: 0x001C9A04 File Offset: 0x001C7C04
		private void CompleteWithError(Exception error)
		{
			OlapCommunicationException ex = error as OlapCommunicationException;
			if (ex == null)
			{
				ex = new OlapCommunicationException("Problem with service call", error);
			}
			GetDescriptionsDataCompletedEventArgs args = new GetDescriptionsDataCompletedEventArgs(ex, this.CurrentRequestInfo.State, new EmptyFieldInfoData());
			this.OnDescriptionsDataCompleted(args);
		}

		// Token: 0x04002215 RID: 8725
		private ContainerNode infosHierarchyRoot;
	}
}
