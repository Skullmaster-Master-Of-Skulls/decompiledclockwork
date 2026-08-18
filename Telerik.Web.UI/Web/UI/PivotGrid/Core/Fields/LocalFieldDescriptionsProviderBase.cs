using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x02000CB9 RID: 3257
	public abstract class LocalFieldDescriptionsProviderBase : FieldDescriptionProviderBase
	{
		// Token: 0x1700273B RID: 10043
		// (get) Token: 0x060079D7 RID: 31191 RVA: 0x001BF5E2 File Offset: 0x001BD7E2
		// (set) Token: 0x060079D8 RID: 31192 RVA: 0x001BF5EA File Offset: 0x001BD7EA
		public object CurrentState { get; protected set; }

		// Token: 0x060079D9 RID: 31193
		protected abstract IFieldInfoData GenerateDescriptionsData();

		// Token: 0x060079DA RID: 31194 RVA: 0x001BF5F4 File Offset: 0x001BD7F4
		protected virtual ContainerNode GetFieldDescriptionHierarchy(IEnumerable<IPivotFieldInfo> fieldInfos)
		{
			if (fieldInfos == null)
			{
				throw new ArgumentNullException("fieldInfos");
			}
			ContainerNode containerNode = ContainerNode.CreateRootNode();
			foreach (IPivotFieldInfo pivotFieldInfo in fieldInfos)
			{
				Type nonNullableType = PivotTypeExtensions.GetNonNullableType(pivotFieldInfo.DataType);
				if (nonNullableType == typeof(DateTime) || nonNullableType == typeof(DateTimeOffset))
				{
					ContainerNode item = this.GenerateDateTimeFolderHierarchy(pivotFieldInfo);
					containerNode.Children.Add(item);
				}
				else
				{
					FieldInfoNode item2 = new FieldInfoNode(pivotFieldInfo);
					containerNode.Children.Add(item2);
				}
			}
			return containerNode;
		}

		// Token: 0x060079DB RID: 31195 RVA: 0x001BF6AC File Offset: 0x001BD8AC
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice.")]
		public override void GetDescriptionsDataAsync(object state)
		{
			Exception error = null;
			IFieldInfoData data = null;
			try
			{
				base.IsBusy = true;
				data = this.GenerateDescriptionsData();
			}
			catch (Exception ex)
			{
				error = ex;
				data = new EmptyFieldInfoData();
			}
			finally
			{
				this.OnDescriptionsDataCompleted(new GetDescriptionsDataCompletedEventArgs(error, this.CurrentState, data));
			}
		}

		// Token: 0x060079DC RID: 31196 RVA: 0x001BF70C File Offset: 0x001BD90C
		internal virtual ContainerNode GenerateDateTimeFolderHierarchy(IPivotFieldInfo fieldInfoItem)
		{
			ContainerNode containerNode = new ContainerNode(fieldInfoItem.DisplayName, ContainerNodeRole.Folder);
			PropertyFieldInfo propertyFieldInfo = fieldInfoItem as PropertyFieldInfo;
			if (propertyFieldInfo != null)
			{
				containerNode.Children.Add(LocalFieldDescriptionsProviderBase.GenerateDateTimeStep(propertyFieldInfo, DateTimeStep.Year, PivotLocalizationManager.YearGroupField, true));
				containerNode.Children.Add(LocalFieldDescriptionsProviderBase.GenerateDateTimeStep(propertyFieldInfo, DateTimeStep.Quarter, PivotLocalizationManager.QuarterGroupField, true));
				containerNode.Children.Add(LocalFieldDescriptionsProviderBase.GenerateDateTimeStep(propertyFieldInfo, DateTimeStep.Month, PivotLocalizationManager.MonthGroupField, true));
				containerNode.Children.Add(LocalFieldDescriptionsProviderBase.GenerateDateTimeStep(propertyFieldInfo, DateTimeStep.Week, PivotLocalizationManager.WeekGroupField, true));
				containerNode.Children.Add(LocalFieldDescriptionsProviderBase.GenerateDateTimeStep(propertyFieldInfo, DateTimeStep.Day, PivotLocalizationManager.DayGroupField, true));
				containerNode.Children.Add(LocalFieldDescriptionsProviderBase.GenerateDateTimeStep(propertyFieldInfo, DateTimeStep.Hour, PivotLocalizationManager.HourGroupField, false));
				containerNode.Children.Add(LocalFieldDescriptionsProviderBase.GenerateDateTimeStep(propertyFieldInfo, DateTimeStep.Minute, PivotLocalizationManager.MinuteGroupField, false));
				containerNode.Children.Add(LocalFieldDescriptionsProviderBase.GenerateDateTimeStep(propertyFieldInfo, DateTimeStep.Second, PivotLocalizationManager.SecondGroupField, false));
				FieldInfoNode item = new FieldInfoNode(propertyFieldInfo);
				containerNode.Children.Add(item);
			}
			return containerNode;
		}

		// Token: 0x060079DD RID: 31197 RVA: 0x001BF808 File Offset: 0x001BDA08
		internal static ContainerNode GenerateDateTimeStep(PropertyFieldInfo propertyFieldInfo, DateTimeStep step, string stringFormat, bool autoGenerate)
		{
			return new FieldInfoNode(new DateTimePropertyFieldInfo(propertyFieldInfo, step, propertyFieldInfo.Name)
			{
				Name = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
				{
					propertyFieldInfo.Name,
					step.ToString()
				}),
				DisplayName = string.Format(CultureInfo.InvariantCulture, stringFormat, new object[]
				{
					propertyFieldInfo.DisplayName
				}),
				PreferredRole = FieldRoles.Row,
				AllowedRoles = (FieldRoles.Row | FieldRoles.Column),
				AutoGenerateField = autoGenerate
			});
		}

		// Token: 0x060079DE RID: 31198 RVA: 0x001BF896 File Offset: 0x001BDA96
		protected override void OnDescriptionsDataCompleted(GetDescriptionsDataCompletedEventArgs args)
		{
			if (this.CurrentState == args.State)
			{
				this.ClearLocalState();
			}
			base.OnDescriptionsDataCompleted(args);
		}

		// Token: 0x060079DF RID: 31199 RVA: 0x001BF8B3 File Offset: 0x001BDAB3
		private void ClearLocalState()
		{
			this.CurrentState = null;
		}
	}
}
