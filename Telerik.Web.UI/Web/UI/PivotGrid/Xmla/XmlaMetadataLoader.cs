using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D9B RID: 3483
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Design choice.")]
	internal class XmlaMetadataLoader : OlapMetadataLoader
	{
		// Token: 0x060081AC RID: 33196 RVA: 0x001D9199 File Offset: 0x001D7399
		public XmlaMetadataLoader(XmlaConnectionSettings connectionSettings)
		{
			this.connectionSettings = connectionSettings;
			this.requestStack = new Queue<Action>();
		}

		// Token: 0x060081AD RID: 33197 RVA: 0x001D91B4 File Offset: 0x001D73B4
		public override void LoadData()
		{
			lock (this)
			{
				if (!this.IsLoading())
				{
					this.StartLoading();
				}
			}
		}

		// Token: 0x060081AE RID: 33198 RVA: 0x001D91FC File Offset: 0x001D73FC
		private void TimeExpired(object state)
		{
			lock (this)
			{
				if (this.IsLoading())
				{
					OlapCatalogInfo catalogInfo = new OlapCatalogInfo();
					OlapCommunicationException error = null;
					if (this.IsReady())
					{
						catalogInfo = this.GetInfoFromMetadata();
					}
					else
					{
						error = new OlapCommunicationException("Metadata was not returned in time.");
					}
					this.EndLoading();
					this.OnDataLoaded(new MetadataLoadedEventsArgs(catalogInfo, error));
				}
			}
		}

		// Token: 0x060081AF RID: 33199 RVA: 0x001D9270 File Offset: 0x001D7470
		private bool IsLoading()
		{
			return this.timeoutTimer != null;
		}

		// Token: 0x060081B0 RID: 33200 RVA: 0x001D9280 File Offset: 0x001D7480
		private void StartLoading()
		{
			this.info = new MultiDimensionalInfo();
			this.EnqueueRequests();
			this.dataSetsToLoad = this.requestStack.Count;
			this.timeoutTimer = new Timer(new TimerCallback(this.TimeExpired), null, 20000, -1);
			this.ExecuteNextRequestInQueue();
		}

		// Token: 0x060081B1 RID: 33201 RVA: 0x001D92D4 File Offset: 0x001D74D4
		private void EnqueueRequests()
		{
			this.requestStack.Enqueue(new Action(this.GetDimentionsAsync));
			this.requestStack.Enqueue(new Action(this.GetHierarchiesAsync));
			this.requestStack.Enqueue(new Action(this.GetLevelsAsync));
			this.requestStack.Enqueue(new Action(this.GetMeasureGroupsAsync));
			this.requestStack.Enqueue(new Action(this.GetMeasuresAsync));
			this.requestStack.Enqueue(new Action(this.GetNamedSetsAsync));
			this.requestStack.Enqueue(new Action(this.GetKpisAsync));
		}

		// Token: 0x060081B2 RID: 33202 RVA: 0x001D9384 File Offset: 0x001D7584
		private void ExecuteNextRequestInQueue()
		{
			if (this.requestStack.Count > 0)
			{
				Action action = this.requestStack.Dequeue();
				action();
			}
		}

		// Token: 0x060081B3 RID: 33203 RVA: 0x001D93B4 File Offset: 0x001D75B4
		private void EndLoading()
		{
			Timer timer = this.timeoutTimer;
			this.timeoutTimer = null;
			timer.Dispose();
		}

		// Token: 0x060081B4 RID: 33204 RVA: 0x001D93D5 File Offset: 0x001D75D5
		private bool IsReady()
		{
			return this.dataSetsToLoad == 0;
		}

		// Token: 0x060081B5 RID: 33205 RVA: 0x001D93E0 File Offset: 0x001D75E0
		private void RaiseCompletedOrContinueLoading()
		{
			lock (this)
			{
				if (this.IsReady() && this.IsLoading())
				{
					OlapCatalogInfo infoFromMetadata = this.GetInfoFromMetadata();
					OlapCommunicationException error = null;
					this.EndLoading();
					this.OnDataLoaded(new MetadataLoadedEventsArgs(infoFromMetadata, error));
				}
				else
				{
					this.ExecuteNextRequestInQueue();
				}
			}
		}

		// Token: 0x060081B6 RID: 33206 RVA: 0x001D944C File Offset: 0x001D764C
		private void RaiseCompletedWithError(OlapCommunicationException error)
		{
			lock (this)
			{
				if (this.IsLoading())
				{
					OlapCatalogInfo catalogInfo = new OlapCatalogInfo();
					this.EndLoading();
					this.OnDataLoaded(new MetadataLoadedEventsArgs(catalogInfo, error));
				}
			}
		}

		// Token: 0x060081B7 RID: 33207 RVA: 0x001D94A4 File Offset: 0x001D76A4
		private OlapCatalogInfo GetInfoFromMetadata()
		{
			OlapCatalogInfo olapCatalogInfo = new OlapCatalogInfo();
			OlapCubeInfo olapCubeInfo = new OlapCubeInfo();
			olapCatalogInfo.Name = "MyCatalog";
			olapCatalogInfo.Cubes.Add(olapCubeInfo);
			olapCubeInfo.Name = this.connectionSettings.Cube;
			this.GenerateDimensions(olapCubeInfo);
			this.GenerateMeasures(olapCubeInfo);
			this.GenerateNamedSets(olapCubeInfo);
			this.GenerateKpis(olapCubeInfo);
			return olapCatalogInfo;
		}

		// Token: 0x060081B8 RID: 33208 RVA: 0x001D9504 File Offset: 0x001D7704
		private void GenerateDimensions(OlapCubeInfo cubeInfo)
		{
			foreach (DimensionSchemaElement dimensionItem in this.info.Dimensions)
			{
				DimensionSchemaElement dimensionSchemaElement = XmlaMetadataLoader.CreateDimensionInfo(dimensionItem);
				cubeInfo.Dimensions.Add(dimensionSchemaElement);
				this.GenerateHierarchiesForDimension(dimensionItem, dimensionSchemaElement);
			}
		}

		// Token: 0x060081B9 RID: 33209 RVA: 0x001D956C File Offset: 0x001D776C
		private void GenerateMeasures(OlapCubeInfo cubeInfo)
		{
			foreach (MeasureSchemaElement measureItem in this.info.Measures)
			{
				MeasureSchemaElement item = XmlaMetadataLoader.CreateMeasureInfo(measureItem);
				cubeInfo.Measures.Add(item);
			}
		}

		// Token: 0x060081BA RID: 33210 RVA: 0x001D95CC File Offset: 0x001D77CC
		private void GenerateNamedSets(OlapCubeInfo cubeInfo)
		{
			foreach (NamedSetSchemaElement item in this.info.Sets)
			{
				cubeInfo.NamedSets.Add(item);
			}
		}

		// Token: 0x060081BB RID: 33211 RVA: 0x001D9624 File Offset: 0x001D7824
		private void GenerateKpis(OlapCubeInfo cubeInfo)
		{
			foreach (KpiSchemaElement item in this.info.Kpis)
			{
				cubeInfo.Kpis.Add(item);
			}
		}

		// Token: 0x060081BC RID: 33212 RVA: 0x001D969C File Offset: 0x001D789C
		private void GenerateHierarchiesForDimension(DimensionSchemaElement dimensionItem, DimensionSchemaElement newDimensionInfo)
		{
			List<HierarchySchemaElement> list = (from hi in this.info.DimensionHierarchies
			where hi.DimensionUniqueName == dimensionItem.UniqueName
			select hi).ToList<HierarchySchemaElement>();
			foreach (HierarchySchemaElement hierarchyItem in list)
			{
				HierarchySchemaElement hierarchySchemaElement = XmlaMetadataLoader.CreateHierarchyInfo(hierarchyItem);
				newDimensionInfo.Hierarchies.Add(hierarchySchemaElement);
				this.GenerateLevelsForHierarchy(dimensionItem, hierarchyItem, hierarchySchemaElement);
			}
		}

		// Token: 0x060081BD RID: 33213 RVA: 0x001D976C File Offset: 0x001D796C
		private void GenerateLevelsForHierarchy(DimensionSchemaElement dimensionItem, HierarchySchemaElement hierarchyItem, HierarchySchemaElement newHierarchyItem)
		{
			List<LevelSchemaElement> list = (from li in this.info.DimensionLevels
			where li.DimensionUniqueName == dimensionItem.UniqueName
			where li.HierarchyUniqueName == hierarchyItem.UniqueName
			select li).ToList<LevelSchemaElement>();
			foreach (LevelSchemaElement levelItem in list)
			{
				LevelSchemaElement item = XmlaMetadataLoader.CreateLevelInfo(levelItem);
				newHierarchyItem.Levels.Add(item);
			}
		}

		// Token: 0x060081BE RID: 33214 RVA: 0x001D9810 File Offset: 0x001D7A10
		private static MeasureSchemaElement CreateMeasureInfo(MeasureSchemaElement measureItem)
		{
			return new MeasureSchemaElement
			{
				GroupCaption = measureItem.GroupCaption,
				Caption = measureItem.Caption,
				CatalogName = measureItem.CatalogName,
				CubeName = measureItem.CubeName,
				GroupName = measureItem.GroupName,
				Name = measureItem.Name,
				UniqueName = measureItem.UniqueName
			};
		}

		// Token: 0x060081BF RID: 33215 RVA: 0x001D9878 File Offset: 0x001D7A78
		private static LevelSchemaElement CreateLevelInfo(LevelSchemaElement levelItem)
		{
			return new LevelSchemaElement
			{
				Caption = levelItem.Caption,
				CatalogName = levelItem.CatalogName,
				CubeName = levelItem.CubeName,
				DimensionUniqueName = levelItem.DimensionUniqueName,
				HierarchyUniqueName = levelItem.HierarchyUniqueName,
				Name = levelItem.Name,
				UniqueName = levelItem.UniqueName
			};
		}

		// Token: 0x060081C0 RID: 33216 RVA: 0x001D98E0 File Offset: 0x001D7AE0
		private static HierarchySchemaElement CreateHierarchyInfo(HierarchySchemaElement hierarchyItem)
		{
			return new HierarchySchemaElement
			{
				AllMemberName = hierarchyItem.AllMemberName,
				Caption = hierarchyItem.Caption,
				CatalogName = hierarchyItem.CatalogName,
				CubeName = hierarchyItem.CubeName,
				DefaultMember = hierarchyItem.DefaultMember,
				DimensionUniqueName = hierarchyItem.DimensionUniqueName,
				DisplayFolder = hierarchyItem.DisplayFolder,
				Grouping = hierarchyItem.Grouping,
				Name = hierarchyItem.Name,
				UniqueName = hierarchyItem.UniqueName,
				ViewType = hierarchyItem.ViewType
			};
		}

		// Token: 0x060081C1 RID: 33217 RVA: 0x001D9978 File Offset: 0x001D7B78
		private static DimensionSchemaElement CreateDimensionInfo(DimensionSchemaElement dimensionItem)
		{
			return new DimensionSchemaElement
			{
				Caption = dimensionItem.Caption,
				CatalogName = dimensionItem.CatalogName,
				CubeName = dimensionItem.CubeName,
				Name = dimensionItem.Name,
				UniqueName = dimensionItem.UniqueName
			};
		}

		// Token: 0x060081C2 RID: 33218 RVA: 0x001D99C8 File Offset: 0x001D7BC8
		private void UnsubscribeFromClientSender(object sender)
		{
			XmlaWebClient xmlaWebClient = sender as XmlaWebClient;
			xmlaWebClient.SendRequestCompleted -= this.DimensionsClientRequestCompleted;
		}

		// Token: 0x060081C3 RID: 33219 RVA: 0x001D99F0 File Offset: 0x001D7BF0
		private string CreateDiscoverMethod(string requestType)
		{
			XmlaMethodDiscover xmlaMethodDiscover = new XmlaMethodDiscover(requestType);
			xmlaMethodDiscover.AddProperty(XmlaProperties.Catalog(this.connectionSettings.Database));
			xmlaMethodDiscover.AddRestiction(new XmlaRestrictionProperty
			{
				Name = "CUBE_NAME",
				Value = this.connectionSettings.Cube
			});
			xmlaMethodDiscover.MergeProperties(this.connectionSettings.QueryProperties);
			return xmlaMethodDiscover.ToXml();
		}

		// Token: 0x060081C4 RID: 33220 RVA: 0x001D9A5C File Offset: 0x001D7C5C
		private void GetDimentionsAsync()
		{
			string xmlaRequest = this.CreateDiscoverMethod("MDSCHEMA_DIMENSIONS");
			XmlaClientRequestInfo requestInfo = new XmlaClientRequestInfo(xmlaRequest, this.connectionSettings, null);
			XmlaWebClient xmlaWebClient = new XmlaWebClient();
			xmlaWebClient.SendRequestCompleted += this.DimensionsClientRequestCompleted;
			xmlaWebClient.SendRequestAsync(requestInfo);
		}

		// Token: 0x060081C5 RID: 33221 RVA: 0x001D9AA4 File Offset: 0x001D7CA4
		private void DimensionsClientRequestCompleted(object sender, XmlaClientRequestCompletedEventArgs e)
		{
			this.UnsubscribeFromClientSender(sender);
			OlapCommunicationException soapError = XmlaWebClient.GetSoapError(e);
			if (soapError != null)
			{
				this.RaiseCompletedWithError(soapError);
				return;
			}
			this.ParseDimensionsResult(e.Result);
			this.dataSetsToLoad--;
			this.RaiseCompletedOrContinueLoading();
		}

		// Token: 0x060081C6 RID: 33222 RVA: 0x001D9AEC File Offset: 0x001D7CEC
		private void ParseDimensionsResult(string result)
		{
			XmlaMdSchemaDimensionsReader xmlaMdSchemaDimensionsReader = new XmlaMdSchemaDimensionsReader(result);
			IEnumerable<DimensionSchemaElement> dimensions = xmlaMdSchemaDimensionsReader.Dimensions;
			foreach (DimensionSchemaElement item in dimensions)
			{
				this.info.Dimensions.Add(item);
			}
		}

		// Token: 0x060081C7 RID: 33223 RVA: 0x001D9B4C File Offset: 0x001D7D4C
		private void GetHierarchiesAsync()
		{
			string xmlaRequest = this.CreateDiscoverMethod("MDSCHEMA_HIERARCHIES");
			XmlaClientRequestInfo requestInfo = new XmlaClientRequestInfo(xmlaRequest, this.connectionSettings, null);
			XmlaWebClient xmlaWebClient = new XmlaWebClient();
			xmlaWebClient.SendRequestCompleted += this.HierarchiesClientRequestCompleted;
			xmlaWebClient.SendRequestAsync(requestInfo);
		}

		// Token: 0x060081C8 RID: 33224 RVA: 0x001D9B94 File Offset: 0x001D7D94
		private void HierarchiesClientRequestCompleted(object sender, XmlaClientRequestCompletedEventArgs e)
		{
			this.UnsubscribeFromClientSender(sender);
			OlapCommunicationException soapError = XmlaWebClient.GetSoapError(e);
			if (soapError != null)
			{
				this.RaiseCompletedWithError(soapError);
				return;
			}
			this.ParseHierarchiesResult(e.Result);
			this.dataSetsToLoad--;
			this.RaiseCompletedOrContinueLoading();
		}

		// Token: 0x060081C9 RID: 33225 RVA: 0x001D9BDC File Offset: 0x001D7DDC
		private void ParseHierarchiesResult(string result)
		{
			XmlaMdSchemaHierarchiesReader xmlaMdSchemaHierarchiesReader = new XmlaMdSchemaHierarchiesReader(result);
			IEnumerable<HierarchySchemaElement> hierarchies = xmlaMdSchemaHierarchiesReader.Hierarchies;
			foreach (HierarchySchemaElement item in hierarchies)
			{
				this.info.DimensionHierarchies.Add(item);
			}
		}

		// Token: 0x060081CA RID: 33226 RVA: 0x001D9C3C File Offset: 0x001D7E3C
		private void GetLevelsAsync()
		{
			string xmlaRequest = this.CreateDiscoverMethod("MDSCHEMA_LEVELS");
			XmlaClientRequestInfo requestInfo = new XmlaClientRequestInfo(xmlaRequest, this.connectionSettings, null);
			XmlaWebClient xmlaWebClient = new XmlaWebClient();
			xmlaWebClient.SendRequestCompleted += this.LevelsClientRequestCompleted;
			xmlaWebClient.SendRequestAsync(requestInfo);
		}

		// Token: 0x060081CB RID: 33227 RVA: 0x001D9C84 File Offset: 0x001D7E84
		private void LevelsClientRequestCompleted(object sender, XmlaClientRequestCompletedEventArgs e)
		{
			this.UnsubscribeFromClientSender(sender);
			OlapCommunicationException soapError = XmlaWebClient.GetSoapError(e);
			if (soapError != null)
			{
				this.RaiseCompletedWithError(soapError);
				return;
			}
			this.ParseLevelsResult(e.Result);
			this.dataSetsToLoad--;
			this.RaiseCompletedOrContinueLoading();
		}

		// Token: 0x060081CC RID: 33228 RVA: 0x001D9CCC File Offset: 0x001D7ECC
		private void ParseLevelsResult(string result)
		{
			XmlaMdSchemaLevelsReader xmlaMdSchemaLevelsReader = new XmlaMdSchemaLevelsReader(result);
			IEnumerable<LevelSchemaElement> levels = xmlaMdSchemaLevelsReader.Levels;
			foreach (LevelSchemaElement item in levels)
			{
				this.info.DimensionLevels.Add(item);
			}
		}

		// Token: 0x060081CD RID: 33229 RVA: 0x001D9D2C File Offset: 0x001D7F2C
		private void GetMeasureGroupsAsync()
		{
			string xmlaRequest = this.CreateDiscoverMethod("MDSCHEMA_MEASUREGROUPS");
			XmlaClientRequestInfo requestInfo = new XmlaClientRequestInfo(xmlaRequest, this.connectionSettings, null);
			XmlaWebClient xmlaWebClient = new XmlaWebClient();
			xmlaWebClient.SendRequestCompleted += this.MeasureGroupsClientRequestCompleted;
			xmlaWebClient.SendRequestAsync(requestInfo);
		}

		// Token: 0x060081CE RID: 33230 RVA: 0x001D9D74 File Offset: 0x001D7F74
		private void GetMeasuresAsync()
		{
			string xmlaRequest = this.CreateDiscoverMethod("MDSCHEMA_MEASURES");
			XmlaClientRequestInfo requestInfo = new XmlaClientRequestInfo(xmlaRequest, this.connectionSettings, null);
			XmlaWebClient xmlaWebClient = new XmlaWebClient();
			xmlaWebClient.SendRequestCompleted += this.MeasuresClientRequestCompleted;
			xmlaWebClient.SendRequestAsync(requestInfo);
		}

		// Token: 0x060081CF RID: 33231 RVA: 0x001D9DBC File Offset: 0x001D7FBC
		private void MeasureGroupsClientRequestCompleted(object sender, XmlaClientRequestCompletedEventArgs e)
		{
			this.UnsubscribeFromClientSender(sender);
			if (XmlaWebClient.GetSoapError(e) == null)
			{
				this.ParseMeasureGroupsResult(e.Result);
			}
			this.dataSetsToLoad--;
			this.RaiseCompletedOrContinueLoading();
		}

		// Token: 0x060081D0 RID: 33232 RVA: 0x001D9DFC File Offset: 0x001D7FFC
		private void ParseMeasureGroupsResult(string result)
		{
			XmlaMdSchemaMeasureGroupsReader xmlaMdSchemaMeasureGroupsReader = new XmlaMdSchemaMeasureGroupsReader(result);
			IEnumerable<MeasureGroupSchemaElement> measureGroups = xmlaMdSchemaMeasureGroupsReader.MeasureGroups;
			foreach (MeasureGroupSchemaElement item in measureGroups)
			{
				this.info.MeasureGroups.Add(item);
			}
		}

		// Token: 0x060081D1 RID: 33233 RVA: 0x001D9E5C File Offset: 0x001D805C
		private void MeasuresClientRequestCompleted(object sender, XmlaClientRequestCompletedEventArgs e)
		{
			this.UnsubscribeFromClientSender(sender);
			OlapCommunicationException soapError = XmlaWebClient.GetSoapError(e);
			if (soapError != null)
			{
				this.RaiseCompletedWithError(soapError);
				return;
			}
			this.ParseMeasuresResult(e.Result);
			this.dataSetsToLoad--;
			this.RaiseCompletedOrContinueLoading();
		}

		// Token: 0x060081D2 RID: 33234 RVA: 0x001D9EC4 File Offset: 0x001D80C4
		private void ParseMeasuresResult(string result)
		{
			XmlaMdSchemaMeasuresReader xmlaMdSchemaMeasuresReader = new XmlaMdSchemaMeasuresReader(result);
			IEnumerable<MeasureSchemaElement> measures = xmlaMdSchemaMeasuresReader.Measures;
			using (IEnumerator<MeasureSchemaElement> enumerator = measures.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MeasureSchemaElement item = enumerator.Current;
					MeasureGroupSchemaElement measureGroupSchemaElement = this.info.MeasureGroups.FirstOrDefault((MeasureGroupSchemaElement p) => p.Name == item.GroupName);
					if (measureGroupSchemaElement != null)
					{
						item.GroupCaption = measureGroupSchemaElement.Caption;
					}
					else
					{
						item.GroupCaption = item.GroupName;
					}
					this.info.Measures.Add(item);
				}
			}
		}

		// Token: 0x060081D3 RID: 33235 RVA: 0x001D9F98 File Offset: 0x001D8198
		private void GetNamedSetsAsync()
		{
			string xmlaRequest = this.CreateDiscoverMethod("MDSCHEMA_SETS");
			XmlaClientRequestInfo requestInfo = new XmlaClientRequestInfo(xmlaRequest, this.connectionSettings, null);
			XmlaWebClient xmlaWebClient = new XmlaWebClient();
			xmlaWebClient.SendRequestCompleted += this.NamedSetsClientRequestCompleted;
			xmlaWebClient.SendRequestAsync(requestInfo);
		}

		// Token: 0x060081D4 RID: 33236 RVA: 0x001D9FE0 File Offset: 0x001D81E0
		private void NamedSetsClientRequestCompleted(object sender, XmlaClientRequestCompletedEventArgs e)
		{
			this.UnsubscribeFromClientSender(sender);
			if (XmlaWebClient.GetSoapError(e) == null)
			{
				this.ParseNamedSetsResult(e.Result);
			}
			this.dataSetsToLoad--;
			this.RaiseCompletedOrContinueLoading();
		}

		// Token: 0x060081D5 RID: 33237 RVA: 0x001DA020 File Offset: 0x001D8220
		private void ParseNamedSetsResult(string result)
		{
			XmlaMdSchemaSetsReader xmlaMdSchemaSetsReader = new XmlaMdSchemaSetsReader(result);
			IEnumerable<NamedSetSchemaElement> sets = xmlaMdSchemaSetsReader.Sets;
			foreach (NamedSetSchemaElement item in sets)
			{
				this.info.Sets.Add(item);
			}
		}

		// Token: 0x060081D6 RID: 33238 RVA: 0x001DA080 File Offset: 0x001D8280
		private void GetKpisAsync()
		{
			string xmlaRequest = this.CreateDiscoverMethod("MDSCHEMA_KPIS");
			XmlaClientRequestInfo requestInfo = new XmlaClientRequestInfo(xmlaRequest, this.connectionSettings, null);
			XmlaWebClient xmlaWebClient = new XmlaWebClient();
			xmlaWebClient.SendRequestCompleted += this.KpisClientRequestCompleted;
			xmlaWebClient.SendRequestAsync(requestInfo);
		}

		// Token: 0x060081D7 RID: 33239 RVA: 0x001DA0C8 File Offset: 0x001D82C8
		private void KpisClientRequestCompleted(object sender, XmlaClientRequestCompletedEventArgs e)
		{
			this.UnsubscribeFromClientSender(sender);
			if (XmlaWebClient.GetSoapError(e) == null)
			{
				this.ParseKpisResult(e.Result);
			}
			this.dataSetsToLoad--;
			this.RaiseCompletedOrContinueLoading();
		}

		// Token: 0x060081D8 RID: 33240 RVA: 0x001DA108 File Offset: 0x001D8308
		private void ParseKpisResult(string result)
		{
			XmlaMdSchemaKpisReader xmlaMdSchemaKpisReader = new XmlaMdSchemaKpisReader(result);
			IEnumerable<KpiSchemaElement> kpis = xmlaMdSchemaKpisReader.Kpis;
			foreach (KpiSchemaElement item in kpis)
			{
				this.info.Kpis.Add(item);
			}
		}

		// Token: 0x040023C5 RID: 9157
		private readonly XmlaConnectionSettings connectionSettings;

		// Token: 0x040023C6 RID: 9158
		private MultiDimensionalInfo info;

		// Token: 0x040023C7 RID: 9159
		private Timer timeoutTimer;

		// Token: 0x040023C8 RID: 9160
		private int dataSetsToLoad;

		// Token: 0x040023C9 RID: 9161
		private Queue<Action> requestStack;
	}
}
