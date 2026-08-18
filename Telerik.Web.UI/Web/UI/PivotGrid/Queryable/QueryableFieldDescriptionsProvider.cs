using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x02000D6F RID: 3439
	public class QueryableFieldDescriptionsProvider : LocalFieldDescriptionsProviderBase
	{
		// Token: 0x0600803B RID: 32827 RVA: 0x001D51E8 File Offset: 0x001D33E8
		public override void GetDescriptionsDataAsync(object state)
		{
			if (state == null)
			{
				throw new ArgumentNullException("state");
			}
			QueryableDataProvider queryableDataProvider = state as QueryableDataProvider;
			QueryableDataProvider queryableDataProvider2 = base.CurrentState as QueryableDataProvider;
			if (base.CurrentState != state || queryableDataProvider2.Source != queryableDataProvider.Source)
			{
				base.CurrentState = state;
				base.GetDescriptionsDataAsync(state);
			}
		}

		// Token: 0x0600803C RID: 32828 RVA: 0x001D5240 File Offset: 0x001D3440
		protected override IFieldInfoData GenerateDescriptionsData()
		{
			QueryableDataProvider queryableDataProvider = base.CurrentState as QueryableDataProvider;
			List<IPivotFieldInfo> fieldInfos = this.GenerateDescriptions().ToList<IPivotFieldInfo>();
			ContainerNode fieldDescriptionHierarchy = this.GetFieldDescriptionHierarchy(fieldInfos);
			if (queryableDataProvider.CalculatedFields.Count > 0)
			{
				ContainerNode containerNode = new ContainerNode(PivotLocalizationManager.CalculatedFields, ContainerNodeRole.Folder);
				fieldDescriptionHierarchy.Children.Add(containerNode);
				for (int i = 0; i < queryableDataProvider.CalculatedFields.Count; i++)
				{
					CalculatedField calculatedField = queryableDataProvider.CalculatedFields[i];
					CalculatedPivotFieldInfo info = new CalculatedPivotFieldInfo(calculatedField);
					FieldInfoNode item = new FieldInfoNode(info);
					containerNode.Children.Add(item);
				}
			}
			return new FieldInfoData(fieldDescriptionHierarchy);
		}

		// Token: 0x0600803D RID: 32829 RVA: 0x001D52E8 File Offset: 0x001D34E8
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IQueryable", Justification = "IQueryable is known interface.")]
		private IEnumerable<IPivotFieldInfo> GenerateDescriptions()
		{
			QueryableDataProvider queryableDataProvider = base.CurrentState as QueryableDataProvider;
			IQueryable source = queryableDataProvider.Source;
			if (source == null)
			{
				throw new InvalidOperationException("State should be IQueryable");
			}
			List<IPivotFieldInfo> list = new List<IPivotFieldInfo>();
			foreach (PropertyInfo propertyInfo in source.ElementType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				QueryableFieldDescription item = new QueryableFieldDescription(propertyInfo);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x0600803E RID: 32830 RVA: 0x001D5358 File Offset: 0x001D3558
		internal override ContainerNode GenerateDateTimeFolderHierarchy(IPivotFieldInfo fieldInfoItem)
		{
			ContainerNode containerNode = new ContainerNode(fieldInfoItem.DisplayName, ContainerNodeRole.Folder);
			PropertyFieldInfo propertyFieldInfo = fieldInfoItem as PropertyFieldInfo;
			if (propertyFieldInfo != null)
			{
				containerNode.Children.Add(LocalFieldDescriptionsProviderBase.GenerateDateTimeStep(propertyFieldInfo, DateTimeStep.Year, PivotLocalizationManager.YearGroupField, true));
				containerNode.Children.Add(LocalFieldDescriptionsProviderBase.GenerateDateTimeStep(propertyFieldInfo, DateTimeStep.Quarter, PivotLocalizationManager.QuarterGroupField, true));
				containerNode.Children.Add(LocalFieldDescriptionsProviderBase.GenerateDateTimeStep(propertyFieldInfo, DateTimeStep.Month, PivotLocalizationManager.MonthGroupField, true));
				containerNode.Children.Add(LocalFieldDescriptionsProviderBase.GenerateDateTimeStep(propertyFieldInfo, DateTimeStep.Day, PivotLocalizationManager.DayGroupField, true));
				FieldInfoNode item = new FieldInfoNode(propertyFieldInfo);
				containerNode.Children.Add(item);
			}
			return containerNode;
		}
	}
}
