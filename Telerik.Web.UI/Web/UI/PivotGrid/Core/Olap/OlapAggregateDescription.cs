using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000CFA RID: 3322
	[DataContract]
	public class OlapAggregateDescription : AggregateDescriptionBase, IInitializeDescription, IDataFieldDescription
	{
		// Token: 0x06007BF0 RID: 31728 RVA: 0x001C7C5F File Offset: 0x001C5E5F
		internal OlapAggregateDescription()
		{
		}

		// Token: 0x1700279E RID: 10142
		// (get) Token: 0x06007BF1 RID: 31729 RVA: 0x001C7C67 File Offset: 0x001C5E67
		// (set) Token: 0x06007BF2 RID: 31730 RVA: 0x001C7C6F File Offset: 0x001C5E6F
		[DataMember]
		[SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Design choice.")]
		public string MemberName
		{
			get
			{
				return this.memberName;
			}
			set
			{
				if (this.memberName != value)
				{
					this.memberName = value;
					base.OnPropertyChanged("MemberName");
				}
			}
		}

		// Token: 0x1700279F RID: 10143
		// (get) Token: 0x06007BF3 RID: 31731 RVA: 0x001C7C91 File Offset: 0x001C5E91
		public override bool DisplayValueAsKpi
		{
			get
			{
				return this.FieldInfo != null && this.FieldInfo.DisplayValueAsKpi;
			}
		}

		// Token: 0x170027A0 RID: 10144
		// (get) Token: 0x06007BF4 RID: 31732 RVA: 0x001C7CA8 File Offset: 0x001C5EA8
		protected Type DataType
		{
			get
			{
				if (this.FieldInfo != null)
				{
					return this.FieldInfo.DataType;
				}
				return null;
			}
		}

		// Token: 0x170027A1 RID: 10145
		// (get) Token: 0x06007BF5 RID: 31733 RVA: 0x001C7CBF File Offset: 0x001C5EBF
		// (set) Token: 0x06007BF6 RID: 31734 RVA: 0x001C7CC7 File Offset: 0x001C5EC7
		internal OlapAggregateFieldInfo FieldInfo { get; set; }

		// Token: 0x06007BF7 RID: 31735 RVA: 0x001C7CD0 File Offset: 0x001C5ED0
		internal override IPivotFieldInfo GetFieldInfo()
		{
			return this.FieldInfo;
		}

		// Token: 0x06007BF8 RID: 31736 RVA: 0x001C7CD8 File Offset: 0x001C5ED8
		internal override RequiredField GetRequiredField()
		{
			return null;
		}

		// Token: 0x06007BF9 RID: 31737 RVA: 0x001C7CDC File Offset: 0x001C5EDC
		protected override string GetDisplayName()
		{
			string displayName = base.GetDisplayName();
			if (displayName != null)
			{
				return displayName;
			}
			if (this.FieldInfo != null && this.FieldInfo.DisplayName != null)
			{
				return this.FieldInfo.DisplayName;
			}
			return this.MemberName;
		}

		// Token: 0x06007BFA RID: 31738 RVA: 0x001C7D1C File Offset: 0x001C5F1C
		protected override Cloneable CreateInstanceCore()
		{
			return new OlapAggregateDescription();
		}

		// Token: 0x06007BFB RID: 31739 RVA: 0x001C7D24 File Offset: 0x001C5F24
		protected override void CloneCore(Cloneable source)
		{
			OlapAggregateDescription olapAggregateDescription = source as OlapAggregateDescription;
			if (olapAggregateDescription != null)
			{
				this.MemberName = olapAggregateDescription.MemberName;
				this.FieldInfo = olapAggregateDescription.FieldInfo;
			}
			base.CloneCore(source);
		}

		// Token: 0x06007BFC RID: 31740 RVA: 0x001C7D5A File Offset: 0x001C5F5A
		public override string GetUniqueName()
		{
			return this.MemberName;
		}

		// Token: 0x170027A2 RID: 10146
		// (get) Token: 0x06007BFD RID: 31741 RVA: 0x001C7D62 File Offset: 0x001C5F62
		bool IInitializeDescription.Initialized
		{
			get
			{
				return this.FieldInfo != null;
			}
		}

		// Token: 0x06007BFE RID: 31742 RVA: 0x001C7D70 File Offset: 0x001C5F70
		void IInitializeDescription.Initialize(IDataProvider provider)
		{
			if (provider == null)
			{
				return;
			}
			this.FieldInfo = (provider.FieldInfos.GetFieldDescriptionByMember(this.MemberName) as OlapAggregateFieldInfo);
		}

		// Token: 0x06007BFF RID: 31743 RVA: 0x001C7D92 File Offset: 0x001C5F92
		Type IDataFieldDescription.GetDataType()
		{
			return this.DataType;
		}

		// Token: 0x04002201 RID: 8705
		private string memberName;
	}
}
