using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200176B RID: 5995
	internal class CustomShapeDesigner : ComponentDesigner, ITypeDescriptorContext, IServiceProvider, IWindowsFormsEditorService
	{
		// Token: 0x0600E9E5 RID: 59877 RVA: 0x00352F8C File Offset: 0x0035118C
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public CustomShapeDesigner()
		{
		}

		// Token: 0x170046F1 RID: 18161
		// (get) Token: 0x0600E9E6 RID: 59878 RVA: 0x00352F94 File Offset: 0x00351194
		public override DesignerVerbCollection Verbs
		{
			get
			{
				if (this._verbs == null)
				{
					this._verbs = new DesignerVerbCollection();
					this._verbs.Add(new DesignerVerb("Edit Points", new EventHandler(this.EditPointsVerb)));
				}
				return this._verbs;
			}
		}

		// Token: 0x0600E9E7 RID: 59879 RVA: 0x00352FD4 File Offset: 0x003511D4
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		internal void EditPointsVerb(object sender, EventArgs args)
		{
			DesignerActionUIService designerActionUIService = (DesignerActionUIService)this.GetService(typeof(DesignerActionUIService));
			if (designerActionUIService != null)
			{
				designerActionUIService.HideUI(base.Component);
			}
			CustomShape customShape = base.Component as CustomShape;
			CustomShapeEditorForm customShapeEditorForm = new CustomShapeEditorForm();
			if (customShape != null)
			{
				customShapeEditorForm.EditorControl.Dimension = customShape.Dimension;
				customShapeEditorForm.EditorControl.Points.AddRange(customShape.Points);
				if (customShapeEditorForm.ShowDialog() == DialogResult.OK)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					foreach (ShapePoint component in customShape.Points)
					{
						designerHost.DestroyComponent(component);
					}
					customShape.Points.Clear();
					foreach (ShapePoint shapePoint in customShapeEditorForm.EditorControl.Points)
					{
						ShapePoint shapePoint2 = (ShapePoint)designerHost.CreateComponent(typeof(ShapePoint));
						shapePoint2.X = shapePoint.X;
						shapePoint2.Y = shapePoint.Y;
						shapePoint2.ControlPoint1 = shapePoint.ControlPoint1;
						shapePoint2.ControlPoint2 = shapePoint.ControlPoint2;
						shapePoint2.Bezier = shapePoint.Bezier;
						shapePoint2.Locked = shapePoint.Locked;
						shapePoint2.Anchor = shapePoint.Anchor;
						customShape.Points.Add(shapePoint2);
					}
					customShape.Dimension = customShapeEditorForm.EditorControl.Dimension;
				}
			}
		}

		// Token: 0x0600E9E8 RID: 59880 RVA: 0x0035319C File Offset: 0x0035139C
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this._changeService = (IComponentChangeService)base.GetService(typeof(IComponentChangeService));
			this._changeService.ComponentRemoving += this.changeService_ComponentRemoving;
		}

		// Token: 0x0600E9E9 RID: 59881 RVA: 0x003531D7 File Offset: 0x003513D7
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._changeService.ComponentRemoving -= this.changeService_ComponentRemoving;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600E9EA RID: 59882 RVA: 0x003531FC File Offset: 0x003513FC
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		private void changeService_ComponentRemoving(object sender, ComponentEventArgs e)
		{
			if (e.Component == base.Component)
			{
				IDesignerHost designerHost = (IDesignerHost)base.GetService(typeof(IDesignerHost));
				if (base.Component != null)
				{
					foreach (ShapePoint component in (base.Component as CustomShape).Points)
					{
						designerHost.DestroyComponent(component);
					}
				}
			}
		}

		// Token: 0x170046F2 RID: 18162
		// (get) Token: 0x0600E9EB RID: 59883 RVA: 0x00353288 File Offset: 0x00351488
		public IContainer Container
		{
			get
			{
				return this.Container;
			}
		}

		// Token: 0x170046F3 RID: 18163
		// (get) Token: 0x0600E9EC RID: 59884 RVA: 0x00353290 File Offset: 0x00351490
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public object Instance
		{
			get
			{
				return base.Component;
			}
		}

		// Token: 0x0600E9ED RID: 59885 RVA: 0x00353298 File Offset: 0x00351498
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public void OnComponentChanged()
		{
			PropertyDescriptor member = TypeDescriptor.GetProperties(base.Component)["Points"];
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			componentChangeService.OnComponentChanged(base.Component, member, null, null);
		}

		// Token: 0x0600E9EE RID: 59886 RVA: 0x003532E0 File Offset: 0x003514E0
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public bool OnComponentChanging()
		{
			try
			{
				PropertyDescriptor member = TypeDescriptor.GetProperties(base.Component)["Points"];
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				componentChangeService.OnComponentChanging(base.Component, member);
			}
			catch (CheckoutException ex)
			{
				if (ex != CheckoutException.Canceled)
				{
					throw;
				}
				return false;
			}
			return true;
		}

		// Token: 0x170046F4 RID: 18164
		// (get) Token: 0x0600E9EF RID: 59887 RVA: 0x0035334C File Offset: 0x0035154C
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return TypeDescriptor.GetProperties(base.Component)["Points"];
			}
		}

		// Token: 0x0600E9F0 RID: 59888 RVA: 0x00353363 File Offset: 0x00351563
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		object IServiceProvider.GetService(Type serviceType)
		{
			if (serviceType == typeof(ITypeDescriptorContext) || serviceType == typeof(IWindowsFormsEditorService))
			{
				return this;
			}
			return base.GetService(serviceType);
		}

		// Token: 0x0600E9F1 RID: 59889 RVA: 0x00353392 File Offset: 0x00351592
		public void CloseDropDown()
		{
		}

		// Token: 0x0600E9F2 RID: 59890 RVA: 0x00353394 File Offset: 0x00351594
		public void DropDownControl(Control control)
		{
		}

		// Token: 0x0600E9F3 RID: 59891 RVA: 0x00353398 File Offset: 0x00351598
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public DialogResult ShowDialog(Form dialog)
		{
			IUIService iuiservice = (IUIService)this.GetService(typeof(IUIService));
			if (iuiservice != null)
			{
				return iuiservice.ShowDialog(dialog);
			}
			return dialog.ShowDialog(base.Component as IWin32Window);
		}

		// Token: 0x04004345 RID: 17221
		private IComponentChangeService _changeService;

		// Token: 0x04004346 RID: 17222
		private DesignerVerbCollection _verbs;
	}
}
