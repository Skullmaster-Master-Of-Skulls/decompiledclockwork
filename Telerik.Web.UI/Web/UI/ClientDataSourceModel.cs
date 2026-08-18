using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000110 RID: 272
	[PersistChildren(true)]
	[DefaultProperty("Fields")]
	[ParseChildren(true, "Fields")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ClientDataSourceModel : StateManager
	{
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00027737 File Offset: 0x00025937
		internal bool ShouldSerializeFields
		{
			get
			{
				return this._fields != null && this._fields.Count > 0;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00027751 File Offset: 0x00025951
		internal bool ShouldSerializeID
		{
			get
			{
				return !string.IsNullOrEmpty(this.ID);
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00027761 File Offset: 0x00025961
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x00027781 File Offset: 0x00025981
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the name of ID field of the model")]
		[Category("Behavior")]
		public virtual string ID
		{
			get
			{
				return (base.ViewState["ModelID"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ModelID"] = value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00027794 File Offset: 0x00025994
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual ClientDataSourceModelFieldCollection Fields
		{
			get
			{
				if (this._fields == null)
				{
					this._fields = new ClientDataSourceModelFieldCollection();
				}
				return this._fields;
			}
		}

		// Token: 0x040002D4 RID: 724
		private ClientDataSourceModelFieldCollection _fields;
	}
}
