using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Web.ModelBinding
{
	// Token: 0x0200063A RID: 1594
	public sealed class ControlValueProvider : SimpleValueProvider
	{
		// Token: 0x170016DA RID: 5850
		// (get) Token: 0x06004F10 RID: 20240 RVA: 0x00112EC5 File Offset: 0x001110C5
		// (set) Token: 0x06004F11 RID: 20241 RVA: 0x00112ECD File Offset: 0x001110CD
		public string PropertyName { get; private set; }

		// Token: 0x06004F12 RID: 20242 RVA: 0x00112ED6 File Offset: 0x001110D6
		public ControlValueProvider(ModelBindingExecutionContext modelBindingExecutionContext, string propertyName) : base(modelBindingExecutionContext)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x00112EE8 File Offset: 0x001110E8
		protected override object FetchValue(string controlId)
		{
			if (string.IsNullOrEmpty(controlId))
			{
				return null;
			}
			Control service = base.ModelBindingExecutionContext.GetService<Control>();
			string text = this.PropertyName;
			Control control = service.FindControl(controlId) ?? DataBoundControlHelper.FindControl(service, controlId);
			if (control == null)
			{
				return null;
			}
			ControlValuePropertyAttribute controlValuePropertyAttribute = (ControlValuePropertyAttribute)TypeDescriptor.GetAttributes(control)[typeof(ControlValuePropertyAttribute)];
			if (string.IsNullOrEmpty(text))
			{
				if (controlValuePropertyAttribute == null || string.IsNullOrEmpty(controlValuePropertyAttribute.Name))
				{
					return null;
				}
				text = controlValuePropertyAttribute.Name;
			}
			object obj = DataBinder.Eval(control, text);
			if (controlValuePropertyAttribute != null && controlValuePropertyAttribute.DefaultValue != null && controlValuePropertyAttribute.DefaultValue.Equals(obj))
			{
				return null;
			}
			return obj;
		}
	}
}
