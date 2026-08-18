using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.AnalysisServices.AdomdClient;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D59 RID: 3417
	internal class AdomdMetadataLoader : OlapMetadataLoader
	{
		// Token: 0x06007F7F RID: 32639 RVA: 0x001D1F35 File Offset: 0x001D0135
		public AdomdMetadataLoader(AdomdConnectionSettings connectionSettings)
		{
			this.connectionSettings = connectionSettings;
		}

		// Token: 0x06007F80 RID: 32640 RVA: 0x001D1F44 File Offset: 0x001D0144
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		public override void LoadData()
		{
			OlapCatalogInfo catalogInfo = AdomdMetadataLoader.GetEmptyCatalogInfo();
			OlapCommunicationException error = null;
			try
			{
				catalogInfo = this.ConnectAndGetMetadata();
			}
			catch (AdomdException innerException)
			{
				error = new OlapCommunicationException("Adomd communication error", innerException);
			}
			this.OnDataLoaded(new MetadataLoadedEventsArgs(catalogInfo, error));
		}

		// Token: 0x06007F81 RID: 32641 RVA: 0x001D1F90 File Offset: 0x001D0190
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private static OlapCatalogInfo GetEmptyCatalogInfo()
		{
			return new OlapCatalogInfo
			{
				Description = "Empty catalog.",
				Name = "EmptyCatalog"
			};
		}

		// Token: 0x06007F82 RID: 32642 RVA: 0x001D1FBC File Offset: 0x001D01BC
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private OlapCatalogInfo ConnectAndGetMetadata()
		{
			OlapCatalogInfo olapCatalogInfo = new OlapCatalogInfo();
			using (AdomdConnection adomdConnection = new AdomdConnection(this.connectionSettings.ConnectionString))
			{
				adomdConnection.Open();
				AdomdMetadataLoader.measureGroupsCaptions = this.GetMeasureGroupsAndCaptions();
				this.PrepareMetadata(adomdConnection, olapCatalogInfo);
				adomdConnection.Close();
			}
			return olapCatalogInfo;
		}

		// Token: 0x06007F83 RID: 32643 RVA: 0x001D2020 File Offset: 0x001D0220
		private IDictionary<string, string> GetMeasureGroupsAndCaptions()
		{
			string mdxQuery = string.Format(CultureInfo.InvariantCulture, "SELECT DISTINCT MEASUREGROUP_NAME, MEASUREGROUP_CAPTION FROM $system.MDSCHEMA_MEASUREGROUPS WHERE CUBE_NAME ='{0}'", new object[]
			{
				this.connectionSettings.Cube
			});
			return DefaultAdomdClient.GetMeasureGroupsAndCaptions(this.connectionSettings.ConnectionString, mdxQuery);
		}

		// Token: 0x06007F84 RID: 32644 RVA: 0x001D206C File Offset: 0x001D026C
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private void PrepareMetadata(AdomdConnection cubeConnection, OlapCatalogInfo catalogInfo)
		{
			foreach (CubeDef cubeDef in cubeConnection.Cubes)
			{
				if (cubeDef.Type == 1 && cubeDef.Name == this.connectionSettings.Cube)
				{
					OlapCubeInfo cubeInfo = AdomdMetadataLoader.GetCubeInfo(cubeDef);
					catalogInfo.Cubes.Add(cubeInfo);
				}
			}
		}

		// Token: 0x06007F85 RID: 32645 RVA: 0x001D20D0 File Offset: 0x001D02D0
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private static OlapCubeInfo GetCubeInfo(CubeDef cubeItem)
		{
			OlapCubeInfo olapCubeInfo = new OlapCubeInfo();
			olapCubeInfo.Caption = cubeItem.Caption;
			olapCubeInfo.Name = cubeItem.Name;
			AdomdMetadataLoader.InitializeDimensions(cubeItem, olapCubeInfo);
			AdomdMetadataLoader.InitializeMeasures(cubeItem, olapCubeInfo);
			AdomdMetadataLoader.InitializeNamedSets(cubeItem, olapCubeInfo);
			AdomdMetadataLoader.InitializeKpis(cubeItem, olapCubeInfo);
			return olapCubeInfo;
		}

		// Token: 0x06007F86 RID: 32646 RVA: 0x001D2118 File Offset: 0x001D0318
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private static void InitializeDimensions(CubeDef cube, OlapCubeInfo cubeInfo)
		{
			foreach (Dimension dimension in cube.Dimensions)
			{
				DimensionSchemaElement dimensionSchemaElement = AdomdMetadataLoader.CreateDimensionInfo(dimension);
				cubeInfo.Dimensions.Add(dimensionSchemaElement);
				foreach (Hierarchy hierarchy in dimension.Hierarchies)
				{
					HierarchySchemaElement hierarchySchemaElement = AdomdMetadataLoader.CreateHierarchyInfo(hierarchy, dimension);
					dimensionSchemaElement.Hierarchies.Add(hierarchySchemaElement);
					foreach (Level levelItem in hierarchy.Levels)
					{
						LevelSchemaElement item = AdomdMetadataLoader.CreateLevelInfo(levelItem, dimension, hierarchy);
						hierarchySchemaElement.Levels.Add(item);
					}
				}
			}
		}

		// Token: 0x06007F87 RID: 32647 RVA: 0x001D21CC File Offset: 0x001D03CC
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private static void InitializeMeasures(CubeDef cube, OlapCubeInfo cubeInfo)
		{
			foreach (Measure measureItem in cube.Measures)
			{
				MeasureSchemaElement item = AdomdMetadataLoader.CreateMeasureInfo(measureItem);
				cubeInfo.Measures.Add(item);
			}
		}

		// Token: 0x06007F88 RID: 32648 RVA: 0x001D220C File Offset: 0x001D040C
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private static void InitializeNamedSets(CubeDef cube, OlapCubeInfo cubeInfo)
		{
			foreach (NamedSet namedSetItem in cube.NamedSets)
			{
				NamedSetSchemaElement item = AdomdMetadataLoader.CreateNamedSetInfo(namedSetItem);
				cubeInfo.NamedSets.Add(item);
			}
		}

		// Token: 0x06007F89 RID: 32649 RVA: 0x001D224C File Offset: 0x001D044C
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private static void InitializeKpis(CubeDef cube, OlapCubeInfo cubeInfo)
		{
			foreach (Kpi kpi in cube.Kpis)
			{
				KpiSchemaElement item = AdomdMetadataLoader.CreateKpisInfo(kpi);
				cubeInfo.Kpis.Add(item);
			}
		}

		// Token: 0x06007F8A RID: 32650 RVA: 0x001D228C File Offset: 0x001D048C
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private static DimensionSchemaElement CreateDimensionInfo(Dimension dimensionItem)
		{
			return new DimensionSchemaElement
			{
				Caption = dimensionItem.Caption,
				CatalogName = AdomdMetadataLoader.TryGetPropertyValueAsString(dimensionItem.Properties, "CATALOG_NAME"),
				CubeName = AdomdMetadataLoader.TryGetPropertyValueAsString(dimensionItem.Properties, "CUBE_NAME"),
				Name = dimensionItem.Name,
				UniqueName = dimensionItem.UniqueName
			};
		}

		// Token: 0x06007F8B RID: 32651 RVA: 0x001D22F0 File Offset: 0x001D04F0
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private static HierarchySchemaElement CreateHierarchyInfo(Hierarchy hierarchyItem, Dimension dimensionItem)
		{
			return new HierarchySchemaElement
			{
				Caption = hierarchyItem.Caption,
				CatalogName = AdomdMetadataLoader.TryGetPropertyValueAsString(hierarchyItem.Properties, "CATALOG_NAME"),
				CubeName = AdomdMetadataLoader.TryGetPropertyValueAsString(hierarchyItem.Properties, "CUBE_NAME"),
				Name = hierarchyItem.Name,
				UniqueName = hierarchyItem.UniqueName,
				DimensionUniqueName = dimensionItem.UniqueName,
				DisplayFolder = hierarchyItem.DisplayFolder,
				Grouping = DimensionHierarchyGroupingBehavior.Unknown,
				ViewType = DimensionHierarchyInstanceSelection.Unknown,
				AllMemberName = AdomdMetadataLoader.TryGetPropertyValueAsString(hierarchyItem.Properties, "ALL_MEMBER")
			};
		}

		// Token: 0x06007F8C RID: 32652 RVA: 0x001D2390 File Offset: 0x001D0590
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private static LevelSchemaElement CreateLevelInfo(Level levelItem, Dimension dimensionItem, Hierarchy hierarchyItem)
		{
			return new LevelSchemaElement
			{
				Caption = levelItem.Caption,
				CatalogName = AdomdMetadataLoader.TryGetPropertyValueAsString(levelItem.Properties, "CATALOG_NAME"),
				CubeName = AdomdMetadataLoader.TryGetPropertyValueAsString(levelItem.Properties, "CUBE_NAME"),
				Name = levelItem.Name,
				UniqueName = levelItem.UniqueName,
				DimensionUniqueName = dimensionItem.UniqueName,
				HierarchyUniqueName = hierarchyItem.UniqueName
			};
		}

		// Token: 0x06007F8D RID: 32653 RVA: 0x001D240C File Offset: 0x001D060C
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private static NamedSetSchemaElement CreateNamedSetInfo(NamedSet namedSetItem)
		{
			return new NamedSetSchemaElement
			{
				Caption = namedSetItem.Caption,
				Name = namedSetItem.Name,
				Dimensions = AdomdMetadataLoader.TryGetPropertyValueAsString(namedSetItem.Properties, "DIMENSIONS"),
				CatalogName = AdomdMetadataLoader.TryGetPropertyValueAsString(namedSetItem.Properties, "CATALOG_NAME"),
				CubeName = AdomdMetadataLoader.TryGetPropertyValueAsString(namedSetItem.Properties, "CUBE_NAME")
			};
		}

		// Token: 0x06007F8E RID: 32654 RVA: 0x001D247C File Offset: 0x001D067C
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private static KpiSchemaElement CreateKpisInfo(Kpi kpi)
		{
			return new KpiSchemaElement
			{
				Caption = kpi.Caption,
				DisplayFolder = kpi.DisplayFolder,
				Name = kpi.Name,
				StatusGraphic = kpi.StatusGraphic,
				TrendGraphic = kpi.TrendGraphic,
				CatalogName = AdomdMetadataLoader.TryGetPropertyValueAsString(kpi.Properties, "CATALOG_NAME"),
				CubeName = AdomdMetadataLoader.TryGetPropertyValueAsString(kpi.Properties, "CUBE_NAME"),
				GoalMemberUniqueName = AdomdMetadataLoader.TryGetPropertyValueAsString(kpi.Properties, "KPI_GOAL"),
				StatusMemberUniqueName = AdomdMetadataLoader.TryGetPropertyValueAsString(kpi.Properties, "KPI_STATUS"),
				TrendMemberUniqueName = AdomdMetadataLoader.TryGetPropertyValueAsString(kpi.Properties, "KPI_TREND"),
				ValueMemberUniqueName = AdomdMetadataLoader.TryGetPropertyValueAsString(kpi.Properties, "KPI_VALUE")
			};
		}

		// Token: 0x06007F8F RID: 32655 RVA: 0x001D2550 File Offset: 0x001D0750
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private static MeasureSchemaElement CreateMeasureInfo(Measure measureItem)
		{
			MeasureSchemaElement measureSchemaElement = new MeasureSchemaElement();
			string empty = string.Empty;
			measureSchemaElement.Caption = measureItem.Caption;
			measureSchemaElement.CatalogName = AdomdMetadataLoader.TryGetPropertyValueAsString(measureItem.Properties, "CATALOG_NAME");
			measureSchemaElement.CubeName = AdomdMetadataLoader.TryGetPropertyValueAsString(measureItem.Properties, "CUBE_NAME");
			measureSchemaElement.Name = measureItem.Name;
			measureSchemaElement.UniqueName = measureItem.UniqueName;
			measureSchemaElement.DisplayFolder = measureItem.DisplayFolder;
			measureSchemaElement.GroupName = AdomdMetadataLoader.TryGetPropertyValueAsString(measureItem.Properties, "MEASUREGROUP_NAME");
			measureSchemaElement.DataTypeNumber = AdomdMetadataLoader.TryGetPropertyValueAsInt(measureItem.Properties, "DATA_TYPE");
			if (measureSchemaElement.GroupName != null && AdomdMetadataLoader.measureGroupsCaptions != null && AdomdMetadataLoader.measureGroupsCaptions.TryGetValue(measureSchemaElement.GroupName, out empty))
			{
				measureSchemaElement.GroupCaption = empty;
			}
			else
			{
				measureSchemaElement.GroupCaption = measureSchemaElement.GroupName;
			}
			return measureSchemaElement;
		}

		// Token: 0x06007F90 RID: 32656 RVA: 0x001D262C File Offset: 0x001D082C
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private static string TryGetPropertyValueAsString(PropertyCollection properties, string name)
		{
			Property property = AdomdMetadataLoader.FindPropertyByName(properties, name);
			if (property != null && property.Value != null)
			{
				return property.Value.ToString();
			}
			return null;
		}

		// Token: 0x06007F91 RID: 32657 RVA: 0x001D2660 File Offset: 0x001D0860
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private static int TryGetPropertyValueAsInt(PropertyCollection properties, string name)
		{
			Property property = AdomdMetadataLoader.FindPropertyByName(properties, name);
			if (property != null && property.Value != null)
			{
				return int.Parse(property.Value.ToString(), CultureInfo.InvariantCulture);
			}
			return 0;
		}

		// Token: 0x06007F92 RID: 32658 RVA: 0x001D26A0 File Offset: 0x001D08A0
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Justification = "Design choice.")]
		private static Property FindPropertyByName(PropertyCollection properties, string name)
		{
			foreach (Property property in properties)
			{
				if (property.Name == name)
				{
					return property;
				}
			}
			return null;
		}

		// Token: 0x04002319 RID: 8985
		private static IDictionary<string, string> measureGroupsCaptions;

		// Token: 0x0400231A RID: 8986
		private readonly AdomdConnectionSettings connectionSettings;
	}
}
