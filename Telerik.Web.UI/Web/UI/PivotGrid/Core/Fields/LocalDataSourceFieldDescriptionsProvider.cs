using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CBA RID: 3258
	public class LocalDataSourceFieldDescriptionsProvider : LocalFieldDescriptionsProviderBase
	{
		// Token: 0x1400012D RID: 301
		// (add) Token: 0x060079E2 RID: 31202 RVA: 0x001BF8CC File Offset: 0x001BDACC
		// (remove) Token: 0x060079E3 RID: 31203 RVA: 0x001BF904 File Offset: 0x001BDB04
		public event EventHandler<ContainerNodeEventArgs> AddingContainerNode;

		// Token: 0x060079E4 RID: 31204 RVA: 0x001BF93C File Offset: 0x001BDB3C
		public override void GetDescriptionsDataAsync(object state)
		{
			if (state == null)
			{
				throw new ArgumentNullException("state");
			}
			LocalDataSourceProvider localDataSourceProvider = state as LocalDataSourceProvider;
			LocalDataSourceProvider localDataSourceProvider2 = base.CurrentState as LocalDataSourceProvider;
			if (base.CurrentState != state || localDataSourceProvider2.ItemsSource != localDataSourceProvider.ItemsSource)
			{
				base.CurrentState = state;
				base.GetDescriptionsDataAsync(state);
			}
		}

		// Token: 0x060079E5 RID: 31205 RVA: 0x001BF994 File Offset: 0x001BDB94
		protected override IFieldInfoData GenerateDescriptionsData()
		{
			LocalDataSourceProvider localDataSourceProvider = base.CurrentState as LocalDataSourceProvider;
			object itemsSource = (localDataSourceProvider != null) ? localDataSourceProvider.ItemsSource : base.CurrentState;
			IFieldInfoExtractor concreteExtractor = LocalDataSourceFieldDescriptionsProvider.GetConcreteExtractor(itemsSource);
			List<IPivotFieldInfo> list = this.GetDescriptions(concreteExtractor).ToList<IPivotFieldInfo>();
			ContainerNode containerNode = ContainerNode.CreateRootNode();
			ContainerNode fieldDescriptionHierarchy = this.GetFieldDescriptionHierarchy(list);
			if (this.AddingContainerNode != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					IPivotFieldInfo info = list[i];
					ContainerNode containerNode2 = fieldDescriptionHierarchy.Children[i];
					ContainerNodeEventArgs containerNodeEventArgs = new ContainerNodeEventArgs(containerNode2, info);
					this.AddingContainerNode(this, containerNodeEventArgs);
					if (!containerNodeEventArgs.Cancel && containerNodeEventArgs.ContainerNode != null)
					{
						containerNode.Children.Add(containerNodeEventArgs.ContainerNode);
					}
				}
			}
			else
			{
				containerNode = fieldDescriptionHierarchy;
			}
			if (localDataSourceProvider != null && localDataSourceProvider.CalculatedFields.Count > 0)
			{
				ContainerNode containerNode3 = new ContainerNode(PivotLocalizationManager.CalculatedFields, ContainerNodeRole.Folder);
				containerNode.Children.Add(containerNode3);
				for (int j = 0; j < localDataSourceProvider.CalculatedFields.Count; j++)
				{
					CalculatedField calculatedField = localDataSourceProvider.CalculatedFields[j];
					CalculatedPivotFieldInfo info2 = new CalculatedPivotFieldInfo(calculatedField);
					FieldInfoNode item = new FieldInfoNode(info2);
					containerNode3.Children.Add(item);
				}
			}
			return new FieldInfoData(containerNode);
		}

		// Token: 0x060079E6 RID: 31206 RVA: 0x001BFADE File Offset: 0x001BDCDE
		protected virtual IEnumerable<IPivotFieldInfo> GetDescriptions(IFieldInfoExtractor getter)
		{
			return getter.GetDescriptions();
		}

		// Token: 0x060079E7 RID: 31207 RVA: 0x001BFAE8 File Offset: 0x001BDCE8
		private static IFieldInfoExtractor GetConcreteExtractor(object itemsSource)
		{
			DataSet dataSet = itemsSource as DataSet;
			if (dataSet != null)
			{
				DataTable table = dataSet.Tables[0];
				return new DataTableFieldDescriptionsExtractor(table);
			}
			DataTable dataTable = itemsSource as DataTable;
			if (dataTable != null)
			{
				return new DataTableFieldDescriptionsExtractor(dataTable);
			}
			IListSource listSource = itemsSource as IListSource;
			if (listSource != null)
			{
				return new EnumerableFieldDescriptionsExtractor(listSource.GetList());
			}
			IEnumerable enumerable = itemsSource as IEnumerable;
			if (enumerable != null)
			{
				return new EnumerableFieldDescriptionsExtractor(enumerable);
			}
			return new EnumerableFieldDescriptionsExtractor(new List<object>());
		}
	}
}
