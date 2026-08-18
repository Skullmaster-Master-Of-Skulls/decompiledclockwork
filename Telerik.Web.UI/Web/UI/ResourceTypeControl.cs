using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020012F3 RID: 4851
	internal class ResourceTypeControl : DataBoundControl
	{
		// Token: 0x0600CBC2 RID: 52162 RVA: 0x002D85E4 File Offset: 0x002D67E4
		public ResourceTypeControl(ResourceType resourceType)
		{
			this._resourceType = resourceType;
		}

		// Token: 0x170041B5 RID: 16821
		// (get) Token: 0x0600CBC3 RID: 52163 RVA: 0x002D85F3 File Offset: 0x002D67F3
		private RadScheduler Scheduler
		{
			get
			{
				return (RadScheduler)this.Parent;
			}
		}

		// Token: 0x0600CBC4 RID: 52164 RVA: 0x002D8600 File Offset: 0x002D6800
		protected override void PerformDataBinding(IEnumerable data)
		{
			base.PerformDataBinding(data);
			if (string.IsNullOrEmpty(this._resourceType.KeyField) || string.IsNullOrEmpty(this._resourceType.ForeignKeyField) || string.IsNullOrEmpty(this._resourceType.TextField))
			{
				throw new ArgumentException("KeyField, ForeignKeyField and TextField are required for databinding");
			}
			foreach (object obj in data)
			{
				object key = DataBinder.Eval(obj, this._resourceType.KeyField);
				string text = DataBinder.Eval(obj, this._resourceType.TextField).ToString();
				Resource resource = new Resource();
				resource.Key = key;
				resource.Text = text;
				resource.Type = this._resourceType.Name;
				resource.DataItem = obj;
				this.Scheduler.Resources.Add(resource);
			}
		}

		// Token: 0x0600CBC5 RID: 52165 RVA: 0x002D8700 File Offset: 0x002D6900
		protected override void Render(HtmlTextWriter writer)
		{
		}

		// Token: 0x0400357C RID: 13692
		private ResourceType _resourceType;
	}
}
