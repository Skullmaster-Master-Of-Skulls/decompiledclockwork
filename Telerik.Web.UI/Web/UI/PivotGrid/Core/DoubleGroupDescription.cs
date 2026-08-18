using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Groups;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CC9 RID: 3273
	[DataContract]
	public sealed class DoubleGroupDescription : PropertyGroupDescriptionBase, IDoubleGroupDescription
	{
		// Token: 0x06007A77 RID: 31351 RVA: 0x001C1884 File Offset: 0x001BFA84
		public DoubleGroupDescription()
		{
			this.Step = 100.0;
		}

		// Token: 0x17002753 RID: 10067
		// (get) Token: 0x06007A78 RID: 31352 RVA: 0x001C189B File Offset: 0x001BFA9B
		internal override bool TransformsData
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002754 RID: 10068
		// (get) Token: 0x06007A79 RID: 31353 RVA: 0x001C189E File Offset: 0x001BFA9E
		// (set) Token: 0x06007A7A RID: 31354 RVA: 0x001C18A6 File Offset: 0x001BFAA6
		[DefaultValue(100.0)]
		[DataMember]
		public double Step
		{
			get
			{
				return this.step;
			}
			set
			{
				if (this.step != value)
				{
					this.step = value;
					base.OnPropertyChanged("Step");
				}
			}
		}

		// Token: 0x06007A7B RID: 31355 RVA: 0x001C1C70 File Offset: 0x001BFE70
		protected internal override IEnumerable<object> GetAllNames(IEnumerable<object> uniqueNames, IEnumerable<object> parentGroupNames)
		{
			double start = double.NegativeInfinity;
			double end = double.PositiveInfinity;
			foreach (object obj in parentGroupNames)
			{
				if (obj is DoubleGroup)
				{
					DoubleGroup doubleGroup = (DoubleGroup)obj;
					start = Math.Max(start, doubleGroup.Start);
					end = Math.Min(end, doubleGroup.End);
				}
			}
			double itterationStart = start;
			double itterationEnd = end;
			if (itterationStart != double.NegativeInfinity && itterationEnd != double.PositiveInfinity)
			{
				double groupIndexOffset = itterationStart;
				int groupIndex = (int)(groupIndexOffset / this.step) + ((groupIndexOffset < 0.0) ? 1 : 0);
				for (;;)
				{
					double groupStart = this.step * (double)groupIndex;
					double groupEnd = this.step * (double)(groupIndex + 1);
					if (groupStart >= itterationEnd)
					{
						break;
					}
					yield return new DoubleGroup(groupStart, groupEnd);
					groupIndex++;
				}
			}
			else
			{
				foreach (object uniqueGroup in uniqueNames)
				{
					if (uniqueGroup is DoubleGroup)
					{
						DoubleGroup group = (DoubleGroup)uniqueGroup;
						if (group.Start != double.NegativeInfinity && group.End != double.PositiveInfinity)
						{
							yield return group;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06007A7C RID: 31356 RVA: 0x001C1C9C File Offset: 0x001BFE9C
		protected internal override object GroupNameFromItem(object item, int level)
		{
			object obj = base.GroupNameFromItem(item, level);
			if (obj == null)
			{
				return null;
			}
			return this.GetGroupNameForValidDouble(Convert.ToDouble(obj, base.Culture));
		}

		// Token: 0x06007A7D RID: 31357 RVA: 0x001C1CCC File Offset: 0x001BFECC
		private object GetGroupNameForValidDouble(double value)
		{
			int num = (int)Math.Floor(value / this.Step);
			double start = this.Step * (double)num;
			double end = this.Step * (double)(num + 1);
			return new DoubleGroup(start, end);
		}

		// Token: 0x06007A7E RID: 31358 RVA: 0x001C1D0A File Offset: 0x001BFF0A
		protected override Cloneable CreateInstanceCore()
		{
			return new DoubleGroupDescription();
		}

		// Token: 0x06007A7F RID: 31359 RVA: 0x001C1D14 File Offset: 0x001BFF14
		protected override void CloneOverride(Cloneable source)
		{
			DoubleGroupDescription doubleGroupDescription = source as DoubleGroupDescription;
			if (doubleGroupDescription != null)
			{
				this.Step = doubleGroupDescription.Step;
			}
		}

		// Token: 0x0400218C RID: 8588
		private double step;
	}
}
